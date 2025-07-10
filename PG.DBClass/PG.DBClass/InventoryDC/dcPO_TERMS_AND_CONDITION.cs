using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.InventoryDC
{
    [DBTable(Name = "PO_TERMS_AND_CONDITION")]
    public partial class dcPO_TERMS_AND_CONDITION : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_PO_T_AND_C_ID = 0;
        private int m_PURCHASE_ID = 0;
        private string m_PURCHASE_NO=string.Empty;
        private int m_T_AND_C_ID = 0;
        private string m_NAME = string.Empty;
        private string m_DESCRIPTION = string.Empty;
        private int m_SL_NO = 0;
        private int m_CREATE_BY = 0;
        private DateTime? m_CREATE_DATE = null;
        private int m_UPDATE_BY = 0;
        private DateTime? m_UPDATE_DATE = null;

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


        [DBColumn(Name = "PO_T_AND_C_ID", Storage = "m_PO_T_AND_C_ID", DbType = "107", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int PO_T_AND_C_ID
        {
            get { return this.m_PO_T_AND_C_ID; }
            set
            {
                this.m_PO_T_AND_C_ID = value;
                this.NotifyPropertyChanged("PO_T_AND_C_ID");
            }
        }

        [DBColumn(Name = "PURCHASE_ID", Storage = "m_PURCHASE_ID", DbType = "107")]
        public int PURCHASE_ID
        {
            get { return this.m_PURCHASE_ID; }
            set
            {
                this.m_PURCHASE_ID = value;
                this.NotifyPropertyChanged("PURCHASE_ID");
            }
        }

        [DBColumn(Name = "PURCHASE_NO", Storage = "m_PURCHASE_NO", DbType = "126")]
        public string PURCHASE_NO
        {
            get { return this.m_PURCHASE_NO; }
            set
            {
                this.m_PURCHASE_NO = value;
                this.NotifyPropertyChanged("PURCHASE_NO");
            }
        }

        [DBColumn(Name = "T_AND_C_ID", Storage = "m_T_AND_C_ID", DbType = "107")]
        public int T_AND_C_ID
        {
            get { return this.m_T_AND_C_ID; }
            set
            {
                this.m_T_AND_C_ID = value;
                this.NotifyPropertyChanged("T_AND_C_ID");
            }
        }

        [DBColumn(Name = "NAME", Storage = "m_NAME", DbType = "126")]
        public string PO_TC_NAME
        {
            get { return this.m_NAME; }
            set
            {
                this.m_NAME = value;
                this.NotifyPropertyChanged("NAME");
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

        [DBColumn(Name = "SL_NO", Storage = "m_SL_NO", DbType = "107")]
        public int SL_NO
        {
            get { return this.m_SL_NO; }
            set
            {
                this.m_SL_NO = value;
                this.NotifyPropertyChanged("SL_NO");
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

        [DBColumn(Name = "UPDATE_BY", Storage = "m_UPDATE_BY", DbType = "107")]
        public int UPDATE_BY
        {
            get { return this.m_UPDATE_BY; }
            set
            {
                this.m_UPDATE_BY = value;
                this.NotifyPropertyChanged("UPDATE_BY");
            }
        }

        [DBColumn(Name = "UPDATE_DATE", Storage = "m_UPDATE_DATE", DbType = "106")]
        public DateTime? UPDATE_DATE
        {
            get { return this.m_UPDATE_DATE; }
            set
            {
                this.m_UPDATE_DATE = value;
                this.NotifyPropertyChanged("UPDATE_DATE");
            }
        }

        #endregion //properties
    }
}
