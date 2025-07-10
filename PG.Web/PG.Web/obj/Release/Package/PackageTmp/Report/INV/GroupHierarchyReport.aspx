<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="GroupHierarchyReport.aspx.cs" Inherits="PG.Web.Report.INV.GroupHierarchyReport" %>
<%@ Register src="~/Controls/ItemGroupTree.ascx" tagname="ItemGroupTree" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">  
	
    <script src="../../javascript/pg.ui.itemgrouptree.js" type="text/javascript"></script>
	<link href="../../css/skin/ui.dynatree.css" rel="stylesheet" type="text/css" />
	<link href="../../css/pg.ui.itemgrouptree.css" rel="stylesheet" type="text/css" />

	<script language="javascript" type="text/javascript">
	    // <!CDATA[
	    var ReportViewPageLink = '<%=this.ReportViewPageLink%>';
	    var ReportViewPDFPageLink = '<%=this.ReportViewPDFPageLink%>';
	    var ifPrintButton = '<%=ifPrintButton.ClientID%>';
	    var ItemGroupListServiceLink = '<%=this.ItemGroupListServiceLink%>';

	    var hdnCompanyID = '<%=hdnCompanyID.ClientID %>';
	    var groupPopupID = '<%=dvPopupItemGroup.ClientID %>';
	    var dvItemGroupID = '<%=dvItemGroup.ClientID %>';
	    var txtItemGroupNameParent = '<%=txtItemGroupNameParent.ClientID %>';
	    var hdnItemGroupIDParent = '<%=hdnItemGroupIDParent.ClientID %>';
	    var hdnItemGroupParentKey = '<%=hdnItemGroupParentKey.ClientID %>';


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


	    function tbopen(key, isPrint, isPDFAutoPrint, showWait) {
	        key = key || '';
	        isPrint = isPrint || false;
	        showWait = showWait || true;
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
	        //alert($('#' + ddlBalanceType).val());
	        $('#' + txtItemGroupNameParent).attr('readonly', 'readonly');

	        $groupPopup = $('#' + groupPopupID).ItemGroupTree({
	            title: 'Select Item Group',
	            autoLink: true,
	            autoLinkUpdate: true,
	            linkControlID: dvItemGroupID,
	            highlightLink: true,
	            keyboard: true,

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
	        ///__doPostBack("<%= this.btnLoad.UniqueID %>", "");
    __doPostBack("btnLoad", "");
}


function btnSalaryInfo_onclick() {

}

function btnSalaryInfo_onclick() {

}

// ]]>
	</script>

	<style type="text/css">
		
		.groupBoxContainer
		{
			height: 100%;
			width: 720px;
			overflow: auto;
			margin-left: 5px;
			margin-top: 5px;
		}



		.dvGroup
		{
		  height:auto;
		  width:auto;
		}
				
		.dvPopupItemGroup
		{
			display: none;    
			border: 0px solid black;
			width: 0px;
			height: 0px;
		}
		
		
		.textPopup1
		{
			font-family: Verdana, Arial, Helvetica, sans-serif;
			border: 1px #1B68B8 solid;
			BACKGROUND-COLOR: #FFFFFF;
			COLOR: #000000;
			FONT-SIZE:11px;
			WIDTH: 160px;
			HEIGHT:16px;
			padding-left:2px;
		}
	   
	   
   
	</style>
	
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvPageContent" style="width:100%; height:auto;">
	<div id="dvContentHeader" class="dvContentHeader">
	<div id="dvHeader" class="dvHeader" >
		<asp:Label ID="lblHeader" CssClass="lblHeader" runat="server" Text="Group Hierarchy Report"></asp:Label>
	</div>
	<!--Message Div -->
	<div id="dvMsg" runat="server" class="dvMessage">
		<asp:Label ID="lblMessage" runat="server" Font-Size="Small" Width="100%"> </asp:Label>
	</div>
	 <div id="dvHeaderControl" class="dvHeaderControl">
	 </div>
	</div>

	<div id="dvContentMain" class="dvContentMain" style="max-height: 425px">
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
								<span>Group Hierarchy Report</span> 
							 </td>
							</tr>
						 </table>
						 
					  </div>
					  
				  </div>
				  <div id="groupContent" class="groupContent" style="width:100%;height:300px; overflow:auto;">
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
									   CssClass="group_linkText textBoxReadOnlyEdit fldRequired enableIsDirty" Width="350px" 
										></asp:TextBox>
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
					 &nbsp;</td>
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
					 &nbsp;</td>
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
	

	 <div id="dvContentFooter1" class="dvContentFooter">
		 <table>
			  <tr>
				<td>
				</td>
                  <td>
				  <asp:Button ID="btnLoad" runat="server" Text="Load" CssClass="buttonRefresh checkIsDirty" OnClick="btnLoad_Click" />
					
				</td>
			
				<td>
				 
					<asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonCancel checkIsDirty" OnClick="btnCancel_Click" />
				</td>
				
				<td>
				   <asp:Button ID="btnRefresh" runat="server" Text="Refresh" CssClass="buttonRefresh checkIsDirty" OnClick="btnRefresh_Click"  />
				   </td>

			   
				<td>
				   
				 </td>
				

				 <td>
					<input id="btnClose" type="button" runat="server" class="buttonClose" value="Close" onclick="if (ContentForm){ ContentForm.CloseForm();}" />
				</td>


			  </tr>
		   </table>    
	
	</div>
         <div id="dvContentFooter" class="dvContentFooter">
            <div id="dvContentFooterInner" class="dvContentFooterInner">
                <div style="width: 100%; height: 100%; margin-bottom: 0px;">
                    <div style="width: auto; min-width: 300px; height: auto; text-align: left;">
                        <table border="0">
                            <tr>
                                <td style="width: 100px;"></td>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Text="Report View"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlReportViewMode" runat="server" CssClass="dropDownList">
                                        <asp:ListItem Value="0">In This Tab</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="1">In New Tab</asp:ListItem>
                                        <asp:ListItem Value="2">In New Window</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlReportViewType" runat="server" CssClass="dropDownList">
                                        <asp:ListItem Value="0">Screen</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="1">PDF</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td></td>

                                <td style="width: 20px;"></td>
                                <td style="width: 10px;"></td>
                                <td>
                                    <div id="dvPrintIFrame" class="dvPrintIFrame">
                                        <iframe id="ifPrintButton" runat="server" width="0" height="0"></iframe>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>

	</div> 
	<div id="dvPopupItemGroup" class="dvPopupGroup" runat="server">
		<uc1:itemgrouptree ID="ItemGroupTree1" runat="server" />
	</div>
</asp:Content>
