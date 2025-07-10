namespace PG.Core.Windows.ExControls.DateRangeEx
{
    partial class DateRangeCombobox
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnClear = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnToday = new System.Windows.Forms.Button();
            this.btnThisMonth = new System.Windows.Forms.Button();
            this.btnThisYear = new System.Windows.Forms.Button();
            this.btnThisWeek = new System.Windows.Forms.Button();
            this.btnYesterday = new System.Windows.Forms.Button();
            this.btnPrevWeek = new System.Windows.Forms.Button();
            this.btnPrevMonth = new System.Windows.Forms.Button();
            this.btnPrevYear = new System.Windows.Forms.Button();
            this.btnNextYear = new System.Windows.Forms.Button();
            this.btnNextMonth = new System.Windows.Forms.Button();
            this.btnNextWeek = new System.Windows.Forms.Button();
            this.btnNextDay = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.pnlDates = new System.Windows.Forms.Panel();
            this.cboDates = new PG.Core.Windows.ExControls.ComboBoxEx.CustomComboBox();
            this.pnlDates.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClear
            // 
            this.btnClear.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnClear.Location = new System.Drawing.Point(225, 0);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(20, 20);
            this.btnClear.TabIndex = 5;
            this.btnClear.Text = "X";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnToday
            // 
            this.btnToday.Location = new System.Drawing.Point(6, 69);
            this.btnToday.Name = "btnToday";
            this.btnToday.Size = new System.Drawing.Size(69, 23);
            this.btnToday.TabIndex = 6;
            this.btnToday.Text = "Today";
            this.toolTip1.SetToolTip(this.btnToday, "Today");
            this.btnToday.UseVisualStyleBackColor = true;
            this.btnToday.Click += new System.EventHandler(this.btnToday_Click);
            // 
            // btnThisMonth
            // 
            this.btnThisMonth.Location = new System.Drawing.Point(158, 69);
            this.btnThisMonth.Name = "btnThisMonth";
            this.btnThisMonth.Size = new System.Drawing.Size(69, 23);
            this.btnThisMonth.TabIndex = 7;
            this.btnThisMonth.Text = "This Month";
            this.toolTip1.SetToolTip(this.btnThisMonth, "This Month");
            this.btnThisMonth.UseVisualStyleBackColor = true;
            this.btnThisMonth.Click += new System.EventHandler(this.btnThisMonth_Click);
            // 
            // btnThisYear
            // 
            this.btnThisYear.Location = new System.Drawing.Point(234, 69);
            this.btnThisYear.Name = "btnThisYear";
            this.btnThisYear.Size = new System.Drawing.Size(69, 23);
            this.btnThisYear.TabIndex = 8;
            this.btnThisYear.Text = "This Year";
            this.toolTip1.SetToolTip(this.btnThisYear, "This Year");
            this.btnThisYear.UseVisualStyleBackColor = true;
            this.btnThisYear.Click += new System.EventHandler(this.btnThisYear_Click);
            // 
            // btnThisWeek
            // 
            this.btnThisWeek.Location = new System.Drawing.Point(82, 69);
            this.btnThisWeek.Name = "btnThisWeek";
            this.btnThisWeek.Size = new System.Drawing.Size(69, 23);
            this.btnThisWeek.TabIndex = 10;
            this.btnThisWeek.Text = "This Week";
            this.toolTip1.SetToolTip(this.btnThisWeek, "This Week");
            this.btnThisWeek.UseVisualStyleBackColor = true;
            this.btnThisWeek.Click += new System.EventHandler(this.btnThisWeek_Click);
            // 
            // btnYesterday
            // 
            this.btnYesterday.Location = new System.Drawing.Point(6, 100);
            this.btnYesterday.Name = "btnYesterday";
            this.btnYesterday.Size = new System.Drawing.Size(69, 23);
            this.btnYesterday.TabIndex = 11;
            this.btnYesterday.Text = "Prv. Day";
            this.toolTip1.SetToolTip(this.btnYesterday, "Yesterday");
            this.btnYesterday.UseVisualStyleBackColor = true;
            this.btnYesterday.Click += new System.EventHandler(this.btnYesterday_Click);
            // 
            // btnPrevWeek
            // 
            this.btnPrevWeek.Location = new System.Drawing.Point(82, 100);
            this.btnPrevWeek.Name = "btnPrevWeek";
            this.btnPrevWeek.Size = new System.Drawing.Size(69, 23);
            this.btnPrevWeek.TabIndex = 12;
            this.btnPrevWeek.Text = "Prv. Week";
            this.toolTip1.SetToolTip(this.btnPrevWeek, "Previous Week");
            this.btnPrevWeek.UseVisualStyleBackColor = true;
            this.btnPrevWeek.Click += new System.EventHandler(this.btnPrevWeek_Click);
            // 
            // btnPrevMonth
            // 
            this.btnPrevMonth.Location = new System.Drawing.Point(158, 100);
            this.btnPrevMonth.Name = "btnPrevMonth";
            this.btnPrevMonth.Size = new System.Drawing.Size(69, 23);
            this.btnPrevMonth.TabIndex = 13;
            this.btnPrevMonth.Text = "Prv. Month";
            this.toolTip1.SetToolTip(this.btnPrevMonth, "Previous Month");
            this.btnPrevMonth.UseVisualStyleBackColor = true;
            this.btnPrevMonth.Click += new System.EventHandler(this.btnPrevMonth_Click);
            // 
            // btnPrevYear
            // 
            this.btnPrevYear.Location = new System.Drawing.Point(234, 100);
            this.btnPrevYear.Name = "btnPrevYear";
            this.btnPrevYear.Size = new System.Drawing.Size(69, 23);
            this.btnPrevYear.TabIndex = 14;
            this.btnPrevYear.Text = "Prv. Year";
            this.toolTip1.SetToolTip(this.btnPrevYear, "Previous Year");
            this.btnPrevYear.UseVisualStyleBackColor = true;
            this.btnPrevYear.Click += new System.EventHandler(this.btnPrevYear_Click);
            // 
            // btnNextYear
            // 
            this.btnNextYear.Location = new System.Drawing.Point(233, 129);
            this.btnNextYear.Name = "btnNextYear";
            this.btnNextYear.Size = new System.Drawing.Size(69, 23);
            this.btnNextYear.TabIndex = 18;
            this.btnNextYear.Text = "Nxt. Year";
            this.toolTip1.SetToolTip(this.btnNextYear, "Next Year");
            this.btnNextYear.UseVisualStyleBackColor = true;
            this.btnNextYear.Click += new System.EventHandler(this.btnNextYear_Click);
            // 
            // btnNextMonth
            // 
            this.btnNextMonth.Location = new System.Drawing.Point(157, 129);
            this.btnNextMonth.Name = "btnNextMonth";
            this.btnNextMonth.Size = new System.Drawing.Size(69, 23);
            this.btnNextMonth.TabIndex = 17;
            this.btnNextMonth.Text = "Nxt. Month";
            this.toolTip1.SetToolTip(this.btnNextMonth, "Next Month");
            this.btnNextMonth.UseVisualStyleBackColor = true;
            this.btnNextMonth.Click += new System.EventHandler(this.btnNextMonth_Click);
            // 
            // btnNextWeek
            // 
            this.btnNextWeek.Location = new System.Drawing.Point(81, 129);
            this.btnNextWeek.Name = "btnNextWeek";
            this.btnNextWeek.Size = new System.Drawing.Size(69, 23);
            this.btnNextWeek.TabIndex = 16;
            this.btnNextWeek.Text = "Nxt. Week";
            this.toolTip1.SetToolTip(this.btnNextWeek, "Next Week");
            this.btnNextWeek.UseVisualStyleBackColor = true;
            this.btnNextWeek.Click += new System.EventHandler(this.btnNextWeek_Click);
            // 
            // btnNextDay
            // 
            this.btnNextDay.Location = new System.Drawing.Point(5, 129);
            this.btnNextDay.Name = "btnNextDay";
            this.btnNextDay.Size = new System.Drawing.Size(69, 23);
            this.btnNextDay.TabIndex = 15;
            this.btnNextDay.Text = "Nxt. Day";
            this.toolTip1.SetToolTip(this.btnNextDay, "Next Day");
            this.btnNextDay.UseVisualStyleBackColor = true;
            this.btnNextDay.Click += new System.EventHandler(this.btnNextDay_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "From";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "To";
            // 
            // dtpFrom
            // 
            this.dtpFrom.CustomFormat = "dd-MMM-yyyy";
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFrom.Location = new System.Drawing.Point(44, 9);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(185, 20);
            this.dtpFrom.TabIndex = 2;
            // 
            // dtpTo
            // 
            this.dtpTo.CustomFormat = "dd-MMM-yyyy";
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTo.Location = new System.Drawing.Point(44, 34);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(185, 20);
            this.dtpTo.TabIndex = 3;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(242, 11);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(62, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(242, 40);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(62, 23);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pnlDates
            // 
            this.pnlDates.Controls.Add(this.btnNextYear);
            this.pnlDates.Controls.Add(this.btnNextMonth);
            this.pnlDates.Controls.Add(this.btnNextWeek);
            this.pnlDates.Controls.Add(this.btnNextDay);
            this.pnlDates.Controls.Add(this.btnPrevYear);
            this.pnlDates.Controls.Add(this.btnPrevMonth);
            this.pnlDates.Controls.Add(this.btnPrevWeek);
            this.pnlDates.Controls.Add(this.btnYesterday);
            this.pnlDates.Controls.Add(this.btnThisWeek);
            this.pnlDates.Controls.Add(this.btnThisYear);
            this.pnlDates.Controls.Add(this.btnThisMonth);
            this.pnlDates.Controls.Add(this.btnToday);
            this.pnlDates.Controls.Add(this.btnClose);
            this.pnlDates.Controls.Add(this.btnOK);
            this.pnlDates.Controls.Add(this.dtpTo);
            this.pnlDates.Controls.Add(this.dtpFrom);
            this.pnlDates.Controls.Add(this.label2);
            this.pnlDates.Controls.Add(this.label1);
            this.pnlDates.Location = new System.Drawing.Point(3, 30);
            this.pnlDates.Name = "pnlDates";
            this.pnlDates.Size = new System.Drawing.Size(307, 162);
            this.pnlDates.TabIndex = 1;
            this.pnlDates.Visible = false;
            this.pnlDates.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlDates_Paint);
            // 
            // cboDates
            // 
            this.cboDates.AllowResizeDropDown = false;
            this.cboDates.ControlSize = new System.Drawing.Size(1, 1);
            this.cboDates.DropDownControl = null;
            this.cboDates.DropDownSizeMode = PG.Core.Windows.ExControls.ComboBoxEx.CustomComboBox.SizeMode.UseControlSize;
            this.cboDates.DropSize = new System.Drawing.Size(121, 106);
            this.cboDates.Location = new System.Drawing.Point(0, 0);
            this.cboDates.Name = "cboDates";
            this.cboDates.Size = new System.Drawing.Size(220, 21);
            this.cboDates.TabIndex = 0;
            this.cboDates.SelectedIndexChanged += new System.EventHandler(this.cboDates_SelectedIndexChanged);
            this.cboDates.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cboDates_KeyPress);
            this.cboDates.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboDates_KeyDown);
            this.cboDates.DropDown += new System.EventHandler(this.cboDates_DropDown);
            // 
            // DateRange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.pnlDates);
            this.Controls.Add(this.cboDates);
            this.Name = "DateRange";
            this.Size = new System.Drawing.Size(258, 25);
            this.Load += new System.EventHandler(this.DateRange_Load);
            this.Resize += new System.EventHandler(this.DateRange_Resize);
            this.EnabledChanged += new System.EventHandler(this.DateRange_EnabledChanged);
            this.pnlDates.ResumeLayout(false);
            this.pnlDates.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClear;
        private PG.Core.Windows.ExControls.ComboBoxEx.CustomComboBox cboDates;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnToday;
        private System.Windows.Forms.Button btnThisMonth;
        private System.Windows.Forms.Button btnThisYear;
        private System.Windows.Forms.Button btnThisWeek;
        private System.Windows.Forms.Button btnYesterday;
        private System.Windows.Forms.Panel pnlDates;
        private System.Windows.Forms.Button btnPrevYear;
        private System.Windows.Forms.Button btnPrevMonth;
        private System.Windows.Forms.Button btnPrevWeek;
        private System.Windows.Forms.Button btnNextYear;
        private System.Windows.Forms.Button btnNextMonth;
        private System.Windows.Forms.Button btnNextWeek;
        private System.Windows.Forms.Button btnNextDay;
    }
}
