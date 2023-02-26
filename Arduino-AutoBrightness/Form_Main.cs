using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Arduino_AutoBrightness
{
    public partial class Form_Main : Form
    {
        bool toggle = false;
        static int prevBright = -1;
        static int bright;
        int adjustBright = 0;
        static SerialPort serialPort = new SerialPort();
        public Form_Main()
        {
            InitializeComponent();
        }


        private void button_Connect_Click(object sender, EventArgs e)
        {
            if(comboBox_Port.SelectedItem == null) {

                return;
            }
            try
            {
                if(serialPort.IsOpen)
                {
                    serialPort.Close();
                }
                else
                {
                    serialPort.PortName = comboBox_Port.SelectedItem.ToString();
                    serialPort.BaudRate = 9600;
                    serialPort.DataBits = 8;
                    serialPort.StopBits = StopBits.One;
                    serialPort.Parity = Parity.None;
                    serialPort.DataReceived += serialPort_DataReceived;
                    serialPort.Open();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "연결오류");
            }
            if (serialPort.IsOpen)
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

        private void ChangeBrightness(int brightness)
        {
            if(brightness < 0) brightness = 0;
            else if(brightness > 100) brightness = 100;

            var psi = new ProcessStartInfo();
            psi.FileName = Environment.GetEnvironmentVariable("LocalAppData") + @"\Microsoft\WindowsApps\Monitorian.exe";
            psi.Arguments = @"/set all "+brightness;
            Process.Start(psi);
            Debug.WriteLine("밝기: " + brightness + "%");
            if (label_CurrentBright.InvokeRequired)
            {
                this.label_CurrentBright.Invoke(new MethodInvoker(delegate { label_CurrentBright.Text = "밝기 " + brightness + "%"; }));
            }
            else
                label_CurrentBright.Text = "밝기 " + brightness + "%";
        }

        private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (toggle)
            {
                return;
            }
            try
            {
                bright = Int32.Parse(serialPort.ReadLine());
                if(bright - prevBright <= 1 && bright - prevBright <= -1)
                {
                    return;
                }
                ChangeBrightness(bright+adjustBright);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
                return;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serialPort.IsOpen)
            {
                serialPort.Close();
                Properties.Settings.Default.LastUsePort = serialPort.PortName;
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
                comboBox_Port.Items.Add(item);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Visible = false;
            string lastUsePort = Properties.Settings.Default.LastUsePort;
            comboBox_Port.Items.Add(lastUsePort);
            comboBox_Port.SelectedIndex = 0;
            button_Connect_Click(sender, e);
            notifyIcon.ContextMenuStrip = contextMenuStrip;
            trackBar_adjustBright.Value = Properties.Settings.Default.LastBrightAdjust;
        }

        private void 열기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowInTaskbar = true;
            this.Visible= true;
            this.WindowState = FormWindowState.Normal;
        }

        private void 종료ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
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
            ChangeBrightness(bright + trackBar_adjustBright.Value);
        }
    }
}
