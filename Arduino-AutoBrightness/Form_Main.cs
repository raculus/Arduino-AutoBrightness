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
            var psi = new ProcessStartInfo();
            psi.FileName = Environment.GetEnvironmentVariable("LocalAppData") + @"\Microsoft\WindowsApps\Monitorian.exe";
            psi.Arguments = @"/set all "+brightness;
            Process.Start(psi);
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
                ChangeBrightness(bright);
                Debug.WriteLine("밝기: " + bright + "%");
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
                return;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
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
        }

        private void 열기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowInTaskbar = true;
            this.Visible= true;
            this.WindowState = FormWindowState.Normal;
        }

        private void 종료ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();

            if (serialPort.IsOpen)
            {
                serialPort.Close();
                Properties.Settings.Default.LastUsePort = serialPort.PortName;
                Properties.Settings.Default.Save();
            }
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
    }
}
