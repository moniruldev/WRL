<%@ Page Language="C#" MasterPageFile="~/Admin/AdminMaster.Master"  AutoEventWireup="true" CodeBehind="LongTask.aspx.cs" Inherits="PG.Web.Admin.LongTask" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript">
// <!CDATA[






function tbopen(key, userid)
{
    if(!key)
    {
      key = ''; 
    }
    
    var url = "Admin/User.aspx?uid=" + key
    //if (pageInTab == 1)
    if (ZForm.PageMode == Enums.PageMode.InTab)
    {

       var tdata = new xtabdata();
       tdata.linktype = Enums.LinkType.Direct;
       tdata.id = 6310;
       tdata.name = "User";
       //tdata.label = "User: " + userid;
       tdata.label = "User";
       tdata.type = 0;
       tdata.url = url;
       tdata.tabaction = Enums.TabAction.InNewTab;
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
 <div  id="dvPageContent" style="width:100%; height:546px;" >
    <div id="dvHeader" class="dvHeader">
        <asp:Label ID="lblHeader" runat="server" Text="Long Task" CssClass="lblHeader"></asp:Label>
    </div>
    <div id="dvMessage" class="dvMessage" >
        <asp:Label ID="lblMessage" runat="server" CssClass="lblMessage" Text="" Width="100%"></asp:Label>
    </div>
    
    <div id = "dvControls" style="position:relative"> 
        <asp:Button ID="btnRefresh" runat="server"  CssClass="buttoncommon"
            style="position: absolute; top: 6px; left: 251px;" Text="Refresh" 
                onclick="btnRefresh_Click" />
        
         <asp:Label ID="lblTotal" runat="server" Text="Total: 0" 
         
            style="position: absolute; top: 10px; left: 481px; width: 96px;"></asp:Label>

        <div id="dvGridContainer" class="gridContainer"  
                style="position:absolute; top: 44px; LEFT: 15px; width:97%; height:  469px;">
                    <div id="dvGridHeader" class="gridHeaderDiv" style="width:90%;height:20px;">

                    </div> 

    <div id="dvGrid" style="width:90%; height: 500px; overflow:auto;">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            CellPadding="1" CellSpacing="1" ForeColor="#333333" GridLines="None" 
            Font-Names="Arial" Font-Size="9pt" 
            DataKeyNames="TaskID" onrowdatabound="GridView1_RowDataBound" 
            onrowdeleting="GridView1_RowDeleting" EmptyDataText="There is no record" 
            Width="666px" onrowcommand="GridView1_RowCommand" >
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <Columns>
                <asp:BoundField DataField="TaskID" HeaderText="Task ID" />
                <asp:BoundField DataField="Task" HeaderText="Task" />
                 <asp:BoundField DataField="TaskState" HeaderText="State" />
                <asp:ButtonField Text="Stop" ButtonType="Button" CommandName="stoptask" />
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
