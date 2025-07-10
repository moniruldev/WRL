<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="ProductionMcFGList.aspx.cs" ViewStateMode="Disabled" Inherits="PG.Web.Production.ProductionMcFGList" %>
<%@ Register assembly="DevExpress.Web.v14.2, Version=14.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script src="../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
    <script src="../javascript/jquery.attributeobserver.js" type="text/javascript"></script>
    <link href="../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />
  
    <script language="javascript" type="text/javascript">
        var isPageResize = true;
        ContentForm.CalendarImageURL = "../image/calendar.png";
        //var chkIS_PACKING = '<%=chkIS_PACKING.ClientID%>';
        function ShowProgress() {
            $('#' + updateProgressID).show();
        }
        function PageResizeCompleted(pg, cntMain) {
            resizeContentInner(cntMain);
        }

        function resizeContentInner(cntMain) {
            var contHeight = $("#dvContentMainInner").height();

            var topHeight = $("#dvTop").height();

            var middleHeight = contHeight - topHeight;

            $("#dvMiddle").height(middleHeight);
            $("#tblMiddle").height(middleHeight);

            $("#dvReportList").height(middleHeight);
            $("#dvParam").height(middleHeight);

        }

        $(document).ready(function () {
            $('#' + txtGridPageNo).keydown(function (e) {
                if (e.keyCode == 13) {
                    e.preventDefault();
                    $('#' + btnGridPageGoTo).click();
                }
            });
        });


        function tbopen(key, userid) {
            key = key || '';

            var url = "";

            
           
          <%--  var IS_PACKING = document.getElementById("<%=chkIS_PACKING.ClientID%>");
            var IS_P_BATTERY = document.getElementById("<%=chkIS_PBattery.ClientID%>");
            
            if (IS_PACKING.checked)
                url = IForm.RootPath + "Production/ProductionPackingEntry_ItemWise.aspx?id=" + key;
            else if (IS_P_BATTERY.checked)
                url = IForm.RootPath + "Production/ProductionAssamblyEntryWP_P_BAT.aspx?id=" + key;
                else--%>
            url = IForm.RootPath + "Production/ProductionMcPackingEntry.aspx?id=" + key;
            if (IForm.PageMode == Enums.PageMode.InTab) {

                var tdata = new xtabdata();
                tdata.linktype = Enums.LinkType.Direct;
                tdata.id = 0;
             <%--   var IS_PACKINGs = document.getElementById("<%=chkIS_PACKING.ClientID%>");
                if (IS_PACKINGs.checked)
                {
                    tdata.name = "Production  Packing Entry";
                    tdata.label = "Production  Packing Entry";
                }
                else--%>
                {
                    tdata.name = "Production Assembly FG  Entry";
                    tdata.label = "Production Assembly FG Entry";
                }
               
                tdata.type = 0;
                tdata.url = url;
                tdata.tabaction = Enums.TabAction.InNewTab;
                tdata.selecttab = 1;
                tdata.reload = 0;
                tdata.param = "";


                try {
                    window.parent.TabMenu.OpenMenuByData(tdata);
                }
                catch (err) {
                    alert("error in page");
                }
            }
            else {
                //on new window/tab
                //window.open(url,'_blank');   

                window.location = url;
            }
        }
   </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvPageContent" style="width: 100%; height: auto;">
          <div id="dvContentHeader" class="dvContentHeader">
                <div id="dvHeader" class="dvHeader_Prod">
                    <asp:Label ID="lblHeader" CssClass="lblHeader" runat="server" Text="Production MC List"></asp:Label>
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
                     <table style="width : 600px">
                         <tr>

                        <td align="right" class="auto-style2">
                            <asp:Label ID="Label1" runat="server" Text="From Date :" Width="100px"></asp:Label>
                        </td>
                        <td class="auto-style2"  >
                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="textBox textDate dateParse" Style="text-align: left;" Width="100px" autofocus></asp:TextBox>
                        </td>
                        <td align="right">
                             <asp:Label ID="Label2" runat="server" Text="To Date :" Width="70px"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtToDate" runat="server" CssClass="textBox textDate dateParse" Style="text-align: left;" Width="100px" ></asp:TextBox></td>
                                  <td align="right">
                                        <asp:Label ID="lblAutho" runat="server" Text="Status :"></asp:Label>
                                  </td>
                                  <td>
                                      <asp:DropDownList ID="ddlAUTHO_STATUS" runat="server"  CssClass="dropDownList enableIsDirty">
                                                <asp:ListItem Selected="True" Value="">-All-</asp:ListItem>
                                                <asp:ListItem Value="Y">Authorized</asp:ListItem>
                                                <asp:ListItem Value="N">Un-Authorized</asp:ListItem>
                                            </asp:DropDownList>
                                  </td>
                    </tr>
                              <tr>

                        <td align="right" class="auto-style2">
                            <asp:Label ID="lblDEPT_ID" runat="server" Text="Department : " Width="100px"></asp:Label>
                        </td>
                        <td class="auto-style2" colspan="2">
                            <asp:DropDownList ID="ddlDEPT_ID" runat="server" CssClass="dropDownList required" Width="150px" Enabled="False" ViewStateMode="Enabled"></asp:DropDownList>
                           <%-- <asp:TextBox ID="txtProductionDate" runat="server" CssClass="textBox textDate dateParse" Style="text-align: left;" Width="100px" autofocus></asp:TextBox>--%>
                        </td>
                        <td>
                             
                        </td>
                        <td>
                            

                        </td>
                                  <td>

                                  </td>
                    </tr>

                    <tr>
                        <td></td>
                            <td>
                                <%--<asp:Button ID="btnUpload" CssClass="buttonSearch" runat="server" OnClick="btnUpload_Click" Text="Load Data" />--%>

                                <asp:Button ID="btnUpload" runat="server" CssClass="buttonSearch" Style="padding-left: 22px;"
                                Text="Show Data" OnClick="btnUpload_Click"  /> 
                                  <asp:HiddenField ID="hdnCompanyID" runat="server" Value ="0" />
                            </td>
                        <td>
                             <asp:CheckBox ID="chkIS_PBattery" CssClass="checkbox" runat ="server" Text="P_Battery" Checked="false" Visible="false" />  
                        </td>
                        <td> 
                            <asp:CheckBox ID="chkIS_PACKING" CssClass="checkbox" runat ="server" Text="Packing" Checked="true" Visible="true" />
                        </td>
                        <td> <asp:HiddenField ID="hdnLoggedInUser" runat="server" Value ="0" /></td>
                        <td></td>
                    </tr>
                         </table>
                    </div>


             <div id="dvControls" style="width: 100%; height : auto;">
                <div id="dvControlsInner" class="groupBoxContainer boxShadow" style="width: 1000px; height : auto;">
                 
                        <div id="dvGrid" style="width: 100%; height: auto; overflow: auto;">
                            <dx:ASPxGridView ID="grdAssemblyList" runat="server" AutoGenerateColumns="False" Width="98%"  ClientInstanceName="grdAssemblyList" >
                                <Columns>
                                      <dx:GridViewDataTextColumn Caption="Action" UnboundType="String"  Width="50px" VisibleIndex="0">
                                        <DataItemTemplate>
                                            <dx:ASPxHyperLink ID="hyperLink"  runat="server" OnInit="hyperLink_Init">
                                            </dx:ASPxHyperLink>
                                        </DataItemTemplate>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Production NO" Name="lblPROD_NO" VisibleIndex="2"  Width="150px" FieldName="PROD_NO">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Entry By" Name="lblEDIT_BY_ID" Width="80px" VisibleIndex="4" FieldName="ENTRY_BY">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Dept. ID" Name="lblDEPT_ID" VisibleIndex="5" Visible="false" FieldName="DEPT_ID">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Shift Name" Name="lblSHIFT_NAME" VisibleIndex="6" Width="100px" FieldName="SHIFT_NAME">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Supervisor" Name="lblSUPERVISOR_NAME" VisibleIndex="7" Width="200px" FieldName="FULL_NAME">
                                    </dx:GridViewDataTextColumn>
                                   
                                    <dx:GridViewDataTextColumn Caption="PROD_ID" FieldName="PROD_ID" Name="hdnPROD_ID" Visible="false" VisibleIndex="1">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataDateColumn Caption="Production Date" FieldName="PRODUCTION_DATE" Width="100px"   Name="lblPRODUCTION_DATE" VisibleIndex="3">
                                         <PropertiesDateEdit DisplayFormatString="dd-MMM-yyyy" ></PropertiesDateEdit>
                                    </dx:GridViewDataDateColumn>
                                   
                                      <dx:GridViewDataTextColumn Caption="Prod. Batch NO" FieldName="PROD_BATCH_NO" Width="70px" Name="txtPROD_BATCH_NO" VisibleIndex="8">
                                      </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Auth Status" FieldName="AUTH_STATUS" Width="70px" Name="txtAUTH_STATUS" VisibleIndex="8">
                                      </dx:GridViewDataTextColumn>
                                </Columns>
                                <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                                <SettingsSearchPanel Visible="True"  />
                                <SettingsPager AlwaysShowPager="True" PageSize="20">
                                </SettingsPager>
                                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="400"    />
                                <Styles>
                                    <Header CssClass="headerRow_Prod" HorizontalAlign="Center" Font-Bold="True" ForeColor="White" Font-Size="9pt">
                                         
                                    </Header>
                                    <AlternatingRow BackColor="#FFFFCC">
                                    </AlternatingRow>
                                </Styles>
                            </dx:ASPxGridView>


                       
                        </div>
                        <%--    <div id="dvGridFooter" style="width: 100%; height: 25px; font-size: smaller;" class="subFooter">
                            <table style="height: 100%; width: 100%; font-weight: bold;" cellspacing="2" cellpadding="1"
                                rules="all">
                                <tr>
                                    <td align="left" style="width: 40%">
                                        <table>
                                            <tr>
                                                <td style="width: 2px;">
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblTotal" runat="server" Text="Rows: 0 of 0"></asp:Label>
                                                    <asp:HiddenField ID="hdnRowCount" runat="server" Value="0" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td align="right" style="width: 60%">
                                        <div id="dvGridPager" class="dvGridPager">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="btnGridPageGoTo" runat="server" Text="Go"  />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label2" runat="server" Text="Page Size:"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlGridPageSize" runat="server" CssClass="dropDownList" Width="50"
                                                            Height="18" AutoPostBack="True" >
                                                            <asp:ListItem Value="10">10</asp:ListItem>
                                                            <asp:ListItem Value="20">20</asp:ListItem>
                                                            <asp:ListItem Value="30">30</asp:ListItem>
                                                            <asp:ListItem Value="50" Selected="True">50</asp:ListItem>
                                                            <asp:ListItem Value="100">100</asp:ListItem>
                                                            <asp:ListItem Value="200">200</asp:ListItem>
                                                            <asp:ListItem Value="0">all</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label4" runat="server" Text="Page:"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtGridPageNo" runat="server" CssClass="textBox" Width="30" Height="14"
                                                            Style="text-align: center;">0</asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblGridPageInfo" runat="server" Text=" of 0"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnGridPageFirst" runat="server" Text="" CssClass="btnGridPageFirst"
                                                             ToolTip="First" />
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnGridPagePrev" runat="server" Text="" CssClass="btnGridPagePrev"
                                                            ToolTip="Previous" />
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnGridPageNext" runat="server" Text="" CssClass="btnGridPageNext"
                                                            ToolTip="Next" />
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnGridPageLast" runat="server" Text="" CssClass="btnGridPageLast"
                                                            ToolTip="Last" />
                                                    </td>
                                                    <td style="width: 2px;">
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    --%>

                </div>
            </div>
             </div>
        </div>
</asp:Content>
