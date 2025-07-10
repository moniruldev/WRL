<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GLAccountSelection.ascx.cs" Inherits="PG.Web.Controls.GLAccountSelection" %>
<%@ Register src="GroupTree.ascx" tagname="GroupTree" tagprefix="uc1" %>

<link href="../css/form.css" rel="stylesheet" type="text/css" />
<script src="../javascript/PG.accutility.js" type="text/javascript"></script>


<style type="text/css">

.label
{
	font-family: Verdana;
	font-size: 8pt;
	color: #465360;
}

.ui-widget input
{
	font-size: 9pt;
}

.gridContainer
{
	height: auto;
	width: 100%;
}

.gridContainerInner
{
	height: 100%;
	width: 100%;
}

.gridHeader
{
    width:100%;
    height:20px;
    background-image: url('../image/header13.png'); 
    background-repeat:repeat-x;

    color:White;
       	   
    vertical-align:middle;
    font-weight:bold;
}

.gridContent
{
    width:auto;
    height: 285px;
	overflow:auto;
	padding: 0px 1px 0px 1px;
}

.gridContinerInner
{

}


.gridFooter
{
    width:100%;
    height:auto;
    min-height:20px;
    border-top: solid 1px blue;
    vertical-align:middle;
    font-weight:bold;
    
}

.gridPager
{
    width:100%;
    height:auto;
}


.grid
{
	
}

.gridRow
{
	cursor:pointer;
}

.headerRow
{
	
}

.footerRow
{
	
}

.pagerRow
{
	
}


.evenRow
{
	/* matching the first, third and so on (index 0, 2, 4 etc.). */
	background-color:#EFF3FB;    /* grey */
}

.oddRow
{
	/* matching the second, fourth and so on (index 1, 3, 5 etc.). */	
	background-color:White;    	
}


.highlightRow
{
	/* background-color : red !important; */
	
	background-color:#beebff;
}

.selectedRow
{
	color:White;
	background-color: #1F4AA5;
}

.disabledRow
{
	font-style:italic;
	color:Gray;
}



.gridRowCount
{
	
}

.gridBoxShadow
{
    border: solid 1px blue;
	background-image: url('../image/bg_greendot.gif');
    -moz-box-shadow: 3px 3px 4px #C0C0C0;
    -webkit-box-shadow: 3px 3px 4px #C0C0C0;
    box-shadow: 3px 3px 4px #C0C0C0;
	-ms-filter: "progid:DXImageTransform.Microsoft.Shadow(Strength=4, Direction=135, Color='#C0C0C0')";
	filter: progid:DXImageTransform.Microsoft.Shadow(Strength=4, Direction=135, Color='#C0C0C0');
}



.accList
{   
   	  width: 100%;
   	  height: 408px;
}


.accListHeader
{
	height:auto;
	width:100%; 
}

.accListBody
{
	height:350px;
	width:100%;
}

.accListFooter
{
	height:auto;
	width:100%;
}

.ddlSearchBy {}
.txtSearch {}

.ddlAccountType{}

.btnLoadData
{
	 
}
.btnLoadDataID{}


.txtSearch
{
	
}

.btnSelect
{
	
}

.btnSelectClose
{
	
}

.btnClose
{
	
}

.btnSelectALL
{
	
}

.btnUnselectALL
{
	
}

.ui-dialog .ui-dialog-content
{
	padding: 2px 0px 0px 0px;
}



.pagerRow td
{
    border-width: 0;   
    padding: 0 6px;   
    border-left: solid 1px #666;   
    font-weight: bold;   
    color: #fff;     
 }
 
