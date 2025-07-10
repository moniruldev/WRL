<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="JournalFull.aspx.cs" Inherits="PG.Web.Accounting.GeneralLedger.JournalFull" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="../../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
    <link href="../../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />
    <script src="../../javascript/pg.logintask.js?lj" type="text/javascript"></script>
    <script src="../../javascript/jquery-watch.js" type="text/javascript"></script>


   <script type="text/javascript">
        // <!CDATA[
       var isPageResize = true;
       var journalID = 0;
       var tempJournalDetID = 0;

       ContentForm.CalendarImageURL = '../../image/calendar.png';

       var lblHeader = '<%=lblHeader.ClientID%>';
       var lblMessage = '<%=lblMessage.ClientID%>';

       var hdnCompanyID = '<%=hdnCompanyID.ClientID%>';
       var hdnJournalID = '<%=hdnJournalID.ClientID%>';
       var ddlAccYear = '<%=ddlAccYear.ClientID%>';
       var rblJournalType = '<%=rblJournalType.ClientID%>';

       var GLAccountServiceLink = '<%=this.GLAccountServiceLink%>';
       var GLGroupServiceLink = '<%=this.GLGroupServiceLink%>';
       var AccRefServiceLink = '<%=this.AccRefServiceLink%>';
       var InstrumentGetServiceLink = '<%=this.InstrumentGetServiceLink%>';
       var InstrumentUpdateServiceLink = '<%=this.InstrumentUpdateServiceLink%>';

       var GetJournalServiceLink = '<%=this.GetJournalServiceLink%>';
       var GetJournalListServiceLink = '<%=this.GetJournalListServiceLink%>';
       var UpdateJournalServiceLink = '<%=this.UpdateJournalServiceLink%>';
       
       var ReportGeneratePageLink = '<%=this.ReportGeneratePageLink%>';
       var ReportViewPageLink = '<%=this.ReportViewPageLink%>';

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

       function getThisJournalID() {
           var jID = parseInt($('#' + hdnJournalID).val());
           return jID;
       }


       function bindButtonEvents() {
           
           $('#btnAddNew').click(function (e) {
               AddTask();

           });

           $('#btnEdit').click(function (e) {
               EditTask();
           });

           $('#btnDelete').click(function (e) {
               DeleteTask();
           });

           $('#btnSave').click(function (e) {
               SaveTask();
           });

           $('#btnCancel').click(function (e) {
               CancelTask();
           });

           $('#btnJournalPost').click(function (e) {
               JournalPostTask();
           });

       }


       function bindJournalTypePopup() {
           $('#dvPopupJournalType').dialog({
               title: "Select Journal Type",
               autoOpen: false,
               resizable: false,
               modal: true,
               position: 'center',
               closeOnEscape: true,
               top: 0,
               left: 0,
               width: 250,
               height: 300,
               open: function (event, ui) {
                   //self.$popupDialog.parent().appendTo(jQuery("form:first"));
                   // $('#' + curTxtAccRefCodeID).closest('table').closest('tr').addClass('gridSelectedRow');
                   //$(ui.element).closest('table').closest('tr').addClass('gridSelectedRow');
                   var curJTypeID = parseInt($('#hdnJournalTypeID').val());
                   if (curJTypeID > 0) {
                       // $(':radio[value="' + selectedValue + '"]').attr('checked', 'checked');
                       $('#dvPopupJournalType').find(':radio[value="' + curJTypeID + '"]').attr('checked', 'checked');
                   }
               },
               close: function (event, ui) {
                   //$('#' + curTxtAccRefCodeID).closest('table').closest('tr').removeClass('gridSelectedRow');
               }
           });

           $('#dvPopupJournalType').find('.btnPopupJournalTypeOK').click(function (e) {
               //alert('OK CLicked');
               //AccRefPopupOKClicked();
               SetJournalType();
               $('#dvPopupJournalType').dialog('close');
           });

           $('#dvPopupJournalType').find('.btnPopupJournalTypeCancel').click(function (e) {
               //alert('OK CLicked');
               //AccRefPopupOKClicked();
               $('#dvPopupJournalType').dialog('close');
           });

           $('#btnJournalType').click(function (e) {
               $('#dvPopupJournalType').dialog('open');
           });
       }

       function ChangeJournalType() {
           
       }


       $(document).ready(function () {
           //journalID = JSUtility.GetQueryStringInt('id', document.location.href);
           journalID = getThisJournalID();
           bindButtonEvents();
           bindJournalTypePopup();

           if (!IForm.IsPostBack) {
               if (journalID > 0) {
                   ReadTask();
               }
               else {
                   AddTask();
               }
           }

           //           $("#txtJournalDate").val("02-Nov-2014");
           //            $("#txtJournalDate").prop("readonly", true);

           //$("#txtJournalDate").prop("disabled", true);
           //Text1
           //txtJournalNo


       });



       function Testing() {
           $("#txtJournalDate").prop("readonly", false);
       }


       function ReadTask() {

           $('#hdnEditDataModeInt').val(Enums.EditMode.Read);
           $('#' + lblHeader).text('Journal : Read');
           if (journalID > 0) {
               if (ReadJournal(journalID)) {
               }
               else {
               }
               SetControlForFormDataMode(Enums.EditMode.Read);
           }
       }

       function AddTask() {
           //alert('Add Task');
           $('#hdnEditDataModeInt').val(Enums.EditMode.Add);
           $('#' + hdnJournalID).val('0');
           $('#' + lblHeader).text('Journal : New');

           ClearData();
           ClearGrid();
           CheckAndNewBlankRow();
           SetControlForFormDataMode(Enums.EditMode.Add);
           var curJTypeID = parseInt($('#hdnJournalTypeID').val());
           if (curJTypeID == 0) {
               SetJournalType();
           }
       }

       function EditTask() {
           //alert('Edit Task');
           $('#hdnEditDataModeInt').val(Enums.EditMode.Edit);
           //$('#' + hdnJournalID).val('0');
           $('#' + lblHeader).text('Journal : Edit');
           SetControlForFormDataMode(Enums.EditMode.Edit);
       }

       function DeleteTask() {
           alert('Delete Task');
       }

       function SaveTask() {
           //alert('Save Task');
           var bStatus = false;

           var editModeInt = parseInt($('#hdnEditDataModeInt').val());
           var journalID = parseInt($('#' + hdnJournalID).val());

           editModeInt = Enums.EditMode.Edit;

           if (!CheckJournalDet(true)) {
               return false;
           }

           var task = { EditModeInt: editModeInt
                        , JournalID: journalID

           };

           var jrnl = ReadJournalFromUI();
           var jrnlDetList = ReadJournalDetFromUI();

           //TODO: data validation required

           //// data validation required

           var bResult = UpdateJournal(task, jrnl, jrnlDetList);

           if (JSSecurity.IsLoginExpired) {

           }
           else {
               if (bResult != null) {
                   if (bResult.IsError) {
                       alert(bResult.ErrorText);
                       return false;
                   }
                   else {
                       if (bResult.JournalID > 0) {
                           //alert("Saved, ID: " + bResult.JournalID);
                           $('#' + hdnJournalID).val(bResult.JournalID)
                           alert("Journal Successfully Saved.");
                           ReadTask();
                           bStatus = true;
                       }
                       else {
                           alert('Error! journal not saved.');
                       }
                   }
               }
               else {
                   alert("Error! journal not saved.");
               }
           }
           return bStatus;
       }

       function CancelTask() {
           alert('Cancel Task');
       }

       function JournalPostTask() {
           alert('Journal Post Task');
       }



