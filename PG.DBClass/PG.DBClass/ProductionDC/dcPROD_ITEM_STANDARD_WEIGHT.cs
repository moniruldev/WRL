using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.ProductionDC
{
    [DBTable(Name = "PROD_ITEM_STANDARD_WEIGHT")]
    public partial class dcPROD_ITEM_STANDARD_WEIGHT : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_ID = 0;
        private int m_ITEM_ID = 0;
        private decimal m_GRID_PANEL_GM = 0;
        private decimal m_GRID_PC_GM = 0;
        private decimal m_PASTE_PANEL_GM = 0;
        private decimal m_PASTE_PC_GM = 0;
        private decimal m_GRID_PASTE_PANEL_GM = 0;
        private decimal m_GRID_PANEL_KG = 0;
        private decimal m_GRID_PC_KG = 0;
        private decimal m_PASTE_PANEL_KG = 0;
        private decimal m_PASTE_PC_KG = 0;
        private decimal m_GRID_PASTE_PANEL_KG = 0;
        private int m_DEPT_ID = 0;
        private decimal m_PANEL_PC = 0;
        private decimal m_GRID_PASTE_PC = 0;
        private decimal m_GRID_PASTE_PC_KG = 0;
        private decimal m_UOM_ID = 0;

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


        [DBColumn(Name = "ID", Storage = "m_ID", DbType = "107", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int ID
        {
            get { return this.m_ID; }
            set
            {
                this.m_ID = value;
                this.NotifyPropertyChanged("ID");
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

        [DBColumn(Name = "GRID_PANEL_GM", Storage = "m_GRID_PANEL_GM", DbType = "107")]
        public decimal GRID_PANEL_GM
        {
            get { return this.m_GRID_PANEL_GM; }
            set
            {
                this.m_GRID_PANEL_GM = value;
                this.NotifyPropertyChanged("GRID_PANEL_GM");
            }
        }

        [DBColumn(Name = "GRID_PC_GM", Storage = "m_GRID_PC_GM", DbType = "107")]
        public decimal GRID_PC_GM
        {
            get { return this.m_GRID_PC_GM; }
            set
            {
                this.m_GRID_PC_GM = value;
                this.NotifyPropertyChanged("GRID_PC_GM");
            }
        }

        [DBColumn(Name = "PASTE_PANEL_GM", Storage = "m_PASTE_PANEL_GM", DbType = "107")]
        public decimal PASTE_PANEL_GM
        {
            get { return this.m_PASTE_PANEL_GM; }
            set
            {
                this.m_PASTE_PANEL_GM = value;
                this.NotifyPropertyChanged("PASTE_PANEL_GM");
            }
        }

        [DBColumn(Name = "PASTE_PC_GM", Storage = "m_PASTE_PC_GM", DbType = "107")]
        public decimal PASTE_PC_GM
        {
            get { return this.m_PASTE_PC_GM; }
            set
            {
                this.m_PASTE_PC_GM = value;
                this.NotifyPropertyChanged("PASTE_PC_GM");
            }
        }

        [DBColumn(Name = "GRID_PASTE_PANEL_GM", Storage = "m_GRID_PASTE_PANEL_GM", DbType = "107")]
        public decimal GRID_PASTE_PANEL_GM
        {
            get { return this.m_GRID_PASTE_PANEL_GM; }
            set
            {
                this.m_GRID_PASTE_PANEL_GM = value;
                this.NotifyPropertyChanged("GRID_PASTE_PANEL_GM");
            }
        }

        [DBColumn(Name = "GRID_PANEL_KG", Storage = "m_GRID_PANEL_KG", DbType = "107")]
        public decimal GRID_PANEL_KG
        {
            get { return this.m_GRID_PANEL_KG; }
            set
            {
                this.m_GRID_PANEL_KG = value;
                this.NotifyPropertyChanged("GRID_PANEL_KG");
            }
        }

        [DBColumn(Name = "GRID_PC_KG", Storage = "m_GRID_PC_KG", DbType = "107")]
        public decimal GRID_PC_KG
        {
            get { return this.m_GRID_PC_KG; }
            set
            {
                this.m_GRID_PC_KG = value;
                this.NotifyPropertyChanged("GRID_PC_KG");
            }
        }

        [DBColumn(Name = "PASTE_PANEL_KG", Storage = "m_PASTE_PANEL_KG", DbType = "107")]
        public decimal PASTE_PANEL_KG
        {
            get { return this.m_PASTE_PANEL_KG; }
            set
            {
                this.m_PASTE_PANEL_KG = value;
                this.NotifyPropertyChanged("PASTE_PANEL_KG");
            }
        }

        [DBColumn(Name = "PASTE_PC_KG", Storage = "m_PASTE_PC_KG", DbType = "107")]
        public decimal PASTE_PC_KG
        {
            get { return this.m_PASTE_PC_KG; }
            set
            {
                this.m_PASTE_PC_KG = value;
                this.NotifyPropertyChanged("PASTE_PC_KG");
            }
        }

        [DBColumn(Name = "GRID_PASTE_PANEL_KG", Storage = "m_GRID_PASTE_PANEL_KG", DbType = "107")]
        public decimal GRID_PASTE_PANEL_KG
        {
            get { return this.m_GRID_PASTE_PANEL_KG; }
            set
            {
                this.m_GRID_PASTE_PANEL_KG = value;
                this.NotifyPropertyChanged("GRID_PASTE_PANEL_KG");
            }
        }

        [DBColumn(Name = "DEPT_ID", Storage = "m_DEPT_ID", DbType = "107")]
        public int DEPT_ID
        {
            get { return this.m_DEPT_ID; }
            set
            {
                this.m_DEPT_ID = value;
                this.NotifyPropertyChanged("DEPT_ID");
            }
        }

        [DBColumn(Name = "PANEL_PC", Storage = "m_PANEL_PC", DbType = "107")]
        public decimal PANEL_PC
        {
            get { return this.m_PANEL_PC; }
            set
            {
                this.m_PANEL_PC = value;
                this.NotifyPropertyChanged("PANEL_PC");
            }
        }

        [DBColumn(Name = "GRID_PASTE_PC", Storage = "m_GRID_PASTE_PC", DbType = "107")]
        public decimal GRID_PASTE_PC
        {
            get { return this.m_GRID_PASTE_PC; }
            set
            {
                this.m_GRID_PASTE_PC = value;
                this.NotifyPropertyChanged("GRID_PASTE_PC");
            }
        }

        [DBColumn(Name = "GRID_PASTE_PC_KG", Storage = "m_GRID_PASTE_PC_KG", DbType = "107")]
        public decimal GRID_PASTE_PC_KG
        {
            get { return this.m_GRID_PASTE_PC_KG; }
            set
            {
                this.m_GRID_PASTE_PC_KG = value;
                this.NotifyPropertyChanged("GRID_PASTE_PC_KG");
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

        #endregion //properties
    } 

    public partial class dcPROD_ITEM_STANDARD_WEIGHT
    {
        public string ITEM_NAME { get; set; }
        public string UOM_NAME { get; set; }
        public string UOM_CODE_SHORT { get; set; }
        public string DEPARTMENT_NAME { get; set; }
        

    }
}
