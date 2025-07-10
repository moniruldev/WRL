using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Data.Linq;
//using System.Linq.Dynamic;
using PG.Core.Extentions;
using PG.Core.DBBase;
using PG.Core.DBFilters;
using PG.DBClass.SecurityDC;

namespace PG.BLLibrary.SecurityBL
{
    public class EmpInfoBL
    {

        public static List<dcEmp_Info> GetEmpInfoList(string pEmp_ID)
        {
            return GetEmpInfoList(pEmp_ID, null);
        }
        public static List<dcEmp_Info> GetEmpInfoList(string pEmp_ID, DBContext dc)
        {
            DBCommandInfo cmdInfo = new DBCommandInfo();

            //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT EMP_INFO_VW.* ");
            //sb.Append(" , tblRole.RoleName ");
            sb.Append(" FROM EMP_INFO_VW ");
            //sb.Append(" INNER JOIN tblRole ON tblRole.RoleID = tblUser.RoleID ");
            sb.Append(" WHERE 1=1 ");


            if (pEmp_ID != string.Empty)
            {
                sb.Append(" AND Upper(EMP_INFO_VW.EMP_ID)=Upper(@pEmp_ID) ");
                //cmd.Parameters.AddWithValue("@pUserName", pUserName);
                cmdInfo.DBParametersInfo.Add("@pEmp_ID", pEmp_ID);
            }

            sb.Append(" ORDER BY EMP_INFO_VW.EMP_ID");

            //cmd.CommandType = CommandType.Text;
            //cmd.CommandText = sb.ToString();

            DBQuery dbq = new DBQuery();
            //dbq.DBQueryMode = DBQueryModeEnum.DBCommand;
            //dbq.DBCommand = cmd;

            dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
            cmdInfo.CommandText = sb.ToString();
            cmdInfo.CommandType = CommandType.Text;
            dbq.DBCommandInfo = cmdInfo;


            return GetEmpInfoList(dbq, dc);


        }

        public static List<dcEmp_Info> GetEmpInfoList(DBQuery dbq, DBContext dc)
        {
            List<dcEmp_Info> cObjList = new List<dcEmp_Info>();
            bool isDCInit = false;
            //try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                if (dbq == null)
                {
                    dbq = new DBQuery();
                }
                cObjList = DBQuery.ExecuteDBQuery<dcEmp_Info>(dbq, dc);

                //using (DataContext dataContext = dc.NewDataContext())
                //{

                //}
            }
            //catch { throw; }
            //finally 
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }
            return cObjList;
        }

        public static dcEmp_Info GetEmpInfoByEmpID(string pEmpID)
        {

            return GetEmpInfoByEmpID(pEmpID, null);
        }
        public static dcEmp_Info GetEmpInfoByEmpID(string pEmpID, DBContext dc)
        {

            return GetEmpInfoList(pEmpID, dc).FirstOrDefault();
        }

        public static bool Login(string pEmpID, string pPassword)
        {
            return Login(pEmpID, pPassword, null);
        }
        public static bool Login(string pEmpID, string pPassword, DBContext dc)
        {
            bool isAuthinticate = false;
            dcEmp_Info cEmpInfo = GetEmpInfoByEmpID(pEmpID, dc);
            if (cEmpInfo != null)
            {
                if (string.Compare(cEmpInfo.EMP_PWD, pPassword, true) == 0)
                {
                    isAuthinticate = true;
                }
       }
            return isAuthinticate;

     }

    }
}
