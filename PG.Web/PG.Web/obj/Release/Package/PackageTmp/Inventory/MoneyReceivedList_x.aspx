<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="MoneyReceivedList_x.aspx.cs" EnableViewState="false"  Inherits="PG.Web.Inventory.MoneyReceivedList_x" %>
 


<%@ Register assembly="DevExpress.Web.v14.2, Version=14.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <script src="../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
    <script src="../javascript/jquery.attributeobserver.js" type="text/javascript"></script>
    <link href="../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />

     <script language="javascript" type="text/javascript">



         var CustomerListServiceLink = '<%=this.CustomerListServiceLink%>';
        

         var txtCustCode = '<%=txtCustCode.ClientID%>';
         var btnCustID = '<%=btnCustID.ClientID%>';
         var hdnCustId = '<%=hdnCustId.ClientID%>';
         var txtCustomerName = '<%=txtCustomerName.ClientID%>';





         function tbopen(key, userid) {
             key = key || '';
             var url = "";
         
             
             //var url = IForm.RootPath + "Accounting/GeneralLedger/JournalFull.aspx?id=" + key;
             


             if (IForm.PageMode == Enums.PageMode.InTab) {

                 var tdata = new xtabdata();
                 tdata.linktype = Enums.LinkType.Direct;
                 tdata.id = 0;
               
                 url = IForm.RootPath + "Inventory/Money_Receipt_Entry_x.aspx?id=" + key;
                     tdata.name = "MR Entry : View";
                     tdata.label = "MR Entry : View";
                 
                 //tdata.name = "Transaction of Item (Issue): View";
                 //tdata.label = "Transaction of Item (Issue): View";
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


         $(document).ready(function () {
             var pageInstance = Sys.WebForms.PageRequestManager.getInstance();

             pageInstance.add_pageLoaded(function (sender, args) {
                 var panels = args.get_panelsUpdated();
                 for (i = 0; i < panels.length; i++) {
                     if (panels[i].id == gridUpdatePanelIDDet) {
                         //bindSegmentList(gridViewIDDet);
                     }
                 }
             });

             if ($('#' + txtCustomerName).is(':visible')) {

                 bindCustomerList();
             }

          


         });



         function bindCustomerList() {
             var cgColumns = [{ 'columnName': 'custname', 'width': '180', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                              , { 'columnName': 'custcode', 'width': '50', 'align': 'left', 'highlight': 4, 'label': 'Code' }
                              , { 'columnName': 'custaddress', 'width': '100', 'align': 'left', 'highlight': 0, 'label': 'address' }
                              , { 'columnName': 'custphone', 'width': '150', 'align': 'left', 'highlight': 0, 'label': 'Phone' }


             ];


             //var companyid = $('#' + hdnCompanyID).val();
             //var depthead = $('#' + hdnEmpCode).val();
             //var locationid = $('#' + ddlLocation).val();
             // var seid = $('#' + txtExecutiveID).val();

             //var serviceURL = GLAccountServiceLink + "?isterm=1&includeempty=1&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
             var serviceURL = CustomerListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
             //serviceURL += "&companyid=" + companyid;
             serviceURL += "&ispaging=1";
             serviceURL += "&isRefundable=" + "N";
             serviceURL += "&isRotary=" + "N";
             serviceURL += "&isService_Center=" + "N";
             // serviceURL += "&locationid=" + locationid;
             //serviceURL += "&seid=" + seid;
             // serviceURL += "&empstatus=" + "A";
             //serviceURL += "&acctypefilter=" + Enums.GLAccountTypeFilter.AllAccount;
             // serviceURL += "&namecomptype=" + Enums.DataCompareType.Contains;



             var customerIDElem = $('#' + txtCustCode);

             $('#' + btnCustID).click(function (e) {
                 //elmID = $(elem).attr('id');
                 //$(elem).combogrid("show");
                 $(customerIDElem).combogrid("dropdownClick");
             });


             $(customerIDElem).combogrid({
                 debug: true,
                 searchButton: false,
                 resetButton: false,
                 alternate: true,
                 munit: 'px',
                 scrollBar: true,
                 showPager: true,
                 colModel: cgColumns,
                 autoFocus: true,
                 showError: true,
                 width: 700,
                 url: serviceURL,
                 search: function (event, ui) {
                     //var companyCode = $('#' + ddlCompany).val();
                     //var branchCode = $('#' + hdnBranch).val();
                     //var deptCode = $('#' + hdnDepartment).val();
                     //var locationid = $('#' + lblLocationID).val();
                     // var seid = $('#' + txtExecutiveID).val();
                     var newServiceURL = serviceURL;
                     $(this).combogrid("option", "url", newServiceURL);


                 },
                 select: function (event, ui) {
                     if (!ui.item) {
                         event.preventDefault();

                         // $('#' + hdnDealerID).val('0');
                         $('#' + txtCustCode).val('');
                         return false;
                         //ClearGLAccountData(elemID);
                     }


                     if (ui.item.customerid == '') {
                         event.preventDefault();
                         return false;
                         //ClearGLAccountData(elemID);
                     }
                     else {
                         //alert(ui.item.custaddress);

                         $('#' + hdnCustId).val(ui.item.customerid);
                         $('#' + txtCustCode).val(ui.item.custcode);
                         $('#' + txtCustomerName).val(ui.item.custname);
                       
                     }
                     return false;
                 },

                 lc: ''
             });


             $(customerIDElem).blur(function () {
                 var self = this;

                 var customerID = $(customerIDElem).val();
                 if (customerID == '') {

                     $('#' + txtCustCode).val('');
                     $('#' + txtCustomerName).val('');
                    

                 }
             });
         }

   </script>
      <style type="text/css">
          .auto-style1 {
              height: 17px;
          }
      </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:HiddenField ID="hdnCustId" runat="server" Value="0" />
    <div id="dvPageContent" style="width: 100%; height: auto;">
        <div id="dvContentHeader" class="dvContentHeader">
                <div id="dvHeader" class="dvHeader">
                    <asp:Label ID="lblHeader" CssClass="lblHeader" runat="server" Text="MR List"></asp:Label>
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
                     <table style="width : 800px">
                       <tr>

                        <td align="right" class="auto-style2">
                            <asp:Label ID="lblloccode" runat="server" Text="Location : " Width="100px" style="font-weight: 700" Visible="false"></asp:Label>
                        </td>
                        <td class="auto-style2">
                            <%-- <asp:DropDownList ID="ddlDEPT_ID" runat="server" CssClass="dropDownList required"   Width="150px" ></asp:DropDownList>--%>

                             <asp:TextBox ID="txtLOC_NAME" runat="server" CssClass="textBox required" ViewStateMode="Enabled"  Visible="false"></asp:TextBox> 
                                                <%-- <input id="btnDeptID" type="button" value="" runat="server" class="buttonDropdown" tabindex="-1" />--%>
                          
                        </td>
                        <td> 
                             &nbsp;</td>
                        <td>
                             <asp:HiddenField ID="hdnLOC_CODE" runat="server" Value="0"  />
                        </td>
                    </tr>
                         
            <tr>
                <td style="" align="right">
                    <asp:Label ID="lblCustomerCode" runat="server" Text="Customer Code :" Font-Bold="true"></asp:Label>
                </td>
                
                            <td colspan="8">
                                <asp:TextBox ID="txtCustCode" runat="server" Width="100px" CssClass="textBox" TabIndex="1"></asp:TextBox>
                                <input id="btnCustID" type="button" value="" runat="server" class="buttonDropdown"
                                    tabindex="-1" />
                                <asp:TextBox ID="txtCustomerName" runat="server" Width="415px" CssClass="textBox"></asp:TextBox>
                            </td>
                          
                             
                        


            </tr>
                          <tr>
              <td align="right">
               <asp:Label ID="lblFromDate" runat="server" Text=" From Date:" style="height: 19px; width: 43px; " Font-Bold="True" Visible="true"></asp:Label>
              </td>
              <td>
                 <asp:TextBox ID="txtFromDate" runat="server" CssClass="textBox textDate dateParse" Style="text-align: left;" Width="100px"></asp:TextBox>
              </td>
              <td align="left">
                <%-- <asp:RegularExpressionValidator runat="server" ControlToValidate="txtFromDate" ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$" ErrorMessage="Invalid date format." ValidationGroup="Group1" ForeColor="Red" />--%>
                    <asp:Label ID="lblInvoiceToDate" runat="server" Text="To Date:" style="height: 19px; width: 43px; " Font-Bold="True" Visible="true"></asp:Label>
                   
              </td>
              <td>
                 <asp:TextBox ID="txtTodate" runat="server" CssClass="textBox textDate dateParse" Style="text-align: left;" Width="100px"></asp:TextBox>
                   
              </td>
            
              <td>
                  &nbsp;
                   
                 <asp:RegularExpressionValidator runat="server" ControlToValidate="txtTodate" ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$" ErrorMessage="Invalid date format." ValidationGroup="Group1" ForeColor="Red" />
              </td>
               
             
                 
            </tr>
                         
                         <tr class="rowParam">
                                           
                                            <td align="right" class="auto-style1">
                                   <asp:Label ID="lblisActive" runat="server" Text="IsAuthorized:" Font-Bold="True"></asp:Label>
                                            </td>
                                            <td class="auto-style1">
                                               <asp:DropDownList ID="ddlIsAuthorized" runat="server" CssClass="dropDownList " >
                                                <asp:ListItem Text="All" Value=""></asp:ListItem>
                                                <asp:ListItem   Text="A" Value="A"></asp:ListItem>
                                                 <asp:ListItem Selected="True" Text="U" Value="U"></asp:ListItem>
                                                <asp:ListItem Text="InActive" Value="N"></asp:ListItem>
                                            </asp:DropDownList>               

                                            </td>
                                            <td class="auto-style1">
                                                 </td>
                                            <td align="right" class="auto-style1">
                                            </td>
                                            <td class="auto-style1">
                                            </td>
                                        </tr>
                   
                        
                    <tr>
                        <td></td>
                            <td>
                                  <asp:Button ID="btnUpload" runat="server" CssClass="buttonSearch" Style="padding-left: 22px;"
                                Text="Show Data" OnClick="btnUpload_Click"  /> 
                                  <asp:HiddenField ID="hdnCompanyID" runat="server" Value ="0" />
                            </td>
                        <td>
                            
                            
                        </td>

                        <td> &nbsp;</td>
                        <td> <asp:HiddenField ID="hdnLoggedInUser" runat="server" Value ="0" />
                            
                        </td>
                    </tr>

                             
                         </table>
                    </div>
             <div id="dvControls" style="width: 100%; height : auto;">
                <div id="dvControlsInner" class="groupBoxContainer boxShadow" style="width: 80%; height : auto;">
                 
                        <div id="dvGrid" style="width: 98%; height: 90%; overflow: auto;">
                              
 
                        
                       
                            <dx:ASPxGridView ID="grdReceivedMstList" runat="server" AutoGenerateColumns="False" Width="90%"  ClientInstanceName="grdReceivedMstList" CssClass="textNumberFormat">
                                 <Columns>
                                   <%--  <dx:GridViewCommandColumn ShowClearFilterButton="True" VisibleIndex="0">
                                     </dx:GridViewCommandColumn>--%>
                                     <dx:GridViewDataTextColumn Caption="Action" UnboundType="String"     Width="55px" VisibleIndex="1">
                                        <DataItemTemplate>
                                            <dx:ASPxHyperLink ID="hyperLink" runat="server"   OnInit="hyperLink_Init">
                                            </dx:ASPxHyperLink>
                                        </DataItemTemplate>
                                    </dx:GridViewDataTextColumn>
                                     <dx:GridViewDataTextColumn Caption="Receive NO." Name="lblRECEIVE_NO" VisibleIndex="2"  Width="180px" FieldName="RECEIVE_NO">
                                    </dx:GridViewDataTextColumn>
                                    <%-- <dx:GridViewDataTextColumn Caption="Receive NO." Name="lblRECEIVE_NO" VisibleIndex="3"  Width="150px" FieldName="RECEIVE_NO">
                                    </dx:GridViewDataTextColumn>--%>
                                    
                                     <dx:GridViewDataTextColumn Caption="Cust Code" Name="lblRECEIVE_LOC_DP" VisibleIndex="3"  Width="80px"    FieldName="CUST_CODE">
                                    </dx:GridViewDataTextColumn>
                                      <dx:GridViewDataTextColumn Caption="Cust Name" Name="lblCust_NAME" VisibleIndex="4"  Width="250px" FieldName="CUST_NAME">
                                    </dx:GridViewDataTextColumn>
                                     <dx:GridViewDataDateColumn Caption="Received Date" Name="lblRECEIVED_DATE" VisibleIndex="5" Width="100px"   FieldName="RECEIVE_DATE">
                                        <PropertiesDateEdit DisplayFormatString="dd-MMM-yyyy" ></PropertiesDateEdit>
                                    </dx:GridViewDataDateColumn>
                                      <dx:GridViewDataTextColumn Caption="Pay Mode" Name="lblPAYMENT_MODE" VisibleIndex="6"  Width="100px" FieldName="PAYMENT_MODE">
                                    </dx:GridViewDataTextColumn>
                                     <dx:GridViewDataTextColumn Caption="Amount" Name="lblchqACC_NO" VisibleIndex="10"     CellStyle-CssClass="textNumberFormat"   Width="120px" FieldName="RECV_AMT">
                                         <EditCellStyle CssClass="textNumberFormat">
                                         </EditCellStyle>
                                         <EditFormCaptionStyle CssClass="textNumberFormat">
                                         </EditFormCaptionStyle>
                                         <HeaderStyle CssClass="textNumberFormat" />
                                         <CellStyle CssClass="textNumberFormat"></CellStyle>
                                         <FooterCellStyle Cursor="textNumberFormat">
                                         </FooterCellStyle>
                                         <GroupFooterCellStyle CssClass="textNumberFormat">
                                         </GroupFooterCellStyle>
                                    </dx:GridViewDataTextColumn>
                                    <%-- <dx:GridViewDataTextColumn Caption="Online Number" Name="lblchqACC_NO" VisibleIndex="10"  Width="200px" FieldName="TTNo">
                                    </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Cheque Acc." Name="lblchqACC_NO" VisibleIndex="10"  Width="250px" FieldName="chqACC_NO">
                                    </dx:GridViewDataTextColumn>--%>
                                     
                                    <%--  <dx:GridViewDataTextColumn Caption="Holder ACC_NO" Name="lblHolderACC_NO" VisibleIndex="10"  Width="250px" FieldName="HolderACC_NO">
                                    </dx:GridViewDataTextColumn>--%>
                                  
                                   <%-- <dx:GridViewDataDateColumn Caption="Create Date" Name="lblCREATE_DATE" VisibleIndex="9" Width="100px" FieldName="CREATE_DATE">
                                        <PropertiesDateEdit DisplayFormatString="dd-MMM-yyyy" ></PropertiesDateEdit>
                                    </dx:GridViewDataDateColumn>--%>

                                     </Columns>
                                <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                                <SettingsSearchPanel Visible="True"  />
                                <SettingsPager AlwaysShowPager="True" PageSize="20">
                                </SettingsPager>
                                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="380"   />
                                <Styles>
                                    <Header CssClass="headerRow" HorizontalAlign="Center" Font-Bold="True" ForeColor="White" Font-Size="9pt" BackColor="#80ADE5">
                                         
                                    </Header>
                                    <AlternatingRow BackColor="#FFFFCC">
                                    </AlternatingRow>
                                    <HeaderPanel BackColor="#669999">
                                    </HeaderPanel>
                                </Styles>
                            </dx:ASPxGridView>
                        
                       
                        </div>
                        

                </div>
                    
            </div>
             </div>
        </div>
</asp:Content>
