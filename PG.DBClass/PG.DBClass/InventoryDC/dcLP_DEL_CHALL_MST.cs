using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
    [DBTable(Name = "LP_DEL_CHALL_MST")]
   public class dcLP_DEL_CHALL_MST : DBBaseClass, INotifyPropertyChanged 
    {
        #region private members

        private string m_CHALL_NO = string.Empty;
        private DateTime? m_CHALL_DT = null;
        private string m_DEL_CHALL_NO = string.Empty;
        private string m_LOT_NO = string.Empty;
        private string m_SUP_CODE = string.Empty;
        private string m_DISTRICT_CODE = string.Empty;
        private string m_DIVISION_CODE = string.Empty;
        private string m_BRANCH_CODE = string.Empty;
        private string m_DEPARTMENT_CODE = string.Empty;
        private string m_USER_ID = string.Empty;
        private DateTime? m_ENTER_DATE = null;
        private string m_COMP_ID = string.Empty;
        private DateTime? m_CHALL_RCV_DATE = null;
        private string m_DELIVERY_BY = string.Empty;
        private string m_DELIVERY_PERSON = string.Empty;
        private string m_SCALE_NO = string.Empty;
        private string m_VEHICLE_NO = string.Empty;
        private string m_MOKAM_NAME = string.Empty;

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


        [DBColumn(Name = "CHALL_NO", Storage = "m_CHALL_NO", DbType = "126", IsPrimaryKey = true)]
        public string CHALL_NO
        {
            get { return this.m_CHALL_NO; }
            set
            {
                this.m_CHALL_NO = value;
                this.NotifyPropertyChanged("CHALL_NO");
            }
        }

        [DBColumn(Name = "CHALL_DT", Storage = "m_CHALL_DT", DbType = "106")]
        public DateTime? CHALL_DT
        {
            get { return this.m_CHALL_DT; }
            set
            {
                this.m_CHALL_DT = value;
                this.NotifyPropertyChanged("CHALL_DT");
            }
        }

        [DBColumn(Name = "DEL_CHALL_NO", Storage = "m_DEL_CHALL_NO", DbType = "126")]
        public string DEL_CHALL_NO
        {
            get { return this.m_DEL_CHALL_NO; }
            set
            {
                this.m_DEL_CHALL_NO = value;
                this.NotifyPropertyChanged("DEL_CHALL_NO");
            }
        }

        [DBColumn(Name = "LOT_NO", Storage = "m_LOT_NO", DbType = "126")]
        public string LOT_NO
        {
            get { return this.m_LOT_NO; }
            set
            {
                this.m_LOT_NO = value;
                this.NotifyPropertyChanged("LOT_NO");
            }
        }

        [DBColumn(Name = "SUP_CODE", Storage = "m_SUP_CODE", DbType = "126", IsPrimaryKey = true)]
        public string SUP_CODE
        {
            get { return this.m_SUP_CODE; }
            set
            {
                this.m_SUP_CODE = value;
                this.NotifyPropertyChanged("SUP_CODE");
            }
        }

        [DBColumn(Name = "DISTRICT_CODE", Storage = "m_DISTRICT_CODE", DbType = "126")]
        public string DISTRICT_CODE
        {
            get { return this.m_DISTRICT_CODE; }
            set
            {
                this.m_DISTRICT_CODE = value;
                this.NotifyPropertyChanged("DISTRICT_CODE");
            }
        }

        [DBColumn(Name = "DIVISION_CODE", Storage = "m_DIVISION_CODE", DbType = "126")]
        public string DIVISION_CODE
        {
            get { return this.m_DIVISION_CODE; }
            set
            {
                this.m_DIVISION_CODE = value;
                this.NotifyPropertyChanged("DIVISION_CODE");
            }
        }

        [DBColumn(Name = "BRANCH_CODE", Storage = "m_BRANCH_CODE", DbType = "126")]
        public string BRANCH_CODE
        {
            get { return this.m_BRANCH_CODE; }
            set
            {
                this.m_BRANCH_CODE = value;
                this.NotifyPropertyChanged("BRANCH_CODE");
            }
        }

        [DBColumn(Name = "DEPARTMENT_CODE", Storage = "m_DEPARTMENT_CODE", DbType = "126")]
        public string DEPARTMENT_CODE
        {
            get { return this.m_DEPARTMENT_CODE; }
            set
            {
                this.m_DEPARTMENT_CODE = value;
                this.NotifyPropertyChanged("DEPARTMENT_CODE");
            }
        }

        [DBColumn(Name = "USER_ID", Storage = "m_USER_ID", DbType = "126")]
        public string USER_ID
        {
            get { return this.m_USER_ID; }
            set
            {
                this.m_USER_ID = value;
                this.NotifyPropertyChanged("USER_ID");
            }
        }

        [DBColumn(Name = "ENTER_DATE", Storage = "m_ENTER_DATE", DbType = "106")]
        public DateTime? ENTER_DATE
        {
            get { return this.m_ENTER_DATE; }
            set
            {
                this.m_ENTER_DATE = value;
                this.NotifyPropertyChanged("ENTER_DATE");
            }
        }

        [DBColumn(Name = "COMP_ID", Storage = "m_COMP_ID", DbType = "126", IsPrimaryKey = true)]
        public string COMP_ID
        {
            get { return this.m_COMP_ID; }
            set
            {
                this.m_COMP_ID = value;
                this.NotifyPropertyChanged("COMP_ID");
            }
        }

        [DBColumn(Name = "CHALL_RCV_DATE", Storage = "m_CHALL_RCV_DATE", DbType = "106")]
        public DateTime? CHALL_RCV_DATE
        {
            get { return this.m_CHALL_RCV_DATE; }
            set
            {
                this.m_CHALL_RCV_DATE = value;
                this.NotifyPropertyChanged("CHALL_RCV_DATE");
            }
        }

        [DBColumn(Name = "DELIVERY_BY", Storage = "m_DELIVERY_BY", DbType = "126")]
        public string DELIVERY_BY
        {
            get { return this.m_DELIVERY_BY; }
            set
            {
                this.m_DELIVERY_BY = value;
                this.NotifyPropertyChanged("DELIVERY_BY");
            }
        }

        [DBColumn(Name = "DELIVERY_PERSON", Storage = "m_DELIVERY_PERSON", DbType = "126")]
        public string DELIVERY_PERSON
        {
            get { return this.m_DELIVERY_PERSON; }
            set
            {
                this.m_DELIVERY_PERSON = value;
                this.NotifyPropertyChanged("DELIVERY_PERSON");
            }
        }

        [DBColumn(Name = "SCALE_NO", Storage = "m_SCALE_NO", DbType = "126")]
        public string SCALE_NO
        {
            get { return this.m_SCALE_NO; }
            set
            {
                this.m_SCALE_NO = value;
                this.NotifyPropertyChanged("SCALE_NO");
            }
        }

        [DBColumn(Name = "VEHICLE_NO", Storage = "m_VEHICLE_NO", DbType = "126")]
        public string VEHICLE_NO
        {
            get { return this.m_VEHICLE_NO; }
            set
            {
                this.m_VEHICLE_NO = value;
                this.NotifyPropertyChanged("VEHICLE_NO");
            }
        }

        [DBColumn(Name = "MOKAM_NAME", Storage = "m_MOKAM_NAME", DbType = "126")]
        public string MOKAM_NAME
        {
            get { return this.m_MOKAM_NAME; }
            set
            {
                this.m_MOKAM_NAME = value;
                this.NotifyPropertyChanged("MOKAM_NAME");
            }
        }

        #endregion //properties 
    }
}
