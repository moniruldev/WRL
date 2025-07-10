<%@ Page Title="Accounts Ref. Category Binding" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="GLAccountRefCategory.aspx.cs" Inherits="PG.Web.Accounting.GeneralLedger.GLAccountRefCategory" EnableEventValidation="false" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
    <script src="../../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
    <link href="../../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" /> 
    
     <script language="javascript" type="text/javascript">
         // <!CDATA[

         var isPageResize = true;

         var hdnCompanyID = '<%=hdnCompanyID.ClientID%>';



        var GLAccountServiceLink = '<%=this.GLAccountServiceLink%>';
        var GLAccRefCategoryServiceLink = '<%=this.GLAccRefCategoryServiceLink%>';

         var hdnBtnLoadDataID = '<%=hdnBtnLoadDataID.ClientID%>';

         var updateProgressID = '<%=UpdateProgress1.ClientID%>';

         var gridUpdatePanelIDDet = '<%=UpdatePanel1.ClientID%>';
         var gridViewIDDet = '<%=GridView1.ClientID%>';

         var gridViewID = '<%=GridView1.ClientID%>';
         var gridRowCountID = '<%= hdnRowCount.ClientID %>';




         var txtGLAccountCodeID = '<%=txtGLAccount.ClientID%>';
         var btnGLAccountID = '<%=btnGLAccount.ClientID%>';
         var hdnGLAccountID = '<%=hdnGLAccountID.ClientID%>';


         var txtGLAccountNameID = '<%=txtGLAccountName.ClientID%>';




         var rblAccRefTypeID = '<%=rblAccRefType.ClientID%>';

         var isGridScroll = false;

         function ShowProgress() {
             $('#' + updateProgressID).show();
         }

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

             var pageInstance = Sys.WebForms.PageRequestManager.getInstance();

             pageInstance.add_pageLoaded(function (sender, args) {
                 var panels = args.get_panelsUpdated();
                 for (i = 0; i < panels.length; i++) {
                     //alert(panels[i].id);
                     //ContentForm.InitDefualtFeatureInScope(panels[i].id);

                     if (panels[i].id == gridUpdatePanelIDDet) {
                         bindGLAccRefCategoryListAC();

                     }
                 }

             });

             bindGLAccountList();
             bindGLAccRefCategoryListAC();
         });


         function tbopen(key, userid) {
             if (!key) {
                 key = '';
             }

             var url = "Accounts/Investment.aspx?id=" + key
             //if (pageInTab == 1)
             if (TabVar.PageMode == Enums.PageMode.InTab) {

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


                 try {
                     window.parent.OpenMenuByData(tdata);
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

         function fromParent(val1) {
             alert('this is called from parent: ' + val1);
         }


         function bindGLAccountList() {
             var cgColumns = [{ 'columnName': 'glacccode', 'width': '80', 'align': 'left', 'highlight': 2, 'label': 'Code' }
                                      , { 'columnName': 'glaccname', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                                      , { 'columnName': 'glgroupname', 'width': '130', 'align': 'left', 'highlight': 0, 'label': 'Group' }
                                      , { 'columnName': 'glacctypename', 'width': '100', 'align': 'left', 'highlight': 0, 'label': 'Type' }
             ];

             var companyid = $('#' + hdnCompanyID).val();

             var serviceURL = GLAccountServiceLink + "?isterm=1&includeempty=1&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
             serviceURL += "&companyid=" + companyid;
             serviceURL += "&ispaging=1";
             serviceURL += "&acctypefilter=" + Enums.GLAccountTypeFilter.AllAccount;
             serviceURL += "&namecomptype=" + Enums.DataCompareType.Contains;



             var glAccElem = $('#' + txtGLAccountCodeID);

             $('#' + btnGLAccountID).click(function (e) {
                 $(glAccElem).combogrid("dropdownClick");
             });


             $(glAccElem).combogrid({
                 debug: true,
                 searchButton: false,
                 resetButton: false,
                 alternate: true,
                 munit: 'px',
                 scrollBar: true,
                 showPager: true,
                 showError: true,
                 colModel: cgColumns,
                 width: 500,
                 url: serviceURL,
                 search: function (event, ui) {
                    
                 },
                 select: function (event, ui) {

                     if (!ui.item) {
                         event.preventDefault();
                         $('#' + hdnGLAccountID).val('0');
                         return false;
                         //ClearGLAccountData(elemID);
                     }


                     if (ui.item.glaccid == 0) {
                         event.preventDefault();
                         return false;
                         //ClearGLAccountData(elemID);
                     }
                     else {
                         $('#' + hdnGLAccountID).val(ui.item.glaccid);
                         $('#' + txtGLAccountCodeID).val(ui.item.glacccode);
                         $('#' + txtGLAccountNameID).val(ui.item.glaccname);


                         btnID = $('#' + hdnBtnLoadDataID).val();
                         __doPostBack(btnID, '');
                     }
                     return false;
                 },

                 lc: ''
             });


             $(glAccElem).blur(function () {
                 var self = this;

                 var accNo = $(glAccElem).val();
                 if (accNo == '') {
                     $('#' + hdnGLAccountID).val('0');
                 }
             });

         }

         function bindGLAccRefCategoryListAC() {

             var companyid = $('#' + hdnCompanyID).val();

             var typeid = $("#" + rblAccRefTypeID + " input:radio:checked").val();

             var serviceURL = GLAccRefCategoryServiceLink + "?isterm=1&includeempty=1&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
             serviceURL += "&namecomptype=" + Enums.DataCompareType.Contains + "&ispaging=1";
             //serviceURL += "&typeid=" + typeid;
             serviceURL += "&companyid=" + companyid;


             var cgColumns = [{ 'columnName': 'code', 'width': '80', 'align': 'left', 'highlight': 2, 'label': 'Code' }
                                      , { 'columnName': 'name', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                                      
             ];



             $("#" + gridViewIDDet).find('input[id$="txtAccRefCatCode"]').each(function (index, elem) {
                 //        $(elem).closest('tr').find('input[id$="btnAccRefCode"]').click(function (e) {
                 //            elmID = $(elem).attr('id');
                 //            ShowPopupCostCenter(elmID);
                 //        });

                 $(elem).closest('tr').find('input[id$="btnAccRefCatCode"]').click(function (e) {
                     elmID = $(elem).attr('id');
                     //$(elem).combogrid("show");
                     $(elem).combogrid("dropdownClick");
                 });

                 $(elem).combogrid({
                     debug: true,
                     searchButton: false,
                     resetButton: false,
                     alternate: true,
                     munit: 'px',
                     scrollBar: true,
                     showPager: true,
                     showError: true,
                     colModel: cgColumns,
                     width: 400,
                     url: serviceURL,
                     //"select item" event handler to set input field
                     search: function (event, ui) {
                         //var typeid = $('#' + ddlAccRefType).val();
                         var typeid = $("#" + rblAccRefTypeID + " input:radio:checked").val();
                         var newServiceURL = serviceURL + "&typeid=" + typeid;
                         $(this).combogrid("option", "url", newServiceURL);
                     },
                     select: function (event, ui) {
                         elemID = $(elem).attr('id');

                         if (!ui.item) {
                             event.preventDefault();
                             ClearAccRefData(elemID);
                             return false;
                             //ClearGLAccountData(elemID);
                         }

                         if (ui.item.id == 0) {
                             event.preventDefault();
                             ClearAccRefData(elemID);
                         }
                         else {
                             SetAccRefData(elemID, ui.item);
                         }
                         return false;
                     }
                 });

                 $(elem).blur(function () {
                     var self = this;
                     elemID = $(elem).attr('id');
                     ttCode = $(elem).val();

                     if (ttCode == '') {
                         ClearAccRefData(elemID);
                     }
                     else {

                     }

                     ttID = $(self).closest('tr').find('input[id$="hdnAccRefCatID"]').val();
                     if (ttID == '0' | ttID == '') {
                         $(self).addClass('textError');
                     }
                 });

                 $(elem).focus(function () {
                     var self = this;
                     $(self).removeClass('textError');
                     //$(elem).removeClass('fldDataError');
                 });
             });

         } //bind ref


         function SetAccRefData(elemID, data) {
             $('#' + elemID).val(data.code);
             var detRow = $('#' + elemID).closest('tr.gridRow');
             $(detRow).find('input[id$="hdnAccRefCatID"]').val(data.id);
             $(detRow).find('input[id$="txtAccRefCategoryName"]').val(data.name);
            // $(detRow).find('input[id$="txtAccRefCategory"]').val(data.categoryname);

         }

         function ClearAccRefData(elemID) {
             //$('#' + elemID).val(data.code);
             var detRow = $('#' + elemID).closest('tr.gridRow');
             $(detRow).find('input[id$="hdnAccRefCatID"]').val('0');
             $(detRow).find('input[id$="txtAccRefCategoryName"]').val('');
             //$(detRow).find('input[id$="txtAccRefCategory"]').val('');

         }

         // ]]>
</script>
    
     <style type="text/css">
       
    .dvGridFooter
    {

    }
    

    
    .radiobuttonlist
    {
        font: 12px Verdana, sans-serif;
        color: #000; /* non selected color */
    }
 
    .radiobuttonlist input
    {
        width: 15px;
        height: 10px;
    }
    
    .radiobuttonlist input:focus 
    {
        border: 1px solid #AAAAAA;
    }
    
    .radiobuttonlist label
    {
        color: #3E3928;
        background-color:#E8E5D4;
        padding-left: 6px;
        padding-right: 6px;
        padding-top: 2px;
        padding-bottom: 2px;
        border: 1px solid #AAAAAA;
        margin: 0px 0px 0px 0px;
        white-space: nowrap;
        clear: left;
        margin-right: 5px;
    }
    
    .radiobuttonlist label:focus 
    {
        border-color: Green;
    }
    
    .radiobuttonlist span.selectedradio label
    {
        background-color: #F7F5E8;
        color: #000000;
        font-weight: bold;
        border-bottom-color: #F3F2E7;
        padding-top:4px;
 
    }
    .radiobuttonlist label:hover
    {
        color: #CC3300;
        background: #D1CFC2;
    }
 
    .radiobuttoncontainer
    {
        position:relative;
        z-index:1;
    }
 
    .radiobuttonbackground
    {
        position:relative;
        z-index:0;
        border: solid 1px #AcA899; 
        padding: 10px; 
        background-color:#F3F2E7;
    }
    
    

</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div  id="dvPageContent" style="width:100%; height:auto;" >
     <div id="dvContentHeader" class="dvContentHeader">
    <div id="dvHeader" class="dvHeader">
        <asp:Label ID="lblHeader" runat="server" Text="Accounts Ref. Category Binding" 
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
           </td>     
                 <td>
                              
               <asp:HiddenField ID="hdnCompanyID" runat="server" Value="0" />
                              
           </td>  
                    <td>
                              
                                 <asp:HiddenField ID="hdnGLAccountID" runat="server" Value="0" />
                              
           </td>

           <td>
               <asp:HiddenField ID="hdnBtnLoadDataID" runat="server" Value="" />
           </td>
           <td>
               <asp:HiddenField ID="hdnDebitAmtOpen" runat="server" Value="0" />
           </td>
           <td>
            <asp:HiddenField ID="hdnCreditAmtOpen" runat="server" Value="0" />
           </td>
           
           
           </tr>
        
        </table>
        <table>
              <tr>
           <td>
           
           
           </td>
           <td style="width:80px;">
              <asp:Label ID="Label4" runat="server" Text="Sub Ledger:"></asp:Label>
           
           </td>
           <td>
               <table cellspacing="0" cellpadding="0">
                             <tr>
                              <td>
                                 <asp:TextBox id="txtGLAccount" runat="server" Width="80px" 
                                   CssClass="textBox"></asp:TextBox>
                              
                              </td>
                              <td>
                                <input id="btnGLAccount" type="button" value="" runat="server" class="buttonDropdown"  tabindex="-1" />

                              </td>
                             
                             </tr>
                          
                          </table>

           </td>

            <td>
                              
                                 <asp:TextBox id="txtGLAccountName" runat="server" Width="150px" 
                                   CssClass="textBox"></asp:TextBox>
                              
                              </td>

                              <td>
                              
                              </td>

                              <td >
                              
                              </td>

                              <td>
                              
                                  &nbsp;</td>
         
         
         </tr>
      
        
        
        
        </table>


       <table>


  
         <tr>
           <td>
           </td>
           <td style="width:80px;">
           
        
              <asp:Label ID="Label6" runat="server" Text="Type:"></asp:Label>
           
                              </td>
              <td>
             
               <asp:RadioButtonList ID="rblAccRefType" runat="server" AutoPostBack="True" 
                   CellSpacing="2" RepeatDirection="Horizontal" CssClass="radiobuttonlist"
                   onselectedindexchanged="rblAccRefType_SelectedIndexChanged">
                   <asp:ListItem Selected="True" Value="2">Cost Center</asp:ListItem>
                   <asp:ListItem Value="3">Reference</asp:ListItem>
               </asp:RadioButtonList>
             
             </td>
           <td >
           
        
               &nbsp;</td>
           
        
           <td>
           
        
        <asp:Button ID="btnRefresh" runat="server"  CssClass="buttoncommon"
            Text="Load Data" 
                onclick="btnRefresh_Click" Visible="false"/>
           
             </td>
           
           <td>
               &nbsp;</td>
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
                <td width="32px" align="center">SL#</td>
                <td width="150px" align="left">
                    <asp:Label ID="lblAccRefType" runat="server" Text="Cost Center Category"></asp:Label>
                </td>
                <td width="120px" align="left">Name</td>
                <td width="62px" align="center">Mandatory</td>
                <td width="62px" align="center">Default</td>
            </tr>
            </table>
        </div> 


      <div id="dvGrid" style="width:100%; height: 250px; overflow:auto;">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                                    ShowHeader="False" 
                                 CellPadding="1" CellSpacing="1" ForeColor="#333333" GridLines="None" 
                                    DataKeyNames="GLAccountRefCategoryID,_RecordStateInt" 
                                    onrowdatabound="GridView1_RowDataBound" 
                                    onrowcommand="GridView1_RowCommand" onrowdeleting="GridView1_RowDeleting" 
                                    onrowcreated="GridView1_RowCreated" EnableModelValidation="True" ClientIDMode="AutoID" >
                                 <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                 <Columns>
                                     <asp:TemplateField HeaderText="SL#">
                                        <ItemTemplate >
                                            <asp:Label ID="lblSLNo" runat="server" Text='<%# Bind("GLACCOUNTREFCATEGORYSLNO") %>' 
                                                style="text-align:center;"  width="28px">
                                             </asp:Label>
                                             <asp:HiddenField ID="hdnGLAccountID" runat="server" Value='<%# Bind("GLAccountID") %>'  />
                                        </ItemTemplate>
                                        <ItemStyle Width="30px" VerticalAlign="Top" />
                                     </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Code" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                          <div>
                                            <table border="0" cellpadding="1" cellspacing="1">
                                               <tbody>
                                                  <tr> 
                                                    <td>
                                                      <asp:TextBox ID="txtAccRefCatCode" runat="server" CssClass="textBox textAutoSelect txtAccRefCatCode" Width="120" Text='<%# Bind("AccRefCategoryCode") %>'></asp:TextBox>
                                                      <asp:HiddenField ID="hdnAccRefCatID" runat="server" value='<%# Bind("AccRefCategoryID") %>' />
                                                    </td>
                                                    <td>
                                                       <input id="btnAccRefCatCode" type="button" value="" runat="server" class="buttonDropdown"  tabindex="-1" />
                                                    </td>                                            
                                                     <td>
                                                      <asp:TextBox ID="txtAccRefCategoryName" runat="server" CssClass="textBox" Width="120" Text='<%# Bind("AccRefCategoryName") %>'></asp:TextBox>
                                                    </td>
                                                      
                                                       <td align="center">
                                                      <asp:CheckBox ID="chkISMANDATORY" runat="server" Checked='<%# Bind("ISMANDATORY") %>' Width="60" />
                                                    </td>
                                                       <td align="center">
                                                     
                                                        <asp:CheckBox ID="chkDefault" runat="server" Checked="true" Enabled="false"  Width="60" />

                                                    </td>
                                                      

                                                  </tr>
                                                 
                                               </tbody>
                                            </table>
                                            </div>
                                            <div style="overflow:visible;">

                                            </div>    
                                         </ItemTemplate>
                                         <ItemStyle Width="10" />
                                         <HeaderStyle HorizontalAlign="Left" />
                                         <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                     </asp:TemplateField>
                                     
                                   <asp:TemplateField HeaderText="Delete" ShowHeader="false">
                                     <ItemTemplate>
                                       <asp:LinkButton ID="btnDeleteRow" CssClass="buttonDeleteGrid" Height="20px" Width="40px"
                                         CommandName="delete" runat="server">
                                         </asp:LinkButton> 
                                     </ItemTemplate>
                                       <ItemStyle VerticalAlign="Top" />
                                   </asp:TemplateField>


                                     <asp:TemplateField Visible="false">
                                       <ItemTemplate>
                                            <div style="width:10px; ">
                                                <div>
                                                    <div style="background-position: right center; height:25px; cursor: pointer; background-image: url('../image/more.png'); background-repeat: no-repeat; text-align: left; vertical-align: middle;" onclick="togglePannelStatus(this)" title="More..">
                                                      ...
                                                    </div>
                                                    <div style="display:none;">
                                                        <div class="gridPanel" 
                                                            style=" float:right; width: 0px; height: 0px;">
                                                            <div style="position:relative; height:100%;Width:100%;">
  
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
                                
                            </ContentTemplate>
                            
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnNewRow" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                </div>
      <div id ="dvGridFooter" class="dvGridFooter subFooter">
        <table border="0" > 
            <tr>
              <td>
              </td>
              <td>
               <asp:Button ID="btnNewRow" runat="server" CssClass="buttonNewRow"
                             Text="" onclick="btnNewRow_Click" OnClientClick="ShowProgress();" />
                             
               </td>
                <td style= "width:20px;">
                              <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="300">
                                    <ProgressTemplate>
                                        <asp:Image ID="imgProgress" runat="server" ImageUrl="~/image/loader.gif" />
                                    </ProgressTemplate>
                                 </asp:UpdateProgress>
                             </td>
              <td style="width:100px;">
              </td>
            </tr>
             <tr>
              <td>
              </td>
              <td>


                <asp:HiddenField ID="hdnRowCount" runat="server" Value="0" />


              </td>
              <td>
              </td>
              <td></td>
              
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
                       <asp:Button ID="btnSave" runat="server"  CssClass="buttonSave" Text="Save" OnClick="btnSave_Click"  />
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
