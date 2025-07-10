<%@ Page Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="Customer.aspx.cs" Inherits="PG.Web.Inventory.Customer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../javascript/pg.ui.itemgrouptree.js" type="text/javascript"></script>
	<link href="../css/skin/ui.dynatree.css" rel="stylesheet" type="text/css" />
	<link href="../css/pg.ui.itemgrouptree.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">
        // <!CDATA[
        var txtCustCode = '<%=txtCustomerName.ClientID%>';
        var btnCustID = '<%=btnCustID.ClientID%>';
        var CustomerListServiceLink = '<%=this.CustomerListServiceLink%>';

        //var isPageResize = true;

        //function PageResizeCompleted(pg, cntMain) {
        //    resizeContentInner(cntMain);

        //}

        //function resizeContentInner(cntMain) {
        //    var contHeight = $("#dvContentMain").height();
        //    var contHead = $("#dvControlsHead").height();
        //    var contFooter = $("#dvControlsFooter").height();

        //    var contInnerHeight = contHeight - contHead - contFooter - 5;
        //    $("#dvControls").height(contInnerHeight);

        //    $("#dvControlsInner").height(contInnerHeight - 10);


        //    $("#groupBox").height(contInnerHeight - 10);
        //    var groupHeight = $("#groupBox").height();
        //    var groupHeaderHeight = $("#groupHeader").height();
        //    var groupFooterHeight = $("#groupFooter").height();
        //    $("#groupContent").height(groupHeight - groupHeaderHeight - groupFooterHeight - 2);

        //}

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


            $("#groupBox").height(contInnerHeight - 10);
            var groupHeight = $("#groupBox").height();
            var groupHeaderHeight = $("#groupHeader").height();
            var groupFooterHeight = $("#groupFooter").height();
            $("#groupContent").height(groupHeight - groupHeaderHeight - groupFooterHeight - 2);

        }

        $(document).ready(function () {
            bindCustomerList();
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
            var serviceURL = CustomerListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.Contains;
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
                width: 500,
                url: serviceURL,
                search: function (event, ui) {
                    //var companyCode = $('#' + ddlCompany).val();
                    //var branchCode = $('#' + hdnBranch).val();
                    //var deptCode = $('#' + hdnDepartment).val();
                    //var locationid = $('#' + lblLocationID).val();
                    // var seid = $('#' + txtExecutiveID).val();
                   // var vgroupid = $('#' + hdnGroupID).val();
                    //var newServiceURL = serviceURL;
                    $(this).combogrid("option", "url", serviceURL);


                },
                //select: function (event, ui) {
                //    if (!ui.item) {
                //        event.preventDefault();

                //        //$('#' + hdnGroupID).val('0');
                //        //$('#' + hdnItemID).val('0');
                //        return false;
                //        //ClearGLAccountData(elemID);
                //    }


                //    if (ui.item.itemid == '') {
                //        event.preventDefault();
                //        return false;
                //        //ClearGLAccountData(elemID);
                //    }
                //    else {
                //        // $('#' + hdnDealerID).val(ui.item.dealerid);
                //        //$('#' + hdnItemID).val(ui.item.itemid);
                //        //$('#' + txtItemName).val(ui.item.itemname);

                //        //$('#' + hdnUomID).val(ui.item.itemid);
                //        //$('#' + hdnUomName).val(ui.item.uomname);

                //        //$('#' + txtGroupCode).val(ui.item.itemgroupcode);

                //    }
                //    return false;
                //},

                lc: ''
            }        );


            //$(customerIDElem).blur(function () {
            //    var self = this;

            //    var customerID = $(customerIDElem).val();
            //    if (customerID == '') {

            //        $('#' + txtCustCode).val('');
            //        $('#' + txtCustomerName).val('');
            //        $('#' + txtCustAddress).val('');
            //        $('#' + txtCustPhone).val('');

            //    }
            //});
        }


        //function tbopen(key) {
        //    if (!key) {
        //        key = '';
        //    }

        //    var url = "/Admin/SetPassword.aspx?uid=" + key
        //    //if (pageInTab == 1)
        //    if (ZForm.PageMode == Enums.PageMode.InTab) {

        //        var tdata = new xtabdata();
        //        tdata.linktype = Enums.LinkType.Direct;
        //        tdata.id = 6320;
        //        tdata.name = "SetPassword";
        //        //tdata.label = "User: " + userid;
        //        tdata.label = "Set Password";
        //        tdata.type = 0;
        //        tdata.url = url;
        //        tdata.tabaction = Enums.TabAction.InTabReuse;
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

        function fromParent(val1) {
            alert('this is called from parent: ' + val1);
        }

        function Button1_onclick() {
            //document.getElementById("btnSave").click();
            ///__doPostBack("<%= this.btnSave.UniqueID %>", "");
    __doPostBack("btnSave", "");
}

