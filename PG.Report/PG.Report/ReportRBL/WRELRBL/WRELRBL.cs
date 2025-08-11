using PG.BLLibrary.WRElBL;
using PG.Core.DBBase;
using PG.DBClass.WRELDC;
using PG.Report.ReportClass.WRELRC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.Report.ReportRBL.WRELRBL
{
    public class WRELRBL
    {
        public static List<rcWREL> Get_CNBarcodeInfo_Report(clsPrmWREL prmINV)
        {
            return Get_CNBarcodeInfo_Report(prmINV, null);
        }

        public static List<rcWREL> Get_CNBarcodeInfo_Report(clsPrmWREL prmINV, DBContext dc)
        {
            List<rcWREL> cRptList = new List<rcWREL>();
            bool isDCInit = false;
            //try
            {

                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();
                cmdInfo.DBParametersInfo.Clear();


                sb.Length = 0;

                sb.Append(" Select cnm.CN_NUMBER,clm.CLIENT_NAME,clm.CLIENT_ADDRESS,clm.MOBILE_NO CLIENT_MOBILE,im.ITEM_NAME,rm.ROUTE_NAME,cnm.DESTINATION DIST_NAME,tm.TOWN_NAME ");
                sb.Append(" ,cnm.CREATE_DATE,cnm.BOOKING_DATE,cnm.SERVICE_AMOUNT SERVICE_CHARGE_AMT_DEFAULT,0 WEIGHT,1 QUANTITY ,cnm.CONSIGNEE_NAME,cnm.CONSIGNEE_ADDRESS,cnm.CONSIGNEE_MOBILE_NO,dm.DIST_CODE ");
                sb.Append(" ,dpm.DEPT_NAME ");
                sb.Append(" FROM CN_CREATION_MST cnm ");
                sb.Append(" INNER JOIN CLIENT_MST clm ON cnm.CLIENT_ID=clm.CLIENT_ID ");
                sb.Append(" INNER JOIN ITEM_MST im ON cnm.ITEM_ID=im.ITEM_ID ");
                sb.Append(" LEFT JOIN DEPARTMENT_MST dpm ON cnm.CLIENT_DEPT_ID=dpm.DEPT_ID ");
                sb.Append(" LEFT JOIN ROUTE_MST rm ON cnm.ROUTE_ID=rm.ROUTE_ID ");
                sb.Append(" LEFT JOIN DISTRICT_MST dm ON cnm.DESTINATION_DIST_ID=dm.DIST_ID ");
                sb.Append(" LEFT JOIN THANA_TOWN_MST tm ON cnm.DESTINATION_TOWN_ID=tm.TOWN_ID ");

                sb.Append(" Where 1=1  ");
                if (prmINV.CN_ID > 0)
                {
                    sb.Append(" AND  cnm.CN_ID =@CN_ID ");
                    cmdInfo.DBParametersInfo.Add("@CN_ID", prmINV.CN_ID);

                }






                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                DataTable dtData = DBQuery.ExecuteDBQuery(dbq, dc);

                foreach (DataRow dRow in dtData.Rows)
                {
                    rcWREL stk = new rcWREL();

                    stk.CN_NUMBER = dRow["CN_NUMBER"].ToString();
                    stk.CLIENT_NAME = dRow["CLIENT_NAME"].ToString();
                    stk.CLIENT_ADDRESS = dRow["CLIENT_ADDRESS"].ToString();

                    stk.CLIENT_MOBILE = dRow["CLIENT_MOBILE"].ToString();
                    stk.ITEM_NAME = dRow["ITEM_NAME"].ToString();
                    stk.ROUTE_NAME = dRow["ROUTE_NAME"].ToString();
                    stk.DIST_NAME = dRow["DIST_NAME"].ToString();
                    stk.TOWN_NAME = dRow["TOWN_NAME"].ToString();
                    stk.CREATE_DATE = Convert.ToDateTime(dRow["CREATE_DATE"].ToString());

                    stk.SERVICE_CHARGE_AMT_DEFAULT = Convert.ToDecimal(dRow["SERVICE_CHARGE_AMT_DEFAULT"].ToString());
                    stk.WEIGHT = Convert.ToDecimal(dRow["WEIGHT"].ToString());
                    stk.QUANTITY = Convert.ToInt32(dRow["QUANTITY"].ToString());
                    stk.CONSIGNEE_NAME = dRow["CONSIGNEE_NAME"].ToString();
                    stk.CONSIGNEE_ADDRESS = dRow["CONSIGNEE_ADDRESS"].ToString();
                    stk.CONSIGNEE_MOBILE_NO = dRow["CONSIGNEE_MOBILE_NO"].ToString();


                    stk.img = GenerateQrCode(stk.CN_NUMBER + " Booking Date : " + Convert.ToDateTime(dRow["CREATE_DATE"]).ToString("dd-MMM-yyyy") + " Customer : " + stk.CLIENT_NAME + " Web : www.world-runner.com");
                    cRptList.Add(stk);

                }

            }
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }

            return cRptList;

        }

        private static byte[] GenerateQrCode(string qrmsg)
        {
            QRCoder.QRCodeGenerator qRCodeGenerator = new QRCoder.QRCodeGenerator();
            QRCoder.QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode(qrmsg, QRCoder.QRCodeGenerator.ECCLevel.Q);
            QRCoder.QRCode qRCode = new QRCoder.QRCode(qRCodeData);

            using (Bitmap bmp = qRCode.GetGraphic(5))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);


                    byte[] byteImage = ms.ToArray();
                    //MemoryStream ms1 = new MemoryStream(byteImage);
                    //img = Image.FromStream(ms1);  
                    return byteImage;
                }
            }
        }
        //private static byte[] GenerateQrCode(string qrmsg)
        //{
        //    QRCoder.QRCodeGenerator qRCodeGenerator = new QRCoder.QRCodeGenerator();
        //    QRCoder.QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode(qrmsg, QRCoder.QRCodeGenerator.ECCLevel.Q);
        //    QRCoder.QRCode qRCode = new QRCoder.QRCode(qRCodeData);
            
        //    using (Bitmap bmp = qRCode.GetGraphic(5))
        //    {
        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);


        //            byte[] byteImage = ms.ToArray();
        //            //MemoryStream ms1 = new MemoryStream(byteImage);
        //            //img = Image.FromStream(ms1);  
        //            return byteImage;
        //        }
        //    }
        //}
        //Others

        public static List<rcWREL> Get_CargoManifest_Report(clsPrmWREL prm, DBContext dc)
        {
            List<rcWREL> cRptList = new List<rcWREL>();
            bool isDCInit = false;
            //try
            {

                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                StringBuilder sb = new StringBuilder();
                cmdInfo.DBParametersInfo.Clear();


                sb.Length = 0;
                sb.Append(@"   SELECT MST.cargo_id,mst.cargo_number,MST.cargo_date,dist.dist_name,0 weight_in_kg 
                 ,cn.cn_number,cn.consignee_name,cn.consignee_address,cn.consignee_mobile_no,'' remarks 
                 ,C.CLIENT_NAME,IM.ITEM_NAME,CN.SERVICE_AMOUNT,dept.dept_name DEPARTMENT
                 FROM cargo_creation_mst MST 
                 INNER JOIN cargo_creation_detail DTL ON mst.cargo_id=dtl.cargo_id 
                 INNER JOIN cn_creation_mst CN ON dtl.cn_id=cn.cn_id 
                 LEFT JOIN district_mst DIST ON mst.cargo_destination_dist_id=dist.dist_id 
                 left join client_mst c on cn.client_id=c.client_id
                 LEFT JOIN item_mst IM ON cn.item_id=im.item_id
                 left join agreement_detaill agdtl on cn.agr_detail_id=agdtl.agr_detail_id
                 left join agreement_mst agmst on agdtl.agr_id=agdtl.agr_id 
                 left join department_mst dept on agmst.dept_id=dept.dept_id
                 Where 1=1

                 ");

                if (prm.TRANS_NO != null)
                {
                    sb.Append(" AND  mst.cargo_number =@CARGO_NO ");
                    cmdInfo.DBParametersInfo.Add("@CARGO_NO", prm.TRANS_NO);

                }

                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                cRptList = DBQuery.ExecuteDBQuery<rcWREL>(dbq, dc);

                //foreach (DataRow dRow in dtData.Rows)
                //{
                //    rcWREL stk = new rcWREL();

                //    stk.CN_NUMBER = dRow["CN_NUMBER"].ToString();
                //    stk.img = GenerateQrCode(stk.CN_NUMBER);
                //    cRptList.Add(stk);

                //}

            }
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }

            return cRptList;

        }

        public static List<rcWREL> Get_CNList_Report(clsPrmWREL prm, DBContext dc)
        {
            List<rcWREL> cRptList = new List<rcWREL>();
            bool isDCInit = false;
            //try
            {

                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                //StringBuilder sb = new StringBuilder();
                cmdInfo.DBParametersInfo.Clear();

                StringBuilder sb = new StringBuilder(CN_CREATION_MSTBL.GetCNInfoListSQLString());

                sb.Append(" AND mst.CLIENT_ID= @clientId ");
                cmdInfo.DBParametersInfo.Add("@clientId", prm.CLIENT_ID);

                if (!string.IsNullOrWhiteSpace(prm.ITEM_NAME))
                {
                    sb.Append(" AND UPPER(im.item_name) LIKE @itemName ");
                    cmdInfo.DBParametersInfo.Add("@itemName", "%" + prm.ITEM_NAME.ToUpper() + "%");
                }

                if (!string.IsNullOrWhiteSpace(prm.CONSIGNEE_NAME))
                {
                    sb.Append(" AND UPPER(mst.CONSIGNEE_NAME) LIKE @conName ");
                    cmdInfo.DBParametersInfo.Add("@conName", "%" + prm.CONSIGNEE_NAME.ToUpper() + "%");
                }

                if (!string.IsNullOrWhiteSpace(prm.CN_NUMBER))
                {
                    sb.Append(" AND UPPER(mst.CN_NUMBER) LIKE @cnNumber ");
                    cmdInfo.DBParametersInfo.Add("@cnNumber", "%" + prm.CN_NUMBER.ToUpper() + "%");
                }

                if (prm.CONSIGNEE_MOBILE_NO != "")
                {
                    sb.Append(" AND mst.CONSIGNEE_MOBILE_NO= @mobileNo ");
                    cmdInfo.DBParametersInfo.Add("@mobileNo", prm.CONSIGNEE_MOBILE_NO);
                }


                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;
                cmdInfo.CommandTimeout = 600;
                cmdInfo.CommandText = sb.ToString();
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                cRptList = DBQuery.ExecuteDBQuery<rcWREL>(dbq, dc);

                //foreach (DataRow dRow in dtData.Rows)
                //{
                //    rcWREL stk = new rcWREL();

                //    stk.CN_NUMBER = dRow["CN_NUMBER"].ToString();
                //    stk.img = GenerateQrCode(stk.CN_NUMBER);
                //    cRptList.Add(stk);

                //}

            }
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }

            return cRptList;

        }
    }
}
