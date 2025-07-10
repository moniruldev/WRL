using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PG.Core.DBValidation;
using PG.DBClass.AccountingDC.GeneralLedgerDC;
using PG.DBClass.AccountingDC.AccEnums;
using PG.Core.DBBase;
using PG.DBClass.AccountingDC;
using PG.Core;

namespace PG.BLLibrary.AccountingBL.GeneralLedgerBL
{
    public class JournalValidtationTypeSet
    {
        public const int Journal = 1001;
        public const int JournalMain = 1002;
        public const int JournalDet = 1005;
       
        public const int JournalDetRef = 1010;

        
        public const int JournalDetIns = 1015;
    }

    public class JournalValidationTask : DBValidationTask
    {
        public FormDataMode EditMode = FormDataMode.None;
        public dcJournal Journal = null;
        public List<dcAccRefSettings> AccRefSettingsList = null;
        public dcInstrumentSettings InstrumentSettings = null;
        
        public AccRefTypeEnum AccRefType = AccRefTypeEnum.Reference;
        public dcAccYear AccYear = null;
        public dcJournalType JournalType = null;

        public dcJournalDet JournalDet_Current = null;
        public List<dcGLAccount> GLAccountList = null;

        public bool UseDetIDLink = false;

        public JournalValidationResult CurResult = new JournalValidationResult();
    }

    public class JournalValidationResult : DBValidationResult
    {
        public int JournalID = 0;
        public int JournalDetID = 0;
        public int JournalDetID_Link = 0;
        public int JournalDetLineNo = 0;

        //public int ContinueOnError = 0;


        //public override JournalValidationResult RetunWithCheckDC(ref DBContext dc, bool isDCInit)
        //{
        //    return (JournalValidationResult)base.RetunWithCheckDC(ref dc, isDCInit);
        //}
    }

    public class JournalValidationBL
    {

        public static List<dcGLAccount> LoadGLAccountList(JournalValidationTask pJTask, DBContext dc)
        {
            List<int> pList = pJTask.Journal.JournalDetList.Select(c => c.GLAccountID).ToList();
            return GLAccountBL.GetGLAccountListByIDList(pList, dc);
        }


        public static bool IsJournalYearValid(int pCompanyID, int pAccYearID)
        {
            return IsJournalYearValid(pCompanyID, pAccYearID, null, null);
        }
        public static bool IsJournalYearValid(int pCompanyID, int pAccYearID, DBContext dc)
        {
            return IsJournalYearValid(pCompanyID, pAccYearID, null, dc);
        }
        public static bool IsJournalYearValid(int pCompanyID, int pAccYearID, dcAccYear pAccYear)
        {
            return IsJournalYearValid(pCompanyID, pAccYearID, pAccYear, null);
        }
        public static bool IsJournalYearValid(int pCompanyID, int pAccYearID, dcAccYear pAccYear, DBContext dc)
        {

            bool isValid = false;
            if (pAccYear == null)
            {
                pAccYear = AccYearBL.GetAccYearByID(pCompanyID, pAccYearID, dc);
            }
            if (pAccYear != null)
            {
                if (pAccYear.CompanyID != pCompanyID || pAccYear.AccYearID != pAccYearID)
                {
                    isValid = false;
                }
                else
                {
                    if (!pAccYear.IsClosed)
                    {
                        isValid = true;
                    }
                }
            }
            return isValid;
        }
        public static bool IsJournalDateValid(int pCompanyID, int pAccYearID, DateTime pJournalDate)
        {
            return IsJournalDateValid(pCompanyID, pAccYearID, pJournalDate, null, null);
        }
        public static bool IsJournalDateValid(int pCompanyID, int pAccYearID, DateTime pJournalDate, DBContext dc)
        {
            return IsJournalDateValid(pCompanyID, pAccYearID, pJournalDate, null, dc);
        }
        public static bool IsJournalDateValid(int pCompanyID, int pAccYearID, DateTime pJournalDate, dcAccYear pAccYear)
        {
            return IsJournalDateValid(pCompanyID, pAccYearID, pJournalDate, pAccYear, null);
        }
        public static bool IsJournalDateValid(int pCompanyID, int pAccYearID, DateTime pJournalDate, dcAccYear pAccYear, DBContext dc)
        {
            bool isValid = false;
            if (pAccYear == null)
            {
                pAccYear = AccYearBL.GetAccYearByID(pCompanyID, pAccYearID, dc);
            }
            if (pAccYear != null)
            {
                if (pAccYear.CompanyID != pCompanyID || pAccYear.AccYearID != pAccYearID)
                {
                    isValid = false;
                }
                else
                {
                    if (pJournalDate >= pAccYear.YearStartDate && pJournalDate <= pAccYear.YearEndDate)
                    {
                        isValid = true;
                    }
                }

            }
            return isValid;
        }


