using PG.BLLibrary.WRElBL;
using PG.Core.DBBase;
using PG.Core.DBFilters;
using PG.Core.Web;
using PG.DBClass.WRELDC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace PG.Web.Service.WREL
{
    /// <summary>
    /// Summary description for GetItemListByAgreement
    /// </summary>
    public class GetItemListByAgreement : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int isTerm = WebUtility.GetQueryStringInteger("isterm", context);
            int isCodeName = 1;// WebUtility.GetQueryStringInteger("iscodename", context);

            int isPaging = WebUtility.GetQueryStringInteger("ispaging", context);

            int rows = WebUtility.GetQueryStringInteger("rows", context);
            int page = WebUtility.GetQueryStringInteger("page", context);

            string dealerID = WebUtility.GetQueryString("id", context);
            string searchTerm = WebUtility.GetQueryString("searchTerm", context);
            string dealerCode = WebUtility.GetQueryString("code", context);
            int codeCompType = WebUtility.GetQueryStringInteger("codecomptype", context);

            string dealerName = WebUtility.GetQueryString("name", context);
            int nameCompType = WebUtility.GetQueryStringInteger("namecomptype", context);

            string Selected = WebUtility.GetQueryString("selectedId", context).ToUpper();
            int typeid = WebUtility.GetQueryStringInteger("typeid", context);
            int clientid = WebUtility.GetQueryStringInteger("clientid", context);
            int distancetypeid = WebUtility.GetQueryStringInteger("distancetypeid", context);
            
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


            System.Text.StringBuilder sbStatment = new System.Text.StringBuilder();

            sbStatment.Append(AGREEMENT_DETAILLBL.GetItemByAgreementSQLString());

            DBQuery dbq = new DBQuery();
            List<DBFilter> filterList = new List<DBFilter>();



            List<DBFilter> filterListCN = new List<DBFilter>();
            bool isCodeNameFilter = false;

            if (dealerName != string.Empty)
            {
                DBFilterCompareTypeEnum compTypeAccName = DBFilterCompareTypeEnum.Contains;
                if (nameCompType > 0)
                {
                    compTypeAccName = DBFilterManager.GetCompareTypeFormInt(nameCompType);
                }
                if (isCodeName == 1)
                {
                    filterListCN.Add(new DBFilter("UPPER(IM.ITEM_NAME)", dealerName.ToUpper(), DBFilterDataTypeEnum.String, compTypeAccName, DBFilterCombineTypeEnum.OR));
                    //filterListCN.Add(new DBFilter("HMCOUNTRY_MST.COUNTRY_NAME", dealerName, DBFilterDataTypeEnum.String, compTypeAccName, DBFilterCombineTypeEnum.OR));
                }
                else
                {
                    filterListCN.Add(new DBFilter("UPPER(IM.ITEM_NAME)", dealerName.ToUpper(), DBFilterDataTypeEnum.String, compTypeAccName));
                    //filterListCN.Add(new DBFilter("HMCOUNTRY_MST.COUNTRY_NAME", dealerName, DBFilterDataTypeEnum.String, compTypeAccName));
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


            //if (typeid > 0)
            //{
            //    filterList.Add(new DBFilter("INV_ITEM_MASTER.ITEM_TYPE_ID", typeid));
            //}

            filterList.Add(new DBFilter("amst.client_id", clientid));
            filterList.Add(new DBFilter("adtl.distance_type_id", distancetypeid));




            if (Selected != "")
            {
                filterList.Add(new DBFilter(" TRIM(Upper( IM.ITEM_NAME)) ", Selected.Trim(), DBFilterDataTypeEnum.String));
            }


            if (isPaging == 1 && rows > 0)
            {
                dbq.IsPaging = true;
                dbq.PageNo = page;
                dbq.RowCount = rows;
            }

            dbq.OrderBy = "IM.ITEM_NAME";

            dbq.DBQueryMode = DBQueryModeEnum.SQLStatement;
            dbq.SQLStatement = sbStatment.ToString();
            dbq.DBFilterList = filterList;


            List<dcAGREEMENT_DETAILL> listData = AGREEMENT_DETAILLBL.GetAGREEMENT_DETAILLList(dbq, null);
            int totRec = listData.Count;
            string comma = string.Empty;


            var jsonList = from c in listData
                           select new
                           {
                               itemid = c.ITEM_ID,
                               itemname = c.ITEM_NAME,
                               serviceamt=c.SERVICE_AMOUNT,
                               sladays=c.SLA_DAYS,
                               agreementdate=Convert.ToDateTime(c.AGREEMENT_DATE).ToString("dd-MMM-yyyy"),
                               agrdtlid=c.AGR_DETAIL_ID,
                               agrid=c.AGR_ID,
                               enable = true
                           };





            JavaScriptSerializer jss = new JavaScriptSerializer();
            jss.MaxJsonLength = Int32.MaxValue;
            string jsonData = jss.Serialize(jsonList);


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


            context.Response.ContentType = "application/json";
            context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.Write(sb.ToString());
            context.ApplicationInstance.CompleteRequest();

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