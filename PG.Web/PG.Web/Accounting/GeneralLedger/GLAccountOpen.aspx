<%@ Page Language="C#" MasterPageFile="~/AppMaster.Master"  AutoEventWireup="true" CodeBehind="GLAccountOpen.aspx.cs" Inherits="PG.Web.Accounting.GeneralLedger.GLAccountOpen" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript">
// <!CDATA[

        var isPageResize = true;

        //var gridID = '<%=this.GridView1.ClientID%>';
        var hdnCompanyID = '<%=hdnCompanyID.ClientID%>';

        var ddlAccYear = '<%=ddlAccYear.ClientID%>';


        var totDebitID = '<%=this.txtDebitAmt.ClientID%>';
        var totCreditID = '<%=this.txtCreditAmt.ClientID%>';


        var totDebitDiffID = '<%=this.txtDebitAmtDiff.ClientID%>';
        var totCreditDiffID = '<%=this.txtCreditAmtDiff.ClientID%>';

var gridViewID = '<%=GridView1.ClientID%>';
var gridRowCountID = '<%= hdnRowCount.ClientID %>';

var isGridScroll = false;


function PageResizeCompleted(pg,cntMain) {
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

    var url = "Accounts/Investment.aspx?id=" + key
    //if (pageInTab == 1)
    if (TabVar.PageMode == Enums.PageMode.InTab)
    {

       var tdata = new xtabdata();
       tdata.linktype = Enums.LinkType.Direct;
       tdata.id = 5130;
       tdata.name = "Investment";
       //tdata.label = "User: " + userid;
       tdata.label = "Investment";
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


function tbopenAccRef(key , userid) {
    if (!key) {
        key = '';
    }

    //var compID = $("#" + hdnCompanyID).val();
    var accYearID = $("#" + ddlAccYear).val();
    //var accRefTypeID = $("#" + ddlAccRefTypeID).val();

    var url = IForm.RootPath + "Accounting/GeneralLedger/GLAccountOpenAccRef.aspx?glaccid=" + key;
    url += "&accyearid=" + accYearID;

    //if (pageInTab == 1)
    if (TabVar.PageMode == Enums.PageMode.InTab) {

        var tdata = new xtabdata();
        tdata.linktype = Enums.LinkType.Direct;
        tdata.id = 0;
        tdata.name = "AccRef";
        //tdata.label = "User: " + userid;
        tdata.label = "Account Det Opening";
        tdata.type = 0;
        tdata.url = url;
        tdata.tabaction = Enums.TabAction.InNewTab;
        tdata.selecttab = 1;
        tdata.reload = 0;
        tdata.param = "";


        try {
            //window.parent.OpenMenuByData(tdata);

            window.parent.TabMenu.OpenMenuByData(tdata);
        }
        catch (err) {
            alert("error in page");
        }
    }
    else {
        //on new window/tab
        //window.open(url,'_blank');   

        window.location = url;
    }
}


function fromParent(val1)
{
    alert('this is called from parent: ' + val1);
}


$(document).ready(function () {

    $("#" + gridViewID).find('input.txtDebit').keyup(function (e) {
        var elem = this;
        if (JSUtility.IsPrintableChar(e.keyCode, true, true)) {
            if (JSUtility.GetNumber($(elem).val()) > 0) {
                $(elem).closest('tr').find('input.txtCredit').val('0.00');
            }
            SumBalance();
        }
    });

    $("#" + gridViewID).find('input.txtCredit').keyup(function (e) {
        var elem = this;
        if (JSUtility.IsPrintableChar(e.keyCode, true, true)) {
            if (JSUtility.GetNumber($(elem).val()) > 0) {
                $(elem).closest('tr').find('input.txtDebit').val('0.00');
            }
            SumBalance();
        }
    });


    SumBalance();

});


function SumBalance() {
     var totDebit = 0;
     var totCredit = 0;

     var totDebitDiff = 0;
     var totCreditDiff = 0;
     
     
     var diffAmt = 0
      

     $("#" + gridViewID).find('input.txtDebit').each(function(index, elem) {
         drAmt = parseFloat(JSUtility.GetNumber($(elem).val()));
         if (!isNaN(drAmt)) {
             totDebit += drAmt;
         }
     });


     $("#" + gridViewID).find('input.txtCredit').each(function(index, elem) {
         crAmt = parseFloat(JSUtility.GetNumber($(elem).val()));
         if (!isNaN(crAmt)) {
             totCredit += crAmt;
         }
     });


     if (totDebit > totCredit) {
         totCreditDiff = totDebit - totCredit;
     }
     if (totCredit > totDebit) {
         totDebitDiff = totCredit - totDebit;
     }
     
     
     $("#" + totDebitDiffID).val(JSUtility.AddCommas(totDebitDiff.toFixed(2)));
     $("#" + totCreditDiffID).val(JSUtility.AddCommas(totCreditDiff.toFixed(2)));  

     $("#" + totDebitID).val(JSUtility.AddCommas(totDebit.toFixed(2)));
     $("#" + totCreditID).val(JSUtility.AddCommas(totCredit.toFixed(2)));

     //d
     $("#" + totDebitDiffID).removeClass('txtDiffAmt');
     $("#" + totCreditDiffID).removeClass('txtDiffAmt');
     if (totDebitDiff > 0) {
         $("#" + totDebitDiffID).addClass('txtDiffAmt');
     }
     if (totCreditDiff > 0) {
         $("#" + totCreditDiffID).addClass('txtDiffAmt');
     }

}




// ]]>
</script>

<style type="text/css">
       
    .dvGridFooter
    {

    }
    
    .txtDebit{}
    .txtCredit{}
    
    
    .txtDiffAmt
    {
        color : Red;
    }
   

</style>

    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <div  id="dvPageContent" style="width:100%; height:auto;" >
     <div id="dvContentHeader" class="dvContentHeader">
    <div id="dvHeader" class="dvHeader">
        <asp:Label ID="lblHeader" runat="server" Text="Ledger Openning List" 
            CssClass="lblHeader"></asp:Label>
    </div>
    <div id="dvMsg" runat="server" style="">
        <asp:Label ID="lblMessage" runat="server" Font-Size="Small" Width="100%" 
            Height="16px"></asp:Label>
    </div>
    
    </div>
    
    <div id="dvContentMain" class="dvContentMain">
        <div id="dvControlsHead" style="height:auto;width:100%;">
       <table>
         <tr>
           <td>
           </td>
           <td>
               <asp:Label ID="Label1" runat="server" Text="Year:"></asp:Label></td>
                          <td>
                 <asp:DropDownList ID="ddlAccYear" runat="server" CssClass="dropDownList" 
                     Width="150px" AutoPostBack="True" 
                                  onselectedindexchanged="ddlAccYear_SelectedIndexChanged">
                 </asp:DropDownList>
               </td>
           <td >
           
        <asp:Button ID="btnRefresh" runat="server"  CssClass="buttoncommon"
            Text="Load Data" 
                onclick="btnRefresh_Click" />
           
           </td>
           
           <td style="width:20px;">
      
        
               &nbsp;</td>
           <td>
           
        <asp:Button ID="btnAccRef" runat="server"  CssClass="buttoncommon"
            Text="Ref" 
                onclick="btnRefresh_Click" />
           
             </td>
           <td>
               &nbsp;</td>
           
           <td>
               <asp:HiddenField ID="hdnCompanyID" runat="server" Value="0" />
           </td>
           <td>
           
               &nbsp;</td>
         </tr>
       </table>
      
    </div>

    
      <div id = "dvControls" style="height:auto; width:100%"> 
    
      <div id="dvControlsInner" class="groupBoxContainer boxShadow" style="width:700px;">

        <div id="dvGridContainer" class="gridContainer"  
            style="width:100%; height:  100%;">
     <div id="dvGridHeader" style="width:100%;height:25px; font-size: smaller;" class="subHeader">
            <table style="height: 100%; color: #FFFFFF; font-weight: bold; text-align: center;" class="defFont" 
                cellspacing="1" cellpadding="1" rules="all" >
            <tr>
                <td width="72px" align="left">Code</td>
                <td width="152px" align="left">Name</td>
                <td width="92px" align="left">Type</td>
                <td width="152px" align="left">GL Group</td>
                
                <td width="102px" align="right">Debit</td>
                <td width="102px" align="right">Credit</td>
            </tr>
            </table>
        </div> 


      <div id="dvGrid" style="width:100%; height: 250px; overflow:auto;">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            CellPadding="1" CellSpacing="1" ForeColor="#333333" GridLines="None" 
            Font-Names="Arial" Font-Size="8pt" 
            DataKeyNames="AccYearID,GLAccountID,GLAccountTypeID" onrowdatabound="GridView1_RowDataBound" 
            onrowdeleting="GridView1_RowDeleting" EmptyDataText="There is no record" PageSize="25" 
            onpageindexchanging="GridView1_PageIndexChanging" 
              onrowcreated="GridView1_RowCreated" ShowHeader="False" 
              EnableModelValidation="True" >
            <PagerSettings Mode="NumericFirstLast" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <Columns>
                <asp:BoundField DataField="GLAccountCode"  HeaderText="Code" >
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle Width="70px" HorizontalAlign="Left" />
                </asp:BoundField>                
                <asp:BoundField DataField="GLAccountName" HeaderText="Account Name" >
                 <ItemStyle Width="150px" HorizontalAlign="Left" />
                 </asp:BoundField>
                  <asp:BoundField DataField="GLAccountTypeName" HeaderText="Type" >
                 <ItemStyle Width="90px" HorizontalAlign="Left" />
                 </asp:BoundField>          
                <asp:BoundField DataField="GLGroupName" HeaderText="GL Head">
                  <ItemStyle Width="150px" HorizontalAlign="Left" />
                 </asp:BoundField>
                <asp:TemplateField HeaderText="Debit">
                    <ItemTemplate>
                        <asp:TextBox ID="txtDebitAmt" runat="server" CssClass="textBox txtDebit textNumberOnly textAutoSelect textDecimalFormat " Width="100px" Text='<%# Bind("DebitAmtOpen", "{0:#0.00}") %>' style="text-align:right;" ></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Credit">
                    <ItemTemplate>
                        <asp:TextBox ID="txtCreditAmt" runat="server" CssClass="textBox txtCredit textNumberOnly textAutoSelect textDecimalFormat" Width="100px" Text='<%# Bind("CreditAmtOpen", "{0:#0.00}") %>' style="text-align:right;" ></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:HyperLinkField HeaderText="Det" Text="Det..." />
            </Columns>
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#999999" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        </asp:GridView>
      </div>
      <div id ="dvGridFooter" class="dvGridFooter subFooter">
        <table border="0" > 
            <tr>
              <td>
              </td>
              <td>


                  &nbsp;</td>
              <td style="width:200px;">
              </td>

              <td align="right">
                  <asp:Label ID="Label2" runat="server" Text="Differnece in Openning Balance:"></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtDebitAmtDiff" runat="server" style="text-align:right;width:100px;" CssClass="textBoxReadOnlyEdit"></asp:TextBox>
              </td>
              <td>
                 <asp:TextBox ID="txtCreditAmtDiff" runat="server" style="text-align:right;width:100px;" CssClass="textBoxReadOnlyEdit"></asp:TextBox>
              </td>
            </tr>
             <tr>
              <td>
              </td>
              <td>


         <asp:Label ID="lblTotal" runat="server" Text="Total: 0" 
            style=""></asp:Label>
                <asp:HiddenField ID="hdnRowCount" runat="server" Value="0" />


              </td>
              <td>
              </td>
              <td align="right">
                  <asp:Label ID="Label3" runat="server" Text="Total:"></asp:Label>
              </td>
              <td>
                  <asp:TextBox ID="txtDebitAmt" runat="server" style="text-align:right;width:100px;" CssClass="textBoxReadOnlyEdit"></asp:TextBox>
              </td>
              <td>
                 <asp:TextBox ID="txtCreditAmt" runat="server" style="text-align:right;width:100px;" CssClass="textBoxReadOnlyEdit"></asp:TextBox>
              </td>
            </tr>
        </table>
      </div>  
    </div>

    
     </div>
    </div> 
   
     
      <div id="dvControlsFooter" style="height:auto; width:auto">
              <div style="height:10px;">
              </div>
         </div>


   </div>
    
    
    <div id="dvContentFooter" class="dvContentFooter">
        <div style="width:100%;height:100%; margin-bottom: 0px;">
          <div style="width:100%; min-width:300px; height:auto; text-align:center;">
             <table border="0">
               <tbody>
                  <tr>
                    <td>
                    </td> 
                    <td>
                       <asp:Button ID="btnSave" runat="server"  CssClass="buttonSave"
                              
                                Text="Save"  onclick="btnSave_Click"  />
                    </td>
                    <td>

                    </td>
                    <td>

                    </td>

                    <td>
                        
                    </td>
                  </tr>
               </tbody>
             </table>
           </div>  
         </div>
    </div>
    </div>
</asp:Content>
