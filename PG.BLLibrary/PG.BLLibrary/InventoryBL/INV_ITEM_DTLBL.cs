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
  public  class INV_ITEM_DTLBL
    {
        public static string GetInv_ItemDtl_SqlString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" Select * FROM INV_ITEM_DTL where 1=1 ");
            return sb.ToString();
        }

        public static DataLoadOptions Inv_ItemDtlLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcELECTROLYTE_GRAVITY>(obj => obj.relatedclassname);
            return dlo;
        }
        public static List<dcINV_ITEM_DTL> Inv_ItemDtl_List()
        {
            return Inv_ItemDtl_List(null, null, null,null);
        }
        public static List<dcINV_ITEM_DTL> Inv_ItemDtl_List(string storeId)
        {
            return Inv_ItemDtl_List(null, null, storeId, null);
        }
        public static List<dcINV_ITEM_DTL> Inv_ItemDtl_List(DBContext dc)
        {
            return Inv_ItemDtl_List(null, dc, null, null);
        }

        public static List<dcINV_ITEM_DTL> Inv_ItemDtl_List(DBQuery dbq, DBContext dc, string storeId,string groupId)
        {
            List<dcINV_ITEM_DTL> cObjList = new List<dcINV_ITEM_DTL>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {

                    DBCommandInfo cmdInfo = new DBCommandInfo();
                    StringBuilder sb = new StringBuilder(GetInv_ItemDtl_SqlString());

                    if (!String.IsNullOrEmpty(storeId))
                    {
                        sb.Append("and STORE_ID=@StoreId");
                        cmdInfo.DBParametersInfo.Add("@StoreId", storeId);
                    }

                    if (!String.IsNullOrEmpty(groupId))
                    {
                        sb.Append("and ITEM_GROUP_ID=@GroupId");
                        cmdInfo.DBParametersInfo.Add("@GroupId", groupId);
                    }
                    sb.Append(" order by ITEM_DESC asc");

                    dbq = new DBQuery();
                    dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                    cmdInfo.CommandText = sb.ToString();
                    cmdInfo.CommandType = CommandType.Text;
                    dbq.DBCommandInfo = cmdInfo;
                    cObjList = DBQuery.ExecuteDBQuery<dcINV_ITEM_DTL>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static dcINV_ITEM_DTL GetInv_ItemDtl_By_Id(int id)
        {
            return GetInv_ItemDtl_By_Id(id, null);
        }
        public static dcINV_ITEM_DTL GetInv_ItemDtl_By_Id(int id, DBContext dc)
        {
            dcINV_ITEM_DTL cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    var result = (from c in dataContext.GetTable<dcINV_ITEM_DTL>()
                                  //where c.ELECTROLYTE_GRAVITYID == pELECTROLYTE_GRAVITYID
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



        public static string New_Item_Id(string compId, string catId,string subCatId, string desc, DBContext dc)
        {
            bool isDCInit = false;
            string rak_no = string.Empty;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                string abbr = " SELECT FN_NEW_ITEM_CODE(@COMPID,@CATID,@SUBCATID,@ITEM_DESC) A from Dual ";
                cmdInfo.DBParametersInfo.Add("@COMPID", compId);
                cmdInfo.DBParametersInfo.Add("@CATID", catId);
                cmdInfo.DBParametersInfo.Add("@SUBCATID", subCatId);
                cmdInfo.DBParametersInfo.Add("@ITEM_DESC", desc);
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = abbr;
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                rak_no = Convert.ToString(DBQuery.ExecuteDBScalar(dbq, dc));
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return rak_no;
        }



        public static int Insert(dcINV_ITEM_DTL cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcINV_ITEM_DTL cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcINV_ITEM_DTL>(cObj, false);
                //if (id > 0) { cObj.ELECTROLYTE_GRAVITYID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcINV_ITEM_DTL cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcINV_ITEM_DTL cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;

            dcINV_ITEM_DTL key = new dcINV_ITEM_DTL();
            key.ITEM_ID = cObj.ITEM_ID;

            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcINV_ITEM_DTL>(cObj, key);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pELECTROLYTE_GRAVITYID)
        {
            return Delete(pELECTROLYTE_GRAVITYID, null);
        }
        public static bool Delete(int pELECTROLYTE_GRAVITYID, DBContext dc)
        {
            dcINV_ITEM_DTL cObj = new dcINV_ITEM_DTL();
            //cObj.ELECTROLYTE_GRAVITYID = pELECTROLYTE_GRAVITYID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcINV_ITEM_DTL>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcINV_ITEM_DTL cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcINV_ITEM_DTL cObj, bool isAdd, DBContext dc)
        {
            //cObj._RecordState = isAdd ? Interwave.Core.DBClass.RecordStateEnum.Added : Interwave.Core.DBClass.RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcINV_ITEM_DTL cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcINV_ITEM_DTL cObj, DBContext dc)
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
                        /*
                    case Interwave.Core.DBClass.RecordStateEnum.Added:
                        newID = Insert(cObj, dc);
                        break;
                    case Interwave.Core.DBClass.RecordStateEnum.Edited:
                        if (Update(cObj, dc))
                        {
                            newID = cObj.ELECTROLYTE_GRAVITYID;
                        }
                        break;
                    case Interwave.Core.DBClass.RecordStateEnum.Deleted:
                        if (Delete(cObj.ELECTROLYTE_GRAVITYID, dc))
                        {
                            newID = 1;
                        }
                        break; */
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

        public static bool SaveList(List<dcINV_ITEM_DTL> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcINV_ITEM_DTL> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcINV_ITEM_DTL oDet in detList)
            {
                switch (oDet._RecordState)
                {
                    /* case Interwave.Core.DBClass.RecordStateEnum.Added:
                         int a = Insert(oDet, dc);
                         break;
                     case Interwave.Core.DBClass.RecordStateEnum.Edited:
                         bool e = Update(oDet, dc);
                         break;
                     case Interwave.Core.DBClass.RecordStateEnum.Deleted:
                         bool d = Delete(oDet.ELECTROLYTE_GRAVITYID, dc);
                         break; */
                    default:
                        break;
                }
            }
            dc.CommitTransaction(isTransInit);
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            bStatus = true;
            return bStatus;
        }

   
      

        public static bool Is_Same_Name_Exist(string itemName)
        {
            throw new NotImplementedException();
        }
    }
}
