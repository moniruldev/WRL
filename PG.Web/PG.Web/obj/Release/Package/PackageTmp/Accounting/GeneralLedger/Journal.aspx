<%@ Page Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true"
    MaintainScrollPositionOnPostback="true" CodeBehind="Journal.aspx.cs" Inherits="PG.Web.Accounting.GeneralLedger.Journal"
    EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../Controls/PrintButton.ascx" TagPrefix="uc" TagName="PrintButton" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../javascript/jquery.ui.combogrid.js?v=1.6.3.2" type="text/javascript"></script>
    <script src="../../javascript/jquery.attributeobserver.js" type="text/javascript"></script>
    <link href="../../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />
    <%--<script src="../../javascript/jquery-latest.min.js"></script>--%>
    <script language="javascript" type="text/javascript">
        // <!CDATA[

        var isPageResize = true;

        ContentForm.CalendarImageURL = '../../image/calendar.png';

        var hdnCompanyID = '<%=hdnCompanyID.ClientID%>';
        var hdnJournalID = '<%=hdnJournalID.ClientID%>';
        var hdnGLGroupClassInclude = '<%=hdnGLGroupClassInclude.ClientID%>';
        var hdnGLGroupClassExclude = '<%=hdnGLGroupClassExclude.ClientID%>';

        var txtJournalNo = '<%=txtJournalNo.ClientID%>';
        var txtJournalDate = '<%=txtJournalDate.ClientID%>';

        var ddlJournalType = '<%=ddlJournalType.ClientID%>';
        var ddlAccYear = '<%=ddlAccYear.ClientID%>';

        var ddlLocation='<%=ddlLocation.ClientID%>'

        var hdnJournalUpdateNo = '<%=hdnJournalUpdateNo.ClientID%>';
        var hdnEditDataModeInt = '<%=hdnEditDataModeInt.ClientID%>';


        var txtLocationCode = '<%=txtLocationCode.ClientID%>';
        var txtLocationName = '<%=txtLocationName.ClientID%>';
        var hdnLocationID = '<%=hdnLocationID.ClientID%>';



        var GLAccountServiceLink = '<%=this.GLAccountServiceLink%>';
        var GLGroupServiceLink = '<%=this.GLGroupServiceLink%>';
        var AccRefServiceLink = '<%=this.AccRefServiceLink%>';
        var AccRefCategoryServiceLink = '<%=this.AccRefCategoryServiceLink%>';
        var AccRefSettingsServiceLink = '<%=this.AccRefSettingsServiceLink%>';

        var GetLocationServiceLink = '<%=this.GetLocationServiceLink%>';


        var InstrumentGetServiceLink = '<%=this.InstrumentGetServiceLink%>';
        var InstrumentUpdateServiceLink = '<%=this.InstrumentUpdateServiceLink%>';

        var ValidateJournalServiceLink = '<%=this.ValidateJournalServiceLink%>';

        var reportGeneratePageLink = '<%=this.ReportGeneratePageLink%>';
        var ReportViewPageLink = '<%=this.ReportViewPageLink%>';
        var updateProgressID = '<%=UpdateProgress1.ClientID%>';
        var updateProgressID2 = '<%=UpdateProgress2.ClientID%>';

        var gridUpdatePanelIDDet = '<%=UpdatePanel1.ClientID%>';
        var gridViewIDDet = '<%=GridView1.ClientID%>';

        var gridUpdatePanelIDDet2 = '<%=UpdatePanel2.ClientID%>';
        var gridViewIDDet2 = '<%=GridView2.ClientID%>';


        var totDebitID = '<%=this.txtTotDebitAmt.ClientID%>';
        var totCreditID = '<%=this.txtTotCreditAmt.ClientID%>';

        var dvPopupAccRef = '<%=dvPopupAccRef.ClientID%>';
        var dvPopupIns = '<%=dvPopupIns.ClientID%>';
        var dvPopupInsDetails = '<%=dvPopupInsDetails.ClientID%>';

        var hdnJournalDetRefJson = '<%=hdnJournalDetRefJson.ClientID%>';
        var hdnJournalDetInsJson = '<%=hdnJournalDetInsJson.ClientID%>';

        var hdnJournalDetRefJson2 = '<%=hdnJournalDetRefJson2.ClientID%>';
        var hdnJournalDetInsJson2 = '<%=hdnJournalDetInsJson2.ClientID%>';

        var lblTotalRowDr = '<%=lblTotalRowDr.ClientID%>';
        var lblTotalRowCr = '<%=lblTotalRowCr.ClientID%>';

        var lblDiffAmtDr = '<%=lblDiffAmtDr.ClientID%>';
        var lblDiffAmtCr = '<%=lblDiffAmtCr.ClientID%>';

        var btnSave = '<%=btnSave.ClientID%>';

        var curTextTranTypeCodeID = '';
        var curTxtAccRefCodeID = '';
        var curAccRefTypeID = 0;

        var curTxtInsCodeID = '';

        var curTxtInsDetailsCodeElem = null;



        var curRowID = '';



        function test() {
            //get text width


            var txtJNo = '<%=this.txtJournalNo.ClientID%>';

            var tBox = $('#' + txtJNo);

            tWidth = $('#' + txtJNo).width();

            //        var sensor = $('<div />').css({ margin: 0, padding: 0, width: 'auto' });
            //        sensor.text($('#' + txtJNo).val());
            //        $(txtJNo).append(sensor);
            //        var fWidth = sensor.width();
            //        sensor.remove();

            var text = $('#' + txtJNo).val();
            var org = $(tBox)
            var html = $('<span style="postion:absolute;width:auto;left:-9999px">' + (text || org.html()) + '</span>');
            if (!text) {
                html.css("font-family", org.css("font-family"));
                html.css("font-size", org.css("font-size"));
            }
            $('body').append(html);
            fWidth = html.width();
            html.remove();
            //return width;


            //fWidth = $('#' + txtJNo).val().width();


            alert('boxW:' + tWidth + ', txW: ' + fWidth);


        }


        function ShowProgress() {
            $('#' + updateProgressID).show();
        }

        function ShowProgress2() {
            $('#' + updateProgressID2).show();
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

            //$("#dvControlsInner").height(contInnerHeight);

        }


        function gridTaskAfter() {
            UpdateTotalRowLabel();
        }

        $(document).ready(function () {

            var pageInstance = Sys.WebForms.PageRequestManager.getInstance();

            pageInstance.add_pageLoaded(function (sender, args) {
                var panels = args.get_panelsUpdated();
                for (i = 0; i < panels.length; i++) {
                    //alert(panels[i].id);
                    //ContentForm.InitDefualtFeatureInScope(panels[i].id);

                    if (panels[i].id == gridUpdatePanelIDDet) {
                        bindGridTask(gridViewIDDet);
                    }

                    if (panels[i].id == gridUpdatePanelIDDet2) {
                        bindGridTask(gridViewIDDet2);
                    }
                }
                //$('#' + dvGridDetailsPopup).parent().appendTo(jQuery("form:first"));
                gridTaskAfter();

            });

            //dvpopupgriddetails  

            //ContentForm.InitDefualtFeature();
            //txtPrjectExpenseAutoComplete();
            // ContentForm.ApplyEnterToTabByClassInScope(gripPopupUpdatePanelID, 'gridPopupItem');

            //ContentForm.ApplyEnterToTabByClassInScope(dvGridDetailsPopup, 'gridPopupItem');

            bindLocationList();
            bindGridTask(gridViewIDDet);
            bindGridTask(gridViewIDDet2);

            bindAccRefPopup();
            bindInsPopup();
            bindInsDetailsPopup();

            bindPopupControlEvents();
            UpdateTotalRowLabel();
            //bindSumJournalDetAmt();

        });


        function bindGridTask(gridViewID) {
            bindGLGroupList(gridViewID);
            bindGLAccounctList(gridViewID);
            bindJournalDetEvents(gridViewID);
            bindSumJournalDetAmt(gridViewID);
            bindInsList(gridViewID);
            bindAccRefList(gridViewID);

            bindTranTypeList(gridViewID);

        }

        function bindLocationList() {
            var cgColumns = [{ 'columnName': 'code', 'width': '80', 'align': 'left', 'highlight': 2, 'label': 'Code' }
                             , { 'columnName': 'name', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                             , { 'columnName': 'typename', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Type' }
            ];

            var companyid = $('#' + hdnCompanyID).val();

            var serviceURL = GetLocationServiceLink + "?isterm=1&includeempty=1&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            serviceURL += "&companyid=" + companyid;
            serviceURL += "&ispaging=1";
            serviceURL += "&namecomptype=" + Enums.DataCompareType.Contains;
            serviceURL += "&task=location";

            var elem = $('#' + txtLocationCode);

            $(elem).closest('tr').find('input[id$="btnLocationAC"]').click(function (e) {
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
                width: 500,
                url: serviceURL,
                search: function (event, ui) {
                    var newServiceURL = JSUtility.AddTimeToQueryString(serviceURL);
                    $(this).combogrid("option", "url", newServiceURL);
                },
                //"select item" event handler to set input field
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
                        //ClearGLGroupData(elemID);
                        return false;
                        //ClearGLAccountData(elemID);
                    }

                    if (ui.item.id == 0) {
                        event.preventDefault();
                        return false;
                        //ClearGLAccountData(elemID);
                    }
                    else {
                       // SetGLGroupData(elemID, ui.item);
                    }
                    //setDetInstrument(hdnIsInstrumentElem, txtInstrumentElem, btnInstrumentElem);
                    return false;
                }
            });

            //$(elem).blur(function () {
            //    var self = this;
            //    elemID = $(elem).attr('id');
            //    eCode = $(elem).val();
            //    isComboGridOpen = $(self).combogrid('isOpened');
            //    if (eCode == '') {
            //        //                    if (!validateGLAccount(elemID, null)) {
            //        //                        $(elem).val(prevGLCode);
            //        //                        return false;
            //        //                    }
            //        ClearGLGroupData(elemID);
            //    }
            //    else {
            //        grp = GetGLGroup(eCode);
            //        //                    if (!validateGLAccount(elemID, grp)) {
            //        //                        $(elem).val(prevGLCode);
            //        //                        return false;
            //        //                    }

            //        if (grp == null) {
            //            ClearGLGroupData(elemID);
            //        }
            //        else {
            //            SetGLGroupData(elemID, grp);
            //        }
            //    }
            //    setDetInstrument(hdnIsInstrumentElem, txtInstrumentElem, btnInstrumentElem);
            //    grpID = $(self).closest('tr').find('input[id$="hdnGLGroupID"]').val();
            //    if (grpID == '0' | grpID == '') {
            //        $(self).addClass('textError');
            //    }

            //});

            //$(elem).focus(function () {
            //    var self = this;
            //    var isComboGridOpen = $(self).combogrid('isOpened');

            //    if (!isComboGridOpen) {
            //        prevGLCode = $(self).val();
            //    }

            //    $(self).removeClass('textError');
            //    //$(elem).removeClass('fldDataError');
            //});    
        } // bindLocationList

        function btnSavePost()
        {
            var btnSaveUID = '<%=btnSave.UniqueID%>';

            __doPostBack(btnSaveUID, 'OnClick');
        }

        function btnSaveClick()
        {
            var bStatus = false;

            //alert('Save Clicked!');
            var editModeInt = parseInt($('#' + hdnEditDataModeInt).val());
            var journalID = parseInt($('#' + hdnJournalID).val());

            var task = {
                EditModeInt: editModeInt
                , JournalID: journalID
                , Task: "journalfull"
                , ValidateALL : 0
                , UseDetIDLink: 1
                , IsFromWebUI: 1
            };

            var jrnl = ReadJournalFromUI();

            var jrnlDetListDr = ReadJournalDetFromUI(gridViewIDDet);
            var jrnlDetListCr = ReadJournalDetFromUI(gridViewIDDet2);
            var jrnlDetList = jrnlDetListDr.concat(jrnlDetListCr);

            var jrnlInsListDr = ReadJournalDetInsFromUI('dvGridContainer');
            var jrnlInsListCr = ReadJournalDetInsFromUI('dvGridContainer2');
            var jrnlInsList = jrnlInsListDr.concat(jrnlInsListCr);

            var jrnlRefListDr = ReadJournalDetRefFromUI('dvGridContainer');
            var jrnlRefListCr = ReadJournalDetRefFromUI('dvGridContainer2');
            var jrnlRefList = jrnlRefListDr.concat(jrnlRefListCr);

            var vResult = ValidateJournal(task, jrnl, jrnlDetList, jrnlInsList, jrnlRefList);

            if (vResult != null) {
                if (vResult.IsError) {
                    //$('#' + curTxtAccRefCodeID).val(vResult.DetRefText);
                    alert(vResult.ErrorText);
                    bStatus = false;
                }
                else {
                    bStatus = true;

                    //var jSonString = $('#' + curTxtAccRefCodeID).closest('div.dvGrid').find('input.hdnRefJson').val();
                    //var detRefList = $.parseJSON(jSonString);

                    //var rText = GetAccRefTextByDetIDLink(detRefList, detID_Link, curAccRefTypeID);
                    //$('#' + curTxtAccRefCodeID).val(rText);

                    //$('#' + curTxtAccRefCodeID).val(totRow > 0 ? arText : '');
                }
            }
            else {
                alert('validation result = null');
                bStatus = false;
            }
            return bStatus;
        }


        function ReadJournalFromUI() {

            var companyID = parseInt($('#' + hdnCompanyID).val());
            var journalID = parseInt($('#' + hdnJournalID).val());
            var journalNO = $('#' + txtJournalNo).val();
            var journalDate = $('#' + txtJournalDate).val();
            var journalTypeID = parseInt($('#' + ddlJournalType).val());
            var accYearID = parseInt($('#' + ddlAccYear).val());

            var journalUpdateNo = parseInt($('#' + hdnJournalUpdateNo).val());

            var jrnl = {
                JournalID: journalID
                , CompanyID: companyID
                , JournalNo: journalNO
                , JournalDate: journalDate
                , JournalTypeID: journalTypeID
                , AccYearID: accYearID
                , JournalUpdateNo: journalUpdateNo
            };
            return jrnl;
        }

        function ReadJournalDetFromUI(rGridViewID) {

            var jrnlDetList = [];
            var journalID = $('#' + hdnJournalID).val();
            var grid = $('#' + rGridViewID);
            //var rowContainer = $('#dvGridRowContainer');
            var rowContainer = $('#' + rGridViewID);
            //var rowCount = $(rowContainer).find('div.dvGridRow:visible').length;
            var rowCount = $(rowContainer).find('.gridRow:visible').length;
            
            $(rowContainer).find('.gridRow').each(function (idx, rowElem) {
                var journalDetID = parseInt($(rowElem).find('input[id$="hdnJournalDetID"]').val());
                var recordstateRow = parseInt($(rowElem).find('input[id$="hdnRecordStateInt"]').val());
                var DrCr = parseInt($(rowElem).find('input[id$="hdnDrCr"]').val());

                var journalDetID_Link = parseInt($(rowElem).find('input[id$="hdnJournalDetID_Link"]').val())


                var journalDetSLNo = parseInt($(rowElem).find('span[id$="lblSLNo"]').text());

                var glGroupID = parseInt($(rowElem).find('input[id$="hdnGLGroupID"]').val());
                var glAccountID = parseInt($(rowElem).find('input[id$="hdnGLAccountID"]').val());

                var journalDetDesc = $(rowElem).find('input[id$="txtJournalDetDesc"]').val();

                var debitAmt = 0;
                var creditAmt = 0;
                if (DrCr == 0)
                {
                    var debitAmt = parseFloat(JSUtility.GetNumber($(rowElem).find('input[id$="txtDebitAmt"]').val()));
                }
                else
                {
                    var creditAmt = parseFloat(JSUtility.GetNumber($(rowElem).find('input[id$="txtCreditAmt"]').val()));
                }

                //var tranTypeID = parseInt($(rowElem).find('.hdnTranTypeID').val());

                var detItem = {
                    JournalDetID: journalDetID
                    , _RecordStateInt: recordstateRow
                    , JournalID: journalID
                    , JournalDetID: journalDetID
                    , JournalDetID_Link: journalDetID_Link
                    , JournalDetSLNo: journalDetSLNo
                    , GLAccountID: glAccountID
                    , GLGroupID: glGroupID
                    , JournalDetDesc: journalDetDesc
                    , DebitAmt: debitAmt
                    , CreditAmt: creditAmt
                    , DrCr: DrCr
                };
                jrnlDetList.push(detItem);
            });

            return jrnlDetList;
        }

        function ReadJournalDetInsFromUI(rGridViewID) {
            var grid = $('#' + rGridViewID);
            var jsonText = $(grid).find('.hdnInsJson').val();
            var insList = $.parseJSON(jsonText);
            return insList;
        }

        function ReadJournalDetRefFromUI(rGridViewID)
        {
            var grid = $('#' + rGridViewID);
            var jsonText = $(grid).find('.hdnRefJson').val();
            var refList = $.parseJSON(jsonText);
            return refList;
        }


        function ValidateJournal(task, jrnl, jrnlDetList, jrnlInsList, jrnlRefList)
        {
            var bRetrun = true;
            var companyid = $('#' + hdnCompanyID).val();
            var journalID = $('#' + hdnJournalID).val();

            var serviceUrl = ValidateJournalServiceLink + "?journalid=" + journalID;
            serviceUrl += '&companyid=' + companyid;

            var resultData = null;
            var isError = false;
            var isComplete = false;


            var jsonTaskText = JSUtility.JSONStringify(task);
            var jsonJrnlText = JSUtility.JSONStringify(jrnl);
            var jsonJrnlDetListText = JSUtility.JSONStringify(jrnlDetList);
            var jsonJrnlInsListText = JSUtility.JSONStringify(jrnlInsList);
            var jsonJrnlAccRefListText = JSUtility.JSONStringify(jrnlRefList);

            var jsonPostData = {
                'jsonTask': jsonTaskText
                    , 'jsonJournal': jsonJrnlText
                    , 'jsonJournalDetList': jsonJrnlDetListText
                    , 'jsonJournalInsList': jsonJrnlInsListText
                    , 'jsonJournalAccRefList': jsonJrnlAccRefListText
            };


            var dummyVar = $.ajax({
                //type: "GET",
                type: "POST",
                cache: false,
                async: false,
                dataType: "json",
                data: jsonPostData,
                url: serviceUrl,
                success: function (jsonData) {
                    resultData = jsonData;
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
                    JSSecurity.CheckAndLoginAjax(XMLHttpRequest, textStatus);
                }
            });

            return resultData;
        }


        function PrintTask() {
            var sError = $('#' + hdnPrintError).val();
            var rptKey = $('#' + hdnPrintReportKey).val();
            $('#' + hdnPrintReportKey).val('');
            $('#' + hdnPrintError).val('');

            if (sError != '') {
                alert(sError);
                return;
            }

            rptKey = $('#' + hdnPrintReportKey).val();
            alert('Print:' + rptKey);

        }

        function bindPopupControlEvents() {
            //        var btnID = $('#' + hdnPopupTriggerID).val();
            //        $('#' + dvGridDetailsPopup).find('select[id$="ddlLandOwner"]').change(function() {
            //            ShowProgress2();
            //            //__doPostBack(btnID, 'getbalance');
            //            $('#' + hdnPopupCommandID).val('getbalance');
            //            __doPostBack(btnID, '');
            //            //__doPostBack(btnID, 'OnClick');

            //            
            //            //$('#' + hdnPopupTriggerID).trigger('click');
            //        });
            //        
            //ddlLandOwner
        }

        function loadPopupData() {
            var btnID = $('#' + hdnPopupTriggerID).val();
            //__doPostBack(btnID, '');
            //__doPostBack(btnID, 'fillcombo');
            $('#' + hdnPopupCommandID).val('fillcombo');
            __doPostBack(btnID, '');
            //__doPostBack(btnID, 'OnClick');
            //document.getElementById(hdnPopupTriggerID).click();
            return true;
            //$('#' + hdnPopupTriggerID).trigger('click');
        }


        function UpdateTotalRowLabel()
        {
            var totRowDr = $("#" + gridViewIDDet).find('input.txtDebit:visible').length;
            var totRowCr = $("#" + gridViewIDDet2).find('input.txtCredit:visible').length;

            $('#' + lblTotalRowDr).text("Total Row : " + totRowDr);
            $('#' + lblTotalRowCr).text("Total Row : " + totRowCr);

        }

        function GetTotalSumDebit() {
            var totDebit = 0;
            $("#" + gridViewIDDet).find('input.txtDebit:visible').each(function (index, elem) {
                drAmt = parseFloat(JSUtility.GetNumber($(elem).val()));
                if (!isNaN(drAmt)) {
                    totDebit += drAmt;
                }
            });
            return totDebit;
        }

        function GetTotalSumCredit() {
            var totCredit = 0;
            $("#" + gridViewIDDet2).find('input.txtCredit:visible').each(function (index, elem) {
                crAmt = parseFloat(JSUtility.GetNumber($(elem).val()));
                if (!isNaN(crAmt)) {
                    totCredit += crAmt;
                }
            });
            return totCredit;
        }


        function sumJournalDetAmt() {
            var totDebit = GetTotalSumDebit();
            var totCredit = GetTotalSumCredit();

//            $("#" + gridViewIDDet).find('input.txtDebit:visible').each(function (index, elem) {
//                drAmt = parseFloat(JSUtility.GetNumber($(elem).val()));
//                if (!isNaN(drAmt)) {
//                    totDebit += drAmt;
//                }
//            });

//            $("#" + gridViewIDDet2).find('input.txtCredit:visible').each(function (index, elem) {
//                crAmt = parseFloat(JSUtility.GetNumber($(elem).val()));
//                if (!isNaN(crAmt)) {
//                    totCredit += crAmt;
//                }
//            });

            $("#" + totDebitID).val(JSUtility.FormatCurrency(totDebit));
            $("#" + totCreditID).val(JSUtility.FormatCurrency(totCredit));

            SetDiffAmtLabel(totDebit,totCredit);
        }

        function SetDiffAmtLabel(totDebit, totCredit)
        {
            $("#" + lblDiffAmtDr).text('');
            $("#" + lblDiffAmtCr).text('');

            if (totDebit != totCredit)
            {
                var diffAmt = totDebit - totCredit;
                if (diffAmt > 0)
                {
                    $("#" + lblDiffAmtDr).text("Difference : " + JSUtility.FormatCurrency(diffAmt));
                }
                else
                {
                    $("#" + lblDiffAmtCr).text("Difference : " + JSUtility.FormatCurrency(Math.abs(diffAmt)));
                }
            }


        }


        function GetTotalSumIns() {
            
            var table = $('#' + dvPopupIns).find('.tblIns');

            //var detRefListLinkNew = [];

            var slno = 0;

            var totInsAmt = 0;
            $(table).find('tr.rowIns:visible').each(function (idx, rowElem) {
                //var amt = JSUtility.GetNumber($(rowElem).find('.txtInsPopupAmount:visible').val());
                var amt = JSUtility.GetNumber($(rowElem).find('.txtInsPopupAmount').val());
                totInsAmt += amt;
            });

            return totInsAmt;
        }

        function bindSumJournalDetAmt(gridViewID) {
            //debit credit sum

            if (gridViewID == gridViewIDDet) {
                $("#" + gridViewIDDet).find('input.txtDebit').keyup(function (e) {
                    var elem = this;
                    if (JSUtility.IsPrintableChar(e.keyCode, true, true)) {
                        //                if (JSUtility.GetNumber($(elem).val()) > 0) {
                        //                    $(elem).closest('tr').find('input.txtCredit').val('0.00');
                        //                }
                        sumJournalDetAmt();
                    }
                });
                sumJournalDetAmt();
            }


            //        $("#" + gridViewIDDet).find('input.txtDebit').bind('paste', function () {
            //            //$('span').text('paste behavior detected!')
            //            //alert('paste');
            //            sumJournalDetAmt();
            //        });


            //        $("#" + gridViewIDDet).find('input.txtDebit').paste(function (e) {
            //            alert('paste')
            //            var elem = this;
            ////            if (JSUtility.IsPrintableChar(e.keyCode, true, true)) {
            ////                if (JSUtility.GetNumber($(elem).val()) > 0) {
            ////                    $(elem).closest('tr').find('input.txtCredit').val('0.00');
            ////                }
            ////                sumJournalDetAmt();
            ////            }
            //        });


            if (gridViewID == gridViewIDDet2) {
                $("#" + gridViewIDDet2).find('input.txtCredit').keyup(function (e) {
                    var elem = this;
                    if (JSUtility.IsPrintableChar(e.keyCode, true, true)) {
                        //                if (JSUtility.GetNumber($(elem).val()) > 0) {
                        //                    $(elem).closest('tr').find('input.txtDebit').val('0.00');
                        //                }
                        sumJournalDetAmt();
                    }
                });
                sumJournalDetAmt();
            }

        }

        function setDetRowDebitCredit(ddlDrCrElem, txtDebitElem, txtCreditElem, ddlChange) {
            //        ddlChange = ddlChange || false;
            //        
            //        var drCr = parseInt($(ddlDrCrElem).val());
            //        //alert(drCr);
            //        if (drCr == 0) {
            //            if (ddlChange) {
            //                var crAmt = JSUtility.GetNumber(txtCreditElem.val());
            //                if (crAmt > 0) {
            //                    txtDebitElem.val(txtCreditElem.val());
            //                }
            //            }

            //            txtCreditElem.val('');

            //            txtDebitElem.prop('disabled', false);
            //            txtCreditElem.prop('disabled', true);
            //        }
            //        else {
            //            if (ddlChange) {
            //                var drAmt = JSUtility.GetNumber(txtDebitElem.val());
            //                if (drAmt > 0) {
            //                    txtCreditElem.val(txtDebitElem.val());
            //                }
            //            }
            //            
            //            txtDebitElem.val('');

            //            txtDebitElem.prop('disabled', true);
            //            txtCreditElem.prop('disabled', false);
            //        }
            //        sumJournalDetAmt();
        }

        function setDetInstrument(hdnIsInstrumentElem, txtInstrumentElem, btnInstrumentElem) {
            var isInstrument = parseInt($(hdnIsInstrumentElem).val());
            if (isInstrument == 1) {
                txtInstrumentElem.prop('disabled', false);
                btnInstrumentElem.prop('disabled', false);
            }
            else {
                txtInstrumentElem.prop('disabled', true);
                btnInstrumentElem.prop('disabled', true);
            }
        }


        function bindJournalDetEvents(gridViewID) {

            $("#" + gridViewID).find('.gridRow').each(function (index, elem) {

                var hdnJournalDetID_LinkElem = $(elem).find('input[id$="hdnJournalDetID_Link"]');

                var hdnDrCrElem = $(elem).find('input[id$="hdnDrCr"]');



                var txtGLAccCodeElem = $(elem).find('input[id$="txtGLAccountCode"]');
                var hdnGLAccIDElem = $(elem).find('input[id$="hdnGLAccountID"]');
                var hdnGLGroupIDElem = $(elem).find('input[id$="hdnGLGroupID"]');
                var hdnGLGroupClassIDElem = $(elem).find('input[id$="hdnGLGroupClassID"]');
                var hdnIsInstrumentElem = $(elem).find('input[id$="hdnIsInstrument"]');

                var txtInstrumentElem = $(elem).find('input[id$="txtInstrument"]');
                var btnInstrumentElem = $(elem).find('input[id$="btnInstrument"]');


                //var ddlPrevValue = '';

                //            $(hdnDrCrElem).focus(function (e) {
                //                ddlPrevValue = $(this).val();
                //            });

                //            $(hdnDrCrElem).change(function (e) {
                //                //alert('change');
                //                //setDetRowDebitCredit(ddlDrCrElem, txtDebitElem, txtCreditElem);

                //                var ddlCurValue = $(this).val();

                //                var linkID = parseInt($(hdnJournalDetID_LinkElem).val());
                //                var insCount = GetInsCountByDetLink(linkID);

                //                if (ddlPrevValue != ddlCurValue) {
                //                    var insCount = GetInsCountByDetLink(linkID);
                //                    if (insCount > 0) {
                //                        if (confirm('Instrument entries will be removed for current row. Continue?')) {
                //                            var rCnt = RemoveInsByDetLink(linkID);
                //                            $(txtInstrumentElem).val('');
                //                        }
                //                        else {
                //                            $(this).val(ddlPrevValue);
                //                            return;
                //                        }
                //                    }
                //                }

                //                if (IForm.EditMode == Enums.EditMode.Add | IForm.EditMode == Enums.EditMode.Edit) {
                //                    setDetRowDebitCredit(hdnDrCrElem, txtDebitElem, txtCreditElem, true);
                //                    //check current entry
                //                }
                //            });


                if (gridViewID == gridViewIDDet) {

                    var txtDebitElem = $(elem).find('input.txtDebit');


                    $(txtDebitElem).blur(function (e) {
                        sumJournalDetAmt();
                    });

                    $(txtDebitElem).bind('paste', function () {
                        //$('span').text('paste behaviour detected!')
                        //alert('paste');
                        setTimeout(
                                function () { sumJournalDetAmt(); }
                                , 10 );
                    });
                }

                if (gridViewID == gridViewIDDet2) {
                    var txtCreditElem = $(elem).find('input.txtCredit');

                    $(txtCreditElem).blur(function (e) {
                        sumJournalDetAmt();
                    });

                    $(txtCreditElem).bind('paste', function () {
                        setTimeout(
                    function () { sumJournalDetAmt(); }
                    , 10
               );
                    });
                }


                if (IForm.EditMode == Enums.EditMode.Add | IForm.EditMode == Enums.EditMode.Edit) {
                    //setDetRowDebitCredit(hdnDrCrElem, txtDebitElem, txtCreditElem);
                    setDetInstrument(hdnIsInstrumentElem, txtInstrumentElem, btnInstrumentElem);
                }
            });


            //        $("#" + gridViewIDDet).find('select[id$="ddlDrCr"]').each(function (index, elem) {
            //             $(elem).change(function(e){
            //                 //alert('change');
            //             }); 
            //       
            //        });
        }



        function bindGLGroupList(gridViewID) {
            var cgColumns = [{ 'columnName': 'glgroupcode', 'width': '80', 'align': 'left', 'highlight': 2, 'label': 'Code' }
                             , { 'columnName': 'glgroupnameshort', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Short Name' }
                             , { 'columnName': 'glgroupname', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                             , { 'columnName': 'glgroupnameparent', 'width': '130', 'align': 'left', 'highlight': 0, 'label': 'Parent' }
                            ];


            var glgroupclassinc = $('#' + hdnGLGroupClassInclude).val();
            var glgroupclassexc = $('#' + hdnGLGroupClassExclude).val();
            var companyid = $('#' + hdnCompanyID).val();

            var serviceURL = GLGroupServiceLink + "?isterm=1&includeempty=1&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            serviceURL += "&companyid=" + companyid;
            serviceURL += "&glgroupclassinclude=" + glgroupclassinc;
            serviceURL += "&glgroupclassexclude=" + glgroupclassexc;
            serviceURL += "&ispaging=1";
            serviceURL += "&namecomptype=" + Enums.DataCompareType.Contains;


            //var gridSelector = "#" + gridViewIDDet;
            //var gridSelector = "#" + gridViewIDDet + ",#" + gridViewIDDet2;
            var gridSelector = "#" + gridViewID;


            $(gridSelector).find('input[id$="txtGLGroupCode"]').each(function (index, elem) {
                ///list click

                var elemRow = $(elem).closest('tr.gridRow');

                var hdnJournalDetID_LinkElem = $(elemRow).find('input[id$="hdnJournalDetID_Link"]');

                var hdnDrCrElem = $(elemRow).find('select[id$="hdnDrCr"]');

                var hdnGLGroupIDElem = $(elemRow).find('input[id$="hdnGLGroupID"]');


                var hdnIsInstrumentElem = $(elemRow).find('input[id$="hdnIsInstrument"]');
                var txtInstrumentElem = $(elemRow).find('input[id$="txtInstrument"]');
                var btnInstrumentElem = $(elemRow).find('input[id$="btnInstrument"]');

                var prevGLCode = '';

                $(elem).closest('tr').find('input[id$="btnGLGroupAC"]').click(function (e) {
                    elmID = $(elem).attr('id');
                    //$(elem).combogrid("show");
                    $(elem).combogrid("dropdownClick");
                });

                //highlight: 0=none,1=exact,2=startwith,3=endswith,4=contains,

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
                    width: 500,
                    url: serviceURL,
                    search: function (event, ui) {
                        var newServiceURL = JSUtility.AddTimeToQueryString(serviceURL);
                        $(this).combogrid("option", "url", newServiceURL);
                    },
                    //"select item" event handler to set input field
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
                            ClearGLGroupData(elemID);
                            return false;
                            //ClearGLAccountData(elemID);
                        }



                        if (ui.item.id == 0) {
                            event.preventDefault();
                            return false;
                            //ClearGLAccountData(elemID);
                        }
                        else {
                            SetGLGroupData(elemID, ui.item);
                        }
                        setDetInstrument(hdnIsInstrumentElem, txtInstrumentElem, btnInstrumentElem);
                        return false;
                    }
                });


                $(elem).blur(function () {
                    var self = this;
                    elemID = $(elem).attr('id');
                    eCode = $(elem).val();
                    isComboGridOpen = $(self).combogrid('isOpened');
                    if (eCode == '') {
                        //                    if (!validateGLAccount(elemID, null)) {
                        //                        $(elem).val(prevGLCode);
                        //                        return false;
                        //                    }
                        ClearGLGroupData(elemID);
                    }
                    else {
                        grp = GetGLGroup(eCode);
                        //                    if (!validateGLAccount(elemID, grp)) {
                        //                        $(elem).val(prevGLCode);
                        //                        return false;
                        //                    }

                        if (grp == null) {
                            ClearGLGroupData(elemID);
                        }
                        else {
                            SetGLGroupData(elemID, grp);
                        }
                    }
                    setDetInstrument(hdnIsInstrumentElem, txtInstrumentElem, btnInstrumentElem);
                    grpID = $(self).closest('tr').find('input[id$="hdnGLGroupID"]').val();
                    if (grpID == '0' | grpID == '') {
                        $(self).addClass('textError');
                    }

                });

                $(elem).focus(function () {
                    var self = this;
                    var isComboGridOpen = $(self).combogrid('isOpened');

                    if (!isComboGridOpen) {
                        prevGLCode = $(self).val();
                    }

                    $(self).removeClass('textError');
                    //$(elem).removeClass('fldDataError');
                });

            });                        //textgrpcode each
        } //bindGLGroupList






        function bindGLAccounctList(gridViewID) {
            var cgColumns = [{ 'columnName': 'glacccode', 'width': '80', 'align': 'left', 'highlight': 2, 'label': 'Code' }
                             , { 'columnName': 'glaccname', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                             , { 'columnName': 'glgroupname', 'width': '130', 'align': 'left', 'highlight': 0, 'label': 'Group' }
                             , { 'columnName': 'glacctypename', 'width': '100', 'align': 'left', 'highlight': 0, 'label': 'Type' }
                            ];


            var glgroupclassinc = $('#' + hdnGLGroupClassInclude).val();
            var glgroupclassexc = $('#' + hdnGLGroupClassExclude).val();
            var companyid = $('#' + hdnCompanyID).val();
            var glLocationID = parseInt($('#' + ddlLocation).val());
            //var glLocationID = $('#' + ddlLocation).val();

            var serviceURL = GLAccountServiceLink + "?isterm=1&includeempty=1&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            serviceURL += "&namecomptype=" + Enums.DataCompareType.Contains + "&ispaging=1";
            serviceURL += "&acctypefilter=" + Enums.GLAccountTypeFilter.NormalSubAccount;
            serviceURL += "&glgroupclassinclude=" + glgroupclassinc;
            serviceURL += "&glgroupclassexclude=" + glgroupclassexc;
            serviceURL += "&companyid=" + companyid;
            serviceURL += "&gllocationid=" + glLocationID;

            //var gridSelector = "#" + gridViewIDDet + ",#" + gridViewIDDet2;
            var gridSelector = "#" + gridViewID;

            $(gridSelector).find('input[id$="txtGLAccountCode"]').each(function (index, elem) {
                ///list click

                var elemRow = $(elem).closest('tr.gridRow');

                var hdnJournalDetID_LinkElem = $(elemRow).find('input[id$="hdnJournalDetID_Link"]');

                var hdnDrCrElem = $(elemRow).find('select[id$="hdnDrCr"]');

                var hdnGLAccIDElem = $(elemRow).find('input[id$="hdnGLAccountID"]');


                var hdnIsInstrumentElem = $(elemRow).find('input[id$="hdnIsInstrument"]');
                var txtInstrumentElem = $(elemRow).find('input[id$="txtInstrument"]');
                var btnInstrumentElem = $(elemRow).find('input[id$="btnInstrument"]');

                var prevGLCode = '';

                $(elem).closest('tr').find('input[id$="btnGLAccountAC"]').click(function (e) {
                    elmID = $(elem).attr('id');
                    //$(elem).combogrid("show");
                    $(elem).combogrid("dropdownClick");
                });

                //highlight: 0=none,1=exact,2=startwith,3=endswith,4=contains,

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
                    width: 500,
                    url: serviceURL,

                    search: function (event, ui) {
                        var elemRowCur = $(elem).closest('tr.gridRow');
                        var glgroupID = $(elemRowCur).find('input[id$="hdnGLGroupID"]').val();
                        var newServiceURL = serviceURL + "&glgroupid=" + glgroupID
                        newServiceURL = JSUtility.AddTimeToQueryString(newServiceURL);
                        $(this).combogrid("option", "url", newServiceURL);
                    },
                    select: function (event, ui) {
                        //alert(ui.item.typename);
                        //$(".txtComboGrid").val(ui.item.code);
                        elemID = $(elem).attr('id');
                        if (!validateGLAccount(elemID, ui.item)) {
                            $(elem).val(prevGLCode);
                            return false;
                        }

                        if (!ui.item) {
                            event.preventDefault();
                            ClearGLAccountData(elemID);
                            return false;
                            //ClearGLAccountData(elemID);
                        }


                        if (ui.item.id == 0) {
                            event.preventDefault();
                            return false;
                            //ClearGLAccountData(elemID);
                        }
                        else {
                            SetGLAccountData(elemID, ui.item);
                        }
                        setDetInstrument(hdnIsInstrumentElem, txtInstrumentElem, btnInstrumentElem);
                        return false;
                    }
                });


                $(elem).blur(function () {
                    var self = this;
                    elemID = $(elem).attr('id');
                    eCode = $(elem).val();
                    isComboGridOpen = $(self).combogrid('isOpened');
                    if (eCode == '') {
                        if (!validateGLAccount(elemID, null)) {
                            $(elem).val(prevGLCode);
                            return false;
                        }
                        ClearGLAccountData(elemID);
                    }
                    else {
                        acc = GetGLAccount(eCode);
                        if (!validateGLAccount(elemID, acc)) {
                            $(elem).val(prevGLCode);
                            return false;
                        }

                        if (acc == null) {
                            ClearGLAccountData(elemID);
                        }
                        else {
                            SetGLAccountData(elemID, acc);
                        }
                    }
                    setDetInstrument(hdnIsInstrumentElem, txtInstrumentElem, btnInstrumentElem);
                    accID = $(self).closest('tr').find('input[id$="hdnGLAccountID"]').val();
                    if (accID == '0' | accID == '') {
                        $(self).addClass('textError');
                    }

                });

                $(elem).focus(function () {
                    var self = this;
                    var isComboGridOpen = $(self).combogrid('isOpened');

                    if (!isComboGridOpen) {
                        prevGLCode = $(self).val();
                    }

                    $(self).removeClass('textError');
                    //$(elem).removeClass('fldDataError');
                });

            });                        //textacccode each
        } //bindGLAccounctList


        function validateGLAccount(txtGLAccCodeID, glAccItem) {
            var result = true;
            glAccItem = glAccItem || null;


            var elemRow = $('#' + txtGLAccCodeID).closest('tr.gridRow');

            var hdnJournalDetID_LinkElem = $(elemRow).find('input[id$="hdnJournalDetID_Link"]');
            var hdnDrCrElem = $(elemRow).find('select[id$="hdnDrCr"]');
            var hdnGLAccIDElem = $(elemRow).find('input[id$="hdnGLAccountID"]');

            var hdnIsInstrumentElem = $(elemRow).find('input[id$="hdnIsInstrument"]');
            var txtInstrumentElem = $(elemRow).find('input[id$="txtInstrument"]');
            var btnInstrumentElem = $(elemRow).find('input[id$="btnInstrument"]');
            var drcr = parseInt(hdnDrCrElem.val());

            var linkID = parseInt($(hdnJournalDetID_LinkElem).val());
            var insCount = GetInsCountByDetLink(linkID, drcr);

            var rCnt = 0;

            result = true;

            if (glAccItem == null) {
                if (insCount > 0) {
                    if (confirm('Instrument entries will be removed for current row. Continue?')) {
                        rCnt = RemoveInsByDetLink(linkID, drcr);
                        $(txtInstrumentElem).val('');
                        result = true;
                    }
                    else {
                        result = false;
                        //$(elem).val(prevGLCode);
                        //return false;
                    }
                } //inscount
            }
            else {

                if (glAccItem.isinstrument == 0) {
                    if (insCount > 0) {
                        if (confirm('Instrument entries will be removed for current row. Continue?')) {
                            rCnt = RemoveInsByDetLink(linkID, drcr);
                            $(txtInstrumentElem).val('');
                            result = true;
                        }
                        else {
                            result = false;
                            //$(elem).val(prevGLCode);
                            //return false;

                        }
                    } //inscount
                }
                else {
                    if (insCount > 0) {
                        var insModeID = GetDetRowInsMode(drcr);
                        if (insModeID == Enums.InstrumentMode.Issue) {
                            var curAccID = parseInt($(hdnGLAccIDElem).val());
                            if (curAccID > 0) {
                                if (curAccID != glAccItem.id) {
                                    if (confirm('Instrument entries will be removed for current row. Continue?')) {
                                        rCnt = RemoveInsByDetLink(linkID, drcr);
                                        $(txtInstrumentElem).val('');
                                        result = true;
                                    }
                                    else {
                                        result = false;
                                        //$(elem).val(prevGLCode);
                                        //return false;
                                    }
                                }
                            } //curaccid >0
                        } //insmode == issue
                    } //inscount
                }  //else insid ==0
            } // else item == null

            return result;
        }



        function bindTranTypeList(gridViewID) {
            var cgColumns = [{ 'columnName': 'code', 'width': '80', 'align': 'left', 'highlight': 2, 'label': 'Code' }
                             , { 'columnName': 'name', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                             , { 'columnName': 'categoryname', 'width': '130', 'align': 'left', 'highlight': 0, 'label': 'Category' }
                            ];

            var companyid = $('#' + hdnCompanyID).val();

            var serviceURL = AccRefServiceLink + "?isterm=1&includeempty=1&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            serviceURL += "&namecomptype=" + Enums.DataCompareType.Contains + "&ispaging=1";
            serviceURL += "&typeid=" + Enums.AccRefType.TranCode;
            serviceURL += "&companyid=" + companyid;


            $("#" + gridViewID).find('input[id$="txtTranType"]').each(function (index, elem) {

                $(elem).closest('tr').find('input[id$="btnTranTypeAC"]').click(function (e) {
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
                    cache: false,
                    url: serviceURL,
                    search: function (event, ui) {
                        //var newServiceURL = JSUtility.AddTimeToQueryString(serviceURL);
                        //$(this).combogrid("option", "url", newServiceURL);
                    },
                    //"select item" event handler to set input field
                    select: function (event, ui) {
                        //alert(ui.item.typename);
                        //$(".txtComboGrid").val(ui.item.code);
                        elemID = $(elem).attr('id');
                        if (!ui.item) {
                            event.preventDefault();
                            ClearTranType(elemID);
                            return false;
                            //ClearGLAccountData(elemID);
                        }
                        
                        
                        if (ui.item.id == 0) {
                            event.preventDefault();
                            ClearTranType(elemID);
                        }
                        else {
                            SetTranTypeData(elemID, ui.item);
                        }
                        return false;
                    }
                });

                $(elem).blur(function () {
                    var self = this;
                    elemID = $(elem).attr('id');
                    ttCode = $(elem).val();

                    if (ttCode == '') {
                        ClearTranType(elemID);
                    }
                    else {

                    }

                    ttID = $(self).closest('tr').find('input[id$="hdnTranTypeID"]').val();
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
        }


        function bindAccRefList(gridViewID) {
            $("#" + gridViewID).find('input[id$="txtCostCenter"]').each(function (index, elem) {
                $(elem).keydown(function (e) {
                    elmID = $(elem).attr('id');
                    //insert key(45) or key down (40)  or ctrl+L(76)
                    if (e.keyCode == 45 || e.keyCode == 40 || (e.keyCode == 76 && e.ctrlKey)) {
                        e.preventDefault();
                        ShowPopupAccRef(elmID, Enums.AccRefType.CostCenter);
                    }

                    //L=76
                    //l=108


                    //keydown = 40

                    //if (e.keyCode == 65 && e.ctrlKey) {
                    //    alert('ctrl A');
                    //}

                    //if (e.keyCode == 76 && e.ctrlKey) {
                    //    alert('ctrl L');
                    //}
                });

                $(elem).closest('tr').find('input[id$="btnCostCenter"]').click(function (e) {
                    elmID = $(elem).attr('id');
                    ShowPopupAccRef(elmID, Enums.AccRefType.CostCenter);
                });
            });

            $("#" + gridViewID).find('input[id$="txtReference"]').each(function (index, elem) {
                $(elem).keydown(function (e) {
                    elmID = $(elem).attr('id');
                    //insert key(45) or key down (40)  or ctrl+L(76)
                    if (e.keyCode == 45 || e.keyCode == 40 || (e.keyCode == 76 && e.ctrlKey)) {
                        e.preventDefault();
                        ShowPopupAccRef(elmID, Enums.AccRefType.Reference);
                    }
                });

                $(elem).closest('tr').find('input[id$="btnReference"]').click(function (e) {
                    elmID = $(elem).attr('id');
                    ShowPopupAccRef(elmID, Enums.AccRefType.Reference);
                });
            });

        }


        function bindAccRefListAC() {

            var companyid = $('#' + hdnCompanyID).val();

            var serviceURL = AccRefServiceLink + "?isterm=1&includeempty=1&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            serviceURL += "&namecomptype=" + Enums.DataCompareType.Contains + "&ispaging=1";
            serviceURL += "&typeid=" + Enums.AccRefType.CostCenter;
            serviceURL += "&companyid=" + companyid;


            var cgColumns = [{ 'columnName': 'code', 'width': '80', 'align': 'left', 'highlight': 2, 'label': 'Code' }
                             , { 'columnName': 'name', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                             , { 'columnName': 'categoryname', 'width': '130', 'align': 'left', 'highlight': 0, 'label': 'Category' }
                            ];




            $("#" + gridViewIDDet).find('input[id$="txtCostCenter"]').each(function (index, elem) {
                $(elem).closest('tr').find('input[id$="btnCostCenter"]').click(function (e) {
                    elmID = $(elem).attr('id');
                    ShowPopupCostCenter(elmID);
                });


                $(elem).closest('tr').find('input[id$="btnCostCenterAC"]').click(function (e) {
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
                    showError:true,
                    colModel: cgColumns,
                    width: 400,
                    url: serviceURL,
                    search: function (event, ui) {
                        var newServiceURL = JSUtility.AddTimeToQueryString(serviceURL);
                        $(this).combogrid("option", "url", newServiceURL);
                    },
                    //"select item" event handler to set input field
                    select: function (event, ui) {
                        //alert(ui.item.typename);
                        //$(".txtComboGrid").val(ui.item.code);
                        elemID = $(elem).attr('id');
                        if (!ui.item) {
                            event.preventDefault();
                            ClearCostCenter(elemID);
                            return false;
                            //ClearGLAccountData(elemID);
                        }

                        if (ui.item.id == 0) {
                            event.preventDefault();
                            ClearCostCenter(elemID);
                        }
                        else {
                            SetCostCenterData(elemID, ui.item);
                        }
                        return false;
                    }
                });

                $(elem).blur(function () {
                    var self = this;
                    elemID = $(elem).attr('id');
                    ttCode = $(elem).val();

                    if (ttCode == '') {
                        ClearCostCenter(elemID);
                    }
                    else {

                    }

                    ttID = $(self).closest('tr').find('input[id$="hdnCostCenterID"]').val();
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

        } //bind Cost Center


        function bindInsList(gridViewID) {
            var gridSelector = "#" + gridViewID;
            $(gridSelector).find('input[id$="txtInstrument"]').each(function (index, elem) {
                $(elem).keydown(function (e) {
                    elmID = $(elem).attr('id');
                    //insert key(45) or key down (40)  or ctrl+L(76)
                    if (e.keyCode == 45 || e.keyCode == 40 || (e.keyCode == 76 && e.ctrlKey)) {
                        e.preventDefault();
                        ShowPopupIns(elmID);
                    }
                });

                $(elem).closest('tr').find('input[id$="btnInstrument"]').click(function (e) {
                    elmID = $(elem).attr('id');
                    ShowPopupIns(elmID);
                });
            });
        }



        function bindAccRefPopup() {

            $('#' + dvPopupAccRef).dialog({
                title: "Cost Center Allocation",
                autoOpen: false,
                resizable: false,
                modal: true,
                position: 'center',
                closeOnEscape: true,
                top: 0,
                left: 0,
                width: 550,
                height: 300,
                open: function (event, ui) {
                    //self.$popupDialog.parent().appendTo(jQuery("form:first"));
                    $('#' + curTxtAccRefCodeID).closest('table').closest('tr').addClass('gridSelectedRow');
                    //$(ui.element).closest('table').closest('tr').addClass('gridSelectedRow');
                },
                close: function (event, ui) {
                    $('#' + curTxtAccRefCodeID).closest('table').closest('tr').removeClass('gridSelectedRow');
                }
            });


            $('#' + dvPopupAccRef).find('.btnPopupAccRefOK').click(function (e) {
                //alert('OK CLicked');
                if (AccRefPopupOKClicked()) {
                    $('#' + dvPopupAccRef).dialog('close');
                    var gridRow = $('#' + curTxtAccRefCodeID).closest('tr.gridRow');

                    if ($('#' + curTxtAccRefCodeID).is(":enabled"))
                    {
                        $('#' + curTxtAccRefCodeID).focus();
                    }
                }
            });

            $('#' + dvPopupAccRef).find('.btnPopupAccRefCancel').click(function (e) {
                //alet('OK CLicked');
                $('#' + dvPopupAccRef).dialog('close');
            });

            $('#' + dvPopupAccRef).find('.btnPopupAccRefNewRow').click(function (e) {
                //alet('OK CLicked');
                var table = $('#' + dvPopupAccRef).find('.tblAccRef');
                //var remainAmt = GetAccRefRemainAmount();
                AddNewRowToAccRefList(table, curAccRefTypeID, null);
                //$(newRow).find('.txtAccRefPopupAmount').val(remainAmt);

                //newRow = null;
                table = null;

            });
        } ////function ends

        function bindAccRefPopupListByRow(rowElem, accRefTypeID) {

            var elem = $(rowElem).find('input.txtAccRefPopupAccRef');

            bindAccRefPopupCategoryList(rowElem, accRefTypeID);
            bindAccRefPopupAccRefList(rowElem, accRefTypeID);

            $(elem).closest('tr').find('.btnAccRefPopupDelete').click(function (e) {
                //elmID = $(elem).attr('id');

                ccID = JSUtility.GetNumber($(elem).closest('tr').find('.hdnAccRefPopupAccRefID').val());
                amt = JSUtility.GetNumber($(elem).closest('tr').find('.txtAccRefPopupAmount').val());

                if (ccID > 0 || amt > 0) {
                    if (!confirm('Are your sure to delete?')) {
                        return;
                    }
                }

                $(elem).closest('tr').find('.hdnRecordState').val(Enums.RecordState.Deleted);
                $(elem).closest('tr').hide();
                RefreshAccRefListRowNumber();

            });



        } //function ends


        function bindAccRefPopupAccRefList(rowElem, accRefTypeID)
        {
            var cgColumns = [{ 'columnName': 'code', 'width': '80', 'align': 'left', 'highlight': 2, 'label': 'Code' }
                    , { 'columnName': 'name', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                    , { 'columnName': 'categoryname', 'width': '130', 'align': 'left', 'highlight': 0, 'label': 'Category' }
                ];

            var companyid = $('#' + hdnCompanyID).val();
            var glLocationID = parseInt($('#' + ddlLocation).val());

            var serviceURL = AccRefServiceLink + "?isterm=1&includeempty=1&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            serviceURL += "&namecomptype=" + Enums.DataCompareType.Contains + "&ispaging=1";
            serviceURL += "&typeid=" + accRefTypeID;
            serviceURL += "&companyid=" + companyid;
            serviceURL += "&gllocationid=" + glLocationID;


            var elem = $(rowElem).find('input.txtAccRefPopupAccRef');
            var elemBtn = $(rowElem).find('input.btnAccRefPopupAccRefAC');

            var prevGLCode = '';

            $(elemBtn).click(function (e) {
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
                search: function (event, ui) {
                    var elemRowCur = $(elem).closest('tr');
                    var categoryID = $(elemRowCur).find('.hdnAccRefPopupCategoryID').val();
                    var newServiceURL = serviceURL + "&categoryid=" + categoryID
                    $(this).combogrid("option", "url", newServiceURL);
                },
                //"select item" event handler to set input field
                select: function (event, ui) {
                    //alert(ui.item.typename);
                    //$(".txtComboGrid").val(ui.item.code);
                    elemID = $(elem).attr('id');

                    if (!ui.item) {
                        event.preventDefault();
                        //ClearAccRefPopup(elem);
                        $(elem).val('');
                        $(elem).closest('tr').find('.hdnAccRefPopupAccRefID').val('0');
                        return false;
                    }
                        
                        
                    if (ui.item.id == 0) {
                        event.preventDefault();
                        //ClearAccRefPopup(elem);
                        $(elem).val('');
                        $(elem).closest('tr').find('.hdnAccRefPopupAccRefID').val('0');
                    }
                    else {
                        SetAccRefDataPopup(elem, ui.item);
                        $(elem).closest('tr').find('.txtAccRefPopupAmount').focus();

                    }
                    return false;
                }
            });

            
            $(elem).blur(function () {
                var self = this;
                elemID = $(elem).attr('id');
                ccCode = $(elem).val();

                if (ccCode == '') {
                    //ClearAccRefPopup(elem);

                    $(elem).val('');
                    $(elem).closest('tr').find('.hdnAccRefPopupAccRefID').val('0');
                }
                else {

                }

                ccID = $(self).closest('tr').find('.hdnAccRefPopupAccRefID').val();
                if (ccID == '0' | ccID == '') {
                    $(self).addClass('textError');
                }
            });

            $(elem).focus(function () {
                var self = this;
                $(self).removeClass('textError');
                //$(elem).removeClass('fldDataError');
            });

        }

        function bindAccRefPopupCategoryList(rowElem, accRefTypeID) {
            var cgColumns = [{ 'columnName': 'code', 'width': '80', 'align': 'left', 'highlight': 2, 'label': 'Code' }
                    , { 'columnName': 'name', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                    , { 'columnName': 'parentname', 'width': '130', 'align': 'left', 'highlight': 0, 'label': 'Parent' }
            ];

            var companyid = $('#' + hdnCompanyID).val();

            var serviceURL = AccRefCategoryServiceLink + "?isterm=1&includeempty=1&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            serviceURL += "&namecomptype=" + Enums.DataCompareType.Contains + "&ispaging=1";
            serviceURL += "&typeid=" + accRefTypeID;
            serviceURL += "&companyid=" + companyid;


            var elem = $(rowElem).find('input.txtAccRefPopupCategory');
            var elemBtn = $(rowElem).find('input.btnAccRefPopupCategoryAC');

            var prevGLCode = '';

            $(elemBtn).click(function (e) {
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
                search: function (event, ui) {
                    var newServiceURL = JSUtility.AddTimeToQueryString(serviceURL);
                    $(this).combogrid("option", "url", newServiceURL);
                },
                //"select item" event handler to set input field
                select: function (event, ui) {
                    //alert(ui.item.typename);
                    elemID = $(elem).attr('id');

                    if (!ui.item) {
                        event.preventDefault();
                        //ClearAccRefPopup(elem);
                        $(elem).val('');
                        $(elem).closest('tr').find('.hdnAccRefPopupCategoryID').val('0');

                        $(elem).closest('tr').find('.hdnAccRefPopupAccRefID').val('0');
                        $(elem).closest('tr').find('.txtAccRefPopupAccRef').val('');
                        $(elem).closest('tr').find('.txtAccRefPopupAccRefName').val('');

                        return false;
                    }


                    if (ui.item.id == 0) {
                        event.preventDefault();
                        $(elem).val('');
                        $(elem).closest('tr').find('.hdnAccRefPopupCategoryID').val('0');

                        $(elem).closest('tr').find('.hdnAccRefPopupAccRefID').val('0');
                        $(elem).closest('tr').find('.txtAccRefPopupAccRef').val('');
                        $(elem).closest('tr').find('.txtAccRefPopupAccRefName').val('');

                        //ClearAccRefPopup(elem);
                    }
                    else {
                        $(elem).val(ui.item.code);
                        $(elem).closest('tr').find('.hdnAccRefPopupCategoryID').val(ui.item.id);
                        $(elem).closest('tr').find('.txtAccRefPopupAccRef').focus();

                    }
                    return false;
                }
            });


            $(elem).blur(function () {
                var self = this;
                elemID = $(elem).attr('id');
                ccCode = $(elem).val();

                if (ccCode == '') {
                    // ClearAccRefPopup(elem);
                    $(elem).val('');
                    $(elem).closest('tr').find('.hdnAccRefPopupCategoryID').val('0');

                    $(elem).closest('tr').find('.hdnAccRefPopupAccRefID').val('0');
                    $(elem).closest('tr').find('.txtAccRefPopupAccRef').val('');
                    $(elem).closest('tr').find('.txtAccRefPopupAccRefName').val('');
                }
                else {

                }

                ccID = $(self).closest('tr').find('.hdnAccRefPopupCategoryID').val();
                if (ccID == '0' | ccID == '') {
                    $(self).addClass('textError');
                }
            });

            $(elem).focus(function () {
                var self = this;
                $(self).removeClass('textError');
                //$(elem).removeClass('fldDataError');
            });

        }

        function bindInsPopup() {

            $('#' + dvPopupIns).dialog({
                title: "Instrument Allocation",
                autoOpen: false,
                resizable: false,
                modal: true,
                position: 'center',
                closeOnEscape: true,
                top: 0,
                left: 0,
                width: 500,
                height: 300,
                open: function (event, ui) {
                    //self.$popupDialog.parent().appendTo(jQuery("form:first"));
                    $('#' + curTxtInsCodeID).closest('table').closest('tr').addClass('gridSelectedRow');
                    //$(ui.element).closest('table').closest('tr').addClass('gridSelectedRow');
                },
                close: function (event, ui) {
                    $('#' + curTxtInsCodeID).closest('table').closest('tr').removeClass('gridSelectedRow');
                }
            });

            $('#' + dvPopupIns).find('.btnPopupInsOK').click(function (e) {
                //alert('OK CLicked');
                InsPopupOKClicked();
                $('#' + dvPopupIns).dialog('close');

            });

            $('#' + dvPopupIns).find('.btnPopupInsCancel').click(function (e) {
                //alet('OK CLicked');
                $('#' + dvPopupIns).dialog('close');

            });

            $('#' + dvPopupIns).find('.btnPopupInsNewRow').click(function (e) {
                //alet('OK CLicked');
                var table = $('#' + dvPopupIns).find('.tblIns');
                AddNewRowToInsList(table, null);

            });

        }


        function bindInsPopupListByRow(rowElem) {
            var cgColumns = [{ 'columnName': 'insno', 'width': '120', 'align': 'left', 'highlight': 2, 'label': 'Ins. No' }
                             , { 'columnName': 'insdate', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Ins. Date' }
                             , { 'columnName': 'bankbranchname', 'width': '120', 'align': 'left', 'highlight': 0, 'label': 'Bank,Branch' }
                             , { 'columnName': 'insamt', 'width': '80', 'align': 'right', 'highlight': 0, 'label': 'Ins. Amt' }
                             , { 'columnName': 'remainamt', 'width': '80', 'align': 'right', 'highlight': 0, 'label': 'Remain Amt' }
                            ];

            //        var serviceURL = InstrumentGetServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            //        serviceURL += "&namecomptype=" + Enums.DataCompareType.Contains + "&ispaging=1";

            var companyid = $('#' + hdnCompanyID).val();

            var serviceURL = InstrumentGetServiceLink + "?isterm=1&includeempty=0&iscodename=0&insnocomptype=" + Enums.DataCompareType.StartsWith;
            serviceURL += "&ispaging=1";
            serviceURL += "&companyid=" + companyid;



            $(rowElem).find('.txtInsPopupIns').each(function (index, elem) {
                $(elem).closest('tr').find('.btnInsPopupInsAC').click(function (e) {
                    $(elem).combogrid("dropdownClick");
                });

                $(elem).closest('tr.rowIns').find('.btnInsPopupDelete').click(function (e) {
                    //elmID = $(elem).attr('id');

                    var elemRow = $(elem).closest('tr.rowIns');

                    ccID = JSUtility.GetNumber($(elemRow).find('.hdnInsPopupInsID').val());
                    //amt = JSUtility.GetNumber($(elemRow).find('.txtAccRefPopupAmount').val());


                    //                if (ccID > 0 || amt > 0) {
                    //                    if (!confirm('Are your sure to delete?')) {
                    //                        return;
                    //                    }
                    //                }

                    $(elemRow).find('.hdnRecordState').val(Enums.RecordState.Deleted);
                    $(elemRow).hide();
                    RefreshInsListRowNumber();

                });

                $(elem).closest('tr.rowIns').find('.btnInsPopupInsDet').click(function (e) {
                    //elmID = $(elem).attr('id');

                    ShowPopupInsDetails(elem);
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
                    width: 550,
                    url: serviceURL,
                    search: function (event, ui) {
                        var newServiceURL = JSUtility.AddTimeToQueryString(serviceURL);
                        $(this).combogrid("option", "url", newServiceURL);
                    },
                    //"select item" event handler to set input field
                    search: function (event, ui) {
                        var elemRow = $(this).closest('tr.rowIns');

                        //var insModeID = $(elemRow).find('.hdnInsPopupInsModeID').val();
                        var insModeID = $('#' + dvPopupIns).find('.hdnPopupInsModeIDParent').val();
                        var insTypeID = $(elemRow).find('.ddlInsPopupInsType').val();
                        var newServiceURL = serviceURL + "&instypeid=" + insTypeID + "&insmodeid=" + insModeID;
                        $(this).combogrid("option", "url", newServiceURL);


                        //alert($(ui.element).attr('class'));
                        // alert('search');
                    },
                    select: function (event, ui) {
                        //alert(ui.item.typename);
                        //$(".txtComboGrid").val(ui.item.code);
                        elemID = $(elem).attr('id');

                        if (!ui.item) {
                            event.preventDefault();
                            ClearInsPopup(elemID);
                            return false;
                            //ClearGLAccountData(elemID);
                        }
                        
                        if (ui.item.insid == 0) {
                            event.preventDefault();
                            ClearInsPopup(elem);
                        }
                        else {
                            SetInsDataPopup(elem, ui.item);
                        }
                        return false;
                    }
                });

                $(elem).blur(function () {
                    var self = this;
                    elemID = $(elem).attr('id');
                    var insNo = $(self).val();

                    if (insNo == '') {
                        ClearInsPopup(self);
                    }
                    else {

                    }

                    var insID = $(self).closest('tr.rowIns').find('.hdnInsPopupInsID').val();
                    if (insID == '0' | insID == '') {
                        $(self).addClass('textError');
                    }
                });

                $(elem).focus(function () {
                    var self = this;
                    $(self).removeClass('textError');
                    //$(elem).removeClass('fldDataError');
                });

            });


            $(rowElem).find('.ddlInsPopupInsType').change(function (e) {
                //alert('OK CLicked');
                $(rowElem).find('.hdnInsPopupInsID').val('0');
                $(rowElem).find('.txtInsPopupIns').val('');
                $(rowElem).find('.txtInsPopupAmount').val('');

            });
        }


        function bindInsDetailsPopup() {
            $('#' + dvPopupInsDetails).dialog({
                title: "Instrument Details",
                autoOpen: false,
                resizable: false,
                modal: true,
                position: 'center',
                closeOnEscape: true,
                top: 0,
                left: 0,
                width: 440,
                //height: 300,
                height: 'auto',
                open: function (event, ui) {
                    //self.$popupDialog.parent().appendTo(jQuery("form:first"));
                    $(curTxtInsDetailsCodeElem).closest('tr.rowIns').addClass('gridSelectedRow');
                    //$(ui.element).closest('table').closest('tr').addClass('gridSelectedRow');
                },
                close: function (event, ui) {
                    $(curTxtInsDetailsCodeElem).closest('tr.rowIns').removeClass('gridSelectedRow');
                    curTxtInsDetailsCodeElem = null;
                }
            });


            $('#' + dvPopupInsDetails).find('.btnPopupInsDetailsOK').click(function (e) {
                //alert('OK CLicked');
                if (InsDetailsPopupOKClicked()) {
                    $('#' + dvPopupInsDetails).dialog('close');
                }

            });

            $('#' + dvPopupInsDetails).find('.btnPopupInsDetialsCancel').click(function (e) {
                //alet('OK CLicked');
                $('#' + dvPopupInsDetails).dialog('close');
            });



            $('#' + dvPopupInsDetails).find('.txtInsDetInsNo').keydown(function (e) {
                if (e.keyCode == 13) {
                    CheckInstrumentNo();
                }
            });

            $('#' + dvPopupInsDetails).find('.txtInsDetInsNo').blur(function (e) {
                CheckInstrumentNo();
            });


            $('#' + dvPopupInsDetails).find('.btnPopupInsDetailsNew').click(function (e) {
                ClearInsPopupDetails();
                $('#' + dvPopupInsDetails).find('.txtInsDetInsNo').prop('disabled', false);
                $('#' + dvPopupInsDetails).find('.ddlInsDetInsType').prop('disabled', false);
                $('#' + dvPopupInsDetails).find('.txtInsDetInsNo').focus();
            });





        }

        function CheckInstrumentNo() {
            var insNo = $('#' + dvPopupInsDetails).find('.txtInsDetInsNo').val();
            var insTypeID = $('#' + dvPopupInsDetails).find('.ddlInsDetInsType').val();
            var insModeID = $('#' + dvPopupInsDetails).find('.hdnInsDetInsModeID').val();

            if (insNo.trim() != '') {
                var ins = GetInstrument(0, insNo, insModeID, insTypeID);
                if (ins != null) {
                    SetInsPopupDetailsData(ins);
                }
            }
        }


        function FillDefaultAccRefCategory(txtRefTypeCodeID, accRefTypeID)
        {
            var isEnabled = $('#' + txtRefTypeCodeID).is(':enabled');
            var gridRow = $('#' + txtRefTypeCodeID).closest('tr.gridRow');
            //var glGroupClassID = parseInt($(gridRow).find('input[id$="hdnGLGroupClassID"]').val());

            var glAccountID = parseInt($(gridRow).find('input[id$="hdnGLAccountID"]').val());


            var detID = parseInt($('#' + txtRefTypeCodeID).closest('table').closest('tr').find('input[id$="hdnJournalDetID"]').val())
            var detID_Link = parseInt($('#' + txtRefTypeCodeID).closest('table').closest('tr').find('input[id$="hdnJournalDetID_Link"]').val());
            var drcr = parseInt($('#' + txtRefTypeCodeID).closest('table').closest('tr').find('input[id$="hdnDrCr"]').val())



            if (isEnabled && glAccountID > 0)
            {
                var catList = GetGLAccRefCategoryList(glAccountID, accRefTypeID);
                if (catList != null )
                {
                    //alert('data found');
                    //var newRow = AddNewRowToAccRefList(table, accRefTypeID, rowItem);

                    var table = $('#' + dvPopupAccRef).find('.tblAccRef');
                    //$(table).empty();

                    var rowSLNo = 0;

                    for (var i = 0; i < catList.length; i++) {
                        var catItem = catList[i];
                        //alert(rowItem.journaldetid);
                        rowSLNo++;
                        var ccItem = {
                                        id: 0, code: '', name: '', drcr: drcr, amt: 0
                                        , typeid: catItem.typeid, slno: rowSLNo
                                        , categoryid: catItem.id, categorycode: catItem.code, categoryname: catItem.categoryname
                                        , _recordstateint: Enums.RecordState.Added, linkid: detID_Link
                                        , journaldetid: detID, journaldetrefid: 0
                                        };

                        var newRow = AddNewRowToAccRefList(table, accRefTypeID, ccItem);
                        //if (rowItem._recordstateint != Enums.RecordState.Deleted) {
                        //    rowSLNo++;
                        //    $(newRow).find('.spnAccRefPopupSLNo').text(rowSLNo);
                        //}
                        newRow = null;
                    }

                }
                else
                {
                    //alert('no data');
                }
            }
        }


        function ShowPopupAccRef(txtRefTypeCodeID, accRefTypeID) {
            curTxtAccRefCodeID = txtRefTypeCodeID;
            curAccRefTypeID = accRefTypeID;

            listTitle = 'Amount Allocation';
            colHeaderCode = 'Code';
            switch (accRefTypeID) {
                case Enums.AccRefType.TranCode:
                    listTitle = 'Tran. Code Allocation';
                    colHeaderCode = 'Tran. Clde';
                    break;
                case Enums.AccRefType.CostCenter:
                    listTitle = 'Cost Center Allocation';
                    colHeaderCode = 'Cost Center';
                    break;
                case Enums.AccRefType.Reference:
                    listTitle = 'Reference Allocation';
                    colHeaderCode = 'Reference';
                    break;

            }



            var isEnabled = $('#' + curTxtAccRefCodeID).is(':enabled');

            FillAccRefListFromJSon(curTxtAccRefCodeID, accRefTypeID, isEnabled);

            //FillDefaultAccRefCategory(curTxtAccRefCodeID, accRefTypeID, isEnabled);

            if (isEnabled) {
                $('#' + dvPopupAccRef).find('.btnPopupAccRefOK').prop('disabled', false);
                $('#' + dvPopupAccRef).find('.btnPopupAccRefNewRow').prop('disabled', false);

                $('#' + dvPopupAccRef).find('.txtAccRefPopupCategory').prop('disabled', false);
                $('#' + dvPopupAccRef).find('.btnAccRefPopupCategoryAC').prop('disabled', false);

                $('#' + dvPopupAccRef).find('.txtAccRefPopupAccRef').prop('disabled', false);
                $('#' + dvPopupAccRef).find('.btnAccRefPopupAccRefAC').prop('disabled', false);
                $('#' + dvPopupAccRef).find('.txtAccRefPopupAccRefName').prop('disabled', false);

                $('#' + dvPopupAccRef).find('.txtAccRefPopupAmount').prop('disabled', false);


                $('#' + dvPopupAccRef).find('.btnAccRefPopupDelete').prop('disabled', false);


            }
            else {
                $('#' + dvPopupAccRef).find('.btnPopupAccRefOK').prop('disabled', true);
                $('#' + dvPopupAccRef).find('.btnPopupAccRefNewRow').prop('disabled', true);

                $('#' + dvPopupAccRef).find('.txtAccRefPopupCategory').prop('disabled', true);
                $('#' + dvPopupAccRef).find('.btnAccRefPopupCategoryAC').prop('disabled', true)

                $('#' + dvPopupAccRef).find('.txtAccRefPopupAccRef').prop('disabled', true);
                $('#' + dvPopupAccRef).find('.btnAccRefPopupAccRefAC').prop('disabled', true);
                $('#' + dvPopupAccRef).find('.txtAccRefPopupAccRefName').prop('disabled', true);
               
                $('#' + dvPopupAccRef).find('.txtAccRefPopupAmount').prop('disabled', true);




                $('#' + dvPopupAccRef).find('.btnAccRefPopupDelete').prop('disabled', true);
            }
            $('#' + dvPopupAccRef).find('.colHeaderCode').text(colHeaderCode);


            $('#' + dvPopupAccRef).dialog("option", 'title', listTitle);
            $('#' + dvPopupAccRef).dialog('open');
            // RefreshCostCenterListRowNumber();
            //$('#' + txtCostCenterCodeID).closest('table').closest('tr').addClass('gridSelectedRow');

        }

        function GetDetRowInsMode(drCr) {
            var insModeID = Enums.InstrumentMode.Receive;
            if (drCr == Enums.DrCr.Debit) {
                insModeID = Enums.InstrumentMode.Receive;
            }
            if (drCr == Enums.DrCr.Credit) {
                insModeID = Enums.InstrumentMode.Issue;
            }
            return insModeID;
        }

        function ShowPopupIns(txtInsCodeID) {
            curTxtInsCodeID = txtInsCodeID;

            var isEnabled = $('#' + curTxtInsCodeID).is(':enabled');

            var gridRow = $('#' + curTxtInsCodeID).closest('tr.gridRow');
            var glGroupClassID = parseInt($(gridRow).find('input[id$="hdnGLGroupClassID"]').val());
            var isInstrument = $(gridRow).find('input[id$="hdnIsInstrument"]').val();
            var drCr = parseInt($(gridRow).find('input[id$="hdnDrCr"]').val());



            var insModeID = GetDetRowInsMode(drCr);
            $('#' + dvPopupIns).find('.hdnPopupInsModeIDParent').val(insModeID);

            FillInsListFromJSon(curTxtInsCodeID, isEnabled);


            if (isEnabled) {
                $('#' + dvPopupIns).find('.btnPopupInsOK').prop('disabled', false);
                $('#' + dvPopupIns).find('.btnPopupInsNewRow').prop('disabled', false);

                $('#' + dvPopupIns).find('.ddlInsPopupInsType').prop('disabled', false);
                $('#' + dvPopupIns).find('.txtInsPopupIns').prop('disabled', false);
                $('#' + dvPopupIns).find('.btnInsPopupInsAC').prop('disabled', false);
                $('#' + dvPopupIns).find('.txtInsPopupAmount').prop('disabled', false);
                $('#' + dvPopupIns).find('.btnInsPopupDelete').prop('disabled', false);
            }
            else {
                $('#' + dvPopupIns).find('.btnPopupInsOK').prop('disabled', true);
                $('#' + dvPopupIns).find('.btnPopupInsNewRow').prop('disabled', true);

                $('#' + dvPopupIns).find('.ddlInsPopupInsType').prop('disabled', true);
                $('#' + dvPopupIns).find('.txtInsPopupIns').prop('disabled', true);
                $('#' + dvPopupIns).find('.btnInsPopupInsAC').prop('disabled', true);
                $('#' + dvPopupIns).find('.txtInsPopupAmount').prop('disabled', true);

                $('#' + dvPopupIns).find('.btnInsPopupDelete').prop('disabled', true);
            }

            var strTitle = insModeID == Enums.InstrumentMode.Issue ? "Instrument Allocation - Issue" : "Instrument Allocation - Receive";

            $('#' + dvPopupIns).dialog('option', 'title', strTitle);
            $('#' + dvPopupIns).dialog('open');
            // RefreshCostCenterListRowNumber();
            //$('#' + txtCostCenterCodeID).closest('table').closest('tr').addClass('gridSelectedRow');

        }

        function ShowPopupInsDetails(txtInsDetailsCodeElem) {
            curTxtInsDetailsCodeElem = txtInsDetailsCodeElem;

            var isEnabled = $(txtInsDetailsCodeElem).is(':enabled');



            var insModeID = $('#' + dvPopupIns).find('.hdnPopupInsModeIDParent').val();


            var insID = parseInt($(txtInsDetailsCodeElem).closest('tr.rowIns').find('.hdnInsPopupInsID').val());
            if (insID > 0) {
                SetInsPopupDetails(insID);
            }
            else {
                ClearInsPopupDetails();
                var insNo = $(txtInsDetailsCodeElem).val();
                var insTypeID = $(txtInsDetailsCodeElem).closest('tr.rowIns').find('.ddlInsPopupInsType').val()

                $('#' + dvPopupInsDetails).find('.ddlInsDetInsType').val(insTypeID);
                $('#' + dvPopupInsDetails).find('.txtInsDetInsNo').val(insNo);
                $('#' + dvPopupInsDetails).find('.hdnInsPopupInsModeID').val(insModeID);
                $('#' + dvPopupInsDetails).find('.hdnInsPopupInsID').val(0);
            }


            if (isEnabled) {
                $('#' + dvPopupInsDetails).find('.btnPopupInsDetialsOK').prop('disabled', false);

                $('#' + dvPopupInsDetails).find('.txtInsDetInsDate').prop('disabled', false);
                $('#' + dvPopupInsDetails).find('.txtInsDetIssueName').prop('disabled', false);
                $('#' + dvPopupInsDetails).find('.txtInsDetBankName').prop('disabled', false);
                $('#' + dvPopupInsDetails).find('.txtInsDetBranchName').prop('disabled', false);
                $('#' + dvPopupInsDetails).find('.txtInsDetInsAmt').prop('disabled', false);

                $('#' + dvPopupInsDetails).find('.ddlInsDetInsStatus').prop('disabled', false);
                $('#' + dvPopupInsDetails).find('.txtInsDetInsRemarks').prop('disabled', false);

                $('#' + dvPopupInsDetails).find('.txtInsDetInsDate').datepicker('enable');

                if (insID > 0) {
                    $('#' + dvPopupInsDetails).find('.ddlInsDetInsType').prop('disabled', true);
                    $('#' + dvPopupInsDetails).find('.txtInsDetInsNo').prop('disabled', true);
                }
                else {
                    $('#' + dvPopupInsDetails).find('.ddlInsDetInsType').prop('disabled', false);
                    $('#' + dvPopupInsDetails).find('.txtInsDetInsNo').prop('disabled', false);
                }


            }
            else {
                $('#' + dvPopupInsDetails).find('.btnPopupInsDetailsOK').prop('disabled', true);
                $('#' + dvPopupInsDetails).find('.ddlInsDetInsType').prop('disabled', true);
                $('#' + dvPopupInsDetails).find('.txtInsDetInsNo').prop('disabled', true);
                $('#' + dvPopupInsDetails).find('.txtInsDetInsDate').prop('disabled', true);
                $('#' + dvPopupInsDetails).find('.txtInsDetIssueName').prop('disabled', true);
                $('#' + dvPopupInsDetails).find('.txtInsDetBankName').prop('disabled', true);
                $('#' + dvPopupInsDetails).find('.txtInsDetBranchName').prop('disabled', true);
                $('#' + dvPopupInsDetails).find('.txtInsDetInsAmt').prop('disabled', true);

                $('#' + dvPopupInsDetails).find('.ddlInsDetInsStatus').prop('disabled', true);
                $('#' + dvPopupInsDetails).find('.txtInsDetInsRemarks').prop('disabled', true);

                $('#' + dvPopupInsDetails).find('.txtInsDetInsDate').datepicker('disable');
            }

            var strTitle = '';

            if (insModeID == Enums.InstrumentMode.Issue) {
                strTitle = "Instrument Details - Issue";
                $('#' + dvPopupInsDetails).find('.rowInsDetBankName').hide();
                $('#' + dvPopupInsDetails).find('.rowInsDetBranchName').hide();

                $('#' + dvPopupInsDetails).find('.lblInsPopupIssueName').text("Issue To");
            }
            else {
                strTitle = "Instrument Details - Receive";
                $('#' + dvPopupInsDetails).find('.rowInsDetBankName').show();
                $('#' + dvPopupInsDetails).find('.rowInsDetBranchName').show();

                $('#' + dvPopupInsDetails).find('.lblInsPopupIssueName').text("Receive From");
            }

            $('#' + dvPopupInsDetails).dialog('option', 'title', strTitle);
            $('#' + dvPopupInsDetails).dialog('open');

        }

        function SetInsPopupDetails(insID) {
            if (insID > 0) {
                var insObj = GetInstrument(insID);
                if (insObj != null) {
                    SetInsPopupDetailsData(insObj);

                }
                else {
                    ClearInsPopupDetails();
                }
            }
            else {
                ClearInsPopupDetails();
            }
        }

        function SetInsPopupDetailsData(insObj) {
            insObj = insObj || null;

            var isEnabled = $(curTxtInsDetailsCodeElem).is(':enabled');

            if (insObj != null) {
                $('#' + dvPopupInsDetails).find('.ddlInsDetInsType').val(insObj.instypeid);
                $('#' + dvPopupInsDetails).find('.hdnInsDetInsModeID').val(insObj.insmodeid);
                $('#' + dvPopupInsDetails).find('.txtInsDetInsNo').val(insObj.insno);
                $('#' + dvPopupInsDetails).find('.hdnInsDetInsID').val(insObj.insid);
                $('#' + dvPopupInsDetails).find('.txtInsDetInsDate').val(insObj.insdate);
                $('#' + dvPopupInsDetails).find('.txtInsDetIssueName').val(insObj.issuename);
                $('#' + dvPopupInsDetails).find('.txtInsDetBankName').val(insObj.bankname);
                $('#' + dvPopupInsDetails).find('.txtInsDetBranchName').val(insObj.branchname);

                //JSUtility.FormatNumber(strData, pDecimalPlaces, pFormatStyle);
                $('#' + dvPopupInsDetails).find('.txtInsDetInsAmt').val(JSUtility.FormatNumber(insObj.insamt.toString(), 2));

                $('#' + dvPopupInsDetails).find('.ddlInsDetInsStatus').val(insObj.insstatusid);
                $('#' + dvPopupInsDetails).find('.txtInsDetInsStatusDate').val(insObj.insstatusdate);

                $('#' + dvPopupInsDetails).find('.txtInsDetInsRemarks').val(insObj.remarks);

                if (isEnabled) {
                    $('#' + dvPopupInsDetails).find('.ddlInsDetInsType').prop('disabled', true);
                    $('#' + dvPopupInsDetails).find('.txtInsDetInsNo').prop('disabled', true);
                }
            }
            else {
                ClearInsPopupDetails();
                if (isEnabled) {
                    $('#' + dvPopupInsDetails).find('.ddlInsDetInsType').prop('disabled', false);
                    $('#' + dvPopupInsDetails).find('.txtInsDetInsNo').prop('disabled', false);
                }
            }
        }


        function ClearInsPopupDetails() {
            $('#' + dvPopupInsDetails).find('.ddlInsDetInsType').val(Enums.InstruemntType.Cheque);
            //$('#' + dvPopupInsDetails).find('.hdnInsDetInsModeID').val(insObj.insmodeid);
            $('#' + dvPopupInsDetails).find('.txtInsDetInsNo').val('');
            $('#' + dvPopupInsDetails).find('.hdnInsDetInsID').val(0);
            $('#' + dvPopupInsDetails).find('.txtInsDetInsDate').val('');
            $('#' + dvPopupInsDetails).find('.txtInsDetIssueName').val('');
            $('#' + dvPopupInsDetails).find('.txtInsDetBankName').val('');
            $('#' + dvPopupInsDetails).find('.txtInsDetBranchName').val('');
            $('#' + dvPopupInsDetails).find('.txtInsDetInsAmt').val('');

            $('#' + dvPopupInsDetails).find('.txtInsDetInsStatusDate').val('');

            $('#' + dvPopupInsDetails).find('.txtInsDetInsRemarks').val('');
        }

        function GridNavAccRef(rowElem) {
            $(rowElem).find('.textGridNav').keydown(function (e) {
                var cCol = $(this).closest('td');
                var cRow = $(this).closest('tr');

                var x = $(cCol).index();
                var y = $(cRow).index();

                var isNav = false;
                var newRow = null;
                var row = null;

                if (e.keyCode == 37) {
                    x--;
                }
                if (e.keyCode == 38) {
                    y--;
                    isNav = true;
                    row = $(cRow).prev(':visible');
                    if ($(row).length == 0) {
                        row = $(cRow).prevUntil(':visible').first().prev();
                    }

                    if ($(row).length > 0) {
                        newRow = row;
                    }
                    $(newRow).find('td').eq(x).find('.textGridNav').focus();
                }

                if (e.keyCode == 39) {
                    x++;
                }

                if (e.keyCode == 40) {
                    y++;
                    isNav = true;
                    row = $(cRow).next(':visible');
                    if ($(row).length == 0) {
                        row = $(cRow).nextUntil(':visible').last().next();
                    }

                    if ($(row).length > 0) {
                        newRow = row;
                    }
                    $(newRow).find('td').eq(x).find('.textGridNav').focus();
                }
            });
        }

        function FillAccRefListFromJSon(txtRefTypeCodeID, accRefTypeID, includeOneEmptyRow) {

            //var jSonString = $('#' + hdnJournalDetRefJson).val();

            var jSonString = $('#' + txtRefTypeCodeID).closest('div.dvGrid').find('input.hdnRefJson').val();

            var detRefList = $.parseJSON(jSonString);

            var detID = 0;
            var detID_Link = 0;


            detID = $('#' + txtRefTypeCodeID).closest('table').closest('tr').find('input[id$="hdnJournalDetID"]').val();
            detID_Link = parseInt($('#' + txtRefTypeCodeID).closest('table').closest('tr').find('input[id$="hdnJournalDetID_Link"]').val());



            //alert(detID);

            var detRefListLink = $.grep(detRefList, function (n, i) {
                //return (n.linkid == detID_Link);
                return (n.linkid == detID_Link && n.typeid == accRefTypeID);
            });

            var table = $('#' + dvPopupAccRef).find('.tblAccRef');
            $(table).empty();

            var rowSLNo = 0;
            for (var i = 0; i < detRefListLink.length; i++) {
                var rowItem = detRefListLink[i];
                //alert(rowItem.journaldetid);

                var newRow = AddNewRowToAccRefList(table, accRefTypeID, rowItem);
                if (rowItem._recordstateint != Enums.RecordState.Deleted) {
                    rowSLNo++;
                    $(newRow).find('.spnAccRefPopupSLNo').text(rowSLNo);
                }
            }

            if (rowSLNo == 0)
            {
                var gridRow = $('#' + txtRefTypeCodeID).closest('tr.gridRow');
                var glAccountID = parseInt($(gridRow).find('input[id$="hdnGLAccountID"]').val());

                if (glAccountID > 0){
                    FillDefaultAccRefCategory(txtRefTypeCodeID, accRefTypeID);
                }
            }

            if (includeOneEmptyRow) {
                //var remainAmt = GetAccRefRemainAmount();
                var newRowE = AddNewRowToAccRefList(table, accRefTypeID, null);
                rowSLNo++;
                $(newRowE).find('.spnAccRefPopupSLNo').text(rowSLNo);

                //$(newRowE).find('.txtAccRefPopupAmount').val(remainAmt);
                newRowE = null;
            }

        }

        function AddNewRowToAccRefList(tblAccRef, accRefTypeID, rowData) {
            //var rowCount = $('table tr').length;
            var rowCount = $(tblAccRef).find('tr:visible').length;

            var templateRow = $('.trAccRefTemplate:first').clone();
            templateRow.removeClass('trAccRefTemplate');
            var newRow = templateRow.appendTo(tblAccRef);

            templateRow = null;

            var RowNum = $(newRow).index() + 1;

            if (RowNum % 2 == 0) {
                $(newRow).addClass('tableRowEven');
            }
            else {
                $(newRow).addClass('tableRowOdd');
            }
            //$(tblCostCenter).find('tr:visible:even').addClass('tableRowOdd');  //based on 0 based index

            if (!rowData) {
                rowData = null;
            }

            var rowSLNo = rowCount + 1;
            //rownumber
            //$(newRow).find('.spnCostCenterPopupSLNo').val(RowNum);
            $(newRow).find('.spnAccRefPopupSLNo').text(rowSLNo);
            if (rowData != null) {
                $(newRow).find('.hdnAccRefPopupLinkID').val(rowData.linkid);
                $(newRow).find('.hdnAccRefPopupJournalDetID').val(rowData.journaldetid);
                $(newRow).find('.hdnAccRefPopupJournalDetRefID').val(rowData.journaldetrefid);


                $(newRow).find('.hdnAccRefPopupCategoryID').val(rowData.categoryid)
                $(newRow).find('.txtAccRefPopupCategory').val(rowData.categorycode);


                $(newRow).find('.hdnRecordState').val(rowData._recordstateint);

                $(newRow).find('.txtAccRefPopupAccRef').val(rowData.code);
                $(newRow).find('.hdnAccRefPopupAccRefID').val(rowData.id);

                $(newRow).find('.txtAccRefPopupAccRefName').val(rowData.name);



                $(newRow).find('.txtAccRefPopupAmount').val(rowData.amt);
            }


            //RefreshCostCenterListRowNumber();
            GridNavAccRef(newRow);
            ContentForm.InitDefualtFeatureInScopeElement(newRow);
            //        ContentForm.TextNumberFormatScopeElement(newRow);
            //        ContentForm.TextDecimalFormatScopeElement(newRow);
            bindAccRefPopupListByRow(newRow, accRefTypeID);

            if (rowData != null) {
                if (rowData._recordstateint == Enums.RecordState.Deleted) {
                    $(newRow).hide();
                }
            }


            return newRow;
        }

        function GetAccRefRemainAmount(curtxtAccRefCodeID, pCategoryID) {
            //        totalAmount = totalAmount || 0;
            //        linkID = linkID || 0;
            //        accrefTypeID = accrefTypeID || 0;
            //        drCr = drCr || 0;

            pCategoryID = pCategoryID || 0;


            var totalAmount = JSUtility.GetNumber($('#' + curtxtAccRefCodeID).closest('tr.gridRow').find('input.txtAmount').val());



            if (totalAmount <= 0) {
                return 0;
            }




            var table = $('#' + dvPopupAccRef).find('.tblAccRef');

            //        var numOfVisibleRows = $('tr').filter(function () {
            //            return $(this).css('display') !== 'none';
            //        }).length;


            //var curRows = $(table).find('tr:visible');
            var curRows = $(table).find('tr').filter(function () {
                return $(this).css('display') !== 'none';
            });

            var curTotAmt = 0;
            for (var i = 0; i < curRows.length; i++) {
                var catid = parseInt($(curRows[i]).find('.hdnAccRefPopupCategoryID').val());

                if (pCategoryID > 0) {
                    if (pCategoryID != catid) {
                        continue;
                    }
                }

                var amt = JSUtility.GetNumber($(curRows[i]).find('.txtAccRefPopupAmount').val());
                curTotAmt += amt;
            }
            curRows = null;

            var remainAmt = totalAmount - curTotAmt;
            remainAmt = remainAmt < 0 ? 0 : remainAmt;

            return remainAmt;


            //GetJsonListSummary(
        }


        function RefreshAccRefListRowNumber() {
            var table = $('#' + dvPopupAccRef).find('.tblAccRef');
            $(table).find('tr:visible').each(function (idx, elem) {
                $(elem).find('.spnAccRefPopupSLNo').text(idx + 1);
            });
        }
        function UpdateAccRefListToJson(txtAccRefCodeID, accRefTypeID) {
            //var jSonString = $('#' + hdnJournalDetRefJson).val();

            var jSonString = $('#' + txtAccRefCodeID).closest('div.dvGrid').find('input.hdnRefJson').val();


            var detRefList = $.parseJSON(jSonString);

            var detID = parseInt($('#' + txtAccRefCodeID).closest('table').closest('tr').find('input[id$="hdnJournalDetID"]').val())
            var detID_Link = parseInt($('#' + txtAccRefCodeID).closest('table').closest('tr').find('input[id$="hdnJournalDetID_Link"]').val());

            //var drcr = $(elemRow).find('select[id$="hdnDrCr"]');
            var drcr = parseInt($('#' + txtAccRefCodeID).closest('table').closest('tr').find('input[id$="hdnDrCr"]').val())

            

            //filter out by current link and type
            for (i = 0; i < detRefList.length; ++i) {
                if (detRefList[i].linkid == detID_Link && detRefList[i].typeid == accRefTypeID) {
                    detRefList.splice(i--, 1);
                }
            }

            var table = $('#' + dvPopupAccRef).find('.tblAccRef');

            //var detRefListLinkNew = [];

            var slno = 0;
            $(table).find('tr').each(function (idx, rowElem) {
                //            var linkid = parseInt($(rowElem).find('.hdnCostCenterPopupLinkID').val());
                //            var journaldetid = parseInt($(rowElem).find('.hdnCostCenterPopupJournalDetID').val());

                var linkid = detID_Link;
                var journaldetid = detID;

                var journaldetrefid = parseInt($(rowElem).find('.hdnAccRefPopupJournalDetRefID').val());
                var recordstateRow = $(rowElem).find('.hdnRecordState').val();

                var id = parseInt($(rowElem).find('.hdnAccRefPopupAccRefID').val());
                var code = $(rowElem).find('.txtAccRefPopupAccRef').val();
                var name = $(rowElem).find('.txtAccRefPopupAccRefName').val();
                var categoryid = parseInt($(rowElem).find('.hdnAccRefPopupCategoryID').val());
                var categorycode = $(rowElem).find('.txtAccRefPopupCategory').val();
                var categoryname = $(rowElem).find('.txtAccRefPopupCategory').val();
                var amt = JSUtility.GetNumber($(rowElem).find('.txtAccRefPopupAmount').val());

                var typeid = accRefTypeID;

                var isToList = false;

                var recordstateint = Enums.RecordState.Edited;
                if (recordstateRow == Enums.RecordState.Deleted) {
                    if (journaldetrefid > 0) {
                        recordstateint = Enums.RecordState.Deleted;
                        isToList = true;
                    }
                } // rowstate deleted
                else {
                    if (id > 0) {
                        if (journaldetrefid > 0) {
                            recordstateint = Enums.RecordState.Edited;
                        }
                        else {
                            recordstateint = Enums.RecordState.Added;
                        }
                        isToList = true;
                        slno++;
                    }
                    else {
                        if (journaldetrefid > 0) {
                            recordstateint = Enums.RecordState.Deleted;
                            isToList = true;
                        }
                    }
                }

                if (isToList) {

                    var ccItem = { id: id, code: code, name: name, drcr: drcr, amt: amt
                                , typeid: typeid, slno: slno
                                , categoryid: categoryid, categorycode: categorycode, categoryname: categoryname
                                , _recordstateint: recordstateint, linkid: linkid
                                , journaldetid: journaldetid, journaldetrefid: journaldetrefid
                    };

                    detRefList.push(ccItem);
                }
            });
            return detRefList;

        }

        function SortAccRefData(jsonList) {
            //        jsonList.sort(function (a, b) {
            //            return a.sort1 - b.sort1 || a.sort2 - b.sort2;
            //        });

            //                jsonList.sort(function (a, b) {
            //                    return a.sort1 - b.sort1 || -(a.sort2 - b.sort2);   //sort 2 descending
            //                });

            //sort by: link,type,category,entry slno
            jsonList.sort(function (a, b) {
                return a.linkid - b.linkid || a.typeid - b.typeid || a.categoryid - b.categoryid || a.slno - b.slno;
            });

            //example 2
            //sort name ascending then id descending
            //        function cmp(x, y) { // generic comparison function
            //            return x > y ? 1 : x < y ? -1 : 0;
            //        }
            //        arr.sort(function (a, b) {
            //            //note the minus before -cmp, for descending order
            //            return cmp(
            //            [cmp(a.name, b.name), -cmp(a.id, b.id)],
            //            [cmp(b.name, a.name), -cmp(b.id, a.id)]
            //        );
            //        });




            return jsonList;
        }

        function UpdateAccRefJsonToText(jsonData, pDrCr) {
            pDrCr = pDrCr || Enums.DrCr.Debit;

            jsonData = SortAccRefData(jsonData);
            var jsonText = JSUtility.JSONStringify(jsonData);

            if (pDrCr == Enums.DrCr.Debit) {
                $('#' + hdnJournalDetRefJson).val(jsonText);
            }
            else {
                $('#' + hdnJournalDetRefJson2).val(jsonText);
            }


            //$('#' + txtAccRefCodeID).closest('div.dvGrid').find('input.hdnRefJson').val(jsonText);

            //$('#' + hdnJournalDetRefJson).val(jsonText);
        }


        function GetAccRefTextByDetIDLink(jsonList, linkid, typeid)
        {
          
            var recCount = 0;
            var refCode = '';
            for (var i = 0; i < jsonList.length; i++) {
                if (linkid > 0) {
                    if (jsonList[i].linkid != linkid) {
                        continue;
                    }
                }

                if (typeid > 0) {
                    if (jsonList[i].typeid != typeid) {
                        continue;
                    }
                }

                if (jsonList[i]._recordstateint != Enums.RecordState.Deleted) {
                    if (recCount == 0) {
                        refCode = jsonList[i].code;
                    }
                    recCount++;
                }
            }
            var rText = recCount + " entry(s)";
            if (recCount == 1)
            {
                rText = refCode
            }


            var refText = recCount > 0 ? rText : '';

            return rText;

        }


        function AccRefPopupOKClicked() {
            var cRetrun = true;

            var drcr = parseInt($('#' + curTxtAccRefCodeID).closest('tr.gridRow').find('input[id$="hdnDrCr"]').val())
            var detID_Link = parseInt($('#' + curTxtAccRefCodeID).closest('tr.gridRow').find('input[id$="hdnJournalDetID_Link"]').val());
            var detID = parseInt($('#' + curTxtAccRefCodeID).closest('tr.gridRow').find('input[id$="hdnJournalDetID"]').val())


            var jsonList = UpdateAccRefListToJson(curTxtAccRefCodeID, curAccRefTypeID);
            UpdateAccRefJsonToText(jsonList, drcr);

            var editModeInt = parseInt($('#' + hdnEditDataModeInt).val());
            var journalID = parseInt($('#' + hdnJournalID).val());

            var task = {
                EditModeInt: editModeInt
                , JournalID: journalID
                , Task: "journaldetreftype"
                , AccRefTypeID: curAccRefTypeID
                , JournalDetID: detID
                , JournalDetID_Link: detID_Link
                , UseDetIDLink: 1
                , IsFromWebUI : 1
            };

            var jrnl = ReadJournalFromUI();

            var jrnlDetListDr = ReadJournalDetFromUI(gridViewIDDet);
            var jrnlDetListCr = ReadJournalDetFromUI(gridViewIDDet2);
            var jrnlDetList = jrnlDetListDr.concat(jrnlDetListCr);

            var jrnlInsListDr = ReadJournalDetInsFromUI('dvGridContainer');
            var jrnlInsListCr = ReadJournalDetInsFromUI('dvGridContainer2');
            var jrnlInsList = jrnlInsListDr.concat(jrnlInsListCr);

            var jrnlRefListDr = ReadJournalDetRefFromUI('dvGridContainer');
            var jrnlRefListCr = ReadJournalDetRefFromUI('dvGridContainer2');
            var jrnlRefList = jrnlRefListDr.concat(jrnlRefListCr);

            var vResult = ValidateJournal(task, jrnl, jrnlDetList, jrnlInsList, jrnlRefList);

            if (vResult != null) {
                if (vResult.IsError)
                {
                    $('#' + curTxtAccRefCodeID).val(vResult.DetRefText);
                    alert(vResult.ErrorText);
                    cRetrun = false;
                }
                else
                {
                    cRetrun = true;

                    var jSonString = $('#' + curTxtAccRefCodeID).closest('div.dvGrid').find('input.hdnRefJson').val();
                    var detRefList = $.parseJSON(jSonString);

                    var rText = GetAccRefTextByDetIDLink(detRefList, detID_Link, curAccRefTypeID);
                    $('#' + curTxtAccRefCodeID).val(rText);

                    //$('#' + curTxtAccRefCodeID).val(totRow > 0 ? arText : '');
                }
            }
            else
            {
                alert('validation result = null');
                cRetrun = false;
            }


            return cRetrun;
        }


        function AccRefPopupOKClicked_Old() {
            var drcr = parseInt($('#' + curTxtAccRefCodeID).closest('table').closest('tr').find('input[id$="hdnDrCr"]').val())

            var jsonList = UpdateAccRefListToJson(curTxtAccRefCodeID, curAccRefTypeID);
            UpdateAccRefJsonToText(jsonList, drcr);

            var detID_Link = parseInt($('#' + curTxtAccRefCodeID).closest('table').closest('tr').find('input[id$="hdnJournalDetID_Link"]').val());

            var detAmt = JSUtility.GetNumber($('#' + curTxtAccRefCodeID).closest('table').closest('tr').find('input.txtAmount').val());
            //var detCrAmt = JSUtility.GetNumber($('#' + curTxtAccRefCodeID).closest('table').closest('tr').find('input[id$="txtCreditAmt"]').val());

            var summaryList = GetJsonListSummary(jsonList, detID_Link, curAccRefTypeID);

            var totRow = 0;
            var isCategorySumMatched = true;
            var categoryName = '';
            var categoryID = 0;
            var comma = '';

            for (i = 0; i < summaryList.length; i++) {
                totRow += summaryList[i].count;
                if (summaryList[i].amt != detAmt) {
                    isCategorySumMatched = false;
                    categoryName += comma + ' ' + summaryList[i].categoryname;
                    //categoryID = summaryList[i].categoryid;
                    comma = ',';
                    //break;
                }
            }

            if (!isCategorySumMatched) {
                alert('Amount Not Matched, Category: ' + categoryName);
            }

            var arText = totRow + ' entry(s)';
            if (totRow == 1) {
                arText = summaryList[0].codefirst;
            }

            $('#' + curTxtAccRefCodeID).val(totRow > 0 ? arText : '');
        }


        function UpdateTranTypeJSonList(txtTranTypeCodeID) {
            var code = $('#' + txtTranTypeCodeID).val();
            var id = parseInt($('#' + txtTranTypeCodeID).closest('tr').find('input[id$="hdnTranTypeID"]').val());
            var categoryid = parseInt($('#' + txtTranTypeCodeID).closest('tr').find('input[id$="hdnTranTypeCategoryID"]').val());
            var drcr = parseInt($('#' + txtTranTypeCodeID).closest('table').closest('tr').find('input[id$="hdnDrCr"]').val())
            var typeid = Enums.AccRefType.TranCode;
            var amt = 0;


            var detID = parseInt($('#' + txtTranTypeCodeID).closest('table').closest('tr').find('input[id$="hdnJournalDetID"]').val())
            var detID_Link = parseInt($('#' + txtTranTypeCodeID).closest('table').closest('tr').find('input[id$="hdnJournalDetID_Link"]').val());


            //var jSonString = $('#' + hdnJournalDetRefJson).val();
            var jSonString = $('#' + txtTranTypeCodeID).closest('div.dvGrid').find('input.hdnRefJson').val();
            var detRefList = $.parseJSON(jSonString);

            var idx = -1;
            for (var i = 0; i < detRefList.length; i++) {
                if (detRefList[i].linkid == detID_Link
                    && detRefList[i].typeid == typeid) {
                    idx = i;
                    break;
                }
            }

            if (idx == -1) {
                if (id > 0) {
                    var ccItem = { id: id, code: code, drcr: drcr, amt: amt
                                , typeid: typeid, categoryid: categoryid, categoryname: ''
                                , _recordstateint: Enums.RecordState.Added, linkid: detID_Link
                                , journaldetid: detID, journaldetrefid: 0
                    };
                    detRefList.push(ccItem);
                }
            } //found data
            else {
                if (id > 1) {
                    detRefList[idx].id = id;
                    detRefList[idx].code = code;
                    detRefList[idx].categoryid = categoryid;
                    detRefList[idx].drcr = drcr;
                    detRefList[idx].amt = amt;
                    detRefList[idx]._recordstateint = Enums.RecordState.Edited;

                    //detRefList[idx].linkid = detID_Link; 
                    //detRefList[idx].typeid = typeid;
                }
                else {
                    if (detRefList[idx].journaldetrefid > 0) {
                        detRefList[idx]._recordstateint = Enums.RecordState.Deleted;
                    }
                    else {
                        detRefList.splice(idx, 1);
                    }
                }
            }
            UpdateAccRefJsonToText(detRefList, drcr);
        }

        function GetJsonListSummary(jsonList, linkid, typeid) {
            var summaryList = [];

            linkid = linkid || 0;
            typeid = typeid || 0;

            //        if (!linkid) {
            //            linkid = 0;
            //        }


            //        if (!typeid) {
            //            typeid = 0;
            //        }

            for (var i = 0; i < jsonList.length; i++) {
                if (linkid > 0) {
                    if (jsonList[i].linkid != linkid) {
                        continue;
                    }
                }

                if (typeid > 0) {
                    if (jsonList[i].typeid != typeid) {
                        continue;
                    }
                }


                if (jsonList[i]._recordstateint != Enums.RecordState.Deleted) {
                    var idx = -1;
                    for (var j = 0; j < summaryList.length; j++) {
                        if (summaryList[j].linkid == jsonList[i].linkid
                            && summaryList[j].typeid == jsonList[i].typeid
                            && summaryList[j].categoryid == jsonList[i].categoryid) {
                            idx = j;
                            break;
                        }
                    } //search loop

                    if (idx == -1) {
                        var ccItem = { linkid: jsonList[i].linkid, typeid: jsonList[i].typeid
                                    , categoryid: jsonList[i].categoryid, categoryname: jsonList[i].categoryname
                                    , codefirst: jsonList[i].code
                                    , count: 1, amt: jsonList[i].amt

                        };
                        summaryList.push(ccItem);
                    }
                    else {
                        summaryList[idx].amt += jsonList[i].amt;
                        summaryList[idx].count++;
                    }
                } // not deleted
            }
            return summaryList;
        }


        function GetJsonInsListCount(jsonList, linkid) {

            var inscount = 0;
            linkid = linkid || 0;

            //        if (!linkid) {
            //            linkid = 0;
            //        }

            for (var i = 0; i < jsonList.length; i++) {
                if (jsonList[i]._recordstateint != Enums.RecordState.Deleted) {
                    if (linkid > 0) {
                        if (jsonList[i].linkid == linkid) {
                            inscount++;
                        }
                    }
                    else {
                        inscount++;
                    }
                } // not deleted
            }
            return inscount;
        }



        function FillInsListFromJSon(txtInsCodeID, includeOneEmptyRow) {


            //$('#' + txtInsCodeID).closest('div[id$="hdnJournalDetID"]')
            //$('#' + txtInsCodeID).closest('div[id="dvGrid"]').find('input.hdnInsJson').val();


            //var jSonString = $('#' + hdnJournalDetInsJson).val();
            //var jSonString = $('#' + txtInsCodeID).closest('div[id="dvGrid"]').find('input.hdnInsJson').val();
            var jSonString = $('#' + txtInsCodeID).closest('div.dvGrid').find('input.hdnInsJson').val();

            var detInsList = $.parseJSON(jSonString);

            var detID = 0;
            var detID_Link = 0;

            detID = $('#' + txtInsCodeID).closest('table').closest('tr').find('input[id$="hdnJournalDetID"]').val();
            detID_Link = parseInt($('#' + txtInsCodeID).closest('table').closest('tr').find('input[id$="hdnJournalDetID_Link"]').val());

            //alert(detID);

            var detInsListLink = $.grep(detInsList, function (n, i) {
                return (n.linkid == detID_Link);
            });

            var table = $('#' + dvPopupIns).find('.tblIns');
            $(table).empty();

            var rowSLNo = 0;
            for (var i = 0; i < detInsListLink.length; i++) {
                var rowItem = detInsListLink[i];
                //alert(rowItem.journaldetid);

                var newRow = AddNewRowToInsList(table, rowItem);
                if (rowItem._recordstateint != Enums.RecordState.Deleted) {
                    rowSLNo++;
                    $(newRow).find('.spnInsPopupSLNo').text(rowSLNo);
                }
            }

            if (includeOneEmptyRow) {
                var newRowE = AddNewRowToInsList(table, null);
                rowSLNo++;
                $(newRowE).find('.spnInsPopupSLNo').text(rowSLNo);
            }

        }
        function AddNewRowToInsList(tblIns, rowData) {
            //var rowCount = $('table tr').length;
            //var rowCount = $(tblIns).find('tr:visible').length;
            var rowCount = $(tblIns).find('tr.rowIns:visible').length;


            //var templateRow = $('.trInsTemplate:first').clone();
            var templateRow = $('.trInsTemplate:first').clone(true, true);
            templateRow.removeClass('trInsTemplate');
            templateRow.find('.ddlInsPopupInsType').removeAttr('id');

            var newRow = templateRow.appendTo(tblIns);

            var RowNum = $(newRow).index() + 1;

            if (RowNum % 2 == 0) {
                $(newRow).addClass('tableRowEven');
            }
            else {
                $(newRow).addClass('tableRowOdd');
            }
            //$(tblCostCenter).find('tr:visible:even').addClass('tableRowOdd');  //based on 0 based index

            if (!rowData) {
                rowData = null;
            }
            //Enums.InstrumentLinkType
            var rowSLNo = rowCount + 1;
            //rownumber
            //$(newRow).find('.spnCostCenterPopupSLNo').val(RowNum);
            $(newRow).find('.spnInsPopupSLNo').text(rowSLNo);
            if (rowData != null) {
                $(newRow).find('.hdnInsPopupLinkID').val(rowData.linkid);
                $(newRow).find('.hdnInsPopupJournalDetID').val(rowData.journaldetid);
                $(newRow).find('.hdnInsPopupJournalDetInsID').val(rowData.journaldetinsid);

                $(newRow).find('.hdnRecordState').val(rowData._recordstateint);

                $(newRow).find('.ddlInstrumentType').val(rowData.transtypeid);

                $(newRow).find('.txtInsPopupIns').val(rowData.insno);
                $(newRow).find('.hdnInsPopupInsID').val(rowData.insid);
                $(newRow).find('.hdnInsPopupInsModeID').val(rowData.insmodeid);
                $(newRow).find('.hdnInsPopupInsLinkTypeID').val(rowData.inslinktypeid);

                $(newRow).find('.txtInsPopupInsDate').val(rowData.insdate);


                //            $(newRow).find('.hdnAccRefPopupCategoryID').val(rowData.categoryid)
                //            $(newRow).find('.txtAccRefPopupCategoty').val(rowData.categoryname);

                $(newRow).find('.txtInsPopupAmount').val(rowData.amt);
            }


            //RefreshCostCenterListRowNumber();
            //GridNavAccRef(newRow);

            ContentForm.InitDefualtFeatureInScopeElement(newRow);


            ///ContentForm.InitDefualtFeature();
            //        ContentForm.TextNumberFormatScopeElement(newRow);
            //        ContentForm.TextDecimalFormatScopeElement(newRow);
            ContentForm.InitJQueryDateInScopeElement(newRow, 'txtInsPopupInsDate');
            bindInsPopupListByRow(newRow);

            if (rowData != null) {
                if (rowData._recordstateint == Enums.RecordState.Deleted) {
                    $(newRow).hide();
                }
            }
            return newRow;
        }

        function RefreshInsListRowNumber() {
            var table = $('#' + dvPopupAccRef).find('.tblAccRef');
            $(table).find('tr:visible').each(function (idx, elem) {
                $(elem).find('.spnAccRefPopupSLNo').text(idx + 1);
            });
        }


        function UpdateInsListToJson(txtInsCodeID) {
            //var jSonString = $('#' + hdnJournalDetInsJson).val();

            var jSonString = $('#' + txtInsCodeID).closest('div.dvGrid').find('input.hdnInsJson').val();

            var detInsList = $.parseJSON(jSonString);

            var drcr = parseInt($('#' + txtInsCodeID).closest('tr.gridRow').find('input[id$="hdnDrCr"]').val())

            var detID = parseInt($('#' + txtInsCodeID).closest('tr.gridRow').find('input[id$="hdnJournalDetID"]').val())
            var detID_Link = parseInt($('#' + txtInsCodeID).closest('tr.gridRow').find('input[id$="hdnJournalDetID_Link"]').val());



            //filter out by current link and type
            for (i = 0; i < detInsList.length; ++i) {
                if (detInsList[i].linkid == detID_Link) {
                    detInsList.splice(i--, 1);
                }
            }

            var table = $('#' + dvPopupIns).find('.tblIns');

            //var detRefListLinkNew = [];

            var slno = 0;
            $(table).find('tr.rowIns').each(function (idx, rowElem) {
                //            var linkid = parseInt($(rowElem).find('.hdnCostCenterPopupLinkID').val());
                //            var journaldetid = parseInt($(rowElem).find('.hdnCostCenterPopupJournalDetID').val());

                var linkid = detID_Link;
                var journaldetid = detID;

                var journaldetinsid = parseInt($(rowElem).find('.hdnInsPopupJournalDetInsID').val());
                var recordstateRow = $(rowElem).find('.hdnRecordState').val();

                var insmodeid = parseInt($(rowElem).find('.hdnInsPopupInsModeID').val());
                var instypeid = parseInt($(rowElem).find('.ddlInsPopupInsType').val());


                var inslinktypeid = parseInt($(rowElem).find('.hdnInsPopupInsLinkTypeID').val());


                var insid = parseInt($(rowElem).find('.hdnInsPopupInsID').val());
                var insno = $(rowElem).find('.txtInsPopupIns').val();
                var amt = JSUtility.GetNumber($(rowElem).find('.txtInsPopupAmount').val());


                //var insdate = $(rowElem).find('.txtInsPopupInsDate').val();
                var insdate = '';


                var isValid = false;
                var isToList = false;

                if (amt > 0) {
                    isValid = true;
                }

                var recordstateint = Enums.RecordState.Edited;
                if (recordstateRow == Enums.RecordState.Deleted) {
                    if (journaldetinsid > 0) {
                        recordstateint = Enums.RecordState.Deleted;
                        isToList = true;
                    }
                } // rowstate deleted
                else {
                    if (isValid) {
                        if (journaldetinsid > 0) {
                            recordstateint = Enums.RecordState.Edited;
                        }
                        else {
                            recordstateint = Enums.RecordState.Added;
                        }
                        isToList = true;
                        slno++;
                    }
                    else {
                        if (journaldetinsid > 0) {
                            recordstateint = Enums.RecordState.Deleted;
                            isToList = true;
                        }
                    }
                }

                if (isToList) {

                    var ccItem = { insid: insid, insno: insno, amt: amt
                                , insmodeid: insmodeid, instypeid: instypeid, slno: slno, inslinktypeid: inslinktypeid
                                , _recordstateint: recordstateint, linkid: linkid
                                , journaldetid: journaldetid, journaldetinsid: journaldetinsid, drcr: drcr
                                , insdate: insdate
                    };

                    detInsList.push(ccItem);
                }
            });
            return detInsList;

        }


        function UpdateInsJsonToText(jsonData, pDrCr) {
            //jsonData = SortAccRefData(jsonData);
            pDrCr = pDrCr || Enums.DrCr.Debit;

            var jsonText = JSUtility.JSONStringify(jsonData);
            //$('#' + hdnJournalDetInsJson).val(jsonText);

            //$('#' + txtInsCodeID).closest('div.dvGrid').find('input.hdnInsJson').val(jsonText);
            if (pDrCr == Enums.DrCr.Debit) {
                $('#' + hdnJournalDetInsJson).val(jsonText);
            }
            else {
                $('#' + hdnJournalDetInsJson2).val(jsonText);
            }

            //$(hdnInsJson).val(jsonText);
        }


        function GetInsCountByDetLink(pDetLinkID, pDrCr, pIncludeDeleted) {

            pIncludeDeleted = pIncludeDeleted || false;
            pDrCr = pDrCr || Enums.DrCr.Debit;

            var jSonString = '';

            if (pDrCr == Enums.DrCr.Debit) {
                jSonString = $('#' + hdnJournalDetInsJson).val();
            }
            else {
                jSonString = $('#' + hdnJournalDetInsJson2).val();
            }

            var detInsList = $.parseJSON(jSonString);

            var cnt = 0;
            for (i = 0; i < detInsList.length; i++) {
                if (detInsList[i].linkid == pDetLinkID) {
                    if (detInsList[i]._recordstateint == Enums.RecordState.Deleted) {
                        if (pIncludeDeleted) {
                            cnt++;
                        }
                    }
                    else {

                        cnt++;
                    }
                }
            }
            return cnt;
        }

        function RemoveInsByDetLink(pDetLinkID, pDrCr) {
            var jSonString = $('#' + hdnJournalDetInsJson).val();
            var detInsList = $.parseJSON(jSonString);

            var cnt = 0;
            for (i = 0; i < detInsList.length; i++) {
                if (detInsList[i].linkid == pDetLinkID) {
                    if (detInsList[i]._recordstateint != Enums.RecordState.Deleted) {
                        detInsList[i]._recordstateint = Enums.RecordState.Deleted;
                        cnt++;
                    }
                }
            }
            UpdateInsJsonToText(detInsList);
            return cnt;
        }

        function InsPopupOKClicked() {
            var drcr = parseInt($('#' + curTxtInsCodeID).closest('table').closest('tr').find('input[id$="hdnDrCr"]').val())

            var jsonList = UpdateInsListToJson(curTxtInsCodeID);
            UpdateInsJsonToText(jsonList, drcr);

            var detID_Link = parseInt($('#' + curTxtInsCodeID).closest('table').closest('tr').find('input[id$="hdnJournalDetID_Link"]').val());

            //        var detDbAmt = JSUtility.GetNumber($('#' + curTxtInsCodeID).closest('table').closest('tr').find('input[id$="txtDebitAmt"]').val());
            //        var detCrAmt = JSUtility.GetNumber($('#' + curTxtInsCodeID).closest('table').closest('tr').find('input[id$="txtCreditAmt"]').val());


            var detAmt = JSUtility.GetNumber($('#' + curTxtInsCodeID).closest('table').closest('tr').find('input.txtAmount').val());


            //var summaryList = GetJsonListSummary(jsonData, detID_Link, curAccRefTypeID);

            var totRow = GetJsonInsListCount(jsonList, detID_Link);

            ///var totRow = 0;


            //        var isCategorySumMatched = true;
            //        var categoryName = '';
            //        var categoryID = 0;
            //        var comma = '';

            //        for (i = 0; i < summaryList.length; i++) {
            //            totRow += summaryList[i].count;
            //            if (summaryList[i].amt != (detDbAmt + detCrAmt)) {
            //                isCategorySumMatched = false;
            //                categoryName += comma + ' ' + summaryList[i].categoryname;
            //                //categoryID = summaryList[i].categoryid;
            //                comma = ',';
            //                //break;
            //            }
            //        }

            //        if (!isCategorySumMatched) {
            //            alert('Amount Not Matched, Category: ' + categoryName);
            //        }

            $('#' + curTxtInsCodeID).val(totRow > 0 ? totRow + ' entry(s)' : '');
        }


        function InsDetailsPopupOKClicked() {

            //var insmodeid = 2; //rec

            var companyid = $('#' + hdnCompanyID).val();

            var insmodeid = parseInt($('#' + dvPopupInsDetails).find('.hdnInsDetInsModeID').val());
            var instypeid = parseInt($('#' + dvPopupInsDetails).find('.ddlInsDetInsType').val());

            var insid = parseInt($('#' + dvPopupInsDetails).find('.hdnInsDetInsID').val());
            var insno = $('#' + dvPopupInsDetails).find('.txtInsDetInsNo').val();
            var insdate = $('#' + dvPopupInsDetails).find('.txtInsDetInsDate').val();
            var issuename = $('#' + dvPopupInsDetails).find('.txtInsDetIssueName').val();
            var bankname = $('#' + dvPopupInsDetails).find('.txtInsDetBankName').val();
            var branchname = $('#' + dvPopupInsDetails).find('.txtInsDetBranchName').val();
            var insamt = JSUtility.GetNumber($('#' + dvPopupInsDetails).find('.txtInsDetInsAmt').val());

            var insstatusid = parseInt($('#' + dvPopupInsDetails).find('.ddlInsDetInsStatus').val());
            var insstatusdate = $('#' + dvPopupInsDetails).find('.txtInsDetInsStatusDate').val();

            var remarks = $('#' + dvPopupInsDetails).find('.txtInsDetInsRemarks').val();


            insno = insno.trim();
            if (insno == '') {
                alert('Please enter instrument no.');
                return false;
            }

            if (instypeid == 0) {
                alert('Please select instrument type.');
                return false;
            }

            if (insid == 0) {
                if (!confirm('This new Instrument will be saved to system. continue?')) {
                    return false;
                }
            }

            var insList = [];

            var insItem = { insid: insid, insno: insno, insdate: insdate, insamt: insamt
                         , issuename: issuename, bankname: bankname, branchname: branchname
                         , instypeid: instypeid, insmodeid: insmodeid, companyid: companyid
                         , insstatusid: insstatusid, insstatusdate: insstatusdate, remarks: remarks
            };

            insList.push(insItem);

            var insData = UpdateInstrument(insList);
            if (insData == null) {
                alert('Error Occurred!');
                return false;
            }

            if (insData.insid > 0) {
                $('#' + dvPopupInsDetails).find('.hdnInsDetInsID').val(insData.insid);

                $(curTxtInsDetailsCodeElem).val(insno);
                $(curTxtInsDetailsCodeElem).closest('tr.rowIns').find('.hdnInsPopupInsID').val(insData.insid);
                $(curTxtInsDetailsCodeElem).closest('tr.rowIns').find('.ddlInsPopupInsType').val(insData.instypeid);


                $(curTxtInsDetailsCodeElem).closest('tr.rowIns').find('.txtInsPopupAmount').val(insamt);

                $(curTxtInsDetailsCodeElem).removeClass('textError');
                // $('#' + dvPopupInsDetails).dialog('close');
                return true;
            }
            else {
                alert(insData.msg);
                return false;
            }

            return false;
            //alert(insidNew);
        }

        function GetInstrument(insID, insNo, insModeID, insTypeID) {
            var ins = null;
            var isError = false;
            var isComplete = false;
            //ajax call

            insID = insID || 0;
            insNo = insNo || '';
            insModeID = insModeID || 0;
            insTypeID = insTypeID || 0;


            var companyid = $('#' + hdnCompanyID).val();

            var serviceUrl = InstrumentGetServiceLink + '?insid=' + insID + '&insno=' + insNo + '&insmodeid=' + insModeID + '&instypeid=' + insTypeID;
            serviceUrl += '&companyid=' + companyid;

            //serviceUrl += '&codecomptype=' + Enums.DataCompareType.EqualTo;


            var dummyVar = $.ajax({
                type: "GET",
                //type: "POST",
                cache: false,
                async: false,
                dataType: "json",
                url: serviceUrl,
                success: function (insdata) {
                    if (insdata.rows.length > 0) {
                        ins = insdata.rows[0];
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
            return ins;
        }

        function UpdateInstrument(insList) {
            var isError = false;
            var isComplete = false;

            var insData = null;

            //ajax call

            var serviceUrl = InstrumentUpdateServiceLink;

            insData = { 'name': 'name33', 'amt': 433.33,
                'inslist': JSUtility.JSONStringify(insList)
            };

            var dummyVar = $.ajax({
                type: "POST",
                cache: false,
                async: false,
                dataType: "json",
                url: serviceUrl,
                data: insData,
                success: function (data) {
                    insData = data;
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
            return insData;
        }



        function SetTranTypeData(txtTranTypeCodeID, data) {
            $('#' + txtTranTypeCodeID).val(data.code);
            $('#' + txtTranTypeCodeID).closest('tr').find('input[id$="hdnTranTypeID"]').val(data.id);
            $('#' + txtTranTypeCodeID).closest('tr').find('input[id$="hdnTranTypeCategoryID"]').val(data.categoryid);
            UpdateTranTypeJSonList(txtTranTypeCodeID);
        }

        function ShowTranTypeList(txtTranTypeCodeID) {
            curTextTranTypeCodeID = txtTranTypeCodeID;
            var tranTypeID = $('#' + txtTranTypeCodeID).closest('tr').find('input[id$="hdnTranTypeID"]').val();

            //$("#" + dvGLTranTypePopup).GroupTree("show");
            $("#" + dvTranTypePopup).GroupTree("showByID", tranTypeID);
        }

        function SetGLAccountData(txtGLAccCodeID, data) {
            $('#' + txtGLAccCodeID).val(data.glacccode);

            var detRow = $('#' + txtGLAccCodeID).closest('tr.gridRow');

            $(detRow).find('input[id$="hdnGLAccountID"]').val(data.glaccid);
            $(detRow).find('input[id$="txtGLAccountName"]').val(data.glaccname);

            $(detRow).find('input[id$="hdnGLGroupClassID"]').val(data.glgroupclassid);
            $(detRow).find('input[id$="hdnIsInstrument"]').val(data.isinstrument);

            $(detRow).find('input[id$="hdnGLGroupIDAcc"]').val(data.glgroupid);

            $(detRow).find('input[id$="hdnGLGroupID"]').val(data.glgroupid);
            $(detRow).find('input[id$="txtGLGroupCode"]').val(data.glgroupcode);
            $(detRow).find('input[id$="txtGLGroupName"]').val(data.glgroupnameshort);


            $(detRow).find('input[id$="txtGLGroupCode"]').removeClass('textError');

            var hdnIsInstrumentElem = $(detRow).find('input[id$="hdnIsInstrument"]');
            var txtInstrumentElem = $(detRow).find('input[id$="txtInstrument"]');
            var btnInstrumentElem = $(detRow).find('input[id$="btnInstrument"]');

            //check current entry
            setDetInstrument(hdnIsInstrumentElem, txtInstrumentElem, btnInstrumentElem);
            SetDebitCreditRemainAmt(txtGLAccCodeID);
            sumJournalDetAmt();
        }

        function ClearGLAccountData(txtGLAccCodeID) {
            //$('#' + txtGLAccCodeID).val('');
            var detRow = $('#' + txtGLAccCodeID).closest('tr.gridRow');
            $(detRow).find('input[id$="hdnGLAccountID"]').val('0');
            $(detRow).find('input[id$="txtGLAccountName"]').val('');
            
            
            //$(detRow).find('input[id$="hdnGLGroupClassID"]').val(0);
            //$(detRow).find('input[id$="hdnIsInstrument"]').val(0);
            //$(detRow).find('input[id$="hdnGLGroupID"]').val(0);

            var hdnIsInstrumentElem = $(detRow).find('input[id$="hdnIsInstrument"]');
            var txtInstrumentElem = $(detRow).find('input[id$="txtInstrument"]');
            var btnInstrumentElem = $(detRow).find('input[id$="btnInstrument"]');

            setDetInstrument(hdnIsInstrumentElem, txtInstrumentElem, btnInstrumentElem);
        }

        function SetDebitCreditRemainAmt(txtGLAccCodeID) {
           
            var detRow = $('#' + txtGLAccCodeID).closest('tr.gridRow');
            var curAmtElem = $(detRow).find('input.txtAmount');
            var drcr = parseInt($(detRow).find('input[id$="hdnDrCr"]').val());

            var debitTotal = GetTotalSumDebit();
            var creditTotal = GetTotalSumCredit();

            var curAmt = parseFloat(JSUtility.GetNumber($(curAmtElem).val()));

            var diffAmt = 0;
            if (drcr == Enums.DrCr.Debit) {
                diffAmt = creditTotal - debitTotal;
            }
            else {
                diffAmt = debitTotal - creditTotal;
            }

            diffAmt = diffAmt < 0 ? 0 : diffAmt;
            if (curAmt <= 0) {
                $(curAmtElem).val(JSUtility.FormatCurrency(diffAmt));
                $(curAmtElem).removeClass('fldDataError');
            }

            curAmtElem = null;
        }

        function SetInsRemainAmt(txtInsCodeID, txtDetInsCodeID ) {
            var curDrCrAmt = parseFloat(JSUtility.GetNumber($('#' + txtDetInsCodeID).closest('tr.gridRow').find('input.txtAmount').val()));
            
            var curAmtElem = $(txtInsCodeID).closest('tr.rowIns').find('input.txtInsPopupAmount');
            var totInsAmt = GetTotalSumIns();
            var curAmt = JSUtility.GetNumber($(curAmtElem).val());
            var diffAmt = curDrCrAmt - totInsAmt;
            diffAmt = diffAmt < 0 ? 0 : diffAmt;
            if (curAmt <= 0) {
                $(curAmtElem).val(JSUtility.FormatCurrency(diffAmt));
                $(curAmtElem).removeClass('fldDataError');
            }
            curAmtElem = null;

        }


        function GetGLAccount(accCode) {
            var glAcc = null;
            var isError = false;
            var isComplete = false;
            //ajax call

            var glgroupclassinc = $('#' + hdnGLGroupClassInclude).val();
            var glgroupclassexc = $('#' + hdnGLGroupClassExclude).val();
            var companyid = $('#' + hdnCompanyID).val();

            var serviceURL = GLAccountServiceLink + "?code=" + accCode;
            serviceURL += "&acctypefilter=" + Enums.GLAccountTypeFilter.NormalSubAccount;
            serviceURL += "&glgroupclassinclude=" + glgroupclassinc;
            serviceURL += "&glgroupclassexclude=" + glgroupclassexc;
            serviceURL += "&companyid=" + companyid;


            var dummyVar = $.ajax({
                type: "GET",
                cache: false,
                async: false,
                dataType: "json",
                url: serviceURL,

                success: function (accdata) {
                    //            if (accdata.menu[0].count > 0) {
                    //                menu = menudata.menu[0];
                    //            }
                    if (accdata.rows.length > 0) {
                        glAcc = accdata.rows[0];
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
            return glAcc;
        }

        function GetGLGroup(grpCode) {
            var glGrp = null;
            var isError = false;
            var isComplete = false;
            //ajax call


            var glgroupclassinc = $('#' + hdnGLGroupClassInclude).val();
            var glgroupclassexc = $('#' + hdnGLGroupClassExclude).val();
            var companyid = $('#' + hdnCompanyID).val();

            var serviceURL = GLGroupServiceLink + "?code=" + grpCode;
            serviceURL += "&glgroupclassinclude=" + glgroupclassinc;
            serviceURL += "&glgroupclassexclude=" + glgroupclassexc;
            serviceURL += "&companyid=" + companyid;


            var dummyVar = $.ajax({
                type: "GET",
                cache: false,
                async: false,
                dataType: "json",
                url: serviceURL,

                success: function (grpdata) {
                    //            if (accdata.menu[0].count > 0) {
                    //                menu = menudata.menu[0];
                    //            }
                    if (grpdata.rows.length > 0) {
                        glGrp = grpdata.rows[0];
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
            return glGrp;
        }


        function GetGLAccRefCategoryList(glAccountID, accRefTypeID) {
            var glRefCatList = null;
            var isError = false;
            var isComplete = false;
            //ajax call

            var companyid = $('#' + hdnCompanyID).val();

            var serviceURL = AccRefCategoryServiceLink + "?typeid=" + accRefTypeID;
            serviceURL += "&glaccountid=" + glAccountID;
            serviceURL += "&task=" + "glaccrefcategory";
            serviceURL += "&companyid=" + companyid;


            var dummyVar = $.ajax({
                type: "GET",
                cache: false,
                async: false,
                dataType: "json",
                url: serviceURL,

                success: function (resultdata) {
                    //            if (accdata.menu[0].count > 0) {
                    //                menu = menudata.menu[0];
                    //            }
                    if (resultdata.rows.length > 0) {
                        glRefCatList = resultdata.rows;
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
            return glRefCatList;
        }




        function SetGLGroupData(txtGLGroupCodeID, data) {
            $('#' + txtGLGroupCodeID).val(data.glgroupcode);

            var detRow = $('#' + txtGLGroupCodeID).closest('tr.gridRow');

            $(detRow).find('input[id$="hdnGLGroupID"]').val(data.glgroupid);
            $(detRow).find('input[id$="txtGLGroupName"]').val(data.glgroupnameshort);
            $(detRow).find('input[id$="hdnGLGroupClassID"]').val(data.glgroupclassid);
            $(detRow).find('input[id$="hdnIsInstrument"]').val(data.isinstrument);


            var grpIDAcc = parseInt($(detRow).find('input[id$="hdnGLGroupIDAcc"]').val());

            if (grpIDAcc > 0) {
                if (grpIDAcc != data.glgroupid) {
                    $(detRow).find('input[id$="hdnGLAccountID"]').val('0');
                    $(detRow).find('input[id$="txtGLAccountCode"]').val('');
                    $(detRow).find('input[id$="txtGLAccountName"]').val('');
                }
            }


            //ins
            var hdnIsInstrumentElem = $(detRow).find('input[id$="hdnIsInstrument"]');
            var txtInstrumentElem = $(detRow).find('input[id$="txtInstrument"]');
            var btnInstrumentElem = $(detRow).find('input[id$="btnInstrument"]');


            //


            //check current entry
            setDetInstrument(hdnIsInstrumentElem, txtInstrumentElem, btnInstrumentElem);
        }

        function ClearGLGroupData(txtGLGroupCodeID) {
            //$('#' + txtGLAccCodeID).val('');
            var detRow = $('#' + txtGLGroupCodeID).closest('tr.gridRow');
            $(detRow).find('input[id$="hdnGLGroupID"]').val('0');
            $(detRow).find('input[id$="txtGLGroupName"]').val('');
            $(detRow).find('input[id$="hdnGLGroupClassID"]').val(0);
            $(detRow).find('input[id$="hdnIsInstrument"]').val(0);

            var hdnIsInstrumentElem = $(detRow).find('input[id$="hdnIsInstrument"]');
            var txtInstrumentElem = $(detRow).find('input[id$="txtInstrument"]');
            var btnInstrumentElem = $(detRow).find('input[id$="btnInstrument"]');

            setDetInstrument(hdnIsInstrumentElem, txtInstrumentElem, btnInstrumentElem);
        }



        function ClearTranType(txtTranTypeCodeID) {
            $("#" + txtTranTypeCodeID).val('');
            $('#' + txtTranTypeCodeID).closest('tr').find('input[id$="hdnTranTypeID"]').val('0');
            $('#' + txtTranTypeCodeID).closest('tr').find('input[id$="hdnTranTypeCategoryID"]').val('0');
            UpdateTranTypeJSonList(txtTranTypeCodeID);
        }

        function SetAccRefDataPopup(txtAccRefCodeID, data) {
            $(txtAccRefCodeID).val(data.code);
            $(txtAccRefCodeID).closest('tr').find('.hdnAccRefPopupAccRefID').val(data.id);
            $(txtAccRefCodeID).closest('tr').find('.txtAccRefPopupAccRefName').val(data.name);
            $(txtAccRefCodeID).closest('tr').find('.txtAccRefPopupCategory').val(data.categorycode);
            $(txtAccRefCodeID).closest('tr').find('.hdnAccRefPopupCategoryID').val(data.categoryid);

            //var remainAmt = GetAccRefRemainAmount(data.categoryid);
            var remainAmt = GetAccRefRemainAmount(curTxtAccRefCodeID, data.categoryid);
            var curAmt = JSUtility.GetNumber($(txtAccRefCodeID).closest('tr').find('.txtAccRefPopupAmount').val());
            if (curAmt <= 0) {

                $(txtAccRefCodeID).closest('tr').find('.txtAccRefPopupAmount').val(JSUtility.FormatCurrency(remainAmt));
            }

        }

        function ClearAccRefPopup(txtAccRefCodeID) {
            //$(txtAccRefCodeID).val('');
            //$(txtAccRefCodeID).closest('tr').find('.hdnAccRefPopupAccRefID').val('0');
            //$(txtAccRefCodeID).closest('tr').find('.txtAccRefPopupCategoty').val('');
            //$(txtAccRefCodeID).closest('tr').find('.hdnAccRefPopupCategoryID').val('0');
        }

        function SetInsDataPopup(txtInsCodeID, data) {
            $(txtInsCodeID).val(data.insno);
            $(txtInsCodeID).closest('tr.rowIns').find('.hdnInsPopupInsID').val(data.insid);
            $(txtInsCodeID).closest('tr.rowIns').find('.hdnInsPopupInsLinkTypeID').val(Enums.InstrumentLinkType.Actual);
            //$(txtInsCodeID).closest('tr.rowIns').find('.txtAccRefPopupCategoty').val(data.categoryname);
            //$(txtInsCodeID).closest('tr.rowIns').find('.hdnAccRefPopupCategoryID').val(data.categoryid);


            SetInsRemainAmt(txtInsCodeID, curTxtInsCodeID);

        }

        function ClearInsPopup(txtInsCodeID) {
            $(txtInsCodeID).val('');
            $(txtInsCodeID).closest('tr.rowIns').find('.hdnInsPopupInsID').val('0');
            $(txtInsCodeID).closest('tr.rowIns').find('.hdnInsPopupInsLinkTypeID').val('0');
            //$(txtInsCodeID).closest('tr.rowIns').find('.txtAccRefPopupCategoty').val('');
            // $(txtInsCodeID).closest('tr.rowIns').find('.hdnAccRefPopupCategoryID').val('0');
        }




        function onPrintClick() {
            //alert('Print');

            var rptID = 1530;
            var journalID = $("#" + hdnJournalID).val();

            if (journalID == "0" | journalID == "") {
                alert("Please Save First!");
                return false;
            }

            var serviceURL = this.reportGeneratePageLink;
            var fullURL = serviceURL + "?rptid=" + rptID + "&journalid=" + journalID + "&rptopentype=1"
            //var fullURL = serviceURL + "?rptid=" + rptID + "&payreqid=" + payReqID + "&rptopentype=3" + "&exporttype=pdf"
            $.ajax({
                url: fullURL,
                cache: false,
                dataType: "json",
                type: "POST",
                async: false,
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    //alert(data.info.totrec);
                    //alert(data.report.reportkey);
                    showReport(data.report.reportkey);
                },

                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(textStatus);
                }
            });



            return false;
        }



        function tbopen(key) {
            if (!key) {
                key = '';
            }


            var url = ZForm.RootPath + "Admin/SetPassword.aspx?uid=" + key
            //if (pageInTab == 1)
            if (ZForm.PageMode == Enums.PageMode.InTab) {

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
                    window.parent.TabMenu.OpenMenuByData(tdata);
                }
                catch (err) {
                    alert("error in page");
                }
                $
            }
            else {
                //on new window/tab
                //window.open(url,'_blank');   

                window.location = url;
            }
        }


        function tbload(key, projectid) {
            if (!key) {
                key = '';
            }

            if (!projectid) {
                projectid = '';
            }



            var url = ZForm.RootPath + "Payment/PaymentRequisition.aspx?id=" + key + "&projectid=" + projectid;





            //if (pageInTab == 1)
            if (ZForm.PageMode == Enums.PageMode.InTab) {

                //        var tdata = new xtabdata();
                //        tdata.linktype = Enums.LinkType.Direct;
                //        tdata.id = 0;
                //        tdata.name = "LandPaySchedule";
                //        //tdata.label = "User: " + userid;
                //        tdata.label = "Land Pay Schedule";
                //        tdata.type = 0;
                //        tdata.url = url;
                //        tdata.tabaction = Enums.TabAction.InTab;
                //        tdata.selecttab = 1;
                //        tdata.reload = 0;
                //        tdata.param = "";

                //        try {
                //            window.parent.OpenMenuByData(tdata);
                //        }
                //        catch (err) {
                //            alert("error in page");
                //        }


                //        url = JSUtility.replaceQueryString(url, '_t', ZForm.PageMode);
                //        url = JSUtility.replaceQueryString(url, '_n', ZForm.TabNo);
                ////window.location = url;

                try {
                    window.parent.TabMenu.SetIFrameSource(ZForm.TabNo, url, true);
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

        function showReport(key, showWait) {
            // alert(showWait)

            if (!key) {
                key = '';
            }


            //var url = "./Report/ReportView.aspx?rk=" + key
            var url = this.ReportViewPageLink + "?rk=" + key;
            //if (pageInTab == 1)
            if (ZForm.PageMode == Enums.PageMode.InTab) {
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
                    window.parent.TabMenu.OpenMenuByData(tdata);
                }
                catch (err) {
                    alert("error in page");
                }
            }
            else {
                //on new window/tab
                window.open(url, '_blank');

                //window.location = url;
                //window.open('{0}')
            }
        }



        function togglePannelStatus(content) {
            //alert('ab');
            //detDiv = content.nextSibling;

            strMore = "";
            strLess = ""

            var detDiv = JSUtility.getNextSibling(content);
            var expand = (detDiv.style.display == "none");
            detDiv.style.display = (expand ? "block" : "none");

            var downimg = "url('../image/more.png')";
            var upimg = "url('../image/less.png')";

            content.title = (expand ? strLess : strMore);
            //content.innerHTML = (expand ? strLess : strMore);
            content.style.backgroundImage = (expand ? upimg : downimg);
        }

        function fromParent(val1) {
            alert('this is called from parent: ' + val1);
        }

        function Button1_onclick() {
            //document.getElementById("btnSave").click();
            ///__doPostBack("<%= this.btnSave.UniqueID %>", "");
            __doPostBack("btnSave", "");
        }


        function btnSalaryInfo_onclick() {

        }



        // ]]>
    </script>
    <style type="text/css">
        #dvControlsTab
        {
            padding: 0px;
            background: none;
            border-width: 0px;
        }
        
        #dvControlsTab .ui-tabs-nav
        {
            padding-left: 0px;
            background: transparent;
            border-width: 0px 0px 1px 0px;
            
            border-radius: 0px;
            -moz-border-radius: 0px;
            -webkit-border-radius: 0px;
        }
        
        #dvControlsTab .ui-tabs-selected a
        {
            color: #000;
            font-weight: bold; /*
           border-top: 3px solid #fabd23; 
           border-left: 1px solid #fabd23; 
           border-right: 1px solid #fabd23;
            */
            border-top: 3px solid blue;
            margin-bottom: -1px;
            overflow: visible;
        }
        
        #dvControlsTab .ui-state-default
        {
            /*background: transparent;*/ /* border: none; */
        }
        
        #dvControlsTab .ui-state-default a
        {
            /*color: #c0c0c0;*/
        }
        #dvControlsTab .ui-state-active a
        {
            /* color: #459E00; */
            color: blue;
        }
        
        
        .groupBoxContainer
        {
            height: 100%;
            width: 820px;
            overflow: auto;
            margin-left: 5px;
            margin-top: 5px;
        }
        
        .groupHeader
        {
            height: 20px;
            background-image: url('../../image/header13.png');
            background-repeat: repeat-x;
            color: White;
            font-weight: bold;
        }
        
        .groupBox
        {
            background-image: url('../../image/bg_greendot.gif');
            height: 100%;
            width: 100%;
            min-width: 500px;
            display: inline-block;
            text-align: center;
            vertical-align: middle;
        }
        
        .groupContent
        {
        	width: 100%; 
        	height: 100%;
        }
        
        .groupContenInner
        {
        	width: 100%; 
        	height: auto;
        	overflow: auto;
        }


        .subHeader {
            height: 20px;
            width: 100%;
            background-image: url('../../image/header13.png');
            background-repeat: repeat-x;
            color: White;
            vertical-align: middle;
            font-weight: bold;
        }
        
        .subHeader span
        {
            margin-left: 2px;
        }
        
        
        .groupHeader span
        {
            margin-left: 2px;
            margin-top: 4px;
        }
        
        .dvGridDetailsPopup
        {
            display: none;
            border: 0px solid black;
            height: 0px;
            width: 0px;
        }
        
        .ui-widget input
        {
            font-size: 11px;
        }
        
        .ui-widget select
        {
            font-size: 11px;
        }
        
        
        .dvPopupProject
        {
            display: none;
            border: 0px solid black;
            height: 0px;
            width: 0px;
        }
        
        
        .btnSearch
        {
            height: 19px;
            width: 16px;
            background-image: url('../../image/search.png');
            background-repeat: no-repeat;
            background-position: center bottom;
            cursor: pointer;
        }
        
        .dvPopupGLAccount
        {
            display: none;
            border: 0px solid black;
            width: 0px;
            height: 0px;
        }
        
        
        .dvPopupTranType
        {
            display: none;
            border: 0px solid black;
            width: 0px;
            height: 0px;
        }
        
        .dvPopupCashTranInfo
        {
            display: none;
            border: 0px solid black;
            width: 0px;
            height: 0px;
        }
        
        
        
        
        
        .textGLAccCode
        {
        }
        .hdnGLAccID
        {
        }
        .lblGLAccName
        {
        }
        .txtGLAccName
        {
        }
        
        .txtDebit
        {
        }
        .txtCredit
        {
        }
        
        .lblDiffAmt
        {
            color:red;
        }
        
        .dvPopupAccRef
        {
            display: none;
            border: 0px solid black;
            width: 0px;
            height: 0px;
        }
        
        
        .dvPopupAccRefInner
        {
            height: 100%;
            width: 100%;
        }
        
        .dvPopupAccRefHeader
        {
            width: 100%;
            height: 25px;
        }
        
        .dvPopupAccRefContent
        {
            width: 100%;
            height: 200px;
        }
        .dvPopupAccRefContentInner
        {
            width: 100%;
            height: 100%;
            overflow: auto;
        }
        .dvPopupAccRefFooter
        {
            width: 100%;
            height: 20px;
            border-top: 1px solid blue;
        }
        .dvPopupAccRefFooterInner
        {
            width: 100%;
            height: 100%;
            padding-top: 2px;
            text-align: right;
        }
        
        
        .dvPopupIns
        {
            display: none;
            border: 0px solid black;
            width: 0px;
            height: 0px;
        }
        
        .dvPopupInsInner
        {
            height: 100%;
            width: 100%;
        }
        
        .dvPopupInsHeader
        {
            width: 100%;
            height: 25px;
        }
        
        .dvPopupInsContent
        {
            width: 100%;
            height: 200px;
        }
        .dvPopupInsContentInner
        {
            width: 100%;
            height: 100%;
            overflow: auto;
        }
        .dvPopupInsFooter
        {
            width: 100%;
            height: 20px;
            border-top: 1px solid blue;
        }
        .dvPopupInsFooterInner
        {
            width: 100%;
            height: 100%;
            padding-top: 2px;
            text-align: right;
        }
        
        .ui-dialog .ui-dialog-content
        {
            padding: 2px 0px 0px 0px;
        }
        
        .ui-dialog .ui-dialog-titlebar
        {
            padding: 4px 2px 0px 2px;
        }
        
        .tableRowOdd
        {
            background-color: #F7F6F3;
        }
        
        .tableRowEven
        {
            background-color: White;
        }
        
        .dvPopupInsDetails
        {
            display: none;
            border: 0px solid black;
            width: 0px;
            height: 0px;
        }
        
        .dvPopupInsDetailsInner
        {
            height: 100%;
            width: 100%;
        }
        
        .dvPopupInsDetailsHeader
        {
            width: 100%;
            height: 25px;
        }
        
        .dvPopupInsDetailsContent
        {
            width: 100%;
            height: auto;
        }
        .dvPopupInsDetailsContentInner
        {
            width: 100%;
            height: 100%;
            overflow: auto;
        }
        .dvPopupInsDetailsFooter
        {
            width: 100%;
            height: 20px;
            border-top: 1px solid blue;
        }
        .dvPopupInsDetailsFooterInner
        {
            width: 100%;
            height: 100%;
            padding-top: 2px;
            text-align: right;
        }
        
        #Text1
        {
            width: 538px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvPageContent" style="width: 100%; height: 100%;">
        <div id="dvContentHeader" class="dvContentHeader">
            <div id="dvHeader" class="dvHeader">
                <asp:Label ID="lblHeader" CssClass="lblHeader" runat="server" Text="Voucher"></asp:Label>
            </div>
            <div id="dvMsg" runat="server" class="dvMessage" style="width: 100%; min-height: 20px;
                height: auto; text-align: center;">
                <asp:Label ID="lblMessage" runat="server" Font-Size="Small" Width="100%" Height="16px"></asp:Label>
            </div>
        </div>
        <div id="dvContentMain" class="dvContentMain">
            <div id="dvControlsHead" style="height: auto; width: auto; text-align: left; vertical-align: top;">
            </div>
            <div id="dvControls" style="height: auto; width: 100%">
                <div id="dvControlsInner" class="groupBoxContainer boxShadow">
                    <div id="groupBox">
                        <div id="groupHeader" class="groupHeader">
                            <span>Voucher</span>
                        </div>
                        <div id="groupContent" class="groupContent scrollBar">
                            <div id="groupContenInner">
                                <div id="groupDataMaster" style="width: 100%; height: auto;">
                                    <table style="" border="0" cellspacing="2" cellpadding="1">
                                        <tr>
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

                                    <table style="" border="0" cellspacing="2" cellpadding="1">
                                        <tr>
                                          <td style="" >
                                             <table style="" border="0" cellspacing="2" cellpadding="1">
                                                <tr>
                                                    <td>

                                                    </td>

                                                    <td align="right">
                                                        <asp:Label ID="Label5" runat="server" Text="Location:"></asp:Label>
                                                    </td>

                                                    <td colspan="3">
                                                        
                                                        <asp:DropDownList ID="ddlLocation" runat="server" CssClass="dropDownList" Width="150px" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged" AutoPostBack="true">
                                                       </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                
                                                  <tr>
                                                   <td>
                                                  
                                                   
                                                   </td>
                                                    
                                                  <td align="right">
                                                     <asp:Label ID="Label6" runat="server" Text="Voucher Type:"></asp:Label>

                                                  </td>

                                                      <td colspan="3">


                                                       <asp:DropDownList ID="ddlJournalType" runat="server" CssClass="dropDownList" Width="150px"
                                                         AutoPostBack="True" OnSelectedIndexChanged="ddlJournalType_SelectedIndexChanged">
                                                       </asp:DropDownList>

                                                      </td>


                                                 </tr>
                                                 
                                                 
                                                 
                                                 <tr>
                                                  <td>
                                                  </td>
                                                  <td>
                                                       <asp:Label ID="Label8" runat="server" Text="Location:" Visible="false"></asp:Label>
                                                  </td>  
                                                  <td style="padding-left: 2px;">
                                                    <asp:TextBox ID="txtLocationCode" runat="server" CssClass="textBox" Style="width: 42px;" TabIndex="-1" Visible="false"></asp:TextBox>

                                            </td>
                                            <td>
                                                <input id="btnLocationAC" type="button" value="" runat="server" class="buttonDropdown"
                                                    tabindex="-1" visible="false" />
                                             </td>
                         <td>
                             <asp:TextBox ID="txtLocationName" runat="server" CssClass="textBox" Style="width: 125px;" TabIndex="-1" Visible="false"></asp:TextBox>
                         </td>
                      
                                                
                                                </tr>

                                             </table>

                                          </td>

                                            <td align="center" valign="top">
                                                <table style="" border="0" cellspacing="2" cellpadding="1" >
                                                <tr>
                                                  <td>

                                                      </td>
                                              <td align="right">
                                                     <asp:Label ID="Label15" runat="server" Text="Voucher No:"></asp:Label>

                                                  </td>

                                                  <td>
                                                     <asp:TextBox ID="txtJournalNo" runat="server" CssClass="textLabel fldRequired enableIsDirty"
                                                        Style="text-align: left; width: 145px;"></asp:TextBox>
                                                  </td>

                                                    </tr>
                                                    </table>

                                            </td>

                                            <td style="width:80px;">

                                            </td>
                                          <td style="" align="right">
                                             <table style="" border="0" cellspacing="2" cellpadding="1" >
                                                <tr>
                                                  <td>
                                                  
                                                  </td>
                                                  <td align="right">
                                                     <asp:Label ID="Label3" runat="server" Text="Financial Year:"></asp:Label>
                                                  </td>
                                                  <td>
                                                      <asp:DropDownList ID="ddlAccYear" runat="server" CssClass="dropDownList" Width="120px">
                                                      </asp:DropDownList>
                                                  </td>

                                                </tr>
                                                <tr>
                                                  <td>
                                                  </td>
                                                  <td align="right">
                                                      <asp:Label ID="Label7" runat="server" Text="Voucher Date:"></asp:Label>
                                                  </td>

                                                  <td>
                                                     <asp:TextBox ID="txtJournalDate" runat="server" CssClass="textBox textDate dateParse fldRequired enableIsDirty"
                                                        Style="text-align: left;" Width="100px" Enabled="true"></asp:TextBox>
                                                  </td>
                                                </tr>
                                               
                                             </table>
                                          </td>
                                        
                                        </tr>
                                    </table>

                                    <table style="display: none;" border="0" cellspacing="0" cellpadding="1">
                                        <tr>
                                            <td>
                                            </td>
                                            <td style="" align="right" valign="top">
                                                &nbsp;
                                            </td>
                                            <td style="" align="left">
                                                <asp:HiddenField ID="hdnCompanyID" runat="server" Value="0" />
                                                <asp:HiddenField ID="hdnLocationID" runat="server" Value="0" />
                                            </td>
                                            <td>
                                                <asp:HiddenField ID="hdnJournalID" runat="server" Value="0" />
                                                <asp:HiddenField ID="hdnJournalUpdateNo" runat="server" Value="0" />
                                            </td>
                                            <td>
                                                <asp:HiddenField ID="hdnGLGroupClassInclude" runat="server" Value="0" />
                                                <asp:HiddenField ID="hdnGLGroupClassExclude" runat="server" Value="0" />
                                            </td>
                                            <td>
                                               <asp:HiddenField ID="hdnAccSLNoID" runat="server" Value="0" />
                                               <asp:HiddenField ID="hdnEditDataModeInt" runat="server" Value="0" />
                                            </td>
                                        </tr>
                                     
                                        <tr style="display: none; visibility:hidden;">
                                            <td>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="Label18" runat="server" Text="Description:"></asp:Label>
                                            </td>
                                            <td align="left" colspan="3">
                                                <asp:TextBox ID="txtJournalDesc" runat="server" CssClass="textBox enableIsDirty"
                                                    Style="text-align: left;" Width="334px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlJournalAdjustType" runat="server" CssClass="dropDownList"
                                                    Width="50px" Visible="False">
                                                </asp:DropDownList>
                                                <asp:Label ID="lblPosted" runat="server" Text="-" Visible="False"></asp:Label>
                                            </td>
                                            <td align="right">
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div id="groupDataDetails" style="width: 100%; height: auto;">
                                    <div style="height:5px;"></div>
                                    <div id="groupDataHeaderDebit" class="" style="width: 100%; text-align: center;">
                                        <span style="font-weight: bold">Debits:     </span>
                                     </div>
                                    <div id="dvGridContainer" class="" style="width: auto; height: auto; text-align: left">
                                        <div id="dvGridHeader" style="width: 100%; height: 25px; font-size: smaller;" class="subHeader">
                                            <table style="height: 100%; color: #FFFFFF; font-weight: bold; text-align: center;"
                                                class="defFont" cellspacing="1" cellpadding="1">
                                                <tr class="headerRow">
                                                    <td width="28px" class="headerColLeft">
                                                        SL#
                                                    </td>
                                                    <td width="80px" class="headerColLeft">
                                                        GL Head
                                                    </td>
                                                    <td width="100px" class="headerColLeft">
                                                        Sub Ledger
                                                    </td>
                                                    <td width="80px" class="headerColLeft" style="display: none;">
                                                        Tran. Code
                                                    </td>
                                                    <td width="191px" class="headerColLeft">
                                                        Narration
                                                    </td>
                                                    <td width="101px" class="headerColRight">
                                                        Debit
                                                    </td>
                                                    <td width="81px" class="headerColCenter">
                                                        Instrument
                                                    </td>
                                                    <td width="81px" class="headerColCenter">
                                                        Cost Center
                                                    </td>
                                                    <td width="81px" class="headerColCenter">
                                                        Reference
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div id="dvGrid" style="width: 100%; height: 100px; overflow: auto;" class="dvGrid">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowHeader="False"
                                                        CellPadding="1" CellSpacing="1" ForeColor="#333333" GridLines="None" DataKeyNames="JournalDetID"
                                                        OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand" OnRowDeleting="GridView1_RowDeleting"
                                                        OnRowCreated="GridView1_RowCreated" EnableModelValidation="True" ClientIDMode="AutoID">
                                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="SL#">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSLNo" runat="server" Text='<%# Bind("JournalDetSLNo") %>' Style="text-align: center;"
                                                                        Width="28px">
                                                                    </asp:Label>
                                                                    <asp:HiddenField ID="hdnJournalDetID" runat="server" Value='<%# Bind("JournalDetID") %>' />
                                                                    <asp:HiddenField ID="hdnJournalDetID_Link" runat="server" Value='<%# Bind("JournalDetID_Link") %>' />
                                                                    <asp:HiddenField ID="hdnRecordStateInt" runat="server" Value='<%# Bind("_RecordStateInt") %>' />
                                                                    <asp:HiddenField ID="hdnDrCr" runat="server" Value='<%# Bind("DrCr") %>' />
                                                                </ItemTemplate>
                                                                <ItemStyle VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="GL Group" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <div>
                                                                        <table border="0" cellpadding="1" cellspacing="1">
                                                                            <tbody>
                                                                                <tr style="vertical-align:top">
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtGLGroupCode" runat="server" CssClass="textBox textAutoSelect"
                                                                                            Width="60" Text='<%# Bind("GLGroupCode") %>'></asp:TextBox>
                                                                                        <asp:HiddenField ID="hdnGLGroupID" runat="server" Value='<%# Bind("GLGroupID") %>' />
                                                                                        <asp:HiddenField ID="hdnGLGroupIDEdit" runat="server" Value='<%# Bind("GLGroupIDEdit") %>' />
                                                                                        <asp:HiddenField ID="hdnGLGroupClassID" runat="server" Value='<%# Bind("GLGroupClassID") %>' />
                                                                                        <asp:HiddenField ID="hdnIsInstrument" runat="server" Value='<%# (Boolean.Parse(Eval("IsInstrument").ToString())) ? "1" : "0" %>' />
                                                                                    </td>
                                                                                    <td>
                                                                                        <input id="btnGLGroupAC" type="button" value="" runat="server" class="buttonDropdown"
                                                                                            tabindex="-1" />
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtGLAccountCode" runat="server" CssClass="textBox textAutoSelect"
                                                                                            Width="80" Text='<%# Bind("GLAccountCode") %>'></asp:TextBox>
                                                                                        <asp:HiddenField ID="hdnGLAccountID" runat="server" Value='<%# Bind("GLAccountID") %>' />
                                                                                        <asp:HiddenField ID="hdnGLAccountIDEdit" runat="server" Value='<%# Bind("GLAccountIDEdit") %>' />
                                                                                        <asp:HiddenField ID="hdnGLGroupIDAcc" runat="server" Value='<%# Bind("GLGroupID") %>' />
                                                                                    </td>
                                                                                    <td>
                                                                                        <input id="btnGLAccountAC" type="button" value="" runat="server" class="buttonDropdown"
                                                                                            tabindex="-1" />
                                                                                    </td>
                                                                                    <td style="display: none;">
                                                                                        <asp:TextBox ID="txtTranType" runat="server" CssClass="textBox textAutoSelect" Width="60"
                                                                                            Text='<%# Bind("TranTypeCode") %>'></asp:TextBox>
                                                                                        <asp:HiddenField ID="hdnTranTypeID" runat="server" Value='<%# Bind("TranTypeID") %>' />
                                                                                        <asp:HiddenField ID="hdnTranTypeCategoryID" runat="server" Value='<%# Bind("TranTypeCategoryID") %>' />
                                                                                    </td>
                                                                                    <td style="display: none;">
                                                                                        <input id="btnTranTypeAC" type="button" value="" runat="server" class="buttonDropdown"
                                                                                            tabindex="-1" />
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtJournalDetDesc" runat="server" CssClass="textBox textAutoSelect  "
                                                                                            Width="190" Text='<%# Bind("JournalDetDesc") %>' Style="" ></asp:TextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtDebitAmt" runat="server" CssClass="textBox txtAmount txtDebit textNumberOnly textCurrencyFormat textAutoSelect"
                                                                                            Width="100" Text='<%# Bind("DebitAmt", "{0:#0.00}") %>' Style="text-align: right;"></asp:TextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtInstrument" runat="server" CssClass="textBoxReadOnlyEdit textAutoSelect"
                                                                                            Width="60" Style="text-align: center;" Text='<%# Bind("InstrumentText") %>'></asp:TextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                        <input id="btnInstrument" type="button" value="" runat="server" class="buttonListIcon"
                                                                                            tabindex="-1" title="Instrument List" />
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtCostCenter" runat="server" CssClass="textBoxReadOnlyEdit textAutoSelect"
                                                                                            Width="60" Style="text-align: center; font-size: 8pt;" Text='<%# Bind("CostCenterText") %>'></asp:TextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                        <input id="btnCostCenter" type="button" value="" runat="server" class="buttonListIcon"
                                                                                            tabindex="-1" title="Cost Center Allocation" />
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtReference" runat="server" CssClass="textBoxReadOnlyEdit textAutoSelect"
                                                                                            Width="60" Style="text-align: center;" Text='<%# Bind("ReferenceText") %>'></asp:TextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                        <input id="btnReference" type="button" value="" runat="server" class="buttonListIcon"
                                                                                            tabindex="-1" title="Reference Allocation" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr style="height: 22px;">
                                                                                    <td colspan="2">
                                                                                        <asp:TextBox ID="txtGLGroupName" Width="78" runat="server" Text='<%# Bind("GLGroupNameShort") %>'
                                                                                            CssClass="textLabel" TabIndex="-1"></asp:TextBox>
                                                                                    </td>
                                                                                    <td colspan="4">
                                                                                        <asp:TextBox ID="txtGLAccountName" Width="250" runat="server" Text='<%# Bind("GLAccountName") %>'
                                                                                            CssClass="textLabel" TabIndex="-1"></asp:TextBox>
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
                                                                    <div style="overflow: visible;">
                                                                    </div>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="10" />
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete" ShowHeader="false">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnDeleteRow" CssClass="buttonDeleteIcon" Height="16px" Width="16px"
                                                                        ToolTip="Delete" CommandName="delete" runat="server">
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField Visible="false">
                                                                <ItemTemplate>
                                                                    <div style="width: 10px;">
                                                                        <div>
                                                                            <div style="background-position: right center; height: 25px; cursor: pointer; background-image: url('../image/more.png');
                                                                                background-repeat: no-repeat; text-align: left; vertical-align: middle;" onclick="togglePannelStatus(this)"
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
                                                    <input id="hdnJournalDetRefJson" type="hidden" runat="server" value="[]" />
                                                    <input id="hdnJournalDetInsJson" type="hidden" runat="server" value="[]" />
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnNewRow" EventName="Click" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div id="dvGridControls" style="width: 100%; height: 25px; border-top: solid 1px #C0C0C0;">
                                            <table style="width: auto; height: 100%; text-align: center;" cellspacing="1" cellpadding="1"
                                                border="0">
                                                <tr>
                                                    <td style="width: 2px">
                                                    </td>
                                                   
                                                     <td style="width: 10px">
                                                    </td>

                                                        <td style="width:120px;">

                                                        </td>


                                                    <td style="width: 200px; text-align:left">
                                                        <asp:Label ID="lblTotalRowDr" runat="server" Text="Total Row : 0" Font-Bold="True"></asp:Label>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Label ID="Label1" runat="server" Text="Total Debit:" Font-Bold="True"></asp:Label>
                                                    </td>

                                                    <td style="width: 50px">
                                                        <asp:TextBox ID="txtTotDebitAmt" runat="server" CssClass="textBox" Style="text-align: right;"
                                                            Width="100" TabIndex="-1" Font-Bold="True"></asp:TextBox>
                                                    </td>

                                                    <td align="right" style="width:5px;">
                                                        
                                                    </td>

                                                    <td align="left" style="">
                                                        <asp:Label ID="lblDiffAmtDr" CssClass="lblDiffAmt" runat="server" Text=""></asp:Label>
                                                    </td>

                                                    <td style="width:120px;">


                                                    </td>

                                                    
                                                    <td style="width: 20px;">
                                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                                            DisplayAfter="300">
                                                            <ProgressTemplate>
                                                                <asp:Image ID="imgProgress" runat="server" ImageUrl="~/image/loader.gif" />
                                                            </ProgressTemplate>
                                                        </asp:UpdateProgress>
                                                    </td>

                                                    <td style="width: 90px" align="left">
                                                        <asp:Button ID="btnNewRow" runat="server" CssClass="buttonNewRow" Text="" OnClick="btnNewRow_Click"
                                                            OnClientClick="ShowProgress();" />
                                                    </td>

                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                    <div id="dvGridSeparator" style="width: 100%; height: 15px;">
                                    </div>
                                    <div id="groupDataHeaderCredit" class="" style="width: 100%; text-align: center;">
                                        <span style="font-weight: bold">Credits: </span>
                                    </div>
                                    <div id="dvGridContainer2" class="" style="width: auto; height: auto; text-align: left">
                                        <div id="dvGridHeader2" style="width: 100%; height: 25px; font-size: smaller;" class="subHeader">
                                            <table style="height: 100%; color: #FFFFFF; font-weight: bold; text-align: center;"
                                                class="defFont" cellspacing="1" cellpadding="1">
                                                <tr class="headerRow">
                                                    <td width="28px" class="headerColLeft">
                                                        SL#
                                                    </td>
                                                    <td width="80px" class="headerColLeft">
                                                        GL Head
                                                    </td>
                                                    <td width="100px" class="headerColLeft">
                                                        Sub Ledger
                                                    </td>
                                                    <td width="80px" class="headerColLeft" style="display: none;">
                                                        Tran. Code
                                                    </td>
                                                    <td width="191px" class="headerColLeft">
                                                        Narration
                                                    </td>
                                                    <td width="101px" class="headerColRight">
                                                        Credit
                                                    </td>
                                                    <td width="81px" class="headerColCenter">
                                                        Instrument
                                                    </td>
                                                    <td width="81px" class="headerColCenter">
                                                        Cost Center
                                                    </td>
                                                    <td width="81px" class="headerColCenter">
                                                        Reference
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div id="dvGrid2" style="width: 100%; height: 100px; overflow: auto;" class="dvGrid">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" ShowHeader="False"
                                                        CellPadding="1" CellSpacing="1" ForeColor="#333333" GridLines="None" DataKeyNames="JournalDetID"
                                                        OnRowDataBound="GridView2_RowDataBound" OnRowCommand="GridView2_RowCommand" OnRowDeleting="GridView2_RowDeleting"
                                                        OnRowCreated="GridView2_RowCreated" EnableModelValidation="True" ClientIDMode="AutoID">
                                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="SL#">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSLNo" runat="server" Text='<%# Bind("JournalDetSLNo") %>' Style="text-align: center;"
                                                                        Width="28px">
                                                                    </asp:Label>
                                                                    <asp:HiddenField ID="hdnJournalDetID" runat="server" Value='<%# Bind("JournalDetID") %>' />
                                                                    <asp:HiddenField ID="hdnJournalDetID_Link" runat="server" Value='<%# Bind("JournalDetID_Link") %>' />
                                                                    <asp:HiddenField ID="hdnRecordStateInt" runat="server" Value='<%# Bind("_RecordStateInt") %>' />
                                                                    <asp:HiddenField ID="hdnDrCr" runat="server" Value='<%# Bind("DrCr") %>' />
                                                                </ItemTemplate>
                                                                <ItemStyle VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="GL Account" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <div>
                                                                        <table border="0" cellpadding="1" cellspacing="1">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtGLGroupCode" runat="server" CssClass="textBox textAutoSelect"
                                                                                            Width="60" Text='<%# Bind("GLGroupCode") %>'></asp:TextBox>
                                                                                        <asp:HiddenField ID="hdnGLGroupID" runat="server" Value='<%# Bind("GLGroupID") %>' />
                                                                                        <asp:HiddenField ID="hdnGLGroupIDEdit" runat="server" Value='<%# Bind("GLGroupIDEdit") %>' />
                                                                                        <asp:HiddenField ID="hdnGLGroupClassID" runat="server" Value='<%# Bind("GLGroupClassID") %>' />
                                                                                        <asp:HiddenField ID="hdnIsInstrument" runat="server" Value='<%# (Boolean.Parse(Eval("IsInstrument").ToString())) ? "1" : "0" %>' />
                                                                                    </td>
                                                                                    <td>
                                                                                        <input id="btnGLGroupAC" type="button" value="" runat="server" class="buttonDropdown"
                                                                                            tabindex="-1" />
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtGLAccountCode" runat="server" CssClass="textBox textAutoSelect"
                                                                                            Width="80" Text='<%# Bind("GLAccountCode") %>'></asp:TextBox>
                                                                                        <asp:HiddenField ID="hdnGLAccountID" runat="server" Value='<%# Bind("GLAccountID") %>' />
                                                                                        <asp:HiddenField ID="hdnGLAccountIDEdit" runat="server" Value='<%# Bind("GLAccountIDEdit") %>' />
                                                                                        <asp:HiddenField ID="hdnGLGroupIDAcc" runat="server" Value='<%# Bind("GLGroupID") %>' />
                                                                                    </td>
                                                                                    <td>
                                                                                        <input id="btnGLAccountAC" type="button" value="" runat="server" class="buttonDropdown"
                                                                                            tabindex="-1" />
                                                                                    </td>
                                                                                    <td style="display: none;">
                                                                                        <asp:TextBox ID="txtTranType" runat="server" CssClass="textBox textAutoSelect" Width="60"
                                                                                            Text='<%# Bind("TranTypeCode") %>'></asp:TextBox>
                                                                                        <asp:HiddenField ID="hdnTranTypeID" runat="server" Value='<%# Bind("TranTypeID") %>' />
                                                                                        <asp:HiddenField ID="hdnTranTypeCategoryID" runat="server" Value='<%# Bind("TranTypeCategoryID") %>' />
                                                                                    </td>
                                                                                    <td style="display: none;">
                                                                                        <input id="btnTranTypeAC" type="button" value="" runat="server" class="buttonDropdown"
                                                                                            tabindex="-1" />
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtJournalDetDesc" runat="server" CssClass="textBox textAutoSelect"
                                                                                            Width="190" Text='<%# Bind("JournalDetDesc") %>' Style=""></asp:TextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtCreditAmt" runat="server" CssClass="textBox txtAmount txtCredit textNumberOnly textCurrencyFormat textAutoSelect"
                                                                                            Width="100" Text='<%# Bind("CreditAmt", "{0:#0.00}") %>' Style="text-align: right;"></asp:TextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtInstrument" runat="server" CssClass="textBoxReadOnlyEdit textAutoSelect"
                                                                                            Width="60" Style="text-align: center;" Text='<%# Bind("InstrumentText") %>'></asp:TextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                        <input id="btnInstrument" type="button" value="" runat="server" class="buttonListIcon"
                                                                                            tabindex="-1" title="Instrument List" />
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtCostCenter" runat="server" CssClass="textBoxReadOnlyEdit textAutoSelect"
                                                                                            Width="60" Style="text-align: center; font-size: 8pt;" Text='<%# Bind("CostCenterText") %>'></asp:TextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                        <input id="btnCostCenter" type="button" value="" runat="server" class="buttonListIcon"
                                                                                            tabindex="-1" title="Cost Center Allocation" />
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtReference" runat="server" CssClass="textBoxReadOnlyEdit textAutoSelect"
                                                                                            Width="60" Style="text-align: center;" Text='<%# Bind("ReferenceText") %>'></asp:TextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                        <input id="btnReference" type="button" value="" runat="server" class="buttonListIcon"
                                                                                            tabindex="-1" title="Reference Allocation" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr style="height: 22px;">
                                                                                    <td colspan="2">
                                                                                        <asp:TextBox ID="txtGLGroupName" Width="80" runat="server" Text='<%# Bind("GLGroupNameShort") %>'
                                                                                            CssClass="textLabel" TabIndex="-1"></asp:TextBox>
                                                                                    </td>
                                                                                    <td colspan="4">
                                                                                        <asp:TextBox ID="txtGLAccountName" Width="250" runat="server" Text='<%# Bind("GLAccountName") %>'
                                                                                            CssClass="textLabel" TabIndex="-1"></asp:TextBox>
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
                                                                    <div style="overflow: visible;">
                                                                    </div>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="10" />
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete" ShowHeader="false">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnDeleteRow" CssClass="buttonDeleteIcon" Height="16px" Width="16px"
                                                                        CommandName="delete" runat="server">
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField Visible="false">
                                                                <ItemTemplate>
                                                                    <div style="width: 10px;">
                                                                        <div>
                                                                            <div style="background-position: right center; height: 25px; cursor: pointer; background-image: url('../image/more.png');
                                                                                background-repeat: no-repeat; text-align: left; vertical-align: middle;" onclick="togglePannelStatus(this)"
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
                                                    <input id="hdnJournalDetRefJson2" type="hidden" runat="server" value="[]" />
                                                    <input id="hdnJournalDetInsJson2" type="hidden" runat="server" value="[]" />
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnNewRow2" EventName="Click" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div id="divGridControls2" style="width: 100%; height: 25px; border-top: solid 1px #C0C0C0;">
                                            <table style="width: auto; height: 100%; text-align: center;" cellspacing="1" cellpadding="1"
                                                border="0">
                                                <tr>
                                                    <td style="width: 2px">
                                                    </td>
                                                    <td style="width: 90px" align="left">
                                                        <asp:Button ID="btnNewRow2" runat="server" CssClass="buttonNewRow" Text="" OnClick="btnNewRow2_Click"
                                                            OnClientClick="ShowProgress2();" />
                                                    </td>
                                                    <td style="width: 20px;">
                                                        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel2"
                                                            DisplayAfter="300">
                                                            <ProgressTemplate>
                                                                <asp:Image ID="imgProgress2" runat="server" ImageUrl="~/image/loader.gif" />
                                                            </ProgressTemplate>
                                                        </asp:UpdateProgress>
                                                    </td>
                                                    <td style="width: 10px">

                                                    </td>
                                                    <td style="width: 190px; text-align:left;">
                                                         <asp:Label ID="lblTotalRowCr" runat="server" Text="Total Row : 0" Font-Bold="True"></asp:Label>
                                                    </td>
                                                    <td align="right">
                                                        &nbsp;
                                                    </td>
                                                    <td align="right">
                                                        <asp:Label ID="Label4" runat="server" Text="Total Credit:" Font-Bold="True"></asp:Label>
                                                    </td>
                                                    <td align="right">
                                                        <asp:TextBox ID="txtTotCreditAmt" runat="server" CssClass="textBox" Style="text-align: right;"
                                                            Width="100" TabIndex="-1" Font-Bold="True"></asp:TextBox>
                                                    </td>
                                                    <td align="right" style="width:5px;">
                                                       
                                                    </td>
                                                    <td style="">
                                                        <asp:Label ID="lblDiffAmtCr" CssClass="lblDiffAmt" runat="server" Text=""></asp:Label>
                                                    </td>
                                                    <td style="">
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="dvControlsFooter" style="height: auto; width: auto">
                <div style="height: 10px;">
                </div>
            </div>
        </div>
        <div id="dvContentFooter" class="dvContentFooter">
            <table>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:Button ID="btnAddNew" runat="server" Text="New" CssClass="buttonNew" OnClick="btnAddNew_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonCancel checkIsDirty"
                            OnClick="btnCancel_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonSave checkRequired"
                            OnClick="btnSave_Click" AccessKey="s" />
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="buttonEdit" OnClick="btnEdit_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnPost" runat="server" Text="Post" CssClass="buttoncommon" OnClick="btnPost_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="buttonDelete" OnClick="btnDelete_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" CssClass="buttonRefresh checkIsDirty"
                            OnClick="btnRefresh_Click" />
                    </td>
                    <td>
                        <uc:PrintButton runat="server" ID="ucPrintButton" DefaultPrintAction="Preview" AutoPrint="False" />
                    </td>
                    <td>
                        <input id="btnClose" type="button" runat="server" class="buttonClose" value="Close"
                            onclick="if (ContentForm){ ContentForm.CloseForm();}" />
                    </td>
                    <td>
                    </td>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="Print Format:"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlReportFormat" runat="server" CssClass="dropDownList" Width="100px">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="btnPopupTrigger" runat="server" Text="Button" CssClass="buttonHidden"
                            OnClick="btnPopupTrigger_Click" />
                        <asp:HiddenField ID="hdnPopupTriggerID" runat="server" Value="" />
                        <asp:HiddenField ID="hdnPopupCommand" runat="server" Value="" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    
    <div id="dvPopupAccRef" class="dvPopupAccRef" runat="server">
        <div class="dvPopupAccRefInner">
            <div class="dvPopupAccRefHeader subHeader ui-corner-all" style="font-size: smaller;">
                <table style="height: 100%; color: #FFFFFF; font-weight: bold; text-align: center;"
                    class="defFont" cellspacing="1" cellpadding="1">
                    <tr class="headerRow">
                        <td width="30px" class="headerColLeft">
                            SL#
                        </td>
                        <td width="106px" class="headerColLeft">
                            Category
                        </td>
                        <td width="106px" class="headerColLeft colHeaderCode">
                            Cost Center
                        </td>
                        <td width="126px" class="headerColLeft">
                            Name
                        </td>
                        <td width="102px" class="headerColRight">
                            Amount
                        </td>
                    </tr>
                </table>
            </div>
            <div class="dvPopupAccRefContent">
                <div class="dvPopupAccRefContentInner">
                    <table class="tblAccRef" style="color: #FFFFFF; text-align: center;" cellspacing="1"
                        cellpadding="1">
                    </table>
                </div>
            </div>
            <div class="dvPopupAccRefFooter ">
                <div class="dvPopupAccRefFooterInner">
                    <table align="right">
                        <tr>
                            <td>
                            </td>
                            <td>
                                <input class="btnPopupAccRefNewRow buttonNewRow" type="button" value="" />
                            </td>
                            <td style="width: 240px;">
                            </td>
                            <td>
                                <input class="btnPopupAccRefOK buttonOK" type="button" value="OK" />
                            </td>
                            <td>
                                <input class="btnPopupAccRefCancel buttonCancel stopEnterToTab" type="button" value="Cancel" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
    <div class="dvAccRefTamplate" style="height: 0px; width: 0px; display: none;">
        <table class="tblAccRefTemplate" style="color: #FFFFFF; font-weight: bold; text-align: center;"
            cellspacing="1" cellpadding="1">
            <tr class="trAccRefTemplate rowAccRef">
                <td valign="top" align="center" style="width: 30px;">
                    <span class="spnAccRefPopupSLNo" style="color: Black;"></span>
                    <input class="hdnAccRefPopupLinkID" type="hidden" value="0" tabindex="-1" />
                    <input class="hdnAccRefPopupJournalDetID" type="hidden" value="0" tabindex="-1" />
                    <input class="hdnAccRefPopupJournalDetRefID" type="hidden" value="0" tabindex="-1" />
                    <input class="hdnRecordState" type="hidden" value="0" tabindex="-1" />
                </td>
                
                <td style="width: 90px;">
                    <input type="text" class="txtAccRefPopupCategory textBox textAutoSelect " style="width: 88px;" />
                    <input class="hdnAccRefPopupCategoryID" type="hidden" value="0" />
                </td>
                <td>
                    <input type="button" class="btnAccRefPopupCategoryAC buttonDropdown" value="" tabindex="-1" />
                </td>


                <td style="width: 90px;">
                    <input type="text" class="txtAccRefPopupAccRef textBox textAutoSelect " style="width: 88px;" />
                    <input class="hdnAccRefPopupAccRefID" type="hidden" value="0" />
                </td>
                <td>
                    <input type="button" class="btnAccRefPopupAccRefAC buttonDropdown" value="" tabindex="-1" />
                </td>


                <td>
                    <input type="text" readonly="readonly" tabindex="-1" class="txtAccRefPopupAccRefName textBoxReadOnlyEdit"
                        style="width: 124px;" />
                </td>
                <td>
                    <input type="text" class="txtAccRefPopupAmount textBox textNumberOnly textCurrencyFormat textAutoSelect textGridNav"
                        style="width: 100px" />
                </td>
                <td>
                    <input type="button" class="btnAccRefPopupDelete buttonDeleteGrid" value="" tabindex="-1" />
                </td>
            </tr>
        </table>
    </div>
    <div id="dvPopupIns" class="dvPopupIns" runat="server">
        <div class="dvPopupInsInner">
            <div class="dvPopupInsHeader subHeader ui-corner-all" style="font-size: smaller;">
                <table style="height: 100%; color: #FFFFFF; font-weight: bold; text-align: center;"
                    class="defFont" cellspacing="1" cellpadding="1">
                    <tr class="headerRow">
                        <td width="30px" class="headerColLeft">
                            SL#
                        </td>
                        <td width="100px" class="headerColLeft colHeaderType">
                            Type
                        </td>
                        <td width="190px" class="headerColLeft">
                            Instrument
                        </td>
                        <td width="102px" class="headerColRight">
                            Amount
                        </td>
                    </tr>
                </table>
            </div>
            <div class="dvPopupInsContent">
                <div class="dvPopupInsContentInner">
                    <table class="tblIns" style="color: #FFFFFF; text-align: center;" cellspacing="1"
                        cellpadding="1">
                    </table>
                </div>
            </div>
            <div class="dvPopupInsFooter ">
                <div class="dvPopupInsFooterInner">
                    <table align="right">
                        <tr>
                            <td>
                                <input class="hdnPopupInsModeIDParent" type="hidden" value="0" tabindex="-1" />
                            </td>
                            <td>
                                <input class="btnPopupInsNewRow buttonNewRow" type="button" value="" />
                            </td>
                            <td style="width: 140px;">
                            </td>
                            <td>
                                <input class="btnPopupInsOK buttonOK" type="button" value="OK" />
                            </td>
                            <td>
                                <input class="btnPopupInsCancel buttonCancel stopEnterToTab" type="button" value="Cancel" />
                            </td>
                        </tr>
                    </table>
                    <div style="width: 100%; height: 5px;">
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="dvInsTamplate" style="height: 0px; width: 0px; display: none;">
        <table class="tblInsTemplate" style="color: #FFFFFF; font-weight: bold; text-align: center;"
            cellspacing="1" cellpadding="1">
            <tr class="trInsTemplate rowIns">
                <td valign="top" align="center" style="width: 30px;">
                    <span class="spnInsPopupSLNo" style="color: Black;"></span>
                    <input class="hdnInsPopupLinkID" type="hidden" value="0" tabindex="-1" />
                    <input class="hdnInsJournalDetID" type="hidden" value="0" tabindex="-1" />
                    <input class="hdnInsPopupJournalDetInsID" type="hidden" value="0" tabindex="-1" />
                    <input class="hdnRecordState" type="hidden" value="0" tabindex="-1" />
                    <input class="hdnInsPopupInsLinkTypeID" type="hidden" value="0" tabindex="-1" />
                </td>
                <td>
                    <div>
                        <table>
                            <tr>
                                <td>
                                    <select id="ddlInstrumentType" runat="server" class="dropDownList ddlInsPopupInsType"
                                        style="width: 100px;">
                                    </select>
                                </td>
                                <td>
                                    <input type="text" class="txtInsPopupIns textBox textAutoSelect " style="width: 150px;" />
                                    <input class="hdnInsPopupInsID" type="hidden" value="0" tabindex="-1" />
                                    <input class="hdnInsPopupInsModeID" type="hidden" value="2" tabindex="-1" />
                                </td>
                                <td>
                                    <input type="button" class="btnInsPopupInsAC buttonDropdown" value="" tabindex="-1" />
                                </td>
                                <td>
                                    <input type="button" class="btnInsPopupInsDet buttonEditIcon" value="" tabindex="-1" />
                                </td>
                                <td>
                                    <input type="text" class="txtInsPopupAmount textBox textNumberOnly textCurrencyFormat textAutoSelect"
                                        style="width: 100px" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div>
                        <table>
                        </table>
                    </div>
                </td>
                <td>
                    <input type="button" class="btnInsPopupDelete buttonDeleteGrid" value="" tabindex="-1" />
                </td>
            </tr>
        </table>
    </div>
    <div id="dvPopupInsDetails" class="dvPopupInsDetails" runat="server">
        <div class="dvPopupInsDetailsInner">
            <div class="dvPopupInsDetialsContent">
                <table cellpadding="1" cellspacing="2">
                    <tr class="rowInsDetTransType">
                        <td style="width: 5px;">
                        </td>
                        <td>
                            <span class="label lblInsPopupType">Type</span>
                        </td>
                        <td>
                            <select id="ddlInsDetInsType" class="ddlInsDetInsType dropDownList" runat="server"
                                style="width: 122px;">
                            </select>
                            <input type="hidden" class="hdnInsDetInsModeID" value="2" />
                        </td>
                    </tr>
                    <tr class="rowInsDetInsNo">
                        <td>
                        </td>
                        <td>
                            <span class="label lblInsPopupInsNo">Ins. No</span>
                        </td>
                        <td>
                            <input type="text" class="txtInsDetInsNo textBox" style="width: 250px;" />
                            <input type="hidden" class="hdnInsDetInsID" value="0" />
                        </td>
                    </tr>
                    <tr class="rowInsDetInsDate">
                        <td>
                        </td>
                        <td>
                            <span class="label lblInsPopupInsDate">Ins. Date</span>
                        </td>
                        <td>
                            <input type="text" class="txtInsDetInsDate textBox textDate dateParse" style="width: 80px;" />
                        </td>
                    </tr>
                    <tr class="rowInsDetIssueName">
                        <td>
                        </td>
                        <td>
                            <span class="label lblInsPopupIssueName">Issue To</span>
                        </td>
                        <td>
                            <input type="text" class="txtInsDetIssueName textBox" style="width: 250px;" />
                        </td>
                    </tr>
                    <tr class="rowInsDetBankName">
                        <td>
                        </td>
                        <td>
                            <span class="label lblInsPopupBankName">Bank Name</span>
                        </td>
                        <td>
                            <input type="text" class="txtInsDetBankName textBox" style="width: 250px;" />
                        </td>
                    </tr>
                    <tr class="rowInsDetBranchName">
                        <td>
                        </td>
                        <td>
                            <span class="label lblInsPopupBranchName">Branch Name</span>
                        </td>
                        <td>
                            <input type="text" class="txtInsDetBranchName textBox" style="width: 250px;" />
                        </td>
                    </tr>
                    <tr class="rowInsDetInsAmt">
                        <td>
                        </td>
                        <td>
                            <span class="label lblInsPopupInsAmt">Ins. Amount</span>
                        </td>
                        <td>
                            <input type="text" class="txtInsDetInsAmt textBox textNumberOnly textCurrencyFormat textAutoSelect"
                                style="width: 120px;" />
                        </td>
                    </tr>
                    <tr class="rowInsDetInsStatus">
                        <td>
                        </td>
                        <td>
                            <span class="label lblInsPopupInsStatus">Status</span>
                        </td>
                        <td>
                            <table cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <select id="ddlInsDetInsStatus" class="ddlInsDetInsStatus dropDownList" runat="server"
                                            style="width: 122px;">
                                        </select>
                                    </td>
                                    <td style="width: 2px;">
                                    </td>
                                    <td>
                                        <span class="label lblInsPopupInsStatusDate">Date</span>
                                    </td>
                                    <td style="width: 2px;">
                                    </td>
                                    <td>
                                        <input type="text" class="txtInsDetInsStatusDate textBox textDate dateParse" style="width: 80px;" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr class="rowInsDetInsRemarks">
                        <td>
                        </td>
                        <td>
                            <span class="label lblInsPopupInsRemarks">Remarks</span>
                        </td>
                        <td>
                            <input type="text" class="txtInsDetInsRemarks textBox" style="width: 250px;" />
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
            <div class="dvPopupInsDetailsFooter ">
                <div class="dvPopupInsDetailsFooterInner">
                    <table align="right">
                        <tr>
                            <td>
                            </td>
                            <td>
                                <input class="btnPopupInsDetailsNew buttonNew" type="button" value="New" />
                            </td>
                            <td style="width: 140px;">
                            </td>
                            <td>
                                <input class="btnPopupInsDetailsOK buttonOK" type="button" value="OK" />
                            </td>
                            <td>
                                <input class="btnPopupInsDetialsCancel buttonCancel stopEnterToTab" type="button"
                                    value="Cancel" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
