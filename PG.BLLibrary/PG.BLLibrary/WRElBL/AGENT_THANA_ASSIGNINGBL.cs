using PG.Core.DBBase;
using PG.DBClass.WRELDC;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.BLLibrary.WRElBL
{
    public class AGENT_THANA_ASSIGNINGBL
    {
        public static DataLoadOptions AGENT_THANA_ASSIGNINGLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcAGENT_THANA_ASSIGNING>(obj => obj.relatedclassname);
            return dlo;
        }
        public static List<dcAGENT_THANA_ASSIGNING> GetAGENT_THANA_ASSIGNINGList()
        {
            return GetAGENT_THANA_ASSIGNINGList(null, null);
        }
        public static List<dcAGENT_THANA_ASSIGNING> GetAGENT_THANA_ASSIGNINGList(DBContext dc)
        {
            return GetAGENT_THANA_ASSIGNINGList(null, dc);
        }
        public static List<dcAGENT_THANA_ASSIGNING> GetAGENT_THANA_ASSIGNINGList(DBQuery dbq, DBContext dc)
        {
            List<dcAGENT_THANA_ASSIGNING> cObjList = new List<dcAGENT_THANA_ASSIGNING>();
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
                    cObjList = DBQuery.ExecuteDBQuery<dcAGENT_THANA_ASSIGNING>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcAGENT_THANA_ASSIGNING GetAGENT_THANA_ASSIGNINGByID(int pAGENT_THANA_ASSIGNINGID)
        {
            return GetAGENT_THANA_ASSIGNINGByID(pAGENT_THANA_ASSIGNINGID, null);
        }
        public static dcAGENT_THANA_ASSIGNING GetAGENT_THANA_ASSIGNINGByID(int pAGENT_THANA_ASSIGNINGID, DBContext dc)
        {
            dcAGENT_THANA_ASSIGNING cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    var result = (from c in dataContext.GetTable<dcAGENT_THANA_ASSIGNING>()
                                  where c.AGENT_THANA_ID == pAGENT_THANA_ASSIGNINGID
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

        public static int Insert(dcAGENT_THANA_ASSIGNING cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcAGENT_THANA_ASSIGNING cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcAGENT_THANA_ASSIGNING>(cObj, true);
                if (id > 0) { cObj.AGENT_THANA_ID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcAGENT_THANA_ASSIGNING cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcAGENT_THANA_ASSIGNING cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcAGENT_THANA_ASSIGNING>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pAGENT_THANA_ASSIGNINGID)
        {
            return Delete(pAGENT_THANA_ASSIGNINGID, null);
        }
        public static bool Delete(int pAGENT_THANA_ASSIGNINGID, DBContext dc)
        {
            dcAGENT_THANA_ASSIGNING cObj = new dcAGENT_THANA_ASSIGNING();
            cObj.AGENT_THANA_ID = pAGENT_THANA_ASSIGNINGID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcAGENT_THANA_ASSIGNING>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcAGENT_THANA_ASSIGNING cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcAGENT_THANA_ASSIGNING cObj, bool isAdd, DBContext dc)
        {
            cObj._RecordState = isAdd ? RecordStateEnum.Added : RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcAGENT_THANA_ASSIGNING cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcAGENT_THANA_ASSIGNING cObj, DBContext dc)
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
                                newID = cObj.AGENT_THANA_ID;
                            }
                            break;
                        case RecordStateEnum.Deleted:
                            if (Delete(cObj.AGENT_THANA_ID, dc))
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

        public static bool SaveList(List<dcAGENT_THANA_ASSIGNING> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcAGENT_THANA_ASSIGNING> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcAGENT_THANA_ASSIGNING oDet in detList)
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
                    //    bool d = Delete(oDet.AGENT_THANA_ASSIGNINGID, dc);
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
    }
}
