using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
    [Serializable]
    [DBTable(Name = "TBLUSER")]
    public partial class dcTBLUSER : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private decimal m_USERID = 0;
        private decimal m_APPID = 0;
        private string m_USERNAME = string.Empty;
        private string m_PASSWORD = string.Empty;
        private string m_FULLNAME = string.Empty;
        private string m_DESIGNATION = string.Empty;
        private string m_EMAIL = string.Empty;
        private string m_USERTYPE = string.Empty;
        private decimal m_USERLEVEL = 0;
        private decimal m_ROLEID = 0;
        private string m_LOGUSER = string.Empty;
        private string m_LOGLOCAL = string.Empty;
        private string m_ISACTIVE = string.Empty;
        private string m_ISSYSTEM = string.Empty;
        private string m_ISVISIBLE = string.Empty;
        private string m_MRESTRICT = string.Empty;
        private string m_MNAME1 = string.Empty;
        private string m_MNAME2 = string.Empty;
        private string m_MNAME3 = string.Empty;
        private DateTime? m_USERCREATEDT = null;
        private decimal m_USERLOGINTYPEID = 0;
        private decimal m_USERIDCENTER = 0;
        private string m_EMPCODE = string.Empty;
        private decimal m_EMPID = 0;
        private string m_DEPTCODE = string.Empty;
        private string m_CONCATE = string.Empty;
        private string m_SRVRPLAUTHORITY = string.Empty;
        private string m_CONTACT_NO = string.Empty;
        private string m_LOCATIONCODE = string.Empty;
        private int m_MS_PRC = 0;
        private string m_MS_RPT_MARK = string.Empty;
       
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


        [DBColumn(Name = "USERID", Storage = "m_USERID", DbType = "107", IsPrimaryKey = true)]
        public decimal USERID
        {
            get { return this.m_USERID; }
            set
            {
                this.m_USERID = value;
                this.NotifyPropertyChanged("USERID");
            }
        }

        [DBColumn(Name = "APPID", Storage = "m_APPID", DbType = "107")]
        public decimal APPID
        {
            get { return this.m_APPID; }
            set
            {
                this.m_APPID = value;
                this.NotifyPropertyChanged("APPID");
            }
        }

        [DBColumn(Name = "USERNAME", Storage = "m_USERNAME", DbType = "126")]
        public string USERNAME
        {
            get { return this.m_USERNAME; }
            set
            {
                this.m_USERNAME = value;
                this.NotifyPropertyChanged("USERNAME");
            }
        }

        [DBColumn(Name = "PASSWORD", Storage = "m_PASSWORD", DbType = "126")]
        public string PASSWORD
        {
            get { return this.m_PASSWORD; }
            set
            {
                this.m_PASSWORD = value;
                this.NotifyPropertyChanged("PASSWORD");
            }
        }

        [DBColumn(Name = "FULLNAME", Storage = "m_FULLNAME", DbType = "126")]
        public string FULLNAME
        {
            get { return this.m_FULLNAME; }
            set
            {
                this.m_FULLNAME = value;
                this.NotifyPropertyChanged("FULLNAME");
            }
        }

        [DBColumn(Name = "DESIGNATION", Storage = "m_DESIGNATION", DbType = "126")]
        public string DESIGNATION
        {
            get { return this.m_DESIGNATION; }
            set
            {
                this.m_DESIGNATION = value;
                this.NotifyPropertyChanged("DESIGNATION");
            }
        }

        [DBColumn(Name = "EMAIL", Storage = "m_EMAIL", DbType = "126")]
        public string EMAIL
        {
            get { return this.m_EMAIL; }
            set
            {
                this.m_EMAIL = value;
                this.NotifyPropertyChanged("EMAIL");
            }
        }

        [DBColumn(Name = "USERTYPE", Storage = "m_USERTYPE", DbType = "126")]
        public string USERTYPE
        {
            get { return this.m_USERTYPE; }
            set
            {
                this.m_USERTYPE = value;
                this.NotifyPropertyChanged("USERTYPE");
            }
        }

        [DBColumn(Name = "USERLEVEL", Storage = "m_USERLEVEL", DbType = "107")]
        public decimal USERLEVEL
        {
            get { return this.m_USERLEVEL; }
            set
            {
                this.m_USERLEVEL = value;
                this.NotifyPropertyChanged("USERLEVEL");
            }
        }

        [DBColumn(Name = "ROLEID", Storage = "m_ROLEID", DbType = "107")]
        public decimal ROLEID
        {
            get { return this.m_ROLEID; }
            set
            {
                this.m_ROLEID = value;
                this.NotifyPropertyChanged("ROLEID");
            }
        }

        [DBColumn(Name = "LOGUSER", Storage = "m_LOGUSER", DbType = "119")]
        public string LOGUSER
        {
            get { return this.m_LOGUSER; }
            set
            {
                this.m_LOGUSER = value;
                this.NotifyPropertyChanged("LOGUSER");
            }
        }

        [DBColumn(Name = "LOGLOCAL", Storage = "m_LOGLOCAL", DbType = "119")]
        public string LOGLOCAL
        {
            get { return this.m_LOGLOCAL; }
            set
            {
                this.m_LOGLOCAL = value;
                this.NotifyPropertyChanged("LOGLOCAL");
            }
        }

        [DBColumn(Name = "ISACTIVE", Storage = "m_ISACTIVE", DbType = "119")]
        public string ISACTIVE
        {
            get { return this.m_ISACTIVE; }
            set
            {
                this.m_ISACTIVE = value;
                this.NotifyPropertyChanged("ISACTIVE");
            }
        }

        [DBColumn(Name = "ISSYSTEM", Storage = "m_ISSYSTEM", DbType = "119")]
        public string ISSYSTEM
        {
            get { return this.m_ISSYSTEM; }
            set
            {
                this.m_ISSYSTEM = value;
                this.NotifyPropertyChanged("ISSYSTEM");
            }
        }

        [DBColumn(Name = "ISVISIBLE", Storage = "m_ISVISIBLE", DbType = "119")]
        public string ISVISIBLE
        {
            get { return this.m_ISVISIBLE; }
            set
            {
                this.m_ISVISIBLE = value;
                this.NotifyPropertyChanged("ISVISIBLE");
            }
        }

        [DBColumn(Name = "MRESTRICT", Storage = "m_MRESTRICT", DbType = "119")]
        public string MRESTRICT
        {
            get { return this.m_MRESTRICT; }
            set
            {
                this.m_MRESTRICT = value;
                this.NotifyPropertyChanged("MRESTRICT");
            }
        }

        [DBColumn(Name = "MNAME1", Storage = "m_MNAME1", DbType = "126")]
        public string MNAME1
        {
            get { return this.m_MNAME1; }
            set
            {
                this.m_MNAME1 = value;
                this.NotifyPropertyChanged("MNAME1");
            }
        }

        [DBColumn(Name = "MNAME2", Storage = "m_MNAME2", DbType = "126")]
        public string MNAME2
        {
            get { return this.m_MNAME2; }
            set
            {
                this.m_MNAME2 = value;
                this.NotifyPropertyChanged("MNAME2");
            }
        }

        [DBColumn(Name = "MNAME3", Storage = "m_MNAME3", DbType = "126")]
        public string MNAME3
        {
            get { return this.m_MNAME3; }
            set
            {
                this.m_MNAME3 = value;
                this.NotifyPropertyChanged("MNAME3");
            }
        }

        [DBColumn(Name = "USERCREATEDT", Storage = "m_USERCREATEDT", DbType = "106")]
        public DateTime? USERCREATEDT
        {
            get { return this.m_USERCREATEDT; }
            set
            {
                this.m_USERCREATEDT = value;
                this.NotifyPropertyChanged("USERCREATEDT");
            }
        }

        [DBColumn(Name = "USERLOGINTYPEID", Storage = "m_USERLOGINTYPEID", DbType = "107")]
        public decimal USERLOGINTYPEID
        {
            get { return this.m_USERLOGINTYPEID; }
            set
            {
                this.m_USERLOGINTYPEID = value;
                this.NotifyPropertyChanged("USERLOGINTYPEID");
            }
        }

        [DBColumn(Name = "USERIDCENTER", Storage = "m_USERIDCENTER", DbType = "107")]
        public decimal USERIDCENTER
        {
            get { return this.m_USERIDCENTER; }
            set
            {
                this.m_USERIDCENTER = value;
                this.NotifyPropertyChanged("USERIDCENTER");
            }
        }

        [DBColumn(Name = "EMPCODE", Storage = "m_EMPCODE", DbType = "126")]
        public string EMPCODE
        {
            get { return this.m_EMPCODE; }
            set
            {
                this.m_EMPCODE = value;
                this.NotifyPropertyChanged("EMPCODE");
            }
        }

        [DBColumn(Name = "EMPID", Storage = "m_EMPID", DbType = "107")]
        public decimal EMPID
        {
            get { return this.m_EMPID; }
            set
            {
                this.m_EMPID = value;
                this.NotifyPropertyChanged("EMPID");
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

        [DBColumn(Name = "CONCATE", Storage = "m_CONCATE", DbType = "126")]
        public string CONCATE
        {
            get { return this.m_CONCATE; }
            set
            {
                this.m_CONCATE = value;
                this.NotifyPropertyChanged("CONCATE");
            }
        }

        [DBColumn(Name = "SRVRPLAUTHORITY", Storage = "m_SRVRPLAUTHORITY", DbType = "126")]
        public string SRVRPLAUTHORITY
        {
            get { return this.m_SRVRPLAUTHORITY; }
            set
            {
                this.m_SRVRPLAUTHORITY = value;
                this.NotifyPropertyChanged("SRVRPLAUTHORITY");
            }
        }

        [DBColumn(Name = "CONTACT_NO", Storage = "m_CONTACT_NO", DbType = "126")]
        public string CONTACT_NO
        {
            get { return this.m_CONTACT_NO; }
            set
            {
                this.m_CONTACT_NO = value;
                this.NotifyPropertyChanged("CONTACT_NO");
            }
        }

        [DBColumn(Name = "LOCATIONCODE", Storage = "m_LOCATIONCODE", DbType = "126")]
        public string LOCATIONCODE
        {
            get { return this.m_LOCATIONCODE; }
            set
            {
                this.m_LOCATIONCODE = value;
                this.NotifyPropertyChanged("LOCATIONCODE");
            }
        }

        [DBColumn(Name = "MS_PRC", Storage = "m_MS_PRC", DbType = "107")]
        public int MS_PRC
        {
            get { return this.m_MS_PRC; }
            set
            {
                this.m_MS_PRC = value;
                this.NotifyPropertyChanged("MS_PRC");
            }
        }

        [DBColumn(Name = "MS_RPT_MARK", Storage = "m_MS_RPT_MARK", DbType = "126")]
        public string MS_RPT_MARK
        {
            get { return this.m_MS_RPT_MARK; }
            set
            {
                this.m_MS_RPT_MARK = value;
                this.NotifyPropertyChanged("MS_RPT_MARK");
            }
        }

     




        #endregion //properties
    }
}
