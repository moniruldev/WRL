<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="ProductionSupervisorEntry.aspx.cs" Inherits="PG.Web.Production.ProductionSupervisorEntry" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
    <script src="../javascript/jquery.attributeobserver.js" type="text/javascript"></script>
    <link href="../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />
     <script language="javascript" type="text/javascript">
         var isPageResize = true;
         ContentForm.CalendarImageURL = "../image/calendar.png";

         var hdnCompanyID = '<%=hdnCompanyID.ClientID%>';

   

         
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

         function isDisableKey(evt, obj) {

             var charCode = (evt.which) ? evt.which : event.keyCode
             var value = obj.value;
             alert(charCode);
             var dotcontains = value.indexOf(".") != -1;
             if (dotcontains)
                 if (charCode == 46) return false;
             //if (charCode == 46) return true;
             if (charCode > 0 )
                 return false;
             return true;
         }

         function ShowProgress() {
             $('#' + updateProgressID).show();
         }

         function ShowClosingProgress() {
             $('#' + updateClosingProgressID).show();
         }

         function UserSaveConfirmation() {
             return confirm("Are you sure you want to Save?");
         }

         function UserAuthorizeConfirmation() {
             return confirm("Are you sure you want to Authorized?");
         }
         function PageResizeCompleted(pg, cntMain) {
             resizeContentInner(cntMain);
         }

         function resizeContentInner(cntMain) {
             var contHeight = $("#dvContentMainInner").height();

             var topHeight = $("#dvTop").height();

             var middleHeight = contHeight - topHeight;

             $("#dvMiddle").height(middleHeight);
             $("#tblMiddle").height(middleHeight);

             $("#dvReportList").height(middleHeight);
             $("#dvParam").height(middleHeight);

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


 



         

        

         


       


          



 



 

 


         $(':input').keypress(function (e) {
             if (e.which == 13) {
                 ti = $(this).attr('tabindex') + 1;
                 $('input[tabindex=' + ti + ']').focus();
                 //try to use________ e.which = 9; return e.which;
             } else if (e.which == 9) {
                 e.preventDefault(); //or return false;
             }
         });
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
     <div id="dvPageContent" style="width: 100%; height: 100%;" onkeydown="if(event.keyCode==13){event.keyCode=9; return event.keyCode;}">
         <asp:HiddenField ID="hdnLoggedInUser" runat="server" />
         <asp:HiddenField ID="hdnCompanyID" runat="server" Value="0" />
          <div id="dvContentHeader" class="dvContentHeader">
            <div id="dvHeader" class="dvHeader_Prod" align="left">
                <asp:Label ID="lblHeader" runat="server" Text="Supervisor Allocation" CssClass="lblHeader" Font-Bold="true" Font-Size="15px" ></asp:Label>
            </div>
            <div id="dvMessage" class="dvMessage">
                <asp:Label ID="lblMessage" runat="server" CssClass="lblMessage" Text="" Width="100%"></asp:Label>
            </div>

            <div id="dvHeaderControl" class="dvHeaderControl">
            </div>

        </div>

           <div id="dvContentMain" class="dvContentMain" align="left">
                <div id = "dvControls" style="height:auto; width:100%">
		<div id="dvControlsInner" class="groupBoxContainer boxShadow">    
			 <div id="groupBox">
				  <div id="groupHeader" class="groupHeader">
					  <div style="width:100%;height:20px;">
					 
						 
					  </div>
					  
				  </div>
				      <div id="groupContent" class="groupContent" style="width:100%;height:179px; overflow:auto;">
				          <div id="groupContenInner" style="width:100%;height:auto; text-align:left;">
                              <table border="0" cellspacing="4" cellpadding="2" align="left" style="width: 100%" id="tblProductionMstEntry">
                                  <tr>
                                      <td></td>
                                      <td></td>
                                      <td></td>
                                      <td></td>
                                      <td></td>
                                  </tr>
                                  <tr>
                                      <td style="text-align: right">
                                          <asp:Label ID="lblEMP_ID" runat="server" Text="Emp. ID : "></asp:Label>
                                      </td>
                                      <td style="text-align: left">
                                          <asp:TextBox ID="txtEMP_ID" runat="server" CssClass="textBox" Width="163px"  TabIndex="1" AutoPostBack="True" OnTextChanged="txtEMP_ID_TextChanged"></asp:TextBox>
                                      </td>
                                      <td style="text-align: right">
                                          <asp:Label ID="lblDEPT_ID" runat="server" Text="Department : "></asp:Label>
                                      </td>
                                      <td style="text-align: left">

                                          <asp:TextBox ID="txtDEPT_NAME" runat="server" CssClass="textBox" Width="200px"  TabIndex="1"></asp:TextBox>
                                      </td>
                                      <td style="text-align: right"> 
                                          &nbsp;</td>
                              


                                  </tr>
                                  <tr>
                                      <td style="text-align: right">
                                          <asp:Label ID="lblEMP_NAME" runat="server"  Text="Emp Name :"></asp:Label>
                                      </td>
                                      <td style="text-align: left">
                                          <asp:TextBox ID="txtFULL_NAME" runat="server" CssClass="textBox" Width="250px"  TabIndex="1"></asp:TextBox>
                                      </td>
                                      <td style="text-align: right">
                                          <asp:Label ID="lblDEPT_ID0" runat="server" Text="Designation : "></asp:Label>
                                      </td>
                                      <td>

                                          <asp:TextBox ID="txtDESIGNATION_NAME" runat="server" CssClass="textBox" Width="200px"  TabIndex="1"></asp:TextBox>
                                      </td>
                                      <td style="text-align: right">
                                          &nbsp;</td>
                              
                                  </tr>
                                  <tr>
                                      <td style="text-align: right"> </td>
                                      <td>
                                          <asp:CheckBox ID="chkISACTIVE" runat="server" Text="IS Active" Checked="true" />
                                      </td>
                                      <td style="text-align: right"> <asp:CheckBox ID="chkISOPERATOR" runat="server" Text="IS Operator" /></td>
                                      <td>&nbsp;</td>
                                      <td style="text-align: right">
                                      </td>
                             

                                  </tr>
                                  <tr>
                                      <td style="text-align: right"> 
                                          &nbsp;</td>
                                      <td>
                                      <asp:HiddenField ID="hdnSUPPERVISOR_MSTID" runat="server" />    
                                      </td>
                                      <td style="text-align: right">
                                          &nbsp;</td>
                                      <td>
                                          <asp:HiddenField ID="hdnDEPT_ID" runat="server" />
                                          <asp:HiddenField ID="hdnDESIGNATION_ID" runat="server" />
                                          </td>
                                      <td style="text-align: right">
                                      </td>
                               
                                  </tr>
                                  </table>
                             </div>
                          </div>
                 </div>
            </div>
 
               <div id="dvGridSeparator" runat="server" style="height : 10px; float: left;width: 100%;">
                </div>
          <div id="dvContentFooter" class="dvContentFooter">
            <table>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="btnAddNew" runat="server" Text="New" CssClass="buttonNew" OnClick="btnAddNew_Click"  />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonCancel checkIsDirty" />
                    </td>
                    <td>
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonSave checkRequired" AccessKey="s" OnClientClick="if ( ! UserSaveConfirmation()) return false;" OnClick="btnSave_Click" />
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="buttonEdit" OnClick="btnEdit_Click" />
                    </td>

                    <td>
                        &nbsp;</td>
                    <td>
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="buttonDelete" />
                    </td>
                    <td>
                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" CssClass="buttonRefresh checkIsDirty" OnClick="btnRefresh_Click" />
                    </td>
                    <td>
                        <%--<asp:TextBox ID="TextBox1" runat="server" CssClass="textBox" Style="text-align: right;"
                                        Width="100" TabIndex="-1" Font-Bold="True"></asp:TextBox>--%>
                    </td>
                    <td>
                        <input id="btnClose" type="button" runat="server" class="buttonClose" value="Close" onclick="if (ContentForm) { ContentForm.CloseForm(); }" />
                    </td>
                    <td></td>
                    <td>
                      
                    </td>
                    <td>
                        
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </div>
                    </div>
 </div>
                 

           


        
               


         </div>
</asp:Content>
