/* File Created: April 30, 2013 */
/* Multi Column Autocomplete */

/* column def
var acColumns = [{ name: 'Type', width: 140, valueField: 'value', align: 'left' },
                 { name: 'Remain', width: 120, valueField: 'remainamt', align: 'right'}]
*/


/* int example
  $(".txtPrjectExpenseTypeAutoComplete").mcautocomplete({
            appendTo : 'form:first',
            minLength: 0,
            autoFocus: true,
            matchCase: false,
            showHeader: true,
            showFooter: true,
            listHeight: 120,
            selectFirst: true,
            autoFocus: false,
            columns: acColumns,
            source: function (request, response) {
                var element = this.element;
                var projectid = $('#' + dvProjectSelectinQuick).ProjectSelectionQuick("getProjectID");
                $.ajax({
                    url: serviceURL + "?typename=" + request.term + "&projectid=" + projectid,
                    cache: false,
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        //alert(data.info.totrec);
                        if (data.info.totrec == 0) {
                            data.list.push({
                                id: 0,
                                name: '',
                                enable: false
                            });
                        }
                        
                        
                        response($.map(data.list, function (item) {
                            return {
                                label: item.name,
                                value: item.name,
                                id: item.id,
                                expenseamt: item.expenseamt,
                                adjustedamt: item.adjustedamt,
                                remainamt: item.remainamt,
                                enable: item.enable
                            }
                        }))
                    },

                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        alert(textStatus);
                    }
                });
            },
            open: function (event, ui) {
               $(this).mcautocomplete("openTask");
//                return true;
            },

            close: function (event, ui) {
                $(this).mcautocomplete("closeTask");
                //return true;
            },
            change: function (event, ui) {
                if (!ui.item) {
                    //$(this).val('');
                    //alert('no item');
                    var row = $(this).parentsUntil($('#' + gridViewID2), 'tr.gridRow');
                    //                    clearExpenseRowDetails(row);
                    //                    sumAdjustAmt();
                    //                    calculateTotRequisitionAmt();
                    setTimeout(function () {
                        clearExpenseRowDetails(row);
                        sumAdjustAmt();
                        calculateTotRequisitionAmt();

                    }, 20);
                }
            },

            select: function (event, ui) {
                var row = $(this).parentsUntil($('#' + gridViewID2), 'tr.gridRow');
                //                                setExpenseRowDetails(row, ui.item);
                //                                sumAdjustAmt();
                //                                calculateDueAmt();
                //                                $(row).find('input[id$="txtAdjustAmt"]').focus();
                setTimeout(function () {
                    setExpenseRowDetails(row, ui.item);
                    sumAdjustAmt();
                    calculateTotRequisitionAmt();
                    $(row).find('input[id$="txtAdjustAmt"]').focus();

                }, 20);

            },

            l: 0
        });
*/

