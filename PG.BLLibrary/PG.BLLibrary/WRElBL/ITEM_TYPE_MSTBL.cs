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
    public class ITEM_TYPE_MSTBL
    {
        public static DataLoadOptions ITEM_TYPE_MSTLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcITEM_TYPE_MST>(obj => obj.relatedclassname);
            return dlo;
        }

        public static string GetItemTypeSQLString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" SELECT * ");
            sb.Append(" FROM ITEM_TYPE_MST  ");

            sb.Append(" WHERE 1=1 ");

            return sb.ToString();
        }

        public static List<dcITEM_TYPE_MST> GetItemTypeList(dcITEM_TYPE_MST cobj, DBContext dc)
        {
            List<dcITEM_TYPE_MST> cObjList = new List<dcITEM_TYPE_MST>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetItemTypeSQLString());




                if (cobj.IS_ACTIVE != "0")
                {
                    sb.Append(" AND IS_ACTIVE= @IS_ACTIVE ");
                    cmdInfo.DBParametersInfo.Add("@IS_ACTIVE", cobj.IS_ACTIVE);
                }


                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcITEM_TYPE_MST>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static dcITEM_TYPE_MST GetItemTypeById(int pItemTypeId, DBContext dc)
        {
            dcITEM_TYPE_MST cObjList = new dcITEM_TYPE_MST();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetItemTypeSQLString());




                if (pItemTypeId > 0)
                {
                    sb.Append(" AND ITEM_TYPE_ID= @pItemTypeId ");
                    cmdInfo.DBParametersInfo.Add("@pItemTypeId", pItemTypeId);
                }


                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcITEM_TYPE_MST>(dbq, dc).FirstOrDefault();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static List<dcITEM_TYPE_MST> GetITEM_TYPE_MSTList()
        {
            return GetITEM_TYPE_MSTList(null, null);
        }
        public static List<dcITEM_TYPE_MST> GetITEM_TYPE_MSTList(DBContext dc)
        {
            return GetITEM_TYPE_MSTList(null, dc);
        }
        public static List<dcITEM_TYPE_MST> GetITEM_TYPE_MSTList(DBQuery dbq, DBContext dc)
        {
            List<dcITEM_TYPE_MST> cObjList = new List<dcITEM_TYPE_MST>();
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
                    cObjList = DBQuery.ExecuteDBQuery<dcITEM_TYPE_MST>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcITEM_TYPE_MST GetITEM_TYPE_MSTByID(int pITEM_TYPE_MSTID)
        {
            return GetITEM_TYPE_MSTByID(pITEM_TYPE_MSTID, null);
        }
        public static dcITEM_TYPE_MST GetITEM_TYPE_MSTByID(int pITEM_TYPE_MSTID, DBContext dc)
        {
            dcITEM_TYPE_MST cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    var result = (from c in dataContext.GetTable<dcITEM_TYPE_MST>()
                                  where c.ITEM_TYPE_ID == pITEM_TYPE_MSTID
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

        public static int Insert(dcITEM_TYPE_MST cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcITEM_TYPE_MST cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcITEM_TYPE_MST>(cObj, true);
                if (id > 0) { cObj.ITEM_TYPE_ID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcITEM_TYPE_MST cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcITEM_TYPE_MST cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcITEM_TYPE_MST>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pITEM_TYPE_MSTID)
        {
            return Delete(pITEM_TYPE_MSTID, null);
        }
        public static bool Delete(int pITEM_TYPE_MSTID, DBContext dc)
        {
            dcITEM_TYPE_MST cObj = new dcITEM_TYPE_MST();
            cObj.ITEM_TYPE_ID = pITEM_TYPE_MSTID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcITEM_TYPE_MST>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcITEM_TYPE_MST cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcITEM_TYPE_MST cObj, bool isAdd, DBContext dc)
        {
            cObj._RecordState = isAdd ? RecordStateEnum.Added : RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcITEM_TYPE_MST cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcITEM_TYPE_MST cObj, DBContext dc)
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
                                newID = cObj.ITEM_TYPE_ID;
                            }
                            break;
                        case RecordStateEnum.Deleted:
                            if (Delete(cObj.ITEM_TYPE_ID, dc))
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

        public static bool SaveList(List<dcITEM_TYPE_MST> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcITEM_TYPE_MST> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcITEM_TYPE_MST oDet in detList)
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
                    //    bool d = Delete(oDet.ITEM_TYPE_MSTID, dc);
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

        public static bool IsItemTypeNameExists(string pItemTypeName)
        {
            return IsItemTypeNameExists(pItemTypeName, null);
        }
        public static bool IsItemTypeNameExists(string pItemTypeName, DBContext dc)
        {
            bool isData = false;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetItemTypeSQLString());

                sb.Append(" AND UPPER(ITEM_TYPE_NAME)=UPPER(@itemTypeName) ");
                cmdInfo.DBParametersInfo.Add("@itemTypeName", pItemTypeName);



                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                isData = GetITEM_TYPE_MSTList(dbq, dc).Count > 0;

            }
            finally
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }
            return isData;
        }
        public static bool IsItemTypeNameExists(string pItemTypeName, int pItemTypeId)
        {
            return IsItemTypeNameExists(pItemTypeName, pItemTypeId, null);
        }
        public static bool IsItemTypeNameExists(string pItemTypeName, int pItemTypeId, DBContext dc)
        {
            bool isData = false;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetItemTypeSQLString());

                sb.Append(" AND UPPER(ITEM_TYPE_NAME)=UPPER(@itemTypeName) ");
                cmdInfo.DBParametersInfo.Add("@itemTypeName", pItemTypeName);


                sb.Append(" AND ITEM_TYPE_ID <> @itemTypeID ");
                cmdInfo.DBParametersInfo.Add("@itemTypeID", pItemTypeId);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                isData = GetITEM_TYPE_MSTList(dbq, dc).Count > 0;
            }
            finally
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }
            return isData;
        }
    }
}
