using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.OrganiztionDC
{
    [DBTable(Name = "TBLUSERDEPARTMENT")]
    public partial class dcTBLUSERDEPARTMENT : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_USERDEPTID = 0;
        private int m_USERID = 0;
        private int m_DEPTID = 0;
        private bool m_AllowLogin = false;
        private string m_DEPTCODE = string.Empty;

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


        [DBColumn(Name = "USERDEPTID", Storage = "m_USERDEPTID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int USERDEPTID
        {
            get { return this.m_USERDEPTID; }
            set
            {
                this.m_USERDEPTID = value;
                this.NotifyPropertyChanged("USERDEPTID");
            }
        }

        [DBColumn(Name = "USERID", Storage = "m_USERID", DbType = "107")]
        public int USERID
        {
            get { return this.m_USERID; }
            set
            {
                this.m_USERID = value;
                this.NotifyPropertyChanged("USERID");
            }
        }

        public bool AllowLogin
        {
            get { return this.m_AllowLogin; }
            set
            {
                this.m_AllowLogin = value;
                this.NotifyPropertyChanged("AllowLogin");
            }
        }

        [DBColumn(Name = "DEPTID", Storage = "m_DEPTID", DbType = "107")]
        public int DEPTID
        {
            get { return this.m_DEPTID; }
            set
            {
                this.m_DEPTID = value;
                this.NotifyPropertyChanged("DEPTID");
            }
        }

        [DBColumn(Name = "DEPTCODE", Storage = "m_DEPTCODE", DbType = "126")]
        public string DEPTCODE
        {
            get { return this.m_DEPTCODE; }
            set
            {
                this.m_DEPTCODE = value;
                this.NotifyPropertyChanged("DEPTCODE");
            }
        }

        #endregion //properties

        public string DEPTNAME { get; set; }
    }
}
