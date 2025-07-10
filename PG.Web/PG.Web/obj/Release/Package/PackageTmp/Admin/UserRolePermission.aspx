<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="UserRolePermission.aspx.cs" Inherits="PG.Web.Admin.UserRolePermission" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
    <script src="../javascript/jquery.attributeobserver.js" type="text/javascript"></script>
    <link href="../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />

      <script language="javascript" type="text/javascript">
          var isPageResize = true;
          ContentForm.CalendarImageURL = "../image/calendar.png";
          var RoleListServiceLink = '<%=this.RoleListServiceLink%>';
          var gridUpdatePanelIDDet = '<%=UpdatePanel1.ClientID%>';
          var gridViewIDDet = '<%=GRDDTLITEMLIST.ClientID%>';
          var updateProgressID = '<%=UpdateProgress2.ClientID%>';

          

          function ShowProgress() {
              $('#' + updateProgressID).show();
          }

          function ShowClosingProgress() {
              $('#' + updateClosingProgressID).show();
          }

          function UserSaveConfirmation() {
              return confirm("Are you sure you want to Save?");
          }

          function UserAuthorizeConfirmation() {
              return confirm("Are you sure you want to Authorized?");
          }
          function PageResizeCompleted(pg, cntMain) {
              resizeContentInner(cntMain);
          }

          function resizeContentInner(cntMain) {
              var contHeight = $("#dvContentMainInner").height();

              var topHeight = $("#dvTop").height();

              var middleHeight = contHeight - topHeight;

              $("#dvMiddle").height(middleHeight);
              $("#tblMiddle").height(middleHeight);

              $("#dvReportList").height(middleHeight);
              $("#dvParam").height(middleHeight);

          }

          function tbopen(key, isPrint, isPDFAutoPrint, showWait) {
              key = key || '';
              isPrint = isPrint || false;
              showWait = showWait || true;

              if (isPrint) {
                  if (key != '') {
                      ReportPrint(key, isPDFAutoPrint);
                      return;
                  }
              }

              //var url = "/Report/ReportView.aspx?rk=" + key

              var now = new Date();
              var strTime = now.getTime().toString();
              var url = ReportViewPageLink + "?rk=" + key + "&_tt=" + strTime;
              //var url = ReportViewPageLink + "?rk=" + key;

              //if (pageInTab == 1)
              if (TabVar.PageMode == Enums.PageMode.InTab) {

                  var tdata = new xtabdata();
                  tdata.linktype = Enums.LinkType.Direct;
                  tdata.id = 7999;
                  tdata.name = "Report view";
                  //tdata.label = "User: " + userid;
                  tdata.label = "Report view";
                  tdata.type = 0;
                  tdata.url = url;
                  tdata.tabaction = Enums.TabAction.InNewTab;
                  tdata.selecttab = 1;
                  tdata.reload = 0;
                  tdata.param = "";
                  tdata.showWait = showWait;

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

          $(document).ready(function () {
              var pageInstance = Sys.WebForms.PageRequestManager.getInstance();
              pageInstance.add_pageLoaded(function (sender, args) {
                  var panels = args.get_panelsUpdated();
                  for (i = 0; i < panels.length; i++) {
                      if (panels[i].id == gridUpdatePanelIDDet) {
                          bindRoleList(gridViewIDDet);
                      }
                  }
              });
              bindRoleList(gridViewIDDet);
          });


          function bindRoleList(gridViewID) {
              var cgColumns = [
                                 { 'columnName': 'rolename', 'width': '200', 'align': 'left', 'highlight': 4, 'label': 'Role Name' }
                               , { 'columnName': 'roledesc', 'width': '70', 'align': 'left', 'highlight': 4, 'label': 'Role Desc' }
                               , { 'columnName': 'Is Active', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Is Active' }
                               , { 'columnName': 'Is Admin', 'width': '180', 'align': 'left', 'highlight': 4, 'label': 'Is Admin' }
              ];

              var serviceURL = RoleListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
              serviceURL += "&ispaging=1";
             
              var gridSelector = "#" + gridViewID;
              $(gridSelector).find('input[id$="txtROLENAME"]').each(function (index, elem) {
                  ///list click
                  var elemRow = $(elem).closest('tr.gridRow');

                  var hdnItemIDElem = $(elemRow).find('input[id$="txtROLENAME"]');

                  //var prevGLCode = '';

                  $(elem).closest('tr').find('input[id$="btnROLENAME"]').click(function (e) {
                      elmID = $(elem).attr('id');
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
                      colModel: cgColumns,
                      autoFocus: true,
                      showError: true,
                      width: 800,
                      url: serviceURL,
                      search: function (event, ui) {
                          var elemRowCur = $(elem).closest('tr.gridRow');
                          var newServiceURL = serviceURL;
                          newServiceURL = JSUtility.AddTimeToQueryString(newServiceURL);
                          $(this).combogrid("option", "url", newServiceURL);
                      },

                      select: function (event, ui) {
                          elemID = $(elem).attr('id');
                          if (!ui.item) {
                              event.preventDefault();
                              ClearItemData(elemID);
                              return false;
                          }
                          if (ui.item.id == 0) {
                              event.preventDefault();
                              return false;
                          }
                          else {
                              SetItemData(elemID, ui.item);
                          }
                          // setDetInstrument(hdnIsInstrumentElem, txtInstrumentElem, btnInstrumentElem);
                          return false;
                      }


                      // lc: ''
                  });

                  $(elem).blur(function () {
                      var self = this;
                      elemID = $(elem).attr('id');
                      eCode = $(elem).val();
                      isComboGridOpen = $(self).combogrid('isOpened');
                      if (eCode == '') {
                         
                      }
                      else {
                          if (grp == null) {
                              ClearItemData(elemID);
                          }
                          else {
                              SetItemData(elemID, grp);
                          }
                      }
                  });

              });

          }


          function ClearItemData(txtItemID) {
              var detRow = $('#' + txtItemID).closest('tr.gridRow');
              $(detRow).find('input[id$="hndROLEID"]').val('0');
              $(detRow).find('input[id$="txtROLENAME"]').val('');
              $(detRow).find('input[id$="txtROLEDESC"]').val('');
              $(detRow).find('input[id$="txtISACTIVE"]').val('');
              $(detRow).find('input[id$="txtISADMIN"]').val('');

          }
          function SetItemData(txtItemCodeID, data) {
              $('#' + txtItemCodeID).val(data.itemid);

              var detRow = $('#' + txtItemCodeID).closest('tr.gridRow');
              $(detRow).find('input[id$="hndROLEID"]').val(data.roleid);
              $(detRow).find('input[id$="txtROLENAME"]').val(data.rolename);
              $(detRow).find('input[id$="txtROLEDESC"]').val(data.roledesc);
              $(detRow).find('input[id$="txtISACTIVE"]').val(data.isactive);
              $(detRow).find('input[id$="txtISADMIN"]').val(data.isadmin);
          }
      </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:HiddenField ID="hdnUSERID" runat="server" />

     <div id="dvPageContent" style="width: 100%; height: 100%;" onkeydown="if(event.keyCode==13){event.keyCode=9; return event.keyCode;}">
         <asp:HiddenField ID="hdnLoggedInUser" runat="server" />
         <asp:HiddenField ID="hdnCompanyID" runat="server" Value="0" />


         <div id="dvContentHeader" class="dvContentHeader">
            <div id="dvHeader" class="dvHeader_Prod" align="left">
                <asp:Label ID="lblHeader" runat="server" Text="User Role Entry" CssClass="lblHeader" Font-Bold="true" Font-Size="15px" ></asp:Label>
            </div>
            <div id="dvMessage" class="dvMessage">
                <asp:Label ID="lblMessage" runat="server" CssClass="lblMessage" Text="" Width="100%"></asp:Label>
            </div>
            <div id="dvHeaderControl" class="dvHeaderControl">
            </div>
        </div>

                   <div id="dvContentMain" class="dvContentMain">
                         <div id="dvControlsHead" style="height: auto; width: 100%;" align="left">
                                         <table border="0"   align="left" style="width:100%" >
                                            <tr>
                                                    <td style="text-align: right; width:250px;"> 
                                                        <asp:Label ID="lbluser" runat="server" Text="User :"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtUSERNAME" runat="server" OnTextChanged="txtUSERNAME_TextChanged" AutoPostBack="true" ></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td style="text-align: right; width:250px;">
                                                        <asp:Label ID="lblAppid" runat="server" Text="Application :"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlAPPID" runat="server" Width="200px">
                                                            <asp:ListItem Selected="True" Value="1">PBLINV</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td></td>
                       
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right; width:250px;">
                                                        Full Name :
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="lblUSERNAME" runat="server" Width="250px"></asp:TextBox>
                                                    </td>
                                                    <td></td>
                                                    <td></td>
                        
                                            </tr>

                                              <tr>
                                                    <td style="text-align: right; width:250px;">
                                                        Default Role :
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblDefaultRole" runat="server"></asp:Label>
                                                    </td>
                                                    <td></td>
                                                    <td></td>
                        
                                            </tr>
                                    </table>
                             </div>

                       <div id="dvGridSeparator" runat="server" style="height : 10px; float: left;width: 100%;">
                </div>






               <div id="dvControls" style="height: auto; width: 100%; ">
                     <div id="Div5" runat="server" class="" style="width: 100%; text-align: left; border-top : solid 1px #0b07f5;">
                        <span style="font-weight: bold;font-size : 15px;color :#ff3b00;">Role List : </span>
                    </div>

                    <div id="groupDataDetails" style="width: 90%; height: auto;">
                         <div id="dvGridContainer2" runat="server" class="" style="width: auto; height: auto; text-align: left">
                              <div id="dvGridHeader2" style=" padding-left:50px; width: 735px; height: 25px; font-size: smaller;" class="subHeader_Prod">
                                    <table style="height: 80%; color: #FFFFFF;  font-weight: bold; text-align: left;"
                                        class="defFont" cellspacing="1" cellpadding="1">
                                        <tr class="headerRow_Prod">
                                            <td width="30px" class="headerColCenter_prod">SL#
                                            </td>
                                            <td width="200px" class="headerColCenter_prod"> Role Name
                                            </td>
                                            <td width="20px" ></td>
                                            <td width="200px" class="headerColCenter_prod">Role Description 
                                            </td>
                                             <td width="75px" class="headerColCenter_prod">Is Active
                                            </td>
                                             <td width="75px" class="headerColCenter_prod">Is Admin
                                            </td>
                                            <td width="16px" class="headerColCenter_prod">Delete
                                            </td>
                                
                                        </tr>
                                    </table>
                                </div>
                             
                         <div id="dvGrid" style=" padding-left:50px; width: 800px; height: 450px; overflow: auto;" class="dvGrid">
                                  <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                            <asp:GridView ID="GRDDTLITEMLIST" runat="server" AutoGenerateColumns="False" ShowHeader="False" 
                                                CellPadding="1" CellSpacing="1" ForeColor="#333333" GridLines="Both" DataKeyNames="ROLENAME" 
                                                EnableModelValidation="True" ClientIDMode="AutoID" OnRowCommand="GRDDTLITEMLIST_RowCommand" OnRowCreated="GRDDTLITEMLIST_RowCreated" OnRowDataBound="GRDDTLITEMLIST_RowDataBound" OnRowDeleting="GRDDTLITEMLIST_RowDeleting"  >
                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                <Columns>
                                       

                                                    <asp:TemplateField HeaderText="SL#">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSLNo" runat="server" Text='<%# Bind("SL") %>' Style="text-align: center;"
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
                                                                                 <asp:TextBox ID="txtROLENAME" runat="server" CssClass="textBox textAutoSelect" Width="200px" Text='<%#Bind("ROLENAME") %>'></asp:TextBox>
                                                                                 <asp:HiddenField ID="hndROLEID" runat="server" Value='<%# Bind("ROLEID") %>' />
                                                                            </td>
                                                                            <td>
                                                                                <input id="btnROLENAME" type="button" value="" runat="server"  class="buttonDropdown" tabindex="-1" />
                                                                            </td>
                                                                             <td>
                                                                                <asp:TextBox ID="txtROLEDESC" runat="server" CssClass="textBox textAutoSelect"  Width="200px" Text='<%# Bind("ROLEDESC") %>'></asp:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtISACTIVE" runat="server" CssClass="textBox textAutoSelect" Width="70px" align="right" Text='<%# Bind("ISACTIVE") %>' ></asp:TextBox>
                                                                            </td>
                                                                             <td>
                                                                                 <asp:TextBox ID="txtISADMIN" runat="server" CssClass="textBox textAutoSelect" Width="70px" Text='<%#Bind("ISADMIN") %>'></asp:TextBox>
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
                                            <input id="hdnJournalDetRefJson2" type="hidden" runat="server" value="[]" />
                                            <input id="hdnJournalDetInsJson2" type="hidden" runat="server" value="[]" />
                                   </ContentTemplate>
                                     <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnNewRow" EventName="Click" />
                                        </Triggers>
                                </asp:UpdatePanel>
                                </div>

                             <div id="divGridControls2" style="width: 100%; height: 30px; border-top: solid 1px #C0C0C0; border-bottom : solid 1px #0b07f5; ">
                                    <table style="width: 40%; height: 100%; text-align: center;" cellspacing="1" cellpadding="1"
                                        border="0">
                                        <tr>
                                            <td style="width: 2px"></td>
                                            
                                            <td align="right" style="text-align: left">
                                               &nbsp;
                                            </td>
                                             <td align="right">
                                                &nbsp;</td>
                                            <td align="right">
                                                &nbsp;</td>
                                            <td align="right" style="width: 90px">&nbsp;
                                            </td>
                                              <td style="width: 90px" align="left">
                                                <asp:Button ID="btnNewRow" runat="server" CssClass="buttonNewRow" Text="" OnClientClick="ShowProgress()" OnClick="btnNewRow_Click" />
                                            </td>
                                            <td style="width: 20px;">
                                                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
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

             <div id="dvContentFooter" class="dvContentFooter">
            <table>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="btnAddNew" runat="server" Text="New" CssClass="buttonNew" OnClick="btnAddNew_Click"  />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonCancel checkIsDirty" OnClick="btnCancel_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonSave checkRequired" AccessKey="s" OnClientClick="if ( ! UserSaveConfirmation()) return false;" OnClick="btnSave_Click" />
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="buttonEdit" OnClick="btnEdit_Click" />
                    </td>

                    <td>
                    </td>
                    <td>
                        
                    </td>
                    <td>
                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" CssClass="buttonRefresh checkIsDirty" OnClick="btnRefresh_Click" />
                    </td>
                    <td>
                        <%--<asp:TextBox ID="TextBox1" runat="server" CssClass="textBox" Style="text-align: right;"
                                        Width="100" TabIndex="-1" Font-Bold="True"></asp:TextBox>--%>
                    </td>
                    <td>
                        <input id="btnClose" type="button" runat="server" class="buttonClose" value="Close" onclick="if (ContentForm) { ContentForm.CloseForm(); }" />
                    </td>
                    <td></td>
                    <td>
                      
                    </td>
                    <td>
                        
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </div>
         
         </div>
</asp:Content>