//       $(document).ajaxError(function (event, jqXhr, ajaxSettings, thrownError) {
//           switch (jqXhr.status) {
//               case 401:
//                   var response = $.parseJSON(jqXhr.responseText);
//                   //window.location = response.returnUrl;
//                   alert(response);
//                   break;
//               default:
//                   break;
//           }
//       });

       $(document).ajaxError(function () {
           // $(".log").text("Triggered ajaxError handler.");
           //alert('ajax error');
       });


       function SetJournalType() {
           var curJTypeID = parseInt($('#hdnJournalTypeID').val());
           
           var jTypeID = 0
           var jTypeName = '';
           var selOpt = $("#" + rblJournalType).find(":checked");
           if (selOpt != null) {
               jTypeID = $(selOpt).val();
               jTypeName = $(selOpt).parent().find('label').text();
           }
           $('#hdnJournalTypeID').val(jTypeID);
           $('#spnJournalType').text(jTypeName);

           if (curJTypeID != jTypeID) {
              //jType changed
           }
       }


       function GetGLAccount(accCode) {
           var glAcc = null;
           var isError = false;
           var isComplete = false;
           //ajax call

           //           var glgroupclassinc = $('#' + hdnGLGroupClassInclude).val();
           //           var glgroupclassexc = $('#' + hdnGLGroupClassExclude).val();

           var glgroupclassinc = '';
           var glgroupclassexc = '';
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


           //           var glgroupclassinc = $('#' + hdnGLGroupClassInclude).val();
           //           var glgroupclassexc = $('#' + hdnGLGroupClassExclude).val();

           var glgroupclassinc = '';
           var glgroupclassexc = '';
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

       function bindGLGroupListToRow(rowElem) {
           var cgColumns = [{ 'columnName': 'glgroupcode', 'width': '80', 'align': 'left', 'highlight': 2, 'label': 'Code' }
                             , { 'columnName': 'glgroupnameshort', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Short Name' }
                             , { 'columnName': 'glgroupname', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                             , { 'columnName': 'glgroupnameparent', 'width': '130', 'align': 'left', 'highlight': 0, 'label': 'Parent' }
                            ];


           //           var glgroupclassinc = $('#' + hdnGLGroupClassInclude).val();
           //           var glgroupclassexc = $('#' + hdnGLGroupClassExclude).val();
           var glgroupclassinc = '';
           var glgroupclassexc = '';
           var companyid = $('#' + hdnCompanyID).val();

           var serviceURL = GLGroupServiceLink + "?isterm=1&includeempty=1&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
           serviceURL += "&companyid=" + companyid;
           serviceURL += "&glgroupclassinclude=" + glgroupclassinc;
           serviceURL += "&glgroupclassexclude=" + glgroupclassexc;
           serviceURL += "&ispaging=1";
           serviceURL += "&namecomptype=" + Enums.DataCompareType.Contains;

           var elem = $(rowElem).find('input.txtGLGroup');
           var elemBtn = $(rowElem).find('input.btnGLGroupAC');


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
               colModel: cgColumns,
               autoFocus: true,
               showError: true,
               width: 500,
               url: serviceURL,
               //"select item" event handler to set input field
               select: function (event, ui) {
                   //alert(ui.item.typename);
                   //$(".txtComboGrid").val(ui.item.code);
                   //elemID = $(elem).attr('id');
                   //                    if (!validateGLAccount(elemID, ui.item)) {
                   //                        $(elem).val(prevGLCode);
                   //                        return false;
                   //                    }
                   if (!ui.item) {
                       event.preventDefault();
                       ClearGLGroupData(rowElem);
                       return false;

                   }

                   if (ui.item.id == 0) {
                       event.preventDefault();
                       return false;
                       //ClearGLAccountData(elemID);
                   }
                   else {
                       SetGLGroupData(rowElem, ui.item);
                   }
                   //setDetInstrument(hdnIsInstrumentElem, txtInstrumentElem, btnInstrumentElem);
                   return false;
               }
           });

           $(elem).blur(function () {
               var self = this;
               //elemID = $(elem).attr('id');
               eCode = $(elem).val();
               isComboGridOpen = $(self).combogrid('isOpened');
               if (eCode == '') {
                   //                    if (!validateGLAccount(elemID, null)) {
                   //                        $(elem).val(prevGLCode);
                   //                        return false;
                   //                    }
                   ClearGLGroupData(rowElem);
               }
               else {
                   grp = GetGLGroup(eCode);
                   //                    if (!validateGLAccount(elemID, grp)) {
                   //                        $(elem).val(prevGLCode);
                   //                        return false;
                   //                    }

                   if (grp == null) {
                       ClearGLGroupData(rowElem);
                   }
                   else {
                       SetGLGroupData(rowElem, grp);
                   }
               }
               //setDetInstrument(hdnIsInstrumentElem, txtInstrumentElem, btnInstrumentElem);
               grpID = $(rowElem).find('input.hdnGLGroupID').val();
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

       } //bindGLGroupList

       function SetGLGroupData(rowElem, data) {

           $(rowElem).find('input.txtGLGroup').val(data.glgroupcode);
           $(rowElem).find('input.hdnGLGroupID').val(data.glgroupid);
           $(rowElem).find('input.txtGLGroupName').val(data.glgroupnameshort);
           $(rowElem).find('input.hdnGLGroupClassID').val(data.glgroupclassid);
           $(rowElem).find('input.hdnIsInstrument').val(data.isinstrument);

           var grpIDAcc = parseInt($(rowElem).find('input.hdnGLGroupIDAcc').val());

           if (grpIDAcc > 0) {
               if (grpIDAcc != data.glgroupid) {
                   $(rowElem).find('input.hdnGLAccountID').val('0');
                   $(rowElem).find('input.txtGLAccount').val('');
                   $(rowElem).find('input.txtGLAccountName').val('');
               }
           }


           //           //ins
           //           var hdnIsInstrumentElem = $(detRow).find('input[id$="hdnIsInstrument"]');
           //           var txtInstrumentElem = $(detRow).find('input[id$="txtInstrument"]');
           //           var btnInstrumentElem = $(detRow).find('input[id$="btnInstrument"]');


           //


           //check current entry
           //setDetInstrument(hdnIsInstrumentElem, txtInstrumentElem, btnInstrumentElem);
       }

       function ClearGLGroupData(rowElem) {
           //$('#' + txtGLAccCodeID).val('');

           $(rowElem).find('input.hdnGLGroupID').val('0');
           $(rowElem).find('input.txtGLGroupName').val('');
           $(rowElem).find('input.hdnGLGroupClassID').val('0');
           $(rowElem).find('input.hdnIsInstrument').val('0');


           //           var hdnIsInstrumentElem = $(detRow).find('input[id$="hdnIsInstrument"]');
           //           var txtInstrumentElem = $(detRow).find('input[id$="txtInstrument"]');
           //           var btnInstrumentElem = $(detRow).find('input[id$="btnInstrument"]');

           //           setDetInstrument(hdnIsInstrumentElem, txtInstrumentElem, btnInstrumentElem);
       }

       function bindGLAccounctListToRow(rowElem) {
           var cgColumns = [{ 'columnName': 'glacccode', 'width': '80', 'align': 'left', 'highlight': 2, 'label': 'Code' }
                             , { 'columnName': 'glaccname', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                             , { 'columnName': 'glgroupname', 'width': '130', 'align': 'left', 'highlight': 0, 'label': 'Group' }
                             , { 'columnName': 'glacctypename', 'width': '100', 'align': 'left', 'highlight': 0, 'label': 'Type' }
                            ];


           //           var glgroupclassinc = $('#' + hdnGLGroupClassInclude).val();
           //           var glgroupclassexc = $('#' + hdnGLGroupClassExclude).val();
           var glgroupclassinc = '';
           var glgroupclassexc = '';

           var companyid = $('#' + hdnCompanyID).val();

           var serviceURL = GLAccountServiceLink + "?isterm=1&includeempty=1&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
           serviceURL += "&namecomptype=" + Enums.DataCompareType.Contains + "&ispaging=1";
           serviceURL += "&acctypefilter=" + Enums.GLAccountTypeFilter.NormalSubAccount;
           serviceURL += "&glgroupclassinclude=" + glgroupclassinc;
           serviceURL += "&glgroupclassexclude=" + glgroupclassexc;
           serviceURL += "&companyid=" + companyid;

           //var gridSelector = "#" + gridViewIDDet + ",#" + gridViewIDDet2;
           //var gridSelector = "#" + gridViewID;



           var elem = $(rowElem).find('input.txtGLAccount')
           var elemBtn = $(rowElem).find('input.btnGLAccountAC')

           var prevGLCode = '';

           $(elemBtn).click(function (e) {
               //elmID = $(elem).attr('id');
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
                   var glgroupID = $(rowElem).find('input.hdnGLGroupID').val();
                   var newServiceURL = serviceURL + "&glgroupid=" + glgroupID
                   $(this).combogrid("option", "url", newServiceURL);
               },
               select: function (event, ui) {
                   //alert(ui.item.typename);
                   //$(".txtComboGrid").val(ui.item.code);
                   //elemID = $(elem).attr('id');
                   if (!ValidateGLAccount(rowElem, ui.item)) {
                       $(elem).val(prevGLCode);
                       return false;
                   }

                   if (!ui.item) {
                       event.preventDefault();
                       ClearGLAccountData(rowElem);
                       return false;
                       //ClearGLAccountData(elemID);
                   }


                   if (ui.item.id == 0) {
                       event.preventDefault();
                       return false;
                       //ClearGLAccountData(elemID);
                   }
                   else {
                       SetGLAccountData(rowElem, ui.item);
                   }
                   //setDetInstrument(hdnIsInstrumentElem, txtInstrumentElem, btnInstrumentElem);
                   return false;
               }
           });


           $(elem).blur(function () {
               var self = this;
               //elemID = $(elem).attr('id');
               eCode = $(elem).val();
               isComboGridOpen = $(self).combogrid('isOpened');
               if (eCode == '') {
                   if (!ValidateGLAccount(rowElem, null)) {
                       $(elem).val(prevGLCode);
                       return false;
                   }
                   ClearGLAccountData(rowElem);
               }
               else {
                   acc = GetGLAccount(eCode);
                   if (!ValidateGLAccount(rowElem, acc)) {
                       $(elem).val(prevGLCode);
                       return false;
                   }

                   if (acc == null) {
                       ClearGLAccountData(rowElem);
                   }
                   else {
                       SetGLAccountData(rowElem, acc);
                   }
               }
               //setDetInstrument(hdnIsInstrumentElem, txtInstrumentElem, btnInstrumentElem);
               accID = $(rowElem).find('input.hdnGLAccountID').val();
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


       } //bindGLAccounctList

       function ValidateGLAccount(rowElem, glAccItem) {
           var result = true;
           glAccItem = glAccItem || null;


           //var elemRow = $('#' + txtGLAccCodeID).closest('tr.gridRow');

           //           var hdnJournalDetID_LinkElem = $(elemRow).find('input[id$="hdnJournalDetID_Link"]');
           //           var hdnDrCrElem = $(elemRow).find('select[id$="hdnDrCr"]');
           //           var hdnGLAccIDElem = $(elemRow).find('input[id$="hdnGLAccountID"]');

           //           var hdnIsInstrumentElem = $(elemRow).find('input[id$="hdnIsInstrument"]');
           //           var txtInstrumentElem = $(elemRow).find('input[id$="txtInstrument"]');
           //           var btnInstrumentElem = $(elemRow).find('input[id$="btnInstrument"]');
           //           var drcr = parseInt(hdnDrCrElem.val());

           //           var linkID = parseInt($(hdnJournalDetID_LinkElem).val());
           //           var insCount = GetInsCountByDetLink(linkID, drcr);

           //           var rCnt = 0;

           //           result = true;

           //           if (glAccItem == null) {
           //               if (insCount > 0) {
           //                   if (confirm('Instrument entries will be removed for current row. Continue?')) {
           //                       rCnt = RemoveInsByDetLink(linkID, drcr);
           //                       $(txtInstrumentElem).val('');
           //                       result = true;
           //                   }
           //                   else {
           //                       result = false;
           //                       //$(elem).val(prevGLCode);
           //                       //return false;
           //                   }
           //               } //inscount
           //           }
           //           else {

           //               if (glAccItem.isinstrument == 0) {
           //                   if (insCount > 0) {
           //                       if (confirm('Instrument entries will be removed for current row. Continue?')) {
           //                           rCnt = RemoveInsByDetLink(linkID, drcr);
           //                           $(txtInstrumentElem).val('');
           //                           result = true;
           //                       }
           //                       else {
           //                           result = false;
           //                           //$(elem).val(prevGLCode);
           //                           //return false;

           //                       }
           //                   } //inscount
           //               }
           //               else {
           //                   if (insCount > 0) {
           //                       var insModeID = GetDetRowInsMode(drcr);
           //                       if (insModeID == Enums.InstrumentMode.Issue) {
           //                           var curAccID = parseInt($(hdnGLAccIDElem).val());
           //                           if (curAccID > 0) {
           //                               if (curAccID != glAccItem.id) {
           //                                   if (confirm('Instrument entries will be removed for current row. Continue?')) {
           //                                       rCnt = RemoveInsByDetLink(linkID, drcr);
           //                                       $(txtInstrumentElem).val('');
           //                                       result = true;
           //                                   }
           //                                   else {
           //                                       result = false;
           //                                       //$(elem).val(prevGLCode);
           //                                       //return false;
           //                                   }
           //                               }
           //                           } //curaccid >0
           //                       } //insmode == issue
           //                   } //inscount
           //               }  //else insid ==0
           //           } // else item == null

           return result;
       }

       function SetGLAccountData(rowElem, data) {
           //           $('#' + txtGLAccCodeID).val(data.glacccode);


           $(rowElem).find('input.hdnGLAccountID').val(data.glaccid);
           $(rowElem).find('input.txtGLAccount').val(data.glacccode);
           $(rowElem).find('input.txtGLAccountName').val(data.glaccname);

           $(rowElem).find('input.hdnGLGroupID').val(data.glgroupid);
           $(rowElem).find('input.txtGLGroup').val(data.glgroupcode);
           $(rowElem).find('input.txtGLGroupName').val(data.glgroupnameshort);

           $(rowElem).find('input.hdnGLGroupIDAcc').val(data.glgroupid);
           $(rowElem).find('input.hdnGLGroupClassID').val(data.glgroupclassid);
           $(rowElem).find('input.hdnIsInstrument').val(data.isinstrument);


           $(rowElem).find('input.txtGLGroup').removeClass('textError');


           //           var hdnIsInstrumentElem = $(detRow).find('input[id$="hdnIsInstrument"]');
           //           var txtInstrumentElem = $(detRow).find('input[id$="txtInstrument"]');
           //           var btnInstrumentElem = $(detRow).find('input[id$="btnInstrument"]');

           //           //check current entry
           //           setDetInstrument(hdnIsInstrumentElem, txtInstrumentElem, btnInstrumentElem);
           //           SetDebitCreditRemainAmt(txtGLAccCodeID);
           sumJournalDetAmt();
       }

       function ClearGLAccountData(rowElem) {
           $(rowElem).find('input.hdnGLAccountID').val('0');
           $(rowElem).find('input.txtGLAccount').val('');
           $(rowElem).find('input.txtGLAccountName').val('');
       }


       function bindJournalDetAmtToRow(rowElem) {
            //debit credit sum

           var debitElem = $(rowElem).find('input.txtDebit');
           var creditElem = $(rowElem).find('input.txtCredit');

           $(debitElem).keyup(function (e) {
               var elem = this;
               if (JSUtility.IsPrintableChar(e.keyCode, true, true)) {
                   $(creditElem).val('');
                   sumJournalDetAmt();
               }
           });


            $(creditElem).keyup(function (e) {
                var elem = this;
                if (JSUtility.IsPrintableChar(e.keyCode, true, true)) {
                    $(debitElem).val('');
                    sumJournalDetAmt();
                }
            });

            //sumJournalDetAmt();

        }

       function GetJournalData(pJournalID) {
           var journalData = null;
           var isError = false;
           var isComplete = false;
           //ajax call

           pJournalID = pJournalID || 0;

           var companyid = $('#' + hdnCompanyID).val();

           var serviceUrl = GetJournalServiceLink + '?journalid=' + pJournalID;
           serviceUrl += '&companyid=' + companyid;

           //serviceUrl += '&codecomptype=' + Enums.DataCompareType.EqualTo;


           var dummyVar = $.ajax({
               type: "GET",
               //type: "POST",
               cache: false,
               async: false,
               dataType: "json",
               url: serviceUrl,
               success: function (jsonData) {
                   journalData = jsonData;
                   
//                   if (journaldata.rows.length > 0) {
//                       journal = journaldata.journal;
//                   }
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
                   //alert(textStatus);
                   JSSecurity.CheckAndLoginAjax(XMLHttpRequest, textStatus);
               }
           });
           return journalData;
       }

       function UpdateJournal(task, jrnl, jrnlDetList) {
           var resultData = null;
           var isError = false;
           var isComplete = false;

           var companyid = $('#' + hdnCompanyID).val();
           var journalid=  $('#' + hdnJournalID).val();

           var serviceUrl = UpdateJournalServiceLink + "?journalid=" + journalID;
           serviceUrl += '&companyid=' + companyid;

           var jsonTask = JSUtility.JSONStringify(task);
           var jsonJrnlText = JSUtility.JSONStringify(jrnl);
           var jsonJrnlDetListText = JSUtility.JSONStringify(jrnlDetList);

           var jsonPostData = { 'jsonTask' : jsonTask 
                                , 'jsonJournal': jsonJrnlText
                                , 'jsonJournalDetList': jsonJrnlDetListText
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

                   //                   if (journaldata.rows.length > 0) {
                   //                       journal = journaldata.journal;
                   //                   }
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
                   //                   if (XMLHttpRequest.status && XMLHttpRequest.status == 400) {
                   //                       alert(XMLHttpRequest.responseText);
                   //                   } else {
                   //                       alert("Something went wrong");
                   //                   }

                   JSSecurity.CheckAndLoginAjax(XMLHttpRequest, textStatus);


                   //                   var contentType = XMLHttpRequest.getResponseHeader("Content-Type");
                   //                   if (XMLHttpRequest.status === 200 && contentType.toLowerCase().indexOf("text/html") >= 0) {
                   //                       // assume that our login has expired - reload our current page
                   //                       //window.location.reload();
                   //                       LoginDialog('Admin');
                   //                   }
                   //                   else {
                   //                       alert(textStatus);
                   //                   }
               }
           });
           return resultData;
       }


       function LoginDialog(userID) {

//           $('<div />').html(userID).dialog({
//               title: "Login",
//               resizable: false,
//               position: 'center',
//               modal: true
           //           });


           LoginForm.Show(userID);


       }


       function ClearData() {
           //$('#' + hdnJournalID).val('0');
           $('#hdnJournalUpdateNo').val('0');

           $('#txtJournalNo').val('');
           $('#txtJournalDate').val('');

           $('#dvGridFooter').find('.txtTotDebit').val('0');
           $('#dvGridFooter').find('.txtTotCredit').val('0');
       }

       function ClearGrid() {
           removeAllRow();
       }

       


       function ReadJournal(pJournalID) {
           var journalData = GetJournalData(pJournalID);
           if (journalData == null) {
               return false;
           }
           
           if (journalData.journal == null) {
               alert('No Journal Found!');
               return false;
           }

           ClearData();

           var journal = journalData.journal;

           $('#'+hdnCompanyID).val(journal.CompanyID);

           $('#hdnJournalTypeID').val(journal.JournalTypeID);
           $('#spnJournalType').text(journal.JournalTypeName);

           $('#' + hdnJournalID).val(journal.JournalID);
           $('#hdnJournalUpdateNo').val(journal.JournalUpdateNo);

           $('#txtJournalNo').val(journal.JournalNo);
           $('#txtJournalDate').val(journal.JournalDate);
           $('#'+ddlAccYear).val(journal.AccYearID);

           $('#dvGridFooter').find('.txtTotDebit').val(journal.JournalAmt);
           $('#dvGridFooter').find('.txtTotCredit').val(journal.JournalAmt);

           var journalDet = journalData.journaldetlist;
           $('#spnGridRowCount').text(journalData.journaldetlist.length);


           //alert(journalDet[0].GLAccountCode);


           fillJournalDetails(journalData);


           return true;
       }


       function GetJournalTest() {
           var journalID = $('#Text1').val();
           //alert(journalID);
           ReadJournal(journalID);
       }


       function getRowCountVisible() {
           var rowContainer = $('#dvGridRowContainer');
           var rowCountVisible = $(rowContainer).find('div.dvGridRow:visible').length;
           rowContainer = null;
           return rowCountVisible;
       }

       function CheckAndNewBlankRow() {
           var rowCountVisible = getRowCountVisible();
           if (rowCountVisible == 0) {
               addBlankRow();
               addBlankRow();
           }
           else {
             
           }
       }

       function addNewRow() {
           addBlankRow();
       }

       function removeAllRow() {
           //var rowContainer = $('#dvGridRowContainer');
           $('#dvGridRowContainer').find('.dvGridRow').remove();
       }

       function addBlankRow() {

           var grid = $('#dvGrid');
           var rowContainer = $('#dvGridRowContainer');

           var rowCountAll = $(rowContainer).find('div.dvGridRow').length;
           var rowCountVisible = $(rowContainer).find('div.dvGridRow:visible').length;
           
           var templateRow = $('.dvGridRowTemplate:first').clone();
           templateRow.removeClass('dvGridRowTemplate');

           var newRow = templateRow.appendTo(rowContainer);

           templateRow = null;

           var RowNum = $(newRow).index() + 1;

           if (RowNum % 2 == 0) {
               $(newRow).addClass('rowEven');
           }
           else {
               $(newRow).addClass('rowOdd');
           }

           var rowSLNo = rowCountVisible + 1;
           tempJournalDetID = tempJournalDetID - 1;

           $(newRow).find('.rowSLNo').text(rowSLNo);
           $(newRow).find('input.hdnRecordStateInt').val(Enums.RecordState.Added);
           $(newRow).find('input.hdnJournalDetID').val(tempJournalDetID);

           $("#dvGridFooter").find('.spnGridRowCount').text(rowSLNo);

           checkIfInView(newRow, grid);

           rowContainer = null;
           grid = null;


           bindRowEventsALL(newRow);

           return newRow;
       }

       function bindRowEventsALL(rowElem) {

           ContentForm.TextAutoSelectInScopeElement(rowElem);
           ContentForm.TextNumberOnlyInScopeElement(rowElem);
           ContentForm.TextCurrencyFormatInScopeElement(rowElem);
           ContentForm.TextAreaAutoSizeInScopeElement(rowElem);
           
           bindGLGroupListToRow(rowElem);
           bindGLAccounctListToRow(rowElem);
           bindJournalDetAmtToRow(rowElem);
           bindDeleteToRow(rowElem);
       }

       function bindDeleteToRow(rowElem) {

           var deleteBtn = $(rowElem).find('input.btnDeleteRow');

           $(deleteBtn).click(function (e) {
               //elmID = $(elem).attr('id');
               //$(elem).combogrid("show");
               //$(elem).combogrid("dropdownClick");

               if (isRowHasAnyData(rowElem)) {
                   if (!confirm("Confirm to remove row?")) {
                       return;
                   }
               }

               var detIDElem = $(rowElem).find('input.hdnJournalDetID');
               var recStateElem = $(rowElem).find('input.hdnRecordStateInt');

               var detID = parseInt($(detIDElem).val());
               var recState = parseInt($(recStateElem).val());

               if (detID > 0) {
                   $(recStateElem).val(Enums.RecordState.Deleted);
                   $(rowElem).hide();
               }
               else {
                   detIDElem = null;
                   recStateElem = null;
                   $(rowElem).remove();
               }
               resetGridRowNumber();
               sumJournalDetAmt();
               //alert('delete row');
           });
       }

       function resetGridRowNumber() {
           var rowNum = 0;

           $("#dvGrid").find('span.rowSLNo:visible').each(function (index, elem) {
               rowNum = rowNum + 1;
               $(elem).text(rowNum);
           });

           $("#dvGridFooter").find('.spnGridRowCount').text(rowNum);
       }

       function isRowHasValidData(rowElem) {
           var hasData = false;
           var glAccID = parseInt($(rowElem).find('.hdnGLAccountID').val());

           var detDesc = jQuery.trim($(rowElem).find('.txtJournalDetDesc').val());

           var debitAmt = parseFloat(JSUtility.GetNumber($(rowElem).find('.txtDebit').val()));
           var creditAmt = parseFloat(JSUtility.GetNumber($(rowElem).find('.txtCredit').val()));

//           if ((glAccID > 0) && (debitAmt> 0 || creditAmt > 0)) {
//               hasData = true;
//           }


           if (glAccID > 0 && detDesc != '' && (debitAmt > 0 || creditAmt > 0)) {
               hasData = true;
           }

           return hasData;
       }

       function isRowHasAnyData(rowElem) {
           var hasData = false;

           var glGroupID = parseInt($(rowElem).find('input.hdnGLGroupID').val());
           var glAccID = parseInt($(rowElem).find('input.hdnGLAccountID').val());

           var detDesc = jQuery.trim($(rowElem).find('input.txtJournalDetDesc').val());

           var debitAmt = parseFloat(JSUtility.GetNumber($(rowElem).find('input.txtDebit').val()));
           var creditAmt = parseFloat(JSUtility.GetNumber($(rowElem).find('input.txtCredit').val()));

           //           if ((glAccID > 0) && (debitAmt> 0 || creditAmt > 0)) {
           //               hasData = true;
           //           }


           if (glGroupID > 0 || glAccID > 0 || debitAmt > 0 || creditAmt > 0 || detDesc != '') {
               hasData = true;
           }

           return hasData;
       }


       function checkIfInView(element,container) {
           var offset = element.offset().top - $(container).scrollTop();

           if (offset > container.innerHeight()) {
               // Not in view
               $(container).animate({ scrollTop: offset }, 1000);
               //$(container).scrollTo(element);


//               t = container.offset().top;
//               container.scrollTop(element.position().top - t);

               return false;
           }

           return true;
       }

       function fillJournalDetails(journalData) {

           removeAllRow();

           var totRow = journalData.journaldetlist.length;
           var detList = journalData.journaldetlist;

           //for details
           for (i = 0; i < totRow; i++) {
               var rowElem = addBlankRow();
               
               var rowData = detList[i];

               fillDetToGridRow(rowElem, rowData);

               rowElem = null;
               rowData = null;
           }
           sumJournalDetAmt();
       }


       function fillDetToGridRow(rowElem,rowData) {
           
           $(rowElem).find('.hdnJournalDetID').val(rowData.JournalDetID);
           $(rowElem).find('.hdnRecordStateInt').val(rowData._RecordStateInt);

           $(rowElem).find('.hdnGLAccountID').val(rowData.GLAccountID);
           $(rowElem).find('.txtGLAccount').val(rowData.GLAccountCode);
           $(rowElem).find('.txtGLAccountName').val(rowData.GLAccountName);
           $(rowElem).find('.hdnGLAccountIDEdit').val(rowData.GLAccountID);
           $(rowElem).find('.hdnGLGroupIDAcc').val(rowData.GLGroupIDAcc);

           $(rowElem).find('.hdnGLGroupID').val(rowData.GLGroupID);
           $(rowElem).find('.txtGLGroup').val(rowData.GLGroupCode);
           $(rowElem).find('.txtGLGroupName').val(rowData.GLGroupNameShort);
           $(rowElem).find('.hdnGLGroupIDEdit').val(rowData.GLGroupID);
           $(rowElem).find('.hdnGLGroupClassID').val(rowData.GLGroupClassID);
           
           $(rowElem).find('.hdnIsInstrument').val(rowData.IsInstrument);
           $(rowElem).find('.hdnIsCash').val(rowData.IsCash);

           $(rowElem).find('.txtJournalDetDesc').val(rowData.JournalDetDesc);

           //JSUtility.FormatCurrency

           if (rowData.DebitAmt > 0) {
               $(rowElem).find('.txtDebit').val(JSUtility.FormatCurrency(rowData.DebitAmt));
           }

           if (rowData.CreditAmt > 0) {
               $(rowElem).find('.txtCredit').val(JSUtility.FormatCurrency(rowData.CreditAmt));
           }

//           $(rowElem).find('.txtDebit').val(rowData.DebitAmt);
//           $(rowElem).find('.txtCredit').val(rowData.CreditAmt);


           if (rowData._RecordStateInt == Enums.RecordState.Deleted) {
               $(rowElem).hide();
           }

       }

       function GetTotalSumDebit() {
           var totDebit = 0;
           $("#dvGrid").find('input.txtDebit:visible').each(function (index, elem) {
               drAmt = parseFloat(JSUtility.GetNumber($(elem).val()));
               if (!isNaN(drAmt)) {
                   totDebit += drAmt;
               }
           });
           return totDebit;
       }

       function GetTotalSumCredit() {
           var totCredit = 0;
           $("#dvGrid").find('input.txtCredit:visible').each(function (index, elem) {
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

           $("#dvGridFooter").find('input.txtTotDebit').val(JSUtility.FormatCurrency(totDebit));
           $("#dvGridFooter").find('input.txtTotCredit').val(JSUtility.FormatCurrency(totCredit));
       }

       function isDebitCreditEqual() {
           var retVal = false;
           
           var totDebit = GetTotalSumDebit();
           var totCredit = GetTotalSumCredit();

           if (totDebit == totCredit) {
               retVal = true;
           }
           return retVal;
       }


       function ReadJournalFromUI() {

           var companyID = $('#'+hdnCompanyID).val();
           var journalID = $('#' + hdnJournalID).val();
           var journalNO = $('#txtJournalNo').val();
           var journalDate = $('#txtJournalDate').val();
           var journalTypeID = $('#hdnJournalTypeID').val();
           var accYearID = $('#'+ddlAccYear).val();

           var journalUpdateNo = $('#hdnJournalUpdateNo').val();

           var jrnl = { JournalID: journalID
                        , CompanyID: companyID
                        , JournalNo: journalNO
                        , JournalDate: journalDate
                        , JournalTypeID: journalTypeID
                        , AccYearID: accYearID
                        , JournalUpdateNo: journalUpdateNo
           };
           return jrnl;    
       }

       function ReadJournalDetFromUI() {

           var jrnlDetList = [];
           var journalID = $('#' + hdnJournalID).val();
           var grid = $('#dvGrid');
           var rowContainer = $('#dvGridRowContainer');
           var rowCount = $(rowContainer).find('div.dvGridRow:visible').length;

           var slno = 0;

           $(rowContainer).find('div.dvGridRow').each(function (idx, rowElem) {

               var journalDetID = parseInt($(rowElem).find('.hdnJournalDetID').val());
               var recordstateRow = parseInt($(rowElem).find('.hdnRecordStateInt').val());

               var glGroupID = parseInt($(rowElem).find('.hdnGLGroupID').val());
               var glAccountID = parseInt($(rowElem).find('.hdnGLAccountID').val());

               var journalDetDesc = $(rowElem).find('.txtJournalDetDesc').val();

               var debitAmt = parseFloat(JSUtility.GetNumber($(rowElem).find('.txtDebit').val()));
               var creditAmt = parseFloat(JSUtility.GetNumber($(rowElem).find('.txtCredit').val()));

               var tranTypeID = parseInt($(rowElem).find('.hdnTranTypeID').val());

               var isValid = false;
               var isToList = false;

               var recordstateint = Enums.RecordState.Edited;
               if (recordstateRow == Enums.RecordState.Deleted) {
                   if (journalDetID > 0) {
                       recordstateint = Enums.RecordState.Deleted;
                       isToList = true;
                   }
               } // rowstate deleted
               else {
                   isValid = isRowHasValidData(rowElem);
                   if (isValid) {
                       if (journalDetID > 0) {
                           recordstateint = Enums.RecordState.Edited;
                       }
                       else {
                           recordstateint = Enums.RecordState.Added;
                       }
                       isToList = true;
                       slno++;
                   }
                   else {
                       if (journalDetID > 0) {
                           recordstateint = Enums.RecordState.Deleted;
                           isToList = true;
                       }
                   }
               }


               if (isToList) {
                   var detItem = { JournalDetID: journalDetID
                                   , _RecordStateInt: recordstateint
                                   , journalID: journalID 
                                   , JournalDetSLNo: slno
                                   , GLAccountID: glAccountID
                                   , GLGroupID: glGroupID
                                   , JournalDetDesc : journalDetDesc
                                   , DebitAmt : debitAmt
                                   , CreditAmt: creditAmt
                                   , TranTypeID: tranTypeID
                                 
                   };
                   jrnlDetList.push(detItem);
               }
           });

           return jrnlDetList;
       }


       function CheckJournalDet(isMessage) {
           var bStatus = true;

           isMessage = isMessage || true;

           var grid = $('#dvGrid');
           var rowContainer = $('#dvGridRowContainer');
           var rowCount = $(rowContainer).find('div.dvGridRow:visible').length;

           var slno = 0;

           if (rowCount == 0) {
               alert('Please Enter Debit Credit Data.');
               bStatus = false;
           }

           $(rowContainer).find('div.dvGridRow:visible').each(function (idx, rowElem) {

               var rowNum = parseInt($(rowElem).find('.rowSLNo').text());

               if (isRowHasAnyData(rowElem)) {

                   var journalDetID = parseInt($(rowElem).find('.hdnJournalDetID').val());
                   var recordstateRow = parseInt($(rowElem).find('.hdnRecordStateInt').val());

                   var glGroupID = parseInt($(rowElem).find('.hdnGLGroupID').val());
                   var glAccountID = parseInt($(rowElem).find('.hdnGLAccountID').val());

                   if (glAccountID <= 0) {
                       alert('Please Enter Account at Line No: ' + rowNum);
                       $(rowElem).find('.txtGLAccount').focus();
                       bStatus = false;
                       return false;
                   }

                   var debitAmt = parseFloat(JSUtility.GetNumber($(rowElem).find('.txtDebit').val()));
                   var creditAmt = parseFloat(JSUtility.GetNumber($(rowElem).find('.txtCredit').val()));


                   if (debitAmt <= 0 && creditAmt <= 0) {
                       alert('Please Enter Debit Or Credit Amount at Line No: ' + rowNum);
                       $(rowElem).find('.txtDebit').focus();
                       bStatus = false;
                       return false;
                   }


                   var journalDetDesc = jQuery.trim($(rowElem).find('.txtJournalDetDesc').val());

                   if (journalDetDesc == '') {
                       alert('Please Enter Narration at Line No: ' + rowNum);
                       $(rowElem).find('.txtJournalDetDesc').focus();
                       bStatus = false;
                       return false;
                   }

                   var tranTypeID = parseInt($(rowElem).find('.hdnTranTypeID').val());
               }

           });

           if (bStatus) {
               if (!isDebitCreditEqual()) {
                   alert('Debit and Credit Amount are not equal.');
                   bStatus = false;
               }
           }


           return bStatus;
       }


       function SaveJournal() {
           var bStatus = false;
           
           var editModeInt = parseInt($('#hdnEditDataModeInt').val());
           var journalID = parseInt($('#' + hdnJournalID).val());

           editModeInt = Enums.EditMode.Edit;

           if (!CheckJournalDet(true)) {
               return false;
           }

           var task = { EditModeInt: editModeInt
                        , JournalID: journalID

           };

           var jrnl = ReadJournalFromUI();
           var jrnlDetList = ReadJournalDetFromUI();

           //TODO: data validation required


           //// data validation required

           var bResult = UpdateJournal(task, jrnl, jrnlDetList);

           if (JSSecurity.IsLoginExpired) {

           }
           else {
               if (bResult != null) {
                   if (bResult.IsError) {
                       alert(bResult.ErrorText);
                   }
                   else {
                       alert("Saved, ID: " + bResult.JournalID);
                   }
               }
               else {
                   alert("Error! journal not saved.");
               }
           }
       }



       function SaveJournalTest() {

           var editModeInt = parseInt($('#hdnEditDataModeInt').val());
           var journalID = parseInt($('#' + hdnJournalID).val());

           editModeInt = Enums.EditMode.Edit;

           if (!CheckJournalDet(true)) {
               return;
           }

           var task = { EditModeInt : editModeInt
                        , JournalID : journalID
           
           };
           
           var jrnl = ReadJournalFromUI();
           var jrnlDetList = ReadJournalDetFromUI();

           //TODO: data validation required


           //// data validation required
           
           var bResult = UpdateJournal(task, jrnl, jrnlDetList);

           if (JSSecurity.IsLoginExpired) {

           }
           else {
               if (bResult != null) {
                   if (bResult.IsError) {
                       alert(bResult.ErrorText);
                   }
                   else {
                       alert("Saved, ID: " + bResult.JournalID);
                   }
               }
               else {
                   alert("Error! journal not saved.");
               }
           }





           //alert(jrnl.JournalNo);
       }


       function SetControlForFormDataMode(editMode) {
           var isDisabled = true;

           editMode = editMode || Enums.EditMode.Read;
           if (editMode == Enums.EditMode.Add || editMode == Enums.EditMode.Edit) {
               isDisabled = false;
           }

           $('#txtJournalNo').prop("disabled", isDisabled);
           $('#txtJournalDate').prop("disabled", isDisabled);
           $('#'+ddlAccYear).prop("disabled", isDisabled);

           SetControlDetForFormDataMode(editMode);

           SetButtonForFormDataMode(editMode);
       }


       function SetControlDetForFormDataMode(editMode) {
           var isDisabled = true;

           editMode = editMode || Enums.EditMode.Read;
           if (editMode == Enums.EditMode.Add || editMode == Enums.EditMode.Edit) {
               isDisabled = false;
           }
           
           var rowContainer = $('#dvGridRowContainer');
           
           $(rowContainer).find('div.dvGridRow').each(function (idx, rowElem) {
               $(rowElem).find('.txtGLGroup').prop("disabled", isDisabled);
               $(rowElem).find('.btnGLGroupAC').prop("disabled", isDisabled);

               $(rowElem).find('.txtGLAccount').prop("disabled", isDisabled);
               $(rowElem).find('.btnGLAccountAC').prop("disabled", isDisabled);

               $(rowElem).find('.txtTranType').prop("disabled", isDisabled);
               $(rowElem).find('.btnTranTypeAC').prop("disabled", isDisabled);

               $(rowElem).find('.txtJournalDetDesc').prop("disabled", isDisabled);
               $(rowElem).find('.txtDebit').prop("disabled", isDisabled);
               $(rowElem).find('.txtCredit').prop("disabled", isDisabled);


               $(rowElem).find('.btnDeleteRow').prop("disabled", isDisabled);
               $('#btnNewRowGrid').prop("disabled", isDisabled);

           });

       }

       function SetButtonForFormDataMode(editMode) {
           var isDisabled = true;

           editMode = editMode || Enums.EditMode.Read;
           if (editMode == Enums.EditMode.Add || editMode == Enums.EditMode.Edit) {
               isDisabled = false;
           }

           $('#btnJournalType').prop("disabled", editMode != Enums.EditMode.Add);

           $('#btnAddNew').toggle(isDisabled);
           $('#btnEdit').toggle(isDisabled);
           $('#btnDelete').toggle(isDisabled);


           $('#btnSave').toggle(!isDisabled);
           $('#btnCancel').toggle(!isDisabled);

       }





      // ]]> 
   </script>

  <style type="text/css">
  
    
       .groupBoxContainer
        {
            height: 100%;
            width: 800px;
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
        
        .subHeader
        {
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
        
        
        
        .gridRow
        {
        	
        }
        
        
        .rowEven
        {
        	background-color :White;
        }
         
        .rowOdd
        {
        	background-color : #f7f6f3;
        }
  
        .hidden
        {
        	height:0px;
        	width:0px;
        	display:none;
        }   
        
        .dvPopupJournalType
        {
            display: none;
            border: 0px solid black;
            width: 0px;
            height: 0px;
        }
        
        .dvPopupJournalTypeInner
        {
            height: 100%;
            width: 100%;
        }
        
        .dvPopupJournalTypeHeader
        {
            width: 100%;
            height: 25px;
        }
        
        .dvPopupJournalTypeContent
        {
            width: 100%;
            height: 200px;
        }
        
        .dvPopupJournalTypeContentInner
        {
            width: 100%;
            height: 100%;
            overflow: auto;
        }
        
        .dvPopupJournalTypeFooter
        {
            width: 100%;
            height: 20px;
            border-top: 1px solid blue;
        }
        
        .dvPopupJournalTypeFooterInner
        {
            width: 100%;
            height: 100%;
            padding-top: 2px;
            text-align: right;
        }


        .dvGridRowInstrument
        {
            border-top: 1px solid grey;
            height:auto;
            width: 100%;
        }
        .dvGridRowInstrumentHeader
        {
            border-bottom: 1px solid grey;
            background-color: gray;
            height:auto;
            width: 100%;
            text-align:left;
            color:white;
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
                                       <tbody>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <input id="hdnCompanyID" type="hidden"  runat="server" value="0" />
                                            </td>
                                            <td>
                                                <input id="hdnJournalID" type="hidden" runat="server" value="0" />
                                                <input id="hdnJournalUpdateNo" type="hidden" value="0" />
                                                <input id="hdnEditDataModeInt" type="hidden" value="0" />
                                                <input id="hdnAppObjectID" type="hidden" runat="server" value="0" />
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        </tbody>
                                    </table>
                                    
                                    <table style="width: 100%" border="0" cellspacing="2" cellpadding="1">
                                        <tbody>
                                        <tr>
                                            <td>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="Label3" runat="server" Text="Type:"></asp:Label>
                                            </td>
                                            <td>
                                                <span id="spnJournalType"> </span>
                                                <input id="hdnJournalTypeID" type="hidden" value="0" />

                                            </td>
                                            <td>
                                                <input id="btnJournalType" type="button" value="Type" class="buttoncommon" />
                                            </td>
                                        </tr>
                                       
                                       
                                        <tr>

                                            <td>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="Label15" runat="server" Text="Voucher No:"></asp:Label>
                                            </td>
                                            <td>
                                                <input id="txtJournalNo" type="text" class="textBox fldRequired enableIsDirty" style="text-align:left;" />
                                            </td>
                                            <td>
                                                <input id="Text1" type="text" value="28" />
                                                <input id="Text2" type="text" value="28" />
                                                <input id="Button2" type="button" value="button" onclick="GetJournalTest();" />
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                            <td>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="Label1" runat="server" Text="Voucher Date:"></asp:Label>
                                            </td>
                                            <td>
                                                <input id="txtJournalDate" type="text" class="textBox textDate dateParse fldRequired enableIsDirty" style="text-align:left;" />
                                            </td>
                                            <td>
                                                <input id="Button3" type="button" value="save" onclick="SaveJournalTest();" />
                                                <input id="Button1" type="button" value="login" onclick="LoginDialog('Admin');" />
                                                <input id="Button4" type="button" value="disable" onclick="SetControlForFormDataMode(1);" />
                                                <input id="Button5" type="button" value="enable" onclick="SetControlForFormDataMode(2);" />
                                                <input id="Button6" type="button" value="readonly" onclick="Testing();" />
                                              </td>

                                        </tr>
                                        <tr>
                                           <td>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="Label4" runat="server" Text="Year:"></asp:Label>
                                            </td>
                                            <td>
                                               <asp:DropDownList ID="ddlAccYear" runat="server" CssClass="dropDownList ddlAccYear" Width="120px">
                                                   </asp:DropDownList>
                                            </td>
                                        </tr>
                                        </tbody>
                                    </table>
                                </div>
                                <div id="groupDataDetails" style="width: 100%; height: auto;">
                                    <div id="groupDataHeader" class="" style="width: 100%; text-align: center;">
                                        <span style="font-weight: bold">Voucher Details: </span>
                                    </div>
                                    <div id="dvGridContainer" class="" style="width: auto; height: auto; text-align: left">
                                        <div id="dvGridHeader" style="width: 100%; height: 25px; font-size: smaller;" class="subHeader">
                                            <table style="height: 100%; color: #FFFFFF; font-weight: bold; text-align: center;  "
                                                class="defFont" cellspacing= "1" cellpadding="1" >
                                                <tbody>
                                                <tr class="headerRow">
                                                    <td width="28px" class="colSLNo headerColLeft">
                                                        SL#
                                                    </td>
                                                    <td width="80px" class="colGLGroup headerColLeft">
                                                        GL Head
                                                    </td>
                                                    <td width="100px" class="colGLAccount headerColLeft">
                                                        Sub Ledger
                                                    </td>
                                                    <td width="80px" class="colTranType headerColLeft">
                                                        Tran. Code
                                                    </td>
                                                    <td width="191px" class="colDesc headerColLeft">
                                                        Narration
                                                    </td>
                                                    <td width="101px" class="colDebit headerColRight">
                                                        Debit
                                                    </td>
                                                    <td width="101px" class="colCredit headerColRight">
                                                        Credit
                                                    </td>
                                                    <td width="101px" class="colAction headerColCenter">
                                                        Action
                                                    </td>                                                  
                                                </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                        <div id="dvGrid" style="width: 100%; height: 200px; overflow: auto;" class="dvGrid">
                                            <div id="dvGridRowContainer" style="width: 100%; height: auto;">
                                            <!-- here will be the row from row template   -->
                                            
                                            </div>
                                        </div>
                                        <div id="dvGridFooter" style="width: 100%; height: 25px; border-top: solid 1px #C0C0C0;">
                                            <table style="width: auto; height: 100%; text-align: center;" cellspacing="1" cellpadding="1"
                                                border="0">
                                                <tbody>
                                                <tr>
                                                    <td style="width: 2px">
                                                    </td>
                                                    <td style="width: 90px" align="left">
                                                        <input id="btnNewRowGrid" type="button" value="" class="buttonNewRow" onclick="addNewRow()" />
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                     
                                                    </td>
                                                    <td>
                                                       
                                                    </td>
                                                    <td width="290px;">
                                                    </td>
                                                    <td align="right">
                                                       <span style="font-weight:bold;">Total:</span>
                                                    </td>
                                                    <td width="100px">
                                                        <input type="text" class="txtTotDebit textLabelWithBorder textNumberOnly" style="font-weight:bold; width: 100px;" tabindex="-1"
                                                            readonly="readonly" />
                                                    </td>
                                                    <td width="100px">
                                                        <input type="text" class="txtTotCredit textLabelWithBorder textNumberOnly" style="font-weight:bold; width: 100px;" tabindex="-1"
                                                            readonly="readonly" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                   <td>
                                                   </td>
                                                   <td align="left">
                                                     <span>Total Row: </span>
                                                     <span class="spnGridRowCount">0</span> 
                                                   </td>
                                                   <td>
                                                     
                                                   </td>
                                                   <td>
                                                   </td>
                                                   <td>
                                                   </td>
                                                   <td>
                                                   </td>
                                                   <td align="right">
                                                      <span style="font-weight:bold;">Diffenence:</span>
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
                        <input id="btnAddNew" type="button" value="New" class="buttonNew" />
                        <input id="btnCancel" type="button" value="Cancel" class="buttonCancel checkIsDirty" />
                    </td>
                    <td>
                        <input id="btnSave" type="button" value="Save" class="buttonSave checkRequired" accesskey="s" />
                        <input id="btnEdit" type="button" value="Edit" class="buttonEdit" />
                    </td>
                    <td>
                        <input id="btnJournalPost" type="button" value="Post" class="buttoncommon" />
                    </td>
                    <td>
                        <input id="btnDelete" type="button" value="Delete" class="buttonDelete" />

                    </td>
                    <td>
                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" 
                            CssClass="buttonRefresh checkIsDirty" onclick="btnRefresh_Click" />
                    </td>
                    <td>
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
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="dvTemplateContainer hidden ">
        <div class="dvGridRowTemplate dvGridRow" style="width: 100%; height: auto; border-bottom: 1px solid grey;">
            <table class="tblGridRow" border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr class="gridRow" align="left" valign="top">
                    <td width="28px" align="center" >
                        <span class="rowSLNo">1</span>
                    </td>
                    <td >
                        <div class="dvGridRowWrapper" style="width: auto; height: auto;">
                            <div class="dvGridRowInfo hidden">
                                <input class="hdnJournalDetID" type="hidden" value = "0" />
                                <input class="hdnRecordStateInt" type="hidden" value = "0" />
                                <input class="hdnGLGroupID" type="hidden" value = "0" />
                                <input class="hdnGLGroupIDEdit" type="hidden" value = "0" />
                                <input class="hdnGLGroupClassID" type="hidden" value = "0" />
                                <input class="hdnGLAccountID" type="hidden" value = "0" />
                                <input class="hdnGLAccountIDEdit" type="hidden" value = "0" />
                                <input class="hdnGLGroupIDAcc" type="hidden" value = "0" />
                                
                                <input class="hdnIsInstrument" type="hidden" value = "0" />
                                <input class="hdnIsCash" type="hidden" value = "0" />
                                
                                <input class="hdnTranTypeID" type="hidden" value = "0" />
                                <input class="hdnTranTypeCategoryID" type="hidden" value = "0" />
                            
                            </div>
                            <div class="dvGridRowData" style="text-align:left;">
                                <table class="tblGridRowData" border="0" cellpadding="1" cellspacing="1">
                                    <tr align="left">
                                        <td width="60px">
                                            <input type="text" class="txtGLGroup textBox textAutoSelect" style="width: 60px;" />
                                        </td>
                                        <td width="18px">
                                            <input type="button" value="" runat="server" class="btnGLGroupAC buttonDropdown"
                                                tabindex="-1" />
                                        </td>
                                        <td width="80px">
                                            <input type="text" class="txtGLAccount textBox textAutoSelect" style="width: 80px;" />
                                        </td>
                                        <td width="18px">
                                            <input type="button" value="" runat="server" class="btnGLAccountAC buttonDropdown"
                                                tabindex="-1" />
                                        </td>
                                        <td width="60px">
                                            <input type="text" class="txtTranType textBox textAutoSelect" style="width: 60px;" />
                                        </td>
                                        <td width="18px">
                                            <input type="button" value="" class="btnTranTypeAC buttonDropdown" 
                                                tabindex="-1" />
                                        </td>
                                        <td width="190px">
                                            <textarea class="txtJournalDetDesc textBox textAutoSelect textAreaAutoSize" style="width:188px;" cols="20" rows="2"></textarea>
                                        </td>
                                        <td width="100px">
                                            <input type="text" class="txtDebit textBox textNumberOnly textCurrencyFormat textAutoSelect"
                                                style="width: 100px;" />
                                        </td>
                                        <td width="100px">
                                            <input type="text" class="txtCredit textBox textNumberOnly textCurrencyFormat textAutoSelect"
                                                style="width: 100px;" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="dvGridRowData2">
                                <table class="tblGridRowData2" border="0" cellpadding="1" cellspacing="1">
                                    <tr>
                                        <td width="80px">
                                            <input type="text" class="txtGLGroupName textLabel" style="width: 78px;" tabindex="-1"
                                                readonly="readonly" />
                                        </td>
                                        <td width="150px">
                                            <input type="text" class="txtGLAccountName textLabel" style="width: 148px;" tabindex="-1"
                                                readonly="readonly" />
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="dvGridRowInstrument" style="visibility:hidden;display:none;">
                                <div class="dvGridRowInstrumentHeader">
                                    Instruments
                                </div> 
                                 <table class="tblGridRowDataIns" border="0" cellpadding="1" cellspacing="1">
                                     <tr>
                                        <td width="100px">   
                                         
                                        </td> 
                                        <td width="80px">
                                            <input type="text" class="txtInsNo textBox" style="width: 78px;" tabindex="-1"
                                                />
                                        </td>
                                        <td width="150px">
                                            <input type="text" class="txtInsType textBox" style="width: 148px;" tabindex="-1"
                                                />
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                 </table>
                                <br />
                             </div>

                            <div class="dvGridRowCostRef" style="visibility:hidden;display:none;">
                                 <table class="tblGridRowDataIns" border="0" cellpadding="1" cellspacing="1">
                                     <tr>
                                        <td width="100px">   
                                         Instruments
                                        </td> 
                                        <td width="80px">
                                            <input type="text" class="txtInsNo textBox" style="width: 78px;" tabindex="-1"
                                                />
                                        </td>
                                        <td width="150px">
                                            <input type="text" class="txtInsType textBox" style="width: 148px;" tabindex="-1"
                                                />
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                 </table>
                                <br />
                             </div>


                        </div>
                    </td>
                    <td width="40px" align="left" valign="top">
                        <input type="button" value="" runat="server" class="btnDeleteRow buttonDeleteIcon"
                            tabindex="-1" />
                    </td>
                </tr>
            </table>
        </div>
    </div>

    <div id="dvPopupJournalType" class="dvPopupJournalType hidden ">
         <div class="dvPopupJournalTypeInner">
                <div class="dvPopupJournalTypeHeader subHeader ui-corner-all" style="font-size: smaller; display:none;">
                      Select Journal Type
                </div>

                 <div class="dvPopupJournalTypeContent">
                    <div class="dvPopupJournalTypeContentInner">
                        <asp:RadioButtonList ID="rblJournalType" runat="server" CellPadding="2" 
                            CellSpacing="2">
                        </asp:RadioButtonList>



                    </div>
                </div>


            <div class="dvPopupJournalTypeFooter ">
                <div class="dvPopupJournalTypeFooterInner">
                    <table align="right">
                        <tr>
                            <td>
                            </td>
                            <td>
                               
                            </td>
                            <td style="width: 100px;">
                            </td>
                            <td>
                                <input class="btnPopupJournalTypeOK buttonOK" type="button" value="OK" />
                            </td>
                            <td>
                                <input class="btnPopupJournalTypeCancel buttonCancel stopEnterToTab" type="button" value="Cancel" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>


         </div>
    </div>

</asp:Content>
