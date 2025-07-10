using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Data.Linq;
using PG.Core.DBBase;
using PG.Core.Extentions;
using PG.Core.DBFilters;
using PG.DBClass.AccountingDC;
using PG.DBClass.AccountingDC.AccEnums;
using PG.DBClass.AccountingDC.GeneralLedgerDC;

namespace PG.BLLibrary.AccountingBL.GeneralLedgerBL
{
    /// <summary>
    /// AppMenuBL
    /// Last update By Moni, Date 09-03-2015
    /// </summary>
    public class GLGroupBL
    {
        //public static DataLoadOptions GLGroupLoadOptions()
        //{
        //    DataLoadOptions dlo = new DataLoadOptions();
        //    //dlo.LoadWith<DBClass.PayRoll.SalaryPeriod>(obj => obj.PayRoll.SalaryPeriodType);
        //    //dlo.LoadWith<DBClass.dcPurchase>(obj => obj.Supplier);
        //    //dlo.LoadWith<DBClass.dcPurchase>(obj => obj.TranType);
        //    return dlo;
        //}

        //TODO: need to change this Query Remove as
        public static string GetGLGroupListString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT tblGLGroup.* ");
            sb.Append(", tblGLClass.GLClassName, tblGLClass.GLClassCode, tblGLClass.GLClassSLNo ");
            sb.Append(", tblGLGroup_1.GLGroupCode GLGroupCodeParent, tblGLGroup_1.GLGroupNameShort AS GLGroupNameShortParent, tblGLGroup_1.GLGroupName AS GLGroupNameParent ");
            sb.Append(", tblGLGroupClass.GLGroupClassName ");

            sb.Append(" FROM tblGLGroup ");
            sb.Append(" INNER JOIN tblGLClass ON tblGLGroup.GLClassID = tblGLClass.GLClassID ");
            sb.Append(" LEFT OUTER JOIN tblGLGroup tblGLGroup_1 ON tblGLGroup.GLGroupIDParent = tblGLGroup_1.GLGroupID ");
            sb.Append(" LEFT OUTER JOIN tblGLGroupClass ON tblGLGroup.GLGroupClassID = tblGLGroupClass.GLGroupClassID ");

            sb.Append(" WHERE (1=1) ");

            return sb.ToString();
        }

        public static List<dcGLGroup> GetGLGroupList(int pCompanyID)
        {
            return GetGLGroupList(pCompanyID, 0, 0, null);
        }

        public static List<dcGLGroup> GetGLGroupList(int pCompanyID, DBContext dc)
        {
            return GetGLGroupList(pCompanyID, 0, 0, dc);
        }

        public static List<dcGLGroup> GetGLGroupList(int pCompanyID, int pGLClassID)
        {
            return GetGLGroupList(pCompanyID, pGLClassID, null);
        }

        public static List<dcGLGroup> GetGLGroupList(int pCompanyID, int pGLClassID, DBContext dc)
        {
            return GetGLGroupList(pCompanyID, pGLClassID, 0, dc);
        }

        public static List<dcGLGroup> GetGLGroupList(int pCompanyID, int pGLClassID, int pActiveOption)
        {
            return GetGLGroupList(pCompanyID, pGLClassID, pActiveOption, null);
        }

        public static List<dcGLGroup> GetGLGroupList(int pCompanyID, int pGLClassID, int pActiveOption, DBContext dc)
        {
            List<dcGLGroup> cObjList = new List<dcGLGroup>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetGLGroupListString());

                


                sb.Append(" AND tblGLGroup.CompanyID=@companyID ");
                //cmd.Parameters.AddWithValue("@companyID", pCompanyID);
                cmdInfo.DBParametersInfo.Add("@companyID", pCompanyID);

                if (pGLClassID > 0)
                {
                    sb.Append(" AND tblGLGroup.GLClassID=@GLClassID ");
                    //cmd.Parameters.AddWithValue("@GLClassID", pGLClassID);
                    cmdInfo.DBParametersInfo.Add("@GLClassID", pGLClassID);
                }
                //TODO: need to change this code
                if (pActiveOption > 0) //all
                {
                    if (pActiveOption == 1) //active
                    {
                        //sb.Append(" AND tblGLGroup.IsActive= 1 ");
                        sb.Append(" AND tblGLGroup.IsActive= @isActive ");
                        cmdInfo.DBParametersInfo.Add("@isActive", true);
                    }
                    if (pActiveOption == 2) //inactive
                    {
                        sb.Append(" AND tblGLGroup.IsActive= @isActive ");
                        cmdInfo.DBParametersInfo.Add("@isActive", false);
                        //cmdInfo.DBParametersInfo.Add("@GLClassID", pGLClassID);
                    }
                }

                sb.Append(" ORDER BY tblGLClass.GLClassSLNo, tblGLGroup.GLGroupCode, tblGLGroup.GLGroupName ");

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                //cmd.CommandType = CommandType.Text;
                //cmd.CommandText = sb.ToString();

                //DBQuery dbq = new DBQuery();
                //dbq.DBQueryMode = PG.Core.DBBase.DBQueryModeEnum.DBCommand;
                //dbq.DBCommand = cmd;

                cObjList = GetGLGroupList(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }

