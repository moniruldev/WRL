<%@ Page Language="C#" MasterPageFile="~/Admin/AdminMaster.Master"  AutoEventWireup="true" CodeBehind="UserLocation.aspx.cs" Inherits="PG.Web.Admin.UserLocation" %>

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

//    var selector = '#' + gridViewID + ' input:checkbox';


//    $(selector).each(function (index, elem) {

//        $(elem).click(function () {
//            row = $(this).closest('tr');

//            chkBoxes = $(row).find('input:checkbox:enabled');

//            isChecked = $(this).is(':checked');


//            $(chkBoxes).each(function (index2, elem2) {
//                //$(elem2).checked = true;


//                $(elem2).prop('checked', isChecked);
//            });



//        });

//    });

    //    $('#' + gridViewID).find('input:checkbox').click(function () {

    //        alert('click');
    //    });
})




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
       tdata.id = 4110;
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

function chkCopy_Click() {

    var chkCopyID = '<%=chkCopy.ClientID%>';
    var ddlCopyID = '<%=ddlRoleCopy.ClientID%>';

    if ($('#'+ chkCopyID).is(':checked')) {
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
    

//    if ($('#chkCopy').is(':checked')) {
//        alert('a');
//        //$("#ddlRoleCopy").show();
//        //$('#ddlRoleCopy').attr('visibility', '');
//        $("#ddlRoleCopy").css("visibility", "visible");
//    }
//    else {
//        alert('h');
//        //$("#ddlRoleCopy").hide();
//        //$('#ddlRoleCopy').attr('visibility', 'hidden');
//        $("#ddlRoleCopy").css("visibility", "hidden");
//    }
    return true;
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
        <asp:Label ID="lblHeader" runat="server" Text="User Location" 
            CssClass="lblHeader"></asp:Label>
    </div>
    <div id="dvMsg" runat="server" class="dvMessage"
        style="">
        <asp:Label ID="lblMessage" runat="server" Font-Size="Small" Width="100%" 
            Height="16px"></asp:Label>
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
               <asp:Label ID="Label1" runat="server" Text="User"></asp:Label>
           </td>
           <td>
              <asp:DropDownList ID="ddlUser" runat="server" CssClass="dropDownList"
                style="width: 166px; " 
                 AutoPostBack="True" 
                onselectedindexchanged="ddlUser_SelectedIndexChanged">
             </asp:DropDownList>
        
           </td>
           <td class="style1">
           
        <asp:Button ID="btnRefresh" runat="server"  CssClass="buttonRefresh"
            Text="Load Data" 
                onclick="btnRefresh_Click" />
           
           </td>
           
           <td style="width:20px;">
        <asp:Button ID="btnSave" runat="server"  CssClass="buttonSave"
            style="" 
            Text="Save"  onclick="btnSave_Click"  />
        
           </td>
           <td>
           
               &nbsp;</td>
           <td>
           
      
             </td>
           
           <td>
               &nbsp;</td>
           <td>
           
        <asp:CheckBox ID="chkCopy" runat="server" Text="Copy From" 
                style="" onclick="return chkCopy_Click()" Visible="false" />
             </td>
             
             <td>
               <asp:DropDownList ID="ddlRoleCopy" runat="server" CssClass="dropDownList"
                style="visibility:hidden; width: 166px; ">
               </asp:DropDownList>
             </td>
         </tr>
       </table>
      
    </div>
    
    <div id = "dvControls" style="height:auto;width:100%;"> 
      <div id="dvControlsInner" class="groupBoxContainer boxShadow" style="width:650px;">
    
       <div id="dvGridContainer" class="gridContainer"  
                style="width:100%; height: auto;">
         <div id="dvGridHeader" style="width:100%;height:25px; font-size: smaller;" class="subHeader">
            <table style="height: 100%; color: #FFFFFF; font-weight: bold; text-align: center;" class="defFont" 
                cellspacing="1" cellpadding="1" rules="all" >
            <tr>
                <td width="0px" align="left">
                  
                </td>
                <td width="152px" align="left">Location Name</td>
                <td width="70px" align="left">Allow Login</td>
                
            </tr>
            </table>
        </div> 


         <div id="dvGrid" style="width:auto; height: 250px; overflow:auto;" >
    <asp:GridView style="Z-INDEX: 100;" 
            id="GridView1" runat="server" DataKeyNames="LocationID" 
            CellPadding="1" CellSpacing="1" 
            AutoGenerateColumns="False" onrowdatabound="GridView1_RowDataBound" 
            Font-Names="Arial" Font-Size="9pt" ForeColor="#333333" GridLines="None" 
            ShowHeader="False" onrowcreated="GridView1_RowCreated">
        <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True"  />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333"  />
        <Columns >
            <asp:BoundField DataField="LocationName" HeaderText="Location Name" >
                <ItemStyle BorderColor="Gray" Width="150px"  />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Allow Login">
                <ItemTemplate >
                    <asp:CheckBox ID="chkAllowLogin" runat="server"   />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center"  />
                <ItemStyle BorderColor="Gray" HorizontalAlign="Center" Width="50px"  />
            </asp:TemplateField>
       
        </Columns>
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center"  />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333"  />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"  />
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
