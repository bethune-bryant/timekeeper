namespace TimeKeeper
{
    partial class frmWorking
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
            this.numAskAgain = new System.Windows.Forms.NumericUpDown();
            this.radioYes = new System.Windows.Forms.RadioButton();
            this.radioNew = new System.Windows.Forms.RadioButton();
            this.directorySearcher1 = new System.DirectoryServices.DirectorySearcher();
            this.radioNo = new System.Windows.Forms.RadioButton();
            this.lblAsk = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboNew = new System.Windows.Forms.ComboBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.lblComment = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numAskAgain)).BeginInit();
            this.SuspendLayout();
            // 
            // numAskAgain
            // 
            this.numAskAgain.Location = new System.Drawing.Point(86, 110);
            this.numAskAgain.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numAskAgain.Minimum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.numAskAgain.Name = "numAskAgain";
            this.numAskAgain.Size = new System.Drawing.Size(36, 20);
            this.numAskAgain.TabIndex = 6;
            this.numAskAgain.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.numAskAgain.KeyUp += new System.Windows.Forms.KeyEventHandler(this.all_KeyUp);
            // 
            // radioYes
            // 
            this.radioYes.AutoSize = true;
            this.radioYes.Location = new System.Drawing.Point(12, 12);
            this.radioYes.Name = "radioYes";
            this.radioYes.Size = new System.Drawing.Size(146, 17);
            this.radioYes.TabIndex = 1;
            this.radioYes.TabStop = true;
            this.radioYes.Text = "Yes, I\'m still working on ...";
            this.radioYes.UseVisualStyleBackColor = true;
            this.radioYes.CheckedChanged += new System.EventHandler(this.radioYes_CheckedChanged);
            this.radioYes.KeyUp += new System.Windows.Forms.KeyEventHandler(this.all_KeyUp);
            // 
            // radioNew
            // 
            this.radioNew.AutoSize = true;
            this.radioNew.Location = new System.Drawing.Point(12, 55);
            this.radioNew.Name = "radioNew";
            this.radioNew.Size = new System.Drawing.Size(139, 17);
            this.radioNew.TabIndex = 3;
            this.radioNew.TabStop = true;
            this.radioNew.Text = "No, now I\'m working on:";
            this.radioNew.UseVisualStyleBackColor = true;
            this.radioNew.KeyUp += new System.Windows.Forms.KeyEventHandler(this.all_KeyUp);
            // 
            // directorySearcher1
            // 
            this.directorySearcher1.ClientTimeout = System.TimeSpan.Parse("-00:00:01");
            this.directorySearcher1.ServerPageTimeLimit = System.TimeSpan.Parse("-00:00:01");
            this.directorySearcher1.ServerTimeLimit = System.TimeSpan.Parse("-00:00:01");
            // 
            // radioNo
            // 
            this.radioNo.AutoSize = true;
            this.radioNo.Location = new System.Drawing.Point(12, 78);
            this.radioNo.Name = "radioNo";
            this.radioNo.Size = new System.Drawing.Size(140, 17);
            this.radioNo.TabIndex = 5;
            this.radioNo.TabStop = true;
            this.radioNo.Text = "No, I\'m finished working.";
            this.radioNo.UseVisualStyleBackColor = true;
            this.radioNo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.all_KeyUp);
            // 
            // lblAsk
            // 
            this.lblAsk.AutoSize = true;
            this.lblAsk.Location = new System.Drawing.Point(12, 112);
            this.lblAsk.Name = "lblAsk";
            this.lblAsk.Size = new System.Drawing.Size(68, 13);
            this.lblAsk.TabIndex = 6;
            this.lblAsk.Text = "Ask again in:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(128, 112);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "minutes";
            // 
            // comboNew
            // 
            this.comboNew.FormattingEnabled = true;
            this.comboNew.Location = new System.Drawing.Point(157, 54);
            this.comboNew.Name = "comboNew";
            this.comboNew.Size = new System.Drawing.Size(171, 21);
            this.comboNew.TabIndex = 4;
            this.comboNew.SelectedIndexChanged += new System.EventHandler(this.comboNew_SelectedIndexChanged);
            this.comboNew.KeyUp += new System.Windows.Forms.KeyEventHandler(this.all_KeyUp);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(131, 149);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // txtComment
            // 
            this.txtComment.Location = new System.Drawing.Point(138, 29);
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(190, 20);
            this.txtComment.TabIndex = 2;
            this.txtComment.KeyUp += new System.Windows.Forms.KeyEventHandler(this.all_KeyUp);
            // 
            // lblComment
            // 
            this.lblComment.AutoSize = true;
            this.lblComment.Location = new System.Drawing.Point(34, 32);
            this.lblComment.Name = "lblComment";
            this.lblComment.Size = new System.Drawing.Size(98, 13);
            this.lblComment.TabIndex = 9;
            this.lblComment.Text = "Progress Comment:";
            // 
            // frmWorking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 184);
            this.ControlBox = false;
            this.Controls.Add(this.lblComment);
            this.Controls.Add(this.txtComment);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.comboNew);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblAsk);
            this.Controls.Add(this.radioNo);
            this.Controls.Add(this.radioNew);
            this.Controls.Add(this.radioYes);
            this.Controls.Add(this.numAskAgain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(364, 223);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(364, 223);
            this.Name = "frmWorking";
            this.Text = "Still Working?";
            this.Load += new System.EventHandler(this.frmWorking_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numAskAgain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numAskAgain;
        private System.Windows.Forms.RadioButton radioYes;
        private System.Windows.Forms.RadioButton radioNew;
        private System.DirectoryServices.DirectorySearcher directorySearcher1;
        private System.Windows.Forms.RadioButton radioNo;
        private System.Windows.Forms.Label lblAsk;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboNew;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.Label lblComment;
    }
}