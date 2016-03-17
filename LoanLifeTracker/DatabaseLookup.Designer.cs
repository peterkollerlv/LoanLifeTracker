namespace LoanLifeTracker
{
    partial class DatabaseLookup
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DatabaseLookup));
            this.labelSelectedDayInfo = new System.Windows.Forms.Label();
            this.loanLifeTrackerBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cyberHostBoxDataSet = new LoanLifeTracker.cyberHostBoxDataSet();
            this.cyberHostBoxDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.loanLifeTrackerTableAdapter = new LoanLifeTracker.cyberHostBoxDataSetTableAdapters.LoanLifeTrackerTableAdapter();
            this.gridExistingLoans = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.loanLifeTrackerBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cyberHostBoxDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cyberHostBoxDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridExistingLoans)).BeginInit();
            this.SuspendLayout();
            // 
            // labelSelectedDayInfo
            // 
            this.labelSelectedDayInfo.AutoSize = true;
            this.labelSelectedDayInfo.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSelectedDayInfo.ForeColor = System.Drawing.Color.Gainsboro;
            this.labelSelectedDayInfo.Location = new System.Drawing.Point(149, 9);
            this.labelSelectedDayInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelSelectedDayInfo.Name = "labelSelectedDayInfo";
            this.labelSelectedDayInfo.Size = new System.Drawing.Size(122, 16);
            this.labelSelectedDayInfo.TabIndex = 71;
            this.labelSelectedDayInfo.Text = "Selected date details:";
            // 
            // loanLifeTrackerBindingSource
            // 
            this.loanLifeTrackerBindingSource.DataMember = "LoanLifeTracker";
            this.loanLifeTrackerBindingSource.DataSource = this.cyberHostBoxDataSet;
            // 
            // cyberHostBoxDataSet
            // 
            this.cyberHostBoxDataSet.DataSetName = "cyberHostBoxDataSet";
            this.cyberHostBoxDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // cyberHostBoxDataSetBindingSource
            // 
            this.cyberHostBoxDataSetBindingSource.DataSource = this.cyberHostBoxDataSet;
            this.cyberHostBoxDataSetBindingSource.Position = 0;
            // 
            // loanLifeTrackerTableAdapter
            // 
            this.loanLifeTrackerTableAdapter.ClearBeforeFill = true;
            // 
            // gridExistingLoans
            // 
            this.gridExistingLoans.AllowDrop = true;
            this.gridExistingLoans.AllowUserToAddRows = false;
            this.gridExistingLoans.AllowUserToDeleteRows = false;
            this.gridExistingLoans.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridExistingLoans.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gridExistingLoans.BackgroundColor = System.Drawing.Color.SteelBlue;
            this.gridExistingLoans.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridExistingLoans.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.gridExistingLoans.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(72)))), ((int)(((byte)(125)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial Narrow", 8.25F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(72)))), ((int)(((byte)(125)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridExistingLoans.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridExistingLoans.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridExistingLoans.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial Narrow", 8.25F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(72)))), ((int)(((byte)(125)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridExistingLoans.DefaultCellStyle = dataGridViewCellStyle2;
            this.gridExistingLoans.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.gridExistingLoans.EnableHeadersVisualStyles = false;
            this.gridExistingLoans.GridColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.gridExistingLoans.Location = new System.Drawing.Point(3, 86);
            this.gridExistingLoans.Margin = new System.Windows.Forms.Padding(4);
            this.gridExistingLoans.Name = "gridExistingLoans";
            this.gridExistingLoans.ReadOnly = true;
            this.gridExistingLoans.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial Narrow", 8.25F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(72)))), ((int)(((byte)(125)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridExistingLoans.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gridExistingLoans.RowHeadersVisible = false;
            this.gridExistingLoans.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(72)))), ((int)(((byte)(125)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridExistingLoans.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.gridExistingLoans.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridExistingLoans.Size = new System.Drawing.Size(477, 206);
            this.gridExistingLoans.TabIndex = 65;
            // 
            // DatabaseLookup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(5F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.ClientSize = new System.Drawing.Size(484, 297);
            this.Controls.Add(this.gridExistingLoans);
            this.Controls.Add(this.labelSelectedDayInfo);
            this.Font = new System.Drawing.Font("Arial Narrow", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DatabaseLookup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Database Conncetion";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.loanLifeTrackerBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cyberHostBoxDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cyberHostBoxDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridExistingLoans)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelSelectedDayInfo;
        private System.Windows.Forms.BindingSource cyberHostBoxDataSetBindingSource;
        private cyberHostBoxDataSet cyberHostBoxDataSet;
        private System.Windows.Forms.BindingSource loanLifeTrackerBindingSource;
        private cyberHostBoxDataSetTableAdapters.LoanLifeTrackerTableAdapter loanLifeTrackerTableAdapter;
        public System.Windows.Forms.DataGridView gridExistingLoans;
    }
}