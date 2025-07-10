using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using PG.Core.DBBase;

namespace PG.DBClass.PDLAccountingDC
{
    [DBTable(Name = "GL_COA", IsDBTableLink = true)]
    public partial class dcGL_COA : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private string m_COA_CODE = string.Empty;
        private string m_COA_DESC_LONG = string.Empty;
        private string m_COA_DESC_SHORT = string.Empty;
        private string m_COA_MAIN_GRP = string.Empty;
        private string m_COA_ACCOUNT_TYPE = string.Empty;
        private string m_COA_PARENT_CODE = string.Empty;
        private Int32 m_COA_POS_IN_REPORT = 0;
        private string m_COA_DEF_BAL_TYPE = string.Empty;
        private bool m_IS_RC_DRIVEN = false;
        private bool m_IS_VEND_DRIVEN = false;
        private bool m_IS_BUDGET = false;
        private bool m_IS_ADVANCE_REG = false;
        private bool m_IS_ADVANCE_BLOCK = false;
        private string m_GROUP_LADGER = string.Empty;

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


        [DBColumn(Name = "COA_CODE", Storage = "m_COA_CODE", DbType = "126", IsPrimaryKey = true)]
        public string COA_CODE
        {
            get { return this.m_COA_CODE; }
            set
            {
                this.m_COA_CODE = value;
                this.NotifyPropertyChanged("COA_CODE");
            }
        }

        [DBColumn(Name = "COA_DESC_LONG", Storage = "m_COA_DESC_LONG", DbType = "126")]
        public string COA_DESC_LONG
        {
            get { return this.m_COA_DESC_LONG; }
            set
            {
                this.m_COA_DESC_LONG = value;
                this.NotifyPropertyChanged("COA_DESC_LONG");
            }
        }

        [DBColumn(Name = "COA_DESC_SHORT", Storage = "m_COA_DESC_SHORT", DbType = "126")]
        public string COA_DESC_SHORT
        {
            get { return this.m_COA_DESC_SHORT; }
            set
            {
                this.m_COA_DESC_SHORT = value;
                this.NotifyPropertyChanged("COA_DESC_SHORT");
            }
        }

        [DBColumn(Name = "COA_MAIN_GRP", Storage = "m_COA_MAIN_GRP", DbType = "126")]
        public string COA_MAIN_GRP
        {
            get { return this.m_COA_MAIN_GRP; }
            set
            {
                this.m_COA_MAIN_GRP = value;
                this.NotifyPropertyChanged("COA_MAIN_GRP");
            }
        }

        [DBColumn(Name = "COA_ACCOUNT_TYPE", Storage = "m_COA_ACCOUNT_TYPE", DbType = "126")]
        public string COA_ACCOUNT_TYPE
        {
            get { return this.m_COA_ACCOUNT_TYPE; }
            set
            {
                this.m_COA_ACCOUNT_TYPE = value;
                this.NotifyPropertyChanged("COA_ACCOUNT_TYPE");
            }
        }

        [DBColumn(Name = "COA_PARENT_CODE", Storage = "m_COA_PARENT_CODE", DbType = "126")]
        public string COA_PARENT_CODE
        {
            get { return this.m_COA_PARENT_CODE; }
            set
            {
                this.m_COA_PARENT_CODE = value;
                this.NotifyPropertyChanged("COA_PARENT_CODE");
            }
        }

        [DBColumn(Name = "COA_POS_IN_REPORT", Storage = "m_COA_POS_IN_REPORT", DbType = "111")]
        public Int32 COA_POS_IN_REPORT
        {
            get { return this.m_COA_POS_IN_REPORT; }
            set
            {
                this.m_COA_POS_IN_REPORT = value;
                this.NotifyPropertyChanged("COA_POS_IN_REPORT");
            }
        }

        [DBColumn(Name = "COA_DEF_BAL_TYPE", Storage = "m_COA_DEF_BAL_TYPE", DbType = "126")]
        public string COA_DEF_BAL_TYPE
        {
            get { return this.m_COA_DEF_BAL_TYPE; }
            set
            {
                this.m_COA_DEF_BAL_TYPE = value;
                this.NotifyPropertyChanged("COA_DEF_BAL_TYPE");
            }
        }

        [DBColumn(Name = "IS_RC_DRIVEN", Storage = "m_IS_RC_DRIVEN", DbType = "126")]
        public bool IS_RC_DRIVEN
        {
            get { return this.m_IS_RC_DRIVEN; }
            set
            {
                this.m_IS_RC_DRIVEN = value;
                this.NotifyPropertyChanged("IS_RC_DRIVEN");
            }
        }

        [DBColumn(Name = "IS_VEND_DRIVEN", Storage = "m_IS_VEND_DRIVEN", DbType = "126")]
        public bool IS_VEND_DRIVEN
        {
            get { return this.m_IS_VEND_DRIVEN; }
            set
            {
                this.m_IS_VEND_DRIVEN = value;
                this.NotifyPropertyChanged("IS_VEND_DRIVEN");
            }
        }

        [DBColumn(Name = "IS_BUDGET", Storage = "m_IS_BUDGET", DbType = "126")]
        public bool IS_BUDGET
        {
            get { return this.m_IS_BUDGET; }
            set
            {
                this.m_IS_BUDGET = value;
                this.NotifyPropertyChanged("IS_BUDGET");
            }
        }

        [DBColumn(Name = "IS_ADVANCE_REG", Storage = "m_IS_ADVANCE_REG", DbType = "126")]
        public bool IS_ADVANCE_REG
        {
            get { return this.m_IS_ADVANCE_REG; }
            set
            {
                this.m_IS_ADVANCE_REG = value;
                this.NotifyPropertyChanged("IS_ADVANCE_REG");
            }
        }

        [DBColumn(Name = "IS_ADVANCE_BLOCK", Storage = "m_IS_ADVANCE_BLOCK", DbType = "126")]
        public bool IS_ADVANCE_BLOCK
        {
            get { return this.m_IS_ADVANCE_BLOCK; }
            set
            {
                this.m_IS_ADVANCE_BLOCK = value;
                this.NotifyPropertyChanged("IS_ADVANCE_BLOCK");
            }
        }

        [DBColumn(Name = "GROUP_LADGER", Storage = "m_GROUP_LADGER", DbType = "126")]
        public string GROUP_LADGER
        {
            get { return this.m_GROUP_LADGER; }
            set
            {
                this.m_GROUP_LADGER = value;
                this.NotifyPropertyChanged("GROUP_LADGER");
            }
        }

        #endregion //properties
    }
}
