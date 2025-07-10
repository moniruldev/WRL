<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="Prod_ASM_Rejection_List.aspx.cs" ViewStateMode="Disabled"  Inherits="PG.Web.Production.Prod_ASM_Rejection_List" %>
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

            var url = IForm.RootPath + "Production/Production_Asm_Plate_Rejection_Declare.aspx?id=" + key;
            //var url = IForm.RootPath + "Accounting/GeneralLedger/JournalFull.aspx?id=" + key;


            if (IForm.PageMode == Enums.PageMode.InTab) {

                var tdata = new xtabdata();
                tdata.linktype = Enums.LinkType.Direct;
                tdata.id = 0;
                tdata.name = "Plate Reject Declaration";
                //tdata.label = "User: " + userid;
                tdata.label = "Plate Reject Declaration";
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
             height: 20px;
         }
     </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvPageContent" style="width: 100%; height: auto;">
          <div id="dvContentHeader" class="dvContentHeader">
                <div id="dvHeader" class="dvHeader_Prod">
                    <asp:Label ID="lblHeader" CssClass="lblHeader" runat="server" Text="Assembly Plate Reject Declaration List"></asp:Label>
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
                     <table style="width : 650px">
                              <tr>

                        <td align="right" class="auto-style2">
                            <asp:Label ID="Label8" runat="server" Text="Rejection Date From :" ></asp:Label>
                        </td>
                        <td class="auto-style2" colspan="2">
                            <asp:TextBox ID="txtRejectionDateFrom" runat="server" CssClass="textBox textDate dateParse" Style="text-align: left;" Width="100px" autofocus></asp:TextBox>
                        </td>
                        <td align="right" class="auto-style2">
                            <asp:Label ID="lbl" runat="server" Text="Date To :" Width="100px"></asp:Label>
                        </td>
                        <td class="auto-style2" colspan="2">
                            <asp:TextBox ID="txtRejectionDateTo" runat="server" CssClass="textBox textDate dateParse" Style="text-align: left;" Width="100px" ></asp:TextBox>
                        </td>
                    </tr>

                         <tr>
                             <td class="auto-style1" align="right">
                             <asp:Label ID="lblDEPT_ID" runat="server" Text="Department : " Width="100px"></asp:Label>
                        </td>
                        <td class="auto-style1">
                            <asp:DropDownList ID="ddlDEPT_ID" runat="server" CssClass="dropDownList required" Width="150px" ViewStateMode="Enabled" ></asp:DropDownList>

                        </td>
                             <td class="auto-style1">

                             </td>
                             <td class="auto-style1">

                             </td>
                             <td></td>
                         </tr>
                          <tr>
                             <td class="auto-style1" align="right">
                             <asp:Label ID="lblAuthorized" runat="server" Text="Is Authorized : " Width="100px"></asp:Label>
                        </td>
                        <td class="auto-style1">
                            <asp:DropDownList ID="ddlAuthorized" runat="server" CssClass="dropDownList required" Width="150px" ViewStateMode="Enabled">
                                <asp:ListItem  Text="Unauthorized" Value="U"></asp:ListItem>
                                 <asp:ListItem  Text="Authorized" Value="A"></asp:ListItem>
                                 <asp:ListItem  Selected="True" Text="All" Value="0"></asp:ListItem>
                            </asp:DropDownList>

                        </td>
                             <td class="auto-style1">

                             </td>
                             <td class="auto-style1">

                             </td>
                             <td></td>
                         </tr>

                    <tr>
                        <td></td>
                            <td>
                                <%--<asp:Button ID="btnUpload" CssClass="buttonSearch" runat="server" OnClick="btnUpload_Click" Text="Show Data" />--%>
                                 <asp:Button ID="Button1" runat="server" CssClass="buttonSearch" Style="padding-left: 22px;"
                                Text="Show Data" OnClick="btnUpload_Click"  /> 
                                  <asp:HiddenField ID="hdnDeptID" runat="server" Value ="136" />
                            </td>
                        <td>
                            
                            
                        </td>

                        <td> &nbsp;</td>
                        <td> <asp:HiddenField ID="hdnLoggedInUser" runat="server" Value ="0" /></td>
                    </tr>
                         </table>
                    </div>


             <div id="dvControls" style="width: 100%; height : 600px;">
                <div id="dvControlsInner" class="groupBoxContainer boxShadow" style="width: 1000px; height : auto;">
                 
                        <div id="dvGrid" style="width: 100%; height: auto; overflow: auto;">
                            <dx:ASPxGridView ID="grdRejectionList" runat="server" AutoGenerateColumns="False" Width="98%"  ClientInstanceName="grdPastingList" >
                                <Columns>
                                      <dx:GridViewDataTextColumn Caption="Action" UnboundType="String"  Width="50px" VisibleIndex="0">
                                        <DataItemTemplate>
                                            <dx:ASPxHyperLink ID="hyperLink"  runat="server" OnInit="hyperLink_Init">
                                            </dx:ASPxHyperLink>
                                        </DataItemTemplate>
                                    </dx:GridViewDataTextColumn>
                                   
                                    <dx:GridViewDataTextColumn Caption="Rejection No" Name="lblRejection_NO" Width="80px" VisibleIndex="2" FieldName="PROD_ASM_REJ_NO">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Dept. ID" Name="lblDEPT_ID" VisibleIndex="4" Visible="false" FieldName="DEPT_ID">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Rejection Reason" Name="lblRejectionReason" VisibleIndex="5" Width="100px" FieldName="REJECTION_ASM_REASON">
                                    </dx:GridViewDataTextColumn>
                                    
                                   
                                    <dx:GridViewDataTextColumn Caption="PROD_REJECTION_ID" FieldName="PROD_ASM_REJ_ID" Name="hdnRejection_ID" Visible="false" VisibleIndex="1">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataDateColumn Caption="Rejection Date" FieldName="PROD_ASM_REJ_DATE" Width="100px"   Name="lblRejection_DATE" VisibleIndex="3">
                                         <PropertiesDateEdit DisplayFormatString="dd-MMM-yyyy" ></PropertiesDateEdit>
                                    </dx:GridViewDataDateColumn>
                                   
                                  
                                   <dx:GridViewDataTextColumn Caption="Auth Status" FieldName="AUTHO_STATUS" Width="70px" Name="txtAUTH_STATUS" VisibleIndex="8">
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
