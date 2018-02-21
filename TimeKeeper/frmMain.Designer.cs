namespace TimeKeeper
{
    partial class frmMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.dataThisWeek = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.weeklyEmployerReportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.monthlyEmployerReportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.dailyTaskReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.jiraLoginSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsFileLocationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.taskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.stillWorkingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.commonTasksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editCommonTasksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newCommonTaskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteACommonTaskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editACommonTaskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.recentTasksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showHideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recentTasksToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.timerKeyHooks = new System.Windows.Forms.Timer(this.components);
            this.timerSave = new System.Windows.Forms.Timer(this.components);
            this.saveFileDialogReport = new System.Windows.Forms.SaveFileDialog();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupThisWeek = new System.Windows.Forms.GroupBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupLastWeek = new System.Windows.Forms.GroupBox();
            this.dataLastWeek = new System.Windows.Forms.DataGridView();
            this.groupPrevious = new System.Windows.Forms.GroupBox();
            this.tabPrevious = new System.Windows.Forms.TabControl();
            this.timerWorking = new System.Windows.Forms.Timer(this.components);
            this.saveFileDialogSettings = new System.Windows.Forms.SaveFileDialog();
            this.hideOnCloseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            ((System.ComponentModel.ISupportInitialize)(this.dataThisWeek)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupThisWeek.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupLastWeek.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataLastWeek)).BeginInit();
            this.groupPrevious.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataThisWeek
            // 
            this.dataThisWeek.AllowUserToAddRows = false;
            this.dataThisWeek.AllowUserToDeleteRows = false;
            this.dataThisWeek.AllowUserToOrderColumns = true;
            this.dataThisWeek.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataThisWeek.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataThisWeek.Location = new System.Drawing.Point(3, 16);
            this.dataThisWeek.MultiSelect = false;
            this.dataThisWeek.Name = "dataThisWeek";
            this.dataThisWeek.ReadOnly = true;
            this.dataThisWeek.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataThisWeek.Size = new System.Drawing.Size(825, 357);
            this.dataThisWeek.TabIndex = 0;
            this.dataThisWeek.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataEntries_CellDoubleClick);
            this.dataThisWeek.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dataEntries_KeyUp);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.taskToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(831, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reportsToolStripMenuItem,
            this.toolStripSeparator4,
            this.settingsToolStripMenuItem,
            this.toolStripSeparator5,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // reportsToolStripMenuItem
            // 
            this.reportsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reportToolStripMenuItem,
            this.weeklyEmployerReportsToolStripMenuItem,
            this.monthlyEmployerReportsToolStripMenuItem,
            this.toolStripSeparator1,
            this.dailyTaskReportToolStripMenuItem});
            this.reportsToolStripMenuItem.Name = "reportsToolStripMenuItem";
            this.reportsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.reportsToolStripMenuItem.Text = "Reports";
            // 
            // reportToolStripMenuItem
            // 
            this.reportToolStripMenuItem.Name = "reportToolStripMenuItem";
            this.reportToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.reportToolStripMenuItem.Text = "Full Employer Reports:";
            // 
            // weeklyEmployerReportsToolStripMenuItem
            // 
            this.weeklyEmployerReportsToolStripMenuItem.Name = "weeklyEmployerReportsToolStripMenuItem";
            this.weeklyEmployerReportsToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.weeklyEmployerReportsToolStripMenuItem.Text = "Weekly Employer Reports";
            // 
            // monthlyEmployerReportsToolStripMenuItem
            // 
            this.monthlyEmployerReportsToolStripMenuItem.Name = "monthlyEmployerReportsToolStripMenuItem";
            this.monthlyEmployerReportsToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.monthlyEmployerReportsToolStripMenuItem.Text = "Monthly Employer Reports";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(212, 6);
            // 
            // dailyTaskReportToolStripMenuItem
            // 
            this.dailyTaskReportToolStripMenuItem.Name = "dailyTaskReportToolStripMenuItem";
            this.dailyTaskReportToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.dailyTaskReportToolStripMenuItem.Text = "Daily Task Report";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(149, 6);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hideOnCloseToolStripMenuItem,
            this.toolStripSeparator7,
            this.jiraLoginSettingsToolStripMenuItem,
            this.settingsFileLocationToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // jiraLoginSettingsToolStripMenuItem
            // 
            this.jiraLoginSettingsToolStripMenuItem.Name = "jiraLoginSettingsToolStripMenuItem";
            this.jiraLoginSettingsToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.jiraLoginSettingsToolStripMenuItem.Text = "Jira Login";
            this.jiraLoginSettingsToolStripMenuItem.Click += new System.EventHandler(this.jiraLoginSettingsToolStripMenuItem_Click);
            // 
            // settingsFileLocationToolStripMenuItem
            // 
            this.settingsFileLocationToolStripMenuItem.Name = "settingsFileLocationToolStripMenuItem";
            this.settingsFileLocationToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.settingsFileLocationToolStripMenuItem.Text = "Settings File Location";
            this.settingsFileLocationToolStripMenuItem.Click += new System.EventHandler(this.settingsFileLocationToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(149, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // taskToolStripMenuItem
            // 
            this.taskToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.closeToolStripMenuItem1,
            this.stillWorkingToolStripMenuItem,
            this.toolStripSeparator2,
            this.commonTasksToolStripMenuItem,
            this.editCommonTasksToolStripMenuItem,
            this.toolStripSeparator3,
            this.recentTasksToolStripMenuItem});
            this.taskToolStripMenuItem.Name = "taskToolStripMenuItem";
            this.taskToolStripMenuItem.Size = new System.Drawing.Size(42, 20);
            this.taskToolStripMenuItem.Text = "Task";
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.addToolStripMenuItem.Text = "Add";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem1
            // 
            this.closeToolStripMenuItem1.Name = "closeToolStripMenuItem1";
            this.closeToolStripMenuItem1.Size = new System.Drawing.Size(179, 22);
            this.closeToolStripMenuItem1.Text = "Close";
            this.closeToolStripMenuItem1.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // stillWorkingToolStripMenuItem
            // 
            this.stillWorkingToolStripMenuItem.Name = "stillWorkingToolStripMenuItem";
            this.stillWorkingToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.stillWorkingToolStripMenuItem.Text = "Still Working?";
            this.stillWorkingToolStripMenuItem.Click += new System.EventHandler(this.timerWorking_Tick);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(176, 6);
            // 
            // commonTasksToolStripMenuItem
            // 
            this.commonTasksToolStripMenuItem.Name = "commonTasksToolStripMenuItem";
            this.commonTasksToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.commonTasksToolStripMenuItem.Text = "Common Tasks";
            // 
            // editCommonTasksToolStripMenuItem
            // 
            this.editCommonTasksToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newCommonTaskToolStripMenuItem,
            this.deleteACommonTaskToolStripMenuItem,
            this.editACommonTaskToolStripMenuItem});
            this.editCommonTasksToolStripMenuItem.Name = "editCommonTasksToolStripMenuItem";
            this.editCommonTasksToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.editCommonTasksToolStripMenuItem.Text = "Edit Common Tasks";
            // 
            // newCommonTaskToolStripMenuItem
            // 
            this.newCommonTaskToolStripMenuItem.Name = "newCommonTaskToolStripMenuItem";
            this.newCommonTaskToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.newCommonTaskToolStripMenuItem.Text = "New Common Task";
            this.newCommonTaskToolStripMenuItem.Click += new System.EventHandler(this.newCommonTaskToolStripMenuItem_Click);
            // 
            // deleteACommonTaskToolStripMenuItem
            // 
            this.deleteACommonTaskToolStripMenuItem.Name = "deleteACommonTaskToolStripMenuItem";
            this.deleteACommonTaskToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.deleteACommonTaskToolStripMenuItem.Text = "Delete a Common Task";
            // 
            // editACommonTaskToolStripMenuItem
            // 
            this.editACommonTaskToolStripMenuItem.Name = "editACommonTaskToolStripMenuItem";
            this.editACommonTaskToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.editACommonTaskToolStripMenuItem.Text = "Edit a Common Task";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(176, 6);
            // 
            // recentTasksToolStripMenuItem
            // 
            this.recentTasksToolStripMenuItem.Name = "recentTasksToolStripMenuItem";
            this.recentTasksToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.recentTasksToolStripMenuItem.Text = "Recent Tasks";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 820);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(831, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(816, 17);
            this.lblStatus.Spring = true;
            this.lblStatus.Text = "Daily Time: 45 Weekly Time: Monthly Time:";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "TimeKeeper";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.BalloonTipClicked += new System.EventHandler(this.notifyIcon1_BalloonTipClicked);
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showHideToolStripMenuItem,
            this.recentTasksToolStripMenuItem1,
            this.toolStripSeparator6,
            this.exitToolStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 98);
            // 
            // showHideToolStripMenuItem
            // 
            this.showHideToolStripMenuItem.Name = "showHideToolStripMenuItem";
            this.showHideToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.showHideToolStripMenuItem.Text = "Show/Hide";
            this.showHideToolStripMenuItem.Click += new System.EventHandler(this.showHideToolStripMenuItem_Click);
            // 
            // recentTasksToolStripMenuItem1
            // 
            this.recentTasksToolStripMenuItem1.Name = "recentTasksToolStripMenuItem1";
            this.recentTasksToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.recentTasksToolStripMenuItem1.Text = "Recent Tasks";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(149, 6);
            // 
            // exitToolStripMenuItem1
            // 
            this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            this.exitToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem1.Text = "Exit";
            this.exitToolStripMenuItem1.Click += new System.EventHandler(this.iconExitToolStripMenuItem_Click);
            // 
            // timerKeyHooks
            // 
            this.timerKeyHooks.Tick += new System.EventHandler(this.timerKeyHooks_Tick);
            // 
            // timerSave
            // 
            this.timerSave.Interval = 5000;
            this.timerSave.Tick += new System.EventHandler(this.timerSave_Tick);
            // 
            // saveFileDialogReport
            // 
            this.saveFileDialogReport.DefaultExt = "csv";
            this.saveFileDialogReport.FileName = "report.csv";
            this.saveFileDialogReport.Filter = "Reports|*.csv";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupThisWeek);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(831, 796);
            this.splitContainer1.SplitterDistance = 376;
            this.splitContainer1.TabIndex = 3;
            // 
            // groupThisWeek
            // 
            this.groupThisWeek.Controls.Add(this.dataThisWeek);
            this.groupThisWeek.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupThisWeek.Location = new System.Drawing.Point(0, 0);
            this.groupThisWeek.Name = "groupThisWeek";
            this.groupThisWeek.Size = new System.Drawing.Size(831, 376);
            this.groupThisWeek.TabIndex = 1;
            this.groupThisWeek.TabStop = false;
            this.groupThisWeek.Text = "This Week:";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.groupLastWeek);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.groupPrevious);
            this.splitContainer2.Size = new System.Drawing.Size(831, 416);
            this.splitContainer2.SplitterDistance = 207;
            this.splitContainer2.TabIndex = 0;
            // 
            // groupLastWeek
            // 
            this.groupLastWeek.Controls.Add(this.dataLastWeek);
            this.groupLastWeek.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupLastWeek.Location = new System.Drawing.Point(0, 0);
            this.groupLastWeek.Name = "groupLastWeek";
            this.groupLastWeek.Size = new System.Drawing.Size(831, 207);
            this.groupLastWeek.TabIndex = 2;
            this.groupLastWeek.TabStop = false;
            this.groupLastWeek.Text = "Last Week:";
            // 
            // dataLastWeek
            // 
            this.dataLastWeek.AllowUserToAddRows = false;
            this.dataLastWeek.AllowUserToDeleteRows = false;
            this.dataLastWeek.AllowUserToOrderColumns = true;
            this.dataLastWeek.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataLastWeek.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataLastWeek.Location = new System.Drawing.Point(3, 16);
            this.dataLastWeek.MultiSelect = false;
            this.dataLastWeek.Name = "dataLastWeek";
            this.dataLastWeek.ReadOnly = true;
            this.dataLastWeek.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataLastWeek.Size = new System.Drawing.Size(825, 188);
            this.dataLastWeek.TabIndex = 0;
            this.dataLastWeek.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataLastWeek_CellDoubleClick);
            this.dataLastWeek.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dataLastWeek_KeyUp);
            // 
            // groupPrevious
            // 
            this.groupPrevious.Controls.Add(this.tabPrevious);
            this.groupPrevious.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupPrevious.Location = new System.Drawing.Point(0, 0);
            this.groupPrevious.Name = "groupPrevious";
            this.groupPrevious.Size = new System.Drawing.Size(831, 205);
            this.groupPrevious.TabIndex = 2;
            this.groupPrevious.TabStop = false;
            this.groupPrevious.Text = "By Month:";
            // 
            // tabPrevious
            // 
            this.tabPrevious.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPrevious.Location = new System.Drawing.Point(3, 16);
            this.tabPrevious.Name = "tabPrevious";
            this.tabPrevious.SelectedIndex = 0;
            this.tabPrevious.Size = new System.Drawing.Size(825, 186);
            this.tabPrevious.TabIndex = 1;
            // 
            // timerWorking
            // 
            this.timerWorking.Tick += new System.EventHandler(this.timerWorking_Tick);
            // 
            // saveFileDialogSettings
            // 
            this.saveFileDialogSettings.DefaultExt = "tkf";
            this.saveFileDialogSettings.FileName = "settings.tkf";
            this.saveFileDialogSettings.Filter = "Timekeeper Setting Files|*.tkf";
            // 
            // hideOnCloseToolStripMenuItem
            // 
            this.hideOnCloseToolStripMenuItem.Name = "hideOnCloseToolStripMenuItem";
            this.hideOnCloseToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.hideOnCloseToolStripMenuItem.Text = "Hide on Close";
            this.hideOnCloseToolStripMenuItem.Click += new System.EventHandler(this.hideOnCloseToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(183, 6);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(831, 842);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.Text = "Timekeeper";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Shown += new System.EventHandler(this.frmMain_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dataThisWeek)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupThisWeek.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.groupLastWeek.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataLastWeek)).EndInit();
            this.groupPrevious.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataThisWeek;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Timer timerKeyHooks;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem showHideToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem taskToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem1;
        private System.Windows.Forms.Timer timerSave;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupThisWeek;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.GroupBox groupLastWeek;
        private System.Windows.Forms.DataGridView dataLastWeek;
        private System.Windows.Forms.GroupBox groupPrevious;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem recentTasksToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recentTasksToolStripMenuItem1;
        private System.Windows.Forms.TabControl tabPrevious;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.Timer timerWorking;
        private System.Windows.Forms.ToolStripMenuItem reportsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem weeklyEmployerReportsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem dailyTaskReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem monthlyEmployerReportsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem jiraLoginSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsFileLocationToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialogSettings;
        private System.Windows.Forms.ToolStripMenuItem commonTasksToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editCommonTasksToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newCommonTaskToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteACommonTaskToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editACommonTaskToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stillWorkingToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem1;
        public System.Windows.Forms.SaveFileDialog saveFileDialogReport;
        private System.Windows.Forms.ToolStripMenuItem hideOnCloseToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
    }
}