$.widget('custom.mcautocomplete', $.ui.autocomplete, {
    isHeaderFooterCreated: false,
    totalItems: 0,
    openTask: function () {
        var menuID = $(this.element).data("mcautocomplete").menu.element.attr('id');
        var self = this;
        totColumn = this.options.columns.length;
        totWidth = this.getTotWidth() + (totColumn * 4);

        elemPos = $(this.element).position();
        elemHeight = $(this.element).height();

        popTop = elemPos.top + elemHeight;
        popLeft = elemPos.left;

        menu = document.getElementById(menuID);
        popDiv = $(menu).parent();

        $(popDiv).css('top', popTop + 2 + 'px');
        $(popDiv).css('left', popLeft + 'px');
        $(popDiv).css('width', totWidth + 22 + 'px');
        $(popDiv).addClass('ui-corner-all').addClass('ui-widget-content');

        $(popDiv).show();



        menuHeight = this.options.listHeight;

        $(menu).css('position', 'relative');
        //$(popDiv).css('top', popTop + 20 + 'px');
        $(menu).css('top', '0px');
        $(menu).css('left', '0px');
        $(menu).width(totWidth + 'px');
        $(menu).height(menuHeight + 'px');

        $(menu).removeClass('ui-corner-all').removeClass('ui-widget-content');


        //            setTimeout(function () {
        //                            $(menu).focus();
        //                             }
        //                      , 20
        //            )

        //$(popDiv).focus();

        var x = 0;
        return true;
    },

    closeTask: function () {
        var menuID = $(this.element).data("mcautocomplete").menu.element.attr('id');
        var self = this;
        popDiv = document.getElementById(menuID);
        parentDiv = $(popDiv).parent();
        $(parentDiv).hide();
        $(parentDiv).css('top', '0px');
        //TabMenu.ResizeTabControlALL();
        return true;

    },

    getTotWidth: function () {
        totWidth = 0;
        $.each(this.options.columns, function (index, item) {
            totWidth += item.width;
        });
        return totWidth;
    },

    _renderMenu: function (ul, items) {
        var self = this;
        self.totalItems = items.length;
        totColumn = this.options.columns.length;
        totWidth = this.getTotWidth() + (totColumn * 4);

        totRec = items.length;
        if (items.length == 1) {
//            if (items[0].id == 0 && items[0].enable == false) {
//                totRec = 0;
//            }
            if (items[0].id == 0) {
                totRec = 0;
            }

        }
        else {

        }

        $.each(items, function (index, item) {

            if (item.id == 0 && item.enable == false) {
                //liItem.disableSelection();
                //liItem.addClass('ui-autocomplete-category');
                ul.append("<li class='ui-autocomplete-norecord'>" + '' + "</li>")
            }
            else {
                self._renderItem(ul, item);
            }
        });

        if (self.isHeaderFooterCreated == false) {
            headerHeight = this.options.showHeader ? 20 : 0;
            footerHeight = this.options.showFooter ? 20 : 0;

            popHeight = this.options.listHeight + headerHeight + footerHeight + 8;
            popWidth = totWidth + 20;

            popDiv = $('<div style="height:' + popHeight + 'px;width:' + popWidth + 'px;position:absolute;display:none;"></div>');
            //inDiv.append('<div style="height:25px;width:100%;background-color:red;"></div>');
            ul.wrapAll(popDiv);

            if (this.options.showHeader) {
                headerTable = $('<table cellspacing="0" cellpadding="0" style="width:' + totWidth + 'px;" ></table>');
                tRow = $('<tr></tr>');
                $.each(this.options.columns, function (index, item) {
                    colWidth = item.width + 2
                    tRow.append('<td style="padding:0 1px 0 1px;width:' + colWidth + 'px;text-align:' + item.align + '">' + item.name + '</td>');
                });
                headerTable.append(tRow);
                headerDiv = $('<div class="ui-widget-header macHeader" style="height:20px;width:' + popWidth + 'px;"></div>');
                headerDiv.append(headerTable);

                $(ul).parent().prepend(headerDiv);
            }

            if (this.options.showFooter) {
                footerDiv = $('<div class="ui-widget-header macFooter" style="height:20px;width:' + popWidth + 'px;"></div>');

                footerDiv.append('<span class="spnTotalRec" style="padding:0 2px;">Total: ' + '0' + ' record(s)</span>');
                $(ul).parent().append(footerDiv);

            }
            self.isHeaderFooterCreated = true;
        }


        $(ul).parent().find('.spnTotalRec').text('Total: ' + totRec + ' record(s)');

    },
    _renderItem: function (ul, item) {
        var t = '',
            result = '';
        dataTerm = this.term;

        totColumn = this.options.columns.length;
        totWidth = this.getTotWidth() + (totColumn * 4);
        table = $('<table cellspacing="0" cellpadding="0" style="table-layout:fixed;width:' + totWidth + 'px;"></table>');
        tRow = $('<tr></tr>');
        $.each(this.options.columns, function (index, column) {
            var strAlign = column.align;
            var colData = item[column.valueField ? column.valueField : index];

            if (index == 0) {
                colData = colData.replace(new RegExp("(?![^&;]+;)(?!<[^<>]*)(" + $.ui.autocomplete.escapeRegex(dataTerm) + ")(?![^<>]*>)(?![^&;]+;)", "gi"), "$1");
            }
            colWidth = column.width + 2;

            tRow.append('<td title="' + colData + '" style="overflow:hidden;text-overflow:ellipsis;white-space:nowrap;padding:0 1px 0 1px;max-width: ' + colWidth + 'px;width:' + colWidth + 'px;text-align:' + column.align + ';">' + colData + '</td>');
        });
        table.append(tRow);
        ancWidth = totWidth;
        anc = $('<a class="mcacAnchor" style="padding:0px 0px 0px 0px; width:' + ancWidth + 'px;"></a>');
        anc.append(table);

        liItem = $('<li></li>');
        liItem.data('item.autocomplete', item).append(anc).appendTo(ul);

        result = liItem;
        //result = $('<li></li>').data('item.autocomplete', item).append(anc).appendTo(ul);
        return result;
    }
});