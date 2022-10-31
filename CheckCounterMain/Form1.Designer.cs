namespace CheckCounterMain
{
    partial class fMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fMain));
            this.pnProgress = new System.Windows.Forms.Panel();
            this.pg1 = new System.Windows.Forms.ProgressBar();
            this.tbLog = new System.Windows.Forms.TextBox();
            this.pnVolumes = new System.Windows.Forms.Panel();
            this.lblVolumes = new System.Windows.Forms.Label();
            this.tbVolumesFilePath = new System.Windows.Forms.TextBox();
            this.btnVolumesFile = new System.Windows.Forms.Button();
            this.tbCountersFilePath = new System.Windows.Forms.TextBox();
            this.lblFile = new System.Windows.Forms.Label();
            this.cbReadVolumes = new System.Windows.Forms.CheckBox();
            this.pnResult = new System.Windows.Forms.Panel();
            this.tbCtrl = new System.Windows.Forms.TabControl();
            this.tbCheck = new System.Windows.Forms.TabPage();
            this.btnClear = new System.Windows.Forms.Button();
            this.dgvReport = new System.Windows.Forms.DataGridView();
            this.btnSave = new System.Windows.Forms.Button();
            this.tbSettings = new System.Windows.Forms.TabPage();
            this.btnSettings = new System.Windows.Forms.Button();
            this.dgvSettings = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.tbExceptions = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbErrors = new System.Windows.Forms.CheckedListBox();
            this.cbMajor = new System.Windows.Forms.CheckBox();
            this.tbGx = new System.Windows.Forms.TrackBar();
            this.lblGx = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbTy = new System.Windows.Forms.TrackBar();
            this.tbTz = new System.Windows.Forms.TrackBar();
            this.lblTy = new System.Windows.Forms.Label();
            this.lblTz = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_Process = new System.Windows.Forms.Button();
            this.btnChooseFolder = new System.Windows.Forms.Button();
            this.btnChooseCountersFile = new System.Windows.Forms.Button();
            this.pnProgress.SuspendLayout();
            this.pnVolumes.SuspendLayout();
            this.pnResult.SuspendLayout();
            this.tbCtrl.SuspendLayout();
            this.tbCheck.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).BeginInit();
            this.tbSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSettings)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbGx)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbTy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbTz)).BeginInit();
            this.SuspendLayout();
            // 
            // pnProgress
            // 
            this.pnProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnProgress.AutoSize = true;
            this.pnProgress.Controls.Add(this.pg1);
            this.pnProgress.Controls.Add(this.tbLog);
            this.pnProgress.Location = new System.Drawing.Point(8, 69);
            this.pnProgress.Name = "pnProgress";
            this.pnProgress.Size = new System.Drawing.Size(1290, 257);
            this.pnProgress.TabIndex = 6;
            this.pnProgress.Visible = false;
            // 
            // pg1
            // 
            this.pg1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pg1.Location = new System.Drawing.Point(4, 3);
            this.pg1.Name = "pg1";
            this.pg1.Size = new System.Drawing.Size(1276, 29);
            this.pg1.TabIndex = 5;
            // 
            // tbLog
            // 
            this.tbLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbLog.Location = new System.Drawing.Point(4, 38);
            this.tbLog.Multiline = true;
            this.tbLog.Name = "tbLog";
            this.tbLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbLog.Size = new System.Drawing.Size(1275, 212);
            this.tbLog.TabIndex = 0;
            this.tbLog.WordWrap = false;
            // 
            // pnVolumes
            // 
            this.pnVolumes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnVolumes.Controls.Add(this.lblVolumes);
            this.pnVolumes.Controls.Add(this.tbVolumesFilePath);
            this.pnVolumes.Controls.Add(this.btnVolumesFile);
            this.pnVolumes.Location = new System.Drawing.Point(9, 34);
            this.pnVolumes.Name = "pnVolumes";
            this.pnVolumes.Size = new System.Drawing.Size(956, 36);
            this.pnVolumes.TabIndex = 15;
            this.pnVolumes.Visible = false;
            // 
            // lblVolumes
            // 
            this.lblVolumes.AutoSize = true;
            this.lblVolumes.Location = new System.Drawing.Point(0, 6);
            this.lblVolumes.Name = "lblVolumes";
            this.lblVolumes.Size = new System.Drawing.Size(115, 20);
            this.lblVolumes.TabIndex = 8;
            this.lblVolumes.Text = "Файл объемов:";
            // 
            // tbVolumesFilePath
            // 
            this.tbVolumesFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbVolumesFilePath.Location = new System.Drawing.Point(142, 3);
            this.tbVolumesFilePath.Name = "tbVolumesFilePath";
            this.tbVolumesFilePath.Size = new System.Drawing.Size(672, 27);
            this.tbVolumesFilePath.TabIndex = 9;
            // 
            // btnVolumesFile
            // 
            this.btnVolumesFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVolumesFile.BackColor = System.Drawing.SystemColors.Info;
            this.btnVolumesFile.Location = new System.Drawing.Point(820, 2);
            this.btnVolumesFile.Name = "btnVolumesFile";
            this.btnVolumesFile.Size = new System.Drawing.Size(130, 30);
            this.btnVolumesFile.TabIndex = 10;
            this.btnVolumesFile.Text = "Выбрать файл...";
            this.btnVolumesFile.UseVisualStyleBackColor = false;
            this.btnVolumesFile.Click += new System.EventHandler(this.btnVolumesFile_Click);
            // 
            // tbCountersFilePath
            // 
            this.tbCountersFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCountersFilePath.Location = new System.Drawing.Point(151, 5);
            this.tbCountersFilePath.Name = "tbCountersFilePath";
            this.tbCountersFilePath.Size = new System.Drawing.Size(672, 27);
            this.tbCountersFilePath.TabIndex = 3;
            // 
            // lblFile
            // 
            this.lblFile.AutoSize = true;
            this.lblFile.Location = new System.Drawing.Point(12, 9);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(138, 20);
            this.lblFile.TabIndex = 7;
            this.lblFile.Text = "Файлы показаний:";
            // 
            // cbReadVolumes
            // 
            this.cbReadVolumes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbReadVolumes.AutoSize = true;
            this.cbReadVolumes.Location = new System.Drawing.Point(1103, 6);
            this.cbReadVolumes.Name = "cbReadVolumes";
            this.cbReadVolumes.Size = new System.Drawing.Size(195, 24);
            this.cbReadVolumes.TabIndex = 14;
            this.cbReadVolumes.Text = "Считывать дог. объемы";
            this.cbReadVolumes.UseVisualStyleBackColor = true;
            this.cbReadVolumes.CheckedChanged += new System.EventHandler(this.cbReadVolumes_CheckedChanged);
            // 
            // pnResult
            // 
            this.pnResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnResult.Controls.Add(this.tbCtrl);
            this.pnResult.Controls.Add(this.label3);
            this.pnResult.Controls.Add(this.tbExceptions);
            this.pnResult.Controls.Add(this.groupBox1);
            this.pnResult.Location = new System.Drawing.Point(9, 70);
            this.pnResult.Name = "pnResult";
            this.pnResult.Size = new System.Drawing.Size(1291, 812);
            this.pnResult.TabIndex = 11;
            this.pnResult.Visible = false;
            // 
            // tbCtrl
            // 
            this.tbCtrl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCtrl.Controls.Add(this.tbCheck);
            this.tbCtrl.Controls.Add(this.tbSettings);
            this.tbCtrl.Location = new System.Drawing.Point(3, 6);
            this.tbCtrl.Name = "tbCtrl";
            this.tbCtrl.SelectedIndex = 0;
            this.tbCtrl.Size = new System.Drawing.Size(963, 674);
            this.tbCtrl.TabIndex = 16;
            // 
            // tbCheck
            // 
            this.tbCheck.BackColor = System.Drawing.Color.White;
            this.tbCheck.Controls.Add(this.btnClear);
            this.tbCheck.Controls.Add(this.dgvReport);
            this.tbCheck.Controls.Add(this.btnSave);
            this.tbCheck.Location = new System.Drawing.Point(4, 29);
            this.tbCheck.Name = "tbCheck";
            this.tbCheck.Padding = new System.Windows.Forms.Padding(3);
            this.tbCheck.Size = new System.Drawing.Size(955, 641);
            this.tbCheck.TabIndex = 0;
            this.tbCheck.Text = "Отчет";
            this.tbCheck.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            this.btnClear.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnClear.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnClear.Location = new System.Drawing.Point(260, 604);
            this.btnClear.MaximumSize = new System.Drawing.Size(186, 30);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(186, 30);
            this.btnClear.TabIndex = 4;
            this.btnClear.Text = "Очистить";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // dgvReport
            // 
            this.dgvReport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvReport.BackgroundColor = System.Drawing.Color.White;
            this.dgvReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvReport.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvReport.Location = new System.Drawing.Point(6, 6);
            this.dgvReport.Name = "dgvReport";
            this.dgvReport.RowHeadersWidth = 51;
            this.dgvReport.RowTemplate.Height = 25;
            this.dgvReport.Size = new System.Drawing.Size(943, 592);
            this.dgvReport.TabIndex = 1;
            this.dgvReport.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvReport_CellClick);
            this.dgvReport.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvReport_DataBindingComplete);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnSave.Location = new System.Drawing.Point(452, 604);
            this.btnSave.MaximumSize = new System.Drawing.Size(186, 30);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(186, 30);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "Сохранить в файл...";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tbSettings
            // 
            this.tbSettings.BackColor = System.Drawing.Color.White;
            this.tbSettings.Controls.Add(this.btnSettings);
            this.tbSettings.Controls.Add(this.dgvSettings);
            this.tbSettings.Location = new System.Drawing.Point(4, 29);
            this.tbSettings.Name = "tbSettings";
            this.tbSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tbSettings.Size = new System.Drawing.Size(955, 641);
            this.tbSettings.TabIndex = 1;
            this.tbSettings.Text = "Настройки";
            this.tbSettings.UseVisualStyleBackColor = true;
            // 
            // btnSettings
            // 
            this.btnSettings.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnSettings.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnSettings.Location = new System.Drawing.Point(378, 606);
            this.btnSettings.MaximumSize = new System.Drawing.Size(186, 29);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(186, 29);
            this.btnSettings.TabIndex = 3;
            this.btnSettings.Text = "Сохранить настройки";
            this.btnSettings.UseVisualStyleBackColor = false;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // dgvSettings
            // 
            this.dgvSettings.AllowUserToAddRows = false;
            this.dgvSettings.AllowUserToDeleteRows = false;
            this.dgvSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSettings.BackgroundColor = System.Drawing.Color.White;
            this.dgvSettings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSettings.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvSettings.Location = new System.Drawing.Point(3, 3);
            this.dgvSettings.Name = "dgvSettings";
            this.dgvSettings.RowHeadersWidth = 51;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.White;
            this.dgvSettings.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvSettings.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.dgvSettings.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dgvSettings.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.White;
            this.dgvSettings.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvSettings.RowTemplate.Height = 29;
            this.dgvSettings.Size = new System.Drawing.Size(946, 597);
            this.dgvSettings.TabIndex = 2;
            this.dgvSettings.TabStop = false;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 683);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(150, 20);
            this.label3.TabIndex = 13;
            this.label3.Text = "Возникшие ошибки:";
            // 
            // tbExceptions
            // 
            this.tbExceptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbExceptions.Location = new System.Drawing.Point(3, 709);
            this.tbExceptions.Multiline = true;
            this.tbExceptions.Name = "tbExceptions";
            this.tbExceptions.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbExceptions.Size = new System.Drawing.Size(963, 100);
            this.tbExceptions.TabIndex = 12;
            this.tbExceptions.WordWrap = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.cbMajor);
            this.groupBox1.Controls.Add(this.tbGx);
            this.groupBox1.Controls.Add(this.lblGx);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.tbTy);
            this.groupBox1.Controls.Add(this.tbTz);
            this.groupBox1.Controls.Add(this.lblTy);
            this.groupBox1.Controls.Add(this.lblTz);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(972, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(316, 803);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Показывать ошибки";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lbErrors);
            this.groupBox2.Location = new System.Drawing.Point(0, 63);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(310, 564);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Из списка:";
            // 
            // lbErrors
            // 
            this.lbErrors.BackColor = System.Drawing.SystemColors.Control;
            this.lbErrors.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbErrors.CheckOnClick = true;
            this.lbErrors.FormattingEnabled = true;
            this.lbErrors.Location = new System.Drawing.Point(6, 26);
            this.lbErrors.Name = "lbErrors";
            this.lbErrors.Size = new System.Drawing.Size(291, 528);
            this.lbErrors.TabIndex = 16;
            this.lbErrors.SelectedIndexChanged += new System.EventHandler(this.lbErrors_SelectedIndexChanged);
            // 
            // cbMajor
            // 
            this.cbMajor.AutoSize = true;
            this.cbMajor.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.cbMajor.Location = new System.Drawing.Point(7, 33);
            this.cbMajor.Name = "cbMajor";
            this.cbMajor.Size = new System.Drawing.Size(145, 24);
            this.cbMajor.TabIndex = 17;
            this.cbMajor.Text = "Только важные";
            this.cbMajor.UseVisualStyleBackColor = true;
            this.cbMajor.CheckedChanged += new System.EventHandler(this.cbMajor_CheckedChanged);
            // 
            // tbGx
            // 
            this.tbGx.Location = new System.Drawing.Point(53, 728);
            this.tbGx.Name = "tbGx";
            this.tbGx.Size = new System.Drawing.Size(218, 56);
            this.tbGx.TabIndex = 11;
            this.tbGx.Value = 4;
            this.tbGx.Scroll += new System.EventHandler(this.tbGx_Scroll);
            // 
            // lblGx
            // 
            this.lblGx.AutoSize = true;
            this.lblGx.Location = new System.Drawing.Point(271, 737);
            this.lblGx.Name = "lblGx";
            this.lblGx.Size = new System.Drawing.Size(26, 20);
            this.lblGx.TabIndex = 13;
            this.lblGx.Text = "Gx";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 737);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(26, 20);
            this.label5.TabIndex = 12;
            this.label5.Text = "Gx";
            // 
            // tbTy
            // 
            this.tbTy.Location = new System.Drawing.Point(53, 683);
            this.tbTy.Name = "tbTy";
            this.tbTy.Size = new System.Drawing.Size(218, 56);
            this.tbTy.TabIndex = 6;
            this.tbTy.Value = 3;
            this.tbTy.Scroll += new System.EventHandler(this.tbTy_Scroll);
            // 
            // tbTz
            // 
            this.tbTz.Location = new System.Drawing.Point(53, 633);
            this.tbTz.Maximum = 50;
            this.tbTz.Name = "tbTz";
            this.tbTz.Size = new System.Drawing.Size(218, 56);
            this.tbTz.TabIndex = 4;
            this.tbTz.Value = 20;
            this.tbTz.Scroll += new System.EventHandler(this.tbTz_Scroll);
            // 
            // lblTy
            // 
            this.lblTy.AutoSize = true;
            this.lblTy.Location = new System.Drawing.Point(271, 694);
            this.lblTy.Name = "lblTy";
            this.lblTy.Size = new System.Drawing.Size(23, 20);
            this.lblTy.TabIndex = 9;
            this.lblTy.Text = "Ty";
            // 
            // lblTz
            // 
            this.lblTz.AutoSize = true;
            this.lblTz.Location = new System.Drawing.Point(271, 643);
            this.lblTz.Name = "lblTz";
            this.lblTz.Size = new System.Drawing.Size(23, 20);
            this.lblTz.TabIndex = 8;
            this.lblTz.Text = "Tz";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 694);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "Ty";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 643);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Tz";
            // 
            // btn_Process
            // 
            this.btn_Process.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Process.BackColor = System.Drawing.Color.PaleGreen;
            this.btn_Process.Location = new System.Drawing.Point(967, 35);
            this.btn_Process.Name = "btn_Process";
            this.btn_Process.Size = new System.Drawing.Size(331, 30);
            this.btn_Process.TabIndex = 13;
            this.btn_Process.Text = "Проверить";
            this.btn_Process.UseVisualStyleBackColor = false;
            this.btn_Process.Click += new System.EventHandler(this.btn_Process_Click);
            // 
            // btnChooseFolder
            // 
            this.btnChooseFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChooseFolder.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnChooseFolder.Location = new System.Drawing.Point(967, 3);
            this.btnChooseFolder.Name = "btnChooseFolder";
            this.btnChooseFolder.Size = new System.Drawing.Size(130, 30);
            this.btnChooseFolder.TabIndex = 12;
            this.btnChooseFolder.Text = "Выбрать папку...";
            this.btnChooseFolder.UseVisualStyleBackColor = false;
            this.btnChooseFolder.Click += new System.EventHandler(this.btnChooseFolder_Click);
            // 
            // btnChooseCountersFile
            // 
            this.btnChooseCountersFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChooseCountersFile.BackColor = System.Drawing.SystemColors.Info;
            this.btnChooseCountersFile.Location = new System.Drawing.Point(829, 2);
            this.btnChooseCountersFile.Name = "btnChooseCountersFile";
            this.btnChooseCountersFile.Size = new System.Drawing.Size(130, 30);
            this.btnChooseCountersFile.TabIndex = 4;
            this.btnChooseCountersFile.Text = "Выбрать файл...";
            this.btnChooseCountersFile.UseVisualStyleBackColor = false;
            this.btnChooseCountersFile.Click += new System.EventHandler(this.btnChooseCountersFile_Click);
            // 
            // fMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1312, 894);
            this.Controls.Add(this.cbReadVolumes);
            this.Controls.Add(this.btnChooseCountersFile);
            this.Controls.Add(this.btnChooseFolder);
            this.Controls.Add(this.lblFile);
            this.Controls.Add(this.btn_Process);
            this.Controls.Add(this.pnVolumes);
            this.Controls.Add(this.pnResult);
            this.Controls.Add(this.tbCountersFilePath);
            this.Controls.Add(this.pnProgress);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "fMain";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Проверка показаний счетчиков v2.6";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.fMain_FormClosing);
            this.Load += new System.EventHandler(this.fMain_Load);
            this.Shown += new System.EventHandler(this.fMain_Shown);
            this.pnProgress.ResumeLayout(false);
            this.pnProgress.PerformLayout();
            this.pnVolumes.ResumeLayout(false);
            this.pnVolumes.PerformLayout();
            this.pnResult.ResumeLayout(false);
            this.pnResult.PerformLayout();
            this.tbCtrl.ResumeLayout(false);
            this.tbCheck.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).EndInit();
            this.tbSettings.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSettings)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tbGx)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbTy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbTz)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel pnProgress;
        private ProgressBar pg1;
        private TextBox tbLog;
        private Panel pnVolumes;
        private Label lblVolumes;
        private TextBox tbVolumesFilePath;
        private Button btnVolumesFile;
        private TextBox tbCountersFilePath;
        private Label lblFile;
        private CheckBox cbReadVolumes;
        private Panel pnResult;
        private Label label3;
        private TextBox tbExceptions;
        private GroupBox groupBox1;
        private Button btnSave;
        private CheckedListBox lbErrors;
        private TrackBar tbGx;
        private Label lblGx;
        private Label label5;
        private TrackBar tbTy;
        private TrackBar tbTz;
        private Label lblTy;
        private Label lblTz;
        private Label label2;
        private Label label1;
        private DataGridView dgvReport;
        private Button btn_Process;
        private Button btnChooseFolder;
        private Button btnChooseCountersFile;
        private TabControl tbCtrl;
        private TabPage tbCheck;
        private TabPage tbSettings;
        private DataGridView dgvSettings;
        private Button btnSettings;
        private Button btnClear;
        private CheckBox cbMajor;
        private GroupBox groupBox2;
    }
}