
<%@ Page Title="" Language="C#" EnableViewState="false"  MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="ITCAGAINSTIGRFROMFLOOR.aspx.cs" Inherits="PG.Web.Report.INV.ITCAGAINSTIGRFROMFLOOR" %>


<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

     <script type="text/javascript">
         ContentForm.CalendarImageURL = "../../image/calendar.png";
        

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
            <div id="dvHeader" class="dvHeader">
                <asp:Label ID="lblHeader" CssClass="lblHeader" runat="server" Text="Date wise ITC List"></asp:Label>
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
                            <asp:Label ID="lblDateFrom" runat="server" Text="ITC Date From:" Font-Bold="true"></asp:Label>
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
                                    <td align="right">
                                   <asp:Label ID="lblisActive" runat="server" Text="IsActive:" Font-Bold="true" Visible="false"></asp:Label>
                                    </td>
                                      <td >
                                            <asp:DropDownList ID="ddlIsActive" runat="server"  Visible="false">
                                                <asp:ListItem Text="All" Value="0"></asp:ListItem>
                                                <asp:ListItem Selected="True" Text="Active" Value="Y"></asp:ListItem>
                                                <asp:ListItem Text="InActive" Value="N"></asp:ListItem>
                                            </asp:DropDownList>               
                                    </td>

                                                        <td >

                                                           
                                                        </td>
                                                        
                                                        <td >

                                                           
               
             
                                                        </td>
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
                <div id="dvControlsInner" class="groupBoxContainer boxShadow" style="width: 1400px">
                    <div id="dvGridContainer" style="width: 100%; height: auto; text-align: left;">
                      
                        <div id="dvGrid" style="width: 100%; height: 500px; overflow: auto;">
                            <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="ASPxGridView1" runat="server"  Width="100%" AutoGenerateColumns="False" KeyFieldName="REQ_ISSUE_NO" >
         <%-- <ClientSideEvents Init="grid_Init" BeginCallback="grid_BeginCallback" EndCallback="grid_EndCallback" />--%>

            <%-- <Settings ShowFilterRow="True" />--%>
             <%--  <SettingsSearchPanel Visible="True" />--%>
           <%-- <Settings ShowFilterRow="True" ShowFilterRowMenu="true" ShowGroupPanel="True" ShowFooter="True" />--%>
           <%-- <ClientSideEvents Init="grid_Init" BeginCallback="grid_BeginCallback" EndCallback="grid_EndCallback" />--%>

            <Settings VerticalScrollableHeight="250" ShowFooter="True" ShowGroupFooter="VisibleAlways" />

            <TotalSummary>
                <dx:ASPxSummaryItem FieldName="Size" SummaryType="Sum" />
                <dx:ASPxSummaryItem FieldName="ISSUE_QNTY" ShowInColumn="ITC Qty" ShowInGroupFooterColumn="ITC Qty" SummaryType="Sum" Tag="ITC Qty" />
                 <dx:ASPxSummaryItem FieldName="RCV_QNTY" ShowInColumn="IRR Qty" ShowInGroupFooterColumn="IRR Qty" SummaryType="Sum" Tag="ITC Qty" />
            </TotalSummary>
           
             <GroupSummary>
                  <dx:ASPxSummaryItem SummaryType="Count"  />
            
            <dx:ASPxSummaryItem FieldName="ISSUE_QNTY" ShowInGroupFooterColumn="ITC Qty" SummaryType="Sum" />
            <dx:ASPxSummaryItem FieldName="RCV_QNTY" ShowInGroupFooterColumn="IRR Qty" SummaryType="Sum" />
        </GroupSummary>

           


            <Columns>
                <%--<dx:GridViewDataTextColumn Caption="BOM Name" FieldName="BOM_ITEM_DESC" VisibleIndex="0" Width="150px">
                                <DataItemTemplate>
                                    <asp:LinkButton ID="lnkBillOfMaterial" runat="server" CommandArgs="BM" OnClick="lnkBillOfMaterial_Click" Text='<%# Bind("BOM_ITEM_DESC") %>'></asp:LinkButton>
                                </DataItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <CellStyle HorizontalAlign="Center"></CellStyle>
                            </dx:GridViewDataTextColumn>--%>

                 <%-- <dx:GridViewDataTextColumn FieldName="BOM_ID">
                                <DataItemTemplate>
                                    <dx:ASPxLabel ID="lblBom_ID" 
                                    <asp:LinkButton ID="lnkBillOfMaterial" runat="server" CommandArgs="BM" OnClick="lnkBillOfMaterial_Click" Text='<%# Bind("BOM_ITEM_DESC") %>'></asp:LinkButton>
                                </DataItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <CellStyle HorizontalAlign="Center"></CellStyle>
                            </dx:GridViewDataTextColumn>--%>
                <dx:GridViewDataTextColumn Caption="Group Name" FieldName="ITEM_GROUP_NAME" VisibleIndex="1" Width="100px"  >
                    <%--<Settings AllowDragDrop="True" ShowFilterRowMenu="True" />--%>
                    <Settings AllowGroup="True" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Item Name" FieldName="ITEM_NAME" VisibleIndex="2" Width="150px" >
                   <%-- <Settings AllowGroup="True" />--%>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="ITC Date" FieldName="REQ_ISSUE_DATE" VisibleIndex="3" Width="100px" >
              
                       <DataItemTemplate>
                            <%#Eval("REQ_ISSUE_DATE", "{0:dd-MMM-yyyy}")%>
                        </DataItemTemplate>
                   </dx:GridViewDataTextColumn>
                 <dx:GridViewDataTextColumn Caption="ITC No" FieldName="REQ_ISSUE_NO" VisibleIndex="4" Width="180px" />
                 <dx:GridViewDataTextColumn Caption="Req No" FieldName="REQ_NO" VisibleIndex="5" Width="180px" />
                <dx:GridViewDataTextColumn Caption="Req Date" FieldName="REQ_DATE" VisibleIndex="6" Width="100px" >
                  <DataItemTemplate>
                            <%#Eval("REQ_DATE", "{0:dd-MMM-yyyy}")%>
                        </DataItemTemplate>
                   </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn  Caption="From Dept" FieldName="FromDept" VisibleIndex="7" Width="150px" />
                 <dx:GridViewDataTextColumn  Caption="To Dept" FieldName="TODept" VisibleIndex="8" Width="150px" />

                 <dx:GridViewDataTextColumn  Caption="ITC Qty" FieldName="ISSUE_QNTY" VisibleIndex="9" Width="80px" >
                    <%-- <PropertiesTextEdit DisplayFormatString="c" />--%>
            </dx:GridViewDataTextColumn>
                 <dx:GridViewDataTextColumn  Caption="IRR Qty" FieldName="RCV_QNTY" VisibleIndex="10" Width="80px" />

                 <dx:GridViewDataTextColumn  Caption="Uom" FieldName="UOM_NAME" VisibleIndex="11" Width="80px" />

               <%-- <dx:GridViewDataTextColumn FieldName="Total" VisibleIndex="12" UnboundType="Decimal">
                <PropertiesTextEdit DisplayFormatString="c" />
            </dx:GridViewDataTextColumn>--%>

               <%-- <dx:GridViewDataTextColumn FieldName="Size" VisibleIndex="4" Width="80px">
                    <Settings AllowAutoFilter="false" />Caption="BOM_ITEM_DESC"
                </dx:GridViewDataTextColumn>--%>
                <%--<dx:GridViewCommandColumn ShowClearFilterButton="true" ShowApplyFilterButton="true" VisibleIndex="4" />--%>
            </Columns>
                                <SettingsBehavior AllowFixedGroups="True" AutoExpandAllGroups="True" SortMode="Value" />
            <SettingsPager NumericButtonCount="20">
                <PageSizeItemSettings Visible="true" Items="50,100" />
            </SettingsPager>

                          <Settings ShowGroupPanel="true" ShowFilterBar="Visible" ShowFilterRow="True"  ShowFilterRowMenu="True" ShowGroupedColumns="True"  ShowHeaderFilterButton="True" ShowGroupButtons="True" />
           
            <SettingsDataSecurity AllowInsert="false" AllowEdit="false" AllowDelete="false" />
                                <Styles>
                                    <Header BackColor="#0033CC" ForeColor="White">
                                    </Header>
                                    <AlternatingRow BackColor="#FFFFCC">
                                    </AlternatingRow>
                                    <GroupFooter BackColor="#CCCCFF">
                                    </GroupFooter>
                                    <GroupPanel BackColor="#9999FF">
                                    </GroupPanel>
                                </Styles>
        </dx:ASPxGridView>

                <dx:ASPxButton ID="btnXlsExport" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                    OnClick="btnXlsExport_Click" />

                             <dx:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="ASPxGridView1"></dx:ASPxGridViewExporter>
                        </div>
                       
                    </div>
                </div>
            </div>
        </div>
         <div id="dvContentFooter" class="dvContentFooter">
            <table>
                <tr>
                    <td></td>
                     <td>
                                <asp:DropDownList ID="ddlReportViewType" runat="server" CssClass="dropDownList">
                                        <asp:ListItem Value="0">Screen</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="1">PDF</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                     <td>
                                    <asp:DropDownList ID="ddlReportViewMode" runat="server" CssClass="dropDownList">
                                        <asp:ListItem Value="0">In This Tab</asp:ListItem>
                                        <asp:ListItem Value="1">In New Tab</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="2">In New Window</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                    <td>
                        <asp:Button ID="btnAddNew" runat="server" Text="New" CssClass="buttonNew" Visible="false"/>
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonCancel checkIsDirty"  Visible="false" />
                    </td>
                    <td>
                        <asp:Button ID="btnSave" runat="server" Text="Generate DC,GP" CssClass="buttonSave checkRequired" AccessKey="s" OnClientClick="if ( ! UserDeleteConfirmation()) return false;" Width="100px" Visible="false"   />
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="buttonEdit" Visible="false" />
                    </td>

                   

                    <td>
                        <asp:Button ID="btnPost" runat="server" Text="Post" CssClass="buttoncommon" Visible="false" />
                    </td>
                    <td>
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="buttonDelete" Visible="false" />
                    </td>
                    <td>
                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" CssClass="buttonRefresh checkIsDirty" Visible="false" />
                    </td>
                    <td>
                        <%--<uc:PrintButton runat="server" ID="ucPrintButton" DefaultPrintAction="Preview" AutoPrint="False" />--%>
                    </td>
                    <td>
                        <input id="btnClose" type="button" runat="server" class="buttonClose" value="Close" onclick="if (ContentForm) { ContentForm.CloseForm(); }"  />
                    </td>
                    <td></td>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="Print Format:"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlReportFormat" runat="server" CssClass="dropDownList" Width="100px">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="btnPopupTrigger" runat="server" Text="Button" CssClass="buttonHidden" />
                        <asp:HiddenField ID="hdnPopupTriggerID" runat="server" Value="" />
                        <asp:HiddenField ID="hdnPopupCommand" runat="server" Value="" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
   
</asp:Content>
