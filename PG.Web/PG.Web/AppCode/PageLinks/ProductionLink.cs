using PG.Core.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PG.Web.PageLinks
{
    public class ProductionLink
    {


        public static string GetLink_User_List
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/Production/GetUserList.ashx"); }
        }

        public static string GetLink_ProductionItemList
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/Production/GetLink_ProducitonItemList.ashx"); }
        }

        public static string GetLink_FormationItemList
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/Production/GetItemListForReformEntry.ashx"); }
        }


        public static string GetLink_PackageList
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/Production/GetPackageList.ashx"); }
        }

        public static string GetLink_BOMItemList
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/Production/GetLink_BOMItemList.ashx"); }
        }

        public static string GetLink_ItemStock_SDateList
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/Production/GetLink_ItemStock_SDateList.ashx"); }
        }

        public static string GetLinkPackingItemListServiceLink
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/Production/GetLinkPackingItemListServiceLink.ashx"); }
        }

        public static string GetLink_BOMList
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/Production/GetLink_BOMList.ashx"); }
        }


        public static string GetLink_UOMList
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/Production/GetLink_UOMList.ashx"); }
        }

        public static string GetLink_SHIFTList
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/Production/GetLink_SHIFTList.ashx"); }
        }

        public static string GetLink_MACHINEList
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/Production/GetLink_MACHINEList.ashx"); }
        }

        public static string GetLink_SuppervisorList
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/Production/GetLink_SuppervisorList.ashx"); }
        }


        public static string GetLink_BOMWiseItemList
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/Production/GetLink_BOMWiseItemList.ashx"); }
        }

        public static string GetLink_Deptwise_ItemList
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/Production/GetDeptwiseItemList.ashx"); }
        }

        public static string GetLink_Deptwise_ItemList_v1
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/Production/GetDeptwiseItemList_v1.ashx"); }
        }

        public static string GetLink_Deptwise_Reject_ItemList
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/Production/GetDeptwiseRejectItemList_v1.ashx"); }
        }

        public static string GetLink_Dept_List
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/Production/GetDepartmentList.ashx"); }
        }

        public static string Get_LinkBatch_WiseItem_List
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/Production/GetLinkBatchWiseItemList.ashx"); }
        }

        public static string Get_LinkBatch_WiseItem_for_Rejection_List
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/Production/GetLinkBatchWiseItemListForRejectionDeclare_v1.ashx"); }
        }

         public static string Get_LinkBatchNo_ProductionDate_List
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/Production/GetProductionDateWiseBatchNo.ashx"); }
        }

         public static string Get_LinkStockTransferableItem_List
         {
             get { return WebUtility.GetAbsoluteUrl("~/Service/Production/GetLinkStockTransferableItemList.ashx"); }
         }

         public static string Get_LinkProductionNo_List
         {
             get { return WebUtility.GetAbsoluteUrl("~/Service/Production/GetProductionNoList.ashx"); }
         }

         public static string Get_LinkGetStorageLocationList
         {
             get { return WebUtility.GetAbsoluteUrl("~/Service/Production/GetStorageLocationList.ashx"); }
         }

         public static string GetLink_AssemblyMachineListInfo
         {
             get { return WebUtility.GetAbsoluteUrl("~/Service/Production/GetLink_AssemblyMachineList.ashx"); }
         }

         public static string GetLink_RejectItemList
         {
             get { return WebUtility.GetAbsoluteUrl("~/Service/Production/GetLink_RejectItemList.ashx"); }
         }

         public static string GetLink_BatteryCategoryList
         {
             get { return WebUtility.GetAbsoluteUrl("~/Service/Production/GetLink_BatteryCategoryList.ashx"); }
         }
        
    }
}