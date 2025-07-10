using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
    [DBTable(Name = "DEPARTMENT_INFO_MVW")]
  public  class dcDEPARTMENT_INFO_MVW : DBBaseClass, INotifyPropertyChanged 
    {
        #region private members

        private string m_DEPARTMENT_CODE = string.Empty;
        private string m_DEPARTMENT_NAME = string.Empty;
        private string m_DEPT_ADMIN = string.Empty;
        private string m_UNIT = string.Empty;
        private string m_IS_SALES_DEPT = string.Empty;

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


        [DBColumn(Name = "DEPARTMENT_CODE", Storage = "m_DEPARTMENT_CODE", DbType = "126")]
        public string DEPARTMENT_CODE
        {
            get { return this.m_DEPARTMENT_CODE; }
            set
            {
                this.m_DEPARTMENT_CODE = value;
                this.NotifyPropertyChanged("DEPARTMENT_CODE");
            }
        }

        [DBColumn(Name = "DEPARTMENT_NAME", Storage = "m_DEPARTMENT_NAME", DbType = "126")]
        public string DEPARTMENT_NAME
        {
            get { return this.m_DEPARTMENT_NAME; }
            set
            {
                this.m_DEPARTMENT_NAME = value;
                this.NotifyPropertyChanged("DEPARTMENT_NAME");
            }
        }

        [DBColumn(Name = "DEPT_ADMIN", Storage = "m_DEPT_ADMIN", DbType = "126")]
        public string DEPT_ADMIN
        {
            get { return this.m_DEPT_ADMIN; }
            set
            {
                this.m_DEPT_ADMIN = value;
                this.NotifyPropertyChanged("DEPT_ADMIN");
            }
        }

        [DBColumn(Name = "UNIT", Storage = "m_UNIT", DbType = "126")]
        public string UNIT
        {
            get { return this.m_UNIT; }
            set
            {
                this.m_UNIT = value;
                this.NotifyPropertyChanged("UNIT");
            }
        }

        [DBColumn(Name = "IS_SALES_DEPT", Storage = "m_IS_SALES_DEPT", DbType = "126")]
        public string IS_SALES_DEPT
        {
            get { return this.m_IS_SALES_DEPT; }
            set
            {
                this.m_IS_SALES_DEPT = value;
                this.NotifyPropertyChanged("IS_SALES_DEPT");
            }
        }

        #endregion //properties 
    }
}
