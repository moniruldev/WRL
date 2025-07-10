using PG.Core.DBBase;
using PG.DBClass.AccountingDC.GeneralLedgerDC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Linq;
using System.Text;

namespace PG.BLLibrary.AccountingBL.GeneralLedgerBL
{
    public class LocationAccRefBL
    {
        public static DataLoadOptions LocationAccRefLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcLocationAccRef>(obj => obj.relatedclassname);
            return dlo;
        }

        public static string GetLocationAccRefListString()
        {
            StringBuilder sb = new StringBuilder();
            //TODO Change this query as
            sb.Append(" SELECT tblLocationAccRef.LocationAccRefID,tblLocationAccRef.LocationID,tblLocation.LocationName,tblAccRef.AccRefID,tblAccRef.AccRefCategoryID,tblAccRef.AccRefCode,tblAccRef.AccRefName, tblAccRefCategory.AccRefCategoryCode, tblAccRefCategory.AccRefCategoryName ");
              sb.Append(", tblAccRefType.AccRefTypeCode, tblAccRefType.AccRefTypeName ");
              sb.Append(" FROM tblAccRef ");
              sb.Append(" INNER JOIN tblAccRefCategory ON tblAccRef.AccRefCategoryID = tblAccRefCategory.AccRefCategoryID ");
              sb.Append(" INNER JOIN tblAccRefType ON tblAccRefCategory.AccRefTypeID = tblAccRefType.AccRefTypeID  ");
              sb.Append(" INNER JOIN  tblLocationAccRef ON tblAccRef.AccRefID=tblLocationAccRef.AccRefID ");
              sb.Append(" INNER JOIN tblLocation ON tblLocationAccRef.LocationID=tblLocation.LocationID ");

              sb.Append(" WHERE 1=1 ");

              return sb.ToString();
        }
        public static List<dcLocationAccRef> GetLocationAccRefList(int pLocationID)
        {
            return GetLocationAccRefList(pLocationID, null);
        }
        public static List<dcLocationAccRef> GetLocationAccRefList(int pLocationID,DBContext dc)
        {
            DBCommandInfo cmdInfo = new DBCommandInfo();
            StringBuilder sb = new StringBuilder(GetLocationAccRefListString());


            if (pLocationID > 0)
            {
                sb.Append(" AND tblLocationAccRef.LocationID=@pLocationID ");
                cmdInfo.DBParametersInfo.Add("@pLocationID", pLocationID);

            }

            sb.Append(" ORDER BY tblLocation.LocationName ");

            //cmd.CommandType = CommandType.Text;
            //cmd.CommandText = sb.ToString();

            //PG.Core.DBBase.DBQuery dbq = new PG.Core.DBBase.DBQuery();
            //dbq.DBQueryMode = PG.Core.DBBase.DBQueryModeEnum.DBCommand;
            //dbq.DBCommand = cmd;

            DBQuery dbq = new DBQuery();
            dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
            cmdInfo.CommandText = sb.ToString();
            cmdInfo.CommandType = CommandType.Text;
            dbq.DBCommandInfo = cmdInfo;

            return GetLocationAccRefList(dbq, dc);
        }
        public static List<dcLocationAccRef> GetLocationAccRefList(DBQuery dbq, DBContext dc)
        {
            List<dcLocationAccRef> cObjList = new List<dcLocationAccRef>();
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
                    cObjList = DBQuery.ExecuteDBQuery<dcLocationAccRef>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcLocationAccRef GetLocationAccRefByID(int pLocationAccRefID)
        {
            return GetLocationAccRefByID(pLocationAccRefID, null);
        }
        public static dcLocationAccRef GetLocationAccRefByID(int pLocationAccRefID, DBContext dc)
        {
            dcLocationAccRef cObj = null;
           

            DBCommandInfo cmdInfo = new DBCommandInfo();
            //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            StringBuilder sb = new StringBuilder(GetLocationAccRefListString());




            if (pLocationAccRefID > 0)
            {
                sb.Append(" AND tblLocationAccRef.LocationAccRefID=@pLocationAccRefID ");
                cmdInfo.DBParametersInfo.Add("@pLocationAccRefID", pLocationAccRefID);
            }

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


            cObj = GetLocationAccRefList(dbq, dc).FirstOrDefault();
            return cObj;
        }

        public static int Insert(dcLocationAccRef cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcLocationAccRef cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcLocationAccRef>(cObj, true);
                if (id > 0) { cObj.LocationAccRefID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcLocationAccRef cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcLocationAccRef cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcLocationAccRef>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pLocationAccRefID)
        {
            return Delete(pLocationAccRefID, null);
        }
        public static bool Delete(int pLocationAccRefID, DBContext dc)
        {
            dcLocationAccRef cObj = new dcLocationAccRef();
            cObj.LocationAccRefID = pLocationAccRefID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcLocationAccRef>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcLocationAccRef cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcLocationAccRef cObj, bool isAdd, DBContext dc)
        {
            //cObj._RecordState = isAdd ? Interwave.Core.DBClass.RecordStateEnum.Added : Interwave.Core.DBClass.RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcLocationAccRef cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcLocationAccRef cObj, DBContext dc)
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
                        /*case Interwave.Core.DBClass.RecordStateEnum.Added:
                            newID = Insert(cObj, dc);
                            break;
                        case Interwave.Core.DBClass.RecordStateEnum.Edited:
                            if (Update(cObj, dc))
                            {
                                newID = cObj.LocationAccRefID;
                            }
                            break;
                        case Interwave.Core.DBClass.RecordStateEnum.Deleted:
                            if (Delete(cObj.LocationAccRefID, dc))
                            {
                                newID = 1;
                            }
                            break;*/
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

        public static bool SaveList(List<dcLocationAccRef> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcLocationAccRef> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcLocationAccRef oDet in detList)
            {
                switch (oDet._RecordState)
                {
                    /*case Interwave.Core.DBClass.RecordStateEnum.Added:
                        int a = Insert(oDet, dc);
                        break;
                    case Interwave.Core.DBClass.RecordStateEnum.Edited:
                        bool e = Update(oDet, dc);
                        break;
                    case Interwave.Core.DBClass.RecordStateEnum.Deleted:
                        bool d = Delete(oDet.LocationAccRefID, dc);
                        break;*/
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
