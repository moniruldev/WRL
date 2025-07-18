using PG.Core.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PG.Web.PageLinks
{
    public class InventoryLink
    {
        public static string GetLink_CustomerList
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/Inventory/GetCustomerList.ashx"); }
        }

        public static string GetLink_CompanyList
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/Inventory/GetCompanyList.ashx"); }
        }

        public static string GetLink_ItemTypeList
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/Inventory/GetItemTypeList.ashx"); }
        }


        public static string GetLink_ItemClassList
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/Inventory/GetItemClassList.ashx"); }
        }

        public static string GetLink_ItemGroupList
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/Inventory/GetItemGroupList.ashx"); }
        }

        public static string GetLink_BrandList
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/Inventory/GetBrandList.ashx"); }
        }
        public static string GetLink_Deptwise_ItemList
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/Production/GetIdeptwisetemList.ashx"); }
        }

        public static string GetLink_ItemList
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/Inventory/GetItemList.ashx"); }
        }
        public static string GetLink_MenuList
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/Inventory/GetMenuList.ashx"); }
        }
        public static string GetLink_Indent_ItemList
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/Inventory/ItemListFromIndent.ashx"); }
        }


        public static string GetLink_InvoiceList
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/Inventory/GetInvoiceList.ashx"); }
        }

        public static string GetLink_SupplierList
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/Inventory/GetSupplierList.ashx"); }
        }

        public static string GetLink_QcRefList
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/Inventory/GetQcRefList.ashx"); }
        }

        public static string GetLink_CNFList
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/Inventory/GetCNFAgent.ashx"); }
        }

        public static string GetLink_INSCOMList
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/Inventory/GetInsuruanceCompanyList.ashx"); }
        }

        public static string GetLink_Manufactist
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/Inventory/GetManufactureList.ashx"); }
        }




        public static string GetLink_LcList
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/Inventory/GetLCListForInvoice.ashx"); }
        }

        public static string GetLink_LC_List
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/Inventory/GetLcList.ashx"); }
        }
        
        public static string GetLink_RepairBatteryItemList
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/Inventory/GetRepairBattery_ITEM.ashx"); }
        }
        public static string GetLink_ItemList_ByLC
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/Inventory/GetItemListByLc.ashx"); }
        }
        

        public static string GetLink_Repair_ItemList
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/Inventory/GetRepairItemService.ashx"); }
        }

        public static string GetApproval_EmpList
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/Inventory/GetApprovalEmpList.ashx"); }
        }

        public static string GetLinkAllEmpList
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/Inventory/GetAllEmpList.ashx"); }
        }

        public static string GetLink_BankList
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/Inventory/GetBankList.ashx"); }
        }

        public static string GetLink_Bank_BranchList
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/Inventory/GetBranch.ashx"); }
        }


        public static string GetLink_Account_HolderList
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/Inventory/GetAccountHolderList.ashx"); }
        }

        public static string GetLink_GetImpPOList
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/Inventory/GetImpPOList.ashx"); }
        }

        public static string GetLink_GetItemListbyIMPPurchaseID
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/Inventory/GetItemListbyIMPPurchaseID.ashx"); }
        }

        public static string GetLink_GetStoreRejectItemList
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/Inventory/GetStoreRejectItemList.ashx"); }
        }

        public static string GetLink_CountryList
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/HMS/GetCountryList.ashx"); }
        }

        public static string GetLink_DistrictList
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/WREL/GetDistrictList.ashx"); }
        }
        public static string GetLink_TownList
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/WREL/GetTownList.ashx"); }
        }
        public static string GetLink_RouteList
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/WREL/GetRouteList.ashx"); }
        }

         public static string GetLink_CNMasterList
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/WREL/GetCNMasterList.ashx"); }
        }

         public static string GetLink_ClientList
         {
             get { return WebUtility.GetAbsoluteUrl("~/Service/WREL/GetClientMstList.ashx"); }
         }
        

        
        
    }

}