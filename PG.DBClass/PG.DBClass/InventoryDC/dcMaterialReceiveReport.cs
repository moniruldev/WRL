using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
    public class dcMaterialReceiveReport
    {
        private string m_supAddress = string.Empty;
        public string SUP_NAME { get; set; }
        public string SUP_ADDRESS
        {

            get { return this.MRR_NOTE; }
            set
            {
                this.m_supAddress = value;

            }

        }
        public string MRR_NO { get; set; }
        public DateTime? MRR_DATE { get; set; }
        public string PURCHASE_TYPE { get; set; }

        public string REF_NO { get; set; }
        public string MRR_NOTE { get; set; }
        public string LC_NO { get; set; }
        public string CHALLAN_NO { get; set; }
        public string CREATE_BY_NAME{ get; set;}
        public string CREATE_BY_DESIGNATION { get; set; }
        public string QC_BY_NAME { get; set; }
        public string QC_BY { get; set; }
        public DateTime? QC_DATE { get; set; }
        public string QC_BY_DESIGNATION { get; set; }
        public Byte[] SIGN_PHOTO_CREATE_BY { get; set; }
        public Byte[] SIGN_PHOTO_QC_BY { get; set; }
        public DateTime? CREATE_DATE { get; set; }
        public string STORE_INCHARGE_NAME { get; set; }
        public string STORE_INCHARGE_DESIGNATION { get; set; }
        public Byte[] SIGN_PHOTO_STORE_INCHARGE { get; set; }
        public string STORE_NAME { get; set; }


    }
}
