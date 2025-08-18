using PG.Core.DBBase;
using PG.DBClass.WRELDC;
using PG.Report.ReportClass.WRELRC;
using PG.Report.ReportEnums;
using PG.Report.ReportRBL.WRELRBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.Report.ReportGen.WRELRGN
{
    public class WRELRGN
    {
        public static AppReport CN_Barcode_Report(clsPrmWREL rptClass, ReportOptions rptOptions)
        {
            return CN_Barcode_Report(rptClass, rptOptions, null);
        }
        public static AppReport CN_Barcode_Report(clsPrmWREL rptClass, ReportOptions rptOptions, DBContext dc)
        {
            AppReport rpt = new AppReport();
            rpt.ReportID = ReportIDEnum.Department_Production_Report;
            rpt.ReportOptions = rptOptions;
           // SetParameter(rptClass, rpt, dc);
            rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.WRELDef.rptCNBarcodeDual.rdlc";
            List<rcWREL> rList = WRELRBL.Get_CNBarcodeInfo_Report(rptClass, dc);
            rpt.DataSources.Add(new AppReport.DataSource("dsCN", rList));
            return rpt;
        }


        public static AppReport CN_BarcodeSingle_Report(clsPrmWREL rptClass, ReportOptions rptOptions)
        {
            return CN_BarcodeSingle_Report(rptClass, rptOptions, null);
        }
        public static AppReport CN_BarcodeSingle_Report(clsPrmWREL rptClass, ReportOptions rptOptions, DBContext dc)
        {
            AppReport rpt = new AppReport();
            rpt.ReportID = ReportIDEnum.Department_Production_Report;
            rpt.ReportOptions = rptOptions;
            // SetParameter(rptClass, rpt, dc);
            rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.WRELDef.rptCNBarcode.rdlc";
            List<rcWREL> rList = WRELRBL.Get_CNBarcodeInfo_Report(rptClass, dc);
            rpt.DataSources.Add(new AppReport.DataSource("dsCN", rList));
            return rpt;
        }
        public static AppReport CargoManifest_Report(clsPrmWREL rptClass, ReportOptions rptOptions)
        {
            return CargoManifest_Report(rptClass, rptOptions, null);
        }
        public static AppReport CargoManifest_Report(clsPrmWREL rptClass, ReportOptions rptOptions, DBContext dc)
        {
            AppReport rpt = new AppReport();
            rpt.ReportID = ReportIDEnum.ItemReport;
            rpt.ReportOptions = rptOptions;
            rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.WRELDef.rptCargoManifest.rdlc";
            List<rcWREL> rList = WRELRBL.Get_CargoManifest_Report(rptClass, dc);
            rpt.DataSources.Add(new AppReport.DataSource("dsCargo", rList));
            return rpt;
        }

        public static AppReport CNList_Report(clsPrmWREL rptClass, ReportOptions rptOptions)
        {
            return CNList_Report(rptClass, rptOptions, null);
        }
        public static AppReport CNList_Report(clsPrmWREL rptClass, ReportOptions rptOptions, DBContext dc)
        {
            AppReport rpt = new AppReport();
            rpt.ReportID = ReportIDEnum.ItemReport;
            rpt.ReportOptions = rptOptions;
            rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.WRELDef.rptCNDashboardList.rdlc";
            List<rcWREL> rList = WRELRBL.Get_CNList_Report(rptClass, dc);
            rpt.DataSources.Add(new AppReport.DataSource("dsCargo", rList));
            return rpt;
        }

        public static AppReport CN_Reference_Report(clsPrmWREL rptClass, ReportOptions rptOptions)
        {
            return CN_Reference_Report(rptClass, rptOptions, null);
        }
        public static AppReport CN_Reference_Report(clsPrmWREL rptClass, ReportOptions rptOptions, DBContext dc)
        {
            AppReport rpt = new AppReport();
            rpt.ReportID = ReportIDEnum.Department_Production_Report;
            rpt.ReportOptions = rptOptions;
            // SetParameter(rptClass, rpt, dc);
            rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.WRELDef.rptCNReference.rdlc";
            List<rcWREL> rList = WRELRBL.Get_CNReferenceInfo_Report(rptClass, dc);
            rpt.DataSources.Add(new AppReport.DataSource("dsCN", rList));
            return rpt;
        }

    }
}
