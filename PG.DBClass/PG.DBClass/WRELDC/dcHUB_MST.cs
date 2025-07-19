using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.WRELDC
{
    [DBTable(Name = "HUB_MST")]
    public partial class dcHUB_MST : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_HUB_ID = 0;
        private string m_HUB_NAME = string.Empty;
        private decimal m_HUB_TYPE_ID = 0;
        private string m_ADDRESS = string.Empty;
        private string m_PHONE_NO = string.Empty;
        private string m_RESPONSIBLE_PERSON = string.Empty;
        private string m_RP_MOBILE_NO = string.Empty;
        private string m_DESCRIPTION = string.Empty;
        private string m_IS_ACTIVE = string.Empty;
        private string m_CREATE_BY = string.Empty;
        private DateTime? m_CREATE_DATE = null;
        private string m_EDIT_BY = string.Empty;
        private DateTime? m_EDIT_DATE = null;
        private int m_DIST_ID = 0;
        private int m_TOWN_ID = 0;
        


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


        [DBColumn(Name = "HUB_ID", Storage = "m_HUB_ID", DbType = "107", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int HUB_ID
        {
            get { return this.m_HUB_ID; }
            set
            {
                this.m_HUB_ID = value;
                this.NotifyPropertyChanged("HUB_ID");
            }
        }

        [DBColumn(Name = "HUB_NAME", Storage = "m_HUB_NAME", DbType = "126")]
        public string HUB_NAME
        {
            get { return this.m_HUB_NAME; }
            set
            {
                this.m_HUB_NAME = value;
                this.NotifyPropertyChanged("HUB_NAME");
            }
        }

        [DBColumn(Name = "HUB_TYPE_ID", Storage = "m_HUB_TYPE_ID", DbType = "107")]
        public decimal HUB_TYPE_ID
        {
            get { return this.m_HUB_TYPE_ID; }
            set
            {
                this.m_HUB_TYPE_ID = value;
                this.NotifyPropertyChanged("HUB_TYPE_ID");
            }
        }

        [DBColumn(Name = "ADDRESS", Storage = "m_ADDRESS", DbType = "126")]
        public string ADDRESS
        {
            get { return this.m_ADDRESS; }
            set
            {
                this.m_ADDRESS = value;
                this.NotifyPropertyChanged("ADDRESS");
            }
        }

        [DBColumn(Name = "PHONE_NO", Storage = "m_PHONE_NO", DbType = "126")]
        public string PHONE_NO
        {
            get { return this.m_PHONE_NO; }
            set
            {
                this.m_PHONE_NO = value;
                this.NotifyPropertyChanged("PHONE_NO");
            }
        }

        [DBColumn(Name = "RESPONSIBLE_PERSON", Storage = "m_RESPONSIBLE_PERSON", DbType = "126")]
        public string RESPONSIBLE_PERSON
        {
            get { return this.m_RESPONSIBLE_PERSON; }
            set
            {
                this.m_RESPONSIBLE_PERSON = value;
                this.NotifyPropertyChanged("RESPONSIBLE_PERSON");
            }
        }

        [DBColumn(Name = "RP_MOBILE_NO", Storage = "m_RP_MOBILE_NO", DbType = "126")]
        public string RP_MOBILE_NO
        {
            get { return this.m_RP_MOBILE_NO; }
            set
            {
                this.m_RP_MOBILE_NO = value;
                this.NotifyPropertyChanged("RP_MOBILE_NO");
            }
        }

        [DBColumn(Name = "DESCRIPTION", Storage = "m_DESCRIPTION", DbType = "126")]
        public string DESCRIPTION
        {
            get { return this.m_DESCRIPTION; }
            set
            {
                this.m_DESCRIPTION = value;
                this.NotifyPropertyChanged("DESCRIPTION");
            }
        }

        [DBColumn(Name = "IS_ACTIVE", Storage = "m_IS_ACTIVE", DbType = "126")]
        public string IS_ACTIVE
        {
            get { return this.m_IS_ACTIVE; }
            set
            {
                this.m_IS_ACTIVE = value;
                this.NotifyPropertyChanged("IS_ACTIVE");
            }
        }

        [DBColumn(Name = "CREATE_BY", Storage = "m_CREATE_BY", DbType = "126")]
        public string CREATE_BY
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

        [DBColumn(Name = "EDIT_BY", Storage = "m_EDIT_BY", DbType = "126")]
        public string EDIT_BY
        {
            get { return this.m_EDIT_BY; }
            set
            {
                this.m_EDIT_BY = value;
                this.NotifyPropertyChanged("EDIT_BY");
            }
        }

        [DBColumn(Name = "EDIT_DATE", Storage = "m_EDIT_DATE", DbType = "106")]
        public DateTime? EDIT_DATE
        {
            get { return this.m_EDIT_DATE; }
            set
            {
                this.m_EDIT_DATE = value;
                this.NotifyPropertyChanged("EDIT_DATE");
            }
        }

        [DBColumn(Name = "DIST_ID", Storage = "m_DIST_ID", DbType = "107")]
        public int DIST_ID
        {
            get { return this.m_DIST_ID; }
            set
            {
                this.m_DIST_ID = value;
                this.NotifyPropertyChanged("DIST_ID");
            }
        }

        [DBColumn(Name = "TOWN_ID", Storage = "m_TOWN_ID", DbType = "107")]
        public int TOWN_ID
        {
            get { return this.m_TOWN_ID; }
            set
            {
                this.m_TOWN_ID = value;
                this.NotifyPropertyChanged("TOWN_ID");
            }
        }

        #endregion //properties
    }

    public partial class dcHUB_MST
    {
        public string DIST_NAME { get; set; }
        public string TOWN_NAME { get; set; }


    }
}
