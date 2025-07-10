using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG.Report.ReportEnums
{
    public enum ReportIDEnum
    {
        None = 0,
        GL_TrialBalance = 1501,
        GL_IncomeStatement = 1502,
        GL_BalanceSheet = 1503,

        GL_ReceiptPayment = 1504,
        GL_CashFlowStatement = 1505,


        GL_ChartOfAccounts = 1520,


        GL_Ledger = 1521,
        GL_LedgerSummary = 1522,

        GL_JournalList = 1530,
        GL_JournalBook = 1531,
        GL_Journal = 1532,

        GL_CashSummary = 1540,
        GL_CashJournalList = 1541,
        GL_CashJournalBook = 1542,

        GL_CostCenterSummary = 1551,
        GL_ReferenceSummary = 1552,
        GL_TranCodeSummary = 1553,

        Money_Receive=1554,
        Customer_Adjustment_Report=1555,

        GL_CostCenterDetails = 1561,
        GL_ReferenceDetails = 1562,
        GL_TranCodeDetails = 1563,
        ReOrderLebelPreview_Search = 1700,
        ReSafetyLebelPreview_Search = 1701,
        ReLeadTimePreview_Search = 1702,
        SalesRequisitionPreview_Search = 1705,
        RowwiseSalesRequisitionPreview_Search = 1706,
        InvoiceBatteryPreview_Search = 1711,
        InvoiceIPSPreview_Search = 1712,
        InvoiceIPSAndBatteryPreview_Search = 1713,
        Invoice_Search = 1714,
        GatePassPreview_Search = 1715,
        GatePassItemDetailsPreview_Search = 1716,
        DeliveryChallanItemDetailsPreview_Search = 1717,
        MaterialReceiveItemDetailsPreview_Search = 1718,
        InternalReceiveItemDetailsPreview_Search = 1719,
        ItemStockforLocalitemtype_Search = 1720,
        ItemStockforForeignitemtype_Search = 1721,
        ItemStockforLacalandForeignitemtype_Search = 1722,
        DeliveryChallanSupplierReturn = 1723,
        GatePassSupplierReturn = 1724,
        ItemReport = 1730,
        IGRByFloorPreview_Search = 1731,
        ITCByStorePreview_Search = 1732,
        IRRByStorePreview_Search = 1733,
        DirectITCIRRPreview_Search = 1734,
        OutSalePreview = 1735,
        ForeignPurchasePreview = 1736,
        LocalPurchasePreview = 1737,
        MRRPreview = 1738,
        ITCtoStore = 1739,       
        IRRByStore=1750,
        IRRByStoreExcelExport = 1758,
        Material_Stock_Monitor=1751,
        Sales_Return_Report = 1755,
        Refundable_Repair_Report = 1756,
        Department_Production_Report=2001,
        Formation_Production_Report=2002,
        Lead_Consumption_Summary=2004,
        Lead_Consumption_Details=2005,
        Production_Summary_Report=2006,
        Battery_Production=3001,
        Monthly_ITEM_Production = 3002,
        Monthly_CAT_Production = 3003,

        MaterialStock=1601,
        MaterialStockPrice=1602,
        IRRByFloorReport = 1603,
        PurchaseRequisitionReport = 1604,
        QcPassReport=1605,
        LCCostingReport=1606,
    }
}
