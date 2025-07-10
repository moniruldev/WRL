
//Version: 4.2.0
//Date: September 02, 2014


/// <reference path="../jquery-latest.min.js" />
/// <reference path="PG.enums.js" />

var JSUtility = new function () {
    this.Keys = Enums.Keys;

    this.FormatNumber_FormatStyle = 'ban'; //eng,ban,ind
    this.FormatNumber_DecimalPlaces = 0;
    this.FormatNumber_CurrencySymbol = '';
    this.FormatNumber_ThousandSymbol = ',';
    this.FormatNumber_DecimalSymbol = '.';

    this.FormatCurrency_FormatStyle = 'ban'; //eng,ban,ind
    this.FormatCurrency_DecimalPlaces = 2;
    this.FormatCurrency_CurrencySymbol = '';
    this.FormatCurrency_ThousandSymbol = ',';
    this.FormatCurrency_DecimalSymbol = '.';


    this.GetRootPath = function () {
        return location.protocol + '//' + location.host;
    };

    this.isIE = function () {
        var ua = window.navigator.userAgent;
        var msie = ua.indexOf("MSIE ");
        if (msie > 0) {      // If Internet Explorer, return version number
            return true;
        }
        else {
            return false;
        }
    };


    this.IsIE6 = function () {
        //var isIE = navigator.userAgent.indexOf("MSIE") > -1;

        var ua = window.navigator.userAgent;
        var msie = ua.indexOf("MSIE ");

        if (msie > 0)      // If Internet Explorer, return version number
        {
            var version = parseInt(ua.substring(msie + 5, ua.indexOf(".", msie)));
            if (version <= 6)
                return true;
            else
                return false;
        }

        else {            // If another browser, return 0
            return false;
        }
    };

    this.IsIE7 = function () {
        //var isIE = navigator.userAgent.indexOf("MSIE") > -1;

        var ua = window.navigator.userAgent;
        var msie = ua.indexOf("MSIE ");

        if (msie > 0)      // If Internet Explorer, return version number
        {
            var version = parseInt(ua.substring(msie + 5, ua.indexOf(".", msie)));
            if (version <= 7)
                return true;
            else
                return false;
        }

        else {            // If another browser, return 0
            return false;
        }

    };

    this.GetViewPortWidth = function () {
        //retruns browser view port width;
        var width = 0;

        if ((document.documentElement) && (document.documentElement.clientWidth)) {
            width = document.documentElement.clientWidth;
        }
        else if ((document.body) && (document.body.clientWidth)) {
            width = document.body.clientWidth;
        }
        else if (window.innerWidth) {
            width = window.innerWidth;
        }

        return width;

    };


    this.GetViewPortHeight = function () {
        //retruns browser view port height;

        var height = 0;

        if (window.innerHeight) {
            height = window.innerHeight;
        }
        else if ((document.documentElement) && (document.documentElement.clientHeight)) {
            height = document.documentElement.clientHeight;
        }

        return height;
    };



    this.AddJavascript = function (jsname, pos) {
        var th = document.getElementsByTagName(pos)[0];
        var s = document.createElement('script');
        s.setAttribute('type', 'text/javascript');
        s.setAttribute('src', jsname);
        th.appendChild(s);
    };


    this.GetQueryString = function (key, url, default_) {
        if (default_ == null) default_ = "";
        key = key.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
        var regex = new RegExp("[\\?&]" + key + "=([^&#]*)");
        var qs = regex.exec(url);
        if (qs == null)
            return default_;
        else
            return qs[1];
    };

    this.GetQueryStringInt = function (key, url, default_) {
        if (default_ == null) default_ = 0;
        var qs = this.GetQueryString(key, url, default_);
        if (qs == null) {
            return default_;
        }
        else {
            var qsInt = parseInt(qs);
            if (isNaN(qsInt)) {
                return default_;
            }
            else {
                return qsInt;
            }
        }
    };



    this.ReplaceQueryString = function (url, param, value) {
        var re = new RegExp("([?|&])" + param + "=.*?(&|$)", "i");
        if (url.match(re))
            return url.replace(re, '$1' + param + "=" + value + '$2');
        else if (url.indexOf("?") == -1)
            return url + '?' + param + "=" + value;
        else
            return url + '&' + param + "=" + value;
    };



    this.AddTimeToQueryString = function (url, param) {
        var self = this;
        param = param || '_tt';
        var tt = new Date().getTime()
        return self.ReplaceQueryString(url, param, tt);
    };

    this.KeyValuePair = function () {
        //Assigning the Key from the arguments
        this._strKey = (arguments.length == 2) ? arguments[0] : null;
        //Assigning the Key from the arguments
        this._strValue = (arguments.length == 2) ? arguments[1] : null;

        // if parameter is defined, the key is set.
        // if parameter is not defined, the key is retrieved.
        this.Key = function () {
            if (arguments.length == 1)
                this._strKey = arguments[0];
            else
                return this._strKey;
        }
        // if parameter is defined, the value is set.
        // if parameter is not defined, the value is retrieved.
        this.Value = function () {
            if (arguments.length == 1)
                this._strValue = arguments[0];
            else
                return this._strValue;
        }
        // Params: (KeyValuePair objkvp)
        // Return: boolean
        this.Equals = function (oKV) {
            try {
                if (oKV.GetType() == this.GetType()) {
                    if (this._strKey == oKV.Key() && this._strValue == oKV.Value()) {
                        return true;
                    }
                }
            }
            catch (e) {
                throw "(Exception: kvp_e0) Invalid parameter type";
            }
            return false;
        }
        // Return: string
        this.GetType = function () {
            return "Library.KeyValuePair";
        }
        // Return: string (delimits the key and value if provided)
        this.ToString = function () {
            var strDelimiter = (arguments.length == 1) ? arguments[0] : "";
            return this._strKey + strDelimiter + this._strValue;
        }
    };

    ///colleciton
    this.Collection = function () {
        var collection = {};
        var order = [];

        this.add = function (property, value) {
            if (!this.exists(property)) {
                collection[property] = value;
                order.push(property);
            }
        }
        this.remove = function (property) {
            collection[property] = null;
            var ii = order.length;
            while (ii-- > 0) {
                if (order[ii] == property) {
                    order[ii] = null;
                    break;
                }
            }
        }
        this.toString = function () {
            var output = [];
            for (var ii = 0; ii < order.length; ++ii) {
                if (order[ii] != null) {
                    output.push(collection[order[ii]]);
                }
            }
            return output;
        }
        this.getKeys = function () {
            var keys = [];
            for (var ii = 0; ii < order.length; ++ii) {
                if (order[ii] != null) {
                    keys.push(order[ii]);
                }
            }
            return keys;
        }
        this.update = function (property, value) {
            if (value != null) {
                collection[property] = value;
            }
            var ii = order.length;
            while (ii-- > 0) {
                if (order[ii] == property) {
                    order[ii] = null;
                    order.push(property);
                    break;
                }
            }
        }
        this.exists = function (property) {
            return collection[property] != null;
        }
    };



    /* it's because Firefox/Opera considers the whitespace between element nodes to be text nodes
    (whereas IE does not) and therefore using .nextSibling on an element gets that text node in Firefox */
    this.GetNextSibling = function (elem) {
        do {
            elem = elem.nextSibling;
        } while (elem && elem.nodeType != 1);
        return elem;

    };


    //retrun if source of event in query element;
    this.IsEventInElement = function (ev, elementId) {
        var eventSrc = ev && ev.target ? ev.target : event.srcElement;
        var tgtElement = document.getElementById(elementId);

        while (eventSrc) {
            if (eventSrc == tgtElement) {
                return true;
            }
            eventSrc = eventSrc.parentNode;
        }
        return false;
    };

    this.IsExistsInString = function (strData, strFind) {
        if (strData == '' | strFind == '') {
            return false;
        }
        var strArray = strData.toString().split(',');

        isFound = false;
        $(strArray).each(function (index) {
            if (strArray[index].toString().toLowerCase() == strFind.toString().toLowerCase()) {
                //return true;
                isFound = true;
                return false;
            }
        });

        return isFound;
    };



    this.GetArreyObjectIndex = function (array, attr, value) {
        for (var i = 0; i < array.length; i++) {
            if (array[i].hasOwnProperty(attr) && array[i][attr] === value) {
                return i;
            }
        }
        return -1;
    };



    this.DisableBackSpace = function () {
        var self = this;
        $(document).keydown(function (ev) {

            //            if ((ev.keyCode == 8 ||
            //                    (ev.keyCode == 37 && ev.altKey) ||
            //                    (ev.keyCode == 39 && ev.altKey))
            //                &&
            //                    (ev.srcElement.form == null || ev.srcElement.isTextEdit == false)
            //                ) {
            //                //                ev.cancelBubble = true;
            //                //                ev.returnValue = false;
            //                ev.preventDefault();
            //            }

            //        if ((ev.keyCode == 8 ||
            //                    (ev.keyCode == 37 && ev.altKey) ||
            //                    (ev.keyCode == 39 && ev.altKey))
            //                &&
            //                    (ev.srcElement.isTextEdit == false)
            //                ) {
            //            //                ev.cancelBubble = true;
            //            //                ev.returnValue = false;
            //            ev.preventDefault();
            //        }

            if (ev.keyCode == 8) {
                if (ev.srcElement.isTextEdit == false) {
                    ev.preventDefault();
                }
                else {
                    if ($(ev.srcElement).is("[readonly]")) {
                        ev.preventDefault();
                    }
                }
            }
        });

    };

    this.DisableReadOnlyBackSpaceInScope = function (pScope, pClassName) {
        this.DisableReadOnlyBackSpace(pClassName, pScope);
    };

    this.DisableReadOnlyBackSpace = function (pClassName, pScope) {
        var self = this;
        if (!pClassName) {
            pClassName = '';
        }

        if (!pScope) {
            pScope = '';
        }
        var selectorStr = pClassName == '' ? 'input' : 'input.' + pClassName


        if (pScope != '') {
            selectorStr = '#' + pScope + ' ' + selectorStr;
        }

        $(selectorStr + ':[readonly]').keydown(function (ev) {
            switch (ev.keyCode) {
                case self.Keys.backspace:
                    //alert('back space');
                    ev.preventDefault();
                    break;
            }
        });

    };


    this.GetSum = function (pClassName, pScope) {
        var self = this;
        //var selectorStr = pClassName == '' ? 'input' : 'input.' + pClassName


        if (!pScope) {
            pScope = '';
        }

        var selectorStr = '.' + pClassName;
        if (pScope != '') {
            selectorStr = '#' + pScope + ' ' + selectorStr;
        }

        //selectorStr = selectorStr + ":visible"

        var totSum = 0;
        //$(selectorStr + ':visible').each(function (index, elem) {
        $(selectorStr).each(function (index, elem) {
            //check dtpicker trigger
            if ($(elem).is(':visible')) {
                var elemVal = $(elem).val();
                if (self.IsNumber(elemVal)) {
                    totSum += parseFloat(elemVal);
                }
            }
        });

        return totSum;
    };

    this.GetSumByElements = function (pElements) {
        var self = this;
        //var selectorStr = pClassName == '' ? 'input' : 'input.' + pClassName
        //selectorStr = selectorStr + ":visible"

        var totSum = 0;
        $(pElements).each(function (index, elem) {
            //check dtpicker trigger
            var elemVal = $(elem).val();
            if (self.IsNumber(elemVal)) {
                totSum += parseFloat(elemVal);
            }
        });
        return totSum;
    };


    //add commas to number string .. 1000.00 -> 1,000.00
    this.AddCommas = function (nStr) {
        nStr += '';
        x = nStr.split('.');
        x1 = x[0];
        x2 = x.length > 1 ? '.' + x[1] : '';
        var rgx = /(\d+)(\d{3})/;
        while (rgx.test(x1)) {
            x1 = x1.replace(rgx, '$1' + ',' + '$2');
        }
        return x1 + x2;
    };

    this.RemoveCommas = function (nStr) {
        if (!nStr) {
            nStr = '';
        }
        var newString = nStr.replace(/,/g, ''); //remove whitespace

        return newString;
    };


    this.FormatNumberOld = function (number, decimalPlaces, curSymbol, thousandSymbol, decimalSymbol) {
        var self = this;
        var orgNumber = number;
        number = self.UnFormatNumber(number);
        if (self.IsNumber(number) == false) {
            return orgNumber;
        }
        number = number || 0;
        decimalPlaces = !isNaN(decimalPlaces = Math.abs(decimalPlaces)) ? decimalPlaces : 2;
        curSymbol = curSymbol !== undefined ? curSymbol : "";
        thousandSymbol = thousandSymbol || ",";
        decimalSymbol = decimalSymbol || ".";
        var negative = number < 0 ? "-" : "";
        i = parseInt(number = Math.abs(+number || 0).toFixed(decimalPlaces), 10) + "";
        j = (j = i.length) > 3 ? j % 3 : 0;
        var numFormatted = curSymbol + negative + (j ? i.substr(0, j) + thousandSymbol : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + thousandSymbol) + (decimalPlaces ? decimalSymbol + Math.abs(number - i).toFixed(decimalPlaces).slice(2) : "");
        return numFormatted;
    };


    ///formatStyle: eng,ban,ind
    this.FormatNumber = function (number, decimalPlaces, formatStyle, currencySymbol, thousandSymbol, decimalSymbol) {
        var self = this;
        var orgNumber = number;
        number = self.UnFormatNumber(number);
        if (self.IsNumber(number) == false) {
            return orgNumber;
        }
        number = number || 0;
        //        decimalPlaces = !isNaN(decimalPlaces = Math.abs(decimalPlaces)) ? decimalPlaces : 2;
        //        curSymbol = curSymbol !== undefined ? curSymbol : "";
        //        thousandSymbol = thousandSymbol || ",";
        //        decimalSymbol = decimalSymbol || ".";
        //        //formatStyle = formatStyle || "eng";
        //        formatStyle = formatStyle || "ban";

        formatStyle = formatStyle || this.FormatNumber_FormatStyle;
        decimalPlaces = decimalPlaces || this.FormatNumber_DecimalPlaces;
        currencySymbol = currencySymbol || this.FormatNumber_CurrencySymbol;
        thousandSymbol = thousandSymbol || this.FormatNumber_ThousandSymbol;
        decimalSymbol = decimalSymbol || this.FormatNumber_DecimalSymbol;


        var negative = number < 0 ? "-" : "";

        var numFormatted = '';
        switch (formatStyle.toLowerCase()) {
            case 'eng':
                i = parseInt(number = Math.abs(+number || 0).toFixed(decimalPlaces), 10) + "";
                j = (j = i.length) > 3 ? j % 3 : 0;
                numFormatted = curSymbol + negative + (j ? i.substr(0, j) + thousandSymbol : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + thousandSymbol) + (decimalPlaces ? decimalSymbol + Math.abs(number - i).toFixed(decimalPlaces).slice(2) : "");
                break;
            case 'ban':
            case 'ind':
                number += '';
                //x = nStr.split('.');
                x = number.split(decimalSymbol);
                x1 = x[0];
                //x2 = x.length > 1 ? decimalSymbol + x[1] : '';
                k = parseInt(number = Math.abs(+number || 0).toFixed(decimalPlaces), 10) + "";
                x2 = decimalPlaces ? decimalSymbol + Math.abs(number - k).toFixed(decimalPlaces).slice(2) : ""
                var rgx = /(\d+)(\d{3})/;
                var z = 0;
                var len = String(x1).length;
                var num = parseInt((len / 2) - 1);
                while (rgx.test(x1)) {
                    if (z > 0) {
                        x1 = x1.replace(rgx, '$1' + thousandSymbol + '$2');
                    }
                    else {
                        x1 = x1.replace(rgx, '$1' + thousandSymbol + '$2');
                        rgx = /(\d+)(\d{2})/;
                    }
                    z++;
                    num--;
                    if (num == 0) {
                        break;
                    }
                }
                numFormatted = currencySymbol + negative + x1 + x2;
                break;
        } //switch case
        return numFormatted;
    };

    this.FormatCurrency = function (number, decimalPlaces, formatStyle, currencySymbol, thousandSymbol, decimalSymbol) {

        formatStyle = formatStyle || this.FormatCurrency_FormatStyle;
        decimalPlaces = decimalPlaces || this.FormatCurrency_DecimalPlaces;
        currencySymbol = currencySymbol || this.FormatCurrency_CurrencySymbol;
        thousandSymbol = thousandSymbol || this.FormatCurrency_ThousandSymbol;
        decimalSymbol = decimalSymbol || this.FormatCurrency_DecimalSymbol;


        return this.FormatNumber(number, decimalPlaces, formatStyle, currencySymbol, thousandSymbol, decimalSymbol);

    };

    this.FormatNumber2 = function (nStr) {
        nStr += '';
        x = nStr.split('.');
        x1 = x[0];
        x2 = x.length > 1 ? '.' + x[1] : '';
        var rgx = /(\d+)(\d{3})/;
        var z = 0;
        var len = String(x1).length;
        var num = parseInt((len / 2) - 1);

        while (rgx.test(x1)) {
            if (z > 0) {
                x1 = x1.replace(rgx, '$1' + ',' + '$2');
            }
            else {
                x1 = x1.replace(rgx, '$1' + ',' + '$2');
                rgx = /(\d+)(\d{2})/;
            }
            z++;
            num--;
            if (num == 0) {
                break;
            }
        }
        return x1 + x2;
    };



    this.UnFormatNumber = function (number) {
        //var s = parseFloat(3);

        var numVal = parseFloat(number.toString().replace(/[^0-9-.]/g, '')); // 12345.99 
        return numVal;
    };


    //gets the number removes commas
    this.GetNumber = function (nStr) {
        var newString = nStr.replace(/[^\d\.\-\ ]/g, ''); //get only number
        newString = newString.replace(/ /g, ''); //remove whitespace
        if (nStr.startsWith('(') && nStr.endsWith(')')) {
            newString = '-' + newString;
        }
        var num = 0;
        if (newString != '') {
            num = parseFloat(newString);
        }

        return num;
    };


    this.ValidateNumber = function (e, allowDecimal, allowNegative, allowComma) {
        if (e.shiftKey || e.ctrlKey || e.altKey) { // if shift, ctrl or alt keys held down 
            e.preventDefault();         // Prevent character input 
        } else {
            var n = e.keyCode;
            if (!((n <= 46)              // 8=backspace
                || (n >= 35 && n <= 40)     // arrow keys/home/end 
                || (n >= 48 && n <= 57)     // numbers on keyboard
                || (n >= 96 && n <= 105)   // number on keypad
                || (allowDecimal && ((n == 190) || (n == 110)))
                || (allowNegative && ((n == 109) || (n == 189)))
                || (allowComma && (n == 188))
                )
                ) {
                e.preventDefault();     // Prevent character input
            }
        }
    };


    this.IsPrintableChar = function (keycode, includeDelete, includeEnter) {

        var valid =
             keycode == 32 ||                                       // spacebar
            (keycode > 47 && keycode < 58) ||                     // number keys
            (includeEnter && keycode == 13) ||                     // return key(s) (if you want to allow carriage returns)
            (includeDelete && (keycode == 8 || keycode == 46)) ||   // backspace & delete key      
            (keycode > 64 && keycode < 91) ||                     // letter keys
            (keycode > 95 && keycode < 112) ||                     // numpad keys
            (keycode > 185 && keycode < 193) ||                     // ;=,-./` (in order)
            (keycode > 218 && keycode < 223);                       // [\]' (in order)

        return valid;
    };


    this.IsNumber = function (n) {

        //YUI: return typeof o === 'number' && isFinite(o);

        // return (input - 0) == input && input.length > 0;


        return !isNaN(parseFloat(n)) && isFinite(n);
    };


    this.ToNumber = function (n) {
        var num = this.GetNumber(n);
        if (this.IsNumber(num)) {
            nam = parseFloat(num);
        }
        else {
            num = 0;
        }
        return num;
    };


    this.GetJSONData = function (n) {

        //YUI: return typeof o === 'number' && isFinite(o);

        // return (input - 0) == input && input.length > 0;


        return !isNaN(parseFloat(n)) && isFinite(n);
    };


    this.ExcapeRegex = function (value) {
        return value.replace(/[\-\[\]{}()*+?.,\\\^$|#\s]/g, "\\$&");
    };


    this.IsElementInViewByID = function (elemID, containerID) {
        return this.IsElementInView($('#' + elemID), $('#' + containerID));
    };

    this.IsElementInView = function (elem, container) {
        if (elem == null | container == null) {
            return false;
        }

        if ($(container).is(':visible') == false | $(elem).is(':visible') == false) {
            return false;
        }
        var docViewTop = $(container).offset().top;
        var docViewBottom = docViewTop + $(container).height();

        var elemTop = $(elem).offset().top;
        var elemBottom = elemTop + $(elem).height();

        return ((elemBottom <= docViewBottom) && (elemTop >= docViewTop));

    };

    this.JSONStringify = function (obj) {
        var self = this;

        if (obj === null)
        {
            return 'null';
        }
        var t = typeof (obj);

        if (t === 'undefined')
        {
            return 'null';
        }

        if (t != "object") {
            // simple data type
            if (t == "string") obj = '"' + obj + '"';
            return String(obj);
        } else {
            // recurse array or object
            var n, v, json = [], arr = (obj && obj.constructor == Array);

            for (n in obj) {
                v = obj[n];
                t = typeof (v);
                if (obj.hasOwnProperty(n)) {
                    if (t == "string") v = '"' + self.JSONEscape(v) + '"'; else if (t == "object" && v !== null) v = self.JSONStringify(v);
                    json.push((arr ? "" : '"' + n + '":') + String(v));
                }
            }
            return (arr ? "[" : "{") + String(json) + (arr ? "]" : "}");
        }
    };

    //this.JSONStringify = function (obj) {
    //    var self = this;
    //    var t = typeof (obj);
    //    if (t != "object" || obj === null) {
    //        // simple data type
    //        if (t == "string") obj = '"' + obj + '"';
    //        return String(obj);
    //    } else {
    //        // recurse array or object
    //        var n, v, json = [], arr = (obj && obj.constructor == Array);

    //        for (n in obj) {
    //            v = obj[n];
    //            t = typeof (v);
    //            if (obj.hasOwnProperty(n)) {
    //                if (t == "string") v = '"' + self.JSONEscape(v) + '"'; else if (t == "object" && v !== null) v = self.JSONStringify(v);
    //                json.push((arr ? "" : '"' + n + '":') + String(v));
    //            }
    //        }
    //        return (arr ? "[" : "{") + String(json) + (arr ? "]" : "}");
    //    }
    //};


    this.ReplaceAll = function (strData, strFind, strReplace) {
        return strData.replace(new RegExp(strFind, 'g'), strReplace);
    };


    this.JSONEscape = function (jsonString) {
        return jsonString.replace(/\\/g, "\\\\")
                                    .replace(/\"/g, "\\\"")
                                    .replace(/\'/g, "\\\'")
                                    .replace(/\n/g, "\\\\n")
                                    .replace(/\r/g, "\\\\r")
                                    .replace(/\t/g, "\\\\t");



        //        return jsonString.replace(/\\/g, "\\\\")
        //                                    .replace(/\"/g, "\\\"")
        //                                    .replace(/\'/g, "\\\'")
        //                                    .replace(/\n/g, "\\\\n")
        //                                    .replace(/\r/g, "\\\\r")
        //                                    .replace(/\f/g, "\\f")
        //                                    .replace(/\b/g, "\b")
        //                                    .replace(/\t/g, "\t")
        //                                    .replace(/\u2028/g, "\\u2028")
        //                                    .replace(/\u2029/g, "\\u2029");




        //        return jsonString.replace(new RegExp(/\\/g), "\\\\")
        //                                    .replace(new RegExp("\"", 'g'), "\\\"")
        //                                    .replace(new RegExp("\'", 'g'), "\\\'")
        //                                    .replace(new RegExp("\n", 'g'), "\\\\n")
        //                                    .replace(new RegExp("\r", 'g'), "\\\\r")
        //                                    .replace(new RegExp("\f", 'g'), "\\f")
        //                                    .replace(new RegExp("\b", 'g'), "\b")
        //                                    .replace(new RegExp("\t", 'g'), "\t")
        //                                    .replace(new RegExp("\u2028", 'g'), "\\u2028")
        //                                    .replace(new RegExp("\u2029", 'g'), "\\u2029");

    };
    //    
    //As per user667073 suggested, except reordering the backslash replacement first, and fixing the quote replacement

    //    escape = function (str) {
    //  return str
    //    .replace(/[\\]/g, '\\\\')
    //    .replace(/[\"]/g, '\\\"')
    //    .replace(/[\/]/g, '\\/')
    //    .replace(/[\b]/g, '\\b')
    //    .replace(/[\f]/g, '\\f')
    //    .replace(/[\n]/g, '\\n')
    //    .replace(/[\r]/g, '\\r')
    //    .replace(/[\t]/g, '\\t');
    //};

    //    this.DisableBackSpace = function(className) 
    //    {
    //       if (!className)
    //       {
    //         className='';
    //       }
    //       if (className != '') {
    //       
    //         $('input.' + className + '[readonly]').keypress(function(ev){
    //                alert('dd');
    //                return false;
    //            });
    ////            $('input.' + className).keypress(function(ev) {
    ////                alert('dd');
    ////                return false;
    ////            }); 
    //          
    //       }
    //    };


}                       //end of class function



//Below are some examples of how to use this class.

//var oKeyValue1 = new KeyValuePair();
//oKeyValue1.Key("Name");
//oKeyValue1.Value("Ryan");
// 
//var oKeyValue2 = new KeyValuePair("Name", "Jason");
// 
//// Are they equal?
//alert(oKeyValue1.Equals(oKeyValue2));
// 
//// Look inside
//alert(oKeyValue1.ToString());
//alert(oKeyValue1.ToString(":"));
// 
//// Get type
//alert(oKeyValue1.GetType());
//alert(oKeyValue1.GetType() == oKeyValue2.GetType());
// 
//// Change
//oKeyValue1.Value("Jason");
// 
//// Show they are equal
//alert(oKeyValue1.Equals(oKeyValue2));






//var myCollection = new Collection();
//myCollection.add("first", "Adam");
//myCollection.add("second", "Eve");
//myCollection.add("third", "Cane");
//myCollection.add("fourth", "Abel");
//myCollection.remove("second");
//myCollection.add("fifth", "Noah");
//alert(myCollection.toString());
//alert(myCollection.getKeys());
//myCollection.update("first");
//alert(myCollection.toString());
//alert(myCollection.exists("third"));
//alert(myCollection.exists("something"));


//var name = myTreeView_Data.selectedNodeID.value;
//var selectedNode = $get(name);

//if(selectedNode)
//{ 
//selectedNode.scrollIntoView(true);
//}


//var elem = document.getElementById('TreeView1_SelectedNode');   
//        if(elem != null )   
//        {     
//            var node = document.getElementById(elem.value);     
//            if(node != null)     
//            {       
//                node.scrollIntoView(true);       
//            }   
//        }


// function automatedClick(e)
//    {
//      var myEvt = document.createEvent('MouseEvents');
//      var link;
//      link = document.getElementById('theLink2');
//      //the following tests the redirect case.
//      //link = document.getElementById('theLink');
//      myEvt.initMouseEvent('click', false, false, window, 1, 0, 0, 0, 0, false, false, false, false ,1 ,null);   
//      link.dispatchEvent(myEvt);
//      if(link.href != null && link.href != ''){
//        if(link.href.search("JavaScript")>0){
//          //call JavaScript Function
//        }else{
//          //redirect 
//          window.location = link.href;
//        }
//      }
//    }


// <script type="text/javascript">
//    var a = $('.path > .to > .element > a')[0];
//    var e = document.createEvent('MouseEvents');
//    e.initEvent( 'click', true, true );
//    a.dispatchEvent(e);
//    </script>

//$(function() {
//    $('#btn').click(function() {
//        $('#thickboxId').triggerHandler('click');
//    })
//})


//        function doClick(){
//            jQuery('#mylink').trigger('click'); 
//        };
