using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.ProductionDC
{
    [DBTable(Name = "PROD_QA_OPERATION_DTL_TEMP")]
    public partial class dcPROD_QA_OPERATION_DTL_TEMP : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_PROD_QA_DTL_ID_TEMP = 0;
        private int m_ITEM_ID = 0;
        private decimal m_ITEM_QTY = 0;
        private decimal m_PASS_QTY = 0;
        private decimal m_REJECT_QTY = 0;
        private decimal m_WEIGHT = 0;
        private decimal m_WEIGHT_UOM_ID = 0;
        private decimal m_THIKNESS = 0;
        private decimal m_THIKNESS_UOM_ID = 0;
        private string m_FRAME_CRACK = string.Empty;
        private string m_CAVITY = string.Empty;
        private string m_WINDOW_MISSING = string.Empty;
        private string m_FEATHER = string.Empty;
        private decimal m_SPINE_DIAMETER = 0;
        private decimal m_SPINE_DIAMETER_UOM_ID = 0;
        private string m_REMARKS = string.Empty;
        private int m_PROD_DTL_ID = 0;
        private string m_PROD_BATCH_NO_DTL = string.Empty;
        private decimal m_WEIGHT_2 = 0;
        private decimal m_THIKNESS_2 = 0;
        private int m_PENDING_QTY = 0;
        private int m_PROD_MST_ID = 0;
        

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


        [DBColumn(Name = "PROD_QA_DTL_ID_TEMP", Storage = "m_PROD_QA_DTL_ID_TEMP", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int PROD_QA_DTL_ID_TEMP
        {
            get { return this.m_PROD_QA_DTL_ID_TEMP; }
            set
            {
                this.m_PROD_QA_DTL_ID_TEMP = value;
                this.NotifyPropertyChanged("PROD_QA_DTL_ID_TEMP");
            }
        }

        [DBColumn(Name = "ITEM_ID", Storage = "m_ITEM_ID", DbType = "107")]
        public int ITEM_ID
        {
            get { return this.m_ITEM_ID; }
            set
            {
                this.m_ITEM_ID = value;
                this.NotifyPropertyChanged("ITEM_ID");
            }
        }

        [DBColumn(Name = "ITEM_QTY", Storage = "m_ITEM_QTY", DbType = "107")]
        public decimal ITEM_QTY
        {
            get { return this.m_ITEM_QTY; }
            set
            {
                this.m_ITEM_QTY = value;
                this.NotifyPropertyChanged("ITEM_QTY");
            }
        }

        [DBColumn(Name = "PASS_QTY", Storage = "m_PASS_QTY", DbType = "107")]
        public decimal PASS_QTY
        {
            get { return this.m_PASS_QTY; }
            set
            {
                this.m_PASS_QTY = value;
                this.NotifyPropertyChanged("PASS_QTY");
            }
        }

        [DBColumn(Name = "REJECT_QTY", Storage = "m_REJECT_QTY", DbType = "107")]
        public decimal REJECT_QTY
        {
            get { return this.m_REJECT_QTY; }
            set
            {
                this.m_REJECT_QTY = value;
                this.NotifyPropertyChanged("REJECT_QTY");
            }
        }

        [DBColumn(Name = "WEIGHT", Storage = "m_WEIGHT", DbType = "107")]
        public decimal WEIGHT
        {
            get { return this.m_WEIGHT; }
            set
            {
                this.m_WEIGHT = value;
                this.NotifyPropertyChanged("WEIGHT");
            }
        }

        [DBColumn(Name = "WEIGHT_UOM_ID", Storage = "m_WEIGHT_UOM_ID", DbType = "107")]
        public decimal WEIGHT_UOM_ID
        {
            get { return this.m_WEIGHT_UOM_ID; }
            set
            {
                this.m_WEIGHT_UOM_ID = value;
                this.NotifyPropertyChanged("WEIGHT_UOM_ID");
            }
        }

        [DBColumn(Name = "THIKNESS", Storage = "m_THIKNESS", DbType = "107")]
        public decimal THIKNESS
        {
            get { return this.m_THIKNESS; }
            set
            {
                this.m_THIKNESS = value;
                this.NotifyPropertyChanged("THIKNESS");
            }
        }

        [DBColumn(Name = "THIKNESS_UOM_ID", Storage = "m_THIKNESS_UOM_ID", DbType = "107")]
        public decimal THIKNESS_UOM_ID
        {
            get { return this.m_THIKNESS_UOM_ID; }
            set
            {
                this.m_THIKNESS_UOM_ID = value;
                this.NotifyPropertyChanged("THIKNESS_UOM_ID");
            }
        }

        [DBColumn(Name = "FRAME_CRACK", Storage = "m_FRAME_CRACK", DbType = "126")]
        public string FRAME_CRACK
        {
            get { return this.m_FRAME_CRACK; }
            set
            {
                this.m_FRAME_CRACK = value;
                this.NotifyPropertyChanged("FRAME_CRACK");
            }
        }

        [DBColumn(Name = "CAVITY", Storage = "m_CAVITY", DbType = "126")]
        public string CAVITY
        {
            get { return this.m_CAVITY; }
            set
            {
                this.m_CAVITY = value;
                this.NotifyPropertyChanged("CAVITY");
            }
        }

        [DBColumn(Name = "WINDOW_MISSING", Storage = "m_WINDOW_MISSING", DbType = "126")]
        public string WINDOW_MISSING
        {
            get { return this.m_WINDOW_MISSING; }
            set
            {
                this.m_WINDOW_MISSING = value;
                this.NotifyPropertyChanged("WINDOW_MISSING");
            }
        }

        [DBColumn(Name = "FEATHER", Storage = "m_FEATHER", DbType = "126")]
        public string FEATHER
        {
            get { return this.m_FEATHER; }
            set
            {
                this.m_FEATHER = value;
                this.NotifyPropertyChanged("FEATHER");
            }
        }

        [DBColumn(Name = "SPINE_DIAMETER", Storage = "m_SPINE_DIAMETER", DbType = "107")]
        public decimal SPINE_DIAMETER
        {
            get { return this.m_SPINE_DIAMETER; }
            set
            {
                this.m_SPINE_DIAMETER = value;
                this.NotifyPropertyChanged("SPINE_DIAMETER");
            }
        }

        [DBColumn(Name = "SPINE_DIAMETER_UOM_ID", Storage = "m_SPINE_DIAMETER_UOM_ID", DbType = "107")]
        public decimal SPINE_DIAMETER_UOM_ID
        {
            get { return this.m_SPINE_DIAMETER_UOM_ID; }
            set
            {
                this.m_SPINE_DIAMETER_UOM_ID = value;
                this.NotifyPropertyChanged("SPINE_DIAMETER_UOM_ID");
            }
        }

        [DBColumn(Name = "REMARKS", Storage = "m_REMARKS", DbType = "126")]
        public string REMARKS
        {
            get { return this.m_REMARKS; }
            set
            {
                this.m_REMARKS = value;
                this.NotifyPropertyChanged("REMARKS");
            }
        }

        [DBColumn(Name = "PROD_DTL_ID", Storage = "m_PROD_DTL_ID", DbType = "107")]
        public int PROD_DTL_ID
        {
            get { return this.m_PROD_DTL_ID; }
            set
            {
                this.m_PROD_DTL_ID = value;
                this.NotifyPropertyChanged("PROD_DTL_ID");
            }
        }

         [DBColumn(Name = "PROD_BATCH_NO_DTL", Storage = "m_PROD_BATCH_NO_DTL", DbType = "126")]
        public string PROD_BATCH_NO_DTL
        {
            get { return this.m_PROD_BATCH_NO_DTL; }
            set
            {
                this.m_PROD_BATCH_NO_DTL = value;
                this.NotifyPropertyChanged("PROD_BATCH_NO_DTL");
            }
        }

         [DBColumn(Name = "WEIGHT_2", Storage = "m_WEIGHT_2", DbType = "107")]
         public decimal WEIGHT_2
         {
             get { return this.m_WEIGHT_2; }
             set
             {
                 this.m_WEIGHT_2 = value;
                 this.NotifyPropertyChanged("WEIGHT_2");
             }
         }

         [DBColumn(Name = "THIKNESS_2", Storage = "m_THIKNESS_2", DbType = "107")]
         public decimal THIKNESS_2
         {
             get { return this.m_THIKNESS_2; }
             set
             {
                 this.m_THIKNESS_2 = value;
                 this.NotifyPropertyChanged("THIKNESS_2");
             }
         }

        [DBColumn(Name = "PENDING_QTY", Storage = "m_PENDING_QTY", DbType = "107")]
         public int PENDING_QTY
         {
             get { return this.m_PENDING_QTY; }
             set
             {
                 this.m_PENDING_QTY = value;
                 this.NotifyPropertyChanged("PENDING_QTY");
             }
         }

        [DBColumn(Name = "PROD_MST_ID", Storage = "m_PROD_MST_ID", DbType = "107")]
        public int PROD_MST_ID
        {
            get { return this.m_PROD_MST_ID; }
            set
            {
                this.m_PROD_MST_ID = value;
                this.NotifyPropertyChanged("PROD_MST_ID");
            }
        }
        
        #endregion //properties
    }

    public partial class dcPROD_QA_OPERATION_DTL_TEMP
    {

      
      

       
        public bool IS_SELECTED { get; set; }

        private string m_FULL_NAME = "";
        private string m_DEPARTMENT_NAME = "";
        private string m_SHIFT_NAME = "";
        private string m_FORECUSTMONTH = "";
        private string m_FORECUSTYEAR = "";
        private string m_SUPERVISOR_NAME = "";
        private string m_SHIFT_INCHARGE_NAME = "";

        private string m_MACHINE_NAME = "";
        private string m_ITEM_NAME = "";
        public string MACHINE_NAME
        {
            get { return m_MACHINE_NAME; }
            set { m_MACHINE_NAME = value; }
        }

        public string ITEM_NAME
        {
            get { return m_ITEM_NAME; }
            set { m_ITEM_NAME = value; }
        }
        public string SHIFT_INCHARGE_NAME
        {
            get { return m_SHIFT_INCHARGE_NAME; }
            set { m_SHIFT_INCHARGE_NAME = value; }
        }
        public string FULL_NAME
        {
            get { return m_FULL_NAME; }
            set { m_FULL_NAME = value; }
        }

        public string DEPARTMENT_NAME
        {
            get { return m_DEPARTMENT_NAME; }
            set { m_DEPARTMENT_NAME = value; }
        }


        public string SHIFT_NAME
        {
            get { return m_SHIFT_NAME; }
            set { m_SHIFT_NAME = value; }
        }



        public string UN_LOAD_PROD_NO { get; set; }
        public string UN_LOAD_AUTH_STATUS { get; set; }


        public string FORECUSTMONTH
        {
            get { return m_FORECUSTMONTH; }
            set { m_FORECUSTMONTH = value; }
        }

        public string FORECUSTYEAR
        {
            get { return m_FORECUSTYEAR; }
            set { m_FORECUSTYEAR = value; }
        }

        private string m_ENTRY_BY = "";

        public string ENTRY_BY
        {
            get { return m_ENTRY_BY; }
            set { m_ENTRY_BY = value; }
        }
        public string IS_UNLOADED { get; set; }
        public int? m_UN_LOADED_PROD_ID = 0;
        public int? UN_LOADED_PROD_ID
        {
            get { return this.m_UN_LOADED_PROD_ID; }
            set
            {
                this.m_UN_LOADED_PROD_ID = value;
            }
        }
    }
}
