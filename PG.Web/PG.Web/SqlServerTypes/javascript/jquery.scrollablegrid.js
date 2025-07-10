/// <reference path="jsutility.js" />
/// <reference path="jquery-latest.min.js" />
/// <reference path="jquery-ui-latest.min.js" />


(function($) {
    $.fn.Scrollable = function(options) {
        var defaults = {
            ScrollHeight: 0,
            GridHeight: 0,
            GridWidth: 0,
            ColWidths: null
        };
        var options = $.extend(defaults, options);
        var self = this;
        return this.each(function() {

            var isHeader = false;
            var isFooter = false;
            var isPager = false;

            var headerHeight = 0;
            var footerHeight = 0;
            var pagerHeight = 0;

            var grid = $(this).get(0);
            //var grid = $(this).find('.grid');
            //var grid = self;
            if ($(grid).find('.gridRow').length == 0) {
                return;
            }

            if ($(grid).find('.headerRow').length > 0) {
                isHeader = true;
                headerHeight = $(grid).find('.headerRow').height();
            }

            //isHeader = $(grid).find('.headerRow').length > 0;
            //isFooter = $(grid).find('.footerRow').length > 0;
            //isPager = $(grid).find('.pagerRow').length > 0;

            if ($(grid).find('.footerRow').length > 0) {
                isFooter = true;
                footerHeight = $(grid).find('.footerRow').height();
            }


            if ($(grid).find('.pagerRow').length > 0) {
                isPager = true;
                pagerHeight = $(grid).find('.pagerRow').height();
            }


            gridAttributes = grid.attributes;

            var gridParent = $(grid).parent();

            //gridWidth = $(grid).width();
            parentWidth = $(gridParent).width();

            var gridWidth = parentWidth - 17;
            var curGridWidth = $(grid).width();
            var gridContWidth = curGridWidth + 17;

            //$(grid).parent.width(gridContWidth);
            
            $(grid).parent().parent().height('100%');
            $(grid).parent().parent().width(gridContWidth);
            $(grid).parent().height('100%');
            $(grid).parent().width('100%');



            var gridHeight = $(grid).height();
            var newScrollHeight = defaults.ScrollHeight;
            if (defaults.GridHeight > 0) {
                pHeight = $(grid).parent().height();
                //newScrollHeight = pHeight - headerHeight - footerHeight - pagerHeight;
                newScrollHeight = defaults.GridHeight - headerHeight - footerHeight - pagerHeight;
                //$(grid).parent().parent().height(defaults.GridHeight);
                // $(grid).parent().height(defaults.GridHeight);
            }


            var cellWidths = new Array();

            if (defaults.ColWidths == null) {
                $(grid).find('.gridRow:first TD').each(function(index, domEle) {
                    //$(domEle).width($(domEle).width());
                    cellWidths[index] = $(domEle).width();

                    //cellWidths[index] = domEle.style.width;
                });
            }
            else {
                cellWidths = defaults.ColWidths;
            }

            if (isHeader) {
                //var headerCellWidths = new Array();

                $(grid).find('.headerRow:first TH').each(function(index, domEle) {
                    //$(domEle).width($(domEle).width());
                    $(domEle).width(cellWidths[index]);

                });

                var hDiv = document.createElement("div");
                $(hDiv).appendTo(gridParent); //$(gridParent).add('div');
                //$(hDiv).width(gridWidth); gridContWidth
                $(hDiv).width(gridContWidth);
                $(hDiv).height('auto');

                var hTable = document.createElement("table");

                for (i = 0; i < gridAttributes.length; i++) {
                    if (gridAttributes[i].specified && gridAttributes[i].name != "id") {
                        $(hTable).attr(gridAttributes[i].name, gridAttributes[i].value);
                    }
                }
                //$(hTable).get(0).style.cssText = grid.style.cssText;
                $(hTable).prependTo(hDiv);
                $(hTable).removeClass('grid');
                //$(hTable).width(gridWidth);

                var hBody = document.createElement("tbody");
                $(hBody).appendTo(hTable);
                $(grid).find('.headerRow').appendTo($(hBody));



            }

            //now data row
            var rDiv = document.createElement("div");
            //$(rDiv).width(gridWidth + 17);
            $(rDiv).width(gridContWidth);
            //$(rDiv).height(defaults.ScrollHeight);
            $(rDiv).height(newScrollHeight);
            $(rDiv).css('overflow', 'auto');
            $(rDiv).appendTo(gridParent);
            $(grid).appendTo($(rDiv));
            $(grid).width('');
            //$(grid).width(gridWidth);
            //$(grid).get(0).style.cssText = grid.style.cssText;

            ///now set grid colwidth
            $(grid).find('.gridRow:first TR').each(function(index, domEle) {
                //$(domEle).width($(domEle).width());
                $(domEle).width(cellWidths[index]);

            });



            if (isFooter) {
                var fDiv = document.createElement("div");
                $(fDiv).appendTo(gridParent);
                //$(fDiv).width(gridWidth);
                $(fDiv).width(gridContWidth);

                $(fDiv).height('auto');
                var fTable = document.createElement("table");


                for (i = 0; i < gridAttributes.length; i++) {
                    if (gridAttributes[i].specified && gridAttributes[i].name != "id") {
                        $(fTable).attr(gridAttributes[i].name, gridAttributes[i].value);
                    }
                }
                // $(fTable).get(0).style.cssText = grid.style.cssText;
                $(fTable).appendTo(fDiv);
                $(fTable).removeClass('grid');
                //$(fTable).width(gridWidth);
                $(fTable).width('');

                var fBody = document.createElement("tbody");
                $(fBody).appendTo(fTable);
                $(grid).find('.footerRow').appendTo($(fBody));



            }
            if (isPager) {
                var pDiv = document.createElement("div");
                $(pDiv).appendTo(gridParent);
                //$(pDiv).width(gridWidth); 
                $(pDiv).width(gridContWidth);
                $(fDiv).height('auto');
                var pTable = document.createElement("table");
                $(pTable).appendTo(pDiv);
                for (i = 0; i < gridAttributes.length; i++) {
                    if (gridAttributes[i].specified && gridAttributes[i].name != "id") {
                        $(pTable).attr(gridAttributes[i].name, gridAttributes[i].value);
                    }
                }
                //$(pTable).get(0).style.cssText = grid.style.cssText;
                $(pTable).removeClass('grid');
                //$(pTable).width(gridWidth);
                $(pTable).width(curGridWidth);
                var pBody = document.createElement("tbody");
                $(pBody).appendTo(pTable);
                $(grid).find('.pagerRow').appendTo($(pBody));

            }



            //$(gridParent).append($(grid));
            //$(grid).parent().append($(grid));
            //$(hTable).add("body");




            //            grid.parentNode.appendChild(document.createElement("div"));
            //            var parentDiv = grid.parentNode;

            //            var table = document.createElement("table");
            //            for (i = 0; i < grid.attributes.length; i++) {
            //                if (grid.attributes[i].specified && grid.attributes[i].name != "id") {
            //                    table.setAttribute(grid.attributes[i].name, grid.attributes[i].value);
            //                }
            //            }
            //            table.style.cssText = grid.style.cssText;
            //            table.style.width = gridWidth + "px";
            //            table.appendChild(document.createElement("tbody"));
            //            
            //            table.getElementsByTagName("tbody")[0].appendChild(grid.getElementsByTagName("TR")[0]);
            //            
            //            var cells = table.getElementsByTagName("TH");

            //            var gridRow = grid.getElementsByTagName("TR")[0];
            //            for (var i = 0; i < cells.length; i++) {
            //                var width;
            //                if (headerCellWidths[i] > gridRow.getElementsByTagName("TD")[i].offsetWidth) {
            //                    width = headerCellWidths[i];
            //                }
            //                else {
            //                    width = gridRow.getElementsByTagName("TD")[i].offsetWidth;
            //                }
            //                cells[i].style.width = parseInt(width - 3) + "px";
            //                gridRow.getElementsByTagName("TD")[i].style.width = parseInt(width - 3) + "px";
            //            }
            //            parentDiv.removeChild(grid);


            //            var dummyHeader = document.createElement("div");
            //            dummyHeader.appendChild(table);
            //            parentDiv.appendChild(dummyHeader);
            //            if (options.Width > 0) {
            //                gridWidth = options.Width;
            //            }
            //            var scrollableDiv = document.createElement("div");
            //            gridWidth = parseInt(gridWidth) + 17;
            //            scrollableDiv.style.cssText = "overflow:auto;height:" + options.ScrollHeight + "px;width:" + gridWidth + "px";
            //            scrollableDiv.appendChild(grid);
            //            parentDiv.appendChild(scrollableDiv);
        });
    };
})(jQuery);