        public static JournalValidationResult ValidateJournalFull(JournalValidationTask pJTask)
        {
            return ValidateJournalFull(pJTask, null);
        }

        public static JournalValidationResult ValidateJournalFull(JournalValidationTask pJTask, DBContext dc)
        {
            JournalValidationResult vResult = new JournalValidationResult();
            vResult.ValidationTask = pJTask;
            vResult = JournalValidationBL.ValidateJournalMain(pJTask,dc);
            pJTask.CurResult = vResult;

            //vResult = JournalValidationBL.ValidateJournalDetList(pJTask, dc);

            if (pJTask.GLAccountList == null || pJTask.GLAccountList.Count == 0)
            {
                pJTask.GLAccountList = LoadGLAccountList(pJTask, dc);
            } 


            if (!vResult.IsError || vResult.StopContinue || !pJTask.ValidateALL)
            {
                vResult = ValidateJournalDetList(pJTask, dc);
                if (!vResult.IsError || vResult.StopContinue || !pJTask.ValidateALL)
                {
                    vResult = ValidateJournalDetRef(pJTask, dc);
                }
                if (!vResult.IsError || vResult.StopContinue || !pJTask.ValidateALL)
                {
                    vResult = ValidateJournalDetInsList(pJTask, dc);
                }
            }
            return vResult;
        }

        public static JournalValidationResult ValidateJournalMain(JournalValidationTask pJTask)
        {
            return ValidateJournalMain(pJTask, null);
        }
        public static JournalValidationResult ValidateJournalMain(JournalValidationTask pJTask, DBContext dc)
        {
            JournalValidationResult vResult = new JournalValidationResult();
            vResult.ValidationTask = pJTask;

            bool isDCInit = false;

            if (pJTask.Journal.JournalTypeID == 0)
            {
                if (vResult.AddErrorCheckReturnDC("Journal Type Not Assingned!", ref dc, isDCInit))
                {
                    return vResult;
                }
            }

            if (pJTask.Journal.CompanyID == 0)
            {
                if (vResult.AddErrorCheckReturnDC("Company Not Assingned!", ref dc, isDCInit))
                {
                    return vResult;
                }
            }

            if (pJTask.Journal.AccYearID == 0)
            {
                if (vResult.AddErrorCheckReturnDC("Journal Year Not Assingned!", ref dc, isDCInit))
                {
                    return vResult;
                }
            }

            //init connection if not openend
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

            if (pJTask.JournalType == null)
            {
                pJTask.JournalType = JournalTypeBL.GetJournalTypeByID(pJTask.Journal.CompanyID, pJTask.Journal.JournalTypeID, dc);
            }

            if (pJTask.AccYear == null)
            {
                pJTask.AccYear =  AccYearBL.GetAccYearByID(pJTask.Journal.CompanyID, pJTask.Journal.AccYearID, dc);
            }

            if ( pJTask.JournalType.AccSLNoID == 0)
            {
                if (pJTask.Journal.JournalNo.Trim() == string.Empty)
                {
                    if (vResult.AddErrorCheckReturnDC("Please Enter Journal No!", ref dc, isDCInit))
                    {
                        return vResult;
                    }
                }
            }

            if (IsJournalYearValid(pJTask.Journal.CompanyID, pJTask.Journal.AccYearID, pJTask.AccYear, dc) == false)
            {
                if (vResult.AddErrorCheckReturnDC("Journal Year Not Vald!", ref dc, isDCInit))
                {
                    return vResult;
                }
            }

            if (IsJournalDateValid(pJTask.Journal.CompanyID, pJTask.Journal.AccYearID, pJTask.Journal.JournalDate, pJTask.AccYear, dc) == false)
            {
                string errString = string.Format("Journal Date Not Vald!"); ;
                if (vResult.AddErrorCheckReturnDC(errString, ref dc, isDCInit))
                {
                    return vResult;
                }
            }

            if (pJTask.EditMode == FormDataMode.Add)
            {
                if (JournalBL.IsJournalNoExists(pJTask.Journal.CompanyID, pJTask.Journal.AccYearID, pJTask.Journal.JournalNo.Trim(), dc))
                {
                    string errString = string.Format("Journal No Already Exists!");
                    if (vResult.AddErrorCheckReturnDC(errString, ref dc, isDCInit))
                    {
                        return vResult;
                    }
                }
            }
            else
            {
                if (JournalBL.IsJournalNoExists(pJTask.Journal.CompanyID, pJTask.Journal.AccYearID, pJTask.Journal.JournalNo.Trim(), pJTask.Journal.JournalID, dc))
                {
                    vResult.StopContinue = true;
                    string errString = string.Format("Debit Credit Amount is not equal.");
                    if (vResult.AddErrorCheckReturnDC(errString, ref dc, isDCInit))
                    {
                        return vResult;
                    }

                   
                }
            }

            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return vResult;
        }

