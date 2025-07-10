using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.InventoryDC
{
    [DBTable(Name = "INDT_APPROVAL_CONFIG")]
    public partial class dcINDT_APPROVAL_CONFIG : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_PERMISSION_ID = 0;
        private int m_DEPARTMENT_ID = 0;
        private string m_CREATE_ALLOW = string.Empty;
        private string m_AUTHO_ALLOW = string.Empty;
        private string m_CHECK_ALLOW = string.Empty;
        private string m_APPROVE_ALLOW = string.Empty;

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


        [DBColumn(Name = "PERMISSION_ID", Storage = "m_PERMISSION_ID", DbType = "107", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int PERMISSION_ID
        {
            get { return this.m_PERMISSION_ID; }
            set
            {
                this.m_PERMISSION_ID = value;
                this.NotifyPropertyChanged("PERMISSION_ID");
            }
        }

        [DBColumn(Name = "DEPARTMENT_ID", Storage = "m_DEPARTMENT_ID", DbType = "107")]
        public int DEPARTMENT_ID
        {
            get { return this.m_DEPARTMENT_ID; }
            set
            {
                this.m_DEPARTMENT_ID = value;
                this.NotifyPropertyChanged("DEPARTMENT_ID");
            }
        }

        [DBColumn(Name = "CREATE_ALLOW", Storage = "m_CREATE_ALLOW", DbType = "126")]
        public string CREATE_ALLOW
        {
            get { return this.m_CREATE_ALLOW; }
            set
            {
                this.m_CREATE_ALLOW = value;
                this.NotifyPropertyChanged("CREATE_ALLOW");
            }
        }

        [DBColumn(Name = "AUTHO_ALLOW", Storage = "m_AUTHO_ALLOW", DbType = "126")]
        public string AUTHO_ALLOW
        {
            get { return this.m_AUTHO_ALLOW; }
            set
            {
                this.m_AUTHO_ALLOW = value;
                this.NotifyPropertyChanged("AUTHO_ALLOW");
            }
        }

        [DBColumn(Name = "CHECK_ALLOW", Storage = "m_CHECK_ALLOW", DbType = "126")]
        public string CHECK_ALLOW
        {
            get { return this.m_CHECK_ALLOW; }
            set
            {
                this.m_CHECK_ALLOW = value;
                this.NotifyPropertyChanged("CHECK_ALLOW");
            }
        }

        [DBColumn(Name = "APPROVE_ALLOW", Storage = "m_APPROVE_ALLOW", DbType = "126")]
        public string APPROVE_ALLOW
        {
            get { return this.m_APPROVE_ALLOW; }
            set
            {
                this.m_APPROVE_ALLOW = value;
                this.NotifyPropertyChanged("APPROVE_ALLOW");
            }
        }

        #endregion //properties
    }
}
