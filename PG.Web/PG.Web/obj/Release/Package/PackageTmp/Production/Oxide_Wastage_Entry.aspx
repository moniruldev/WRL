<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="Oxide_Wastage_Entry.aspx.cs" Inherits="PG.Web.Production.Oxide_Wastage_Entry" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
    <script src="../javascript/jquery.attributeobserver.js" type="text/javascript"></script>
    <link href="../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        var isPageResize = true;
        ContentForm.CalendarImageURL = "../image/calendar.png";

        var ItemListServiceLinkd = '<%=this.ItemListServiceLinkd%>';
        var ProductionDateWiseBatch_List = '<%=this.ProductionDateWiseBatch_List%>';

        var gridUpdatePanelIDDet = '<%=UpdatePanel1.ClientID%>';
        var gridViewIDDet = '<%=GridView1.ClientID%>';
        var updateProgressID = '<%=UpdateProgress2.ClientID%>';

        var ddlDEPT_ID = '<%=ddlDEPT_ID.ClientID%>';
        var txtTotQty = '<%=txtTotQty.ClientID%>';

        $(document).on('keyup', '.txtQty', function () {
            sumGrandQty();
        });

        $(document).on('blur', '.txtQty', function () {
            sumGrandQty();
        });
        var addQty = 0;
        var addAmt = 0;
        var addWt = 0;


        function UserDeleteConfirmation() {
            return confirm("Are you sure you want to Save ?");
        }
        function UserSaveConfirmation() {
            return confirm("Are you sure you want to Save?");
        }

        function UserAuthorizeConfirmation() {
            return confirm("Are you sure you want to Authorized?");
        }

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

        function calcWeight(texbx) {
            var detRow = $(texbx).closest('tr.gridRow');
            var pcqty = $(detRow).find('input[id$="hdnPANEL_PC"]').val();
            var standardweight = $(detRow).find('input[id$="txtITEM_STANDARD_WEIGHT_KG"]').val();
            var qty = $(detRow).find('input[id$="txtRejection_QTY"]').val();
            var productionqty = $(detRow).find('input[id$="txtPRODUCTION_QTY"]').val();
            var tgoodqty = Number(productionqty) - Number(qty);
            $(detRow).find('input[id$="txtGOOD_QTY"]').val(tgoodqty);
            var tweight = qty * standardweight;
            $(detRow).find('input[id$="txtREJECTION_WEIGHT"]').val(tweight);
        }
        function onlyNos(e, t) {
            try {
                if (window.event) {
                    var charCode = window.event.keyCode;
                }
                else if (e) {
                    var charCode = e.which;
                }
                else { return true; }
                if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                    return false;
                }
                return true;
            }
            catch (err) {
                alert(err.Description);
            }
        }


        function GetTotalSumAddedQty() {
            debugger;
            var totAdd = 0;
            $(document).find('.txtQty').each(function (index, elem) {
                addQty = parseFloat(JSUtility.GetNumber($(elem).val()));

                if (!isNaN(addQty)) {
                    totAdd += addQty;
                }
            });
            return totAdd;
        }

 

        function sumGrandQty() {
            var totAdded = GetTotalSumAddedQty();
            $("#" + txtTotQty).val(JSUtility.FormatCurrency(totAdded));
        }

        
        function showOverlay() {
            document.getElementById("overlay").style.display = "block";
        }

        function hideOverlay() {
            document.getElementById("overlay").style.display = "none";
        }

        function ShowProgress() {
            $('#' + updateProgressID).show();
        }

        function UserDeleteConfirmation() {
            return confirm("Are you sure you want to Save and Authorized?");
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

        // alert('OK');
        $(document).ready(function () {

            var pageInstance = Sys.WebForms.PageRequestManager.getInstance();

            pageInstance.add_pageLoaded(function (sender, args) {
                var panels = args.get_panelsUpdated();
                for (i = 0; i < panels.length; i++) {
                    //alert(panels[i].id);
                    //ContentForm.InitDefualtFeatureInScope(panels[i].id);

                    if (panels[i].id == gridUpdatePanelIDDet) {
                        bindItemList(gridViewIDDet);
                        sumGrandQty();
                       
                    }

                }
                //$('#' + dvGridDetailsPopup).parent().appendTo(jQuery("form:first"));
                //gridTaskAfter();

            });


            // alert('OK 1');

            //if ($('#' + txtCompanyName).is(':visible')) {

            //    bindCompanyList();
            //}

            //if ($('#' + txtCustomerName).is(':visible')) {

            //    bindCustomerList();
            //}
            ////alert('OK 1');
            //bindItemGroupList(gridViewIDDet);
            //alert('OK 2');

            bindItemList(gridViewIDDet);
            bindBatchList(gridViewIDDet);
        });

      

        function bindItemList(gridViewID) {
            var cgColumns = [
                               { 'columnName': 'itemname', 'width': '200', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                             , { 'columnName': 'uomname', 'width': '70', 'align': 'left', 'highlight': 4, 'label': 'Uom' }
                             //, { 'columnName': 'bomname', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'BOM Name' }
            ];
            var DEPT_ID = $('#' + ddlDEPT_ID).val();
            var serviceURL = ItemListServiceLinkd + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            serviceURL += "&ispaging=1&isFinished=W&deptid=" + DEPT_ID;
          

            var gridSelector = "#" + gridViewID;
            $(gridSelector).find('input[id$="txtITEM_NAME"]').each(function (index, elem) {
                ///list click
                var elemRow = $(elem).closest('tr.gridRow');

                var hdnItemIDElem = $(elemRow).find('input[id$="txtITEM_NAME"]');

                //var prevGLCode = '';

                $(elem).closest('tr').find('input[id$="btnITEM_NAME"]').click(function (e) {
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
                    colModel: cgColumns,
                    autoFocus: true,
                    showError: true,
                    width: 350,
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
                        return false;
                    }
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
                        var vbatchNo = $(elemRowCur).find('input[id$="hdnBatchNo"]').val();
                        var serviceURL = ItemListServiceLinkd + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
                        serviceURL += "&ispaging=1&batchno=" + vbatchNo;

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

        function ClearItemData(txtItemID) {
            //$('#' + txtGLAccCodeID).val('');
            var detRow = $('#' + txtItemID).closest('tr.gridRow');
            $(detRow).find('input[id$="hdnITEM_ID"]').val('0');
            $(detRow).find('input[id$="txtITEM_NAME"]').val('');
            $(detRow).find('input[id$="txtITEM_STANDARD_WEIGHT_KG"]').val('0');
            $(detRow).find('input[id$="hdnPANEL_PC"]').val('0');

        }
        function SetItemData(txtItemCodeID, data) {
            $('#' + txtItemCodeID).val(data.itemid);

            var detRow = $('#' + txtItemCodeID).closest('tr.gridRow');
            $(detRow).find('input[id$="hdnITEM_ID"]').val(data.itemid);
            $(detRow).find('input[id$="txtITEM_NAME"]').val(data.itemname);
            $(detRow).find('input[id$="txtITEM_STANDARD_WEIGHT_KG"]').val(data.itemstandardweightkg);
            $(detRow).find('input[id$="hdnPANEL_PC"]').val(data.panelpc);
            $(detRow).find('input[id$="hdnUOM_ID"]').val(data.uomid);
            $(detRow).find('input[id$="txtUOM_NAME"]').val(data.uomname);
            $(detRow).find('input[id$="txtPRODUCTION_QTY"]').val(data.productionqty);

        }


        //Set Batch List

        function bindBatchList(gridViewID) {
            var cgColumns = [
                               { 'columnName': 'batchNO', 'width': '200', 'align': 'left', 'highlight': 4, 'label': 'Batch No' }
            ];
            var DEPT_ID = $('#' + ddlDEPT_ID).val();
            var proddate = $('#' + txtProductionDate).val();
            var serviceURL = ProductionDateWiseBatch_List + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            serviceURL += "&ispaging=1&isFinished=Y&deptid=" + DEPT_ID;
            // + "&proddate=" + proddate
            var gridSelector = "#" + gridViewID;
            $(gridSelector).find('input[id$="txtBatchNo"]').each(function (index, elem) {
                ///list click
                var elemRow = $(elem).closest('tr.gridRow');
                var hdnItemIDElem = $(elemRow).find('input[id$="txtBatchNo"]');
                $(elem).closest('tr').find('input[id$="btnBatchNo"]').click(function (e) {
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
                    showPager: false,
                    colModel: cgColumns,
                    autoFocus: true,
                    showError: true,
                    width: 115,
                    url: serviceURL,
                    search: function (event, ui) {
                        var elemRowCur = $(elem).closest('tr.gridRow');
                        var DEPT_ID = $('#' + ddlDEPT_ID).val();
                        var proddate = $('#' + txtProductionDate).val();
                        var serviceURL = ProductionDateWiseBatch_List + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
                        serviceURL += "&ispaging=1&isFinished=Y&deptid=" + DEPT_ID ;
                        var newServiceURL = serviceURL //+ "&groupid=" + vgroupid+ "&proddate=" + proddate
                        newServiceURL = JSUtility.AddTimeToQueryString(newServiceURL);
                        $(this).combogrid("option", "url", newServiceURL);
                    },

                    select: function (event, ui) {
                        elemID = $(elem).attr('id');
                        if (!ui.item) {
                            event.preventDefault();
                            ClearBatchData(elemID);
                            return false;
                        }
                        if (ui.item.id == 0) {
                            event.preventDefault();
                            return false;
                        }
                        else {
                            SetBatchData(elemID, ui.item);
                        }
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
                        var serviceURL = ProductionDateWiseBatch_List + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
                        serviceURL += "&ispaging=1";

                        var prcNo = GetItemNo(eCode, serviceURL);

                        if (prcNo == null) {
                            ClearBatchData(elemID);
                        }
                        else {
                            SetBatchData(elemID, grp);
                        }

                    }
                });

            });

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

        function ClearBatchData(txtBatch) {
            //$('#' + txtGLAccCodeID).val('');
            $(detRow).find('input[id$="txtBatchNo"]').val('');
            var detRow = $('#' + txtBatch).closest('tr.gridRow');
            $(detRow).find('input[id$="hdnBatchNo"]').val('');

        }
        function SetBatchData(txtBatch, data) {
            $('#' + txtBatch).val(data.itemid);

            var detRow = $('#' + txtBatch).closest('tr.gridRow');
           
            $(detRow).find('input[id$="txtBatchNo"]').val(data.batchNO);
            $(detRow).find('input[id$="hdnBatchNo"]').val(data.batchNO);
            
            

           


        }


    </script>

     <style type="text/css">

     .overlay {
        background-color: #000;
        cursor: wait;
        display: none;
        height: 100%;
        left: 0;
        opacity: 0.4;
        position: fixed;
        top: 0;
        width: 100%;
        z-index: 9999998;
    }

        
      
      
        .hidden
        {
            /*visibility:hidden;*/
            display:none;
        }
     </style>

        

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="dvPageContent" style="width: 100%; height: auto;">
        <div id="dvContentHeader" class="dvContentHeader">
            <div id="dvHeader" class="dvHeader_Prod">
                <asp:Label ID="lblHeader" CssClass="lblHeader" runat="server" Text="Oxide Wastage Entry"></asp:Label>
            </div>
            <!--Message Div -->
            <div id="dvMsg" runat="server" class="dvMessage" style="">
                            <asp:HiddenField ID="hdnWASTAGE_ID" runat="server" />
                             <asp:HiddenField ID="hdnDeptID" runat="server" Value ="0" />
                              
                <asp:Label ID="lblMessage" runat="server" Font-Size="Small" Width="100%"> </asp:Label>
            </div>
            <div id="dvHeaderControl" class="dvHeaderControl">
            </div>
        </div>

        <div id="dvContentMain" class="dvContentMain">
            <div id="dvControlsHead" style="height: auto; width: 100%;">
                <table>
                    <tr>
                         

                        <td align="right" class="auto-style2">
                            <asp:Label ID="lblWASTAGENo" runat="server" Text="Wastage NO :" style="font-weight: 700"></asp:Label>
                        </td>
                        <td class="auto-style2">
                            <asp:TextBox ID="txtWASTAGE_NO" runat="server" Style="text-align: left; font-weight: 700;" CssClass="textBox" ForeColor="Red"  Enabled="False"></asp:TextBox>
                        </td>
                        <td align="right">
                              <asp:Label ID="lblWASTAGE_DATE" runat="server" Text="Date :" style="font-weight: 700"></asp:Label>
                        </td>
                        <td>
                              <asp:TextBox ID="txtWASTAGE_DATE" runat="server" CssClass="textBox textDate dateParse" Style="text-align: left;" Width="100px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                                  <asp:Label ID="lblDEPT_ID" runat="server" Text="Department : " style="font-weight: 700"></asp:Label>
                              </td>
                              <td style="text-align: left">

                                  <asp:DropDownList ID="ddlDEPT_ID" runat="server" CssClass="dropDownList required" Width="190px" ></asp:DropDownList>
                              </td>
                    </tr>
                </table>
            </div>
            <div id="dvControls" style="width: 100%; height: 400px;">
                <div id="Div3" runat="server" class="" style="width: 100%; text-align: left;">
                    <span style="font-weight: bold;font-size : 15px;color :#ff3b00;">Wastage Details: </span>
                </div>

                <div id="Div1" runat="server" class="" style="width: auto; height: auto; text-align: left">
                    <div id="dvGridHeaderClosing" style="width: 700px; height: 25px; font-size: smaller;" class="subHeader_Prod">
                        <table style="height: 100%; color: #FFFFFF; font-weight: bold; text-align: left;"
                            class="defFont" cellspacing="1" cellpadding="1">
                            <tr class="headerRow_Prod">
                                <td width="65px" class="headerColCenter_prod">SL#
                                </td>
                                <td width="250px" class="headerColCenter_prod">Item Name
                                </td>
                                <td width="15px" class="headerColLeft"></td>
                                 <td width="70px" class="headerColCenter_prod">UOM
                                </td>
                                 <td width="80px" class="headerColCenter_prod"> Qty
                                </td>
                                <td width="150px" class="headerColCenter_prod">Remarks
                                </td>
                                <td width="16px" class="headerColCenter">Delete
                                </td>
                                
                            </tr>
                        </table>
                    </div>
                    <div id="dvGridClosing" style="width: 1024px; height: 250px; overflow: auto;" class="dvGrid">
                      <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowHeader="False" 
                                    CellPadding="1" CellSpacing="1" ForeColor="#333333" GridLines="Both" DataKeyNames="ITEM_ID" 
                                    EnableModelValidation="True" ClientIDMode="AutoID" OnRowCommand="GridView1_RowCommand" OnRowCreated="GridView1_RowCreated" OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting"  >
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <Columns>
                                       

                                        <asp:TemplateField HeaderText="SL#">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSLNO" runat="server" Text='<%# Bind("SI") %>' Style="text-align: center;"
                                                    Width="60px">
                                                </asp:Label>
                                                 <asp:HiddenField ID="hdnRecordStateInt" runat="server" Value='<%# Bind("_RecordStateInt") %>' />
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
                                                                    <asp:HiddenField ID="hdnWASTAGE_DTL_ID" runat="server" Value='<%# Bind("WASTAGE_DTL_ID") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtITEM_NAME" runat="server" CssClass="textBox textAutoSelect" Width="250px" Text='<%# Bind("ITEM_NAME") %>'></asp:TextBox>
                                                                    <asp:HiddenField ID="hdnITEM_ID" runat="server" Value='<%# Bind("ITEM_ID") %>' />
                                                                </td>
                                                                <td>
                                                                    <input id="btnITEM_NAME" type="button" value="" runat="server" class="buttonDropdown" tabindex="-1" />
                                                                </td>
                                                                  <td>

                                                                    <asp:TextBox ID="txtUOM_NAME" runat="server" CssClass="textBox textAutoSelect"  Width="65px" Text='<%# Bind("UOM_NAME") %>'></asp:TextBox>
                                                                    <asp:HiddenField ID="hdnUOM_ID" runat="server" Value='<%# Bind("UOM_ID") %>' />
                                                                </td>
                                                                 
                                                                <td>
                                                                    <asp:TextBox ID="txtITEM_QTY" runat="server" CssClass="txtQty textBox textAutoSelect " style="text-align: right;"   Width="75px" align="right" Text='<%# Bind("ITEM_QTY") %>'   onkeypress=" return isNumberKey(event,this);"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtREMARKS" runat="server" CssClass="textBox textAutoSelect" Width="140px" Text='<%# Bind("REMARKS") %>' Style=""></asp:TextBox>
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
                                                                <div style="position: relative; height: 100%; width: 100%;">
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
                                <input id="Hidden1" type="hidden" runat="server" value="[]" />
                                <input id="Hidden2" type="hidden" runat="server" value="[]" />
                       </ContentTemplate>
                       <%--  <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnNewRow" EventName="Click" />
                            </Triggers>--%>
                    </asp:UpdatePanel>


                    </div>
                    <div id="divGridControls2" style="width: 100%; height: 25px; border-top: solid 1px #C0C0C0;">
                                            <table style="width: auto; height: 100%; text-align: center;" cellspacing="1" cellpadding="1"
                                                border="0">
                                                <tr>
                                                    <td style="width: 2px"></td>
                                                    <td style="width: 90px" align="left">
                                                        <asp:Button ID="btnNewRow2" runat="server" CssClass="buttonNewRow" Text="" OnClientClick="ShowProgress()" OnClick="btnNewRow2_Click" />
                                                    </td>
                                                    <td style="width: 20px;">
                                                        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                                            DisplayAfter="300">
                                                            <ProgressTemplate>
                                                                <asp:Image ID="imgProgress2" runat="server" ImageUrl="~/image/loader.gif" />
                                                            </ProgressTemplate>
                                                        </asp:UpdateProgress>
                                                    </td>
                                                    <td style="width : 200px;"></td>
                                                    <td align="right">&nbsp;
                                                    </td>
                                                    <td align="right">
                                                        <asp:Label ID="lbltotalSalesAmount" runat="server" Text="Total Qnty:" Font-Bold="True" Visible="true"></asp:Label>
                                                    </td>
                                                    <td align="right">
                                                        <asp:TextBox ID="txtTotQty" runat="server"   style="text-align: right;" CssClass="textBox"
                                                            Width="100" TabIndex="-1" Font-Bold="True" Visible="true"></asp:TextBox>
                                                    </td>
                                                     <td align="right"  Width="80">
                                                         &nbsp;</td>
                                                    <td align="right">&nbsp;
                                                        
                                                    </td>

                                                </tr>
                                            </table>
                                        </div>
                </div>
                

            
        </div>
        <div id="dvContentFooter" class="dvContentFooter">
            <table>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="btnAddNew" runat="server" Text="New" CssClass="buttonNew" OnClick="btnAddNew_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonCancel checkIsDirty" />
                    </td>
                    <td>
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonSave checkRequired" AccessKey="s" OnClick="btnSave_Click" OnClientClick="if ( ! UserSaveConfirmation()) return false;" />
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="buttonEdit" OnClick="btnEdit_Click" />
                    </td>


                    <td>
                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" CssClass="buttonRefresh checkIsDirty" OnClick="btnRefresh_Click" />
                    </td>
                    <td>
                       <asp:Button ID="btnAuthorize" runat="server" Text="Authorize" CssClass="buttoncommon" OnClientClick="if ( ! UserAuthorizeConfirmation()) return false;" OnClick="btnAuthorize_Click" />
                    </td>
                    <td>
                        <input id="btnClose" type="button" runat="server" class="buttonClose" value="Close" onclick="if (ContentForm) { ContentForm.CloseForm(); }" />
                    </td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td>
                        <asp:Button ID="btnPopupTrigger" runat="server" Text="Button" CssClass="buttonHidden" />
                        <asp:HiddenField ID="hdnPopupTriggerID" runat="server" Value="" />
                        <asp:HiddenField ID="hdnPopupCommand" runat="server" Value="" />
                    </td>
                </tr>
            </table>
        </div>

     </div>


    <div id="overlay" class="overlay" >
         <div style="margin:auto;width:200px;height:400px;background-color:black;border:solid 1px black;
                  text-align:center; vertical-align:middle;"> 
           <span style="color:white; font-size:medium;" >Please Wait...</span>
             <br />
             <img alt="" src="../../image/progress.gif" />
         </div>
    </div>

    <div id="overlayReport" class="overlay" style="opacity: 0.8;">
         <div style="margin:auto;width:450px;height:80px; position: relative;background-color: blue;
                  text-align:center; vertical-align:middle; cursor:auto; z-index: 9999999;">
           <table width="100%">
           <tr>
               <td>
                   <span style="color:white; font-size:medium;" >Click Open Report to view Report.</span>
               </td>
           </tr>
           <tr>
             <td>         
                <input id="btnOpenReportWindow" type="button" value="Open Report" class="buttoncommon" />
                <input id="btnCacnelReportWindow" type="button" value="Cancel" class="buttoncommon"  />  
             </td>
           </tr>            
            </table>
         </div>
    </div>
</asp:Content>
