using System;
using System.Collections;
using System.Collections.Generic;
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

using PG.DBClass.OrganiztionDC;
using PG.BLLibrary.OrganizationBL;


namespace PG.Service.Organization
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class GetLocation : IHttpHandler
    {
        public class LocationParams
        {
            public int isTerm { get; set; }
            public int isCodeName { get; set; }
            public int isPaging { get; set; }
            public int rows { get; set; }
            public int page { get; set; }
            public int locationID { get; set; }
            public string searchTerm { get; set; }
            public string locationCode { get; set; }
            public int codeCompType { get; set; }
            public string locationName { get; set; }
            public int nameCompType { get; set; }
            public int parentID { get; set; }
            public int locationTypeID { get; set; }

            public int needCategoryID { get; set; }
            public int refTypeFilterType { get; set; }
            public int companyID { get; set; }

            public int includeHeirerchy { get; set; }

            public string sortBy { get; set; }

            public string sortOrder { get; set; }


            public bool result_IsPaging { get; set; }
            public int result_PageNo { get; set; }
            public int result_TotalPage { get; set; }
            public int result_TotalRecord { get; set; }
            public int result_RecordCount { get; set; }
        }

        public void ProcessRequest(HttpContext context)
        {
            LocationParams qryParams = new LocationParams();

            qryParams.isTerm = WebUtility.GetQueryStringInteger("isterm", context);
            qryParams.isCodeName = WebUtility.GetQueryStringInteger("iscodename", context);


            qryParams.isPaging = WebUtility.GetQueryStringInteger("ispaging", context);

            qryParams.rows = WebUtility.GetQueryStringInteger("rows", context);
            qryParams.page = WebUtility.GetQueryStringInteger("page", context);

            qryParams.locationID = WebUtility.GetQueryStringInteger("id", context);

            qryParams.searchTerm = WebUtility.GetQueryString("searchTerm", context);

            qryParams.locationCode = WebUtility.GetQueryString("code", context);
            qryParams.codeCompType = WebUtility.GetQueryStringInteger("codecomptype", context);

            qryParams.locationName = WebUtility.GetQueryString("name", context);
            qryParams.nameCompType = WebUtility.GetQueryStringInteger("namecomptype", context);


            qryParams.parentID = WebUtility.GetQueryStringInteger("parentid", -1, context);
            qryParams.locationTypeID = WebUtility.GetQueryStringInteger("typeid", context);

            qryParams.refTypeFilterType = WebUtility.GetQueryStringInteger("typefiltertype", context);

            qryParams.companyID = WebUtility.GetQueryStringInteger("companyid", context);

            qryParams.sortBy = WebUtility.GetQueryString("sidx", context); //colname
            qryParams.sortOrder = WebUtility.GetQueryString("sord", context);  //asc,desc

            qryParams.includeHeirerchy = WebUtility.GetQueryStringInteger("includehr", context);




            string strTask = WebUtility.GetQueryString("task", context);
            strTask = strTask.Trim() == string.Empty ? "location" : strTask;

            StringBuilder sb = new StringBuilder();

            //List<dcAccRefCategory> listData = new List<dcAccRefCategory>();

            switch (strTask.ToLower())
            {
                case "location":
                    sb.Append(GetLocationJSonString(qryParams));
                    break;
                default:
                    sb.Append(GetLocationJSonString(qryParams));
                    break;
            }

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

        public string GetLocationJSonString(LocationParams qryParams)
        {
            if (qryParams.isTerm == 1)
            {
                qryParams.locationCode = qryParams.searchTerm;
            }

            if (qryParams.isCodeName == 1)
            {
                qryParams.locationName = qryParams.locationCode;
            }


            //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            StringBuilder sbStatment = new StringBuilder();
            sbStatment.Append(LocationBL.GetLocation_SQLString());

            DBQuery dbq = new DBQuery();
            List<DBFilter> filterList = new List<DBFilter>();

            filterList.Add(new DBFilter("tblLocation.CompanyID", qryParams.companyID));

            if (qryParams.locationID > 0)
            {
                filterList.Add(new DBFilter("tblLocation.LocationID", qryParams.locationID));
            }

            List<DBFilter> filterListCN = new List<DBFilter>();
            bool isCodeNameFilter = false;
            if (qryParams.locationCode != string.Empty)
            {
                DBFilterCompareTypeEnum compTypeAccCode = DBFilterCompareTypeEnum.EqualTo;
                if (qryParams.codeCompType > 0)
                {
                    compTypeAccCode = DBFilterManager.GetCompareTypeFormInt(qryParams.codeCompType);
                }
                filterListCN.Add(new DBFilter("tblLocation.LocationCode", qryParams.locationCode, DBFilterDataTypeEnum.String, compTypeAccCode));
                isCodeNameFilter = true;
            }

            if (qryParams.locationName != string.Empty)
            {
                DBFilterCompareTypeEnum compTypeAccName = DBFilterCompareTypeEnum.StartsWith;
                if (qryParams.nameCompType > 0)
                {
                    compTypeAccName = DBFilterManager.GetCompareTypeFormInt(qryParams.nameCompType);
                }
                if (qryParams.isCodeName == 1)
                {
                    filterListCN.Add(new DBFilter("tblLocation.LocationName", qryParams.locationName, DBFilterDataTypeEnum.String, compTypeAccName, DBFilterCombineTypeEnum.OR));
                }
                else
                {
                    filterListCN.Add(new DBFilter("tblLocation.LocationName", qryParams.locationName, DBFilterDataTypeEnum.String, compTypeAccName));
                }
                isCodeNameFilter = true;
            }

            if (isCodeNameFilter)
            {
                if (qryParams.isCodeName == 1)
                {
                    filterList.Add(new DBFilter(filterListCN));
                }
                else
                {
                    filterList.AddRange(filterListCN);
                }
            }
            if (qryParams.parentID != -1)
            {
                filterList.Add(new DBFilter("tblLocation.LocationIDParent", qryParams.parentID));
            }

            if (qryParams.locationTypeID > 0)
            {
                filterList.Add(new DBFilter("tblLocation.LocationTypeID", qryParams.locationTypeID));
            }

            //if (accRefCategoryID > 0)
            //{
            //    filterList.Add(new DBFilter("tblAccRefCategory.AccRefCategoryID", accRefCategoryID));
            //}

            if (qryParams.isPaging == 1 && qryParams.rows > 0)
            {
                dbq.IsPaging = true;
                dbq.PageNo = qryParams.page;
                dbq.RowCount = qryParams.rows;
            }

            dbq.OrderBy = "tblLocation.LocationCode,tblLocation.LocationName";

            //cmd.CommandType = CommandType.Text;
            //cmd.CommandText = sb.ToString();

            //PG.Core.DBBase.DBQuery dbq = new PG.Core.DBBase.DBQuery();
            dbq.DBQueryMode = DBQueryModeEnum.SQLStatement;
            dbq.SQLStatement = sbStatment.ToString();
            dbq.DBFilterList = filterList;

            List<dcLocation> listData = new List<dcLocation>();

            listData = LocationBL.GetLocationList(dbq, null);

            qryParams.result_IsPaging = dbq.IsPaging;
            qryParams.result_PageNo = dbq.PageNo;
            qryParams.result_TotalPage = dbq.TotalPage;
            qryParams.result_TotalRecord = dbq.TotalRecord;
            qryParams.result_RecordCount = listData.Count;

            int totRec = listData.Count;
            string comma = string.Empty;



            var jsonList = from c in listData
                           select new
                           {
                               id = c.LocationID,
                               code = c.LocationCode,
                               name = c.LocationName,
                               typeid = c.LocationTypeID,
                               typename = c.LocationTypeName,
                               companyid = c.CompanyID,
                               //companyname = c.CompanyName,
                               parenid = c.LocationIDParent,
                               //parentname = c.AccRefCategoryNameParent,
                               enable = true
                           };


            JavaScriptSerializer jss = new JavaScriptSerializer();
            string jsonData = jss.Serialize(jsonList);


            StringBuilder sb = new StringBuilder();

            string pageNo = "1";
            string totalPage = "1";
            string records = listData.Count.ToString();

            string errorNo = "0";
            string errorString = string.Empty;

            if (qryParams.result_IsPaging)
            {
                pageNo = qryParams.result_PageNo.ToString();
                totalPage = qryParams.result_TotalPage.ToString();
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

            return sb.ToString();
        }

    }
}
