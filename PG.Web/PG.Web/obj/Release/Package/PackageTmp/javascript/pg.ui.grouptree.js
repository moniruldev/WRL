
/// <reference path="jsutility.js" />
/// <reference path="jquery-latest.min.js" />
/// <reference path="jquery-ui-latest.min.js" />
/// <reference path="jquery.treeview.js" />
/// <reference path="jquery-latest.min-vsdoc.js" />



jQuery.fn.reverse = [].reverse;

(function($) {
    var Classes = {
        tree: "group_tree",
        node: "group_node",
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

        btnOk: "group_btnOk",
        btnCancel: "group_btnCancel",

        linkText: "group_linkText",
        linkValue: "group_linkValue",

        linkControlSelected: "group_linkControlSelected",

        textSearch: "group_textSearch",
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


    $.widget("InterwaveUI.GroupTree", {
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
            width: 300,
            height: 'auto',
            minHeight: 380,
            bodyHeight: 280,
            zIndex: 0,

            l: ''
        },
        // initialize the plugin
        _create: function() {
            //		    this.element.addClass( "progressbar" );
            //		    this._update();
            //alert('created');
        },
        _init: function() {
            //alert('grouptree init');
            var self = this;
            //alert(this.$popupControl);


            // this.$treeControl = $("#" + this.options.treeID).treeview({

            this.$treeControl = $(this.element).find('ul.' + Classes.tree).treeview({
                collapsed: true
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
            data = this._getTreePathList();

            $(this.element).find('input[type=text].' + Classes.textSearch).autocomplete({
                source: data,
                minLength: 0,
                autoFocus: true,
                select: function(event, ui) {
                    var selectedObj = ui.item;
                    //                    $(autoCompelteElement).val(selectedObj.label);
                    //                    $('#' + hiddenElementID).val(selectedObj.value);
                    //alert(selectedObj.id);
                    self._unHighlightNodeALL();
                    self.selectNodeByID(selectedObj.id);
                    self.expandParentNodesByID(selectedObj.id);
                    self.scrollToSelectedNode();
                    self._setSelectedNodePathText();
                    //return false;
                }
            });

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


            this._bindTreeEvents();
            this._bindDialogEvents();
            if (this.options.keyboard) {
                this._bindKeyNavigation();
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

        _bindTreeEvents: function() {
            var self = this;

            var span = this.options.treeID + ' li span';

            //        this.$treeControl.find('li span').addClass('glgroup_node');
            //        this.$treeControl.find('li span').addClass('glgroup_span');

            //unbind default click behaviour = expand collapse
            this.$treeControl.find('li span').unbind('click');

            this.$treeControl.find('li span.' + Classes.nodeSpan).click(function(e) {
                //self._highLightNode(self._getNodeByNodeSpan(this));
                self._selectNode(self._getNodeByNodeSpan(this));
            });

            this.$treeControl.find('li span.' + Classes.nodeSpan).dblclick(function(e) {
                self._selectNode(self._getNodeByNodeSpan(this));
                if (self.options.enableSelect) {
                    self._onOkButtonClick(e);
                }
            });

            this.$treeControl.find('li span.' + Classes.nodeSpan).mouseover(function(e) {
                $(this).addClass(Classes.nodeSpanHover);
                var gtr = self._getNodePathText(self._getNodeByNodeSpan(this), true);
                self._setNodePathText(gtr);
            });

            this.$treeControl.find('li span.' + Classes.nodeSpan).mouseout(function(e) {
                $(this).removeClass(Classes.nodeSpanHover);
                self._setSelectedNodePathText();
            });


            //            $("." + Classes.linkText).keydown(function(ev) {
            //                alert('d');
            //            });

            // alert(this.$treeControl);

            //            this.$treeControl.keydown(function(ev) {
            //                alert('kki');
            //                //       return self._onTreeKeyDown(ev);
            //            });

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


        _bindKeyNavigation: function() {
            var self = this;


            //            this.$treeControl.parent().attr('tabindex', '-1');
            //            this.$treeControl.parent().keydown(function(ev) {
            //                self._onTreeKeyDown(ev);
            //            });

            //this.$treeControl.attr('tabindex', '-1');
            this.$popupDialog.keydown(function(ev) {
                self._onTreeKeyDown(ev);
            });



            //            this.$treeControl.attr('tabindex', '-1');
            //            this.$treeControl.keydown(function(ev) {
            //                self._onTreeKeyDown(ev);
            //            });




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

        _onTreeKeyDown: function(ev) {
            var cNode = this._getHighligtedNode();
            var nNode = null;
            switch (ev.keyCode) {
                case Keys.down:
                    //alert('down');
                    if (cNode) {
                        if (cNode.hasClass('collapsable')) {
                            nNode = cNode.find('li.' + Classes.node + ':first');
                        }
                        else {
                            nNode = cNode.next('li.' + Classes.node);
                            if (nNode.length == 0) {
                                var x = 0;
                                pNode = cNode;
                                while (x == 0) {
                                    pNode = pNode.parentsUntil(this.$treecontrol, 'li.' + Classes.node + ':first');
                                    if (pNode.length > 0) {
                                        pnNode = pNode.next('li.' + Classes.node);
                                        if (pnNode.length > 0) {
                                            nNode = pnNode;
                                            x = 1;
                                        }
                                    }
                                    else {
                                        x = 1; //for break
                                    }

                                }
                            }
                        }


                        if (nNode.length > 0) {
                            this._highlightNode(nNode, true);
                        }
                    }
                    ev.preventDefault();
                    return false;

                    break;

                case Keys.up:
                    //alert('down');
                    if (cNode) {
                        nNode = cNode.prev('li.' + Classes.node);
                        if (nNode.length > 0) {
                            if (nNode.hasClass('collapsable')) {
                                nNode = nNode.find('li.' + Classes.node + ':visible:last');
                            }
                        }
                        else {
                            nNode = cNode.parentsUntil(this.$treecontrol, 'li.' + Classes.node + ':first');
                        }

                        if (nNode.length > 0) {
                            this._highlightNode(nNode, true);
                        }

                        nNode = null;
                    }

                    ev.preventDefault();
                    return false;
                    break;

                case Keys.left:
                    //alert('down');
                    if (cNode) {
                        this._collapseNode(cNode);
                    }
                    ev.preventDefault();
                    return false;
                    break;

                case Keys.right:
                    //alert('down');
                    if (cNode) {
                        this._expandNode(cNode);
                    }
                    ev.preventDefault();
                    return false;
                    break;

                case Keys.enter:
                    //alert('enter');
                    break;

                case Keys.space:
                    //alert('space');
                    break;
            };
            nNode = null;
            cNode = null;

            //alert('kd');
            //return true;
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



            this.$treeControl.focus();


        },

        _onClose: function() {
            if (this.options.autoLink) {
                if (this.options.highlightLink) {
                    this._highLightLink(this.curLinkControlID, false);

                    var linkControl =  $("#" + this.curLinkControlID).find("." + Classes.linkText);

                    if($(linkControl).is(":disabled") == false)
                    {
                        $(linkControl).focus();
                        $(linkControl).select();
                    }
                }
            }

            $(this.element).find('input[type=text].' + Classes.textSearch).val('');

            this._unHighlightNodeALL();
            this._unSelectALL();
        },

        _onOkButtonClick: function(e) {
            var sNode = this._getSelectedNode();
            var gid = null;
            var gname = null;
            if (sNode) {
                gid = $(sNode).attr('gid');
                gname = $(sNode).attr('gname');
            }
            if (this.options.autoLink) {
                if (this.options.autoLinkUpdate) {
                    $("#" + this.curLinkControlID).find("." + Classes.linkValue).val(gid);
                    $("#" + this.curLinkControlID).find("." + Classes.linkText).val(gname);
                }
            }
            sNode = null;


            this._trigger("okclick", null, { value: gid, text: gname });
            this.$popupDialog.dialog("close");
        },

        _onCancelButtonClick: function(e) {
            this.$popupDialog.dialog("close");
        },


        _getNodeByNodeSpan: function(nodeSpan) {
            return $(nodeSpan).closest('li.' + Classes.node);
        },

        _selectNode: function(node) {
            this.$treeControl.find('li.' + Classes.node).removeClass(Classes.nodeSelected);
            //this.$treeControl.find('li>span.' + Classes.nodeSpan).removeClass(Classes.nodeSpanSelected);
            this.$treeControl.find('span.' + Classes.nodeSpan).removeClass(Classes.nodeSpanSelected);

            $(node).addClass(Classes.nodeSelected);
            $(node).find('>span.' + Classes.nodeSpan).addClass(Classes.nodeSpanSelected); // adds it to the one that's just been clicked
            //$(node).find('span.' + Classes.nodeSpan + ':first').addClass(Classes.nodeSpanSelected); // adds it to the one that's just been clicked 

            if (this.options.keyboard) {
                this._highlightNode(node, true);
            }

            var gid = $(node).attr('gid');
            var gname = $(node).attr('gname');
            this.selectedID = gid;
            this.selectedText = gname;

        },

        _highlightNode: function(node, isScroll) {
            //this.$treeControl.find('li.' + Classes.node).removeClass(Classes.nodeHighlight);
            //this.$treeControl.find('li>span.' + Classes.nodeSpan).removeClass(Classes.nodeSpanHighlight);
            //this.$treeControl.find('span.' + Classes.nodeSpan).removeClass(Classes.nodeSpanHighlight);


            this._unHighlightNodeALL();

            $(node).addClass(Classes.nodeHighlight);
            $(node).find('>span.' + Classes.nodeSpan).addClass(Classes.nodeSpanHighlight); // adds it to the one that's just been clicked

            if (isScroll) {
                this._scrollToNode(node);
            }
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

        _getLinkGLGroupID: function(ctlID) {
            return $("#" + ctlID).find("." + Classes.linkValue).val();
        },

        _setLinkGroupData: function(ctlID) {

        },

        _unSelectALL: function() {
            this.$treeControl.find("li." + Classes.node).removeClass(Classes.nodeSelected);
            this.$treeControl.find("li span." + Classes.nodeSpan).removeClass(Classes.nodeSpanSelected);
            this.selectedID = null;
            this.selectedText = null;

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
            //$(node).select();
            //var elmPos = $(this._getSelectedNodeSpan()).offset();
            //$(node).find('>span.' + Classes.nodeSpan)
            //var elmPos = $(node).find('>span.' + Classes.nodeSpan).offset();
            //var elmPos = $(node).find('>span.' + Classes.).offset();

            pTop = this.$treeControl.parent('div').position().top;

            //this._setNodePathText("posTop:" + $(node).position().top + ", offTop:" + $(node).offset().top + ",prTop:" + pTop);
            var container = this.$treeControl.parent('div')
            //this._setNodePathText(this._isElementIntoView(node, container));  
            //;
            // alert(this._isElementIntoView(node, container));
            var element = $(node).find('>span.' + Classes.nodeSpan);
            //posText = "posTop:" + $(node).position().top + ", offTop:" + $(node).offset().top + ", prTop:" + pTop + ", prScrollTop:" + $(container).scrollTop();
            //posText = "posTop:" + $(node).position().top + ", offTop:" + $(node).offset().top + ", prTop:" + pTop + ", prScrollTop:" + $(container).scrollTop();

            if (this._isElementIntoView(element, container)) {
                //this._setNodePathText('true : ' + posText);
            }
            else {
                //$(element).focus();
                //var tTop = this.$treeControl.position().top;
                //                var elmPos = $(element).offset();
                //                var elmHeight = $(element).height();
                //                var contHeight = $(container).height();
                //                if (elmPos) {
                //                    diff = elmPos.top - contHeight;
                //                    //this.$treeControl.parent('div').scrollTop(diff);
                //                    this.$treeControl.parent('div').scrollTop(elmPos.top);
                //                }
            }

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
        show: function(gidorctl) {

            var id = 0;
            if (this.options.autoLink) {
                if (gidorctl) {
                    this.curLinkControlID = gidorctl;
                }
                else {
                    this.curLinkControlID = this.options.linkControlID;
                }
                if (this.options.highlightLink) {
                    this._highLightLink(this.curLinkControlID, true);
                }
                id = this._getLinkGLGroupID(this.curLinkControlID);
            }
            else {
                id = gidorctl;
            }

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

        selectNodeByID: function(id) {
            var self = this;
            this.$treeControl.find("li[gid='" + id + "']").each(function(index, domEle) {
                self._selectNode(domEle);
                //$(domEle).children("span." + Classes.nodeSpan).trigger('click');
            });
        },



        expandNodeByID: function(id) {
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

        scrollToSelectedNode: function() {
            var elmPos = $(this._getSelectedNodeSpan()).offset();
            if (elmPos) {
                this.$treeControl.parent('div').scrollTop(elmPos.top);
            }
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
