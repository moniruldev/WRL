using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG.Report.ReportClass.InventoryRC
{
    [Serializable]
    public class rcItemGroupReport
    {
        public string ITEM_NAME { get; set; }
        public string ITEM_CODE { get; set; }
        public string UOM_NAME { get; set; }
        public string ITEM_GROUP_NAME { get; set; }
        public string ITEM_DESCRIPTION { get; set; }
        public string ITEM_TYPE_NAME { get; set; }
        public string ITEM_CLASS_NAME { get; set; }
        public string ITEM_SNS_NAME { get; set; }

        private int m_ItemLevel = 0;
        public int ItemLevel
        {
            get { return this.m_ItemLevel; }
            set { this.m_ItemLevel = value; }
        }


        private int m_ChildItemCount = 0;
        public int ChildItemCount
        {
            get { return this.m_ChildItemCount; }
            set { this.m_ChildItemCount = value; }
        }

        private int m_ItemGroupParentID = 0;
        public int ItemGroupParentID
        {
            get { return this.m_ItemGroupParentID; }
            set { this.m_ItemGroupParentID = value; }
        }

        private int m_ItemType = 0;
        public int ItemType
        {
            get { return this.m_ItemType; }
            set { this.m_ItemType = value; }
        }

        ////0=item,1=Sum/Total, 2=BlankSpace, 
        //private int m_ItemShowType = 0;
        //public int ItemShowType
        //{
        //    get { return this.m_ItemShowType; }
        //    set { this.m_ItemShowType = value; }
        //}


        private int m_ItemID = 0;
        public int ItemID
        {
            get { return this.m_ItemID; }
            set { this.m_ItemID = value; }
        }

        private string m_ItemCode = string.Empty;
        public string ItemCode
        {
            get { return this.m_ItemCode; }
            set { this.m_ItemCode = value; }
        }


        private string m_ItemName = string.Empty;
        public string ItemName
        {
            get { return this.m_ItemName; }
            set { this.m_ItemName = value; }
        }

        private string m_ItemNameShort = string.Empty;
        public string ItemNameShort
        {
            get { return this.m_ItemNameShort; }
            set { this.m_ItemNameShort = value; }
        }


        private string m_ItemNameIndent = string.Empty;
        public string ItemNameIndent
        {
            get { return this.m_ItemNameIndent; }
            set { this.m_ItemNameIndent = value; }
        }

        private string m_ItemNameDispaly = string.Empty;
        public string ItemNameDispaly
        {
            get { return this.m_ItemNameDispaly; }
            set { this.m_ItemNameDispaly = value; }
        }



        private int m_AccGLClassID = 0;
        public int AccGLClassID
        {
            get { return this.m_AccGLClassID; }
            set { this.m_AccGLClassID = value; }
        }

        private int m_ItemIDParent = 0;
        public int ItemIDParent
        {
            get { return this.m_ItemIDParent; }
            set { this.m_ItemIDParent = value; }
        }

        private string m_ItemNameParent = string.Empty;
        public string ItemNameParent
        {
            get { return this.m_ItemNameParent; }
            set { this.m_ItemNameParent = value; }
        }

        private string m_ItemNameParentEffective = string.Empty;
        public string ItemNameParentEffective
        {
            get { return this.m_ItemNameParentEffective; }
            set { this.m_ItemNameParentEffective = value; }
        }


        private string m_ItemNameFull = string.Empty;
        public string ItemNameFull
        {
            get { return this.m_ItemNameFull; }
            set { this.m_ItemNameFull = value; }
        }

        private int m_ItemSLNo = 0;
        public int ItemSLNo
        {
            get { return this.m_ItemSLNo; }
            set { this.m_ItemSLNo = value; }
        }


    


        private bool m_IsBoldName = false;
        public bool IsBoldName
        {
            get { return this.m_IsBoldName; }
            set { this.m_IsBoldName = value; }
        }

        private bool m_IsItalicName = false;
        public bool IsItalicName
        {
            get { return this.m_IsItalicName; }
            set { this.m_IsItalicName = value; }
        }

        private bool m_IsUnderlinedName = false;
        public bool IsUnderlinedName
        {
            get { return this.m_IsUnderlinedName; }
            set { this.m_IsUnderlinedName = value; }
        }


        private bool m_IsBoldAmt = false;
        public bool IsBoldAmt
        {
            get { return this.m_IsBoldAmt; }
            set { this.m_IsBoldAmt = value; }
        }

        private bool m_IsItalicAmt = false;
        public bool IsItalicAmt
        {
            get { return this.m_IsItalicAmt; }
            set { this.m_IsItalicAmt = value; }
        }

        private bool m_IsUnderlinedAmt = false;
        public bool IsUnderlinedAmt
        {
            get { return this.m_IsUnderlinedAmt; }
            set { this.m_IsUnderlinedAmt = value; }
        }

        private string m_BorderTopAmt = "None";
        public string BorderTopAmt
        {
            get { return this.m_BorderTopAmt; }
            set { this.m_BorderTopAmt = value; }
        }

        private string m_BorderBottomAmt = "None";
        public string BorderBottomAmt
        {
            get { return this.m_BorderBottomAmt; }
            set { this.m_BorderBottomAmt = value; }
        }

        private string m_BorderWidthTopAmt = "1pt";
        public string BorderWidthTopAmt
        {
            get { return this.m_BorderWidthTopAmt; }
            set { this.m_BorderWidthTopAmt = value; }
        }

        private string m_BorderWidthBottomAmt = "1pt";
        public string BorderWidthBottomAmt
        {
            get { return this.m_BorderWidthBottomAmt; }
            set { this.m_BorderWidthBottomAmt = value; }
        }


        private bool m_IsBoldAmtSub1 = false;
        public bool IsBoldAmtSub1
        {
            get { return this.m_IsBoldAmtSub1; }
            set { this.m_IsBoldAmtSub1 = value; }
        }

        private bool m_IsItalicAmtSub1 = false;
        public bool IsItalicAmtSub1
        {
            get { return this.m_IsItalicAmtSub1; }
            set { this.m_IsItalicAmtSub1 = value; }
        }


        private string m_BorderTopAmtSub1 = "None";
        public string BorderTopAmtSub1
        {
            get { return this.m_BorderTopAmtSub1; }
            set { this.m_BorderTopAmtSub1 = value; }
        }

        private string m_BorderBottomAmtSub1 = "None";
        public string BorderBottomAmtSub1
        {
            get { return this.m_BorderBottomAmtSub1; }
            set { this.m_BorderBottomAmtSub1 = value; }
        }

        private string m_BorderWidthTopAmtSub1 = "1pt";
        public string BorderWidthTopAmtSub1
        {
            get { return this.m_BorderWidthTopAmtSub1; }
            set { this.m_BorderWidthTopAmtSub1 = value; }
        }

        private string m_BorderWidthBottomAmtSub1 = "1pt";
        public string BorderWidthBottomAmtSub1
        {
            get { return this.m_BorderWidthBottomAmtSub1; }
            set { this.m_BorderWidthBottomAmtSub1 = value; }
        }

        private string m_NumberFormat = string.Empty;
        public string NumberFormat
        {
            get { return this.m_NumberFormat; }
            set { this.m_NumberFormat = value; }
        }

        private bool m_ItemIsCash = false;
        public bool ItemIsCash
        {
            get { return this.m_ItemIsCash; }
            set { this.m_ItemIsCash = value; }
        }

        private bool m_ItemIsBank = false;
        public bool ItemIsBank
        {
            get { return this.m_ItemIsBank; }
            set { this.m_ItemIsBank = value; }
        }

        private string m_GroupledgerShowType = "";
        public string GroupledgerShowType
        {
            get { return this.m_GroupledgerShowType; }
            set { this.m_GroupledgerShowType = value; }
        }
        //public string ITEM_DESCRIPTION { get; set; }
    }
}
