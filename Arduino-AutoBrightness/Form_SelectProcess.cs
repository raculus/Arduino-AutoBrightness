using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Arduino_AutoBrightness
{
    public partial class Form_SelectProcess : Form
    {
        
        readonly List<Process> processes = GetProcesses();

        public Form_SelectProcess()
        {
            InitializeComponent();
        }

        private void Form_SelectProcess_Load(object sender, EventArgs e)
        {
            foreach(var p in processes)
            {
                listBox_currentProcesses.Items.Add(p.MainWindowTitle);
            }
            var stopProcessStr = Properties.Settings.Default.stopProcessList;
            var stopProcessArr = stopProcessStr.Split('\n');
            foreach(var item in stopProcessArr)
            {
                if(item != "")
                    listBox_stopProcesses.Items.AddRange(stopProcessArr);
            }
        }

        static private List<Process> GetProcesses()
        {
            var list = new List<Process>();
            Process[] processes = Process.GetProcesses();
            foreach (Process p in processes)
            {
                if (p.MainWindowTitle != "")
                {
                    list.Add(p);
                }
            }
            return list;
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            if(listBox_currentProcesses.SelectedItem != null)
            {
                int idx = listBox_currentProcesses.SelectedIndex;
                var processName = processes[idx].ProcessName;
                if (!(listBox_stopProcesses.Items.Contains(processName)))
                {
                    listBox_stopProcesses.Items.Add(processName);
                }                
            }
        }

        private void button_del_Click(object sender, EventArgs e)
        {
            if(listBox_stopProcesses.SelectedItem != null)
            {
                listBox_stopProcesses.Items.Remove(listBox_stopProcesses.SelectedItem);
            }
        }

        private void Form_SelectProcess_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.stopProcessList = String.Join("\n",listBox_stopProcesses.Items.OfType<string>().ToArray());
        }
    }
}
