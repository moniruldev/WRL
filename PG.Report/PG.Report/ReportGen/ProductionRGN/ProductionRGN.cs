using PG.Core.DBBase;
using PG.DBClass.InventoryDC;
using PG.DBClass.ProductionDC;
//using PG.Report.ReportClass.InventoryRC;
//using PG.Report.ReportClass.InventoryRC;
using PG.Report.ReportClass.ProductionRC;
using PG.Report.ReportEnums;
using PG.Report.ReportRBL.InventoryRBL;
using PG.Report.ReportRBL.ProductionRBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG.Report.ReportGen.ProductionRGN
{
    public class ProductionRGN
    {

        public static AppReport Department_Production_Report(clsPrmInventory rptClass, ReportOptions rptOptions)
        {
            return Department_Production_Report(rptClass, rptOptions, null);
        }
        public static AppReport Department_Production_Report(clsPrmInventory rptClass, ReportOptions rptOptions, DBContext dc)
        {
            AppReport rpt = new AppReport();
            rpt.ReportID = ReportIDEnum.Department_Production_Report;
            rpt.ReportOptions = rptOptions;
            SetParameter(rptClass, rpt, dc);
            rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.ProductionDef.rptDepartmentProduction.rdlc";
            List<rcProduction> rList = ProductionRBL.Department_Production_Report(rptClass, dc);
            rpt.DataSources.Add(new AppReport.DataSource("dsDeptProduction", rList));
            return rpt;
        }



        //Department

        public static AppReport Department_Used_Material_Report(clsPrmInventory rptClass, ReportOptions rptOptions)
        {
            return Department_Used_Material_Report(rptClass, rptOptions, null);
        }
        public static AppReport Department_Used_Material_Report(clsPrmInventory rptClass, ReportOptions rptOptions, DBContext dc)
        {
            AppReport rpt = new AppReport();
            rpt.ReportID = ReportIDEnum.Department_Production_Report;
            rpt.ReportOptions = rptOptions;
            SetParameter(rptClass, rpt, dc);

            if (rptClass.report_category == "Date-Wise")
            {
                //if (rptClass.From_Dept_Id == 135)
                    rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.ProductionDef.rptDepartmentUsedMaterialReport.rdlc";

                  
                //else
                //    rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.ProductionDef.rptItemWiseDeptProductionReport.rdlc";
            }
            else
            {
            //    if (rptClass.From_Dept_Id == 135)
            //        rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.ProductionDef.rptItemWiseGridProductionReport.rdlc";
            //    else
                rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.ProductionDef.rptItemWiseUsedMaterialReport.rdlc";
            }

            List<rcProduction> rList = ProductionRBL.Department_Used_Material_Report(rptClass, dc);
            rpt.DataSources.Add(new AppReport.DataSource("dsDeptProduction", rList));
            return rpt;
        }















        public static AppReport casting_Production_Report(clsPrmInventory rptClass, ReportOptions rptOptions)
        {
            return casting_Production_Report(rptClass, rptOptions, null);
        }
        public static AppReport casting_Production_Report(clsPrmInventory rptClass, ReportOptions rptOptions, DBContext dc)
        {
            AppReport rpt = new AppReport();
            rpt.ReportID = ReportIDEnum.Department_Production_Report;
            rpt.ReportOptions = rptOptions;
            SetParameter(rptClass, rpt, dc);


            if (rptClass.report_category == "Date-Wise")
            {
                //if (rptClass.From_Dept_Id == 135)
                //    rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.ProductionDef.rptGridDepartmentProduction.rdlc";
                //else
                    rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.ProductionDef.rptItemWiseDeptProductionReport.rdlc";
            }
            else
            {
                //if (rptClass.From_Dept_Id == 135)
                //    rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.ProductionDef.rptItemWiseGridProductionReport.rdlc";
                 
                //    //rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.ProductionDef.rptItemWiseDeptProductionReport.rdlc";
                //if (rptClass.From_Dept_Id == 2 || rptClass.From_Dept_Id == 137)
                //    //rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.ProductionDef.rptGrayOxideProductionReport.rdlc";
                //    rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.ProductionDef.rptItemWiseDeptProduction.rdlc";
                //else
                    rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.ProductionDef.rptItemWiseGridProductionReport.rdlc";
            }

            List<rcProduction> rList = ProductionRBL.Department_Production_Report(rptClass, dc);
            rpt.DataSources.Add(new AppReport.DataSource("dsDeptProduction", rList));
            return rpt;
        }


        //Formation

        public static AppReport Formation_Production_Report(clsPrmInventory rptClass, ReportOptions rptOptions)
        {
            return Formation_Production_Report(rptClass, rptOptions, null);
        }
        public static AppReport Formation_Production_Report(clsPrmInventory rptClass, ReportOptions rptOptions, DBContext dc)
        {
            AppReport rpt = new AppReport();
            rpt.ReportID = ReportIDEnum.Formation_Production_Report;
            rpt.ReportOptions = rptOptions;
            SetParameter(rptClass, rpt, dc);
            rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.ProductionDef.rptFormationProductionReport.rdlc";
            List<rcProduction> rList = ProductionRBL.Department_Production_Report(rptClass, dc);
            rpt.DataSources.Add(new AppReport.DataSource("dsDeptProduction", rList));
            return rpt;
        }

        public static AppReport Formation_Loading_Report(clsPrmInventory rptClass, ReportOptions rptOptions)
        {
            return Formation_Loading_Report(rptClass, rptOptions, null);
        }
        public static AppReport Formation_Loading_Report(clsPrmInventory rptClass, ReportOptions rptOptions, DBContext dc)
        {
            AppReport rpt = new AppReport();
            rpt.ReportID = ReportIDEnum.Department_Production_Report;
            rpt.ReportOptions = rptOptions;
            SetParameter(rptClass, rpt, dc);

            if (rptClass.report_category == "Date-Wise")
            {
                if (rptClass.From_Dept_Id == 135)
                    rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.ProductionDef.rptGridDepartmentProduction.rdlc";
                else
                    rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.ProductionDef.rptItemWiseDeptProductionReport.rdlc";
            }
            else
            {
                if (rptClass.From_Dept_Id == 135)
                    rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.ProductionDef.rptItemWiseGridProductionReport.rdlc";
                else
                    rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.ProductionDef.rptItemWiseFormationLoadingReport.rdlc";
            }

            List<rcProduction> rList = ProductionRBL.Formation_Loading_Report(rptClass, dc);
            rpt.DataSources.Add(new AppReport.DataSource("dsDeptProduction", rList));
            return rpt;
        }



        public static AppReport Formation_Un_Loading_Report(clsPrmInventory rptClass, ReportOptions rptOptions)
        {
            return Formation_Un_Loading_Report(rptClass, rptOptions, null);
        }
        public static AppReport Formation_Un_Loading_Report(clsPrmInventory rptClass, ReportOptions rptOptions, DBContext dc)
        {
            AppReport rpt = new AppReport();
            rpt.ReportID = ReportIDEnum.Department_Production_Report;
            rpt.ReportOptions = rptOptions;
            SetParameter(rptClass, rpt, dc);


            //if (rptClass.report_category == "Date-Wise")
            //{
            //    if (rptClass.From_Dept_Id == 135)
            //        rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.ProductionDef.rptGridDepartmentProduction.rdlc";
            //    else
            //        rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.ProductionDef.rptItemWiseDeptProductionReport.rdlc";
            //}
            //else
            //{
                rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.ProductionDef.rptItemWiseFormationUnloadingReport.rdlc";
            //}

            List<rcProduction> rList = ProductionRBL.Formation_Un_Loading_Report(rptClass, dc);
            rpt.DataSources.Add(new AppReport.DataSource("dsDeptProduction", rList));
            return rpt;
        }





       







        //Pasting

        public static AppReport Pasting_Production_Report(clsPrmInventory rptClass, ReportOptions rptOptions)
        {
            return Pasting_Production_Report(rptClass, rptOptions, null);
        }
        public static AppReport Pasting_Production_Report(clsPrmInventory rptClass, ReportOptions rptOptions, DBContext dc)
        {
            AppReport rpt = new AppReport();
            rpt.ReportID = ReportIDEnum.Department_Production_Report;
            rpt.ReportOptions = rptOptions;
            SetParameter(rptClass, rpt, dc);
            rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.ProductionDef.rptDepartmentProduction.rdlc";
            List<rcProduction> rList = ProductionRBL.Department_Production_Report(rptClass, dc);
            rpt.DataSources.Add(new AppReport.DataSource("dsDeptProduction", rList));
            return rpt;
        }

        #region Oxide
        public static AppReport Oxide_Production_Report(clsPrmInventory rptClass, ReportOptions rptOptions)
        {
            return Oxide_Production_Report(rptClass, rptOptions, null);
        }

        public static AppReport Oxide_DateWise_Production_Report(clsPrmInventory rptClass, ReportOptions rptOptions)
        {
            return Oxide_DateWise_Production_Report(rptClass, rptOptions, null);
        }
        public static AppReport Oxide_Production_Report(clsPrmInventory rptClass, ReportOptions rptOptions, DBContext dc)
        {
            AppReport rpt = new AppReport();
            rpt.ReportID = ReportIDEnum.Department_Production_Report;
            rpt.ReportOptions = rptOptions;
            SetParameter(rptClass, rpt, dc);
            //rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.ProductionDef.rptDepartmentProduction.rdlc";
            rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.ProductionDef.Oxide_rptDepartmentProduction.rdlc";
            List<rcProduction> rList = ProductionRBL.Department_Production_Report(rptClass, dc);
            rpt.DataSources.Add(new AppReport.DataSource("dsDeptProduction", rList));
            return rpt;
        }
        public static AppReport Oxide_DateWise_Production_Report(clsPrmInventory rptClass, ReportOptions rptOptions, DBContext dc)
        {
            AppReport rpt = new AppReport();
            rpt.ReportID = ReportIDEnum.Department_Production_Report;
            rpt.ReportOptions = rptOptions;
            SetParameter(rptClass, rpt, dc);
            rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.ProductionDef.rptOxideItemWiseDetails.rdlc";
            List<rcProduction> rList = ProductionRBL.Oxide_Department_Production_Report(rptClass, dc);
            rpt.DataSources.Add(new AppReport.DataSource("dsDeptProduction", rList));
            return rpt;
        }

        public static AppReport Gray_And_Red_Oxide_Daily_Production_Report(clsPrmInventory rptClass, ReportOptions rptOptions)
        {
            return Gray_And_Red_Oxide_Daily_Production_Report(rptClass, rptOptions, null);
        }
        public static AppReport Gray_And_Red_Oxide_Daily_Production_Report(clsPrmInventory rptClass, ReportOptions rptOptions, DBContext dc)
        {
            AppReport rpt = new AppReport();
            rpt.ReportID = ReportIDEnum.Department_Production_Report;
            rpt.ReportOptions = rptOptions;
            SetParameter(rptClass, rpt, dc);

            rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.ProductionDef.rptGray_Red_Oxide_Daily_Production.rdlc";

            rptClass.DeptID = 2;
            List<rcProduction> grayOxideList = ProductionRBL.Gray_Oxide_Daily_Production_Report(rptClass, dc);
            rpt.DataSources.Add(new AppReport.DataSource("dsGrayOxideProduction", grayOxideList));

            rptClass.DeptID = 137;
            List<rcProduction> redOxideList = ProductionRBL.Red_Oxide_Daily_Production_Report(rptClass, dc);
            rpt.DataSources.Add(new AppReport.DataSource("dsRedOxide", redOxideList));

            List<rcProduction> itemList = ProductionRBL.Oxide_Item_Closing_Report(rptClass, dc);
            rpt.DataSources.Add(new AppReport.DataSource("dsOxideItemList", itemList));

            return rpt;
        }


        public static AppReport RedOxide_RM_Summary_Report(clsPrmInventory rptClass, ReportOptions rptOptions)
        {
            return RedOxide_RM_Summary_Report(rptClass, rptOptions, null);
        }
        public static AppReport RedOxide_RM_Summary_Report(clsPrmInventory rptClass, ReportOptions rptOptions, DBContext dc)
        {
            AppReport rpt = new AppReport();
            rpt.ReportOptions = rptOptions;
            rpt.ReportOptions.ReportTitle = "Oxide Production Summmary Report";
            SetParameterOxideStock(rptClass, rpt, dc);
            rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.ProductionDef.rptRedOxideRMSummaryReport.rdlc";

            List<rcFormationProductionSummary> rList = MaterialStockRBL.Oxide_RM_Summary_Report(rptClass, dc);

            List<rcFormationProductionSummary> byProductList = rList.Where(x => x.IS_BY_PRODUCT == "Y").ToList();
            List<rcFormationProductionSummary> nonByProductList = rList.Where(x => x.IS_BY_PRODUCT == "N").ToList();

            rpt.DataSources.Add(new AppReport.DataSource("byProductList", byProductList));
            rpt.DataSources.Add(new AppReport.DataSource("nonByProductList", nonByProductList));

            //rpt.DataSources.Add(new AppReport.DataSource("Item_Stock", byProductList));

            return rpt;
        }



        public static AppReport Oxide_RM_Summary_Report(clsPrmInventory rptClass, ReportOptions rptOptions)
        {
            return Oxide_RM_Summary_Report(rptClass, rptOptions, null);
        }
        public static AppReport Oxide_RM_Summary_Report(clsPrmInventory rptClass, ReportOptions rptOptions, DBContext dc)
        {
            AppReport rpt = new AppReport();
            rpt.ReportOptions = rptOptions;
            rpt.ReportOptions.ReportTitle = "Oxide Production Summmary Report";
            SetParameterOxideStock(rptClass, rpt, dc);
            rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.ProductionDef.rptOxideRMSummaryReport.rdlc";

            List<rcFormationProductionSummary> rList = MaterialStockRBL.Oxide_RM_Summary_Report(rptClass, dc);
            rpt.DataSources.Add(new AppReport.DataSource("Item_Stock", rList));
            return rpt;
        }

        public static AppReport Oxide_Production_Summary_Report(clsPrmInventory rptClass, ReportOptions rptOptions)
        {
            return Oxide_Production_Summary_Report(rptClass, rptOptions, null);
        }
        public static AppReport Oxide_Production_Summary_Report(clsPrmInventory rptClass, ReportOptions rptOptions, DBContext dc)
        {
            AppReport rpt = new AppReport();
            rpt.ReportOptions = rptOptions;
            rpt.ReportOptions.ReportTitle = "Oxide Production Stock Summmary Report";
            SetParameterOxideStock(rptClass, rpt, dc);
            rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.ProductionDef.rptOxideProdSummaryReport.rdlc";

            List<rcFormationProductionSummary> rList = MaterialStockRBL.Oxide_Production_Summary_Report(rptClass, dc);
            rpt.DataSources.Add(new AppReport.DataSource("Item_Stock", rList));
            return rpt;
        }

        //Stock_report
        public static AppReport Oxide_Production_Stock_Summary_Report(clsPrmInventory rptClass, ReportOptions rptOptions)
        {
            return Oxide_Production_Stock_Summary_Report(rptClass, rptOptions, null);
        }
        public static AppReport Oxide_Production_Stock_Summary_Report(clsPrmInventory rptClass, ReportOptions rptOptions, DBContext dc)
        {
            AppReport rpt = new AppReport();
            rpt.ReportOptions = rptOptions;
            rpt.ReportOptions.ReportTitle = "Oxide Production Stock Summmary Report";
            SetParameterOxideStock(rptClass, rpt, dc);
            rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.ProductionDef.rptOxideProdSummaryStockReport.rdlc";

            List<rcFormationProductionSummary> rList = MaterialStockRBL.Oxide_Production_Stock_Summary_Report(rptClass, dc);
            rpt.DataSources.Add(new AppReport.DataSource("Item_Stock", rList));
            return rpt;
        }
        #endregion

        #region TRansfermation

        public static AppReport Pasting_Recipe_Details_Report(clsPrmInventory rptClass, ReportOptions rptOptions)
        {
            return Pasting_Recipe_Details_Report(rptClass, rptOptions, null);
        }
        public static AppReport Pasting_Recipe_Details_Report(clsPrmInventory rptClass, ReportOptions rptOptions, DBContext dc)
        {
            AppReport rpt = new AppReport();
            rpt.ReportOptions = rptOptions;
            rpt.ReportOptions.ReportTitle = " Recipe Report";
            SetParameterOxideStock(rptClass, rpt, dc);
            rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.ProductionDef.rptPastinRcpDetails.rdlc";

            List<rcItemTransfermation> rList = MaterialStockRBL.Pasting_Recipe_Details_Report(rptClass, dc);
            rpt.DataSources.Add(new AppReport.DataSource("rcItemTransfermation", rList));
            return rpt;
        }




        public static AppReport Transfer_Details_Report(clsPrmInventory rptClass, ReportOptions rptOptions)
        {
            return Transfer_Details_Report(rptClass, rptOptions, null);
        }
        public static AppReport Transfer_Details_Report(clsPrmInventory rptClass, ReportOptions rptOptions, DBContext dc)
        {
            AppReport rpt = new AppReport();
            rpt.ReportOptions = rptOptions;
            rpt.ReportOptions.ReportTitle = "Oxide Production Summmary Report";
            SetParameterOxideStock(rptClass, rpt, dc);
            rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.ProductionDef.rptTransfermationDetails.rdlc";

            List<rcItemTransfermation> rList = MaterialStockRBL.Transfermation_Details_Report(rptClass, dc);
            rpt.DataSources.Add(new AppReport.DataSource("rcItemTransfermation", rList));
            return rpt;
        }
        #endregion

        #region IB
        public static AppReport IB_Monthly_Used_RM_Report(clsPrmInventory rptClass, ReportOptions rptOptions)
        {
            return IB_Monthly_Used_RM_Report(rptClass, rptOptions, null);
        }
        public static AppReport IB_Monthly_Used_RM_Report(clsPrmInventory rptClass, ReportOptions rptOptions, DBContext dc)
        {
            AppReport rpt = new AppReport();
            rpt.ReportID = ReportIDEnum.Department_Production_Report;
            rpt.ReportOptions = rptOptions;
            SetParameter(rptClass, rpt, dc);
           // rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.ProductionDef.rptIB_Row_Material_Stock.rdlc";
            rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.ProductionDef.rptIB_Row_Material_Stock_With_Mixture.rdlc";

            List<PG.Report.ReportClass.InventoryRC.rcMaterialStock> rList = MaterialStockRBL.IBStockReport(rptClass, dc);

            List<PG.Report.ReportClass.InventoryRC.rcMaterialStock> dsDeptProduction = rList.Where(x => x.IS_BY_PRODUCT == "Y").ToList();
            List<PG.Report.ReportClass.InventoryRC.rcMaterialStock> dsRM_with_mixture = rList.Where(x => x.IS_BY_PRODUCT == "N").ToList();

           // List<PG.Report.ReportClass.InventoryRC.rcMaterialStock> rList = MaterialStockRBL.IBStockReport(rptClass, dc);
            //rptClass.ItemClassId = 0;
           
          //  List<PG.Report.ReportClass.InventoryRC.rcMaterialStock> rFinishedList = MaterialStockRBL.IBStockReport(rptClass, dc);
            rpt.DataSources.Add(new AppReport.DataSource("dsDeptProduction", dsDeptProduction));
            rpt.DataSources.Add(new AppReport.DataSource("dsRM_with_mixture", dsRM_with_mixture));
            //rpt.DataSources.Add(new AppReport.DataSource("dsisFinished", rFinishedList));
            return rpt;
        }



        public static AppReport IB_Monthly_Finished_Item_Report(clsPrmInventory rptClass, ReportOptions rptOptions)
        {
            return IB_Monthly_Finished_Item_Report(rptClass, rptOptions, null);
        }
        public static AppReport IB_Monthly_Finished_Item_Report(clsPrmInventory rptClass, ReportOptions rptOptions, DBContext dc)
        {
            AppReport rpt = new AppReport();
            rpt.ReportID = ReportIDEnum.Department_Production_Report;
            rpt.ReportOptions = rptOptions;
            SetParameter(rptClass, rpt, dc);
            rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.ProductionDef.rptIB_Finished_Item_Stock.rdlc";
            //List<PG.Report.ReportClass.InventoryRC.rcMaterialStock> rList = MaterialStockRBL.DepartmentStockReport(rptClass, dc);
            rptClass.ItemClassId = 0;
            rptClass.is_Finished = "Y";
            List<PG.Report.ReportClass.InventoryRC.rcMaterialStock> rFinishedList = MaterialStockRBL.IBStockReport(rptClass, dc);
            rpt.DataSources.Add(new AppReport.DataSource("dsIBFinished", rFinishedList));
            return rpt;
        }

        public static void SetParameterOxideStock(clsPrmInventory prmINV, AppReport rpt, DBContext dc)
        {
            string criteriaString = string.Empty;
            string Group_Name = string.Empty;
            string deptName = string.Empty;
            string Item_Name = string.Empty; 
            string Item_Type = string.Empty;
            string Store = string.Empty;
            string classNmae = string.Empty;
            string Dept_Name = string.Empty;
            string invmonth = string.Empty;
            string platetype = string.Empty;
            string fdate = string.Empty;
            string tdate = string.Empty;
            string deptShrotName = string.Empty;

            criteriaString = "Date From :" + prmINV.fromProdDate + " To " + prmINV.toProdDate;

            string strList = string.Empty;
            invmonth = prmINV.fromProdDate;

            if (prmINV.From_Dept_Id > 0)
            {
                if (!string.IsNullOrEmpty(prmINV.From_Dept_Name))
                {
                    deptName = " " + prmINV.From_Dept_Name;
                }
            }


            if (prmINV.itemgroupName != "")
            {
                Group_Name = "Group :" + prmINV.itemgroupName;
            }
            else
            {
                Group_Name = "Group : All";

            }

            if (prmINV.itemName != "")
            {
                Item_Name = "Item :" + prmINV.itemName;
            }
            else
            {
                Item_Name = "Item : All";

            }

            if (prmINV.itemtypeName != "")
            {
                Item_Type = "Type :" + prmINV.itemtypeName;
            }
            else
            {
                Item_Type = "Type : All";

            }

            if (prmINV.From_Dept_Id > 0)
            {
                Dept_Name = "Dept : " + prmINV.From_Dept_Name;
            }
            if (!string.IsNullOrEmpty(prmINV.Plate_Type))
            {
                if (prmINV.Plate_Type == "R")
                {
                    platetype = "Rejected Plate Repot";
                }
                if (prmINV.Plate_Type == "U")
                {
                    platetype = "Unformed Plate Repot";
                }
                if (prmINV.Plate_Type == "F")
                {
                    platetype = "Formed Plate Repot";
                }
            }

            fdate = prmINV.fromProdDate;
            tdate = prmINV.toProdDate;

            if (prmINV.From_Dept_Id == 2)
            {
                deptShrotName = "Gray";
            }
            if (prmINV.From_Dept_Id == 137)
            {
                deptShrotName = "Red";
            }

            Store = "Store :" + prmINV.storeName;

            classNmae = "Class :" + prmINV.itemclassname;

            //rpt.AddParameter("prmCompanyName", rpt.ReportOptions.CompanyName);
            //rpt.AddParameter("prmPoweredBy", rpt.ReportOptions.AppPoweredBy);

            rpt.AddParameter("prmCriteriaString", criteriaString);
            //rpt.AddParameter("deptName", deptName);
            rpt.AddParameter("prmGroup", Group_Name);
            rpt.AddParameter("prmItemName", Item_Name);
            rpt.AddParameter("prmItemType", Item_Type);
            rpt.AddParameter("prmStore", Store);
            rpt.AddParameter("prmclassNmae", classNmae);
            rpt.AddParameter("prmDeptNmae", Dept_Name);
            rpt.AddParameter("prmInvMonth", invmonth);
            rpt.AddParameter("prmfdate", fdate);
            rpt.AddParameter("prmtdate", tdate);
            rpt.AddParameter("prmdeptShrotName", deptShrotName);

        }





        public static AppReport IB_ProcessTypeWiseProd_Report(clsPrmInventory rptClass, ReportOptions rptOptions)
        {
            return IB_ProcessTypeWiseProd_Report(rptClass, rptOptions, null);
        }
        public static AppReport IB_ProcessTypeWiseProd_Report(clsPrmInventory rptClass, ReportOptions rptOptions, DBContext dc)
        {
            AppReport rpt = new AppReport();
            rpt.ReportID = ReportIDEnum.Department_Production_Report;
            rpt.ReportOptions = rptOptions;
            SetParameterIBProcess(rptClass, rpt, dc);
            rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.ProductionDef.rptIBProcessWiseProdReport.rdlc";
            List<rcProduction> rFinishedList = ProductionRBL.IBProcessTypeWise_Report(rptClass, dc);
            
            rpt.DataSources.Add(new AppReport.DataSource("dsProcess", rFinishedList));
            return rpt;
        }




        public static AppReport IB_Production_Entry_Report(clsPrmInventory rptClass, ReportOptions rptOptions)
        {
            return IB_Production_Entry_Report(rptClass, rptOptions, null);
        }
        public static AppReport IB_Production_Entry_Report(clsPrmInventory rptClass, ReportOptions rptOptions, DBContext dc)
        {
            AppReport rpt = new AppReport();
            rpt.ReportID = ReportIDEnum.Department_Production_Report;
            rpt.ReportOptions = rptOptions;
            SetParameterProductionStock(rptClass, rpt, dc);


            if (rptClass.report_category == "Date-Wise")
            {
                rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.ProductionDef.rptIBroductionEntry_dateWise.rdlc";
            }
            else
            {
                rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.ProductionDef.rptIBProductionEntryReport_ItemWise.rdlc";
            }

            List<rcProduction> rList = ProductionRBL.Department_Production_Report(rptClass, dc);
            rpt.DataSources.Add(new AppReport.DataSource("dsDeptProduction", rList));
            return rpt;
        }
        #endregion


        public static void SetParameter(clsPrmInventory prm, AppReport rpt, DBContext dc)
        {
            string criteriaString = string.Empty;
            string dateString = string.Empty;
            string postString = string.Empty;

            string Reporttype = string.Empty;
            string Customer = string.Empty;
            string Processtype = string.Empty;
            string stlm_Name = string.Empty;

            if (prm.From_Dept_Id > 0)
            {
                criteriaString = prm.From_Dept_Name;
            }

            if (prm.Shift_Id != "0" && !string.IsNullOrEmpty(prm.Shift_Id))
            {
                criteriaString = criteriaString + prm.Shift_name;
            }

            if (prm.StorageLocationId >0)
            {
                criteriaString = criteriaString + "  Storage Loc:" + prm.STLM_NAME;
            }

            
            if (prm.itemGroup_id > 0)
            {
                criteriaString = criteriaString + "  Item Group:" + prm.itemgroupName;
            }
            if (prm.item_id > 0)
            {
                criteriaString = criteriaString + "  Item:" + prm.itemName + ",";
            }

            if(prm.ProcessType=="C")
            {
                Processtype = "Process Type :"+"Cutting";
            }
            if (prm.ProcessType == "F")
            {
                Processtype = "Process Type :" + "Filling";
            }
            if (prm.ProcessType == "S")
            {
                Processtype = "Process Type :" + "Sulphation";
            }

            if (prm.isPacking !="")
            {
                if (prm.From_Dept_Id == 136)
                criteriaString = criteriaString + "  Packing :" + prm.isPacking + ",";
            }
            else
            {
                if (prm.From_Dept_Id == 136)
                    criteriaString = criteriaString + "  Packing :" + prm.isPacking + ",";
            }

            criteriaString = criteriaString + "  From Date :" + prm.fromProdDate + "  To Date :" + prm.toProdDate;

            rpt.AddParameter("prmCompanyName", rpt.ReportOptions.CompanyName);
            rpt.AddParameter("prmPoweredBy", rpt.ReportOptions.AppPoweredBy);
            rpt.AddParameter("prmUserName", rpt.ReportOptions.UserName);
            rpt.AddParameter("prmReportCriteriaString", criteriaString);
            rpt.AddParameter("prmTitleName", rpt.ReportOptions.ReportTitle);
            rpt.AddParameter("prmReportCaption", rpt.ReportOptions.ReportCaption);
            rpt.AddParameter("prmProcessType", Processtype);

        }
        public static void SetParameterIBProcess(clsPrmInventory prm, AppReport rpt, DBContext dc)
        {
            string criteriaString = string.Empty;
            string dateString = string.Empty;
            string postString = string.Empty;

            string Reporttype = string.Empty;
            string Customer = string.Empty;
            string Processtype = string.Empty;
            string shift = string.Empty;
            string department = string.Empty;
            string itemName=string.Empty;

            if (prm.From_Dept_Name!=string.Empty)
            {
                department ="Department: "+ prm.From_Dept_Name;
            }

            //if (prm.Shift_Id != "0" && !string.IsNullOrEmpty(prm.Shift_Id))
            //{
            //    shift = "Shift :" + prm.Shift_name;
            //}


          
            if (prm.item_id > 0)
            {
                itemName = "Item: " + prm.itemName;
            }

            if (prm.ProcessType == "C")
            {
                Processtype = "Process Type : " + "Cutting";
            }
            if (prm.ProcessType == "F")
            {
                Processtype = "Process Type : " + "Filling";
            }
            if (prm.ProcessType == "S")
            {
                Processtype = "Process Type : " + "Sulphation";
            }

            //string fdate
            criteriaString = "From Date : " + Convert.ToDateTime(prm.FromDate).ToString("dd-MMM-yyyy") + "  To Date :" + Convert.ToDateTime(prm.ToDate).ToString("dd-MMM-yyyy");

           
          
            rpt.AddParameter("prmProcessType", Processtype);
            rpt.AddParameter("prmDepartment", department);
           // rpt.AddParameter("prmItemName", itemName);
            rpt.AddParameter("prmCriteriaString", criteriaString);



        }

        #region Electrolyte

        public static AppReport Electrolyte_RM_Summary_Report(clsPrmInventory rptClass, ReportOptions rptOptions)
        {
            return Electrolyte_RM_Summary_Report(rptClass, rptOptions, null);
        }
        public static AppReport Electrolyte_RM_Summary_Report(clsPrmInventory rptClass, ReportOptions rptOptions, DBContext dc)
        {
            AppReport rpt = new AppReport();
            rpt.ReportOptions = rptOptions;
            rpt.ReportOptions.ReportTitle = "Electrolyte RM Summmary Report";
            SetParameterElectrolyteStock(rptClass, rpt, dc);
            rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.ProductionDef.rptElectrolyteRawMaterial.rdlc";

            List<PG.Report.ReportClass.InventoryRC.rcMaterialStock> rList = MaterialStockRBL.Electrolyte_RM_Summary_Report(rptClass, dc);
            rpt.DataSources.Add(new AppReport.DataSource("Item_Stock", rList));
            return rpt;
        }


        public static AppReport Electrolyte_Production_Report(clsPrmInventory rptClass, ReportOptions rptOptions)
        {
            return Electrolyte_Production_Report(rptClass, rptOptions, null);
        }
        public static AppReport Electrolyte_Production_Report(clsPrmInventory rptClass, ReportOptions rptOptions, DBContext dc)
        {
            AppReport rpt = new AppReport();
            rpt.ReportID = ReportIDEnum.Department_Production_Report;
            rpt.ReportOptions = rptOptions;
            SetParameter(rptClass, rpt, dc);
            rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.ProductionDef.Electrolyte_rptDepartmentProduction.rdlc";
            List<rcProduction> rList = ProductionRBL.Department_Production_Report(rptClass, dc);
            rpt.DataSources.Add(new AppReport.DataSource("dsDeptProduction", rList));
            return rpt;
        }




        public static void SetParameterElectrolyteStock(clsPrmInventory prmINV, AppReport rpt, DBContext dc)
        {
            string criteriaString = string.Empty;
            string Group_Name = string.Empty;
            string deptName = string.Empty;
            string Item_Name = string.Empty;
            string Item_Type = string.Empty;
            string Store = string.Empty;
            string classNmae = string.Empty;
            string Dept_Name = string.Empty;
            string invmonth = string.Empty;
            string platetype = string.Empty;
            string fdate = string.Empty;
            string tdate = string.Empty;
            string deptShrotName = string.Empty;

            criteriaString = "Date From :" + prmINV.fromProdDate + " To " + prmINV.toProdDate;

            string strList = string.Empty;
            invmonth = prmINV.fromProdDate;

            if (prmINV.From_Dept_Id > 0)
            {
                if (!string.IsNullOrEmpty(prmINV.From_Dept_Name))
                {
                    deptName = " Dept : " + prmINV.From_Dept_Name;
                    criteriaString = criteriaString + " " + deptName;
                }
            }


            if (prmINV.itemgroupName != "")
            {
                Group_Name = "Group :" + prmINV.itemgroupName;
            }
            else
            {
                Group_Name = "Group : All";

            }

            if (prmINV.itemName != "")
            {
                Item_Name = "Item :" + prmINV.itemName;
                criteriaString = criteriaString + " " + Item_Name;
            }
            else
            {
                Item_Name = "Item : All";
                criteriaString = criteriaString + " " + Item_Name;
            }

            if (prmINV.itemtypeName != "")
            {
                Item_Type = "Type :" + prmINV.itemtypeName;
            }
            else
            {
                Item_Type = "Type : All";

            }

            if (prmINV.From_Dept_Id > 0)
            {
                Dept_Name = "Dept : " + prmINV.From_Dept_Name;
                //criteriaString = criteriaString + " " + Dept_Name;
            }
            

            fdate = prmINV.fromProdDate;
            tdate = prmINV.toProdDate;

          
            Store = "Store :" + prmINV.storeName;

            classNmae = "Class :" + prmINV.itemclassname;

            //rpt.AddParameter("prmCompanyName", rpt.ReportOptions.CompanyName);
            //rpt.AddParameter("prmPoweredBy", rpt.ReportOptions.AppPoweredBy);

            rpt.AddParameter("prmCriteriaString", criteriaString);
            //rpt.AddParameter("deptName", deptName);
            //rpt.AddParameter("prmGroup", Group_Name);
            //rpt.AddParameter("prmItemName", Item_Name);
            //rpt.AddParameter("prmItemType", Item_Type);
            //rpt.AddParameter("prmStore", Store);
            //rpt.AddParameter("prmclassNmae", classNmae);
            //rpt.AddParameter("prmDeptNmae", Dept_Name);
            //rpt.AddParameter("prmInvMonth", invmonth);
            //rpt.AddParameter("prmfdate", fdate);
            //rpt.AddParameter("prmtdate", tdate);
            //rpt.AddParameter("prmdeptShrotName", deptShrotName);

        }





        public static AppReport Electrolyte_Production_Summary_Report(clsPrmInventory rptClass, ReportOptions rptOptions)
        {
            return Electrolyte_Production_Summary_Report(rptClass, rptOptions, null);
        }
        public static AppReport Electrolyte_Production_Summary_Report(clsPrmInventory rptClass, ReportOptions rptOptions, DBContext dc)
        {
            AppReport rpt = new AppReport();
            rpt.ReportOptions = rptOptions;
            rpt.ReportOptions.ReportTitle = "Electrolyte Production Stock Summmary Report";
            SetParameterElectrolyteStock(rptClass, rpt, dc);
            rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.ProductionDef.rptElectrolyteProdSummaryReport.rdlc";

            List<rcFormationProductionSummary> rList = MaterialStockRBL.Electrolyte_Production_Summary_Report(rptClass, dc);
            rpt.DataSources.Add(new AppReport.DataSource("Item_Stock", rList));
            return rpt;
        }



        #endregion


        #region DWM
        public static AppReport DMW_Production_Report(clsPrmInventory rptClass, ReportOptions rptOptions)
        {
            return DMW_Production_Report(rptClass, rptOptions, null);
        }
        public static AppReport DMW_Production_Report(clsPrmInventory rptClass, ReportOptions rptOptions, DBContext dc)
        {
            AppReport rpt = new AppReport();
            rpt.ReportID = ReportIDEnum.Department_Production_Report;
            rpt.ReportOptions = rptOptions;
            SetParameter(rptClass, rpt, dc);
            rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.ProductionDef.DMW_rptDepartmentProduction.rdlc";
            List<rcProduction> rList = ProductionRBL.Department_Production_Report(rptClass, dc);
            rpt.DataSources.Add(new AppReport.DataSource("dsDeptProduction", rList));
            return rpt;
        }



        public static AppReport DMW_RM_Summary_Report(clsPrmInventory rptClass, ReportOptions rptOptions)
        {
            return DMW_RM_Summary_Report(rptClass, rptOptions, null);
        }
        public static AppReport DMW_RM_Summary_Report(clsPrmInventory rptClass, ReportOptions rptOptions, DBContext dc)
        {
            AppReport rpt = new AppReport();
            rpt.ReportOptions = rptOptions;
            rpt.ReportOptions.ReportTitle = "DMW RM Summmary Report";
            SetParameterOxideStock(rptClass, rpt, dc);
            rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.ProductionDef.rptDMWRawMaterial.rdlc";

            List<PG.Report.ReportClass.InventoryRC.rcMaterialStock> rList = MaterialStockRBL.DMW_RM_Summary_Report(rptClass, dc);
            rpt.DataSources.Add(new AppReport.DataSource("Item_Stock", rList));
            return rpt;
        }


        public static AppReport DMW_Production_Summary_Report(clsPrmInventory rptClass, ReportOptions rptOptions)
        {
            return DMW_Production_Summary_Report(rptClass, rptOptions, null);
        }
        public static AppReport DMW_Production_Summary_Report(clsPrmInventory rptClass, ReportOptions rptOptions, DBContext dc)
        {
            AppReport rpt = new AppReport();
            rpt.ReportOptions = rptOptions;
            rpt.ReportOptions.ReportTitle = "DMW Production Stock Summmary Report";
            SetParameterElectrolyteStock(rptClass, rpt, dc);
            rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.ProductionDef.rptDMWProdSummaryReport.rdlc";

            List<rcFormationProductionSummary> rList = MaterialStockRBL.DMW_Production_Summary_Report(rptClass, dc);
            rpt.DataSources.Add(new AppReport.DataSource("Item_Stock", rList));
            return rpt;
        }

        #endregion

        //Rejection Plate

        public static AppReport GetRejection_Report(clsPrmInventory prmINV, ReportOptions rptOptions)
        {
            return GetRejection_Report(prmINV, rptOptions, null);
        }
        public static AppReport GetRejection_Report(clsPrmInventory prmINV, ReportOptions rptOptions, DBContext dc)
        {
            AppReport rpt = new AppReport();
            rpt.ReportOptions = rptOptions;
            rpt.ReportOptions.ReportTitle = "Rejection Report";
            SetParameterPlateRejection(prmINV, rpt, dc);
            //Summary  
            //if (prmINV.Report_Type == 0)
            //{
            //    if (prmINV.DeptID == 136 || prmINV.DeptID == 140)
            //    {
            //        rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.ProductionDef.rptRejectionSummaryReport.rdlc";
            //        List<rcProduction> rList = ProductionRBL.Get_RejectionInfoAssemblySumm_Report(prmINV, dc);

            //        rpt.DataSources.Add(new AppReport.DataSource("Rejection", rList));
            //    }
            //    else
            //    {
            //        rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.ProductionDef.rptRejectionSummaryReport.rdlc";
            //        List<rcProduction> rList = ProductionRBL.Get_RejectionInfoSumm_Report(prmINV, dc);

            //        rpt.DataSources.Add(new AppReport.DataSource("Rejection", rList));
            //    }
              
            //}
            //Details 
            if (prmINV.Report_Type == 1)
            {
                //if (prmINV.DeptID == 136 || prmINV.DeptID == 140)
                //{
                //    rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.ProductionDef.rptRejectionReport.rdlc";
                //    List<rcProduction> rList = ProductionRBL.Get_RejectionAssemblyDetailsInfo_Report(prmINV, dc);

                //    rpt.DataSources.Add(new AppReport.DataSource("Rejection", rList));
                //}
                //else
                //{
                    rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.ProductionDef.rptRejectionReport.rdlc";
                    List<rcProduction> rList = ProductionRBL.Get_RejectionDetailsInfo_Report(prmINV, dc);

                    rpt.DataSources.Add(new AppReport.DataSource("Rejection", rList));
                //}
            }

            return rpt;
        }



        public static void SetParameterPlateRejection(clsPrmInventory prmINV, AppReport rpt, DBContext dc)
        {
            string criteriaString = string.Empty;
            string Group_Name = string.Empty;
            string deptName = string.Empty;
            string Item_Name = string.Empty;
            string Item_Type = string.Empty;
            string Store = string.Empty;
            string classNmae = string.Empty;
            string Dept_Name = string.Empty;
            //string invmonth = string.Empty;
           
            criteriaString = "Date From :" + prmINV.FromDate.Value.ToString("dd-MMM-yyyy") + " To " + prmINV.ToDate.Value.ToString("dd-MMM-yyyy");

            string strList = string.Empty;
            //invmonth = prmINV.FromDate.Value.ToString("MMM-yyyy");

            if (prmINV.DeptID > 0)
            {
                if (!string.IsNullOrEmpty(prmINV.From_Dept_Name))
                {
                    deptName = "Department: " + prmINV.From_Dept_Name;
                }
            }


            if (prmINV.itemgroupName != "")
            {
                Group_Name = "Group :" + prmINV.itemgroupName;
            }
            else
            {
                Group_Name = "Group : All";

            }

            if (prmINV.itemName != "")
            {
                Item_Name = "Item :" + prmINV.itemName;
            }
            else
            {
                Item_Name = "Item : All";

            }

            if (prmINV.itemtypeName != "")
            {
                Item_Type = "Type :" + prmINV.itemtypeName;
            }
            else
            {
                Item_Type = "Type : All";

            }

            //if (prmINV.DeptID > 0)
            //{
            //    Dept_Name = "Department : " + prmINV.From_Dept_Name;
            //}
           

           


            Store = "Store :" + prmINV.storeName;

            classNmae = "Class :" + prmINV.itemclassname;

            rpt.AddParameter("prmCompanyName", rpt.ReportOptions.CompanyName);
            rpt.AddParameter("prmPoweredBy", rpt.ReportOptions.AppPoweredBy);

            rpt.AddParameter("prmCriteriaString", criteriaString);
            rpt.AddParameter("deptName", deptName);
            rpt.AddParameter("prmGroup", Group_Name);
            rpt.AddParameter("prmItemName", Item_Name);
            rpt.AddParameter("prmItemType", Item_Type);
           // rpt.AddParameter("prmStore", Store);
            rpt.AddParameter("prmclassNmae", classNmae);
            //rpt.AddParameter("prmDeptNmae", Dept_Name);
            //rpt.AddParameter("prmInvMonth", invmonth);
           


        }



        public static void SetParameterProductionStock(clsPrmInventory prmINV, AppReport rpt, DBContext dc)
        {
            string criteriaString = string.Empty;
            string Group_Name = string.Empty;
            string deptName = string.Empty;
            string Item_Name = string.Empty;
            string Item_Type = string.Empty;
            string Store = string.Empty;
            string classNmae = string.Empty;
            string Dept_Name = string.Empty;
            string invmonth = string.Empty;
            string platetype = string.Empty;
            string fdate = string.Empty;
            string tdate = string.Empty;
            string deptShrotName = string.Empty;
            string ProcessType = string.Empty;
            string autho = string.Empty;

            criteriaString = "Date From :" + prmINV.fromProdDate + " To " + prmINV.toProdDate;

            string strList = string.Empty;
            invmonth = prmINV.fromProdDate;

            if (prmINV.From_Dept_Id > 0)
            {
                if (!string.IsNullOrEmpty(prmINV.From_Dept_Name))
                {
                    deptName = " Dept : " + prmINV.From_Dept_Name;
                    criteriaString = criteriaString + " " + deptName;
                }
            }


            if (prmINV.itemgroupName != "")
            {
                Group_Name = "Group :" + prmINV.itemgroupName;
            }
            else
            {
                Group_Name = "Group : All";

            }

            if (prmINV.itemName != "")
            {
                Item_Name = "Item :" + prmINV.itemName;
                criteriaString = criteriaString + " " + Item_Name;
            }
            else
            {
                Item_Name = "Item : All";
                criteriaString = criteriaString + " " + Item_Name;
            }

            if (prmINV.itemtypeName != "")
            {
                Item_Type = "Type :" + prmINV.itemtypeName;
            }
            else
            {
                Item_Type = "Type : All";

            }

            if (prmINV.From_Dept_Id > 0)
            {
                Dept_Name = "Dept : " + prmINV.From_Dept_Name;
                //criteriaString = criteriaString + " " + Dept_Name;
            }
            if(prmINV.ProcessType!="")
            {
                ProcessType = "Process : " + prmINV.ProcessType;
                criteriaString = criteriaString + " " + ProcessType;
            }
            else
            {
                ProcessType = "Process : All";
                criteriaString = criteriaString + " " + ProcessType;
            }

            if (prmINV.autho_status != "")
            {
                autho = "Autho : " + prmINV.autho_status;
                criteriaString = criteriaString + " " + autho;
            }
            else
            {
                autho = "Autho : All";
                criteriaString = criteriaString + " " + autho;
            }
            fdate = prmINV.fromProdDate;
            tdate = prmINV.toProdDate;


            Store = "Store :" + prmINV.storeName;

            classNmae = "Class :" + prmINV.itemclassname;

            //rpt.AddParameter("prmCompanyName", rpt.ReportOptions.CompanyName);
            //rpt.AddParameter("prmPoweredBy", rpt.ReportOptions.AppPoweredBy);

            rpt.AddParameter("prmCriteriaString", criteriaString);
            //rpt.AddParameter("deptName", deptName);
            //rpt.AddParameter("prmGroup", Group_Name);
            //rpt.AddParameter("prmItemName", Item_Name);
            //rpt.AddParameter("prmItemType", Item_Type);
            //rpt.AddParameter("prmStore", Store);
            //rpt.AddParameter("prmclassNmae", classNmae);
            //rpt.AddParameter("prmDeptNmae", Dept_Name);
            //rpt.AddParameter("prmInvMonth", invmonth);
            //rpt.AddParameter("prmfdate", fdate);
            //rpt.AddParameter("prmtdate", tdate);
            //rpt.AddParameter("prmdeptShrotName", deptShrotName);

        }


        #region ASSEMBLY
        public static AppReport ASM_Production_Details_Report(clsPrmInventory rptClass, ReportOptions rptOptions)
        {
            return ASM_Production_Details_Report(rptClass, rptOptions, null);
        }
        public static AppReport ASM_Production_Details_Report(clsPrmInventory rptClass, ReportOptions rptOptions, DBContext dc)
        {
            AppReport rpt = new AppReport();
            rpt.ReportID = ReportIDEnum.Department_Production_Report;
            rpt.ReportOptions = rptOptions;
            SetParameter(rptClass, rpt, dc);

            if (rptClass.report_category == "Date-Wise")
            {
                rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.ProductionDef.rptASMItemWiseDeptProductionReport.rdlc";
            }
            else
            {
                rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.ProductionDef.rptASMItemWiseProductionDetailsReport.rdlc";
            }

            List<rcProduction> rList = ProductionRBL.Department_Production_Report(rptClass, dc);
            rpt.DataSources.Add(new AppReport.DataSource("dsDeptProduction", rList));
            return rpt;
        }

        #endregion


        #region ********************************************  Pure Lead*****************************************************

        public static AppReport PureLead_Production_Report(clsPrmInventory rptClass, ReportOptions rptOptions)
        {
            return PureLead_Production_Report(rptClass, rptOptions, null);
        }


        public static AppReport PureLead_Production_Report(clsPrmInventory rptClass, ReportOptions rptOptions, DBContext dc)
        {
            AppReport rpt = new AppReport();
            rpt.ReportID = ReportIDEnum.Department_Production_Report;
            rpt.ReportOptions = rptOptions;
            SetParameter(rptClass, rpt, dc);
            rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.ProductionDef.PureLead_rptDepartmentProduction.rdlc";
            List<rcProduction> rList = ProductionRBL.Department_Production_Report(rptClass, dc);
            rpt.DataSources.Add(new AppReport.DataSource("dsDeptProduction", rList));
            return rpt;
        }


        #endregion


        #region **************************MRB******************************

        public static AppReport MRB_Production_Report(clsPrmInventory rptClass, ReportOptions rptOptions)
        {
            return MRB_Production_Report(rptClass, rptOptions, null);
        }
        public static AppReport MRB_Production_Report(clsPrmInventory rptClass, ReportOptions rptOptions, DBContext dc)
        {
            AppReport rpt = new AppReport();
            rpt.ReportID = ReportIDEnum.Department_Production_Report;
            rpt.ReportOptions = rptOptions;
            SetParameter(rptClass, rpt, dc);
            rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.ProductionDef.MRB_rptDepartmentProduction.rdlc";
            List<rcProduction> rList = ProductionRBL.MRB_Department_Production_Report(rptClass, dc);
            rpt.DataSources.Add(new AppReport.DataSource("dsDeptProduction", rList));
            return rpt;
        }



        public static AppReport MRB_Production_Summary_Report(clsPrmInventory rptClass, ReportOptions rptOptions)
        {
            return MRB_Production_Summary_Report(rptClass, rptOptions, null);
        }
        public static AppReport MRB_Production_Summary_Report(clsPrmInventory rptClass, ReportOptions rptOptions, DBContext dc)
        {
            AppReport rpt = new AppReport();
            rpt.ReportOptions = rptOptions;
            rpt.ReportOptions.ReportTitle = "MRB Production Stock Summmary Report";
            SetParameterElectrolyteStock(rptClass, rpt, dc);
            rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.ProductionDef.MRBrptProdSummaryReport.rdlc";

            List<rcFormationProductionSummary> rList = MaterialStockRBL.MRB_Production_Summary_Report(rptClass, dc);
            rpt.DataSources.Add(new AppReport.DataSource("Item_Stock", rList));
            return rpt;
        }


        #endregion


        #region **********************************Item Stock Ledger *********************************************

        public static AppReport Item_Ledger_Report(clsPrmInventory rptClass, ReportOptions rptOptions)
        {
            return Item_Ledger_Report(rptClass, rptOptions, null);
        }
        public static AppReport Item_Ledger_Report(clsPrmInventory rptClass, ReportOptions rptOptions, DBContext dc)
        {
            AppReport rpt = new AppReport();
            rpt.ReportOptions = rptOptions;
            rpt.ReportOptions.ReportTitle = "Item Ledger Report";
            SetParameterElectrolyteStock(rptClass, rpt, dc);
            rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.ProductionDef.rptItemStockLedgerReport.rdlc";
            List<rcItemTransfermation> rList = MaterialStockRBL.Item_Stock_Ledger_Report(rptClass, dc);
            rpt.DataSources.Add(new AppReport.DataSource("Item_Stock_Ledger", rList));
            return rpt;
        }

        #endregion


        #region ********************************************  Pure Lead Consumption*****************************************************
        public static AppReport GeneratePureLeadConsumption(clsPrmInventory rptClass, ReportOptions rptOptions)
        {
            return GeneratePureLeadConsumption(rptClass, rptOptions, null);
        }
        public static AppReport GeneratePureLeadConsumption(clsPrmInventory rptClass, ReportOptions rptOptions, DBContext dc)
        {
            AppReport rpt = new AppReport();
            rpt.ReportID = ReportEnums.ReportIDEnum.Lead_Consumption_Summary;
            rpt.ReportOptions = rptOptions;
            //rpt.ReportOptions.ReportTitle = "Lead Consumption";
            //SetParameter(rptClass, rpt, dc);
            SetParameterElectrolyteStock(rptClass, rpt, dc);
           // rpt.AddParameter("prmShowParentGroup", prmLedger.GroupLedgerShowType == GroupsLedgerShowEnum.Ledgers ? "1" : "0");
            rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.ProductionDef.rptLead_ConsumptionDept_Report.rdlc";
            List<dcMaterialStock> rList = ProductionRBL.GetPureLeadConsumption(rptClass, dc);
            List<dcMaterialStock> rBList = ProductionRBL.GetBatteryLeadConsumption(rptClass, dc);


            rpt.DataSources.Add(new AppReport.DataSource("Item_Stock", rList));
            rpt.DataSources.Add(new AppReport.DataSource("BStock", rBList));



            List<dcMaterialStock> rPasting = ProductionRBL.GeneratePestingRejectGridSummReport(rptClass, dc);
            //List<rcFormationProductionSummary> rFormation = ProductionRBL.Formation_Production_Summary_Report(rptClass, dc);
             rcFormationProductionSummary  rFormation = ProductionRBL.Formation_Production_Summary_Report(rptClass, dc).FirstOrDefault();
            //rpt.DataSources.Add(new AppReport.DataSource("rFormation", rFormation));

             dcMaterialStock rPastingR = new  dcMaterialStock();
             
            rPastingR.DEPARTMENT_NAME=rFormation.DEPARTMENT_NAME;
            rPastingR.OP_GD_GRID_BAL=rFormation.OP_F_BAL+rFormation.OP_UF_BAL+rFormation.OP_WIP;
            rPastingR.TOTAL_GOOD_GRID_RCV_QTY=rFormation.RCV_FROM_IB+rFormation.RCV_FROM_PASTING;
            rPastingR.ITC_GD_GRID_QTY=rFormation.ISS_TO_ASSEMBLY;
            rPastingR.IRR_REJ_GRID_QTY=rFormation.REJECT_QTY;
            rPastingR.TOTAL_REJ_GRID_WET = rFormation.TOTAL_REJECT_WEIGHT;
            rPasting.Add(rPastingR);




             rcAssemblyFinishedStock  rASMList = ProductionRBL.AssemblyUsePlateReport(rptClass, dc).FirstOrDefault();
            dcMaterialStock rASMR = new dcMaterialStock();

            rASMR.DEPARTMENT_NAME = rASMList.DEPARTMENT_NAME;
            rASMR.OP_GD_GRID_BAL = rASMList.OP_GD_PLATE_BAL;
            rASMR.TOTAL_GOOD_GRID_RCV_QTY = rASMList.TOTAL_GD_PLATE_RCV_QTY;
            rASMR.ITC_GD_GRID_QTY = rASMList.TOTAL_PLATE_CONS_WITH_REJ_QTY;
            rASMR.IRR_REJ_GRID_QTY = rASMList.TOTAL_REJ_REMAIN_QTY;
            rASMR.TOTAL_REJ_GRID_WET = rASMList.REJ_STOCK_QTY;
            rPasting.Add(rASMR);



            rcAssemblyFinishedStock  rVRLAList = ProductionRBL.VRLA_AssemblyUsePlateReport(rptClass, dc).FirstOrDefault();
            dcMaterialStock rVRLA = new dcMaterialStock();

            rVRLA.DEPARTMENT_NAME = rVRLAList.DEPARTMENT_NAME;
            rVRLA.OP_GD_GRID_BAL = rVRLAList.OP_GD_PLATE_BAL;
            rVRLA.TOTAL_GOOD_GRID_RCV_QTY = rVRLAList.TOTAL_GD_PLATE_RCV_QTY;
            rVRLA.ITC_GD_GRID_QTY = rVRLAList.TOTAL_PLATE_CONS_WITH_REJ_QTY;
            rVRLA.IRR_REJ_GRID_QTY = rVRLAList.IRR_REJ_PLATE_QTY;
            rVRLA.TOTAL_REJ_GRID_WET = rVRLAList.REJ_STOCK_QTY;
            rPasting.Add(rVRLA);


            rpt.DataSources.Add(new AppReport.DataSource("rPasting", rPasting));


            return rpt;
        }
        #endregion




        #region ********************************************  Battery Lead Consumption*****************************************************
        public static AppReport GenerateBatteryLeadConsumption(clsPrmInventory rptClass, ReportOptions rptOptions)
        {
            return GenerateBatteryLeadConsumption(rptClass, rptOptions, null);
        }
        public static AppReport GenerateBatteryLeadConsumption(clsPrmInventory rptClass, ReportOptions rptOptions, DBContext dc)
        {
            AppReport rpt = new AppReport();
            rpt.ReportID = ReportEnums.ReportIDEnum.Lead_Consumption_Summary;
            rpt.ReportOptions = rptOptions;
            //SetParameter(rptClass, rpt, dc);
            SetParameterElectrolyteStock(rptClass, rpt, dc);
            rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.ProductionDef.rptLead_ConsumptionDept_Report.rdlc";
            List<dcMaterialStock> rList = ProductionRBL.GetPureLeadConsumption(rptClass, dc);
            rpt.DataSources.Add(new AppReport.DataSource("Item_Stock", rList));

            return rpt;
        }
        #endregion




        #region ******************************************** Monthly Battery  Production *****************************************************
        public static AppReport GenerateMonthlyBatteryProduction(clsPrmInventory rptClass, ReportOptions rptOptions)
        {
            return GenerateMonthlyBatteryProduction(rptClass, rptOptions, null);
        }
        public static AppReport GenerateMonthlyBatteryProduction(clsPrmInventory rptClass, ReportOptions rptOptions, DBContext dc)
        {
            AppReport rpt = new AppReport();
            rpt.ReportID = ReportEnums.ReportIDEnum.Battery_Production;
            rpt.ReportOptions = rptOptions;
            SetParameterElectrolyteStock(rptClass, rpt, dc);
            rpt.ReportEmbeddedResource = @"PG.Report.ReportDef.ProductionDef.rptBatteryProduction_Report.rdlc";
            List<dcMaterialStock> rList = ProductionRBL.GetMonthlyBatteryProduction(rptClass, dc);
            rpt.DataSources.Add(new AppReport.DataSource("Item_Stock", rList));

            return rpt;
        }
        #endregion


        public static AppReport GenerateProductionReportExport(clsPrmInventory rptClass, ReportOptions rptOptions)
        {
            return GenerateProductionReportExport(rptClass, rptOptions, null);
        }
        public static AppReport GenerateProductionReportExport(clsPrmInventory rptClass, ReportOptions rptOptions, DBContext dc)
        {
            AppReport rpt = new AppReport();
            rpt.ReportID = ReportEnums.ReportIDEnum.PurchaseRequisitionReport;
            rpt.ReportOptions = rptOptions;
            rpt.ReportOptions.ReportTitle = "Production Finished Report";
            rpt.ExcelFileName = "ProductionFinishedReport";
            rpt.IsExcelExport = true;
            rpt.ExcelData = ProductionRBL.Get_Department_Production_Finished_ExcelData(rptClass, true, dc);
            return rpt;
        }

      

        public static AppReport GenerateProductionRMReportExport(clsPrmInventory rptClass, ReportOptions rptOptions)
        {
            return GenerateProductionRMReportExport(rptClass, rptOptions, null);
        }
        public static AppReport GenerateProductionRMReportExport(clsPrmInventory rptClass, ReportOptions rptOptions, DBContext dc)
        {
            AppReport rpt = new AppReport();
            rpt.ReportID = ReportEnums.ReportIDEnum.PurchaseRequisitionReport;
            rpt.ReportOptions = rptOptions;
            rpt.ReportOptions.ReportTitle = "Production RM Report";
            rpt.ExcelFileName = "ProductionRMReport";
            rpt.IsExcelExport = true;
            rpt.ExcelData = ProductionRBL.Get_Department_Production_RM_ExcelData(rptClass, true, dc);
            return rpt;
        }


    }
}
