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
    public class BRANCH_THANA_ASSIGNINGBL
    {
        public static DataLoadOptions BRANCH_THANA_ASSIGNINGLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcBRANCH_THANA_ASSIGNING>(obj => obj.relatedclassname);
            return dlo;
        }
        public static string GetBranchThanaInfoSQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT BT.*,t.town_name FROM branch_thana_assigning BT ");
            sb.Append(" INNER JOIN  thana_town_mst T ON BT.town_id=t.town_id  ");
            sb.Append(" WHERE 1=1 ");

            return sb.ToString();
        }

        public static List<dcBRANCH_THANA_ASSIGNING> GetBranchThanaInfoByBranchId(int pBranchId, DBContext dc)
        {
            List<dcBRANCH_THANA_ASSIGNING> cObjList = new List<dcBRANCH_THANA_ASSIGNING>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetBranchThanaInfoSQLString());
                if (pBranchId > 0)
                {
                    sb.Append(" AND BT.BRANCH_ID= @pBranchId ");
                    cmdInfo.DBParametersInfo.Add("@pBranchId", pBranchId);
                }
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcBRANCH_THANA_ASSIGNING>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static List<dcBRANCH_THANA_ASSIGNING> GetBRANCH_THANA_ASSIGNINGList()
        {
            return GetBRANCH_THANA_ASSIGNINGList(null, null);
        }
        public static List<dcBRANCH_THANA_ASSIGNING> GetBRANCH_THANA_ASSIGNINGList(DBContext dc)
        {
            return GetBRANCH_THANA_ASSIGNINGList(null, dc);
        }
        public static List<dcBRANCH_THANA_ASSIGNING> GetBRANCH_THANA_ASSIGNINGList(DBQuery dbq, DBContext dc)
        {
            List<dcBRANCH_THANA_ASSIGNING> cObjList = new List<dcBRANCH_THANA_ASSIGNING>();
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
                    cObjList = DBQuery.ExecuteDBQuery<dcBRANCH_THANA_ASSIGNING>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcBRANCH_THANA_ASSIGNING GetBRANCH_THANA_ASSIGNINGByID(int pBRANCH_THANA_ID)
        {
            return GetBRANCH_THANA_ASSIGNINGByID(pBRANCH_THANA_ID, null);
        }
        public static dcBRANCH_THANA_ASSIGNING GetBRANCH_THANA_ASSIGNINGByID(int pBRANCH_THANA_ID, DBContext dc)
        {
            dcBRANCH_THANA_ASSIGNING cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    var result = (from c in dataContext.GetTable<dcBRANCH_THANA_ASSIGNING>()
                                  where c.BRANCH_THANA_ID == pBRANCH_THANA_ID
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

        public static int Insert(dcBRANCH_THANA_ASSIGNING cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcBRANCH_THANA_ASSIGNING cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcBRANCH_THANA_ASSIGNING>(cObj, true);
                if (id > 0) { cObj.BRANCH_THANA_ID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcBRANCH_THANA_ASSIGNING cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcBRANCH_THANA_ASSIGNING cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcBRANCH_THANA_ASSIGNING>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pBRANCH_THANA_ID)
        {
            return Delete(pBRANCH_THANA_ID, null);
        }
        public static bool Delete(int pBRANCH_THANA_ID, DBContext dc)
        {
            dcBRANCH_THANA_ASSIGNING cObj = new dcBRANCH_THANA_ASSIGNING();
            cObj.BRANCH_THANA_ID = pBRANCH_THANA_ID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcBRANCH_THANA_ASSIGNING>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcBRANCH_THANA_ASSIGNING cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcBRANCH_THANA_ASSIGNING cObj, bool isAdd, DBContext dc)
        {
            cObj._RecordState = isAdd ? RecordStateEnum.Added : RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcBRANCH_THANA_ASSIGNING cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcBRANCH_THANA_ASSIGNING cObj, DBContext dc)
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
                                newID = cObj.BRANCH_THANA_ID;
                            }
                            break;
                        case RecordStateEnum.Deleted:
                            if (Delete(cObj.BRANCH_THANA_ID, dc))
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

        public static bool SaveList(List<dcBRANCH_THANA_ASSIGNING> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcBRANCH_THANA_ASSIGNING> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcBRANCH_THANA_ASSIGNING oDet in detList)
            {
                switch (oDet._RecordState)
                {
                    case RecordStateEnum.Added:
                        int a = Insert(oDet, dc);
                        break;
                    case RecordStateEnum.Edited:
                        bool e = Update(oDet, dc);
                        break;
                    case RecordStateEnum.Deleted:
                        bool d = Delete(oDet.BRANCH_THANA_ID, dc);
                        break;
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
