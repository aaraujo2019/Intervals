namespace Intercept_Intervals.UI
{
    partial class frm_Intercept_Intervals
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Intercept_Intervals));
            this.LblTitulos = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.grpValueResult = new System.Windows.Forms.GroupBox();
            this.dtgValueCalculate = new System.Windows.Forms.DataGridView();
            this.jobno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dhid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.from = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.to = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Length_Grade = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Au_Grade = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ag_Grade = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalRegister = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Vn_mod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SKDHSamples = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpExportWell = new System.Windows.Forms.GroupBox();
            this.dtgDetailHoleID = new System.Windows.Forms.DataGridView();
            this.chkSelection = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Vn_mod_codes = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.grpComments = new System.Windows.Forms.GroupBox();
            this.txtComent = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.cbxHole = new System.Windows.Forms.ComboBox();
            this.btnLock = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnRefresch = new System.Windows.Forms.Button();
            this.lblSavedItems = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.grpValueResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgValueCalculate)).BeginInit();
            this.grpExportWell.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgDetailHoleID)).BeginInit();
            this.grpComments.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // LblTitulos
            // 
            this.LblTitulos.BackColor = System.Drawing.Color.Transparent;
            this.LblTitulos.Font = new System.Drawing.Font("Segoe UI Symbol", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTitulos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(176)))), ((int)(((byte)(138)))), ((int)(((byte)(22)))));
            this.LblTitulos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.LblTitulos.Location = new System.Drawing.Point(8, 9);
            this.LblTitulos.Name = "LblTitulos";
            this.LblTitulos.Size = new System.Drawing.Size(310, 49);
            this.LblTitulos.TabIndex = 90;
            this.LblTitulos.Text = "Intercept Intervals";
            this.LblTitulos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::Intercept_Intervals.Properties.Resources.grancolo;
            this.pictureBox4.Location = new System.Drawing.Point(270, 2);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(57, 37);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox4.TabIndex = 150;
            this.pictureBox4.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.grpValueResult, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.grpExportWell, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.grpComments, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblSavedItems, 1, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 55);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.1F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 77.72512F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.320695F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1240, 633);
            this.tableLayoutPanel1.TabIndex = 152;
            // 
            // grpValueResult
            // 
            this.grpValueResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpValueResult.Controls.Add(this.dtgValueCalculate);
            this.grpValueResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpValueResult.ForeColor = System.Drawing.SystemColors.Highlight;
            this.grpValueResult.Location = new System.Drawing.Point(623, 85);
            this.grpValueResult.Name = "grpValueResult";
            this.grpValueResult.Size = new System.Drawing.Size(614, 485);
            this.grpValueResult.TabIndex = 149;
            this.grpValueResult.TabStop = false;
            this.grpValueResult.Text = "Calculated Results";
            // 
            // dtgValueCalculate
            // 
            this.dtgValueCalculate.AllowUserToAddRows = false;
            this.dtgValueCalculate.AllowUserToDeleteRows = false;
            this.dtgValueCalculate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgValueCalculate.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dtgValueCalculate.BackgroundColor = System.Drawing.Color.White;
            this.dtgValueCalculate.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgValueCalculate.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dtgValueCalculate.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgValueCalculate.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.jobno,
            this.dhid,
            this.from,
            this.to,
            this.Length_Grade,
            this.Au_Grade,
            this.Ag_Grade,
            this.TotalRegister,
            this.Vn_mod,
            this.SKDHSamples});
            this.dtgValueCalculate.Location = new System.Drawing.Point(3, 17);
            this.dtgValueCalculate.Name = "dtgValueCalculate";
            this.dtgValueCalculate.ReadOnly = true;
            this.dtgValueCalculate.RowHeadersWidth = 30;
            this.dtgValueCalculate.Size = new System.Drawing.Size(608, 465);
            this.dtgValueCalculate.TabIndex = 112;
            // 
            // jobno
            // 
            this.jobno.DataPropertyName = "jobno";
            this.jobno.HeaderText = "jobno";
            this.jobno.Name = "jobno";
            this.jobno.ReadOnly = true;
            this.jobno.Width = 63;
            // 
            // dhid
            // 
            this.dhid.DataPropertyName = "dhid";
            this.dhid.HeaderText = "dhid";
            this.dhid.Name = "dhid";
            this.dhid.ReadOnly = true;
            this.dhid.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dhid.Width = 56;
            // 
            // from
            // 
            this.from.DataPropertyName = "from";
            this.from.HeaderText = "from";
            this.from.Name = "from";
            this.from.ReadOnly = true;
            this.from.Width = 57;
            // 
            // to
            // 
            this.to.DataPropertyName = "to";
            this.to.HeaderText = "to";
            this.to.Name = "to";
            this.to.ReadOnly = true;
            this.to.Width = 42;
            // 
            // Length_Grade
            // 
            this.Length_Grade.DataPropertyName = "LENGTH";
            this.Length_Grade.HeaderText = "Length_Grade";
            this.Length_Grade.Name = "Length_Grade";
            this.Length_Grade.ReadOnly = true;
            this.Length_Grade.Width = 111;
            // 
            // Au_Grade
            // 
            this.Au_Grade.HeaderText = "Au_Grade";
            this.Au_Grade.Name = "Au_Grade";
            this.Au_Grade.ReadOnly = true;
            this.Au_Grade.Width = 87;
            // 
            // Ag_Grade
            // 
            this.Ag_Grade.HeaderText = "Ag_Grade";
            this.Ag_Grade.Name = "Ag_Grade";
            this.Ag_Grade.ReadOnly = true;
            this.Ag_Grade.Width = 87;
            // 
            // TotalRegister
            // 
            this.TotalRegister.HeaderText = "TotalRegister";
            this.TotalRegister.Name = "TotalRegister";
            this.TotalRegister.ReadOnly = true;
            this.TotalRegister.Width = 105;
            // 
            // Vn_mod
            // 
            this.Vn_mod.DataPropertyName = "Vn_mod";
            this.Vn_mod.HeaderText = "Vn_mod";
            this.Vn_mod.Name = "Vn_mod";
            this.Vn_mod.ReadOnly = true;
            this.Vn_mod.Width = 78;
            // 
            // SKDHSamples
            // 
            this.SKDHSamples.HeaderText = "SKDHSamples";
            this.SKDHSamples.Name = "SKDHSamples";
            this.SKDHSamples.ReadOnly = true;
            this.SKDHSamples.Visible = false;
            // 
            // grpExportWell
            // 
            this.grpExportWell.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpExportWell.Controls.Add(this.dtgDetailHoleID);
            this.grpExportWell.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpExportWell.ForeColor = System.Drawing.SystemColors.Highlight;
            this.grpExportWell.Location = new System.Drawing.Point(3, 85);
            this.grpExportWell.Name = "grpExportWell";
            this.grpExportWell.Size = new System.Drawing.Size(614, 485);
            this.grpExportWell.TabIndex = 148;
            this.grpExportWell.TabStop = false;
            this.grpExportWell.Text = "Select Vn_mod";
            // 
            // dtgDetailHoleID
            // 
            this.dtgDetailHoleID.AllowUserToAddRows = false;
            this.dtgDetailHoleID.AllowUserToDeleteRows = false;
            this.dtgDetailHoleID.BackgroundColor = System.Drawing.Color.White;
            this.dtgDetailHoleID.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgDetailHoleID.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dtgDetailHoleID.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgDetailHoleID.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chkSelection,
            this.Vn_mod_codes});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtgDetailHoleID.DefaultCellStyle = dataGridViewCellStyle3;
            this.dtgDetailHoleID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgDetailHoleID.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dtgDetailHoleID.Location = new System.Drawing.Point(3, 17);
            this.dtgDetailHoleID.Name = "dtgDetailHoleID";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgDetailHoleID.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dtgDetailHoleID.RowHeadersWidth = 30;
            this.dtgDetailHoleID.Size = new System.Drawing.Size(608, 465);
            this.dtgDetailHoleID.TabIndex = 112;
            this.dtgDetailHoleID.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellContentClick);
            // 
            // chkSelection
            // 
            this.chkSelection.DataPropertyName = "Selection";
            this.chkSelection.HeaderText = "Selection";
            this.chkSelection.Name = "chkSelection";
            this.chkSelection.ReadOnly = true;
            this.chkSelection.Width = 40;
            // 
            // Vn_mod_codes
            // 
            this.Vn_mod_codes.DataPropertyName = "Vn_mod";
            this.Vn_mod_codes.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.Vn_mod_codes.HeaderText = "Vn_mod ";
            this.Vn_mod_codes.Name = "Vn_mod_codes";
            this.Vn_mod_codes.ToolTipText = "--";
            // 
            // grpComments
            // 
            this.grpComments.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpComments.Controls.Add(this.txtComent);
            this.grpComments.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpComments.ForeColor = System.Drawing.SystemColors.Highlight;
            this.grpComments.Location = new System.Drawing.Point(623, 3);
            this.grpComments.Name = "grpComments";
            this.grpComments.Size = new System.Drawing.Size(614, 76);
            this.grpComments.TabIndex = 147;
            this.grpComments.TabStop = false;
            this.grpComments.Text = "Comments";
            // 
            // txtComent
            // 
            this.txtComent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtComent.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtComent.Location = new System.Drawing.Point(6, 19);
            this.txtComent.Multiline = true;
            this.txtComent.Name = "txtComent";
            this.txtComent.Size = new System.Drawing.Size(602, 51);
            this.txtComent.TabIndex = 137;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 5;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.Controls.Add(this.cbxHole, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnLock, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnSave, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnExport, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnRefresch, 3, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(614, 76);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // cbxHole
            // 
            this.cbxHole.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbxHole.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxHole.ForeColor = System.Drawing.SystemColors.Highlight;
            this.cbxHole.FormattingEnabled = true;
            this.cbxHole.Location = new System.Drawing.Point(3, 3);
            this.cbxHole.MaxDropDownItems = 10;
            this.cbxHole.Name = "cbxHole";
            this.cbxHole.Size = new System.Drawing.Size(342, 24);
            this.cbxHole.TabIndex = 111;
            // 
            // btnLock
            // 
            this.btnLock.BackColor = System.Drawing.Color.White;
            this.btnLock.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLock.Image = global::Intercept_Intervals.Properties.Resources.lock_40;
            this.btnLock.Location = new System.Drawing.Point(554, 3);
            this.btnLock.Name = "btnLock";
            this.btnLock.Size = new System.Drawing.Size(40, 40);
            this.btnLock.TabIndex = 151;
            this.btnLock.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnLock.UseVisualStyleBackColor = false;
            this.btnLock.Click += new System.EventHandler(this.btnLock_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.White;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = global::Intercept_Intervals.Properties.Resources.save_40;
            this.btnSave.Location = new System.Drawing.Point(371, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(40, 40);
            this.btnSave.TabIndex = 145;
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExport
            // 
            this.btnExport.BackColor = System.Drawing.Color.White;
            this.btnExport.Enabled = false;
            this.btnExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.Image = global::Intercept_Intervals.Properties.Resources.excel_40;
            this.btnExport.Location = new System.Drawing.Point(432, 3);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(40, 40);
            this.btnExport.TabIndex = 148;
            this.btnExport.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnRefresch
            // 
            this.btnRefresch.BackColor = System.Drawing.Color.White;
            this.btnRefresch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresch.Image = global::Intercept_Intervals.Properties.Resources.refresh40;
            this.btnRefresch.Location = new System.Drawing.Point(493, 3);
            this.btnRefresch.Name = "btnRefresch";
            this.btnRefresch.Size = new System.Drawing.Size(40, 40);
            this.btnRefresch.TabIndex = 140;
            this.btnRefresch.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRefresch.UseVisualStyleBackColor = false;
            this.btnRefresch.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // lblSavedItems
            // 
            this.lblSavedItems.AutoSize = true;
            this.lblSavedItems.BackColor = System.Drawing.Color.White;
            this.lblSavedItems.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSavedItems.ForeColor = System.Drawing.Color.Red;
            this.lblSavedItems.Location = new System.Drawing.Point(623, 573);
            this.lblSavedItems.Name = "lblSavedItems";
            this.lblSavedItems.Size = new System.Drawing.Size(382, 25);
            this.lblSavedItems.TabIndex = 150;
            this.lblSavedItems.Text = "The changes have not been saved.";
            this.lblSavedItems.Visible = false;
            // 
            // frm_Intercept_Intervals
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1257, 701);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.LblTitulos);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frm_Intercept_Intervals";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Intercept Intervals";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_Intercept_Intervals_FormClosing);
            this.Load += new System.EventHandler(this.frm_Intercept_Intervals_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.grpValueResult.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgValueCalculate)).EndInit();
            this.grpExportWell.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgDetailHoleID)).EndInit();
            this.grpComments.ResumeLayout(false);
            this.grpComments.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label LblTitulos;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox grpValueResult;
        private System.Windows.Forms.DataGridView dtgValueCalculate;
        private System.Windows.Forms.DataGridViewTextBoxColumn jobno;
        private System.Windows.Forms.DataGridViewTextBoxColumn dhid;
        private System.Windows.Forms.DataGridViewTextBoxColumn from;
        private System.Windows.Forms.DataGridViewTextBoxColumn to;
        private System.Windows.Forms.DataGridViewTextBoxColumn Length_Grade;
        private System.Windows.Forms.DataGridViewTextBoxColumn Au_Grade;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ag_Grade;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalRegister;
        private System.Windows.Forms.DataGridViewTextBoxColumn Vn_mod;
        private System.Windows.Forms.DataGridViewTextBoxColumn SKDHSamples;
        private System.Windows.Forms.GroupBox grpExportWell;
        private System.Windows.Forms.DataGridView dtgDetailHoleID;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chkSelection;
        private System.Windows.Forms.DataGridViewComboBoxColumn Vn_mod_codes;
        private System.Windows.Forms.GroupBox grpComments;
        private System.Windows.Forms.TextBox txtComent;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ComboBox cbxHole;
        private System.Windows.Forms.Button btnLock;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnRefresch;
        private System.Windows.Forms.Label lblSavedItems;
    }
}