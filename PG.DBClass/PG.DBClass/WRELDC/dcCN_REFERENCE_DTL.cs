using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.WRELDC
{
    [DBTable(Name = "CN_REFERENCE_DTL")]
    public partial class dcCN_REFERENCE_DTL : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_CN_REF_DTL_ID = 0;
        private int m_CN_ID = 0;
        private string m_REF_CLIENT_CODE = string.Empty;
        private string m_REF_MOBILE_NO = string.Empty;
        private string m_REF_CHALLAN_NO = string.Empty;
        private string m_REF_ACCOUNT_NO = string.Empty;
        private string m_CREATE_BY = string.Empty;
        private DateTime? m_CREATE_DATE = null;
        private string m_EDIT_BY = string.Empty;
        private DateTime? m_EDIT_DATE = null;
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


        [DBColumn(Name = "CN_REF_DTL_ID", Storage = "m_CN_REF_DTL_ID", DbType = "107", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int CN_REF_DTL_ID
        {
            get { return this.m_CN_REF_DTL_ID; }
            set
            {
                this.m_CN_REF_DTL_ID = value;
                this.NotifyPropertyChanged("CN_REF_DTL_ID");
            }
        }

        [DBColumn(Name = "CN_ID", Storage = "m_CN_ID", DbType = "107")]
        public int CN_ID
        {
            get { return this.m_CN_ID; }
            set
            {
                this.m_CN_ID = value;
                this.NotifyPropertyChanged("CN_ID");
            }
        }

        [DBColumn(Name = "REF_CLIENT_CODE", Storage = "m_REF_CLIENT_CODE", DbType = "126")]
        public string REF_CLIENT_CODE
        {
            get { return this.m_REF_CLIENT_CODE; }
            set
            {
                this.m_REF_CLIENT_CODE = value;
                this.NotifyPropertyChanged("REF_CLIENT_CODE");
            }
        }

        [DBColumn(Name = "REF_MOBILE_NO", Storage = "m_REF_MOBILE_NO", DbType = "126")]
        public string REF_MOBILE_NO
        {
            get { return this.m_REF_MOBILE_NO; }
            set
            {
                this.m_REF_MOBILE_NO = value;
                this.NotifyPropertyChanged("REF_MOBILE_NO");
            }
        }

        [DBColumn(Name = "REF_CHALLAN_NO", Storage = "m_REF_CHALLAN_NO", DbType = "126")]
        public string REF_CHALLAN_NO
        {
            get { return this.m_REF_CHALLAN_NO; }
            set
            {
                this.m_REF_CHALLAN_NO = value;
                this.NotifyPropertyChanged("REF_CHALLAN_NO");
            }
        }

        [DBColumn(Name = "REF_ACCOUNT_NO", Storage = "m_REF_ACCOUNT_NO", DbType = "126")]
        public string REF_ACCOUNT_NO
        {
            get { return this.m_REF_ACCOUNT_NO; }
            set
            {
                this.m_REF_ACCOUNT_NO = value;
                this.NotifyPropertyChanged("REF_ACCOUNT_NO");
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
        #endregion //properties
    }

    public partial class dcCN_REFERENCE_DTL
    {
        private string m_DESTINATION_DIST_NAME = "";
        private string m_DESTINATION_TOWN_NAME = "";
        private string m_CLIENT_NAME = "";
        private string m_AGREEMENT_DESCRIPTION = "";

        private string m_ROUTE_NAME = "";
        private string m_HUB_NAME = "";

        public string DESTINATION_DIST_NAME
        {
            get { return this.m_DESTINATION_DIST_NAME; }
            set { this.m_DESTINATION_DIST_NAME = value; }
        }

        public string DESTINATION_TOWN_NAME
        {
            get { return this.m_DESTINATION_TOWN_NAME; }
            set { this.m_DESTINATION_TOWN_NAME = value; }
        }

        public string CLIENT_NAME
        {
            get { return this.m_CLIENT_NAME; }
            set { this.m_CLIENT_NAME = value; }
        }

        public string AGREEMENT_DESCRIPTION
        {
            get { return this.m_AGREEMENT_DESCRIPTION; }
            set { this.m_AGREEMENT_DESCRIPTION = value; }
        }



        public string ROUTE_NAME
        {
            get { return this.m_ROUTE_NAME; }
            set { this.m_ROUTE_NAME = value; }
        }

        public string HUB_NAME
        {
            get { return this.m_HUB_NAME; }
            set { this.m_HUB_NAME = value; }
        }

        public int SLNO { get; set; }
        public string CN_NUMBER { get; set; }
        public string DISTANCE_TYPE_NAME { get; set; }
        public string CONSIGNEE_NAME { get; set; }

         public string CONSIGNEE_ADDRESS { get; set; }
         public string CONSIGNEE_MOBILE_NO { get; set; }
        
            
        
    }
}
