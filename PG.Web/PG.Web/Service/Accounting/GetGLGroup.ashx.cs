using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Text;
using System.Web.Script.Serialization;

using PG.Core.DBBase;
using PG.Core.DBFilters;
using PG.Core.Web;

using PG.DBClass.AccountingDC.AccEnums;
using PG.DBClass.AccountingDC.GeneralLedgerDC;
using PG.BLLibrary.AccountingBL;
using PG.BLLibrary.AccountingBL.GeneralLedgerBL;

namespace PG.Service.Accounting
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class GetGLGroup : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        { 

            //System.Collections.Specialized.StringDictionary list = new System.Collections.Specialized.StringDictionary();

            int isTerm = WebUtility.GetQueryStringInteger("isterm", context);
            //int includeEmpty = WebUtility.GetQueryStringInteger("includeempty", context);
            int isCodeName = WebUtility.GetQueryStringInteger("iscodename", context);

            int isPaging = WebUtility.GetQueryStringInteger("ispaging", context);

            int rows = WebUtility.GetQueryStringInteger("rows", context);
            int page = WebUtility.GetQueryStringInteger("page", context);

            int grpID = WebUtility.GetQueryStringInteger("id", context);

            string searchTerm = WebUtility.GetQueryString("searchTerm", context);
            string grpCode = WebUtility.GetQueryString("code", context);

            int codeCompType = WebUtility.GetQueryStringInteger("codecomptype", context);
            
            string grpName = WebUtility.GetQueryString("name", context);
            int nameCompType = WebUtility.GetQueryStringInteger("namecomptype", context);

            string grpNameShort = WebUtility.GetQueryString("nameshort", context);
            int nameShortCompType = WebUtility.GetQueryStringInteger("nameshortcomptype", context);


            int companyID = WebUtility.GetQueryStringInteger("companyid", context);

            int isNextGLAccountCode = WebUtility.GetQueryStringInteger("isnextacccode", context);


            List<int> glGroupClassInclude = WebUtility.GetQueryStringIntList("glgroupclassinclude", context);
            List<int> glGroupClassExclude = WebUtility.GetQueryStringIntList("glgroupclassexclude", context);
            List<int> glClassList = WebUtility.GetQueryStringIntList("glclass", context);

            string sortBy = WebUtility.GetQueryString("sidx", context); //colname
            string sortOrder = WebUtility.GetQueryString("sord", context);  //asc,desc

            int includeHeirerchy = WebUtility.GetQueryStringInteger("includehr", context);

            if (isTerm == 1)
            {
                grpCode = searchTerm;
            }

            if (isCodeName == 1)
            {
                grpName = grpCode;
                grpNameShort = grpCode;
            }


            //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            System.Text.StringBuilder sbStatment = new System.Text.StringBuilder();
            sbStatment.Append(GLGroupBL.GetGLGroupListString());
     
            DBQuery dbq = new DBQuery();
            List<DBFilter> filterList = new List<DBFilter>();

            filterList.Add(new DBFilter("tblGLGroup.CompanyID", companyID));


            if (grpID > 0)
            {
                filterList.Add(new DBFilter("tblGLGroup.GLGroupID", grpID));

                //sb.Append(" AND tblGLAccount.GLAccountID=@accID ");
                //cmd.Parameters.AddWithValue("@accID", accID);
            }


            List<DBFilter> filterListCN = new List<DBFilter>();
            bool isCodeNameFilter = false;
            if (grpCode != string.Empty)
            {
                DBFilterCompareTypeEnum compTypeGrpCode = DBFilterCompareTypeEnum.EqualTo;
                if (codeCompType > 0)
                {
                    compTypeGrpCode = DBFilterManager.GetCompareTypeFormInt(codeCompType);
                }
                filterListCN.Add(new DBFilter("tblGLGroup.GLGroupCode", grpCode, DBFilterDataTypeEnum.String, compTypeGrpCode));
                isCodeNameFilter = true;
            }

            if (grpName != string.Empty)
            {
                DBFilterCompareTypeEnum compTypeGrpName = DBFilterCompareTypeEnum.StartsWith;
                if (nameCompType > 0)
                {
                    compTypeGrpName = DBFilterManager.GetCompareTypeFormInt(nameCompType);
                }
                if (isCodeName == 1)
                {
                    filterListCN.Add(new DBFilter("tblGLGroup.GLGroupName", grpName, DBFilterDataTypeEnum.String, compTypeGrpName, DBFilterCombineTypeEnum.OR));
                }
                else
                {
                    filterListCN.Add(new DBFilter("tblGLGroup.GLGroupName", grpName, DBFilterDataTypeEnum.String, compTypeGrpName));
                }
                isCodeNameFilter = true;
            }

            if (grpNameShort != string.Empty)
            {
                DBFilterCompareTypeEnum compTypeGrpNameShort = DBFilterCompareTypeEnum.StartsWith;
                if (nameShortCompType > 0)
                {
                    compTypeGrpNameShort = DBFilterManager.GetCompareTypeFormInt(nameShortCompType);
                }
                if (isCodeName == 1)
                {
                    filterListCN.Add(new DBFilter("tblGLGroup.GLGroupNameShort", grpNameShort, DBFilterDataTypeEnum.String, compTypeGrpNameShort, DBFilterCombineTypeEnum.OR));
                }
                else
                {
                    filterListCN.Add(new DBFilter("tblGLGroup.GLGroupNameShort", grpNameShort, DBFilterDataTypeEnum.String, compTypeGrpNameShort));
                }
                isCodeNameFilter = true;
            }

            if (isCodeNameFilter)
            {
                if (isCodeName == 1)
                {
                    filterList.Add(new DBFilter(filterListCN));
                }
                else
                {
                    filterList.AddRange(filterListCN);
                }
            }

            if (glGroupClassInclude.Count > 0)
            {
                if (glGroupClassInclude[0] != 0)
                {
                    filterList.Add(new DBFilter("tblGLGroup.GLGroupClassID", glGroupClassInclude, DBFilterDataTypeEnum.Integer, DBFilterCompareTypeEnum.IN));
                }
            }

            if (glGroupClassExclude.Count > 0)
            {
                if (glGroupClassExclude[0] != 0)
                {
                    DBFilter flt = new DBFilter("tblGLGroup.GLGroupClassID", glGroupClassExclude, DBFilterDataTypeEnum.Integer, DBFilterCompareTypeEnum.IN);
                    flt.NegateExpression = true;
                    filterList.Add(flt);
                }
            }

            if (isPaging == 1 && rows > 0)
            {
                dbq.IsPaging = true;
                dbq.PageNo = page;
                dbq.RowCount = rows;
            }

            dbq.OrderBy = "tblGLGroup.GLGroupCode,tblGLGroup.GLGroupName";

            //cmd.CommandType = CommandType.Text;
            //cmd.CommandText = sb.ToString();

            //PG.Core.DBBase.DBQuery dbq = new PG.Core.DBBase.DBQuery();
            dbq.DBQueryMode = DBQueryModeEnum.SQLStatement;
            dbq.SQLStatement = sbStatment.ToString();
            dbq.DBFilterList = filterList;
            
            //dbq.DBCommand = cmd;

            List<dcGLGroup> listData = GLGroupBL.GetGLGroupList(dbq, null);
            int totRec = listData.Count;
            string comma = string.Empty;
            //bool isEmptyRecord = false;

            //if (listData.Count == 0 && includeEmpty == 1)
            //{
            //    dcGLGroup gGrp = new dcGLGroup();
            //    listData.Add(gGrp);
            //    isEmptyRecord = true;
            //}


            //System.Web.Script.Serialization.JavaScriptSerializer serl = new System.Web.Script.Serialization.JavaScriptSerializer();
            //string d = serl.Serialize(listData);

            var jsonList = from c in listData
                           select new
                           {
                               glgroupid = c.GLGroupID,
                               glgroupcode = c.GLGroupCode,
                               glgroupname = c.GLGroupName,
                               glgroupnameshort = c.GLGroupNameShort,
                               glgroupnameshortname = c.GLGroupNameShortName,
                               companyid = c.CompanyID,
                               //companyname = c.CompanyName,
                               glgroupidparent = c.GLGroupIDParent,
                               glgroupnameparent = c.GLGroupNameParentEffective,
                               glclassid = c.GLClassID,
                               glgroupclassid = c.GLGroupClassID,
                               glgroupclassname = c.GLGroupClassName,
                               iscash = c.IsCash ? 1 : 0,
                               isbank = c.IsBank ? 1 : 0,
                               isinventory = c.IsInventory ? 1 : 0,
                               isinstrument = c.IsInstrument ? 1 : 0,
                               glaccountcodenext = isNextGLAccountCode == 1 ? GLAccountBL.GetNextGLAccountCode(c.CompanyID,c.GLGroupID ): "",
                               enable = true
                           };

            JavaScriptSerializer jss = new JavaScriptSerializer();
            string jsonData = jss.Serialize(jsonList);

            //var myobj = jsSerializer.Deserialize<List<BigCommerceOrderProduct>>(jsonData);
            
                //JavaScriptSerializer.Deserialize

            StringBuilder sb = new StringBuilder();

            string pageNo = "1";
            string totalPage = "1";
            string records = listData.Count.ToString();
            //string actualrecords = totRec.ToString();

            string errorNo = "0";
            string errorString = string.Empty;

            if (dbq.IsPaging)
            {
                pageNo = dbq.PageNo.ToString();
                totalPage = dbq.TotalPage.ToString();
                //actualrecords = dbq.TotalRecord.ToString();
                //records = isEmptyRecord ? "1" : actualrecords;
            }

            if (totRec == 0)
            {
                errorNo = "1";
                errorString = "No Record Found!";
            }

            sb.Append("{");
            sb.Append("\"page\":" + pageNo);
            sb.Append(",\"totalpage\":" + totalPage);
            sb.Append(",\"records\":" + records);
            sb.Append(",\"errorno\":" + errorNo);
            sb.Append(",\"errorstring\":" + "\"" + errorString + "\"");
            sb.Append(",\"rows\":" + jsonData);
            sb.Append("}");

            //sb.Append("{\"rows\":");
            //sb.Append(jsonData);
            //sb.Append("}");

            //////
            //sb.Append("{\"acc\":[");

            //foreach (dcGLAccount glAcc in listData)
            //{
            //    sb.Append(comma);
            //    sb.Append("{");
            //    sb.Append("\"id\":\"" + glAcc.GLAccountID.ToString() + "\",");
            //    sb.Append("\"code\":\"" + glAcc.GLAccountCode + "\",");
            //    sb.Append("\"name\":\"" + glAcc.GLAccountName + "\",");

            //    sb.Append("\"lc\":\"" + "last" + "\"");
            //    sb.Append("}");
            //    comma = ",";
            //}

            //sb.Append("]");
            /////
            //sb.Append("}");

            context.Response.ContentType = "application/json";
            context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.Write(sb.ToString());
            context.ApplicationInstance.CompleteRequest();

            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
