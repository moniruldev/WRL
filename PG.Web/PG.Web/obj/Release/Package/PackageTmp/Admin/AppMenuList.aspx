<%@ Page Language="C#" MasterPageFile="~/Admin/AdminMaster.Master"  AutoEventWireup="true" CodeBehind="AppMenuList.aspx.cs" Inherits="PG.Web.Admin.AppMenuList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript">
// <!CDATA[

function tbopen(key, userid)
{
    if(!key)
    {
      key = ''; 
    }

    var url = "Admin/AppMenu.aspx?id=" + key
    //if (pageInTab == 1)
    if (ZForm.PageMode == Enums.PageMode.InTab)
    {

       var tdata = new xtabdata();
       tdata.linktype = Enums.LinkType.Direct;
       tdata.id = 2102;
       tdata.name = "AppMenu";
       //tdata.label = "User: " + userid;
       tdata.label = "App Menu";
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
    
  /*.FixedHeader { POSITION: relative; TOP: expression(this.offsetParent.scrollTop); BACKGROUND-COLOR: white } */
    
    .FixedHeader { POSITION: relative; BACKGROUND-COLOR: white }
    
    #dvMessage
    {
        height: 20px;
    }
    
</style>

    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <div  id="dvPageContent" style="width:100%; height:546px;" >
    <div id="dvHeader" class="dvHeader">
        <asp:Label ID="lblHeader" runat="server" Text="App Menu List" 
            CssClass="lblHeader"></asp:Label>
    </div>
    <div id="dvMsg" runat="server"
        style="width:100%; min-height:20px; height:auto; text-align: center; ">
        <asp:Label ID="lblMessage" runat="server" Font-Size="Small" Width="100%" 
            Height="16px"></asp:Label>
    </div>
    <div id="dvControlsHead" style="height:auto;width:100%;">
       <table>
         <tr>
           <td>
           </td>
           <td>
             <asp:Label ID="Label2" runat="server" Text="Type"></asp:Label>
           </td>
           <td style="">
               <asp:DropDownList ID="ddlLoanType" runat="server" CssClass="dropDownList" Width="100px">
               </asp:DropDownList>
           </td>
           <td>
               <asp:Label ID="Label1" runat="server" Text="Status"></asp:Label>
           </td>
           <td style="">
               <asp:DropDownList ID="ddlLoanStatus" runat="server" CssClass="dropDownList" Width="100px">
               </asp:DropDownList>
           </td>
           
           <td style="width:20px;">
           </td>
           <td>
           
        <asp:Button ID="btnRefresh" runat="server"  CssClass="buttoncommon"
            Text="Load Data" 
                onclick="btnRefresh_Click" />
           
           </td>
           <td>
        <asp:Button ID="btnAddNew" runat="server"  CssClass="buttoncommon"
            style="width: 150px;" 
            Text="New Loan" Width="112px"  />
        
           </td>
           
           <td>
           </td>
           <td>
           
         <asp:Label ID="lblTotal" runat="server" Text="Total: 0" 
            style="width: 96px;"></asp:Label>

           </td>
         </tr>
       </table>
      
    </div>
    
    
    <div id = "dvControls" 
         style="height: auto; width:100%;"> 
        <div id="dvGridContainer" class="gridContainer"  
            style="width:97%; height:  460px;">

    <div id="dvGrid" style="width:90%; height: 500px; overflow:auto;">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            CellPadding="1" CellSpacing="1" ForeColor="#333333" GridLines="None" 
            Font-Names="Arial" Font-Size="9pt" 
            DataKeyNames="AppMenuID" onrowdatabound="GridView1_RowDataBound" 
            onrowdeleting="GridView1_RowDeleting" EmptyDataText="There is no record" PageSize="25" 
            onpageindexchanging="GridView1_PageIndexChanging" >
            <PagerSettings Mode="NumericFirstLast" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <Columns>
                <asp:HyperLinkField HeaderText="" Text="Edit" >
                <ItemStyle Width="50px" />
                </asp:HyperLinkField>
                <asp:BoundField DataField="AppMenuID" HeaderText="MenuID" >
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle Width="120px" HorizontalAlign="Left" />
                </asp:BoundField>                
                <asp:BoundField DataField="AppMenuText" HeaderText="Menu Text" />
                <asp:BoundField DataField="AppMenuNameParent" HeaderText="Parent" />
                
            </Columns>
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#999999" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        </asp:GridView>
      </div>
        </div>
     </div>

    </div>
</asp:Content>
