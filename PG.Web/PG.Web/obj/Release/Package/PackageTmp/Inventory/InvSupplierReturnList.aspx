<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" ViewStateMode="Disabled" AutoEventWireup="true" CodeBehind="InvSupplierReturnList.aspx.cs" Inherits="PG.Web.Inventory.InvSupplierReturnList" %>
<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script src="../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
    <script src="../javascript/jquery.attributeobserver.js" type="text/javascript"></script>
    <link href="../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        // <!CDATA[
  

        ContentForm.CalendarImageURL = "../image/calendar.png";

       

        var txtSupplierName = '<%=txtSupplierName.ClientID%>';
        var btnSupplierID = '<%=btnSupplierID.ClientID%>';
        var hdnSupplierID = '<%=hdnSupplierID.ClientID%>';
        var SupplierListServiceLink = '<%=this.SupplierListServiceLink%>';

    

        function PageResizeCompleted(pg, cntMain) {
            resizeContentInner(cntMain);

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

            if ($('#' + txtSupplierName).is(':visible')) {

                bindSupplierList();


            }

        });
    



        function tbopen(key, userid) {
            key = key || '';

            var url = IForm.RootPath + "Inventory/INVSupplierReturn.aspx?id=" + key;

            if (IForm.PageMode == Enums.PageMode.InTab) {

                var tdata = new xtabdata();
                tdata.linktype = Enums.LinkType.Direct;
                tdata.id = 0;
                tdata.name = "Supplier Return";
                tdata.label = "Supplier Return";
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

        function fromParent(val1) {
            alert('this is called from parent: ' + val1);
        }

        function bindSupplierList() {
            var cgColumns = [{ 'columnName': 'supcode', 'width': '80', 'align': 'left', 'highlight': 2, 'label': 'Code' }
                             , { 'columnName': 'supname', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                             , { 'columnName': 'supaddress', 'width': '200', 'align': 'left', 'highlight': 4, 'label': 'Address' }
                             , { 'columnName': 'supphone', 'width': '80', 'align': 'left', 'highlight': 0, 'label': 'phone' }



            ];


            //var companyid = $('#' + hdnCompanyID).val();
            //var depthead = $('#' + hdnEmpCode).val();
            //var locationid = $('#' + ddlLocation).val();
            // var seid = $('#' + txtExecutiveID).val();

            //var serviceURL = GLAccountServiceLink + "?isterm=1&includeempty=1&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            var serviceURL = SupplierListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            //serviceURL += "&companyid=" + companyid;
            serviceURL += "&ispaging=1&suptype=L";
          
            // serviceURL += "&locationid=" + locationid;
            //serviceURL += "&seid=" + seid;
            // serviceURL += "&empstatus=" + "A";
            //serviceURL += "&acctypefilter=" + Enums.GLAccountTypeFilter.AllAccount;
            // serviceURL += "&namecomptype=" + Enums.DataCompareType.Contains;



            var supplierIDElem = $('#' + txtSupplierName);

            $('#' + btnSupplierID).click(function (e) {
                //elmID = $(elem).attr('id');
                //$(elem).combogrid("show");
                $(supplierIDElem).combogrid("dropdownClick");
              
            });


            $(supplierIDElem).combogrid({
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
                width: 600,
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
                        $('#' + txtSupplierName).val('');
                        return false;
                        //ClearGLAccountData(elemID);
                    }


                    if (ui.item.supid == '') {
                        event.preventDefault();
                        return false;
                        //ClearGLAccountData(elemID);
                    }
                    else {
                        // $('#' + hdnDealerID).val(ui.item.dealerid);
                        $('#' + hdnSupplierID).val(ui.item.supid);
                        $('#' + txtSupplierName).val(ui.item.supname);


                    }
                    return false;
                },

                lc: ''
            });


            $(supplierIDElem).blur(function () {
                var self = this;

                var customerID = $(supplierIDElem).val();
                if (customerID == '') {
                     $('#' + hdnSupplierID).val('0');
                    $('#' + txtSupplierName).val('');

                }
            });
        }




        // ]]>
    </script>
    <style type="text/css">
        /*.FixedHeader { POSITION: relative; TOP: expression(this.offsetParent.scrollTop); BACKGROUND-COLOR: white } */

        .FixedHeader {
            position: relative;
            background-color: white;
        }

        #dvMessage {
            height: 20px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <asp:HiddenField ID="hdnSupplierID" runat="server" Value="0" />
    <asp:HiddenField ID="hdnDeptId" runat="server" />
    <div id="dvPageContent" style="width: 100%; height: auto;">
        <div id="dvContentHeader" class="dvContentHeader">
            <div id="dvHeader" class="dvHeader">
                <asp:Label ID="lblHeader" CssClass="lblHeader" runat="server" Text="Supplier Return List"></asp:Label>
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
                            <asp:Label ID="Label5" runat="server" Text="Return No:"></asp:Label>
                           
                        </td>
                        <td>
                             <asp:TextBox ID="txtRtnNo" runat="server" CssClass="textBox notEnterToTab"></asp:TextBox>
                        </td>
                        <td align="right">
                        <asp:Label ID="Label1" runat="server" Text="Supplier:"></asp:Label>

                        </td>
                                                    
                        <td >
                       <asp:TextBox ID="txtSupplierName" runat="server" CssClass="textBox" Width="180px"></asp:TextBox>
                       <input id="btnSupplierID" type="button" value="" runat="server" class="buttonDropdown" tabindex="-1" />
                       </td>

                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label6" runat="server" Text="Date From:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="textBox textDate dateParse"></asp:TextBox>
                        </td>

                        <td align="right">
                            <asp:Label ID="lblToDate" runat="server" Text="Date To:"></asp:Label>
                        </td>

                        <td>
                            <asp:TextBox ID="txtToDate" runat="server"  CssClass="textBox textDate dateParse"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                          <td align="right">
                            <asp:Label ID="lblAuthStatus" runat="server" Text="Auth Status:" ></asp:Label>&nbsp;
                        </td>

                        <td>
                            <asp:DropDownList ID="ddlAuthorizationStatus" runat="server" CssClass="dropDownList">
                                  <asp:ListItem Text="All" Value="0" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Unauthorized" Value="N" ></asp:ListItem>
                                <asp:ListItem Text="Authorized" Value="Y"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                         <td align="right">
                             <asp:Label ID="lblFromDepartment" runat="server" Text="Department:"></asp:Label>

                         </td>
                         <td align="left">
                          <asp:DropDownList ID="ddlFromDepartment" runat="server" ViewStateMode="Enabled" CssClass="dropDownList">
                          </asp:DropDownList>
                        </td>
                      
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                          <td>
                            <asp:Button ID="btnRefresh" runat="server" CssClass="buttonRefresh"    Text="Show Data" OnClick="btnShow_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnAddNew" runat="server" CssClass="buttonNew"    Text="New Return" Height="26px" />
                            <%--<input id="btnAddNew" type="button" runat="server" value="New IGR (Requisition)" class="buttonNew" />--%>
                        </td>
                    </tr>

                </table>
            </div>

            <div id="dvControls" style="width: 100%;">
                <div id="dvControlsInner" class="groupBoxContainer boxShadow" style="width:90%">
                    <div id="dvGridContainer" style="width: 100%; height: auto; text-align: left;">

                        <div id="dvGrid" style="width:100%; height: 250px; overflow: auto;">
                            <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="ASPxGridView1" runat="server" Width="100%" AutoGenerateColumns="False" KeyFieldName="RTN_NO">
                                <Settings VerticalScrollableHeight="250" ShowFooter="True" ShowGroupFooter="VisibleAlways" />
                             <%--   <TotalSummary>
                                    <dx:ASPxSummaryItem FieldName="Size" SummaryType="Sum" />
                                    <dx:ASPxSummaryItem FieldName="INDT_QTY" ShowInColumn="INDT_QTY" DisplayFormat="Total:{0}" ShowInGroupFooterColumn="PURCHASE_QTY" SummaryType="Sum" />
                                </TotalSummary>
                                <GroupSummary>
                                    <dx:ASPxSummaryItem SummaryType="Count" />
                                    <dx:ASPxSummaryItem FieldName="INDT_QTY" ShowInGroupFooterColumn="INDT_QTY" SummaryType="Sum" />
                                </GroupSummary>--%>
                                <Columns>
                                       <dx:GridViewDataTextColumn Caption="Action" UnboundType="String" Width="55px" VisibleIndex="1">
                                        <DataItemTemplate>
                                            <dx:ASPxHyperLink ID="hyperLink" runat="server" OnInit="hyperLink_Init">
                                            </dx:ASPxHyperLink>
                                        </DataItemTemplate>
                                    </dx:GridViewDataTextColumn>
                                      <dx:GridViewDataTextColumn Caption="Return No" FieldName="RTN_NO" VisibleIndex="1" Width="100px">
                                    </dx:GridViewDataTextColumn>  
                                    
                                    <dx:GridViewDataTextColumn Caption="Return Date" FieldName="RTN_DATE" VisibleIndex="3" Width="80px">
                                        <DataItemTemplate>
                                            <%#Eval("RTN_DATE", "{0:dd-MMM-yyyy}")%>
                                        </DataItemTemplate>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Supplier" FieldName="SUPPLIER_NAME" VisibleIndex="8" Width="80px" />
                                    <dx:GridViewDataTextColumn Caption="Auth Status" FieldName="AUTH_STATUS" VisibleIndex="8" Width="20px" />
                                    <dx:GridViewDataTextColumn Caption="Create By" FieldName="FULLNAME" VisibleIndex="8" Width="20px" />
                                  
                                </Columns>
                                <SettingsBehavior AllowFixedGroups="True" AutoExpandAllGroups="True" SortMode="Value" />
                                <SettingsPager NumericButtonCount="20">
                                    <PageSizeItemSettings Visible="true" Items="50,100" />
                                </SettingsPager>
                                <Settings ShowGroupPanel="true" ShowFilterBar="Visible"  ShowGroupedColumns="True" ShowHeaderFilterButton="True" ShowGroupButtons="True" />

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
        </div>
    </div>
</asp:Content>