// ]]>
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <div id="dvPageContent" style="width:100%; height:100%;">
   
    <div id="dvContentHeader" class="dvContentHeader">  
        <div id="dvHeader" class="dvHeader" >
            <asp:Label ID="lblHeader" CssClass="lblHeader" runat="server" Text="Customer"></asp:Label>
        </div>
        <div id="dvMessage" class="dvMessage" >
            <asp:Label ID="lblMessage" runat="server" Text="" Font-Size="Small" Width="100%"></asp:Label>
        </div>
   </div>

   <div id="dvContentMain" class="dvContentMain"> 
    <div id = "dvControls" style="height:auto; width:100%">
         <div id="dvControlsInner" class="groupBoxContainer boxShadow">    
           <div id="groupBox">
             <div id="groupHeader" class="groupHeader">
                      <div style="width:100%;height:20px;">
                         <table>
                            <tr>
                             <td>
                                <div id="dvIconEditMode" class="iconView" runat="server" ></div>
                             </td>
                             <td>
                                <span>Customer</span> 
                             </td>
                            </tr>
                         </table>
                         
                      </div>
                      
                  </div>
             
             <div id="groupContent" class="groupContent" style="width:100%;height:400px; overflow:auto;">
               <div id="groupContenInner" style="width:100%;height:auto; text-align:left;">
                <table cellpadding="2" cellspacing="4">
               <%--<tr>
                 <td style="width:20px;">
                 </td>
                 <td>
                 
                 </td>
                 <td>
                 
                 </td>
               </tr>--%>
            <tr>
                 <td>
                 </td>
                 <td>
                  <asp:Label id="lblCustomerType" runat="server" Text="Customer Code" ></asp:Label>
                 </td>
                 <td>
                  <asp:TextBox id="txtCustomerCode" runat="server" CssClass="textBox" Width="300px" Height="25px"></asp:TextBox> 
                 </td>
               </tr>
            
              <tr>
                 <td>
                 </td>
                 <td>
                  <asp:Label id="lblCustomerName" runat="server" Text="Customer Name" ></asp:Label><span style="color: red">*</span>
                 </td>
                 <td>
                  <asp:TextBox id="txtCustomerName" runat="server" CssClass="textBox" Width="300px" Height="25px"></asp:TextBox>
                     <input id="btnCustID" type="button" value="" runat="server" class="buttonDropdown" style="display:none;"
                                    tabindex="-1" />
                 </td>
               </tr>

                <tr>
                 <td>
                 </td>
                 <td>
                   <asp:Label  id="lblCustomerAddress" runat="server" Text="Customer Address" ></asp:Label>
                 </td>
                 <td>
                  <%--  <asp:TextBox id="txtCustomerAddress" runat="server" Width="352px" CssClass="textBox"></asp:TextBox>--%>
                     <asp:TextBox id="txtCustomerAddress" TextMode="multiline" Columns="50" Rows="5" runat="server" Width="300px" CssClass="textBox"></asp:TextBox>
                 </td>
               </tr>

                <tr>
                 <td>
                 </td>
                 <td>
                  <asp:Label ID="lblCustMobile" runat="server" Text="Mobile" ></asp:Label>
                 </td>
                 <td>
                 <asp:TextBox id="txtCustMobile" runat="server" CssClass="textBox" Width="300px" Height="25px"></asp:TextBox>
                 </td>
               </tr>
            <tr>
                 <td>
                 </td>
                 <td>
                  <asp:Label ID="Label1" runat="server" Text="Another Mobile(if any)" ></asp:Label>
                 </td>
                 <td>
                 <asp:TextBox id="txtPhoneAnother" runat="server" CssClass="textBox" Width="300px" Height="25px"></asp:TextBox>
                 </td>
               </tr>
                    <tr>
                 <td>
                 </td>
                 <td>
                  <asp:Label ID="Label2" runat="server" Text="Email" ></asp:Label>
                 </td>
                 <td>
                 <asp:TextBox id="txtEmail" runat="server" CssClass="textBox" Width="300px" Height="25px"></asp:TextBox>
                 </td>
               </tr>
                     <tr>
              <td>
              </td>
              <td>
               <asp:Label ID="lblCountry" runat="server" Text="Country"></asp:Label>
             </td>
            <td>
              <asp:DropDownList ID="ddlCountry" runat="server" Width="305" Height="25px" CssClass="dropDownList enableIsDirty"> </asp:DropDownList>
            </td>
         </tr>



                    <tr>
                 <td>
                 </td>
                 <td>
                  <asp:Label ID="Label3" runat="server" Text="Contact Person Name" ></asp:Label>
                 </td>
                 <td>
                 <asp:TextBox id="txtContactPerson" runat="server" CssClass="textBox" Width="300px" Height="25px"></asp:TextBox>
                 </td>
               </tr>
                    <tr>
                 <td>
                 </td>
                 <td>
                  <asp:Label ID="Label4" runat="server" Text="Contact Person Phone" ></asp:Label>
                 </td>
                 <td>
                 <asp:TextBox id="txtContactPersonPhone" runat="server" CssClass="textBox" Width="300px" Height="25px"></asp:TextBox>
                 </td>
               </tr>
                    <tr>
                 <td>
                 </td>
                 <td>
                  <asp:Label ID="Label5" runat="server" Text="Cont. Per. Another Phone " ></asp:Label>
                 </td>
                 <td>
                 <asp:TextBox id="txtContactPersonPhoneAnother" runat="server" CssClass="textBox" Width="300px" Height="25px"></asp:TextBox>
                 </td>
               </tr>
                 <tr>
                 <td>
                 </td>
                 <td>
                  <asp:Label ID="lblCompanyID" runat="server" Text="Company Name " ></asp:Label>
                 </td>
                 <td>
                     <asp:DropDownList ID="ddlCompanyID" runat="server" CssClass="dropDownList enableIsDirty"  Width="305" Height="25px" ></asp:DropDownList>
                 <%--<asp:TextBox id="txtCompanyName" runat="server" CssClass="textBox" Width="300px" Height="25px"></asp:TextBox>--%>
                 </td>
               </tr>
            </table>

               </div>
             </div>

             <div id="groupFooter" class="groupFooter">
                      <div style="width:100%;height:12px;">
                      
                      </div>
                  </div>

            </div>
          </div>
      </div>  

        </div>  
       
   
      <div id="dvContentFooter" class="dvContentFooter">
           <table>
              <tr>
                <td>
                </td>
                <td>
                 <asp:Button ID="btnAddNew" runat="server" Text="Reset" CssClass="buttonNew" width="90px" Height="26px"
                onclick="btnReset_Click" />
                </td>
                <td>
                 <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonSave" 
                onclick="btnSave_Click" />
                </td>
                </tr> 
            </table>
        </div>
    </div> 
</asp:Content>

