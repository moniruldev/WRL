using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Data.Linq;
//using System.Linq.Dynamic;
using PG.Core.DBBase;
using PG.Core.Extentions;
using PG.Core.DBFilters;
using PG.DBClass;
using PG.DBClass.AccountingDC;
using PG.DBClass.AccountingDC.AccEnums;


namespace PG.BLLibrary.AccountingBL
{
    public class AccHelper
    {
        public static int GetDrCrBalanceType(decimal pBalanceAmt)
        {
            return GetDrCrBalanceType(pBalanceAmt, (int)DebitCreditEnum.Debit);
        }

        public static int GetDrCrBalanceType(decimal pBalanceAmt, int pBalanceType)
        {
            int balanceType = 0;
            if (pBalanceAmt == 0)
            {
                balanceType = pBalanceType;
            }
            else
            {
                if (pBalanceAmt > 0)
                {
                    balanceType = (int)DebitCreditEnum.Debit;
                }
                else
                {
                    balanceType = (int)DebitCreditEnum.Credit;
                }
            }
            return balanceType;
        }

        public static string GetDrCrBalanceText(decimal pBalanceAmt)
        {
           
            string balanceText = "";

            if (pBalanceAmt != 0)
            {
                balanceText = pBalanceAmt > 0 ? "Dr" : "Cr";
            }

            return balanceText;
        }

        public static string GetDrCrBalanceText(decimal pBalanceAmt, int pBalanceType)
        {
            string balanceText = "";
            if (pBalanceAmt != 0)
            {
                if (pBalanceAmt > 0)
                {
                    balanceText = "Dr";
                }
                else
                {
                    balanceText = "Cr";
                }
            }
            return balanceText;
        }


        public static string GetDrCrText(int pBalanceType)
        {
            string type = "Dr";

            if (pBalanceType == (int)DebitCreditEnum.Credit)
            {
                type = "Cr";
            }
            else
            {
                type = "Dr";
            }
            return type;
        }

        public static string GetAmountString(decimal pBalanceAmt, string pFormatString)
        {
            string amtString = string.Empty;

            pFormatString = (pFormatString.Trim() == string.Empty) ? "#0.00" : pFormatString;
            if (pBalanceAmt >= 0)
            {
                amtString = pBalanceAmt.ToString(pFormatString);
            }
            else
            {
                amtString = "(" + pBalanceAmt.ToString(pFormatString) + ")";
            }
            return amtString;
        }

        public static string GetAmountString(decimal pBalanceAmt, int pBalanceType, string pFormatString)
        {
            if (pBalanceAmt < 0)
            {
                throw new ArgumentException("Negative Balance not allowed");
            }

            string amtString = string.Empty;

            pFormatString = (pFormatString.Trim() == string.Empty) ? "#0.00" : pFormatString;
            if (pBalanceType == (int)DebitCreditEnum.Debit)
            {
                amtString = pBalanceAmt.ToString(pFormatString);
            }
            else
            {
                amtString = "(" + pBalanceAmt.ToString(pFormatString) + ")";
            }
            return amtString;
        }
        public static List<int> GetGLAccountTypeIDFilterList(int typeFilterInt)
        {
            return GetGLAccountTypeIDFilterList((GLAccountTypeFilterEnum)typeFilterInt);
        }
        public static List<int> GetGLAccountTypeIDFilterList(GLAccountTypeFilterEnum typeFilter)
        {
            List<int> typeIDList = new List<int>();
            switch (typeFilter)
            {
                case GLAccountTypeFilterEnum.NoFilter:
                    break;
                case GLAccountTypeFilterEnum.AllAccount:
                    typeIDList.Add((int)GLAccountTypeEnum.NormalAccount);
                    typeIDList.Add((int)GLAccountTypeEnum.ControlAccount);
                    typeIDList.Add((int)GLAccountTypeEnum.SubAccount);
                    break;
                case GLAccountTypeFilterEnum.NormalAccount:
                    typeIDList.Add((int)GLAccountTypeEnum.NormalAccount);
                    break;
                case GLAccountTypeFilterEnum.ControlAccount:
                    typeIDList.Add((int)GLAccountTypeEnum.ControlAccount);
                    break;
                case GLAccountTypeFilterEnum.SubAccount:
                    typeIDList.Add((int)GLAccountTypeEnum.SubAccount);
                    break;
                case GLAccountTypeFilterEnum.NormalControlAccount:
                    typeIDList.Add((int)GLAccountTypeEnum.NormalAccount);
                    typeIDList.Add((int)GLAccountTypeEnum.ControlAccount);
                    break;
                case GLAccountTypeFilterEnum.NormalSubAccount:
                    typeIDList.Add((int)GLAccountTypeEnum.NormalAccount);
                    typeIDList.Add((int)GLAccountTypeEnum.SubAccount);
                    break;
                case GLAccountTypeFilterEnum.ControlSubAccount:
                    typeIDList.Add((int)GLAccountTypeEnum.ControlAccount);
                    typeIDList.Add((int)GLAccountTypeEnum.SubAccount);
                    break;
                case GLAccountTypeFilterEnum.SubAccountByControl:
                    typeIDList.Add((int)GLAccountTypeEnum.SubAccount);
                    break;
            }
            return typeIDList;
        }


        public static string GetGLAccountTypeIDFilterListString(int typeFilterInt)
        {
            return GetGLAccountTypeIDFilterListString((GLAccountTypeFilterEnum)typeFilterInt);
        }
        public static string GetGLAccountTypeIDFilterListString(GLAccountTypeFilterEnum typeFilter)
        {
            string strList = string.Empty;
            switch (typeFilter)
            {
                case GLAccountTypeFilterEnum.NoFilter:
                    break;
                case GLAccountTypeFilterEnum.AllAccount:
                    strList = ((int)GLAccountTypeEnum.NormalAccount).ToString();
                    strList +=  "," +  ((int)GLAccountTypeEnum.ControlAccount).ToString();
                    strList += "," + ((int)GLAccountTypeEnum.SubAccount).ToString();
                    break;
                case GLAccountTypeFilterEnum.NormalAccount:
                    strList = ((int)GLAccountTypeEnum.NormalAccount).ToString();
                    break;
                case GLAccountTypeFilterEnum.ControlAccount:
                    strList = ((int)GLAccountTypeEnum.ControlAccount).ToString();
                    break;
                case GLAccountTypeFilterEnum.SubAccount:
                    strList = ((int)GLAccountTypeEnum.SubAccount).ToString();
                    break;
                case GLAccountTypeFilterEnum.NormalControlAccount:
                    strList = ((int)GLAccountTypeEnum.NormalAccount).ToString();
                    strList += "," + ((int)GLAccountTypeEnum.ControlAccount).ToString();
                    break;
                case GLAccountTypeFilterEnum.NormalSubAccount:
                    strList = ((int)GLAccountTypeEnum.NormalAccount).ToString();
                    strList += "," + ((int)GLAccountTypeEnum.SubAccount).ToString();
                    break;
                case GLAccountTypeFilterEnum.ControlSubAccount:
                    strList = ((int)GLAccountTypeEnum.ControlAccount).ToString();
                    strList += "," + ((int)GLAccountTypeEnum.SubAccount).ToString();
                    break;
                case GLAccountTypeFilterEnum.SubAccountByControl:
                    strList = ((int)GLAccountTypeEnum.SubAccount).ToString();
                    break;
            }
            return strList;
        }


    }
}
