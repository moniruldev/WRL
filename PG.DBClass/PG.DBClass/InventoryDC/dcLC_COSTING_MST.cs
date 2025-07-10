using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.InventoryDC
{
    [DBTable(Name = "LC_COSTING_MST")]
    public partial class dcLC_COSTING_MST : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_COSTING_ID = 0;
        private string m_COSTING_NO = string.Empty;
        private DateTime? m_COSTING_DATE = null;
        private int m_LC_ID = 0;
        private int m_ITEM_ID = 0;
        private string m_B_E_NO = string.Empty;
        private string m_BILL_NO = string.Empty;
        private decimal m_CNF_PRICE = 0;
        private decimal m_CONVERSION_RATE = 0;
        private decimal m_DOCUMENT_VALUE = 0;
        private decimal m_MARINE_INSURANCE = 0;
        private decimal m_INSURANCE_AND_OTH = 0;
        private decimal m_ASSESSABLE_VALUE = 0;
        private decimal m_GLOBAL_TAXES = 0;
        private decimal m_CD = 0;
        private decimal m_RD = 0;
        private decimal m_SD = 0;
        private decimal m_VAT = 0;
        private decimal m_AIT = 0;
        private decimal m_AT = 0;
        private decimal m_TOTAL_TAXES = 0;
        private decimal m_CLEARING_CHARGE = 0;
        private decimal m_PORT_CHARGE = 0;
        private decimal m_ADD_VAT = 0;
        private decimal m_OTHERS = 0;
        private decimal m_TOTAL_PORT_CHARGE = 0;
        private decimal m_SHIPPING_CHARGE = 0;
        private decimal m_NOC_CHARGE = 0;
        private decimal m_BERTH_OPERATOR_CHARGE = 0;
        private decimal m_SP_PERMISSION_CHARGE = 0;
        private decimal m_SAFTA_PURPOSE_CHARGE = 0;
        private decimal m_CNF_AGENT_COMMISION = 0;
        private decimal m_OTHERS_CHARGE = 0;
        private decimal m_TOTAL_CLEARING_CHARGE = 0;
        private decimal m_SEA_FREIGHT = 0;
        private decimal m_TRANSPORT = 0;
        private decimal m_MISCELLANEOUS = 0;
        private decimal m_TOTAL_COST_WITH_VAT = 0;
        private decimal m_TOTAL_COST_WO_VAT = 0;
        private decimal m_FACTOR = 0;
        private decimal m_UNIT_RATE = 0;
        private decimal m_LC_RATE = 0;
        private int m_DEPT_ID = 0;
        private decimal m_ITEM_QTY = 0;
        private decimal m_PORT_AIT = 0;
        private decimal m_CONVERTED_ITEM_QTY = 0;
        private string m_UOM_NAME = string.Empty;
        private string m_CONVERTED_UOM_NAME = string.Empty;
        private int m_CREATE_BY = 0;
        private DateTime? m_CREATE_DATE = null;
        private int m_UPDATE_BY = 0;
        private DateTime? m_UPDATE_DATE = null;
        private int m_AUTHO_BY = 0;
        private DateTime? m_AUTHO_DATE = null;
        private string m_AUTHO_STATUS = string.Empty;
        private decimal m_CNF_PRICE_ACT = 0;
        private decimal m_CONVERSION_RATE_ACT = 0;
        private decimal m_DOCUMENT_VALUE_ACT = 0;
        private decimal m_MARINE_INSURANCE_ACT = 0;
        private decimal m_INSURANCE_AND_OTH_ACT = 0;
        private decimal m_ASSESSABLE_VALUE_ACT = 0;
        private decimal m_GLOBAL_TAXES_ACT = 0;
        private decimal m_CD_ACT = 0;
        private decimal m_RD_ACT = 0;
        private decimal m_SD_ACT = 0;
        private decimal m_VAT_ACT = 0;
        private decimal m_AIT_ACT = 0;
        private decimal m_AT_ACT = 0;
        private decimal m_TOTAL_TAXES_ACT = 0;
        private decimal m_CLEARING_CHARGE_ACT = 0;
        private decimal m_PORT_CHARGE_ACT = 0;
        private decimal m_ADD_VAT_ACT = 0;
        private decimal m_OTHERS_ACT = 0;
        private decimal m_TOTAL_PORT_CHARGE_ACT = 0;
        private decimal m_SHIPPING_CHARGE_ACT = 0;
        private decimal m_NOC_CHARGE_ACT = 0;
        private decimal m_BERTH_OPERATOR_CHARGE_ACT = 0;
        private decimal m_SP_PERMISSION_CHARGE_ACT = 0;
        private decimal m_SAFTA_PURPOSE_CHARGE_ACT = 0;
        private decimal m_CNF_AGENT_COMMISION_ACT = 0;
        private decimal m_OTHERS_CHARGE_ACT = 0;
        private decimal m_TOTAL_CLEARING_CHARGE_ACT = 0;
        private decimal m_SEA_FREIGHT_ACT = 0;
        private decimal m_TRANSPORT_ACT = 0;
        private decimal m_MISCELLANEOUS_ACT = 0;
        private decimal m_TOTAL_COST_WITH_VAT_ACT = 0;
        private decimal m_TOTAL_COST_WO_VAT_ACT = 0;
        private decimal m_FACTOR_ACT = 0;
        private decimal m_UNIT_RATE_ACT = 0;
        private decimal m_LC_RATE_ACT = 0;
        private decimal m_ITEM_QTY_ACT = 0;
        private decimal m_PORT_AIT_ACT = 0;
        private decimal m_CONVERTED_ITEM_QTY_ACT = 0;
        private string m_UOM_NAME_ACT = string.Empty;
        private string m_CONVERTED_UOM_NAME_ACT = string.Empty;
        private int m_COSTING_ITEM_CAT_ID = 0;
        private decimal m_LC_TOT_VALUE_BDT = 0;
        private int m_LC_TOT_TYPE = 0;
        private int m_IMP_PURCHASE_ID = 0;
        
        

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


        [DBColumn(Name = "COSTING_ID", Storage = "m_COSTING_ID", DbType = "107", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int COSTING_ID
        {
            get { return this.m_COSTING_ID; }
            set
            {
                this.m_COSTING_ID = value;
                this.NotifyPropertyChanged("COSTING_ID");
            }
        }

        [DBColumn(Name = "COSTING_NO", Storage = "m_COSTING_NO", DbType = "126")]
        public string COSTING_NO
        {
            get { return this.m_COSTING_NO; }
            set
            {
                this.m_COSTING_NO = value;
                this.NotifyPropertyChanged("COSTING_NO");
            }
        }

        [DBColumn(Name = "COSTING_DATE", Storage = "m_COSTING_DATE", DbType = "106")]
        public DateTime? COSTING_DATE
        {
            get { return this.m_COSTING_DATE; }
            set
            {
                this.m_COSTING_DATE = value;
                this.NotifyPropertyChanged("COSTING_DATE");
            }
        }

        [DBColumn(Name = "LC_ID", Storage = "m_LC_ID", DbType = "107")]
        public int LC_ID
        {
            get { return this.m_LC_ID; }
            set
            {
                this.m_LC_ID = value;
                this.NotifyPropertyChanged("LC_ID");
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

        [DBColumn(Name = "B_E_NO", Storage = "m_B_E_NO", DbType = "126")]
        public string B_E_NO
        {
            get { return this.m_B_E_NO; }
            set
            {
                this.m_B_E_NO = value;
                this.NotifyPropertyChanged("B_E_NO");
            }
        }

        [DBColumn(Name = "BILL_NO", Storage = "m_BILL_NO", DbType = "126")]
        public string BILL_NO
        {
            get { return this.m_BILL_NO; }
            set
            {
                this.m_BILL_NO = value;
                this.NotifyPropertyChanged("BILL_NO");
            }
        }

        [DBColumn(Name = "CNF_PRICE", Storage = "m_CNF_PRICE", DbType = "107")]
        public decimal CNF_PRICE
        {
            get { return this.m_CNF_PRICE; }
            set
            {
                this.m_CNF_PRICE = value;
                this.NotifyPropertyChanged("CNF_PRICE");
            }
        }

        [DBColumn(Name = "CONVERSION_RATE", Storage = "m_CONVERSION_RATE", DbType = "107")]
        public decimal CONVERSION_RATE
        {
            get { return this.m_CONVERSION_RATE; }
            set
            {
                this.m_CONVERSION_RATE = value;
                this.NotifyPropertyChanged("CONVERSION_RATE");
            }
        }

        [DBColumn(Name = "DOCUMENT_VALUE", Storage = "m_DOCUMENT_VALUE", DbType = "107")]
        public decimal DOCUMENT_VALUE
        {
            get { return this.m_DOCUMENT_VALUE; }
            set
            {
                this.m_DOCUMENT_VALUE = value;
                this.NotifyPropertyChanged("DOCUMENT_VALUE");
            }
        }

        [DBColumn(Name = "MARINE_INSURANCE", Storage = "m_MARINE_INSURANCE", DbType = "107")]
        public decimal MARINE_INSURANCE
        {
            get { return this.m_MARINE_INSURANCE; }
            set
            {
                this.m_MARINE_INSURANCE = value;
                this.NotifyPropertyChanged("MARINE_INSURANCE");
            }
        }

        [DBColumn(Name = "INSURANCE_AND_OTH", Storage = "m_INSURANCE_AND_OTH", DbType = "107")]
        public decimal INSURANCE_AND_OTH
        {
            get { return this.m_INSURANCE_AND_OTH; }
            set
            {
                this.m_INSURANCE_AND_OTH = value;
                this.NotifyPropertyChanged("INSURANCE_AND_OTH");
            }
        }

        [DBColumn(Name = "ASSESSABLE_VALUE", Storage = "m_ASSESSABLE_VALUE", DbType = "107")]
        public decimal ASSESSABLE_VALUE
        {
            get { return this.m_ASSESSABLE_VALUE; }
            set
            {
                this.m_ASSESSABLE_VALUE = value;
                this.NotifyPropertyChanged("ASSESSABLE_VALUE");
            }
        }

        [DBColumn(Name = "GLOBAL_TAXES", Storage = "m_GLOBAL_TAXES", DbType = "107")]
        public decimal GLOBAL_TAXES
        {
            get { return this.m_GLOBAL_TAXES; }
            set
            {
                this.m_GLOBAL_TAXES = value;
                this.NotifyPropertyChanged("GLOBAL_TAXES");
            }
        }

        [DBColumn(Name = "CD", Storage = "m_CD", DbType = "107")]
        public decimal CD
        {
            get { return this.m_CD; }
            set
            {
                this.m_CD = value;
                this.NotifyPropertyChanged("CD");
            }
        }

        [DBColumn(Name = "RD", Storage = "m_RD", DbType = "107")]
        public decimal RD
        {
            get { return this.m_RD; }
            set
            {
                this.m_RD = value;
                this.NotifyPropertyChanged("RD");
            }
        }

        [DBColumn(Name = "SD", Storage = "m_SD", DbType = "107")]
        public decimal SD
        {
            get { return this.m_SD; }
            set
            {
                this.m_SD = value;
                this.NotifyPropertyChanged("SD");
            }
        }

        [DBColumn(Name = "VAT", Storage = "m_VAT", DbType = "107")]
        public decimal VAT
        {
            get { return this.m_VAT; }
            set
            {
                this.m_VAT = value;
                this.NotifyPropertyChanged("VAT");
            }
        }

        [DBColumn(Name = "AIT", Storage = "m_AIT", DbType = "107")]
        public decimal AIT
        {
            get { return this.m_AIT; }
            set
            {
                this.m_AIT = value;
                this.NotifyPropertyChanged("AIT");
            }
        }

        [DBColumn(Name = "AT", Storage = "m_AT", DbType = "107")]
        public decimal AT
        {
            get { return this.m_AT; }
            set
            {
                this.m_AT = value;
                this.NotifyPropertyChanged("AT");
            }
        }

        [DBColumn(Name = "TOTAL_TAXES", Storage = "m_TOTAL_TAXES", DbType = "107")]
        public decimal TOTAL_TAXES
        {
            get { return this.m_TOTAL_TAXES; }
            set
            {
                this.m_TOTAL_TAXES = value;
                this.NotifyPropertyChanged("TOTAL_TAXES");
            }
        }

        [DBColumn(Name = "CLEARING_CHARGE", Storage = "m_CLEARING_CHARGE", DbType = "107")]
        public decimal CLEARING_CHARGE
        {
            get { return this.m_CLEARING_CHARGE; }
            set
            {
                this.m_CLEARING_CHARGE = value;
                this.NotifyPropertyChanged("CLEARING_CHARGE");
            }
        }

        [DBColumn(Name = "PORT_CHARGE", Storage = "m_PORT_CHARGE", DbType = "107")]
        public decimal PORT_CHARGE
        {
            get { return this.m_PORT_CHARGE; }
            set
            {
                this.m_PORT_CHARGE = value;
                this.NotifyPropertyChanged("PORT_CHARGE");
            }
        }

        [DBColumn(Name = "ADD_VAT", Storage = "m_ADD_VAT", DbType = "107")]
        public decimal ADD_VAT
        {
            get { return this.m_ADD_VAT; }
            set
            {
                this.m_ADD_VAT = value;
                this.NotifyPropertyChanged("ADD_VAT");
            }
        }

        [DBColumn(Name = "OTHERS", Storage = "m_OTHERS", DbType = "107")]
        public decimal OTHERS
        {
            get { return this.m_OTHERS; }
            set
            {
                this.m_OTHERS = value;
                this.NotifyPropertyChanged("OTHERS");
            }
        }

        [DBColumn(Name = "TOTAL_PORT_CHARGE", Storage = "m_TOTAL_PORT_CHARGE", DbType = "107")]
        public decimal TOTAL_PORT_CHARGE
        {
            get { return this.m_TOTAL_PORT_CHARGE; }
            set
            {
                this.m_TOTAL_PORT_CHARGE = value;
                this.NotifyPropertyChanged("TOTAL_PORT_CHARGE");
            }
        }

        [DBColumn(Name = "SHIPPING_CHARGE", Storage = "m_SHIPPING_CHARGE", DbType = "107")]
        public decimal SHIPPING_CHARGE
        {
            get { return this.m_SHIPPING_CHARGE; }
            set
            {
                this.m_SHIPPING_CHARGE = value;
                this.NotifyPropertyChanged("SHIPPING_CHARGE");
            }
        }

        [DBColumn(Name = "NOC_CHARGE", Storage = "m_NOC_CHARGE", DbType = "107")]
        public decimal NOC_CHARGE
        {
            get { return this.m_NOC_CHARGE; }
            set
            {
                this.m_NOC_CHARGE = value;
                this.NotifyPropertyChanged("NOC_CHARGE");
            }
        }

        [DBColumn(Name = "BERTH_OPERATOR_CHARGE", Storage = "m_BERTH_OPERATOR_CHARGE", DbType = "107")]
        public decimal BERTH_OPERATOR_CHARGE
        {
            get { return this.m_BERTH_OPERATOR_CHARGE; }
            set
            {
                this.m_BERTH_OPERATOR_CHARGE = value;
                this.NotifyPropertyChanged("BERTH_OPERATOR_CHARGE");
            }
        }

        [DBColumn(Name = "SP_PERMISSION_CHARGE", Storage = "m_SP_PERMISSION_CHARGE", DbType = "107")]
        public decimal SP_PERMISSION_CHARGE
        {
            get { return this.m_SP_PERMISSION_CHARGE; }
            set
            {
                this.m_SP_PERMISSION_CHARGE = value;
                this.NotifyPropertyChanged("SP_PERMISSION_CHARGE");
            }
        }

        [DBColumn(Name = "SAFTA_PURPOSE_CHARGE", Storage = "m_SAFTA_PURPOSE_CHARGE", DbType = "107")]
        public decimal SAFTA_PURPOSE_CHARGE
        {
            get { return this.m_SAFTA_PURPOSE_CHARGE; }
            set
            {
                this.m_SAFTA_PURPOSE_CHARGE = value;
                this.NotifyPropertyChanged("SAFTA_PURPOSE_CHARGE");
            }
        }

        [DBColumn(Name = "CNF_AGENT_COMMISION", Storage = "m_CNF_AGENT_COMMISION", DbType = "107")]
        public decimal CNF_AGENT_COMMISION
        {
            get { return this.m_CNF_AGENT_COMMISION; }
            set
            {
                this.m_CNF_AGENT_COMMISION = value;
                this.NotifyPropertyChanged("CNF_AGENT_COMMISION");
            }
        }

        [DBColumn(Name = "OTHERS_CHARGE", Storage = "m_OTHERS_CHARGE", DbType = "107")]
        public decimal OTHERS_CHARGE
        {
            get { return this.m_OTHERS_CHARGE; }
            set
            {
                this.m_OTHERS_CHARGE = value;
                this.NotifyPropertyChanged("OTHERS_CHARGE");
            }
        }

        [DBColumn(Name = "TOTAL_CLEARING_CHARGE", Storage = "m_TOTAL_CLEARING_CHARGE", DbType = "107")]
        public decimal TOTAL_CLEARING_CHARGE
        {
            get { return this.m_TOTAL_CLEARING_CHARGE; }
            set
            {
                this.m_TOTAL_CLEARING_CHARGE = value;
                this.NotifyPropertyChanged("TOTAL_CLEARING_CHARGE");
            }
        }

        [DBColumn(Name = "SEA_FREIGHT", Storage = "m_SEA_FREIGHT", DbType = "107")]
        public decimal SEA_FREIGHT
        {
            get { return this.m_SEA_FREIGHT; }
            set
            {
                this.m_SEA_FREIGHT = value;
                this.NotifyPropertyChanged("SEA_FREIGHT");
            }
        }

        [DBColumn(Name = "TRANSPORT", Storage = "m_TRANSPORT", DbType = "107")]
        public decimal TRANSPORT
        {
            get { return this.m_TRANSPORT; }
            set
            {
                this.m_TRANSPORT = value;
                this.NotifyPropertyChanged("TRANSPORT");
            }
        }

        [DBColumn(Name = "MISCELLANEOUS", Storage = "m_MISCELLANEOUS", DbType = "107")]
        public decimal MISCELLANEOUS
        {
            get { return this.m_MISCELLANEOUS; }
            set
            {
                this.m_MISCELLANEOUS = value;
                this.NotifyPropertyChanged("MISCELLANEOUS");
            }
        }

        [DBColumn(Name = "TOTAL_COST_WITH_VAT", Storage = "m_TOTAL_COST_WITH_VAT", DbType = "107")]
        public decimal TOTAL_COST_WITH_VAT
        {
            get { return this.m_TOTAL_COST_WITH_VAT; }
            set
            {
                this.m_TOTAL_COST_WITH_VAT = value;
                this.NotifyPropertyChanged("TOTAL_COST_WITH_VAT");
            }
        }

        [DBColumn(Name = "TOTAL_COST_WO_VAT", Storage = "m_TOTAL_COST_WO_VAT", DbType = "107")]
        public decimal TOTAL_COST_WO_VAT
        {
            get { return this.m_TOTAL_COST_WO_VAT; }
            set
            {
                this.m_TOTAL_COST_WO_VAT = value;
                this.NotifyPropertyChanged("TOTAL_COST_WO_VAT");
            }
        }

        [DBColumn(Name = "FACTOR", Storage = "m_FACTOR", DbType = "107")]
        public decimal FACTOR
        {
            get { return this.m_FACTOR; }
            set
            {
                this.m_FACTOR = value;
                this.NotifyPropertyChanged("FACTOR");
            }
        }

        [DBColumn(Name = "UNIT_RATE", Storage = "m_UNIT_RATE", DbType = "107")]
        public decimal UNIT_RATE
        {
            get { return this.m_UNIT_RATE; }
            set
            {
                this.m_UNIT_RATE = value;
                this.NotifyPropertyChanged("UNIT_RATE");
            }
        }

        [DBColumn(Name = "LC_RATE", Storage = "m_LC_RATE", DbType = "107")]
        public decimal LC_RATE
        {
            get { return this.m_LC_RATE; }
            set
            {
                this.m_LC_RATE = value;
                this.NotifyPropertyChanged("LC_RATE");
            }
        }

        [DBColumn(Name = "DEPT_ID", Storage = "m_DEPT_ID", DbType = "107")]
        public int DEPT_ID
        {
            get { return this.m_DEPT_ID; }
            set
            {
                this.m_DEPT_ID = value;
                this.NotifyPropertyChanged("DEPT_ID");
            }
        }

        [DBColumn(Name = "ITEM_QTY", Storage = "m_ITEM_QTY", DbType = "107")]
        public decimal ITEM_QTY
        {
            get { return this.m_ITEM_QTY; }
            set
            {
                this.m_ITEM_QTY = value;
                this.NotifyPropertyChanged("ITEM_QTY");
            }
        }

        [DBColumn(Name = "PORT_AIT", Storage = "m_PORT_AIT", DbType = "107")]
        public decimal PORT_AIT
        {
            get { return this.m_PORT_AIT; }
            set
            {
                this.m_PORT_AIT = value;
                this.NotifyPropertyChanged("PORT_AIT");
            }
        }

        [DBColumn(Name = "CONVERTED_ITEM_QTY", Storage = "m_CONVERTED_ITEM_QTY", DbType = "107")]
        public decimal CONVERTED_ITEM_QTY
        {
            get { return this.m_CONVERTED_ITEM_QTY; }
            set
            {
                this.m_CONVERTED_ITEM_QTY = value;
                this.NotifyPropertyChanged("CONVERTED_ITEM_QTY");
            }
        }

        [DBColumn(Name = "UOM_NAME", Storage = "m_UOM_NAME", DbType = "107")]
        public string UOM_NAME
        {
            get { return this.m_UOM_NAME; }
            set
            {
                this.m_UOM_NAME = value;
                this.NotifyPropertyChanged("UOM_NAME");
            }
        }

        [DBColumn(Name = "CONVERTED_UOM_NAME", Storage = "m_CONVERTED_UOM_NAME", DbType = "107")]
        public string CONVERTED_UOM_NAME
        {
            get { return this.m_CONVERTED_UOM_NAME; }
            set
            {
                this.m_CONVERTED_UOM_NAME = value;
                this.NotifyPropertyChanged("CONVERTED_UOM_NAME");
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
        public int UPDATE_BY
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

        [DBColumn(Name = "AUTHO_BY", Storage = "m_AUTHO_BY", DbType = "107")]
        public int AUTHO_BY
        {
            get { return this.m_AUTHO_BY; }
            set
            {
                this.m_AUTHO_BY = value;
                this.NotifyPropertyChanged("AUTHO_BY");
            }
        }

        [DBColumn(Name = "AUTHO_DATE", Storage = "m_AUTHO_DATE", DbType = "106")]
        public DateTime? AUTHO_DATE
        {
            get { return this.m_AUTHO_DATE; }
            set
            {
                this.m_AUTHO_DATE = value;
                this.NotifyPropertyChanged("AUTHO_DATE");
            }
        }

        [DBColumn(Name = "AUTHO_STATUS", Storage = "m_AUTHO_STATUS", DbType = "126")]
        public string AUTHO_STATUS
        {
            get { return this.m_AUTHO_STATUS; }
            set
            {
                this.m_AUTHO_STATUS = value;
                this.NotifyPropertyChanged("AUTHO_STATUS");
            }
        }

        [DBColumn(Name = "CNF_PRICE_ACT", Storage = "m_CNF_PRICE_ACT", DbType = "107")]
        public decimal CNF_PRICE_ACT
        {
            get { return this.m_CNF_PRICE_ACT; }
            set
            {
                this.m_CNF_PRICE_ACT = value;
                this.NotifyPropertyChanged("CNF_PRICE_ACT");
            }
        }

        [DBColumn(Name = "CONVERSION_RATE_ACT", Storage = "m_CONVERSION_RATE_ACT", DbType = "107")]
        public decimal CONVERSION_RATE_ACT
        {
            get { return this.m_CONVERSION_RATE_ACT; }
            set
            {
                this.m_CONVERSION_RATE_ACT = value;
                this.NotifyPropertyChanged("CONVERSION_RATE_ACT");
            }
        }

        [DBColumn(Name = "DOCUMENT_VALUE_ACT", Storage = "m_DOCUMENT_VALUE_ACT", DbType = "107")]
        public decimal DOCUMENT_VALUE_ACT
        {
            get { return this.m_DOCUMENT_VALUE_ACT; }
            set
            {
                this.m_DOCUMENT_VALUE_ACT = value;
                this.NotifyPropertyChanged("DOCUMENT_VALUE_ACT");
            }
        }

        [DBColumn(Name = "MARINE_INSURANCE_ACT", Storage = "m_MARINE_INSURANCE_ACT", DbType = "107")]
        public decimal MARINE_INSURANCE_ACT
        {
            get { return this.m_MARINE_INSURANCE_ACT; }
            set
            {
                this.m_MARINE_INSURANCE_ACT = value;
                this.NotifyPropertyChanged("MARINE_INSURANCE_ACT");
            }
        }

        [DBColumn(Name = "INSURANCE_AND_OTH_ACT", Storage = "m_INSURANCE_AND_OTH_ACT", DbType = "107")]
        public decimal INSURANCE_AND_OTH_ACT
        {
            get { return this.m_INSURANCE_AND_OTH_ACT; }
            set
            {
                this.m_INSURANCE_AND_OTH_ACT = value;
                this.NotifyPropertyChanged("INSURANCE_AND_OTH_ACT");
            }
        }

        [DBColumn(Name = "ASSESSABLE_VALUE_ACT", Storage = "m_ASSESSABLE_VALUE_ACT", DbType = "107")]
        public decimal ASSESSABLE_VALUE_ACT
        {
            get { return this.m_ASSESSABLE_VALUE_ACT; }
            set
            {
                this.m_ASSESSABLE_VALUE_ACT = value;
                this.NotifyPropertyChanged("ASSESSABLE_VALUE_ACT");
            }
        }

        [DBColumn(Name = "GLOBAL_TAXES_ACT", Storage = "m_GLOBAL_TAXES_ACT", DbType = "107")]
        public decimal GLOBAL_TAXES_ACT
        {
            get { return this.m_GLOBAL_TAXES_ACT; }
            set
            {
                this.m_GLOBAL_TAXES_ACT = value;
                this.NotifyPropertyChanged("GLOBAL_TAXES_ACT");
            }
        }

        [DBColumn(Name = "CD_ACT", Storage = "m_CD_ACT", DbType = "107")]
        public decimal CD_ACT
        {
            get { return this.m_CD_ACT; }
            set
            {
                this.m_CD_ACT = value;
                this.NotifyPropertyChanged("CD_ACT");
            }
        }

        [DBColumn(Name = "RD_ACT", Storage = "m_RD_ACT", DbType = "107")]
        public decimal RD_ACT
        {
            get { return this.m_RD_ACT; }
            set
            {
                this.m_RD_ACT = value;
                this.NotifyPropertyChanged("RD_ACT");
            }
        }

        [DBColumn(Name = "SD_ACT", Storage = "m_SD_ACT", DbType = "107")]
        public decimal SD_ACT
        {
            get { return this.m_SD_ACT; }
            set
            {
                this.m_SD_ACT = value;
                this.NotifyPropertyChanged("SD_ACT");
            }
        }

        [DBColumn(Name = "VAT_ACT", Storage = "m_VAT_ACT", DbType = "107")]
        public decimal VAT_ACT
        {
            get { return this.m_VAT_ACT; }
            set
            {
                this.m_VAT_ACT = value;
                this.NotifyPropertyChanged("VAT_ACT");
            }
        }

        [DBColumn(Name = "AIT_ACT", Storage = "m_AIT_ACT", DbType = "107")]
        public decimal AIT_ACT
        {
            get { return this.m_AIT_ACT; }
            set
            {
                this.m_AIT_ACT = value;
                this.NotifyPropertyChanged("AIT_ACT");
            }
        }

        [DBColumn(Name = "AT_ACT", Storage = "m_AT_ACT", DbType = "107")]
        public decimal AT_ACT
        {
            get { return this.m_AT_ACT; }
            set
            {
                this.m_AT_ACT = value;
                this.NotifyPropertyChanged("AT_ACT");
            }
        }

        [DBColumn(Name = "TOTAL_TAXES_ACT", Storage = "m_TOTAL_TAXES_ACT", DbType = "107")]
        public decimal TOTAL_TAXES_ACT
        {
            get { return this.m_TOTAL_TAXES_ACT; }
            set
            {
                this.m_TOTAL_TAXES_ACT = value;
                this.NotifyPropertyChanged("TOTAL_TAXES_ACT");
            }
        }

        [DBColumn(Name = "CLEARING_CHARGE_ACT", Storage = "m_CLEARING_CHARGE_ACT", DbType = "107")]
        public decimal CLEARING_CHARGE_ACT
        {
            get { return this.m_CLEARING_CHARGE_ACT; }
            set
            {
                this.m_CLEARING_CHARGE_ACT = value;
                this.NotifyPropertyChanged("CLEARING_CHARGE_ACT");
            }
        }

        [DBColumn(Name = "PORT_CHARGE_ACT", Storage = "m_PORT_CHARGE_ACT", DbType = "107")]
        public decimal PORT_CHARGE_ACT
        {
            get { return this.m_PORT_CHARGE_ACT; }
            set
            {
                this.m_PORT_CHARGE_ACT = value;
                this.NotifyPropertyChanged("PORT_CHARGE_ACT");
            }
        }

        [DBColumn(Name = "ADD_VAT_ACT", Storage = "m_ADD_VAT_ACT", DbType = "107")]
        public decimal ADD_VAT_ACT
        {
            get { return this.m_ADD_VAT_ACT; }
            set
            {
                this.m_ADD_VAT_ACT = value;
                this.NotifyPropertyChanged("ADD_VAT_ACT");
            }
        }

        [DBColumn(Name = "OTHERS_ACT", Storage = "m_OTHERS_ACT", DbType = "107")]
        public decimal OTHERS_ACT
        {
            get { return this.m_OTHERS_ACT; }
            set
            {
                this.m_OTHERS_ACT = value;
                this.NotifyPropertyChanged("OTHERS_ACT");
            }
        }

        [DBColumn(Name = "TOTAL_PORT_CHARGE_ACT", Storage = "m_TOTAL_PORT_CHARGE_ACT", DbType = "107")]
        public decimal TOTAL_PORT_CHARGE_ACT
        {
            get { return this.m_TOTAL_PORT_CHARGE_ACT; }
            set
            {
                this.m_TOTAL_PORT_CHARGE_ACT = value;
                this.NotifyPropertyChanged("TOTAL_PORT_CHARGE_ACT");
            }
        }

        [DBColumn(Name = "SHIPPING_CHARGE_ACT", Storage = "m_SHIPPING_CHARGE_ACT", DbType = "107")]
        public decimal SHIPPING_CHARGE_ACT
        {
            get { return this.m_SHIPPING_CHARGE_ACT; }
            set
            {
                this.m_SHIPPING_CHARGE_ACT = value;
                this.NotifyPropertyChanged("SHIPPING_CHARGE_ACT");
            }
        }

        [DBColumn(Name = "NOC_CHARGE_ACT", Storage = "m_NOC_CHARGE_ACT", DbType = "107")]
        public decimal NOC_CHARGE_ACT
        {
            get { return this.m_NOC_CHARGE_ACT; }
            set
            {
                this.m_NOC_CHARGE_ACT = value;
                this.NotifyPropertyChanged("NOC_CHARGE_ACT");
            }
        }

        [DBColumn(Name = "BERTH_OPERATOR_CHARGE_ACT", Storage = "m_BERTH_OPERATOR_CHARGE_ACT", DbType = "107")]
        public decimal BERTH_OPERATOR_CHARGE_ACT
        {
            get { return this.m_BERTH_OPERATOR_CHARGE_ACT; }
            set
            {
                this.m_BERTH_OPERATOR_CHARGE_ACT = value;
                this.NotifyPropertyChanged("BERTH_OPERATOR_CHARGE_ACT");
            }
        }

        [DBColumn(Name = "SP_PERMISSION_CHARGE_ACT", Storage = "m_SP_PERMISSION_CHARGE_ACT", DbType = "107")]
        public decimal SP_PERMISSION_CHARGE_ACT
        {
            get { return this.m_SP_PERMISSION_CHARGE_ACT; }
            set
            {
                this.m_SP_PERMISSION_CHARGE_ACT = value;
                this.NotifyPropertyChanged("SP_PERMISSION_CHARGE_ACT");
            }
        }

        [DBColumn(Name = "SAFTA_PURPOSE_CHARGE_ACT", Storage = "m_SAFTA_PURPOSE_CHARGE_ACT", DbType = "107")]
        public decimal SAFTA_PURPOSE_CHARGE_ACT
        {
            get { return this.m_SAFTA_PURPOSE_CHARGE_ACT; }
            set
            {
                this.m_SAFTA_PURPOSE_CHARGE_ACT = value;
                this.NotifyPropertyChanged("SAFTA_PURPOSE_CHARGE_ACT");
            }
        }

        [DBColumn(Name = "CNF_AGENT_COMMISION_ACT", Storage = "m_CNF_AGENT_COMMISION_ACT", DbType = "107")]
        public decimal CNF_AGENT_COMMISION_ACT
        {
            get { return this.m_CNF_AGENT_COMMISION_ACT; }
            set
            {
                this.m_CNF_AGENT_COMMISION_ACT = value;
                this.NotifyPropertyChanged("CNF_AGENT_COMMISION_ACT");
            }
        }

        [DBColumn(Name = "OTHERS_CHARGE_ACT", Storage = "m_OTHERS_CHARGE_ACT", DbType = "107")]
        public decimal OTHERS_CHARGE_ACT
        {
            get { return this.m_OTHERS_CHARGE_ACT; }
            set
            {
                this.m_OTHERS_CHARGE_ACT = value;
                this.NotifyPropertyChanged("OTHERS_CHARGE_ACT");
            }
        }

        [DBColumn(Name = "TOTAL_CLEARING_CHARGE_ACT", Storage = "m_TOTAL_CLEARING_CHARGE_ACT", DbType = "107")]
        public decimal TOTAL_CLEARING_CHARGE_ACT
        {
            get { return this.m_TOTAL_CLEARING_CHARGE_ACT; }
            set
            {
                this.m_TOTAL_CLEARING_CHARGE_ACT = value;
                this.NotifyPropertyChanged("TOTAL_CLEARING_CHARGE_ACT");
            }
        }

        [DBColumn(Name = "SEA_FREIGHT_ACT", Storage = "m_SEA_FREIGHT_ACT", DbType = "107")]
        public decimal SEA_FREIGHT_ACT
        {
            get { return this.m_SEA_FREIGHT_ACT; }
            set
            {
                this.m_SEA_FREIGHT_ACT = value;
                this.NotifyPropertyChanged("SEA_FREIGHT_ACT");
            }
        }

        [DBColumn(Name = "TRANSPORT_ACT", Storage = "m_TRANSPORT_ACT", DbType = "107")]
        public decimal TRANSPORT_ACT
        {
            get { return this.m_TRANSPORT_ACT; }
            set
            {
                this.m_TRANSPORT_ACT = value;
                this.NotifyPropertyChanged("TRANSPORT_ACT");
            }
        }

        [DBColumn(Name = "MISCELLANEOUS_ACT", Storage = "m_MISCELLANEOUS_ACT", DbType = "107")]
        public decimal MISCELLANEOUS_ACT
        {
            get { return this.m_MISCELLANEOUS_ACT; }
            set
            {
                this.m_MISCELLANEOUS_ACT = value;
                this.NotifyPropertyChanged("MISCELLANEOUS_ACT");
            }
        }

        [DBColumn(Name = "TOTAL_COST_WITH_VAT_ACT", Storage = "m_TOTAL_COST_WITH_VAT_ACT", DbType = "107")]
        public decimal TOTAL_COST_WITH_VAT_ACT
        {
            get { return this.m_TOTAL_COST_WITH_VAT_ACT; }
            set
            {
                this.m_TOTAL_COST_WITH_VAT_ACT = value;
                this.NotifyPropertyChanged("TOTAL_COST_WITH_VAT_ACT");
            }
        }

        [DBColumn(Name = "TOTAL_COST_WO_VAT_ACT", Storage = "m_TOTAL_COST_WO_VAT_ACT", DbType = "107")]
        public decimal TOTAL_COST_WO_VAT_ACT
        {
            get { return this.m_TOTAL_COST_WO_VAT_ACT; }
            set
            {
                this.m_TOTAL_COST_WO_VAT_ACT = value;
                this.NotifyPropertyChanged("TOTAL_COST_WO_VAT_ACT");
            }
        }

        [DBColumn(Name = "FACTOR_ACT", Storage = "m_FACTOR_ACT", DbType = "107")]
        public decimal FACTOR_ACT
        {
            get { return this.m_FACTOR_ACT; }
            set
            {
                this.m_FACTOR_ACT = value;
                this.NotifyPropertyChanged("FACTOR_ACT");
            }
        }

        [DBColumn(Name = "UNIT_RATE_ACT", Storage = "m_UNIT_RATE_ACT", DbType = "107")]
        public decimal UNIT_RATE_ACT
        {
            get { return this.m_UNIT_RATE_ACT; }
            set
            {
                this.m_UNIT_RATE_ACT = value;
                this.NotifyPropertyChanged("UNIT_RATE_ACT");
            }
        }

        [DBColumn(Name = "LC_RATE_ACT", Storage = "m_LC_RATE_ACT", DbType = "107")]
        public decimal LC_RATE_ACT
        {
            get { return this.m_LC_RATE_ACT; }
            set
            {
                this.m_LC_RATE_ACT = value;
                this.NotifyPropertyChanged("LC_RATE_ACT");
            }
        }

        [DBColumn(Name = "ITEM_QTY_ACT", Storage = "m_ITEM_QTY_ACT", DbType = "107")]
        public decimal ITEM_QTY_ACT
        {
            get { return this.m_ITEM_QTY_ACT; }
            set
            {
                this.m_ITEM_QTY_ACT = value;
                this.NotifyPropertyChanged("ITEM_QTY_ACT");
            }
        }

        [DBColumn(Name = "PORT_AIT_ACT", Storage = "m_PORT_AIT_ACT", DbType = "107")]
        public decimal PORT_AIT_ACT
        {
            get { return this.m_PORT_AIT_ACT; }
            set
            {
                this.m_PORT_AIT_ACT = value;
                this.NotifyPropertyChanged("PORT_AIT_ACT");
            }
        }

        [DBColumn(Name = "CONVERTED_ITEM_QTY_ACT", Storage = "m_CONVERTED_ITEM_QTY_ACT", DbType = "107")]
        public decimal CONVERTED_ITEM_QTY_ACT
        {
            get { return this.m_CONVERTED_ITEM_QTY_ACT; }
            set
            {
                this.m_CONVERTED_ITEM_QTY_ACT = value;
                this.NotifyPropertyChanged("CONVERTED_ITEM_QTY_ACT");
            }
        }

        [DBColumn(Name = "UOM_NAME_ACT", Storage = "m_UOM_NAME_ACT", DbType = "126")]
        public string UOM_NAME_ACT
        {
            get { return this.m_UOM_NAME_ACT; }
            set
            {
                this.m_UOM_NAME_ACT = value;
                this.NotifyPropertyChanged("UOM_NAME_ACT");
            }
        }

        [DBColumn(Name = "CONVERTED_UOM_NAME_ACT", Storage = "m_CONVERTED_UOM_NAME_ACT", DbType = "126")]
        public string CONVERTED_UOM_NAME_ACT
        {
            get { return this.m_CONVERTED_UOM_NAME_ACT; }
            set
            {
                this.m_CONVERTED_UOM_NAME_ACT = value;
                this.NotifyPropertyChanged("CONVERTED_UOM_NAME_ACT");
            }
        }

        [DBColumn(Name = "COSTING_ITEM_CAT_ID", Storage = "m_COSTING_ITEM_CAT_ID", DbType = "107")]
        public int COSTING_ITEM_CAT_ID
        {
            get { return this.m_COSTING_ITEM_CAT_ID; }
            set
            {
                this.m_COSTING_ITEM_CAT_ID = value;
                this.NotifyPropertyChanged("COSTING_ITEM_CAT_ID");
            }
        }

         [DBColumn(Name = "LC_TOT_VALUE_BDT", Storage = "m_LC_TOT_VALUE_BDT", DbType = "107")]
        public decimal LC_TOT_VALUE_BDT
        {
            get { return this.m_LC_TOT_VALUE_BDT; }
            set
            {
                this.m_LC_TOT_VALUE_BDT = value;
                this.NotifyPropertyChanged("LC_TOT_VALUE_BDT");
            }
        }

         [DBColumn(Name = "LC_TOT_TYPE", Storage = "m_LC_TOT_TYPE", DbType = "107")]
         public int LC_TOT_TYPE
        {
            get { return this.m_LC_TOT_TYPE; }
            set
            {
                this.m_LC_TOT_TYPE = value;
                this.NotifyPropertyChanged("LC_TOT_TYPE");
            }
        }

         [DBColumn(Name = "IMP_PURCHASE_ID", Storage = "m_IMP_PURCHASE_ID", DbType = "107")]
         public int IMP_PURCHASE_ID
        {
            get { return this.m_IMP_PURCHASE_ID; }
            set
            {
                this.m_IMP_PURCHASE_ID = value;
                this.NotifyPropertyChanged("IMP_PURCHASE_ID");
            }
        }

        
        
        
        #endregion //properties
    }

    public partial class dcLC_COSTING_MST
    {
        public string ITEM_NAME { get; set; }
        public string LC_NO { get; set; }
        public string IMP_PURCHASE_NO { get; set; }
        public string SUP_NAME { get; set; }
        public int SUP_ID { get; set; }
        public string COSTING_ITEM_CAT_NAME { get; set; }
        

    }
}
