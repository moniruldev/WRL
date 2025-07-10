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
   public class INV_ITEM_TYPEBL
    {

        public static string GetItemType_info_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" Select INV_ITEM_TYPE.* FROM INV_ITEM_TYPE where 1=1 ");
            return sb.ToString();
        }

        public static List<dcINV_ITEM_TYPE> Inv_Item_Type_List()
        {
            return Inv_Item_Type_List(null, null, null);
        }
        public static List<dcINV_ITEM_TYPE> Inv_Item_Type_List(DBContext dc)
        {
            return Inv_Item_Type_List(null, dc, null);
        }
        public static List<dcINV_ITEM_TYPE> Inv_Item_Type_List(string rackId)
        {
            return Inv_Item_Type_List(null, null, rackId);
        }
        public static List<dcINV_ITEM_TYPE> Inv_Item_Type_List(DBQuery dbq, DBContext dc, string typeId)
        {
            List<dcINV_ITEM_TYPE> cObjList = new List<dcINV_ITEM_TYPE>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {

                    DBCommandInfo cmdInfo = new DBCommandInfo();
                    StringBuilder sb = new StringBuilder(GetItemType_info_SQLString());
                    //if (!String.IsNullOrEmpty(typeId))
                    //{
                    //    sb.Append("Where COMPANY_ID=@COMPANY_ID");
                    //    cmdInfo.DBParametersInfo.Add("@COMPANY_ID", typeId);
                    //}

                    dbq = new DBQuery();
                    dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                    cmdInfo.CommandText = sb.ToString();
                    cmdInfo.CommandType = CommandType.Text;
                    dbq.DBCommandInfo = cmdInfo;
                    cObjList = DBQuery.ExecuteDBQuery<dcINV_ITEM_TYPE>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static dcINV_ITEM_TYPE Get_Inv_Item_Type_By_Id(int id)
        {
            return Get_Inv_Item_Type_By_Id(id, null);
        }
        public static dcINV_ITEM_TYPE Get_Inv_Item_Type_By_Id(int id, DBContext dc)
        {
            dcINV_ITEM_TYPE cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    var result = (from c in dataContext.GetTable<dcINV_ITEM_TYPE>()
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

        public static List<dcINV_ITEM_TYPE> Get_Inv_Item_Type_List(DBQuery dbq, DBContext dc)
        {
            List<dcINV_ITEM_TYPE> cObjList = new List<dcINV_ITEM_TYPE>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                if (dbq == null)
                {
                    dbq = new DBQuery();
                    //dbq.OrderBy = "PeriodStartDate";
                }
                cObjList = DBQuery.ExecuteDBQuery<dcINV_ITEM_TYPE>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static int Insert(dcINV_ITEM_TYPE cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcINV_ITEM_TYPE cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcINV_ITEM_TYPE>(cObj, false);
                //if (id > 0) { cObj.ELECTROLYTE_GRAVITYID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcINV_ITEM_TYPE cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcINV_ITEM_TYPE cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;

            dcINV_ITEM_TYPE key = new dcINV_ITEM_TYPE();
            key.ITEM_TYPE_ID = cObj.ITEM_TYPE_ID;

            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcINV_ITEM_TYPE>(cObj, key);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int id)
        {
            return Delete(id, null);
        }
        public static bool Delete(int id, DBContext dc)
        {
            dcINV_ITEM_TYPE cObj = new dcINV_ITEM_TYPE();
            //cObj.ELECTROLYTE_GRAVITYID = pELECTROLYTE_GRAVITYID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcINV_ITEM_TYPE>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcINV_ITEM_TYPE cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcINV_ITEM_TYPE cObj, bool isAdd, DBContext dc)
        {
            //cObj._RecordState = isAdd ? Interwave.Core.DBClass.RecordStateEnum.Added : Interwave.Core.DBClass.RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcINV_ITEM_TYPE cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcINV_ITEM_TYPE cObj, DBContext dc)
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

        public static bool SaveList(List<dcINV_ITEM_TYPE> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcINV_ITEM_TYPE> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcINV_ITEM_TYPE oDet in detList)
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

    }
}
