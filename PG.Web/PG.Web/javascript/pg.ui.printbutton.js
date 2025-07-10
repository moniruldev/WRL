/// <reference path="jsutility.js" />
/// <reference path="jquery-latest.min.js" />
/// <reference path="jquery-ui-latest.min.js" />
/// <reference path="jquery-latest.min-vsdoc.js" />
/// <reference path="ScrollableGridPlugin.js" />
/// <reference path="PG.ui.projectlist.js" />


(function($) {
    var Classes = {

        ddlSearchBy: "ddlSearchBy",
        txtSearch: "txtSearch",
        btnLoadData: "btnLoadData",
        btnLoadDataID: "btnLoadDataID",

        txtCode: "txtCode",
        txtName: "txtName",


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


    $.widget("PG.UI.PrintButton", {
        $projectListPopup: null,
        $txtProjectCode: null,
        $txtProjectName: null,
        $txtCompanyName: null,
        $hdnProjectID: null,
        $hdnLandID: null,
        $hdnIsEnabled: null,

        projectServiceLink: 'hdnProjectServiceLink',
        projectID: 'hdnProjectID',
        projectCode: 'txtProjectCode',
        projectName: 'txtProjectName',
        companyName: 'txtCompanyName',
        landID: 'hdnLandID',
        btnProject: 'btnProjectPopup',
        btnClear: 'btnProjectClear',

        projectListPopup: 'dvPopupProjectQuick',
        isEnabled: 'hdnIsEnabled',

        _isEnabled: true,

        options: {
            highlightLink: true,
            keyboard: true,
            gridHeight: 150,
            enableSelect: true,
            codeMinLength: 0,
            nameMinLength: 0,


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

            self.$txtProjectCode = $(this.element).find('input[id$="' + self.projectCode + '"]');
            self.$txtProjectName = $(this.element).find('input[id$="' + self.projectName + '"]');
            self.$txtCompanyName = $(this.element).find('input[id$="' + self.companyName + '"]');
            self.$hdnProjectID = $(this.element).find('input[id$="' + self.projectID + '"]');
            self.$hdnLandID = $(this.element).find('input[id$="' + self.landID + '"]');
            self.$hdnIsEnabled = $(this.element).find('input[id$="' + self.isEnabled + '"]');


            var enble = parseInt(self.$hdnIsEnabled.val());
            this._isEnabled = enble == 1 ? true : false;


            this._initProjectCodeAutoComplete();
            this._initProjectNameAutoComplete();

            this._intProjectListPopup();

            //this._bindDialogEvents();
            //this._bindAjaxEvents();

            this.enabled(this._isEnabled);
        },

        destroy: function() {
            //		    this.element
            //			    .removeClass( "progressbar" )
            //			    .text("");

            // call the base destroy function
            $.Widget.prototype.destroy.call(this);
        },

        //        _bindDialogEvents: function() {
        //            var self = this;

        //            //            $('.ui-widget-overlay').live('click', function(event) {
        //            //                self.$popupDialog.dialog("close");
        //            //            });

        //            $(this.element).find('input.' + Classes.btnSelect).click(function(e) {
        //                self._onSelectButtonClick(e, false);
        //            });

        //            $(this.element).find('input.' + Classes.btnSelectClose).click(function(e) {
        //                self._onSelectButtonClick(e, true);
        //            });

        //            $(this.element).find('input.' + Classes.btnClose).click(function(e) {
        //                self._onCloseButtonClick(e);
        //            });

        //            $(this.element).find('input.' + Classes.btnSelectALL).click(function(e) {
        //                self.selectALL();
        //            });

        //            $(this.element).find('input.' + Classes.btnUnSelectALL).click(function(e) {
        //                self.unselectALL();
        //            });


        //            $(this.element).find('input.' + Classes.btnLoadData).click(function(e) {
        //                self._loadData();
        //            });

        //            $(this.element).find('input.' + Classes.txtSearch).keydown(function(e) {
        //                if (e.keyCode == 13) {
        //                    self._loadData();
        //                    e.preventDefault();
        //                }
        //            });



        //        },



        //        _bindAjaxEvents: function() {
        //            var self = this;
        //            var prm = Sys.WebForms.PageRequestManager.getInstance();

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

        //        },
        _monkeyPatchAutocomplete: function() {

            // Don't really need to save the old fn, 
            // but I could chain if I wanted to
            var oldFn = $.ui.autocomplete.prototype._renderItem;

            //            $.ui.autocomplete.prototype._renderItem = function(ul, item) {
            //                var re = new RegExp("^" + this.term, "i");
            //                var t = item.label.replace(re, "<span style='font-weight:bold;color:Blue;'>" + this.term + "</span>");
            //                return $("<li></li>")
            //                      .data("item.autocomplete", item)
            //                      .append("<a>" + t + "</a>")
            //                      .appendTo(ul);
            //            };

            $.ui.autocomplete.prototype._renderMenu = function(ul, items) {
                var self = this;
                // $(ul).css('width', settings.dropDownWidth);

                //                $.each(data, function(index, item) {
                //                    self._renderItem(ul, item);
                //                });

                $.each(items, function(index, item) {
                    //                    if (item.category != currentCategory) {
                    //                        ul.append("<li class='ui-autocomplete-category'>" + item.category + "</li>");
                    //                        currentCategory = item.category;
                    //                    }
                    self._renderItem(ul, item);
                });


                $(ul).append("<div class='myFooter'>some footer text</div>");

            };
        },

        _customProjectAutoComplete: function() {
            $.widget("custom.customcomplete", $.ui.autocomplete, {
                // our fancy new _renderMenu function adds the header and footers!
                _renderMenu: function(ul, items) {
                    var self = this;
                    $.each(items, function(index, item) {
                        //                if (index == 0)
                        //                    ul.append('<li><input type="checkbox"> I\'m at the top! Choose me!</li>');
                        self._renderItem(ul, item);
                        //                if (index == items.length - 1)
                        //                    ul.append('<li><input type="checkbox"> I\'m at the bottom! Choose me!</li>');

                        if (index == items.length - 1)
                            ul.append('<li>Total 2 Found.</li>');
                    });
                }
            });

        },




        _setProjectData: function(prjData) {
            var self = this;

            $(this.element).find('input[id$="' + self.projectCode + '"]').val(prjData.code);
            $(this.element).find('input[id$="' + self.projectName + '"]').val(prjData.name);
            $(this.element).find('input[id$="' + self.companyName + '"]').val(prjData.companyname);
            $(this.element).find('input[id$="' + self.projectID + '"]').val(prjData.id);
            $(this.element).find('input[id$="' + self.landID + '"]').val(prjData.landid);

            //this._trigger("select", null, prjData.objProject);
            this._trigger("select", null, prjData);

        },

        _initProjectCodeAutoComplete: function() {
            var self = this;
            var serviceURL = $(this.element).find('input[id$="' + this.projectServiceLink + '"]').val();
            // //find('span[id$="' + this.projectCode + '"]')
            //self._monkeyPatchAutocomplete();
            //self._customProjectAutoComplete();
            self.$txtProjectCode.autocomplete({
                minLength: self.options.codeMinLength,
                matchContains: false,
                autoFill: false,
                mustMatch: true,
                source: function(request, response) {
                    $.ajax({
                        url: serviceURL + "?searchfield=prjcode" + "&prjcode=" + request.term,
                        cache: false,
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function(data) {
                            //alert(data.info.totrec);
                            response($.map(data.list, function(item) {
                                return {
                                    // label: item.code + "-" + item.name,
                                    label: item.code + " : " + item.name + " : " + item.companyname,
                                    value: item.code,
                                    id: item.id,
                                    desc: item.compname,
                                    projectData: item

                                }
                            }))
                            //$('.myFooter').text('dfdfdf');
                        },
                        error: function(XMLHttpRequest, textStatus, errorThrown) {
                            alert(textStatus);
                        }
                    });
                },
                change: function(event, ui) {
                    //$(this).next("input[id^=txtProjectCode]").val('');
                    //return false;
                },
                select: function(event, ui) {
                    //tbload(ui.item.id);
                    //alert(ui.item.id);
                    //alert(ui.item.value);
                    self._setProjectData(ui.item.projectData);
                },
                open: function(event, ui) {
                    $('.ui-autocomplete.ui-menu').addClass(Classes.txtCode);
                },
                close: function(event, ui) {
                    $('.ui-autocomplete.ui-menu').removeClass(Classes.txtCode);
                }
            });

            self.$txtProjectCode.blur(function() {
                var curText = self.$txtProjectCode.val().trim();
                if (curText == '') {
                    self.clearData();
                }
                else {
                    var prjItem = self._getProjectByCode(curText);
                    if (prjItem == null) {
                        self.clearData();
                        //                        if (confirm('project code not found!')) {
                        //                            self.clearData();
                        //                        }
                        //                        else {
                        //                            setTimeout(function() {
                        //                                self.$txtProjectCode.focus();
                        //                                //self.$txtProjectCode.val(curText);
                        //                            }, 0)
                        //                        }


                    }
                    else {
                        self.setSelectedItem(prjItem);
                    }
                }
            });

        },

        _initProjectNameAutoComplete: function() {
            var self = this;
            var serviceURL = $(this.element).find('input[id$="' + this.projectServiceLink + '"]').val();
            self.$txtProjectName.autocomplete({
                minLength: self.options.nameMinLength,
                source: function(request, response) {
                    $.ajax({
                        url: serviceURL + "?searchfield=prjname" + "&prjname=" + request.term,
                        cache: false,
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function(data) {
                            //alert(data.info.totrec);
                            response($.map(data.list, function(item) {
                                return {
                                    // label: item.code + "-" + item.name,
                                    label: item.name + " : " + item.code + " : " + item.companyname,
                                    value: item.name,
                                    id: item.id,
                                    desc: item.compname,
                                    projectData: item

                                }
                            }))
                        },
                        error: function(XMLHttpRequest, textStatus, errorThrown) {
                            alert(textStatus);
                        }
                    });
                },
                select: function(event, ui) {
                    //tbload(ui.item.id);
                    //alert(ui.item.id);
                    // alert(ui.item.value);
                    self._setProjectData(ui.item.projectData);
                },
                open: function(event, ui) {
                    $('.ui-autocomplete.ui-menu').addClass(Classes.txtName);
                },
                close: function(event, ui) {
                    $('.ui-autocomplete.ui-menu').removeClass(Classes.txtName);
                }
            });

            self.$txtProjectName.blur(function() {
                if ((self.$txtProjectName).val().trim() == '') {
                    self.clearData();
                }
                else {
                    var prjItem = self._getProjectByName((self.$txtProjectName).val().trim());
                    if (prjItem == null) {
                        self.clearData();
                    }
                    else {
                        self.setSelectedItem(prjItem);
                    }
                }
            });
        },


        _getProjectByID: function(prjID) {
            var self = this;
            var serviceURL = $(this.element).find('input[id$="' + this.projectServiceLink + '"]').val();

            var prj = null;
            var dummyVar = $.ajax({
                type: "GET",
                cache: false,
                async: false,
                dataType: "json",
                url: serviceURL + "?prjid=" + prjID,
                success: function(data) {
                    //alert(data.info.rowcount);
                    if (data.info.rowcount > 0) {
                        prj = data.list[0];
                    }
                },
                complete: function() {
                    //if (!isError) {
                    //return;
                    //alert (menu.name);
                    //}
                    //isComplete = true;
                },
                error: function(XMLHttpRequest, textStatus, errorThrown) {
                    //isError = true;
                    alert(textStatus);
                }
            });
            return prj;
        },


        _getProjectByCode: function(prjCode) {
            var self = this;
            var serviceURL = $(this.element).find('input[id$="' + this.projectServiceLink + '"]').val();

            var prj = null;
            var dummyVar = $.ajax({
                type: "GET",
                cache: false,
                async: false,
                dataType: "json",
                url: serviceURL + "?prjcode=" + prjCode,
                success: function(data) {
                    //alert(data.info.rowcount);
                    if (data.info.rowcount > 0) {
                        prj = data.list[0];
                    }
                },
                complete: function() {
                    //if (!isError) {
                    //return;
                    //alert (menu.name);
                    //}
                    //isComplete = true;
                },
                error: function(XMLHttpRequest, textStatus, errorThrown) {
                    //isError = true;
                    alert(textStatus);
                }
            });
            return prj;
        },

        _getProjectByName: function(prjName) {
            var self = this;
            var serviceURL = $(this.element).find('input[id$="' + this.projectServiceLink + '"]').val();

            var prj = null;
            var dummyVar = $.ajax({
                type: "GET",
                cache: false,
                async: false,
                dataType: "json",
                url: serviceURL + "?prjname=" + prjName + "&namecomptype=1",
                success: function(data) {
                    //alert(data.info.rowcount);
                    if (data.info.rowcount > 0) {
                        prj = data.list[0];
                    }
                },
                complete: function() {
                    //if (!isError) {
                    //return;
                    //alert (menu.name);
                    //}
                    //isComplete = true;
                },
                error: function(XMLHttpRequest, textStatus, errorThrown) {
                    //isError = true;
                    alert(textStatus);
                }
            });
            return prj;
        },


        _intProjectListPopup: function() {
            var self = this;
            var emlPopup = $(this.element).find('div[id$="' + self.projectListPopup + '"]')
            self.$projectListPopup = $(emlPopup).ProjectList({
                title: 'Select Project',
                autoLink: false,
                autoLinkUpdate: false,
                linkControlID: '',
                highlightLink: false,
                keyboard: true,
                enableSelect: true,
                enableProjectStatus: true,
                selectclick: function(event, data) {
                    setTimeout(function() { self._setProjectData(data) }, 50);
                    //$('#' + dvProjectPopup).ProjectList("close");
                    //setProjectData(data)

                },
                open: function(event, ui) {
                    // $("#dvGLGroup").addClass("dvGLGroupSelected");
                },
                close: function(event, ui) {
                    //            $("#dvGLGroup").removeClass("dvGLGroupSelected");
                    //            $('#' + ctlGLGroupText).focus();
                    //            $('#' + ctlGLGroupText).select();
                    self._trigger("listpopupclose", event, ui);

                }
            }); //projectlist

            $(this.element).find('div[id$="' + self.btnProject + '"]').click(function() {
                if (self._isEnabled == true) {
                    self.$projectListPopup.ProjectList("show");
                }
            });

            $(this.element).find('div[id$="' + self.btnClear + '"]').click(function() {
                if (self._isEnabled) {
                    //self.$projectListPopup.ProjectList("show");
                    self.clearData();
                }
            });
        },

        getProjectID: function() {
            var self = this;
            var prjID = parseInt(self.$hdnProjectID.val());
            return prjID;
        },

        getProjectCode: function() {
            var self = this;
            var prjCode = self.$txtProjectCode.val();
            return prjCode
        },


        getSelectedItem: function() {
            var self = this;

            var prjID = parseInt(self.$hdnProjectID.val());
            var prjCode = self.$txtProjectCode.val();
            var prjName = self.$txtProjectName.val();
            var compName = self.$txtCompanyName.val();
            var landID = parseInt(self.$hdnLandID.val());

            var selItem = {
                projectid: prjID,
                projectcode: prjCode,
                projectname: prjName,
                companyname: compName,
                landid: landID
            };

            return selItem;
        },



        setSelectedItem: function(prjItem) {
            var self = this;
            self.$hdnProjectID.val(prjItem.id);
            self.$txtProjectCode.val(prjItem.code);
            self.$txtProjectName.val(prjItem.name);
            self.$txtCompanyName.val(prjItem.companyname);
            self.$hdnLandID.val(prjItem.landid);
        },


        setItemByID: function(prjID) {
            var self = this;

            var prj = self._getProjectByID(prjID);
            if (prj != null) {
                self.setSelectedItem(prj);
            }
            else {
                self.clearData();
            }

        },


        setItemByCode: function(prjCode) {
            var self = this;

            var prj = self._getProjectByCode(prjCode);
            if (prj != null) {
                self.setSelectedItem(prj);
            }
            else {
                self.clearData();
            }

        },


        enabled: function(isEnabled) {
            var self = this;
            if (isEnabled == undefined) {

            }
            else {
                //self.$txtProjectCode.prop('disabled', !isEnabled);
                //self.$txtProjectName.prop('disabled', !isEnabled);
                //self.$txtCompanyName.prop('disabled', !isEnabled);



                self.$txtProjectCode.attr('readonly', !isEnabled);
                self.$txtProjectName.attr('readonly', !isEnabled);
                //self.$txtProjectName.attr('readonly', !isEnabled);

                //$(".selector").autocomplete({ disabled: true });
                self.$txtProjectCode.autocomplete({ disabled: !isEnabled });
                self.$txtProjectName.autocomplete({ disabled: !isEnabled });

                self._isEnabled = isEnabled;
                var enable = self._isEnabled ? "1" : "0";
                self.$hdnIsEnabled.val(enable);
            }
            //}
            return self._isEnabled;
        },

        clearData: function() {
            var self = this;
            self.$txtProjectCode.val('');
            self.$txtProjectName.val('');
            self.$txtCompanyName.val('');
            self.$hdnProjectID.val('0');
            self.$hdnLandID.val('0');
        },

        //for comma hazzard
        _lf: function() { }
    });


})(jQuery);