using PG.Core.DBBase;
using PG.DBClass;
using PG.DBClass.OrganiztionDC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Linq;
using System.Text;

namespace PG.BLLibrary.OrganizationBL
{
    public class TBLUSERDEPARTMENTBL
    {
        public static DataLoadOptions TBLUSERDEPARTMENTLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcTBLUSERDEPARTMENT>(obj => obj.relatedclassname);
            return dlo;
        }
        public static string GetuserDepartment_Info_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" Select tbluserdepartment.* FROM tbluserdepartment where 1=1 ");
            return sb.ToString();
        }

        public static List<dcTBLUSERDEPARTMENT> GetTBLUSERDEPARTMENTList(int pUserid)
        {
            return GetTBLUSERDEPARTMENTList(null, null,pUserid);
        }

        public static string GetDepartment_Info_SQLString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" Select DEPARTMENT_INFO.* FROM DEPARTMENT_INFO where 1=1 ");
            return sb.ToString();
        }
        public static List<dcDEPARTMENT_INFO> Department_Info_List(DBQuery dbq, DBContext dc, string isStore)
        {
            List<dcDEPARTMENT_INFO> cObjList = new List<dcDEPARTMENT_INFO>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {

                    DBCommandInfo cmdInfo = new DBCommandInfo();
                    StringBuilder sb = new StringBuilder(GetDepartment_Info_SQLString());
                    

                    sb.Append(" order by DEPARTMENT_NAME asc ");

                    dbq = new DBQuery();
                    dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                    cmdInfo.CommandText = sb.ToString();
                    cmdInfo.CommandType = CommandType.Text;
                    dbq.DBCommandInfo = cmdInfo;
                    cObjList = DBQuery.ExecuteDBQuery<dcDEPARTMENT_INFO>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }



        public static List<dcTBLUSERDEPARTMENT> GetTBLUSERDEPARTMENTList(DBQuery dbq, DBContext dc, int pUserid)
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
                        DBCommandInfo cmdInfo = new DBCommandInfo();
                        StringBuilder sb = new StringBuilder(GetuserDepartment_Info_SQLString());
                        if (!String.IsNullOrEmpty(pUserid.ToString()))
                        {
                            sb.Append("and userid=@pUserid");
                            cmdInfo.DBParametersInfo.Add("@pUserid", pUserid);
                        }

                       // sb.Append(" order by DEPARTMENT_NAME asc ");

                        //dbq = new DBQuery();
                        //dbq.OrderBy = "YearStartDate Desc";
                        dbq = new DBQuery();
                        dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                        cmdInfo.CommandText = sb.ToString();
                        cmdInfo.CommandType = CommandType.Text;
                        dbq.DBCommandInfo = cmdInfo;
                        cObjList = DBQuery.ExecuteDBQuery<dcTBLUSERDEPARTMENT>(dbq, dc);
                    }
                   // cObjList = DBQuery.ExecuteDBQuery<dcTBLUSERDEPARTMENT>(dbq, dc);
                }
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
            dcTBLUSERDEPARTMENT cObjKey = new dcTBLUSERDEPARTMENT();
            cObjKey.USERDEPTID = cObj.USERDEPTID;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcTBLUSERDEPARTMENT>(cObj,cObjKey);
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

        public static bool UpdateTBLUSERDEPARTMENT(dcTBLUSERDEPARTMENT cObj)
        {
            return UpdateTBLUSERDEPARTMENT(cObj, null);
        }

        public static bool UpdateTBLUSERDEPARTMENT(dcTBLUSERDEPARTMENT cObj, DBContext dc)
        {
            if (cObj.DEPTID == 0 || cObj.USERID == 0)
            {
                return false;
            }
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

            dcTBLUSERDEPARTMENT cObjN = GetTBLUSERDEPARTMENTByDepartmentID(cObj.DEPTID, cObj.USERID, dc);



            if (cObjN == null && cObj.AllowLogin)
            {
                cObjN = new dcTBLUSERDEPARTMENT();
                cObjN.DEPTID = cObj.DEPTID;
                cObjN.USERID = cObj.USERID;
                cObjN.AllowLogin = cObj.AllowLogin;
                cObjN._RecordState = RecordStateEnum.Added;
                cnt = dc.DoInsert<dcTBLUSERDEPARTMENT>(cObjN);
            }
            else if (cObjN != null)
            {
                cObjN.AllowLogin = cObj.AllowLogin;
                cObjN._RecordState = RecordStateEnum.Edited;
                //cnt = Update(cObjN,null);
                    //dc.DoUpdate<dcTBLUSERDEPARTMENT>(cObjN );
            }

            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        private static dcTBLUSERDEPARTMENT GetTBLUSERDEPARTMENTByDepartmentID(int p1, int p2, DBContext dc)
        {
            dcTBLUSERDEPARTMENT cObjList = new dcTBLUSERDEPARTMENT();
            DBQuery dbq = new DBQuery();

            bool isDCInit = false;
            try
            {
              
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                   
                   
                        DBCommandInfo cmdInfo = new DBCommandInfo();
                        StringBuilder sb = new StringBuilder(GetuserDepartment_Info_SQLString());
                        if (!String.IsNullOrEmpty(p1.ToString()))
                        {
                            sb.Append(" and USERID=@pUserid ");
                            cmdInfo.DBParametersInfo.Add("@pUserid", p2);
                        }
                        if (!String.IsNullOrEmpty(p2.ToString()))
                        {
                            sb.Append(" and deptID=@pdeptID ");
                            cmdInfo.DBParametersInfo.Add("@pdeptID", p1);
                        }

                        // sb.Append(" order by DEPARTMENT_NAME asc ");

                        //dbq = new DBQuery();
                        //dbq.OrderBy = "YearStartDate Desc";
                        dbq = new DBQuery();
                        dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                        cmdInfo.CommandText = sb.ToString();
                        cmdInfo.CommandType = CommandType.Text;
                        dbq.DBCommandInfo = cmdInfo;
                        cObjList = DBQuery.ExecuteDBQuery<dcTBLUSERDEPARTMENT>(dbq, dc).FirstOrDefault();
                  
                    // cObjList = DBQuery.ExecuteDBQuery<dcTBLUSERDEPARTMENT>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static bool UpdateTBLUSERDEPARTMENT (List<dcTBLUSERDEPARTMENT> cList)
        {
            return UpdateTBLUSERDEPARTMENT(cList, null);
        }

        public static bool UpdateTBLUSERDEPARTMENT(List<dcTBLUSERDEPARTMENT> cList, DBContext dc)
        {
            bool bSataus = false;
            bool isDCInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            foreach (dcTBLUSERDEPARTMENT cObj in cList)
            {
                bSataus = UpdateTBLUSERDEPARTMENT(cObj, dc);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            bSataus = true;
            return bSataus;
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
}
