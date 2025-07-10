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
    public class LocationGLAccountBL
    {
       // public static DataLoadOptions LocationGLAccountLoadOptions()
        //{
          //  DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcLocationGLAccount>(obj => obj.relatedclassname);
         //   return dlo;
      //  }

        public static string GetLocationGLAccountListString()
        {

            StringBuilder sb = new StringBuilder();

            sb.Append(" SELECT tblLocationGLAccount.LocationGLAccountID,tblLocation.LocationID,tblLocation.LocationName,tblGLAccount.GLAccountID,tblGLAccount.GLAccountCode,tblGLAccount.GLAccountName,tblGLGroup.GLGroupCode, tblGLGroup.GLGroupName  ");
            sb.Append(" FROM tblGLAccount ");
            sb.Append(" INNER JOIN tblGLGroup ON tblGLAccount.GLGroupID = tblGLGroup.GLGroupID ");
            sb.Append(" INNER JOIN tblLocationGLAccount ON tblGLAccount.GLAccountID=tblLocationGLAccount.GLAccountID ");
            sb.Append(" INNER JOIN tblLocation ON tblLocationGLAccount.LocationID=tblLocation.LocationID ");
            sb.Append(" WHERE (1=1)  ");

            return sb.ToString();
        }

        public static string GetLocationReferencetListString()
        {

            StringBuilder sb = new StringBuilder();

            sb.Append(" Select LocationID,AccRefID FROM tblLocationAccRef ");
            sb.Append(" WHERE (1=1)  ");

            return sb.ToString();
        }


        public static List<dcLocationGLAccount> GetLocationGLAccountList(int pLocationID)
        {
            return GetLocationGLAccountList(pLocationID, null);
        }
        public static List<dcLocationGLAccount> GetLocationGLAccountList(int pLocationID, DBContext dc)
        {
            DBCommandInfo cmdInfo = new DBCommandInfo();
            StringBuilder sb = new StringBuilder(GetLocationGLAccountListString());


            if (pLocationID > 0)
            {
                sb.Append(" AND tblLocationGLAccount.LocationID=@pLocationID ");
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

            return GetLocationGLAccountList(dbq, dc);

            //return GetLocationGLAccountList(null, dc);
        }
       public  static List<dcLocationGLAccount> GetLocationGLAccountList(DBQuery dbq, DBContext dc)
        {
            List<dcLocationGLAccount> cObjList = new List<dcLocationGLAccount>();
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
                    cObjList = DBQuery.ExecuteDBQuery<dcLocationGLAccount>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcLocationGLAccount GetLocationGLAccountByID(int pLocationGLAccountID)
        {
            return GetLocationGLAccountByID(pLocationGLAccountID, null);
        }
        public static dcLocationGLAccount GetLocationGLAccountByID(int pLocationGLAccountID, DBContext dc)
        {
            dcLocationGLAccount cObj = null;
           
            DBCommandInfo cmdInfo = new DBCommandInfo();
            //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            StringBuilder sb = new StringBuilder(GetLocationGLAccountListString());




            if (pLocationGLAccountID > 0)
            {
                sb.Append(" AND tblLocationGLAccount.LocationGLAccountID=@pLocationGLAccountID ");
                cmdInfo.DBParametersInfo.Add("@pLocationGLAccountID", pLocationGLAccountID);
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


            cObj = GetLocationGLAccountList(dbq, dc).FirstOrDefault();
            return cObj;
        }

        public static int Insert(dcLocationGLAccount cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcLocationGLAccount cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcLocationGLAccount>(cObj, true);
                if (id > 0) { cObj.LocationGLAccountID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcLocationGLAccount cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcLocationGLAccount cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcLocationGLAccount>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pLocationGLAccountID)
        {
            return Delete(pLocationGLAccountID, null);
        }
        public static bool Delete(int pLocationGLAccountID, DBContext dc)
        {
            dcLocationGLAccount cObj = new dcLocationGLAccount();
            cObj.LocationGLAccountID = pLocationGLAccountID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcLocationGLAccount>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcLocationGLAccount cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcLocationGLAccount cObj, bool isAdd, DBContext dc)
        {
           // cObj._RecordState = isAdd ? Interwave.Core.DBClass.RecordStateEnum.Added : Interwave.Core.DBClass.RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcLocationGLAccount cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcLocationGLAccount cObj, DBContext dc)
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
                       /* case Interwave.Core.DBClass.RecordStateEnum.Added:
                            newID = Insert(cObj, dc);
                            break;
                        case Interwave.Core.DBClass.RecordStateEnum.Edited:
                            if (Update(cObj, dc))
                            {
                                newID = cObj.LocationGLAccountID;
                            }
                            break;
                        case Interwave.Core.DBClass.RecordStateEnum.Deleted:
                            if (Delete(cObj.LocationGLAccountID, dc))
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

        public static bool SaveList(List<dcLocationGLAccount> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcLocationGLAccount> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcLocationGLAccount oDet in detList)
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
                        bool d = Delete(oDet.LocationGLAccountID, dc);
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

        public static bool IsAccLedgerCodeExists(int pLocationID, int pGLAccountID)
        {
            return IsAccLedgerCodeExists(pLocationID, pGLAccountID, null);
        }
        public static bool IsAccLedgerCodeExists(int pLocationID, int pGLAccountID, DBContext dc)
        {
            bool isData = false;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetLocationGLAccountListString());

                sb.Append(" AND tblLocationGLAccount.LocationID=@pLocationID ");
                cmdInfo.DBParametersInfo.Add("@pLocationID", pLocationID);

                sb.Append(" AND tblLocationGLAccount.GLAccountID=@pGLAccountID ");
                cmdInfo.DBParametersInfo.Add("@pGLAccountID", pGLAccountID);


                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                isData = GetLocationGLAccountList(dbq, dc).Count > 0;

            }
            finally
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }
            return isData;
        }
       public static bool IsAccLedgerCodeExists(int pLocationID, int pGLAccountID, int pLocationGLAccountID)
        {
            return IsAccLedgerCodeExists(pLocationID, pGLAccountID,pLocationGLAccountID, null);
        }
       public static bool IsAccLedgerCodeExists(int pLocationID, int pGLAccountID, int pLocationGLAccountID, DBContext dc)
        {
            bool isData = false;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetLocationGLAccountListString());

                sb.Append(" AND tblLocationGLAccount.LocationID=@pLocationID ");
                cmdInfo.DBParametersInfo.Add("@pLocationID", pLocationID);

                sb.Append(" AND tblLocationGLAccount.GLAccountID=@pGLAccountID ");
                cmdInfo.DBParametersInfo.Add("@pGLAccountID", pGLAccountID);


                sb.Append(" AND tblLocationGLAccount.LocationGLAccountID <> @pLocationGLAccountID ");
                //cmd.Parameters.AddWithValue("@accRefID", pAccRefID);
                cmdInfo.DBParametersInfo.Add("@pLocationGLAccountID", pLocationGLAccountID);

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

                isData = GetLocationGLAccountList(dbq, dc).Count > 0;
            }
            finally
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }
            return isData;
        }

        //Create Moni
       public static bool IsAccRefCodeExists(int pLocationID, int pGLAccRefID)
       {
           return IsAccRefCodeExists(pLocationID, pGLAccRefID, null);
       }
       public static bool IsAccRefCodeExists(int pLocationID, int pGLAccRefID, DBContext dc)
       {
           bool isData = false;
           bool isDCInit = false;
           try
           {
               isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

               DBCommandInfo cmdInfo = new DBCommandInfo();
               StringBuilder sb = new StringBuilder(GetLocationReferencetListString());

               sb.Append(" AND tblLocationAccRef.LocationID=@pLocationID ");
               cmdInfo.DBParametersInfo.Add("@pLocationID", pLocationID);

               sb.Append(" AND tblLocationAccRef.AccRefID=@pGLAccRefID ");
               cmdInfo.DBParametersInfo.Add("@pGLAccRefID", pGLAccRefID);


               DBQuery dbq = new DBQuery();
               dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
               cmdInfo.CommandText = sb.ToString();
               cmdInfo.CommandType = CommandType.Text;
               dbq.DBCommandInfo = cmdInfo;

               isData = GetLocationGLAccountList(dbq, dc).Count > 0;

           }
           finally
           {
               DBContextManager.ReleaseDBContext(ref dc, isDCInit);
           }
           return isData;
       }

       public static bool IsAccRefCodeExists(int pLocationID, int pAccRefID, int pLocationAccRefID)
       {
           return IsAccRefCodeExists(pLocationID, pAccRefID, pLocationAccRefID, null);
       }
       public static bool IsAccRefCodeExists(int pLocationID, int pAccRefID, int pLocationAccRefID, DBContext dc)
       {
           bool isData = false;
           bool isDCInit = false;
           try
           {
               isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
               //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
               DBCommandInfo cmdInfo = new DBCommandInfo();
               StringBuilder sb = new StringBuilder(GetLocationGLAccountListString());

               sb.Append(" AND tblLocationAccRef.LocationID=@pLocationID ");
               cmdInfo.DBParametersInfo.Add("@pLocationID", pLocationID);

               sb.Append(" AND tblLocationAccRef.GLAccountID=@pAccRefID ");
               cmdInfo.DBParametersInfo.Add("@pAccRefID", pAccRefID);


               sb.Append(" AND tblLocationAccRef.LocationGLAccountID <> @pLocationAccRefID ");
               //cmd.Parameters.AddWithValue("@accRefID", pAccRefID);
               cmdInfo.DBParametersInfo.Add("@pLocationAccRefID", pLocationAccRefID);

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

               isData = GetLocationGLAccountList(dbq, dc).Count > 0;
           }
           finally
           {
               DBContextManager.ReleaseDBContext(ref dc, isDCInit);
           }
           return isData;
       }
    }
}
