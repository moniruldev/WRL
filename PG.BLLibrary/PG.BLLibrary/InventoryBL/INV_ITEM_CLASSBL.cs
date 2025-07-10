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
   public class INV_ITEM_CLASSBL
    {

       public static string Inv_Item_Class_SqlString()
       {
           StringBuilder sb = new StringBuilder();
           sb.Append("SELECT INV_ITEM_CLASS.* ");
           sb.Append(" FROM INV_ITEM_CLASS ");
           sb.Append(" WHERE 1=1 ");
           return sb.ToString();
       }

        public static DataLoadOptions Inv_Item_Class_LoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcELECTROLYTE_GRAVITY>(obj => obj.relatedclassname);
            return dlo;
        }
        public static List<dcINV_ITEM_CLASS> Inv_Item_Class_List()
        {
            return Inv_Item_Class_List(null, null);
        }
     
        public static List<dcINV_ITEM_CLASS> Inv_Item_Class_List(DBContext dc)
        {
            return Inv_Item_Class_List(null, dc);
        }
        public static List<dcINV_ITEM_CLASS> Inv_Item_Class_List(DBQuery dbq, DBContext dc)
        {
            List<dcINV_ITEM_CLASS> cObjList = new List<dcINV_ITEM_CLASS>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    if (dbq == null)
                    {

                        DBCommandInfo cmdInfo = new DBCommandInfo();
                        StringBuilder sb = new StringBuilder(Inv_Item_Class_SqlString());


                        dbq = new DBQuery();
                        dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                        cmdInfo.CommandText = sb.ToString();
                        cmdInfo.CommandType = CommandType.Text;
                        dbq.DBCommandInfo = cmdInfo;
                    }
                    cObjList = DBQuery.ExecuteDBQuery<dcINV_ITEM_CLASS>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcINV_ITEM_CLASS Get_Inv_Item_Class_By_Id(int id)
        {
            return Get_Inv_Item_Class_By_Id(id, null);
        }
        public static dcINV_ITEM_CLASS Get_Inv_Item_Class_By_Id(int id, DBContext dc)
        {
            dcINV_ITEM_CLASS cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    var result = (from c in dataContext.GetTable<dcINV_ITEM_CLASS>()
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


        public static int Insert(dcINV_ITEM_CLASS cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcINV_ITEM_CLASS cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcINV_ITEM_CLASS>(cObj, false);
                //if (id > 0) { cObj.ELECTROLYTE_GRAVITYID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcINV_ITEM_CLASS cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcINV_ITEM_CLASS cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;

            dcINV_ITEM_CLASS key = new dcINV_ITEM_CLASS();
          //  key.ITEM_ID = cObj.ITEM_ID;

            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcINV_ITEM_CLASS>(cObj, key);
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
            dcINV_ITEM_CLASS cObj = new dcINV_ITEM_CLASS();
            //cObj.ELECTROLYTE_GRAVITYID = pELECTROLYTE_GRAVITYID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcINV_ITEM_CLASS>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcINV_ITEM_CLASS cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcINV_ITEM_CLASS cObj, bool isAdd, DBContext dc)
        {
            //cObj._RecordState = isAdd ? Interwave.Core.DBClass.RecordStateEnum.Added : Interwave.Core.DBClass.RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcINV_ITEM_CLASS cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcINV_ITEM_CLASS cObj, DBContext dc)
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

        public static bool SaveList(List<dcINV_ITEM_CLASS> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcINV_ITEM_CLASS> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcINV_ITEM_CLASS oDet in detList)
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

        public static dcINV_ITEM_CLASS GetRectifierParameterInfo(string pBSRNo)
        {
            return GetRectifierParameterInfo(pBSRNo, null);
        }
        public static dcINV_ITEM_CLASS GetRectifierParameterInfo(string pBSRNo, DBContext dc)
        {

            bool isDCInit = false;
            dcINV_ITEM_CLASS cObj = null;

            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();
                sb.Append(" Select * from INV_RACK");
                sb.Append(" WHERE RACK_ID=@RACK_ID ");
                cmdInfo.DBParametersInfo.Add("@RACK_ID", pBSRNo);
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObj = DBQuery.ExecuteDBQuery<dcINV_ITEM_CLASS>(dbq, dc).FirstOrDefault();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObj;
        }

        public static bool Is_Same_Name_Exist(string itemName)
        {
            throw new NotImplementedException();
        }
    }
}
