<%@ Page Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="Item.aspx.cs" Inherits="PG.Web.Inventory.Item" %>

<%@ Register src="~/Controls/ItemGroupTree.ascx" tagname="ItemGroupTree" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<script src="../javascript/pg.accutility.js" type="text/javascript"></script>--%>
	<script src="../javascript/pg.ui.itemgrouptree.js" type="text/javascript"></script>

	<link href="../css/skin/ui.dynatree.css" rel="stylesheet" type="text/css" />
	<link href="../css/pg.ui.itemgrouptree.css" rel="stylesheet" type="text/css" />

	<script language="javascript" type="text/javascript">
	    // <!CDATA[

	    var hdnCompanyID = '<%=hdnCompanyID.ClientID %>';

	    var groupPopupID = '<%=dvPopupItemGroup.ClientID %>';
	    var dvItemGroupID = '<%=dvItemGroup.ClientID %>';
	    var txtCode = '<%=txtCode.ClientID %>';
	    var ItemListServiceLinkTop = '<%=this.ItemListServiceLink%>';
	    var txtItemGroupNameParent = '<%=txtItemGroupNameParent.ClientID %>';
	    var hdnItemGroupIDParent = '<%=hdnItemGroupIDParent.ClientID %>';
	    var hdnItemGroupParentKey = '<%=hdnItemGroupParentKey.ClientID %>';
	    var hdnGroupID = '<%=hdnGroupID.ClientID %>';
	    var btnGroupID = '<%=btnGroupID.ClientID %>';
	    var txtItemName = '<%=txtName.ClientID %>';
	    var btnItemID = '<%= btnItemID.ClientID%>';



	    var ItemGroupListServiceLink = '<%=this.ItemGroupListServiceLink%>';

	    