        public static JournalValidationResult ValidateJournalDetList(JournalValidationTask pJTask)
        {
            return ValidateJournalDetList(pJTask, null);
        }
        public static JournalValidationResult ValidateJournalDetList(JournalValidationTask pJTask, DBContext dc)
        {
            JournalValidationResult vResult = pJTask.CurResult;
            vResult.ValidationTask = pJTask;
            bool isDCInit = false;
            if (pJTask.Journal.JournalDetList.Where(c=>c._RecordState != RecordStateEnum.Deleted).Count() == 0)
            {
                vResult.StopContinue = true;
                if (vResult.AddErrorCheckReturnDC("No Transaction entry to save!", ref dc, isDCInit))
                {
                    return vResult;
                }
            }

            if (pJTask.Journal.JournalDetList.Where(c => c._RecordState != RecordStateEnum.Deleted).Sum(c => c.DebitAmt)
                != pJTask.Journal.JournalDetList.Where(c => c._RecordState != RecordStateEnum.Deleted).Sum(c => c.CreditAmt))
            {
                vResult.StopContinue = true;
                if (vResult.AddErrorCheckReturnDC("Debit Credit Amount is not equal.", ref dc, isDCInit))
                {
                    return vResult;
                }
            }

            if (pJTask.GLAccountList == null || pJTask.GLAccountList.Count ==0)
            {
                pJTask.GLAccountList = LoadGLAccountList(pJTask, dc);
            } 

            foreach (dcJournalDet jDet in pJTask.Journal.JournalDetList)
            {
                pJTask.JournalDet_Current = jDet;
                vResult = ValidateJournalDet(pJTask, dc);
            }

            return vResult;
        }

