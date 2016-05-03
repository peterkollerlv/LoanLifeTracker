namespace LoanLifeTrackerV00
{
    partial class LoanReportDataView
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
            this.loanReportDataGrid = new System.Windows.Forms.DataGridView();
            this.loanReportDataLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.loanReportDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // loanReportDataGrid
            // 
            this.loanReportDataGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.loanReportDataGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.loanReportDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.loanReportDataGrid.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.loanReportDataGrid.Location = new System.Drawing.Point(27, 59);
            this.loanReportDataGrid.Name = "loanReportDataGrid";
            this.loanReportDataGrid.Size = new System.Drawing.Size(513, 281);
            this.loanReportDataGrid.TabIndex = 0;
            // 
            // loanReportDataLabel
            // 
            this.loanReportDataLabel.AutoSize = true;
            this.loanReportDataLabel.Location = new System.Drawing.Point(24, 22);
            this.loanReportDataLabel.Name = "loanReportDataLabel";
            this.loanReportDataLabel.Size = new System.Drawing.Size(52, 13);
            this.loanReportDataLabel.TabIndex = 1;
            this.loanReportDataLabel.Text = "(dynamic)";
            // 
            // LoanReportDataView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(561, 363);
            this.Controls.Add(this.loanReportDataLabel);
            this.Controls.Add(this.loanReportDataGrid);
            this.Name = "LoanReportDataView";
            this.Text = "Report View";
            ((System.ComponentModel.ISupportInitialize)(this.loanReportDataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label loanReportDataLabel;
        public System.Windows.Forms.DataGridView loanReportDataGrid;
    }
}