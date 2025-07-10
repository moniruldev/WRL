<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="Solar_Openning_List.aspx.cs" ViewStateMode="Disabled" Inherits="PG.Web.Production.Solar_Openning_List" %>
<%@ Register assembly="DevExpress.Web.v14.2, Version=14.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script src="../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
    <script src="../javascript/jquery.attributeobserver.js" type="text/javascript"></script>
    <link href="../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />
  
    <script language="javascript" type="text/javascript">
        var isPageResize = true;
        ContentForm.CalendarImageURL = "../image/calendar.png";
       <%-- var chkIS_PACKING = '<%=chkIS_PACKING.ClientID%>';--%>
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

            var url = IForm.RootPath + "Production/Solar_Openning_Entry.aspx?id=" + key;
            //var url = IForm.RootPath + "Accounting/GeneralLedger/JournalFull.aspx?id=" + key;


            if (IForm.PageMode == Enums.PageMode.InTab) {

                var tdata = new xtabdata();
                tdata.linktype = Enums.LinkType.Direct;
                tdata.id = 0;
                tdata.name = "Solar Openning Entry";
                //tdata.label = "User: " + userid;
                tdata.label = "Solar Openning Entry";
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

     <style type="text/css">
         .auto-style1 {
             width: 52px;
         }
         .auto-style2 {
             width: 107px;
         }
         .auto-style3 {
             width: 120px;
         }
     </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvPageContent" style="width: 100%; height: auto;">
          <div id="dvContentHeader" class="dvContentHeader">
                <div id="dvHeader" class="dvHeader_Prod">
                    <asp:Label ID="lblHeader" CssClass="lblHeader" runat="server" Text="Solar Openning List"></asp:Label>
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
                            <asp:Label ID="Label1" runat="server" Text="Openning Date :" Width="100px"></asp:Label>
                        </td>
                        <td width="200px"  >
                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="textBox textDate dateParse" Style="text-align: left;" Width="100px"></asp:TextBox>
                        </td>
                        <td align="right">
                             
                        <td width="200px">
                             </td>
                                  <td align="right" style="text-align:right;">
                                        
                                  </td>
                                  <td class="auto-style3">
                                    
                                  </td>
                    </tr>
                              <tr>

                        <td align="right" class="auto-style2">
                            <asp:Label ID="lblDEPT_ID" runat="server" Text="Department : " Width="100px"></asp:Label>
                        </td>
                        <td class="auto-style2">
                            <asp:DropDownList ID="ddlDEPT_ID" runat="server" CssClass="dropDownList required" Width="200px" Enabled="False" ViewStateMode="Enabled"></asp:DropDownList>
                           <%-- <asp:TextBox ID="txtProductionDate" runat="server" CssClass="textBox textDate dateParse" Style="text-align: left;" Width="100px" autofocus></asp:TextBox>--%>
                        </td>
                        <td style="text-align:right;">
                             <asp:Label ID="lblAutho" runat="server" Text="Status :"></asp:Label>
                        </td>
                        <td class="auto-style1">
                         <asp:DropDownList ID="ddlAUTHO_STATUS" runat="server"  CssClass="dropDownList enableIsDirty">
                          <asp:ListItem Selected="True" Value="">-All-</asp:ListItem>
                          <asp:ListItem Value="Y">Authorized</asp:ListItem>
                          <asp:ListItem Value="N">Un-Authorized</asp:ListItem>
                         </asp:DropDownList>
                         </td>
                         <td class="auto-style3">

                        </td>
                    </tr>

                    <tr>
                        <td></td>
                            <td>

                                <asp:Button ID="btnUpload" runat="server" CssClass="buttonSearch" Style="padding-left: 22px;"
                                Text="Show Data" OnClick="btnUpload_Click"  /> 
                                  <asp:HiddenField ID="hdnCompanyID" runat="server" Value ="0" />
                            </td>
                      
                        <td> &nbsp;</td>
                        <td> 
                          <asp:HiddenField ID="hdnLoggedInUser" runat="server" Value ="0" />  

                        </td>
                    </tr>
                         </table>
                    </div>


             <div id="dvControls" style="width: 100%; height : auto;">
                <div id="dvControlsInner" class="groupBoxContainer boxShadow" style="width: 1000px; height : auto;">
                 
                        <div id="dvGrid" style="width: 100%; height: auto; overflow: auto;">
                            <dx:ASPxGridView ID="grdPureLeadList" runat="server" AutoGenerateColumns="False" Width="98%"  ClientInstanceName="grdPureLeadList" >
                                <Columns>
                                      <dx:GridViewDataTextColumn Caption="Action" UnboundType="String"  Width="50px" VisibleIndex="0">
                                        <DataItemTemplate>
                                            <dx:ASPxHyperLink ID="hyperLink"  runat="server" OnInit="hyperLink_Init">
                                            </dx:ASPxHyperLink>
                                        </DataItemTemplate>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="MONTH" Name="lblMONTH" VisibleIndex="2"  Width="150px" FieldName="MONTH">
                                    </dx:GridViewDataTextColumn>
                                     <dx:GridViewDataTextColumn Caption="YEAR" Name="lblYEAR" VisibleIndex="2"  Width="150px" FieldName="YEAR">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Entry By" Name="lblENTRY_BY_ID" Width="80px" VisibleIndex="4" FieldName="ENTRY_ID">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Dept. ID" Name="lblDEPT_ID" VisibleIndex="5" Visible="false" FieldName="DEPT_ID">
                                    </dx:GridViewDataTextColumn>
                               <%--  <dx:GridViewDataTextColumn Caption="Shift Name" Name="lblSHIFT_NAME" VisibleIndex="6" Width="100px" Visible="false"  FieldName="SHIFT_NAME">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Supervisor" Name="lblSUPERVISOR_NAME" VisibleIndex="7" Width="200px" Visible="false" FieldName="FULL_NAME">
                                    </dx:GridViewDataTextColumn>--%>
                                   
                                    <dx:GridViewDataTextColumn Caption="OPEN_MST_ID" FieldName="OPEN_MST_ID" Name="hdnOPEN_MST_ID" Visible="false" VisibleIndex="1">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataDateColumn Caption="Openning Date" FieldName="OPENNING_DATE" Width="100px"   Name="lblPRODUCTION_DATE" VisibleIndex="3">
                                         <PropertiesDateEdit DisplayFormatString="dd-MMM-yyyy" ></PropertiesDateEdit>
                                    </dx:GridViewDataDateColumn>
                                   
                                      <%--<dx:GridViewDataTextColumn Caption="Prod. Batch NO" FieldName="PROD_BATCH_NO" Width="70px" Name="txtPROD_BATCH_NO" VisibleIndex="8">
                                      </dx:GridViewDataTextColumn>--%>
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
                </div>
            </div>
             </div>
        </div>
</asp:Content>
