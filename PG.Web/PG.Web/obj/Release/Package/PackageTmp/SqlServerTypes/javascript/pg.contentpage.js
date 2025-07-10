
//Version: 2.2.0
//Date: June 02, 2014

/// <reference path="jsutility.js" />
/// <reference path="PG.enums.js" />
/// <reference path="tabclass.js" />
/// <reference path="tabmenu.js" />
/// <reference path="jquery-latest.min.js" />



$(document).ready(function () {
    if (IForm.IsPageResize) {
        ContentPage.ResizePageContent();
    }
});

$(window).resize(function () {
    if (IForm.IsPageResize) {
        ContentPage.ResizePageContent();
    }
});

$(window).bind('unload', function () {
    //alert('ddd');
    $(document).empty();
});







//$(window).resize(function () {
//    if (IForm.IsPageResize) {
//        ContentPage.ResizePageContent();
//    }
//});




//used for iframe content page to resize

var ContentPage = new function () {
    this.ResizePageContent = function () {
        try {
            if (window.frameElement) {
                var iframeHeight = window.frameElement.clientHeight;
                $('#dvPageContent').height(iframeHeight + 'px');
            }
            else {
                $('html').height('100%');
                $('body').height('100%');
                var bodyHeight = $('body').height();
                $('#dvPageContent').height(bodyHeight + 'px');
            }
            var pageHeight = document.getElementById('dvPageContent').clientHeight;

            var dvContMain = document.getElementById('dvContentMain');
            if (dvContMain != null) {
                var contHeaderHeight = document.getElementById('dvContentHeader').clientHeight;
                var contMainTop = document.getElementById('dvContentMain').clientHeight;
                var contFooterHeight = document.getElementById('dvContentFooter').clientHeight;
                var contMainHeight = pageHeight - contHeaderHeight - contFooterHeight - 2; //1 for border
                $('#dvContentMain').height(contMainHeight + 'px');
            }
            else {
                $('#dvPageContent').find('div:first').height(pageHeight - 2);
            }
            if (window.PageResizeCompleted) {
                window.PageResizeCompleted(pageHeight, contMainHeight ? contMainHeight : 0);
            }

            dvContMain = null;

        }
        catch (er) {
        }

    };
}


function ResizePageContentOld() {
    try {
        if (window.frameElement) {
            var iframeHeight = window.frameElement.clientHeight;
            //$('#dvPageContent').height(iframeHeight + 'px');
            $('#dvPageContent').height(iframeHeight + 'px');
        }
        else {
            $('html').height('100%');
            $('body').height('100%');
            var bodyHeight = $('body').height();
            $('#dvPageContent').height(bodyHeight + 'px');
        }
        //var pageHeight = document.getElementById('dvPageContent').style.height;
        var pageHeight = document.getElementById('dvPageContent').clientHeight;
        //pageHeight = pageHeight.toString().substring(0, pageHeight.indexOf('px'));

        var dvContMain = document.getElementById('dvContentMain');
        if (dvContMain != null) {
            var contHeaderHeight = document.getElementById('dvContentHeader').clientHeight;
            var contMainTop = document.getElementById('dvContentMain').clientHeight;
            var contFooterHeight = document.getElementById('dvContentFooter').clientHeight;
            var contMainHeight = pageHeight - contHeaderHeight - contFooterHeight - 2; //1 for border
            $('#dvContentMain').height(contMainHeight + 'px');
        }
        else {
            // $('#dvPageContent').find('div:first').height(pageHeight - 2);
            $('#dvPageContent').find('div:first').height(pageHeight - 2);
        }

        if (window.PageResizeCompleted) {
            window.PageResizeCompleted(pageHeight, contMainHeight ? contMainHeight : 0);
        }
        dvContMain = null;
    }
    catch (er) {
    }
}