<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="INVMrrListForAccUpdate.aspx.cs" Inherits="PG.Web.Inventory.INVMrrListForAccUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

     <script src="../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
    <script src="../javascript/jquery.attributeobserver.js" type="text/javascript"></script>
    <link href="../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />

        <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css"/> 
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap-theme.min.css"/>  
     <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js" type="text/javascript"></script> 

    <script language="javascript" type="text/javascript">
        // <!CDATA[

        ContentForm.CalendarImageURL = "../image/calendar.png";

        var btnGridPageGoTo = '<%=btnGridPageGoTo.ClientID %>';
        var txtGridPageNo = '<%=txtGridPageNo.ClientID %>';


        var SupplierListServiceLink = '<%=this.SupplierListServiceLink%>';
        var txtSupplierName = '<%=txtSupplierName.ClientID%>';
        var btnSupplierID = '<%=btnSupplierID.ClientID%>';
        var hdnSupplierID = '<%=hdnSupplierID.ClientID%>';

        var ItemListServiceLink = '<%=this.ItemListServiceLink%>';
        var btnItemLoad = '<%= btnItemLoad.ClientID%>';
        var hdnItemIdForFilter = '<%= hdnItemIdForFilter.ClientID%>';
        var txtItemName = '<%= txtItemName.ClientID%>';


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
            $('#' + txtGridPageNo).keydown(function (e) {
                if (e.keyCode == 13) {
                    e.preventDefault();
                    $('#' + btnGridPageGoTo).click();
                }
            });
        });

        function tbopen(key, userid) {
            key = key || '';

            var url = IForm.RootPath + "Inventory/INVNewMRRFromLC.aspx?id=" + key + "&type=" + "LC";
            //var url = IForm.RootPath + "Accounting/GeneralLedger/JournalFull.aspx?id=" + key;


            if (IForm.PageMode == Enums.PageMode.InTab) {

                var tdata = new xtabdata();
                tdata.linktype = Enums.LinkType.Direct;
                tdata.id = 0;
                tdata.name = "New MRR";
                //tdata.label = "User: " + userid;
                tdata.label = "New MRR";
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

        function isNumberKey(evt, obj) {

            var charCode = (evt.which) ? evt.which : event.keyCode
            var value = obj.value;
            var dotcontains = value.indexOf(".") != -1;
            if (dotcontains)
                if (charCode == 46) return false;
            if (charCode == 46) return true;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;

        }

        function GetTotalamt() {
            var mrrQty = document.getElementById('<%= txtQty.ClientID %>').value;
            var ItemRate = document.getElementById('<%= txtItemRate.ClientID %>').value;

            var Total = parseFloat(mrrQty) * parseFloat(ItemRate);
            //alert(Total);
            document.getElementById('<%= txtTotalCost.ClientID %>').value = Total;

        }

        function fromParent(val1) {
            alert('this is called from parent: ' + val1);
        }

        $(document).ready(function () {

            if ($('#' + txtSupplierName).is(':visible')) {

                bindSupplierList();
            }

            if ($('#' + txtItemName).is(':visible')) {
                bindItemList();
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
            serviceURL += "&ispaging=1"; //&suptype=F
            // serviceURL += "&locationid=" + locationid;
            //serviceURL += "&seid=" + seid;
            // serviceURL += "&empstatus=" + "A";
            //serviceURL += "&acctypefilter=" + Enums.GLAccountTypeFilter.AllAccount;
            // serviceURL += "&namecomptype=" + Enums.DataCompareType.Contains;



            var supplierIDElem = $('#' + txtSupplierName);

            $('#' + txtSupplierName).click(function (e) {

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

        function bindItemList() {

            var cgColumns = [{ 'columnName': 'itemname', 'width': '200', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                             , { 'columnName': 'uomname', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Uom' }
                             , { 'columnName': 'itemcode', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Code' }
                             , { 'columnName': 'itemgroupdesc', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Group Name' }
                             , { 'columnName': 'class_name', 'width': '180', 'align': 'left', 'highlight': 4, 'label': 'Class Name' }
                             //, { 'columnName': 'item_type', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Item Type' }



            ];
            var serviceURL = ItemListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;


            serviceURL += "&ispaging=1&isigr=1";
            var groupIDElem = $('#' + txtItemName);

            $('#' + txtItemName).click(function (e) {
                $(groupIDElem).combogrid("dropdownClick");
            });


            $(groupIDElem).combogrid({
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
                width: 750,
                url: serviceURL,
                search: function (event, ui) {
                    var vgroupid = 0;
                    //var groupName = $('#' + txtGroupName).val();
                    //if (groupName != "") {
                    //    vgroupid = $('#' + hdnGroupID).val();
                    //    if (vgroupid == "0" || vgroupid == "") {
                    //        vgroupid = 0;
                    //    }
                    //} else {
                    //    $('#' + hdnGroupID).val('0');

                    //}
                    var newServiceURL = serviceURL;//+ "&groupid=" + vgroupid;

                    newServiceURL = JSUtility.AddTimeToQueryString(newServiceURL);
                    // var newServiceURL = serviceURL;
                    $(this).combogrid("option", "url", newServiceURL);


                },
                select: function (event, ui) {
                    if (!ui.item) {
                        event.preventDefault();

                        // $('#' + hdnDealerID).val('0');
                        //$('#' + txtDealerID).val('');
                        return false;
                        //ClearGLAccountData(elemID);
                    }


                    if (ui.item.dealerid == '') {
                        event.preventDefault();
                        return false;
                        //ClearGLAccountData(elemID);
                    }
                    else {
                        $('#' + hdnItemIdForFilter).val('0');
                        // $('#' + hdnDealerID).val(ui.item.dealerid);
                        $('#' + hdnItemIdForFilter).val(ui.item.itemid);
                        $('#' + txtItemName).val(ui.item.itemname);
                        //$('#' + txtGroupCode).val(ui.item.itemgroupcode);

                    }
                    return false;
                },

                lc: ''
            });


            $(groupIDElem).blur(function () {
                var self = this;

                var groupID = $(groupIDElem).val();
                if (groupID == '') {
                    $('#' + hdnItemIdForFilter).val('0');
                    $('#' + txtItemName).val('');
                    //$('#' + txtGroupCode).val('');
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
        td{
            padding:2px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="dvPageContent" style="width: 100%; height: auto;">
        <div id="dvContentHeader" class="dvContentHeader">
            <div id="dvHeader" class="dvHeader">
                <asp:Label ID="lblHeader" CssClass="lblHeader" runat="server" Text="MRR Update"></asp:Label>
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
                        <td></td>

                        <td align="right">
                            <asp:Label ID="lblMRRNo" runat="server" CssClass="" Text="MRR No:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMRRNo" runat="server" CssClass="form-control" Height="25px" Width="200"></asp:TextBox>
                        </td>

                      
                 
                           <td></td>
                        <td align="right">
                            <asp:Label ID="Label7" runat="server" Text="Item :"></asp:Label>

                        </td>
                        <td>
                            
                            <table>
                                <tr>
                                    <td> <asp:TextBox ID="txtItemName" Width="200px" Height="25px" runat="server" CssClass="form-control" Enabled="true"></asp:TextBox></td>
                                    <td> <input id="btnItemLoad" type="button" value="" Height="25px" runat="server" class="buttonDropdown" tabindex="-1" visible="false" /></td>
                                </tr>
                            </table>
                           
                           

                        </td>
                          <td colspan="2">
                               <asp:HiddenField ID="hdnItemIdForFilter" runat="server" />
                          </td>
                    </tr>
                    <tr>
                        <td></td>
                          <td align="right">
                            <asp:Label ID="Label6" runat="server" Text="Date From:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFromDate" runat="server" Height="25px" Width="200px" CssClass="textBox textDate dateParse"></asp:TextBox>
                        </td>
                        <td></td>
                          <td align="right">
                            <asp:Label ID="Label3" runat="server" CssClass="" Text="Supplier :" ></asp:Label>

                            </td>
                        <td>
                            <table>
                                <tr>
                                    <td> <asp:TextBox ID="txtSupplierName" runat="server" CssClass="form-control" Height="25px" Width="200px"></asp:TextBox></td>
                                    <td> <input id="btnSupplierID" type="button" Height="25px" value="" runat="server" class="buttonDropdown" tabindex="-1" visible="false" /></td>
                                </tr>
                            </table>
                           
                           
                        </td>
                       <td colspan="2"><asp:HiddenField ID="hdnSupplierID" runat="server" Value="0" /></td>

                    </tr>
                     <tr>
                                  
                        <td ></td>
                         <td align="right">
                          <asp:Label ID="lblToDate" runat="server" Text="Date To:"></asp:Label>
                          </td>
                         <td>
                          <asp:TextBox ID="txtToDate" runat="server" Height="25px" Width="200px" CssClass="textBox textDate dateParse"></asp:TextBox>
                         </td>
                         <td></td>
                      </tr>
                      <tr>
                        <td></td>
                        
                    </tr>

                    <tr>
                        <td></td>

                      
                        <td></td>
                        <td>
                            <asp:Button ID="btnRefresh" runat="server" CssClass="buttonRefresh" Style=""
                                Text="Search" OnClick="btnRefresh_Click" />
                        </td>
                        <td></td>
                        <td>&nbsp;
                        </td>
                        <td>&nbsp;
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                </table>
            </div>
            <div id="dvControls" style="width: 100%;">
                <div id="dvControlsInner" class="groupBoxContainer boxShadow" style="width: 1000px;">
                    <div id="dvGridContainer" style="width:1150px; height:auto; text-align: left; overflow-y:scroll;">
                        <div id="dvGridHeader" style="width: 100%; height: 25px; font-size: smaller;" class="subHeader">
                            <table style="height: 100%; color: #FFFFFF; font-weight: bold; text-align: center;"
                                class="defFont" cellspacing="1" cellpadding="1" rules="all">
                                <tr>
                                    <td width="50px" align="left">Action</td>
                                    <td width="140px" align="left">MRR No
                                    </td>
                                     <td width="140px" align="left">LC No
                                    </td>
                                    <td width="100px" align="left">MRR Date
                                    </td>
                                    <td width="160px" align="left">Item Name
                                    </td>
                                     <td width="50px" align="left">UOM
                                    </td>
                                    <td width="60px" align="left">Quantity</td>
                                    <td width="60px" align="left">Price</td>
                                    <td width="80px" align="left">Total Cost</td>
                                    <td width="170px" align="left">Supplier</td>
                                    <td width="120px" align="left">MRR By
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="dvGrid" style="width: auto; height: 250px; overflow: auto;">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="1" CssClass="table table-striped table-bordered table-condensed"
                                CellSpacing="1" ForeColor="#333333" GridLines="None" Font-Names="Arial" Font-Size="9pt"
                                DataKeyNames="MRR_ID" OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting" OnRowCommand="GridView1_RowCommand"
                                EmptyDataText="No Data Found" PageSize="15" OnPageIndexChanging="GridView1_PageIndexChanging"
                                OnSelectedIndexChanged="GridView1_SelectedIndexChanged" ShowHeader="False">
                                <PagerSettings Mode="NumericFirstLast" />
                                <RowStyle BackColor="#A6F7EF" ForeColor="#333333" />
                                <Columns>
                                  <%--  <asp:HyperLinkField HeaderText="" Text="EDIT">
                                        <ControlStyle Height="20px" Width="50px" />
                                        <ItemStyle Width="50px" /> F2F7B3
                                    </asp:HyperLinkField>--%>
                                    <asp:TemplateField HeaderText="Action"  ItemStyle-HorizontalAlign="Center">  
                                         <ItemTemplate>  
                                            <asp:LinkButton ID="lnkEdit"  CommandName="edititem"  CssClass="btn btn-info"  CommandArgument='<%# Bind("MRR_DET_ID") %>' runat="server" CausesValidation="false" ><i class="glyphicon glyphicon-edit" style="padding-right:4px;"></i>Edit</asp:LinkButton>  
                                             <asp:HiddenField ID="hdnItemID" runat="server" Value='<%# Bind("ITEM_ID") %>' />
                                              <asp:HiddenField ID="hdnStoreId" runat="server" Value='<%# Bind("STORE_ID") %>' />
                                         </ItemTemplate>  
                          
                                     </asp:TemplateField>  
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="hdnMRRId" runat="server" Text='<%# Bind("MRR_ID") %>' Style="text-align: center;" Width="80px"> </asp:Label>
                                             <asp:Label ID="lblLcNo" runat="server" Text='<%# Bind("LC_NO") %>' Style="text-align: center;" Width="80px"> </asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="MRR_NO" HeaderText="Purchase No" ItemStyle-Width="140px">
                                        <ItemStyle Width="140px"></ItemStyle>
                                    </asp:BoundField>
                                      <asp:BoundField DataField="LC_NO" HeaderText="LC No" ItemStyle-Width="140px">
                                        <ItemStyle Width="140px"></ItemStyle>
                                    </asp:BoundField>

                                    <asp:BoundField DataField="MRR_DATE" HeaderText="Date" DataFormatString="{0:dd-MMM-yyyy}"
                                        ItemStyle-Width="100">

                                        <ItemStyle Width="100px"></ItemStyle>
                                    </asp:BoundField>
                                   
                                 <%--   <asp:BoundField DataField="ITEM_NAME" ItemStyle-Width="160">

                                        <ItemStyle Width="160px"></ItemStyle>
                                    </asp:BoundField>--%>
                                     <asp:TemplateField HeaderText="Item Name" Visible="true" HeaderStyle-Width="160px" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemName" runat="server" Text='<%# Bind("ITEM_NAME") %>'  Width="160px"></asp:Label>

                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                     <asp:BoundField DataField="UOM_NAME" ItemStyle-Width="50">

                                        <ItemStyle Width="50px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="MRR_QTY" ItemStyle-Width="60">

                                        <ItemStyle Width="60px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="UNIT_PRICE" ItemStyle-Width="60">

                                        <ItemStyle Width="60px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TOTAL_COST" ItemStyle-Width="80">

                                        <ItemStyle Width="80px"></ItemStyle>
                                    </asp:BoundField>
                                     <asp:BoundField DataField="SUP_NAME" HeaderText="SUP_NAME" ItemStyle-Width="170px" />
                                    <asp:BoundField DataField="CREATE_BY_NAME" HeaderText="CREATE_BY_NAME" ItemStyle-Width="120px" />


                                </Columns>
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#999999" />
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            </asp:GridView>
                        </div>
                          <div id="myModalEditItem" class="modal fade" >  
            <div class="modal-dialog" style="max-width:600px; align-content:space-around;">  
                <div class="modal-content">  
                    <div class="modal-header">  

                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>  
                        <h4 class="modal-title">Item Rate Update</h4>  
                    </div>  
                    <div class="modal-body" style="overflow-y: scroll; max-height: 85%; margin-top: 10px; margin-bottom: 10px;">  
                        <asp:Label ID="Label4" runat="server" ClientIDMode="Static"></asp:Label>  
                        <asp:Label ID="lblMemoNocomm" runat="server" Visible="false" ></asp:Label>
                        <asp:HiddenField ID="hdnItemIDCriterion" runat="server" />
                         <asp:HiddenField ID="hdnQATEMPEntryID" runat="server" Value="0" />
                         <asp:HiddenField ID="hdnMRRDtlId" runat="server" Value="0" />
                        <asp:HiddenField ID="hdnStoreId" runat="server" Value="0" />
                   

                        <table id="tblQcModal" class="qcModal" runat="server"> 
                              <tr>
                                <td align="right" >
                                    <asp:Label ID="lblLcNo" runat="server" Font-Bold="true" CssClass="text-info" Text="LC No :" ></asp:Label>
                                </td>
                                <td >
                                     <asp:Label ID="txtLcNo" CssClass="text-success" Font-Bold="true" runat="server" Visible="true" ></asp:Label>
                                </td>
                            </tr>
                               <tr>
                                <td align="right" >
                                    <asp:Label ID="lblItemName" runat="server" Font-Bold="true" CssClass="text-info" Text="Item Name :" ></asp:Label>
                                </td>
                                <td >
                                     <asp:Label ID="lblItemNameText" CssClass="text-success" Font-Bold="true" runat="server" Visible="true" ></asp:Label>
                                </td>
                            </tr>
                     
                            <tr id="trQty" runat="server" visible="true" >
                                <td align="right">
                                    <asp:Label ID="lblQty" runat="server" CssClass="text-info" Text="MRR Quantity : " Font-Bold="true"  ></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox runat="server" ID="txtQty" Height="25px" Width="100px" CssClass="textBox textBoxReadOnly" Enabled="false"></asp:TextBox>
                                </td>
                           
                            </tr>

                             <tr id="trRate" runat="server" visible="true">
                                <td align="right">
                                    <asp:Label ID="lblItemRate" runat="server" Text="Item Rate : " Font-Bold="true" CssClass="text-info" ></asp:Label>
                                </td>
                                <td align="left">
                                 <asp:TextBox runat="server" ID="txtItemRate" Height="25px" Width="100px" CssClass="textBox" onchange="GetTotalamt()" CausesValidation="True" AutoCompleteType="Disabled" onkeypress="return isNumberKey(event,this);"></asp:TextBox>
                                    
                                </td>
                             
                            </tr>
                         
    

                              <tr id="trCost" runat="server" visible="true">
                                <td align="right">
                                    <asp:Label ID="lblTotalCost" runat="server" Text="Total Cost : " Font-Bold="true" CssClass="text-info" ></asp:Label>
                                </td>
                                <td align="left">
                                 <asp:TextBox runat="server" ID="txtTotalCost" Height="25px" Width="100px" CssClass="textBoxReadOnly" Enabled="false"></asp:TextBox>
                                    
                                </td>
                          
                            </tr>

                        </table>

        
                    </div>  
                    <div class="modal-footer"> 
                        <table>
                            <tr>
                        <td>
                     <asp:Button ID="btnSaveCriterion" runat="server" Text="Update" CssClass="buttonSave" OnClick="btnSaveCriterion_Click" Visible="true"   />
                   
                        <button type="button"  Class="buttonClose"  data-dismiss="modal">Close</button>  
                             </td>
                                </tr>
                          </table> 
                    </div>  
                </div>  
            </div>  
        </div>  
                        <div id="dvGridFooter" style="width: 100%; height: 25px; font-size: smaller;" class="subFooter">
                            <table style="height: 100%; width: 100%; font-weight: bold;" cellspacing="2" cellpadding="1"
                                rules="all">
                                <tr>
                                    <td align="left" style="width: 40%">
                                        <table>
                                            <tr>
                                                <td style="width: 2px;"></td>
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
                                                        <asp:Button ID="btnGridPageGoTo" runat="server" Text="Go" OnClick="btnGridPageGoTo_Click" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label2" runat="server" Text="Page Size:"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlGridPageSize" runat="server" CssClass="dropDownList" Width="50"
                                                            Height="18" AutoPostBack="True" OnSelectedIndexChanged="ddlGridPageSize_SelectedIndexChanged">
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
                                                        <asp:Label ID="Label1" runat="server" Text="Page:"></asp:Label>
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
                                                            OnClick="btnGridPageFirst_Click" ToolTip="First" />
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnGridPagePrev" runat="server" Text="" CssClass="btnGridPagePrev"
                                                            OnClick="btnGridPagePrev_Click" ToolTip="Previous" />
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnGridPageNext" runat="server" Text="" CssClass="btnGridPageNext"
                                                            OnClick="btnGridPageNext_Click" ToolTip="Next" />
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnGridPageLast" runat="server" Text="" CssClass="btnGridPageLast"
                                                            OnClick="btnGridPageLast_Click" ToolTip="Last" />
                                                    </td>
                                                    <td style="width: 2px;"></td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="dvContentFooter" class="dvContentFooter">
        </div>
    </div>
</asp:Content>
