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
    public class ITEM_MSTBL
    {
        public static DataLoadOptions ITEM_MSTLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcITEM_MST>(obj => obj.relatedclassname);
            return dlo;
        }
        public static string GetItemMstListString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" SELECT ITEM_ID,ITEM_NAME,SERVICE_CHARGE_AMT_DEFAULT ");
            sb.Append(" FROM ITEM_MST  ");

            sb.Append(" WHERE IS_ACTIVE='Y' ");


            return sb.ToString();
        }

        public static string GetItemMstSQLString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" SELECT IM.*,it.item_type_name,uom.uom_name FROM item_mst IM ");
            sb.Append(" INNER JOIN item_type_mst IT ON im.item_type_id=it.item_type_id ");
            sb.Append(" INNER JOIN uom_mst UOM ON im.uom_id=uom.uom_id ");
            sb.Append(" WHERE 1=1 ");

            return sb.ToString();
        }

        public static dcITEM_MST GetItemByItemId(int pItemId, DBContext dc)
        {
            dcITEM_MST cObjList = new dcITEM_MST();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetItemMstSQLString());

                if (pItemId > 0)
                {
                    sb.Append(" AND IM.ITEM_ID= @pItemId ");
                    cmdInfo.DBParametersInfo.Add("@pItemId", pItemId);
                }


                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcITEM_MST>(dbq, dc).FirstOrDefault();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcITEM_MST> GetItemList(clsPrmWREL prmObj, DBContext dc)
        {
            List<dcITEM_MST> cObjList = new List<dcITEM_MST>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetItemMstSQLString());

                if (prmObj.Status != "0")
                {
                    sb.Append(" AND im.IS_ACTIVE = @Status ");
                    cmdInfo.DBParametersInfo.Add("@Status", prmObj.Status);
                }
                if(prmObj.ITEM_NAME!="")
                {
                    sb.Append(" AND UPPER(IM.ITEM_NAME) LIKE UPPER('%" + prmObj.ITEM_NAME + "%') ");
                    //sb.Append(" AND UPPER(IM.ITEM_NAME) Like UPPER('%" + prmObj.ITEM_NAME) + "%') ");
                   
                }
                if (prmObj.Item_Type_ID >0)
                {
                    sb.Append(" AND IM.ITEM_TYPE_ID = @Item_Type_ID ");
                    cmdInfo.DBParametersInfo.Add("@Item_Type_ID", prmObj.Item_Type_ID);

                }

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcITEM_MST>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcITEM_MST> GetITEM_MSTList()
        {
            return GetITEM_MSTList(null, null);
        }
        public static List<dcITEM_MST> GetITEM_MSTList(DBContext dc)
        {
            return GetITEM_MSTList(null, dc);
        }
        public static List<dcITEM_MST> GetITEM_MSTList(DBQuery dbq, DBContext dc)
        {
            List<dcITEM_MST> cObjList = new List<dcITEM_MST>();
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
                    cObjList = DBQuery.ExecuteDBQuery<dcITEM_MST>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcITEM_MST GetITEM_MSTByID(int pITEM_MSTID)
        {
            return GetITEM_MSTByID(pITEM_MSTID, null);
        }
        public static dcITEM_MST GetITEM_MSTByID(int pITEM_MSTID, DBContext dc)
        {
            dcITEM_MST cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    var result = (from c in dataContext.GetTable<dcITEM_MST>()
                                  where c.ITEM_ID == pITEM_MSTID
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

        public static int Insert(dcITEM_MST cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcITEM_MST cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcITEM_MST>(cObj, true);
                if (id > 0) { cObj.ITEM_ID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcITEM_MST cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcITEM_MST cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcITEM_MST>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pITEM_MSTID)
        {
            return Delete(pITEM_MSTID, null);
        }
        public static bool Delete(int pITEM_MSTID, DBContext dc)
        {
            dcITEM_MST cObj = new dcITEM_MST();
            cObj.ITEM_ID = pITEM_MSTID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcITEM_MST>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcITEM_MST cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcITEM_MST cObj, bool isAdd, DBContext dc)
        {
            cObj._RecordState = isAdd ? RecordStateEnum.Added : RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcITEM_MST cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcITEM_MST cObj, DBContext dc)
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
                                newID = cObj.ITEM_ID;
                            }
                            break;
                        case RecordStateEnum.Deleted:
                            if (Delete(cObj.ITEM_ID, dc))
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

        public static bool SaveList(List<dcITEM_MST> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcITEM_MST> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcITEM_MST oDet in detList)
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
                    //    bool d = Delete(oDet.ITEM_MSTID, dc);
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

        public static bool IsItemNameExists(string pItemName)
        {
            return IsItemNameExists(pItemName, null);
        }
        public static bool IsItemNameExists(string pItemName, DBContext dc)
        {
            bool isData = false;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetItemMstSQLString());

                sb.Append(" AND UPPER(im.ITEM_NAME)=UPPER(@itemName) ");
                cmdInfo.DBParametersInfo.Add("@itemName", pItemName);



                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                isData = GetITEM_MSTList(dbq, dc).Count > 0;

            }
            finally
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }
            return isData;
        }
        public static bool IsItemNameExists(string pItemName, int pItemID)
        {
            return IsItemNameExists(pItemName, pItemID, null);
        }
        public static bool IsItemNameExists(string pItemName, int pItemID, DBContext dc)
        {
            bool isData = false;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetItemMstSQLString());

                sb.Append(" AND UPPER(im.ITEM_NAME)=UPPER(@itemName) ");
                cmdInfo.DBParametersInfo.Add("@itemName", pItemName);


                sb.Append(" AND im.ITEM_ID <> @itemID ");
                cmdInfo.DBParametersInfo.Add("@itemID", pItemID);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                isData = GetITEM_MSTList(dbq, dc).Count > 0;
            }
            finally
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }
            return isData;
        }


        public static string getItemIDByItemName(string pitemName, DBContext dc)
        {
            bool isDCInit = false;
            string AutoStatus = string.Empty;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder("SELECT   ITEM_ID FROM ITEM_MST Where UPPER(ITEM_NAME)=UPPER(@pitemName) ");
                cmdInfo.DBParametersInfo.Add("@pitemName", pitemName);


                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                AutoStatus = Convert.ToString(DBQuery.ExecuteDBScalar(dbq, dc));
               
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return AutoStatus;
        }
    }
}
