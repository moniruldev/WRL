using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.InventoryDC
{
    [DBTable(Name = "EMP_SIGNATURE_UPLOAD")]
    public partial class dcEMP_SIGNATURE_UPLOAD : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private decimal m_EMP_SIGN_ID = 0;
        private string m_EMP_ID = string.Empty;
        private string m_EMP_NAME = string.Empty;
        private string m_SIGN_PHOTO = string.Empty;
        private string m_STATUS = string.Empty;
        private string m_CREATE_BY = string.Empty;
        private DateTime? m_CREATE_DATE = null;
        private DateTime? m_UPLOAD_DATE = null;
        private string m_CONTENT_TYPE = string.Empty;
        private decimal m_USER_ID = 0; 

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


        [DBColumn(Name = "EMP_SIGN_ID", Storage = "m_EMP_SIGN_ID", DbType = "107", IsPrimaryKey = true)]
        public decimal EMP_SIGN_ID
        {
            get { return this.m_EMP_SIGN_ID; }
            set
            {
                this.m_EMP_SIGN_ID = value;
                this.NotifyPropertyChanged("EMP_SIGN_ID");
            }
        }

        [DBColumn(Name = "EMP_ID", Storage = "m_EMP_ID", DbType = "126")]
        public string EMP_ID
        {
            get { return this.m_EMP_ID; }
            set
            {
                this.m_EMP_ID = value;
                this.NotifyPropertyChanged("EMP_ID");
            }
        }

        [DBColumn(Name = "EMP_NAME", Storage = "m_EMP_NAME", DbType = "126")]
        public string EMP_NAME
        {
            get { return this.m_EMP_NAME; }
            set
            {
                this.m_EMP_NAME = value;
                this.NotifyPropertyChanged("EMP_NAME");
            }
        }

        [DBColumn(Name = "SIGN_PHOTO", Storage = "m_SIGN_PHOTO", DbType = "102")]
        public string SIGN_PHOTO
        {
            get { return this.m_SIGN_PHOTO; }
            set
            {
                this.m_SIGN_PHOTO = value;
                this.NotifyPropertyChanged("SIGN_PHOTO");
            }
        }

        [DBColumn(Name = "STATUS", Storage = "m_STATUS", DbType = "126")]
        public string STATUS
        {
            get { return this.m_STATUS; }
            set
            {
                this.m_STATUS = value;
                this.NotifyPropertyChanged("STATUS");
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

        [DBColumn(Name = "UPLOAD_DATE", Storage = "m_UPLOAD_DATE", DbType = "106")]
        public DateTime? UPLOAD_DATE
        {
            get { return this.m_UPLOAD_DATE; }
            set
            {
                this.m_UPLOAD_DATE = value;
                this.NotifyPropertyChanged("UPLOAD_DATE");
            }
        }

         [DBColumn(Name = "CONTENT_TYPE", Storage = "m_CONTENT_TYPE", DbType = "126")]
        public string CONTENT_TYPE
        {
            get { return this.m_CONTENT_TYPE; }
            set
            {
                this.m_CONTENT_TYPE = value;
                this.NotifyPropertyChanged("CONTENT_TYPE");
            }
        }

         [DBColumn(Name = "USER_ID", Storage = "m_USER_ID", DbType = "107")]
         public decimal USER_ID
         {
             get { return this.m_USER_ID; }
             set
             {
                 this.m_USER_ID = value;
                 this.NotifyPropertyChanged("USER_ID");
             }
         }
        
        #endregion //properties
    }
}
