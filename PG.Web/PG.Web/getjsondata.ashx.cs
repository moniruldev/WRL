using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

using PG.Core.Web;
using System.Text;
using PG.DBClass.OrganiztionDC;
using PG.BLLibrary.OrganizationBL;
using System.Web.Script.Serialization;

namespace PG.Web
{
    /// <summary>
    /// Summary description for loginsilent
    /// </summary>
    public class getjsondata : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string taskName = WebUtility.GetQueryString("task", context).ToLower();

            //string jsonData = string.Empty; 
            string jsonData = "{}";
            switch (taskName)
            {
                //case "nextprjcode":
                //    jsonData = BLLibrary.ServiceBL.ServiceDataBL.GetJSon_NexProjectCode(context);
                //    break;
                //case "isprjcodeexists":
                //    jsonData = BLLibrary.ServiceBL.ServiceDataBL.GetJSon_IsProjectCodeExists(context);
                //    break;
                case "location":
                    jsonData = GetLocationData(context);
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.Write(jsonData);
            context.ApplicationInstance.CompleteRequest();


        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }


        public string GetLocationData(HttpContext context)
        {
            int companyID = WebUtility.GetQueryStringInteger("companyid", context);
            string locationCode = WebUtility.GetQueryString("locationcode", context);

            dcLocation loc = LocationBL.GetLocaionByCode(locationCode, companyID);

            List<dcLocation> listData = new List<dcLocation>();
            if (loc != null)
            {
                listData.Add(loc);
            }

            int totRec = listData.Count;

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

            //if (qryParams.result_IsPaging)
            //{
            //    pageNo = qryParams.result_PageNo.ToString();
            //    totalPage = qryParams.result_TotalPage.ToString();
            //    //actualrecords = dbq.TotalRecord.ToString();
            //    //records = isEmptyRecord ? "1" : actualrecords;
            //}

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