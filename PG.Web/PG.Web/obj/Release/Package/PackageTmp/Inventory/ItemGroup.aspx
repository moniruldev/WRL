<%@ Page Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="ItemGroup.aspx.cs" Inherits="PG.Web.Inventory.ItemGroup" %>

<%@ Register Src="~/Controls/ItemGroupTree.ascx" TagName="ItemGroupTree" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<script src="../javascript/pg.accutility.js" type="text/javascript"></script>--%>
	<script src="../javascript/pg.ui.itemgrouptree.js?v=1.1" type="text/javascript"></script>

	<link href="../css/skin/ui.dynatree.css" rel="stylesheet" type="text/css" />
	<link href="../css/pg.ui.itemgrouptree.css?v=1.1" rel="stylesheet" type="text/css" />

	<script language="javascript" type="text/javascript">
// <!CDATA[

		var hdnCompanyID = '<%=hdnCompanyID.ClientID %>';

		var groupPopupID = '<%=dvPopupItemGroup.ClientID %>';
		var dvItemGroupID = '<%=dvItemGroup.ClientID %>';
		var txtItemGroupNameParent = '<%=txtItemGroupNameParent.ClientID %>';
		var hdnItemGroupIDParent = '<%=hdnItemGroupIDParent.ClientID %>';
		var hdnItemGroupParentKey = '<%=hdnItemGroupParentKey.ClientID %>';

	    var ItemGroupListServiceLink = '<%=this.ItemGroupListServiceLink%>';


  
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


