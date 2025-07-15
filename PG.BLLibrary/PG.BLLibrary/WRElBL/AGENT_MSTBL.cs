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
    public class AGENT_MSTBL
    {
        public static DataLoadOptions AGENT_MSTLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcAGENT_MST>(obj => obj.relatedclassname);
            return dlo;
        }

        public static string GetAgentSQLString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" SELECT * ");
            sb.Append(" FROM AGENT_MST  ");

            sb.Append(" WHERE 1=1 ");

            return sb.ToString();
        }

        public static string GetAgentListString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" SELECT AGENT_ID,AGENT_COMPANY_NAME,OWNER_NAME,OWNER_MOBILE_NO,CONTACT_PERSON,CONTACT_MOBILE_NO,BANK_NAME,BRANCH,ACCOUNT_NO,IS_ACTIVE ");
            sb.Append(" FROM AGENT_MST  ");

            sb.Append(" WHERE IS_ACTIVE='Y' ");


            return sb.ToString();
        }
        public static List<dcAGENT_MST> GetAGENT_MSTList()
        {
            return GetAGENT_MSTList(null, null);
        }
        public static List<dcAGENT_MST> GetAGENT_MSTList(DBContext dc)
        {
            return GetAGENT_MSTList(null, dc);
        }
        public static List<dcAGENT_MST> GetAGENT_MSTList(DBQuery dbq, DBContext dc)
        {
            List<dcAGENT_MST> cObjList = new List<dcAGENT_MST>();
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
                    cObjList = DBQuery.ExecuteDBQuery<dcAGENT_MST>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcAGENT_MST GetAGENT_MSTByID(int pAGENT_MSTID)
        {
            return GetAGENT_MSTByID(pAGENT_MSTID, null);
        }
        public static dcAGENT_MST GetAGENT_MSTByID(int pAGENT_MSTID, DBContext dc)
        {
            dcAGENT_MST cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    var result = (from c in dataContext.GetTable<dcAGENT_MST>()
                                  where c.AGENT_ID == pAGENT_MSTID
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

        public static dcAGENT_MST GetAgentInfoById(int pAgent_ID)
        {
            return GetAgentInfoById(pAgent_ID, null).FirstOrDefault();
        }
        public static List<dcAGENT_MST> GetAgentInfoById(int pAgent_ID, DBContext dc)
        {
            List<dcAGENT_MST> cObjList = new List<dcAGENT_MST>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetAgentSQLString());
                if (pAgent_ID > 0)
                {
                    sb.Append(" AND AGENT_ID= @pAgent_ID ");
                    cmdInfo.DBParametersInfo.Add("@pAgent_ID", pAgent_ID);
                }
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcAGENT_MST>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcAGENT_MST> GetAgentList(dcAGENT_MST prmHms, DBContext dc)
        {
            List<dcAGENT_MST> cObjList = new List<dcAGENT_MST>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetAgentSQLString());




                if (prmHms.IS_ACTIVE != "0")
                {
                    sb.Append(" AND IS_ACTIVE= @IS_ACTIVE ");
                    cmdInfo.DBParametersInfo.Add("@IS_ACTIVE", prmHms.IS_ACTIVE);
                }


                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcAGENT_MST>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }


        public static int Insert(dcAGENT_MST cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcAGENT_MST cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcAGENT_MST>(cObj, true);
                if (id > 0) { cObj.AGENT_ID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcAGENT_MST cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcAGENT_MST cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcAGENT_MST>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pAGENT_MSTID)
        {
            return Delete(pAGENT_MSTID, null);
        }
        public static bool Delete(int pAGENT_MSTID, DBContext dc)
        {
            dcAGENT_MST cObj = new dcAGENT_MST();
            cObj.AGENT_ID = pAGENT_MSTID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcAGENT_MST>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcAGENT_MST cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcAGENT_MST cObj, bool isAdd, DBContext dc)
        {
            cObj._RecordState = isAdd ? RecordStateEnum.Added : RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcAGENT_MST cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcAGENT_MST cObj, DBContext dc)
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
                                newID = cObj.AGENT_ID;
                            }
                            break;
                        case RecordStateEnum.Deleted:
                            if (Delete(cObj.AGENT_ID, dc))
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

        public static bool SaveList(List<dcAGENT_MST> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcAGENT_MST> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcAGENT_MST oDet in detList)
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
                    //    bool d = Delete(oDet.AGENT_MSTID, dc);
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
