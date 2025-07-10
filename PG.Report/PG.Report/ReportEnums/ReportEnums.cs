using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG.Report.ReportEnums
{

    public enum ReportOpenTypeEnum
    {
        Preview = 0,
        Print = 1,
        Export = 2,
    }

    public enum ReportExportTypeEnum
    {
        PDF = 0,
        Excel = 1,
        WORD = 2,
    }

    public enum ReportViewModeEnum
    {
        InThisTab = 0,
        InNewTab = 1,
        InNewWindow = 2,
        InDialog = 4,
    }


    public enum GLReportTypeEnum
    {
        Condensed = 1,
        Standard = 2,
        Detials = 3,
        Ledger = 4,
        LedgerDetails = 5,
    }

    public enum GLReportItemTypeEnum
    {
        Blank = 0,

        Class = 1,
        Group = 2,
        Ledger = 3,
        LedgerControl = 4,
        LedgerSub = 5,

        LedgerControlSum = 6,


        AccRefType = 11,
        AccRefCategory = 12,
        AccRef = 13,

        Header = 20,
        SubTotal = 21,
        GrandTotal = 22,
        Description = 23,


        PLItem = 80,
        PLClosing = 81,

        System = 99,

    }

    public enum GLReportTotalSumModeEnum
    {
        ShowALL = 0,
        SubTotalByGroup = 1,
        SubTotalTopGroup = 2,

    }

    public enum GLReportOpeningBalanceEnum
    {
        None = 0,
        ShowALL = 1,
        ShowIndvidual = 2,
        ShowYear = 3,
        ShowDateRange = 4,
    }


    //public enum GLReportItemAmountEnum
    //{
    //    All = 0,

    //    OpeningDebit = 1,
    //    OpeningCredit = 2,
    //    OpeningBalance = 3,

    //    TranDebit = 4,
    //    TranCredit = 5,
    //    TranBalance = 6,

    //    ClosingDebit = 7,
    //    ClosingCredit = 8,
    //    ClosingBalance = 9,
    //}


    public enum GLReportItemTranTypeEnum
    {
        Tran = 1,
        SubAcc = 2,
        Contra = 3,
        Instrument = 4,
        CostCenter = 5,
        Reference = 6,
        

        System = 7,
        GroupFirst = 8,
        GroupLast = 9,
        GroupHeader = 10,
        GroupFooter = 11,

    }



}
