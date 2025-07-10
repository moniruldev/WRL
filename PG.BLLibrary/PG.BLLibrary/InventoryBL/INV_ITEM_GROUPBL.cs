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
    public class INV_ITEM_GROUPBL
    {
    


      public static string ItemGroup_SqlString()
      {
          StringBuilder sb = new StringBuilder();
          sb.Append("SELECT INV_ITEM_GROUP.* ");
          sb.Append(", INV_ITEM_GROUP_1.ITEM_GROUP_CODE AS ITEM_GROUP_CODE_PARENT, INV_ITEM_GROUP_1.ITEM_GROUP_NAME AS ITEM_GROUP_NAME_PARENT, ");
          sb.Append("  FN_GET_NEW_ITEM_CODE(INV_ITEM_GROUP.ITEM_GROUP_ID) ITEM_CODE ");
          sb.Append(" FROM INV_ITEM_GROUP ");
          sb.Append(" LEFT OUTER JOIN INV_ITEM_GROUP INV_ITEM_GROUP_1 ON INV_ITEM_GROUP.ITEM_GROUP_ID_PARENT = INV_ITEM_GROUP_1.ITEM_GROUP_ID ");
          sb.Append(" WHERE (1=1) AND INV_ITEM_GROUP.IS_ACTIVE='Y' ");

          return sb.ToString();
      }



      public static string ItemGroupThatHasItem_SqlString()
      {
          StringBuilder sb = new StringBuilder();
          sb.Append("SELECT INV_ITEM_GROUP.* ");
          sb.Append(", INV_ITEM_GROUP_1.ITEM_GROUP_CODE AS ITEM_GROUP_CODE_PARENT, INV_ITEM_GROUP_1.ITEM_GROUP_NAME AS ITEM_GROUP_NAME_PARENT ");
          sb.Append(" FROM INV_ITEM_GROUP ");
          sb.Append(" LEFT OUTER JOIN INV_ITEM_GROUP INV_ITEM_GROUP_1 ON INV_ITEM_GROUP.ITEM_GROUP_ID_PARENT = INV_ITEM_GROUP_1.ITEM_GROUP_ID ");        
          sb.Append(" WHERE (1=1)");
          sb.Append(" and INV_ITEM_GROUP.ITEM_GROUP_ID IN(SELECT DISTINCT INV_ITEM_MASTER.ITEM_GROUP_ID from INV_ITEM_MASTER) ");

          return sb.ToString();
      }
      public static List<dcINV_ITEM_GROUP> GetItemGroupList()
      {
          return GetItemGroupList(null);
      }


       public static List<dcINV_ITEM_GROUP> GetItemGroupListByParentId(DBContext dc,int? parentId)
        {
            List<dcINV_ITEM_GROUP> cObjList = new List<dcINV_ITEM_GROUP>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(ItemGroup_SqlString());

                if (parentId>0)
                {
                    sb.Append(" and INV_ITEM_GROUP.ITEM_GROUP_ID_PARENT=@parentId");
                    cmdInfo.DBParametersInfo.Add("@parentId",parentId);
                }
                sb.Append(" order by INV_ITEM_GROUP.ITEM_GROUP_NAME asc ");
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                cObjList = DBQuery.ExecuteDBQuery<dcINV_ITEM_GROUP>(dbq, dc).ToList();
               
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
      public static List<dcINV_ITEM_GROUP> GetItemGroupList(DBContext dc)
      {

          return GetItemGroupList(string.Empty, dc);

          
      }

      public static List<dcINV_ITEM_GROUP> GetItemGroupList(string pGroupName, DBContext dc)
      {
          List<dcINV_ITEM_GROUP> cObjList = new List<dcINV_ITEM_GROUP>();
          bool isDCInit = false;
          try
          {
              isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

              DBCommandInfo cmdInfo = new DBCommandInfo();
              StringBuilder sb = new StringBuilder(ItemGroup_SqlString());

              if (pGroupName != string.Empty)
              {
                  sb.Append(" AND UPPER(INV_ITEM_GROUP.ITEM_GROUP_NAME) LIKE UPPER(:ItemGroupName) ");
                  //cmd.Parameters.AddWithValue("@glGroupID", pGLGroupID);
                  cmdInfo.DBParametersInfo.Add(":ItemGroupName",  '%' + pGroupName + '%');
              }
              sb.Append(" ORDER BY INV_ITEM_GROUP.ITEM_GROUP_NAME ");


              DBQuery dbq = new DBQuery();
              dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
              cmdInfo.CommandText = sb.ToString();
              cmdInfo.CommandType = CommandType.Text;
              dbq.DBCommandInfo = cmdInfo;

              cObjList = DBQuery.ExecuteDBQuery<dcINV_ITEM_GROUP>(dbq, dc).ToList();

              cObjList = GetChildItemCount(cObjList, dc);


          }
          catch { throw; }
          finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
          return cObjList;
      }
      public static string GETNewGroupCode(DBContext dc)
      {
          bool isDCInit = false;
          string Code = string.Empty;
          try
          {
              isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

              DBCommandInfo cmdInfo = new DBCommandInfo();
              StringBuilder sb = new StringBuilder("SELECT  FN_NEW_GROUP_CODE FROM DUAL");
              DBQuery dbq = new DBQuery();
              dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
              cmdInfo.CommandText = sb.ToString();
              cmdInfo.CommandType = CommandType.Text;
              dbq.DBCommandInfo = cmdInfo;
              Code = Convert.ToString(DBQuery.ExecuteDBScalar(dbq, dc));
              //cObj = DBQuery.ExecuteDBQuery<dcINVOICE_MASTER>(dbq, dc).FirstOrDefault();
          }
          catch { throw; }
          finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
          return Code;
      }
   
        public static List<dcINV_ITEM_GROUP> GetItemGroupList(DBQuery dbq, DBContext dc)
        {
            List<dcINV_ITEM_GROUP> cObjList = new List<dcINV_ITEM_GROUP>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                if (dbq == null)
                {
                    dbq = new DBQuery();
                    //dbq.OrderBy = "PeriodStartDate";
                }
                cObjList = DBQuery.ExecuteDBQuery<dcINV_ITEM_GROUP>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcINV_ITEM_GROUP> GetItemGroupList(int pActiveOption, DBContext dc)
        {
            List<dcINV_ITEM_GROUP> cObjList = new List<dcINV_ITEM_GROUP>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);             
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(ItemGroup_SqlString());


                if (pActiveOption > 0)
                {

                }


                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;            
                cObjList = GetItemGroupList(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }


        public static bool InsertGroupIDTo_ParamValue(List<dcINV_ITEM_GROUP> pGroupList)
        {
            return InsertGroupIDTo_ParamValue(pGroupList, null);
        }
        public static bool InsertGroupIDTo_ParamValue(List<dcINV_ITEM_GROUP> pGroupList, DBContext dc)
        {
            List<dcINV_ITEM_GROUP> cObjList = new List<dcINV_ITEM_GROUP>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                StringBuilder sb1 = new StringBuilder();
                sb1.Append("DELETE FROM TEMP_PARAMETER_VALUE WHERE PRM_GROUP = 'ITEM_GROUP_ID'");
                dc.ExecuteNonQuery(sb1.ToString());


                DBCommandInfo cmdInfo = new DBCommandInfo();

                foreach(dcINV_ITEM_GROUP grp in pGroupList)
                {
                    StringBuilder sb = new StringBuilder();

                    sb.Append("INSERT INTO TEMP_PARAMETER_VALUE(PRM_GROUP,PRM_ID)");
                    sb.Append(string.Format("VALUES('{0}',{1})","ITEM_GROUP_ID", grp.ITEM_GROUP_ID));

                    dc.ExecuteNonQuery(sb.ToString());
                    
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return true;
        }


        public static List<dcINV_ITEM_GROUP> GetChildItemCount(List<dcINV_ITEM_GROUP> pGroupList)
        {
            return GetChildItemCount(pGroupList, null);
        }
        public static List<dcINV_ITEM_GROUP> GetChildItemCount(List<dcINV_ITEM_GROUP> pGroupList, DBContext dc)
        {
            List<dcINV_ITEM_GROUP> cObjList = new List<dcINV_ITEM_GROUP>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                InsertGroupIDTo_ParamValue(pGroupList, dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();


                sb.Append("SELECT ITEM_GROUP_ID, COUNT(*) CHILD_ITEM_COUNT ");
                sb.Append(" FROM INV_ITEM_MASTER ");
                sb.Append(" INNER JOIN TEMP_PARAMETER_VALUE ON  INV_ITEM_MASTER.ITEM_GROUP_ID = TEMP_PARAMETER_VALUE.PRM_ID AND TEMP_PARAMETER_VALUE.PRM_GROUP = 'ITEM_GROUP_ID' ");
                sb.Append(" WHERE 1=1 ");
                sb.Append(" GROUP BY INV_ITEM_MASTER.ITEM_GROUP_ID ");

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                cObjList = DBQuery.ExecuteDBQuery<dcINV_ITEM_GROUP>(dbq, dc).ToList();


                foreach(dcINV_ITEM_GROUP grp in cObjList)
                {
                    dcINV_ITEM_GROUP grp1 = pGroupList.Where(c => c.ITEM_GROUP_ID == grp.ITEM_GROUP_ID).FirstOrDefault();
                    if (grp1 != null)
                    {
                        grp1.CHILD_ITEM_COUNT = grp.CHILD_ITEM_COUNT;
                    }
                }

                cObjList = pGroupList;

            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        

        public static List<dcINV_ITEM_GROUP> SetGLGroupListHeirerchy(List<dcINV_ITEM_GROUP> pGLGroupList, List<dcINV_ITEM_GROUP> pGLGroupListFull)
        {
            return SetGLGroupListHeirerchy(pGLGroupList, pGLGroupListFull, true);
        }
        public static List<dcINV_ITEM_GROUP> SetGLGroupListHeirerchy(List<dcINV_ITEM_GROUP> pGLGroupList, List<dcINV_ITEM_GROUP> pGLGroupListFull, bool pIncludeParent)
        {
            List<dcINV_ITEM_GROUP> newGrpList = new List<dcINV_ITEM_GROUP>();
            foreach (dcINV_ITEM_GROUP grp in pGLGroupList)
            {
                dcINV_ITEM_GROUP cGroup = newGrpList.FirstOrDefault(c => c.ITEM_GROUP_ID == grp.ITEM_GROUP_ID);
                bool hasParent = false;
                if (cGroup == null)
                {
                    if (grp.ITEM_GROUP_ID_PARENT != 0)
                    {
                        hasParent = CheckAndAddItemGroupParents(grp.ITEM_GROUP_ID_PARENT, newGrpList, pGLGroupList, pGLGroupListFull, pIncludeParent);
                    }
                    grp.HasParent = hasParent;
                    newGrpList.Add(grp);
                }
                else
                {
                    //cGroup
                }
            }
            return newGrpList;
        }

        public static bool CheckAndAddItemGroupParents(int pGLGroupIDParent, List<dcINV_ITEM_GROUP> pNewGrpList, List<dcINV_ITEM_GROUP> pGLGroupList, List<dcINV_ITEM_GROUP> pGLGroupListFull, bool pIncludeParent)
        {
            bool isParent = false;

            dcINV_ITEM_GROUP prGroup = pNewGrpList.FirstOrDefault(c => c.ITEM_GROUP_ID == pGLGroupIDParent);
            if (prGroup == null)
            {
                //dcGLGroup grpNew = pGLGroupListFull.FirstOrDefault(c => c.GLGroupID == pGLGroupIDParent);
                bool hasParent = false;
                dcINV_ITEM_GROUP grpNew = pGLGroupList.FirstOrDefault(c => c.ITEM_GROUP_ID == pGLGroupIDParent);
                if (grpNew != null)
                {
                    if (grpNew.ITEM_GROUP_ID_PARENT != 0)
                    {
                        hasParent = CheckAndAddItemGroupParents(grpNew.ITEM_GROUP_ID_PARENT, pNewGrpList, pGLGroupList, pGLGroupListFull, pIncludeParent);
                    }
                    grpNew.HasParent = hasParent;
                    pNewGrpList.Add(grpNew);
                    isParent = true;
                }
                else
                {
                    if (pIncludeParent)
                    {
                        dcINV_ITEM_GROUP grpPrt = pGLGroupListFull.FirstOrDefault(c => c.ITEM_GROUP_ID == pGLGroupIDParent);
                        if (grpPrt != null)
                        {
                            if (grpPrt.ITEM_GROUP_ID_PARENT != 0)
                            {
                                hasParent = CheckAndAddItemGroupParents(grpPrt.ITEM_GROUP_ID_PARENT, pNewGrpList, pGLGroupList, pGLGroupListFull, pIncludeParent);
                            }
                            grpPrt.HasParent = hasParent;
                            pNewGrpList.Add(grpPrt);
                            isParent = true;
                        }
                    }

                } //pare i full
            }
            else //group exists 
            {
                isParent = true;
            }
            return isParent;
        }




        public static List<dcINV_ITEM_GROUP> FormatItemGroup(clsPrmInventory prmInventory, List<dcINV_ITEM_GROUP> groupListFull)
        {
            if (groupListFull == null)
            {
                return new List<dcINV_ITEM_GROUP>();
            }

            List<dcINV_ITEM_GROUP> newGroupList = new List<dcINV_ITEM_GROUP>();

            int levelNo = 0;
            if (prmInventory.GroupIDParent > 0)
            {
                dcINV_ITEM_GROUP grpP = groupListFull.Where(c => c.ITEM_GROUP_ID == prmInventory.GroupIDParent).FirstOrDefault();
                grpP.ITEM_GROUP_LEVEL_CURRENT = 0;
                newGroupList.Add(grpP);

                levelNo = 1;
            }


            //List<dcINV_ITEM_GROUP> rootGroups = groupListFull.Where(c => c.ITEM_GROUP_ID_PARENT == 0).ToList();
            List<dcINV_ITEM_GROUP> rootGroups = groupListFull.Where(c => c.ITEM_GROUP_ID_PARENT == prmInventory.GroupIDParent).ToList();


            int maxLevel = groupListFull.Max(c => c.ITEM_GROUP_LEVEL);


            switch (prmInventory.OrderBy)
            {
                case InventoryOrderByEnum.SLNo:
                    rootGroups = rootGroups.OrderBy(c => c.ITEM_GROUP_SLNO).ToList();
                    break;
                case InventoryOrderByEnum.Code:
                    rootGroups = rootGroups.OrderBy(c => c.ITEM_GROUP_CODE).ToList();
                    break;
                case InventoryOrderByEnum.Name:
                    rootGroups = rootGroups.OrderBy(c => c.ITEM_GROUP_NAME).ToList();
                    break;
            }

            string hrName = string.Empty;

           

            foreach (dcINV_ITEM_GROUP rtGroup in rootGroups)
            {
                rtGroup.ITEM_GROUP_NAME_INDENT = rtGroup.ITEM_GROUP_NAME_INDENT;
                rtGroup.ITEM_GROUP_LEVEL_CURRENT = levelNo;
                newGroupList.Add(rtGroup);
                int nextLevelNo = levelNo + 1;
                ProcessChildGroup(rtGroup, nextLevelNo, prmInventory, newGroupList, groupListFull);
            }
            return newGroupList;
        }


        public static bool ProcessChildGroup(dcINV_ITEM_GROUP parentGroup, int levelNo, clsPrmInventory prmInventory, List<dcINV_ITEM_GROUP> groupListNew, List<dcINV_ITEM_GROUP> groupListFull)
        {
            List<dcINV_ITEM_GROUP> childGroupList = groupListFull.Where(c => c.ITEM_GROUP_ID_PARENT == parentGroup.ITEM_GROUP_ID).OrderBy(c => c.ITEM_GROUP_SLNO).ToList();
            parentGroup.CHILD_GROUP_COUNT = childGroupList.Count;
            switch (prmInventory.OrderBy)
            {
                case InventoryOrderByEnum.Code:
                    childGroupList = childGroupList.OrderBy(c => c.ITEM_GROUP_CODE).ToList();
                    break;
                case InventoryOrderByEnum.Name:
                    childGroupList = childGroupList.OrderBy(c => c.ITEM_GROUP_NAME).ToList();
                    break;
                case InventoryOrderByEnum.SLNo:
                    childGroupList = childGroupList.OrderBy(c => c.ITEM_GROUP_SLNO).ToList();
                    break;
            }

            int count = 0;
            foreach (dcINV_ITEM_GROUP childGroup in childGroupList)
            {
                count++;
                childGroup.ITEM_GROUP_LEVEL_CURRENT = levelNo;
                childGroup.ITEM_GROUP_NAME_INDENT = childGroup.ITEM_GROUP_NAME;
                //childGroup.GLGroupNameIndent = string.Concat(ArrayList.Repeat("\t", levelNo).ToArray()) + childGroup.GLGroupName;
                //childGroup.GLGroupNameIndent = string.Concat(ArrayList.Repeat("\t\t\t", levelNo).ToArray()) + childGroup.GLGroupName;


                groupListNew.Add(childGroup);
                int nextLevelNo = levelNo + 1;
                ProcessChildGroup(childGroup, nextLevelNo, prmInventory, groupListNew, groupListFull);
            }
            return count > 0;
        }




        public static dcINV_ITEM_GROUP GetItemGroupByID(int pItemGroupID)
        {
            return GetItemGroupByID(pItemGroupID, null);
        }
        public static dcINV_ITEM_GROUP GetItemGroupByID(int pItemGroupID, DBContext dc)
        {
            DBCommandInfo cmdInfo = new DBCommandInfo();
            System.Text.StringBuilder sb = new System.Text.StringBuilder(ItemGroup_SqlString());

            if (pItemGroupID > 0)
            {
                sb.Append(" AND INV_ITEM_GROUP.ITEM_GROUP_ID=@ItemGroupID ");
                //cmd.Parameters.AddWithValue("@glGroupID", pGLGroupID);
                cmdInfo.DBParametersInfo.Add("@ItemGroupID", pItemGroupID);
            }

            DBQuery dbq = new DBQuery();
            dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
            cmdInfo.CommandText = sb.ToString();
            cmdInfo.CommandType = CommandType.Text;
            dbq.DBCommandInfo = cmdInfo;

            dcINV_ITEM_GROUP cObj = GetItemGroupList(dbq, dc).FirstOrDefault();
            return cObj;
        }


        public static int GetChildGroupCountByItemGroupID(int pItemGroupID)
        {
            return GetChildGroupCountByItemGroupID(pItemGroupID, null);
        }
        public static int GetChildGroupCountByItemGroupID(int pItemGroupID, DBContext dc)
        {
            DBCommandInfo cmdInfo = new DBCommandInfo();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append("SELECT COUNT(*) TOT_REC ");
            sb.Append(" FROM INV_ITEM_GROUP ");
            sb.Append(" WHERE 1=1 " );

            //if (pItemGroupID > 0)
            {
                sb.Append(" AND INV_ITEM_GROUP.ITEM_GROUP_ID_PARENT=@ItemGroupID ");
                cmdInfo.DBParametersInfo.Add("@ItemGroupID", pItemGroupID);
            }

            DBQuery dbq = new DBQuery();
            dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
            cmdInfo.CommandText = sb.ToString();
            cmdInfo.CommandType = CommandType.Text;
            dbq.DBCommandInfo = cmdInfo;


            int cnt = Convert.ToInt32(DBQuery.ExecuteDBScalar(dbq, dc));

            return cnt;
        }


        public static bool IsItemGroupCodeExists(string pItemGroupCode)
        {
            return IsItemGroupCodeExists(pItemGroupCode, null);
        }
        public static bool IsItemGroupCodeExists(string pItemGroupCode, DBContext dc)
        {
            bool isData = false;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                StringBuilder sb = new StringBuilder(ItemGroup_SqlString());

                sb.Append(" AND UPPER(INV_ITEM_GROUP.ITEM_GROUP_CODE)=UPPER(@itemGroupCode) ");
                //cmd.Parameters.AddWithValue("@gLGroupCode", pGLGroupCode);
                cmdInfo.DBParametersInfo.Add("@itemGroupCode", pItemGroupCode);



                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                isData = GetItemGroupList(dbq, dc).Count > 0;

            }
            finally
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }
            return isData;
        }
        public static bool IsItemGroupCodeExists(string pItemGroupCode, int pItemGroupID)
        {
            return IsItemGroupCodeExists(pItemGroupCode, pItemGroupID, null);
        }
        public static bool IsItemGroupCodeExists(string pItemGroupCode, int pItemGroupID, DBContext dc)
        {
            bool isData = false;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                StringBuilder sb = new StringBuilder(ItemGroup_SqlString());

                sb.Append(" AND UPPER(INV_ITEM_GROUP.ITEM_GROUP_CODE)=UPPER(@itemGroupCode) ");
                //cmd.Parameters.AddWithValue("@gLGroupName", pGLGroupName);
                cmdInfo.DBParametersInfo.Add("@itemGroupCode", pItemGroupCode);

                //if (pGLGroupIDParent != -1)
                //{
                //    sb.Append(" AND tblGLGroup.GLGroupIDParent=@gLGroupIDParent ");
                //    cmd.Parameters.AddWithValue("@gLGroupIDParent", pGLGroupIDParent);
                //}




                sb.Append(" AND INV_ITEM_GROUP.ITEM_GROUP_ID <> @itemGroupID ");
                //cmd.Parameters.AddWithValue("@gLGroupID", pGLGroupID);
                cmdInfo.DBParametersInfo.Add("@itemGroupID", pItemGroupID);

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                isData = GetItemGroupList(dbq, dc).Count > 0;
            }
            finally
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }
            return isData;
        }


        public static bool IsItemGroupNameExists(string pItemGroupName)
        {
            return IsItemGroupNameExists(pItemGroupName, null);
        }
        public static bool IsItemGroupNameExists(string pItemGroupName, DBContext dc)
        {
            bool isData = false;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                StringBuilder sb = new StringBuilder(ItemGroup_SqlString());

                sb.Append(" AND UPPER(INV_ITEM_GROUP.ITEM_GROUP_NAME)=UPPER(@itemGroupName) ");
                cmdInfo.DBParametersInfo.Add("@itemGroupName", pItemGroupName);



                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;

                isData = GetItemGroupList(dbq, dc).Count > 0;

            }
            finally
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }
            return isData;
        }
        public static bool IsItemGroupNameExists(string pItemGroupName, int pItemGroupID)
        {
            return IsItemGroupNameExists(pItemGroupName, pItemGroupID, null);
        }
        public static bool IsItemGroupNameExists(string pItemGroupName, int pItemGroupID, DBContext dc)
        {
            bool isData = false;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                StringBuilder sb = new StringBuilder(ItemGroup_SqlString());

                sb.Append(" AND UPPER(INV_ITEM_GROUP.ITEM_GROUP_NAME)=UPPER(@itemGroupName) ");
                cmdInfo.DBParametersInfo.Add("@itemGroupName", pItemGroupName);


                sb.Append(" AND INV_ITEM_GROUP.ITEM_GROUP_ID <> @itemGroupID ");
                //cmd.Parameters.AddWithValue("@gLGroupID", pGLGroupID);
                cmdInfo.DBParametersInfo.Add("@itemGroupID", pItemGroupID);

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
                isData = GetItemGroupList(dbq, dc).Count > 0;
            }
            finally
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }
            return isData;
        }


        public static int Insert(dcINV_ITEM_GROUP cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcINV_ITEM_GROUP cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcINV_ITEM_GROUP>(cObj, true);
                if (id > 0) { cObj.ITEM_GROUP_ID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcINV_ITEM_GROUP cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcINV_ITEM_GROUP cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;

            dcINV_ITEM_GROUP key = new dcINV_ITEM_GROUP();
           // key.ITEM_ID = cObj.ITEM_ID;

            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcINV_ITEM_GROUP>(cObj, key);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pItemGroupID)
        {
            return Delete(pItemGroupID, null);
        }
        public static bool Delete(int pItemGroupID, DBContext dc)
        {
            dcINV_ITEM_GROUP cObj = new dcINV_ITEM_GROUP();
            cObj.ITEM_GROUP_ID = pItemGroupID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcINV_ITEM_GROUP>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcINV_ITEM_GROUP cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcINV_ITEM_GROUP cObj, bool isAdd, DBContext dc)
        {
            cObj._RecordState = isAdd ? RecordStateEnum.Added : RecordStateEnum.Edited; 
            return Save(cObj, dc);
        }

        public static int Save(dcINV_ITEM_GROUP cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcINV_ITEM_GROUP cObj, DBContext dc)
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
                    if (cObj.ITEM_GROUP_ID_PARENT > 0)
                    {
                        dcINV_ITEM_GROUP grpParent = INV_ITEM_GROUPBL.GetItemGroupByID(cObj.ITEM_GROUP_ID_PARENT, dc);
                        if (grpParent != null)
                        {
                            cObj.ITEM_GROUP_LEVEL = grpParent.ITEM_GROUP_LEVEL + 1;
                        }
                    }
                    else
                    {
                        cObj.ITEM_GROUP_LEVEL = 0;
                    }


                    switch (cObj._RecordState)
                    {
                        
                        case RecordStateEnum.Added:
                            newID = Insert(cObj, dc);
                            break;
                        case RecordStateEnum.Edited:
                            if (Update(cObj, dc))
                            {
                                newID = cObj.ITEM_GROUP_ID;
                            }
                            break;
                        case RecordStateEnum.Deleted:
                            if (Delete(cObj.ITEM_GROUP_ID, dc))
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

        public static bool SaveList(List<dcINV_ITEM_GROUP> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcINV_ITEM_GROUP> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcINV_ITEM_GROUP oDet in detList)
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

        public static bool Is_Same_Name_Exist(string itemName)
        {
            throw new NotImplementedException();
        }
    }
}
