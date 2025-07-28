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
    }
}
