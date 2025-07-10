<%@ Page Title="" Language="C#" EnableViewState="false" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="Production_PastingDataExport.aspx.cs" Inherits="PG.Web.Report.Production.Production_PastingDataExport" %>

<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

     <script type="text/javascript">
         ContentForm.CalendarImageURL = "../../image/calendar.png";
         var ReportViewPageLink = '<%=this.ReportViewPageLink%>';
         var ReportViewPDFPageLink = '<%=this.ReportViewPDFPageLink%>';
         var ReportPrintPageLink = '<%=this.ReportPrintPageLink%>';
         var ReportPDFPageLink = '<%=this.ReportPDFPageLink%>';

        var timeout;
        function scheduleGridUpdate(grid) {
            window.clearTimeout(timeout);
            timeout = window.setTimeout( 
                function() { grid.Refresh(); },
                2000
            );
        }
        function grid_Init(s, e) {
            scheduleGridUpdate(s);
        }
        function grid_BeginCallback(s, e) {
            window.clearTimeout(timeout);
        }
        function grid_EndCallback(s, e) {
            scheduleGridUpdate(s);
        }




        function ShowProgress() {
            $('#' + updateProgressID).show();
        }

        function UserConfirmation() {
            return confirm("Are you sure you want to Stock Issue and Print DC,GP ?");
        }


        function resizeContentInner(cntMain) {
            var contHeight = $("#dvContentMain").height();
            var contHead = $("#dvControlsHead").height();
            var contFooter = $("#dvControlsFooter").height();

            var contInnerHeight = contHeight - contHead - contFooter - 5;
            $("#dvControls").height(contInnerHeight);

            $("#dvControlsInner").height(contInnerHeight - 10);
            $("#dvGridContainer").height(contInnerHeight - 10);
            var gridHeight = $("#dvGridContainer").height();
            var gridHeaderHeight = $("#dvGridHeader").height();
            var gridFooterHeight = $("#dvGridFooter").height();
            $("#dvGrid").height(gridHeight - gridHeaderHeight - gridFooterHeight - 2);
        }


        $(document).ready(function () {
            $('#' + txtGridPageNo).keydown(function (e) {
                if (e.keyCode == 13) {
                    e.preventDefault();
                    $('#' + btnGridPageGoTo).click();
                }
            });
        });


        //function tbopen(key, userid) {
        //    key = key || '';

        //    var url = IForm.RootPath + "Inventory/NewDCGPAgainstInvoice.aspx?id=" + key;
        //    //var url = IForm.RootPath + "Accounting/GeneralLedger/JournalFull.aspx?id=" + key;


        //    if (IForm.PageMode == Enums.PageMode.InTab) {

        //        var tdata = new xtabdata();
        //        tdata.linktype = Enums.LinkType.Direct;
        //        tdata.id = 0;
        //        tdata.name = "DC and GP Against Invoice";
        //        //tdata.label = "User: " + userid;
        //        tdata.label = "DC and GP Against Invoice";
        //        tdata.type = 0;
        //        tdata.url = url;
        //        tdata.tabaction = Enums.TabAction.InNewTab;
        //        tdata.selecttab = 1;
        //        tdata.reload = 0;
        //        tdata.param = "";


        //        try {
        //            window.parent.TabMenu.OpenMenuByData(tdata);
        //        }
        //        catch (err) {
        //            alert("error in page");
        //        }
        //    }
        //    else {
        //        //on new window/tab
        //        //window.open(url,'_blank');   

        //        window.location = url;
        //    }
        //}


        function ReportPrint(key, isPDFAutoPrint) {
            var rptPageLink = ReportViewPageLink;
            if (isPDFAutoPrint) {
                //rptPageLink = ReportPDFPageLink;
                rptPageLink = ReportViewPDFPageLink;
            }

            //var url = "./Report/ReportView.aspx?rk=" + key
            var now = new Date();
            var strTime = now.getTime().toString();
            var url = ReportViewPageLink + "?rk=" + key + "&_tt=" + strTime;

            //var url = rptPageLink + "?rk=" + key;

            iframe = document.getElementById(ifPrintButton);
            if (iframe === null) {
                iframe = document.createElement('iframe');
                iframe.id = hiddenIFrameID;
                //        iframe.style.display = 'none';
                //        iframe.style = 'none';
                document.body.appendChild(iframe);
            }
            iframe.src = url;
        }


    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

       <div id="dvPageContent" style="width: 100%; height: auto;">
        <div id="dvContentHeader" class="dvContentHeader">
             <asp:HiddenField ID="hdnLoggedInUser" runat="server" />
         <asp:HiddenField ID="hdnCompanyID" runat="server" Value="0" />
            <div id="dvHeader" class="dvHeader_Prod">
                <asp:Label ID="lblHeader" CssClass="lblHeader" runat="server" Text="Pasting Production Status"></asp:Label>
            </div>
            <!--Message Div -->
            <div id="dvMsg" runat="server" class="dvMessage" style="">
                <asp:Label ID="lblMessage" runat="server" Font-Size="Small" Width="100%"> </asp:Label>
            </div>
            <div id="dvHeaderControl" class="dvHeaderControl">
            </div>
        </div>
        <div id="dvContentMain" class="dvContentMain">
            <div id="dvControlsHead" style="height: auto; width: 100%;">
                <table>
                   
                  
                    <tr>
                        <td align="right">
                            <asp:Label ID="lblDateFrom" runat="server" Text=" Date From:" Font-Bold="true"></asp:Label>
                        </td>
                        <td colspan="2">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtFromDate" runat="server" Width="80px" CssClass="textBox textDate dateParse"></asp:TextBox>
                                    </td>
                                    <td style="width: 4px;">
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="lblToDate" runat="server" Text="Date To:" Font-Bold="true"></asp:Label>
                                    </td>
                                
                                    <td >
                                        <asp:TextBox ID="txtToDate" runat="server" Width="80px" CssClass="textBox textDate dateParse"></asp:TextBox>
                                    </td>
                                </tr>
                               

                               

                            </table>
                        </td>
                  
                       
                       
                    </tr>
                     <tr>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text=" Department :" Font-Bold="true"></asp:Label>
                                    </td>
                                     <td colspan="2">
                                          <asp:DropDownList ID="ddlDEPT_ID" runat="server" CssClass="dropDownList required" Width="190px" ViewStateMode="Enabled" ></asp:DropDownList>
                                     </td>
                                     <td></td>
                                     <td></td>
                                </tr>
                

                     <tr>
                                    <td>
                                  
                                    </td>
                          <td >
                                                    <asp:Button ID="btnShow" runat="server" CssClass="buttonSearch" Style="padding-left: 22px;"
                                Text="Show Data" OnClick="btnShow_Click"  />            
                                                        </td>

                                                        
                                                             <td style="padding-right: 4px">
                                                                 &nbsp;</td>
                                                           
                                                        
                                                        
                                                        <td >

                                                           
              
             
                                                        </td>
                    </tr>
                </table>
            </div>
            <div id="dvControls" style="width: 100%;">
                <div id="dvControlsInner" class="groupBoxContainer boxShadow" style="width: 98%">
                    <div id="dvGridContainer" style="width: 100%; height: 670px; text-align: left;">
 
                        <div id="dvGrid" style="width: auto; height: 100%; overflow: auto;">
                            <dx:ASPxGridView ID="grdGridCast" ClientInstanceName="grdGridCast" runat="server" KeyFieldName="PRODUCTION_DATE"  Width="92%"  AutoGenerateColumns="False"  DataKeyNames="BOM_ID"  >
                                
             <Settings  ShowGroupPanel="True" ShowFilterBar="Visible" ShowFilterRow="True"   ShowFilterRowMenu="True" ShowGroupedColumns="True"  ShowHeaderFilterButton="True" HorizontalScrollBarMode="Visible" ShowFooter="True" ShowGroupFooter="VisibleAlways" />
            <TotalSummary>
                <dx:ASPxSummaryItem FieldName="ITEM_QTY" SummaryType="Sum" DisplayFormat="Total Qty : {0}" ValueDisplayFormat=" {0}" />
            </TotalSummary>
            <GroupSummary>
                <dx:ASPxSummaryItem FieldName="ITEM_QTY"  SummaryType="Sum" />
                <dx:ASPxSummaryItem SummaryType="Count"  />
                <dx:ASPxSummaryItem FieldName="ITEM_QTY" ShowInGroupFooterColumn="Production Qty" DisplayFormat="Sub Total : {0}" SummaryType="Sum" />
            </GroupSummary>

            <Columns>
                <dx:GridViewDataTextColumn Caption="PROD NO" FieldName="PROD_NO" VisibleIndex="0" Width="150px" FixedStyle="Left" >
                               
                                <CellStyle HorizontalAlign="Center" ></CellStyle>
                            </dx:GridViewDataTextColumn>

                
                <dx:GridViewDataTextColumn Caption="Department Name" FieldName="DEPARTMENT_NAME" VisibleIndex="1"   >
                     <CellStyle Wrap="False"></CellStyle>
                </dx:GridViewDataTextColumn>  
                <dx:GridViewDataTextColumn Caption="Shift Name" FieldName="SHIFT_NAME" VisibleIndex="2" > 
                    <CellStyle Wrap="False"></CellStyle>
                </dx:GridViewDataTextColumn>  
                <dx:GridViewDataTextColumn Caption="Batch NO" FieldName="PROD_BATCH_NO" VisibleIndex="3"  >
                        <CellStyle Wrap="False"></CellStyle>
                </dx:GridViewDataTextColumn> 
                 <dx:GridViewDataTextColumn Caption="Supervisor" FieldName="SUPERVISOR_NAME" VisibleIndex="4" >
                      <CellStyle Wrap="False"></CellStyle>
                 </dx:GridViewDataTextColumn>
                 <dx:GridViewDataTextColumn Caption="Create By" FieldName="CREATE_BY" VisibleIndex="20" >
                      <CellStyle Wrap="False"></CellStyle>
                 </dx:GridViewDataTextColumn>
                <dx:GridViewDataDateColumn Caption="Create Date" FieldName="ENTRY_DATE"   VisibleIndex="21" >
                    <PropertiesDateEdit DisplayFormatString="dd-MMM-yyyy" ></PropertiesDateEdit>
                </dx:GridViewDataDateColumn  >
                <dx:GridViewDataTextColumn Caption="Forecast Month" FieldName="FORECUSTMONTH" VisibleIndex="7"  >
                     <CellStyle Wrap="False"></CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Forecast Year" FieldName="FORECUSTYEAR" VisibleIndex="8"  >
                     <CellStyle Wrap="False"></CellStyle>
                </dx:GridViewDataTextColumn>
                 <dx:GridViewDataDateColumn Caption="Production Date"  FieldName="PRODUCTION_DATE" VisibleIndex="9" >
                      <PropertiesDateEdit DisplayFormatString="dd-MMM-yyyy" ></PropertiesDateEdit>
                 </dx:GridViewDataDateColumn >

                 <dx:GridViewDataTextColumn Caption="Item Name" FieldName="ITEM_NAME" VisibleIndex="10"  Width="200px" >
                      <CellStyle Wrap="False" BackColor="AliceBlue"></CellStyle>
                 </dx:GridViewDataTextColumn>
                 <dx:GridViewDataTextColumn Caption="Production Qty" FieldName="ITEM_QTY" VisibleIndex="11"    >
                      <CellStyle Wrap="False" BackColor="AliceBlue"></CellStyle>
                 </dx:GridViewDataTextColumn>
                 <dx:GridViewDataTextColumn Caption="Panel UOM" FieldName="PANEL_UOM_NAME" VisibleIndex="12" >
                      <CellStyle Wrap="False"></CellStyle>
                 </dx:GridViewDataTextColumn>
                 <dx:GridViewDataTextColumn Caption="Weight" FieldName="ITEM_WEIGHT" VisibleIndex="13"  >
                      <CellStyle Wrap="False"></CellStyle>
                 </dx:GridViewDataTextColumn>

                <dx:GridViewDataTextColumn Caption="Machine Name" FieldName="MACHINE_NAME" VisibleIndex="14"  >
                     <CellStyle Wrap="False"></CellStyle>
                </dx:GridViewDataTextColumn>
               <dx:GridViewDataTextColumn Caption="Grid Batch NO" FieldName="GRID_BATCH" VisibleIndex="15" >
                     <CellStyle Wrap="False"></CellStyle>
                </dx:GridViewDataTextColumn> 
                <dx:GridViewDataDateColumn Caption="Batch Start" FieldName="BATCH_STARTTIME" VisibleIndex="16"  >
                     <PropertiesDateEdit DisplayFormatString="dd-MMM-yyyy" ></PropertiesDateEdit>
                </dx:GridViewDataDateColumn> 
                 <dx:GridViewDataTextColumn Caption="Batch Start (Time)" FieldName="STARTTIME" VisibleIndex="17" >
                      <CellStyle Wrap="False"></CellStyle>
                 </dx:GridViewDataTextColumn>
                <dx:GridViewDataDateColumn Caption="Batch End" FieldName="BATCH_ENDTIME" VisibleIndex="18" > 
                  <PropertiesDateEdit DisplayFormatString="dd-MMM-yyyy" ></PropertiesDateEdit>
                </dx:GridViewDataDateColumn> 
                 <dx:GridViewDataTextColumn Caption="Batch End (Time)" FieldName="ENDTIME" VisibleIndex="19" >
                      <CellStyle Wrap="False"></CellStyle>
                 </dx:GridViewDataTextColumn>
            </Columns>
                                <SettingsBehavior AutoExpandAllGroups="True" ColumnResizeMode="Control" ConfirmDelete="True" EnableCustomizationWindow="True" />
                                <SettingsLoadingPanel Mode="ShowOnStatusBar" />
            <SettingsPager NumericButtonCount="200" PageSize="200" AlwaysShowPager="True">
                <PageSizeItemSettings Visible="true" Items="400,600,1000" />
            </SettingsPager>
     
            <Settings ShowGroupPanel="True" VerticalScrollableHeight="370" VerticalScrollBarMode="Visible" />
                                <SettingsDetail ExportMode="All" />
            <SettingsDataSecurity AllowInsert="false" AllowEdit="false" />
             <SettingsSearchPanel Visible="True"  />
                                <Styles>
                                    <Header CssClass="headerRow_Prod" Font-Bold="True" Font-Italic="False" HorizontalAlign="Center" ForeColor="Black" VerticalAlign="Middle">
                                    </Header>
                                    <AlternatingRow BackColor="#FFFFCC" Wrap="True">
                                    </AlternatingRow>
                                    <HeaderPanel CssClass="headerRow_Prod">
                                    </HeaderPanel>
                                </Styles>
        </dx:ASPxGridView>

                <dx:ASPxButton ID="btnXlsExport" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                    OnClick="btnXlsExport_Click" />
                             <dx:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grdGridCast"></dx:ASPxGridViewExporter>
                        </div>
                       
                    </div>
                </div>
            </div>
        </div>
         <div id="dvContentFooter" class="dvContentFooter">
             
        </div>
    </div>
   
</asp:Content>