$(document).ready(function () {
	//alert($('#' + ddlBalanceType).val());
    var cgColumns = [ { 'columnName': 'itemgroupdesc', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                     , { 'columnName': 'itemgroupcode', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Code' }
                     , { 'columnName': 'itemgroupnameparent', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Group Parent' }
                     ];

    var serviceURL = ItemGroupListServiceLink + "?isterm=1&includeempty=0&hasitem=0&ispaging=1&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;

    //alert('adfsafd');

    $groupPopup = $('#' + groupPopupID).ItemGroupTree({
		title: 'Select Item Group',
		autoLink: true,
		autoLinkUpdate: true,
		linkControlID: dvItemGroupID,
		highlightLink: true,
		keyboard: true,
        isSearch:true,
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



	//$(".dynaTree").dynatree("getTree").getNodeByKey("id4.3.2").activate();


});    //ready

function OpenItemGroupTree() {
    //document.body.style.cursor = 'wait';

    //alert('open group');
    //$("#" + groupPopupID).ItemGroupTree("show", itemGroupKey);

	if ($("#" + txtItemGroupNameParent).is(":disabled")) {
		$("#" + groupPopupID).ItemGroupTree("option", "enableSelect", false);
	}
	else {
		$("#" + groupPopupID).ItemGroupTree("option", "enableSelect", true);
	}

	if ($("#" + txtItemGroupNameParent).is(":disabled") == false) {
		var itemGroupKey = $("#" + hdnItemGroupParentKey).val();             
		document.body.style.cursor = 'wait';
		$("#" + groupPopupID).ItemGroupTree("show", itemGroupKey);
		document.body.style.cursor = 'auto';
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
    <div id="dvPageContent" style="width: 100%; height: auto;">
	<div id="dvContentHeader" class="dvContentHeader">
            <div id="dvHeader" class="dvHeader">
		<asp:Label ID="lblHeader" CssClass="lblHeader" runat="server" Text="Item Group"></asp:Label>
	</div>
	<!--Message Div -->
	<div id="dvMsg" runat="server" class="dvMessage">
		<asp:Label ID="lblMessage" runat="server" Font-Size="Small" Width="100%"> </asp:Label>
	</div>
	 <div id="dvHeaderControl" class="dvHeaderControl">
	 </div>
	</div>

	<div id="dvContentMain" class="dvContentMain">
            <div id="dvControls" style="height: auto; width: 100%">
		<div id="dvControlsInner" class="groupBoxContainer boxShadow">    
			 <div id="groupBox">
				  <div id="groupHeader" class="groupHeader">
                            <div style="width: 100%; height: 20px;">
						 <table>
							<tr>
							 <td>
                                            <div id="dvIconEditMode" class="iconView" runat="server"></div>
							 </td>
							 <td>
								<span>Item Group</span> 
							 </td>
							</tr>
						 </table>
						 
					  </div>
					  
				  </div>
                        <div id="groupContent" class="groupContent" style="width: 100%; height: 300px; overflow: auto;">
                            <div id="groupContenInner" style="width: 100%; height: auto; text-align: left;">
		  
			<table cellpadding="2" cellspacing="4">
				 <tr>
                                        <td></td>
				   <td>
                                            <asp:HiddenField ID="hdnIsSystem" runat="server" Value="0" />
				   
				   </td>
				   <td>
				   
                                            <asp:HiddenField ID="hdnCompanyID" runat="server" Value="0" />
				   
				   </td>

				   <td>
				   
                                            <asp:HiddenField ID="hdnItemGroupID" runat="server" Value="0" />
				   
				   </td>
				   
                                        <td></td>

				 </tr>


					 <tr>
                                        <td></td>
                                        <td align="right">
                                            <asp:Label ID="Label4" runat="server" Text="Item Group Parent :"></asp:Label>
				   </td>
                                        <td align="left">
					<div id="dvItemGroup" class="group_linkControl dvGroup" runat="server">
					   <table cellpadding="0" cellspacing="0" border="0">
						   <tr>
							   <td style="">
								   <asp:TextBox ID="txtItemGroupNameParent" runat="server" 
                                                                CssClass="group_linkText textBoxReadOnlyEdit fldRequired enableIsDirty" Width="350px"></asp:TextBox>
							   </td>
							   <td>
								   <div class="btnPopup">
								   </div>
							   </td>
							   <td>
								   <div class="btnClear">
								   </div>
							   </td>
						   </tr>
					   </table>
                                                <input type="hidden" id="hdnItemGroupIDParent" runat="server" value="0" />
                                                <input type="hidden" id="hdnItemGroupParentKey" runat="server" value="" />

                                                <input type="hidden" id="hdnItemGroupIDParentEdit" runat="server" value="0" />

				   </div>
				 
					 </td>
                                        <td align="right"></td>
                                        <td align="left"></td>
				 </tr> 

								  <tr>
                                        <td></td>
                                        <td style="" align="right">&nbsp;</td>
                                        <td style="" align="left"></td>
                                        <td style="" align="right">&nbsp;</td>
                                        <td style="" align="left">&nbsp;</td>
				 </tr>

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
                                        <td></td>
				 <td style="" align="right">
                                            <asp:Label ID="Label1" runat="server" Text="Item Group Name"></asp:Label>
				 </td>
				 <td style="" align="left">
                                            <asp:TextBox ID="txtName" runat="server" CssClass="textBox fldRequired enableIsDirty"
                                                Width="250px"></asp:TextBox>
				 </td>
                                        <td style="" align="right">&nbsp;</td>
                                        <td style="" align="left">&nbsp;</td>
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
				   <asp:Label id="Label3" runat="server" Text="Item Group Code" ></asp:Label>
				 </td>
				 <td align="left" >
					<asp:TextBox id="txtCode" runat="server" CssClass="textBox fldRequired enableIsDirty" width="164px" Enabled="false"></asp:TextBox>
				 </td>
				  <td align="right" >
					  </td>
				 <td align="left">
                  
				 </tr>
			   
				 <tr>

                                        <td align="right" style="vertical-align: top;" colspan="2">
                                            <asp:Label ID="Label9" runat="server" Text="Group Description:"></asp:Label>
				   </td>
                                        <td align="left" colspan="2">
                                            <asp:TextBox ID="txtDesc" runat="server" CssClass="textBox fldRequired enableIsDirty"
                                                Width="380px" Height="28px" TextMode="MultiLine"></asp:TextBox>
					 </td>



				 </tr>
				 
			 
				
				 <tr>
                                        <td></td>
                                        <td style="" align="right">&nbsp;</td>
                                        <td style="" align="left">&nbsp;</td>
                                        <td style="" align="right">&nbsp;</td>
                                        <td style="" align="left">&nbsp;</td>
				 </tr> 


<%--				  <tr>
				  <td>
				   </td>
				 <td style="" align="right">
				   
				   <asp:Label id="lblBalanceType" runat="server" Text="Balance Type"></asp:Label>
					  </td>
				 <td style="" align="left">
					 <asp:DropDownList ID="ddlBalanceType" runat="server" 
						 CssClass="dropDownList enableIsDirty" width="90">
						 <asp:ListItem Value="0" Selected="True">Debit</asp:ListItem>
						 <asp:ListItem Value="1">Credit</asp:ListItem>
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
				   <asp:Label id="lblIsGrossProfit" runat="server" Text="Is Gross Profit:"></asp:Label>
					  </td>
				 <td style="" align="left">
					 <asp:DropDownList ID="ddlIsGrossProfit" runat="server" 
						 CssClass="dropDownList enableIsDirty" width="90">
						 <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
						 <asp:ListItem Value="1">Yes</asp:ListItem>
					 </asp:DropDownList>
					  </td>
				  <td style="" align="right">
					  &nbsp;</td>
				 <td style="" align="left">
					 &nbsp;</td>
				 </tr> --%>

				 <tr>
                                        <td></td>
                                        <td style="" align="right">&nbsp;</td>
                                        <td style="" align="left">&nbsp;</td>
                                        <td style="" align="right">&nbsp;</td>
                                        <td style="" align="left">&nbsp;</td>
				 </tr> 

				  <%-- <tr>
				  <td>
				   </td>
				 <td style="" align="right">
				   <asp:Label id="Label5" runat="server" Text="Show As Ledger" ></asp:Label>
					  </td>
				 <td style="" align="left">
					 <asp:DropDownList ID="ddlShowAsLedger" runat="server" 
						 CssClass="dropDownList" Width="90">
						 <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
						 <asp:ListItem Value="1">Yes</asp:ListItem>
					 </asp:DropDownList>
					  </td>
				  <td style="" align="right">
					  &nbsp;</td>
				 <td style="" align="left">
					 &nbsp;</td>
				 </tr> --%>


			  <tr>
                                        <td></td>
				 <td style="" align="right">
				   
                                            <asp:Label ID="Label6" runat="server" Text="Item Group Status"></asp:Label>
					  </td>
				 <td style="" align="left">
					 <asp:DropDownList ID="ddlIsActive" runat="server" CssClass="dropDownList enableIsDirty" Width="90">
                          <asp:ListItem Value="1" Selected="True">Active</asp:ListItem>
						 <asp:ListItem Value="0">Inactive</asp:ListItem>
						
					 </asp:DropDownList>
					  </td>
                                        <td style="" align="right">&nbsp;</td>
                                        <td style="" align="left">&nbsp;</td>
				 </tr> 


			   <%--<tr>
				 <td>
				   </td>
				 <td align="right" >
				   <asp:Label id="Label8" runat="server" Text="GL Head Code (other system)" ></asp:Label>
				 </td>
				 <td align="left" >
					<asp:TextBox id="txtGLGroupCodeOS" runat="server" CssClass="textBox fldRequired enableIsDirty" 
						width="164px"></asp:TextBox>
				 </td>
				  <td align="right" >
					  </td>
				 <td align="left">
					 </td>
				 </tr>--%>


				   <tr>
                                        <td></td>
                                        <td style="" align="right">&nbsp;</td>
                                        <td style="" align="left">&nbsp;</td>
                                        <td style="" align="right">&nbsp;</td>
                                        <td style="" align="left">&nbsp;</td>
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
                                        <td></td>
                                        <td style="" align="right">&nbsp;</td>
                                        <td style="" align="left"></td>
                                        <td style="" align="right">&nbsp;</td>
                                        <td style="" align="left">&nbsp;</td>
				 </tr> 
			  </table>
			  </div>

			  </div>
				  <div id="groupFooter" class="groupFooter">
                            <div style="width: 100%; height: 12px;">
					  </div>
				  </div>
			</div>
		  </div>  
		</div>  
	</div>
	

	 <div id="dvContentFooter" class="dvContentFooter">
		 <table>
			  <tr>
                    <td></td>
				<td>
				   <asp:Button ID="btnAddNew" runat="server" Text="New" CssClass="buttonNew" 
                            OnClick="btnAddNew_Click" />
					<asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonCancel checkIsDirty" 
                            OnClick="btnCancel_Click" />
				</td>
				<td>
				  <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonSave" 
                            OnClick="btnSave_Click" />
					<asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="buttonEdit" 
                            OnClick="btnEdit_Click" />
				</td>
				<td>
				 <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="buttonDelete" 
                            OnClick="btnDelete_Click" />
				</td>
				
				<td>
				   <asp:Button ID="btnRefresh" runat="server" Text="Refresh" CssClass="buttonRefresh checkIsDirty" 
                            OnClick="btnRefresh_Click" />
				   </td>

				   
                    <td></td>
				

				 <td>
                        <input id="btnClose" type="button" runat="server" class="buttonClose" value="Close" onclick="if (ContentForm) { ContentForm.CloseForm(); }" />
				</td>


			  </tr>
		   </table>    
	
	</div>
	</div> 
	<div id="dvPopupItemGroup" class="dvPopupGroup" runat="server">
		<uc1:ItemGroupTree ID="ItemGroupTree1" runat="server" />
	</div>
</asp:Content>

