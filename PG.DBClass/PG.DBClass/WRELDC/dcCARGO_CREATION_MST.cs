using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.WRELDC
{
    [DBTable(Name = "CARGO_CREATION_MST")]
    public partial class dcCARGO_CREATION_MST : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_CARGO_ID = 0;
        private string m_CARGO_NUMBER = string.Empty;
        private DateTime? m_CARGO_DATE = null;
        private decimal m_CARGO_STARTING_DIS_ID = 0;
        private decimal m_CARGO_DESTINATION_DIST_ID = 0;
        private decimal m_CARGO_DESTINATION_TOWN_ID = 0;
        private int m_ROUTE_ID = 0;
        private string m_MANAGER_ID = string.Empty;
        private decimal m_WEIGHT_IN_KG = 0;
        private string m_REMARKS = string.Empty;
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


        [DBColumn(Name = "CARGO_ID", Storage = "m_CARGO_ID", DbType = "107", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int CARGO_ID
        {
            get { return this.m_CARGO_ID; }
            set
            {
                this.m_CARGO_ID = value;
                this.NotifyPropertyChanged("CARGO_ID");
            }
        }

        [DBColumn(Name = "CARGO_NUMBER", Storage = "m_CARGO_NUMBER", DbType = "126")]
        public string CARGO_NUMBER
        {
            get { return this.m_CARGO_NUMBER; }
            set
            {
                this.m_CARGO_NUMBER = value;
                this.NotifyPropertyChanged("CARGO_NUMBER");
            }
        }

        [DBColumn(Name = "CARGO_DATE", Storage = "m_CARGO_DATE", DbType = "106")]
        public DateTime? CARGO_DATE
        {
            get { return this.m_CARGO_DATE; }
            set
            {
                this.m_CARGO_DATE = value;
                this.NotifyPropertyChanged("CARGO_DATE");
            }
        }

        [DBColumn(Name = "CARGO_STARTING_DIS_ID", Storage = "m_CARGO_STARTING_DIS_ID", DbType = "107")]
        public decimal CARGO_STARTING_DIS_ID
        {
            get { return this.m_CARGO_STARTING_DIS_ID; }
            set
            {
                this.m_CARGO_STARTING_DIS_ID = value;
                this.NotifyPropertyChanged("CARGO_STARTING_DIS_ID");
            }
        }

        [DBColumn(Name = "CARGO_DESTINATION_DIST_ID", Storage = "m_CARGO_DESTINATION_DIST_ID", DbType = "107")]
        public decimal CARGO_DESTINATION_DIST_ID
        {
            get { return this.m_CARGO_DESTINATION_DIST_ID; }
            set
            {
                this.m_CARGO_DESTINATION_DIST_ID = value;
                this.NotifyPropertyChanged("CARGO_DESTINATION_DIST_ID");
            }
        }

        [DBColumn(Name = "CARGO_DESTINATION_TOWN_ID", Storage = "m_CARGO_DESTINATION_TOWN_ID", DbType = "107")]
        public decimal CARGO_DESTINATION_TOWN_ID
        {
            get { return this.m_CARGO_DESTINATION_TOWN_ID; }
            set
            {
                this.m_CARGO_DESTINATION_TOWN_ID = value;
                this.NotifyPropertyChanged("CARGO_DESTINATION_TOWN_ID");
            }
        }

        [DBColumn(Name = "ROUTE_ID", Storage = "m_ROUTE_ID", DbType = "107")]
        public int ROUTE_ID
        {
            get { return this.m_ROUTE_ID; }
            set
            {
                this.m_ROUTE_ID = value;
                this.NotifyPropertyChanged("ROUTE_ID");
            }
        }

        [DBColumn(Name = "MANAGER_ID", Storage = "m_MANAGER_ID", DbType = "126")]
        public string MANAGER_ID
        {
            get { return this.m_MANAGER_ID; }
            set
            {
                this.m_MANAGER_ID = value;
                this.NotifyPropertyChanged("MANAGER_ID");
            }
        }

        [DBColumn(Name = "WEIGHT_IN_KG", Storage = "m_WEIGHT_IN_KG", DbType = "107")]
        public decimal WEIGHT_IN_KG
        {
            get { return this.m_WEIGHT_IN_KG; }
            set
            {
                this.m_WEIGHT_IN_KG = value;
                this.NotifyPropertyChanged("WEIGHT_IN_KG");
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

    public partial class dcCARGO_CREATION_MST
    {
       public List<dcCARGO_CREATION_DETAIL> cargoDetails = new List<dcCARGO_CREATION_DETAIL>();
       public string STARTING_DIST_NAME { get; set; }
       public string DESTINATION_DIST_NAME { get; set; }
       public string TOWN_NAME { get; set; }
       public string ROUTE_NAME { get; set; }
       public string MANAGER_NAME { get; set; }
    }
}
