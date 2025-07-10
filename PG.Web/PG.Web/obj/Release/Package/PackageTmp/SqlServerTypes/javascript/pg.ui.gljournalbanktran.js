/// <reference path="jsutility.js" />
/// <reference path="jquery-latest.min.js" />
/// <reference path="jquery-ui-latest.min.js" />
/// <reference path="jquery-latest.min-vsdoc.js" />

(function($) {
    var Classes = {
        grid: "grid",
        gridRow: "gridRow",
        
        
        btnClose: "btnClose",

        l: ""

    }; //classes end
    
    
    $.widget("InterwaveUI.GLJournalBankTran", {
        $popupDialog: null,
        $gridView: null,
        gridViewID: 'GridView1',
        updatePanelID: 'UpdatePanel1',
        options: {
            title: 'Bank Transaction Info',
            keyboard: true,
            width: 720,
            height: 250,
            scrollGrid: true,

            l: ''
        },
        _create: function() {
            //		    this.element.addClass( "progressbar" );
            //		    this._update();
            //alert('created');

        },
        
        _init: function() {
            var self = this;
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
                width: this.options.width,  //300
                height: this.options.height, //'auto'
                open: function(event, ui) {
                    self.$popupDialog.parent().appendTo(jQuery("form:first"));

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


            this._bindDialogEvents();
        
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

            $(this.element).find('input.' + Classes.btnClose).click(function(e) {
                self._onCloseButtonClick(e);
            });

        },
        
        _onShow: function() {

        },

        _onClose: function() {
           
        },

        _onCloseButtonClick: function(e) {
            this.$popupDialog.dialog("close");
        },
        
         show: function() {
            this.$popupDialog.dialog("open");
        },


        //for comma hazzard
        _lf: function() { }
    });
    

})(jQuery);