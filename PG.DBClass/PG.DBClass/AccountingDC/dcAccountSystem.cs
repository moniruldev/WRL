using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using PG.Core.DBBase;

namespace PG.DBClass.AccountingDC
{
    [Table(Name = "tblAccSystem")]
    public partial class dcAccountSystem : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_AccSystemID = 0;
        private string m_AccSystemName = string.Empty;
        private string m_AccSystemNameB = string.Empty;
        private int m_AccSystemSLNo = 0;
        private string m_AccSystemDesc = string.Empty;
        private bool m_IsSystem = false;
        private bool m_IsActive = false;
        private bool m_IsVisible = false;

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


        [Column(Name = "AccSystemID", Storage = "m_AccSystemID", DbType = "Int NOT NULL", IsPrimaryKey = true, UpdateCheck = UpdateCheck.Never)]
        public int AccSystemID
        {
            get { return this.m_AccSystemID; }
            set
            {
                
                this.m_AccSystemID = value;
                this.NotifyPropertyChanged("AccSystemID");
            }
        }

        [Column(Name = "AccSystemName", Storage = "m_AccSystemName", DbType = "NVarChar(50) NULL", UpdateCheck = UpdateCheck.Never)]
        public string AccSystemName
        {
            get { return this.m_AccSystemName; }
            set
            {
                this.m_AccSystemName = value;
                this.NotifyPropertyChanged("AccSystemName");
            }
        }

        [Column(Name = "AccSystemNameB", Storage = "m_AccSystemNameB", DbType = "NVarChar(50) NULL", UpdateCheck = UpdateCheck.Never)]
        public string AccSystemNameB
        {
            get { return this.m_AccSystemNameB; }
            set
            {
                this.m_AccSystemNameB = value;
                this.NotifyPropertyChanged("AccSystemNameB");
            }
        }

        [Column(Name = "AccSystemSLNo", Storage = "m_AccSystemSLNo", DbType = "Int NULL", UpdateCheck = UpdateCheck.Never)]
        public int AccSystemSLNo
        {
            get { return this.m_AccSystemSLNo; }
            set
            {
                this.m_AccSystemSLNo = value;
                this.NotifyPropertyChanged("AccSystemSLNo");
            }
        }

        [Column(Name = "AccSystemDesc", Storage = "m_AccSystemDesc", DbType = "NVarChar(150) NULL", UpdateCheck = UpdateCheck.Never)]
        public string AccSystemDesc
        {
            get { return this.m_AccSystemDesc; }
            set
            {
                this.m_AccSystemDesc = value;
                this.NotifyPropertyChanged("AccSystemDesc");
            }
        }

        [Column(Name = "IsSystem", Storage = "m_IsSystem", DbType = "Bit NULL", UpdateCheck = UpdateCheck.Never)]
        public bool IsSystem
        {
            get { return this.m_IsSystem; }
            set
            {
                this.m_IsSystem = value;
                this.NotifyPropertyChanged("IsSystem");
            }
        }

        [Column(Name = "IsActive", Storage = "m_IsActive", DbType = "Bit NULL", UpdateCheck = UpdateCheck.Never)]
        public bool IsActive
        {
            get { return this.m_IsActive; }
            set
            {
                this.m_IsActive = value;
                this.NotifyPropertyChanged("IsActive");
            }
        }

        [Column(Name = "IsVisible", Storage = "m_IsVisible", DbType = "Bit NULL", UpdateCheck = UpdateCheck.Never)]
        public bool IsVisible
        {
            get { return this.m_IsVisible; }
            set
            {
                this.m_IsVisible = value;
                this.NotifyPropertyChanged("IsVisible");
            }
        }

        #endregion //properties
    }
}
