<%@ Page Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="LocationwiseLedgerBinding.aspx.cs" Inherits="PG.Web.Accounting.GeneralLedger.LocationwiseLedgerBinding" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

        <script src="../../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
    <link href="../../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        // <!CDATA[
        var isPageResize = true;

        

        var GLAccountServiceLink = '<%=this.GLAccountServiceLink%>';
        var GLGroupServiceLink = '<%=this.GLGroupServiceLink%>';
        var hdnCompanyID = '<%=hdnCompanyID.ClientID%>';


        var txtGLAccount = '<%=txtGLAccount.ClientID%>';
        var btnGLAccount = '<%=btnGLAccount.ClientID%>';
        var hdnGLAccountID = '<%=hdnGLAccountID.ClientID%>';
        var txtGLAccountName = '<%=txtGLAccountName.ClientID%>';
        var hdnGLGroupIDAcc = '<%=hdnGLGroupIDAcc.ClientID%>';


        var txtGLGroup = '<%=txtGLGroup.ClientID%>';
        var btnGLGroup = '<%=btnGLGroup.ClientID%>';
        var hdnGLGroupID = '<%=hdnGLGroupID.ClientID%>';
        var ddlLocation = '<%=ddlLocation.ClientID%>'
        var txtGLGroupName = '<%=txtGLGroupName.ClientID%>';

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


            $("#groupBox").height(contInnerHeight - 10);
            var groupHeight = $("#groupBox").height();
            var groupHeaderHeight = $("#groupHeader").height();
            var groupFooterHeight = $("#groupFooter").height();
            $("#groupContent").height(groupHeight - groupHeaderHeight - groupFooterHeight - 2);  

        }

        function tbopen(key) {
            if (!key) {
                key = '';
            }


            var url = "/Admin/SetPassword.aspx?uid=" + key
            //if (pageInTab == 1)
            if (TabVar.PageMode == Enums.PageMode.InTab) {

                var tdata = new xtabdata();
                tdata.linktype = Enums.LinkType.Direct;
                tdata.id = 6320;
                tdata.name = "SetPassword";
                //tdata.label = "User: " + userid;
                tdata.label = "Set Password";
                tdata.type = 0;
                tdata.url = url;
                tdata.tabaction = Enums.TabAction.InTabReuse;
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

        

        function ReportPrint(key, isPDFAutoPrint) {
            var rptPageLink = ReportViewPageLink;
            if (isPDFAutoPrint) {
                //rptPageLink = ReportPDFPageLink;
                rptPageLink = ReportViewPDFPageLink;
            }

            //var url = "./Report/ReportView.aspx?rk=" + key
            var now = new Date();
            var strTime = now.getTime().toString();
            var url = ReportViewPageLink + "?rk=" + key + "&_tt=" + strTime;

            //var url = rptPageLink + "?rk=" + key;

            iframe = document.getElementById(ifPrintButton);
            if (iframe === null) {
                iframe = document.createElement('iframe');
                iframe.id = hiddenIFrameID;
                //        iframe.style.display = 'none';
                //        iframe.style = 'none';
                document.body.appendChild(iframe);
            }
            iframe.src = url;
        }
        

        function fromParent(val1) {
    alert('this is called from parent: ' + val1);
}

        $(document).ready(function () {
    str = document.body.innerHTML


    //    $('#tblParam tr').each(function () {
    //        if ($(this).find('td:empty').length) $(this).remove();
    //    });

    $("#tblParam tr.rowParam").each(function () {
        var cell = $.trim($(this).find('td').text());
        if (cell.length == 0) {
            //console.log('empty');
            //$(this).addClass('nodisplay');
            $(this).hide();
        }
    });


});

        $(document).ready(function () {

           

            if ($('#' + txtGLAccount).is(':visible')) {
                
           bindGLAccountList();
           
    }
            if ($('#' + txtGLGroup).is(':visible')) {
                bindGLGroupList();
            }
   

});

        function bindGLAccountList() {

            
            var cgColumns = [{ 'columnName': 'glacccode', 'width': '80', 'align': 'left', 'highlight': 2, 'label': 'Code' }
                             , { 'columnName': 'glaccname', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                             , { 'columnName': 'glgroupname', 'width': '130', 'align': 'left', 'highlight': 0, 'label': 'Group' }
                             , { 'columnName': 'glacctypename', 'width': '100', 'align': 'left', 'highlight': 0, 'label': 'Type' }
            ];

            var companyid = $('#' + hdnCompanyID).val();
            var glLocationID = parseInt($('#' + ddlLocation).val());

            //var serviceURL = GLAccountServiceLink + "?isterm=1&includeempty=1&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            var serviceURL = GLAccountServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            serviceURL += "&companyid=" + companyid;
            serviceURL += "&ispaging=1";
            serviceURL += "&acctypefilter=" + Enums.GLAccountTypeFilter.AllAccount;
            serviceURL += "&namecomptype=" + Enums.DataCompareType.Contains;
            //serviceURL += "&gllocationid=" + glLocationID;

            

            var glAccElem = $('#' + txtGLAccount);
            
            $('#' + btnGLAccount).click(function (e) {
               
                //elmID = $(elem).attr('id');
                //$(elem).combogrid("show");
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
                colModel: cgColumns,
                autoFocus: true,
                showError: true,
                width: 500,
                url: serviceURL,
                search: function (event, ui) {
                    var glgroupID = $('#' + hdnGLGroupID).val();
                    var newServiceURL = serviceURL + "&glgroupid=" + glgroupID
                    $(this).combogrid("option", "url", newServiceURL);
                },
                select: function (event, ui) {
                    if (!ui.item) {
                        event.preventDefault();

                        $('#' + hdnGLAccountID).val('0');
                        $('#' + txtGLAccountName).val('');
                        $('#' + hdnGLGroupIDAcc).val('0');
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
                        $('#' + txtGLAccount).val(ui.item.glacccode);
                        $('#' + txtGLAccountName).val(ui.item.glaccname);
                        $('#' + hdnGLGroupIDAcc).val(ui.item.glgroupid);

                        //                $('#' + hdnGLGroupID).val(ui.item.glgroupid);
                        //                $('#' + txtGLGroup).val(ui.item.glgroupcode);
                        //                $('#' + txtGLGroupName).val(ui.item.glgroupname);

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
                    $('#' + txtGLAccountName).val('');
                    $('#' + hdnGLGroupIDAcc).val('0');
                }
            });

            //    $('#' + ddlJournalType).change(function () {
            //        var self = this;
            //        $('#' + hdnJournalID).val('0');
            //        $('#' + txtJournalNo).val('');
            //    });

        }
       

        function bindGLGroupList() {
            //    var cgColumns = [{ 'columnName': 'glgroupcode', 'width': '80', 'align': 'left', 'highlight': 2, 'label': 'Code' }
            //                             , { 'columnName': 'glgroupname', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Name' }
            //                             , { 'columnName': 'glgroupnameparent', 'width': '130', 'align': 'left', 'highlight': 0, 'label': 'Parent' }
            //                            ];


            var cgColumns = [{ 'columnName': 'glgroupcode', 'width': '80', 'align': 'left', 'highlight': 2, 'label': 'Code' }
                             , { 'columnName': 'glgroupnameshort', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Short Name' }
                             , { 'columnName': 'glgroupname', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                             , { 'columnName': 'glgroupnameparent', 'width': '130', 'align': 'left', 'highlight': 0, 'label': 'Parent' }
            ];

            var companyid = $('#' + hdnCompanyID).val();

            var serviceURL = GLGroupServiceLink + "?isterm=1&includeempty=1&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            serviceURL += "&companyid=" + companyid;
            serviceURL += "&ispaging=1";
            serviceURL += "&namecomptype=" + Enums.DataCompareType.Contains;

            var glGrpElem = $('#' + txtGLGroup);

            $('#' + btnGLGroup).click(function (e) {
                //elmID = $(elem).attr('id');
                //$(elem).combogrid("show");
                $(glGrpElem).combogrid("dropdownClick");
            });


            $(glGrpElem).combogrid({
                debug: true,
                searchButton: false,
                resetButton: false,
                alternate: true,
                munit: 'px',
                scrollBar: true,
                showPager: true,
                autoFocus: true,
                showError: true,
                colModel: cgColumns,
                width: 600,
                url: serviceURL,
                search: function (event, ui) {
                    //            var journalTypeID = $('#' + ddlJournalType).val();
                    //            var newServiceURL = serviceURL + "&journaltypeid=" + journalTypeID
                    //            $(this).combogrid("option", "url", newServiceURL);
                },
                select: function (event, ui) {

                    if (!ui.item) {
                        event.preventDefault();
                        $('#' + hdnGLGroupID).val('0');
                        $('#' + txtGLGroupName).val('');
                        return false;
                        //ClearGLAccountData(elemID);
                    }

                    if (ui.item.glgroupid == 0) {
                        event.preventDefault();
                        return false;
                        //ClearGLAccountData(elemID);
                    }
                    else {
                        $('#' + hdnGLGroupID).val(ui.item.glgroupid);
                        $('#' + txtGLGroup).val(ui.item.glgroupcode);
                        $('#' + txtGLGroupName).val(ui.item.glgroupname);

                        var accID = parseInt($('#' + hdnGLAccountID).val());
                        if (accID > 0) {
                            var grpIDAcc = parseInt($('#' + hdnGLGroupIDAcc).val());
                            if (ui.item.glgroupid != grpIDAcc) {
                                $('#' + hdnGLGroupIDAcc).val('0');
                                $('#' + hdnGLAccountID).val('0');
                                $('#' + txtGLAccount).val('');
                                $('#' + txtGLAccountName).val('');
                            }
                        }
                    }
                    return false;
                },

                lc: ''
            });


            $(glGrpElem).blur(function () {
                var self = this;

                var grpCode = $(glGrpElem).val();
                if (grpCode == '') {
                    $('#' + hdnGLGroupID).val('0');
                    $('#' + txtGLGroupName).val('');
                }
            });

            //    $('#' + ddlJournalType).change(function () {
            //        var self = this;
            //        $('#' + hdnJournalID).val('0');
            //        $('#' + txtJournalNo).val('');
            //    });

        }

        function Button1_onclick() {
            //document.getElementById("btnSave").click();
            ///__doPostBack("<%= this.btnSave.UniqueID %>", "");
            __doPostBack("btnSave", "");
        }

function btnSalaryInfo_onclick() {

}

function btnSalaryInfo_onclick() {

}

// ]]>
    </script>

    <style type="text/css">
    
             .dvGroup
        {
         width: 182px;
         height: 20px; 
         border: 1px solid lightgrey;
         
        }
        
        
        .textPopup1
        {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            border: 1px #1B68B8 solid;
            BACKGROUND-COLOR: #FFFFFF;
            COLOR: #000000;
            FONT-SIZE:11px;
            WIDTH: 160px;
            HEIGHT:16px;
            padding-left:2px;
        }
       
       
   
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvPageContent" style="width:100%; height:auto;">
    <div id="dvContentHeader" class="dvContentHeader">
    <div id="dvHeader" class="dvHeader" >
        <asp:Label ID="lblHeader" CssClass="lblHeader" runat="server" Text="Location wise Ledger Binding"></asp:Label>
    </div>
    <!--Message Div -->
    <div id="dvMsg" runat="server" class="dvMessage">
        <asp:Label ID="lblMessage" runat="server" Font-Size="Small" Width="100%"> </asp:Label>
    </div>
     <div id="dvHeaderControl" class="dvHeaderControl">
     </div>
    </div>

    <div id="dvContentMain" class="dvContentMain">
    <div id = "dvControls" style="height:auto; width:100%">
        <div id="dvControlsInner" class="groupBoxContainer boxShadow">    
             <div id="groupBox">
                  <div id="groupHeader" class="groupHeader">
                      <div style="width:100%;height:20px;">
                         <table>
                            <tr>
                             <td>
                                <div id="dvIconEditMode" class="iconView" runat="server" ></div>
                             </td>
                             <td>
                                <span>Location wise Ledger Binding</span> 
                             </td>
                            </tr>
                         </table>
                         
                      </div>
                      
                  </div>
                  <div id="groupContent" class="groupContent" style="width:100%;height:300px; overflow:auto;">
                  <div id="groupContenInner" style="width:100%;height:auto; text-align:left;">
              <table style="text-align:left;" border="0" cellspacing="4" cellpadding="2">
                 <tr>
                   <td style="width:10px;">
                   </td>
                   <td>
                       &nbsp;</td>
                   <td>
                      <%-- <asp:HiddenField ID="hdnGLAccountID" runat="server" Value="0" />--%>
                   </td>
                   <td>
                   </td>
                   <td>
                   </td>
                 </tr>

               <tr>
                <td>
                   </td>
                 <td style="" align="right">
                   <asp:Label id="Label9" runat="server" Text="Location:" ></asp:Label>
                    </td>
                 <td style="" align="left">
                     <asp:DropDownList ID="ddlLocation" runat="server" CssClass="dropDownList" Width="130">
                     </asp:DropDownList>
                      <asp:HiddenField ID="hdnCompanyID" runat="server" Value="0" />
                    </td>
                  <td style="" align="right">
                    </td>
                 <td style="" align="left">
                    </td>
                      <td>
                     </td>
                 </tr>  
                 
            <tr>
                 <td>
                   </td>
                 <td style="" align="right">
                     &nbsp;</td>
                 <td style="" align="left">
                      
                     &nbsp;</td>
                  <td style="" align="right">
                    </td>
                 <td style="" align="left">
                    </td>
                      <td>
                     </td>
                 </tr> 

                  <tr class="rowParam">
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                <asp:Label ID="lblGLGroup" runat="server" Text="GL Group:" ></asp:Label>
                                            </td>
                                            <td colspan="4">
                                                <table cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtGLGroup" runat="server" Width="150px" CssClass="textBox"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <input id="btnGLGroup" type="button" value="" runat="server" class="buttonDropdown"
                                                                tabindex="-1" />
                                                        </td>
                                                        <td class="tdSpacer">
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtGLGroupName" runat="server" Width="150px" CssClass="textBox"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:HiddenField ID="hdnGLGroupID" runat="server" Value="0" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                 
                <tr class="rowParam">
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                <asp:Label ID="lblGLAccount" runat="server" Text="Account /Sub Ledger:"></asp:Label>
                                            </td>
                                            <td colspan="4">
                                                <table cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtGLAccount" runat="server" Width="150px" CssClass="textBox"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <input id="btnGLAccount" type="button" value="" runat="server" class="buttonDropdown"
                                                                tabindex="-1" />
                                                        </td>
                                                        <td class="tdSpacer">
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtGLAccountName" runat="server" Width="150px" CssClass="textBox"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:HiddenField ID="hdnGLAccountID" runat="server" Value="0" />
                                                            <asp:HiddenField ID="hdnGLGroupIDAcc" runat="server" Value="0" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                  


                 
                 <tr>
                 <td>
                   </td>
                 <td style="" align="right">
                     &nbsp;</td>
                 <td style="" align="left">
                      
                     &nbsp;</td>
                  <td style="" align="right">
                    </td>
                 <td style="" align="left">
                    </td>
                      <td>
                     </td>
                 </tr>  
                 
                 
                  <tr>
                  <td>
                   </td>
                 <td style="" align="right">
                     &nbsp;</td>
                
                  <td style="" align="left">

                      </td>
                 <td style="" align="left">
                     </td>
                      <td>
                     </td>
                 </tr>   
                 
                 <tr>
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
                  <div id="groupFooter" class="groupFooter">
                      <div style="width:100%;height:12px;">
                      
                      </div>
                  </div>
            </div>
          </div>  


        </div>  
    </div>
    

     <div id="dvContentFooter" class="dvContentFooter">
         <table>
              <tr>
                <td>
                </td>
                <td>
                   <asp:Button ID="btnAddNew" runat="server" Text="New" CssClass="buttonNew" 
                        onclick="btnAddNew_Click" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonCancel checkIsDirty" 
                     onclick="btnCancel_Click"   />
                </td>
                <td>
                  <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonSave" 
                    onclick="btnSave_Click" />
                    <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="buttonEdit" 
                        onclick="btnEdit_Click"   />
                </td>
                <td>
                 <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="buttonDelete" 
                        onclick="btnDelete_Click"   />
                </td>
                
                <td>
                   <asp:Button ID="btnRefresh" runat="server" Text="Refresh" CssClass="buttonRefresh checkIsDirty" 
                        onclick="btnRefresh_Click"   />
                   </td>

               
                <td>
                   
                 </td>
                

                 <td>
                    <input id="btnClose" type="button" runat="server" class="buttonClose" value="Close" onclick="if (ContentForm){ ContentForm.CloseForm();}" />
                </td>


              </tr>
           </table>    
    
    </div>
    </div> 
</asp:Content>

