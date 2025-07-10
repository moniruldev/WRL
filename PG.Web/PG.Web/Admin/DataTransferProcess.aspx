<%@ Page Language="C#" MasterPageFile="~/Admin/AdminMaster.Master"  AutoEventWireup="true" CodeBehind="DataTransferProcess.aspx.cs" Inherits="PG.Web.Admin.DataTransferProcess" %>

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
function ShowCuttingProgress() {
    $('#' + UpdateProgressCutting).show();
}

function ShowCuttingProgress() {
    $('#' + UpdateProgressCutting).show();
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
        .groupBoxContainer {
            width: 900px;
        }    
    
   
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
                                        <td></td>
                                        <td align="right">
                                            <asp:Label ID="lblFromDate" runat="server" Text="Date From:"></asp:Label>
                                        </td>
                                        <td>
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtFromDate" runat="server" Width="90px" CssClass="textBox textDate dateParse"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 4px;"></td>
                                                    <td>
                                                        <asp:Label ID="lblToDate" runat="server" Text="Date To:"></asp:Label>
                                                    </td>
                                                    <td style="width: 2px;"></td>
                                                    <td>
                                                        <asp:TextBox ID="txtToDate" runat="server" Width="90px" CssClass="textBox textDate dateParse"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td align="right" class="">&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                    </tr>
            <tr>
             <td>
              </td>
                <td>
              </td>
              <td>
              </td>
              <td> 
                 <asp:Button ID="btnRefresh" runat="server"  CssClass="buttonRefresh"
                    style="" Text="Load" 
                onclick="btnRefresh_Click" />
              </td>
              <td>
                 <asp:Button ID="btnProcess" runat="server"  CssClass="buttonRefresh" style="" Text="Process" OnClick="btnProcess_Click" />
                     
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
             
 
   
    <div id="dvGrid" style="height:  100%;">    
         
           
          <div id="dvGridHeader5" style="width: 900px; height: 25px; font-size: smaller;" class="subHeader">
                <table style="height: 80%; color: #FFFFFF; font-weight: bold; text-align: left;"
                    class="defFont" cellspacing="1" cellpadding="1">
                    <tr class="headerRow">
                        <td width="30px" class="headerColCenter">SL#
                        </td>

                        <td width="200px" class="headerColCenter">Master Table Name
                        </td>
                        <td width="102px" class="headerColCenter">Master Table Filter
                        </td>
                        <td width="130px" class="headerColCenter">Master table Join Key
                        </td>
                         <td width="115px" class="headerColCenter">Detail Table    
                        </td>
                         <td width="133px" class="headerColCenter">Detail to Master Join Key
                        </td>
                         <td width="50px" class="headerColCenter">Is Active   
                        </td>

                    </tr>
                </table>    
            </div>     
            <div id="groupBoxContainer boxShadow2" style="height: auto; ">  
                <div id="groupDataDetails2" style="height: auto;">

                    <div id="Div11" class=" " runat="server" style="height: auto; text-align: left;">

                        <div id="dvGrid2" style="width: 900px; height: 90px; " class="dvGrid">
                            <asp:UpdatePanel ID="updatePanelCutting" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowHeader="False"
                                        CellPadding="1" CellSpacing="1" ForeColor="#333333" GridLines="Both" DataKeyNames="MASTER_TABLE"
                                        EnableModelValidation="True" ClientIDMode="AutoID" OnRowCommand="GridView1_RowCommand" OnRowCreated="GridView1_RowCreated" OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting">
                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                        <Columns>


                                            <asp:TemplateField HeaderText="SL#">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSLNo" runat="server" Text='<%#Container.DataItemIndex+1 %>' Style="text-align: center;"
                                                        Width="30px">
                                                    </asp:Label>

                                                </ItemTemplate>
                                                <ItemStyle VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText=" Item Type" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <div>
                                                        <table border="0" cellpadding="1" cellspacing="1">  
                                                            <tbody>
                                                                <tr> 
                                                                    
                                                                    <td>
                                                                        <asp:TextBox ID="txtMasterTable" runat="server" CssClass="textBox textAutoSelect" Width="197px" Text='<%# Bind("MASTER_TABLE") %>'></asp:TextBox>
                                                                     
                                                                    </td>   
                                                                    <td>
                                                                        
                                                                    </td>  
                                                                    <td>
                                                                        <asp:TextBox ID="txtMasterTableFilter" runat="server" CssClass="textBox textAutoSelect " Width="100px" BackColor="Khaki" align="right" Text='<%# Bind("MASTER_TABLE_FILTER") %>'></asp:TextBox>
                                                                    </td>
                                                                         <td>
                                                                        <asp:TextBox ID="txtMasterToDetail" runat="server" CssClass="textBox textAutoSelect " Width="128px" BackColor="Khaki" align="right" Text='<%# Bind("MASTER_TO_DETAIL") %>'></asp:TextBox>
                                                                    </td>
                                                                         <td>
                                                                        <asp:TextBox ID="txtDetailTable" runat="server" CssClass="textBox textAutoSelect " Width="110px" BackColor="Khaki" align="right" Text='<%# Bind("DETAIL_TABLE") %>'></asp:TextBox>
                                                                    </td>
                                                                         <td>
                                                                        <asp:TextBox ID="txtDetailToMaster" runat="server" CssClass="textBox textAutoSelect " Width="130px" BackColor="Khaki" align="right" Text='<%# Bind("DETAIL_TO_MASTER") %>'></asp:TextBox>
                                                                    </td>
                                                                         <td>  
                                                                        <asp:TextBox ID="txtIsActive" runat="server" CssClass="textBox textAutoSelect " Width="50px" BackColor="Khaki" align="right" Text='<%# Bind("IS_ACTIVE") %>'></asp:TextBox>
                                                                    </td>
                                                                   

                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                    <div style="overflow: visible;">
                                                    </div>
                                                </ItemTemplate>
                                                <ItemStyle Width="10" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" ShowHeader="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnDeleteRow" CssClass="buttonDeleteIcon" Height="16px" Width="18px"
                                                        CommandName="delete" runat="server">
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <div style="width: 10px;">
                                                        <div>
                                                            <div style="background-position: right center; height: 25px; cursor: pointer; background-image: url('../image/more.png'); background-repeat: no-repeat; text-align: left; vertical-align: middle;"
                                                                onclick="togglePannelStatus(this)"
                                                                title="More..">
                                                                ...
                                                            </div>
                                                            <div style="display: none;">
                                                                <div class="gridPanel" style="float: right; width: 0px; height: 0px;">
                                                                    <div style="position: relative; height: 80%; width: 100%;">
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Size="Smaller" />
                                        <EditRowStyle BackColor="#999999" />
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    </asp:GridView>
                                    <input id="Hidden5" type="hidden" runat="server" value="[]" />
                                    <input id="Hidden6" type="hidden" runat="server" value="[]" />
                                </ContentTemplate>
                               <%-- <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnCuttingNewRow" EventName="Click" />
                                </Triggers>--%>
                            </asp:UpdatePanel>
                        </div>


                        <div id="divGridControls1" style="width: 100%; height: 30px; border-top: solid 1px #C0C0C0; border-bottom: solid 1px #0b07f5;">
                            <table style="width: 70%; height: 100%; text-align: center;" cellspacing="1" cellpadding="1"
                                border="0">
                                <tr>
                                    <td style="width: 2px"></td>

                                    <td style="width: 160px; text-align: right;"></td>
                                    <td align="right" style="text-align: left">&nbsp;
                                    </td>
                                    <td align="right">&nbsp;</td>
                                    <td align="right">&nbsp;</td>
                                    <td align="right" style="width: 90px">&nbsp;
                                    </td>
                                    <td style="width: 90px" align="left">
                                        <%--<asp:Button ID="btnCuttingNewRow" runat="server" CssClass="buttonNewRow" Text="" OnClientClick="ShowCuttingProgress()" OnClick="btnCuttingNewRow_Click" />--%>
                                    </td>
                                    <td style="width: 20px;">
                                        <asp:UpdateProgress ID="UpdateProgressCutting" runat="server" AssociatedUpdatePanelID="updatePanelCutting"
                                            DisplayAfter="300">
                                            <ProgressTemplate>
                                                <asp:Image ID="imgProgress2" runat="server" ImageUrl="~/image/loader.gif" />
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </td>



                                </tr>


                            </table>
                        </div>
                    </div>

                     



                </div>
            </div>

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
