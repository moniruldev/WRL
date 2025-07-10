using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG.DBClass.AccountingDC.AccEnums
{
    public enum CompanyTypeEnum
    {
        GroupCompany = 1,
        Company = 2,
        NonProfitOrganiztion = 3,
    }

    public enum DebitCreditEnum
    {
        Debit = 0,
        Credit = 1,
    }


    public enum AccSystemEnum
    {
        Defaulft = 1,
        ProvidentFund = 2,
    }


    public enum GLClassEnum
    {
        Assets = 1,
        Liabilities = 2,
        Income = 3,
        Expense = 4,
        ProfitAndLoss = 5,
    }

    public enum GLGroupClassEnum
    {
        //Assets
        CurrentAssets = 1100,
            CashInHand = 1110,
            BankAccounts = 1115,
            SecuritiesDeposits_Assets = 1120,
            LoansAndAdavnces_Assets = 1125,
            StockInHand = 1130,
            AccountsReceivable = 1135,  //accounts Receivable, Sundry Debtors

        FixedAssets = 1200,
        Investments = 1300,

        //Liabilities
        CapitalAccounts = 2100,
            ReservesAndSurplus = 2110,
        CurrentLiabilities = 2200,
            DutiesAndTaxes = 2210,
            ExpensesPayable = 2220,
            AccountsPayable = 2230, //accounts Payable, SundryCreditors_AP
        Loans_Liability = 2300,
            BankODAccount = 2310,
            SecuredLoans = 2320,
            UnsecuredLoans = 2330,
        SuspenseAccounts = 2400,


        //Incomes
        SalesAccounts = 3100,
        DirectIncomes = 3200,
        IndirectIncomes = 3300,
       

        //Expenses
        CostOfSales = 4100,  //Cost of sales, purchases
        DirectExpenses = 4200,
        IndirectExpenses = 4300,
        

        //Profit And Loss
        ProfitAndLoss = 5100,
    }


    public enum PostOptionEnum
    {
        ALL = 0,
        Posted = 1,
        UnPosted = 2,
    }


    public enum GLAccountTypeEnum
    {
        NormalAccount = 1,
        ControlAccount = 2,
        SubAccount = 3,
    }

    public enum GLAccountTypeFilterEnum
    {
        NoFilter = 0,
        NormalAccount = 1,
        ControlAccount = 2,
        SubAccount = 3,
        NormalSubAccount = 4,
        NormalControlAccount = 5,
        ControlSubAccount = 6,
        SubAccountByControl = 7,
        AllAccount = 8,
    }


    public enum JournalTypeEnum
    {
        GeneralJournal = 1,
        ReceiptJournal = 2,
        PaymentJournal = 3,
        ContraJournal = 4,
        DebitNote = 5,
        CreditNote = 6,
    }


    public enum AccRefTypeEnum
    {
        TranCode = 1,
        CostCenter = 2,
        Reference = 3,

    }


    public enum InstrumentModeEnum
    {
        Issue = 1,
        Receive = 2,
    }

    public enum InstrumentTypeEnum
    {
        Cash = 1,
        Cheque = 2,
        DemandDraft = 3,
        PayOrder = 4,
        CreditCard = 5,
        BankTransfer = 6,
        ElectronicPayment = 7,
        Other = 8,
    }

    public enum InstrumentStatusEnum
    {
        NA = 1,
        NotCleared = 2,
        Cleared = 3,
        Void = 4,
    }

    public enum InstrumentLinkTypeEnum
    {
        Actual = 1,
        Reverse = 2,
        Correction = 3,
    }


    public enum GLTranTypeEnum
    {
        GLTran = 1,

        Diposit = 21,
        Withdraw = 22,

        PFEmp = 510,
        PFComp = 511,

        PFEmpInt = 512,
        PFCompInt = 513,

        PFForfeiture = 514,
        PFPayment = 515,

        PFProfit = 516,

        PFLoan = 517,
        PFLoanRealise = 518,

        PFForfeitrueIncome = 519,

        PFTrans = 520,
    }

    public enum AccSLNoEnum
    {
        GeneralJournal = 1,

    }


    public enum JournalSourceTypeEnum
    {
        GeneralJournal = 1,


        PF_UnauditInterest = 300,
        PF_Forfeitrue = 301,
        PF_Payment = 302,
        PF_ForfeitrueIncome = 303,
        PF_Profit = 304,
    }


    public enum AccBalanceShowTypeEnum
    {
        Default = 1,
        DebitCredit = 2,
        DebitCreditBracket = 3,
    }



    public enum AccOrderByEnum
    {
        SLNo = 1,
        Code = 2,
        Name = 3,

        BalanceAsc = 4,
        BalanceDesc = 5,

        DateAsc = 6,
        DateDesc = 7,

        CountAsc = 8,
        CountDesc = 9,

    }


    public enum ReceviePaymentModeEnum
    {
        ReceivePayment = 1,
        Accrual = 2,
    }


    public enum InculdeOpBalanceEnum
    {
        None = 0,
        IncludeALL = 1,
        IncludeYear = 2,
        IncludeDateRange = 3,
        IncludeALLIndvidual = 4,
    }

    public enum IncludePostEnum
    {
        All = 0,
        Posted = 1,
        Unposted = 2,
    }

    public enum GroupsLedgerShowEnum
    {
        Groups = 1,
        Ledgers = 2,
        GroupsAndLedger = 3,
    }


    public enum CashTranOption
    {
        CashTran = 1,
        CashTranContra = 2,
    }


    public enum ItemAmountTypeEnum
    {
        CheckAny = 0,

        OpeningDebit = 1,
        OpeningCredit = 2,
        OpeningBalance = 3,

        TranDebit = 4,
        TranCredit = 5,
        TranBalance = 6,

        ClosingDebit = 7,
        ClosingCredit = 8,
        ClosingBalance = 9,

    }


    public enum AmountShowTypeEnum
    {
        None = 0,

        ClosingBalance = 1,
        OpeningTransClosing = 2,
      

    }

    public enum JournalReportFormatEnum
    {
        Default = 1,
        SingleVoucher = 2,
        DebitCreditJ = 3,

    }
}
