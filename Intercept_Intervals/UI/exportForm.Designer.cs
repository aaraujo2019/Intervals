namespace Intercept_Intervals.UI
{
    partial class exportForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(exportForm));
            this.btnExport = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grboxExport = new System.Windows.Forms.GroupBox();
            this.rdBtnTarget = new System.Windows.Forms.RadioButton();
            this.rdBtnSource = new System.Windows.Forms.RadioButton();
            this.rdBtnHoleId = new System.Windows.Forms.RadioButton();
            this.lvwSource = new System.Windows.Forms.ListView();
            this.cbxTarget = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblSource = new System.Windows.Forms.Label();
            this.grboxExport.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnExport
            // 
            this.btnExport.BackColor = System.Drawing.Color.White;
            this.btnExport.ForeColor = System.Drawing.Color.Navy;
            this.btnExport.Location = new System.Drawing.Point(283, 220);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 0;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ForeColor = System.Drawing.Color.Navy;
            this.btnCancel.Location = new System.Drawing.Point(202, 220);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // grboxExport
            // 
            this.grboxExport.BackColor = System.Drawing.Color.Transparent;
            this.grboxExport.Controls.Add(this.rdBtnTarget);
            this.grboxExport.Controls.Add(this.rdBtnSource);
            this.grboxExport.Controls.Add(this.rdBtnHoleId);
            this.grboxExport.ForeColor = System.Drawing.Color.Navy;
            this.grboxExport.Location = new System.Drawing.Point(9, 13);
            this.grboxExport.Name = "grboxExport";
            this.grboxExport.Size = new System.Drawing.Size(350, 51);
            this.grboxExport.TabIndex = 2;
            this.grboxExport.TabStop = false;
            this.grboxExport.Text = "Export By";
            // 
            // rdBtnTarget
            // 
            this.rdBtnTarget.AutoSize = true;
            this.rdBtnTarget.Location = new System.Drawing.Point(256, 19);
            this.rdBtnTarget.Name = "rdBtnTarget";
            this.rdBtnTarget.Size = new System.Drawing.Size(56, 17);
            this.rdBtnTarget.TabIndex = 2;
            this.rdBtnTarget.Tag = "TARGET";
            this.rdBtnTarget.Text = "Target";
            this.rdBtnTarget.UseVisualStyleBackColor = true;
            // 
            // rdBtnSource
            // 
            this.rdBtnSource.AutoSize = true;
            this.rdBtnSource.Location = new System.Drawing.Point(142, 19);
            this.rdBtnSource.Name = "rdBtnSource";
            this.rdBtnSource.Size = new System.Drawing.Size(59, 17);
            this.rdBtnSource.TabIndex = 1;
            this.rdBtnSource.Tag = "SOURCE";
            this.rdBtnSource.Text = "Source";
            this.rdBtnSource.UseVisualStyleBackColor = true;
            // 
            // rdBtnHoleId
            // 
            this.rdBtnHoleId.AutoSize = true;
            this.rdBtnHoleId.Checked = true;
            this.rdBtnHoleId.Location = new System.Drawing.Point(28, 19);
            this.rdBtnHoleId.Name = "rdBtnHoleId";
            this.rdBtnHoleId.Size = new System.Drawing.Size(59, 17);
            this.rdBtnHoleId.TabIndex = 0;
            this.rdBtnHoleId.TabStop = true;
            this.rdBtnHoleId.Tag = "HOLE";
            this.rdBtnHoleId.Text = "Hole Id";
            this.rdBtnHoleId.UseVisualStyleBackColor = true;
            this.rdBtnHoleId.CheckedChanged += new System.EventHandler(this.rdBtnHoleId_CheckedChanged);
            // 
            // lvwSource
            // 
            this.lvwSource.CheckBoxes = true;
            this.lvwSource.ForeColor = System.Drawing.Color.Navy;
            this.lvwSource.Location = new System.Drawing.Point(9, 94);
            this.lvwSource.Name = "lvwSource";
            this.lvwSource.Size = new System.Drawing.Size(151, 149);
            this.lvwSource.TabIndex = 3;
            this.lvwSource.UseCompatibleStateImageBehavior = false;
            this.lvwSource.View = System.Windows.Forms.View.List;
            // 
            // cbxTarget
            // 
            this.cbxTarget.BackColor = System.Drawing.Color.White;
            this.cbxTarget.ForeColor = System.Drawing.Color.Navy;
            this.cbxTarget.FormattingEnabled = true;
            this.cbxTarget.Location = new System.Drawing.Point(169, 94);
            this.cbxTarget.Name = "cbxTarget";
            this.cbxTarget.Size = new System.Drawing.Size(190, 21);
            this.cbxTarget.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(166, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Target :";
            // 
            // lblSource
            // 
            this.lblSource.AutoSize = true;
            this.lblSource.ForeColor = System.Drawing.Color.Navy;
            this.lblSource.Location = new System.Drawing.Point(12, 78);
            this.lblSource.Name = "lblSource";
            this.lblSource.Size = new System.Drawing.Size(47, 13);
            this.lblSource.TabIndex = 6;
            this.lblSource.Text = "Source :";
            // 
            // exportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(370, 261);
            this.Controls.Add(this.lblSource);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbxTarget);
            this.Controls.Add(this.lvwSource);
            this.Controls.Add(this.grboxExport);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnExport);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "exportForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Export";
            this.Load += new System.EventHandler(this.exportForm_Load);
            this.grboxExport.ResumeLayout(false);
            this.grboxExport.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox grboxExport;
        private System.Windows.Forms.RadioButton rdBtnTarget;
        private System.Windows.Forms.RadioButton rdBtnSource;
        private System.Windows.Forms.RadioButton rdBtnHoleId;
        private System.Windows.Forms.ListView lvwSource;
        private System.Windows.Forms.ComboBox cbxTarget;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblSource;
    }
}