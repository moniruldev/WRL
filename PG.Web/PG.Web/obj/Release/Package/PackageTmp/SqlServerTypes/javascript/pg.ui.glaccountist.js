/// <reference path="PG.jsutility.js" />
/// <reference path="PG.accutility.js" />

/// <reference path="jquery-latest.min.js" />
/// <reference path="jquery-ui-latest.min.js" />
/// <reference path="jquery-latest.min-vsdoc.js" />


(function ($) {
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

        accList: "accList",
        accListHeader: "accListHeader",
        accListBody: "accListBody",
        accListFooter: "accListFooter",


        ddlSearchBy: "ddlSearchBy",
        txtSearch: "txtSearch",
        btnLoadData: "btnLoadData",
        btnLoadDataID: "btnLoadDataID",

        ddlAccountType: "ddlAccountType",
        hdnAccClass: "hdnAccClass",

        dvGLGroup: "dvGLGroup",

        btnSelect: "btnSelect",
        btnSelectClose: "btnSelectClose",
        btnClose: "btnClose",

        btnSelectALL: "btnSelectALL",
        btnUnSelectALL: "btnUnselectALL",

        txtPageGoTo: "txtPageGoTo",
        btnPageGoTo: "btnGridPageGoTo",
        btnPageGoToID: "btnPageGoToID",



        glGroupPopup: "dvGroupListPopup",
        glGroup: "group_linkControl",
        glGroupText: "group_linkText",
        glGroupValue: "group_linkValue",

        glGroupBtnPopup: "btnPopup",
        glGroupBtnClear: "btnClear",




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


    $.widget("InterwaveUI.GLAccountList", {
        $popupDialog: null,
        $gridView: null,
        $glGroupList: null,
        gridViewID: 'GridView1',
        updatePanelID: 'UpdatePanel1',
        glAccountID: 'GLAccountID',
        glAccountCode: 'GLAccountCode',
        glAccountName: 'GLAccountName',
        glAccountTypeID: 'GLAccountTypeID',
        glAccountTypeName: 'GLAccountTypeName',
        glGroupID: 'GLGroupID',
        glGroupName: 'GLGroupName',
        glAccountClassID: 'GLAccountClassID',
        glAccountClassName: 'GLAccountClassName',
        
        options: {
            title: 'Select GL Account',
            multiSelect: false,
            autoLink: false,
            autoLinkUpdate: false,
            linkControlID: '',
            highlightLink: true,
            keyboard: true,
            width: 770,
            height: 460,
            enableSelect: true,
            enableSelectButton: true,
            enableSelectCloseButton: true,
            enableAccountType: true,
            enableGLGroup: true,

            normalAccount: true,
            controlAccount: false,
            subAccount: true,
            searchBy: 'code',
            loadDataOnShow: false,
            removeDataOnClose: true,

            l: ''
        },

        _create: function () {
            //		    this.element.addClass( "progressbar" );
            //		    this._update();
            //alert('created');

        },

        _init: function () {
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
                open: function (event, ui) {
                    self.$popupDialog.parent().appendTo(jQuery("form:first"));

                    //                    if (self.options.scrollGrid) {
                    //                        self._makeGridScrollable();
                    //                    }
                    self._onShow();
                    self._trigger("open", event, ui);
                },
                close: function (event, ui) {
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


            this._initGLGroupList();

        },

        destroy: function () {
            //		    this.element
            //			    .removeClass( "progressbar" )
            //			    .text("");

            // call the base destroy function
            $.Widget.prototype.destroy.call(this);
        },

        _bindGridEvents: function () {
            var self = this;

            var selector = $(this.element).find("." + Classes.grid + " TR." + Classes.gridRow);
            $(selector).css('background-color', '');

            $(selector).filter(":odd").addClass("oddRow");
            $(selector).filter(":even").addClass("evenRow");

            $(selector).mouseenter(function () {
                $(this).addClass("highlightRow");
            });

            $(selector).mouseout(function () {
                $(this).removeClass("highlightRow");
            });

            $(selector).click(function () {
                //alert('click');
                eType = $(this).find('input[id$="' + self.glAccountTypeID + '"]').val();
                if (self._isAccountTypeValid(eType)) {
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
                }
            }); //click


            //page go to
            $(this.element).find('input.' + Classes.txtPageGoTo).keydown(function (e) {
                if (e.keyCode == 13) {
                    e.preventDefault();
                    self._pageGoTo();
                }
            });


        },

        _bindDialogEvents: function () {
            var self = this;

            //            $('.ui-widget-overlay').live('click', function(event) {
            //                self.$popupDialog.dialog("close");
            //            });

            $(this.element).find('input.' + Classes.btnSelect).click(function (e) {
                self._onSelectButtonClick(e, false);
            });

            $(this.element).find('input.' + Classes.btnSelectClose).click(function (e) {
                self._onSelectButtonClick(e, true);
            });

            $(this.element).find('input.' + Classes.btnClose).click(function (e) {
                self._onCloseButtonClick(e);
            });

            $(this.element).find('input.' + Classes.btnSelectALL).click(function (e) {
                self.selectALL();
            });

            $(this.element).find('input.' + Classes.btnUnSelectALL).click(function (e) {
                self.unselectALL();
            });


            $(this.element).find('input.' + Classes.btnLoadData).click(function (e) {
                self._loadData();
            });

            $(this.element).find('input.' + Classes.txtSearch).keydown(function (e) {
                if (e.keyCode == 13) {
                    e.preventDefault();
                    self._loadData();
                }
            });
        },

        _bindKeyNavigation: function () {
            var self = this;
        },

        _bindAjaxEvents: function () {
            var self = this;
            var prm = Sys.WebForms.PageRequestManager.getInstance();

            prm.add_endRequest(function () {
                // re-bind your jQuery events here
                //alert('END REQUEST');
                //MakeGridScrollable();
                //GridFunctions();
                self._bindGridEvents();
                self._setRowGLAccountType();
            });
        },

        _initGLGroupList: function () {
            var self = this;

            var glGroup = $(this.element).find("." + Classes.glGroup);

            var grpListPopup = $(this.element).find("." + Classes.glGroupPopup);

            this.$glGroupList = $(grpListPopup).GroupTree({
                title: 'Select GL Group',
                autoLink: false,
                autoLinkUpdate: false,
                linkControlID: '',
                highlightLink: false,
                keyboard: true,

                okclick: function (event, data) {
                    //alert("GL Group ID: " + data.value);
                    self._setGLGroupData(data);
                },
                open: function (event, ui) {

                },
                close: function (event, ui) {
                }
            });


            $(glGroup).find('.btnPopup').click(function (e) {
                //$(selector).GroupTree("show");
                self._openGLGroupTree();
            });


            $(glGroup).find('.btnClear').click(function (e) {
                self._clearGLGroupData();
            });


            $(glGroup).find('.group_linkText').keydown(function (e) {
                switch (e.keyCode) {
                    case 46:  //delete
                        self._clearGLGroupData();
                        break;
                    case 8:  //backspace
                        self._clearGLGroupData();
                        e.preventDefault();
                        break;
                    case 13:  //enter
                        self._openGLGroupTree();
                        e.preventDefault();
                        break;
                }
            });

        },

        _openGLGroupTree: function () {
            //this.$glGroupList.GroupTree("show");
            var selector = $(this.element).find("." + Classes.glGroup);
            var glGroupID = $(selector).find('.group_linkValue').val();

            this.$glGroupList.GroupTree("option", "enableSelect", this.options.enableGLGroup);
            this.$glGroupList.GroupTree("showByID", glGroupID);

            //$(selector).GroupTree("show");
        },

        _setGLGroupData: function (data) {
            var self = this;
            var selector = $(this.element).find("." + Classes.glGroup);

            $(selector).find('.group_linkText').val(data.text);
            $(selector).find('.group_linkValue').val(data.value);

        },

        _clearGLGroupData: function () {
            var self = this;
            var selector = $(this.element).find("." + Classes.glGroup);


            if ($(selector).find('.group_linkText').is(':disabled')) {
            }
            else {
                $(selector).find('.group_linkText').val('');
                $(selector).find('.group_linkValue').val('0');
            }
        },


        _isControlAccount: function (gRow) {
            var isControlAcc = false;
            accTypeID = $(gRow).find('input[id$="' + this.glAccountTypeID + '"]').val();
            if (accTypeID == AccUtility.GLAccountType.ControlAccount) {
                isControlAcc = true;
            }

            return isControlAcc;
        },


        _loadData: function () {
            btnID = $(this.element).find('input.' + Classes.btnLoadDataID).val();
            __doPostBack(btnID, '');
        },

        _pageGoTo: function () {
            btnID = $(this.element).find('input.' + Classes.btnPageGoToID).val();
            __doPostBack(btnID, '');
        },


        _onShow: function () {
            //this._resizeDivs();
            //totRow = $(this.element).find("." + Classes.gridRowCount).val();
            //$(this.element).find('table[id$="' + this.gridViewID + '"]').css('width', '100%');

            if (this.options.enableSelect) {
                if (this.options.enableSelectButton) {
                    //$(this.element).find("." + Classes.btnSelect).attr('disabled', '');
                    $(this.element).find("." + Classes.btnSelect).removeAttr('disabled');
                }
                else {
                    $(this.element).find("." + Classes.btnSelect).attr('disabled', 'disabled');
                }
                //$(this.element).find("." + Classes.btnSelectClose).attr('disabled', '');
                $(this.element).find("." + Classes.btnSelectClose).removeAttr('disabled');

                if (this.options.multiSelect) {
                    //                    $(this.element).find("." + Classes.btnSelectALL).attr('disabled', '');
                    //                    $(this.element).find("." + Classes.btnUnSelectALL).attr('disabled', '');
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

            if (this.options.enableAccountType) {
                //$(this.element).find("." + Classes.ddlAccountType).attr('disabled', '');
                $(this.element).find("." + Classes.ddlAccountType).removeAttr('disabled');
            }
            else {
                $(this.element).find("." + Classes.ddlAccountType).attr('disabled', 'disabled');
            }

            if (this.options.enableGLGroup) {
                //                $(this.element).find("." + Classes.dvGLGroup).attr('disabled', '');
                //                $(this.element).find("." + Classes.dvGLGroup).find(':input').attr('disabled', '');
                //                $(this.element).find("." + Classes.dvGLGroup).find('.' + Classes.glGroupBtnClear).attr('disabled', '');
                $(this.element).find("." + Classes.dvGLGroup).removeAttr('disabled');
                $(this.element).find("." + Classes.dvGLGroup).find(':input').removeAttr('disabled');
                $(this.element).find("." + Classes.dvGLGroup).find('.' + Classes.glGroupBtnClear).removeAttr('disabled');

                //$('#idOfTheDIV :input').attr('disabled', true);
            }
            else {
                $(this.element).find("." + Classes.dvGLGroup).attr('disabled', 'disabled');
                $(this.element).find("." + Classes.dvGLGroup).find(':input').attr('disabled', 'disabled');
                $(this.element).find("." + Classes.dvGLGroup).find('.' + Classes.glGroupBtnClear).attr('disabled', 'disabled');
            }


        },

        _onClose: function () {
            $(this.element).find("." + Classes.txtSearch).val('');
            if (this.options.removeDataOnClose) {
                this.removeRows();
            }

        },


        _resizeDivs: function () {

            $(this.element).find('div[id$="' + this.updatePanelID + '"]').height(340);
            //            dcHight = $(this.element).height();
            //            $(this.element).find('.' + Classes.empList).height(dcHight - 4);
            //            eHight = $(this.element).find('.' + Classes.empList).height();
            //            headerHeight = $(this.element).find('.' + Classes.empListHeader).height();
            //            footerHeight = $(this.element).find('.' + Classes.empListFooter).height();

            //            bdHeight = eHight - headerHeight - footerHeight;
            //            $(this.element).find('.' + Classes.empListBody).height(bdHeight);

        },

        _setRowGLAccountType: function () {
            //$(this.element).find("." + Classes.grid + " TR." + Classes.gridRow);
            var self = this;
            $(this.element).find("." + Classes.grid + " TR." + Classes.gridRow).each(function (index, elem) {
                eType = $(elem).find('input[id$="' + self.glAccountTypeID + '"]').val();
                if (!self._isAccountTypeValid(eType)) {
                    $(elem).addClass(Classes.disabledRow);
                }
            });

        },

        _getSelectedRow: function () {
            //var selector = $(this.element).find("." + Classes.grid + " ." + Classes.gridRow);
            return this.$popupDialog.find("." + Classes.grid + " ." + Classes.selectedRow);
        },



        _onSelectButtonClick: function (e, isClose) {
            var self = this;
            var rows = this._getSelectedRow();

            if (rows.length == 0) {
                alert('No Row Selected');
                return;
            }

            var eID = 0;
            var eCode = '';
            var eName = '';
            var eTypeID = 0;
            var eTypeName = '';
            var eGrpID = 0;
            var eGrpName = '';

            if (rows.length == 1) {
                eID = $(rows).find('input[id$="' + self.glAccountID + '"]').val();
                eCode = $(rows).find('span[id$="' + self.glAccountCode + '"]').text();
                eName = $(rows).find('span[id$="' + self.glAccountName + '"]').text();

                eTypeID = $(rows).find('input[id$="' + self.glAccountTypeID + '"]').val();
                eTypeName = $(rows).find('input[id$="' + self.glAccountTypeName + '"]').text();

                eGrpID = $(rows).find('input[id$="' + self.glGroupID + '"]').val();
                eGrpName = $(rows).find('span[id$="' + self.glGroupName + '"]').text();

                eAccClassID = $(rows).find('input[id$="' + self.glAccountClassID + '"]').val();
                //eAccClassName = $(rows).find('span[id$="' + self.glAccountClassName + '"]').text(); 
                eAccClassName = '';

                if (self._isAccountTypeValid(eType)) {
                    this._trigger("selectclick", null, { id: eID, code: eCode, name: eName
                                                            , typeid: eTypeID, typename: eTypeName
                                                            , groupid: eGrpID, groupname: eGrpName
                                                            , accclassid: eAccClassID, accclassname: eAccClassName
                    });
                }
                else {
                    alert('Account Type not valid for selection!');
                }
            }
            else {
                var data = new Array();
                $(rows).each(function (index, elem) {
                    eID = $(elem).find('input[id$="' + self.glAccountID + '"]').val();
                    eCode = $(elem).find('span[id$="' + self.glAccountCode + '"]').text();
                    eName = $(elem).find('span[id$="' + self.glAccountName + '"]').text();

                    eTypeID = $(elem).find('input[id$="' + self.glAccountTypeID + '"]').val();
                    eTypeName = $(rows).find('input[id$="' + self.glAccountTypeName + '"]').text();

                    eGrpID = $(rows).find('input[id$="' + self.glGroupID + '"]').val();
                    eGrpName = $(rows).find('span[id$="' + self.glGroupName + '"]').text();

                    eAccClassID = $(rows).find('input[id$="' + self.glAccountClassID + '"]').val();
                    //eAccClassName = $(rows).find('span[id$="' + self.glAccountClassName + '"]').text(); 
                    eAccClassName = '';

                    if (self._isAccountTypeValid(eType)) {
                        var c = { id: eID, code: eCode, name: eName
                                    , typeid: eTypeID, typename: eTypeName
                                    , groupid: eGrpID, groupname: eGrpName
                                    , accclassid: eAccClassID, accclassname: eAccClassName
                        };
                        data.push(c);
                    }
                });
                this._trigger("selectclick", null, data);
            }

            self.unselectALL();

            //alert(eName);
            if (isClose) {
                this.$popupDialog.dialog("close");
            }
        },

        _onCloseButtonClick: function (e) {
            this.$popupDialog.dialog("close");
        },

        _isAccountTypeValid: function (accType) {
            var isValid = false;
            switch (parseInt(accType)) {
                case AccUtility.GLAccountType.NormalAccount:
                    isValid = this.options.normalAccount;
                    break;
                case AccUtility.GLAccountType.ControlAccount:
                    isValid = this.options.controlAccount;
                    break;
                case AccUtility.GLAccountType.SubAccount:
                    isValid = this.options.subAccount;
                    break;
            }
            return isValid;
        },

        _setSearchBy: function () {
            var sval = 1;
            switch (this.options.searchBy) {
                case 'code':
                    sval = 1;
                    break;
                case 'name':
                    sval = 2;
                    break;
            }
            $(this.element).find("." + Classes.ddlSearchBy).val(sval);
        },

        //public functions
        show: function (strSearch, delRows) {

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

        setAccountType: function (accTypeID) {
            //ddlAccountType
            $(this.element).find("." + Classes.ddlAccountType).val(accTypeID);

        },

        setGLGroup: function (glGroupID) {
            if (glGroupID > 0) {
                var gName = this.$glGroupList.GroupTree("getNameByID", glGroupID);
                if (gName != null) {
                    $(this.element).find("." + Classes.glGroupValue).val(glGroupID);
                    $(this.element).find("." + Classes.glGroupText).val(gName);
                }
            }
        },


        setAccClass: function (glAccClass) {
            $(this.element).find("." + Classes.hdnAccClass).val(glAccClass);
        },


        selectALL: function () {
            var self = this;
            if (this.options.multiSelect) {
                //var selector = $(this.element).find("." + Classes.grid + " ." + Classes.gridRow);
                $(this.element).find("." + Classes.grid + " ." + Classes.gridRow).each(function (index, elem) {
                    eType = $(elem).find('input[id$="' + self.glAccountTypeID + '"]').val();
                    if (self._isAccountTypeValid(eType)) {
                        $(elem).addClass("selectedRow");
                    }
                });

                //$(selector).addClass("selectedRow");
            }
        },

        removeRows: function () {
            var selector = $(this.element).find("." + Classes.grid + " ." + Classes.gridRow);
            $(selector).remove();

            var selector1 = $(this.element).find("." + Classes.pagerRow);
            $(selector1).remove();

        },


        unselectALL: function () {
            var selector = $(this.element).find("." + Classes.grid + " ." + Classes.gridRow);
            $(selector).removeClass("selectedRow");
        },

        //for comma hazzard
        _lf: function () { }
    });


})(jQuery);