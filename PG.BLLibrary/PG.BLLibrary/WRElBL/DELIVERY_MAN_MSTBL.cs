using PG.BLLibrary.SecurityBL;
using PG.Core.DBBase;
using PG.DBClass.SecurityDC;
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
    public class DELIVERY_MAN_MSTBL
    {
        public static DataLoadOptions DELIVERY_MAN_MSTLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcDELIVERY_MAN_MST>(obj => obj.relatedclassname);
            return dlo;
        }
        public static string GetDeliveryManMstListString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" SELECT DELIVERY_MAN_ID,MOBILE_NO,DELIVERY_MAN_NAME ");
            sb.Append(" FROM DELIVERY_MAN_MST  ");

            sb.Append(" WHERE IS_ACTIVE='Y' ");


            return sb.ToString();
        }

        public static string GetDeliveryManSQLString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" SELECT mst.*,a.agent_company_name AGENT_NAME FROM delivery_man_mst MST ");
            sb.Append(" LEFT JOIN agent_mst A ON mst.agent_id=a.agent_id  ");
            sb.Append(" WHERE 1=1 ");

            return sb.ToString();
        }
        public static List<dcDELIVERY_MAN_MST> GetDELIVERY_MAN_MSTList()
        {
            return GetDELIVERY_MAN_MSTList(null, null);
        }
        public static List<dcDELIVERY_MAN_MST> GetDELIVERY_MAN_MSTList(DBContext dc)
        {
            return GetDELIVERY_MAN_MSTList(null, dc);
        }
        public static List<dcDELIVERY_MAN_MST> GetDELIVERY_MAN_MSTList(DBQuery dbq, DBContext dc)
        {
            List<dcDELIVERY_MAN_MST> cObjList = new List<dcDELIVERY_MAN_MST>();
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
                    cObjList = DBQuery.ExecuteDBQuery<dcDELIVERY_MAN_MST>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcDELIVERY_MAN_MST GetDELIVERY_MAN_MSTByID(int pDELIVERY_MAN_MSTID)
        {
            return GetDELIVERY_MAN_MSTByID(pDELIVERY_MAN_MSTID, null);
        }
        public static dcDELIVERY_MAN_MST GetDELIVERY_MAN_MSTByID(int pDELIVERY_MAN_MSTID, DBContext dc)
        {
            dcDELIVERY_MAN_MST cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    var result = (from c in dataContext.GetTable<dcDELIVERY_MAN_MST>()
                                  where c.DELIVERY_MAN_ID == pDELIVERY_MAN_MSTID
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

        public static dcDELIVERY_MAN_MST GetDeliveryManInfoById(int pDELIVERY_MAN_MSTID)
        {
            return GetDeliveryManInfoById(pDELIVERY_MAN_MSTID, null);
        }
        public static dcDELIVERY_MAN_MST GetDeliveryManInfoById(int pDELIVERY_MAN_MSTID, DBContext dc)
        {
            dcDELIVERY_MAN_MST cObj = new dcDELIVERY_MAN_MST();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetDeliveryManSQLString());
                if (pDELIVERY_MAN_MSTID > 0)
                {
                    sb.Append(" AND MST.DELIVERY_MAN_ID= @pDELIVERY_MAN_MSTID ");
                    cmdInfo.DBParametersInfo.Add("@pDELIVERY_MAN_MSTID", pDELIVERY_MAN_MSTID);
                }
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObj = DBQuery.ExecuteDBQuery<dcDELIVERY_MAN_MST>(dbq, dc).FirstOrDefault();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObj;
        }

        public static List<dcDELIVERY_MAN_MST> GetDeliveryManList(clsPrmWREL prm, DBContext dc)
        {
            List<dcDELIVERY_MAN_MST> cObjList = new List<dcDELIVERY_MAN_MST>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetDeliveryManSQLString());


                if (prm.Status != "0")
                {
                    sb.Append(" AND mst.IS_ACTIVE= @Status ");
                    cmdInfo.DBParametersInfo.Add("@Status", prm.Status);
                }


                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcDELIVERY_MAN_MST>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static int Insert(dcDELIVERY_MAN_MST cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcDELIVERY_MAN_MST cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcDELIVERY_MAN_MST>(cObj, true);
                if (id > 0) { cObj.DELIVERY_MAN_ID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcDELIVERY_MAN_MST cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcDELIVERY_MAN_MST cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcDELIVERY_MAN_MST>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pDELIVERY_MAN_MSTID)
        {
            return Delete(pDELIVERY_MAN_MSTID, null);
        }
        public static bool Delete(int pDELIVERY_MAN_MSTID, DBContext dc)
        {
            dcDELIVERY_MAN_MST cObj = new dcDELIVERY_MAN_MST();
            cObj.DELIVERY_MAN_ID = pDELIVERY_MAN_MSTID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcDELIVERY_MAN_MST>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcDELIVERY_MAN_MST cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcDELIVERY_MAN_MST cObj, bool isAdd, DBContext dc)
        {
            cObj._RecordState = isAdd ? RecordStateEnum.Added : RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcDELIVERY_MAN_MST cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcDELIVERY_MAN_MST cObj, DBContext dc)
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
                                newID = cObj.DELIVERY_MAN_ID;
                            }
                            break;
                        case RecordStateEnum.Deleted:
                            if (Delete(cObj.DELIVERY_MAN_ID, dc))
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

                       if(cObj._RecordState == RecordStateEnum.Added)
                       {
                           bool isExist = UserBL.IsUserExists(1, cObj.MOBILE_NO.Trim());
                           if(!isExist)
                           {
                               dcUser objUser = new dcUser();
                               objUser.AppID = 1;
                               objUser.UserName = cObj.MOBILE_NO;
                               objUser.Password = cObj.MOBILE_NO;
                               objUser.RoleID = 7;
                               objUser.FullName = cObj.DELIVERY_MAN_NAME;
                               objUser.Email = "";
                               objUser.IsActive = true;
                               objUser.ISDELIVERYMAN = "Y";

                               int userId = UserBL.Insert(objUser);
                               if (userId > 0)
                                   bStatus = true;
                           }
                         
                       }
                        if(cObj._RecordState == RecordStateEnum.Edited)
                        {
                            dcUser existingUser = UserBL.GetAllUserByUserName(1, cObj.MOBILE_NO,null);
                            if (existingUser != null)
                            {
                                dcUser user = new dcUser();
                                user.UserID = existingUser.UserID;
                                user.FullName = cObj.DELIVERY_MAN_NAME;
                                user.IsActive = cObj.IS_ACTIVE == "Y"? true : false;
                                bStatus = UserBL.Update(user);

                            }
                        }

                       
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

        public static bool SaveList(List<dcDELIVERY_MAN_MST> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcDELIVERY_MAN_MST> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcDELIVERY_MAN_MST oDet in detList)
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
                    //    bool d = Delete(oDet.DELIVERY_MAN_MSTID, dc);
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

        public static bool IsMobileNumberExists(string pMobileNo, int pDELIVERY_MAN_ID)
        {
            return IsMobileNumberExists(pMobileNo, pDELIVERY_MAN_ID, null);
        }

        public static bool IsMobileNumberExists(string pMobileNo,int pDELIVERY_MAN_ID, DBContext dc)
        {
            StringBuilder sb = new StringBuilder();
            bool isData = false;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();

                sb.Append(" select Count(MOBILE_NO) tID From DELIVERY_MAN_MST Where MOBILE_NO=@MobileNo AND DELIVERY_MAN_ID <> @DeliveryManId ");
                cmdInfo.DBParametersInfo.Add("@MobileNo", pMobileNo);
                cmdInfo.DBParametersInfo.Add("@DeliveryManId", pDELIVERY_MAN_ID);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;


                int tcount = Convert.ToInt32(DBQuery.ExecuteDBScalar(dbq));
                if (tcount > 0)
                {
                    isData = true;
                }

            }
            finally
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }
            return isData;
        }
    }
}
