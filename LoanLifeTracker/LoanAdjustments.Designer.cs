namespace LoanLifeTracker
{
    partial class LoanAdjustments
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoanAdjustments));
            this.panelPrincipleAdjust = new System.Windows.Forms.Panel();
            this.labelPrincipalAdjustment = new System.Windows.Forms.Label();
            this.inputInitialLoanAmount = new System.Windows.Forms.NumericUpDown();
            this.buttonAddAdjustment = new System.Windows.Forms.Button();
            this.buttonClosePrincipleAdjust = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.labelPrincipleAdjustDate = new System.Windows.Forms.Label();
            this.panelAddPayment = new System.Windows.Forms.Panel();
            this.inputPaymentAmount = new System.Windows.Forms.TextBox();
            this.panelPaymentAllocation = new System.Windows.Forms.Panel();
            this.labelPaymentInterestAmount = new System.Windows.Forms.Label();
            this.inputPaymentInterestAmount = new System.Windows.Forms.NumericUpDown();
            this.labelInterestPaymentCurrency = new System.Windows.Forms.Label();
            this.labelPaymentPrincipalAmount = new System.Windows.Forms.Label();
            this.labelPaymantAllocation = new System.Windows.Forms.Label();
            this.inputPaymentAllocationTrack = new System.Windows.Forms.TrackBar();
            this.inputPaymentPrincipalAmount = new System.Windows.Forms.NumericUpDown();
            this.labelPrincipalPaymentCurrency = new System.Windows.Forms.Label();
            this.buttonPaymentReset = new System.Windows.Forms.Button();
            this.gridPaymentList = new System.Windows.Forms.DataGridView();
            this.buttonCloseAddPayment = new System.Windows.Forms.Button();
            this.labelPaymentCurrency = new System.Windows.Forms.Label();
            this.buttonAddPayment = new System.Windows.Forms.Button();
            this.labelPaymentAmount = new System.Windows.Forms.Label();
            this.inputPaymentDate = new System.Windows.Forms.DateTimePicker();
            this.lablePaymentDate = new System.Windows.Forms.Label();
            this.panelPrincipleAdjust.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.inputInitialLoanAmount)).BeginInit();
            this.panelAddPayment.SuspendLayout();
            this.panelPaymentAllocation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.inputPaymentInterestAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.inputPaymentAllocationTrack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.inputPaymentPrincipalAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridPaymentList)).BeginInit();
            this.SuspendLayout();
            // 
            // panelPrincipleAdjust
            // 
            this.panelPrincipleAdjust.BackColor = System.Drawing.Color.SteelBlue;
            this.panelPrincipleAdjust.Controls.Add(this.labelPrincipalAdjustment);
            this.panelPrincipleAdjust.Controls.Add(this.inputInitialLoanAmount);
            this.panelPrincipleAdjust.Controls.Add(this.buttonAddAdjustment);
            this.panelPrincipleAdjust.Controls.Add(this.buttonClosePrincipleAdjust);
            this.panelPrincipleAdjust.Controls.Add(this.dateTimePicker1);
            this.panelPrincipleAdjust.Controls.Add(this.labelPrincipleAdjustDate);
            this.panelPrincipleAdjust.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPrincipleAdjust.Location = new System.Drawing.Point(0, 0);
            this.panelPrincipleAdjust.Name = "panelPrincipleAdjust";
            this.panelPrincipleAdjust.Size = new System.Drawing.Size(433, 429);
            this.panelPrincipleAdjust.TabIndex = 0;
            // 
            // labelPrincipalAdjustment
            // 
            this.labelPrincipalAdjustment.AutoSize = true;
            this.labelPrincipalAdjustment.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPrincipalAdjustment.Location = new System.Drawing.Point(27, 62);
            this.labelPrincipalAdjustment.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPrincipalAdjustment.Name = "labelPrincipalAdjustment";
            this.labelPrincipalAdjustment.Size = new System.Drawing.Size(112, 16);
            this.labelPrincipalAdjustment.TabIndex = 58;
            this.labelPrincipalAdjustment.Text = "Adjustment Amount";
            // 
            // inputInitialLoanAmount
            // 
            this.inputInitialLoanAmount.DecimalPlaces = 2;
            this.inputInitialLoanAmount.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inputInitialLoanAmount.Location = new System.Drawing.Point(30, 83);
            this.inputInitialLoanAmount.Margin = new System.Windows.Forms.Padding(4);
            this.inputInitialLoanAmount.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.inputInitialLoanAmount.Name = "inputInitialLoanAmount";
            this.inputInitialLoanAmount.Size = new System.Drawing.Size(110, 26);
            this.inputInitialLoanAmount.TabIndex = 57;
            this.inputInitialLoanAmount.ThousandsSeparator = true;
            // 
            // buttonAddAdjustment
            // 
            this.buttonAddAdjustment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddAdjustment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(72)))), ((int)(((byte)(125)))));
            this.buttonAddAdjustment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAddAdjustment.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAddAdjustment.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.buttonAddAdjustment.Location = new System.Drawing.Point(241, 400);
            this.buttonAddAdjustment.Margin = new System.Windows.Forms.Padding(4);
            this.buttonAddAdjustment.Name = "buttonAddAdjustment";
            this.buttonAddAdjustment.Size = new System.Drawing.Size(110, 25);
            this.buttonAddAdjustment.TabIndex = 56;
            this.buttonAddAdjustment.Text = "Add Adjustment";
            this.buttonAddAdjustment.UseVisualStyleBackColor = false;
            // 
            // buttonClosePrincipleAdjust
            // 
            this.buttonClosePrincipleAdjust.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClosePrincipleAdjust.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(72)))), ((int)(((byte)(125)))));
            this.buttonClosePrincipleAdjust.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClosePrincipleAdjust.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonClosePrincipleAdjust.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.buttonClosePrincipleAdjust.Location = new System.Drawing.Point(359, 400);
            this.buttonClosePrincipleAdjust.Margin = new System.Windows.Forms.Padding(4);
            this.buttonClosePrincipleAdjust.Name = "buttonClosePrincipleAdjust";
            this.buttonClosePrincipleAdjust.Size = new System.Drawing.Size(70, 25);
            this.buttonClosePrincipleAdjust.TabIndex = 55;
            this.buttonClosePrincipleAdjust.Text = "Confirm";
            this.buttonClosePrincipleAdjust.UseVisualStyleBackColor = false;
            this.buttonClosePrincipleAdjust.Click += new System.EventHandler(this.buttonClosePrincipleAdjust_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CalendarFont = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dateTimePicker1.CustomFormat = "MMMM dd, yyyy";
            this.dateTimePicker1.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(30, 32);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(4);
            this.dateTimePicker1.MaxDate = new System.DateTime(3000, 12, 31, 0, 0, 0, 0);
            this.dateTimePicker1.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(160, 26);
            this.dateTimePicker1.TabIndex = 53;
            // 
            // labelPrincipleAdjustDate
            // 
            this.labelPrincipleAdjustDate.AutoSize = true;
            this.labelPrincipleAdjustDate.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPrincipleAdjustDate.Location = new System.Drawing.Point(27, 11);
            this.labelPrincipleAdjustDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPrincipleAdjustDate.Name = "labelPrincipleAdjustDate";
            this.labelPrincipleAdjustDate.Size = new System.Drawing.Size(109, 16);
            this.labelPrincipleAdjustDate.TabIndex = 54;
            this.labelPrincipleAdjustDate.Text = "Date of Adjustment";
            // 
            // panelAddPayment
            // 
            this.panelAddPayment.BackColor = System.Drawing.Color.SteelBlue;
            this.panelAddPayment.Controls.Add(this.inputPaymentAmount);
            this.panelAddPayment.Controls.Add(this.panelPaymentAllocation);
            this.panelAddPayment.Controls.Add(this.buttonPaymentReset);
            this.panelAddPayment.Controls.Add(this.gridPaymentList);
            this.panelAddPayment.Controls.Add(this.buttonCloseAddPayment);
            this.panelAddPayment.Controls.Add(this.labelPaymentCurrency);
            this.panelAddPayment.Controls.Add(this.labelPaymentAmount);
            this.panelAddPayment.Controls.Add(this.inputPaymentDate);
            this.panelAddPayment.Controls.Add(this.lablePaymentDate);
            this.panelAddPayment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAddPayment.Location = new System.Drawing.Point(0, 0);
            this.panelAddPayment.Name = "panelAddPayment";
            this.panelAddPayment.Size = new System.Drawing.Size(433, 429);
            this.panelAddPayment.TabIndex = 1;
            // 
            // inputPaymentAmount
            // 
            this.inputPaymentAmount.BackColor = System.Drawing.Color.Gainsboro;
            this.inputPaymentAmount.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold);
            this.inputPaymentAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(72)))), ((int)(((byte)(125)))));
            this.inputPaymentAmount.Location = new System.Drawing.Point(181, 32);
            this.inputPaymentAmount.Name = "inputPaymentAmount";
            this.inputPaymentAmount.Size = new System.Drawing.Size(71, 26);
            this.inputPaymentAmount.TabIndex = 67;
            this.inputPaymentAmount.TextChanged += new System.EventHandler(this.inputPaymentAmount_TextChanged);
            this.inputPaymentAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.inputPaymentAmount_KeyPress);
            // 
            // panelPaymentAllocation
            // 
            this.panelPaymentAllocation.Controls.Add(this.labelPaymentInterestAmount);
            this.panelPaymentAllocation.Controls.Add(this.inputPaymentInterestAmount);
            this.panelPaymentAllocation.Controls.Add(this.labelInterestPaymentCurrency);
            this.panelPaymentAllocation.Controls.Add(this.labelPaymentPrincipalAmount);
            this.panelPaymentAllocation.Controls.Add(this.labelPaymantAllocation);
            this.panelPaymentAllocation.Controls.Add(this.inputPaymentAllocationTrack);
            this.panelPaymentAllocation.Controls.Add(this.buttonAddPayment);
            this.panelPaymentAllocation.Controls.Add(this.inputPaymentPrincipalAmount);
            this.panelPaymentAllocation.Controls.Add(this.labelPrincipalPaymentCurrency);
            this.panelPaymentAllocation.Location = new System.Drawing.Point(3, 64);
            this.panelPaymentAllocation.Name = "panelPaymentAllocation";
            this.panelPaymentAllocation.Size = new System.Drawing.Size(430, 107);
            this.panelPaymentAllocation.TabIndex = 66;
            // 
            // labelPaymentInterestAmount
            // 
            this.labelPaymentInterestAmount.AutoSize = true;
            this.labelPaymentInterestAmount.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPaymentInterestAmount.ForeColor = System.Drawing.Color.Gainsboro;
            this.labelPaymentInterestAmount.Location = new System.Drawing.Point(4, 49);
            this.labelPaymentInterestAmount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPaymentInterestAmount.Name = "labelPaymentInterestAmount";
            this.labelPaymentInterestAmount.Size = new System.Drawing.Size(49, 16);
            this.labelPaymentInterestAmount.TabIndex = 54;
            this.labelPaymentInterestAmount.Text = "Interest";
            // 
            // inputPaymentInterestAmount
            // 
            this.inputPaymentInterestAmount.DecimalPlaces = 2;
            this.inputPaymentInterestAmount.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inputPaymentInterestAmount.Location = new System.Drawing.Point(7, 68);
            this.inputPaymentInterestAmount.Margin = new System.Windows.Forms.Padding(4);
            this.inputPaymentInterestAmount.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.inputPaymentInterestAmount.Minimum = new decimal(new int[] {
            100000000,
            0,
            0,
            -2147483648});
            this.inputPaymentInterestAmount.Name = "inputPaymentInterestAmount";
            this.inputPaymentInterestAmount.Size = new System.Drawing.Size(71, 26);
            this.inputPaymentInterestAmount.TabIndex = 57;
            this.inputPaymentInterestAmount.ValueChanged += new System.EventHandler(this.inputPaymentInterestAmount_ValueChanged);
            // 
            // labelInterestPaymentCurrency
            // 
            this.labelInterestPaymentCurrency.AutoSize = true;
            this.labelInterestPaymentCurrency.ForeColor = System.Drawing.Color.Gainsboro;
            this.labelInterestPaymentCurrency.Location = new System.Drawing.Point(85, 74);
            this.labelInterestPaymentCurrency.Name = "labelInterestPaymentCurrency";
            this.labelInterestPaymentCurrency.Size = new System.Drawing.Size(26, 15);
            this.labelInterestPaymentCurrency.TabIndex = 62;
            this.labelInterestPaymentCurrency.Text = "USD";
            // 
            // labelPaymentPrincipalAmount
            // 
            this.labelPaymentPrincipalAmount.AutoSize = true;
            this.labelPaymentPrincipalAmount.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPaymentPrincipalAmount.ForeColor = System.Drawing.Color.Gainsboro;
            this.labelPaymentPrincipalAmount.Location = new System.Drawing.Point(363, 48);
            this.labelPaymentPrincipalAmount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPaymentPrincipalAmount.Name = "labelPaymentPrincipalAmount";
            this.labelPaymentPrincipalAmount.Size = new System.Drawing.Size(54, 16);
            this.labelPaymentPrincipalAmount.TabIndex = 56;
            this.labelPaymentPrincipalAmount.Text = "Principal";
            // 
            // labelPaymantAllocation
            // 
            this.labelPaymantAllocation.AutoSize = true;
            this.labelPaymantAllocation.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPaymantAllocation.ForeColor = System.Drawing.Color.Gainsboro;
            this.labelPaymantAllocation.Location = new System.Drawing.Point(4, 0);
            this.labelPaymantAllocation.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPaymantAllocation.Name = "labelPaymantAllocation";
            this.labelPaymantAllocation.Size = new System.Drawing.Size(109, 16);
            this.labelPaymantAllocation.TabIndex = 60;
            this.labelPaymantAllocation.Text = "Payment Allocation";
            // 
            // inputPaymentAllocationTrack
            // 
            this.inputPaymentAllocationTrack.AutoSize = false;
            this.inputPaymentAllocationTrack.LargeChange = 20;
            this.inputPaymentAllocationTrack.Location = new System.Drawing.Point(-3, 19);
            this.inputPaymentAllocationTrack.Maximum = 400;
            this.inputPaymentAllocationTrack.Name = "inputPaymentAllocationTrack";
            this.inputPaymentAllocationTrack.Size = new System.Drawing.Size(429, 45);
            this.inputPaymentAllocationTrack.TabIndex = 53;
            this.inputPaymentAllocationTrack.TickFrequency = 5;
            this.inputPaymentAllocationTrack.ValueChanged += new System.EventHandler(this.inputPaymentAllocationTrack_ValueChanged);
            // 
            // inputPaymentPrincipalAmount
            // 
            this.inputPaymentPrincipalAmount.DecimalPlaces = 2;
            this.inputPaymentPrincipalAmount.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inputPaymentPrincipalAmount.Location = new System.Drawing.Point(324, 68);
            this.inputPaymentPrincipalAmount.Margin = new System.Windows.Forms.Padding(4);
            this.inputPaymentPrincipalAmount.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.inputPaymentPrincipalAmount.Minimum = new decimal(new int[] {
            100000000,
            0,
            0,
            -2147483648});
            this.inputPaymentPrincipalAmount.Name = "inputPaymentPrincipalAmount";
            this.inputPaymentPrincipalAmount.Size = new System.Drawing.Size(70, 26);
            this.inputPaymentPrincipalAmount.TabIndex = 55;
            this.inputPaymentPrincipalAmount.ValueChanged += new System.EventHandler(this.inputPaymentPrincipalAmount_ValueChanged);
            // 
            // labelPrincipalPaymentCurrency
            // 
            this.labelPrincipalPaymentCurrency.AutoSize = true;
            this.labelPrincipalPaymentCurrency.ForeColor = System.Drawing.Color.Gainsboro;
            this.labelPrincipalPaymentCurrency.Location = new System.Drawing.Point(401, 74);
            this.labelPrincipalPaymentCurrency.Name = "labelPrincipalPaymentCurrency";
            this.labelPrincipalPaymentCurrency.Size = new System.Drawing.Size(26, 15);
            this.labelPrincipalPaymentCurrency.TabIndex = 61;
            this.labelPrincipalPaymentCurrency.Text = "USD";
            // 
            // buttonPaymentReset
            // 
            this.buttonPaymentReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPaymentReset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(72)))), ((int)(((byte)(125)))));
            this.buttonPaymentReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPaymentReset.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPaymentReset.ForeColor = System.Drawing.Color.Gainsboro;
            this.buttonPaymentReset.Location = new System.Drawing.Point(359, 11);
            this.buttonPaymentReset.Margin = new System.Windows.Forms.Padding(4);
            this.buttonPaymentReset.Name = "buttonPaymentReset";
            this.buttonPaymentReset.Size = new System.Drawing.Size(61, 28);
            this.buttonPaymentReset.TabIndex = 65;
            this.buttonPaymentReset.Text = "Reset";
            this.buttonPaymentReset.UseVisualStyleBackColor = false;
            // 
            // gridPaymentList
            // 
            this.gridPaymentList.AllowDrop = true;
            this.gridPaymentList.AllowUserToAddRows = false;
            this.gridPaymentList.AllowUserToDeleteRows = false;
            this.gridPaymentList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridPaymentList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gridPaymentList.BackgroundColor = System.Drawing.Color.SteelBlue;
            this.gridPaymentList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridPaymentList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.gridPaymentList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridPaymentList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridPaymentList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridPaymentList.Cursor = System.Windows.Forms.Cursors.IBeam;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.LightSkyBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.DeepSkyBlue;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.LightCyan;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridPaymentList.DefaultCellStyle = dataGridViewCellStyle2;
            this.gridPaymentList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.gridPaymentList.GridColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.gridPaymentList.Location = new System.Drawing.Point(4, 179);
            this.gridPaymentList.Margin = new System.Windows.Forms.Padding(4);
            this.gridPaymentList.Name = "gridPaymentList";
            this.gridPaymentList.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.gridPaymentList.RowHeadersVisible = false;
            this.gridPaymentList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.LightSkyBlue;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridPaymentList.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.gridPaymentList.Size = new System.Drawing.Size(425, 214);
            this.gridPaymentList.TabIndex = 64;
            // 
            // buttonCloseAddPayment
            // 
            this.buttonCloseAddPayment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCloseAddPayment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(72)))), ((int)(((byte)(125)))));
            this.buttonCloseAddPayment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCloseAddPayment.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCloseAddPayment.ForeColor = System.Drawing.Color.Gainsboro;
            this.buttonCloseAddPayment.Location = new System.Drawing.Point(327, 400);
            this.buttonCloseAddPayment.Margin = new System.Windows.Forms.Padding(4);
            this.buttonCloseAddPayment.Name = "buttonCloseAddPayment";
            this.buttonCloseAddPayment.Size = new System.Drawing.Size(102, 25);
            this.buttonCloseAddPayment.TabIndex = 63;
            this.buttonCloseAddPayment.Text = "Accept";
            this.buttonCloseAddPayment.UseVisualStyleBackColor = false;
            this.buttonCloseAddPayment.Click += new System.EventHandler(this.buttonCloseAddPayment_Click);
            // 
            // labelPaymentCurrency
            // 
            this.labelPaymentCurrency.AutoSize = true;
            this.labelPaymentCurrency.ForeColor = System.Drawing.Color.Gainsboro;
            this.labelPaymentCurrency.Location = new System.Drawing.Point(258, 38);
            this.labelPaymentCurrency.Name = "labelPaymentCurrency";
            this.labelPaymentCurrency.Size = new System.Drawing.Size(26, 15);
            this.labelPaymentCurrency.TabIndex = 59;
            this.labelPaymentCurrency.Text = "USD";
            // 
            // buttonAddPayment
            // 
            this.buttonAddPayment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddPayment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(72)))), ((int)(((byte)(125)))));
            this.buttonAddPayment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAddPayment.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAddPayment.ForeColor = System.Drawing.Color.Gainsboro;
            this.buttonAddPayment.Location = new System.Drawing.Point(163, 68);
            this.buttonAddPayment.Margin = new System.Windows.Forms.Padding(4);
            this.buttonAddPayment.Name = "buttonAddPayment";
            this.buttonAddPayment.Size = new System.Drawing.Size(110, 25);
            this.buttonAddPayment.TabIndex = 58;
            this.buttonAddPayment.Text = "Add Payment";
            this.buttonAddPayment.UseVisualStyleBackColor = false;
            this.buttonAddPayment.Click += new System.EventHandler(this.buttonAddPayment_Click);
            // 
            // labelPaymentAmount
            // 
            this.labelPaymentAmount.AutoSize = true;
            this.labelPaymentAmount.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPaymentAmount.ForeColor = System.Drawing.Color.Gainsboro;
            this.labelPaymentAmount.Location = new System.Drawing.Point(179, 12);
            this.labelPaymentAmount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPaymentAmount.Name = "labelPaymentAmount";
            this.labelPaymentAmount.Size = new System.Drawing.Size(97, 16);
            this.labelPaymentAmount.TabIndex = 50;
            this.labelPaymentAmount.Text = "Payment Amount";
            // 
            // inputPaymentDate
            // 
            this.inputPaymentDate.CalendarFont = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.inputPaymentDate.CustomFormat = "MMMM dd, yyyy";
            this.inputPaymentDate.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inputPaymentDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.inputPaymentDate.Location = new System.Drawing.Point(13, 31);
            this.inputPaymentDate.Margin = new System.Windows.Forms.Padding(4);
            this.inputPaymentDate.Name = "inputPaymentDate";
            this.inputPaymentDate.Size = new System.Drawing.Size(160, 26);
            this.inputPaymentDate.TabIndex = 51;
            this.inputPaymentDate.ValueChanged += new System.EventHandler(this.inputPaymentDate_ValueChanged);
            // 
            // lablePaymentDate
            // 
            this.lablePaymentDate.AutoSize = true;
            this.lablePaymentDate.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lablePaymentDate.ForeColor = System.Drawing.Color.Gainsboro;
            this.lablePaymentDate.Location = new System.Drawing.Point(8, 11);
            this.lablePaymentDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lablePaymentDate.Name = "lablePaymentDate";
            this.lablePaymentDate.Size = new System.Drawing.Size(80, 16);
            this.lablePaymentDate.TabIndex = 49;
            this.lablePaymentDate.Text = "Payment Date";
            // 
            // LoanAdjustments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(5F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.ClientSize = new System.Drawing.Size(433, 429);
            this.Controls.Add(this.panelAddPayment);
            this.Controls.Add(this.panelPrincipleAdjust);
            this.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoanAdjustments";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "(Default)";
            this.TopMost = true;
            this.panelPrincipleAdjust.ResumeLayout(false);
            this.panelPrincipleAdjust.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.inputInitialLoanAmount)).EndInit();
            this.panelAddPayment.ResumeLayout(false);
            this.panelAddPayment.PerformLayout();
            this.panelPaymentAllocation.ResumeLayout(false);
            this.panelPaymentAllocation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.inputPaymentInterestAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.inputPaymentAllocationTrack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.inputPaymentPrincipalAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridPaymentList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.Panel panelPrincipleAdjust;
        public System.Windows.Forms.Panel panelAddPayment;
        private System.Windows.Forms.Button buttonCloseAddPayment;
        private System.Windows.Forms.TrackBar inputPaymentAllocationTrack;
        public System.Windows.Forms.Label labelInterestPaymentCurrency;
        public System.Windows.Forms.Label labelPrincipalPaymentCurrency;
        private System.Windows.Forms.Label labelPaymantAllocation;
        public System.Windows.Forms.Label labelPaymentCurrency;
        private System.Windows.Forms.Label labelPaymentPrincipalAmount;
        private System.Windows.Forms.Label labelPaymentInterestAmount;
        public System.Windows.Forms.NumericUpDown inputPaymentInterestAmount;
        public System.Windows.Forms.NumericUpDown inputPaymentPrincipalAmount;
        private System.Windows.Forms.Button buttonAddPayment;
        private System.Windows.Forms.Label labelPaymentAmount;
        public System.Windows.Forms.DateTimePicker inputPaymentDate;
        private System.Windows.Forms.Label lablePaymentDate;
        private System.Windows.Forms.Label labelPrincipalAdjustment;
        public System.Windows.Forms.NumericUpDown inputInitialLoanAmount;
        private System.Windows.Forms.Button buttonAddAdjustment;
        private System.Windows.Forms.Button buttonClosePrincipleAdjust;
        public System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label labelPrincipleAdjustDate;
        public System.Windows.Forms.DataGridView gridPaymentList;
        private System.Windows.Forms.Panel panelPaymentAllocation;
        private System.Windows.Forms.Button buttonPaymentReset;
        private System.Windows.Forms.TextBox inputPaymentAmount;
    }
}