<%--		var ddlBalanceType = '<%=ddlBalanceType.ClientID %>';

		var lblIsGrossProfit = '<%=lblIsGrossProfit.ClientID %>';
		var ddlIsGrossProfit = '<%=ddlIsGrossProfit.ClientID %>';--%>

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

	    function bindGroupList() {
	        var cgColumns = [
                             //{ 'columnName': 'itemgroupid', 'width': '80', 'align': 'left', 'highlight': 2, 'label': 'Group ID' }
                             { 'columnName': 'itemgroupcode', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Code' }
                            , { 'columnName': 'itemgroupdesc', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                            , { 'columnName': 'itemgroupnameparent', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Group Parent' }
                            , { 'columnName': 'itemcode', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Next Item Code' }

	        ];
	        var serviceURL = ItemGroupListServiceLink + "?isterm=1&includeempty=0&hasitem=1&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;

	        serviceURL += "&ispaging=1";
	        var groupIDElem = $('#' + txtItemGroupNameParent);

	        $('#' + btnGroupID).click(function (e) {
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
	                    // $('#' + hdnDealerID).val(ui.item.dealerid);
	                    $('#' + hdnItemGroupIDParent).val(ui.item.itemgroupid);
	                    $('#' + txtItemGroupNameParent).val(ui.item.itemgroupdesc);
	                    //$('#' + txtGroupCode).val(ui.item.itemgroupcode);
	                    $("#" + txtCode).val(ui.item.itemcode);
	                }
	                return false;
	            },

	            lc: ''
	        });


	        $(groupIDElem).blur(function () {
	            var self = this;

	            var groupID = $(groupIDElem).val();
	            if (groupID == '') {
	                // $('#' + hdnDealerID).val('0');
	                $('#' + txtItemGroupNameParent).val('');
	                $('#' + hdnItemGroupIDParent).val('0');
	                $("#" + txtCode).val('');
	            }
	        });
	    }

	    function tbopen(key) {
	        if (!key) {
	            key = '';
	        }


	        var url = "/Admin/SetPassword.aspx?uid=" + key
	        //if (pageInTab == 1)
	        if (TabVar.PageMode == Enums.PageMode.InTab) {

	            var tdata = new xtabdata();
	            tdata.linktype = Enums.LinkType.Direct;
	            tdata.id = 6320;
	            tdata.name = "SetPassword";
	            //tdata.label = "User: " + userid;
	            tdata.label = "Set Password";
	            tdata.type = 0;
	            tdata.url = url;
	            tdata.tabaction = Enums.TabAction.InTabReuse;
	            tdata.selecttab = 1;
	            tdata.reload = 0;
	            tdata.param = "";



	            try {
	                window.parent.OpenMenuByData(tdata);
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

	    function tbopenSalInfo(key) {
	        if (!key) {
	            key = '';
	        }


	        var url = "/Master/EmpSalaryInfo.aspx?eid=" + key
	        //if (pageInTab == 1)
	        if (TabVar.PageMode == Enums.PageMode.InTab) {

	            var tdata = new xtabdata();
	            tdata.linktype = Enums.LinkType.Direct;
	            tdata.id = 6320;
	            tdata.name = "EmpSalaryInfo";
	            //tdata.label = "User: " + userid;
	            tdata.label = "Emp. Salary Sturture";
	            tdata.type = 0;
	            tdata.url = url;
	            tdata.tabaction = Enums.TabAction.InTabReuse;
	            tdata.selecttab = 1;
	            tdata.reload = 0;
	            tdata.param = "";

	            try {
	                window.parent.OpenMenuByData(tdata);
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
	    function bindItemListFilter() {

	        var cgColumns = [
                { 'columnName': 'itemcode', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Code' }
                             , { 'columnName': 'itemname', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                             , { 'columnName': 'itemgroupdesc', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Group' }
                             , { 'columnName': 'uomname', 'width': '40', 'align': 'left', 'highlight': 4, 'label': 'UOM' }
                              //, { 'columnName': 'class_name', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Class Name' }
                             , { 'columnName': 'item_type', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Item Type' }
	        ];

	        var itemServiceURL = ItemListServiceLinkTop + "?isterm=1&includeempty=0&iscodename=1&isIndentPurchase=1&codecomptype=" + Enums.DataCompareType.StartsWith;

	        itemServiceURL += "&ispaging=1";
	        var itemIDElem = $('#' + txtItemName);
	       
	        $('#' + btnItemID).click(function (e) {
	            $(itemIDElem).combogrid("dropdownClick");
	        });

	        $(itemIDElem).combogrid({
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
	            url: itemServiceURL,
	            search: function (event, ui) {
	                //var companyCode = $('#' + ddlCompany).val();
	                //var branchCode = $('#' + hdnBranch).val();
	                //var deptCode = $('#' + hdnDepartment).val();
	                //var locationid = $('#' + lblLocationID).val();
	                // var seid = $('#' + txtExecutiveID).val();
	                var vgroupid = $('#' + hdnGroupID).val();
	                var newServiceURL = itemServiceURL + "&groupid=" + vgroupid;
	                $(this).combogrid("option", "url", newServiceURL);


	            },
	            select: function (event, ui) {
	                if (!ui.item) {
	                    event.preventDefault();

	                    //$('#' + hdnGroupID).val('0');
	                    //$('#' + hdnItemID).val('0');
	                    return false;
	                    //ClearGLAccountData(elemID);
	                }


	                if (ui.item.itemid == '') {
	                    event.preventDefault();
	                    return false;
	                    //ClearGLAccountData(elemID);
	                }
	                else {
	                    // $('#' + hdnDealerID).val(ui.item.dealerid);
	                    //$('#' + hdnItemID).val(ui.item.itemid);
	                    //$('#' + txtItemName).val(ui.item.itemname);

	                    //$('#' + hdnUomID).val(ui.item.itemid);
	                    //$('#' + hdnUomName).val(ui.item.uomname);

	                    //$('#' + txtGroupCode).val(ui.item.itemgroupcode);

	                }
	                return false;
	            },

	            lc: ''
	        });


	        //$(itemIDElem).blur(function () {
	        //    var self = this;
	        //    var groupID = $(itemIDElem).val();
	        //    if (groupID == '') {
	        //        // $('#' + hdnDealerID).val('0');
	        //        $('#' + txtItemName).val('');
	        //        $('#' + hdnItemID).val('0');
	        //        //$('#' + txtGroupCode).val('');
	        //    }
	        //});
	    }


	    $(document).ready(function () {
	        //alert($('#' + ddlBalanceType).val());
	        var cgColumns = [{ 'columnName': 'itemgroupdesc', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                            , { 'columnName': 'itemgroupcode', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Code' }
                            , { 'columnName': 'itemgroupnameparent', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Group Parent' }
	        ];

	        var serviceURL = ItemGroupListServiceLink + "?isterm=1&includeempty=0&hasitem=0&ispaging=1&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;

	        $groupPopup = $('#' + groupPopupID).ItemGroupTree({
	            title: 'Select Item Group',
	            autoLink: true,
	            autoLinkUpdate: true,
	            linkControlID: dvItemGroupID,
	            highlightLink: true,
	            keyboard: true,
	            isSearch: true,
	            searchColumns: cgColumns,
	            searchServiceURL: serviceURL,

	            okclick: function (event, data) {
	                //alert('ok');
	                SetItemGroupData(data);
	                //OnGLGroupChange(data.glclassid, data.glgroupid);
	                //ContentForm.MakeControlIsDirty(txtGLGroupNameParent, true);
	            },
	            open: function (event, ui) {
	                // $("#dvGLGroup").addClass("dvGLGroupSelected");
	            },
	            close: function (event, ui) {
	                //            $("#dvGLGroup").removeClass("dvGLGroupSelected");
	                //            $('#' + ctlGLGroupText).focus();
	                //            $('#' + ctlGLGroupText).select();
	            }
	        });


	        $("#" + dvItemGroupID).find('.btnPopup').click(function (e) {
	            //alert('ok');
	            OpenItemGroupTree();
	            //$("#" + groupPopupID).GroupTree("show", '');
	        });

	        $("#" + txtItemGroupNameParent).keydown(function (e) {
	            switch (e.keyCode) {
	                case 46:  //delete
	                    ClearData();
	                    break;
	                case 8:  //backspace
	                    ClearData();
	                    e.preventDefault();
	                    break;
	                case 13:  //enter
	                    OpenItemGroupTree();
	                    e.preventDefault();
	                    break;

	            }

	            //delete 
	            if (e.keyCode == 46) {
	                //alert('delete');
	                ClearData();
	            }
	            // backspace
	            if (e.keyCode == 8) {
	                //alert('delete');
	                ClearData();
	                e.preventDefault();
	            }

	        });


	        $("#" + dvItemGroupID).find('.btnClear').click(function (e) {
	            ClearData();
	        });

	        //OnGLGroupChange(parseInt($("#" + hdnGLClassID).val()), parseInt($("#" + hdnGLGroupIDParent).val()));
	        bindGroupList();
	        bindItemListFilter();
	        //$(".dynaTree").dynatree("getTree").getNodeByKey("id4.3.2").activate();
	       

	    });    //ready

	    function OpenItemGroupTree() {


	        if ($("#" + txtItemGroupNameParent).is(":disabled")) {
	            $("#" + groupPopupID).ItemGroupTree("option", "enableSelect", false);
	        }
	        else {
	            $("#" + groupPopupID).ItemGroupTree("option", "enableSelect", true);
	        }

	        if ($("#" + txtItemGroupNameParent).is(":disabled") == false) {
	            var itemGroupKey = $("#" + hdnItemGroupParentKey).val();

	            $("#" + groupPopupID).ItemGroupTree("show", itemGroupKey);
	        }
	    }


	    function SetItemGroupData(data) {

	        $("#" + txtItemGroupNameParent).val(data.itemgroupnameshow);
	        $("#" + hdnItemGroupIDParent).val(data.itemgroupid);
	        $("#" + hdnItemGroupParentKey).val(data.itemgroupkey);
	      
	        

	    }



	    function ClearData() {
	        if ($('#' + txtItemGroupNameParent).is(":disabled") == false) {
	            $("#" + txtItemGroupNameParent).val('');
	            $("#" + hdnItemGroupIDParent).val('0');
	            $("#" + hdnItemGroupParentKey).val('');
	            ContentForm.MakeControlIsDirty(txtItmGroupNameParent, true);
	        }
	    }

	    function OnGLGroupChange(glClassID, glGroupIDParent) {
	        glClassID = glClassID || 0;
	        glGroupIDParent = glGroupIDParent || 0;
	        if (glGroupIDParent == 0) {
	            if (glClassID == AccUtility.GLClass.Income | glClassID == AccUtility.GLClass.Expense) {
	                $("#" + lblIsGrossProfit).css("visibility", "visible");
	                $("#" + ddlIsGrossProfit).css("visibility", "visible");
	                $("#" + ddlIsGrossProfit).val('0');
	            }
	            else {
	                $("#" + lblIsGrossProfit).css("visibility", "hidden");
	                $("#" + ddlIsGrossProfit).css("visibility", "hidden");
	                $("#" + ddlIsGrossProfit).val($("#" + hdnGLGroupParentIsGrossProfit).val());
	            }
	        }
	        else {
	            $("#" + lblIsGrossProfit).css("visibility", "hidden");
	            $("#" + ddlIsGrossProfit).css("visibility", "hidden");
	            $("#" + ddlIsGrossProfit).val($("#" + hdnGLGroupParentIsGrossProfit).val());
	        }


	        var balanceType = $("#" + hdnGLGroupParentBalanceType).val();
	        $("#" + ddlBalanceType).val(balanceType);

	    }

	    function fromParent(val1) {
	        alert('this is called from parent: ' + val1);
	    }

	    function Button1_onclick() {
	        //document.getElementById("btnSave").click();
	        ///__doPostBack("<%= this.btnSave.UniqueID %>", "");
	        __doPostBack("btnSave", "");
	    }


	    function btnSalaryInfo_onclick() {

	    }

	    function btnSalaryInfo_onclick() {

	    }

	    // ]]>
	</script>

	<style type="text/css">
	    .groupBoxContainer {
	        height: 100%;
	        width: 720px;
	        overflow: auto;
	        margin-left: 5px;
	        margin-top: 5px;
	    }



	    .dvGroup {
	        height: auto;
	        width: auto;
	    }

	    .dvPopupItemGroup {
	        display: none;
	        border: 0px solid black;
	        width: 0px;
	        height: 0px;
	    }


	    .textPopup1 {
	        font-family: Verdana, Arial, Helvetica, sans-serif;
	        border: 1px #1B68B8 solid;
	        BACKGROUND-COLOR: #FFFFFF;
	        COLOR: #000000;
	        FONT-SIZE: 11px;
	        WIDTH: 160px;
	        HEIGHT: 16px;
	        padding-left: 2px;
	    }
	</style>
	

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvPageContent" style="width:100%; height:auto;">
	<div id="dvContentHeader" class="dvContentHeader">
	<div id="dvHeader" class="dvHeader" >
		<asp:Label ID="lblHeader" CssClass="lblHeader" runat="server" Text="Item"></asp:Label>
	</div>
	<!--Message Div -->
	<div id="dvMsg" runat="server" class="dvMessage">
		<asp:Label ID="lblMessage" runat="server" Font-Size="Small" Width="100%"> </asp:Label>
	</div>
	 <div id="dvHeaderControl" class="dvHeaderControl">
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
								<span>Item</span> 
							 </td>
							</tr>
						 </table>
						 
					  </div>
					  
				  </div>
				  <div id="groupContent" class="groupContent" style="width:100%;height:500px; overflow:auto;">
				  <div id="groupContenInner" style="width:100%;height:auto; text-align:left;">
		  
			<table cellpadding="2" cellspacing="4">
				 <tr>
				   <td>
				   </td>
				   <td>
					   <asp:HiddenField ID="hdnIsSystem" runat="server" Value ="0" />
				   
				   </td>
				   <td>
				   
					   <asp:HiddenField ID="hdnCompanyID" runat="server" Value ="0" />
				   
                                    <asp:HiddenField ID="hdnGroupID" runat="server" />
				   
				   </td>

				   <td>
				   
					   <asp:HiddenField ID="hdnItemID" runat="server" Value ="0" />
				   
				   </td>
				   
				   <td>
				   </td>

				 </tr>


					 <tr>
				 <td>
				   </td>
				 <td align="right" >
				   <asp:Label id="Label4" runat="server" Text="Item Group:" ></asp:Label>
					 </td>
				 <td align="left" >
					<div id="dvItemGroup" class="group_linkControl dvGroup" runat="server">
					   <table cellpadding="0" cellspacing="0" border="0">
						   <tr>
							   <td style="">
								   <asp:TextBox ID="txtItemGroupNameParent" runat="server" 
									   CssClass="textBox textBoxReadOnlyEdit fldRequired enableIsDirty" Width="350px" 
										></asp:TextBox>
							   </td>
							   <td>
                                   <input id="btnGroupID" type="button" value="" runat="server" class="buttonDropdown" tabindex="-1" />
                           
								  <%-- <div class="btnPopup">
								   </div>--%>
							   </td>
							   <td>
								  <%-- <div class="btnClear">
								   </div>--%>
							   </td>
						   </tr>
					   </table>
					   <input type="hidden" ID="hdnItemGroupIDParent" runat="server"  value="0" />
					   <input type="hidden" ID="hdnItemGroupParentKey" runat="server"  value="" />

					   <input type="hidden" ID="hdnItemGroupIDParentEdit" runat="server"  value="0" />

				   </div>
				 
					 </td>
				  <td align="right">
					  </td>
				 <td align="left" >
					 </td>
				 </tr> 

		<%--						  <tr>
				 <td>
				   </td>
				 <td style="" align="right">
					 &nbsp;</td>
				 <td style="" align="left">
					 </td>
				  <td style="" align="right">
					  &nbsp;</td>
				 <td style="" align="left">
					 &nbsp;</td>
				 </tr>--%>

			<%--<tr>
				 <td>
				   </td>
				 <td align="right" >
				   <asp:Label id="Label2" runat="server" Text="GL Head Short Name" ></asp:Label>
				 </td>
				 <td align="left" >
					<asp:TextBox id="txtNameShort" runat="server" CssClass="textBox fldRequired enableIsDirty" 
						width="164px"></asp:TextBox>
				 </td>
				  <td align="right" >
					  </td>
				 <td align="left">
					 </td>
				 </tr>--%>
			
				 <tr>
				 <td>
				   </td>
				 <td style="" align="right">
				   <asp:Label id="Label1" runat="server" Text="Item Name:" ></asp:Label>
				 </td>
				 <td style="" align="left">
					<asp:TextBox id="txtName" runat="server" CssClass="textBox " 
						width="380px" MaxLength="200" ></asp:TextBox>
				 </td>
				  <td style="text-align: left" align="right">
					    <input id="btnItemID" type="button" value="" runat="server" class="buttonDropdown" tabindex="-1" style="display:none;" /></td>
				 <td style="" align="left">
					 &nbsp;</td>
				 </tr>

                <tr>
				 <td>
				   </td>
				 <td style="" align="right">
				   <asp:Label id="Label12" runat="server" Text="Alter. Item Name:" ></asp:Label>
				 </td>
				 <td style="" align="left">
					<asp:TextBox id="txtALTER_ITEM_NAME" runat="server" CssClass="textBox fldRequired enableIsDirty" 
						width="380px" MaxLength="200" ></asp:TextBox>
				 </td>
				  <td style="" align="right">
					  &nbsp;</td>
				 <td style="" align="left">
					 &nbsp;</td>
				 </tr>
				   <%--<tr>
				 <td>
				   </td>
				 <td style="" align="right">
				   <asp:Label id="Label7" runat="server" Text="GL Head Name Bangla" Visible="false" ></asp:Label>
				 </td>
				 <td style="" align="left">
					<asp:TextBox id="txtNameB" runat="server" CssClass="textBox fldRequired enableIsDirty" 
						width="250px" Visible="false" ></asp:TextBox>
				 </td>
				  <td style="" align="right">
					  &nbsp;</td>
				 <td style="" align="left">
					 &nbsp;</td>
				 </tr>--%>
			 
				
				  <tr>
				 <td>
				   </td>
				 <td align="right" >
				   <asp:Label id="Label3" runat="server" Text="Item Code:" ></asp:Label>
				 </td>
				 <td align="left" >
					<asp:TextBox id="txtCode" runat="server" CssClass="textBox fldRequired enableIsDirty" 
						width="164px" MaxLength="50"></asp:TextBox>
				 </td>
				  <td align="right" >
					  </td>
				 <td align="left">
					 </td>
				 </tr>
			   
				 <tr style="display:none;">
				 <td>
				   </td>
				 <td style="" align="right">
					<asp:Label ID="lblSND_ITEM_CODE" runat="server" Text="SND ITEM CODE :"></asp:Label>
				 </td>
				 <td style="" align="left">
                     <asp:TextBox id="txtSND_ITEM_CODE" runat="server" CssClass="textBox fldRequired enableIsDirty" 
						width="164px" MaxLength="50"></asp:TextBox>
					 </td>
				  <td style="" align="right">
					  &nbsp;</td>
				 <td style="" align="left">
					 &nbsp;</td>
				 </tr>
				 
                 <tr>
				 <td>
				   </td>
				 <td align="right" style="vertical-align:top;" >
				   <asp:Label id="Label9" runat="server"  Text="Item Description:" ></asp:Label>
				 </td>
				 <td align="left" >
					<asp:TextBox id="txtDesc" runat="server" CssClass="textBox fldRequired enableIsDirty" 
						width="380px" Height="28px" TextMode="MultiLine"></asp:TextBox>
				 </td>
				  <td align="right" >
					  </td>
				 <td align="left">
					 </td>
				 </tr>
				 <tr>
				  <td>
				   </td>
				 <td style="" align="right">
					 <asp:Label ID="Label10" runat="server" Text="SNS Type:"></asp:Label>
                     </td>
				 <td style="" align="left">					 
                     <asp:DropDownList ID="ddlItemSNS" runat="server" 
						 CssClass="dropDownList enableIsDirty" width="200">
						 <asp:ListItem Value="0" Selected="True">(Select Item Storable Type)</asp:ListItem>
					 </asp:DropDownList>
				 </td>
				  <td style="" align="right">
					  &nbsp;</td>
				 <td style="" align="left">
					 &nbsp;</td>
				 </tr> 


				  <tr>
				  <td>
				   </td>
				 <td style="" align="right">
				   
				   <asp:Label id="lblItemClass" runat="server" Text="Item Class:"></asp:Label>
					  </td>
				 <td style="" align="left">
					 <asp:DropDownList ID="ddlItemClass" runat="server" 
						 CssClass="dropDownList enableIsDirty" width="200">
						 <asp:ListItem Value="0" Selected="True">(Select Item Class)</asp:ListItem>
					 </asp:DropDownList>
					  </td>
				  <td    align="left">
					<asp:CheckBox ID="chkIS_PRIME" runat="server" Text="Prime Item " Visible="false" />
                  </td>
				 <td style="" align="left">
					 &nbsp;</td>
				 </tr> 
				 
								
				  <tr>
				  <td>
				   </td>
				 <td style="" align="right">
				   <asp:Label id="lblItemType" runat="server" Text="Item Type:" Visible="true"></asp:Label>
					  </td>
				 <td style="" align="left">
					 <asp:DropDownList ID="ddlItemType" runat="server" 
						 CssClass="dropDownList enableIsDirty" width="200" Visible="true">
						 <asp:ListItem Value="0" Selected="True">(Select Item Type)</asp:ListItem>
					 </asp:DropDownList>
					  </td>
				  <td style="" align="left">
                      <asp:CheckBox ID="chkFOR_PRODUCTION" runat="server" Text=""  Visible="true"/>
                      <asp:Label runat="server" ID="lblchkForProd" Text="For Production" Style="color:green; font-weight:bold;font-size:12px;"></asp:Label>
					  </td>
				 <td style="" align="left">
					 &nbsp;</td>
				 </tr> 

				  <tr>
				  <td>
				   </td>
				 <td style="" align="right">
				   <asp:Label id="Label5" runat="server" Text="Unit Of Measure:" ></asp:Label>
					  </td>
				 <td style="" align="left">
					 <asp:DropDownList ID="ddlUOM" runat="server" 
						 CssClass="dropDownList" Width="200">
						 <asp:ListItem Value="0" Selected="True">(select uom)</asp:ListItem>
						 
					 </asp:DropDownList>
					  </td>
				  <td style="" align="right">
					  &nbsp;</td>
				 <td style="" align="left">
					 &nbsp;</td>
				 </tr> 
                 <tr>
				  <td>
				   </td>
				 <td style="" align="right">
				   
				   <asp:Label id="lblStore" runat="server" Text="Store:" Visible="true"></asp:Label>
					  </td>
				 <td style="" align="left">
					<asp:DropDownList runat="server" ID="ddlStore" CssClass="dropDownList"></asp:DropDownList>
					  </td>
				  <td style="" align="right">
					  &nbsp;</td>
				 <td style="" align="left">
					 &nbsp;</td>
				 </tr> 


                  <tr>
				  <td>
				   </td>
				 <td style="" align="right">
				   
				   <asp:Label id="Label11" runat="server" Text="Safety Stock Level:" Visible="false"></asp:Label>
					  </td>
				 <td style="" align="left">
					<asp:TextBox ID="txtSafetyLevel" runat="server" CssClass="textBox" Visible="false"></asp:TextBox>
					  </td>
				  <td style="" align="right">
					  &nbsp;</td>
				 <td style="" align="left">
					 &nbsp;</td>
				 </tr> 

			  <tr>
				  <td>
				   </td>
				 <td style="" align="right">
				   
				   <asp:Label id="Label6" runat="server" Text="IS Active:"></asp:Label>
					  </td>
				 <td style="" align="left">
					 <asp:DropDownList ID="ddlIsActive" runat="server" 
						 CssClass="dropDownList enableIsDirty" width="90px">
						 <asp:ListItem Value="0">No</asp:ListItem>
						 <asp:ListItem Value="1" Selected="True">Yes</asp:ListItem>
					 </asp:DropDownList>
					  </td>
				  <td style="" align="right">
					  &nbsp;</td>
				 <td style="" align="left">
					 &nbsp;</td>
				 </tr> 

				   <tr>
				  <td>
				   </td>
				 <td style="" align="right">
                     <asp:Label id="Label13" runat="server" Text="IS Visible:"></asp:Label>
					  </td>
				 <td style="" align="left">
					  <asp:DropDownList ID="ddlIS_VISIBLE" runat="server" 
						 CssClass="dropDownList enableIsDirty" width="90px">
						 <asp:ListItem Value="0">No</asp:ListItem>
						 <asp:ListItem Value="1" Selected="True">Yes</asp:ListItem>
					 </asp:DropDownList>
				 </td>
				  <td style="" align="right">
					  &nbsp;</td>
				 <td style="" align="left">
					 &nbsp;</td>
				 </tr> 

                  <tr>
				 <td>
				   </td>
				 <td align="right" >
				   <asp:Label id="Label2" runat="server" Text="Re Order Level:" ></asp:Label>
				 </td>
				 <td align="left" >
					<asp:TextBox id="txtReOrderLevel" runat="server" CssClass="textBox textNumberOnly fldRequired enableIsDirty" 
						width="80px"></asp:TextBox>
				 </td>
				  <td align="right" >
					  </td>
				 <td align="left">
					 </td>
				 </tr>


                <tr>
				 <td>
				   </td>
				 <td align="right" >
				   <asp:Label id="Label7" runat="server" Text="Safety Stock Level:" ></asp:Label>
				 </td>
				 <td align="left" >
					<asp:TextBox id="txtSaftyStockLevel" runat="server" CssClass="textBox textNumberOnly fldRequired enableIsDirty" 
						width="80px"></asp:TextBox>
				 </td>
				  <td align="right" >
					  </td>
				 <td align="left">
					 </td>
				 </tr>


                 <tr>
				 <td>
				   </td>
				 <td align="right" >
				   <asp:Label id="Label8" runat="server" Text="Lead Time (Day):" ></asp:Label>
				 </td>
				 <td align="left" >
					<asp:TextBox id="txtLeadTime" runat="server" CssClass="textBox textNumberOnly fldRequired enableIsDirty" 
						width="80px"></asp:TextBox>
				 </td>
				  <td align="right" >
					  </td>
				 <td align="left">
					 </td>
				 </tr>


				<%--<tr>
				<td>
				   </td>
				 <td style="" align="right">
				   <asp:Label id="lblNamePredefined" runat="server" Text="Predefined Group Name:" ></asp:Label>
					</td>
				 <td style="" align="left">
					<asp:TextBox id="txtNamePredifined" runat="server" CssClass="textBox" 
						width="234px" ReadOnly="True" Enabled="False"></asp:TextBox>
					</td>
				  <td style="" align="right">
					  &nbsp;</td>
				 <td style="" align="left">
					 &nbsp;</td>
				 </tr>              --%>
						
                
                <tr>
				<td>
				   </td>
				 <td style="" align="right">
				   <asp:Label id="lblBrand" runat="server" Text="Brand Name:"  Visible="false"></asp:Label>
					</td>
				 <td style="" align="left">
					 <asp:DropDownList ID="ddlBrand" runat="server" 
						 CssClass="dropDownList enableIsDirty" width="190px" Visible="false" >
						  <asp:ListItem Value="0" Selected="True">(Select Brand)</asp:ListItem>
					 </asp:DropDownList>
					</td>
				  <td style="" align="right">
					  &nbsp;</td>
				 <td style="" align="left">
					 &nbsp;</td>
				 </tr>              
						<tr>
				<td>
				   </td>
				 <td style="" align="right">
				   <asp:Label id="lblCategory" runat="server" Text="Category Name:" Visible="false" ></asp:Label>
					</td>
				 <td style="" align="left">
					 <asp:DropDownList ID="ddlCategory" runat="server" 
						 CssClass="dropDownList enableIsDirty" width="190px" Visible="false" >
						 <asp:ListItem Value="0" Selected="True">(Select Battery Category)</asp:ListItem>
					 </asp:DropDownList>
					</td>
				  <td style="" align="right">
					  &nbsp;</td>
				 <td style="" align="left">
					 &nbsp;</td>
				 </tr>       			  
				  <tr>
				  <td>
				   </td>
				 <td style="" align="right">
					 &nbsp;</td>
				 <td style="" align="left">
					
				 
					 </td>
				  <td style="" align="right">
					  &nbsp;</td>
				 <td style="" align="left">
					 &nbsp;</td>
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
				   <asp:Button ID="btnAddNew" runat="server" Text="New" CssClass="buttonNew" 
						onclick="btnAddNew_Click" />
					<asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonCancel checkIsDirty" 
					 onclick="btnCancel_Click"   />
				</td>
				<td>
				  <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonSave" 
					onclick="btnSave_Click" />
					<asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="buttonEdit" 
						onclick="btnEdit_Click"   />
				</td>
				<td>
				 <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="buttonDelete" 
						onclick="btnDelete_Click"   />
				</td>
				
				<td>
				   <asp:Button ID="btnRefresh" runat="server" Text="Refresh" CssClass="buttonRefresh checkIsDirty" 
						onclick="btnRefresh_Click"   />
				   </td>

			   
				<td>
				   
				 </td>
				

				 <td>
					<input id="btnClose" type="button" runat="server" class="buttonClose" value="Close" onclick="if (ContentForm) { ContentForm.CloseForm(); }" />
				</td>


			  </tr>
		   </table>    
	
	</div>
	</div> 
	<div id="dvPopupItemGroup" class="dvPopupGroup" runat="server" style="display:none;">
		<uc1:ItemGroupTree ID="ItemGroupTree1" runat="server" />
	</div>


</asp:Content>

