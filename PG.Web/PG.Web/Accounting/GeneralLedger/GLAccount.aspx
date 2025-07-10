<%@ Page Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="GLAccount.aspx.cs" Inherits="PG.Web.Accounting.GeneralLedger.GLAccount" %>

<%@ Register src="../../Controls/GLGroupTree.ascx" tagname="GLGroupTree" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

	<script src="../../javascript/pg.accutility.js" type="text/javascript"></script>
	<script src="../../javascript/pg.ui.glgrouptree.js" type="text/javascript"></script>
	<script src="../../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
   
	<link href="../../css/pg.ui.glgrouptree.css" rel="stylesheet" type="text/css" />
	
	<link href="../../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />
	<link href="../../css/skin/ui.dynatree.css" rel="stylesheet" type="text/css" />

  


	<script language="javascript" type="text/javascript">
// <!CDATA[

		var GLAccountServiceLink = '<%=this.GLAccountServiceLink%>';
		var GLGroupServiceLink = '<%=this.GLGroupServiceLink%>'; 


		var groupPopupID = '<%=dvPopupGLGroup.ClientID %>';
		var dvGLGroupID = '<%=dvGLGroup.ClientID %>';

		var hdnCompanyID = '<%=hdnCompanyID.ClientID %>';
		var hdnGLAccountID = '<%=hdnGLAccountID.ClientID %>';


		var txtCode = '<%=txtCode.ClientID %>';

		var dvGLAccount = '<%=dvGLAccount.ClientID %>';
		var txtGLAccountCodeParent = '<%=txtGLAccountCodeParent.ClientID%>';
		var hdnGLAccountIDParent = '<%=hdnGLAccountIDParent.ClientID%>';
		var txtGLAccountNameParent = '<%=txtGLAccountNameParent.ClientID%>';

		var txtGLGroupName = '<%=txtGLGroupName.ClientID %>';
		var hdnGLGroupID = '<%=hdnGLGroupID.ClientID %>';
		var hdnGLClassID = '<%=hdnGLClassID.ClientID %>';
		var hdnGLGroupClassID = '<%=hdnGLGroupClassID.ClientID %>';
		var hdnGLGroupKey = '<%=hdnGLGroupKey.ClientID %>';

		var hdnBalanceType = '<%=hdnBalanceType.ClientID %>';

		var ddlBalanceType = '<%=ddlBalanceType.ClientID %>';

		var lblGLAccount = '<%=lblGLAccount.ClientID %>';

		var ddlAccountType = '<%=ddlAccountType.ClientID %>';

		var txtOpBalance = '<%=txtOpBalance.ClientID %>';
		var ddlOpBalDrCr = '<%=ddlOpBalDrCr.ClientID %>';


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


function tbopen(key)
{
	 if(!key)
	 {
	   key = '';
	 }
 
	
	var url = "/Admin/SetPassword.aspx?uid=" + key
	//if (pageInTab == 1)
	if (TabVar.PageMode == Enums.PageMode.InTab)
	{

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
	   
	   
							 
	   try
	   {                                          
		window.parent.OpenMenuByData(tdata);
	   }
	   catch(err)
	   {
		   alert("error in page");
	   }
   }
   else
   {
	  //on new window/tab
	   //window.open(url,'_blank');   
   
	   window.location = url;
   }
}


