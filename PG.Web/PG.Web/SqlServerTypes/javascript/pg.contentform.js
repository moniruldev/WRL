//Version: 2.2.0
//Date: June 02, 2014


/// <reference path="pg.jsutility.js" /
/// <reference path="pg.enums.js" />

/// <reference path="jquery-latest.min.js" />
/// <reference path="jquery-ui-latest.min.js" />

$(document).ready(function () {
    if (typeof IForm !== 'undefined') {
        if (IForm.InitDefaultFeature) {
            var pageInstance = Sys.WebForms.PageRequestManager.getInstance();
            pageInstance.add_pageLoaded(function (sender, args) {
                var panels = args.get_panelsUpdated();
                for (i = 0; i < panels.length; i++) {
                    ContentForm.InitDefualtFeatureInScope(panels[i].id);
                }
            });
            ContentForm.InitDefualtFeature();
        }
    }
});

var ContentForm = new function () {

    this.RootPath = "";
    this.GetDataInfoURL = "Service/GetDataInfo.ashx";
    this.FnNameParseDate = "parsedate";

    this.NumberFormatStyle = 'ban';
    this.DecimalPlaces = 2;
    this.DecimalSeparator = '.';
    this.ThousandSeparator = ',';


    this.DateFromat = 'dd-M-yy';
    this.CalendarImageURL = '../image/calendar.png';


    this.ClassNames = {
        textDate: 'textDate',
        dateParse: 'dateParse',
        fldRequiredMark: 'fldRequiredMark',
        lblRequiredMark: 'lblRequiredMark',
        fldRequired: 'fldRequired',
        lblRequired: 'lblRequired',
        checkRequired: 'checkRequired',
        textAreaAutoSize: 'textAreaAutoSize',
        textNumberOnly: 'textNumberOnly',
        fldDataError: 'fldDataError',
        textNumberFormat: 'textNumberFormat',
        textCurrencyFormat: 'textCurrencyFormat',

        textAutoSelect: 'textAutoSelect',

        notEnterToTab: 'notEnterToTab',
        stopEnterToTab: 'stopEnterToTab',

        enableIsDirty: 'enableIsDirty',
        isDirty: 'isDirty',
        checkIsDirty: 'checkIsDirty',

        enableConfirm: 'enableConfirm',

        l: ""
    };


    this.InitDefualtFeature = function () {
        JSUtility.DisableBackSpace();
        //JSUtility.DisableReadOnlyBackSpace();
        this.InitJQueryDate();
        this.DateParse();
        this.TextAutoSelect();
        this.TextNumberOnly();
        this.TextNumberFormat();
        this.TextCurrencyFormat();
        //this.AddRequiredFieldMark();
        //this.AddRequiredFieldMarkOnly();
        this.TextAreaAutoSize();
        this.ApplyEnterToTab();
        this.ApplyIsConfirm();
        this.ApplyCheckRequired();
        this.ApplyIsDirty();

    };

    this.InitDefualtFeatureInScope = function (pScope) {
        //JSUtility.DisableReadOnlyBackSpaceInScope(pScope);
        this.InitJQueryDateInScope(pScope);
        this.DateParseInScope(pScope);
        this.TextAutoSelectInScope(pScope);
        this.TextNumberOnlyInScope(pScope);
        this.TextNumberFormatInScope(pScope);
        this.TextCurrencyFormatInScope(pScope);
        //this.AddRequiredFieldMarkInScope(pScope);
        //this.AddRequiredFieldMarkOnlyInScope(pScope);
        this.TextAreaAutoSizeInScope(pScope);
        this.ApplyEnterToTabInScope(pScope);
        this.ApplyIsConfirmInScope(pScope);
        this.ApplyCheckRequiredInScope(pScope);
        this.ApplyIsDirtyInScope(pScope);
    };

    this.InitDefualtFeatureInScopeElement = function (pScopeElement) {
        //JSUtility.DisableReadOnlyBackSpaceInScope(pScope);
        this.InitJQueryDateInScopeElement(pScopeElement);
        this.DateParseInScopeElement(pScopeElement);
        this.TextAutoSelectInScopeElement(pScopeElement);
        this.TextNumberOnlyInScopeElement(pScopeElement);
        this.TextNumberFormatInScopeElement(pScopeElement);
        this.TextCurrencyFormatInScopeElement(pScopeElement);
        //this.AddRequiredFieldMarkInScopeElement(pScopeElement);
        //this.AddRequiredFieldMarkOnlyInScopeElement(pScopeElement);
        this.TextAreaAutoSizeInScopeElement(pScopeElement);
        //this.ApplyEnterToTabInScopeElement(pScopeElement);
        //this.ApplyIsConfirmInScopeElement(pScopeElement);
        //this.ApplyCheckRequiredInScopeElement(pScopeElement);
        //this.ApplyIsDirtyInScopeElement(pScopeElement);
    }

    this.InitJQueryDateInScope = function (pScope, pClassName, pIsButton, pDateFormat) {
        this.InitJQueryDate(pClassName, pIsButton, pDateFormat, pScope);
    };


    this.InitJQueryDate = function (pClassName, pIsButton, pDateFormat, pScope) {

        pClassName = pClassName || this.ClassNames.textDate;
        pDateFormat = pDateFormat || this.DateFromat;
        pIsButton = pIsButton || true;
        pScope = pScope || '';



        pShowOn = pIsButton ? 'button' : 'focus';
        pClassName = pClassName == '' ? this.ClassNames.textDate : pClassName;
        pDateFormat = pDateFormat == '' ? this.DateFromat : pDateFormat;
        //imgURL = "../image/calendar.png";
        imgURL = this.CalendarImageURL;

        var pickerOpts = {
            dateFormat: pDateFormat,
            changeMonth: true,
            changeYear: true,
            constrainInput: true,
            showOtherMonths: true,
            selectOtherMonths: false,
            yearRange: "-10:+10",
            appendText: '',
            showOn: pShowOn,  //focus, button, both
            buttonText: 'Select date',
            buttonImageOnly: true,
            buttonImage: imgURL,
            disabled: false,
            showButtonPanel: true,
            defaultDate: null,
            beforeShow: function (input, inst) {
                if ($(input).prop("readonly")) {
                    //   $(input).datepicker("option", "maxDate", '-1');
                    //  $(input).datepicker("option", "minDate", '1');
                    return false;
                }
                else {
                    //    $(input).datepicker("option", "maxDate", null);
                    //   $(input).datepicker("option", "minDate", null);
                }
            },
            onSelect: function (dateText, inst) {
                //alert(dateText);
            }
        };

        var selectorStr = '.' + pClassName
        if (pScope != '') {
            selectorStr = '#' + pScope + ' ' + selectorStr;
        }

        //$(selectorStr + ':not(:disabled):not([readonly])').datepicker(pickerOpts);
        //$(selectorStr).datepicker(pickerOpts);

        $(selectorStr).each(function (index, elem) {

            $(elem).datepicker(pickerOpts);

            if ($(elem).is(":disabled")) {
                $(elem).datepicker("disable");
            }

            $(elem).AttributeObserver('disabled', function (oldValue, newValue) {
                //alert(['Page title changed from ', oldValue, ' to ', newValue].join(''));
                //$('#Text1').val(newValue);
                var isReadOnly = $(elem).is("[readonly]");
                if ($(elem).is(":disabled")) {
                    $(elem).datepicker("disable");
                }
                else {
                    $(elem).datepicker("enable");
                    // if (isReadOnly) {
                    // }
                    // else {
                    //     $(elem).datepicker("enable");
                    // }
                }
            }, 100);
            
            //            $(elem).AttributeObserver('readonly', function (oldValue, newValue) {
            //                //alert(['Page title changed from ', oldValue, ' to ', newValue].join(''));
            //                //$('#Text1').val(newValue);
            //                var isDisabled = $(elem).is(":disabled");

            //                if ($(elem).is("[readonly]")) {
            //                    $(elem).datepicker("disable");
            //                }
            //                else {
            //                    if (isDisabled) {

            //                    }
            //                    else {
            //                        $(elem).datepicker("enable");
            //                    }
            //                    //$(elem).datepicker(isReadOnly ? "disable" : "enable");
            //                }
            //            }, 500);



        });

        //move dtpicker element to top left for not apperring the scrollbar
        //ui-datepicker-div
        //$('#ui-datepicker-div').css({ 'top' : '0px', 'left': '0px' });
        $('#ui-datepicker-div').hide();
        $('.ui-datepicker-trigger').css({ 'margin-left': '3px', 'vertical-align': 'middle', 'cursor': 'pointer' });

    };

    this.InitJQueryDateInScopeElement = function (pScopeElement, pClassName, pIsButton, pDateFormat) {

        if (!pScopeElement) {
            return;
        }

        pClassName = pClassName || this.ClassNames.textDate;
        pScopeElement = pScopeElement || '';
        pDateFormat = pDateFormat || this.DateFromat;
        pIsButton = pIsButton || true;



        pShowOn = pIsButton ? 'button' : 'focus';
        pClassName = pClassName == '' ? this.ClassNames.textDate : pClassName;
        pDateFormat = pDateFormat == '' ? this.DateFromat : pDateFormat;
        //imgURL = "../image/calendar.png";
        imgURL = this.CalendarImageURL;

        var pickerOpts = {
            dateFormat: pDateFormat,
            changeMonth: true,
            changeYear: true,
            constrainInput: true,
            showOtherMonths: true,
            selectOtherMonths: false,
            yearRange: "-10:+10",
            appendText: '',
            showOn: pShowOn,  //focus, button, both
            buttonText: 'Select date',
            buttonImageOnly: true,
            buttonImage: imgURL,
            showButtonPanel: true
        };

        var selectorStr = '.' + pClassName
        //        if (pScope != '') {
        //            selectorStr = '#' + pScope + ' ' + selectorStr;
        //        }

        //$(pScopeElement).find(selectorStr + ':not(:disabled):not([readonly])').datepicker(pickerOpts);


        $(pScopeElement).find(selectorStr).each(function (index, elem) {

            $(elem).datepicker(pickerOpts);

            if ($(elem).is(":disabled")) {
                $(elem).datepicker("disable");
            }

            $(elem).AttributeObserver('disabled', function (oldValue, newValue) {
                //alert(['Page title changed from ', oldValue, ' to ', newValue].join(''));
                //$('#Text1').val(newValue);

                if ($(elem).is(":disabled")) {
                    $(elem).datepicker("disable");
                }
                else {
                    $(elem).datepicker("enable");
                }
            }, 500);
        });



        //move dtpicker element to top left for not apperring the scrollbar
        //ui-datepicker-div
        //$('#ui-datepicker-div').css({ 'top' : '0px', 'left': '0px' });

        //$('#ui-datepicker-div').hide();
        $('.ui-datepicker-trigger').css({ 'margin-left': '3px', 'vertical-align': 'middle', 'cursor': 'pointer' });
    };



    this.DateParseInScope = function (pScope, pClassName) {
        this.DateParse(pClassName, pScope);
    };

    this.DateParse = function (pClassName, pScope) {
        var self = this;
       
        pClassName = pClassName || this.ClassNames.dateParse;
        pScope = pScope || '';

        pClassName = pClassName == '' ? this.ClassNames.dateParse : pClassName;

        var selectorStr = '.' + pClassName
        if (pScope != '') {
            selectorStr = '#' + pScope + ' ' + selectorStr;
        }

        //  $(selectorStr + ':not(:disabled):not([readonly])').each(function (index, elem) {

        $(selectorStr).each(function (index, elem) {
            //check dtpicker trigger
            // ui-datepicker-trigger
            $(elem).blur(function () {

                if ($(elem).is(":disabled")) {
                    return false;
                }
                if ($(elem).is("[readonly]")) {
                    return false;
                }

                var strDate = $(elem).val();
                if (strDate != '') {
                    var parsedDate = self.GetDateParse(strDate);
                    if (parsedDate != '') {
                        $(elem).val(parsedDate)
                    }
                }
            });
        });

    };


    this.DateParseInScopeElement = function (pScopeElement, pClassName) {
        var self = this;

        if (!pScopeElement) {
            return;
        }

        pClassName = pClassName || this.ClassNames.textDate;
        pScopeElement = pScopeElement || '';


        pClassName = pClassName == '' ? this.ClassNames.dateParse : pClassName;

        var selectorStr = '.' + pClassName
        //        if (pScope != '') {
        //            selectorStr = '#' + pScope + ' ' + selectorStr;
        //        }

        //  $(pScopeElement).find(selectorStr + ':not(:disabled):not([readonly])').each(function (index, elem) {

        $(pScopeElement).find(selectorStr).each(function (index, elem) {
            //check dtpicker trigger
            // ui-datepicker-trigger
            $(elem).blur(function () {

                if ($(elem).is(":disabled")) {
                    return false;
                }
                if ($(elem).is("[readonly]")) {
                    return false;
                }

                var strDate = $(elem).val();
                if (strDate != '') {
                    var parsedDate = self.GetDateParse(strDate);
                    if (parsedDate != '') {
                        $(elem).val(parsedDate)
                    }
                }
            });
        });

    };


    this.GetDateParse = function (strDate) {
        var fnName = this.FnNameParseDate;
        var serviceURL = this.RootPath + this.GetDataInfoURL + "?fn=" + fnName + "&strdate=" + strDate;

        var parsedDate = '';

        var dummyVar = $.ajax({
            type: "GET",
            cache: false,
            async: false,
            dataType: "json",
            url: serviceURL,
            success: function (data) {
                parsedDate = data.date.datestring;
            },
            complete: function () {
                //if (!isError) {
                //return;
                //alert (menu.name);
                //}
                //isComplete = true;
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                //isError = true;
                alert(textStatus);
            }
        });
        dummyVar = null;
        return parsedDate;

    };


    this.AddRequiredFieldMarkOnlyInScope = function (pScope, pClassName, pClassNameMark) {
        this.AddRequiredFieldMarkOnly(pClassName, pClassNameMark, pScope);
    };

    this.AddRequiredFieldMarkOnly = function (pClassName, pClassNameMark, pScope) {

        pClassName = pClassName || this.ClassNames.fldRequiredMark;
        pClassNameMark = pClassNameMark || this.ClassNames.lblRequiredMark;
        pScope = pScope || '';

        pClassName = pClassName == '' ? this.ClassNames.fldRequiredMark : pClassName;
        pClassNameMark = pClassNameMark == '' ? this.ClassNames.lblRequiredMark : pClassNameMark;


        var selectorStr = '.' + pClassName
        if (pScope != '') {
            selectorStr = '#' + pScope + ' ' + selectorStr;
        }

        $(selectorStr + ':not(:disabled):not([readonly])').each(function (index, elem) {
            //check dtpicker trigger
            // ui-datepicker-trigger
            var nextElem = $(elem).next();
            var insElem = elem;

            if (nextElem != null) {
                if ($(nextElem).hasClass('ui-datepicker-trigger')) {
                    insElem = nextElem;
                }
            }
            $('<span class="' + pClassNameMark + '">*</span>').insertAfter(insElem);
            insElem = null;
            nextElem = null;
        });


    };


    this.AddRequiredFieldMarkInScope = function (pScope, pClassName, pClassNameMark) {
        this.AddRequiredFieldMark(pClassName, pClassNameMark, pScope);
    };

    this.AddRequiredFieldMark = function (pClassName, pClassNameMark, pScope) {

        pClassName = pClassName || this.ClassNames.fldRequired;
        pClassNameMark = pClassNameMark || this.ClassNames.lblRequired;
        pScope = pScope || '';


        pClassName = pClassName == '' ? this.ClassNames.fldRequired : pClassName;
        pClassNameMark = pClassNameMark == '' ? this.ClassNames.lblRequired : pClassNameMark;


        var selectorStr = '.' + pClassName
        if (pScope != '') {
            selectorStr = '#' + pScope + ' ' + selectorStr;
        }

        $(selectorStr + ':not(:disabled):not([readonly])').each(function (index, elem) {
            //check dtpicker trigger
            // ui-datepicker-trigger
            var nextElem = $(elem).next();
            var insElem = elem;

            if (nextElem != null) {
                if ($(nextElem).hasClass('ui-datepicker-trigger')) {
                    insElem = nextElem;
                }
            }
            $('<span class="' + pClassNameMark + '">*</span>').insertAfter(insElem);
            insElem = null;
            nextElem = null;
        });
    };



    this.CheckRequiredField = function (pClassName, pScope) {

        pClassName = pClassName || this.ClassNames.fldRequired;
        pScope = pScope || '';

        pClassName = pClassName == '' ? this.ClassNames.fldRequired : pClassName;

        var selectorStr = '.' + pClassName;
        if (pScope != '') {
            selectorStr = '#' + pScope + ' ' + selectorStr;
        }

        var result = true;

        $(selectorStr + ':not(select):not(:disabled):not([readonly])').each(function (index, elem) {
            if ($(elem).val() == '') {
                alert('Please Insert Data');
                $(elem).focus();
                result = false;
                return false;
            }
        });
        return result;
    };

    this.ApplyCheckRequiredInScope = function (pScope, pClassName, pClassRequired, pScopeRequired) {
        this.ApplyCheckRequired(pClassName, pScope, pClassRequired, pScopeRequired);
    };

    this.ApplyCheckRequired = function (pClassName, pScope, pClassRequired, pScopeRequired) {
        var self = this;
        pClassName = pClassName || this.ClassNames.checkRequired;
        pScope = pScope || '';


        pClassName = pClassName == '' ? this.ClassNames.checkRequired : pClassName;

        var selectorStr = '.' + pClassName;
        if (pScope != '') {
            selectorStr = '#' + pScope + ' ' + selectorStr;
        }

        $(selectorStr).click(function () {
            var result = self.CheckRequiredField(pClassRequired, pScopeRequired);
            return result;
        });

    };



    this.TextAreaAutoSizeInScope = function (pScope, pClassName) {
        this.TextAreaAutoSize(pClassName, pScope);
    };

    this.TextAreaAutoSize = function (pClassName, pScope) {

        pClassName = pClassName || this.ClassNames.textAreaAutoSize;
        pScope = pScope || '';


        pClassName = pClassName == '' ? this.ClassNames.textAreaAutoSize : pClassName;

        var selectorStr = 'textarea.' + pClassName
        if (pScope != '') {
            selectorStr = '#' + pScope + ' ' + selectorStr;
        }
        $(selectorStr).autosize();
    };

    this.TextAreaAutoSizeInScopeElement = function (pScopeElement, pClassName) {

        if (!pScopeElement) {
            return;
        }

        pClassName = pClassName || this.ClassNames.textAreaAutoSize;
        pScopeElement = pScopeElement || '';


        pClassName = pClassName == '' ? this.ClassNames.textAreaAutoSize : pClassName;

        var selectorStr = 'textarea.' + pClassName
        //if (pScope != '') {
        //    selectorStr = '#' + pScope + ' ' + selectorStr;
        //}
        $(pScopeElement).find(selectorStr).autosize();
    };



    this.TextNumberOnlyInScope = function (pScope, pClassName) {
        this.TextNumberOnly(pClassName, pScope);
    };

    this.TextNumberOnly = function (pClassName, pScope) {
        var self = this;
        pClassName = pClassName || this.ClassNames.textNumberOnly;
        pScope = pScope || '';

        pClassName = pClassName == '' ? this.ClassNames.textNumberOnly : pClassName;

        var selectorStr = 'input.' + pClassName;
        if (pScope != '') {
            selectorStr = '#' + pScope + ' ' + selectorStr;
        }

        $('input.' + pClassName).blur(function () {
            var strData = $(this).val();
            strData = JSUtility.RemoveCommas(strData);
            if (JSUtility.IsNumber(strData)) {
                $(this).removeClass(self.ClassNames.fldDataError);
            }
            else {
                $(this).addClass(self.ClassNames.fldDataError);
                $(this).one('keypress', function () {
                    $(this).removeClass(self.ClassNames.fldDataError);
                });
            }
        });

    };

    this.TextNumberOnlyInScopeElement = function (pScopeElement, pClassName) {
        var self = this;

        if (!pScopeElement) {
            return;
        }

        pClassName = pClassName || this.ClassNames.textNumberOnly;
        pScopeElement = pScopeElement || '';

        pClassName = pClassName == '' ? this.ClassNames.textNumberOnly : pClassName;

        var selectorStr = 'input.' + pClassName;
        //        if (pScope != '') {
        //            selectorStr = '#' + pScope + ' ' + selectorStr;
        //        }

        $(pScopeElement).find('input.' + pClassName).blur(function () {
            var strData = $(this).val();
            strData = JSUtility.RemoveCommas(strData);
            if (JSUtility.IsNumber(strData)) {
                $(this).removeClass(self.ClassNames.fldDataError);
            }
            else {
                $(this).addClass(self.ClassNames.fldDataError);
                $(this).one('keypress', function () {
                    $(this).removeClass(self.ClassNames.fldDataError);
                });
            }
        });

    };

    this.TextNumberFormatInScope = function (pScope, pClassName, pFormatStyle, pDecimalPlaces) {
        this.TextNumberFormat(pClassName, pScope, pFormatStyle, pDecimalPlaces);
    };

    this.TextNumberFormat = function (pClassName, pScope, pFormatStyle, pDecimalPlaces) {
        var self = this;
        pClassName = pClassName || this.ClassNames.textNumberFormat;
        pScope = pScope || '';
        //pFormatStyle = pFormatStyle || self.NumberFormatStyle

        pClassName = pClassName == '' ? self.ClassNames.textNumberFormat : pClassName;

        var selectorStr = 'input.' + pClassName;
        if (pScope != '') {
            selectorStr = '#' + pScope + ' ' + selectorStr;
        }

        $(selectorStr).each(function () {
            var strData = $(this).val();
            //strData = JSUtility.FormatNumber(strData, 0);
            strData = JSUtility.FormatNumber(strData, pDecimalPlaces, pFormatStyle);
            $(this).val(strData);
        });

        $(selectorStr).blur(function () {
            var strData = $(this).val();
            //strData = JSUtility.FormatNumber(strData, 0);
            strData = JSUtility.FormatNumber(strData, pDecimalPlaces, pFormatStyle);
            $(this).val(strData);
        });
    };


    this.TextNumberFormatInScopeElement = function (pScopeElement, pClassName, pFormatStyle, pDecimalPlaces) {
        var self = this;

        if (!pScopeElement) {
            return;
        }

        pClassName = pClassName || this.ClassNames.textNumberFormat;
        pScopeElement = pScopeElement || '';
        //pFormatStyle = pFormatStyle || self.NumberFormatStyle

        pClassName = pClassName == '' ? self.ClassNames.textNumberFormat : pClassName;

        var selectorStr = 'input.' + pClassName;
        //        if (pScope != '') {
        //            selectorStr = '#' + pScope + ' ' + selectorStr;
        //        }

        $(pScopeElement).find(selectorStr).each(function () {
            var strData = $(this).val();
            //strData = JSUtility.FormatNumber(strData, 0);
            strData = JSUtility.FormatNumber(strData, pDecimalPlaces, pFormatStyle);
            $(this).val(strData);
        });

        $(pScopeElement).find(selectorStr).blur(function () {
            var strData = $(this).val();
            //strData = JSUtility.FormatNumber(strData, 0);
            strData = JSUtility.FormatNumber(strData, pDecimalPlaces, pFormatStyle);
            $(this).val(strData);
        });
    };

    this.TextCurrencyFormatInScope = function (pScope, pClassName, pDecimalPlaces) {
        this.TextCurrencyFormat(pClassName, pScope, pDecimalPlaces);
    };

    this.TextCurrencyFormat = function (pClassName, pScope, pFormatStyle, pDecimalPlaces) {
        var self = this;
        pClassName = pClassName || this.ClassNames.textCurrencyFormat;
        pScope = pScope || '';

        pClassName = pClassName == '' ? self.ClassNames.textCurrencyFormat : pClassName;

        //        if (!pDecimalPlaces) {
        //            pDecimalPlaces = self.DecimalPlaces;
        //        }

        //        if (!pFormatStyle) {
        //            pFormatStyle = self.NumberFormatStyle;
        //        }

        var selectorStr = 'input.' + pClassName;
        if (pScope != '') {
            selectorStr = '#' + pScope + ' ' + selectorStr;
        }

        $(selectorStr).each(function () {
            var strData = $(this).val();
            strData = JSUtility.FormatCurrency(strData, pDecimalPlaces, pFormatStyle);
            $(this).val(strData);
        });

        $(selectorStr).blur(function () {
            var strData = $(this).val();
            strData = JSUtility.FormatCurrency(strData, pDecimalPlaces, pFormatStyle);
            $(this).val(strData);
        });
    };

    this.TextCurrencyFormatInScopeElement = function (pScopeElement, pClassName, pFormatStyle, pDecimalPlaces) {
        var self = this;

        if (!pScopeElement) {
            return
        }

        pClassName = pClassName || this.ClassNames.textCurrencyFormat;
        pScopeElement = pScopeElement || '';

        pClassName = pClassName == '' ? self.ClassNames.textCurrencyFormat : pClassName;

        //        pDecimalPlaces = pDecimalPlaces || JSUtility.FormatCurrency_DecimalPlaces;
        //        if (!pDecimalPlaces) {
        //            pDecimalPlaces = self.DecimalPlaces;
        //        }
        //        if (!pFormatStyle) {
        //            pFormatStyle = self.NumberFormatStyle;
        //        }

        var selectorStr = 'input.' + pClassName;
        //        if (pScope != '') {
        //            selectorStr = '#' + pScope + ' ' + selectorStr;
        //        }

        $(pScopeElement).find(selectorStr).each(function () {
            var strData = $(this).val();
            strData = JSUtility.FormatCurrency(strData, pDecimalPlaces, pFormatStyle);
            $(this).val(strData);
        });

        $(pScopeElement).find(selectorStr).blur(function () {
            var strData = $(this).val();
            strData = JSUtility.FormatCurrency(strData, pDecimalPlaces, pFormatStyle);
            $(this).val(strData);
        });
    };

    this.TextAutoSelectInScope = function (pScope, pClassName) {
        this.TextAutoSelect(pClassName, pScope);
    };

    this.TextAutoSelect = function (pClassName, pScope) {
        var self = this;
        pClassName = pClassName || this.ClassNames.textAutoSelect;
        pScope = pScope || '';

        pClassName = pClassName == '' ? self.ClassNames.textAutoSelect : pClassName;

        var selectorStr = '.' + pClassName
        if (pScope != '') {
            selectorStr = '#' + pScope + ' ' + selectorStr;
        }

        //selectorStr = selectorStr + ':not(:disabled)'
        selectorStr = selectorStr;
        $(selectorStr).each(function (index, elem) {
            $(elem).focus(function () {
                var cElem = this;
                if ($(cElem).is(":disabled")) {
                    return false;
                }

                setTimeout(function () {
                    if ($(cElem).is(":focus")) {
                        $(cElem).select();
                    }
                    cElem = null;
                }, 0);
            });
        });
    };


    this.TextAutoSelectInScopeElement = function (pScopeElement, pClassName) {
        var self = this;

        if (!pScopeElement) {
            return;
        }

        pClassName = pClassName || this.ClassNames.textAutoSelect;
        pScopeElement = pScopeElement || '';

        pClassName = pClassName == '' ? self.ClassNames.textAutoSelect : pClassName;
        var selectorStr = '.' + pClassName

        //selectorStr = selectorStr + ':not(:disabled)';
        selectorStr = selectorStr

        $(pScopeElement).find(selectorStr).each(function (index, elem) {
            $(elem).focus(function () {
                var cElem = this;

                if ($(cElem).is(":disabled")) {
                    return false;
                }

                setTimeout(function () {
                    if ($(cElem).is(":focus")) {
                        $(cElem).select();
                    }
                    cElem = null;
                }, 0);
            });
        });
    };




    this.ApplyEnterToTabInScope = function (pScope) {
        this.ApplyEnterToTab(pScope);
    };
    this.ApplyEnterToTab = function (pScope, pExcludeClass) {
        var self = this;

        pExcludeClass = pExcludeClass || '';
        pScope = pScope || '';
        pExcludeClass = pExcludeClass == '' ? this.ClassNames.notEnterToTab : pExcludeClass;

        var selectorStr = 'input:not(.' + pExcludeClass + '), select:not(.' + pExcludeClass + ')';
        if (pScope != '') {
            selectorStr = '#' + pScope + ' input:not(.' + pExcludeClass + '),' + ' #' + pScope + ' select:not(.' + pExcludeClass + ')';
        }

        // $(selectorStr).live("keydown", function (e) {

        $(document).on("keydown", selectorStr, function (e) {
            /* ENTER PRESSED*/
            if (e.keyCode == 13) {
                /* FOCUS ELEMENT */

                e.preventDefault();

                if ($(this).hasClass(self.ClassNames.stopEnterToTab)) {
                    return;
                }

                var inputs = $(document).eq(0).find(':input:visible:not([type = "hidden"]):not(:disabled):not([TabIndex *= "-1"]):not(.' + pExcludeClass + ')');


                if (inputs.length == 0) {
                    return true;
                }
                inputs.sort(function (a, b) {
                    var aTabIndex = $(a).attr('tabindex');
                    var bTabIndex = $(b).attr('tabindex');
                    if (aTabIndex < bTabIndex)
                        return -1;
                    if (aTabIndex > bTabIndex)
                        return 1;
                    return 0;
                });

                var idx = inputs.index(this);

                var shifted = e.shiftKey;

                if (shifted) {
                    if (idx == 0) {
                        inputs[inputs.length - 1].focus();
                        //inputs[0].select()
                    } else {
                        inputs[idx - 1].focus(); //  handles submit buttons
                        //inputs[idx + 1].select();
                    }
                }
                else {
                    if (idx == inputs.length - 1) {
                        inputs[0].focus();
                        //inputs[0].select()
                    } else {
                        inputs[idx + 1].focus(); //  handles submit buttons
                        //inputs[idx + 1].select();
                    }
                }

                //return false;
            }
        });

    };


    this.ApplyEnterToTabByClassInScope = function (pScope, pClassName, pScopeEnter) {
        this.ApplyEnterToTabByClass(pClassName, pScope, pScopeEnter);
    };
    this.ApplyEnterToTabByClass = function (pClassName, pScope, pScopeEnter) {

        var self = this;

        pClassName = pClassName || '';
        pScope = pScope || '';
        pScopeEnter = pScopeEnter || '';


        if (pClassName == '') {
            return;
        }

        //var selectorStr = 'input, select';

        var selectorStr = 'input:.' + pClassName + ', select.' + pClassName;

        //var inputs = $(document).eq(0).find(':input:visible:not(:disabled):not([TabIndex *= "-1"]):not(.' + pExcludeClass + ')');

        var inputsSelector = '.' + pClassName + ':input:visible:not(:disabled)';

        if (pScope != '') {
            selectorStr = '#' + pScope + ' input,' + ' #' + pScope + ' select';
            selectorStr = '#' + pScope + ' input.' + pClassName + ',' + ' #' + pScope + ' select.' + pClassName;

        }


        // $(selectorStr).live("keydown", function (e) {
        // $(selectorStr).on("keydown", function (e) {
        $(document).on("keydown", selectorStr, function (e) {
            /* ENTER PRESSED*/
            if (e.keyCode == 13) {
                /* FOCUS ELEMENT */

                e.preventDefault();

                if ($(this).hasClass(self.ClassNames.stopEnterToTab)) {
                    return;
                }


                // $('#Text1').val($('#Text1').val() + ',' + isEnterToTabProcessing);

                //e.stopPropagation();

                pScopeEnter = pScopeEnter == '' ? pScope : pScopeEnter;

                var inputsSelector = '.' + pClassName + ':input:visible:not([type = "hidden"]):not(:disabled):not([TabIndex *= "-1"])';
                if (pScopeEnter != '') {
                    inputsSelector = '#' + pScopeEnter + ' ' + inputsSelector;
                }
                var inputs = $(document).eq(0).find(inputsSelector);

                if (inputs.length == 0) {
                    return true;
                }
                inputs.sort(function (a, b) {
                    var aTabIndex = $(a).attr('tabindex');
                    var bTabIndex = $(b).attr('tabindex');
                    if (aTabIndex < bTabIndex)
                        return -1;
                    if (aTabIndex > bTabIndex)
                        return 1;
                    return 0;
                });

                var idx = inputs.index(this);

                var shifted = e.shiftKey;

                if (shifted) {
                    if (idx == 0) {
                        inputs[inputs.length - 1].focus();
                        //inputs[0].select()
                    } else {
                        inputs[idx - 1].focus(); //  handles submit buttons
                        //inputs[idx + 1].select();
                    }
                }
                else {
                    if (idx == inputs.length - 1) {
                        inputs[0].focus();
                        //inputs[0].select()
                    } else {
                        inputs[idx + 1].focus(); //  handles submit buttons
                        //inputs[idx + 1].select();
                    }
                }
                //return false;
            }

            //            //tab
            //            if (e.keyCode == 9) {
            //                alert('d');
            //            }


        });

    };




    this.ApplyIsDirtyInScope = function (pScope, pClassName, pClassNameChanged) {

        this.ApplyIsDirty(pClassName, pClassNameChanged, pScope);

    };

    this.ApplyIsDirty = function (pClassName, pClassNameChanged, pScope) {
        if (IForm.IsDirtyEnabled) {
            this.SetOrClearFormIsDirty(pScope);

            pClassName = pClassName || this.ClassNames.enableIsDirty;
            pClassNameChanged = pClassNameChanged || this.ClassNames.isDirty;
            pScope = pScope || '';


            pClassName = pClassName == '' ? this.ClassNames.enableIsDirty : pClassName;
            pClassNameChanged = pClassNameChanged == '' ? this.ClassNames.isDirty : pClassNameChanged;

            var selectorStr = '.' + pClassName;
            if (pScope != '') {
                selectorStr = '#' + pScope + ' ' + selectorStr;
            }

            $(selectorStr).change(function () {
                if ($(this).is(':disabled') == false) {
                    if (IForm.IsFromUser) {
                        IForm.IsDirty = true;
                        $('#' + IForm.HiddenIsDirtyID).val('1');
                        $(this).addClass(pClassNameChanged);

                    }
                }
            });


            //var selectorStrCheck = '.checkIsDirty';
            var selectorStrCheck = '.' + this.ClassNames.checkIsDirty;
            if (pScope != '') {
                selectorStrCheck = '#' + pScope + ' ' + selectorStrCheck;
            }

            $(selectorStrCheck).click(function () {
                return ContentForm.CheckIsDirty();
            });



            //apply window unload if not in tab
            if (IForm.PageMode == Enums.PageMode.None
                | IForm.PageMode == Enums.PageMode.InWindow) {
                this.WindowUnload();

                //disbale it for submit button
                $('form').submit(function () {
                    IForm.IsDirtyEnabled = false;
                });

                $('form a').click(function () {
                    IForm.IsDirtyEnabled = false;
                });

                ///need to disbale for
                /// hyperlink
            }

        }
    };


    this.MakeFormIsDirty = function (isDirty) {
        if (isDirty == undefined) {
            isDirty = false;
        }
        if (IForm.IsDirtyEnabled) {
            if (isDirty) {
                IForm.IsDirty = true;
                $('#' + IForm.HiddenIsDirtyID).val('1');
            }
            else {
                IForm.IsDirty = fasle;
                $('#' + IForm.HiddenIsDirtyID).val('0');
            }
        }
    };

    this.MakeControlIsDirty = function (cntrolID, isDirty) {
        if (isDirty == undefined) {
            isDirty = false;
        }
        if (IForm.IsDirtyEnabled) {
            if (isDirty) {
                $('#' + cntrolID).addClass(this.ClassNames.isDirty);
                IForm.IsDirty = true;
                $('#' + IForm.HiddenIsDirtyID).val('1');
            }
            else {
                $('#' + cntrolID).removeClass(this.ClassNames.isDirty);
                this.SetOrClearFormIsDirty();
            }
        }
    };



    this.WindowUnload = function () {
        var self = this;
        $(window).bind('beforeunload', function () {
            if (IForm.IsDirtyEnabled) {
                var result = self.CheckIsDirty(false);
                if (result == false) {
                    return "Data Changed! click OK to discard changes OR click Cancel to stay?"
                };
            }
        });
    };

    this.ClearIsDirty = function (pClassName, pClassNameChanged, pScope) {
        this.ClearIsDirty(pScope, pClassName, pClassNameChanged);
    };
    this.ClearIsDirty = function (pClassName, pClassNameChanged, pScope) {

        pClassName = pClassName || this.ClassNames.checkIsDirty;
        pClassNameChanged = pClassNameChanged || this.ClassNames.isDirty;
        pScope = pScope || '';


        var selectorStr = '.' + pClassName;
        if (pScope != '') {
            selectorStr = '#' + pScope + ' ' + selectorStr;
        }
        $(selectorStr).removeClass(pClassNameChanged);
        IForm.IsDirty = false;
        $('#' + IForm.HiddenIsDirtyID).val('0');
    };

    this.CheckFormIsDirtyInScope = function (pScope) {
        this.CheckFormIsDirty(pScope)
    };

    this.CheckFormIsDirty = function (pScope) {

        pScope = pScope || '';

        var selectorStr = '.' + this.ClassNames.isDirty;
        if (pScope != '') {
            selectorStr = '#' + pScope + ' ' + selectorStr;
        }
        curIsDirtyCount = $(selectorStr).length;
        return curIsDirtyCount > 0;
    };


    this.SetOrClearFormIsDirtyInScope = function (pScope) {
        this.SetOrClearFormIsDirty(pScope);
    };

    this.SetOrClearFormIsDirty = function (pScope) {
        if (this.CheckFormIsDirty(pScope)) {
            IForm.IsDirty = true;
            $('#' + IForm.HiddenIsDirtyID).val('1');
        }
        else {
            IForm.IsDirty = false;
            $('#' + IForm.HiddenIsDirtyID).val('0');
        }
    };


    this.CloseForm = function () {
        var retValue = true;

        if (IForm.PageMode == Enums.PageMode.InTab) {
            retValue = window.parent.TabMenu.CloseCurrentTab();
        }
        else {
            window.close();
        }
        return retValue;

    };

    this.CheckIsDirty = function (autoMessage) {
        var result = true;
        autoMessage = autoMessage || true;
        //        if (autoMessage == undefined) {
        //            autoMessage = true;
        //        }

        if (typeof IForm !== 'undefined') {
            if (IForm.IsDirty) {
                if (autoMessage) {
                    result = confirm("Data Changed! click OK to discard changes OR click Cancel to stay?")
                }
                else {
                    result = false;
                }

            }
        }
        //result: true = progress, false= stay
        return result;
    };


    this.ApplyIsConfirmInScope = function (pScope, pClassName) {
        this.ApplyIsConfirm(pClassName, pScope);
    };

    this.ApplyIsConfirm = function (pClassName, pScope) {

        pClassName = pClassName || this.ClassNames.enableConfirm;
        pScope = pScope || '';

        pClassName = pClassName == '' ? this.ClassNames.enableConfirm : pClassName;

        var selectorStr = '.' + pClassName;
        if (pScope != '') {
            selectorStr = '#' + pScope + ' ' + selectorStr;
        }

        $(selectorStr).click(function () {
            var result = true;
            result = confirm("Are you sure to continue?");
            return result;
        });

    };

    //////////
}                                                                                          //end of class function