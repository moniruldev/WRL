using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
  
        [DBTable(Name = "INV_DEPARTMENT_PERMISSION")]
       [Serializable]
        public partial class dcINV_DEPARTMENT_PERMISSION : DBBaseClass, INotifyPropertyChanged
        {
            #region private members

            private int m_PERMISSION_ID = 0;
            private int m_FROM_DEPARTMENT_ID = 0;
            private int m_TO_DEPARTMENT_ID = 0;
            private int m_TYPE_ID = 0;

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


            [DBColumn(Name = "PERMISSION_ID", Storage = "m_PERMISSION_ID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
            public int PERMISSION_ID
            {
                get { return this.m_PERMISSION_ID; }
                set
                {
                    this.m_PERMISSION_ID = value;
                    this.NotifyPropertyChanged("PERMISSION_ID");
                }
            }

            [DBColumn(Name = "FROM_DEPARTMENT_ID", Storage = "m_FROM_DEPARTMENT_ID", DbType = "107")]
            public int FROM_DEPARTMENT_ID
            {
                get { return this.m_FROM_DEPARTMENT_ID; }
                set
                {
                    this.m_FROM_DEPARTMENT_ID = value;
                    this.NotifyPropertyChanged("FROM_DEPARTMENT_ID");
                }
            }

            [DBColumn(Name = "TO_DEPARTMENT_ID", Storage = "m_TO_DEPARTMENT_ID", DbType = "107")]
            public int TO_DEPARTMENT_ID
            {
                get { return this.m_TO_DEPARTMENT_ID; }
                set
                {
                    this.m_TO_DEPARTMENT_ID = value;
                    this.NotifyPropertyChanged("TO_DEPARTMENT_ID");
                }
            }

            [DBColumn(Name = "TYPE_ID", Storage = "m_TYPE_ID", DbType = "107")]
            public int TYPE_ID
            {
                get { return this.m_TYPE_ID; }
                set
                {
                    this.m_TYPE_ID = value;
                    this.NotifyPropertyChanged("TYPE_ID");
                }
            }

            #endregion //properties
        }
      public partial class dcINV_DEPARTMENT_PERMISSION
      {

          private int m_DEPARTMENT_ID = 0;

          public int DEPARTMENT_ID
          {
              get { return this.m_DEPARTMENT_ID; }
              set { this.m_DEPARTMENT_ID = value; }
          }

          private string m_DEPARTMENT_NAME = string.Empty;

          public string DEPARTMENT_NAME
          {
              get { return this.m_DEPARTMENT_NAME; }
              set { this.m_DEPARTMENT_NAME = value; }
          }

          private string m_DEPARTMENT_CODE = string.Empty;
          public string DEPARTMENT_CODE
          {
              get { return this.m_DEPARTMENT_CODE; }
              set { this.m_DEPARTMENT_CODE = value; }
          }
          

          private bool m_IS_ASSIGNED = false;
          public bool IS_ASSIGNED
          {
              get { return this.m_IS_ASSIGNED; }
              set { this.m_IS_ASSIGNED = value; }
          }
        
      }
 
}
