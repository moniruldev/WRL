<%@ Page Language="C#" MasterPageFile="~/AppMaster.Master"  AutoEventWireup="true" CodeBehind="LocationList.aspx.cs" Inherits="PG.Web.Organization.LocationList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript">
// <!CDATA[


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



            $("#dvGridContainer").height(contInnerHeight -10);
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

    var url = IForm.RootPath + "Organization/Location.aspx?id=" + key;

    if (IForm.PageMode == Enums.PageMode.InTab)
    {

       var tdata = new xtabdata();
       tdata.linktype = Enums.LinkType.Direct;
       tdata.id = 0;
       tdata.name = "Location";
       //tdata.label = "User: " + userid;
       tdata.label = "Location";
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
    
    .style1
    {
        width: 113px;
    }
    

    
</style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div  id="dvPageContent" style="width:100%; height:auto;" >
    <div id="dvContentHeader" class="dvContentHeader">
       <div id="dvHeader" class="dvHeader" >
        <asp:Label ID="lblHeader" CssClass="lblHeader" runat="server" Text="Location List"></asp:Label>
    </div>
   
        <!--Message Div -->
    <div id="dvMsg" runat="server" class="dvMessage"
        style="">
        <asp:Label ID="lblMessage" runat="server" Font-Size="Small" Width="100%"> </asp:Label>
    </div>



     <div id="dvHeaderControl" class="dvHeaderControl">
         
     </div>
    
    
    </div>
  <div id="dvContentMain" class="dvContentMain">
   <div id="dvControlsHead" style="height:auto;width:100%;">
      <table>
             <tr>
               <td>
               </td>
               <td>
                </td>
               <td >
           
            <asp:Button ID="btnRefresh" runat="server"  CssClass="buttonRefresh" style="padding-left:22px;"
                Text="Load Data" 
                    onclick="btnRefresh_Click" />
           
               </td>
           
               <td>

                <input id="btnAddNew" type="button" runat="server" value="New Location" class="buttonNew" style="padding-left:22px;width:120px;" />


               </td>
               <td>
               <asp:FileUpload ID="FileUpload1" runat="server" />  
                </td>
               <td>
                 <asp:Button ID="UPButton" runat="server" Text="Upload" OnClick="UPButton_Click" /> </td>
           
               <td>
               </td>
               <td>
           
              

               </td>
             </tr>
           </table>
   </div>
   
   <div id = "dvControls" style="width:100%;"> 
   <div id="dvControlsInner" class="groupBoxContainer boxShadow">
      
    <div id="dvGridContainer" style="width:100%; height: auto; text-align:left;">
        <div id="dvGridHeader" style="width:100%;height:25px; font-size: smaller;" class="subHeader">
            <table style="height: 100%; color: #FFFFFF; font-weight: bold; text-align: center;" class="defFont" 
                cellspacing="1" cellpadding="1" rules="all" >
            <tr>
                <td width="50px" align="left">
                  
                </td>
                <td width="70px" align="left">Code</td>
                <td width="200px" align="left">Name</td>
                <td width="100px" align="left">Type</td>
            </tr>
            </table>
        </div> 
        <div id="dvGrid" style="width:auto; height: 250px; overflow:auto;">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            CellPadding="1" CellSpacing="1" ForeColor="#333333" GridLines="None" 
            Font-Names="Arial" Font-Size="9pt" 
            DataKeyNames="LocationID" onrowdatabound="GridView1_RowDataBound" 
            onrowdeleting="GridView1_RowDeleting" EmptyDataText="There is no record" PageSize="25" 
            onpageindexchanging="GridView1_PageIndexChanging" 
            onselectedindexchanged="GridView1_SelectedIndexChanged" 
            ShowHeader="False" >
            <PagerSettings Mode="NumericFirstLast" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <Columns>
                <asp:HyperLinkField HeaderText="" Text="" >
                <ControlStyle CssClass="buttonViewGrid" Height="20px" Width="40px" />
                <ItemStyle Width="50px" />
                </asp:HyperLinkField>
                <asp:BoundField DataField="LocationCode" HeaderText="Code" >
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle Width="70px" HorizontalAlign="Left" />
                </asp:BoundField>  
                 <asp:BoundField DataField="LocationName" HeaderText="Name" >
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle Width="200px" HorizontalAlign="Left" />
                </asp:BoundField>  
                <asp:BoundField DataField="LocationTypeName" HeaderText="Type" >
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle Width="100px" HorizontalAlign="Left" />
                </asp:BoundField>  
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
                 <asp:Label ID="lblTotal" runat="server" Text="Total: 0"></asp:Label>
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
