<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="INVNewPurchaseAgainstIndent.aspx.cs" Inherits="PG.Web.Inventory.INVNewPurchaseAgainstIndent" %>

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

      <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js" type="text/javascript"></script> 
     <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css"/> 
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap-theme.min.css"/>  

    <script language="javascript" type="text/javascript">

        var ReportViewPageLink = '<%=this.ReportViewPageLink%>';
        var ReportViewPDFPageLink = '<%=this.ReportViewPDFPageLink%>';
        var ReportPrintPageLink = '<%=this.ReportPrintPageLink%>';
        var ReportPDFPageLink = '<%=this.ReportPDFPageLink%>';


        var SupplierListServiceLink = '<%=this.SupplierListServiceLink%>';

        var gridUpdatePanelIDDet = '<%=UpdatePanel1.ClientID%>';
        var gridViewIDDet = '<%=GridView2.ClientID%>';
        var updateProgressID = '<%=UpdateProgressTC.ClientID%>';


        var txtSupplierName = '<%=txtSupplierName.ClientID%>';
        var btnSupplierID = '<%=btnSupplierID.ClientID%>';
        var hdnSupplierID = '<%=hdnSupplierID.ClientID%>';


        var txtTotalAmount = '<%=txtTotalAmount.ClientID%>';
        var txtExVatAit = '<%=txtExVatAit.ClientID%>';
        var txtIncludingVat = '<%=txtIncludingVat.ClientID%>';
        var txtSpecialDiscount = '<%=txtSpecialDiscount.ClientID%>';
        var txtVatAIT = '<%=txtVatAIT.ClientID%>';
        


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


        //$(document).on('keyup', '.txtAmt', function () {

        //    OnTextChangegAmt(this);

        //});

        function Calculate(txt) {
         <%--    var totalamt = document.getElementById("<%= txtTotalAmount.ClientID%>").value;
            var discount = document.getElementById("<%= txtSpecialDiscount.ClientID%>").value;
            var exvat = parseFloat(totalamt) - parseFloat(discount)
            document.getElementById("<%= txtExVatAit.ClientID%>").value = parseFloat(totalamt) - parseFloat(discount);--%>


          var totalamt = $('#' + txtTotalAmount).val();
            var discount = $('#' + txtSpecialDiscount).val();
            var exvat = parseFloat(totalamt) - parseFloat(discount);
            $('#' + txtExVatAit).val(exvat);

           
        }

        function Calculate1() {
            var totalamt = $('#' + txtTotalAmount).val();
            var excludingvat = $('#' + txtExVatAit).val();
            var vatait = $('#' + txtVatAIT).val();
            if (excludingvat != 0) {
                var inclvat = parseFloat(excludingvat, 0) + parseFloat(vatait, 0);
            }
            else
            {
                var inclvat = parseFloat(totalamt, 0) + parseFloat(vatait, 0);
            }

            $('#' + txtIncludingVat).val(inclvat);

        }

        function text_changed() {
            var totalamt = $('#' + txtTotalAmount).val();
            var discount = $('#' + txtSpecialDiscount).val();
            var exvat = totalamt - discount;
            $('#' + txtExVatAit).val(exvat);
            //var excludingvat = $('#' + txtExVatAit).val();
            //var vatait = $('#' + txtVatAIT).val();
            //var inclvat = excludingvat + vatait;

            //$('#' + txtIncludingVat).val(inclvat);
        }

        function text_changed1() {
            //var totalamt = $('#' + txtTotalAmount).val();
            //var discount = $('#' + txtSpecialDiscount).val();
            //var exvat = totalamt - discount;
            //$('#' + txtExVatAit).val(exvat);
            var excludingvat = $('#' + txtExVatAit).val();
            var vatait = $('#' + txtVatAIT).val();
            var inclvat = excludingvat + vatait;

            $('#' + txtIncludingVat).val(inclvat);
        }

        

        function OnTextChangegAmt(texbx) {

            var totalamt = 0;
            var itemqty = 0;
            var itemrate = 0;
            var detRow = $(texbx).closest('tr.gridRow');

            itemqty = parseFloat($(detRow).find('input[id$="txtPurchaseQty"]').val());

            if (itemqty == '') {
                itemqty = 0;
            }

            itemrate = $(detRow).find('input[id$="txtITEMRate"]').val();
            if (itemrate == '') {
                itemrate = 0;
            }
            totalamt = parseFloat(itemqty).toFixed(4) * parseFloat(itemrate).toFixed(4);
            totalamt = parseFloat(totalamt).toFixed(4);
            $(detRow).find('input[id$="txtTotalAmt"]').val(totalamt);
            CalculateTotalAmt();

        }

        function CalculateTotalAmt() {


            var grid = document.getElementById("<%= GridView1.ClientID%>");
         
            var totalAmt = 0;
            var totalQty=0;
            for (var i = 0; i < grid.rows.length; i++) {

        
                totalAmt += parseFloat($("input[id*=txtTotalAmt]")[i].value);
                totalQty+=parseFloat($("input[id*=txtPurchaseQty]")[i].value);

            }

            document.getElementById("<%= txtTotalAmount.ClientID%>").value = totalAmt;
            document.getElementById("<%= txtTotPurQty.ClientID%>").value = totalQty;

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

        /* Tab javascript */
        function openCity(evt, cityName) {
            var i, tabcontent, tablinks;
            tabcontent = document.getElementsByClassName("tabcontent");
            for (i = 0; i < tabcontent.length; i++) {
                tabcontent[i].style.display = "none";
            }
            tablinks = document.getElementsByClassName("tablinks");
            for (i = 0; i < tablinks.length; i++) {
                //tablinks[i].className = tablinks[i].className.replace(" active", "#");
            }
            document.getElementById(cityName).style.display = "block";
            evt.currentTarget.className += " active";
        }

        $(function () {
            $("#tabs").tabs();
        });


    </script>
<style type="text/css">
  .FixedHeader {
    position: absolute;
    font-weight: bold;
 }  
  
    .modal-backdrop {
    z-index: 1020 !important;
    display:none;
}

    .auto-style1 {
        height: 42px;
    }

    /* Style the tab */
.tab {
    overflow: hidden;
    border: 1px solid #ccc;
    background-color: #f1f1f1;
}

/* Style the buttons inside the tab */
.tab button {
    background-color: inherit;
    float: left;
    border: none;
    outline: none;
    cursor: pointer;
    padding: 14px 16px;
    transition: 0.3s;
    /*font-size: 17px;*/
    
}

/* Change background color of buttons on hover */
.tab button:hover {
    background-color: #ddd;
}

/* Create an active/current tablink class */
.tab button.active {
    background-color: #ccc;
}

/* Style the tab content */
.tabcontent {
    display: none;
    padding: 6px 12px;
    border: 1px solid #ccc;
    border-top: none;
}
        .colourdisabletextBox {}
         .auto-style9 {
             /*width: 455px;*/
         }
  

</style> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div id="dvPageContent" style="width: 100%; height: 100%;">

        <div id="dvContentHeader" class="dvContentHeader">
            <div id="dvHeader" class="dvHeader" align="left">
                <asp:Label ID="lblHeader" runat="server" Text="Purchase Against Indent" CssClass="lblHeader" Font-Bold="true" Font-Size="15px"></asp:Label>
            </div>
            <div id="dvMessage" class="dvMessage">
                <asp:Label ID="lblMessage" runat="server" CssClass="lblMessage" Text="" Width="100%"></asp:Label>
            </div>

            <div id="dvHeaderControl" class="dvHeaderControl">
            </div>

        </div>

        <div id="dvContentMain" class="dvContentMain" align="center">
            <asp:HiddenField ID="hdnSupplierID" runat="server" Value="0" />
            <asp:HiddenField ID="hdnIndtID" runat="server" Value="0" />
             <asp:HiddenField ID="hdnPurchaseNo" runat="server" Value="" />
            <asp:Label ID="lblMode" runat="server" Visible="false"></asp:Label>
                 <div id="tabs" style="height:auto; background : url(../image/bg_greendot.gif) !important;">
                 <ul>
                 <li><a href="#tabs-1">MASTER DATA</a></li>
                  <li><a href="#tabs-2">TERMS AND CONDITIONS</a></li>
                 </ul> 
           <div id="tabs-1">
            <div id="dvControlsHead" style="height: auto; width: 100%;" align="center">
                <table border="0" cellspacing="4" cellpadding="2" align="center" style="width: 100%; font-size:12px;" id="tblItemEntry">

                    <tr>
                        <td align="right" class="auto-style2">
                            <asp:Label ID="Label5" runat="server"  CssClass="" Text="Purchase No :" Font-Bold="true"></asp:Label>
                        </td>
                        <td align="left" class="auto-style3">

                            <asp:TextBox ID="txtPurchaseNo" ReadOnly="true" runat="server" CssClass="textBox" Font-Bold="true" Width="200px" BackColor="#FFFFCC"></asp:TextBox>
                        </td>
                        <td align="right" class="auto-style2">
                            <asp:Label ID="Label1" runat="server" CssClass="" Text="Purchase Date :" Font-Bold="true"></asp:Label><span style="color: red">*</span>

                        </td>

                        <td align="left" class="auto-style3">

                            <asp:TextBox ID="txtPurchaseDate" runat="server" CssClass="textBox textDate" Width="120" Font-Bold="true"></asp:TextBox>
                        </td>
                        <td align="right">
                            <asp:Label ID="Label3" runat="server" CssClass="" Text="Supplier :" Font-Bold="true"></asp:Label><span style="color: red">*</span></td>
                        <td>
                            <asp:TextBox ID="txtSupplierName" runat="server" CssClass="textBox" Width="180px"></asp:TextBox>
                            <input id="btnSupplierID" type="button" value="" runat="server" class="buttonDropdown"
                                tabindex="-1" />
                        </td>
                    </tr>


                    <tr>
                        <td align="right" class="auto-style2">
                            <asp:Label ID="Label4" runat="server" CssClass="" Text="Remarks :" Font-Bold="true"></asp:Label>
                        </td>
                        <td colspan="3" align="left" class="auto-style2">
                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="textBox" TextMode="MultiLine" Height="30px" Width="300px"></asp:TextBox>

                        </td>
                        <td colspan="2">
                            <asp:CheckBox runat="server" ID="chkIsBusinessHApproval" Checked="false" Text=" Is Business Head Approval Needed" ForeColor="Green" Font-Bold="true" />
                        </td>

                    </tr>

                    <tr>
                        <td align="right" class="auto-style2">
                            <asp:Label ID="Label10" runat="server" CssClass="" Text="INDT No :" Font-Bold="true"></asp:Label>
                        </td>
                        <td align="left" class="auto-style3">
                            <asp:TextBox ID="txtINDTNo" runat="server" CssClass="textBox" ReadOnly="true" Font-Bold="true" Width="200px"></asp:TextBox>
                        </td>
                        <td align="right" class="auto-style2">
                            <asp:Label ID="Label11" runat="server" CssClass="" Text="INDT Date :" Font-Bold="true"></asp:Label>
                        </td>
                        <td align="left" class="auto-style3">

                            <asp:TextBox ID="txtINDTDate" runat="server" ReadOnly="true" CssClass="textBox" Font-Bold="true"></asp:TextBox>
                        </td>
                           <td align="right" class="auto-style2">
                            <asp:Label ID="lblDeliveryDate" runat="server" CssClass="" Text="Delivery Date :" Font-Bold="true"></asp:Label><span style="color: red">*</span>
                        </td>
                        <td align="left" class="auto-style3">

                            <asp:TextBox ID="txtDeliveryDate" runat="server" CssClass="textBox textDate" Font-Bold="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="auto-style1">
                            <asp:Label ID="Label2" runat="server" CssClass="" Text="INDT Remarks :" Font-Bold="true"></asp:Label>
                        </td>
                        <td colspan="" align="left" class="auto-style1">
                            <asp:TextBox ID="txtINDTRemarks" runat="server" CssClass="textBox" TextMode="MultiLine" Height="30px" Width="300px" Enabled="false"></asp:TextBox>

                        </td>
                         <td align="right" class="auto-style1">
                            <asp:Label ID="lblIndtFrom" runat="server" CssClass="" Text="Indent From :" Font-Bold="true"></asp:Label>
                        </td>
                        <td class="auto-style1">
                            <asp:HiddenField ID="hdnDeptId" runat="server" />
                            <asp:TextBox ID="txtINDTFrom" runat="server" ReadOnly="true" CssClass="textBox" Font-Bold="true" Enabled="false"></asp:TextBox>
                            <asp:HiddenField ID="hdnStoreID" runat="server" Value="0" />
                        </td>

                           <td align="right" class="auto-style1">
                            <asp:Label ID="lblDeliveryLocation" CssClass="" runat="server" Text="Delivery Location :" Font-Bold="true"></asp:Label>
                        </td>
                        <td class="auto-style1">
                          
                            <asp:TextBox ID="txtDeliveryLocation" runat="server"  CssClass="textBox" Font-Bold="true" TextMode="MultiLine" Height="30" Width="200px" Enabled="true"></asp:TextBox>
                        </td>
                    </tr>

                </table>
                </div>

               

                </div>
           <div id="tabs-2" >
              <div id="dvControlsTC" style="height: auto; width:750px;">
                <div id="dvControlsInnerTC" class="groupBoxContainer boxShadow" style="height: auto; width: 100%;">

                                <div id="Div1" runat="server" class="" style="width: 100%; text-align: center;">
                                      <%--  <span style="font-weight: bold">Purchase Terms And Condition</span>--%>
                                    </div>
                                    <div id="Div2" runat="server" class="" style="width: auto; height: auto; text-align: left">
                                        <div id="dvGridHeaderTC" style="width: 100%; height: 25px; font-size: smaller;" class="subHeader">
                                            <table style="height: 100%; color: #FFFFFF; font-weight: bold; text-align: center;"
                                                class="defFont" cellspacing="1" cellpadding="1">
                                                <tr class="headerRow">
                                                    <td width="50px" class="headerColLeft">SL#
                                                    </td>
                                                    <td width="650px" class="headerColLeft">Description
                                                    </td>
                                                   
                                                      <td width="30px" class="headerColCenter">Delete
                                                    </td>

                                                </tr>
                                            </table>
                                        </div>
                                        <div id="dvGridTC" style="width: 100%; height:100px; overflow: auto;" class="dvGrid">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" ShowHeader="False"
                                                        CellPadding="1" CellSpacing="1" ForeColor="#333333" GridLines="Both" OnRowDataBound="GridView2_RowDataBound" OnRowCreated="GridView2_RowCreated" OnRowCommand="GridView2_RowCommand" DataKeyNames="T_AND_C_ID"
                                                        EnableModelValidation="True" ClientIDMode="AutoID" OnRowDeleting="GridView2_RowDeleting">
                                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="SL#">
                                                                <ItemTemplate>
                                                             <asp:HiddenField ID="hdnPOTCId" runat="server" Value='<%# Bind("PO_T_AND_C_ID") %>' />
                                                             <asp:HiddenField ID="hdnPurchaseID" runat="server" Value='<%# Bind("PURCHASE_ID") %>' />
                                                            <asp:Label ID="lblItemSlNo" runat="server" Text='<%# Bind("SL_NO") %>' Style="text-align: center;" Width="50px"> </asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Battery Type" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <div>
                                                                        <table border="0" cellpadding="1" cellspacing="1">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td>
                                                                                     <asp:TextBox ID="txtTCDescription" runat="server" CssClass="textBox" Text='<%# Bind("DESCRIPTION") %>' Style="text-align: left;" Width="650px"></asp:TextBox>
                                                                                    </td>

                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </div>
                                                                    <div style="overflow: visible;">
                                                                    </div>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="10" />
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete" ShowHeader="false">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnDeleteRow" CssClass="buttonDeleteIcon" Height="16px" Width="16px"
                                                                        CommandName="delete" runat="server"> </asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField Visible="false">
                                                                <ItemTemplate>
                                                                    <div style="width: 10px;">
                                                                        <div>
                                                                            <div style="background-position: right center; height: 25px; cursor: pointer; background-image: url('../image/more.png'); background-repeat: no-repeat; text-align: left; vertical-align: middle;"
                                                                                onclick="togglePannelStatus(this)"
                                                                                title="More..">
                                                                                ...
                                                                            </div>
                                                                            <div style="display: none;">
                                                                                <div class="gridPanel" style="float: right; width: 0px; height: 0px;">
                                                                                    <div style="position: relative; height: 100%; width: 100%;">
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Size="Smaller" />
                                                        <EditRowStyle BackColor="#999999" />
                                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                    </asp:GridView>
                                                    <input id="Hidden1" type="hidden" runat="server" value="[]" />
                                                    <input id="Hidden2" type="hidden" runat="server" value="[]" />
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnNewRowTC" EventName="Click" />
                                                </Triggers>
                                            </asp:UpdatePanel>


                                        </div>
                                        <div id="divGridControlsTC" style="width: 100%; height: 25px; border-top: solid 1px #C0C0C0;">
                                            <table style="width: auto; height: 100%; text-align: center;" cellspacing="1" cellpadding="1"
                                                border="0">
                                                <tr>
                                                    <td style="width: 2px"></td>
                                                    <td style="width: 90px" align="left">
                                                        <asp:Button ID="btnNewRowTC" runat="server" CssClass="buttonNewRow" Text="" OnClientClick="ShowProgress()" OnClick="btnNewRowTC_Click" />
                                                    </td>
                                                    <td style="width: 20px;">
                                                        <asp:UpdateProgress ID="UpdateProgressTC" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                                            DisplayAfter="300">
                                                            <ProgressTemplate>
                                                                <asp:Image ID="imgProgressTC" runat="server" ImageUrl="~/image/loader.gif" />
                                                            </ProgressTemplate>
                                                        </asp:UpdateProgress>
                                                    </td>
                                                  

                                                    <td align="right">&nbsp;
                                                    </td>

                                                </tr>
                                            </table>
                                        </div>
                                    </div>
               

                </div>
            </div>

            </div>

            </div>

            <div id="dvControls" style="height: auto; width: 100%;">
                <div id="dvControlsInner" class="groupBoxContainer boxShadow" style="height: auto; width: 100%;">

                                <div id="groupDataHeaderCredit" runat="server" class="" style="width: 100%; text-align: center;">
                                        <span style="font-weight: bold">Purchase Against Indent Details</span>
                                    </div>
                                    <div id="dvGridContainer2" runat="server" class="" style="width: auto; height: auto; text-align: left">
                                        <div id="dvGridHeader2" style="width: 100%; height: 25px; font-size: smaller;" class="subHeader">
                                            <table style="height: 100%; color: #FFFFFF; font-weight: bold; text-align: center;"
                                                class="defFont" cellspacing="1" cellpadding="1">
                                                <tr class="headerRow">
                                                    <td width="50px" class="headerColLeft">SL#
                                                    </td>
                                                    <td width="250px" class="headerColLeft">Item Name
                                                    </td>
                                                    <td width="80px" class="headerColLeft">Item Type
                                                    </td>
                                                    <td width="80px" class="headerColLeft">Indt Qty
                                                    </td>
                                                 
                                                    <td width="80px" class="headerColLeft">Already Pur.
                                                    </td>
                                                    <td width="80px" class="headerColLeft">Pur. Qty
                                                    </td>
                                                    <td width="50px" class="headerColLeft">UOM
                                                    </td>
                                                    <td width="80px" class="headerColLeft">Rate
                                                    </td>
                                                    <td width="100px" class="headerColLeft">Total Amt.
                                                    </td>
                                                    <td width="50px" class="headerColCenter">Priority
                                                    </td>
                                                     <td width="100px" class="headerColCenter">Remarks
                                                    </td>
                                                      <td width="30px" class="headerColCenter">Delete
                                                    </td>
                                                      <td width="10px" class="headerColCenter">
                                                    </td>
                                                   <td width="50px" class="headerColCenter">
                                                       <asp:Button ID="btnAddNewItem" runat="server" CssClass="buttonNewRow"  OnClick="btnAddNewItem_Click" />

                                                   </td>

                                                </tr>
                                            </table>

                                        </div>
                                        <div id="dvGrid" style="width: 100%; height: 300px; overflow: auto;" class="dvGrid">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate> <%--Height="250"--%>
                                                    <div id="ScroolDiv" style="overflow: scroll; height:  265px; width: 100%;"> <%--Grid Scrol--%>
                                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowHeader="False"
                                                        CellPadding="1" CellSpacing="1" ForeColor="#333333" GridLines="Both" OnRowDataBound="GridView1_RowDataBound" OnRowCreated="GridView1_RowCreated" OnRowCommand="GridView1_RowCommand" DataKeyNames="ITEM_ID"
                                                        EnableModelValidation="True" ClientIDMode="AutoID" OnRowDeleting="GridView1_RowDeleting">
                                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="SL#">
                                                                <ItemTemplate>
                                                             <asp:HiddenField ID="hdnItemId" runat="server" Value='<%# Bind("ITEM_ID") %>' />
                                                            <asp:HiddenField ID="hdnIndtDetId" runat="server" Value='<%# Bind("INDT_DET_ID") %>' />
                                                            <asp:HiddenField ID="hdnPurchaseDetId" runat="server" Value='<%# Bind("PURCHASE_DET_ID") %>' />
                                                            <asp:HiddenField ID="hdnUomId" runat="server" Value='<%# Bind("UOM_ID") %>' />
                                                            <asp:Label ID="lblItemSlNo" runat="server" Text='<%# Bind("SLNO") %>' Style="text-align: center;" Width="50px"> </asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Battery Type" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <div>
                                                                        <table border="0" cellpadding="1" cellspacing="1">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td>
                                                                                     <asp:Label ID="lblItemName" runat="server" Text='<%# Bind("item_name") %>' Style="text-align: center; font-size:12px;" Width="250px"></asp:Label>
                                                                                    </td>
                                                                                   
                                                                                     <td>
                                                                                         <asp:Label ID="txtItemType" runat="server" Text='<%# Bind("ITEM_TYPE_CODE") %>' Style="text-align: center;" Width="80px"></asp:Label>
                                                                                         <asp:HiddenField runat="server" ID="hdnItemType" Value='<%# Bind("ITEM_TYPE_ID") %>'  />
                                                                                    </td>

                                                                                    <td>
                                                                                     <asp:Label ID="txtIndtQty" runat="server" Text='<%# Bind("INDT_QTY") %>' Style="text-align: center;" Width="80px"></asp:Label>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label ID="txtAlreadyPurchaseQty" runat="server" Text='<%# Bind("ALREADRY_PURCHASE_QTY") %>' Style="text-align: center;" Width="80px"></asp:Label>
                                                                                    </td>

                                                                                    <td>
                                                                                        <asp:TextBox ID="txtPurchaseQty" Text='<%# Bind("PURCHASE_QTY") %>' runat="server" onkeypress="return isNumberKey(event,this);" onchange="OnTextChangegAmt(this)" CssClass="textBox textNumberOnly" Style="text-align: center;" Width="80px"></asp:TextBox>  

                                                                                    </td>

                                                                                    <td>
                                                                                         <asp:Label ID="lblUnitName" runat="server" Text='<%# Bind("UOM_NAME") %>' Style="text-align: center;" Width="50px"></asp:Label>

                                                                                    </td>

                                                                                    <td>
                                                                                       <asp:TextBox ID="txtITEMRate" runat="server" CssClass="textBox textAutoSelect" Width="80" Text='<%# Bind("UNIT_RATE") %>' Style="" onkeypress="return isNumberKey(event,this);" onchange="OnTextChangegAmt(this)"  Enabled="true"></asp:TextBox>

                                                                                    </td>



                                                                                    <td>
                                                                                         <asp:TextBox ID="txtTotalAmt" runat="server" CssClass="textBox textAutoSelect" Width="100" Text='<%# Bind("UNIT_PRICE") %>'   Enabled="true"></asp:TextBox>
                                                                                        <%--<asp:Label ID="lblTotalAmt" runat="server" CssClass="textBox textAutoSelect" Width="100" Text='<%# Bind("total_amount") %>' Style=""></asp:Label>--%>
                                                                                    </td>

                                                                                    <td>
                                                                                       <asp:Label ID="lblIndtPriority" runat="server" Text='<%# Bind("INDT_PRIORITY") %>' Style="text-align: center;" Width="50px"></asp:Label>
                                                                                    </td>

                                                                                    <td>
                                                                                         <asp:TextBox ID="txtRemarks" TextMode="MultiLine" CssClass="textBox textAutoSelect" Text='<%# Bind("NOTE") %>' runat="server" Style="text-align: center;" Width="100px" Rows="1"></asp:TextBox>
                                                                                    </td>

                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </div>
                                                                    <div style="overflow: visible;">
                                                                    </div>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="10" />
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete" ShowHeader="false">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnDeleteRow" CssClass="buttonDeleteIcon" Height="16px" Width="16px"
                                                                        CommandName="delete" runat="server"> </asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField Visible="false">
                                                                <ItemTemplate>
                                                                    <div style="width: 10px;">
                                                                        <div>
                                                                            <div style="background-position: right center; height: 25px; cursor: pointer; background-image: url('../image/more.png'); background-repeat: no-repeat; text-align: left; vertical-align: middle;"
                                                                                onclick="togglePannelStatus(this)"
                                                                                title="More..">
                                                                                ...
                                                                            </div>
                                                                            <div style="display: none;">
                                                                                <div class="gridPanel" style="float: right; width: 0px; height: 0px;">
                                                                                    <div style="position: relative; height: 100%; width: 100%;">
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Size="Smaller" />
                                                        <EditRowStyle BackColor="#999999" />
                                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                    </asp:GridView>
                                                          
                                                        </div>

 





            <div id="myModalItemDetails" class="modal fade" >  
            <div class="modal-dialog" style="width:700px; align-content:space-around;"> <%-- --%>
                <div class="modal-content" style="width:700px;">  
                    <div class="modal-header" style="width:700px;">  
                        
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>  
                        <h4 class="modal-title">Item Details</h4>  
                    </div> 
                    <div class="col-md-12">
                        <asp:Label ID="lblIndtNo" runat="server" Text="Indt No : " Visible="true" ></asp:Label>
                        <asp:Label ID="lblIndtNoText" runat="server" Visible="true"  Font-Bold="true" ></asp:Label>
                    </div> 
                    <div class="modal-body" style="overflow-y:scroll; overflow-x:scroll; height:300px; width:690px; margin-top: 10px; margin-bottom: 10px;"> <%-- --%>
                        <asp:Label ID="Label9" runat="server" ClientIDMode="Static" Visible="false"></asp:Label>  
                       
                         <asp:HiddenField ID="hdnEditMode" runat="server" />
                 
                       <asp:GridView ID="grdItemDetails" runat="server" HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="Black"
            RowStyle-BackColor="White" AlternatingRowStyle-BackColor="#A1DCF2" AlternatingRowStyle-ForeColor="#000"
            BorderStyle="None" BorderWidth="5px" CellPadding="10"  GridLines="Vertical" CssClass="gridView"
            AutoGenerateColumns="false">
            <Columns>
                 <asp:TemplateField HeaderText="SELECT">  
                    <ItemTemplate> 
                       <asp:CheckBox ID="chkSelect" runat="server" Width="52px" onclick="" Checked="false"  />  
                     </ItemTemplate> 
                   <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" /> 
                 </asp:TemplateField>  
                 <asp:TemplateField HeaderText="SL" ItemStyle-Width="30px">   
                     <ItemTemplate>
                          <asp:Label ID="lblItemSlNo" runat="server" Text='<%# Bind("SLNO") %>' Style="text-align: center;" Width="50px"> </asp:Label>
                              
                      
                     </ItemTemplate>
                 </asp:TemplateField>
                <asp:TemplateField HeaderText="Item Name">
                    <ItemTemplate>
                        <asp:Label ID="lblItemName" runat="server" Text='<%# Bind("item_name") %>' Style="text-align: center; font-size:12px;" Width="250px"></asp:Label>
                         <asp:HiddenField ID="hdnItemId" runat="server" Value='<%# Bind("ITEM_ID") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="Indt. Qty">
                    <ItemTemplate>
                       <asp:Label ID="txtIndtQty" runat="server" Text='<%# Bind("INDT_QTY") %>' Style="text-align: center;" Width="80px"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                      <asp:TemplateField HeaderText="Pur. Qty">
                    <ItemTemplate>
                        <asp:TextBox ID="txtPurchaseQty" Text='<%# Bind("PURCHASE_QTY") %>' runat="server" onkeypress="return isNumberKey(event,this);"  CssClass="textBox textNumberOnly" Style="text-align: center;" Width="80px"></asp:TextBox>  
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Rate">
                    <ItemTemplate>
                         <asp:TextBox ID="txtITEMRate" runat="server" CssClass="textBox textAutoSelect" Width="80" Text='<%# Bind("UNIT_RATE") %>' Style="" onkeypress="return isNumberKey(event,this);"   Enabled="true"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="UOM">
                    <ItemTemplate>
                         <asp:Label ID="lblUnitName" runat="server" Text='<%# Bind("UOM_NAME") %>' Style="text-align: center;" Width="50px"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                
          
            </Columns>
        </asp:GridView>
 
                    </div>  
                
                    <div class="modal-footer"> 
                        <table>
                            <tr>
                        <td>
                         <asp:Button ID="btnItemAdd" runat="server" Text="Add" CssClass="buttonSave" OnClientClick="return ValidateBatchStock();" OnClick="btnItemAdd_Click"  Visible="true" />
                         <button type="button"  Class="buttonClose"  data-dismiss="modal">Close</button> 
                            
                             </td>
                                </tr>
                          </table> 
                    </div>  
                </div>  
            </div>  
        </div>  
                                                    <input id="hdnJournalDetRefJson2" type="hidden" runat="server" value="[]" />
                                                    <input id="hdnJournalDetInsJson2" type="hidden" runat="server" value="[]" />

                                                 <table >

                                                        
                                                        <tr>
                                                            <td align="right">&nbsp; </td>
                                                            <td align="right">
                                                                <asp:Label ID="Label7" runat="server" Font-Bold="True" Text="Total Qnty:"></asp:Label>
                                                            </td>
                                                            <td align="right">
                                                                <asp:TextBox ID="txtTotPurQty" runat="server" CssClass="textBox" Font-Bold="True" Style="text-align: right;" TabIndex="-1" Width="90"></asp:TextBox>
                                                                &nbsp; </td>
                                                            <td align="right" style="padding-left:8px">
                                                                <asp:Label ID="Label8" runat="server" Font-Bold="True" Text="Total Amount:"></asp:Label>
                                                            </td>
                                                            <td align="right">
                                                                <asp:TextBox ID="txtTotalAmount" runat="server" CssClass="textBox" Font-Bold="True" Style="text-align: right;" TabIndex="-1" Width="90"></asp:TextBox>
                                                            </td>
                                                            <td align="right" style="padding-left:8px">
                                                                <asp:Label ID="lblSpecialDiscount" runat="server" Font-Bold="True" Text="SP Dis. Amt:"></asp:Label>
                                                            </td>
                                                            <td align="right">
                                                                <asp:TextBox ID="txtSpecialDiscount" runat="server" CssClass="textBox" Font-Bold="True" onchange="Calculate()" Style="text-align: right;"  Width="90"></asp:TextBox>
                                                               </td>
                                                            <td align="right" style="padding-left:8px">
                                                                <asp:Label ID="lblExcludingVat" runat="server" Font-Bold="True" Text="Ex. VAt &amp; AIT:"></asp:Label>
                                                            </td>
                                                            <td align="right">
                                                                <asp:TextBox ID="txtExVatAit" runat="server" CssClass="textBox" Font-Bold="True" Style="text-align: right;" TabIndex="-1" Width="90"></asp:TextBox>
                                                            </td>
                                                            <td align="right" style="padding-left:8px">
                                                                <asp:Label ID="lblVatAIT" runat="server" Font-Bold="True" Text="VAt &amp; AIT:"></asp:Label>
                                                            </td>
                                                            <td align="right">
                                                                <asp:TextBox ID="txtVatAIT" runat="server" CssClass="textBox" Font-Bold="True" onchange="return Calculate1();" Style="text-align: right;" Width="90"></asp:TextBox>
                                                                </td>
                                                            <td align="right" style="padding-left:8px">
                                                                <asp:Label ID="lblIncludingVAT" runat="server" Font-Bold="True" Text="Incl. VAt &amp; AIT:"></asp:Label>
                                                            </td>
                                                            <td align="right">
                                                                <asp:TextBox ID="txtIncludingVat" runat="server" CssClass="textBox" Font-Bold="True" Style="text-align: right;" TabIndex="-1" Width="90"></asp:TextBox>
                                                            </td>
                                                        </tr>

                                                    </table>

                                                </ContentTemplate>
                                              <%--  <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnNewRow2" EventName="Click" />
                                                </Triggers>--%>
                                            </asp:UpdatePanel>


                                        </div>
                                        <div id="divGridControls2" style="width: 100%; height: 25px; border-top: solid 1px #C0C0C0;">
                                            <table style="width: auto; height: 100%; text-align: center;" cellspacing="1" cellpadding="1"
                                                border="0">
                                                <tr>
                                                    <td style="width: 2px"></td>
                                                    <td style="width: 90px" align="left">
                                                        <%--<asp:Button ID="btnNewRow2" runat="server" CssClass="buttonNewRow" Text="" OnClientClick="ShowProgress()" OnClick="btnNewRow2_Click" />--%>
                                                    </td>
                                                    <td style="width: 20px;">
                                                        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                                            DisplayAfter="300">
                                                            <ProgressTemplate>
                                                                <asp:Image ID="imgProgress2" runat="server" ImageUrl="~/image/loader.gif" />
                                                            </ProgressTemplate>
                                                        </asp:UpdateProgress>
                                                    </td>
                                                  

                                                    <td align="right">&nbsp;
                                                    </td>

                                                </tr>
                                            </table>
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
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="buttonDelete" />
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
                        <asp:Button ID="btnPrintPreview" runat="server" Visible="true" Text="Preview PO" CssClass="buttoncommon buttonPrintPreview" Enabled="True" OnClick="btnPrintPreview_Click" />
                    </td>

                    <td>
                        <asp:Button ID="btnMRRPrint" runat="server" Visible="false" Text="Print PO" CssClass="buttoncommon buttonPrint" Enabled="True" OnClick="btnMRRPrint_Click" />
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
