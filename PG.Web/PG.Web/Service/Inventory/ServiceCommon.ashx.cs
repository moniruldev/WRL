using PG.BLLibrary.InventoryBL;
using PG.Core.Web;
using PG.DBClass.InventoryDC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace PG.Web.Service.Inventory
{
   
    public class ServiceCommon : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string taskName = WebUtility.GetQueryString("task", context).ToLower();

            //string jsonData = string.Empty; 
            string jsonData = "{}";
            switch (taskName)
            {
                case "itemgroupbyitemid":
                    jsonData = ItemByItemGroupId(context);
                    break;

                case "location":
                    //  jsonData = GetLocationData(context);
                    break;



            }
            context.Response.ContentType = "application/json";
            context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.Write(jsonData);
            context.ApplicationInstance.CompleteRequest();


        }
        public string ItemByItemGroupId(HttpContext context)
        {
            string groupId = WebUtility.GetQueryStringInteger("groupId", context).ToString();

            List<dcINV_ITEM_DTL> listData = INV_ITEM_DTLBL.Inv_ItemDtl_List(null, null, null, groupId);

            var jsonList = from c in listData
                           select new
                           {
                               ITEM_ID = c.ITEM_ID,
                               ITEM_DESC = c.ITEM_DESC,
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
            errorNo = "1";
            errorString = "No Record Found!";

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
        public string ItemByItemId(HttpContext context)
        {
            string itemId = WebUtility.GetQueryStringInteger("itemId", context).ToString();

            dcINV_ITEM_MASTER jsonList = INV_ITEM_MASTERBL.GetItemByID(Convert.ToInt32(itemId));         

            JavaScriptSerializer jss = new JavaScriptSerializer();
            string jsonData = jss.Serialize(jsonList);

            StringBuilder sb = new StringBuilder();

            string pageNo = "1";
            string totalPage = "1";
         

            string errorNo = "0";
            string errorString = string.Empty;
            errorNo = "1";
            errorString = "No Record Found!";

            sb.Append("{");
            sb.Append("\"page\":" + pageNo);
            sb.Append(",\"totalpage\":" + totalPage);           
            sb.Append(",\"errorno\":" + errorNo);
            sb.Append(",\"errorstring\":" + "\"" + errorString + "\"");
            sb.Append(",\"rows\":" + jsonData);
            sb.Append("}");
            return sb.ToString();
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
