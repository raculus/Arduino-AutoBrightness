namespace Arduino_AutoBrightness
{
    partial class Form_SelectProcess
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listBox_currentProcesses = new System.Windows.Forms.ListBox();
            this.button_add = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.listBox_stopProcesses = new System.Windows.Forms.ListBox();
            this.button_del = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBox_currentProcesses
            // 
            this.listBox_currentProcesses.FormattingEnabled = true;
            this.listBox_currentProcesses.HorizontalScrollbar = true;
            this.listBox_currentProcesses.ItemHeight = 12;
            this.listBox_currentProcesses.Location = new System.Drawing.Point(6, 20);
            this.listBox_currentProcesses.Name = "listBox_currentProcesses";
            this.listBox_currentProcesses.Size = new System.Drawing.Size(183, 112);
            this.listBox_currentProcesses.TabIndex = 0;
            // 
            // button_add
            // 
            this.button_add.Location = new System.Drawing.Point(195, 20);
            this.button_add.Name = "button_add";
            this.button_add.Size = new System.Drawing.Size(75, 23);
            this.button_add.TabIndex = 1;
            this.button_add.Text = "추가";
            this.button_add.UseVisualStyleBackColor = true;
            this.button_add.Click += new System.EventHandler(this.button_add_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listBox_currentProcesses);
            this.groupBox1.Controls.Add(this.button_add);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(283, 145);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "현재 프로세스";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.listBox_stopProcesses);
            this.groupBox2.Controls.Add(this.button_del);
            this.groupBox2.Location = new System.Drawing.Point(12, 163);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(283, 145);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "일시정지할 프로세스";
            // 
            // listBox_stopProcesses
            // 
            this.listBox_stopProcesses.FormattingEnabled = true;
            this.listBox_stopProcesses.HorizontalScrollbar = true;
            this.listBox_stopProcesses.ItemHeight = 12;
            this.listBox_stopProcesses.Location = new System.Drawing.Point(6, 20);
            this.listBox_stopProcesses.Name = "listBox_stopProcesses";
            this.listBox_stopProcesses.Size = new System.Drawing.Size(183, 112);
            this.listBox_stopProcesses.TabIndex = 0;
            // 
            // button_del
            // 
            this.button_del.Location = new System.Drawing.Point(195, 20);
            this.button_del.Name = "button_del";
            this.button_del.Size = new System.Drawing.Size(75, 23);
            this.button_del.TabIndex = 1;
            this.button_del.Text = "제거";
            this.button_del.UseVisualStyleBackColor = true;
            this.button_del.Click += new System.EventHandler(this.button_del_Click);
            // 
            // Form_SelectProcess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(309, 320);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form_SelectProcess";
            this.Text = "프로세스 선택";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_SelectProcess_FormClosing);
            this.Load += new System.EventHandler(this.Form_SelectProcess_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBox_currentProcesses;
        private System.Windows.Forms.Button button_add;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox listBox_stopProcesses;
        private System.Windows.Forms.Button button_del;
    }
}