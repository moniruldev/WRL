/// <reference path="jsutility.js" />
/// <reference path="jquery-latest.min.js" />
/// <reference path="jquery-ui-latest.min.js" />
/// <reference path="jquery-latest.min-vsdoc.js" />
/// <reference path="ScrollableGridPlugin.js" />


(function($) {
    var Classes = {
        grid: "grid",
        gridRow: "gridRow",

        headerRow: "headerRow",
        footerRow: "footerRow",
        pagerRow: "pagerRow",

        evenRow: "evenRow",
        oddRow: "oddRow",

        highlightRow: "highlightRow",
        selectedRow: "selectedRow",
        disabledRow: "disabledRow",


        gridRowCount: "gridRowCount",


        projectList: "projectList",
        projectListHeader: "projectListHeader",
        projectListBody: "projectListBody",
        projectListFooter: "projectListFooter",


        ddlSearchBy: "ddlSearchBy",
        txtSearch: "txtSearch",
        btnLoadData: "btnLoadData",
        btnLoadDataID: "btnLoadDataID",


        ddlProjectStatus: "ddlProjectStatus",
        ddlCompany: "ddlCompany",


        btnSelect: "btnSelect",
        btnSelectClose: "btnSelectClose",
        btnClose: "btnClose",

        btnSelectALL: "btnSelectALL",
        btnUnSelectALL: "btnUnselectALL",


        l: ""

    }; //classes end

    var Keys = {
        left: 37,
        right: 39,
        up: 38,
        down: 40,

        enter: 13,
        space: 32,

        backspace: 8,

        l: ""
    }; //keys end


    var ProjectStatus = {
        Running: 1,
        Closed: 2,

        l: 0
    };


    $.widget("PG.UI.ProjectList", {
        $popupDialog: null,
        $gridView: null,
        gridViewID: 'GridView1',
        updatePanelID: 'UpdatePanel1',
        projectID: 'ProjectID',
        projectCode: 'ProjectCode',
        projectName: 'ProjectName',
        projectStatusID: 'ProjectStatusID',
        projectStatusName: 'ProjectStatusName',
        companyID: 'CompanyID',
        companyName: 'CompanyName',
        landID: 'LandID',
        options: {
            title: 'Select Project',
            multiSelect: false,
            autoLink: false,
            autoLinkUpdate: false,
            linkControlID: '',
            highlightLink: true,
            keyboard: true,
            width: 750,
            height: 410,
            gridHeight: 150,
            scrollGrid: true,
            enableSelect: true,
            enableSelectButton: true,
            enableSelectCloseButton: true,

            enableCompany: true,
            enableProjectStatus: true,

            searchBy: 'code',
            loadDataOnShow: false,
            removeDataOnClose: true,

            l: ''
        },

        _create: function() {
            //		    this.element.addClass( "progressbar" );
            //		    this._update();
            //alert('created');

        },

        _init: function() {
            var self = this;
            //alert($(this.element).attr('id'));
            //alert('acclist init');
            //alert('init'); 
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

                    $('.ui-widget-overlay').height('100%');
                    $('.ui-widget-overlay').width('100%');

                    setTimeout(function() {
                        $('.ui-widget-overlay').height('100%');
                        $('.ui-widget-overlay').width('100%');
                     }, 0);

                    //$('.ui-widget-overlay').height('100%');
                    // $('.ui-widget-overlay').width('100%');

                    //                    if (self.options.scrollGrid) {
                    //                        self._makeGridScrollable();
                    //                    }
                    self._onShow();
                    self._trigger("open", event, ui);
                },
                close: function(event, ui) {
                    self._onClose();
                    self._trigger("close", event, ui);
                }
            });
            self.$popupDialog.parent().appendTo(jQuery("form:first"));


            this._bindGridEvents();
            this._bindDialogEvents();
            this._bindAjaxEvents();
            if (this.options.keyboard) {
                this._bindKeyNavigation();
            }
            totRow = $(this.element).find("." + Classes.gridRowCount).val();
            if (totRow == 0) {
                $(this.element).find('table[id$="' + this.gridViewID + '"]').css('width', '100%');
            }

        },

        destroy: function() {
            //		    this.element
            //			    .removeClass( "progressbar" )
            //			    .text("");

            // call the base destroy function
            $.Widget.prototype.destroy.call(this);
        },

        _bindGridEvents: function() {
            var self = this;

            var selector = $(this.element).find("." + Classes.grid + " TR." + Classes.gridRow);
            $(selector).css('background-color', '');

            $(selector).filter(":odd").addClass("oddRow");
            $(selector).filter(":even").addClass("evenRow");

            //            $(selector).mouseenter(function() {
            //                $(this).addClass("highlightRow");
            //            });

            //            $(selector).mouseleave(function() {
            //                $(this).removeClass("highlightRow");
            //            });


            $(selector).hover(function() {
                $(this).addClass("highlightRow");
            },
                function() {
                    $(this).removeClass("highlightRow");
                }
            );


            $(selector).click(function() {
                //alert('click');


                if ($(this).hasClass("selectedRow")) {
                    $(this).removeClass("selectedRow");
                }
                else {

                    if (self.options.multiSelect == false) {
                        $(selector).removeClass("selectedRow");
                    }
                    //alert(self._isControlAccount(this));
                    $(this).addClass("selectedRow");
                }

            }); //click

        },

        _bindDialogEvents: function() {
            var self = this;

            //            $('.ui-widget-overlay').live('click', function(event) {
            //                self.$popupDialog.dialog("close");
            //            });

            $(this.element).find('input.' + Classes.btnSelect).click(function(e) {
                self._onSelectButtonClick(e, false);
            });

            $(this.element).find('input.' + Classes.btnSelectClose).click(function(e) {
                self._onSelectButtonClick(e, true);
            });

            $(this.element).find('input.' + Classes.btnClose).click(function(e) {
                self._onCloseButtonClick(e);
            });

            $(this.element).find('input.' + Classes.btnSelectALL).click(function(e) {
                self.selectALL();
            });

            $(this.element).find('input.' + Classes.btnUnSelectALL).click(function(e) {
                self.unselectALL();
            });


            //            $(this.element).find('input.' + Classes.btnLoadData).click(function(e) {
            //                self._loadData();
            //            });

            $(this.element).find('input.' + Classes.txtSearch).keydown(function(e) {
                if (e.keyCode == 13) {
                    self._loadData();
                    e.preventDefault();
                }
            });



        },


        _bindKeyNavigation: function() {
            var self = this;


        },

        _bindAjaxEvents: function() {
            var self = this;
            var prm = Sys.WebForms.PageRequestManager.getInstance();

            //            prm.add_endRequest(function() {
            //                // re-bind your jQuery events here
            //                //alert('END REQUEST');
            //                //MakeGridScrollable();
            //                //GridFunctions();

            //                if (self.options.scrollGrid) {
            //                    self._makeGridScrollable();
            //                }

            //                self._bindGridEvents();
            //                self._resizeDivs();

            //            });

            var updatePanelID = $(this.element).find('div[id$="' + self.updatePanelID + '"]').attr('id');
            prm.add_pageLoaded(function(sender, args) {
                var panels = args.get_panelsUpdated();
                for (i = 0; i < panels.length; i++) {
                    if (panels[i].id == updatePanelID) {
                        self._bindGridEvents();
                        self._resizeDivs();
                        if (self.options.scrollGrid) {
                            self._makeGridScrollable();
                        }
                        else {
                            totRow = $(self.element).find("." + Classes.gridRowCount).val();
                            if (totRow == 0) {
                                $(self.element).find('table[id$="' + self.gridViewID + '"]').css('width', '100%');
                            }
                        }

                    }
                }
                //self.$popupDialog.parent().appendTo(jQuery("form:first")); 
            });



        },

        _loadData: function() {
            btnID = $(this.element).find('input.' + Classes.btnLoadDataID).val();
            __doPostBack(btnID, '');
            //$('#' + btnID).click();
        },




        _onShow: function() {
            this._resizeDivs();
            //totRow = $(this.element).find("." + Classes.gridRowCount).val();
            //$(this.element).find('table[id$="' + this.gridViewID + '"]').css('width', '100%');

            if (this.options.enableSelect) {
                if (this.options.enableSelectButton) {
                    $(this.element).find("." + Classes.btnSelect).removeAttr('disabled');
                }
                else {
                    $(this.element).find("." + Classes.btnSelect).attr('disabled', 'disabled');
                }
                $(this.element).find("." + Classes.btnSelectClose).removeAttr('disabled');

                if (this.options.multiSelect) {
                    $(this.element).find("." + Classes.btnSelectALL).removeAttr('disabled');
                    $(this.element).find("." + Classes.btnUnSelectALL).removeAttr('disabled');
                }
                else {
                    $(this.element).find("." + Classes.btnSelectALL).attr('disabled', 'disabled');
                    $(this.element).find("." + Classes.btnUnSelectALL).attr('disabled', 'disabled');
                }

            }
            else {
                $(this.element).find("." + Classes.btnSelect).attr('disabled', 'disabled');
                $(this.element).find("." + Classes.btnSelectClose).attr('disabled', 'disabled');

                $(this.element).find("." + Classes.btnSelectALL).attr('disabled', 'disabled');
                $(this.element).find("." + Classes.btnUnSelectALL).attr('disabled', 'disabled');
            }

            //            if (this.options.enableProjectStatus) {
            //                $(this.element).find("." + Classes.ddlProjectStatus).removeAttr('disabled');
            //            }
            //            else {
            //                $(this.element).find("." + Classes.ddlProjectStatus).attr('disabled', 'disabled');
            //            }

        },

        _onClose: function() {
            $(this.element).find("." + Classes.txtSearch).val('');
            if (this.options.removeDataOnClose) {
                this.removeRows();
            }

        },

        _makeGridScrollable: function() {
            totRow = $(this.element).find("." + Classes.gridRowCount).val();

            var sBodyHeight = $(this.element).find(".projectListBody").height();
            sBodyHeight = sBodyHeight - 8;
            if (totRow > 0) {
                $(this.element).find('table[id$="' + this.gridViewID + '"]').Scrollable({
                    //ScrollHeight: sBodyHeight
                    GridHeight: sBodyHeight
                });
            }
            else {
                $(this.element).find('table[id$="' + this.gridViewID + '"]').css('width', '100%');
            }
        },

        _resizeDivs: function() {

            var listHeight = $(this.element).find('.projectList').height();
            var headerHeight = $(this.element).find('.projectListHeader').height();
            var footerHeight = $(this.element).find('.projectListFooter').height();

            var bodyHeight = listHeight - headerHeight - footerHeight - 4;
            $(this.element).find('.projectListBody').height(bodyHeight);
            $(this.element).find('div[id$="' + this.updatePanelID + '"]').height(bodyHeight);

            //$(this.element).find('div[id$="' + this.gridViewID + '"]').height(bodyHeight);
            //$(this.element).find('.' + Classes.grid).height('auto');

            // $(this.element).find('div[id$="' + this.updatePanelID + '"]').height(340);
            //            dcHight = $(this.element).height();
            //            $(this.element).find('.' + Classes.empList).height(dcHight - 4);
            //            eHight = $(this.element).find('.' + Classes.empList).height();
            //            headerHeight = $(this.element).find('.' + Classes.empListHeader).height();
            //            footerHeight = $(this.element).find('.' + Classes.empListFooter).height();

            //            bdHeight = eHight - headerHeight - footerHeight;
            //            $(this.element).find('.' + Classes.empListBody).height(bdHeight);

        },


        _getSelectedRow: function() {
            //var selector = $(this.element).find("." + Classes.grid + " ." + Classes.gridRow);
            return this.$popupDialog.find("." + Classes.grid + " ." + Classes.selectedRow);
        },



        _onSelectButtonClick: function(e, isClose) {
            var self = this;
            var rows = this._getSelectedRow();

            if (rows.length == 0) {
                alert('No Row Selected');
                return;
            }

            var eID = 0;
            var eCode = '';
            var eName = '';
            var companyID = 0;
            var companyName = '';
            var landID = 0;

            if (rows.length == 1) {
                eID = $(rows).find('input[id$="' + this.projectID + '"]').val();
                eCode = $(rows).find('span[id$="' + this.projectCode + '"]').text();
                eName = $(rows).find('span[id$="' + this.projectName + '"]').text();
                eType = $(rows).find('input[id$="' + this.projectStatusID + '"]').val();
                companyID = $(rows).find('input[id$="' + this.companyID + '"]').val();
                companyName = $(rows).find('span[id$="' + this.companyName + '"]').text();
                landID = $(rows).find('input[id$="' + this.landID + '"]').val();


                this._trigger("selectclick", null, { id: eID, code: eCode, name: eName, companyid: companyID, companyname: companyName, landid: landID });

                //                if (self._isAccountTypeValid(eType)) {
                //                    this._trigger("selectclick", null, { id: eID, code: eCode, name: eName });
                //                }
                //                else {
                //                    alert('Project Type not valid for selection!');
                //                }
            }
            else {
                var data = new Array();
                $(rows).each(function(index, elem) {
                    eID = $(elem).find('input[id$="' + self.projectID + '"]').val();
                    eCode = $(elem).find('span[id$="' + self.projectCode + '"]').text();
                    eName = $(elem).find('span[id$="' + self.projectName + '"]').text();
                    eType = $(elem).find('input[id$="' + self.projectStatusID + '"]').val();
                    companyID = $(rows).find('input[id$="' + self.companyID + '"]').val();
                    companyName = $(rows).find('span[id$="' + this.companyName + '"]').text();
                    landID = $(rows).find('input[id$="' + self.landID + '"]').val();
                    var c = { id: eID, code: eCode, name: eName, companyid: companyID, companyname: companyName, landid: landID };
                    data.push(c);

                    //                    if (self._isAccountTypeValid(eType)) {
                    //                        var c = { id: eID, code: eCode, name: eName };
                    //                        data.push(c);
                    //                    }
                });
                this._trigger("selectclick", null, data);
            }

            self.unselectALL();

            //alert(eName);
            if (isClose) {
                //this.$popupDialog.dialog("close");
                //alert('close');
                this.close();
            }
        },

        _onCloseButtonClick: function(e) {
            this.$popupDialog.dialog("close");
        },

        _setSearchBy: function() {
            var sval = 1;
            switch (this.options.searchBy) {
                case 'code':
                    sval = 1;
                    break;
                case 'name':
                    sval = 2;
                    break;
                case 'address':
                    sval = 3;
                    break;
            }
            $(this.element).find("." + Classes.ddlSearchBy).val(sval);
        },

        //public functions
        show: function(strSearch, delRows) {

            //this.collapseALL();
            this.$popupDialog.dialog("open");
            this._setSearchBy();

            if (strSearch) {
                $(this.element).find("." + Classes.txtSearch).val(strSearch);
            }
            $(this.element).find("." + Classes.txtSearch).focus();
            if (delRows) {
                this.removeRows();
            }

            if (this.options.loadDataOnShow) {
                this._loadData();
            }

        },

        close: function() {
            //alert('close');
            this.$popupDialog.dialog("close");
        },

        selectALL: function() {
            var self = this;
            if (this.options.multiSelect) {
                //var selector = $(this.element).find("." + Classes.grid + " ." + Classes.gridRow);
                $(this.element).find("." + Classes.grid + " ." + Classes.gridRow).each(function(index, elem) {
                    $(elem).addClass("selectedRow");
                    //                    eType = $(elem).find('input[id$="' + self.glAccountTypeID + '"]').val();
                    //                    if (self._isAccountTypeValid(eType)) {
                    //                        $(elem).addClass("selectedRow");
                    //                    }
                });

                //$(selector).addClass("selectedRow");
            }
        },

        removeRows: function() {
            var selector = $(this.element).find("." + Classes.grid + " ." + Classes.gridRow);
            $(selector).remove();

            var selector1 = $(this.element).find("." + Classes.pagerRow);
            $(selector1).remove();

        },


        unselectALL: function() {
            var selector = $(this.element).find("." + Classes.grid + " ." + Classes.gridRow);
            $(selector).removeClass("selectedRow");
        },

        //for comma hazzard
        _lf: function() { }
    });


})(jQuery);