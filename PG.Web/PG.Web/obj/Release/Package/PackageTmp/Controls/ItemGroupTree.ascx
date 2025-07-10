<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ItemGroupTree.ascx.cs" Inherits="PG.Web.Controls.ItemGroupTree" %>

<link href="../css/pg.ui.itemgrouptree.css" rel="stylesheet" type="text/css" />

<script src="../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>

<link href="../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />

<link href="../css/jquery.treeview.css" rel="stylesheet" type="text/css" />
<link href="../css/skin/ui.dynatree.css" rel="stylesheet" type="text/css" />

<style type="text/css">
	.ui-autocomplete {
		max-height: 200px;
		overflow-y: auto;
		/* prevent horizontal scrollbar */
		overflow-x: hidden;
		/* add padding to account for vertical scrollbar */
		padding-right: 20px;
	}
	/* IE 6 doesn't support max-height
	 * we use height instead, but this forces the menu to always be this tall
	 */
	* html .ui-autocomplete {
		height: 200px;
	}
</style>
<script language="javascript" type="text/javascript">
 // <!CDATA[
 

 
 
 
 // ]]>
</script>

 <div class="dvItemGroupList">
			 <div class="dvItemGroupHeader" >
				  <table style="width:100%;" border="0" cellspacing="0" cellpadding="0">
					 <tbody>
					   <tr>
						 <td style="width:15%; font-size:8pt;">
							 <asp:Label ID="Label1" runat="server" Text="Search"></asp:Label>
						 </td>
						 <td style="width:80%; padding-left:2px;">
							 <asp:TextBox ID="txtSearch" runat="server" class="itemgroup_textSearch textBox"></asp:TextBox>
                             <div class="dvHdnSearchID">
                               <asp:HiddenField ID="hdnSearchID" runat="server" Value="0" />
                             </div>
						 </td>
                         <td style="width:5%;">
							<div class="btnPopupSearch">
							</div>
						  </td> 
					   </tr>
					 </tbody>
				  </table>
			 </div>
			 <div class="dvItemGroupBody" >
				<div class="dvItemGroupTree" >
					 <asp:Literal ID="litItemGroup" runat="server"></asp:Literal>
				</div>
			 </div>
			 <div class="dvItemGroupFooter" >
				<div  class="dvItemGroupFooterButton">
					<table cellspacing="2" border="0">
						<tr>
						   <td>
							   <input type="button" value="OK" class="buttonOK itemgroup_btnOk" style="" />
						   </td>
						   <td>
						   </td>
						   <td>
							   <input type="button" value="Cancel" class="buttonCancel itemgroup_btnCancel" style=""  />
						   </td>
						</tr>
					</table>
					
				</div>
				<div class="itemgroup_dvNodePathText">
				 <span class="itemgroup_nodePathText">
				 </span>
				</div>
			   
					
				
			 </div>   
 </div>
 <div >
 </div>