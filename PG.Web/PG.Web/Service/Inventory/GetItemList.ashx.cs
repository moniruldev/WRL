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
    /// Summary description for GetItemList
    /// </summary>
    public class GetItemList : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {

            //System.Collections.Specialized.StringDictionary list = new System.Collections.Specialized.StringDictionary();

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

            int batterytypeid = WebUtility.GetQueryStringInteger("batterytypeid", context);
            string chkisrepair = WebUtility.GetQueryString("chkisrepair", context);

            int codeCompType = WebUtility.GetQueryStringInteger("codecomptype", context);

            string dealerName = WebUtility.GetQueryString("name", context);
            int nameCompType = WebUtility.GetQueryStringInteger("namecomptype", context);

            int groupid = WebUtility.GetQueryStringInteger("groupid", context);
            int classid = WebUtility.GetQueryStringInteger("classid", context);
            int typeid = WebUtility.GetQueryStringInteger("typeid", context);
            int isigr = WebUtility.GetQueryStringInteger("isigr", context);


            int isDirectPurchase = WebUtility.GetQueryStringInteger("isDirectPurchase", context);

            int isIndentPurchase = WebUtility.GetQueryStringInteger("isIndentPurchase", context);


            //string companyCode = WebUtility.GetQueryString("companycode", context).ToUpper();
            //companyCode = companyCode == "0" ? string.Empty : companyCode;

            //string branchCode = WebUtility.GetQueryString("branchcode", context).ToUpper();
            //branchCode = branchCode == "0" ? string.Empty : branchCode;


            //string deptCode = WebUtility.GetQueryString("deptcode", context).ToUpper();
            //deptCode = deptCode == "0" ? string.Empty : deptCode;

            string isdeptstock = WebUtility.GetQueryString("isdeptstock", context);
            int deptid = WebUtility.GetQueryStringInteger("deptid", context);
            int storeid = WebUtility.GetQueryStringInteger("storeid", context);
            int specId = WebUtility.GetQueryStringInteger("specid", context);
            int itemid = WebUtility.GetQueryStringInteger("itemid", context);
            
            string isreturnstock = WebUtility.GetQueryString("isreturnstock", context);

            string Selected = WebUtility.GetQueryString("selectedId", context).ToUpper();

            string sortBy = WebUtility.GetQueryString("sidx", context); //colname
            string sortOrder = WebUtility.GetQueryString("sord", context);  //asc,desc
            string isFinished = WebUtility.GetQueryString("isFinished", context);
            string isMixture = WebUtility.GetQueryString("isMixture", context);
            int itemtypeid = WebUtility.GetQueryStringInteger("itemtypeid", context);
            int isdirectitc = WebUtility.GetQueryStringInteger("isdirectitc", context);
            


            if (isTerm == 1)
            {
                dealerCode = searchTerm;
            }

            if (isCodeName == 1)
            {
                dealerName = dealerCode;
            }

            System.Text.StringBuilder sbStatment = new System.Text.StringBuilder();

            List<dcINV_ITEM_MASTER> deptItemList = INV_ITEM_MASTERBL.GetDeptWiseItemListByType(deptid, itemtypeid, storeid, null);
            if (deptItemList.Count > 0)
            {
                if (isdeptstock == "Y")
                {
                    if (itemtypeid > 0)
                    {
                        sbStatment.Append(INV_ITEM_MASTERBL.GetItem_Mst_Prod_DeptStk_Bytype_SQLString(deptid, specId, itemtypeid));
                    }
                    else
                    {
                        sbStatment.Append(INV_ITEM_MASTERBL.GetItem_Master_Prod_DeptStock_SQLString(deptid, specId));
                    }

                }
                else
                {
                    if (itemtypeid > 0)
                    {
                        
                       
                            sbStatment.Append(INV_ITEM_MASTERBL.GetItem_Dept_Wise_ByType_SQLString(deptid, itemtypeid, storeid));
                        
                        
                    }

                    else
                    {
                        //sbStatment.Append(INV_ITEM_MASTERBL.GetNullItem_Master_SQLString());
                        sbStatment.Append(INV_ITEM_MASTERBL.GetItem_Dept_Wise_ByType_SQLString(deptid, 0, storeid));
                    }
                }

            }

            else
            {
                if (isdirectitc > 0)
                {
                    sbStatment.Append(INV_ITEM_MASTERBL.GetItem_Master_ByType_Store_SQLString(deptid, itemtypeid, storeid));
                }
                if (isdeptstock == "Y")
                {
                    if (itemtypeid > 0)
                    {
                        sbStatment.Append(INV_ITEM_MASTERBL.GetItem_Mst_With_DeptStk_Bytype_SQLString(deptid, specId, itemtypeid));
                    }
                    else
                    {
                        sbStatment.Append(INV_ITEM_MASTERBL.GetItem_Master_With_DeptStock_SQLString(deptid, specId));
                    }

                }
                //else
                //{
                //   sbStatment.Append(INV_ITEM_MASTERBL.GetItem_Master_SQLString());  //old Change on 30-Nov-2021 
                //}
                else
                {
                    if (isreturnstock == "Y")
                    {
                        sbStatment.Append(INV_ITEM_MASTERBL.GetItem_Master_With_Return_SQLString(itemtypeid,storeid));
                    }
                    else
                    {
                        if (isigr == 1)
                        {
                            sbStatment.Append(INV_ITEM_MASTERBL.GetItem_Master_SQLString());

                        }
                        else
                        {
                            if (itemtypeid > 0)
                            {
                                sbStatment.Append(INV_ITEM_MASTERBL.GetItem_Master_ByType_SQLString(itemtypeid, storeid));
                            }
                            else
                            {
                                //sbStatment.Append(INV_ITEM_MASTERBL.GetNullItem_Master_SQLString());
                                sbStatment.Append(INV_ITEM_MASTERBL.GetItem_Master_ByStore_SQLString(storeid));
                            }
                        }
                    }



                }
            }

            DBQuery dbq = new DBQuery();
            List<DBFilter> filterList = new List<DBFilter>();


            List<DBFilter> filterListCN = new List<DBFilter>();
            bool isCodeNameFilter = false;
            if (dealerCode != string.Empty)
            {
                DBFilterCompareTypeEnum compTypeAccCode = DBFilterCompareTypeEnum.Contains;
                if (codeCompType > 0)
                {
                    compTypeAccCode = DBFilterManager.GetCompareTypeFormInt(codeCompType);
                }
                filterListCN.Add(new DBFilter("UPPER(INV_ITEM_MASTER.ITEM_CODE)", dealerCode.ToUpper(), DBFilterDataTypeEnum.String, compTypeAccCode, DBFilterCombineTypeEnum.OR));
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
                    filterListCN.Add(new DBFilter("UPPER(INV_ITEM_MASTER.ITEM_NAME)", dealerName.ToUpper(), DBFilterDataTypeEnum.String, compTypeAccName, DBFilterCombineTypeEnum.OR));
                    //filterListCN.Add(new DBFilter("DEALER_MST.EX_DEALER_ID", dealerName, DBFilterDataTypeEnum.String, compTypeAccName, DBFilterCombineTypeEnum.OR));
                }
                else
                {
                    filterListCN.Add(new DBFilter("UPPER(INV_ITEM_MASTER.ITEM_NAME)", dealerName.ToUpper(), DBFilterDataTypeEnum.String, compTypeAccName));
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
            //if (isdeptstock == "Y")
            //{
            //    if (isFinished != "")
            //    {
            //        filterList.Add(new DBFilter("dept.IS_FINISHED", isFinished));
            //    }

            //    if (isMixture != "")
            //    {
            //        filterList.Add(new DBFilter("dept.IS_MIXTURE", isMixture));
            //    }
            //}

            if (groupid > 0)
            {
                filterList.Add(new DBFilter("INV_ITEM_MASTER.ITEM_GROUP_ID", groupid));
            }

            if (batterytypeid > 0 || chkisrepair != "N" && chkisrepair != "")
            {
                filterList.Add(new DBFilter("INV_ITEM_MASTER.IS_BATTERY", "Y"));
            }

            if (classid > 0)
            {
                filterList.Add(new DBFilter("INV_ITEM_MASTER.ITEM_CLASS_ID", classid));
            }

            if (itemid > 0)
            {
                filterList.Add(new DBFilter("INV_ITEM_MASTER.ITEM_ID", itemid));
            }


            if (typeid > 0)
            {
                filterList.Add(new DBFilter("INV_ITEM_MASTER.ITEM_TYPE_ID", typeid));
            }
            if (Selected != "")
            {
                filterList.Add(new DBFilter(" TRIM(Upper( INV_ITEM_MASTER.ITEM_NAME)) ", Selected.Trim(), DBFilterDataTypeEnum.String));
            }

            if (isPaging == 1 && rows > 0)
            {
                dbq.IsPaging = true;
                dbq.PageNo = page;
                dbq.RowCount = rows;
            }

            dbq.OrderBy = "ITEM_NAME";


            dbq.DBQueryMode = DBQueryModeEnum.SQLStatement;
            dbq.SQLStatement = sbStatment.ToString();
            dbq.DBFilterList = filterList;

            //dbq.DBCommand = cmd;
            ////dbq.IsPaging = false;

            List<dcINV_ITEM_MASTER> listData = INV_ITEM_MASTERBL.GetItemList(dbq, null);


            int totRec = listData.Count;
            string comma = string.Empty;

            var jsonList = from c in listData
                           select new
                           {
                               itemid = c.ITEM_ID,
                               itemcode = c.ITEM_CODE,
                               itemname = c.ITEM_NAME,
                               itemgroupid = c.ITEM_GROUP_ID,
                               itemgroupdesc = c.ITEM_GROUP_NAME,
                               uomid = c.UOM_ID,
                               uomname = c.UOM_CODE,
                               closing_qty = c.CLOSING_QTY,
                               unitprice=c.UNIT_PRICE,
                               //closing_qty = c.CLOSING_QTY - c.PENDING_REQ_QNTY,
                               safety_stock = c.SAFTY_STOCK_LEVEL,
                               reorder_leavel = c.RE_ORDER_LEVEL,
                               class_name = c.ITEM_CLASS_NAME,
                               item_type = c.ITEM_TYPE_NAME,
                               is_prime = c.IS_PRIME,
                               Opening_Price = c.OPENING_PRICE,
                               weightedAvgPrice = c.WEIGHTED_AVERAGE_PRICE,
                               is_outsale_cash = c.IS_OUTSALE_CASH,
                               isbatch = c.IS_BATCH,
                               enable = true
                           };

            JavaScriptSerializer jss = new JavaScriptSerializer();
            jss.MaxJsonLength = Int32.MaxValue;
            string jsonData = jss.Serialize(jsonList);



            StringBuilder sb = new StringBuilder();


            string pageNo = "1";
            string totalPage = "1";
            string records = listData.Count.ToString();

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