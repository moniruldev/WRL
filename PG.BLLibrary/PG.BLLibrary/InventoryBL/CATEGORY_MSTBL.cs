using PG.Core.DBBase;
using PG.DBClass.InventoryDC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Linq;
using System.Text;

namespace PG.BLLibrary.InventoryBL
{
    public class CATEGORY_MSTBL
    {
        public static DataLoadOptions CATEGORY_MSTLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcCATEGORY_MST>(obj => obj.relatedclassname);
            return dlo;
        }

        public static string GetCategory_ListString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" Select mst.*  ");
            sb.Append(" FROM CATEGORY_MST mst ");
            sb.Append(" Where 1=1 ");

            return sb.ToString();
        }

        public static string GetGroupWiseCatListString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT DISTINCT ATT.CAT_ID,CAT.CAT_NAME FROM CATEGORY_MST CAT ");
            sb.Append(" INNER JOIN INV_ITEM_GROP_WISE_ATTRIBUTE ATT ON ATT.CAT_ID=CAT.CAT_ID ");
            sb.Append(" WHERE 1=1 ");
            return sb.ToString();
        }
        public static List<dcCATEGORY_MST> GetCATEGORY_MSTList()
        {
            return GetCATEGORY_MSTList(null, null);
        }
        public static List<dcCATEGORY_MST> GetCATEGORY_MSTList(DBContext dc)
        {
            return GetCATEGORY_MSTList(null, dc);
        }
        public static List<dcCATEGORY_MST> GetCATEGORY_MSTList(DBQuery dbq, DBContext dc)
        {
            List<dcCATEGORY_MST> cObjList = new List<dcCATEGORY_MST>();
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
                    cObjList = DBQuery.ExecuteDBQuery<dcCATEGORY_MST>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        //public static List<dcCATEGORY_MST> GetCategoryList()
        //{
        //    return GetCategoryList(null);
        //}

        public static List<dcCATEGORY_MST> GetGroupWiseCatList(int pGroupID)
        {
            return GetGroupWiseCatList(pGroupID, null);
        }
        public static List<dcCATEGORY_MST> GetGroupWiseCatList(int pGroupID, DBContext dc)
        {
            List<dcCATEGORY_MST> cObjList = new List<dcCATEGORY_MST>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetGroupWiseCatListString());
                if (pGroupID > 0)
                {
                    sb.Append("AND ATT.ITEM_GROUP_ID=@pGroupID ");
                    cmdInfo.DBParametersInfo.Add("@pGroupID", pGroupID);
                }
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcCATEGORY_MST>(dbq, dc).ToList();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static List<dcCATEGORY_MST> GetCategoryList(string PIsActive,DBContext dc)
        {
            List<dcCATEGORY_MST> cObjList = new List<dcCATEGORY_MST>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetCategory_ListString());
                if (PIsActive != "0")
                {
                    sb.Append(" AND MST.IS_ACTIVE=@PIsActive ");
                    cmdInfo.DBParametersInfo.Add("@PIsActive", PIsActive);
                }

                sb.Append(" ORDER BY MST.CAT_NAME ");

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcCATEGORY_MST>(dbq, dc).ToList();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcCATEGORY_MST> GetCatInfoList()
        {
            return GetCatInfoList(null);
        }
        public static List<dcCATEGORY_MST> GetCatInfoList(DBContext dc)
        {
            List<dcCATEGORY_MST> cObjList = new List<dcCATEGORY_MST>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetCategory_ListString());
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcCATEGORY_MST>(dbq, dc).ToList();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

    
        public static dcCATEGORY_MST GetCATEGORY_MSTByID(int pCATEGORY_MSTID)
        {
            return GetCATEGORY_MSTByID(pCATEGORY_MSTID, null);
        }
        public static dcCATEGORY_MST GetCATEGORY_MSTByID(int pCATEGORY_MSTID, DBContext dc)
        {
            dcCATEGORY_MST cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetCategory_ListString());

                //if (pItemID > 0)
                {
                    sb.Append(" AND mst.CAT_ID=@CATEGORY_MSTID ");
                    cmdInfo.DBParametersInfo.Add("@CATEGORY_MSTID", pCATEGORY_MSTID);
                }
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                cObj = DBQuery.ExecuteDBQuery<dcCATEGORY_MST>(dbq, dc).ToList().FirstOrDefault();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObj;
        }

        public static string GETCategoryCode(DBContext dc)
        {
            //dcINVOICE_MASTER cObj = null;
            bool isDCInit = false;
            string CatNo = string.Empty;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder("SELECT  FN_NEW_CAT_CODE FROM DUAL");
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                CatNo = Convert.ToString(DBQuery.ExecuteDBScalar(dbq, dc));
                //cObj = DBQuery.ExecuteDBQuery<dcINVOICE_MASTER>(dbq, dc).FirstOrDefault();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return CatNo;
        }

        public static bool IsCategoryNameExists(string pCategoryName)
        {
            return IsCategoryNameExists(pCategoryName, null);
        }
        public static bool IsCategoryNameExists(string pCategoryName, DBContext dc)
        {
            bool isData = false;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                StringBuilder sb = new StringBuilder(GetCategory_ListString());

                sb.Append(" AND UPPER(MST.CAT_NAME)=UPPER(@pCategoryName) ");
                cmdInfo.DBParametersInfo.Add("@pCategoryName", pCategoryName);



                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                isData = GetCATEGORY_MSTList(dbq, dc).Count > 0;

            }
            finally
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }
            return isData;
        }

        public static bool IsCategoryNameExists(string pCategoryName, int pCatID)
        {
            return IsCategoryNameExists(pCategoryName, pCatID, null);
        }
        public static bool IsCategoryNameExists(string pCategoryName, int pCatID, DBContext dc)
        {
            bool isData = false;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                StringBuilder sb = new StringBuilder(GetCategory_ListString());

                sb.Append(" AND UPPER(MST.CAT_NAME)=UPPER(@pCategoryName) ");
                cmdInfo.DBParametersInfo.Add("@pCategoryName", pCategoryName);


                sb.Append(" AND MST.CAT_ID <> @pCatID ");
                //cmd.Parameters.AddWithValue("@gLGroupID", pGLGroupID);
                cmdInfo.DBParametersInfo.Add("@pCatID", pCatID);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                //cmd.CommandType = CommandType.Text;
                //cmd.CommandText = sb.ToString();

                //PG.Core.DBBase.DBQuery dbq = new PG.Core.DBBase.DBQuery();
                //dbq.DBQueryMode = PG.Core.DBBase.DBQueryModeEnum.DBCommand;
                //dbq.DBCommand = cmd;
                isData = GetCATEGORY_MSTList(dbq, dc).Count > 0;
            }
            finally
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }
            return isData;
        }

        public static int Insert(dcCATEGORY_MST cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcCATEGORY_MST cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcCATEGORY_MST>(cObj, true);
                if (id > 0) { cObj.CAT_ID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcCATEGORY_MST cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcCATEGORY_MST cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcCATEGORY_MST>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pCATEGORY_MSTID)
        {
            return Delete(pCATEGORY_MSTID, null);
        }
        public static bool Delete(int pCATEGORY_MSTID, DBContext dc)
        {
            dcCATEGORY_MST cObj = new dcCATEGORY_MST();
            cObj.CAT_ID = pCATEGORY_MSTID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcCATEGORY_MST>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcCATEGORY_MST cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcCATEGORY_MST cObj, bool isAdd, DBContext dc)
        {
            cObj._RecordState = isAdd ? RecordStateEnum.Added : RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcCATEGORY_MST cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcCATEGORY_MST cObj, DBContext dc)
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
                                newID = cObj.CAT_ID;
                            }
                            break;
                        case RecordStateEnum.Deleted:
                            if (Delete(cObj.CAT_ID, dc))
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

        public static bool SaveList(List<dcCATEGORY_MST> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcCATEGORY_MST> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcCATEGORY_MST oDet in detList)
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
                        bool d = Delete(oDet.CAT_ID, dc);
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
