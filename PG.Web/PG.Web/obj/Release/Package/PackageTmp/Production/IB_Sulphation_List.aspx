<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="IB_Sulphation_List.aspx.cs" Inherits="PG.Web.Production.IB_Sulphation_List" ViewStateMode="Disabled" %>
<%@ Register assembly="DevExpress.Web.v14.2, Version=14.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <script src="../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
    <script src="../javascript/jquery.attributeobserver.js" type="text/javascript"></script>
    <link href="../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />
  
    <script language="javascript" type="text/javascript">
        var isPageResize = true;
        ContentForm.CalendarImageURL = "../image/calendar.png";

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

            var url = IForm.RootPath + "Production/IB_SulphationEntry.aspx?id=" + key;
            //var url = IForm.RootPath + "Accounting/GeneralLedger/JournalFull.aspx?id=" + key;
            if (IForm.PageMode == Enums.PageMode.InTab) {

                var tdata = new xtabdata();
                tdata.linktype = Enums.LinkType.Direct;
                tdata.id = 0;
                tdata.name = "IB Sulphation Entry";
                //tdata.label = "User: " + userid;
                tdata.label = "IB Sulphation Entry";
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
                window.location = url;
            }
        }
   </script>

        
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <div id="dvPageContent" style="width: 100%; height: auto;">
          <div id="dvContentHeader" class="dvContentHeader">
                <div id="dvHeader" class="dvHeader_Prod">
                    <asp:Label ID="lblHeader" CssClass="lblHeader" runat="server" Text="IB Sulphation List"></asp:Label>
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
                            <asp:Label ID="Label8" runat="server" Text="Production Date :" Width="100px"></asp:Label>
                        </td>
                        <td class="auto-style2" colspan="2">
                            <asp:TextBox ID="txtProductionDate" runat="server" CssClass="textBox textDate dateParse" Style="text-align: left;" Width="100px" autofocus></asp:TextBox>
                            <%--<asp:RegularExpressionValidator runat="server" ControlToValidate="txtTranDate" ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$" ErrorMessage="Invalid date format." ValidationGroup="Group1" ForeColor="Red" />--%>
                        </td>
                        <td>
                             <asp:Label ID="lblDEPT_ID" runat="server" Text="Department : " Width="100px"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlDEPT_ID" runat="server" CssClass="dropDownList required" Width="150px" ViewStateMode="Enabled" Enabled="False"></asp:DropDownList>

                        </td>
                                  <td>

                                 <asp:Button ID="btnUpload" runat="server" CssClass="buttonSearch" Style="padding-left: 22px;"
                                Text="Show Data" OnClick="btnUpload_Click"  /> 

                                  </td>
                    </tr>

                    <tr>
                       <td >
                                
                              </td>
                            <td style="background-color:red">
                                <%--<asp:Button ID="btnUpload" CssClass="buttonSearch" runat="server" OnClick="btnUpload_Click" Text="Show Data" />--%>
                                  <asp:HiddenField ID="hdnCompanyID" runat="server" Value ="0" />
                                  <asp:CheckBox ID="chkISSULPHATION" runat="server" Checked="true" ForeColor="White" Enabled="false" Text="Sulphation" />
                            </td>
                        <td>
                            
                            
                        </td>

                        <td> &nbsp;</td>
                        <td> <asp:HiddenField ID="hdnLoggedInUser" runat="server" Value ="0" /></td>
                    </tr>
                         </table>
                    </div>


             <div id="dvControls" style="width: 100%;">
                <div id="dvControlsInner" class="groupBoxContainer boxShadow" style="width: 1000px">
                    <dx:aspxgridview ID="grdCastingList" runat="server" AutoGenerateColumns="False" Width="98%"  ClientInstanceName="grdPastingList" >
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
                                    <dx:GridViewDataDateColumn Caption="Pro. Date" FieldName="PRODUCTION_DATE" Width="100px"   Name="lblPRODUCTION_DATE" VisibleIndex="3">
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
                            </dx:aspxgridview>
                </div>
            </div>
             </div>
        </div>
</asp:Content>
