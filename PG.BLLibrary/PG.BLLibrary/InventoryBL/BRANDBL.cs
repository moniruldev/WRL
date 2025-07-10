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
    public class BRANDBL
    {
        public static DataLoadOptions BRANDLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcBRAND>(obj => obj.relatedclassname);
            return dlo;
        }
        public static string GetBrand_ListString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" Select *  ");
            sb.Append(" FROM Brand mst ");
            sb.Append(" Where 1=1 ");

            return sb.ToString();
        }

        public static string GetGroupWiseBrandListString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT DISTINCT ATT.ITEM_BRAND_ID,BR.BRAND_NAME FROM BRAND BR ");
            sb.Append(" INNER JOIN INV_ITEM_GROP_WISE_ATTRIBUTE ATT ON ATT.ITEM_BRAND_ID=BR.BRAND_ID ");
            sb.Append(" WHERE 1=1 ");
            //sb.Append(" ORDER BY BR.BRAND_NAME ");
            return sb.ToString();
        }
        public static List<dcBRAND> GetBrandListBYID(int pBrandid, DBContext dc)
        {
            List<dcBRAND> cObjList = new List<dcBRAND>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetBrand_ListString());
                if (pBrandid > 0)
                {
                    sb.Append(" AND mst.Brand_id= @Brand_id ");
                    cmdInfo.DBParametersInfo.Add("@Brand_id", pBrandid);
                }
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcBRAND>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static string GetBRAND_INFO_List_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM BRAND WHERE 1=1 ");

            return sb.ToString();
        }
        public static string GETBrandCode( DBContext dc)
        {
            //dcINVOICE_MASTER cObj = null;
            bool isDCInit = false;
            string BrandNo = string.Empty;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder("SELECT  FN_NEW_BRAND_CODE FROM DUAL");
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                BrandNo = Convert.ToString(DBQuery.ExecuteDBScalar(dbq, dc));
                //cObj = DBQuery.ExecuteDBQuery<dcINVOICE_MASTER>(dbq, dc).FirstOrDefault();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return BrandNo;
        }

        public static dcBRAND GetBrandInfoByID(int pBrandID)
        {
            return GetBrandInfoByID(pBrandID, null);
        }
        public static dcBRAND GetBrandInfoByID(int pBrandID, DBContext dc)
        {
            dcBRAND cObjList = new dcBRAND();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetBrand_ListString());

                //if (pItemID > 0)
                {
                    sb.Append(" AND mst.Brand_id=@BrandID ");
                    cmdInfo.DBParametersInfo.Add("@BrandID", pBrandID);
                }
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                cObjList = DBQuery.ExecuteDBQuery<dcBRAND>(dbq, dc).ToList().FirstOrDefault();

            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        //public static dcBRAND GetBrandInfo( DBContext dc)
        //{
        //    dcBRAND cObj = new dcBRAND();
        //    bool isDCInit = false;
        //    try
        //    {
        //        isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

        //        DBCommandInfo cmdInfo = new DBCommandInfo();
        //        StringBuilder sb = new StringBuilder(GetBrand_ListString());
        //        DBQuery dbq = new DBQuery();
        //        dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
        //        cmdInfo.CommandText = sb.ToString();
        //        cmdInfo.CommandType = CommandType.Text;
        //        dbq.DBCommandInfo = cmdInfo;
        //        cObj = DBQuery.ExecuteDBQuery<dcBRAND>(dbq, dc).ToList().FirstOrDefault();

        //    }
        //    catch { throw; }
        //    finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
        //    return cObj;
        //}

        public static bool IsBrandNameExists(string pBrandName)
        {
            return IsBrandNameExists(pBrandName, null);
        }
        public static bool IsBrandNameExists(string pBrandName, DBContext dc)
        {
            bool isData = false;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                StringBuilder sb = new StringBuilder(GetBrand_ListString());

                sb.Append(" AND UPPER(MST.BRAND_NAME)=UPPER(@BrandName) ");
                cmdInfo.DBParametersInfo.Add("@BrandName", pBrandName);



                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                isData = GetBRANDList(dbq, dc).Count > 0;

            }
            finally
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }
            return isData;
        }

        public static bool IsBrandNameExists(string pBrandName, int pBrandID)
        {
            return IsBrandNameExists(pBrandName, pBrandID, null);
        }
        public static bool IsBrandNameExists(string pBrandName, int pBrandID, DBContext dc)
        {
            bool isData = false;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                StringBuilder sb = new StringBuilder(GetBrand_ListString());

                sb.Append(" AND UPPER(MST.BRAND_NAME)=UPPER(@BrandName) ");
                cmdInfo.DBParametersInfo.Add("@BrandName", pBrandName);


                sb.Append(" AND MST.BRAND_ID <> @pBrandID ");
                //cmd.Parameters.AddWithValue("@gLGroupID", pGLGroupID);
                cmdInfo.DBParametersInfo.Add("@pBrandID", pBrandID);

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
                isData = GetBRANDList(dbq, dc).Count > 0;
            }
            finally
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }
            return isData;
        }

     
        public static List<dcBRAND> GetBrandINFList(string PIsActive, DBContext dc)
        {
            List<dcBRAND> cObjList = new List<dcBRAND>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetBRAND_INFO_List_SQLString());
                if (PIsActive != "0")
                {
                    sb.Append(" AND IS_ACTIVE=@PIsActive ");
                    cmdInfo.DBParametersInfo.Add("@PIsActive", PIsActive);
                }

                sb.Append(" ORDER BY BRAND_NAME ");

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcBRAND>(dbq, dc).ToList();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcBRAND> GetBRANDINFOList()
        {
            return GetBRANDINFOList(null);
        }
        public static List<dcBRAND> GetBRANDINFOList(DBContext dc)
        {
            List<dcBRAND> cObjList = new List<dcBRAND>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetBRAND_INFO_List_SQLString());
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcBRAND>(dbq, dc).ToList();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcBRAND> GetBrandListGroup(DBQuery dbq, DBContext dc)
        {
            List<dcBRAND> cObjList = new List<dcBRAND>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    if (dbq == null)
                    {
                        DBCommandInfo cmdInfo = new DBCommandInfo();
                        StringBuilder sb = new StringBuilder(GetGroupWiseBrandListString());
                        dbq = new DBQuery();
                        dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                        cmdInfo.CommandText = sb.ToString();
                        cmdInfo.CommandType = CommandType.Text;
                        dbq.DBCommandInfo = cmdInfo;
                    }
                    cObjList = DBQuery.ExecuteDBQuery<dcBRAND>(dbq, dc).ToList();
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcBRAND> GetGroupWiseBrandList(int pGroupID)
        {
            return GetGroupWiseBrandList(pGroupID,null);
        }
        public static List<dcBRAND> GetGroupWiseBrandList(int pGroupID,DBContext dc)
        {
            List<dcBRAND> cObjList = new List<dcBRAND>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetGroupWiseBrandListString());
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

                cObjList = DBQuery.ExecuteDBQuery<dcBRAND>(dbq, dc).ToList();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static List<dcBRAND> GetBRANDList()
        {
            return GetBRANDList(null, null);
        }
        public static List<dcBRAND> GetBRANDList(DBContext dc)
        {
            return GetBRANDList(null, dc);
        }
        public static List<dcBRAND> GetBRANDList(DBQuery dbq, DBContext dc)
        {
            List<dcBRAND> cObjList = new List<dcBRAND>();
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
                    cObjList = DBQuery.ExecuteDBQuery<dcBRAND>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcBRAND GetBRANDByID(int pBRANDID)
        {
            return GetBRANDByID(pBRANDID, null);
        }
        public static dcBRAND GetBRANDByID(int pBRANDID, DBContext dc)
        {
            dcBRAND cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    var result = (from c in dataContext.GetTable<dcBRAND>()
                                  where c.BRAND_ID == pBRANDID
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

        public static int Insert(dcBRAND cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcBRAND cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcBRAND>(cObj, true);
                if (id > 0) { cObj.BRAND_ID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcBRAND cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcBRAND cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcBRAND>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pBRANDID)
        {
            return Delete(pBRANDID, null);
        }
        public static bool Delete(int pBRANDID, DBContext dc)
        {
            dcBRAND cObj = new dcBRAND();
            cObj.BRAND_ID = pBRANDID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcBRAND>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcBRAND cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcBRAND cObj, bool isAdd, DBContext dc)
        {
            cObj._RecordState = isAdd ? RecordStateEnum.Added : RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcBRAND cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcBRAND cObj, DBContext dc)
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
                                newID = cObj.BRAND_ID;
                            }
                            break;
                        case RecordStateEnum.Deleted:
                            if (Delete(cObj.BRAND_ID, dc))
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

        public static bool SaveList(List<dcBRAND> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcBRAND> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcBRAND oDet in detList)
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
                        bool d = Delete(oDet.BRAND_ID, dc);
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
