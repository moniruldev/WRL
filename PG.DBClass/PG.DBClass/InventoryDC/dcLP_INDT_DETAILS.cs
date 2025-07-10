using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
    [Serializable]
    [DBTable(Name = "LP_INDT_DETAILS")]
    public partial class dcLP_INDT_DETAILS : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private Int64 m_INDT_DET_ID = 0;
        private int m_INDT_SLNO = 0;
        private Int64 m_INDT_ID = 0;
        private int m_ITEM_ID = 0;
        private decimal m_INDT_QTY = 0;
        private decimal m_UNIT_PRICE = 0;
        private int m_UOM_ID = 0;
        private string m_INDT_REMARKS = string.Empty;
        private decimal m_INDT_QTY_APPROVED = 0;
        private string m_APPROVED_REMARKS = string.Empty;
        private int m_CREATE_BY = 0;
        private DateTime? m_CREATE_DATE = null;
        private int? m_UPDATE_BY = 0;
        private DateTime? m_UPDATE_DATE = null;
        private string m_PRIORITY = string.Empty;
        private int? m_ITEM_TYPE_ID = 0;

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


        [DBColumn(Name = "INDT_DET_ID", Storage = "m_INDT_DET_ID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public Int64 INDT_DET_ID
        {
            get { return this.m_INDT_DET_ID; }
            set
            {
                this.m_INDT_DET_ID = value;
                this.NotifyPropertyChanged("INDT_DET_ID");
            }
        }

        [DBColumn(Name = "INDT_SLNO", Storage = "m_INDT_SLNO", DbType = "107")]
        public int INDT_SLNO
        {
            get { return this.m_INDT_SLNO; }
            set
            {
                this.m_INDT_SLNO = value;
                this.NotifyPropertyChanged("INDT_SLNO");
            }
        }

        [DBColumn(Name = "INDT_ID", Storage = "m_INDT_ID", DbType = "107")]
        public Int64 INDT_ID
        {
            get { return this.m_INDT_ID; }
            set
            {
                this.m_INDT_ID = value;
                this.NotifyPropertyChanged("INDT_ID");
            }
        }

        [DBColumn(Name = "ITEM_ID", Storage = "m_ITEM_ID", DbType = "107")]
        public int ITEM_ID
        {
            get { return this.m_ITEM_ID; }
            set
            {
                this.m_ITEM_ID = value;
                this.NotifyPropertyChanged("ITEM_ID");
            }
        }

        [DBColumn(Name = "INDT_QTY", Storage = "m_INDT_QTY", DbType = "107")]
        public decimal INDT_QTY
        {
            get { return this.m_INDT_QTY; }
            set
            {
                this.m_INDT_QTY = value;
                this.NotifyPropertyChanged("INDT_QTY");
            }
        }


        [DBColumn(Name = "UNIT_PRICE", Storage = "m_UNIT_PRICE", DbType = "107")]
        public decimal UNIT_PRICE
        {
            get { return this.m_UNIT_PRICE; }
            set
            {
                this.m_UNIT_PRICE = value;
                this.NotifyPropertyChanged("UNIT_PRICE");
            }
        }

        [DBColumn(Name = "UOM_ID", Storage = "m_UOM_ID", DbType = "107")]
        public int UOM_ID
        {
            get { return this.m_UOM_ID; }
            set
            {
                this.m_UOM_ID = value;
                this.NotifyPropertyChanged("UOM_ID");
            }
        }

        [DBColumn(Name = "INDT_REMARKS", Storage = "m_INDT_REMARKS", DbType = "126")]
        public string INDT_REMARKS
        {
            get { return this.m_INDT_REMARKS; }
            set
            {
                this.m_INDT_REMARKS = value;
                this.NotifyPropertyChanged("INDT_REMARKS");
            }
        }

        [DBColumn(Name = "INDT_QTY_APPROVED", Storage = "m_INDT_QTY_APPROVED", DbType = "107")]
        public decimal INDT_QTY_APPROVED
        {
            get { return this.m_INDT_QTY_APPROVED; }
            set
            {
                this.m_INDT_QTY_APPROVED = value;
                this.NotifyPropertyChanged("INDT_QTY_APPROVED");
            }
        }

        [DBColumn(Name = "APPROVED_REMARKS", Storage = "m_APPROVED_REMARKS", DbType = "126")]
        public string APPROVED_REMARKS
        {
            get { return this.m_APPROVED_REMARKS; }
            set
            {
                this.m_APPROVED_REMARKS = value;
                this.NotifyPropertyChanged("APPROVED_REMARKS");
            }
        }
        [DBColumn(Name = "CREATE_BY", Storage = "m_CREATE_BY", DbType = "107")]
        public int CREATE_BY
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

        [DBColumn(Name = "UPDATE_BY", Storage = "m_UPDATE_BY", DbType = "107")]
        public int? UPDATE_BY
        {
            get { return this.m_UPDATE_BY; }
            set
            {
                this.m_UPDATE_BY = value;
                this.NotifyPropertyChanged("UPDATE_BY");
            }
        }

        [DBColumn(Name = "UPDATE_DATE", Storage = "m_UPDATE_DATE", DbType = "106")]
        public DateTime? UPDATE_DATE
        {
            get { return this.m_UPDATE_DATE; }
            set
            {
                this.m_UPDATE_DATE = value;
                this.NotifyPropertyChanged("UPDATE_DATE");
            }
        }

        [DBColumn(Name = "PRIORITY", Storage = "m_PRIORITY", DbType = "126")]
        public string PRIORITY
        {
            get { return this.m_PRIORITY; }
            set
            {
                this.m_PRIORITY = value;
                this.NotifyPropertyChanged("PRIORITY");
            }
        }

        [DBColumn(Name = "ITEM_TYPE_ID", Storage = "m_ITEM_TYPE_ID", DbType = "107")]
        public int? ITEM_TYPE_ID
        {
            get { return this.m_ITEM_TYPE_ID; }
            set
            {
                this.m_ITEM_TYPE_ID = value;
                this.NotifyPropertyChanged("ITEM_TYPE_ID");
            }
        }

        #endregion //properties




    }

    public partial class dcLP_INDT_DETAILS
    {

        private string m_item_desc = string.Empty;
        private string m_item_name = string.Empty;
        private string m_uom_name = null;
        private string m_item_code = string.Empty;
        private string m_item_group_name = string.Empty;
        private int? m_item_group_id = 0;



        public string item_desc
        {
            get { return m_item_desc; }
            set { this.m_item_desc = value; }
        }

        public string item_name
        {
            get { return m_item_name; }
            set { this.m_item_name = value; }
        }
        public string uom_name
        {
            get { return m_uom_name; }
            set { this.m_uom_name = value; }
        }
        public string item_code
        {
            get { return m_item_code; }
            set { this.m_item_code = value; }
        }
        public string item_group_name
        {
            get { return m_item_group_name; }
            set { this.m_item_group_name = value; }
        }
        public int? item_group_id
        {
            get { return m_item_group_id; }
            set { this.m_item_group_id = value; }
        }

        private decimal m_SAFTY_STOCK_LEVEL = 0;
        public decimal SAFTY_STOCK_LEVEL
        {
            get { return m_SAFTY_STOCK_LEVEL; }
            set { this.m_SAFTY_STOCK_LEVEL = value; }
        }


        private decimal m_TOTAL_REQ_QTY = 0;
        public decimal TOTAL_REQ_QTY
        {
            get { return m_TOTAL_REQ_QTY; }
            set { this.m_TOTAL_REQ_QTY = value; }
        }

        private decimal m_RE_ORDER_LEVEL = 0;
        public decimal RE_ORDER_LEVEL
        {
            get { return m_RE_ORDER_LEVEL; }
            set { this.m_RE_ORDER_LEVEL = value; }
        }
        public string INCLUDE_SAFETY_STOCK { get; set; }

        //10  -5

        private decimal m_FINAL_INDT_QTY = 0;
        public decimal FINAL_INDT_QTY
        {
            get
            {

                decimal finalReqQty = 0;

                if (this.INCLUDE_SAFETY_STOCK == "Y")
                {
                    if (this.TOTAL_REQ_QTY <= 0)
                    {
                        if (this.CLOSING_QTY > this.SAFTY_STOCK_LEVEL)
                        {
                            return 0;
                        }
                        else
                        {
                            finalReqQty = (this.CLOSING_QTY) > 0 ? (this.SAFTY_STOCK_LEVEL - Math.Abs(this.CLOSING_QTY)) : (this.SAFTY_STOCK_LEVEL + Math.Abs(this.CLOSING_QTY));
                            finalReqQty = finalReqQty > 0 ? finalReqQty : 0;
                            return finalReqQty;

                        }
                    }
                    else
                    {

                        if (this.CLOSING_QTY == 0)
                        {
                            finalReqQty = this.SAFTY_STOCK_LEVEL + this.TOTAL_REQ_QTY;
                            finalReqQty = finalReqQty > 0 ? finalReqQty : 0;
                            return finalReqQty;
                        }
                        else
                        {
                            if (this.CLOSING_QTY < 0)
                            {
                                finalReqQty = this.SAFTY_STOCK_LEVEL + this.TOTAL_REQ_QTY + Math.Abs(this.CLOSING_QTY);
                            }
                            else
                            {
                                if ((this.SAFTY_STOCK_LEVEL + this.TOTAL_REQ_QTY) <= this.CLOSING_QTY)
                                {
                                    return this.TOTAL_REQ_QTY;
                                }
                                else
                                {
                                    finalReqQty = this.SAFTY_STOCK_LEVEL + this.TOTAL_REQ_QTY - this.CLOSING_QTY;
                                }

                            }
                            finalReqQty = finalReqQty > 0 ? finalReqQty : 0;
                            return finalReqQty;
                        }


                    }
                }
                else
                {
                    finalReqQty = this.TOTAL_REQ_QTY;
                    finalReqQty = finalReqQty > 0 ? finalReqQty : 0;
                    return finalReqQty;
                }


            }

        }

        private decimal m_CLOSING_QTY = 0;
        public decimal CLOSING_QTY
        {
            get { return m_CLOSING_QTY; }
            set { this.m_CLOSING_QTY = value; }
        }

        private string m_DEPARTMENT_NAME = string.Empty;
        public string DEPARTMENT_NAME
        {
            get { return m_DEPARTMENT_NAME; }
            set { this.m_DEPARTMENT_NAME = value; }
        }

        private string m_INDT_NO = string.Empty;
        public string INDT_NO
        {
            get { return m_INDT_NO; }
            set { this.m_INDT_NO = value; }
        }

        private DateTime? m_INDT_DATE = null;
         public DateTime? INDT_DATE
        {
            get { return m_INDT_DATE; }
            set { this.m_INDT_DATE = value; }
        }

         public decimal WEIGHTED_AVERAGE_PRICE { get; set; }

         private string m_ITEM_TYPE_CODE = string.Empty;
        public string ITEM_TYPE_CODE
        {
            get { return m_ITEM_TYPE_CODE; }
            set { this.m_ITEM_TYPE_CODE = value; }
        }
        
        
    }

}
