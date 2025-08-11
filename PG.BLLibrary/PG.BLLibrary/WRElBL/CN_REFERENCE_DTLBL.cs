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
    public class CN_REFERENCE_DTLBL
    {
        public static DataLoadOptions CN_REFERENCE_DTLLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcCN_REFERENCE_DTL>(obj => obj.relatedclassname);
            return dlo;
        }

        public static string GetCNReferenceDTLListString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" SELECT MST.*,STD.CN_NUMBER ");
            sb.Append(" FROM CN_REFERENCE_DTL mst ");
            sb.Append(" INNER JOIN CN_CREATION_MST STD ON mst.CN_ID=std.CN_ID ");
          
            sb.Append(" WHERE 1=1 ");

            return sb.ToString();
        }

        public static dcCN_REFERENCE_DTL GetCNDetailInfoById(int pCN_ID)
        {
            return GetCNRefDetailsList(pCN_ID, null).FirstOrDefault();
        }

        public static List<dcCN_REFERENCE_DTL> GetCNRefDetailsList()
        {
            return GetCNRefDetailsList(0, null);
        }

        public static List<dcCN_REFERENCE_DTL> GetCNRefDetailsList(int pCN_ID, DBContext dc)
        {
            List<dcCN_REFERENCE_DTL> cObjList = new List<dcCN_REFERENCE_DTL>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetCNReferenceDTLListString());
                if (pCN_ID > 0)
                {
                    sb.Append(" AND mst.CN_ID= @pCN_ID ");
                    cmdInfo.DBParametersInfo.Add("@pCN_ID", pCN_ID);
                }
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcCN_REFERENCE_DTL>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcCN_REFERENCE_DTL> GetCN_REFERENCE_DTLList()
        {
            return GetCN_REFERENCE_DTLList(null, null);
        }
        public static List<dcCN_REFERENCE_DTL> GetCN_REFERENCE_DTLList(DBContext dc)
        {
            return GetCN_REFERENCE_DTLList(null, dc);
        }
        public static List<dcCN_REFERENCE_DTL> GetCN_REFERENCE_DTLList(DBQuery dbq, DBContext dc)
        {
            List<dcCN_REFERENCE_DTL> cObjList = new List<dcCN_REFERENCE_DTL>();
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
                    cObjList = DBQuery.ExecuteDBQuery<dcCN_REFERENCE_DTL>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcCN_REFERENCE_DTL GetCN_REFERENCE_DTLByID(int pCN_REFERENCE_DTLID)
        {
            return GetCN_REFERENCE_DTLByID(pCN_REFERENCE_DTLID, null);
        }
        public static dcCN_REFERENCE_DTL GetCN_REFERENCE_DTLByID(int pCN_REFERENCE_DTLID, DBContext dc)
        {
            dcCN_REFERENCE_DTL cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    var result = (from c in dataContext.GetTable<dcCN_REFERENCE_DTL>()
                                  where c.CN_REF_DTL_ID == pCN_REFERENCE_DTLID
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

        public static int Insert(dcCN_REFERENCE_DTL cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcCN_REFERENCE_DTL cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcCN_REFERENCE_DTL>(cObj, true);
                if (id > 0) { cObj.CN_REF_DTL_ID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcCN_REFERENCE_DTL cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcCN_REFERENCE_DTL cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcCN_REFERENCE_DTL>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pCN_REFERENCE_DTLID)
        {
            return Delete(pCN_REFERENCE_DTLID, null);
        }
        public static bool Delete(int pCN_REFERENCE_DTLID, DBContext dc)
        {
            dcCN_REFERENCE_DTL cObj = new dcCN_REFERENCE_DTL();
            cObj.CN_REF_DTL_ID = pCN_REFERENCE_DTLID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcCN_REFERENCE_DTL>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcCN_REFERENCE_DTL cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcCN_REFERENCE_DTL cObj, bool isAdd, DBContext dc)
        {
            cObj._RecordState = isAdd ? RecordStateEnum.Added : RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcCN_REFERENCE_DTL cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcCN_REFERENCE_DTL cObj, DBContext dc)
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
                        case RecordStateEnum.Added:
                            newID = Insert(cObj, dc);
                            break;
                        case RecordStateEnum.Edited:
                            if (Update(cObj, dc))
                            {
                                newID = cObj.CN_REF_DTL_ID;
                            }
                            break;
                        case RecordStateEnum.Deleted:
                            if (Delete(cObj.CN_REF_DTL_ID, dc))
                            {
                                newID = 1;
                            }
                            break;
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

        public static bool SaveList(List<dcCN_REFERENCE_DTL> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcCN_REFERENCE_DTL> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcCN_REFERENCE_DTL oDet in detList)
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
                    //    bool d = Delete(oDet.CN_REFERENCE_DTLID, dc);
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

        public static dcCN_REFERENCE_DTL GetCNIDInfoByCNNumber(string pCN_NO)
        {
            return GetCNIDInfoByCNNumberList(pCN_NO, null).FirstOrDefault();
        }
        public static List<dcCN_REFERENCE_DTL> GetCNIDInfoByCNNumberList(string pCN_NO, DBContext dc)
        {
            List<dcCN_REFERENCE_DTL> cObjList = new List<dcCN_REFERENCE_DTL>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder("SELECT mst.*,cl.CLIENT_NAME FROM CN_CREATION_MST mst INNER JOIN CLIENT_MST cl ON mst.CLIENT_ID=cl.CLIENT_ID WHERE 1=1 ");
                if (pCN_NO != string.Empty)
                {
                    sb.Append(" AND mst.CN_NUMBER= @pCN_NO ");
                    cmdInfo.DBParametersInfo.Add("@pCN_NO", pCN_NO);
                }
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcCN_REFERENCE_DTL>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
    }
}
