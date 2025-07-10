using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace PG.Core.Windows.ExControls.DateRangeEx
{
    public partial class DateRangeCombobox : UserControl
    {
        private DateTime? m_DateFrom = null;
        private DateTime? m_DateTo = null;

        private DateTime? m_CompareDate = null;

        private DateTime m_CompareDateWork = DateTime.Now;


        private string m_DateFromString = string.Empty;
        private string m_DateToString = string.Empty;

        private string m_DateFormat = "dd-MMM-yyyy";


        private bool m_CloseOnSelection = false;
        private bool m_ShowDayCount = false;
        


        public event EventHandler OKClicked;
        public event EventHandler ClearClicked;

        public DateRangeCombobox()
        {
            InitializeComponent();
        }

        #region Properties


        public DateTime? DateFrom
        {
            get { return m_DateFrom; }
            set { m_DateFrom = value; }
        }

        public DateTime? DateTo
        {
            get { return m_DateTo; }
            set { m_DateTo = value; }
        }

        public String DateFromString
        {
            get { return m_DateFromString; }
        }

        public String DateToString
        {
            get { return m_DateToString; }
        }
        
        public DateTimePicker DateTimePickerFrom
        {
            get { return this.dtpFrom as DateTimePicker; }
        }

        public DateTimePicker DateTimePickerTo
        {
            get { return this.dtpFrom as DateTimePicker; }
        }

        public bool CloseOnSelection
        {
            get { return m_CloseOnSelection; }
            set { m_CloseOnSelection = value; }
        }

        public bool ShowDayCount
        {
            get { return m_ShowDayCount; }
            set { m_ShowDayCount = value; }
        }



        #endregion

        public void RaiseOKClickedEvent()
        {
            EventHandler eventHandler = this.OKClicked;
            if (eventHandler != null)
                this.OKClicked(this, EventArgs.Empty);
        }

        public void RaiseClearClickedEvent()
        {
            EventHandler eventHandler = this.ClearClicked;
            if (eventHandler != null)
                this.ClearClicked(this, EventArgs.Empty);
        }


        public void OKClickedTask()
        {
            m_DateFrom = dtpFrom.Value;
            m_DateTo = dtpTo.Value;

            string txtDate = string.Empty;
            if (dtpFrom.Value == dtpTo.Value)
            {
                txtDate = dtpFrom.Value.ToString(this.m_DateFormat);
            }
            else
            {
                txtDate = dtpFrom.Value.ToString(this.m_DateFormat) + " To " + dtpTo.Value.ToString(this.m_DateFormat);
                if (m_ShowDayCount)
                {
                    int diff = dtpTo.Value.Subtract(dtpFrom.Value).Days + 1;
                    txtDate += " :" + diff + " day(s)";
                }
            }
            cboDates.Text = txtDate;

            cboDates.HideDropDown();
            RaiseOKClickedEvent();
        }


        private void DateRange_Load(object sender, EventArgs e)
        {
            //this.cboDates.DropDownStyle = ComboBoxStyle.DropDownList;
            //this.cboDates.is
            this.cboDates.DropDownControl = pnlDates;
            //cboDates.Enabled = this.Enabled;
            //btnClear.Enabled = this.Enabled;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearDates();
            //ClearClicked(this, new EventArgs());
            RaiseClearClickedEvent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            cboDates.HideDropDown();
        }

        private void cboDates_DropDown(object sender, EventArgs e)
        {
            //dtpFrom.Focus();

            m_CompareDateWork = m_CompareDate.HasValue ? m_CompareDate.Value : DateTime.Now;


            dtpFrom.Value = m_DateFrom.HasValue ? m_DateFrom.Value : m_CompareDateWork;
            dtpTo.Value = m_DateTo.HasValue ? m_DateTo.Value : m_CompareDateWork;

            timer1.Enabled = true;

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            OKClickedTask();
            //m_DateFrom = dtpFrom.Value;
            //m_DateTo = dtpTo.Value;

            //if (dtpFrom.Value == dtpTo.Value)
            //{
            //    cboDates.Text = dtpFrom.Value.ToString(this.m_DateFormat);
            //}
            //else
            //{
            //    cboDates.Text = dtpFrom.Value.ToString(this.m_DateFormat) + " To " + dtpTo.Value.ToString(this.m_DateFormat);
            //}

            //cboDates.HideDropDown();
            //RaiseOKClickedEvent();
        }

        private void cboDates_KeyDown(object sender, KeyEventArgs e)
        {
           // e.Handled = true;
            if (e.KeyCode == Keys.Delete)
            {
                e.Handled = true;
            }
            
        }

        private void cboDates_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void btnToday_Click(object sender, EventArgs e)
        {
            this.dtpFrom.Value = m_CompareDateWork;
            this.dtpTo.Value = m_CompareDateWork;

            if (m_CloseOnSelection) OKClickedTask();
        }

        private void btnThisMonth_Click(object sender, EventArgs e)
        {
            int year = m_CompareDateWork.Year;
            int month = m_CompareDateWork.Month;
            int days = DateTime.DaysInMonth(year, month);

            this.dtpFrom.Value = new DateTime(year,month,1);  
            this.dtpTo.Value = new DateTime(year,month, days);

            if (m_CloseOnSelection) OKClickedTask();
        }

        private void btnThisYear_Click(object sender, EventArgs e)
        {
            int year = m_CompareDateWork.Year;
            this.dtpFrom.Value = new DateTime(year,1, 1);
            this.dtpTo.Value = new DateTime(year, 12, 31);

            if (m_CloseOnSelection) OKClickedTask();
        }

        private void DateRange_Resize(object sender, EventArgs e)
        {
            btnClear.Left = this.Width - btnClear.Width - 2;
            cboDates.Width = this.btnClear.Left - 2;
        }

        private void cboDates_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Focus();
            btnOK.Focus();
            timer1.Enabled = false;
        }
        public void ClearDates()
        {
            this.cboDates.Text = string.Empty;
            m_DateFrom = null;
            m_DateTo = null;
        }
        public void ShowDropDown()
        {
            cboDates.ShowDropDown();
        }
        public void HideDropDown()
        {
            cboDates.HideDropDown();
        }

        private void DateRange_EnabledChanged(object sender, EventArgs e)
        {
            //cboDates.Enabled = this.Enabled;
            //btnClear.Enabled = this.Enabled;
            this.Invalidate();
        }

        private void pnlDates_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnYesterday_Click(object sender, EventArgs e)
        {
            this.dtpFrom.Value = m_CompareDateWork.AddDays(-1);
            this.dtpTo.Value = m_CompareDateWork.AddDays(-1);
            if (m_CloseOnSelection) OKClickedTask();
        }

        private void btnNextDay_Click(object sender, EventArgs e)
        {
            this.dtpFrom.Value = m_CompareDateWork.AddDays(1);
            this.dtpTo.Value = m_CompareDateWork.AddDays(1);
            if (m_CloseOnSelection) OKClickedTask();
        }

        private void btnThisWeek_Click(object sender, EventArgs e)
        {
            int curDayNo = (int)m_CompareDateWork.DayOfWeek;
            DateTime weekStartDate = m_CompareDateWork.AddDays(0 - curDayNo);
            DateTime weekEndDate = m_CompareDateWork.AddDays(6 - curDayNo);
                        

            this.dtpFrom.Value = weekStartDate;
            this.dtpTo.Value = weekEndDate;
            if (m_CloseOnSelection) OKClickedTask();
        }

        private void btnPrevMonth_Click(object sender, EventArgs e)
        {
            int year = m_CompareDateWork.Year;
            int month = m_CompareDateWork.Month - 1;
            month = month < 1 ? 1 : month;
            int days = DateTime.DaysInMonth(year, month);

            this.dtpFrom.Value = new DateTime(year, month, 1);
            this.dtpTo.Value = new DateTime(year, month, days);
            if (m_CloseOnSelection) OKClickedTask();
        }

        private void btnNextMonth_Click(object sender, EventArgs e)
        {
            int year = m_CompareDateWork.Year;
            int month = m_CompareDateWork.Month + 1;
            month = month > 12 ? 12 : month;
            int days = DateTime.DaysInMonth(year, month);

            this.dtpFrom.Value = new DateTime(year, month, 1);
            this.dtpTo.Value = new DateTime(year, month, days);
            if (m_CloseOnSelection) OKClickedTask();
        }

        private void btnPrevYear_Click(object sender, EventArgs e)
        {
            int year = m_CompareDateWork.Year - 1;
            this.dtpFrom.Value = new DateTime(year, 1, 1);
            this.dtpTo.Value = new DateTime(year, 12, 31);
            if (m_CloseOnSelection) OKClickedTask();
        }

        private void btnNextYear_Click(object sender, EventArgs e)
        {
            int year = m_CompareDateWork.Year + 1;
            this.dtpFrom.Value = new DateTime(year, 1, 1);
            this.dtpTo.Value = new DateTime(year, 12, 31);
            if (m_CloseOnSelection) OKClickedTask();
        }

        private void btnPrevWeek_Click(object sender, EventArgs e)
        {
            int curDayNo = (int)m_CompareDateWork.DayOfWeek;
            DateTime weekStartDate = m_CompareDateWork.AddDays(0 - curDayNo);
            DateTime weekEndDate = m_CompareDateWork.AddDays(6 - curDayNo);


            this.dtpFrom.Value = weekStartDate.AddDays(-7);
            this.dtpTo.Value = weekEndDate.AddDays(-7);
            if (m_CloseOnSelection) OKClickedTask();
        }

        private void btnNextWeek_Click(object sender, EventArgs e)
        {
            int curDayNo = (int)m_CompareDateWork.DayOfWeek;
            DateTime weekStartDate = m_CompareDateWork.AddDays(0 - curDayNo);
            DateTime weekEndDate = m_CompareDateWork.AddDays(6 - curDayNo);


            this.dtpFrom.Value = weekStartDate.AddDays(7);
            this.dtpTo.Value = weekEndDate.AddDays(7);
            if (m_CloseOnSelection) OKClickedTask();
        }

        
    }
}
