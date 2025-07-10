<%@ Page Language="C#" MasterPageFile="~/Admin/AdminMaster.Master"  AutoEventWireup="true" CodeBehind="RoleList.aspx.cs" Inherits="PG.Web.Admin.RoleList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript">
// <!CDATA[

var isPageResize = true;
var gridViewID = '<%=GridView1.ClientID%>';
var gridRowCountID = '<%= hdnRowCount.ClientID %>';

var isGridScroll = false;

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



    $("#dvGridContainer").height(contInnerHeight - 10);
    var gridHeight = $("#dvGridContainer").height();
    var gridHeaderHeight = $("#dvGridHeader").height();
    var gridFooterHeight = $("#dvGridFooter").height();
    $("#dvGrid").height(gridHeight - gridHeaderHeight - gridFooterHeight - 2);


}


function tbopen(key, userid)
{
    if(!key)
    {
      key = ''; 
    }
    
    var url = "Admin/Role.aspx?id=" + key
    //if (pageInTab == 1)
    if (ZForm.PageMode == Enums.PageMode.InTab)
    {

       var tdata = new xtabdata();
       tdata.linktype = Enums.LinkType.Direct;
       tdata.id = 0;
       tdata.name = "Role";
       //tdata.label = "User: " + userid;
       tdata.label = "Role";
       tdata.type = 0;
       tdata.url = url;
       tdata.tabaction = Enums.TabAction.InNewTab;
       tdata.selecttab = 1;
       tdata.reload = 0;
       tdata.param = "";
       
                             
       try
       {                                          
        window.parent.TabMenu.OpenMenuByData(tdata);
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

function fromParent(val1)
{
    alert('this is called from parent: ' + val1);
}




// ]]>
</script>

<style type="text/css">
    
   
</style>

    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <div  id="dvPageContent" style="width:100%; height:100%;" >
 
  <div id="dvContentHeader" class="dvContentHeader">  
    <div id="dvHeader" class="dvHeader">
        <asp:Label ID="lblHeader" runat="server" Text="Role List" CssClass="lblHeader"></asp:Label>
    </div>
    <div id="dvMessage" class="dvMessage" >
        <asp:Label ID="lblMessage" runat="server" CssClass="lblMessage" Text="" Width="100%"></asp:Label>
    </div>
  
     <div id="dvHeaderControl" class="dvHeaderControl">
          
     </div>
    
  </div>  
  
  <div id="dvContentMain" class="dvContentMain"> 
       <div id="dvControlsHead" style="height:auto;width:100%;">
      <table style="" cellspacing="2" border="0">
            <tr>
             
              <td>
              </td>
              <td> 
                 <asp:Button ID="btnRefresh" runat="server"  CssClass="buttonRefresh"
                    style="" Text="Refresh" 
                onclick="btnRefresh_Click" />
              </td>
              <td>
                 <input id="btnAddNew" type="button" runat="server" value="New Role" class="buttonNew" style="" /> 
                     
              </td>
              <td>
                
              </td>
            </tr>
            
         </table>        
     
        </div> 
  
    <div id = "dvControls" style="height:auto;width:100%;"> 
   <div id="dvControlsInner" class="groupBoxContainer boxShadow">
   
        
  <div id="dvGridContainer" class="gridContainer"  
                style="width:100%; height:  100%;">
             
 <div id="dvGridHeader" style="width:100%;height:25px; font-size: smaller;" class="subHeader">
            <table style="height: 100%; color: #FFFFFF; font-weight: bold; text-align: center;" class="defFont" 
                cellspacing="1" cellpadding="1" rules="all" >
            <tr>
                <td width="50px" align="left"></td>
                <td width="55px" align="left"></td>
                <td width="105px" align="left">Role Name</td>
                <td width="205px" align="left">Description</td>
            </tr>
            </table>
        </div> 
   
    <div id="dvGrid" style="width:100%; height:  100%;">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            CellPadding="1" CellSpacing="1" ForeColor="#333333" GridLines="None" 
            Font-Names="Arial" Font-Size="9pt" 
            DataKeyNames="RoleID" onrowdatabound="GridView1_RowDataBound" 
            onrowdeleting="GridView1_RowDeleting" EmptyDataText="There is no record" 
            onrowcreated="GridView1_RowCreated" ShowHeader="False" 
            >
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <Columns>
                <asp:HyperLinkField HeaderText="" Text="" >
                <ControlStyle CssClass="buttonEditGrid" Height="20px" Width="40px" />
                <ItemStyle  Width="45px" />
                </asp:HyperLinkField>

                <asp:ButtonField Text="" ItemStyle-Width="40px" >
                <ControlStyle CssClass="buttonDeleteGrid" Height="20px" Width="40px" />
                <ItemStyle  Width="50px" />
                </asp:ButtonField>
                
                <asp:BoundField DataField="RoleName" HeaderText="Role Name" ItemStyle-Width="100px" />
                <asp:BoundField DataField="RoleDesc" HeaderText="Description" ItemStyle-Width="200px" />
                
            </Columns>
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#999999" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        </asp:GridView>
      </div>
   
     <div id="dvGridFooter" style="width:100%;height:25px; font-size: smaller;" class="subFooter">
            <table style="height: 100%; font-weight: bold;"
                cellspacing="1" cellpadding="1" rules="all" >
            <tr>
                <td width="5px" align="left"></td>
                <td align="left">
                  <asp:Label ID="lblTotal" runat="server" Text="Total: 0" 
                     style="width: 96px;"></asp:Label>
                <asp:HiddenField ID="hdnRowCount" runat="server" Value="0" />
                </td>
                <td width="50px"></td>
                
            </tr>
            </table>
        </div> 
   
        </div>
     
     </div>
     </div>

    </div>
    
      <div id="dvContentFooter" class="dvContentFooter">
    
    </div> 
 </div>   
    
</asp:Content>