//(function($) {
//    $.fn.Scrollable = function(options) {
//        var defaults = {
//            ScrollHeight: 300,
//            Width: 0
//        };
//        var options = $.extend(defaults, options);
//        return this.each(function() {
//            var grid = $(this).get(0);
//            var gridWidth = grid.offsetWidth;
//            var headerCellWidths = new Array();
//            for (var i = 0; i < grid.getElementsByTagName("TH").length; i++) {
//                headerCellWidths[i] = grid.getElementsByTagName("TH")[i].offsetWidth;
//            }
//            grid.parentNode.appendChild(document.createElement("div"));
//            var parentDiv = grid.parentNode;

//            var table = document.createElement("table");
//            for (i = 0; i < grid.attributes.length; i++) {
//                if (grid.attributes[i].specified && grid.attributes[i].name != "id") {
//                    table.setAttribute(grid.attributes[i].name, grid.attributes[i].value);
//                }
//            }
//            table.style.cssText = grid.style.cssText;
//            table.style.width = gridWidth + "px";
//            table.appendChild(document.createElement("tbody"));
//            table.getElementsByTagName("tbody")[0].appendChild(grid.getElementsByTagName("TR")[0]);
//            var cells = table.getElementsByTagName("TH");

//            var gridRow = grid.getElementsByTagName("TR")[0];
//            for (var i = 0; i < cells.length; i++) {
//                var width;
//                if (headerCellWidths[i] > gridRow.getElementsByTagName("TD")[i].offsetWidth) {
//                    width = headerCellWidths[i];
//                }
//                else {
//                    width = gridRow.getElementsByTagName("TD")[i].offsetWidth;
//                }
//                cells[i].style.width = parseInt(width - 3) + "px";
//                gridRow.getElementsByTagName("TD")[i].style.width = parseInt(width - 3) + "px";
//            }
//            parentDiv.removeChild(grid);

//            var dummyHeader = document.createElement("div");
//            dummyHeader.appendChild(table);
//            parentDiv.appendChild(dummyHeader);
//            if (options.Width > 0) {
//                gridWidth = options.Width;
//            }
//            var scrollableDiv = document.createElement("div");
//            gridWidth = parseInt(gridWidth) + 17;
//            scrollableDiv.style.cssText = "overflow:auto;height:" + options.ScrollHeight + "px;width:" + gridWidth + "px";
//            scrollableDiv.appendChild(grid);
//            parentDiv.appendChild(scrollableDiv);
//        });
//    };
//})(jQuery);






