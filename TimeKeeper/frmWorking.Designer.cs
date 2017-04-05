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
            ((System.ComponentModel.ISupportInitialize)(this.numAskAgain)).BeginInit();
            this.SuspendLayout();
            // 
            // numAskAgain
            // 
            this.numAskAgain.Location = new System.Drawing.Point(86, 90);
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
            // 
            // radioNew
            // 
            this.radioNew.AutoSize = true;
            this.radioNew.Location = new System.Drawing.Point(12, 35);
            this.radioNew.Name = "radioNew";
            this.radioNew.Size = new System.Drawing.Size(139, 17);
            this.radioNew.TabIndex = 2;
            this.radioNew.TabStop = true;
            this.radioNew.Text = "No, now I\'m working on:";
            this.radioNew.UseVisualStyleBackColor = true;
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
            this.radioNo.Location = new System.Drawing.Point(12, 58);
            this.radioNo.Name = "radioNo";
            this.radioNo.Size = new System.Drawing.Size(140, 17);
            this.radioNo.TabIndex = 4;
            this.radioNo.TabStop = true;
            this.radioNo.Text = "No, I\'m finished working.";
            this.radioNo.UseVisualStyleBackColor = true;
            // 
            // lblAsk
            // 
            this.lblAsk.AutoSize = true;
            this.lblAsk.Location = new System.Drawing.Point(12, 92);
            this.lblAsk.Name = "lblAsk";
            this.lblAsk.Size = new System.Drawing.Size(68, 13);
            this.lblAsk.TabIndex = 6;
            this.lblAsk.Text = "Ask again in:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(128, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "minutes";
            // 
            // comboNew
            // 
            this.comboNew.FormattingEnabled = true;
            this.comboNew.Location = new System.Drawing.Point(157, 34);
            this.comboNew.Name = "comboNew";
            this.comboNew.Size = new System.Drawing.Size(171, 21);
            this.comboNew.TabIndex = 3;
            this.comboNew.SelectedIndexChanged += new System.EventHandler(this.comboNew_SelectedIndexChanged);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(147, 126);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // frmWorking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 161);
            this.ControlBox = false;
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
            this.MinimizeBox = false;
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
    }
}