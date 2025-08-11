using PG.Core.DBBase;
using PG.DBClass.WRELDC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.BLLibrary.WRElBL
{
    public class TEMP_CN_INFOBL
    {
        public static DataLoadOptions TEMP_CN_INFOLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcTEMP_CN_INFO>(obj => obj.relatedclassname);
            return dlo;
        }

        public static string GetTempCNinfoSQLString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" SELECT * FROM TEMP_CN_INFO");
            sb.Append(" WHERE 1=1 ");

            return sb.ToString();
        }
        public static List<dcTEMP_CN_INFO> GetTEMP_CN_INFOList()
        {
            return GetTEMP_CN_INFOList(null, null);
        }
        public static List<dcTEMP_CN_INFO> GetTEMP_CN_INFOList(DBContext dc)
        {
            return GetTEMP_CN_INFOList(null, dc);
        }
        public static List<dcTEMP_CN_INFO> GetTEMP_CN_INFOList(DBQuery dbq, DBContext dc)
        {
            List<dcTEMP_CN_INFO> cObjList = new List<dcTEMP_CN_INFO>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    if (dbq == null)
                    {
                        dbq = new DBQuery();
                        //dbq.OrderBy = "YearStartDate Desc";
                    }
                    cObjList = DBQuery.ExecuteDBQuery<dcTEMP_CN_INFO>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcTEMP_CN_INFO> GetTempCNListInfo(string Prm, DBContext dc)
        {
            List<dcTEMP_CN_INFO> cObjList = new List<dcTEMP_CN_INFO>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetTempCNinfoSQLString());
                //if (Prm.TRANS_TYPE != string.Empty)
                //{
                //    sb.Append(" AND c.TRANS_TYPE= @pTRANS_TYPE ");
                //    cmdInfo.DBParametersInfo.Add("@pTRANS_TYPE", Prm.TRANS_TYPE);
                //}
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcTEMP_CN_INFO>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcTEMP_CN_INFO GetTEMP_CN_INFOByID(int pTEMP_CN_INFOID)
        {
            return GetTEMP_CN_INFOByID(pTEMP_CN_INFOID, null);
        }
        public static dcTEMP_CN_INFO GetTEMP_CN_INFOByID(int pTEMP_CN_INFOID, DBContext dc)
        {
            dcTEMP_CN_INFO cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    var result = (from c in dataContext.GetTable<dcTEMP_CN_INFO>()
                                  where c.SLNO == pTEMP_CN_INFOID
                                  select c).ToList();
                    if (result.Count() > 0)
                    {
                        cObj = result.First();
                    }
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObj;
        }

        public static int Insert(dcTEMP_CN_INFO cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcTEMP_CN_INFO cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcTEMP_CN_INFO>(cObj, true);
                if (id > 0) { cObj.SLNO = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcTEMP_CN_INFO cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcTEMP_CN_INFO cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcTEMP_CN_INFO>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pTEMP_CN_INFOID)
        {
            return Delete(pTEMP_CN_INFOID, null);
        }
        public static bool Delete(int pTEMP_CN_INFOID, DBContext dc)
        {
            dcTEMP_CN_INFO cObj = new dcTEMP_CN_INFO();
            cObj.SLNO = pTEMP_CN_INFOID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcTEMP_CN_INFO>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcTEMP_CN_INFO cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcTEMP_CN_INFO cObj, bool isAdd, DBContext dc)
        {
            //cObj._RecordState = isAdd ? Interwave.Core.DBClass.RecordStateEnum.Added : Interwave.Core.DBClass.RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcTEMP_CN_INFO cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcTEMP_CN_INFO cObj, DBContext dc)
        {
            int newID = 0;
            bool isDCInit = false;
            bool isTransInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                isTransInit = dc.StartTransaction();
                using (DataContext dataContext = dc.NewDataContext())
                {

                    switch (cObj._RecordState)
                    {
                        //case Interwave.Core.DBClass.RecordStateEnum.Added:
                        //    newID = Insert(cObj, dc);
                        //    break;
                        //case Interwave.Core.DBClass.RecordStateEnum.Edited:
                        //    if (Update(cObj, dc))
                        //    {
                        //        newID = cObj.TEMP_CN_INFOID;
                        //    }
                        //    break;
                        //case Interwave.Core.DBClass.RecordStateEnum.Deleted:
                        //    if (Delete(cObj.TEMP_CN_INFOID, dc))
                        //    {
                        //        newID = 1;
                        //    }
                        //    break;
                        default:
                            break;
                    }

                    if (newID > 0)
                    {
                        bool bStatus = false;

                        ///code list save logic here

                        bStatus = true;
                        if (bStatus)
                        {
                            dc.CommitTransaction(isTransInit);
                        }
                    }
                }
            }
            catch
            {
                dc.RollbackTransaction(isTransInit);
                throw;
            }
            finally
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }
            return newID;
        }

        public static bool SaveList(List<dcTEMP_CN_INFO> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcTEMP_CN_INFO> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcTEMP_CN_INFO oDet in detList)
            {
                switch (oDet._RecordState)
                {
                    //case Interwave.Core.DBClass.RecordStateEnum.Added:
                    //    int a = Insert(oDet, dc);
                    //    break;
                    //case Interwave.Core.DBClass.RecordStateEnum.Edited:
                    //    bool e = Update(oDet, dc);
                    //    break;
                    //case Interwave.Core.DBClass.RecordStateEnum.Deleted:
                    //    bool d = Delete(oDet.TEMP_CN_INFOID, dc);
                    //    break;
                    default:
                        break;
                }
            }
            dc.CommitTransaction(isTransInit);
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            bStatus = true;
            return bStatus;
        }

        public static void DeleteTempData(DBContext dc)
        {
            bool isDCInit = false;
             try
            {

                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
               bool isTransInit = dc.StartTransaction();
            DBQuery dbq = new DBQuery();
            dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;

            DBCommandInfo cmdInfo = new DBCommandInfo();
            string abbrdel = " DELETE FROM TEMP_CN_INFO WHERE 1=1 ";
            //cmdInfo.DBParametersInfo.Add("@INVOICE_NO", pObj.invoice_no);

            cmdInfo.CommandText = abbrdel;
            cmdInfo.CommandType = CommandType.Text;
            dbq.DBCommandInfo = cmdInfo;
            DBQuery.ExecuteDBNonQuery(dbq, dc);
            dc.CommitTransaction(isTransInit);
            }
             catch { throw; }
             finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
        }
    }
}
