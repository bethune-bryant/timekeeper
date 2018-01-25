namespace TimeKeeper
{
    partial class frmTimeEntry
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.comboProject = new System.Windows.Forms.ComboBox();
            this.comboTask = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboEmployer = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dateTimePickerStart = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dateTimePickerStop = new System.Windows.Forms.DateTimePicker();
            this.txtComments = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comboJiraTask = new System.Windows.Forms.ComboBox();
            this.lblJiraTask = new System.Windows.Forms.Label();
            this.btnStartAtLast = new System.Windows.Forms.PictureBox();
            this.btnStopAtNow = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.btnStartAtLast)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnStopAtNow)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(340, 143);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(421, 143);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Project:";
            // 
            // comboProject
            // 
            this.comboProject.FormattingEnabled = true;
            this.comboProject.Location = new System.Drawing.Point(77, 12);
            this.comboProject.Name = "comboProject";
            this.comboProject.Size = new System.Drawing.Size(146, 21);
            this.comboProject.TabIndex = 0;
            this.comboProject.KeyUp += new System.Windows.Forms.KeyEventHandler(this.all_KeyUp);
            // 
            // comboTask
            // 
            this.comboTask.FormattingEnabled = true;
            this.comboTask.Location = new System.Drawing.Point(269, 12);
            this.comboTask.Name = "comboTask";
            this.comboTask.Size = new System.Drawing.Size(146, 21);
            this.comboTask.TabIndex = 1;
            this.comboTask.KeyUp += new System.Windows.Forms.KeyEventHandler(this.all_KeyUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(229, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Task:";
            // 
            // comboEmployer
            // 
            this.comboEmployer.FormattingEnabled = true;
            this.comboEmployer.Location = new System.Drawing.Point(480, 12);
            this.comboEmployer.Name = "comboEmployer";
            this.comboEmployer.Size = new System.Drawing.Size(146, 21);
            this.comboEmployer.TabIndex = 2;
            this.comboEmployer.KeyUp += new System.Windows.Forms.KeyEventHandler(this.all_KeyUp);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(421, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Employer:";
            // 
            // dateTimePickerStart
            // 
            this.dateTimePickerStart.CustomFormat = "hh:mm tt dddd MMMM dd, yyyy";
            this.dateTimePickerStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerStart.Location = new System.Drawing.Point(77, 117);
            this.dateTimePickerStart.Name = "dateTimePickerStart";
            this.dateTimePickerStart.Size = new System.Drawing.Size(338, 20);
            this.dateTimePickerStart.TabIndex = 5;
            this.dateTimePickerStart.KeyUp += new System.Windows.Forms.KeyEventHandler(this.all_KeyUp);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(41, 123);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Start:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(421, 123);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Stop:";
            // 
            // dateTimePickerStop
            // 
            this.dateTimePickerStop.CustomFormat = "hh:mm tt dddd MMMM dd, yyyy";
            this.dateTimePickerStop.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerStop.Location = new System.Drawing.Point(459, 117);
            this.dateTimePickerStop.Name = "dateTimePickerStop";
            this.dateTimePickerStop.Size = new System.Drawing.Size(351, 20);
            this.dateTimePickerStop.TabIndex = 6;
            this.dateTimePickerStop.KeyUp += new System.Windows.Forms.KeyEventHandler(this.all_KeyUp);
            // 
            // txtComments
            // 
            this.txtComments.AcceptsReturn = true;
            this.txtComments.Location = new System.Drawing.Point(77, 39);
            this.txtComments.Multiline = true;
            this.txtComments.Name = "txtComments";
            this.txtComments.Size = new System.Drawing.Size(760, 72);
            this.txtComments.TabIndex = 3;
            this.txtComments.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtComments_KeyUp);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 39);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Comments:";
            // 
            // comboJiraTask
            // 
            this.comboJiraTask.FormattingEnabled = true;
            this.comboJiraTask.Location = new System.Drawing.Point(691, 12);
            this.comboJiraTask.Name = "comboJiraTask";
            this.comboJiraTask.Size = new System.Drawing.Size(146, 21);
            this.comboJiraTask.TabIndex = 4;
            this.comboJiraTask.SelectedIndexChanged += new System.EventHandler(this.comboJiraTask_SelectedIndexChanged);
            // 
            // lblJiraTask
            // 
            this.lblJiraTask.AutoSize = true;
            this.lblJiraTask.Location = new System.Drawing.Point(632, 15);
            this.lblJiraTask.Name = "lblJiraTask";
            this.lblJiraTask.Size = new System.Drawing.Size(53, 13);
            this.lblJiraTask.TabIndex = 15;
            this.lblJiraTask.Text = "Jira Task:";
            // 
            // btnStartAtLast
            // 
            this.btnStartAtLast.Image = global::TimeKeeper.Properties.Resources.go_first;
            this.btnStartAtLast.Location = new System.Drawing.Point(15, 117);
            this.btnStartAtLast.Name = "btnStartAtLast";
            this.btnStartAtLast.Size = new System.Drawing.Size(20, 20);
            this.btnStartAtLast.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnStartAtLast.TabIndex = 17;
            this.btnStartAtLast.TabStop = false;
            this.btnStartAtLast.Click += new System.EventHandler(this.btnStartAtLast_Click);
            // 
            // btnStopAtNow
            // 
            this.btnStopAtNow.Image = global::TimeKeeper.Properties.Resources.go_first;
            this.btnStopAtNow.Location = new System.Drawing.Point(816, 117);
            this.btnStopAtNow.Name = "btnStopAtNow";
            this.btnStopAtNow.Size = new System.Drawing.Size(20, 20);
            this.btnStopAtNow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnStopAtNow.TabIndex = 18;
            this.btnStopAtNow.TabStop = false;
            this.btnStopAtNow.Click += new System.EventHandler(this.btnStopAtNow_Click);
            // 
            // frmTimeEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(848, 174);
            this.ControlBox = false;
            this.Controls.Add(this.btnStopAtNow);
            this.Controls.Add(this.btnStartAtLast);
            this.Controls.Add(this.comboJiraTask);
            this.Controls.Add(this.lblJiraTask);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtComments);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dateTimePickerStop);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dateTimePickerStart);
            this.Controls.Add(this.comboEmployer);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboTask);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboProject);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTimeEntry";
            this.Text = "Time Entry";
            this.Load += new System.EventHandler(this.frmTimeEntry_Load);
            this.Shown += new System.EventHandler(this.frmTimeEntry_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.btnStartAtLast)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnStopAtNow)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboProject;
        private System.Windows.Forms.ComboBox comboTask;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboEmployer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateTimePickerStart;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dateTimePickerStop;
        private System.Windows.Forms.TextBox txtComments;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboJiraTask;
        private System.Windows.Forms.Label lblJiraTask;
        private System.Windows.Forms.PictureBox btnStartAtLast;
        private System.Windows.Forms.PictureBox btnStopAtNow;
    }
}