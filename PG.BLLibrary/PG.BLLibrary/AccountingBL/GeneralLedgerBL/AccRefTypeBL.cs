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
using PG.DBClass.AccountingDC;
using PG.DBClass.AccountingDC.AccEnums;
using PG.DBClass.AccountingDC.GeneralLedgerDC;

namespace PG.BLLibrary.AccountingBL.GeneralLedgerBL
{
    public class AccRefTypeBL
    {

        public static string GetAccRefTypeList_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT tblAccRefType.* ");
            sb.Append(" FROM tblAccRefType ");
            sb.Append(" WHERE 1=1 ");
            return sb.ToString();
        }
        //public static DataLoadOptions AccRefTypeLoadOptions()
        //{
        //    DataLoadOptions dlo = new DataLoadOptions();
        //    //dlo.LoadWith<DBClass.PayRoll.SalaryPeriod>(obj => obj.PayRoll.SalaryPeriodType);
        //    //dlo.LoadWith<DBClass.dcPurchase>(obj => obj.Supplier);
        //    //dlo.LoadWith<DBClass.dcPurchase>(obj => obj.TranType);
        //    return dlo;
        //}
        public static List<dcAccRefType> GetAccRefTypeList()
        {
            return GetAccRefTypeList(null);
        }
        public static List<dcAccRefType> GetAccRefTypeList(DBContext dc)
        {
            List<dcAccRefType> cObjList = new List<dcAccRefType>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetAccRefTypeList_SQLString());

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcAccRefType>(dbq, dc).ToList();
                //using (DataContext dataContext = dc.NewDataContext())
                //{
                //    cObjList = (from c in dataContext.GetTable<dcAccRefType>()
                //                 select c).ToList();
                //}
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcAccRefType GetAccRefTypeByID(int pAccRefTypeID)
        {
            return GetAccRefTypeByID(pAccRefTypeID, null);
        }
        public static dcAccRefType GetAccRefTypeByID(int pAccRefTypeID, DBContext dc)
        {
            dcAccRefType cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetAccRefTypeList_SQLString());
                sb.Append(" AND tblAccRefType.AccRefTypeID=@accRefTypeID ");
                cmdInfo.DBParametersInfo.Add("@accRefTypeID", pAccRefTypeID);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObj = DBQuery.ExecuteDBQuery<dcAccRefType>(dbq, dc).FirstOrDefault();
                //using (DataContext dataContext = dc.NewDataContext())
                //{
                //    var result = (from c in dataContext.GetTable<dcAccRefType>()
                //                  where c.AccRefTypeID == pAccRefTypeID
                //                  select c).ToList();
                //    if (result.Count() > 0)
                //    {
                //        cObj = result.First();
                //    }
                //}
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObj;
        }

        public static string GetTextFromEnum(AccRefTypeEnum pAccRefType)
        {
            string strData = string.Empty;

            switch(pAccRefType)
            {
                case AccRefTypeEnum.TranCode:
                    strData = "Tran. Code";
                    break;
                case AccRefTypeEnum.CostCenter:
                    strData = "Cost Center";
                    break;
                case AccRefTypeEnum.Reference:
                    strData = "Reference";
                    break;
            }

            return strData;

        }
    }
}
