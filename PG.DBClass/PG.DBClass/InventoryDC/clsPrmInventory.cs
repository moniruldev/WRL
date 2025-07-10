using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
    public class clsPrmInventory : ICloneable
    {
        public string NumberFormat = "#,###.00;(-)#,###.00;#";
        public string NumberFormatZero = "#,###.00;(-)#,###.00;#0.00";
        public string NumberFormatBlank = "#;#;#";
        public bool FormatNumberInBD = false;


        public InventoryOrderByEnum OrderBy = InventoryOrderByEnum.Name;


        public int GroupIDParent = 0;
        public string withPrice = "N";

        public string TransMonth = string.Empty;

        public string DealerID = string.Empty;
        public string DealerType = string.Empty;
        public string DealerTypeName = string.Empty;

        public string LocCode = string.Empty;
        public string IsActive = string.Empty;

        public int FilterType = -1;
        public Int64 masterId = -1;
        public Int64 chaildId = -1;
        public int STLM_ID = -1;
        public string STLM_NAME = string.Empty;
        public string Chargetype = string.Empty;
        public string Bat_cat_ID = string.Empty;


        public int InvoiceSetProcessID = 0;
        public string InvoiceSetProcessLabel = string.Empty;

        public DateTime? FromDate = null;
        public DateTime? ToDate = null;
        public bool IsBeforeDate = false;


        public DateTime? FromTDate = null;
        public DateTime? ToTDate = null;

        public DateTime? AgeingDate = null;
        public int DeptID = 0;


        public string GroupBy = string.Empty;



        public bool IsIndentName = false;
        public string IndentPaddingChar = "\t";
        public bool IsExcelExport = false;
        //public string IndentPaddingCharExcel = "\t\t";
        public string IndentPaddingCharExcel = "\t\t\t\t";


        //public GLReportTypeEnum ReportType = GLReportTypeEnum.Standard;


        //TODO Change monir
        public int LocationID = 0;

        public List<string> Costing_item_catlist = new List<string>();
        public List<string> LocationIDList = new List<string>();
        public List<string> CatidList = new List<string>();
        public List<string> LocationNameList = new List<string>();

        public List<string> RECEIVEIDList = new List<string>();

        public bool IsLocation = false;

        public string Item_Code = string.Empty;
        public int Report_Type = 0;
        public int Report_Category = 0;
        public string Businesstype = string.Empty;
        public string Businesstype_Name = string.Empty;
        public string location_name = string.Empty;
        public int depo_group = 0;
        public string depo_group_name = string.Empty;
        public string depo_group_inv = string.Empty;
        public string dc_no = string.Empty;
        public string do_no = string.Empty;
        public string salesInvoiceNo = string.Empty;
        public string ITEM_TYPESN = string.Empty;
         
        public string days_from = string.Empty;
        public string days_to = string.Empty;
        public string SE_ID = string.Empty;
        public string SE_Name = string.Empty;
        public string Teritory_ID = string.Empty;
        public string Teritory_Name = string.Empty;
        public decimal Stock_Norms = 0;

        public int itemtype_id = 0;
        public int itemGroup_id = 0;
        public int item_id = 0;
        public int store_id = 0;
        public string itemtypeName = string.Empty;
        public string itemgroupName = string.Empty;
        public string itemName = string.Empty;
        public string storeName = string.Empty;
        public string itemclassname = string.Empty;
        public bool ShowOnlyStockItem = false;
        public int Costing_Item_Type_id = 0;
        public string Costing_Item_Type_name = string.Empty;
        public int custid = 0;
        public string Cust_name = string.Empty;
        public string reporttypes = string.Empty;
        public string reporttypestxt = string.Empty;

        public int sup_id = 0;
        public string sup_name = string.Empty;
        public int MRR_TYPE = 0;
        public string MaterialReceiveNo = string.Empty;

        public string Report_Category_Name = string.Empty;
        public int Battery_Type_ID = 0;

        public string Is_Repair = string.Empty;

        public string LC_No = string.Empty;

        public int SNS_Type = 0;   // Storable - Non-Storable item

        public int bomid = 0;

         // Production Parameter
        public int prod_id = 0;
        public string prod_no = "";
        public DateTime? productiondate = null;
        public string fromProdDate = null;
        public string toProdDate = null;
        public string isPacking = null;
        public string issulphation = null;
        public string issmallparts = null;
        public string user_name = string.Empty;
        public int user_id = 0;
        public int RECEIVE_ID = 0;
        public int Adjust_ID = 0;
        public string Adjust_No = null;

        public string fcid = null;
        public int fcType = 0;
        public string RECEIVE_NO = string.Empty;
        public int CREATE_BY = 0;
        public string BatchNO = String.Empty;

        public bool ShowLiabilitiesFirst = false;
      
        public bool ShowPercentage = false;

        public bool IncludeZeroValue = false;

        public bool IncludeRootGLGroup = true;
        public bool IncludeItemClass = false;

        public bool InsertBlankBetweenGLClass = false;

        public string Quotation_No = String.Empty;

         

        //public bool IncludeGroupHierarchy = false;
        public int MaxHierarchyLevel = -1;


        public int DisplayBalanceLevel = 2;


        public string FullNameSeperator = "-";
        public clsPrmInventory()
        {

        }

        //public clsPrmPmis(int companyID, int accYearID, int glGroupID, int glAccountID, DateTime? fromDate, DateTime? toDate, InculdeOpBalanceEnum includeOpBalanceType, IncludePostEnum includePostType)
        //{
        //    CompanyID = companyID;
        //    GLGroupID = glGroupID;
        //    AccYearID = accYearID;
        //    GLAccountID = glAccountID;

        //    FromDate = fromDate;
        //    ToDate = toDate;
        //}


        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public int ItemTypeId { get; set; }

        public int ItemClassId { get; set; }

        public int ItemGroupId { get; set; }
        public int ItemCategoryID { get; set; }
        public int ItemColorID { get; set; }
        public int ItemSizeID { get; set; }
        public int ItemBrandID { get; set; }

        public string igr_no { get; set; }
        public string report_category { get; set; }

        public string ReportType { get; set; }

        public int Report_Percent { get; set; }

        public int To_Dept_Id { get; set; }

        public int From_Dept_Id { get; set; }

        public string From_Dept_Name { get; set; }

        public string To_Dept_Name { get; set; }

        public int From_MONTH { get; set; }
        public int From_YEAR { get; set; }
        public int TO_MONTH { get; set; }
        public int TO_YEAR { get; set; }

        public string mr_no { get; set; }

        public string Shift_name { get; set; }

        public string Shift_Id { get; set; }

        public string INDT_NO { get; set; }

        public string IsStoreAble { get; set; }

        public string Include_Safety_Stock { get; set; }

        public string REF_TRANS_NO { get; set; }

        public string Loading_Type { get; set; }
        public int StockTransferID { get; set; }

        public string Plate_Type { get; set; }
         public string ProcessType { get; set; }
         
         public string is_Finished { get; set; }
         public string autho_status { get; set; }

         public Int64 Cutting_Mst_Id { get; set; }

         public int MachineId { get; set; }
         public Int64 Dross_Dtl_id { get; set; }


         public Int64 Dross_MST_ID { get; set; }

         public string autho_statustxt { get; set; }

         public int usedItemId { get; set; }
         public decimal qty { get; set; }

         public string StockLevel { get; set; }

         public string ITC_IRR_STATUS { get; set; }
         public int REQ_STATUS { get; set; }
         public int INDT_STATUS { get; set; }
         public string RejectType { get; set; }
         public string isElectrolyte { get; set; }
        // public string isElectrolyte { get; set; }

         public string auth_by = string.Empty;
         public string auth_by_id = string.Empty;
         public int convertid { get; set; }
         public string reorder_level { get; set; }
         public string QC_NO { get; set; }
         public int QC_TYPE_ID { get; set; }
         public string PURCHASE_NO { get; set; }
         public int PURCHASE_TYPE_ID { get; set; }
         public string LC_NO { get; set; }
         public string COSTING_NO { get; set; }
         public int StorageLocationId { get; set; }
         public string IsOperator { get; set; }
         public string FormStatus { get; set; }
         public string IsDirectIssue { get; set; }

    }
}