        public static List<dcGLGroup> GetGLGroupList(DBQuery dbq, DBContext dc)
        {
            List<dcGLGroup> cObjList = new List<dcGLGroup>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                if (dbq == null)
                {
                    dbq = new DBQuery();
                    //dbq.OrderBy = "PeriodStartDate";
                }
                cObjList = DBQuery.ExecuteDBQuery<dcGLGroup>(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }


        public static List<dcGLGroup> GetGLGroupListByParent(int pGLGroupIDParent)
        {
            return GetGLGroupListByParent(pGLGroupIDParent, null);
        }
        public static List<dcGLGroup> GetGLGroupListByParent(int pGLGroupIDParent, DBContext dc)
        {
            List<dcGLGroup> cObjList = new List<dcGLGroup>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder(GetGLGroupListString());

                //sb.Append(" AND tblGLGroup.CompanyID=@companyID ");
                //cmd.Parameters.AddWithValue("@companyID", pCompanyID);

                sb.Append(" AND tblGLGroup.GLGroupIDParent=@GLGroupIDParent ");
                //cmd.Parameters.AddWithValue("@GLGroupIDParent", pGLGroupIDParent);
                cmdInfo.DBParametersInfo.Add("@GLGroupIDParent", pGLGroupIDParent);

                sb.Append(" ORDER BY tblGLClass.GLClassSLNo, tblGLGroup.GLGroupSLNo, tblGLGroup.GLGroupName ");

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                //cmd.CommandType = CommandType.Text;
                //cmd.CommandText = sb.ToString();

                //DBQuery dbq = new DBQuery();
                //dbq.DBQueryMode = PG.Core.DBBase.DBQueryModeEnum.DBCommand;
                //dbq.DBCommand = cmd;

                cObjList = GetGLGroupList(dbq, dc);
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }


        public static List<dcGLGroup> SetGLGroupListHeirerchy(List<dcGLGroup> pGLGroupList, List<dcGLGroup> pGLGroupListFull)
        {
            return SetGLGroupListHeirerchy(pGLGroupList, pGLGroupListFull, true);
        }
        public static List<dcGLGroup> SetGLGroupListHeirerchy(List<dcGLGroup> pGLGroupList, List<dcGLGroup> pGLGroupListFull, bool pIncludeParent)
        {
            List<dcGLGroup> newGrpList = new List<dcGLGroup>();
            foreach (dcGLGroup grp in pGLGroupList)
            {
                dcGLGroup cGroup = newGrpList.FirstOrDefault(c => c.GLGroupID == grp.GLGroupID);
                bool hasParent = false;
                if (cGroup == null)
                {
                    if (grp.GLGroupIDParent != 0)
                    {
                        hasParent = CheckAndAddGLGroupParents(grp.GLGroupIDParent, newGrpList, pGLGroupList, pGLGroupListFull, pIncludeParent);
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

        public static bool CheckAndAddGLGroupParents(int pGLGroupIDParent,List<dcGLGroup> pNewGrpList , List<dcGLGroup> pGLGroupList, List<dcGLGroup> pGLGroupListFull, bool pIncludeParent)
        {
            bool isParent = false;

            dcGLGroup prGroup = pNewGrpList.FirstOrDefault(c => c.GLGroupID == pGLGroupIDParent);
            if (prGroup == null)
            {
                //dcGLGroup grpNew = pGLGroupListFull.FirstOrDefault(c => c.GLGroupID == pGLGroupIDParent);
                bool hasParent = false;
                dcGLGroup grpNew = pGLGroupList.FirstOrDefault(c => c.GLGroupID == pGLGroupIDParent);
                if (grpNew != null)
                {
                    if (grpNew.GLGroupIDParent != 0)
                    {
                        hasParent = CheckAndAddGLGroupParents(grpNew.GLGroupIDParent, pNewGrpList, pGLGroupList, pGLGroupListFull, pIncludeParent);
                    }
                    grpNew.HasParent = hasParent;
                    pNewGrpList.Add(grpNew);
                    isParent = true;
                }
                else
                {
                    if (pIncludeParent)
                    {
                        dcGLGroup grpPrt = pGLGroupListFull.FirstOrDefault(c => c.GLGroupID == pGLGroupIDParent);
                        if (grpPrt != null)
                        {
                            if (grpPrt.GLGroupIDParent != 0)
                            {
                                hasParent = CheckAndAddGLGroupParents(grpPrt.GLGroupIDParent, pNewGrpList, pGLGroupList, pGLGroupListFull, pIncludeParent);
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

        public static List<dcGLGroup> FormatGLGroup(clsPrmLedger prmLedger, List<dcGLGroup> groupListFull)
        {
            if (groupListFull == null)
            {
                return new List<dcGLGroup>();
            }

            List<dcGLGroup> newGroupList = new List<dcGLGroup>();

            List<dcGLGroup> rootGroups = groupListFull.Where(c => c.GLGroupIDParent == 0).ToList();

            int maxLevel = groupListFull.Max(c => c.GLGroupLevel);


            switch (prmLedger.OrderBy)
            {
                case AccOrderByEnum.SLNo:
                    rootGroups = rootGroups.OrderBy(c => c.GLGroupSLNo).ToList();
                    break;
                case AccOrderByEnum.Code:
                    rootGroups = rootGroups.OrderBy(c => c.GLGroupCode).ToList();
                    break;
                case AccOrderByEnum.Name:
                    rootGroups = rootGroups.OrderBy(c => c.GLGroupName).ToList();
                    break;
            }

            string hrName = string.Empty;

            int levelNo = 0;

            foreach (dcGLGroup rtGroup in rootGroups)
            {
                rtGroup.GLGroupNameIndent = rtGroup.GLGroupName;
                rtGroup.GLGroupLevel = levelNo;
                newGroupList.Add(rtGroup);

                int nextLevelNo = levelNo + 1;
                ProcessChildGroup(rtGroup, nextLevelNo, prmLedger, newGroupList, groupListFull);
            }
            return newGroupList;
        }
        public static bool ProcessChildGroup(dcGLGroup parentGroup, int levelNo, clsPrmLedger prmLedger, List<dcGLGroup> groupListNew, List<dcGLGroup> groupListFull)
        {
            List<dcGLGroup> childGroupList = groupListFull.Where(c => c.GLGroupIDParent == parentGroup.GLGroupID).OrderBy(c => c.GLGroupSLNo).ToList();
            parentGroup.ChildGroupCount = childGroupList.Count;
            switch (prmLedger.OrderBy)
            {
                case AccOrderByEnum.Code:
                    childGroupList = childGroupList.OrderBy(c => c.GLGroupCode).ToList();
                    break;
                case AccOrderByEnum.Name:
                    childGroupList = childGroupList.OrderBy(c => c.GLGroupName).ToList();
                    break;
                case AccOrderByEnum.SLNo:
                    childGroupList = childGroupList.OrderBy(c => c.GLGroupSLNo).ToList();
                    break;
            }

            int count = 0;
            foreach (dcGLGroup childGroup in childGroupList)
            {
                count++;
                childGroup.GLGroupLevel = levelNo;
                childGroup.GLGroupNameIndent = childGroup.GLGroupName;
                //childGroup.GLGroupNameIndent = string.Concat(ArrayList.Repeat("\t", levelNo).ToArray()) + childGroup.GLGroupName;
                //childGroup.GLGroupNameIndent = string.Concat(ArrayList.Repeat("\t\t\t", levelNo).ToArray()) + childGroup.GLGroupName;


                groupListNew.Add(childGroup);
                int nextLevelNo = levelNo + 1;
                ProcessChildGroup(childGroup, nextLevelNo, prmLedger, groupListNew, groupListFull);
            }
            return count > 0;
        }

        public static List<dcGLGroup> SetGLGroupListLevelCurrent(List<dcGLGroup> pGLGroupList)
        {
            if (pGLGroupList == null)
            {
                return new List<dcGLGroup>();
            }
            ///first get the first top level group
            List<dcGLGroup> rootGroupList = new List<dcGLGroup>();
            foreach (dcGLGroup grp in pGLGroupList)
            {
                if (grp.GLGroupIDParent == 0)
                {
                    //this is as top level group
                    grp.GLGroupLevelCurrent = 0;
                    grp.HasParent = false;
                    rootGroupList.Add(grp);
                }
                else
                {
                    if (!pGLGroupList.Exists(c => c.GLGroupID == grp.GLGroupIDParent))
                    {
                        grp.GLGroupLevelCurrent = 0;
                        grp.HasParent = false;
                        rootGroupList.Add(grp);
                    }
                }
            }

            //now process by root list
            foreach (dcGLGroup rtGroup in rootGroupList)
            {
                //newGroupList.Add(rtGroup);
                rtGroup.GLGroupLevelCurrent = 0;
                rtGroup.HasParent = false;
                int nextLevelNo = rtGroup.GLGroupLevelCurrent + 1;
                rtGroup.ChildGroupCount = SetChildGroupCurrentLevel(rtGroup, nextLevelNo, pGLGroupList);
            }
            return pGLGroupList;
        }
        public static int SetChildGroupCurrentLevel(dcGLGroup parentGroup, int levelNo, List<dcGLGroup> pGLGroupList)
        {
            List<dcGLGroup> childGroupList = pGLGroupList.Where(c => c.GLGroupIDParent == parentGroup.GLGroupID).ToList();
            parentGroup.ChildGroupCount = childGroupList.Count;

            int count = 0;
            foreach (dcGLGroup childGroup in childGroupList)
            {
                count++;
                childGroup.HasParent = false;
                if (childGroup.GLGroupIDParent > 0)
                {
                    if (pGLGroupList.Exists(c => c.GLGroupID == childGroup.GLGroupIDParent))
                    {
                        childGroup.HasParent = true;
                    }
                }
                childGroup.GLGroupLevelCurrent = levelNo;
                int nextLevelNo = levelNo + 1;
                childGroup.ChildGroupCount = SetChildGroupCurrentLevel(childGroup, nextLevelNo, pGLGroupList);
            }
            return count;
        }

        public static List<dcGLGroup> GetGLGroupListByAccountList(List<dcGLAccount> pGLAccountList, List<dcGLGroup> pGLGroupListFull)
        {
            List<dcGLGroup> newGrpList = new List<dcGLGroup>();

            if (pGLAccountList == null || pGLGroupListFull == null)
            {
                return newGrpList;
            }

            if (pGLAccountList.Count == 0 || pGLGroupListFull.Count == 0)
            {
                return newGrpList;
            }


            foreach (dcGLAccount acc in pGLAccountList)
            {
                dcGLGroup cGroup = newGrpList.FirstOrDefault(c => c.GLGroupID == acc.GLGroupID);
                if (cGroup == null)
                {
                    dcGLGroup nGroup = pGLGroupListFull.FirstOrDefault(c => c.GLGroupID == acc.GLGroupID);
                    if (nGroup != null)
                    {
                        newGrpList.Add(nGroup);
                    }
                }
            }
            return newGrpList;
        }

        public static List<dcGLGroup> GetGLGroupListByGLGroupID(int pCompanyID, int pGLGroupID, bool pIncludeChild, List<dcGLGroup> pGLGroupListFull)
        {
            return GetGLGroupListByGLGroupID(pCompanyID, pGLGroupID, pIncludeChild, pGLGroupListFull, null);
        }
        public static List<dcGLGroup> GetGLGroupListByGLGroupID(int pCompanyID, int pGLGroupID, bool pIncludeChild, List<dcGLGroup> pGLGroupListFull, DBContext dc)
        {
            List<dcGLGroup> cObjList = new List<dcGLGroup>();

            dcGLGroup cGrp = pGLGroupListFull.SingleOrDefault(c => c.GLGroupID == pGLGroupID);
            if (cGrp != null)
            {
                cObjList.Add(cGrp);
                if (pIncludeChild)
                {
                    CheckAndAddChildGroup(pCompanyID, cGrp.GLGroupID, cObjList, pGLGroupListFull, dc);
                }
            }

            return cObjList;
        }

        public static bool CheckAndAddChildGroup(int pCompanyID, int pGLGroupIDParent, List<dcGLGroup> pGLGroupListNew, List<dcGLGroup> pGLGroupListFull, DBContext dc)
        {
            if (pGLGroupListFull == null)
            {
                pGLGroupListFull = GetGLGroupList(pCompanyID, dc);
            }
            List<dcGLGroup> childGroupList = pGLGroupListFull.Where(c => c.GLGroupIDParent == pGLGroupIDParent).ToList();
        
            int count = 0;
            foreach (dcGLGroup childGroup in childGroupList)
            {
                count++;
                dcGLGroup cGrp = pGLGroupListNew.SingleOrDefault(c => c.GLGroupID == childGroup.GLGroupID);
                if (cGrp == null)
                {
                    pGLGroupListNew.Add(childGroup);
                }
                CheckAndAddChildGroup(pCompanyID, childGroup.GLGroupID, pGLGroupListNew, pGLGroupListFull, dc);
            }
            return count > 0;
        }


        public static dcGLGroup GetGLGroupByID(int pCompanyID, int pGLGroupID)
        {
            return GetGLGroupByID(pCompanyID , pGLGroupID, null);
        }
        public static dcGLGroup GetGLGroupByID(int pCompanyID, int pGLGroupID, DBContext dc)
        {
            //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            DBCommandInfo cmdInfo = new DBCommandInfo();
            System.Text.StringBuilder sb = new System.Text.StringBuilder(GetGLGroupListString());

            sb.Append(" AND tblGLGroup.CompanyID=@companyID ");
            cmdInfo.DBParametersInfo.Add("@companyID", pCompanyID);
            //cmd.Parameters.AddWithValue("@companyID", pCompanyID);


            if (pGLGroupID > 0)
            {
                sb.Append(" AND tblGLGroup.glGroupID=@glGroupID ");
                //cmd.Parameters.AddWithValue("@glGroupID", pGLGroupID);
                cmdInfo.DBParametersInfo.Add("@glGroupID", pGLGroupID);
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

            dcGLGroup cObj = GetGLGroupList(dbq, dc).FirstOrDefault();
            return cObj;
        }

  
        public static int Insert(dcGLGroup cObj)
        {
            return Insert(cObj, null);
        }
        public static int Insert(dcGLGroup cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcGLGroup>(cObj, true);
                if (id > 0) { cObj.GLGroupID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcGLGroup cObj)
        {
            return Update(cObj, null);
        }
        public static bool Update(dcGLGroup cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcGLGroup>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pGLGroupID)
        {
            return Delete(pGLGroupID, null);
        }
        public static bool Delete(int pGLGroupID, DBContext dc)
        {
            dcGLGroup cObj = new dcGLGroup();
            cObj.GLGroupID = pGLGroupID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcGLGroup>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcGLGroup cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }
        public static int Save(dcGLGroup cObj, bool isAdd, DBContext dc)
        {
            cObj._RecordState = isAdd ? RecordStateEnum.Added : RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcGLGroup cObj)
        {
            return Save(cObj, null);
        }
        public static int Save(dcGLGroup cObj, DBContext dc)
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
                    if (cObj.GLGroupIDParent > 0)
                    {
                        dcGLGroup grpParent = GLGroupBL.GetGLGroupByID(cObj.CompanyID, cObj.GLGroupIDParent, dc);
                        if (grpParent != null)
                        {
                            cObj.IsGrossProfit = grpParent.IsGrossProfit;
                            cObj.GLGroupLevel = grpParent.GLGroupLevel + 1;

                            if (cObj._RecordState == RecordStateEnum.Added)
                            {
                                cObj.IsCash = grpParent.IsCash;
                                cObj.IsBank = grpParent.IsBank;
                                cObj.IsInstrument = grpParent.IsInstrument;
                                cObj.IsInventory = grpParent.IsInventory;
                                cObj.CashFlowGroupID = grpParent.CashFlowGroupID;
                            }
                        }
                    }
                    else
                    {
                        cObj.GLGroupLevel = 0;
                    }


                    switch (cObj._RecordState)
                    {
                        case RecordStateEnum.Added:
                            newID = Insert(cObj, dc);
                            break;
                        case RecordStateEnum.Edited:
                            if (Update(cObj, dc))
                            {
                                newID = cObj.GLGroupID;
                            }
                            break;
                        case RecordStateEnum.Deleted:
                            if (Delete(cObj.GLGroupID, dc))
                            {
                                newID = cObj.GLGroupID;
                            }
                            break;
                        default:
                            break;
                    }

                    if (newID > 0)
                    {
                        bool bStatus = false;





                        if (cObj._RecordState == RecordStateEnum.Edited)
                        {
                            UpdateGLGroupPropToChild(cObj.CompanyID, newID, dc);
                        }



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


        public static bool IsGLGroupNameExists(int pCompanyID, string pGLGroupName)
        {
            return IsGLGroupNameExists(pCompanyID, pGLGroupName, null);
        }
        public static bool IsGLGroupNameExists(int pCompanyID, string pGLGroupName, DBContext dc)
        {
            bool isData = false;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                StringBuilder sb = new StringBuilder(GetGLGroupListString());

                sb.Append(" AND tblGLGroup.GLGroupName=@gLGroupName ");
                //cmd.Parameters.AddWithValue("@gLGroupName", pGLGroupName);
                cmdInfo.DBParametersInfo.Add("@gLGroupName", pGLGroupName);

                //if (pGLGroupIDParent != -1)
                //{
                //    sb.Append(" AND tblGLGroup.GLGroupIDParent=@gLGroupIDParent ");
                //    cmd.Parameters.AddWithValue("@gLGroupIDParent", pGLGroupIDParent);
                //}

                sb.Append(" AND tblGLGroup.CompanyID=@companyID ");
                //cmd.Parameters.AddWithValue("@companyID", pCompanyID);
                cmdInfo.DBParametersInfo.Add("@companyID", pCompanyID);

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

                isData = GetGLGroupList(dbq, dc).Count > 0;

            }
            finally
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }
            return isData;
        }
        public static bool IsGLGroupNameExists(int pCompanyID, string pGLGroupName, int pGLGroupID)
        {
            return IsGLGroupNameExists(pCompanyID, pGLGroupName, pGLGroupID, null);
        }
        public static bool IsGLGroupNameExists(int pCompanyID, string pGLGroupName, int pGLGroupID, DBContext dc)
        {
            bool isData = false;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                StringBuilder sb = new StringBuilder(GetGLGroupListString());

                sb.Append(" AND tblGLGroup.GLGroupName=@gLGroupName ");
                //cmd.Parameters.AddWithValue("@gLGroupName", pGLGroupName);
                cmdInfo.DBParametersInfo.Add("@gLGroupName", pGLGroupName);

                //if (pGLGroupIDParent != -1)
                //{
                //    sb.Append(" AND tblGLGroup.GLGroupIDParent=@gLGroupIDParent ");
                //    cmd.Parameters.AddWithValue("@gLGroupIDParent", pGLGroupIDParent);
                //}

                sb.Append(" AND tblGLGroup.CompanyID=@companyID ");
                //cmd.Parameters.AddWithValue("@companyID", pCompanyID);
                cmdInfo.DBParametersInfo.Add("@companyID", pCompanyID);


                sb.Append(" AND tblGLGroup.GLGroupID <> @gLGroupID ");
                //cmd.Parameters.AddWithValue("@gLGroupID", pGLGroupID);
                cmdInfo.DBParametersInfo.Add("@gLGroupID", pGLGroupID);

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
                isData = GetGLGroupList(dbq, dc).Count > 0;
            }
            finally
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }
            return isData;
        }


        public static bool IsGLGroupCodeExists(int pCompanyID, string pGLGroupCode)
        {
            return IsGLGroupCodeExists(pCompanyID, pGLGroupCode, null);
        }
        public static bool IsGLGroupCodeExists(int pCompanyID, string pGLGroupCode, DBContext dc)
        {
            bool isData = false;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                StringBuilder sb = new StringBuilder(GetGLGroupListString());

                sb.Append(" AND tblGLGroup.GLGroupCode=@gLGroupCode ");
                //cmd.Parameters.AddWithValue("@gLGroupCode", pGLGroupCode);
                cmdInfo.DBParametersInfo.Add("@gLGroupCode", pGLGroupCode);

                //if (pGLGroupIDParent != -1)
                //{
                //    sb.Append(" AND tblGLGroup.GLGroupIDParent=@gLGroupIDParent ");
                //    cmd.Parameters.AddWithValue("@gLGroupIDParent", pGLGroupIDParent);
                //}

                sb.Append(" AND tblGLGroup.CompanyID=@companyID ");
                //cmd.Parameters.AddWithValue("@companyID", pCompanyID);
                cmdInfo.DBParametersInfo.Add("@companyID", pCompanyID);

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
                isData = GetGLGroupList(dbq, dc).Count > 0;

            }
            finally
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }
            return isData;
        }
        public static bool IsGLGroupCodeExists(int pCompanyID, string pGLGroupName, int pGLGroupID)
        {
            return IsGLGroupCodeExists(pCompanyID, pGLGroupName, pGLGroupID, null);
        }
        public static bool IsGLGroupCodeExists(int pCompanyID, string pGLGroupName, int pGLGroupID, DBContext dc)
        {
            bool isData = false;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                StringBuilder sb = new StringBuilder(GetGLGroupListString());

                sb.Append(" AND tblGLGroup.GLGroupName=@gLGroupName ");
                //cmd.Parameters.AddWithValue("@gLGroupName", pGLGroupName);
                cmdInfo.DBParametersInfo.Add("@gLGroupName", pGLGroupName);

                //if (pGLGroupIDParent != -1)
                //{
                //    sb.Append(" AND tblGLGroup.GLGroupIDParent=@gLGroupIDParent ");
                //    cmd.Parameters.AddWithValue("@gLGroupIDParent", pGLGroupIDParent);
                //}

                sb.Append(" AND tblGLGroup.CompanyID=@companyID ");
                //cmd.Parameters.AddWithValue("@companyID", pCompanyID);
                cmdInfo.DBParametersInfo.Add("@companyID", pCompanyID);


                sb.Append(" AND tblGLGroup.GLGroupID <> @gLGroupID ");
                //cmd.Parameters.AddWithValue("@gLGroupID", pGLGroupID);
                cmdInfo.DBParametersInfo.Add("@gLGroupID", pGLGroupID);

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
                isData = GetGLGroupList(dbq, dc).Count > 0;
            }
            finally
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }
            return isData;
        }


        public static bool IsGLGroupNameShortExists(int pCompanyID, string pGLGroupNameShort)
        {
            return IsGLGroupNameShortExists(pCompanyID, pGLGroupNameShort, null);
        }
        public static bool IsGLGroupNameShortExists(int pCompanyID, string pGLGroupNameShort, DBContext dc)
        {
            bool isData = false;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);

                DBCommandInfo cmdInfo = new DBCommandInfo();
                //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                StringBuilder sb = new StringBuilder(GetGLGroupListString());

                sb.Append(" AND tblGLGroup.GLGroupNameShort=@gLGroupNameShort ");
                //cmd.Parameters.AddWithValue("@gLGroupNameShort", pGLGroupNameShort);
                cmdInfo.DBParametersInfo.Add("@gLGroupNameShort", pGLGroupNameShort);

                //if (pGLGroupIDParent != -1)
                //{
                //    sb.Append(" AND tblGLGroup.GLGroupIDParent=@gLGroupIDParent ");
                //    cmd.Parameters.AddWithValue("@gLGroupIDParent", pGLGroupIDParent);
                //}

                sb.Append(" AND tblGLGroup.CompanyID=@companyID ");
                //cmd.Parameters.AddWithValue("@companyID", pCompanyID);
                cmdInfo.DBParametersInfo.Add("@companyID", pCompanyID);

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
                isData = GetGLGroupList(dbq, dc).Count > 0;

            }
            finally
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }
            return isData;
        }
        public static bool IsGLGroupNameShortExists(int pCompanyID, string pGLGroupNameShort, int pGLGroupID)
        {
            return IsGLGroupNameShortExists(pCompanyID, pGLGroupNameShort, pGLGroupID, null);
        }
        public static bool IsGLGroupNameShortExists(int pCompanyID, string pGLGroupNameShort, int pGLGroupID, DBContext dc)
        {
            bool isData = false;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                StringBuilder sb = new StringBuilder(GetGLGroupListString());

                sb.Append(" AND tblGLGroup.GLGroupNameShort=@gLGroupNameShort ");
                //cmd.Parameters.AddWithValue("@gLGroupNameShort", pGLGroupNameShort);
                cmdInfo.DBParametersInfo.Add("@gLGroupNameShort", pGLGroupNameShort);

                //if (pGLGroupIDParent != -1)
                //{
                //    sb.Append(" AND tblGLGroup.GLGroupIDParent=@gLGroupIDParent ");
                //    cmd.Parameters.AddWithValue("@gLGroupIDParent", pGLGroupIDParent);
                //}

                sb.Append(" AND tblGLGroup.CompanyID=@companyID ");
                //cmd.Parameters.AddWithValue("@companyID", pCompanyID);
                cmdInfo.DBParametersInfo.Add("@companyID", pCompanyID);


                sb.Append(" AND tblGLGroup.GLGroupID <> @gLGroupID ");
                //cmd.Parameters.AddWithValue("@gLGroupID", pGLGroupID);
                cmdInfo.DBParametersInfo.Add("@gLGroupID", pGLGroupID);

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
                isData = GetGLGroupList(dbq, dc).Count > 0;
            }
            finally
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }
            return isData;
        }



        public static List<dcGLGroup> UpdateGLGroupLevelCurrent(List<dcGLGroup> glGroupList)
        {
            if (glGroupList == null)
            {
                return new List<dcGLGroup>();
            }
            List<dcGLGroup> newGroupList = new List<dcGLGroup>();

            int maxLevel = glGroupList.Max(c => c.GLGroupLevel);


            for (int i = 0; i < maxLevel; i++)
            {
                List<dcGLGroup> rootGroups = glGroupList.Where(c => c.GLGroupLevel == i).ToList();
                foreach (dcGLGroup rtGroup in rootGroups)
                {
                    if (rtGroup.GLGroupIDParent > 0)
                    {
                        dcGLGroup pGrp = glGroupList.FirstOrDefault(c => c.GLGroupID == rtGroup.GLGroupIDParent);
                        if (pGrp == null)
                        {
                            rtGroup.GLGroupLevelCurrent = 0;
                        }
                        else
                        {
                            rtGroup.GLGroupLevelCurrent = pGrp.GLGroupLevelCurrent + 1;
                        }
                    }
                    else
                    {
                        rtGroup.GLGroupLevelCurrent = 0;
                    }
                }
            }



            return newGroupList;
        }



     

        public static int GetRootGroupID(int pCompanyID, int pGLGroupID)
        {
            return GetRootGroupID(pCompanyID,pGLGroupID, null, null);
        }
        public static int GetRootGroupID(int pCompanyID, int pGLGroupID, DBContext dc)
        {
            return GetRootGroupID(pCompanyID, pGLGroupID, null, dc);
        }


        public static int GetRootGroupID(int pCompanyID, int pGLGroupID, List<dcGLGroup> pGLGroupList)
        {
            return GetRootGroupID(pCompanyID, pGLGroupID, pGLGroupList, null);
        }
        public static int GetRootGroupID(int pCompanyID, int pGLGroupID, List<dcGLGroup> pGLGroupList, DBContext dc)
        {
            int rootGrpID = 0;

            if (pGLGroupList == null)
            {
                pGLGroupList = GetGLGroupList(pCompanyID, dc);
            }

            int grpIDParent = 0;
            dcGLGroup grp = pGLGroupList.SingleOrDefault(c => c.GLGroupID == pGLGroupID);
            if (grp != null)
            {
                grpIDParent = grp.GLGroupIDParent;
            }

            while (grpIDParent != 0)
            {
                dcGLGroup grp1 = pGLGroupList.SingleOrDefault(c => c.GLGroupID == grpIDParent);
                if (grp1 != null)
                {
                    grpIDParent = grp1.GLGroupIDParent;
                }
                else
                {
                    grpIDParent = 0;
                }
            }


            rootGrpID = grpIDParent;
            return rootGrpID;
        }

        public static void SetGroupNameHierarchy(List<dcGLGroup> grpList)
        {
            SetGroupNameHierarchy(grpList, " -> ");
        }
        public static void SetGroupNameHierarchy(List<dcGLGroup> grpList, string pSeparator)
        {
            SetGroupNameHierarchy(grpList, pSeparator, false);
        }
        public static void SetGroupNameHierarchy(List<dcGLGroup> grpList, string pSeparator, bool isReverse)
        {
            List<dcGLGroup> cList = (from c in grpList
                                        select c).ToList();

            StringBuilder sbName = new StringBuilder();
            string curSeparator = string.Empty;
            foreach (dcGLGroup grp in cList)
            {
                sbName.Length = 0;
                if (grp.GLGroupIDParent > 0)
                {
                    int curParentGrpID = grp.GLGroupIDParent;
                    while (curParentGrpID > 0)
                    {
                        curSeparator = sbName.Length == 0 ? string.Empty : pSeparator;
                        dcGLGroup grpParent = grpList.SingleOrDefault(g => g.GLGroupID == curParentGrpID);
                        sbName.Append(curSeparator).Append(grpParent.GLGroupName);
                        curParentGrpID = grpParent.GLGroupIDParent;
                    }
                    grp.GLGroupNameParentHierarchy = sbName.ToString();
                    grp.GLGroupNameHierarchy = sbName.Append(pSeparator).Append(grp.GLGroupName).ToString();
                }
                else
                {
                    grp.GLGroupNameParentHierarchy = string.Empty;
                    grp.GLGroupNameHierarchy = grp.GLGroupName;
                }
            }//loop
        }


        public static List<dcGLGroup> GetGroupBalance(clsPrmLedger prmLedger)
        {
            return GetGroupBalance(prmLedger, null, null, null);
        }
        public static List<dcGLGroup> GetGroupBalance(clsPrmLedger prmLedger, DBContext dc)
        {
            return GetGroupBalance(prmLedger, null, null, dc);
        }
        public static List<dcGLGroup> GetGroupBalance(clsPrmLedger prmLedger, List<dcGLGroup> grpList)
        {
            return GetGroupBalance(prmLedger, grpList, null, null);
        }
        public static List<dcGLGroup> GetGroupBalance(clsPrmLedger prmLedger, List<dcGLGroup> grpList, DBContext dc)
        {
            return GetGroupBalance(prmLedger, grpList, null, dc);
        }
        public static List<dcGLGroup> GetGroupBalance(clsPrmLedger prmLedger, List<dcGLGroup> grpList, List<dcGLAccount> accBalanceList)
        {
            return GetGroupBalance(prmLedger, grpList, accBalanceList, null);
        }
        public static List<dcGLGroup> GetGroupBalance(clsPrmLedger prmLedger, List<dcGLGroup> grpList, List<dcGLAccount> accBalanceList, DBContext dc)
        {
            if (grpList == null)
            {
                grpList = GetGLGroupList(prmLedger.CompanyID, dc);
                //grpList = FormatGLGroup(prmLedger, grpList);
            }
            grpList = SetGLGroupListLevelCurrent(grpList);



            if (accBalanceList == null)
            {
                accBalanceList = GLAccountBL.GetAccountBalance(prmLedger, grpList, dc);
            }

            if (grpList.Count == 0)
            {
                return grpList;
            }

            decimal openDebitAmtYear = 0;
            decimal openCreditAmtYear = 0;

            decimal openDebitAmtDateRange = 0;
            decimal openCreditAmtDateRange = 0;


            decimal tranDebitAmt = 0;
            decimal tranCreditAmt = 0;


            ///now sum from reverse group level
            //int grpMaxLevel = grpList.Max(c => c.GLGroupLevel);
            int grpMaxLevel = grpList.Max(c => c.GLGroupLevelCurrent);

            int grpCurLevel = grpMaxLevel;
            while (grpCurLevel >= 0)
            {
                //List<dcGLGroup> grpLvlList = grpList.Where(c => c.GLGroupLevel == grpCurLevel).ToList();
                List<dcGLGroup> grpLvlList = grpList.Where(c => c.GLGroupLevelCurrent == grpCurLevel).ToList();
                foreach (dcGLGroup grp in grpLvlList)
                {
                    // dcGLGroup grp = grpList.Single(c => c.AccGLGroupID == grpL.AccGLGroupID);

                    openDebitAmtYear = 0;
                    openCreditAmtYear = 0;

                    openDebitAmtDateRange = 0;
                    openCreditAmtDateRange = 0;

                    tranDebitAmt = 0;
                    tranCreditAmt = 0;

                    //get sub group
                    List<dcGLGroup> grpSupList = grpList.Where(c => c.GLGroupIDParent == grp.GLGroupID).ToList();
                    foreach (dcGLGroup grpSub in grpSupList)
                    {
                        openDebitAmtYear += grpSub.OpenDebitAmtYear;
                        openCreditAmtYear += grpSub.OpenCreditAmtYear;

                        openDebitAmtDateRange += grpSub.OpenDebitAmtDateRange;
                        openCreditAmtDateRange += grpSub.OpenCreditAmtDateRange;

                        tranDebitAmt += grpSub.DebitAmt;
                        tranCreditAmt += grpSub.CreditAmt;

                    }

                    //get account withow control type
                    //List<dcGLAccount> grpAccList = accBalanceList.Where(c => c.GLGroupID == grp.GLGroupID
                    //                            && c.GLAccountTypeID != (int)GLAccountTypeEnum.ControlAccount).ToList();

                    //List<dcGLAccount> grpAccList = accBalanceList.Where(c => c.GLGroupID == grp.GLGroupID).ToList();

                    List<dcGLAccount> grpAccList = new List<dcGLAccount>();

                    switch (prmLedger.GLAccountTypeFilter)
                    {
                        case GLAccountTypeFilterEnum.AllAccount:
                        case GLAccountTypeFilterEnum.NoFilter:
                        case GLAccountTypeFilterEnum.NormalControlAccount:
                            grpAccList = accBalanceList.Where(c => c.GLGroupID == grp.GLGroupID
                                              & c.GLAccountTypeID != (int)GLAccountTypeEnum.SubAccount).ToList();
                            break;
                            case GLAccountTypeFilterEnum.NormalAccount:
                            case GLAccountTypeFilterEnum.ControlAccount:
                            case GLAccountTypeFilterEnum.SubAccount:
                                grpAccList = accBalanceList.Where(c => c.GLGroupID == grp.GLGroupID).ToList();
                                break;
                            case GLAccountTypeFilterEnum.NormalSubAccount:
                                grpAccList = accBalanceList.Where(c => c.GLGroupID == grp.GLGroupID
                                                  & c.GLAccountTypeID != (int)GLAccountTypeEnum.ControlAccount).ToList();
                                break;
                            case GLAccountTypeFilterEnum.ControlSubAccount:
                                grpAccList = accBalanceList.Where(c => c.GLGroupID == grp.GLGroupID
                                                  & c.GLAccountTypeID != (int)GLAccountTypeEnum.NormalAccount).ToList();
                                break;
                        case GLAccountTypeFilterEnum.SubAccountByControl:
                                grpAccList = accBalanceList.Where(c => c.GLGroupID == grp.GLGroupID 
                                                                & c.GLAccountIDParent == prmLedger.GLAccountID
                                                                & c.GLAccountTypeID == (int)GLAccountTypeEnum.SubAccount).ToList();
                                break;
                    }


                    //grpAccList = accBalanceList.Where(c => c.GLGroupID == grp.GLGroupID).ToList();

                    foreach (dcGLAccount acc in grpAccList)
                    {
                        openDebitAmtYear += acc.OpenDebitAmtYear;
                        openCreditAmtYear += acc.OpenCreditAmtYear;

                        openDebitAmtDateRange += acc.OpenDebitAmtDateRange;
                        openCreditAmtDateRange += acc.OpenCreditAmtDateRange;

                        tranDebitAmt += acc.DebitAmt;
                        tranCreditAmt += acc.CreditAmt;
                    }

                    //now sum up
                    //opening year
                    grp.OpenDebitAmtYear = openDebitAmtYear;
                    grp.OpenCreditAmtYear = openCreditAmtYear; ;
                    grp.OpenAmtYear = grp.OpenDebitAmtYear - grp.OpenCreditAmtYear;
                    grp.OpenDebitBalanceAmtYear = grp.OpenAmtYear >= 0 ? Math.Abs(grp.OpenAmtYear) : 0;
                    grp.OpenCreditBalanceAmtYear = grp.OpenAmtYear < 0 ? Math.Abs(grp.OpenAmtYear) : 0;
                    grp.OpenBalnceAmtYear = Math.Abs(grp.OpenAmtYear);
                    grp.DrCrOpenYear = AccHelper.GetDrCrBalanceType(grp.OpenAmtYear, grp.BalanceType);
                    grp.DrCrOpenTextYear = AccHelper.GetDrCrBalanceText(grp.OpenAmtYear, grp.BalanceType);


                    //opening daterange
                    grp.OpenDebitAmtDateRange =  openDebitAmtDateRange;
                    grp.OpenCreditAmtDateRange =  openCreditAmtDateRange; ;
                    grp.OpenAmtDateRange = grp.OpenDebitAmtDateRange - grp.OpenCreditAmtDateRange;
                    grp.OpenDebitBalanceAmtDateRange = grp.OpenAmtDateRange >= 0 ? Math.Abs(grp.OpenAmtDateRange) : 0;
                    grp.OpenCreditBalanceAmtDateRange = grp.OpenAmtDateRange <= 0 ? Math.Abs(grp.OpenAmtDateRange) : 0;
                    grp.OpenBalanceAmtDateRange = Math.Abs(grp.OpenAmtDateRange);
                    grp.DrCrOpenDateRange = AccHelper.GetDrCrBalanceType(grp.OpenAmtDateRange, grp.BalanceType);
                    grp.DrCrOpenTextDateRange = AccHelper.GetDrCrBalanceText(grp.OpenAmtDateRange, grp.BalanceType);



                    //opening
                    grp.OpenDebitAmt = openDebitAmtYear + openDebitAmtDateRange;
                    grp.OpenCreditAmt = openCreditAmtYear + openCreditAmtDateRange; ;
                    grp.OpenAmt = grp.OpenDebitAmt - grp.OpenCreditAmt;
                    grp.OpenDebitBalanceAmt = grp.OpenAmt >= 0 ? Math.Abs(grp.OpenAmt) : 0;
                    grp.OpenCreditBalanceAmt = grp.OpenAmt <= 0 ? Math.Abs(grp.OpenAmt) : 0;
                    grp.OpenBalnceAmt = Math.Abs(grp.OpenAmt);
                    grp.DrCrOpen = AccHelper.GetDrCrBalanceType(grp.OpenAmt, grp.BalanceType);
                    grp.DrCrOpenText = AccHelper.GetDrCrBalanceText(grp.OpenAmt, grp.BalanceType);

                    //
                    grp.DebitAmt = tranDebitAmt;
                    grp.CreditAmt = tranCreditAmt;
                    grp.TranAmt = grp.DebitAmt - grp.CreditAmt;
                    grp.TranDebitBalanceAmt = grp.TranAmt >= 0 ? Math.Abs(grp.TranAmt) : 0;
                    grp.TranCreditBalanceAmt = grp.TranAmt <= 0 ? Math.Abs(grp.TranAmt) : 0;
                    grp.TranBalanceAmt = Math.Abs(grp.TranAmt);
                    grp.DrCrTranBalance = AccHelper.GetDrCrBalanceType(grp.TranAmt, grp.BalanceType);
                    grp.DrCrTranBalanceText = AccHelper.GetDrCrBalanceText(grp.TranAmt, grp.BalanceType);



                    //closing
                    grp.CloseDebitAmt = grp.OpenDebitAmt + grp.DebitAmt;
                    grp.CloseCreditAmt = grp.OpenCreditAmt + grp.CreditAmt;
                    grp.CloseAmt = grp.CloseDebitAmt - grp.CloseCreditAmt;
                    grp.CloseDebitBalanceAmt = grp.CloseAmt >= 0 ? Math.Abs(grp.CloseAmt) : 0;
                    grp.CloseCreditBalanceAmt = grp.CloseAmt <= 0 ? Math.Abs(grp.CloseAmt) : 0;
                    grp.CloseBalanceAmt = Math.Abs(grp.CloseAmt);
                    grp.DrCrCloseBalance = AccHelper.GetDrCrBalanceType(grp.CloseAmt, grp.BalanceType);
                    grp.DrCrCloseBalanceText = AccHelper.GetDrCrBalanceText(grp.CloseAmt, grp.BalanceType); ;
                }
                grpCurLevel--;
            }

            return grpList;
        }

        public static void UpdateGLGroupClass(int pCompanyID)
        {
            UpdateGLGroupClass(pCompanyID, null);
        }
        public static void UpdateGLGroupClass(int pCompanyID, DBContext dc)
        {
            List<dcGLGroupClass> grpClassList = GLGroupClassBL.GetGLGroupClassList(dc);
            List<dcGLGroup> grpList = GLGroupBL.GetGLGroupList(pCompanyID, dc);


            List<dcGLGroupClass> grpClassListRoot = grpClassList.Where(c => c.GLGroupClassIDParent == 0).ToList();

            foreach(dcGLGroupClass grpClass in grpClassListRoot)
            {
                dcGLGroup grp = grpList.Where(c => c.GLGroupClassID == grpClass.GLGroupClassID).FirstOrDefault();

                int newID = 0;
                if (grp == null)
                {
                    dcGLGroup grpNew = new dcGLGroup();
                    grpNew.CompanyID = pCompanyID;
                    grpNew.GLClassID = grpClass.GLClassID;
                    grpNew.GLGroupClassID = grpClass.GLGroupClassID;
                    grpNew.GLGroupIDParent = 0;
                    grpNew.IsSystem = true;
                    grpNew.GLGroupLevel = grpClass.GLGroupClassLevel;
                    grpNew.GLGroupSLNo = grpClass.GLGroupClassSLNo;
                    grpNew.GLGroupCode = grpClass.GLGroupClassCode;
                    grpNew.GLGroupName = grpClass.GLGroupClassName;
                    grpNew.GLGroupNameSys = grpClass.GLGroupClassName;
                    grpNew.GLGroupNamePrint = grpClass.GLGroupClassName;
                    grpNew.BalanceType = grpClass.BalanceType;
                    grpNew.IsGrossProfit = grpClass.IsGrossProfit;
                    grpNew.IsCash = grpClass.IsCash;
                    grpNew.IsBank = grpClass.IsBank;
                    grpNew.IsInstrument = grpClass.IsInstrument;
                    grpNew.CashFlowGroupID = grpClass.CashFlowGroupID;
                    grpNew.ShowAlways = grpClass.ShowAlways;
                    newID = GLGroupBL.Insert(grpNew, dc);
                    
                }
                else
                {
                    newID = grp.GLGroupID;
                }

                //sub group
                List<dcGLGroupClass> grpClassListSub = grpClassList.Where(c => c.GLGroupClassIDParent == grpClass.GLGroupClassID).ToList();

                foreach (dcGLGroupClass grpClassSub in grpClassListSub)
                {
                    dcGLGroup grpSub = grpList.Where(c => c.GLGroupIDParent == newID 
                                                        && c.GLGroupClassID == grpClassSub.GLGroupClassID).FirstOrDefault();

                    if (grpSub == null)
                    {
                        dcGLGroup grpNewSub = new dcGLGroup();
                        grpNewSub.CompanyID = pCompanyID;
                        grpNewSub.GLClassID = grpClassSub.GLClassID;
                        grpNewSub.GLGroupClassID = grpClassSub.GLGroupClassID;
                        grpNewSub.GLGroupIDParent = newID;
                        grpNewSub.IsSystem = true;
                        grpNewSub.GLGroupLevel = grpClass.GLGroupClassLevel;
                        grpNewSub.GLGroupSLNo = grpClassSub.GLGroupClassSLNo;
                        grpNewSub.GLGroupCode = grpClassSub.GLGroupClassCode;
                        grpNewSub.GLGroupName = grpClassSub.GLGroupClassName;
                        grpNewSub.GLGroupNameSys = grpClassSub.GLGroupClassName;
                        grpNewSub.GLGroupNamePrint = grpClassSub.GLGroupClassName;
                        grpNewSub.IsGrossProfit = grpClassSub.IsGrossProfit;
                        grpNewSub.IsInstrument = grpClassSub.IsInstrument;
                        grpNewSub.IsCash = grpClass.IsCash;
                        grpNewSub.IsBank = grpClass.IsBank;
                        grpNewSub.CashFlowGroupID = grpClass.CashFlowGroupID;
                        int newIDSub = GLGroupBL.Insert(grpNewSub, dc);
                    }
                }
            }

        }

        public static string GetGLGroupULTreeText()
        {
            return GetGLGroupULTreeText(0, false, "glgroup_tree", null);
        }
        public static string GetGLGroupULTreeText(int pParentID)
        {
            return GetGLGroupULTreeText(pParentID, false, "glgroup_tree", null);
        }
        public static string GetGLGroupULTreeText(int pParentID, List<dcGLGroup> cList)
        {
            return GetGLGroupULTreeText(pParentID, false, "glgroup_tree", cList);
        }
        public static string GetGLGroupULTreeText(int pParentID, bool pCheckBox, List<dcGLGroup> cList)
        {
            return GetGLGroupULTreeText(pParentID, pCheckBox, "glgroup_tree", cList);
        }
        public static string GetGLGroupULTreeText(int pParentID, bool pCheckBox, string treeClassName, List<dcGLGroup> cList)
        {
            //if (cList == null)
            //{
            //    return string.Empty;
            //}


            StringBuilder sb = new StringBuilder();
            //sb.Append("<ul id='" + treeID + "' class='glgroup_tree'>");

            //string attrTabIndex = "tabindex=\"0\"";




            sb.Append("<ul class='" + treeClassName + "'>");
            
            FillGLGroupStringRecursive(sb, 0, -1, pCheckBox, cList);
            
            sb.Append("</ul>");

            return sb.ToString();
        }

        private static int FillGLGroupStringRecursive(StringBuilder pSbTree, int pParentID, int pLevel, bool pCheckBox, List<dcGLGroup> cList)
        {

            if (cList == null)
            {
                return 0;
            }

            int cnt = 0;
            string sTab = "\t";
            string sNewLine = "\r\n";


            pLevel++;

            List<dcGLGroup> pList = (from c in cList
                                        where c.GLGroupIDParent == pParentID
                                        orderby c.GLGroupSLNo, c.GLGroupName
                                        select c).ToList();

            foreach (dcGLGroup grp in pList)
            {
                cnt++;
                int childCount = (from c in cList
                                  where c.GLGroupIDParent == grp.GLGroupID
                                  orderby c.GLGroupSLNo, c.GLGroupName
                                  select c).Count();

                //string jsScript = "javascript:onGLGroupSelect(" + grp.AccGLGroupID.ToString() + ");";
                string strPrefix = PG.Core.Utility.Helper.RepeatString(sTab, pLevel);
                pSbTree.AppendLine();
                pSbTree.Append(strPrefix);

                //string jsClick = "onGLGroupSelect(" + grp.AccGLGroupID.ToString() + ");";
                string attrGID = "gid=\"" + grp.GLGroupID.ToString() + "\"";
                string attrGName = "gname=\"" + System.Web.HttpUtility.HtmlEncode(grp.GLGroupName) + "\"";

                string attrChildCount = "childcount=\"" + childCount.ToString() + "\"";


                string attrNodeClass = "class=\"group_node\"";
                string attrNodeSpanClass = "class=\"group_nodeSpan\"";

                //data="key: '3', isFolder: true"
                //with checkbox
                //pSbTree.Append("<li " + attrLiID + " " + attrData + "><div style=\"height:20px;width:200px\"> <input type=\"checkbox\" /><span class=\"tree_Node\" " + attrID + " " + attrChildCount + " onclick= " + jsClick + " >").Append(grp.AccGLGroupName).Append("</span></div>");

                if (pCheckBox)
                {
                    pSbTree.Append("<li " + attrGID + " " + attrGName + " " + attrChildCount + " " + attrNodeClass + ">");
                    pSbTree.Append("<div>");
                    pSbTree.Append("<input type=\"checkbox\" class=\"group_nodeCheckBox\"/>");
                    pSbTree.Append("<span " + attrNodeSpanClass + ">").Append(grp.GLGroupName).Append("</span>");
                    pSbTree.Append("</div>");
                }
                else
                {
                    pSbTree.Append("<li " + attrGID + " " + attrGName + " " + attrChildCount + " " + attrNodeClass + ">");
                    pSbTree.Append("<span " + attrNodeSpanClass + ">").Append(grp.GLGroupName).Append("</span>");
                }


                //pSbTree.Append("<span " + attrGID + " " + attrNodeSpanClass + ">").Append(grp.AccGLGroupName).Append("</span>");

                //remember the positon for insert <ul> tab if it has child node
                int ulPos = pSbTree.Length;

                //Recurisive call
                int totChild = FillGLGroupStringRecursive(pSbTree, grp.GLGroupID, pLevel, pCheckBox, cList);
                if (totChild > 0)
                {
                    string ulTag = sNewLine + strPrefix + "<ul>" + sNewLine;
                    pSbTree.Insert(ulPos, ulTag);
                    pSbTree.AppendLine();
                    pSbTree.Append(strPrefix + "</ul>");
                    pSbTree.AppendLine();

                }
                pSbTree.Append("</li>");

            }
            return cnt;
        }

        public static bool IsGLGroupInGLGroup(int pGLGroupID, int pParentGLGroupID)
        {
            return IsGLGroupInGLGroup(pGLGroupID, pParentGLGroupID, null, null);
        }
        public static bool IsGLGroupInGLGroup(int pGLGroupID, int pParentGLGroupID, List<dcGLGroup> pGLGroupList)
        {
            return IsGLGroupInGLGroup(pGLGroupID, pParentGLGroupID, pGLGroupList, null);
        }
        public static bool IsGLGroupInGLGroup(int pGLGroupID, int pParentGLGroupID, List<dcGLGroup> pGLGroupList, DBContext dc)
        {
            bool bStatus = false;


            if (pGLGroupList == null)
            {
                //pGLGroupList = GetGLGroupList(true, true, AccOrderByEnum.Code, "-", dc);
            }

            dcGLGroup glGroup = pGLGroupList.SingleOrDefault(c => c.GLGroupID == pGLGroupID);

            int curParentID = glGroup.GLGroupIDParent;

            bStatus = pParentGLGroupID == curParentID;

            while (bStatus == false)
            {
                dcGLGroup parentGroup = pGLGroupList.Single(c => c.GLGroupID == curParentID);
                curParentID = parentGroup.GLGroupID;
                bStatus = pParentGLGroupID == curParentID;
                if (curParentID == 0)
                {
                    break;
                }
                else
                {
                    curParentID = parentGroup.GLGroupIDParent;
                }
            }
            return bStatus;
        }

        public static bool UpdateGLGroupPropToChild(int pCompanyID,  int pGLGroupIDParent, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                isTransInit = dc.StartTransaction();
                dcGLGroup grpParent = GetGLGroupByID(pCompanyID, pGLGroupIDParent, dc);

                List<dcGLGroup> grpChildList = GetGLGroupListByParent(pGLGroupIDParent, dc);

                foreach (dcGLGroup grpChild in grpChildList)
                {
                    dcGLGroup grpUpdate = new dcGLGroup();
                    grpUpdate.GLGroupID = grpChild.GLGroupID;

                    grpUpdate.IsGrossProfit = grpParent.IsGrossProfit;

                    //grpUpdate.IsCash = grpParent.IsCash;
                    //grpUpdate.IsBank = grpParent.IsBank;
                    //grpUpdate.IsInstrument = grpParent.IsInstrument;
                    //grpUpdate.CashFlowGroupID = grpParent.CashFlowGroupID;

                    Update(grpUpdate, dc);
                    UpdateGLGroupPropToChild(pCompanyID, grpChild.GLGroupID, dc);
                }
                dc.CommitTransaction(isTransInit);
                bStatus = true;
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return bStatus;
        }

        public static List<dcGLGroup> GetGLGroupListFull(bool includeRoot, bool orderByGroup, AccOrderByEnum pOrderBy, string indentChar)
        {
            return GetGLGroupListFull(includeRoot, orderByGroup, pOrderBy, indentChar, null);
        }
        public static List<dcGLGroup> GetGLGroupListFull(bool includeRoot, bool orderByGroup, AccOrderByEnum pOrderBy, string indentChar, DBContext dc)
        {
            List<dcGLGroup> cObjList = new List<dcGLGroup>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    cObjList = (from c in dataContext.GetTable<dcGLGroup>()
                                orderby c.GLGroupSLNo
                                select c).ToList();

                    //cObjList = FormatGLGroup(cObjList, includeRoot, orderByGroup, pOrderBy, indentChar, cObjList);

                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }



    }
}
