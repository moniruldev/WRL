using PG.BLLibrary.InventoryBL;
using PG.Core.DBBase;
using PG.Core.DBFilters;
using PG.Core.Web;
using PG.DBClass.InventoryDC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace PG.Web.Service.Inventory
{
    /// <summary>
    /// Summary description for GetItemGroupList
    /// </summary>
    public class GetItemGroupList : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {

            //System.Collections.Specialized.StringDictionary list = new System.Collections.Specialized.StringDictionary();

            int hasItem = WebUtility.GetQueryStringInteger("hasitem", context);


            int isTerm = WebUtility.GetQueryStringInteger("isterm", context);
            int isCodeName = 1;// WebUtility.GetQueryStringInteger("iscodename", context);

            int isPaging = WebUtility.GetQueryStringInteger("ispaging", context);

            int rows = WebUtility.GetQueryStringInteger("rows", context);
            int page = WebUtility.GetQueryStringInteger("page", context);

            string dealerID = WebUtility.GetQueryString("id", context);

            int locationID = WebUtility.GetQueryStringInteger("locationid", context);
            string seid = WebUtility.GetQueryString("seid", context);
            string searchTerm = WebUtility.GetQueryString("searchTerm", context);
            string dealerCode = WebUtility.GetQueryString("code", context);

            int codeCompType = WebUtility.GetQueryStringInteger("codecomptype", context);

            string dealerName = WebUtility.GetQueryString("name", context);
            int nameCompType = WebUtility.GetQueryStringInteger("namecomptype", context);

            //string companyCode = WebUtility.GetQueryString("companycode", context).ToUpper();
            //companyCode = companyCode == "0" ? string.Empty : companyCode;

            //string branchCode = WebUtility.GetQueryString("branchcode", context).ToUpper();
            //branchCode = branchCode == "0" ? string.Empty : branchCode;


            //string deptCode = WebUtility.GetQueryString("deptcode", context).ToUpper();
            //deptCode = deptCode == "0" ? string.Empty : deptCode;




            string sortBy = WebUtility.GetQueryString("sidx", context); //colname
            string sortOrder = WebUtility.GetQueryString("sord", context);  //asc,desc


            if (isTerm == 1)
            {
                dealerCode = searchTerm;
            }

            if (isCodeName == 1)
            {
                dealerName = dealerCode;
            }


            //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            System.Text.StringBuilder sbStatment = new System.Text.StringBuilder();
            sbStatment.Append(INV_ITEM_GROUPBL.ItemGroup_SqlString());
            //if (hasItem == 1)
            //{
            //    sbStatment.Append(" and INV_ITEM_GROUP.ITEM_GROUP_ID IN(SELECT DISTINCT INV_ITEM_MASTER.ITEM_GROUP_ID from INV_ITEM_MASTER) ");
            //}

            DBQuery dbq = new DBQuery();
            List<DBFilter> filterList = new List<DBFilter>();

            //filterList.Add(new DBFilter("EMP_Info.CompanyCode", companyCode));


            //if (accID > 0)
            //{
            //    filterList.Add(new DBFilter("tblGLAccount.GLAccountID", accID));

            //    //sb.Append(" AND tblGLAccount.GLAccountID=@accID ");
            //    //cmd.Parameters.AddWithValue("@accID", accID);
            //}


            List<DBFilter> filterListCN = new List<DBFilter>();
            bool isCodeNameFilter = false;
            if (dealerCode != string.Empty)
            {
                DBFilterCompareTypeEnum compTypeAccCode = DBFilterCompareTypeEnum.Contains;
                if (codeCompType > 0)
                {
                    compTypeAccCode = DBFilterManager.GetCompareTypeFormInt(codeCompType);
                }
                filterListCN.Add(new DBFilter("UPPER(INV_ITEM_GROUP.ITEM_GROUP_CODE)", dealerCode.ToUpper(), DBFilterDataTypeEnum.String, compTypeAccCode, DBFilterCombineTypeEnum.OR));
                //filterListCN.Add(new DBFilter("DEALER_MST.EX_DEALER_ID", dealerName, DBFilterDataTypeEnum.String, compTypeAccCode, DBFilterCombineTypeEnum.OR));

                isCodeNameFilter = true;
            }

            if (dealerName != string.Empty)
            {
                DBFilterCompareTypeEnum compTypeAccName = DBFilterCompareTypeEnum.Contains;
                if (nameCompType > 0)
                {
                    compTypeAccName = DBFilterManager.GetCompareTypeFormInt(nameCompType);
                }
                if (isCodeName == 1)
                {
                    filterListCN.Add(new DBFilter("UPPER(INV_ITEM_GROUP.ITEM_GROUP_NAME)", dealerName.ToUpper(), DBFilterDataTypeEnum.String, compTypeAccName, DBFilterCombineTypeEnum.OR));
                    //filterListCN.Add(new DBFilter("DEALER_MST.EX_DEALER_ID", dealerName, DBFilterDataTypeEnum.String, compTypeAccName, DBFilterCombineTypeEnum.OR));
                }
                else
                {
                    filterListCN.Add(new DBFilter("UPPER(INV_ITEM_GROUP.ITEM_GROUP_NAME)", dealerName.ToUpper(), DBFilterDataTypeEnum.String, compTypeAccName));
                    //filterListCN.Add(new DBFilter("DEALER_MST.EX_DEALER_ID", dealerName, DBFilterDataTypeEnum.String, compTypeAccName));
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


            //if (locationID > 0)
            //{
            //    filterList.Add(new DBFilter("TBLLOCATION.LOCATIONID", locationID));
            //    // DBFilterCompareTypeEnum compTypeAccCode = DBFilterCompareTypeEnum.EqualTo;
            //    //if (codeCompType > 0)
            //    //{
            //    //    compTypeAccCode = DBFilterManager.GetCompareTypeFormInt(codeCompType);
            //    //}

            //    // filterListCN.Add(new DBFilter("TBLLOCATION.LOCATIONID", locationID, DBFilterDataTypeEnum.String, compTypeAccCode));
            //    // isCodeNameFilter = true;
            //}
            //if (seid != string.Empty)
            //{
            //    filterList.Add(new DBFilter("SE_DEALER.SE_ID", seid));
            //}

            //if (branchCode != string.Empty)
            //{
            //    filterList.Add(new DBFilter("EMP_INFO.BRANCH_CODE", branchCode));
            //}



            if (isPaging == 1 && rows > 0)
            {
                dbq.IsPaging = true;
                dbq.PageNo = page;
                dbq.RowCount = rows;
            }

            dbq.OrderBy = "INV_ITEM_GROUP.ITEM_GROUP_DESC";

            //cmd.CommandType = CommandType.Text;
            //cmd.CommandText = sb.ToString();

            //PG.Core.DBBase.DBQuery dbq = new PG.Core.DBBase.DBQuery();
            dbq.DBQueryMode = DBQueryModeEnum.SQLStatement;
            dbq.SQLStatement = sbStatment.ToString();
            dbq.DBFilterList = filterList;

            //dbq.DBCommand = cmd;

            List<dcINV_ITEM_GROUP> listData = INV_ITEM_GROUPBL.GetItemGroupList(dbq,null);
            int totRec = listData.Count;
            string comma = string.Empty;



            //System.Web.Script.Serialization.JavaScriptSerializer serl = new System.Web.Script.Serialization.JavaScriptSerializer();
            //string d = serl.Serialize(listData);

            var jsonList = from c in listData
                           select new
                           {
                               itemgroupid = c.ITEM_GROUP_ID,
                               itemgroupcode = c.ITEM_GROUP_CODE,
                               itemgroupname = c.ITEM_GROUP_NAME,
                               itemgroupdesc = c.ITEM_GROUP_NAME,
                               itemgroupnameparent=c.ITEM_GROUP_NAME_PARENT, 
                               itemcode=c.ITEM_CODE,
                               enable = true
                           };





            JavaScriptSerializer jss = new JavaScriptSerializer();
            jss.MaxJsonLength = Int32.MaxValue;
            string jsonData = jss.Serialize(jsonList);

            //var myobj = jsSerializer.Deserialize<List<BigCommerceOrderProduct>>(jsonData);

            //JavaScriptSerializer.Deserialize

            StringBuilder sb = new StringBuilder();


            string pageNo = "1";
            string totalPage = "1";
            //string records = listData.Count.ToString();
            string records = dbq.TotalRecord.ToString();

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

            //sb.Append("{");
            //sb.Append("\"page\":" + pageNo);
            //sb.Append(",\"total\":" + total);
            //sb.Append(",\"records\":" + records);
            //sb.Append(",\"actualrecords\":" + actualrecords);
            //sb.Append(",\"rows\":" + jsonData);
            //sb.Append("}");

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