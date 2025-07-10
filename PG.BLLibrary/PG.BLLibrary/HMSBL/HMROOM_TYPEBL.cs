using PG.Core.DBBase;
using PG.DBClass.HMSDC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.BLLibrary.HMSBL
{
    public class HMROOM_TYPEBL
    {
        public static DataLoadOptions HMROOM_TYPELoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcHMROOM_TYPE>(obj => obj.relatedclassname);
            return dlo;
        }

        public static string GetRoomTypeListString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" SELECT * FROM HMROOM_TYPE RT ");
            sb.Append(" WHERE 1=1 ");
          

            return sb.ToString();
        }

        public static List<dcHMROOM_TYPE> GetRoomTypeList()
        {
            return GetRoomTypeListById(0);
        }

        public static dcHMROOM_TYPE GetRoomTypeInfoById(int pRoomTypeId)
        {
            return GetRoomTypeListById(pRoomTypeId, null).FirstOrDefault();
        }

        public static List<dcHMROOM_TYPE> GetRoomTypeListById(int pRoomTypeId)
        {
            return GetRoomTypeListById(pRoomTypeId, null);
        }

        public static List<dcHMROOM_TYPE> GetRoomTypeListById(int pRoomTypeId, DBContext dc)
        {
            List<dcHMROOM_TYPE> cObjList = new List<dcHMROOM_TYPE>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetRoomTypeListString());
                if (pRoomTypeId > 0)
                {
                    sb.Append(" AND RT.ROOM_TYPE_ID= @pRoomTypeId ");
                    cmdInfo.DBParametersInfo.Add("@pRoomTypeId", pRoomTypeId);
                }
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcHMROOM_TYPE>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcHMROOM_TYPE> GetRoomTypeList(dcPrmHMS prmHms, DBContext dc)
        {
            List<dcHMROOM_TYPE> cObjList = new List<dcHMROOM_TYPE>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetRoomTypeListString());
                if (prmHms.TRANS_ID > 0)
                {
                    sb.Append(" AND RT.ROOM_TYPE_ID= @TRANS_ID ");
                    cmdInfo.DBParametersInfo.Add("@TRANS_ID", prmHms.TRANS_ID);
                }

                if (prmHms.TITLE != "")
                {
                    sb.Append(" AND UPPER(RT.TITLE) LIKE UPPER('%" + prmHms.TITLE + "%') ");
                }

                if (prmHms.MAX_PERSON > 0)
                {
                    sb.Append(" AND RT.MAX_PERSON = " + prmHms.MAX_PERSON +"  ");
                }


                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcHMROOM_TYPE>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static List<dcHMROOM_TYPE> GetHMROOM_TYPEList()
        {
            return GetHMROOM_TYPEList(null, null);
        }
        public static List<dcHMROOM_TYPE> GetHMROOM_TYPEList(DBContext dc)
        {
            return GetHMROOM_TYPEList(null, dc);
        }
        public static List<dcHMROOM_TYPE> GetHMROOM_TYPEList(DBQuery dbq, DBContext dc)
        {
            List<dcHMROOM_TYPE> cObjList = new List<dcHMROOM_TYPE>();
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
                    cObjList = DBQuery.ExecuteDBQuery<dcHMROOM_TYPE>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcHMROOM_TYPE GetHMROOM_TYPEByID(int pHMROOM_TYPEID)
        {
            return GetHMROOM_TYPEByID(pHMROOM_TYPEID, null);
        }
        public static dcHMROOM_TYPE GetHMROOM_TYPEByID(int pHMROOM_TYPEID, DBContext dc)
        {
            dcHMROOM_TYPE cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    var result = (from c in dataContext.GetTable<dcHMROOM_TYPE>()
                                  where c.ROOM_TYPE_ID == pHMROOM_TYPEID
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

        public static int Insert(dcHMROOM_TYPE cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcHMROOM_TYPE cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcHMROOM_TYPE>(cObj, true);
                if (id > 0) { cObj.ROOM_TYPE_ID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcHMROOM_TYPE cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcHMROOM_TYPE cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcHMROOM_TYPE>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pHMROOM_TYPEID)
        {
            return Delete(pHMROOM_TYPEID, null);
        }
        public static bool Delete(int pHMROOM_TYPEID, DBContext dc)
        {
            dcHMROOM_TYPE cObj = new dcHMROOM_TYPE();
            cObj.ROOM_TYPE_ID = pHMROOM_TYPEID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcHMROOM_TYPE>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcHMROOM_TYPE cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcHMROOM_TYPE cObj, bool isAdd, DBContext dc)
        {
            cObj._RecordState = isAdd ? RecordStateEnum.Added : RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcHMROOM_TYPE cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcHMROOM_TYPE cObj, DBContext dc)
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
                                newID = cObj.ROOM_TYPE_ID;
                            }
                            break;
                        case RecordStateEnum.Deleted:
                            if (Delete(cObj.ROOM_TYPE_ID, dc))
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

        public static bool SaveList(List<dcHMROOM_TYPE> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcHMROOM_TYPE> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcHMROOM_TYPE oDet in detList)
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
                        bool d = Delete(oDet.ROOM_TYPE_ID, dc);
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
