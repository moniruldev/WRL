<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="QC_Pass_List_view.aspx.cs" Inherits="PG.Web.Inventory.QC_Pass_List_view" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function UserDeleteConfirmation() {
            return confirm("Are you sure you want to Save and Authorized?");
        }

    </script>
     <script src="../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
    <script src="../javascript/jquery.attributeobserver.js" type="text/javascript"></script>
    <link href="../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />

      <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css"/> 
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap-theme.min.css"/>  
     <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js" type="text/javascript"></script> 

    <script language="javascript" type="text/javascript">

        var ReportViewPageLink = '<%=this.ReportViewPageLink%>';
        var ReportViewPDFPageLink = '<%=this.ReportViewPDFPageLink%>';
        var ReportPrintPageLink = '<%=this.ReportPrintPageLink%>';
        var ReportPDFPageLink = '<%=this.ReportPDFPageLink%>';

        var SupplierListServiceLink = '<%=this.SupplierListServiceLink%>';

        function checkFilled() {
            var inputVal = document.getElementById("<%=txtRemarks.ClientID%>");
            if (inputVal.value == "") {
                inputVal.style.backgroundColor = "";
              
            }
            else {
                inputVal.style.backgroundColor = "";
            }
        }

        function tbopen(key, isPrint, isPDFAutoPrint, showWait) {
            key = key || '';
            isPrint = isPrint || false;
            showWait = showWait || true;

            if (isPrint) {
                if (key != '') {
                    ReportPrint(key, isPDFAutoPrint);
                    return;
                }
            }

            //var url = "/Report/ReportView.aspx?rk=" + key

            var now = new Date();
            var strTime = now.getTime().toString();
            var url = ReportViewPageLink + "?rk=" + key + "&_tt=" + strTime;
            //var url = ReportViewPageLink + "?rk=" + key;

            //if (pageInTab == 1)
            if (TabVar.PageMode == Enums.PageMode.InTab) {

                var tdata = new xtabdata();
                tdata.linktype = Enums.LinkType.Direct;
                tdata.id = 7999;
                tdata.name = "Report view";
                //tdata.label = "User: " + userid;
                tdata.label = "Report view";
                tdata.type = 0;
                tdata.url = url;
                tdata.tabaction = Enums.TabAction.InNewTab;
                tdata.selecttab = 1;
                tdata.reload = 0;
                tdata.param = "";
                tdata.showWait = showWait;

                try {
                    //window.parent.OpenMenuByData(tdata);
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

        $(document).ready(function () {

            checkFilled();

            if ($('#' + txtSupplierName).is(':visible')) {

                bindSupplierList();
            }


        });
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
            serviceURL += "&ispaging=1";
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
                        $('#' + txtDealerID).val('');
                        return false;
                        //ClearGLAccountData(elemID);
                    }


                    if (ui.item.dealerid == '') {
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
                    // $('#' + hdnDealerID).val('0');
                    $('#' + txtSupplierName).val('');

                }
            });
        }

    </script>
        <style type="text/css">
          .qcModal td{
            padding:3px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div id="dvPageContent" style="width: 100%; height: 100%;">

        <div id="dvContentHeader" class="dvContentHeader">
            <div id="dvHeader" class="dvHeader" align="left">
                <asp:Label ID="lblHeader" runat="server" Text="QC Pass Against MRR" CssClass="lblHeader" Font-Bold="true" Font-Size="15px"></asp:Label>
            </div>
            <div id="dvMessage" class="dvMessage">
                <asp:Label ID="lblMessage" runat="server" CssClass="lblMessage" Text="" Width="100%"></asp:Label>
            </div>

            <div id="dvHeaderControl" class="dvHeaderControl">
            </div>

        </div>

        <div id="dvContentMain" class="dvContentMain" align="center">
            <asp:HiddenField ID="hdnSupplierID" runat="server" Value="0" />
            <asp:HiddenField ID="hdnQCID" runat="server" Value="0" />
            <asp:HiddenField ID="hdnStoreID" runat="server" Value="0" />
            <asp:Label ID="lblMode" runat="server" Visible="false"></asp:Label>
            <div id="dvControlsHead" style="height: auto; width: 100%;" align="center">
                <table border="0" cellspacing="4" cellpadding="2" align="center" style="width: 100%" id="tblItemEntry">

                    <tr>
                        <td align="right" class="auto-style2">
                            <asp:Label ID="Label5" runat="server" Text="QC Pass No :" Font-Bold="true"></asp:Label>
                        </td>
                        <td align="left" class="auto-style3">

                            <asp:TextBox ID="txtQCPassNo" ReadOnly="true" runat="server" CssClass="colourdisabletextBox" Font-Bold="true" BackColor="#FFFFCC"></asp:TextBox>
                        </td>
                        <td align="right" class="auto-style2">
                            <asp:Label ID="lblPassDate" runat="server" Text="Pass Date :" Font-Bold="true"></asp:Label><span style="color: red">*</span>

                        </td>

                        <td align="left" class="auto-style3">

                            <asp:TextBox ID="txtPassDate" runat="server" CssClass="textBox textDate" Font-Bold="true"></asp:TextBox>
                        </td>
                    
                    </tr>


                    <tr>
                           <td align="right">
                         <asp:Label runat="server" ID="lblQCType" Text="QC Type :"  Font-Bold="true"></asp:Label>
                     </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlQCType" CssClass="dropDownList" runat="server"></asp:DropDownList>
                        </td>
                    
                           <td align="right" class="auto-style2">
                            <asp:Label ID="Label10" runat="server" Text="MRR No :" Font-Bold="true"></asp:Label>
                        </td>
                           <td align="left" class="auto-style3">
                            <asp:TextBox ID="txtMRRNo" runat="server" ReadOnly="true" CssClass="textBox"  Font-Bold="true" ></asp:TextBox>
                        </td>

                    </tr>

                    <tr>
                      <td align="right" class="auto-style2">
                            <asp:Label ID="lblRemarks" runat="server" Text="Remarks :" Font-Bold="true"></asp:Label>
                        </td>
                        <td  align="left" class="auto-style2">
                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="textBox" onchange=" return checkFilled();"  TextMode="MultiLine" Height="30px" Width="300px"></asp:TextBox>

                        </td>
                     
                        <td align="right" class="auto-style2">
                            <asp:Label ID="lblMRRDate" runat="server" Text="MRR Date :" Font-Bold="true"></asp:Label>
                        </td>
                        <td align="left" class="auto-style3">

                            <asp:TextBox ID="txtMRRDate" runat="server" ReadOnly="true" CssClass="textBox" Font-Bold="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="auto-style2">
                            <asp:Label ID="lblSupplier" runat="server" Text="Supplier :" Font-Bold="true"></asp:Label>
                        </td>
                        <td colspan="3" align="left" class="auto-style2">
                            <asp:TextBox ID="txtSupplier" runat="server" CssClass="textBox"  Width="200px" Enabled="false"></asp:TextBox>

                        </td>
                     
                    </tr>

                </table>

            </div>

            <div id="dvControls" style="height: auto; width: 100%;">
                <div id="dvControlsInner" class="groupBoxContainer boxShadow" style="height: auto; width: 100%;">


                    <div id="dvGridContainer" style="width: 100%; height: 100%;">


                        <div id="dvGrid" style="width: 95%; height: 300px; overflow: auto; align-content: flex-start;">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                                CellPadding="1" CellSpacing="1" ForeColor="#333333" GridLines="None"
                                Font-Names="Arial" Font-Size="9pt" PageSize="15"
                                EmptyDataText="No Data Found" Width="95%" Style="margin-bottom: 0px" OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting" OnRowCommand="GridView1_RowCommand" align="Left">
                                <PagerSettings Mode="NumericFirstLast" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <Columns>
                                    <asp:TemplateField HeaderText="SL#" HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnItemId" runat="server" Value='<%# Bind("ITEM_ID") %>' />
                                            <asp:HiddenField ID="hdnMRRDetId" runat="server" Value='<%# Bind("TRANS_DET_ID") %>' />
                                            <asp:HiddenField ID="hdnPassDetId" runat="server" Value='<%# Bind("QC_DET_ID") %>' />
                                            <asp:HiddenField ID="hdnUomId" runat="server" Value='<%# Bind("UOM_ID") %>' />
                                            <asp:Label ID="lblItemSlNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>' Style="text-align: center;" Width="80px"> </asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Item Name" Visible="true" HeaderStyle-Width="150px" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemName" runat="server" Text='<%# Bind("item_name") %>' Style="text-align: center;" Width="150px"></asp:Label>

                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Item Type" Visible="true" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="txtItemType" runat="server" Text='<%# Bind("ITEM_TYPE_CODE") %>' Style="text-align: center;" Width="100px"></asp:Label>
                                            <asp:HiddenField runat="server" ID="hdnItemType" Value='<%# Bind("ITEM_TYPE_ID") %>'  />
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="MRR Qty" Visible="true" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="txtMRRQty" runat="server" Text='<%# Bind("MRR_QTY") %>' Style="text-align: center;" Width="100px"></asp:Label>

                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Already QC" Visible="true">
                                        <ItemTemplate>
                                            <asp:Label ID="txtAlreadyQCQty" runat="server" Text='<%# Bind("ALREADY_QC_QTY") %>' Style="text-align: center;" Width="100px"></asp:Label>

                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Already Transfer" Visible="true">
                                        <ItemTemplate>
                                            <asp:Label ID="txtAlreadyTransferQty" runat="server" Text='<%# Bind("ALREADY_TRANS_QTY") %>' Style="text-align: center;" Width="100px"></asp:Label>

                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Item Qty" Visible="true">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtItemQty" Text='<%# Bind("QC_ITEM_QTY") %>' runat="server" CssClass=" textBox textNumberOnly" Style="text-align: center;" Width="100px"></asp:TextBox>
                                            <asp:HiddenField ID="hdnItemQty" runat="server" Value='<%# Bind("QC_ITEM_QTY") %>' />
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="UOM" Visible="true" HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUnitName" runat="server" Text='<%# Bind("UOM_CODE") %>' Style="text-align: center;" Width="80px"></asp:Label>

                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Unit Rate" Visible="false" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUnitRate" runat="server" Text='<%# Bind("UNIT_PRICE") %>' Style="text-align: center;" Width="100px"></asp:Label>

                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Total Cost" Visible="false" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotalCost" runat="server" Text='<%# Bind("TOTAL_COST") %>' Style="text-align: center;" Width="100px"></asp:Label>

                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                       <asp:TemplateField HeaderText="QC Criterion" Visible="true">
                                        <ItemTemplate>
                                           <%-- <asp:TextBox ID="txtRemarks" TextMode="MultiLine" Text='<%# Bind("QC_NOTE_DTL") %>' runat="server" Style="text-align: center;" Width="150px" Rows="1"></asp:TextBox>--%>
                                         <asp:LinkButton ID="lnkQCCriterion"  CommandName="qc"   CssClass="btn btn-default" Style="padding:1px; margin:1px; border:1px solid #647DD8"  CommandArgument='<%# Bind("ITEM_ID") %>' runat="server" CausesValidation="false" Text="" ><i class="glyphicon glyphicon-edit" style="padding-right:4px;color:blue;font-size:14px"></i></asp:LinkButton>  

                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="MRR Remarks" Visible="true">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMRRRemarks" runat="server" Text='<%# Bind("NOTE") %>' Style="text-align: center;" Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                 

                                   <asp:TemplateField HeaderText="Delete" Visible="true">
                                    <ItemTemplate>
                                    <asp:LinkButton ID="btnDeleteRow" CssClass="buttonDeleteIcon" Height="16px" Width="16px" CommandName="delete" runat="server">
                                    </asp:LinkButton>
                                   </ItemTemplate>
                                   <ItemStyle VerticalAlign="Top" />
                                   </asp:TemplateField>




                                </Columns>
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#999999" />
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            </asp:GridView>
                        </div>

                          <div id="myModalCom" class="modal fade" >  
            <div class="modal-dialog" style="max-width:650px; align-content:space-around;">  
                <div class="modal-content">  
                    <div class="modal-header">  

                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>  
                        <h4 class="modal-title">Criterion detail page</h4>  
                    </div>  
                    <div class="modal-body" style="overflow-y: scroll; max-height: 85%; margin-top: 10px; margin-bottom: 10px;">  
                        <asp:Label ID="Label4" runat="server" ClientIDMode="Static"></asp:Label>  
                        <asp:Label ID="lblMemoNocomm" runat="server" Visible="false" ></asp:Label>
                        <asp:HiddenField ID="hdnItemIDCriterion" runat="server" />
                         <asp:HiddenField ID="hdnQATEMPEntryID" runat="server" Value="0" />
                         <asp:HiddenField ID="hdnQCDtlId" runat="server" Value="0" />

                   

                        <table id="tblQcModal" class="qcModal" runat="server"> 
                               <tr>
                                <td align="right">
                                    <asp:Label ID="lblItemName" runat="server" Font-Bold="true" Text="Item Name :" ></asp:Label>
                                </td>
                                <td>
                                     <asp:Label ID="lblItemNameText" CssClass="text-success" Font-Bold="true" runat="server" Visible="true" ></asp:Label>
                                </td>
                            </tr>
                         <%--   <tr style="">
                                <td></td>
                                <td></td>
                            </tr>--%>
                            <tr id="trVendor" runat="server" visible="false" >
                                <td align="right">
                                    <asp:Label ID="lblVendor" runat="server" Text="Approved Vendor? : " Font-Bold="true"  ></asp:Label>
                                </td>
                               <%--  <td></td>--%>
                                <td align="left">
                                    <asp:DropDownList runat="server" ID="ddlVendor" CssClass="dropdown dropDownList" Height="25px">
                                        <asp:ListItem Text="--Select--" Value="" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="N" ></asp:ListItem>

                                    </asp:DropDownList>
                                    
                                </td>
                              <%--  <td>&nbsp;&nbsp;</td>
                                <td> </td>--%>
                            </tr>
                           <%--   <tr style="">
                                <td></td>
                                <td></td>
                            </tr>--%>

                                 <tr id="trAnalysisCirtificate"  runat="server" visible="false">
                                <td align="right">
                                   
                                </td>
                                <%-- <td>&nbsp;</td>--%>
                                <td colspan="6">
                                   <asp:CheckBox runat="server" ID="chkAnalysisCirtificate" />
                                    <asp:Label runat="server" ID="lblAnalysisCirtificate" Text="Supplier Should Provide the Certificate of Analysis" Font-Bold="true"></asp:Label>
                                    
                                </td>
                              <%--  <td>&nbsp;&nbsp;</td>
                                <td> </td>--%>
                            </tr>
                            <%--  <tr style="">
                                <td></td>
                                <td></td>
                            </tr >--%>
                               <tr id="trOESResult" runat="server" visible="false">
                                <td align="right" >
                                    <asp:Label ID="lblOESResult" runat="server" Text="OES Test Result : " Font-Bold="true"  ></asp:Label>
                                </td>
                               <%--  <td></td>--%>
                                <td align="left">
                                    <asp:DropDownList runat="server" ID="ddlOESResult" CssClass=" dropdown dropDownList" Height="25px">
                                        <asp:ListItem Text="--Select--" Value="" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Ok" Value="Y" ></asp:ListItem>
                                        <asp:ListItem Text="Not OK" Value="N" ></asp:ListItem>

                                    </asp:DropDownList>
                                    
                                </td>
                             <%--   <td>&nbsp;&nbsp;</td>
                                <td> </td>--%>
                            </tr>
                         <%--     <tr style="">
                                <td></td>
                                <td></td>
                            </tr>--%>
                             <tr id="trRLeadPercent" runat="server" visible="false">
                                <td align="right">
                                    <asp:Label ID="lblRLeadPercent" runat="server" Text="Percentage of Red Lead : " Font-Bold="true"  ></asp:Label>
                                </td>
                                 <%--<td></td>--%>
                                <td align="left">
                                 <asp:TextBox runat="server" ID="txtRLeadPercent" Height="25px" Width="200px" CssClass="textBox"></asp:TextBox>
                                    
                                </td>
                              <%--  <td>&nbsp;&nbsp;</td>
                                <td> </td>--%>
                            </tr>
                           <%--   <tr style="">
                                <td></td>
                                <td></td>
                            </tr>--%>
                            <tr id="trIronRange" runat="server" visible="false">
                                <td align="right">
                                    <asp:Label ID="lblIronRange" runat="server" Text="Iron Range : " Font-Bold="true"  ></asp:Label>
                                </td>
                                 <%--<td></td>--%>
                                <td align="left">
                                    <asp:DropDownList runat="server" ID="ddlIronRange" CssClass=" dropdown dropDownList" Height="25px">
                                        <asp:ListItem Text="--Select--" Value="" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="N" ></asp:ListItem>

                                    </asp:DropDownList>
                                    <asp:Label runat="server" ID="Label1" Text=" <70 ppm "></asp:Label>
                                </td>
                              
                            <%--    <td>&nbsp;&nbsp;</td>
                                <td> </td>--%>
                            </tr>
                          <%--    <tr style="">
                                <td></td>
                                <td></td>
                            </tr>--%>

                              <tr id="trGravity" runat="server" visible="false">
                                <td align="right">
                                    <asp:Label ID="lblGravity" runat="server" Text="Specific Gravity : " Font-Bold="true"  ></asp:Label>
                                </td>
                                 <%--<td></td>--%>
                                <td align="left">
                                 <asp:TextBox runat="server" ID="txtGravity" Height="25px" Width="200px" CssClass="textBox"></asp:TextBox>
                                    
                                </td>
                              <%--  <td>&nbsp;&nbsp;</td>
                                <td> </td>--%>
                            </tr>
                         <%--     <tr style="">
                                <td></td>
                                <td></td>
                            </tr>--%>
                                <tr id="trTransparency" runat="server" visible="false">
                                <td align="right">
                                    <asp:Label ID="lblTransparency" runat="server" Text="Transparency : " Font-Bold="true"  ></asp:Label>
                                </td>
                                 <%--<td></td>--%>
                                <td align="left">
                                    <asp:DropDownList runat="server" ID="ddlTransparency" CssClass=" dropdown dropDownList"  Height="25px">
                                        <asp:ListItem Text="--Select--" Value="" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="N" ></asp:ListItem>

                                    </asp:DropDownList>
                                    
                                </td>
                              
                            <%--    <td>&nbsp;&nbsp;</td>
                                <td> </td>--%>
                            </tr>
                         <%--     <tr style="">
                                <td></td>
                                <td></td>
                            </tr>--%>
                                <tr id="trNaOHContent" runat="server" visible="false">
                                <td align="right">
                                    <asp:Label ID="lblNaOHContent" runat="server" Text="NaOH Content : " Font-Bold="true"  ></asp:Label>
                                </td>
                                 <%--<td></td>--%>
                                <td align="left">
                                 <asp:TextBox runat="server" ID="txtNaOHContent" Height="25px" Width="200px" CssClass="textBox"></asp:TextBox>
                                    
                                </td>
                              <%--  <td>&nbsp;&nbsp;</td>
                                <td> </td>--%>
                            </tr>
                          <%--    <tr style="">
                                <td></td>
                                <td></td>
                            </tr>--%>
                              <tr id="trApprovalSample" runat="server" visible="false">
                                <td align="right">
                                    <asp:Label ID="lblApprovedSample" runat="server" Text="Approved Sample? : " Font-Bold="true"  ></asp:Label>
                                </td>
                                 <%--<td></td>--%>
                                <td align="left">
                                    <asp:DropDownList runat="server" ID="ddlApprovedSample" CssClass=" dropdown dropDownList" Height="25px">
                                        <asp:ListItem Text="--Select--" Value="" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="N" ></asp:ListItem>

                                    </asp:DropDownList>
                                    
                                </td>
                              
                              <%--  <td>&nbsp;&nbsp;</td>
                                <td> </td>--%>
                            </tr>
                          <%--    <tr style="">
                                <td></td>
                                <td></td>
                            </tr>--%>
                               <tr id="trWasher" runat="server" visible="false">
                                <td align="right">
                                   
                                </td>
                                <%-- <td>&nbsp;</td>--%>
                                <td colspan="6">
                                   <asp:CheckBox runat="server" ID="chkWasher" />
                                    <asp:Label runat="server" ID="lblWasher" Text="To be Supplied With Washer" Font-Bold="true"></asp:Label>
                                    
                                </td>
                              <%--  <td>&nbsp;&nbsp;</td>
                                <td> </td>--%>
                            </tr>
                          <%--    <tr style="height:5px;">
                                <td></td>
                                <td></td>
                            </tr>--%>

                        </table>

        
                    </div>  
                    <div class="modal-footer"> 
                        <table>
                            <tr>
                        <td>
                     <asp:Button ID="btnSaveCriterion" runat="server" Text="Add" CssClass="buttonNew" OnClick="btnSaveCriterion_Click" Visible="true"   />
                       <%-- class="btn btn-default"--%>
                            <%-- <button id="btnSave" type="button"  Class="buttonSave" OnClick="btnSave_Click" >Save</button>  --%>
                   
                        <button type="button"  Class="buttonClose"  data-dismiss="modal">Close</button>  
                             </td>
                                </tr>
                          </table> 
                    </div>  
                </div>  
            </div>  
        </div>  


                    </div>

                </div>
            </div>

        </div>

        <div id="dvContentFooter" class="dvContentFooter">
            <table align="center">
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="btnAddNew" runat="server" Text="New" CssClass="buttonNew" Visible="false" OnClick="btnAddNew_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonSave" OnClick="btnSave_Click" OnClientClick="if ( ! UserDeleteConfirmation()) return false;" />

                    </td>
                    <td>
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="buttonEdit" OnClick="btnEdit_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="buttonDelete" Visible="false" />
                    </td>
                     <td>
                        <asp:Button ID="btnQCTransfer" runat="server" Text="Transfer to Store" CssClass="buttoncommon" OnClick="btnQCTransfer_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonCancel checkIsDirty" />

                    </td>


                    <td>
                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" CssClass="buttonRefresh checkIsDirty" />
                    </td>


                    <td></td>


                    <td>
                        <input id="btnClose" type="button" runat="server" class="buttonClose" value="Close" onclick="if (ContentForm) { ContentForm.CloseForm(); }" />
                    </td>

                          <td>
                        <asp:Button ID="btnMRRPrint" runat="server" Visible="true" Text="Print PO" CssClass="buttoncommon" Enabled="True" OnClick="btnMRRPrint_Click" />
                    </td>
                    <td>
                        <asp:Label ID="Label6" runat="server" Text="Print Format:"></asp:Label>
                    </td>
                    <td>
                        <td>
                            <asp:DropDownList ID="ddlReportViewType" runat="server" CssClass="dropDownList">
                                <asp:ListItem Value="0">Screen</asp:ListItem>
                                <asp:ListItem Selected="True" Value="1">PDF</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlReportViewMode" runat="server" CssClass="dropDownList">
                            <asp:ListItem Value="0">In This Tab</asp:ListItem>
                            <asp:ListItem Value="1">In New Tab</asp:ListItem>
                            <asp:ListItem Selected="True" Value="2">In New Window</asp:ListItem>
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
