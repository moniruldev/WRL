<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="Production_Battery_Rejection_Entry.aspx.cs" Inherits="PG.Web.Production.Production_Battery_Rejection_Entry" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

     <script src="../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
    <script src="../javascript/jquery.attributeobserver.js" type="text/javascript"></script>
    <link href="../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">

        var isPageResize = true;
        ContentForm.CalendarImageURL = "../image/calendar.png";
        var ItemListServiceLink = '<%=this.ItemListServiceLink%>';
        var ddlDeptID = '<%=ddlDept_ID.ClientID%>';
        var gridViewIDDet = '<%=grdRejectionDeclare.ClientID%>';
        var gridUpdatePanelIDDet = '<%=UpdatePanel1.ClientID%>';

    

        var updateProgressID = '<%=UpdateProgress2.ClientID%>';
        var hdnDeptID = '<%=hdnDept_ID.ClientID %>';
        var btnSave = '<%=btnSave.ClientID%>';

        function isNumberKey(evt, obj) {

            var charCode = (evt.which) ? evt.which : event.keyCode
            var value = obj.value;
            var dotcontains = value.indexOf(".") != -1;
            if (dotcontains)
                if (charCode == 46) return false;
            if (charCode == 46) return true;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }

        function ShowProgress() {
            $('#' + updateProgressID).show();
        }
        function UserSaveConfirmation() {
            return confirm("Are you sure you want to Save?");
        }
        function PageResizeCompleted(pg, cntMain) {
            resizeContentInner(cntMain);
        }

        function UserSaveConfirmation() {
            return confirm("Are you sure you want to Save?");
        }

        function UserAuthorizeConfirmation() {
            return confirm("Are you sure you want to Authorized?");
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
                        bindItemList(gridViewIDDet);

                    }

                }

                bindItemList(gridViewIDDet);
            });

          

            


        });

      



        function bindItemList(gridViewID) {
            var cgColumns = [
                               { 'columnName': 'itemname', 'width': '200', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                             , { 'columnName': 'uomname', 'width': '70', 'align': 'left', 'highlight': 4, 'label': 'Uom Name' }
                           //  , { 'columnName': 'itemcode', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Code' }
                             , { 'columnName': 'itemgroupdesc', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Group Name' }
                              , { 'columnName': 'item_type', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Item Type' }
                             //, { 'columnName': 'closing_qty', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Cls Qty' }
                             //, { 'columnName': 'class_name', 'width': '120', 'align': 'left', 'highlight': 4, 'label': 'Class Name' }
                           //  , { 'columnName': 'bomname', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'BOM Name' }
                           //  , { 'columnName': 'bomno', 'width': '180', 'align': 'left', 'highlight': 4, 'label': 'BOM NO' }
                             , { 'columnName': 'uomid', 'width': '50', 'align': 'left', 'highlight': 4, 'label': 'Uom ID' }
                            //  , { 'columnName': 'bomid', 'width': '50', 'align': 'left', 'highlight': 4, 'label': 'BOM ID' }


            ];


            //var companyid = $('#' + hdnCompanyID).val();
            //var depthead = $('#' + hdnEmpCode).val();
            var ddlDEPT_ID = $('#' + ddlDeptID).val();

            //var serviceURL = GLAccountServiceLink + "?isterm=1&includeempty=1&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            var serviceURL = ItemListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            //serviceURL += "&companyid=" + companyid;
            serviceURL += "&ispaging=1&isFinished=Y&batterytypeid=1&deptid=" + ddlDEPT_ID;
            //serviceURL += "&locationid=" + locationid;
            // serviceURL += "&empstatus=" + "A";
            //serviceURL += "&acctypefilter=" + Enums.GLAccountTypeFilter.AllAccount;
            // serviceURL += "&namecomptype=" + Enums.DataCompareType.Contains;
            var gridSelector = "#" + gridViewID;
            $(gridSelector).find('input[id$="txtItemName"]').each(function (index, elem) {
                ///list click
                var elemRow = $(elem).closest('tr.gridRow');
                var hdnItemIDElem = $(elemRow).find('input[id$="txtItemName"]');
                //var prevGLCode = '';
                $(elem).closest('tr').find('input[id$="btnItemName"]').click(function (e) {
                    elmID = $(elem).attr('id');
                    //$(elem).combogrid("show");
                    $(elem).combogrid("dropdownClick");
                });
                //var compNameElem = $('#' + txtCompanyName);

                //$('#' + btnCompanyID).click(function (e) {
                //    //elmID = $(elem).attr('id');
                //    //$(elem).combogrid("show");
                //    $(compNameElem).combogrid("dropdownClick");
                //});


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
                        //var vgroupid = $(elemRowCur).find('input[id$="hdngroupId"]').val();
                        //var vgroupid = $('#' + hdngroupId).val();
                        var newServiceURL = serviceURL;
                        //var newServiceURL = serviceURL + "&companycode=" + companyCode + "&branchcode=" + branchCode + "&deptcode=" + deptCode
                        newServiceURL = JSUtility.AddTimeToQueryString(newServiceURL);
                        $(this).combogrid("option", "url", newServiceURL);


                    },

                    select: function (event, ui) {
                        //alert(ui.item.typename);
                        //$(".txtComboGrid").val(ui.item.code);
                        elemID = $(elem).attr('id');
                        //                    if (!validateGLAccount(elemID, ui.item)) {
                        //                        $(elem).val(prevGLCode);
                        //                        return false;
                        //                    }
                        if (!ui.item) {
                            event.preventDefault();
                            ClearItemData(elemID);
                            return false;
                            //ClearGLAccountData(elemID);
                        }
                        if (ui.item.id == 0) {
                            event.preventDefault();
                            return false;
                            //ClearGLAccountData(elemID);
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
                        ClearItemData(elemID);
                    }
                    else {
                        var serviceURL = ItemListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
                        serviceURL += "&ispaging=1";

                        var prcNo = GetItemNo(eCode, serviceURL);

                        if (prcNo == null) {
                            ClearItemData(elemID);
                        }
                        else {
                            SetItemData(elemID, grp);
                        }

                    }
                });

            });

        }


        function SetItemData(txtItemCodeID, data) {
            $('#' + txtItemCodeID).val(data.itemid);

            var detRow = $('#' + txtItemCodeID).closest('tr.gridRow');
            $(detRow).find('input[id$="hdnItemID"]').val(data.itemid);
            $(detRow).find('input[id$="txtItemName"]').val(data.itemname);
            //$(detRow).find('input[id$="hdnUomID"]').val(data.uomid);
            //$(detRow).find('input[id$="txtUOM_NAME"]').val(data.uomname);
        }

        function ClearItemData(txtItemID) {
            //$('#' + txtGLAccCodeID).val('');
            var detRow = $('#' + txtItemID).closest('tr.gridRow');
            $(detRow).find('input[id$="hdnItemId"]').val('0');
            $(detRow).find('input[id$="txtItemName"]').val('');
            //$(detRow).find('input[id$="hdnUomID"]').val('0');
            //$(detRow).find('input[id$="txtUOM_NAME"]').val('');

        }
        function GetItemNo(eCode, serviceURL) {
            var prcNo = null;
            var isError = false;
            var isComplete = false;
            //ajax call

            var newServiceURL = serviceURL + " &selectedId=" + eCode;
            var dummyVar = $.ajax({
                type: "GET",
                cache: false,
                async: false,
                dataType: "json",
                url: newServiceURL,

                success: function (prddata) {
                    //            if (accdata.menu[0].count > 0) {
                    //                menu = menudata.menu[0];
                    //            }
                    if (prddata.rows.length > 0) {
                        prcNo = prddata.rows[0];
                    }
                },
                complete: function () {
                    if (!isError) {
                        //return;
                        //alert (menu.name);
                    }
                    isComplete = true;
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    isError = true;
                    alert(textStatus);
                }
            });
            return prcNo;
        }


    </script>

     <style type="text/css">
         .auto-style1 {
             width: 209px;
         }
         .auto-style3 {
             width: 169px;
         }
         .auto-style4 {
             width: 272px;
         }
         .colourdisabletextBox {}
         .auto-style5 {
             width: 104px;
         }
     </style>

</asp:Content>




<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
        <div id="dvContentHeader" class="dvContentHeader" >

        <div id="dvHeader" class="dvHeader_Prod" align="left">
            <asp:Label ID="lblHeader" runat="server" Text="Battery Reject Entry" CssClass="lblHeader" Font-Bold="true" Font-Size="15px" > </asp:Label>

        </div>

        <div id="dvMessage" class="dvMessage">
                <asp:Label ID="lblMessage" runat="server" CssClass="lblMessage" Text="" Width="100%"></asp:Label>
            </div>
</div>

    <div id="dvContentMain" runat="server" align="left" > 

        <div id="dvControlsHead" style="height: auto; width: 100%;" align="left" > 
            <table border="0" cellspacing="4" cellpadding="2" align="left" style="width: 90%" id="lblRejectionEntry" >
                <tr>
                    <td style="text-align:right" class="auto-style3">
                        <asp:Label ID="lblRejectionNo" runat="server" Text="Rejection NO : "></asp:Label>

                    </td>
                    <td style="text-align:left" class="auto-style1">
                        <asp:TextBox ID="txtRejectionNo" runat="server" CssClass="colourdisabletextBox" Enabled="false" Width="189px" Height="18px"></asp:TextBox>
                    </td>
                    <td>

                    </td>
                    <td style="text-align: right" class="auto-style5">
                         <asp:Label ID="lblRejectionDate" runat="server"  Text="Rejection Date :"></asp:Label>
                     </td>
                    <td class="auto-style4">
                         <asp:TextBox ID="txtRejectionDate" runat="server" CssClass="textBox textDate dateParse" Style="text-align: left;" Width="100px"></asp:TextBox>
                        <asp:TextBox ID="txtRejectionDateID" runat="server" CssClass="textBox" Style="text-align: left; display: none" Width="150px"></asp:TextBox>
                       </td>
                    <td><asp:HiddenField ID="hdnRejectionID" runat="server" /></td>
                    <td>
                        <asp:HiddenField ID="hdnLoggedInUser" runat="server" />
                    </td>

                </tr>

                <tr>
                     <td style="text-align: right" class="auto-style3">
                                  <asp:Label ID="lblDept_ID" runat="server" Text="Department : "></asp:Label>
                              </td>
                              <td style="text-align: left" class="auto-style1">

                                  <asp:DropDownList ID="ddlDept_ID" runat="server" CssClass="dropDownList required" Width="190px" ></asp:DropDownList>
                              </td>
                    <td></td>
                    <td style="text-align: right" class="auto-style5">
                         <asp:Label ID="lblSHIFT_ID" runat="server" Text="Shift : "></asp:Label>
                    </td>
                     <td class="">
                     <asp:DropDownList ID="ddlShift_ID" runat="server" CssClass="dropDownList" Width="155px"></asp:DropDownList>
                     </td>
                    <td> <asp:HiddenField id="hdnDept_ID" runat="server"/> </td>
                    <td>
                        <asp:HiddenField ID="hdnCompanyID" runat="server" />
                    </td>

                </tr>

           


            </table>
        </div>

    

    <div id="dvGridControl" style="height:auto">
            <div id="dvGrid"  style="border:1px solid blue; width:1000px" >

                <div id="dvGridHeader" style="width:1000px;">
                    <table width="900px">
                      <tr class="headerRow_Prod" >
                          <td style="width:40px; " align="center">SL#</td>
                          <td style="width:200px; " align="center">Item Name</td>
                          <td style="width:100px; " align="center">Reject Qty</td>
                          <td style="width:100px; " align="center">Remarks</td>
                          <td style="width:18px; " align="center">Delete</td>
                      </tr>
                  </table> 
                </div>
                <div id="dvGridMain" class="dvGrid"  style="width:900px; height:300px; overflow: auto;">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="grdRejectionDeclare" runat="server" AutoGenerateColumns="false" ShowHeader="false" CellPadding="1" 
                                 CellSpacing="1" GridLines="Both" DataKeyNames="REJECT_MST_ID" ClientIDMode="AutoID" OnRowCommand="grdRejectionDeclare_RowCommand"
                                  OnRowCreated="grdRejectionDeclare_RowCreated" OnRowDataBound="grdRejectionDeclare_RowDataBound" OnRowDeleting="grdRejectionDeclare_RowDeleting">
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <Columns>
                                    <asp:TemplateField ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSlNo" runat="server" Text='<%# Bind("SLNO")%>' style="text-align:center; width:80px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <div>
                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="txtItemName" runat="server" CssClass="textBox textAutoSelect" Width="350px" Text='<%# Bind("ITEM_NAME") %>'></asp:TextBox>
                                                                <asp:HiddenField ID="hdnItemID" runat="server" Value='<%# Bind("ITEM_ID") %>' />
                                                                <asp:HiddenField ID="hdnRejectionDTLID" runat="server" Value='<%# Bind("REJECT_DTL_ID") %>' />
                                                            </td>
                                                            <td>
                                                                <input id="btnItemName" type="button" value="" runat="server" class="buttonDropdown" tabindex="-1" />
                                                            </td>
                                                            <td style="text-align :right;">
                                                                <asp:TextBox ID="txtRejectionQty" runat="server" CssClass="textBox textAutoSelect"  Width="185px" Text='<%# Bind("REJECT_QTY") %>' onkeypress=" return isNumberKey(event,this);"></asp:TextBox>
                                                            </td>
                                                           
                                                             <td >
                                                               <asp:TextBox ID="txtRemarks" runat="server" CssClass="textBox textAutoSelect"  Width="185px" Text='<%# Bind("REMARKS") %>'></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnDeleteRow" runat="server" CssClass="buttonDeleteIcon" Height="16px" Width="18px" CommandName="delete"></asp:LinkButton>
                                        </ItemTemplate>
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
            </div>

                <div id="divGridControls2" style="width: 100%; height: 30px; border-top: solid 1px #C0C0C0; border-bottom : solid 1px #0b07f5; ">
                     <table style="width: 70%; height: 100%; text-align: center;" cellspacing="1" cellpadding="1" border="0">
                            <tr>
                                          
                                            <td style="width:10px" align="left">
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

                                            <td align="right">
                                                &nbsp;</td>
                                           
                                            <td align="right" style="width: 90px">&nbsp;
                                            </td>

                                        </tr>
                            
                                    </table>

                </div>
            </div>
        <div id="dvContentFooter" class="dvContentFooter">
            <table>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="btnAddNew" text="New" CssClass="buttonNew" OnClick="btnAddNew_Click" runat="server" />

                    </td>
                    <%--<td>
                        <asp:Button ID="btnCancel" runat="server" CssClass="buttonCancel" Text="Cancel" OnClick="btnCancel_Click" />
                    </td>--%>
                    <td>
                        <asp:Button ID="btnSave" runat="server" CssClass="buttonSave checkRequired" Text="Save" AccessKey="s" OnClientClick="if ( ! UserSaveConfirmation()) return false;" OnClick="btnSave_Click" />
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="buttonEdit" OnClick="btnEdit_Click" />
                    </td>
                     <td>
                        <asp:Button ID="btnAuthorize" runat="server" Text="Authorize" CssClass="buttoncommon" OnClientClick="if ( ! UserAuthorizeConfirmation()) return false;" OnClick="btnAuthorize_Click" />
                    </td>
                     <td>
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="buttonDelete" OnClick="btnDelete_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnRefresh" runat="server" Text="Refresh" CssClass="buttonRefresh checkIsDirty" OnClick="btnRefresh_Click" />
                        </td>

                         <td>
                            <input id="btnClose" type="button" runat="server" class="buttonClose" value="Close" onclick="if (ContentForm) { ContentForm.CloseForm(); }" />
                        </td>
                </tr>
            </table>



        </div>

 </div>


</asp:Content>