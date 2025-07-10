<%@ Page Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true"
    CodeBehind="GLReports.aspx.cs" Inherits="PG.Web.Report.AccountingReport.GLReports" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../Controls/GLGroupTree.ascx" TagName="GLGroupTree" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
    <link href="../../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        // <!CDATA[

        var isPageResize = true;
        ContentForm.CalendarImageURL = "../../image/calendar.png";

        var hdnCompanyID = '<%=hdnCompanyID.ClientID%>';

        var ReportViewPageLink = '<%=this.ReportViewPageLink%>';
        var ReportViewPDFPageLink = '<%=this.ReportViewPDFPageLink%>';
        var ReportPrintPageLink = '<%=this.ReportPrintPageLink%>';
        var ReportPDFPageLink = '<%=this.ReportPDFPageLink%>';

        var GLAccountServiceLink = '<%=this.GLAccountServiceLink%>';
        var GLGroupServiceLink = '<%=this.GLGroupServiceLink%>';

        var GetJournalListServiceLink = '<%=this.GetJournalListServiceLink%>';

        var AccRefServiceLink = '<%=this.AccRefServiceLink%>';

        var ifPrintButton = '<%=ifPrintButton.ClientID%>';


        var ddlAccYear = '<%=ddlAccYear.ClientID%>';

        var txtFromDateID = '<%=txtFromDate.ClientID%>';
        var txtToDateID = '<%=txtToDate.ClientID%>';

        var txtJournalNo = '<%=txtJournalNo.ClientID%>';
        var btnJournalNo = '<%=btnJournalNo.ClientID%>';
        var hdnJournalID = '<%=hdnJournalID.ClientID%>';
        var txtGLGroupName = '<%=txtGLGroupName.ClientID%>';

        var ddlJournalType = '<%=ddlJournalType.ClientID%>';


        var txtGLAccount = '<%=txtGLAccount.ClientID%>';
        var btnGLAccount = '<%=btnGLAccount.ClientID%>';
        var hdnGLAccountID = '<%=hdnGLAccountID.ClientID%>';
        var txtGLAccountName = '<%=txtGLAccountName.ClientID%>';
        var hdnGLGroupIDAcc = '<%=hdnGLGroupIDAcc.ClientID%>';


        var txtGLGroup = '<%=txtGLGroup.ClientID%>';
        var btnGLGroup = '<%=btnGLGroup.ClientID%>';
        var hdnGLGroupID = '<%=hdnGLGroupID.ClientID%>';


        var txtAccRefCode = '<%=txtAccRefCode.ClientID%>';
        var btnAccRefCode = '<%=btnAccRefCode.ClientID%>';
        var hdnAccRefID = '<%=hdnAccRefID.ClientID%>';
        var txtAccRefName = '<%=txtAccRefName.ClientID%>';


        var hdnAccRefTypeID = '<%=hdnAccRefTypeID.ClientID%>';
        var ddlAccRefCategory = '<%=ddlAccRefCategory.ClientID%>';
        var ddlLocation = '<%=ddlLocation.ClientID%>';

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

            if ($('#' + txtJournalNo).is(':visible')) {
                bindJournalList();
            }

            if ($('#' + txtGLAccount).is(':visible')) {
                bindGLAccountList();
            }

            if ($('#' + txtGLGroup).is(':visible')) {
                bindGLGroupList();
            }

            if ($('#' + txtAccRefCode).is(':visible')) {
                bindAccRefListAC();
            }

            //    $glPopup = $('#dvPopupGLGroup').GroupTree({
            //        title: 'Select GL Group',
            //        autoLink: true,
            //        autoLinkUpdate: true,
            //        linkControlID: 'dvGLGroup',
            //        highlightLink: true,
            //        keyboard: true,

            //        okclick: function(event, data) {
            //            //alert("GL Group ID: " + data.value);
            //            //setGLGroupData(data)
            //        },
            //        open: function(event, ui) {
            //            // $("#dvGLGroup").addClass("dvGLGroupSelected");
            //        },
            //        close: function(event, ui) {
            //            //            $("#dvGLGroup").removeClass("dvGLGroupSelected");
            //            //            $('#' + ctlGLGroupText).focus();
            //            //            $('#' + ctlGLGroupText).select();
            //        }
            //    });


            //    $("#btnGLGroup").click(function(e) {
            //        //gid = $("#" + ctlGLGroupID).val();
            //        //$glPopup.GLGroupPopup("show", gid);


            //        //        $glPopup.GLGroupPopup('option', 'autoLink', false) 
            //        //        $glPopup.GLGroupPopup("show", gid);

            //        //$glPopup.GLGroupPopup('option', 'linkControlID', 'dvGLGroup')
            //        $('#dvPopupGLGroup').GroupTree("show");
            //    });

        });

        function bindJournalList() {
            var cgColumns = [{ 'columnName': 'journalno', 'width': '80', 'align': 'left', 'highlight': 2, 'label': 'No' }
                        , { 'columnName': 'journaldate', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Date' }
                        , { 'columnName': 'journaltypename', 'width': '120', 'align': 'left', 'highlight': 0, 'label': 'Type' }
                        , { 'columnName': 'journallocation', 'width': '120', 'align': 'left', 'highlight': 0, 'label': 'Loc' }
                        , { 'columnName': 'journaldesc', 'width': '80', 'align': 'left', 'highlight': 0, 'label': 'Narration' }
                        , { 'columnName': 'journalamt', 'width': '100', 'align': 'right', 'highlight': 0, 'label': 'Amount' }
                    ];

            var companyid = $('#' + hdnCompanyID).val();
            //var locationid = $('#' + ddlLocation).val();
            //Change Moni

          

            var serviceURL = GetJournalListServiceLink + "?isterm=1&includeempty=1&jnocomptype=" + Enums.DataCompareType.StartsWith;
            serviceURL += "&companyid=" + companyid;
            //serviceURL += "&locationid=" + locationid;

            var jNoElem = $('#' + txtJournalNo);

            $('#' + btnJournalNo).click(function (e) {
                //elmID = $(elem).attr('id');
                //$(elem).combogrid("show");
                $(jNoElem).combogrid("dropdownClick");
            });


            $(jNoElem).combogrid({
                debug: true,
                searchButton: false,
                resetButton: false,
                alternate: true,
                munit: 'px',
                scrollBar: true,
                showPager: true,
                showError: true,
                colModel: cgColumns,
                width: 600,
                url: serviceURL,
                search: function (event, ui) {
                    var yearID = $('#' + ddlAccYear).val();
                    var journalTypeID = $('#' + ddlJournalType).val();
                    var locationid = $('#' + ddlLocation).val();
                    var newServiceURL = serviceURL + "&yearid=" + yearID + "&journaltypeid=" + journalTypeID + "&locationid=" + locationid
                    $(this).combogrid("option", "url", newServiceURL);
                },
                select: function (event, ui) {
                    if (!ui.item) {
                        event.preventDefault();
                        $('#' + hdnJournalID).val('0');
                        $('#' + txtJournalNo).val('');
                        return false;
                        //ClearGLAccountData(elemID);
                    } 
                    
                    
                    
                    if (ui.item.journalid == 0) {
                        event.preventDefault();
                        return false;
                        //ClearGLAccountData(elemID);
                    }
                    else {
                        $('#' + hdnJournalID).val(ui.item.journalid);
                        $('#' + txtJournalNo).val(ui.item.journalno);
                    }
                    return false;
                },

                lc: ''
            });


            $(jNoElem).blur(function () {
                var self = this;

                jNo = $(jNoElem).val();
                if (jNo == '') {
                    $('#' + hdnJournalID).val('0');
                }
            });

            $('#' + ddlJournalType).change(function () {
                var self = this;
                $('#' + hdnJournalID).val('0');
                $('#' + txtJournalNo).val('');
            });

        }


        function bindGLAccountList() {
            var cgColumns = [{ 'columnName': 'glacccode', 'width': '80', 'align': 'left', 'highlight': 2, 'label': 'Code' }
                             , { 'columnName': 'glaccname', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                             , { 'columnName': 'glgroupname', 'width': '130', 'align': 'left', 'highlight': 0, 'label': 'Group' }
                             , { 'columnName': 'glacctypename', 'width': '100', 'align': 'left', 'highlight': 0, 'label': 'Type' }
                            ];

            var companyid = $('#' + hdnCompanyID).val();

            //var serviceURL = GLAccountServiceLink + "?isterm=1&includeempty=1&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            var serviceURL = GLAccountServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            serviceURL += "&companyid=" + companyid;
            serviceURL += "&ispaging=1";
            serviceURL += "&acctypefilter=" + Enums.GLAccountTypeFilter.AllAccount;
            serviceURL += "&namecomptype=" + Enums.DataCompareType.Contains;



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


        //$(function() {
        //    $('#txtGLGroup').keydown(function(event) {
        //        alert(event.keyCode);
        //    });
        //});

        function bindAccRefListAC() {

            var companyid = $('#' + hdnCompanyID).val();
            var typeid = $('#' + hdnAccRefTypeID).val();
            var catagoryid = $('#' + ddlAccRefCategory).val();

            //var selected = $("#q_7 input:radio:checked").val();

            //var typeid = $("#" + rblAccRefTypeID + " input:radio:checked").val();

            var serviceURL = AccRefServiceLink + "?isterm=1&includeempty=1&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            serviceURL += "&namecomptype=" + Enums.DataCompareType.Contains + "&ispaging=1";
            //serviceURL += "&typeid=" + typeid;
            serviceURL += "&companyid=" + companyid;


            var cgColumns = [{ 'columnName': 'code', 'width': '80', 'align': 'left', 'highlight': 2, 'label': 'Code' }
                             , { 'columnName': 'name', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                             , { 'columnName': 'categoryname', 'width': '130', 'align': 'left', 'highlight': 0, 'label': 'Category' }
                            ];

            var accRefElem = $('#' + txtAccRefCode);

            $('#' + btnAccRefCode).click(function (e) {
                //elmID = $(elem).attr('id');
                //$(elem).combogrid("show");
                $(accRefElem).combogrid("dropdownClick");
            });

            $(accRefElem).combogrid({
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
                    var typeid = $('#' + hdnAccRefTypeID).val();
                    var catagoryid = $('#' + ddlAccRefCategory).val();
                    var newServiceURL = serviceURL + "&typeid=" + typeid;
                    newServiceURL = newServiceURL + "&categoryid=" + catagoryid;
                    $(this).combogrid("option", "url", newServiceURL);
                },
                select: function (event, ui) {
                    elemID = $(accRefElem).attr('id');

                    if (!ui.item) {
                        event.preventDefault();
                        ClearAccRefData();
                        return false;
                        //ClearGLAccountData(elemID);
                    }


                    if (ui.item.id == 0) {
                        event.preventDefault();
                        ClearAccRefData();
                    }
                    else {
                        SetAccRefData(ui.item);
                    }
                    return false;
                }
            });

            $(accRefElem).blur(function () {
                var self = this;
                elemID = $(accRefElem).attr('id');
                ttCode = $(accRefElem).val();

                if (ttCode == '') {
                    ClearAccRefData(elemID);
                }
                else {

                }

                ttID = $('#' + hdnAccRefID).val();
                if (ttID == '0' | ttID == '') {
                    $(self).addClass('textError');
                }
            });

            $(accRefElem).focus(function () {
                var self = this;
                $(self).removeClass('textError');
                //$(elem).removeClass('fldDataError');
            });

            $('#' + ddlAccRefCategory).change(function () {
                var self = this;
                //$(self).removeClass('textError');
                //$(elem).removeClass('fldDataError');

                ClearAccRefData();
            });


        } //bind ref


        function SetAccRefData(data) {
            $('#' + txtAccRefCode).val(data.code);
            $('#' + hdnAccRefID).val(data.id);
            $('#' + txtAccRefName).val(data.name);
        }

        function ClearAccRefData() {
            //$('#' + elemID).val(data.code);

            $('#' + txtAccRefCode).val('');
            $('#' + hdnAccRefID).val('0');
            $('#' + txtAccRefName).val('');
            $('#' + txtAccRefCode).removeClass('fldDataError');

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
        
        
        .dvGroupListPopup
        {
            display: none;
            height: 0px;
            width: 0px;
        }
        
        
        .textPopup
        {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            border: 1px #1B68B8 solid;
            background-color: #FFFFFF;
            color: #000000;
            font-size: 11px;
            width: 160px;
            height: 16px;
            padding-left: 2px;
        }
        
        .btnPopup
        {
            height: 20px;
            width: 16px;
            background-image: url(/image/dropdown.gif);
            background-repeat: no-repeat;
            background-position: center bottom;
            cursor: pointer;
        }
        
        .btnPopup:hover
        {
            background-image: url(/image/dropdown_over.gif);
        }
        
        .dvSpacer
        {
            height: 10px;
            width: 100%;
        }
        
        
        .dvReportList
        {
            height: 100%;
            width: 100%;
            overflow: auto;
        }
        .dvParam
        {
            height: 100%;
            width: 100%;
            overflow: auto;
        }
        
        
        .tblParam
        {
            /* border-collapse: collapse;    */
            height: auto;
        }
        
        

        
        .tblParam td
        {
            height: auto;
        }
        
        
        .cboYesNo
        {
            width: 50px;
        }
        
        .tdSpacer
        {
            width: 10px;
        }
        
        .rowParam
        {
        }
        
        .rowSpacer
        {
            height:20px;
        }
        
        
        .dvPrintIFrame
        {
            height: 0px;
            width: 0px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvPageContent" style="width: 100%; height: 100%;">
        <div id="dvContentHeader" class="dvContentHeader">
            <div id="dvHeader" class="dvHeader">
                Report - Genarel Ledger
            </div>
            <div id="dvMessage" class="dvMessage">
                <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
            </div>
            <div id="dvHeaderControl" class="dvHeaderControl">
            </div>
        </div>
        <div id="dvContentMain" class="dvContentMain">
            <div id="dvContentMainInner" class="dvContentMainInner" style="height: 100%;">
                <div id="dvTop" style="height: auto; width: 100%;">
                    <table cellspacing="0" cellpadding="0" style="height: auto; width: 100%;">
                        <tr>
                            <td>
                            </td>
                            <td valign="top" style="width: 200px;">
                                Select Reports:
                            </td>
                            <td style="border-left: 1px solid grey;">
                            </td>
                            <td valign="top" style="">
                                <div id="dvParamHeader" class="dvParamHeader" style="height: auto; width: 100%;">
                                    <table cellspacing="0" cellpadding="0" border="0" style="height: auto; width: 100%;">
                                        <tr>
                                            <td style="border-bottom: 1px solid grey;">
                                                <asp:Label ID="lblReportName" runat="server" Text="Chart Of Accounts" Font-Bold="True"
                                                    Font-Size="10pt"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="dvMiddle" style="height: auto; width: 100%">
                    <table id="tblMiddle" cellspacing="0" cellpadding="0" style="height: 100%; width: 100%;
                        min-width: 700px;">
                        <tr style="height: 100%">
                            <td>
                            </td>
                            <td valign="top" style="width: 200px;">
                                <div id="dvReportList" class="dvReportList">
                                    <table cellspacing="2" cellpadding="1">
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:TreeView ID="tvwReport" runat="server" OnSelectedNodeChanged="tvwReport_SelectedNodeChanged"
                                                    NodeIndent="10">
                                                    <HoverNodeStyle BackColor="#CCCCFF" />
                                                    <Nodes>
                                                        <asp:TreeNode Text="GL Report" Value="GL Report" Expanded="True" SelectAction="Expand">
                                                            <asp:TreeNode Selected="True" Text="Chart Of Accounts" Value="1520"></asp:TreeNode>
                                                            <asp:TreeNode Text="Trial Balance" Value="1501"></asp:TreeNode>
                                                            <asp:TreeNode Text="Receipt/Payment" Value="1504"></asp:TreeNode>
                                                            <%--<asp:TreeNode Text="Cash Flow Statment" Value="1505"></asp:TreeNode>--%>
                                                            <asp:TreeNode Text="Income Statement" Value="1502"></asp:TreeNode>
                                                            <asp:TreeNode Text="Balance Sheet" Value="1503"></asp:TreeNode>
                                                        </asp:TreeNode>
                                                        <asp:TreeNode Text="Account Book" Value="0" Expanded="True" SelectAction="Expand">
                                                            <asp:TreeNode Text="Cash/Bank" Value="Cash/Bank" Expanded="False" SelectAction="Expand">
                                                                <asp:TreeNode Text="Cash Summary" Value="1540"></asp:TreeNode>
                                                                <asp:TreeNode Text="Cash Journal List" Value="1541"></asp:TreeNode>
                                                                <asp:TreeNode Text="Cash Journal Book" Value="1542"></asp:TreeNode>
                                                            </asp:TreeNode>
                                                            <asp:TreeNode Text="Journal" Value="Journal" Expanded="False" SelectAction="Expand">
                                                                <asp:TreeNode Text="Journal List" Value="1530"></asp:TreeNode>
                                                                <asp:TreeNode Text="Journal Book" Value="1531"></asp:TreeNode>
                                                                <asp:TreeNode Text="Journal" Value="1532"></asp:TreeNode>
                                                            </asp:TreeNode>
                                                            <asp:TreeNode Text="Ledger" Value="Ledger" Expanded="False" SelectAction="Expand">
                                                                <asp:TreeNode Text="Ledger" Value="1521"></asp:TreeNode>
                                                                <asp:TreeNode Text="Ledger Summary" Value="1522"></asp:TreeNode>
                                                            </asp:TreeNode>
                                                        </asp:TreeNode>
                                                        <asp:TreeNode Expanded="False" SelectAction="Expand" 
                                                            Text="Cost Center/Reference" Value="0">
                                                            <asp:TreeNode Text="Cost Center Summary" Value="1551"></asp:TreeNode>
                                                            <asp:TreeNode Text="Cost Center Details" Value="1561"></asp:TreeNode>
                                                            <asp:TreeNode Text="Reference Summary" Value="1552"></asp:TreeNode>
                                                            <asp:TreeNode Text="Reference Details" Value="1562"></asp:TreeNode>
                                                           <%-- <asp:TreeNode Text="Tran. Code Summary" Value="1553"></asp:TreeNode>
                                                            <asp:TreeNode Text="Tran. Code Details" Value="1563"></asp:TreeNode>--%>
                                                        </asp:TreeNode>
                                                    </Nodes>
                                                    <NodeStyle ForeColor="Black" />
                                                    <SelectedNodeStyle BackColor="#CCCCFF" ForeColor="White" Font-Bold="True" />
                                                </asp:TreeView>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                            <td style="border-left: 1px solid grey;">
                            </td>
                            <td valign="top" style="height: 100%;">
                                <div id="dvParam" class="dvParam">
                                    <table id="tblParam" cellspacing="4" cellpadding="2" border="0" class="tblParam">
                                        <tr>
                                            <td style="width: 5px;">
                                            </td>
                                            <td style="width: 200px;">
                                                <div class="dvSpacer">
                                                </div>
                                            </td>
                                            <td>
                                                <asp:HiddenField ID="hdnCompanyID" runat="server" Value="0" />
                                            </td>
                                        </tr>
                                        <tr class="rowParam">
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                <asp:Label ID="lblReportType" runat="server" Text="Report Type:" Visible="False"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlReportType" runat="server" Width="170px" CssClass="dropDownList"
                                                    Visible="False" AutoPostBack="True" OnSelectedIndexChanged="ddlReportType_SelectedIndexChanged">
                                                    <asp:ListItem Selected="True" Text="List" Value="0"> </asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr class="rowParam">
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                <asp:Label ID="lblAccYear" runat="server" Text="Year:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlAccYear" runat="server" CssClass="dropDownList" Width="170px"
                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlAccYear_SelectedIndexChanged">
                                                    <asp:ListItem Selected="True" Text="(select)" Value="0"> </asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                         <tr >
                                            <td>
                                               
                                            </td>
                                            <td align="right">
                                                 <asp:Label ID="lblLocation" runat="server" Text="Location:"></asp:Label>
                                            </td>
                                            <td>
                                                 <asp:DropDownList ID="ddlLocation" runat="server" CssClass="dropDownList" Width="170px">
                                                      <asp:ListItem Selected="True" Text="(All)" Value="0"> </asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                             <td>
                                            </td>
                                            <td style="" align="right">
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 5px;">
                                            </td>
                                            <td style="">
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr class="rowParam">
                                            <td>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="lblFromDate" runat="server" Text="Date From:"></asp:Label>
                                            </td>
                                            <td>
                                                <table cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtFromDate" runat="server" Width="80px" CssClass="textBox textDate dateParse"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 4px;">
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblToDate" runat="server" Text="To:"></asp:Label>
                                                        </td>
                                                        <td style="width: 2px;">
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtToDate" runat="server" Width="80px" CssClass="textBox textDate dateParse"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td align="right" class="">
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>

                                        <tr class="rowParam">
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                <asp:Label ID="lblJournalType" runat="server" Text="Journal Type:" Visible="False"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlJournalType" runat="server" Width="170px" CssClass="dropDownList"
                                                    Visible="False">
                                                    <asp:ListItem Selected="True" Text="(all type)" Value="0"> </asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr class="rowParam">
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                <asp:Label ID="lblGLGroup" runat="server" Text="GL Group:" Visible="False"></asp:Label>
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
                                                <asp:Label ID="lblGLAccount" runat="server" Text="Account /Sub Ledger:" Visible="False"></asp:Label>
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
                                        <tr class="rowParam">
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                <asp:Label ID="lblJournalNo" runat="server" Text="Journal No:"></asp:Label>
                                            </td>
                                            <td colspan="4">
                                                <table cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtJournalNo" runat="server" Width="150px" CssClass="textBox"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <input id="btnJournalNo" type="button" value="" runat="server" class="buttonDropdown"
                                                                tabindex="-1" />
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                            <asp:HiddenField ID="hdnJournalID" runat="server" Value="0" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>

                                       <tr class="rowParam">
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                <asp:Label ID="lblAccRefCategory" runat="server" Text="Cost Center Category:"></asp:Label>
                                            </td>
                                            <td style="">
                                                <asp:DropDownList ID="ddlAccRefCategory" runat="server" CssClass="dropDownList" Width="170px">
                                                    <asp:ListItem Value="0">All</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                               <asp:HiddenField ID="hdnAccRefTypeID" runat="server" Value="0" />
                                            </td>
                                            <td style="" align="right">
                                                &nbsp;
                                            </td>
                                            <td>
                                            </td>
                                        </tr>

                                        <tr class="rowParam">
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                <asp:Label ID="lblAccRefCode" runat="server" Text="Cost Center:"></asp:Label>
                                            </td>
                                            <td colspan="4">
                                                <table cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtAccRefCode" runat="server" Width="150px" CssClass="textBox"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <input id="btnAccRefCode" type="button" value="" runat="server" class="buttonDropdown"
                                                                tabindex="-1" />
                                                        </td>
                                                        <td class="tdSpacer">
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtAccRefName" runat="server" Width="150px" CssClass="textBox"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:HiddenField ID="hdnAccRefID" runat="server" Value="0" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>

                                        <tr class="rowSpacer">
                                            <td>
                                            </td>
                                            <td style="">
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr class="rowParam">
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                <asp:Label ID="lblBlock" runat="server" Text="Show:"></asp:Label>
                                            </td>
                                            <td style="">
                                                <asp:DropDownList ID="ddlBlock" runat="server" CssClass="dropDownList">
                                                    <asp:ListItem Value="0">All</asp:ListItem>
                                                    <asp:ListItem Value="1">Open</asp:ListItem>
                                                    <asp:ListItem Value="2">Blocked</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                &nbsp;
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr class="rowParam">
                                            <td>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="lblOrderBy" runat="server" Text="Order By:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlOrderBy" runat="server" CssClass="dropDownList" Width="170px">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                            <td align="right">
                                                &nbsp;
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr class="rowParam">
                                            <td>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="lblReportFormat" runat="server" Text="Journal Report Format:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlReportFormat" runat="server" CssClass="dropDownList" Width="170px">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                            <td align="right">
                                                &nbsp;
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr class="rowParam">
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                <asp:Label ID="lblAmountShowType" runat="server" Text="Amount Show Type:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlAmountShowType" runat="server" CssClass="dropDownList"  Width="170px">
                                                    <asp:ListItem Selected="True" Value="1">Closing Balance</asp:ListItem>
                                                    <asp:ListItem Value="2">Opening,Transaction,Closing</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr class="rowParam">
                                            <td>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="lblIncludePostType" runat="server" Text="Include Post Type:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlIncludePostType" runat="server" CssClass="dropDownList" Width="85px">
                                                    <asp:ListItem Value="0">ALL</asp:ListItem>
                                                    <asp:ListItem Selected="True" Value="1">Posted</asp:ListItem>
                                                    <asp:ListItem Value="2">Unposted</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr class="rowParam">
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                <asp:Label ID="lblIncludeOpBal" runat="server" Text="Include Opening Balance:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlIncludeOpBal" runat="server" CssClass="dropDownList"  Width="170px">
                                                    <asp:ListItem Value="0">None</asp:ListItem>
                                                    <asp:ListItem Selected="True" Value="1">Include ALL</asp:ListItem>
                                                    <asp:ListItem Value="2">Include ALL Indv.</asp:ListItem>
                                                    <asp:ListItem Value="3">Include Year</asp:ListItem>
                                                    <asp:ListItem Value="4">Include DateRange</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>

                                        <tr class="rowParam">
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                <asp:Label ID="lblIncludeZero" runat="server" Text="Include Zero:"></asp:Label>
                                            </td>
                                            <td style="">
                                                <asp:DropDownList ID="ddlIncludeZero" runat="server" CssClass="dropDownList cboYesNo">
                                                    <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                &nbsp;
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr class="rowParam">
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                <asp:Label ID="lblHeirarchyLevel" runat="server" Text="Heirarchy Level:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlHeirarchyLevel" runat="server" CssClass="dropDownList cboYesNo" Width="170px">
                                                    <asp:ListItem Value="-1">All</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                            <td align="right">
                                                &nbsp;
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr class="rowParam">
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                <asp:Label ID="lblIncludeAllAccount" runat="server" Text="Include All Account:"></asp:Label>
                                            </td>
                                            <td style="">
                                                <asp:DropDownList ID="ddlIncludeAllAccount" runat="server" CssClass="dropDownList cboYesNo">
                                                    <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                &nbsp;
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr class="rowParam">
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                <asp:Label ID="lblGroupLedgerShowType" runat="server" Text="Group/Ledger:"></asp:Label>
                                            </td>
                                            <td style="">
                                                <asp:DropDownList ID="ddlGroupLedgerShowType" runat="server" CssClass="dropDownList"  Width="170px">
                                                    <asp:ListItem  Value="1">Groups</asp:ListItem>
                                                    <asp:ListItem Value="2">Ledgers</asp:ListItem>
                                                    <asp:ListItem Selected="True" Value="3">Groups And Ledgers</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                &nbsp;
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr class="rowParam">
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                <asp:Label ID="lblIncludeGLClass" runat="server" Text="Show Root Group:"></asp:Label>
                                            </td>
                                            <td style="">
                                                <asp:DropDownList ID="ddlIncludeGLClass" runat="server" CssClass="dropDownList cboYesNo">
                                                    <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                &nbsp;
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr class="rowParam">
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                <asp:Label ID="lblIncludeGroupParents" runat="server" Text="Include Group Parents:"></asp:Label>
                                            </td>
                                            <td style="">
                                                <asp:DropDownList ID="ddlIncludeGroupParents" runat="server" CssClass="dropDownList cboYesNo">
                                                    <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                &nbsp;
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr class="rowParam">
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                <asp:Label ID="lblIncludeGroupChilds" runat="server" Text="Include Group Childs:"></asp:Label>
                                            </td>
                                            <td style="">
                                                <asp:DropDownList ID="ddlIncludeGroupChilds" runat="server" CssClass="dropDownList cboYesNo">
                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                    <asp:ListItem Selected="True" Value="1">Yes</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                &nbsp;
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                &nbsp;
                                            </td>
                                            <td style="">
                                                &nbsp;
                                            </td>
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                &nbsp;
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr class="rowParam">
                                            <td>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="lblBalnceSheetShowMethod" runat="server" Text="Show Method:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlBalnceSheetShowMethod" runat="server" CssClass="dropDownList"  Width="170px">
                                                    <asp:ListItem Selected="True" Value="0">Assets/Liabilities</asp:ListItem>
                                                    <asp:ListItem Value="1">Liabilities/Assets</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr class="rowParam">
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                <asp:Label ID="lblShowPercentage" runat="server" Text="Show Percentage:"></asp:Label>
                                            </td>
                                            <td style="">
                                                <asp:DropDownList ID="ddlShowPercentage" runat="server" CssClass="dropDownList cboYesNo">
                                                    <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                &nbsp;
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr class="rowParam">
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                <asp:Label ID="lblControlAccountSummary" runat="server" Text="Control Account Summary:"></asp:Label>
                                            </td>
                                            <td style="">
                                                <asp:DropDownList ID="ddlControlAccountSummary" runat="server" CssClass="dropDownList cboYesNo">
                                                    <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                &nbsp;
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr class="rowParam">
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                <asp:Label ID="lblIncludeSubForControl" runat="server" Text="Show Sub Accounts :"></asp:Label>
                                            </td>
                                            <td style="">
                                                <asp:DropDownList ID="ddlIncludeSubForControl" runat="server" CssClass="dropDownList cboYesNo">
                                                    <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                &nbsp;
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr class="rowParam">
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                <asp:Label ID="lblIncludeContraEntry" runat="server" Text="Show Contra Entries:"></asp:Label>
                                            </td>
                                            <td style="">
                                                <asp:DropDownList ID="ddlIncludeContraEntry" runat="server" CssClass="dropDownList cboYesNo">
                                                    <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                &nbsp;
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr class="rowParam">
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                <asp:Label ID="lblIncludeInstrument" runat="server" Text="Show Instrument:"></asp:Label>
                                            </td>
                                            <td style="">
                                                <asp:DropDownList ID="ddlIncludeInstrument" runat="server" 
                                                    CssClass="dropDownList cboYesNo" Enabled="False">
                                                    <asp:ListItem  Value="0">No</asp:ListItem>
                                                    <asp:ListItem Selected="True" Value="1">Yes</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                &nbsp;
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr class="rowParam">
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                <asp:Label ID="lblIncludeCostCenter" runat="server" Text="Show Cost Center:"></asp:Label>
                                            </td>
                                            <td style="">
                                                <asp:DropDownList ID="ddlIncludeCostCenter" runat="server" CssClass="dropDownList cboYesNo">
                                                    <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                &nbsp;
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr class="rowParam">
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                <asp:Label ID="lblIncludeReference" runat="server" Text="Show Reference:"></asp:Label>
                                            </td>
                                            <td style="">
                                                <asp:DropDownList ID="ddlIncludeReference" runat="server" CssClass="dropDownList cboYesNo">
                                                    <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                &nbsp;
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr class="rowParam">
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                <asp:Label ID="lblIncludeTranCode" runat="server" Text="Show Tran. Code:" Visible="false"></asp:Label>
                                            </td>
                                            <td style="">
                                                <asp:DropDownList ID="ddlIncludeTranCode" runat="server" CssClass="dropDownList cboYesNo" Visible="false">
                                                    <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                &nbsp;
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr class="rowParam">
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                            </td>
                                            <td style="">
                                            </td>
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                &nbsp;
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr class="rowParam">
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                <asp:Label ID="lblIncludeDetOfDetails" runat="server" Text="Show Details For all:"></asp:Label>
                                            </td>
                                            <td style="">
                                                <asp:DropDownList ID="ddlIncludeDetOfDetails" runat="server" CssClass="dropDownList cboYesNo">
                                                    <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                &nbsp;
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
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div id="dvContentFooter" class="dvContentFooter">
            <div id="dvContentFooterInner" class="dvContentFooterInner">
                <div style="width: 100%; height: 100%; margin-bottom: 0px;">
                    <div style="width: auto; min-width: 300px; height: auto; text-align: left;">
                        <table border="0">
                            <tr>
                                <td style="width: 100px;">
                                
                                </td>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Text="Report View"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlReportViewMode" runat="server" CssClass="dropDownList">
                                        <asp:ListItem Value="0">In This Tab</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="1">In New Tab</asp:ListItem>
                                        <asp:ListItem Value="2">In New Window</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:Button ID="btnView" runat="server" Text="View Report" Width="100px" CssClass="buttoncommon buttonPrintPreview"
                                        OnClick="btnView_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="btnPrint" runat="server" Text="Print Report" Width="100px" CssClass="buttoncommon buttonPrint"
                                        OnClick="btnPrint_Click" />
                                </td>
                                <td style="width: 20px;">
                                </td>
                                <td>
                                    <asp:Label ID="Label3" runat="server" Text="Get Report As:"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlExport" runat="server" Width="70" CssClass="dropDownList">
                                        <asp:ListItem Selected="True" Value="0">PDF</asp:ListItem>
                                        <asp:ListItem Value="1">Excel</asp:ListItem>
                                        <asp:ListItem Value="2">Word</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Button ID="btnExport" runat="server" Text="Get Report" Width="100px" CssClass="buttoncommon buttonExport"
                                        OnClick="btnExport_Click" />
                                </td>
                                <td style="width: 10px;">
                                </td>
                                <td>
                                    <div id="dvPrintIFrame" class="dvPrintIFrame">
                                        <iframe id="ifPrintButton" runat="server" width="0" height="0"></iframe>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
