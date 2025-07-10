using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.AccountingDC.GeneralLedgerDC
{
	[DBTable(Name = "tblLocationGLAccount")]
public partial  class dcLocationGLAccount : DBBaseClass , INotifyPropertyChanged 
{
	#region private members

	private int m_LocationGLAccountID = 0;
	private int m_LocationID = 0;
	private int m_GLAccountID = 0;
	private int m_Permission = 0;

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


	[DBColumn(Name = "LocationGLAccountID", Storage="m_LocationGLAccountID",IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
	public int LocationGLAccountID
	{
		get {return this.m_LocationGLAccountID;}
		set 
		{
			this.m_LocationGLAccountID = value;
			this.NotifyPropertyChanged("LocationGLAccountID");
		}
	}

	[DBColumn(Name = "LocationID", Storage="m_LocationID" )]
	public int LocationID
	{
		get {return this.m_LocationID;}
		set 
		{
			this.m_LocationID = value;
			this.NotifyPropertyChanged("LocationID");
		}
	}

	[DBColumn(Name = "GLAccountID", Storage="m_GLAccountID" )]
	public int GLAccountID
	{
		get {return this.m_GLAccountID;}
		set 
		{
			this.m_GLAccountID = value;
			this.NotifyPropertyChanged("GLAccountID");
		}
	}

	[DBColumn(Name = "Permission", Storage="m_Permission" )]
	public int Permission
	{
		get {return this.m_Permission;}
		set 
		{
			this.m_Permission = value;
			this.NotifyPropertyChanged("Permission");
		}
	}

	#endregion //properties 
}

	public partial class dcLocationGLAccount
	{
		private string m_LocationName = string.Empty;
		public string LocationName
		{
			get { return m_LocationName; }
			set { this.m_LocationName = value; }
		}

		private string m_GLAccountCode = string.Empty;
		public string GLAccountCode
		{
			get { return m_GLAccountCode; }
			set { this.m_GLAccountCode = value; }
		}

		private string m_GLAccountName = string.Empty;
		public string GLAccountName
		{
			get { return m_GLAccountName; }
			set { this.m_GLAccountName = value; }
		}

        private string m_GLGroupCode = string.Empty;
        public string GLGroupCode
		{
            get { return m_GLGroupCode; }
            set { this.m_GLGroupCode = value; }
		}

        private string m_GLGroupName = string.Empty;
        public string GLGroupName
        {
            get { return m_GLGroupName; }
            set { this.m_GLGroupName = value; }
        }
        

	}
}
