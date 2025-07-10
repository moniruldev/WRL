using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Data.Linq;
//using System.Linq.Dynamic;
using System.Web;
using PG.Core.Web;

namespace PG.BLLibrary.ServiceBL
{
    public class ServiceDataBL
    {
        public static string GetJSon_NexProjectCode(HttpContext context)
        {
            //int compID = WebUtility.GetQueryStringInteger("compid", context);

            
            //string strDate = WebUtility.GetQueryString("prjdate", context);

            //DateTime? prjDate = null;

            //if ( PG.Core.Utility.Conversion.DateParseExact(strDate, out prjDate))
            //{

            //}
            //else
            //{
            //    prjDate = null;
            //}

            //// string 

            //string prjCode = string.Empty;
            //string prjDateString = string.Empty;


            //if (compID > 0 && prjDate.HasValue)
            //{
            //    prjCode = ProjectBL.ProjectBL.GetNextProjectCode(compID, prjDate.Value, false);

            //}
            //if (prjDate.HasValue)
            //{
            //    prjDateString = prjDate.Value.ToString("dd-MMM-yyyy");
            //}


            StringBuilder sb = new StringBuilder();

            ////System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            ////string jsonData = jss.Serialize(jsonList);


            ////format 2
            //sb.Append("{");
            //sb.Append("\"project\":");
            //sb.Append("{");
            //sb.Append("\"prjcode\":\"" + prjCode + "\"");
            //sb.Append(",\"prjdate\":\"" + prjDateString + "\"");
            ////sb.Append(",\"totalrecord\":" + qTotalRecord.ToString());
            ////sb.Append(",\"pageno\":" + qPageNo.ToString());
            ////sb.Append(",\"totalpage\":" + qTotalPage.ToString());
            ////sb.Append(",\"name\":\"" + floor.FloorName + "\"");
            //sb.Append("}");

            //sb.Append("}");



            return sb.ToString();
        }


        public static string GetJSon_IsProjectCodeExists(HttpContext context)
        {
            //string prjCode = WebUtility.GetQueryString("prjcode", context).Trim();
            //int prjID = WebUtility.GetQueryStringInteger("prjid", context);
            //int editModeInt = WebUtility.GetQueryStringInteger("editmode", context);
            //ZCore.FormDataMode editMode = ZCore.Utility.Conversion.ConvertIntToEditMode(editModeInt);

            //// string 


            //bool isExists = false;
            //if (prjCode != string.Empty)
            //{
            //    if (editMode == ZCore.FormDataMode.Edit)
            //    {
            //        isExists = ProjectBL.ProjectBL.IsProjectCodeExists(prjCode, prjID);
            //    }
            //    else
            //    {
            //        isExists = ProjectBL.ProjectBL.IsProjectCodeExists(prjCode);
            //    }
            //}

            StringBuilder sb = new StringBuilder();

            //System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            //string jsonData = jss.Serialize(jsonList);


            //string strExists = isExists ? "1" : "0";

            ////format 2
            //sb.Append("{");
            //sb.Append("\"project\":");
            //sb.Append("{");
            //sb.Append("\"prjcode\":\"" + prjCode + "\"");
            //sb.Append(",\"isexists\":" + strExists);
            ////sb.Append(",\"pageno\":" + qPageNo.ToString());
            ////sb.Append(",\"totalpage\":" + qTotalPage.ToString());
            ////sb.Append(",\"name\":\"" + floor.FloorName + "\"");
            //sb.Append("}");

            //sb.Append("}");



            return sb.ToString();
        }


        public static string GetJSon_ParseDate(HttpContext context)
        {
            string strDate = WebUtility.GetQueryString("strdate", context);

            DateTime? dtDate = null;
            string dateString = string.Empty;
            if (PG.Core.Utility.Conversion.DateParseExact(strDate, out dtDate))
            {
                dateString = dtDate.Value.ToString("dd-MMM-yyyy");
            }

            // string 

            string prjCode = string.Empty;


            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            sb.Append("\"date\":");
            sb.Append("{");
            sb.Append("\"datestring\":\"" + dateString + "\"");

            //sb.Append(",\"pageno\":" + qPageNo.ToString());

            sb.Append("}");

            sb.Append("}");

            return sb.ToString();
        }

    }
}
