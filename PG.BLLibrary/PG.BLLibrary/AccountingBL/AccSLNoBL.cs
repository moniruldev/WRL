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


namespace PG.BLLibrary.AccountingBL
{
    public class AccSLNoBL
    {

        public static string GetAccSLNoList_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT tblAccSLNo.* ");
            sb.Append(" FROM tblAccSLNo ");
            sb.Append(" WHERE 1=1 ");
            return sb.ToString();
        }
        //public static DataLoadOptions AccSLNoLoadOptions()
        //{
        //    DataLoadOptions dlo = new DataLoadOptions();
        //    //dlo.LoadWith<DBClass.PayRoll.SalaryPeriod>(obj => obj.PayRoll.SalaryPeriodType);
        //    //dlo.LoadWith<DBClass.dcPurchase>(obj => obj.Supplier);
        //    //dlo.LoadWith<DBClass.dcPurchase>(obj => obj.TranType);
        //    return dlo;
        //}
        public static List<dcAccSLNo> GetAccSLNoList()
        {
            return GetAccSLNoList(null, null);
        }
        public static List<dcAccSLNo> GetAccSLNoList(DBContext dc)
        {
            return GetAccSLNoList(null, dc);
        }
        public static List<dcAccSLNo> GetAccSLNoList(DBQuery dbq, DBContext dc)
        {
            List<dcAccSLNo> cObjList = new List<dcAccSLNo>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetAccSLNoList_SQLString());

                //DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcAccSLNo>(dbq, dc).ToList();

                //using (DataContext dataContext = dc.NewDataContext())
                //{
                //    cObjList = (from c in dataContext.GetTable<dcAccSLNo>()
                //                select c).ToList();
                //}
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcAccSLNo GetAccSLNoByID(int pAccSLNoID)
        {
            return GetAccSLNoByID(pAccSLNoID, null);
        }
        public static dcAccSLNo GetAccSLNoByID(int pAccSLNoID, DBContext dc)
        {
            dcAccSLNo cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetAccSLNoList_SQLString());
                sb.Append(" AND tblAccSLNo.AccSLNoID=@accSLNoID ");
                cmdInfo.DBParametersInfo.Add("@accSLNoID", pAccSLNoID);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObj = DBQuery.ExecuteDBQuery<dcAccSLNo>(dbq, dc).FirstOrDefault();
                //using (DataContext dataContext = dc.NewDataContext())
                //{
                //    var result = (from c in dataContext.GetTable<dcAccSLNo>()
                //                  where c.AccSLNoID == pAccSLNoID
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

        public static int Insert(dcAccSLNo cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcAccSLNo cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcAccSLNo>(cObj, true);
                if (id > 0) { cObj.AccSLNoID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcAccSLNo cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcAccSLNo cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcAccSLNo>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pAccSLNoID)
        {
            return Delete(pAccSLNoID, null);
        }
        public static bool Delete(int pAccSLNoID, DBContext dc)
        {
            dcAccSLNo cObj = new dcAccSLNo();
            cObj.AccSLNoID = pAccSLNoID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcAccSLNo>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static string GetNextAccSLNo(int pAccSLNoID, bool pUpdateSLNo)
        {
            return GetNextAccSLNo(pAccSLNoID, pUpdateSLNo, null);
        }

        public static string GetNextAccSLNo(int pAccSLNoID, bool pUpdateSLNo, DBContext dc)
        {
            bool isDCInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            int slNoID = pAccSLNoID;
            dcAccSLNo accSLNO = GetAccSLNoByID(slNoID, dc);

            int newSLNo = accSLNO.AccSLNoLast + 1;

            string strSLNo = newSLNo.ToString();
            if (accSLNO.IsPad)
            {
                if (accSLNO.PadChar != string.Empty)
                {
                    strSLNo = strSLNo.PadLeft(accSLNO.AccSLNoLength, accSLNO.PadChar.ToCharArray()[0]);
                }
            }
            strSLNo = accSLNO.AccSLNoPrefix + accSLNO.AccSLNoPrefixSep +  strSLNo + accSLNO.AccSLNoSuffixSep + accSLNO.AccSLNoSuffix;

            if (pUpdateSLNo)
            {
                dcAccSLNo slNoUpd = new dcAccSLNo();
                slNoUpd.AccSLNoID = accSLNO.AccSLNoID;
                slNoUpd.AccSLNoLast = newSLNo;
                Update(slNoUpd, dc);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return strSLNo;
        }


    }
}