function tbopenAccRef(key, userid) {
	if (!key) {
		key = '';
	}
	var url = IForm.RootPath + "Accounting/GeneralLedger/GLAccountOpenAccRef.aspx?glaccid=" + key;

	//if (pageInTab == 1)
	if (TabVar.PageMode == Enums.PageMode.InTab) {

		var tdata = new xtabdata();
		tdata.linktype = Enums.LinkType.Direct;
		tdata.id = 0;
		tdata.name = "AccRef";
		//tdata.label = "User: " + userid;
		tdata.label = "Account Det Opening";
		tdata.type = 0;
		tdata.url = url;
		tdata.tabaction = Enums.TabAction.InNewTab;
		tdata.selecttab = 1;
		tdata.reload = 0;
		tdata.param = "";


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


$(document).ready(function () {
	$glPopup = $('#' + groupPopupID).GLGroupTree({
		title: 'Select GL Group',
		autoLink: true,
		autoLinkUpdate: true,
		linkControlID: dvGLGroupID,
		highlightLink: true,
		keyboard: true,
		allowTopNodeSelect: false,
		okclick: function (event, data) {
			SetGLGroupData(data);
			OnGLGroupChange(data.glclassid, data.glgroupid);
			ContentForm.MakeControlIsDirty(txtGLGroupName, true);
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

	$("#" + dvGLGroupID).find('.btnPopup').click(function (e) {
		OpenGLGroupTree();
	});

	$("#" + txtGLGroupName).keydown(function (e) {
		switch (e.keyCode) {
			case 46:  //delete
				ClearGLGroupData();
				break;
			case 8:  //backspace
				ClearGLGroupData();
				e.preventDefault();
				break;
			case 13:  //enter
				OpenGLGroupTree();
				e.preventDefault();
				break;
			case 40:  //down
				OpenGLGroupTree();
				e.preventDefault();
				break;
		}

		//delete 
		if (e.keyCode == 46) {
			//alert('delete');
			ClearGLGroupData();
		}
		// backspace
		if (e.keyCode == 8) {
			//alert('delete');
			ClearGLGroupData();
			e.preventDefault();
		}

	});


	$("#" + dvGLGroupID).find('.btnClear').click(function (e) {
		ClearGLGroupData();
	});


	//    $("#dvPopupGLAccount").GLAccountList({
	//        multiSelect: false,
	//        enableAccountType: false,
	//        normalAccount: false,
	//        controlAccount: true,
	//        subAccount: false,
	//        enableSelectButton: false,
	//        selectclick: function (event, data) {
	//            //alert("ID: " + data.EmpCode);
	//            SetGLAccountData(data)
	//            ContentForm.MakeControlIsDirty(txtGLAccountCodeParent, true);
	//        }
	//    }); //glaccount list

	//    $("#btnGLAccount").click(function () {
	//        OpenGLAccountSelection();
	//    });

	//    $("#btnGLAccountClear").click(function () {
	//        ClearDataGLAccount();
	//    });


	var cgColumns = [{ 'columnName': 'glacccode', 'width': '80', 'align': 'left', 'highlight': 2, 'label': 'Code' }
							 , { 'columnName': 'glaccname', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Name' }
							 , { 'columnName': 'glgroupname', 'width': '130', 'align': 'left', 'highlight': 0, 'label': 'Group' }
							 , { 'columnName': 'glacctypename', 'width': '100', 'align': 'left', 'highlight': 0, 'label': 'Type' }
							];


	//var glgroupclass = $('#' + hdnGLGroupClassList).val();

	var comapanyid = $('#' + hdnCompanyID).val();

	var serviceURL = GLAccountServiceLink + "?isterm=1&includeempty=1&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
	serviceURL += "&namecomptype=" + Enums.DataCompareType.Contains + "&ispaging=1";
	serviceURL += "&acctypefilter=" + Enums.GLAccountTypeFilter.ControlAccount
	serviceURL += "&companyid=" + comapanyid
	
	//serviceURL += "&glgroupid=" + glGroupID

	//serviceURL += "&glgroupclass=" + glgroupclass;


	$("#" + txtGLAccountCodeParent).combogrid({
		debug: true,
		searchButton: false,
		resetButton: false,
		alternate: true,
		munit: 'px',
		scrollBar: true,
		showPager: true,
		showError: true,
		colModel: cgColumns,
		width: 500,
		url: serviceURL,
		//"select item" event handler to set input field
		search: function (event, ui) {
			var glGroupID = $("#" + hdnGLGroupID).val();
			var newServiceURL = serviceURL + "&glgroupid=" + glGroupID
			$(this).combogrid("option", "url", newServiceURL);
		},

		select: function (event, ui) {
			//alert(ui.item.typename);
			//$(".txtComboGrid").val(ui.item.code);
			//elemID = $(elem).attr('id');
			//elemID = txtGLAccountCodeParent;

			if (!ui.item) {
				event.preventDefault();
				ClearGLAccountData();
				return false;
				//ClearGLAccountData(elemID);
			}
			
			if (ui.item.id == 0) {
				event.preventDefault();
				ClearGLAccountData();
			}
			else {
				SetGLAccountData(ui.item);
			}
			return false;
		}
	});

	$("#" + dvGLAccount).find('.btnPopup').click(function (e) {
		$("#" + txtGLAccountCodeParent).combogrid("dropdownClick");
	});


	$("#" + txtGLAccountCodeParent).keydown(function (e) {
		switch (e.keyCode) {
//            case 46:  //delete
//                ClearGLAccountData();
//                break;
//            case 8:  //backspace
//                ClearGLAccountData();
//                e.preventDefault();
//                break;
			//            case 13:  //enter 
			//                OpenGLAccountSelection(); 
			//                e.preventDefault(); 
			//                break; 
		}

	});

	$("#" + dvGLAccount).find('.btnClear').click(function (e) {
		ClearGLAccountData();
	});




	$("#" + ddlAccountType).change(function () {
		//alert($("#" + ddlAccountType).val());
		OnAccTypeChange($("#" + ddlAccountType).val());
	});


	OnAccTypeChange($("#" + ddlAccountType).val());

});          //doc ready

function OpenGLGroupTree() {
	if ($("#" + txtGLGroupName).is(":disabled")) {
		$("#" + groupPopupID).GLGroupTree("option", "enableSelect", false);
	}
	else {
		$("#" + groupPopupID).GLGroupTree("option", "enableSelect", true);
	}

	if ($("#" + txtGLGroupName).is(":disabled") == false) {
		var glGroupKey = $("#" + hdnGLGroupKey).val();

		$("#" + groupPopupID).GLGroupTree("show", glGroupKey);
	}
}


function GetGLGroup(grpCode) {
	var glGrp = null;
	var isError = false;
	var isComplete = false;
	//ajax call

	grpCode = grpCode || '';


	var companyid = $('#' + hdnCompanyID).val();

	var serviceURL = GLGroupServiceLink + "?code=" + grpCode;
	serviceURL += "&companyid=" + companyid;
	serviceURL += "&isnextacccode=1";


	var dummyVar = $.ajax({
		type: "GET",
		cache: false,
		async: false,
		dataType: "json",
		url: serviceURL,

		success: function (grpdata) {
			//            if (accdata.menu[0].count > 0) {
			//                menu = menudata.menu[0];
			//            }
			if (grpdata.rows.length > 0) {
				glGrp = grpdata.rows[0];
			}
		},
		complete: function () {
			if (!isError) {
				//return;
				//alert (menu.name);
			}
			isComplete = true;
		},
		error: function (XMLHttpRequest, textStatus, errorThrown) {
			isError = true;
			alert(textStatus);
		}
	});
	return glGrp;
}


function SetGLGroupData(data) {
	$("#" + txtGLGroupName).val(data.glgroupname);
	$("#" + hdnGLGroupID).val(data.glgroupid);
	$("#" + hdnGLClassID).val(data.glclassid);
	$("#" + hdnGLGroupClassID).val(data.glgroupclassid);
	$("#" + hdnGLGroupKey).val(data.glgroupkey);

	$("#" + hdnBalanceType).val(data.balancetype);


	if (IForm.EditMode = Enums.EditMode.Add) {
		var glGroup = GetGLGroup(data.glgroupcode);
		if (glGroup != null) {
		    $("#" + txtCode).val(glGroup.glaccountcodenext);
		}
	}
}



function ClearGLGroupData() {
	if ($('#' + txtGLGroupName).is(":disabled") == false) {
		$("#" + txtGLGroupName).val('');
		$("#" + hdnGLGroupID).val('0');
		$("#" + hdnGLClassID).val('0');
		$("#" + hdnGLGroupClassID).val('0');
		$("#" + hdnGLGroupKey).val('');
		ContentForm.MakeControlIsDirty(txtGLGroupName, true);
	}
}

function OnGLGroupChange(glClassID, glGroupIDParent) {
	//    glClassID = glClassID || 0;
	//    glGroupIDParent = glGroupIDParent || 0;
	//    if (glGroupIDParent == 0) {
	//        if (glClassID == AccUtility.GLClass.Income | glClassID == AccUtility.GLClass.Expense) {
	//            $("#" + lblIsGrossProfit).css("visibility", "visible");
	//            $("#" + ddlIsGrossProfit).css("visibility", "visible");
	//        }
	//        else {
	//            $("#" + lblIsGrossProfit).css("visibility", "hidden");
	//            $("#" + ddlIsGrossProfit).css("visibility", "hidden");
	//        }
	//    }
	//    else {
	//        $("#" + lblIsGrossProfit).css("visibility", "hidden");
	//        $("#" + ddlIsGrossProfit).css("visibility", "hidden");
	//    }

	var balanceType = $("#" + hdnBalanceType).val();
	$("#" + ddlBalanceType).val(balanceType);

	//var opBal = $("#" + txtOpBalance).val();

	var opBal = parseFloat(JSUtility.GetNumber($("#" + txtOpBalance).val()));


	if (opBal == 0) {
		$("#" + ddlOpBalDrCr).val(balanceType);
	}
}

function OpenGLAccountSelection() {
	if ($("#" + txtGLAccountCodeParent).is(":disabled") == false) {
		accCode = $("#" + txtGLAccountCodeParent).val();
		$("#dvPopupGLAccount").GLAccountList("setAccountType", AccUtility.GLAccountType.ControlAccount);

		if ($("#" + txtGLGroupID).is(":disabled") == false) {
			glGroupID = $("#" + hdnGLGroupID).val();
			$("#dvPopupGLAccount").GLAccountList("setGLGroup", glGroupID);
		}
		$("#dvPopupGLAccount").GLAccountList("show", accCode, true);
	}
}


function SetGLAccountData(data) {
	if ($("#" + txtGLAccountCodeParent).is(":disabled") == false) {
		$('#' + txtGLAccountCodeParent).val(data.glacccode);
		$('#' + hdnGLAccountIDParent).val(data.glaccid);
		$('#' + txtGLAccountNameParent).val(data.glaccname);

		$('#' + txtGLGroupName).val(data.glgroupname);
		$('#' + hdnGLGroupID).val(data.glgroupid);
		$('#' + hdnGLClassID).val(data.glclassid);
		$('#' + hdnGLGroupClassID).val(data.glgroupclassid);
		$('#' + hdnGLGroupKey).val(data.glgroupkey);

		ContentForm.MakeControlIsDirty(txtGLAccountCodeParent, true);
		ContentForm.MakeControlIsDirty(txtGLGroupName, true);
	}
}

function ClearGLAccountData() {
	if ($('#' + txtGLAccountCodeParent).is(":disabled") == false) {
		$('#' + txtGLAccountCodeParent).val('');
		$('#' + hdnGLAccountIDParent).val('0');
		$('#' + txtGLAccountNameParent).val('');
		ContentForm.MakeControlIsDirty(txtGLAccountCodeParent, true);
	}
}



function OnAccTypeChange(accTypeID) {
	if (accTypeID == AccUtility.GLAccountType.SubAccount) {
		$("#" + dvGLAccount).css("visibility", "visible");
		$("#" + lblGLAccount).css("visibility", "visible");
	}
	else {
		$("#" + dvGLAccount).css("visibility", "hidden");
		$("#" + lblGLAccount).css("visibility", "hidden");
	}
}


function fromParent(val1)
{
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
function btnOpenDet_onclick() {
	var accID = parseInt($("#" + hdnGLAccountID).val());

	if (accID > 0) {
		tbopenAccRef(accID);
	}
	else {
		alert('Please save first!');
	}
}

	</script>

	<style type="text/css">
	
	   .dvGroup
		{
		 width: 178px;
		 height: 20px; 
		 border: 1px solid lightgrey;
		 
		}
		
		.dvGLAccount
		{
			width: 178px;
			height: 40px; 
			border: 1px solid lightgrey;
		}
		
		.dvPopupGLGroup
		{
			display: none;    
			border: 0px solid black;
			width: 0px;
			height: 0px;
		}
		
		
	   .dvPopupGLAccount
	   {
		  display: none;    
		  border: 0px solid black;
		  width: 0px;
		  height: 0px;
	   }
		
		
		
		.textPopup
		{
			font-family: Verdana, Arial, Helvetica, sans-serif;
			border: 1px #1B68B8 solid;
			BACKGROUND-COLOR: #FFFFFF;
			COLOR: #000000;
			FONT-SIZE:11px;
			WIDTH: 140px;
			HEIGHT:16px;
			padding-left:2px;
		}

	</style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<div id="dvPageContent" style="width:100%; height:auto;">
	<div id="dvContentHeader" class="dvContentHeader">
	<div id="dvHeader" class="dvHeader" >
		<asp:Label ID="lblHeader" CssClass="lblHeader" runat="server" Text="GL Account/Sub Ledger"></asp:Label>
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
		<div id="dvControlsInner" class="groupBoxContainer boxShadow" style="width:700px;">    
			 <div id="groupBox">
				  <div id="groupHeader" class="groupHeader">
					  <div style="width:100%;height:20px;">
						 <table>
							<tr>
							 <td>
								<div id="dvIconEditMode" class="iconView" runat="server" ></div>
							 </td>
							 <td>
								<span>GL Account/Sub Ledger</span> 
							 </td>
							</tr>
						 </table>
						 
					  </div>
					  
				  </div>
				  <div id="groupContent" class="groupContent" style="width:100%;height:300px; overflow:auto;">
				  <div id="groupContenInner" style="width:100%;height:auto; text-align:left;">
				  
				  <table>
					 <tr>
						<td>
						
						</td>
						
						<td>

						   <table style="" border="0" cellpadding="2" cellspacing="4" >
							  <tr>
								<td>
								</td>
								<td>
									&nbsp;</td>
								<td>
									<asp:HiddenField ID="hdnCompanyID" runat="server" />
									<asp:HiddenField ID="hdnGLAccountID" runat="server" Value="0" />
								</td>
								<td>
								</td>
								<td>
								</td>
								</tr>

									   <tr>
									<td></td>
									<td align="right" valign="top" >
									<asp:Label id="lblGLGroup" runat="server" Text="GL Group:" ></asp:Label>
										</td>
									 <td align="left" >
										<div id="dvGLGroup" class="group_linkControl dvGroup" runat="server">
					<table cellpadding="0" cellspacing="0" style="">
						<tr>
							<td style="">
								<asp:TextBox ID="txtGLGroupName" runat="server" CssClass="group_linkText textBoxReadOnlyEdit" 
									style="font-size:8pt;" TabIndex="0" Width="250px" ></asp:TextBox>
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
					<input type="hidden" ID="hdnGLGroupID" runat="server"  value="0" />
					<input type="hidden" ID="hdnGLClassID" runat="server"  value="0" />
					<input type="hidden" ID="hdnGLGroupClassID" runat="server"  value="0" />
					<input type="hidden" ID="hdnGLGroupKey" runat="server"  value="" />
					<input type="hidden" ID="hdnBalanceType" runat="server"  value="0" />

					<input type="hidden" ID="hdnGLGroupIDEdit" runat="server"  value="0" />
					<input type="hidden" ID="hdnGLClassIDEdit" runat="server"  value="0" />
					<input type="hidden" ID="hdnGLGroupClassIDEdit" runat="server"  value="0" />

				</div>
				 
									</td>
									<td align="right">
										</td>
									<td align="left">
								   </td>
			   
									</tr> 

			<tr>
									<td>
							</td>

							<td style="" align="right">
							<asp:Label id="Label6" runat="server" Text="Ledger Type:" ></asp:Label>
								</td>
							<td style="" align="left">
								<asp:DropDownList ID="ddlAccountType" runat="server"  
									Width="180px" CssClass="dropDownList" 
						 
									>
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
								<asp:Label id="Label3" runat="server" Text="Ledger Code" ></asp:Label>
								</td>
								<td style="" align="left">
								<asp:TextBox id="txtCode" runat="server" CssClass="textBox" width="180px" Enabled="false"></asp:TextBox>
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
							<asp:Label id="Label1" runat="server" Text="Ledger Name" ></asp:Label>
							</td>
							<td align="left">
							<asp:TextBox id="txtName" runat="server" CssClass="textBox" 
								width="280px"></asp:TextBox>
							</td>
							<td align="right">
								</td>
							<td align="left">
								</td>
							</tr>
					  
				           <tr>
							<td>
							</td>
							<td align="right" >
							<asp:Label id="Label7" runat="server" Text="Ledger Name Bangla" Visible="false" ></asp:Label>
							</td>
							<td align="left">
							<asp:TextBox id="txtNameB" runat="server" CssClass="textBox" 
								width="280px" Visible="false"></asp:TextBox>
							</td>
							<td align="right">
								</td>
							<td align="left">
								</td>
							</tr>
				 
					   
				 
							  <tr>
								 <td>
			
								 </td>
			   
							 <td align="right" valign="top" >
				<asp:Label id="lblGLAccount" runat="server" Text="Sub Account Of:" ></asp:Label>
					</td>
							 <td align="left" >
					<div id="dvGLAccount" runat="server" class="dvGLAccount">
						<table cellpadding="0" cellspacing="0" style="width:100%;height:100%;">
							<tr>
								<td style="">
									<asp:TextBox ID="txtGLAccountCodeParent" runat="server" CssClass="textBox" 
									></asp:TextBox>
								</td>
								<td>
									<div id="btnGLAccount" class="btnPopup">
									</div>
								</td>
								<td>
									<div id="btnGLAccountClear" class="btnClear">
									</div>
								</td>
							</tr>
							<tr>
								<td colspan="3">
									<asp:TextBox ID="txtGLAccountNameParent" runat="server" CssClass="textBoxReadOnlyEdit" 
									></asp:TextBox>
								</td>
							</tr>
						</table>
						<asp:HiddenField ID="hdnGLAccountIDParent" runat="server" Value="0" />

					</div></td>
						  <td align="right">
				
						</td>
						<td align="left">
						</td>
							 </tr>
				
							  <tr>
								<td></td>
								<td style="" align="right">
									&nbsp;</td>
								<td style="" align="left">
					
					
					
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
									<asp:Label id="lblBalanceType" runat="server" Text="Balance Type:" ></asp:Label>
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
								<asp:Label id="lblNamePredefined" runat="server" Text="Predefined Account Name:" ></asp:Label>
								 </td>
								<td style="" align="left">
								<asp:TextBox id="txtNamePredifined" runat="server" CssClass="textBox" 
									width="234px" ReadOnly="True" Enabled="False"></asp:TextBox>
								 </td>
								<td style="" align="right">
									&nbsp;</td>
								<td style="" align="left">
					 
									</td>
								</tr> 

							<tr>
								<td>
								</td>
								<td style="" align="right">
				   
									&nbsp;</td>
								<td style="" align="left">
									&nbsp;</td>
								<td style="" align="right">
									&nbsp;</td>
								<td style="" align="left">
					 
									</td>
								</tr> 

						   <tr>
							<td>
							</td>
							<td style="" align="right">
								&nbsp;</td>
							<td style="" align="left">
												   &nbsp;</td>
							<td style="" align="right">
								&nbsp;</td>
							<td style="" align="left">
					 
								</td>
							</tr>  


						 </table>
						
						</td>
						
						


						<td style="width:10px">
						  
						
						</td> 

						<td valign="top">
						   <table style="" border="0" cellpadding="2" cellspacing="4" >
							 <tr style="height:20px;">
							   <td>
							   </td>
							   <td>
								
							   </td>
							   <td>
							   
							   </td>
							   <td>
							   
							   </td>
							 
							 </tr>
							 
							 <tr>
							   <td>
							   </td>
							   <td colspan="2">
								   <asp:Label id="Label2" runat="server" Text="Opening Balance:" Font-Bold="True" ></asp:Label>
							   </td>

							 
							 </tr>

							  <tr>
							   <td>
							   </td>
							   <td>
								   <asp:Label id="Label4" runat="server" Text="Year:" ></asp:Label>
							   </td>
							   <td>
								   <asp:DropDownList ID="ddlOpBalYear" runat="server" CssClass="dropDownList" style="width:100px;">
									   <asp:ListItem Value="0">(select)</asp:ListItem>
								   </asp:DropDownList>
							   </td>
							   <td>
							   
							   </td>
							 
							 </tr>

							  <tr>
							   <td>
							   </td>
							   <td>
								   <asp:Label id="Label5" runat="server" Text="Balance:" ></asp:Label>
							   </td>
							   <td>
								   <asp:TextBox ID="txtOpBalance" runat="server" CssClass="textBox textNumberOnly textDecimalFormat" style="width:100px;" Text="0" Enabled="false"></asp:TextBox>

							   </td>
							   <td>
								  <asp:DropDownList ID="ddlOpBalDrCr" runat="server" CssClass="dropDownList" style="width:40px;">
									   <asp:ListItem Value="0">Dr</asp:ListItem>
									   <asp:ListItem Value="1">Cr</asp:ListItem>
								   </asp:DropDownList>
							   </td>



							 
							 </tr>

							 <tr>
							   <td>
							   
							   </td>


							   <td>
							   
							   </td>

							   <td>
							   
							   
								   <input id="btnOpenDet" type="button" value="Opening Details" class="buttoncommon" style="width:100px;" onclick="return btnOpenDet_onclick()" /></td>
							 
							 </tr>
							 

						   </table>
						
						</td>

						<td>
						
						
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
					<input id="btnClose" type="button" runat="server" class="buttonClose" value="Close" onclick="if (ContentForm){ ContentForm.CloseForm();}" />
				</td>


			  </tr>
		   </table>    
	
	</div>
	</div> 

	<div id="dvPopupGLGroup" class="dvPopupGroup" runat="server">
		<uc1:GLGroupTree ID="GLGroupTree1" runat="server" />
	</div>
	


</asp:Content>

