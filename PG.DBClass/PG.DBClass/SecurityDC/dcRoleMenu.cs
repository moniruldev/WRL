using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.SecurityDC
{
    [Serializable]
    [DBTable(Name = "tblRoleMenu")]
    public partial class dcRoleMenu : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_ROLEMENUID = 0;
        private int m_APPID = 0;
        private int m_ROLEID = 0;
        private int m_APPMENUID = 0;
        private bool m_SHOWMENU = false;

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


        [DBColumn(Name = "ROLEMENUID", Storage = "m_ROLEMENUID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int ROLEMENUID
        {
            get { return this.m_ROLEMENUID; }
            set
            {
                this.m_ROLEMENUID = value;
                this.NotifyPropertyChanged("ROLEMENUID");
            }
        }

        [DBColumn(Name = "APPID", Storage = "m_APPID", DbType = "Int NULL")]
        public int APPID
        {
            get { return this.m_APPID; }
            set
            {
                this.m_APPID = value;
                this.NotifyPropertyChanged("APPID");
            }
        }

        [DBColumn(Name = "ROLEID", Storage = "m_ROLEID", DbType = "Int NULL")]
        public int ROLEID
        {
            get { return this.m_ROLEID; }
            set
            {
                this.m_ROLEID = value;
                this.NotifyPropertyChanged("ROLEID");
            }
        }

        [DBColumn(Name = "APPMENUID", Storage = "m_APPMENUID", DbType = "Int NULL")]
        public int APPMENUID
        {
            get { return this.m_APPMENUID; }
            set
            {
                this.m_APPMENUID = value;
                this.NotifyPropertyChanged("APPMENUID");
            }
        }

        [DBColumn(Name = "SHOWMENU", Storage = "m_SHOWMENU", DbType = "Bit NULL")]
        public bool SHOWMENU
        {
            get { return this.m_SHOWMENU; }
            set
            {
                this.m_SHOWMENU = value;
                this.NotifyPropertyChanged("SHOWMENU");
            }
        }

        #endregion //properties
    }

     public partial class dcRoleMenu
     {
         string m_ROLE_NAME = "";

         public string ROLE_NAME
         {
             get { return this.m_ROLE_NAME; }
             set
             {
                 this.m_ROLE_NAME = value;
             }
         }
     }
}