        public static JournalValidationResult ValidateJournalDet(JournalValidationTask pJTask)
        {
            return ValidateJournalDet(pJTask, null);
        }
        public static JournalValidationResult ValidateJournalDet(JournalValidationTask pJTask, DBContext dc)
        {
            JournalValidationResult vResult = pJTask.CurResult;
            vResult.ValidationTask = pJTask;
            dcJournalDet jDet = pJTask.JournalDet_Current;

            if (pJTask.GLAccountList == null | pJTask.GLAccountList.Count == 0)
            {
                pJTask.GLAccountList = LoadGLAccountList(pJTask, dc);
            }

            dcGLAccount glAccount = null;

            string strDebitCredit = jDet.DebitCredit == DebitCreditEnum.Debit ? "Debit" : "Credit";

            bool isDCInit = false;

            if (pJTask.JournalType == null)
            {
                pJTask.JournalType = JournalTypeBL.GetJournalTypeByID(pJTask.Journal.CompanyID, pJTask.Journal.JournalTypeID, dc);
            }


            if (jDet._RecordState == RecordStateEnum.Deleted)
            {
                //Delete task Check
            }
            else
            {
                if (jDet.GLAccountID == 0)
                {
                    string errString = string.Format("Plase Enter Account/Sub Ledger, For {1} Line No : {0}", jDet.JournalDetSLNo, strDebitCredit);
                    if (vResult.AddErrorCheckReturnDC(errString, ref dc, isDCInit))
                    {
                        return vResult;
                    }
                }

                if (jDet.GLAccountID > 0)
                {
                    if (GLAccountBL.IsGLAccountIDExists(pJTask.Journal.CompanyID, jDet.GLAccountID, dc) == false)
                    {
                        string errString = string.Format("Plase Enter Account/Sub Ledger, For {1} Line No : {0}", jDet.JournalDetSLNo, strDebitCredit);
                        if (vResult.AddErrorCheckReturnDC(errString, ref dc, isDCInit))
                        {
                            return vResult;
                        }
                    }
                }

                glAccount = pJTask.GLAccountList.Where(c => c.GLAccountID == jDet.GLAccountID).FirstOrDefault();
                if (glAccount == null)
                {
                    string errString = string.Format("Erron in Account/Sub Ledger, For {1} Line No : {0}", jDet.JournalDetSLNo, strDebitCredit);
                    if (vResult.AddErrorCheckReturnDC(errString, ref dc, isDCInit))
                    {
                        return vResult;
                    }
                }
                else
                {
                    if (glAccount.GLAccountTypeID == (int)GLAccountTypeEnum.ControlAccount)
                    {
                        string errString = string.Format("Control Account/Ledger is not allowed!, For {1} Line No : {0}", jDet.JournalDetSLNo, strDebitCredit);
                        if (vResult.AddErrorCheckReturnDC(errString, ref dc, isDCInit))
                        {
                            return vResult;
                        }
                    }
                }

                if (pJTask.JournalType.IsDetDesc)
                {
                    if (pJTask.JournalType.IsDetDescEmpty == false)
                    {
                        if (jDet.JournalDetDesc.Trim() == string.Empty)
                        {
                            string errString = string.Format("Plase Enter Narration, For {1} Line No : {0}", jDet.JournalDetSLNo, strDebitCredit);
                            if (vResult.AddErrorCheckReturnDC(errString, ref dc, isDCInit))
                            {
                                return vResult;
                            }
                        }
                    }
                }

            }
            return vResult;
        }


