<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GroupTree.ascx.cs" Inherits="PG.Web.Controls.GroupTree" %>

<link href="../css/PG.ui.grouptree.css" rel="stylesheet" type="text/css" />

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

 <div class="dvGroupList">
             <div class="dvGroupHeader" >
                  <table style="width:100%;" border="0" cellspacing="0" cellpadding="0">
                     <tbody>
                       <tr>
                         <td style="width:15%; font-size:8pt;">
                             <asp:Label ID="Label1" runat="server" Text="Search"></asp:Label>
                         </td>
                         <td style="width:85%; padding-left:2px;">
                             <asp:TextBox ID="txtSearch" runat="server" class="group_textSearch textBox"></asp:TextBox>
                         </td>
                       </tr>
                     </tbody>
                  </table>
             </div>
             <div class="dvGroupBody" >
                <div class="dvGroupTree" >
                     <asp:Literal ID="litGLGroup" runat="server"></asp:Literal>
                </div>
             </div>
             <div class="dvGroupFooter" >
                <div  class="dvGroupFooterButton">
                    <table cellspacing="2" border="0">
                        <tr>
                           <td>
                               <input type="button" value="OK" class="buttoncommon group_btnOk" style="width:60px; height:22px;" />
                           </td>
                           <td>
                           </td>
                           <td>
                               <input type="button" value="Cancel" class="buttoncommon group_btnCancel" style="width:60px; height:22px; "  />
                           </td>
                        </tr>
                    </table>
                    
                </div>
                <div class="group_dvNodePathText">
                 <span class="group_nodePathText">
                 </span>
                </div>
               
                    
                
             </div>   
 </div>
 <div >
 </div>