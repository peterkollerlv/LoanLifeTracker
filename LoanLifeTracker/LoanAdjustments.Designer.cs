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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoanAdjustments));
            this.panelPrincipleAdjust = new System.Windows.Forms.Panel();
            this.principalAdjustmentAmount = new System.Windows.Forms.TextBox();
            this.labelPrincipalAdjustment = new System.Windows.Forms.Label();
            this.buttonAddAdjustment = new System.Windows.Forms.Button();
            this.buttonClosePrincipleAdjust = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.labelPrincipleAdjustDate = new System.Windows.Forms.Label();
            this.panelAddPayment = new System.Windows.Forms.Panel();
            this.labelSelectedDayInfo = new System.Windows.Forms.Label();
            this.buttonRemovePayment = new System.Windows.Forms.Button();
            this.inputPaymentAmount = new System.Windows.Forms.TextBox();
            this.panelPaymentAllocation = new System.Windows.Forms.Panel();
            this.labelPrincipalPercent = new System.Windows.Forms.Label();
            this.labelInterestPercent = new System.Windows.Forms.Label();
            this.inputPaymentInterestAmount = new System.Windows.Forms.TextBox();
            this.inputPaymentPrincipalAmount = new System.Windows.Forms.TextBox();
            this.labelPaymentInterestAmount = new System.Windows.Forms.Label();
            this.labelInterestPaymentCurrency = new System.Windows.Forms.Label();
            this.labelPaymentPrincipalAmount = new System.Windows.Forms.Label();
            this.labelPaymantAllocation = new System.Windows.Forms.Label();
            this.inputPaymentAllocationTrack = new System.Windows.Forms.TrackBar();
            this.labelPrincipalPaymentCurrency = new System.Windows.Forms.Label();
            this.gridPaymentList = new System.Windows.Forms.DataGridView();
            this.buttonCloseAddPayment = new System.Windows.Forms.Button();
            this.labelPaymentCurrency = new System.Windows.Forms.Label();
            this.labelPaymentAmount = new System.Windows.Forms.Label();
            this.inputPaymentDate = new System.Windows.Forms.DateTimePicker();
            this.lablePaymentDate = new System.Windows.Forms.Label();
            this.buttonAddPayment = new System.Windows.Forms.Button();
            this.paymentBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panelPrincipleAdjust.SuspendLayout();
            this.panelAddPayment.SuspendLayout();
            this.panelPaymentAllocation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.inputPaymentAllocationTrack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridPaymentList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.paymentBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // panelPrincipleAdjust
            // 
            this.panelPrincipleAdjust.BackColor = System.Drawing.Color.SteelBlue;
            this.panelPrincipleAdjust.Controls.Add(this.principalAdjustmentAmount);
            this.panelPrincipleAdjust.Controls.Add(this.labelPrincipalAdjustment);
            this.panelPrincipleAdjust.Controls.Add(this.buttonAddAdjustment);
            this.panelPrincipleAdjust.Controls.Add(this.buttonClosePrincipleAdjust);
            this.panelPrincipleAdjust.Controls.Add(this.dateTimePicker1);
            this.panelPrincipleAdjust.Controls.Add(this.labelPrincipleAdjustDate);
            this.panelPrincipleAdjust.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPrincipleAdjust.Location = new System.Drawing.Point(0, 0);
            this.panelPrincipleAdjust.Name = "panelPrincipleAdjust";
            this.panelPrincipleAdjust.Size = new System.Drawing.Size(508, 481);
            this.panelPrincipleAdjust.TabIndex = 0;
            // 
            // principalAdjustmentAmount
            // 
            this.principalAdjustmentAmount.BackColor = System.Drawing.Color.Gainsboro;
            this.principalAdjustmentAmount.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold);
            this.principalAdjustmentAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(72)))), ((int)(((byte)(125)))));
            this.principalAdjustmentAmount.Location = new System.Drawing.Point(30, 83);
            this.principalAdjustmentAmount.Name = "principalAdjustmentAmount";
            this.principalAdjustmentAmount.ShortcutsEnabled = false;
            this.principalAdjustmentAmount.Size = new System.Drawing.Size(71, 26);
            this.principalAdjustmentAmount.TabIndex = 68;
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
            // buttonAddAdjustment
            // 
            this.buttonAddAdjustment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddAdjustment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(72)))), ((int)(((byte)(125)))));
            this.buttonAddAdjustment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAddAdjustment.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAddAdjustment.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.buttonAddAdjustment.Location = new System.Drawing.Point(316, 452);
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
            this.buttonClosePrincipleAdjust.Location = new System.Drawing.Point(434, 452);
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
            this.panelAddPayment.Controls.Add(this.labelSelectedDayInfo);
            this.panelAddPayment.Controls.Add(this.buttonRemovePayment);
            this.panelAddPayment.Controls.Add(this.inputPaymentAmount);
            this.panelAddPayment.Controls.Add(this.panelPaymentAllocation);
            this.panelAddPayment.Controls.Add(this.gridPaymentList);
            this.panelAddPayment.Controls.Add(this.buttonCloseAddPayment);
            this.panelAddPayment.Controls.Add(this.labelPaymentCurrency);
            this.panelAddPayment.Controls.Add(this.labelPaymentAmount);
            this.panelAddPayment.Controls.Add(this.inputPaymentDate);
            this.panelAddPayment.Controls.Add(this.lablePaymentDate);
            this.panelAddPayment.Controls.Add(this.buttonAddPayment);
            this.panelAddPayment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAddPayment.Location = new System.Drawing.Point(0, 0);
            this.panelAddPayment.Name = "panelAddPayment";
            this.panelAddPayment.Size = new System.Drawing.Size(508, 481);
            this.panelAddPayment.TabIndex = 1;
            // 
            // labelSelectedDayInfo
            // 
            this.labelSelectedDayInfo.AutoSize = true;
            this.labelSelectedDayInfo.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSelectedDayInfo.ForeColor = System.Drawing.Color.Gainsboro;
            this.labelSelectedDayInfo.Location = new System.Drawing.Point(199, 9);
            this.labelSelectedDayInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelSelectedDayInfo.Name = "labelSelectedDayInfo";
            this.labelSelectedDayInfo.Size = new System.Drawing.Size(122, 16);
            this.labelSelectedDayInfo.TabIndex = 70;
            this.labelSelectedDayInfo.Text = "Selected date details:";
            // 
            // buttonRemovePayment
            // 
            this.buttonRemovePayment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRemovePayment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(72)))), ((int)(((byte)(125)))));
            this.buttonRemovePayment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRemovePayment.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRemovePayment.ForeColor = System.Drawing.Color.Gainsboro;
            this.buttonRemovePayment.Location = new System.Drawing.Point(4, 452);
            this.buttonRemovePayment.Margin = new System.Windows.Forms.Padding(4);
            this.buttonRemovePayment.Name = "buttonRemovePayment";
            this.buttonRemovePayment.Size = new System.Drawing.Size(110, 25);
            this.buttonRemovePayment.TabIndex = 69;
            this.buttonRemovePayment.Text = "Remove Payment";
            this.buttonRemovePayment.UseVisualStyleBackColor = false;
            this.buttonRemovePayment.Visible = false;
            this.buttonRemovePayment.Click += new System.EventHandler(this.buttonRemovePayment_Click);
            // 
            // inputPaymentAmount
            // 
            this.inputPaymentAmount.BackColor = System.Drawing.Color.Gainsboro;
            this.inputPaymentAmount.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold);
            this.inputPaymentAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(72)))), ((int)(((byte)(125)))));
            this.inputPaymentAmount.Location = new System.Drawing.Point(49, 81);
            this.inputPaymentAmount.Name = "inputPaymentAmount";
            this.inputPaymentAmount.ShortcutsEnabled = false;
            this.inputPaymentAmount.Size = new System.Drawing.Size(71, 26);
            this.inputPaymentAmount.TabIndex = 67;
            this.inputPaymentAmount.TextChanged += new System.EventHandler(this.inputPaymentAmount_TextChanged);
            this.inputPaymentAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.inputPaymentAmount_KeyPress);
            this.inputPaymentAmount.KeyUp += new System.Windows.Forms.KeyEventHandler(this.inputPaymentAmount_KeyUp);
            // 
            // panelPaymentAllocation
            // 
            this.panelPaymentAllocation.Controls.Add(this.labelPrincipalPercent);
            this.panelPaymentAllocation.Controls.Add(this.labelInterestPercent);
            this.panelPaymentAllocation.Controls.Add(this.inputPaymentInterestAmount);
            this.panelPaymentAllocation.Controls.Add(this.inputPaymentPrincipalAmount);
            this.panelPaymentAllocation.Controls.Add(this.labelPaymentInterestAmount);
            this.panelPaymentAllocation.Controls.Add(this.labelInterestPaymentCurrency);
            this.panelPaymentAllocation.Controls.Add(this.labelPaymentPrincipalAmount);
            this.panelPaymentAllocation.Controls.Add(this.labelPaymantAllocation);
            this.panelPaymentAllocation.Controls.Add(this.inputPaymentAllocationTrack);
            this.panelPaymentAllocation.Controls.Add(this.labelPrincipalPaymentCurrency);
            this.panelPaymentAllocation.Location = new System.Drawing.Point(7, 113);
            this.panelPaymentAllocation.Name = "panelPaymentAllocation";
            this.panelPaymentAllocation.Size = new System.Drawing.Size(501, 107);
            this.panelPaymentAllocation.TabIndex = 66;
            // 
            // labelPrincipalPercent
            // 
            this.labelPrincipalPercent.AutoSize = true;
            this.labelPrincipalPercent.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPrincipalPercent.ForeColor = System.Drawing.Color.Gainsboro;
            this.labelPrincipalPercent.Location = new System.Drawing.Point(439, 47);
            this.labelPrincipalPercent.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPrincipalPercent.Name = "labelPrincipalPercent";
            this.labelPrincipalPercent.Size = new System.Drawing.Size(30, 16);
            this.labelPrincipalPercent.TabIndex = 71;
            this.labelPrincipalPercent.Text = "px%";
            // 
            // labelInterestPercent
            // 
            this.labelInterestPercent.AutoSize = true;
            this.labelInterestPercent.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInterestPercent.ForeColor = System.Drawing.Color.Gainsboro;
            this.labelInterestPercent.Location = new System.Drawing.Point(63, 47);
            this.labelInterestPercent.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelInterestPercent.Name = "labelInterestPercent";
            this.labelInterestPercent.Size = new System.Drawing.Size(26, 16);
            this.labelInterestPercent.TabIndex = 70;
            this.labelInterestPercent.Text = "ix%";
            // 
            // inputPaymentInterestAmount
            // 
            this.inputPaymentInterestAmount.BackColor = System.Drawing.Color.Gainsboro;
            this.inputPaymentInterestAmount.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold);
            this.inputPaymentInterestAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(72)))), ((int)(((byte)(125)))));
            this.inputPaymentInterestAmount.Location = new System.Drawing.Point(47, 68);
            this.inputPaymentInterestAmount.Name = "inputPaymentInterestAmount";
            this.inputPaymentInterestAmount.ShortcutsEnabled = false;
            this.inputPaymentInterestAmount.Size = new System.Drawing.Size(71, 26);
            this.inputPaymentInterestAmount.TabIndex = 68;
            this.inputPaymentInterestAmount.TextChanged += new System.EventHandler(this.inputPaymentInterestAmount_TextChanged);
            this.inputPaymentInterestAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.inputPaymentInterestAmount_KeyPress);
            // 
            // inputPaymentPrincipalAmount
            // 
            this.inputPaymentPrincipalAmount.BackColor = System.Drawing.Color.Gainsboro;
            this.inputPaymentPrincipalAmount.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold);
            this.inputPaymentPrincipalAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(72)))), ((int)(((byte)(125)))));
            this.inputPaymentPrincipalAmount.Location = new System.Drawing.Point(418, 66);
            this.inputPaymentPrincipalAmount.Name = "inputPaymentPrincipalAmount";
            this.inputPaymentPrincipalAmount.ShortcutsEnabled = false;
            this.inputPaymentPrincipalAmount.Size = new System.Drawing.Size(71, 26);
            this.inputPaymentPrincipalAmount.TabIndex = 69;
            this.inputPaymentPrincipalAmount.TextChanged += new System.EventHandler(this.inputPaymentPrincipalAmount_TextChanged);
            // 
            // labelPaymentInterestAmount
            // 
            this.labelPaymentInterestAmount.AutoSize = true;
            this.labelPaymentInterestAmount.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPaymentInterestAmount.ForeColor = System.Drawing.Color.Gainsboro;
            this.labelPaymentInterestAmount.Location = new System.Drawing.Point(6, 47);
            this.labelPaymentInterestAmount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPaymentInterestAmount.Name = "labelPaymentInterestAmount";
            this.labelPaymentInterestAmount.Size = new System.Drawing.Size(49, 16);
            this.labelPaymentInterestAmount.TabIndex = 54;
            this.labelPaymentInterestAmount.Text = "Interest";
            // 
            // labelInterestPaymentCurrency
            // 
            this.labelInterestPaymentCurrency.AutoSize = true;
            this.labelInterestPaymentCurrency.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold);
            this.labelInterestPaymentCurrency.ForeColor = System.Drawing.Color.Gainsboro;
            this.labelInterestPaymentCurrency.Location = new System.Drawing.Point(5, 71);
            this.labelInterestPaymentCurrency.Name = "labelInterestPaymentCurrency";
            this.labelInterestPaymentCurrency.Size = new System.Drawing.Size(36, 20);
            this.labelInterestPaymentCurrency.TabIndex = 62;
            this.labelInterestPaymentCurrency.Text = "USD";
            // 
            // labelPaymentPrincipalAmount
            // 
            this.labelPaymentPrincipalAmount.AutoSize = true;
            this.labelPaymentPrincipalAmount.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPaymentPrincipalAmount.ForeColor = System.Drawing.Color.Gainsboro;
            this.labelPaymentPrincipalAmount.Location = new System.Drawing.Point(377, 47);
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
            this.inputPaymentAllocationTrack.LargeChange = 100;
            this.inputPaymentAllocationTrack.Location = new System.Drawing.Point(-3, 19);
            this.inputPaymentAllocationTrack.Maximum = 400;
            this.inputPaymentAllocationTrack.Name = "inputPaymentAllocationTrack";
            this.inputPaymentAllocationTrack.Size = new System.Drawing.Size(501, 45);
            this.inputPaymentAllocationTrack.TabIndex = 53;
            this.inputPaymentAllocationTrack.TickFrequency = 5;
            this.inputPaymentAllocationTrack.ValueChanged += new System.EventHandler(this.inputPaymentAllocationTrack_ValueChanged);
            // 
            // labelPrincipalPaymentCurrency
            // 
            this.labelPrincipalPaymentCurrency.AutoSize = true;
            this.labelPrincipalPaymentCurrency.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold);
            this.labelPrincipalPaymentCurrency.ForeColor = System.Drawing.Color.Gainsboro;
            this.labelPrincipalPaymentCurrency.Location = new System.Drawing.Point(376, 69);
            this.labelPrincipalPaymentCurrency.Name = "labelPrincipalPaymentCurrency";
            this.labelPrincipalPaymentCurrency.Size = new System.Drawing.Size(36, 20);
            this.labelPrincipalPaymentCurrency.TabIndex = 61;
            this.labelPrincipalPaymentCurrency.Text = "USD";
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
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(72)))), ((int)(((byte)(125)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(72)))), ((int)(((byte)(125)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridPaymentList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridPaymentList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridPaymentList.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(72)))), ((int)(((byte)(125)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridPaymentList.DefaultCellStyle = dataGridViewCellStyle2;
            this.gridPaymentList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.gridPaymentList.EnableHeadersVisualStyles = false;
            this.gridPaymentList.GridColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.gridPaymentList.Location = new System.Drawing.Point(7, 227);
            this.gridPaymentList.Margin = new System.Windows.Forms.Padding(4);
            this.gridPaymentList.Name = "gridPaymentList";
            this.gridPaymentList.ReadOnly = true;
            this.gridPaymentList.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(72)))), ((int)(((byte)(125)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridPaymentList.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gridPaymentList.RowHeadersVisible = false;
            this.gridPaymentList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(72)))), ((int)(((byte)(125)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridPaymentList.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.gridPaymentList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridPaymentList.Size = new System.Drawing.Size(500, 214);
            this.gridPaymentList.TabIndex = 64;
            // 
            // buttonCloseAddPayment
            // 
            this.buttonCloseAddPayment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCloseAddPayment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(72)))), ((int)(((byte)(125)))));
            this.buttonCloseAddPayment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCloseAddPayment.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCloseAddPayment.ForeColor = System.Drawing.Color.Gainsboro;
            this.buttonCloseAddPayment.Location = new System.Drawing.Point(402, 452);
            this.buttonCloseAddPayment.Margin = new System.Windows.Forms.Padding(4);
            this.buttonCloseAddPayment.Name = "buttonCloseAddPayment";
            this.buttonCloseAddPayment.Size = new System.Drawing.Size(102, 25);
            this.buttonCloseAddPayment.TabIndex = 63;
            this.buttonCloseAddPayment.Text = "Close";
            this.buttonCloseAddPayment.UseVisualStyleBackColor = false;
            this.buttonCloseAddPayment.Click += new System.EventHandler(this.buttonCloseAddPayment_Click);
            // 
            // labelPaymentCurrency
            // 
            this.labelPaymentCurrency.AutoSize = true;
            this.labelPaymentCurrency.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelPaymentCurrency.ForeColor = System.Drawing.Color.Gainsboro;
            this.labelPaymentCurrency.Location = new System.Drawing.Point(12, 84);
            this.labelPaymentCurrency.Name = "labelPaymentCurrency";
            this.labelPaymentCurrency.Size = new System.Drawing.Size(36, 20);
            this.labelPaymentCurrency.TabIndex = 59;
            this.labelPaymentCurrency.Text = "USD";
            // 
            // labelPaymentAmount
            // 
            this.labelPaymentAmount.AutoSize = true;
            this.labelPaymentAmount.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPaymentAmount.ForeColor = System.Drawing.Color.Gainsboro;
            this.labelPaymentAmount.Location = new System.Drawing.Point(13, 61);
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
            // buttonAddPayment
            // 
            this.buttonAddPayment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddPayment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(72)))), ((int)(((byte)(125)))));
            this.buttonAddPayment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAddPayment.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAddPayment.ForeColor = System.Drawing.Color.Gainsboro;
            this.buttonAddPayment.Location = new System.Drawing.Point(284, 452);
            this.buttonAddPayment.Margin = new System.Windows.Forms.Padding(4);
            this.buttonAddPayment.Name = "buttonAddPayment";
            this.buttonAddPayment.Size = new System.Drawing.Size(110, 25);
            this.buttonAddPayment.TabIndex = 58;
            this.buttonAddPayment.Text = "Add Payment";
            this.buttonAddPayment.UseVisualStyleBackColor = false;
            this.buttonAddPayment.Click += new System.EventHandler(this.buttonAddPayment_Click);
            // 
            // paymentBindingSource
            // 
            this.paymentBindingSource.DataSource = typeof(LoanLifeTracker.Payment);
            // 
            // LoanAdjustments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(5F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.ClientSize = new System.Drawing.Size(508, 481);
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
            this.panelAddPayment.ResumeLayout(false);
            this.panelAddPayment.PerformLayout();
            this.panelPaymentAllocation.ResumeLayout(false);
            this.panelPaymentAllocation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.inputPaymentAllocationTrack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridPaymentList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.paymentBindingSource)).EndInit();
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
        private System.Windows.Forms.Button buttonAddPayment;
        private System.Windows.Forms.Label labelPaymentAmount;
        public System.Windows.Forms.DateTimePicker inputPaymentDate;
        private System.Windows.Forms.Label lablePaymentDate;
        private System.Windows.Forms.Label labelPrincipalAdjustment;
        private System.Windows.Forms.Button buttonAddAdjustment;
        private System.Windows.Forms.Button buttonClosePrincipleAdjust;
        public System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label labelPrincipleAdjustDate;
        public System.Windows.Forms.DataGridView gridPaymentList;
        private System.Windows.Forms.Panel panelPaymentAllocation;
        private System.Windows.Forms.TextBox inputPaymentAmount;
        private System.Windows.Forms.TextBox inputPaymentInterestAmount;
        private System.Windows.Forms.TextBox inputPaymentPrincipalAmount;
        private System.Windows.Forms.TextBox principalAdjustmentAmount;
        private System.Windows.Forms.Label labelPrincipalPercent;
        private System.Windows.Forms.Label labelInterestPercent;
        private System.Windows.Forms.Button buttonRemovePayment;
        private System.Windows.Forms.BindingSource paymentBindingSource;
        private System.Windows.Forms.Label labelSelectedDayInfo;
    }
}