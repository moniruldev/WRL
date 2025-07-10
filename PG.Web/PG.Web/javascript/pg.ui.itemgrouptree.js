
/// <reference path="jsutility.js" />
/// <reference path="jquery-latest.min.js" />
/// <reference path="jquery-ui-latest.min.js" />
/// <reference path="jquery.treeview.js" />
/// <reference path="jquery.dynatree.js" />
/// <reference path="jquery.ui.combogrid.js" />

/// <reference path="jquery-latest.min-vsdoc.js" />



jQuery.fn.reverse = [].reverse;

(function($) {
    var Classes = {
        treediv : "itemgroup_div",
        tree: "itemgroup_tree",
        node: "itemgroup_node",
        nodeHighlight: "group_nodeHighlight",
        nodeSelected: "group_nodeSelected",

        nodeSpan: "group_nodeSpan",
        nodeSpanHover: "group_nodeSpanHover",
        nodeSpanHighlight: "group_nodeSpanHighlight",
        nodeSpanSelected: "group_nodeSpanSelected",

        nodePathText: "group_nodePathText",

        nodeCheckBox: "group_nodeCheckBox",
        nodeCheckBoxHover: "group_nodeCheckBoxHover",
        nodeCheckBoxSelected: "group_nodeCheckBoxSelected",

        popupBody: "dvPopupGroupBody",

        btnOk: "itemgroup_btnOk",
        btnCancel: "itemgroup_btnCancel",

        textSearch: "group_textSearch",
        textSearchList: "itemgroup_textSearch",
        hiddenSearchID: "itemgroup_hdnSearchID",
        searchBox: "dvSearch",
        l: ""

    };


    var Keys = {

        backspace: 8,
        tab: 9,

        enter: 13,
        escape: 27,

        space: 32,
        end: 35,
        home: 36,

        left: 37,
        right: 39,
        up: 38,
        down: 40,

        insert: 45,
        del: 46,

        multiply: 106,
        plus: 107,
        minus: 108,
        devide: 111,

        l: ""
    };


    $.widget("InterwaveUI.ItemGroupTree", {
        $popupDialog: null,
        $treeControl: null,

        selectedID: '',
        selectedText: '',
        curLinkControlID: '',
        options: {
            title: 'Select Group',
            multiSelect: false,
            autoLink: false,
            autoLinkUpdate: false,
            linkControlID: '',
            highlightLink: true,
            keyboard: true,
            enableSelect: true,
            allowTopNodeSelect : true,
            width: 400,
            height: 'auto',
            minHeight: 380,
            bodyHeight: 280,
            zIndex: 0,
            isSearch: false,
            searchColumns: null,
            searchServiceURL : '',

            l: ''
        },
        // initialize the plugin
        _create: function() {
            //		    this.element.addClass( "progressbar" );
            //		    this._update();
            //alert('created');
        },
        _init: function() {
            var self = this;


            this.$treeControl = $(this.element).find('div.' + Classes.treediv).dynatree({
                minExpandLevel: 1, // 1: root node is not collapsible
                selectMode: 1, // 1:single, 2:multi, 3:multi-hier
                clickFolderMode: 1, // 1:activate, 2:expand, 3:activate and expand
                noLink: false, // Use <span> instead of <a> tags for all nodes
                icon: false,
                onActivate: function(node) {
                    //logMsg("onActivate(%o)", node);
                    //alert(node.data.key);

//                    $("#echoActive").text(node.data.title);
//                    if( node.data.url )
//                      window.open(node.data.url);
                  },
                debugLevel: 1 // 0:quiet, 1:normal, 2:debug
            });


            $(this.element).find("script").remove();
            this.$popupDialog = $(this.element).dialog({
                title: self.options.title,
                autoOpen: false,
                resizable: false,
                modal: true,
                position: 'center',
                closeOnEscape: true,
                top: 0,
                left: 0,
                //show: "fade",
                //hide: "fadeout",
                //open: function(event, ui) { $(".ui-dialog-titlebar-close").hide(); }
                width: this.options.width,  //300
                height: this.options.height, //'auto'
                minHeight: this.options.minHeight,  //380, //for ie6, 2 line space for path text
                open: function(event, ui) {
                    self.$popupDialog.parent().appendTo(jQuery("form:first"));
                    self._onShow();
                    self._trigger("open", event, ui);
                },
                close: function(event, ui) {

                    self._onClose();
                    self._trigger("close", event, ui);
                }
                //        buttons: {
                //            "Close": function() {
                //			  $( this ).dialog( "close" );
                //			},
                //            "Save": function() {
                //              alert("save");
                //              $( this ).dialog( "close" );
                //              window.location.reload();
                //            }
                //        }
            });


           




            //            $(this.element).find('input[type=text].' + Classes.textSearch).keydown(function(e) {
            //                self._onSearchKeyDown(e);
            //            });
            //alert('o');
           
           // data = this._getTreePathList();

//            $(this.element).find('input[type=text].' + Classes.textSearch).autocomplete({
//                source: data,
//                minLength: 0,
//                autoFocus: true,
//                select: function(event, ui) {
//                    var selectedObj = ui.item;
//                    //                    $(autoCompelteElement).val(selectedObj.label);
//                    //                    $('#' + hiddenElementID).val(selectedObj.value);
//                    //alert(selectedObj.id);
//                    self._unHighlightNodeALL();
//                    self.selectNodeByID(selectedObj.id);
//                    self.expandParentNodesByID(selectedObj.id);
//                    self.scrollToSelectedNode();
//                    self._setSelectedNodePathText();
//                    //return false;
//                }
//            });

            //            //ajax source
            //            source: function (request, response) {
            //	            $.ajax({
            //	                type: "POST",
            //	                contentType: "application/json; charset=utf-8",
            //	                url: "/Services/PredictiveSearch.asmx/GetAllPredictions",
            //	                data: "{'keywordStartsWith':'" + request.term + "'}",
            //	                dataType: "json",
            //	                async: true,
            //	                success: function (data){
            //	                    response(data.d);
            //	                },
            //	                error: function (result) {
            //	                    alert("Due to unexpected errors we were unable to load data");
            //	                }
            //	            });
            //	        },


            this._bindDialogEvents();
            if (self.options.isSearch) {
                this._bindSearchList();
            }
            //this._bindKeySearch();
        },

        destroy: function() {
            //		    this.element
            //			    .removeClass( "progressbar" )
            //			    .text("");

            // call the base destroy function
            $.Widget.prototype.destroy.call(this);
        },

        _bindDialogEvents: function() {
            var self = this;

            //            $('.ui-widget-overlay').live('click', function(event) {
            //                self.$popupDialog.dialog("close");
            //            });

            $(this.element).find('input.' + Classes.btnOk).click(function(e) {
                if (self.options.enableSelect) {
                    self._onOkButtonClick(e);
                }
            });

            $(this.element).find('input.' + Classes.btnCancel).click(function(e) {
                self._onCancelButtonClick(e);
            });

        },


        _bindSearchList: function () {
            var self = this;

            var textSearchBox = $(this.element).find('input[type=text].' + Classes.textSearchList);

            $(textSearchBox).combogrid({
                debug: true,
                searchButton: false,
                resetButton: false,
                alternate: true,
                munit: 'px',
                scrollBar: true,
                showPager: true,
                colModel: self.options.searchColumns,
                autoFocus: true,
                showError: true,
                width: 450,
                url: self.options.searchServiceURL,
                search: function (event, ui) {
                    //var companyCode = $('#' + ddlCompany).val();
                    //var branchCode = $('#' + hdnBranch).val();
                    //var deptCode = $('#' + hdnDepartment).val();
                    //var locationid = $('#' + lblLocationID).val();
                    // var seid = $('#' + txtExecutiveID).val();

                    //var newServiceURL = serviceURL;
                    //$(this).combogrid("option", "url", newServiceURL);


                },
                select: function (event, ui) {

                    //alert(ui.item.itemgroupid);
                    if (!ui.item) {
                        event.preventDefault();

                        // $('#' + hdnDealerID).val('0');
                        //$('#' + txtDealerID).val('');
                        return false;
                    }

                    //alert(ui.item.itemgroupname);
                    //$('#' + hdnGroupID).val(ui.item.itemgroupid);
                    $(textSearchBox).val(ui.item.itemgroupname);
                    $(self.element).find('.dvHdnSearchID input[type=hidden]').val(ui.item.itemgroupid);
                    var grpKey = 'grpid' + ui.item.itemgroupid;
                    self.selectNodeByKey(grpKey);

                    //self.scrollToSelectedNode();

                    //var node = self.$treeControl.dynatree("getActiveNode");
                   // self.$treeControl.dynatree("getTree").getNodeByKey(grpKey).select();
                    //$("#tree").dynatree("getTree").getNodeByKey("1234").select();

                    //self._scrollToNode(selNode);
                    //if (ui.item.dealerid == '') {
                    //    event.preventDefault();
                    //    return false;
                    //    //ClearGLAccountData(elemID);
                    //}
                    //else {
                    //    //$('#' + hdnGroupID).val(ui.item.itemgroupid);
                    //    // $('#' + txtGroupName).val(ui.item.itemgroupdesc);


                    //}
                    return false;
                },

                lc: ''
            });

            $(self.element).find('.btnPopupSearch').click(function (e) {
                $(textSearchBox).combogrid("dropdownClick");
                //alert('ok');
               // OpenItemGroupTree();
                //$("#" + groupPopupID).GroupTree("show", '');
            });



        },

        _bindKeySearch: function() {
            var self = this;

            $(this.element).find('input[type=text].' + Classes.textSearch).keydown(function(e) {
                self._onSearchKeyDown(e);
            });


            //$('input[type=text]
            $(this.element).find('input[type=text].' + Classes.textSearch).keyup(function(e) {
                self._onSearchKeyUp(e);
            });
        },

        _onSearchKeyDown: function(ev) {
            //alert('kdown');
        },

        _onSearchKeyUp: function(ev) {
            //alert('kup');
            var self = this;
            sText = $(this.element).find('input[type=text].' + Classes.textSearch).val();
            self._getSearchList(sText);

        },

        _setHeightWidth: function() {

            this.$popupDialog.dialog('option', 'height', this.options.height);
            this.$popupDialog.dialog('option', 'minHeight', this.options.minHeight);
            this.$popupDialog.dialog('option', 'width', this.options.width);
            $(this.$popupDialog).find('div.' + Classes.popupBody).height(this.options.bodyHeight);

        },

        _onShow: function() {

            $('.ui-widget-overlay').height('100%');
            $('.ui-widget-overlay').width('100%');

            this._setHeightWidth();


            if (this.options.enableSelect) {
               // $(this.element).find("." + Classes.btnOk).attr('disabled', '');
               $(this.element).find("." + Classes.btnOk).prop('disabled', false);
            }
            else {
               // $(this.element).find("." + Classes.btnOk).attr('disabled', 'disabled');
               $(this.element).find("." + Classes.btnOk).prop('disabled', true);
            }

            $(this.element).find('input[type=text].' + Classes.textSearchList).val('');
            $(this.element).find('.dvHdnSearchID input[type=hidden]').val('');

            this.$treeControl.focus();
        },

        _onClose: function() {

            $(this.element).find('input[type=text].' + Classes.textSearch).val('');

//            this._unHighlightNodeALL();
//            this._unSelectALL();
        },

        _onOkButtonClick: function(e) {
            var self = this;
            var node = self.$treeControl.dynatree("getActiveNode");
            if (node == null){
                alert('No Item Selected!');
                return;
            }

            //this._trigger("okclick", null, { value: gid, text: gname });

            if (!self.options.allowTopNodeSelect){
                //var level = self.$treeControl.dynatree("getLevel");
                var level = node.getLevel();
                if (level <= 1){
                  alert('Select Not allowed for GL Class!');
                   return;
                }
            }

            this._trigger("okclick", null, node.data.item);
            this.$popupDialog.dialog("close");
        },

        _onCancelButtonClick: function(e) {
            this.$popupDialog.dialog("close");
        },


        _getNodeByNodeSpan: function(nodeSpan) {
            return $(nodeSpan).closest('li.' + Classes.node);
        },

        _selectNode: function(node) {
           

        },

        _highlightNode: function(node, isScroll) {
        
        },

        _unHighlightNodeALL: function() {
            this.$treeControl.find('li.' + Classes.node).removeClass(Classes.nodeHighlight);
            //this.$treeControl.find('li>span.' + Classes.nodeSpan).removeClass(Classes.nodeSpanHighlight);
            this.$treeControl.find('span.' + Classes.nodeSpan).removeClass(Classes.nodeSpanHighlight);
        },


        _getSelectedNode: function() {
            return this.$treeControl.find("li." + Classes.nodeSelected);
        },

        _getSelectedNodeSpan: function() {
            return this.$treeControl.find("li span." + Classes.nodeSpanSelected);
        },

        _getHighligtedNode: function() {
            return this.$treeControl.find("li." + Classes.nodeHighlight);
        },

        _getNodePathText: function(nodeElem, includeSelf) {
            var treeText = '';
            var sep = '';
            //            $(nodeElem).parentsUntil(this.$treeControl, 'li')
            //                .find("span." + Classes.nodeSpan + ":first").reverse().each(function(index, domEle) {
            //                    var gname = $(domEle).text();
            //                    treeText += sep + gname;
            //                    sep = ' - ';
            //                });

            $(nodeElem).parentsUntil(this.$treeControl, 'li.' + Classes.node).reverse().each(function(index, domEle) {
                var gname = $(domEle).attr('gname');
                treeText += sep + gname;
                sep = ' - ';
            });

            if (includeSelf) {
                txt = $(nodeElem).attr('gname');
                if (treeText == '') {
                    treeText = txt;
                }
                else {
                    treeText += ' - ' + txt;
                }
            }

            return treeText;
        },

        _setSelectedNodePathText: function() {
            var gtr = this._getNodePathText(this._getSelectedNode(), true);
            this._setNodePathText(gtr, true);
        },

        _setNodePathText: function(pathText, boldLast) {

            fText = '';
            if (pathText) {
                if (boldLast) {
                    if (pathText != '') {
                        lastIdx = pathText.toString().lastIndexOf('-');
                        if (lastIdx == -1) {
                            pathText = '<b>' + pathText + '</b>';
                        }
                        else {
                            lastText = pathText.toString().substring(lastIdx + 1);
                            prevText = pathText.toString().substr(0, lastIdx + 1);
                            pathText = prevText + '<b>' + lastText + '</b>';
                        }
                    }
                }
                fText = pathText
            }

            $(this.element).find('span.' + Classes.nodePathText).html(fText);
        },

        _highLightLink: function(ctlID, isHighLight) {
            if (isHighLight) {
                $("#" + ctlID).addClass(Classes.linkControlSelected);
            }
            else {
                $("#" + ctlID).removeClass(Classes.linkControlSelected);
            }
        },


        _expandNode: function(node) {
            if ($(node).hasClass('expandable')) {
                $(node).children('div.hitarea').each(function(index, domEle) {
                    $(domEle).trigger('click');
                    $(domEle).siblings('ul').css('display', 'block');
                });
            }
        },

        _collapseNode: function(node) {
            if ($(node).hasClass('collapsable')) {
                $(node).children('div.hitarea').each(function(index, domEle) {
                    $(domEle).trigger('click');
                    $(domEle).siblings('ul').css('display', 'none');
                });
            }
        },

        _scrollToNode: function(node) {
           

            ////pTop = this.$treeControl.parent('div').position().top;

            ////this._setNodePathText("posTop:" + $(node).position().top + ", offTop:" + $(node).offset().top + ",prTop:" + pTop);
            ////var container = this.$treeControl.parent('div')
            ////this._setNodePathText(this._isElementIntoView(node, container));  
            ////;
            //// alert(this._isElementIntoView(node, container));
            ////var element = $(node).find('>span.' + Classes.nodeSpan);
            ////posText = "posTop:" + $(node).position().top + ", offTop:" + $(node).offset().top + ", prTop:" + pTop + ", prScrollTop:" + $(container).scrollTop();
            ////posText = "posTop:" + $(node).position().top + ", offTop:" + $(node).offset().top + ", prTop:" + pTop + ", prScrollTop:" + $(container).scrollTop();

            //if (this._isElementIntoView(element, container)) {
            //    //this._setNodePathText('true : ' + posText);
            //    alert('in view');
            //}
            //else {
            //    alert('not in view');


            //    //$(element).focus();
            //    //var tTop = this.$treeControl.position().top;
            //    //                var elmPos = $(element).offset();
            //    //                var elmHeight = $(element).height();
            //    //                var contHeight = $(container).height();
            //    //                if (elmPos) {
            //    //                    diff = elmPos.top - contHeight;
            //    //                    //this.$treeControl.parent('div').scrollTop(diff);
            //    //                    this.$treeControl.parent('div').scrollTop(elmPos.top);
            //    //                }
            //}

        },

        _isElementIntoView: function(elem, container) {
            //            var docViewTop = $(container).scrollTop();
            //            var docViewBottom = docViewTop + $(container).height();

            var contTop = $(container).position().top;
            var contHeight = $(container).height();

            //var elemTop = $(elem).offset().top;
            var elemTop = $(elem).position().top;
            var elemHeight = $(elem).height();

            var contScrollTop = $(container).scrollTop();
            var contViewTop = contTop;
            var contViewBottom = contTop + contHeight;

            var elemViewTop = elemTop;
            var elemViewBottom = elemViewTop + elemHeight;

            var eT = $(elem).text();
            var tTop = this.$treeControl.position().top;

            posText = "posTop:" + elemTop + ", posBottom:" + elemViewBottom + ", prTop:" + pTop + ", prScrollTop:" + contScrollTop;

            posText += ", treeTop:" + tTop + ", contViewTop:" + contViewTop + ", contViewBottom:" + contViewBottom;

            var inView = false;
            if (elemViewTop >= contViewTop && elemViewBottom <= contViewBottom) {
                inView = true;
                // this._setNodePathText('true (' + eT + ') : ' + posText);
            }
            else {
                //this._setNodePathText('false (' + eT + '): ' + posText);
            }
            return inView;

        },

        _getTreePathList: function() {
            var self = this;

            //(this.$treeControl, 'li.' + Classes.node)

            //this.$treeControl.find("li[name*='" + id + "']") //for string search
            //this.$treeControl.find("li[name~='" + id + "']") //for word search
            var data = new Array();

            this.$treeControl.find("li." + Classes.node).each(function(index, domEle) {
                // self._selectNode(domEle);
                //$(domEle).children("span." + Classes.nodeSpan).trigger('click');
                gid = $(domEle).attr('gid');
                gname = $(domEle).attr('gname');
                gnameshow = $(domEle).attr('gname');
                gpath = self._getNodePathText(domEle, true);

                //var c = { label: gname, value: gpath, id: gid };
                var c = { label: gpath, value: gname, id: gid };
                data.push(c);

            });
            return data;
        },

        _getSearchList: function(searchText) {
            var self = this;

            //(this.$treeControl, 'li.' + Classes.node)

            //this.$treeControl.find("li[name*='" + id + "']") //for string search
            //this.$treeControl.find("li[name~='" + id + "']") //for word search


            this.$treeControl.find("li[gname*='" + searchText + "']").each(function(index, domEle) {
                // self._selectNode(domEle);
                //$(domEle).children("span." + Classes.nodeSpan).trigger('click');
                gid = $(domEle).attr('gid');
                gname = $(domEle).attr('gname');
                gpath = self._getNodePathText(domEle, true);

            });
        },


        //public functions
        show: function(gkey) {
            var self = this;
            this.$popupDialog.dialog("open");
            self.selectNodeByKey(gkey);
        },


        showByID: function(id) {
            this._unSelectALL();
            this._unHighlightNodeALL();
            this.collapseALL();
            this.$popupDialog.dialog("open");
            if (id > 0) {
                this.selectNodeByID(id);
                this.expandParentNodesByID(id);
                this.scrollToSelectedNode();
            }
            this._setSelectedNodePathText();
        },

        selectNodeByKey: function(gkey) {
            var self = this;
             gkey = gkey || '';
             if (gkey != '') {
                var node = self.$treeControl.dynatree("getTree").getNodeByKey(gkey);
                if (node != null){
                    node.activate();
                    self.scrollToSelectedNode();
                }
             }
        },



        expandNodeByKey: function(id) {
            var self = this;
            this.$treeControl.find("li[gid='" + id + "']").each(function(index, domEle) {
                $(domEle).hasClass('expandable').children('div.hitarea').each(function(index, domEle) {
                    $(domEle).trigger('click');
                    $(domEle).siblings('ul').css('display', 'block');
                });
            });


            //            this.$treeControl.find("li span[gid='" + id + "']").each(function(index, domEle) {
            //                $(domEle).closest('li.expandable').children('div.hitarea').each(function(index, domEle) {
            //                    $(domEle).trigger('click');
            //                    $(domEle).siblings('ul').css('display', 'block');
            //                });
            //            });
        },

        expandParentNodesByID: function(id) {
            var self = this;
            this.$treeControl.find("li[gid='" + id + "']").each(function(index, domEle) {
                $(domEle).parentsUntil('#' + self.options.treeID, 'li.expandable')
                        .children('div.hitarea').each(function(index, domEle) {
                            $(domEle).trigger('click');
                            $(domEle).siblings('ul').css('display', 'block');
                        });
            });
        },


        expandALL: function() {
            var self = this;
            this.$treeControl.find("li.expandable").children('div.hitarea').each(function(index, domEle) {
                $(domEle).trigger('click');
                $(domEle).siblings('ul').css('display', 'block');
            });
        },

        collapseALL: function() {
            var self = this;
            this.$treeControl.find("li.collapsable").children('div.hitarea').each(function(index, domEle) {
                $(domEle).trigger('click');
                $(domEle).siblings('ul').css('display', 'none');
            });
        },

        scrollToSelectedNode: function () {
            var self = this;
            //var elmNode = self._getSelectedNodeSpan().span;

            var node = self.$treeControl.dynatree("getActiveNode");
            //alert("Currently active: " + node.data.title);

            var elmPos = $(node.li).offset();
            var parentHeight = self.$treeControl.parent('div').height();
            var controlPos = self.$treeControl.offset();

            var elmTop = elmPos.top;
            var elmTopPad = elmTop - 199;
            var controlTop = controlPos.top;

            //alert(elmTop);
            //alert(controlTop);

            //$(self.element).find('span.' + Classes.nodePathText).html(elmPos.top);

            var vScrollTop = 0

            if (elmPos) {
                if (elmTopPad >= 0 & elmTopPad <= parentHeight) {

                }
                else (elmTopPad > 265)
                {
                    vScrollTop = elmTopPad;
                    vScrollTop = elmTopPad - controlTop;

                    self.$treeControl.parent('div').scrollTop(vScrollTop);
                }
            }


            //alert(elmTop);
            //alert(parentHeight);

            //aler(self.$treeControl.parent('div'));
            //alert(self.$treeControl.parent('div').height());
            //alert(self.$treeControl.parent('div').attr('class'));


            //if (elmPos) {
            //    self.$treeControl.parent('div').scrollTop(elmPos.top);
            //}
        },

        getNameByID: function(id) {
            var self = this;
            var retValue = null;
            var elems =  this.$treeControl.find("li[gid='" + id + "']");
            if (elems.length > 0){
                retValue = $(elems[0]).attr('gname');
            }
            
            return retValue;
        },


        getSelectedNodePathText: function() {
            strPath = '';
            sNode = this._getSelectedNodeSpan();
            if (sNode) {
                this._getNodePathText(sNode, true);
            }

            return strPath;
        },


        //for comma hazzard
        _lf: function() { }
    });
})(jQuery);
