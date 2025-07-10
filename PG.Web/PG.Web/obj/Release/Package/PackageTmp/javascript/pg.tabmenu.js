
//Version: 2.2.0
//Date: June 02, 2014


/// <reference path="PG.jsutility.js" />
/// <reference path="PG.enums.js" />

/// <reference path="PG.tabclass.js" />
/// <reference path="jquery-latest.min.js" />

(function ($) {
    $.fn.purgeFrame = function () {
        var deferred;

        if ($.browser.msie && parseFloat($.browser.version, 10) < 9) {
            deferred = purge(this);
        } else {
            this.remove();
            deferred = $.Deferred();
            deferred.resolve();
        }

        return deferred;
    };

    function purge($frame) {
        var sem = $frame.length
                      , deferred = $.Deferred();

        $frame.load(function () {
            var frame = this;
            frame.contentWindow.document.innerHTML = '';

            sem -= 1;
            if (sem <= 0) {
                $frame.remove();
                deferred.resolve();
            }
        });
        $frame.attr('src', 'about:blank');

        if ($frame.length === 0) {
            deferred.resolve();
        }

        return deferred.promise();
    }
})(jQuery);


var TabMenu = new function () {
    var $tabcontrol;
    var tab_counter = 1;
    var currentTabs = new Array();
    var selectedTabIndex = 0;   //home tab
    var removedTabIndex = -1;

    var tabOptionListID = "ulTabButtonOption";
    var iframeClickBindDelay = 5000;
    var tabAutoClose = false;

    var timerTabOption = null;

    this.timerTabOptionDelay = 3000;  //3sec
    this.showWait = true;
    this.CheckWindowUnload = true;

    this.homeMenuID = 1100;
    this.homeMenuUrl = "Home.aspx";
    this.homeMenuLabel = "Home";

    this.RootPath = "";
    this.GetMenuURL = "";

    this.CreateTabControl = function (tabctlID) {
        var self = this;
        $tabcontrol = $("#" + tabctlID).tabs({
            tabTemplate: '<li><a href="#{href}">#{label}</a> <span class="ui-icon ui-icon-close">Remove Tab</span></li>',
            //fx: [{opacity: "toggle", duration: "slow"}, null ]
            //fx: [{collapsible: true}],
            add: function (event, ui) {
                ui.tab.id = "t-" + tab_counter;
                //$tabcontrol.tabs('select', '#' + ui.panel.id);
                $tabcontrol.tabs("option", "active", ui.index);
            },
            remove: function (event, ui) {
                if (!tabAutoClose) {
                    self.RemoveTabFromListByIndex(removedTabIndex);
                    //                    if (!JSUtility.IsIE6()) {
                    //                        self.ResizeTabControlALL();
                    //                    }
                }
            },
            select: function (e, ui) {
                window.location.hash = ui.tab.hash;
                selectedTabIndex = ui.index;
                if (JSUtility.IsIE7()) {
                    self.ResizeTabControlALLTimer(0);
                }
                self.TabSelectTasks(selectedTabIndex);
            }
        });

        //$('#' + tabctlID + ' span.ui-icon-close').live('click', function () {
        // $('#' + tabctlID + ' span.ui-icon-close').on('click', function () {
        //$(document).on("click", "a.foo", fn)

        var closeSelector = '#' + tabctlID + ' span.ui-icon-close';
        $(document).on('click', closeSelector, function () {
            var index = $('li', $tabcontrol).index($(this).parent());
            self.CloseTab(index);
            //            var tabNo = self.GetTabNoByTabIndex(index);
            //            if (self.CheckFormIsDirty(tabNo) == true) {
            //                removedTabIndex = index;
            //                self.UnBindIFrameOnClick(tabNo);
            //                self.UnSetIFrameReadyStateChange(tabNo);
            //                $tabcontrol.tabs('remove', index);
            //            }
        });


        self.AddTabToList(1, Enums.LinkType.FromMenu, this.homeMenuID, this.homeMenuLabel, this.homeMenuUrl, "");
        self.SetIFrameOnClick(1);

        //alert('b');

        //SetIFrameOnLoad(1);

        //BindIFrameOnClick(1);
        //BindIFrameOnClick(1);
        return $tabcontrol;
    };

    this.CloseTab = function (tabIndex) {
        var self = this;
        var retVal = false;
        var tabNo = self.GetTabNoByTabIndex(tabIndex);
        if (self.CheckFormIsDirty(tabNo) == true) {
            removedTabIndex = tabIndex;
            //            self.UnBindIFrameOnClick(tabNo);
            //            self.UnSetIFrameReadyStateChange(tabNo);

            var dvIFrameID = "dvIFrame-" + tabNo;

            var iframeID = "iframe-" + tabNo;

            //$('#' + iframeID).contents().find("body").find("input").unbind();
            //$('#' + iframeID).contents().find("body").find("*").unbind();

            $('#' + iframeID).contents().find("*").unbind();


            //$('#' + iframeID).attr('src', 'javascript:false');
            //$('#' + iframeID).attr('src', 'about:blank');
            $('#' + iframeID).remove();
            //$('#' + iframeID).purgeFrame();


            //            var dv = document.getElementById(dvIFrameID);
            //            var ifr = document.getElementById(iframeID);

            //            var gc = document.getElementById('garbage-collector');
            //            gc.appendChild(dv.removeChild(ifr));
            //            //gc.appendChild(parentNode.removeChild(childNode));
            //            gc.innerHTML = '';



            //$('#' + iframeID).remove();
            //alert('purge');

            //$('#' + iframeID).removeAttr();
            //$('#' + iframeID).clearAttributes();

            //            $('#' + iframeID).empty();
            //            $('#' + iframeID).remove();

            //$('#' + iframeID).purgeFrame();
            //$tabcontrol.tabs('remove', tabIndex);

            var panelId = $tabcontrol.find(".ui-tabs-active").remove().attr("aria-controls");
            $("#" + panelId).remove();
            $tabcontrol.tabs("refresh");
            self.RemoveTabFromListByIndex(removedTabIndex);


            retVal = true;
        }
        return retVal;
    };

    this.CloseCurrentTab = function () {
        return this.CloseTab(selectedTabIndex);
    };

    this.AddTabToList = function (tabNo, linktype, menuid, label, url, param) {
        var newTab = new xtab(tabNo, "tabs-" + tabNo, linktype, menuid, label, url, param);
        currentTabs.push(newTab);
    };


    this.UpdateTabList = function (tabNo, linktype, menuid, name, url, param) {
        var tab = this.GetTabFromListByTabNo(tabNo);
        tab.linktype = linktype;
        tab.menuid = menuid;
        tab.name = name;
        tab.url = url;
        tab.param = param;
    };

    this.RemoveTabFromListByIndex = function (idx) {
        currentTabs.splice(idx, 1);

    };

    this.RemoveTabFromListByTabNo = function (tabNo) {
        var idx = -1
        for (var i = 0; i < currentTabs.length; i++) {
            if (currentTabs[i].tabno == tabNo) {
                idx = i;
                break;
            }
        }
        if (idx != -1) {
            currentTabs.splice(idx, 1);
        }
    };

    this.GetTabFromListByTabNo = function (tabNo) {
        var idx = -1
        for (var i = 0; i < currentTabs.length; i++) {
            if (currentTabs[i].tabno == tabNo) {
                idx = i;
                break;
            }
        }
        return currentTabs[idx];
    };


    this.GetMenuItemByID = function (id) {
        var self = this;
        var menu = null;
        var isError = false;
        var serviceURL = this.GetMenuURL + "?id=" + id;

        //alert(serviceURL);

        //ajax call
        var dummyVar = $.ajax({
            type: "GET",
            cache: false,
            async: false,
            dataType: "json",
            url: serviceURL,
            success: function (menudata) {
                //alert('true');
                if (menudata.menu[0].count > 0) {
                    menu = menudata.menu[0];
                }
            },
            complete: function () {
                if (isError) {
                    //return;
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                //alert(self.GetMenuURL);
                isError = true;
                alert(textStatus);
            }
        });
        dummyVar = null;

        return menu;
    };

    this.OpenMenu = function (id) {
        var self = this;
        setTimeout(function () { self.OpenMenuTimeOut(id) }
               , 0);
        //        if (id == this.homeMenuID)  //homeMenuID=1100
        //        {
        //            SelectTabByTabNo(1);
        //        }
        //        else {
        //            var menu = GetMenuItemByID(id);
        //            if (menu == null) {
        //                //alert('Data error');
        //                return;
        //            }


        //            var tdata = new xtabdata();
        //            tdata.linktype = Enums.LinkType.FromMenu;
        //            tdata.id = menu.id;
        //            tdata.name = menu.name;
        //            tdata.label = menu.label;
        //            tdata.type = menu.type;
        //            tdata.url = menu.url;
        //            tdata.tabaction = menu.tabaction;
        //            tdata.selecttab = 1;
        //            tdata.reload = menu.reload;
        //            tdata.param = "";

        //            OpenMenuByData(tdata);
        //        }
    };

    this.OpenMenuTimeOut = function (id) {

        if (id == this.homeMenuID)  //homeMenuID=1100
        {
            this.SelectTabByTabNo(1);
        }
        else {
            var menu = this.GetMenuItemByID(id);
            if (menu == null) {
                //alert('Data error');
                return;
            }


            var tdata = new xtabdata();
            tdata.linktype = Enums.LinkType.FromMenu;
            tdata.id = menu.id;
            tdata.name = menu.name;
            tdata.label = menu.label;
            tdata.type = menu.type;
            tdata.url = menu.url;
            tdata.tabaction = menu.tabaction;
            tdata.selecttab = 1;
            tdata.reload = menu.reload;
            tdata.param = "";
            tdata.showWait = this.showWait;

            this.OpenMenuByData(tdata);
            menu = null;
        }

    };


    this.OpenMenuByData = function (tabdata) {
        var self = this;
        setTimeout(function () { self.OpenMenuByDataTimeOut(tabdata) }
           , 100);

        //        if (tabdata.id == this.homeMenuID)  //homeMenuID=1100
        //        {
        //            this.SelectTabByTabNo(1);
        //        }
        //        else {
        //            switch (tabdata.tabaction) {
        //                case 0:
        //                    //no action
        //                    this.OpenMenuInTab(tabdata);
        //                    break;
        //                case 1:
        //                    this.OpenMenuInTab(tabdata);
        //                    break;
        //                case 2:
        //                    OpenMenuInNewTab(tabdata);
        //                    break;
        //                case 3:
        //                    this.FindandOpenMenuInTab(tabdata);
        //                    break;
        //                case 4:
        //                    this.FindandOpenMenuInTab(tabdata);
        //                    break;
        //                default:
        //                    ///
        //                    break;
        //            }
        //        }
    };

    this.OpenMenuByDataTimeOut = function (tabdata) {
        if (tabdata.id == this.homeMenuID)  //homeMenuID=1100
        {
            this.SelectTabByTabNo(1);
        }
        else {
            switch (tabdata.tabaction) {
                case 0:
                    //no action
                    this.OpenMenuInTab(tabdata);
                    break;
                case 1:
                    this.OpenMenuInTab(tabdata);
                    break;
                case 2:
                    this.OpenMenuInNewTab(tabdata);
                    break;
                case 3:
                    this.FindandOpenMenuInTab(tabdata);
                    break;
                case 4:
                    this.FindandOpenMenuInTab(tabdata);
                    break;
                default:
                    ///
                    break;
            }
        }
    };


    this.OpenMenuInTab = function (tabdata) {
        var selected = $tabcontrol.tabs('option', 'selected'); // => 0

        if (selected <= 0) {
            this.OpenMenuInNewTab(tabdata);
        }
        else {
            var tabNo = currentTabs[selected].tabno;
            var iframeID = "iframe-" + tabNo;

            var url = tabdata.url;
            var tabName = tabdata.name;
            var tabLabel = tabdata.label;

            //SetIFrameSouce(iframeID,url);
            this.SetIFrameSource(tabNo, url);
            this.SetTabLabelByTabNo(tabNo, tabLabel, tabdata.showWait);
            this.UpdateTabList(tabNo, tabdata.linktype, tabdata.id, tabName, url, tabdata.param);
        }
    };

    this.OpenMenuInNewTab = function (tabdata) {
        //var menu = GetMenuItemByID(id);
        var url = tabdata.url;
        var tabName = tabdata.name;
        var tabLabel = tabdata.label;
        tab_counter++;

        var divTabID = "tabs-" + tab_counter;

        var tabid = "#" + divTabID;
        $tabcontrol.tabs("add", tabid, tabLabel);
        this.AddTabToList(tab_counter, tabdata.linktype, tabdata.id, tabName, url, tabdata.param);
        this.ResizeDvTabs();
        this.ResizeTabPanelByNo(tab_counter);

        var ctlTab = document.getElementById(divTabID);

        var divFrameID = "dvIFrame-" + tab_counter;
        var elemDivFrame = document.createElement("div");
        elemDivFrame.setAttribute("id", divFrameID);
        elemDivFrame.setAttribute("class", "dvIFrame");

        elemDivFrame.style.border = 'none';
        elemDivFrame.style.borderWidth = "0px 0px 0px 0px";
        elemDivFrame.style.padding = "0px 0px 0px 0px";

        ctlTab.appendChild(elemDivFrame);
        this.ResizeDvIFrameByNo(tab_counter);
        //
        var iframeID = "iframe-" + tab_counter;
        var elemIFrame = document.createElement("iframe");
        elemIFrame.setAttribute("id", iframeID);
        elemIFrame.setAttribute("name", iframeID);

        elemIFrame.setAttribute("class", "ifContent");
        elemIFrame.setAttribute("frameborder", "0");
        elemIFrame.style.border = 'none';
        elemIFrame.style.borderWidth = "0px 0px 0px 0px";
        elemIFrame.frameBorder = 0;

        elemDivFrame.appendChild(elemIFrame);
        this.ResizeIFrameByNo(tab_counter);

        elemIFrame = null;
        elemDivFrame = null;
        ctlTab = null;

        //this.SetIFrameReadyStateChange(tab_counter);
        this.SetIFrameSource(tab_counter, url, tabdata.showWait);
        if (!JSUtility.IsIE6()) {
            this.ResizeTabControlALL();
        }
        // this.SetIFrameOnClick(tab_counter);

    };

    this.SetIFrameOnClick = function (tabNo) {
        var self = this;
        //var frameElemID = "iframe-" + tabNo;
        //var iframe = document.getElementById(frameElemID);
        setTimeout(function () { self.BindIFrameOnClick(tabNo); }, iframeClickBindDelay);
    };

    this.BindIFrameOnClick = function (tabNo) {
        var self = this;
        if (tabNo > 0) {
            var frameElemID = "iframe-" + tabNo;

            var iframe = document.getElementById(frameElemID);
            if (iframe) {
                var iframewindow = iframe.contentWindow ? iframe.contentWindow.document : iframe.contentDocument;
                $(iframewindow).click(function () { self.OnIFrameClick(); });
            }
            iframe = null;
        }
    };

    this.OnIFrameClick = function () {
        $('#' + tabOptionListID).hide();
    };

    this.UnBindIFrameOnClick = function (tabNo) {
        var frameElemID = "iframe-" + tabNo;
        var iframe = document.getElementById(frameElemID);
        if (iframe) {
            try {
                var iframewindow = iframe.contentWindow ? iframe.contentWindow.document : iframe.contentDocument;
                if (iframewindow) {
                    $(iframewindow).unbind('click');
                    //$(iframewindow).empty();
                }
                iframewindow = null;
            }
            catch (er) { }
            //$(iframe).empty();
        }
        iframe = null;
    };

    this.TabSelectTasks = function (tabIndex) {
        this.PopupsHide();
        //tabNo = GetTabNoByTabIndex(tabIndex);
        //BindIFrameOnClick(tabNo);
    };

    this.PopupsHide = function () {
        $("#" + tabOptionListID).hide();
    };

    this.ResizeDvTabs = function () {
        if (JSUtility.IsIE6()) {
            var dvCenterHeight = $('#dvCenter').height();
            var dvCenterWidth = $('#dvCenter').width();

            $('#dvContent').height('100%');
            $('#dvContent').width('100%');

            $('#dvTabs').height('100%');
            $('#dvTabs').width('100%');
        }
        else {
            var dvCenterHeight1 = $('#dvCenter').height();
            //$('.dvContent').height((dvCenterHeight - 4) + 'px');

            $('#dvContent').height((dvCenterHeight1 - 4) + 'px');

            $('#dvTabs').height('100%');
            $('#dvTabs').width('100%');
        }
    };

    this.ResizeTabPanelByNo = function (tabNo) {
        var tabHeight = $('#dvTabs').height();
        var tabNavHeight = $('#dvTabs > ul.ui-tabs-nav').height();
        var dvHeight = tabHeight - tabNavHeight;

        var tabID = "tabs-" + tabNo;
        if (JSUtility.IsIE6()) {
            var tabCenterHeight = $('#dvCenter').height();
            $('#dvTabs > div.ui-tabs-panel').height((tabCenterHeight - tabNavHeight - 4) + 'px');
            $('#' + tabID).width('100%');
        }
        else {
            $('#' + tabID).height((dvHeight) + 'px');
            $('#' + tabID).width('100%');
        }
    };

    this.ResizeDvIFrameByNo = function (tabNo) {
        var tabID = "tabs-" + tabNo;
        var tabHeight = document.getElementById(tabID).style.height;
        tabHeight = tabHeight.toString().substring(0, tabHeight.indexOf('px'));

        var dvElemID = "dvIFrame-" + tabNo;
        if (JSUtility.IsIE6()) {
            $('#' + dvElemID).height((tabHeight) + 'px');
            $('#' + dvElemID).width('100%');
        }
        else {
            $('#' + dvElemID).height('100%');
            $('#' + dvElemID).width('100%');
        }
    };

    this.ResizeIFrameByNo = function (tabNo) {
        var dvIFrameID = "dvIFrame-" + tabNo;
        var dvIFrameHeight = document.getElementById(dvIFrameID).style.height;
        dvIFrameHeight = dvIFrameHeight.toString().substring(0, dvIFrameHeight.indexOf('px'));

        var frameElemID = "iframe-" + tabNo;
        if (JSUtility.IsIE6()) {
            $('#' + frameElemID).height((dvIFrameHeight) + 'px');
            $('#' + frameElemID).width('100%');
            var iFrame = document.getElementById(frameElemID);
            if (iFrame) {
                if (iFrame.contentWindow.ResizePageContent) {
                    iFrame.contentWindow.ResizePageContent();
                }
            }
            iFrame = null;
        }
        else {
            $('#' + frameElemID).height('100%');
            $('#' + frameElemID).width('100%');
        }
    };

    this.ResizeTabPanelFrameALL = function () {

        var idx = -1;
        for (var i = 0; i < currentTabs.length; i++) {
            var tabNo = currentTabs[i].tabno;

            this.ResizeTabPanelByNo(tabNo);
            this.ResizeDvIFrameByNo(tabNo);
            this.ResizeIFrameByNo(tabNo);
        }
    };

    this.ResizeTab = function (tabNo) {
        this.ResizeTabPanelByNo(tabNo);
        this.ResizeDvIFrameByNo(tabNo);
        this.ResizeIFrameByNo(tabNo)
    };

    this.ResizeTabControlALL = function () {
        this.ResizeDvTabs();
        this.ResizeTabPanelFrameALL();
    };

    this.ResizeTabControlALLTimer = function (miliseconds) {
        setTimeout(this.ResizeTabControlALL, miliseconds);
    };



    this.OpenMenuInTabNo = function (tabdata, tabNo) {
        var idx = this.GetTabIndexByTabNo(tabNo)
        if (idx == -1) {
            this.OpenMenuInNewTab(tabdata);
        }
        else {
            var url = tabdata.url;
            var tabName = tabdata.name;
            var tabLabel = tabdata.label;


            this.SelectTabByTabNo(tabNo);
            this.UpdateTabList(tabNo, tabdata.linktype, tabdata.id, tabName, url, tabdata.param);

            if (tabdata.reload == 1) {
                var iframeID = "iframe-" + tabNo;
                this.SetTabLabelByTabNo(tabNo, tabLabel);
                this.SetIFrameSource(tabNo, url, tabdata.showWait);
            }
        }
    };

    this.FindandOpenMenuInTab = function (tabdata) {
        var idx = -1
        for (var i = 0; i < currentTabs.length; i++) {
            if (currentTabs[i].menuid == tabdata.id) {
                idx = i;
                break;
            }
        }
        if (idx == -1) {
            this.OpenMenuInNewTab(tabdata);
        }
        else {
            var tabNo = currentTabs[idx].tabno;
            this.OpenMenuInTabNo(tabdata, tabNo);
        }
    };



    this.SetIFrameSource = function (tabNo, url, isShowWait) {
        var self = this;
        if (self.CheckFormIsDirty(tabNo) == true) {
            var iframeID = "iframe-" + tabNo;
            //fortabmod 
            url = JSUtility.ReplaceQueryString(url, "_t", Enums.PageMode.InTab);
            //fortabno
            url = JSUtility.ReplaceQueryString(url, "_n", tabNo);
            //fortime
            var tt = new Date().getTime()
            url = JSUtility.ReplaceQueryString(url, "_tt", tt);

            this.UnSetIFrameReadyStateChange(tabNo);
            var ifr = document.getElementById(iframeID);

            $(ifr).empty();

            ifr.setAttribute("src", url);

            //alert(isShowWait);

            if (isShowWait) {
                this.SetIFrameReadyStateChange(tabNo);
            }

            ifr = null;
        }
        //alert('tab loaded b');
    };

    this.SetIFrameReadyStateChange = function (tabNo) {
        var iframeID = "iframe-" + tabNo;
        var ifrS = document.getElementById(iframeID);

        $('#dvWait').show();
        if (JSUtility.isIE()) {
            ifrS.onreadystatechange = function () {
                if (ifrS.readyState == "complete") {
                    // alert("Local iframe is now loaded.");
                    $('#dvWait').hide();
                }
            };
        }
        else {
            ifrS.onload = function () {
                //alert("Local iframe is now loaded. onload");
                $('#dvWait').hide();
            };
        }
        //ifrS = null;
    };

    this.UnSetIFrameReadyStateChange = function (tabNo) {
        var iframeID = "iframe-" + tabNo;
        var ifrUs = document.getElementById(iframeID);
        if (JSUtility.isIE()) {
            //$(ifr).unbind('onreadystatechange');
            ifrUs.onreadystatechange = null;
            //            ifr.onreadystatechange = function() {
            //                if (ifr.readyState == "complete") {
            //                    // alert("Local iframe is now loaded.");
            //                    $('#dvWait').hide();
            //                }
            //            };
        }
        else {
            $(ifrUs).unbind('onload');
            // ifr.onload = null;
            //            ifr.onload = function() {
            //                //alert("Local iframe is now loaded. onload");
            //                $('#dvWait').hide();
            //            };
        }
        //ifrUs = null;
    };



    this.SetTabLabelByTabNo = function (tabNo, label) {
        var tabheadID = "t-" + tabNo;
        var tabHead = document.getElementById(tabheadID);
        tabHead.innerHTML = label;

        tabHead = null;
    };

    this.SelectTabByTabNo = function (tabNo) {
        var idx = this.GetTabIndexByTabNo(tabNo);
        if (idx != -1) {
            $tabcontrol.tabs("select", idx);
        }
    };

    this.GetTabIndexByTabNo = function (tabNo) {
        var idx = -1;
        for (var i = 0; i < currentTabs.length; i++) {
            if (currentTabs[i].tabno == tabNo) {
                idx = i;
                break;
            }
        }
        return idx;
    };


    this.GetTabNo = function (tabName) {
        var tabNo = 0;
        var idx = tabName.lastIndexOf('-');
        if (idx != -1) {
            var strNo = tabName.substring(idx)
            if (strNo != '') {
                tabNo = parseInt(strNo);
            }
        }
        return tabNo;
    };

    this.GetTabNoByTabIndex = function (tabIndex) {
        var tabNo = 0;
        if (tabIndex >= 0 && tabIndex <= currentTabs.length) {

            tabNo = currentTabs[tabIndex].tabno;
        }
        return tabNo;
    };

    this.CloseAllTab = function (autoMessage) {
        var self = this;
        var result = true;
        tabAutoClose = true;

        //var curTabLength = $tabcontrol.tabs('length');
        var curTabLength = currentTabs.length;

        for (var i = curTabLength - 1; i >= 1; i--) {
            var tabNo = this.GetTabNoByTabIndex(i);
            if (self.CheckFormIsDirty(tabNo, autoMessage) == true) {
                this.UnBindIFrameOnClick(tabNo);
                this.UnSetIFrameReadyStateChange(tabNo);
                $tabcontrol.tabs('remove', i);
                this.RemoveTabFromListByIndex(i);
            }
            else {
                result = false;
                break;
            }
        }
        tabAutoClose = false;
        return result;
    };


    this.CreateTabOptionButton = function () {
        var self = this;

        $("#dvTabButton")
        .button({
            text: false,
            icons: {
                primary: "ui-icon-triangle-1-s"
            }
        })
        .click(function (e) {
            self.onButtonOptionClick();
        });

        $("#liCloseALL").click(function (e) {
            self.onCloseAllTabClick();
        });

        $('#dvTabButton').mouseenter(function (e) {
            self.stopTabOptionTimer();
        });


        $('#dvTabButton').mouseout(function (e) {
            self.onTabOptionMouseOut();
        });

        $('#ulTabButtonOption li').mouseenter(function (e) {
            $(this).addClass('tabButtonOptionsLiHover');
            self.stopTabOptionTimer();
        });

        $('#ulTabButtonOption li').mouseout(function (e) {
            $(this).removeClass('tabButtonOptionsLiHover');
            self.onTabOptionMouseOut();

        });

        $(document).mousedown(function (ev) {
            self.document_onmousedown(ev);
        });

    };

    this.document_onmousedown = function (ev) {
        this.stopTabOptionTimer();
        if (JSUtility.IsEventInElement(ev, 'ulTabButtonOption')) return true;
        if (JSUtility.IsEventInElement(ev, 'dvTabButton')) return true;
        $('#ulTabButtonOption').hide();
    };


    this.onTabOptionMouseOut = function () {
        var self = this;
        this.stopTabOptionTimer();
        //timerTabOption = setTimeout(this.closeTabOption, 3000);
        //timerTabOption = setTimeout(self.closeTabOption, 3000);
        timerTabOption = setTimeout(function () { self.closeTabOption(); }, self.timerTabOptionDelay);
    };

    this.stopTabOptionTimer = function () {
        if (timerTabOption) {
            clearTimeout(timerTabOption);
        }
        timerTabOption = null;
    };


    this.closeTabOption = function () {
        this.stopTabOptionTimer();
        $('#ulTabButtonOption').hide();
    };

    this.onButtonOptionClick = function () {
        $('#ulTabButtonOption').toggle();

        var btnLeft = $('#dvTabButton').offset().left;

        var btnTop = $('#dvTabButton').offset().top + $('#dvTabButton').outerHeight(); // +$('#divSaveButton').css('padding');
        var btnWidth = $('#dvTabButton').outerWidth();

        var popupWidth = $('#ulTabButtonOption').outerWidth();
        var popupLeft = btnLeft + btnWidth - popupWidth;
        $('#ulTabButtonOption').css('left', popupLeft).css('top', btnTop);

        if ($('#ulTabButtonOption').is(':visible')) {
        }
    };

    this.onCloseAllTabClick = function () {
        $('#ulTabButtonOption').toggle();

        if ($('#dvTabs').tabs('length') > 1) {
            if (confirm('Are You Sure To Close all open tabs?')) {
                TabMenu.CloseAllTab();
            }
        }
    };

    this.CheckFormIsDirty = function (tabNo, autoMessage) {
        var result = true;
        var self = this;
        var iframeID = "iframe-" + tabNo;
        var contWindow = document.getElementById(iframeID).contentWindow;
        if (contWindow) {
            if (contWindow.ContentForm) {
                if (contWindow.ContentForm.CheckIsDirty) {
                    var tabIndex = self.GetTabIndexByTabNo(tabNo);
                    $tabcontrol.tabs('select', tabIndex)
                    result = contWindow.ContentForm.CheckIsDirty(autoMessage);
                    //alert(result);
                }
            }
        }
        contWindow = null;
        return result;
    };


    this.WindowUnloadALL = function () {
        //return;
        var self = this;
        $(window).bind('beforeunload', function () {
            if (self.CheckWindowUnload) {
                var result = self.CloseAllTab(false);
                if (result == false) {
                    return "Data Changed! click OK to discard changes OR click Cancel to stay?"
                };
            }
        });
    };



    this.CallChild = function () {
        //fromParent

        var tabNo = currentTabs[selectedTabIndex].tabno;
        var iframeID = "iframe-" + tabNo;
        //var contWindow =  document.getElementById('targetFrame').contentWindow;
        var contWindow = document.getElementById(iframeID).contentWindow;

        if (contWindow.fromParent) {
            //alert('true');
            contWindow.fromParent('myvalue');
        }

        contWindow = null;
    };

}


   