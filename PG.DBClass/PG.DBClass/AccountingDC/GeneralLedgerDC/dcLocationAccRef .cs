using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.AccountingDC.GeneralLedgerDC
{
	[DBTable(Name = "tblLocationAccRef")]
public partial  class dcLocationAccRef : DBBaseClass , INotifyPropertyChanged 
{
	#region private members

	private int m_LocationAccRefID = 0;
	private int m_LocationID = 0;
	private int m_AccRefID = 0;
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


	[DBColumn(Name = "LocationAccRefID", Storage="m_LocationAccRefID", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
	public int LocationAccRefID
	{
		get {return this.m_LocationAccRefID;}
		set 
		{
			this.m_LocationAccRefID = value;
			this.NotifyPropertyChanged("LocationAccRefID");
		}
	}

	[DBColumn(Name = "LocationID", Storage="m_LocationID")]
	public int LocationID
	{
		get {return this.m_LocationID;}
		set 
		{
			this.m_LocationID = value;
			this.NotifyPropertyChanged("LocationID");
		}
	}

	[DBColumn(Name = "AccRefID", Storage="m_AccRefID" )]
	public int AccRefID
	{
		get {return this.m_AccRefID;}
		set 
		{
			this.m_AccRefID = value;
			this.NotifyPropertyChanged("AccRefID");
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

	public partial class dcLocationAccRef
	{
		private string m_LocationName = string.Empty;
		public string LocationName
		{
			get { return m_LocationName; }
			set { this.m_LocationName = value; }
		}

		private int m_AccRefCategoryID = 0;
		public int AccRefCategoryID
		{
			get { return m_AccRefCategoryID; }
			set { this.m_AccRefCategoryID = value; }
		}

		private string m_AccRefCode = string.Empty;
		public string AccRefCode
		{
			get { return m_AccRefCode; }
			set { this.m_AccRefCode = value; }
		}

		private string m_AccRefName = string.Empty;
		public string AccRefName
		{
			get { return m_AccRefName; }
			set { this.m_AccRefName = value; }
		}

		private string m_AccRefCategoryName = string.Empty;
		public string AccRefCategoryName
		{
			get { return m_AccRefCategoryName; }
			set { this.m_AccRefCategoryName = value; }
		}

		private string m_AccRefTypeName = string.Empty;
		public string AccRefTypeName
		{
			get { return m_AccRefTypeName; }
			set { this.m_AccRefTypeName = value; }
		}


	}
}
