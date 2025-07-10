using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.ProductionDC
{
    [Serializable]
    [DBTable(Name = "BATERY_CATEGORY")]
    public partial class dcBATERY_CATEGORY : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private string m_BATERY_CAT_ID = string.Empty;
        private string m_BATERY_CAT_DESCR = string.Empty;
        private int m_GUARANTEE_MONTHS = 0;
        private string m_MST_CAT_ID = string.Empty;
        private string m_MAIN_CAT_GROUP = string.Empty;


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


        [DBColumn(Name = "BATERY_CAT_ID", Storage = "m_BATERY_CAT_ID", DbType = "126", IsPrimaryKey = true)]
        public string BATERY_CAT_ID
        {
            get { return this.m_BATERY_CAT_ID; }
            set
            {
                this.m_BATERY_CAT_ID = value;
                this.NotifyPropertyChanged("BATERY_CAT_ID");
            }
        }

        [DBColumn(Name = "BATERY_CAT_DESCR", Storage = "m_BATERY_CAT_DESCR", DbType = "126")]
        public string BATERY_CAT_DESCR
        {
            get { return this.m_BATERY_CAT_DESCR; }
            set
            {
                this.m_BATERY_CAT_DESCR = value;
                this.NotifyPropertyChanged("BATERY_CAT_DESCR");
            }
        }

        [DBColumn(Name = "GUARANTEE_MONTHS", Storage = "m_GUARANTEE_MONTHS", DbType = "126")]
        public int GUARANTEE_MONTHS
        {
            get { return this.m_GUARANTEE_MONTHS; }
            set
            {
                this.m_GUARANTEE_MONTHS = value;
                this.NotifyPropertyChanged("GUARANTEE_MONTHS");
            }
        }


        [DBColumn(Name = "MST_CAT_ID", Storage = "m_MST_CAT_ID", DbType = "126")]
        public string MST_CAT_ID
        {
            get { return this.m_MST_CAT_ID; }
            set
            {
                this.m_MST_CAT_ID = value;
                this.NotifyPropertyChanged("MST_CAT_ID");
            }
        }


        [DBColumn(Name = "MAIN_CAT_GROUP", Storage = "m_MAIN_CAT_GROUP", DbType = "126")]
        public string MAIN_CAT_GROUP
        {
            get { return this.m_MAIN_CAT_GROUP; }
            set
            {
                this.m_MAIN_CAT_GROUP = value;
                this.NotifyPropertyChanged("MAIN_CAT_GROUP");
            }
        }




        #endregion //properties
    }
}
