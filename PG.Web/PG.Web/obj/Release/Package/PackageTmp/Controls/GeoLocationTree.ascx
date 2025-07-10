<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GeoLocationTree.ascx.cs" Inherits="PG.Web.Controls.GeoLocationTree" %>

<link href="../css/pg.ui.geolocationtree.css" rel="stylesheet" type="text/css" />

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
<script type="text/javascript">
 // <!CDATA[
 
 
 // ]]>
</script>

 <div class="dvGeoLocationList">
			 <div class="dvGeoLocationHeader" >
				  <table style="width:100%;" border="0" cellspacing="0" cellpadding="0">
					 <tbody>
					   <tr>
						 <td style="width:15%; font-size:8pt;">
							 <asp:Label ID="Label1" runat="server" Text="Search"></asp:Label>
						 </td>
						 <td style="width:85%; padding-left:2px;">
							 <asp:TextBox ID="txtSearch" runat="server" class="geolocation_textSearch textBox"></asp:TextBox>
						 </td>
					   </tr>
					 </tbody>
				  </table>
			 </div>
			 <div class="dvGeoLocationBody" >
				<div class="dvGeoLocationTree" >
					 <asp:Literal ID="litGeoLocation" runat="server"></asp:Literal>
				</div>
			 </div>
			 <div class="dvGeoLocationFooter" >
				<div  class="dvGeoLocationFooterButton">
					<table cellspacing="2" border="0">
						<tr>
						   <td>
							   <input type="button" value="OK" class="buttonOK geolocation_btnOk" style="" />
						   </td>
						   <td>
						   </td>
						   <td>
							   <input type="button" value="Cancel" class="buttonCancel geolocation_btnCancel" style=""  />
						   </td>
						</tr>
					</table>
					
				</div>
				<div class="geolocation_dvNodePathText">
				 <span class="geolocation_nodePathText">
				 </span>
				</div>
			 </div>   
 </div>
 <div >
 </div>