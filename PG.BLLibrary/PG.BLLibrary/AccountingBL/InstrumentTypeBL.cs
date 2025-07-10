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

namespace PG.BLLibrary.AccountingBL
{
    public class InstrumentTypeBL
    {

        public static string InstrumentType_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT tblInstrumentType.* ");
            sb.Append(" FROM tblInstrumentType ");
            sb.Append(" WHERE 1=1 ");
            return sb.ToString();
        }
        //public static DataLoadOptions InstrumentTypeLoadOptions()
        //{
        //    DataLoadOptions dlo = new DataLoadOptions();
        //    //dlo.LoadWith<DBClass.PayRoll.SalaryPeriod>(obj => obj.PayRoll.SalaryPeriodType);
        //    //dlo.LoadWith<DBClass.dcPurchase>(obj => obj.Supplier);
        //    //dlo.LoadWith<DBClass.dcPurchase>(obj => obj.TranType);
        //    return dlo;
        //}
        public static List<dcInstrumentType> GetInstrumentTypeList()
        {
            return GetInstrumentTypeList(null);
        }
        public static List<dcInstrumentType> GetInstrumentTypeList(DBContext dc)
        {
            List<dcInstrumentType> cObjList = new List<dcInstrumentType>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(InstrumentType_SQLString());
                sb.Append(" AND tblInstrumentType.IsVisible=@isVisible ");
                cmdInfo.DBParametersInfo.Add("@isVisible", true);
                sb.Append(" Order By tblInstrumentType.InstrumentTypeSLNo");

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcInstrumentType>(dbq, dc).ToList();
                //using (DataContext dataContext = dc.NewDataContext())
                //{
                //    cObjList = (from c in dataContext.GetTable<dcInstrumentType>()
                //                where c.IsVisible == true
                //                orderby c.InstrumentTypeSLNo 
                //                select c).ToList();
                //}
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcInstrumentType GetInstrumentTypeByID(int pInstrumentTypeID)
        {
            return GetInstrumentTypeByID(pInstrumentTypeID, null);
        }
        public static dcInstrumentType GetInstrumentTypeByID(int pInstrumentTypeID, DBContext dc)
        {
            dcInstrumentType cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(InstrumentType_SQLString());
                sb.Append(" AND tblInstrumentType.InstrumentTypeID=@instrumentTypeID ");
                cmdInfo.DBParametersInfo.Add("@instrumentTypeID", true);
                //sb.Append(" Order By tblInstrumentType.InstrumentTypeSLNo");

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObj = DBQuery.ExecuteDBQuery<dcInstrumentType>(dbq, dc).FirstOrDefault();
                //using (DataContext dataContext = dc.NewDataContext())
                //{
                //    var result = (from c in dataContext.GetTable<dcInstrumentType>()
                //                  where c.InstrumentTypeID == pInstrumentTypeID
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
    }
}
