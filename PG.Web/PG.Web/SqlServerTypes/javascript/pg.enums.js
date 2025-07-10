/* File Created: April 30, 2013 */

//PG.enums

//Version: 1.0.3
//Date: April 30, 2014


function Enums() {
    //There is no sence to instansiate enum
    throw Error.notImplemented();
}

Enums.LinkType = { FromMenu: 1, Direct: 2 }
Enums.TabAction = { None: 0, InTab: 1, InNewTab: 2, InTabReuse: 3, InTabReuseByParam: 4 }
Enums.PageMode = { None: 0, InTab: 1, InDialog: 2, InWindow: 3, InTabDialog: 4 }
Enums.EditMode = { None: 0, Read: 1, Add: 2, Edit: 3, Delete: 4, List: 5 }
Enums.RecordState = {Read : 0, Added: 1, Edited: 2, Deleted : 3 }


Enums.Permission = {
    None: 0, Read: 1, Add: 2, Edit: 4, Delete: 8
                    , Execute: 16, Enabled: 32, Visible: 64, Full: 127
}

Enums.DataCompareType = {
    EqualTo: 1, NotEqualTo: 2, GreaterThan: 3, LessThan: 4
        , GreaterThanEqualTo: 5, LessThanEqualTo: 6
        , Range: 7, IN: 8
        , Contains: 9, StartsWith: 10, EndsWith: 11
}


Enums.Keys = {
    backspace: 8, tab: 9, enter: 13, escape: 27
        , space: 32, end: 35, home: 36
        , left: 37, right: 39, up: 38, down: 40
        , insert: 45, del: 46
        , multiply: 106, plus: 107, minus: 108, devide: 111
}

Enums.AccRefType = { TranCode: 1, CostCenter: 2, Reference: 3 }
Enums.GLClass = { Assest: 1, Liabilities: 2, Income: 3, Expense: 4, ProfitAndLoss: 5 }

Enums.GLGroupClass = { None: 0
                        //Assets
                        , CurrentAssets : 1100
                            , CashInHand : 1110
                            , BankAccounts : 1115
                            , SecuritiesDeposits_Assets : 1120
                            , LoansAndAdavnces_Assets : 1125
                            , StockInHand : 1130
                            , SundryDebtors_AR : 1135  //accounts Receivable
                        , FixedAssets : 1200
                        , Investments : 1300

                        //Liabilites
                        , CapitalAccounts : 2100
                        , ReservesAndSurplus : 2110
                        , CurrentLiabilities : 2200
                            , DutiesAndTaxes : 2210
                            , ExpensesPayable : 2220
                            , SundryCreditors_AP : 2230 //accounts Payable
                        , Loans_Liability : 2300
                            , BankODAccount : 2310
                            , SecuredLoans : 2320
                            , UnsecuredLoans : 2330
                        , SuspenseAccounts : 2400

                        //Income
                        , DirectIncomes : 3100
                        , IndirectIncomes : 3200
                        , SalesAccounts : 3300
 
                        //Expense
                        , DirectExpenses : 4100
                        , IndirectExpenses : 4200
                        , PurchaseAccounts : 4300

                        //ProfitAndLoss
                        , ProfitAndLoss : 5100

}

Enums.GLAccountTypeFilter = { NoFilter: 0
                              ,  NormalAccount : 1, ControlAccount : 2, SubAccount : 3
                              , NormalSubAccount : 4,  NormalControlAccount : 5, ControlSubAccount : 6
                              , SubAccountByControl: 7, AllAccount: 8 
}

Enums.JournalType = { GeneralJournal: 1, ReceiptJournal: 2, PaymentJournal: 3
                      , ContraJournal: 4 , DebitNote : 5, CreditNote: 6
}

Enums.DrCr = { Debit: 0, Credit: 1 }

Enums.InstrumentMode = { Issue: 1, Receive: 2 }
Enums.InstruemntType = { Cash: 1, Cheque: 2, DemandDraft: 3
                        , PayOrder: 4, CreditCard: 5, BankTransfer: 6
                        , ElectronicPayment: 7, Other: 8
}

Enums.InstrumentLinkType = { Actual: 1, Reverse: 2, Correction: 3 }


//Enums.FileAccess = {ReadOnly : 1, Write :2, NoAccess :3}