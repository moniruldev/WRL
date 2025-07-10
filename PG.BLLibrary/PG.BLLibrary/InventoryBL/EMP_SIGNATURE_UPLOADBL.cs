using PG.Core.DBBase;
using PG.DBClass.InventoryDC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.BLLibrary.InventoryBL
{
    public class EMP_SIGNATURE_UPLOADBL
    {
        public static DataLoadOptions EMP_SIGNATURE_UPLOADLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcEMP_SIGNATURE_UPLOAD>(obj => obj.relatedclassname);
            return dlo;
        }
        public static List<dcEMP_SIGNATURE_UPLOAD> GetEMP_SIGNATURE_UPLOADList()
        {
            return GetEMP_SIGNATURE_UPLOADList(null, null);
        }
        public static List<dcEMP_SIGNATURE_UPLOAD> GetEMP_SIGNATURE_UPLOADList(DBContext dc)
        {
            return GetEMP_SIGNATURE_UPLOADList(null, dc);
        }
        public static List<dcEMP_SIGNATURE_UPLOAD> GetEMP_SIGNATURE_UPLOADList(DBQuery dbq, DBContext dc)
        {
            List<dcEMP_SIGNATURE_UPLOAD> cObjList = new List<dcEMP_SIGNATURE_UPLOAD>();
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
                    cObjList = DBQuery.ExecuteDBQuery<dcEMP_SIGNATURE_UPLOAD>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcEMP_SIGNATURE_UPLOAD GetEMP_SIGNATURE_UPLOADByID(int pEMP_SIGNATURE_UPLOADID)
        {
            return GetEMP_SIGNATURE_UPLOADByID(pEMP_SIGNATURE_UPLOADID, null);
        }
        public static dcEMP_SIGNATURE_UPLOAD GetEMP_SIGNATURE_UPLOADByID(int pEMP_SIGNATURE_UPLOADID, DBContext dc)
        {
            dcEMP_SIGNATURE_UPLOAD cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    var result = (from c in dataContext.GetTable<dcEMP_SIGNATURE_UPLOAD>()
                                  //where c.EMP_SIGNATURE_UPLOADID == pEMP_SIGNATURE_UPLOADID
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

        public static int Insert(dcEMP_SIGNATURE_UPLOAD cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcEMP_SIGNATURE_UPLOAD cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcEMP_SIGNATURE_UPLOAD>(cObj, true);
                //if (id > 0) { cObj.EMP_SIGNATURE_UPLOADID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcEMP_SIGNATURE_UPLOAD cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcEMP_SIGNATURE_UPLOAD cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcEMP_SIGNATURE_UPLOAD>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pEMP_SIGNATURE_UPLOADID)
        {
            return Delete(pEMP_SIGNATURE_UPLOADID, null);
        }
        public static bool Delete(int pEMP_SIGNATURE_UPLOADID, DBContext dc)
        {
            dcEMP_SIGNATURE_UPLOAD cObj = new dcEMP_SIGNATURE_UPLOAD();
            //cObj.EMP_SIGNATURE_UPLOADID = pEMP_SIGNATURE_UPLOADID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcEMP_SIGNATURE_UPLOAD>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcEMP_SIGNATURE_UPLOAD cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcEMP_SIGNATURE_UPLOAD cObj, bool isAdd, DBContext dc)
        {
            //cObj._RecordState = isAdd ? Interwave.Core.DBClass.RecordStateEnum.Added : Interwave.Core.DBClass.RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcEMP_SIGNATURE_UPLOAD cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcEMP_SIGNATURE_UPLOAD cObj, DBContext dc)
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
                        //case Interwave.Core.DBClass.RecordStateEnum.Added:
                        //    newID = Insert(cObj, dc);
                        //    break;
                        //case Interwave.Core.DBClass.RecordStateEnum.Edited:
                        //    if (Update(cObj, dc))
                        //    {
                        //        newID = cObj.EMP_SIGNATURE_UPLOADID;
                        //    }
                        //    break;
                        //case Interwave.Core.DBClass.RecordStateEnum.Deleted:
                        //    if (Delete(cObj.EMP_SIGNATURE_UPLOADID, dc))
                        //    {
                        //        newID = 1;
                        //    }
                        //    break;
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

        public static bool SaveList(List<dcEMP_SIGNATURE_UPLOAD> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcEMP_SIGNATURE_UPLOAD> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcEMP_SIGNATURE_UPLOAD oDet in detList)
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
                    //    bool d = Delete(oDet.EMP_SIGNATURE_UPLOADID, dc);
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

        public static List<dcEMP_SIGNATURE_UPLOAD> GetHRPDFUploadFile_ByID(int FORM_TYPE_ID, DBContext dc)
        {
            List<dcEMP_SIGNATURE_UPLOAD> cObjList = new List<dcEMP_SIGNATURE_UPLOAD>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    DBCommandInfo cmdInfo = new DBCommandInfo();
                    StringBuilder sb = new StringBuilder();


                    sb.Append(" SELECT EMP_SIGN_ID,EMP_ID,EMP_NAME,SIGN_PHOTO,STATUS,CREATE_BY,CREATE_DATE,UPLOAD_DATE  ");
                        sb.Append(" FROM EMP_SIGNATURE_UPLOAD   ");
                        
                        sb.Append(" WHERE 1=1 ");

                        //sb.Append(" AND b.FORM_TYPE_ID=" + FORM_TYPE_ID + " ");

                   
                    
                    //sb.Append(" AND HR_SAL_APP_FILE_UPLOAD.HR_APP_NO=@pHR_APP_NO ");
                    //cmdInfo.DBParametersInfo.Add("@pHR_APP_NO", pHR_APP_NO);

                        sb.Append(" ORDER BY EMP_NAME ");

                    DBQuery dbq = new DBQuery();
                    dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                    cmdInfo.CommandText = sb.ToString();
                    cmdInfo.CommandType = CommandType.Text;
                    dbq.DBCommandInfo = cmdInfo;
                    cObjList = DBQuery.ExecuteDBQuery<dcEMP_SIGNATURE_UPLOAD>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

    }
}
