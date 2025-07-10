using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.ProductionBL
{
     [Serializable]
    [DBTable(Name = "PROD_TBLFORECAST_DTL")]
    public partial class dcPROD_TBLFORECAST_DTL : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_FC_DET_ID = 0;
        private int m_FC_ID = 0;
        private decimal m_ITEM_ID = 0;
        private decimal m_ITEM_FC_QTY = 0;
        private decimal m_UOM_ID = 0;
        private string m_REMARKS = "";

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


        [DBColumn(Name = "FC_DET_ID", Storage = "m_FC_DET_ID",  DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true )]
        public int FC_DET_ID
        {
            get { return this.m_FC_DET_ID; }
            set
            {
                this.m_FC_DET_ID = value;
                this.NotifyPropertyChanged("FC_DET_ID");
            }
        }

        [DBColumn(Name = "FC_ID", Storage = "m_FC_ID", DbType = "107")]
        public int FC_ID
        {
            get { return this.m_FC_ID; }
            set
            {
                this.m_FC_ID = value;
                this.NotifyPropertyChanged("FC_ID");
            }
        }

        [DBColumn(Name = "ITEM_ID", Storage = "m_ITEM_ID", DbType = "107")]
        public decimal ITEM_ID
        {
            get { return this.m_ITEM_ID; }
            set
            {
                this.m_ITEM_ID = value;
                this.NotifyPropertyChanged("ITEM_ID");
            }
        }

        [DBColumn(Name = "ITEM_FC_QTY", Storage = "m_ITEM_FC_QTY", DbType = "107")]
        public decimal ITEM_FC_QTY
        {
            get { return this.m_ITEM_FC_QTY; }
            set
            {
                this.m_ITEM_FC_QTY = value;
                this.NotifyPropertyChanged("ITEM_FC_QTY");
            }
        }

        [DBColumn(Name = "UOM_ID", Storage = "m_UOM_ID", DbType = "107")]
        public decimal UOM_ID
        {
            get { return this.m_UOM_ID; }
            set
            {
                this.m_UOM_ID = value;
                this.NotifyPropertyChanged("UOM_ID");
            }
        }

           [DBColumn(Name = "REMARKS", Storage = "m_REMARKS", DbType = "107")]
        public string REMARKS
        {
            get { return this.m_REMARKS; }
            set
            {
                this.m_REMARKS = value;
                this.NotifyPropertyChanged("REMARKS");
            }
        }
        

        #endregion //properties
    }

    public partial class dcPROD_TBLFORECAST_DTL
    {
        string m_ITEM_NAME = "";
        string m_ITEM_TYPE_NAME = "";
        string m_ITEM_CLASS_NAME = "";
        string m_ITEM_GROUP_NAME = "";
        string m_UOM_NAME = "";
        string m_FC_NO = "";
        string m_BOM_NO = "";
        int m_BOM_ID = 0;
        int m_BOM_ITEM_ID = 0;


        [DBColumn(Name = "UOM_NAME", Storage = "m_UOM_NAME", DbType = "107")]
        public string UOM_NAME
        {
            get { return this.m_UOM_NAME; }
            set
            {
                this.m_UOM_NAME = value;
              //  this.NotifyPropertyChanged("UOM_NAME");
            }
        }



        [DBColumn(Name = "ITEM_NAME", Storage = "m_ITEM_NAME", DbType = "107")]
        public string ITEM_NAME
        {
            get { return this.m_ITEM_NAME; }
            set
            {
                this.m_ITEM_NAME = value;
               // this.NotifyPropertyChanged("ITEM_NAME");
            }
        }

        [DBColumn(Name = "ITEM_TYPE_NAME", Storage = "m_ITEM_TYPE_NAME", DbType = "107")]
        public string ITEM_TYPE_NAME
        {
            get { return this.m_ITEM_TYPE_NAME; }
            set
            {
                this.m_ITEM_TYPE_NAME = value;
               // this.NotifyPropertyChanged("ITEM_TYPE_NAME");
            }
        }

        [DBColumn(Name = "ITEM_CLASS_NAME", Storage = "m_ITEM_CLASS_NAME", DbType = "107")]
        public string ITEM_CLASS_NAME
        {
            get { return this.m_ITEM_CLASS_NAME; }
            set
            {
                this.m_ITEM_CLASS_NAME = value;
                //this.NotifyPropertyChanged("ITEM_CLASS_NAME");
            }
        }

        [DBColumn(Name = "ITEM_GROUP_NAME", Storage = "m_ITEM_GROUP_NAME", DbType = "107")]
        public string ITEM_GROUP_NAME
        {
            get { return this.m_ITEM_GROUP_NAME; }
            set
            {
                this.m_ITEM_GROUP_NAME = value;
              //  this.NotifyPropertyChanged("ITEM_GROUP_NAME");
            }
        }



        [DBColumn(Name = "FC_NO", Storage = "m_FC_NO", DbType = "107")]
        public string FC_NO
        {
            get { return this.m_FC_NO; }
            set
            {
                this.m_FC_NO = value;
            }
        }

          string m_FC_DESC = "";
        public string FC_DESC
        {
            get { return this.m_FC_DESC; }
            set { this.m_FC_DESC = value; }
        }
        
        public string BOM_NO
        {
            get { return this.m_BOM_NO; }
            set { this.m_BOM_NO = value; }
        }

        public int BOM_ID
        {
            get { return this.m_BOM_ID; }
            set { this.m_BOM_ID = value; }
        }

        public int  BOM_ITEM_ID
        {
            get { return this.m_BOM_ITEM_ID; }
            set { this.m_BOM_ITEM_ID = value; }
        }
    }
}
