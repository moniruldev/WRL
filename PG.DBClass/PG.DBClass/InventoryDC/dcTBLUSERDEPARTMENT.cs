using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
    [DBTable(Name = "TBLUSERDEPARTMENT")]
    public partial class dcTBLUSERDEPARTMENT : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_USERDEPTID = 0;
        private int m_USERID = 0;
        private int m_DEPTID = 0;
        private string m_DEPTCODE = string.Empty;

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


        [DBColumn(Name = "USERDEPTID", Storage = "m_USERDEPTID", DbType = "107", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int USERDEPTID
        {
            get { return this.m_USERDEPTID; }
            set
            {
                this.m_USERDEPTID = value;
                this.NotifyPropertyChanged("USERDEPTID");
            }
        }

        [DBColumn(Name = "USERID", Storage = "m_USERID", DbType = "107")]
        public int USERID
        {
            get { return this.m_USERID; }
            set
            {
                this.m_USERID = value;
                this.NotifyPropertyChanged("USERID");
            }
        }

        [DBColumn(Name = "DEPTID", Storage = "m_DEPTID", DbType = "107")]
        public int DEPTID
        {
            get { return this.m_DEPTID; }
            set
            {
                this.m_DEPTID = value;
                this.NotifyPropertyChanged("DEPTID");
            }
        }

        [DBColumn(Name = "DEPTCODE", Storage = "m_DEPTCODE", DbType = "126")]
        public string DEPTCODE
        {
            get { return this.m_DEPTCODE; }
            set
            {
                this.m_DEPTCODE = value;
                this.NotifyPropertyChanged("DEPTCODE");
            }
        }

        #endregion //properties
    }

     public partial class dcTBLUSERDEPARTMENT
     {
         private int m_DEPARTMENT_ID = 0;
         public int DEPARTMENT_ID
         {
             get { return m_DEPARTMENT_ID; }
             set { m_DEPARTMENT_ID = value; }
         }

         private string m_DEPARTMENT_NAME = string.Empty;
         public string DEPARTMENT_NAME
         {
             get { return m_DEPARTMENT_NAME; }
             set { m_DEPARTMENT_NAME = value; }
         }

         private string m_DEPARTMENT_CODE = string.Empty;
         public string DEPARTMENT_CODE
         {
             get { return m_DEPARTMENT_CODE; }
             set { m_DEPARTMENT_CODE = value; }
         }

         public string DEPARTMENT
         {
             get { return this.DEPARTMENT_CODE + ":" + " " + this.DEPARTMENT_NAME; }
         }
         public int SLNO { get; set; }

         private List<dcDEPARTMENT_INFO> m_departmentDetList = null;
         public List<dcDEPARTMENT_INFO> departmentDetList
         {
             get { return m_departmentDetList; }
             set { m_departmentDetList = value; }
         }

         private string m_IS_STORE = string.Empty;
         public string IS_STORE
         {
             get { return m_IS_STORE; }
             set { m_IS_STORE = value; }
         }

         

     }






    //public partial class dcTBLUSERDEPARTMENT : DBBaseClass, INotifyPropertyChanged
    //{
    //    #region private members

    //    private decimal m_USERDEPTID = 0;
    //    private decimal m_USERID = 0;
    //    private decimal m_DEPTID = 0;
    //    private string m_DEPTCODE = string.Empty;

    //    #endregion  //private members

    //    #region public events

    //    public event PropertyChangedEventHandler PropertyChanged;
    //    private void NotifyPropertyChanged(string info)
    //    {
    //        _UpdateChangedList(info);
    //        if (PropertyChanged != null)
    //        {
    //            PropertyChanged(this, new PropertyChangedEventArgs(info));
    //        }
    //    }

    //    #endregion //public events

    //    #region properties


    //    [DBColumn(Name = "USERDEPTID", Storage = "m_USERDEPTID", DbType = "107")]
    //    public decimal USERDEPTID
    //    {
    //        get { return this.m_USERDEPTID; }
    //        set
    //        {
    //            this.m_USERDEPTID = value;
    //            this.NotifyPropertyChanged("USERDEPTID");
    //        }
    //    }

    //    [DBColumn(Name = "USERID", Storage = "m_USERID", DbType = "107")]
    //    public decimal USERID
    //    {
    //        get { return this.m_USERID; }
    //        set
    //        {
    //            this.m_USERID = value;
    //            this.NotifyPropertyChanged("USERID");
    //        }
    //    }

    //    [DBColumn(Name = "DEPTID", Storage = "m_DEPTID", DbType = "107")]
    //    public decimal DEPTID
    //    {
    //        get { return this.m_DEPTID; }
    //        set
    //        {
    //            this.m_DEPTID = value;
    //            this.NotifyPropertyChanged("DEPTID");
    //        }
    //    }

    //    [DBColumn(Name = "DEPTCODE", Storage = "m_DEPTCODE", DbType = "126")]
    //    public string DEPTCODE
    //    {
    //        get { return this.m_DEPTCODE; }
    //        set
    //        {
    //            this.m_DEPTCODE = value;
    //            this.NotifyPropertyChanged("DEPTCODE");
    //        }
    //    }

    //    #endregion //properties
    //}
    //public partial class dcTBLUSERDEPARTMENT
    //{



    //    private int m_DEPARTMENT_ID = 0;
    //    public int DEPARTMENT_ID
    //    {
    //        get { return m_DEPARTMENT_ID; }
    //        set { m_DEPARTMENT_ID = value; }
    //    }

    //    private string m_DEPARTMENT_NAME = string.Empty;
    //    public string DEPARTMENT_NAME
    //    {
    //        get { return m_DEPARTMENT_NAME; }
    //        set { m_DEPARTMENT_NAME = value; }
    //    }

    //    private string m_DEPARTMENT_CODE = string.Empty;
    //    public string DEPARTMENT_CODE
    //    {
    //        get { return m_DEPARTMENT_CODE; }
    //        set { m_DEPARTMENT_CODE = value; }
    //    }

    //    public string DEPARTMENT
    //    {
    //        get { return this.DEPARTMENT_CODE + ":" + " " + this.DEPARTMENT_NAME; }
    //    }
    //}
}
