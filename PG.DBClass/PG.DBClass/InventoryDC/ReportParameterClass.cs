using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{
     [Serializable]
    public class ReportParameterClass : ICloneable
    {
        public string ItemCode { get; set; }
        public string CategoryId { get; set; }
        public string SubCategoryId { get; set; }
        public string Level { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string FromDatef { get; set; }
        public string ToDatet { get; set; }
        public string SectionCode { get; set; }
        public string IssueNo { get; set; }
        public string RequisitionCondition { get; set; }
        public string RequisitionIndentCondition { get; set; }
        public string DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int ItemGroupId { get; set; }
        public string GroupName { get; set; }
        public string ItemName { get; set; }
        public string IndentNo{ get; set; }

        public string CustomerId { get; set; }
        public string LocationId { get; set; }
        public string DealerId { get; set; }
        public string InvoiceNo { get; set; }
        public string SalesInvoiceNo { get; set; }
        public string LocationCode { get; set; }
        public string InvoiceType { get; set; }
        public string GDC_CHALLAN_NO { get; set; }
        public string SL_No { get; set; }
        public string GC_NO { get; set; }
        public string RTN_NO { get; set; }
        public string AUTH_STATUS { get; set; }
        public string DC_NO { get; set; }
        public string ClaimType { get; set; }
        public string ChallanNo { get; set; }
        public string SalesFromDatef { get; set; }
        public string SalesToDatet { get; set; }
        public string ReceiveFromDatef { get; set; }
        public string ReceiveToDatet { get; set; }
        public string GatePassNo { get; set; }
        public string DeliveryChallanNo { get; set; }
        public string MaterialReceiveNo { get; set; }
        public string InternalReceiveNo { get; set; }
        public string Item { get; set; }
        public int? ItemId { get; set; }
        public string StoreId { get; set; }
        public int? ItemTypeId { get; set; }
        public string IRRNo { get; set; }
        public string IGRNo { get; set; }
        public string LCNo { get; set; }
        public int LoginUserId { get; set; }

        public string UserName { get; set; }
        public string PurchaseNo { get; set; }
        public string SupplierId { get; set; }
        public string SupplierName { get; set; }
        public Int64 REQ_ISSUE_ID { get; set; }
        public Int64 ISSUE_RECEIVE_ID { get; set; }
        public int ItemSNSId { get; set; }
        public Int64 REQ_ID { get; set; }
        public object Clone()
        {
            return this.MemberwiseClone();
        }



        public string ITC_NO { get; set; }
        public string MRR_NO { get; set; }

        public int MRR_TYPE { get; set; }



        public int Report_Percent { get; set; }

        public int LC_ID { get; set; }

        public string ReportType { get; set; }

        public int SNS_Type { get; set; }
        public string SNS_Type_Name { get; set; }

        public int deptId { get; set; }
        public string ITC_IRR_STATUS { get; set; }
        public int INTERNAL_TRANS_TYPE_ID { get; set; }
        public int sup_id { get; set; }

        public string Quotation_No { get; set; }
         
    }
}
