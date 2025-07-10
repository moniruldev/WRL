using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.InventoryDC
{
    [DBTable(Name = "LC_WISE_FIXED_CHARGE")]
    public partial class dcLC_WISE_FIXED_CHARGE : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_FIXED_CHARGE_ID = 0;
        private int m_LC_ID = 0;
        private decimal m_PORT_CHARGE = 0;
        private decimal m_ADD_VAT = 0;
        private decimal m_PORT_AIT = 0;
        private decimal m_OTHERS = 0;
        private decimal m_TOTAL_PORT_CHARGE = 0;
        private decimal m_SHIPPING_CHARGE = 0;
        private decimal m_NOC_CHARGE = 0;
        private decimal m_SAFTA_CHARGE = 0;
        private decimal m_BIRTH_OPERATOR_CHARGE = 0;
        private decimal m_TRANSPORT = 0;
        private decimal m_SP_PERMISSION_CHARGE = 0;
        private decimal m_CNF_COMMISION = 0;
        private decimal m_MISCELLANEOUS = 0;
        private decimal m_OTHERS_CHARGE = 0;
        private decimal m_TOTAL_CHARGE = 0;
        private decimal m_SEA_FREIGHT = 0;
        private decimal m_GLOBAL_TAXES = 0;
        private decimal m_GLOBAL_TAXES_ACT = 0;
        private decimal m_PORT_CHARGE_ACT = 0;
        private decimal m_ADD_VAT_ACT = 0;
        private decimal m_PORT_AIT_ACT = 0;
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


        [DBColumn(Name = "FIXED_CHARGE_ID", Storage = "m_FIXED_CHARGE_ID", DbType = "107", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int FIXED_CHARGE_ID
        {
            get { return this.m_FIXED_CHARGE_ID; }
            set
            {
                this.m_FIXED_CHARGE_ID = value;
                this.NotifyPropertyChanged("FIXED_CHARGE_ID");
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

        [DBColumn(Name = "SAFTA_CHARGE", Storage = "m_SAFTA_CHARGE", DbType = "107")]
        public decimal SAFTA_CHARGE
        {
            get { return this.m_SAFTA_CHARGE; }
            set
            {
                this.m_SAFTA_CHARGE = value;
                this.NotifyPropertyChanged("SAFTA_CHARGE");
            }
        }

        [DBColumn(Name = "BIRTH_OPERATOR_CHARGE", Storage = "m_BIRTH_OPERATOR_CHARGE", DbType = "107")]
        public decimal BIRTH_OPERATOR_CHARGE
        {
            get { return this.m_BIRTH_OPERATOR_CHARGE; }
            set
            {
                this.m_BIRTH_OPERATOR_CHARGE = value;
                this.NotifyPropertyChanged("BIRTH_OPERATOR_CHARGE");
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

        [DBColumn(Name = "CNF_COMMISION", Storage = "m_CNF_COMMISION", DbType = "107")]
        public decimal CNF_COMMISION
        {
            get { return this.m_CNF_COMMISION; }
            set
            {
                this.m_CNF_COMMISION = value;
                this.NotifyPropertyChanged("CNF_COMMISION");
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

        [DBColumn(Name = "TOTAL_CHARGE", Storage = "m_TOTAL_CHARGE", DbType = "107")]
        public decimal TOTAL_CHARGE
        {
            get { return this.m_TOTAL_CHARGE; }
            set
            {
                this.m_TOTAL_CHARGE = value;
                this.NotifyPropertyChanged("TOTAL_CHARGE");
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
         
        #endregion //properties
    }
}