        public static JournalValidationResult ValidateJournalDetRef(JournalValidationTask pJTask)
        {
            return ValidateJournalDetRef(pJTask, null);
        }
        public static JournalValidationResult ValidateJournalDetRef(JournalValidationTask pJTask, DBContext dc)
        {
            JournalValidationResult vResult = pJTask.CurResult;
            vResult.ValidationTask = pJTask;

            if (pJTask.Journal == null)
            {
                vResult.IsError = true;
                vResult.ErrorString = string.Format("No Journal Data!");
                vResult.ValidationErrorList.Add(new DBValidationError(vResult.ErrorString));
                vResult.StopContinue = true;
                return vResult;
            }

            //fill common list to det list
            JournalDetBL.FillRefListToDetList(pJTask.Journal.JournalDetList, pJTask.Journal.JournalDetRefList,pJTask.UseDetIDLink);

            bool isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            if (pJTask.AccRefSettingsList == null)
            {
                pJTask.AccRefSettingsList = AccRefSettingsBL.GetAccRefSettingsList(pJTask.Journal.CompanyID, dc);
            }

            foreach (dcJournalDet jDet in pJTask.Journal.JournalDetList)
            {
                if (jDet.JournalDetRefList.Where(c => c.AccRefTypeID == (int)AccRefTypeEnum.TranCode
                                                                     && c._RecordState != RecordStateEnum.Deleted).Count() > 0)
                {
                    vResult = ValidateJournalDetRefByType(pJTask, jDet, AccRefTypeEnum.TranCode, dc);

                }

                if (!vResult.IsError || vResult.StopContinue || !pJTask.ValidateALL)
                {
                    if (jDet.JournalDetRefList.Where(c => c.AccRefTypeID == (int)AccRefTypeEnum.CostCenter
                                                     && c._RecordState != RecordStateEnum.Deleted).Count() > 0)
                    {
                        vResult = ValidateJournalDetRefByType(pJTask, jDet, AccRefTypeEnum.CostCenter, dc);

                    }
                }

                if (!vResult.IsError || vResult.StopContinue || !pJTask.ValidateALL)
                {
                    if (jDet.JournalDetRefList.Where(c => c.AccRefTypeID == (int)AccRefTypeEnum.Reference
                                                     && c._RecordState != RecordStateEnum.Deleted).Count() > 0)
                    {
                        vResult = ValidateJournalDetRefByType(pJTask, jDet, AccRefTypeEnum.Reference, dc);

                    }
                }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return vResult;
        }

        public static JournalValidationResult ValidateJournalDetRefByType(JournalValidationTask pJTask, dcJournalDet jDet, AccRefTypeEnum pAccRefType)
        {
            return ValidateJournalDetRefByType(pJTask, jDet, pAccRefType);
        }

        public static JournalValidationResult ValidateJournalDetRefByType(JournalValidationTask pJTask, dcJournalDet jDet, AccRefTypeEnum pAccRefType, DBContext dc)
        {
            JournalValidationResult vResult = pJTask.CurResult;
            vResult.ValidationTask = pJTask;

            string strDebitCredit = jDet.DebitCredit == DebitCreditEnum.Debit ? "Debit" : "Credit";
            if (jDet.GLAccountID == 0)
            {
                vResult.StopContinue = true;
                string errorString = string.Format("{1} Ledger Account not entered!, For Line No: {0}", jDet.JournalDetSLNo, strDebitCredit);
                if (vResult.AddErrorCheckReturn(errorString))
                {
                    return vResult;
                }
                //return vResult;
            }


            bool isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            string strRefType = AccRefTypeBL.GetTextFromEnum(pAccRefType);

            List<dcJournalDetRef> refListType = jDet.JournalDetRefList.Where(c => c.AccRefTypeID == (int)pAccRefType
                                                                     && c._RecordState != RecordStateEnum.Deleted).ToList();

            dcAccRefSettings accRefSettings_Type = pJTask.AccRefSettingsList.Where(c => c.AccRefTypeID == (int)pAccRefType).FirstOrDefault();

            List<dcGLAccountRefCategory> glAccRefCategoryList = GLAccountRefCategoryBL.GetAccountRefCategoryList(jDet.GLAccountID, (int)pAccRefType, dc);

             //group by category
            var rsListSumCategory = from r in refListType
                                    group r by r.AccRefCategoryID into grps
                                    select new
                                    {
                                        Key = grps.Key,
                                        DebitAmt = grps.Sum(c => c.DebitAmt),
                                        CreditAmt = grps.Sum(c => c.CreditAmt),
                                        TotRec = grps.Count(),
                                        GroupList = grps,
                                    };

            //Check GL Account Mandatory/non optional Category
            List<dcGLAccountRefCategory> accCatMandatory = glAccRefCategoryList.Where(c => c.IsMandatory == true).ToList();
            if (accCatMandatory.Count > 0)
            {
                bool isCatExists = false;
                string nonMatchedCat = string.Empty;
                foreach (dcGLAccountRefCategory accCat in accCatMandatory)
                {
                    // accCatMandatory.Where(c=>c.AccRefCategoryID == rsSumCategory.Key).Count()
                    var catList = rsListSumCategory.Where(c => c.Key == accCat.AccRefCategoryID);
                    if (catList.Count() > 0)
                    {
                        isCatExists = true;
                    }
                    else
                    {
                        isCatExists = false;
                        nonMatchedCat = accCat.AccRefCategoryName;
                        break;
                    }
                }
                if (isCatExists == false)
                {
                    string errorString = string.Format("Mandatory {2} Category [{3}] Not Entered, {1} Line No: {0}", jDet.JournalDetSLNo, strDebitCredit, strRefType, nonMatchedCat);
                    if (vResult.AddErrorCheckReturnDC(errorString, ref dc, isDCInit))
                    {
                        return vResult;
                    }
                }
            } //mandatory Categroy

            if (!accRefSettings_Type.AllowMultipleCategory)
            {
                if (rsListSumCategory.Count() > 1)
                {
                    string errorString = string.Format("Multiple {2} Category Not Allowed, {1} Line No: {0}", jDet.JournalDetSLNo, strDebitCredit, strRefType);
                    if (vResult.AddErrorCheckReturnDC(errorString, ref dc, isDCInit))
                    {
                        return vResult;
                    }
                }
            }



            if (refListType.Count > 0)
            {
                if (accRefSettings_Type.TotalSumCheckByCtategory)
                {
                    foreach (var rsSumCategory in rsListSumCategory)
                    {
                        string catName = rsSumCategory.GroupList.FirstOrDefault().AccRefCategoryName;
                        if (jDet.DebitCredit == DebitCreditEnum.Debit)
                        {
                            if (jDet.DebitAmt != rsSumCategory.DebitAmt)
                            {
                                string errorString = string.Format("{2} Category: [{1}] Debit Amount Not equal For Line No: {0}", jDet.JournalDetSLNo, catName, strRefType);
                                if (vResult.AddErrorCheckReturnDC(errorString, ref dc, isDCInit))
                                {
                                    return vResult;
                                }
                            }
                        }
                        if (jDet.DebitCredit == DebitCreditEnum.Credit)
                        {
                            if (jDet.CreditAmt != rsSumCategory.CreditAmt)
                            {
                                string errorString = string.Format("{2} Category: [{1}] Credit Amount Not equal For Line No: {0}", jDet.JournalDetSLNo, catName, strRefType);
                                if (vResult.AddErrorCheckReturnDC(errorString, ref dc, isDCInit))
                                {
                                    return vResult;
                                }
                            }
                        }
                    } //foreach cost cat
                }
                else
                {
                    if (jDet.DebitCredit == DebitCreditEnum.Debit)
                    {
                        decimal debitSum = refListType.Sum(c => c.DebitAmt);
                        if (jDet.DebitAmt != debitSum)
                        {
                            string errorString = string.Format("{1} Debit Amount Not equal For Line No: {0}", jDet.JournalDetSLNo, strRefType);
                            if (vResult.AddErrorCheckReturnDC(errorString, ref dc, isDCInit))
                            {
                                return vResult;
                            }
                        }
                    }
                    if (jDet.DebitCredit == DebitCreditEnum.Credit)
                    {
                        decimal creditSum = refListType.Sum(c => c.CreditAmt);
                        if (jDet.CreditAmt != creditSum)
                        {
                            string errorString = string.Format("{1} Credit Amount Not equal For Line No: {0}", jDet.JournalDetSLNo, strRefType);
                            if (vResult.AddErrorCheckReturnDC(errorString, ref dc, isDCInit))
                            {
                                return vResult;
                            }
                        }
                    }

                } //TotalSumCheckByCtategory
            } // ref entry list count > 0
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return vResult;
        }


        public static JournalValidationResult ValidateJournalDetInsList(JournalValidationTask pJTask)
        {
            return ValidateJournalDetInsList(pJTask, null);
        }
        public static JournalValidationResult ValidateJournalDetInsList(JournalValidationTask pJTask, DBContext dc)
        {
            JournalValidationResult vResult = pJTask.CurResult;
            vResult.ValidationTask = pJTask;

            if (pJTask.Journal == null)
            {
                vResult.IsError = true;
                vResult.ErrorString = string.Format("No Journal Data!");
                vResult.ValidationErrorList.Add(new DBValidationError(vResult.ErrorString));
                vResult.StopContinue = true;
                return vResult;
            }

            //fill common list to det list
            JournalDetBL.FillInsListToDetList(pJTask.Journal.JournalDetList, pJTask.Journal.JournalDetInsList, pJTask.UseDetIDLink);

            bool isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

            foreach (dcJournalDet jDet in pJTask.Journal.JournalDetList)
            {
                vResult = ValidateJournalDetIns(pJTask, jDet, dc);
                if (vResult.IsError)
                {
                    if (vResult.StopContinue || !pJTask.ValidateALL)
                    {
                        break;
                    }
                }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return vResult;
        }

        public static JournalValidationResult ValidateJournalDetIns(JournalValidationTask pJTask, dcJournalDet jDet)
        {
            return ValidateJournalDetIns(pJTask, jDet, null);
        }
        public static JournalValidationResult ValidateJournalDetIns(JournalValidationTask pJTask, dcJournalDet jDet, DBContext dc)
        { 
            JournalValidationResult vResult = pJTask.CurResult;
            vResult.ValidationTask = pJTask;

            string strDebitCredit = jDet.DebitCredit == DebitCreditEnum.Debit ? "Debit" : "Credit";
            if (jDet.GLAccountID == 0)
            {
                vResult.StopContinue = true;
                string errorString = string.Format("{1} Ledger Account not entered For Line No: {0}", jDet.JournalDetSLNo, strDebitCredit);
                if (vResult.AddErrorCheckReturn(errorString))
                {
                    return vResult;
                }
            }

            List<dcJournalDetIns> insList = jDet.JournalDetInsList.Where(c => c._RecordState != RecordStateEnum.Deleted).ToList();

            bool isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

            if (pJTask.InstrumentSettings == null)
            {
                pJTask.InstrumentSettings = InstrumentSettingsBL.GetInstrumentTypeByCompanyID(pJTask.Journal.CompanyID, dc);
            }

            if (pJTask.GLAccountList == null || pJTask.GLAccountList.Count == 0)
            {
                pJTask.GLAccountList = LoadGLAccountList(pJTask, dc);
            } 


            dcGLAccount glAccount = pJTask.GLAccountList.Where(c => c.GLAccountID == jDet.GLAccountID).FirstOrDefault();
            if (glAccount ==  null)
            {
                //vResult.StopContinue = true;
                string errorString = string.Format("{1} Ledger Account not found!, For Line No: {0}", jDet.JournalDetSLNo, strDebitCredit);
                if (vResult.AddErrorCheckReturn(errorString))
                {
                    return vResult;
                }
            }

            if (insList.Count == 0)
            {
                //Debit-Check Recive,   Credit-Check Issue
                if (glAccount.IsInstrument)
                {
                    if (jDet.DebitCredit == DebitCreditEnum.Debit)
                    {
                        if (pJTask.InstrumentSettings.AllowSkipInstrumentEntryIssue == false)
                        {
                            string errorString = string.Format("Debit entry must have Instrument Issue entry, Line No: {0}", jDet.JournalDetSLNo);
                            if (vResult.AddErrorCheckReturnDC(errorString, ref dc, isDCInit))
                            {
                                return vResult;
                            }
                        }
                    }
                    else
                    {
                        if (pJTask.InstrumentSettings.AllowSkipInstrumentEntryIssue == false)
                        {
                            string errorString = string.Format("Credit entry must have Instrument Receive entry, Line No: {0}", jDet.JournalDetSLNo);
                            if (vResult.AddErrorCheckReturnDC(errorString, ref dc, isDCInit))
                            {
                                return vResult;
                            }
                        }
                    }
                }
            }

            if (insList.Count > 0)
            {
                decimal debitAmt = insList.Sum(c => c.DebitAmt);
                decimal creditAmt = insList.Sum(c => c.CreditAmt);
                if (jDet.DebitCredit == DebitCreditEnum.Debit)
                {
                    if (jDet.DebitAmt != debitAmt)
                    {
                        string errorString = string.Format("Debit Instrument Amount Not equal For Line No: {0}", jDet.JournalDetSLNo);
                        if (vResult.AddErrorCheckReturnDC(errorString, ref dc, isDCInit))
                        {
                            return vResult;
                        }
                    }
                }

                if (jDet.DebitCredit == DebitCreditEnum.Credit)
                {
                    if (jDet.CreditAmt != creditAmt)
                    {
                        string errorString = string.Format("Credit Instrument Amount Not equal For Line No: {0}", jDet.JournalDetSLNo);
                        if (vResult.AddErrorCheckReturnDC(errorString, ref dc, isDCInit))
                        {
                            return vResult;
                        }
                    }
                }

            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return vResult;
        }

    }
}