.pagerRow a { color: #666; text-decoration: none; font-size: 10pt; }  
.pagerRow a:hover { color: #000; text-decoration: none; } 
.pagerRow span {color:Yellow; font-size:11pt;} 



        .dvGroupListPopup
        {
        	display : none;
        }

        .dvGLGroup{}


        .dvGroup
        {
         width: 178px;
         height: 20px; 
         border: 1px solid lightgrey;
         
        }
        
        .group_linkText{}
        .group_linkValue{}
        
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
	
        .btnPopup
        {
            height: 20px;
            width: 16px; 
            background-image : url(../image/dropdown.gif) ;
            background-repeat: no-repeat;
            background-position : center bottom;
            cursor: pointer;
        }
        
       .btnPopup:hover
       {
   	      background-image : url(../image/dropdown_over.gif)
       } 
       
       
        .btnClear
        {
            height: 20px;
            width: 16px; 
            background-image : url(../image/crossred.png) ;
            background-repeat: no-repeat;
            background-position : center bottom;
            cursor: pointer;
        }
        
       .btnClear:hover
       {
   	      background-image : url(../image/crossred_over.png)
       } 




</style>
<script language="javascript" type="text/javascript">
 // <!CDATA[

 

 // ]]>

</script>

<div class="accList">
      <div class="accListHeader">
        <div style="height:auto;width:100%;" class="oddRow">
           <table border="0" cellpadding="2" cellspacing="2">
              <tr>
                <td>
                   
                 <asp:Label ID="Label4" runat="server" Text="Search" 
                    CssClass ="label" Font-Bold="true"
                    ></asp:Label>
                   
                </td>
                <td>
                    <asp:DropDownList ID="ddlSearch" runat="server" AppendDataBoundItems="True" 
                        CssClass="ddlSearchBy dropDownList" style="width: 70px; font-size:7pt;">
                        <asp:ListItem Value="0">(none)</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:TextBox ID="txtSearch" runat="server" CssClass="txtSearch textBox" 
                        style="width: 140px; font-size:8pt;"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
                <td>

                   <asp:Label ID="Label6" runat="server" Text="Type" 
                   CssClass ="label"
                    Font-Bold="True"></asp:Label>

                </td>
                <td>
                       <asp:DropDownList ID="ddlAccType" runat="server"  CssClass="ddlAccountType dropDownList" style="font-size:8pt; width:100px;" >
                       </asp:DropDownList>

                    <input id="hdnAccClass" type="hidden" runat="server" value="" class="hdnAccClass" />


                </td>
                <td>
                  
                   <asp:Label ID="Label3" runat="server" Text="GL Group" 
                    CssClass ="label"
                    Font-Bold="True"></asp:Label>
                  
                </td>
                <td>
                  <div id="dvGLGroup" class="group_linkControl dvGroup dvGLGroup" runat="server">
                       <table cellpadding="0" cellspacing="0" style="width:100%;height:100%;">
                           <tr>
                               <td style="">
                                   <asp:TextBox ID="txtGLGroup" runat="server" CssClass="group_linkText textPopup" 
                                       style="font-size:8pt;" TabIndex="0" ></asp:TextBox>
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
                       <input type="hidden" ID="hdnGLGroup" runat="server" class="group_linkValue" value="0" />
                   </div>
                </td>
                <td>
                    <asp:Button ID="btnSearch" runat="server"  CssClass="btnLoadData buttoncommon"
                      Text="Find" style="width: 50px;"  onclick="btnSearch_Click" UseSubmitBehavior="False" 
                       />
                    <input id="hdnBtnSearchID" type="hidden" runat="server" class="btnLoadDataID" value="" />
                </td>
                
              </tr>
           </table> 
        </div>
      </div>
      <div class="accListBody">
             <asp:UpdatePanel ID="UpdatePanel1" runat="server">
              <ContentTemplate>
              <div class="gridContainer"> 
              <div class="gridContinerInner gridBoxShadow" >
            
              <div class="gridHeader">
          
            <table style="color: #FFFFFF; font-weight: bold; text-align: center;" class="defFont" 
                cellspacing="1" cellpadding="1" rules="all" >
            <tr>
                <td width="2px" align="left"></td>
                <td width="102px" align="left">Code</td>
                <td width="202px" align="left">Name</td>
                <td width="202px" align="left">GL Group</td>
                <td width="102px" align="left">Type</td>
            </tr>
            </table>
        </div> 
         <div class="gridContent">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                Font-Names="Arial" Font-Size="9pt" 
                DataKeyNames="GLAccountID" 
                EmptyDataText="There is no record" 
                PageSize="30" 
                        onpageindexchanging="GridView1_PageIndexChanging" CellPadding="1" CellSpacing="1" 
                        ForeColor="#333333" GridLines="None" 
                        onrowdatabound="GridView1_RowDataBound" onsorting="GridView1_Sorting" 
                      AllowPaging="True" EnableViewState="False" ShowHeader="False" 
                >
                     <PagerSettings Mode="NumericFirstLast" />
                     <RowStyle BackColor="#EFF3FB" HorizontalAlign="Left" />
                <Columns>
                    <asp:TemplateField HeaderText="SLNo">
                        <ItemTemplate>
                            <asp:Label ID="SLNo" runat="server" Width="2px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle Width="2px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Code" SortExpression="Acc Code">
                        <ItemTemplate>
                            <asp:HiddenField ID="GLAccountID" runat="server" Value= '<%# Bind("GLAccountID") %>' />
                            <asp:Label ID="GLAccountCode" runat="server" Width="100px" Text='<%# Bind("GLAccountCode") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Name" SortExpression="GLAccountName">
                        <ItemTemplate>
                            <asp:Label ID="GLAccountName" runat="server" Width="200px" Text='<%# Bind("GLAccountName") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle Width="200px" />
                    </asp:TemplateField>
                   
                    <asp:TemplateField HeaderText="GL Group">
                        <ItemTemplate>
                           <asp:Label ID="GLGroupName" runat="server" Text='<%# Bind("GLGroupName") %>'></asp:Label>
                           <asp:HiddenField ID="GLGroupID" runat="server" Value= '<%# Bind("GLGroupID") %>' />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle Width="200px" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Type">
                        <ItemTemplate>
                           <asp:Label ID="GLAccountTypename" runat="server" Text='<%# Bind("GLAccountTypeName") %>'></asp:Label>
                           <asp:HiddenField ID="GLAccountTypeID" runat="server" Value= '<%# Bind("GLAccountTypeID") %>' />
                           <asp:HiddenField ID="GLAccountClassID" runat="server" Value= '<%# Bind("GLAccountClassID") %>' />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>
                    
                </Columns>
                     <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" 
                         Font-Size="8pt" />
                     <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" 
                         Font-Names="Arial" Font-Overline="False" Font-Size="10pt" 
                         CssClass="pagerRow" />
                     <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                     <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" 
                         HorizontalAlign="Left" />
                     <EditRowStyle BackColor="#2461BF" />
                     <AlternatingRowStyle BackColor="White" />
                </asp:GridView> 
              
                </div>
        <div class="gridFooter">
          <table style="height: 25px; width:100%; font-size: smaller;"
                cellspacing="2" cellpadding="1" rules="all" >
            <tr>
                <td align="left" style="width:40%">
                <table>
                   <tr>
                     <td style="width:2px;">
                     
                     </td>
                     <td>
                     <asp:Label ID="lblTotal" runat="server" Text="Rows: 0 of 0"></asp:Label>
                     <input id="hdnRowCount" type="hidden" runat="server" class="gridRowCount" value="0" />
                     </td>
                  </tr>
                </table>
                 
                 
                
                </td>
                <td align="right" style="width:60%" >
                <div id="dvGridPager" class="dvGridPager">
                 <table>
                    <tr>
                      <td>
                          <asp:Button ID="btnGridPageGoTo" runat="server" Text="Go" CssClass="btnPageGoTo"
                              onclick="btnGridPageGoTo_Click" />
                      </td>
                      <td>
                        <asp:Label ID="Label2" runat="server" Text="Page Size:"></asp:Label>
                      </td>
                       <td>
                           <asp:DropDownList ID="ddlGridPageSize" runat="server" CssClass="dropDownList" 
                               Width="50" Height="18" AutoPostBack="True" 
                               onselectedindexchanged="ddlGridPageSize_SelectedIndexChanged">
                               <asp:ListItem>10</asp:ListItem>
                               <asp:ListItem Value="20">20</asp:ListItem>
                               <asp:ListItem Selected="True">30</asp:ListItem>
                               <asp:ListItem>50</asp:ListItem>
                               <asp:ListItem>100</asp:ListItem>
                               <asp:ListItem Value="0">all</asp:ListItem>
                           </asp:DropDownList>
                       </td>


                      <td>
                          <asp:Label ID="Label1" runat="server" Text="Page:"></asp:Label>
                      </td>
                      <td>
                          <asp:TextBox ID="txtGridPageNo" runat="server"  CssClass="textBox txtPageGoTo" Width="30" 
                              Height="14" style="text-align:center;">0</asp:TextBox>
                      </td>
                      <td>
                        <asp:Label ID="lblGridPageInfo" runat="server" Text=" of 0"></asp:Label>
                      </td>
                      <td>
                         <asp:Button ID="btnGridPageFirst" runat="server" Text="" CssClass="btnGridPageFirst" 
                               onclick="btnGridPageFirst_Click" ToolTip="First" />
                      </td>
                      <td>
                             <asp:Button ID="btnGridPagePrev" runat="server" Text="" CssClass="btnGridPagePrev"
                                 onclick="btnGridPagePrev_Click" ToolTip="Previous" />
                      </td>
                      <td>
                        <asp:Button ID="btnGridPageNext" runat="server" Text="" CssClass="btnGridPageNext" 
                                 onclick="btnGridPageNext_Click" ToolTip="Next" />
                      </td>
                      <td> 
                          <asp:Button ID="btnGridPageLast" runat="server" Text=""  CssClass="btnGridPageLast" 
                               onclick="btnGridPageLast_Click" ToolTip="Last"/>
                      </td>
                      <td style="width:2px;">
                      </td>
                    </tr>
                 </table>
                </div>
                   
                </td>
                
            </tr>
            </table>
        </div> 

              </div> 
              </div>
                
            </ContentTemplate>
                 <Triggers>
                     <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                     <asp:AsyncPostBackTrigger ControlID="btnGridPageGoTo" EventName="Click" />
                 </Triggers>
            </asp:UpdatePanel>
      </div>
      <div class="accListFooter">
         <table>
           <tbody>
                <tr>
                  <td>
                  </td>
                  <td>
                      <input type="button" value="Select" class="buttoncommon btnSelect" />
                  </td> 
                  <td>
                  </td>
                  <td>
                      <input type="button" value="Select & Close" class="buttoncommon btnSelectClose" style="width:120px;" />
                  </td>
                  <td>
                  </td>

                  <td>
                  </td>
                  <td>
                    <input type="button" value="Select ALL" class="buttoncommon btnSelectALL" />
                  </td>
                  <td>
                    <input type="button" value="Unselect ALL" class="buttoncommon btnUnselectALL" />
                  </td> 
                  <td>
                     <input type="button" value="Close" class="buttoncommon btnClose" />
                  </td>
                  <td>
                    <input id="hdnBtnPageGoToID" type="hidden" runat="server" class="btnPageGoToID" value="" />
                  </td>
                </tr>
            </tbody>
         </table>
      </div>
      <div id="dvPopupGLGroup" class="dvGroupListPopup" runat="server">
            <uc1:GroupTree ID="GroupTree1" runat="server" />
      </div>
      
</div>