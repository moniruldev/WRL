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
    public class TBLUSERDEPARTMENTBL
    {
        public static DataLoadOptions TBLUSERDEPARTMENTLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcTBLUSERDEPARTMENT>(obj => obj.relatedclassname);
            return dlo;
        }


        public static List<dcTBLUSERDEPARTMENT> GetDepartmentByUserId(int usrId)
        {
            return GetDepartmentByUserId(usrId, null);
        }
        public static List<dcTBLUSERDEPARTMENT> GetDepartmentByUserId(int usrId, DBContext dc)
        {
            List<dcTBLUSERDEPARTMENT> cObjList = new List<dcTBLUSERDEPARTMENT>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetDepartmentByUserId_SQLString());
                if (usrId > 0)
                {
                    sb.Append(" and USERID=@USERID");
                    cmdInfo.DBParametersInfo.Add("@USERID", usrId);
                }
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcTBLUSERDEPARTMENT>(dbq, dc).ToList();

            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }

            return cObjList;
        }
        public static string GetDepartmentByUserId_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT TBLUSERDEPARTMENT.*,DEPARTMENT_INFO.DEPARTMENT_NAME,DEPARTMENT_INFO.DEPARTMENT_CODE,DEPARTMENT_INFO.DEPARTMENT_ID,DEPARTMENT_INFO.IS_STORE ");
            sb.Append(" FROM TBLUSERDEPARTMENT ");
            sb.Append(" inner join DEPARTMENT_INFO on TBLUSERDEPARTMENT.DEPTID=DEPARTMENT_INFO.DEPARTMENT_ID ");
            sb.Append(" WHERE (1=1) ");
            sb.Append(" AND DEPARTMENT_INFO.IS_ACTIVE ='Y' ");
            
            return sb.ToString();
        }

        public static string GetDeptListSQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" select TBLUSERDEPARTMENT.* from TBLUSERDEPARTMENT where 1=1 ");
            //sb.Append("  ");
            return sb.ToString();
        }


        public static List<dcTBLUSERDEPARTMENT> GetDept_ID(int pDeptID, int pUserID, DBContext dc)
        {
            List<dcTBLUSERDEPARTMENT> cObjList = new List<dcTBLUSERDEPARTMENT>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetDepartmentByUserId_SQLString());
                if (pDeptID > 0)
                {
                    sb.Append(" AND TBLUSERDEPARTMENT.DEPTID=@pDeptID ");
                    cmdInfo.DBParametersInfo.Add("@pDeptID", pDeptID);
                }

                if (pUserID > 0)
                {
                    sb.Append(" AND TBLUSERDEPARTMENT.USERID=@pUserID ");
                    cmdInfo.DBParametersInfo.Add("@pUserID", pUserID);
                }


                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcTBLUSERDEPARTMENT>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }



        public static List<dcTBLUSERDEPARTMENT> GetTBLUSERDEPARTMENTList()
        {
            return GetTBLUSERDEPARTMENTList(null, null);
        }
        public static List<dcTBLUSERDEPARTMENT> GetTBLUSERDEPARTMENTList(DBContext dc)
        {
            return GetTBLUSERDEPARTMENTList(null, dc);
        }
        public static List<dcTBLUSERDEPARTMENT> GetTBLUSERDEPARTMENTList(DBQuery dbq, DBContext dc)
        {
            List<dcTBLUSERDEPARTMENT> cObjList = new List<dcTBLUSERDEPARTMENT>();
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
                    cObjList = DBQuery.ExecuteDBQuery<dcTBLUSERDEPARTMENT>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcTBLUSERDEPARTMENT GetTBLUSERDEPARTMENTByID(int pTBLUSERDEPARTMENTID)
        {
            return GetTBLUSERDEPARTMENTByID(pTBLUSERDEPARTMENTID, null);
        }
        public static dcTBLUSERDEPARTMENT GetTBLUSERDEPARTMENTByID(int pTBLUSERDEPARTMENTID, DBContext dc)
        {
            dcTBLUSERDEPARTMENT cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    var result = (from c in dataContext.GetTable<dcTBLUSERDEPARTMENT>()
                                  where c.USERDEPTID == pTBLUSERDEPARTMENTID
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

        public static int Insert(dcTBLUSERDEPARTMENT cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcTBLUSERDEPARTMENT cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcTBLUSERDEPARTMENT>(cObj, true);
                if (id > 0) { cObj.USERDEPTID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcTBLUSERDEPARTMENT cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcTBLUSERDEPARTMENT cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcTBLUSERDEPARTMENT>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pTBLUSERDEPARTMENTID)
        {
            return Delete(pTBLUSERDEPARTMENTID, null);
        }
        public static bool Delete(int pTBLUSERDEPARTMENTID, DBContext dc)
        {
            dcTBLUSERDEPARTMENT cObj = new dcTBLUSERDEPARTMENT();
            cObj.USERDEPTID = pTBLUSERDEPARTMENTID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcTBLUSERDEPARTMENT>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcTBLUSERDEPARTMENT cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcTBLUSERDEPARTMENT cObj, bool isAdd, DBContext dc)
        {
            cObj._RecordState = isAdd ? RecordStateEnum.Added : RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcTBLUSERDEPARTMENT cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcTBLUSERDEPARTMENT cObj, DBContext dc)
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
                                newID = cObj.USERDEPTID;
                            }
                            break;
                        case RecordStateEnum.Deleted:
                            if (Delete(cObj.USERDEPTID, dc))
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

        public static bool SaveList(List<dcTBLUSERDEPARTMENT> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcTBLUSERDEPARTMENT> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcTBLUSERDEPARTMENT oDet in detList)
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
                        bool d = Delete(oDet.USERDEPTID, dc);
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


    //public class TBLUSERDEPARTMENTBL
    //{



    //    //public static List<dcTBLUSERDEPARTMENT> GetDepartmentByUserId(int usrId)
    //    //{
    //    //    return GetDepartmentByUserId(usrId, null);
    //    //}
    //    //public static List<dcTBLUSERDEPARTMENT> GetDepartmentByUserId(int usrId, DBContext dc)
    //    //{
    //    //    List<dcTBLUSERDEPARTMENT> cObjList = new List<dcTBLUSERDEPARTMENT>();
    //    //    bool isDCInit = false;
    //    //    try
    //    //    {
    //    //        isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

    //    //        DBCommandInfo cmdInfo = new DBCommandInfo();
    //    //        StringBuilder sb = new StringBuilder(GetDepartmentByUserId_SQLString());
    //    //        if (usrId>0)
    //    //        {
    //    //            sb.Append(" and USERID=@USERID");
    //    //            cmdInfo.DBParametersInfo.Add("@USERID", usrId);
    //    //        }
    //    //        DBQuery dbq = new DBQuery();
    //    //        dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
    //    //        cmdInfo.CommandText = sb.ToString();
    //    //        cmdInfo.CommandType = CommandType.Text;
    //    //        dbq.DBCommandInfo = cmdInfo;

    //    //        cObjList = DBQuery.ExecuteDBQuery<dcTBLUSERDEPARTMENT>(dbq, dc).ToList();

    //    //    }
    //    //    catch { throw; }
    //    //    finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }

    //    //    return cObjList;
    //    //}
    //    //public static string GetDepartmentByUserId_SQLString()
    //    //{
    //    //    StringBuilder sb = new StringBuilder();
    //    //    sb.Append(" SELECT TBLUSERDEPARTMENT.*,DEPARTMENT_INFO.DEPARTMENT_NAME,DEPARTMENT_INFO.DEPARTMENT_CODE,DEPARTMENT_INFO.DEPARTMENT_ID ");
    //    //    sb.Append(" FROM TBLUSERDEPARTMENT ");
    //    //    sb.Append(" inner join DEPARTMENT_INFO on TBLUSERDEPARTMENT.DEPTID=DEPARTMENT_INFO.DEPARTMENT_ID ");
    //    //    sb.Append(" WHERE (1=1) ");
    //    //    return sb.ToString();
    //    //}


    //    //public static DataLoadOptions TBLUSERDEPARTMENTLoadOptions()
    //    //{
    //    //    DataLoadOptions dlo = new DataLoadOptions();
    //    //    //dlo.LoadWith<DBClass.dcTBLUSERDEPARTMENT>(obj => obj.relatedclassname);
    //    //    return dlo;
    //    //}
    //    //public static List<dcTBLUSERDEPARTMENT> GetTBLUSERDEPARTMENTList()
    //    //{
    //    //    return GetTBLUSERDEPARTMENTList(null, null);
    //    //}
    //    //public static List<dcTBLUSERDEPARTMENT> GetTBLUSERDEPARTMENTList(DBContext dc)
    //    //{
    //    //    return GetTBLUSERDEPARTMENTList(null, dc);
    //    //}
    //    //public static List<dcTBLUSERDEPARTMENT> GetTBLUSERDEPARTMENTList(DBQuery dbq, DBContext dc)
    //    //{
    //    //    List<dcTBLUSERDEPARTMENT> cObjList = new List<dcTBLUSERDEPARTMENT>();
    //    //    bool isDCInit = false;
    //    //    try
    //    //    {
    //    //        isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
    //    //        using (DataContext dataContext = dc.NewDataContext())
    //    //        {
    //    //            if (dbq == null)
    //    //            {
    //    //                dbq = new DBQuery();
    //    //                //dbq.OrderBy = "YearStartDate Desc";
    //    //            }
    //    //            cObjList = DBQuery.ExecuteDBQuery<dcTBLUSERDEPARTMENT>(dbq, dc);
    //    //        }
    //    //    }
    //    //    catch { throw; }
    //    //    finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
    //    //    return cObjList;
    //    //}
    //    //public static dcTBLUSERDEPARTMENT GetTBLUSERDEPARTMENTByID(int pTBLUSERDEPARTMENTID)
    //    //{
    //    //    return GetTBLUSERDEPARTMENTByID(pTBLUSERDEPARTMENTID, null);
    //    //}
    //    //public static dcTBLUSERDEPARTMENT GetTBLUSERDEPARTMENTByID(int pTBLUSERDEPARTMENTID, DBContext dc)
    //    //{
    //    //    dcTBLUSERDEPARTMENT cObj = null;
    //    //    bool isDCInit = false;
    //    //    try
    //    //    {
    //    //        isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
    //    //        using (DataContext dataContext = dc.NewDataContext())
    //    //        {
    //    //            var result = (from c in dataContext.GetTable<dcTBLUSERDEPARTMENT>()
    //    //                          //where c.TBLUSERDEPARTMENTID == pTBLUSERDEPARTMENTID
    //    //                          select c).ToList();
    //    //            if (result.Count() > 0)
    //    //            {
    //    //                cObj = result.First();
    //    //            }
    //    //        }
    //    //    }
    //    //    catch { throw; }
    //    //    finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
    //    //    return cObj;
    //    //}

    //    //public static int Insert(dcTBLUSERDEPARTMENT cObj)
    //    //{
    //    //    return Insert(cObj, null);
    //    //}

    //    //public static int Insert(dcTBLUSERDEPARTMENT cObj, DBContext dc)
    //    //{
    //    //    bool isDCInit = false;
    //    //    int id = 0;
    //    //    isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
    //    //    using (DataContext dataContext = dc.NewDataContext())
    //    //    {
    //    //        id = dc.DoInsert<dcTBLUSERDEPARTMENT>(cObj, true);
    //    //       // if (id > 0) { cObj.TBLUSERDEPARTMENTID = id; }
    //    //    }
    //    //    DBContextManager.ReleaseDBContext(ref dc, isDCInit);
    //    //    return id;
    //    //}

    //    //public static bool Update(dcTBLUSERDEPARTMENT cObj)
    //    //{
    //    //    return Update(cObj, null);
    //    //}

    //    //public static bool Update(dcTBLUSERDEPARTMENT cObj, DBContext dc)
    //    //{
    //    //    bool isDCInit = false;
    //    //    int cnt = 0;
    //    //    isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
    //    //    using (DataContext dataContext = dc.NewDataContext())
    //    //    {
    //    //        cnt = dc.DoUpdate<dcTBLUSERDEPARTMENT>(cObj);
    //    //    }
    //    //    DBContextManager.ReleaseDBContext(ref dc, isDCInit);
    //    //    return cnt > 0;
    //    //}

    //    //public static bool Delete(int pTBLUSERDEPARTMENTID)
    //    //{
    //    //    return Delete(pTBLUSERDEPARTMENTID, null);
    //    //}
    //    //public static bool Delete(int pTBLUSERDEPARTMENTID, DBContext dc)
    //    //{
    //    //    dcTBLUSERDEPARTMENT cObj = new dcTBLUSERDEPARTMENT();
    //    //  //  cObj.TBLUSERDEPARTMENTID = pTBLUSERDEPARTMENTID;
    //    //    bool isDCInit = false;
    //    //    int cnt = 0;
    //    //    isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
    //    //    using (DataContext dataContext = dc.NewDataContext())
    //    //    {
    //    //        cnt = dc.DoDelete<dcTBLUSERDEPARTMENT>(cObj);
    //    //    }
    //    //    DBContextManager.ReleaseDBContext(ref dc, isDCInit);
    //    //    return cnt > 0;
    //    //}

    //    //public static int Save(dcTBLUSERDEPARTMENT cObj, bool isAdd)
    //    //{
    //    //    return Save(cObj, isAdd, null);
    //    //}

    //    //public static int Save(dcTBLUSERDEPARTMENT cObj, bool isAdd, DBContext dc)
    //    //{
    //    //  //  cObj._RecordState = isAdd ? Interwave.Core.DBClass.RecordStateEnum.Added : Interwave.Core.DBClass.RecordStateEnum.Edited;
    //    //    return Save(cObj, dc);
    //    //}

    //    //public static int Save(dcTBLUSERDEPARTMENT cObj)
    //    //{
    //    //    return Save(cObj, null);
    //    //}

    //    //public static int Save(dcTBLUSERDEPARTMENT cObj, DBContext dc)
    //    //{
    //    //    int newID = 0;
    //    //    bool isDCInit = false;
    //    //    bool isTransInit = false;
    //    //    try
    //    //    {
    //    //        isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
    //    //        isTransInit = dc.StartTransaction();
    //    //        using (DataContext dataContext = dc.NewDataContext())
    //    //        {

    //    //            //switch (cObj._RecordState)
    //    //            //{
    //    //            //    case Interwave.Core.DBClass.RecordStateEnum.Added:
    //    //            //        newID = Insert(cObj, dc);
    //    //            //        break;
    //    //            //    case Interwave.Core.DBClass.RecordStateEnum.Edited:
    //    //            //        if (Update(cObj, dc))
    //    //            //        {
    //    //            //            newID = cObj.TBLUSERDEPARTMENTID;
    //    //            //        }
    //    //            //        break;
    //    //            //    case Interwave.Core.DBClass.RecordStateEnum.Deleted:
    //    //            //        if (Delete(cObj.TBLUSERDEPARTMENTID, dc))
    //    //            //        {
    //    //            //            newID = 1;
    //    //            //        }
    //    //            //        break;
    //    //            //    default:
    //    //            //        break;
    //    //            //}

    //    //            if (newID > 0)
    //    //            {
    //    //                bool bStatus = false;

    //    //                ///code list save logic here

    //    //                bStatus = true;
    //    //                if (bStatus)
    //    //                {
    //    //                    dc.CommitTransaction(isTransInit);
    //    //                }
    //    //            }
    //    //        }
    //    //    }
    //    //    catch
    //    //    {
    //    //        dc.RollbackTransaction(isTransInit);
    //    //        throw;
    //    //    }
    //    //    finally
    //    //    {
    //    //        DBContextManager.ReleaseDBContext(ref dc, isDCInit);
    //    //    }
    //    //    return newID;
    //    //}

    //    //public static bool SaveList(List<dcTBLUSERDEPARTMENT> detList)
    //    //{
    //    //    return SaveList(detList, null);
    //    //}

    //    //public static bool SaveList(List<dcTBLUSERDEPARTMENT> detList, DBContext dc)
    //    //{
    //    //    bool bStatus = false;
    //    //    bool isDCInit = false;
    //    //    bool isTransInit = false;
    //    //    isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
    //    //    isTransInit = dc.StartTransaction();
    //    //    foreach (dcTBLUSERDEPARTMENT oDet in detList)
    //    //    {
    //    //        //switch (oDet._RecordState)
    //    //        //{
    //    //        //    case Interwave.Core.DBClass.RecordStateEnum.Added:
    //    //        //        int a = Insert(oDet, dc);
    //    //        //        break;
    //    //        //    case Interwave.Core.DBClass.RecordStateEnum.Edited:
    //    //        //        bool e = Update(oDet, dc);
    //    //        //        break;
    //    //        //    case Interwave.Core.DBClass.RecordStateEnum.Deleted:
    //    //        //        bool d = Delete(oDet.TBLUSERDEPARTMENTID, dc);
    //    //        //        break;
    //    //        //    default:
    //    //        //        break;
    //    //        //}
    //    //    }
    //    //    dc.CommitTransaction(isTransInit);
    //    //    DBContextManager.ReleaseDBContext(ref dc, isDCInit);
    //    //    bStatus = true;
    //    //    return bStatus;
    //    //}
    //}
}
