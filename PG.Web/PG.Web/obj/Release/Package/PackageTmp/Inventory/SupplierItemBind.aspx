<%@ Page Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="SupplierItemBind.aspx.cs" Inherits="PG.Web.Inventory.SupplierItemBind" %>

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


        $(document).ready(function () {


        })


        function fromParent(val1) {
            alert('this is called from parent: ' + val1);
        }

        function chkCopy_Click() {

            if ($('#' + chkCopyID).is(':checked')) {
                // alert('a');
                //$("#ddlRoleCopy").show();
                //$('#ddlRoleCopy').attr('visibility', '');
                $("#" + ddlCopyID).css("visibility", "visible");
            }
            else {
                //  alert('h');
                //$("#ddlRoleCopy").hide();
                //$('#ddlRoleCopy').attr('visibility', 'hidden');
                $("#" + ddlCopyID).css("visibility", "hidden");
            }

            return true;
        }


        // ]]>
</script>

<style type="text/css">
    

    
</style>

    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<%-- <div  id="dvPageContent" style="width:100%; height:100%;" >--%>
   <div id="dvContentHeader" class="dvContentHeader"> 
    <div id="dvHeader" class="dvHeader">
        <asp:Label ID="lblHeader" runat="server" Text="Supplier Item Bind" CssClass="lblHeader"></asp:Label>
    </div>
    <div id="dvMsg" runat="server" class="dvMessage" style="">
        <asp:Label ID="lblMessage" runat="server" Font-Size="Small" Width="100%" Height="16px"></asp:Label>
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
               <asp:Label ID="lblSupplier" runat="server" Text="Supplier"></asp:Label>
           </td>
           <td>
              <asp:DropDownList ID="ddlSupplier" runat="server" Width="130"  CssClass="dropDownList enableIsDirty" AutoPostBack="true" OnSelectedIndexChanged="ddlSupplier_SelectedIndexChanged"> </asp:DropDownList>
        
           </td>
         </tr>
           <tr>
           <td>
           </td>
           <td>
               <asp:Label ID="lblItemGroup" runat="server" Text="Item Group"></asp:Label>
           </td>
           <td>
              <asp:DropDownList ID="ddlItemGroup" runat="server" Width="130"  CssClass="dropDownList enableIsDirty"> </asp:DropDownList>&nbsp; &nbsp;
        
           </td>
               <td>
                    <asp:Button ID="btnLoad" runat="server"  CssClass="buttonRefresh" style="padding-left:22px;" Text="Load" onclick="btnLoad_Click" /> &nbsp;&nbsp; &nbsp;    
           </td>
               <td>
                    <asp:Button ID="btnRefresh" runat="server"  CssClass="buttonRefresh" Text="Reset" onclick="btnClear_Click" />
               </td>
         </tr>
       </table>
      
    </div>
    
    <div id = "dvControls" style="height:auto;width:100%;"> 
      <div id="dvControlsInner" class="groupBoxContainer boxShadow" style="width:650px;">
       <div id="dvGridContainer" class="gridContainer" style="width:100%; height: auto;">
         <div id="dvGridHeader" style="width:100%;height:25px; font-size: smaller;" class="subHeader">
            <table style="height: 90%; color: #FFFFFF; font-weight: bold; text-align: center;" class="defFont" cellspacing="1" cellpadding="1" rules="all" >
            <tr>
                <td width="0px" align="left">
                </td>
                <td align="left" Width="40px">SL</td>
                <td width="600px" align="left">Item Name</td>
                <td width="150px" align="left">Bind with Supplier </td>
                <td align="left" Width="60px"></td>
            </tr>
            </table>
        </div> 


  <div id="dvGrid" style="width:auto; height: 600px; overflow:auto;" >  
    <asp:GridView style="Z-INDEX: 100;" 
            id="GridView1" runat="server"  DataKeyNames="ITEM_ID" 
            CellPadding="1" CellSpacing="1" 
            AutoGenerateColumns="False" 
            Font-Names="Arial" Font-Size="10pt" ForeColor="#333333" GridLines="None" 
            ShowHeader="False">
        <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True"  />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333"  />
        <Columns >
             <asp:TemplateField HeaderText="SL" HeaderStyle-HorizontalAlign="Left">
            <ItemTemplate>
                <%# Container.DataItemIndex + 1 %>
            </ItemTemplate>
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle Width="40px" HorizontalAlign="Left"></ItemStyle>
            </asp:TemplateField>
            <asp:BoundField DataField="ITEM_DESC" HeaderText="Item Name" >
                <ItemStyle BorderColor="Gray" Width="450px"  />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Bind with Supplier">
                <ItemTemplate >
                    <asp:CheckBox ID="chkAllow" runat="server"   />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="left"  />
                <ItemStyle BorderColor="Gray" HorizontalAlign="left" Width="150px"  />
            </asp:TemplateField>
         <%-- <asp:BoundField DataField="ITEM_ID" HeaderText="Item Id">
                <ItemStyle BorderColor="Gray" Width="60px"  />
            </asp:BoundField>--%>
             <asp:TemplateField HeaderText="" HeaderStyle-Width="160px" ItemStyle-Width="60px" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblITEM_ID" runat="server" Text='<%#Eval("ITEM_ID")%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center"  />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333"  />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"  />
        <EditRowStyle BackColor="#999999" />
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
    </asp:GridView> 
      </div>
        </div>  
    
    <%--  </div>--%>

    </div>
   
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
      <div id="dvControlsHead1" style="height:auto;width:100%;">
       <table>
         <tr>
           <td>
           </td>
           <td>
              
           </td>
             <td class="style1">
           
       &nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;
           
           </td>
           
           <td style="width:20px;">
        <asp:Button ID="btnSave" runat="server"  CssClass="buttonSave" style="" Text="Save"  onclick="btnSave_Click"  />
        
           </td>
           <td>
            
        
           </td>
         </tr>
          
       </table>
      
    </div>
    <div id="dvContentFooter" class="dvContentFooter">
    
    </div> 
    
    </div>
</asp:Content>