using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;

namespace PG.DBClass.ProductionDC
{
    [DBTable(Name = "PROD_WORKING_MONTH")]
    public partial class dcPROD_WORKING_MONTH : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_WORKING_MONTH_ID;
        private int m_YEAR = 0;
        private int m_MONTH = 0;
        private DateTime? m_START_DATE = null;
        private DateTime? m_END_DATE = null;
        private int m_CREATE_BY = 0;
        private DateTime? m_CREATE_DATE = null;
        private string m_IS_OPEN = "Y";


        #endregion  //private members

        #region public events

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string info)
        {
            _UpdateChangedList(info);
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        #endregion //public events

        #region properties


        [DBColumn(Name = "WORKING_MONTH_ID", Storage = "m_WORKING_MONTH_ID", DbType = "126", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int WORKING_MONTH_ID
        {
            get { return this.m_WORKING_MONTH_ID; }
            set
            {
                this.m_WORKING_MONTH_ID = value;
                this.NotifyPropertyChanged("WORKING_MONTH_ID");
            }
        }

        [DBColumn(Name = "YEAR", Storage = "m_YEAR", DbType = "107")]
        public int YEAR
        {
            get { return this.m_YEAR; }
            set
            {
                this.m_YEAR = value;
                this.NotifyPropertyChanged("YEAR");
            }
        }

        [DBColumn(Name = "MONTH", Storage = "m_MONTH", DbType = "107")]
        public int MONTH
        {
            get { return this.m_MONTH; }
            set
            {
                this.m_MONTH = value;
                this.NotifyPropertyChanged("MONTH");
            }
        }

        [DBColumn(Name = "START_DATE", Storage = "m_START_DATE", DbType = "106")]
        public DateTime? START_DATE
        {
            get { return this.m_START_DATE; }
            set
            {
                this.m_START_DATE = value;
                this.NotifyPropertyChanged("START_DATE");
            }
        }

        [DBColumn(Name = "END_DATE", Storage = "m_END_DATE", DbType = "106")]
        public DateTime? END_DATE
        {
            get { return this.m_END_DATE; }
            set
            {
                this.m_END_DATE = value;
                this.NotifyPropertyChanged("END_DATE");
            }
        }

        [DBColumn(Name = "CREATE_BY", Storage = "m_CREATE_BY", DbType = "107")]
        public int CREATE_BY
        {
            get { return this.m_CREATE_BY; }
            set
            {
                this.m_CREATE_BY = value;
                this.NotifyPropertyChanged("CREATE_BY");
            }
        }

        [DBColumn(Name = "CREATE_DATE", Storage = "m_CREATE_DATE", DbType = "106")]
        public DateTime? CREATE_DATE
        {
            get { return this.m_CREATE_DATE; }
            set
            {
                this.m_CREATE_DATE = value;
                this.NotifyPropertyChanged("CREATE_DATE");
            }
        }


        [DBColumn(Name = "IS_OPEN", Storage = "m_IS_OPEN", DbType = "107")]
        public string IS_OPEN
        {
            get { return this.m_IS_OPEN; }
            set
            {
                this.m_IS_OPEN = value;
                this.NotifyPropertyChanged("IS_OPEN");
            }
        }



        #endregion //properties
    }

    //public partial class dcPROD_WORKING_MONTH
    //{
    //    public string FULLNAME { get; set; }

    //    public string MONTH_NAME
    //    {

    //        get
    //        {
    //            return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(this.MONTH);

    //        }

    //    }
    //}

}
