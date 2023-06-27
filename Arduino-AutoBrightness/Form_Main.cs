using System;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Arduino_AutoBrightness
{
    public partial class Form_Main : Form
    {
        string[] processArr = { "" };
        bool toggle = false;
        static int prevBright;
        static int bright;
        int adjustBright = 0;  

        public Form_Main()
        {
            InitializeComponent();

            SerialDataReceivedEventHandler handler = serialPort_DataReceived;
            Arduino.AttachDataReceivedEvent(handler);
        }

        private void Connect(bool isShowErr)
        {
            if (comboBox_Port.SelectedItem == null)
            {
                return;
            }
            string portName = comboBox_Port.SelectedItem.ToString();

            try
            {
                Arduino.Connect(portName);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                if(isShowErr)
                    MessageBox.Show(ex.Message, "연결오류");
                Thread.Sleep(1000);
                Connect(false);

            }
            if (Arduino.IsConnected())
            {
                button_Connect.Text = "연결해제";
                comboBox_Port.Enabled = false;
            }
            else
            {
                button_Connect.Text = "연결";
                comboBox_Port.Enabled = true;
            }
        }

        private void button_Connect_Click(object sender, EventArgs e)
        {
            Connect(true);
        }

        private void ChangeBrightness(int brightness)
        {
            if(brightness < 0) brightness = 0;
            else if(brightness > 100) brightness = 100;

            Monitor.SetBrightness(brightness);
            Debug.WriteLine("밝기변경: " + brightness + "%");
            if (label_CurrentBright.InvokeRequired)
                label_CurrentBright.Invoke(new MethodInvoker(delegate { label_CurrentBright.Text = "밝기 " + brightness + "%"; }));
            else
                label_CurrentBright.Text = "밝기 " + brightness + "%";
        }

        private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                bright = Int32.Parse(Arduino.ReadLine());
                Debug.WriteLine("밝기: " + bright + "%");

                if (toggle)
                {
                    return;
                }
                //이전 밝기와 1~0차이일 경우 반영X
                if (bright - prevBright >= -1 && bright - prevBright <= 1)
                {
                    return;
                }
                Process fgProc = ProcessUtils.getForegroundProcess();
                if (!(processArr.Contains(fgProc.ProcessName)))
                {
                    ChangeBrightness(bright + adjustBright);
                    prevBright = bright;
                }

                if (trackBar_adjustBright.InvokeRequired)
                {
                    this.trackBar_adjustBright.Invoke(new MethodInvoker(delegate { trackBar_adjustBright.Enabled = true; }));
                }
                else
                    trackBar_adjustBright.Enabled = true;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
                return;
            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Arduino.IsConnected())
            {
                Properties.Settings.Default.LastUsePort = Arduino.serialPort.PortName;
            }
            Properties.Settings.Default.LastBrightAdjust = trackBar_adjustBright.Value;
            Properties.Settings.Default.Save();

            e.Cancel = true;
            this.Visible = false;
        }


        private void comboBox_Port_Click(object sender, EventArgs e)
        {
            comboBox_Port.Items.Clear();
            foreach (var item in SerialPort.GetPortNames())
            {
                if(item != "COM1")
                    comboBox_Port.Items.Add(item);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Visible = false;
            string lastUsePort = Properties.Settings.Default.LastUsePort;
            comboBox_Port.Items.Add(lastUsePort);
            comboBox_Port.SelectedIndex = 0;
            Connect(false);
            notifyIcon.ContextMenuStrip = contextMenuStrip;
            trackBar_adjustBright.Value = Properties.Settings.Default.LastBrightAdjust;
            processArr = Properties.Settings.Default.stopProcessList.Split('\n');
        }

        private void 열기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowInTaskbar = true;
            this.Visible= true;
            this.WindowState = FormWindowState.Normal;
        }

        private void 종료ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Arduino.IsConnected())
            {
                Arduino.Disconnect();
                Properties.Settings.Default.LastUsePort = Arduino.serialPort.PortName;
            }
            Properties.Settings.Default.LastBrightAdjust = trackBar_adjustBright.Value;
            Properties.Settings.Default.Save();
            Environment.Exit(0);
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            열기ToolStripMenuItem_Click(sender, e);
        }

        private void 일시정지ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toggle = !toggle;
            일시정지ToolStripMenuItem.Checked= toggle;
        }

        private void trackBar_adjustBright_Scroll(object sender, EventArgs e)
        {
            adjustBright = trackBar_adjustBright.Value;
        }

        private void button_selectProcesses_Click(object sender, EventArgs e)
        {
            Form_SelectProcess form_SelectProcess = new Form_SelectProcess();
            form_SelectProcess.FormClosed += new FormClosedEventHandler(Form_SelectProcess_Closed);
            form_SelectProcess.ShowDialog();
        }

        private void Form_SelectProcess_Closed(object sender, FormClosedEventArgs e)
        {
            processArr = Properties.Settings.Default.stopProcessList.Split('\n');
        }

        private void trackBar_adjustBright_MouseUp(object sender, MouseEventArgs e)
        {
            ChangeBrightness(bright + adjustBright);
        }
    }
}
