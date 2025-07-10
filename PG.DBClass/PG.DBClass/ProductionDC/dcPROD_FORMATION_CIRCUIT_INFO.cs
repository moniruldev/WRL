using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.ProductionDC
{
    [DBTable(Name = "PROD_FORMATION_CIRCUIT_INFO")]
    public partial class dcPROD_FORMATION_CIRCUIT_INFO : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private Int64 m_FORMATION_CIRCUIT_ID = 0;
        private Int64 m_PROD_ID = 0;
        private Int64 m_MACHINE_ID = 0;
        private int m_FORMATION_BATCH_SL = 0;
        private string m_AMPER = string.Empty;
        private string m_CYCLE_TIME = string.Empty;
        private string m_START_TIME = string.Empty;
        private string m_REMARKS = string.Empty;
        private string m_SPGR = string.Empty;
        private string m_TEMPARATURE = string.Empty;

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


        [DBColumn(Name = "FORMATION_CIRCUIT_ID", Storage = "m_FORMATION_CIRCUIT_ID", DbType = "107", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public Int64 FORMATION_CIRCUIT_ID
        {
            get { return this.m_FORMATION_CIRCUIT_ID; }
            set
            {
                this.m_FORMATION_CIRCUIT_ID = value;
                this.NotifyPropertyChanged("FORMATION_CIRCUIT_ID");
            }
        }

        [DBColumn(Name = "PROD_ID", Storage = "m_PROD_ID", DbType = "107")]
        public Int64 PROD_ID
        {
            get { return this.m_PROD_ID; }
            set
            {
                this.m_PROD_ID = value;
                this.NotifyPropertyChanged("PROD_ID");
            }
        }

        [DBColumn(Name = "MACHINE_ID", Storage = "m_MACHINE_ID", DbType = "107")]
        public Int64 MACHINE_ID
        {
            get { return this.m_MACHINE_ID; }
            set
            {
                this.m_MACHINE_ID = value;
                this.NotifyPropertyChanged("MACHINE_ID");
            }
        }

        [DBColumn(Name = "FORMATION_BATCH_SL", Storage = "m_FORMATION_BATCH_SL", DbType = "107")]
        public int FORMATION_BATCH_SL
        {
            get { return this.m_FORMATION_BATCH_SL; }
            set
            {
                this.m_FORMATION_BATCH_SL = value;
                this.NotifyPropertyChanged("FORMATION_BATCH_SL");
            }
        }

        [DBColumn(Name = "AMPER", Storage = "m_AMPER", DbType = "126")]
        public string AMPER
        {
            get { return this.m_AMPER; }
            set
            {
                this.m_AMPER = value;
                this.NotifyPropertyChanged("AMPER");
            }
        }

        [DBColumn(Name = "CYCLE_TIME", Storage = "m_CYCLE_TIME", DbType = "126")]
        public string CYCLE_TIME
        {
            get { return this.m_CYCLE_TIME; }
            set
            {
                this.m_CYCLE_TIME = value;
                this.NotifyPropertyChanged("CYCLE_TIME");
            }
        }

        [DBColumn(Name = "START_TIME", Storage = "m_START_TIME", DbType = "126")]
        public string START_TIME
        {
            get { return this.m_START_TIME; }
            set
            {
                this.m_START_TIME = value;
                this.NotifyPropertyChanged("START_TIME");
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


        [DBColumn(Name = "SPGR", Storage = "m_SPGR", DbType = "126")]
        public string SPGR
        {
            get { return this.m_SPGR; }
            set
            {
                this.m_SPGR = value;
                this.NotifyPropertyChanged("SPGR");
            }
        }

        [DBColumn(Name = "TEMPARATURE", Storage = "m_TEMPARATURE", DbType = "126")]
        public string TEMPARATURE
        {
            get { return this.m_TEMPARATURE; }
            set
            {
                this.m_TEMPARATURE = value;
                this.NotifyPropertyChanged("TEMPARATURE");
            }
        }

        #endregion //properties

     
    }

     public partial class dcPROD_FORMATION_CIRCUIT_INFO
     {
         public string MACHINE_NAME { get; set; }
     }
}
