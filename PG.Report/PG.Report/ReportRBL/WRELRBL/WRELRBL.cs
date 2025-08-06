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

                sb.Append(" Select CN_NUMBER FROM CN_CREATION_MST ");
                    sb.Append(" Where 1=1  ");
                    if (prmINV.CN_NUMBER != null)
                    {
                        sb.Append(" AND  CN_NUMBER =@CN_NUMBER ");
                        cmdInfo.DBParametersInfo.Add("@CN_NUMBER", prmINV.CN_NUMBER);
                        
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
                    stk.img = GenerateQrCode(stk.CN_NUMBER);
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
    }
}
