<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="INVBin.aspx.cs" Inherits="PG.Web.Inventory.INVBin" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/form.css" rel="stylesheet" type="text/css" />
    <script src="javascript/jquery-latest.min.js" type="text/javascript"></script>
    <style type="text/css">
        .cmnTable {
            border-collapse: collapse;
            width: 101%;
        }

            .cmnTable th {
                text-align: left;
                padding: 6px;
            }

            .cmnTable tr:nth-child(even) {
                background-color: #f2f2f2;
            }

            .cmnTable th {
                background-color: #C3D8F2;
            }
            
        .has-warning {
            border: 1px solid red;
        }
    </style>

    <script type="text/javascript" language="javascript">


        //Report Part
     <%--   var ReportViewPageLink = '<%=this.ReportViewPageLink%>';
        var ReportViewPDFPageLink = '<%=this.ReportViewPDFPageLink%>';--%>


        function PageResizeCompleted(pg, cntMain) {
            resizeContentInner(cntMain);
        }

        function resizeContentInner(cntMain) {
            var contHeight = $("#dvContentMainInner").height();

            var topHeight = $("#dvTop").height();

            var middleHeight = contHeight - topHeight;

            $("#dvMiddle").height(middleHeight);
            $("#tblMiddle").height(middleHeight);

            $("#dvReportList").height(middleHeight);
            $("#dvParam").height(middleHeight);
        }

        function tbopen(key, isPrint, isPDFAutoPrint, showWait) {
            key = key || '';
            isPrint = isPrint || false;
            showWait = showWait || true;
            var now = new Date();
            var strTime = now.getTime().toString();
            var url = ReportViewPageLink + "?rk=" + key + "&_tt=" + strTime;

            if (TabVar.PageMode == Enums.PageMode.InTab) {

                var tdata = new xtabdata();
                tdata.linktype = Enums.LinkType.Direct;
                tdata.id = 7999;
                tdata.name = "Report view";
                //tdata.label = "User: " + userid;
                tdata.label = "Report view";
                tdata.type = 0;
                tdata.url = url;
                tdata.tabaction = Enums.TabAction.InNewTab;
                tdata.selecttab = 1;
                tdata.reload = 0;
                tdata.param = "";
                tdata.showWait = showWait;

                try {
                    //window.parent.OpenMenuByData(tdata);
                    window.parent.TabMenu.OpenMenuByData(tdata);
                }
                catch (err) {
                    alert("error in page");
                }
            }
            else {
                //on new window/tab
                //window.open(url,'_blank');   

                window.location = url;
            }
        }


        //Report End


        //Common Script
        function ValidateInput() {
            debugger;
            var isValid = true;
            $(".required").each(function () {
                if ($(this).val().length > 0) {
                    $(this).removeClass('has-warning');
                } else {
                    isValid = false;
                    $(this).addClass('has-warning');
                }
            })
            if (isValid) {
                return true;
            }
            return false;

        }

      

        function ResetAndSetMessage(controlName, value, color) {
            $('#' + lblMessage).text("");
            if (controlName == "S") {
                $('#' + lblMessage).text(value);
                $('#' + lblMessage).css('color', color);
            }
        }

        function ResetInput() {
            $('#' + txtBinName).val("");
            $('#' + taDescription).val("");
            $('#' + txtShortName).val("");
        }

        function ResetForm() {
            $('#tblItemList').find('tbody').empty();
            $('#tblItemList').hide();
        }

        $(document).on('click', '.deleteTempRow', function () {
            debugger;
            $(this).parent().parent().remove();
            var parentTableId = $(this).closest('table').attr('id');
            GenerateNewSlNo(parentTableId);
        });

        function GenerateNewSlNo(parentTableId) {
            var newSl = 1;
            $("#" + parentTableId + " .slno").each(function () {
                $(this).text(newSl);
                newSl = newSl + 1;
            });
        }

       

        //End of common Script


        var ddlRack = '<%=ddlRack.ClientID%>';
        var txtBinId = '<%=txtBinId.ClientID%>';
        var txtNumberOfBin = '<%=txtNumberOfBin.ClientID%>';
        var txtBinName = '<%=txtBinName.ClientID%>';
        var cbIsAuto = '<%=cbIsAuto.ClientID%>';
        var taDescription = '<%=taDescription.ClientID%>';
        var txtShortName = '<%=txtShortName.ClientID%>';
        var btnSave = '<%=btnSave.ClientID%>';
        var ddlStore = '<%=ddlStore.ClientID%>'; 
        var ddlRack = '<%=ddlRack.ClientID%>';   
        var btnRefresh = '<%=btnRefresh.ClientID%>';   
        var lblMessage = '<%=lblMessage.ClientID%>'; 

        var btnFind = '<%=btnFind.ClientID%>';
        var ddlStoreSrc = '<%=ddlStoreSrc.ClientID%>';
        var ddlRackSrc = '<%=ddlRackSrc.ClientID%>';
        var btnSearchRackByStore = '<%=btnSearchRackByStore.ClientID%>';
        
        
        

        
        
        


        $(document).on('click', '#btnAddNewItem', function () {

            if (ValidateInput()) {
                var binName = $('#' + txtBinName).val();
                var shortName = $('#' + txtShortName).val();
                var description = $('#' + taDescription).val();
                var rackName = $('#' + ddlRack + ' option:selected').text();
                var rackId = $('#' + ddlRack).val();
             
                var newtr = newtr + '<tr>' +
                                  '<td><span class="slno"></span></td>' +
                                      '<td><span id="lblBinName">' + binName + '</span></td>' +
                                       '<td><span id="lblShortName">' + shortName + '</span></td>' +
                                         '<td><input type="hidden" id="hdnRackId" value=' + rackId + ' /><span id="lblRakName">' + rackName + '</span></td>' +
                                       '<td><span id="lblDescription">' + description + '</span></td>' +
                                   '<td><button class="deleteTempRow"><img src="../image/btnDeleteIconHover.png" alt="Delete" /></button></td>' +
                                '<td></td>' +
                              '</tr>';
                $('#tblItemList').find('tbody').append(newtr);
                $('#tblItemList').show();
                GenerateNewSlNo("tblItemList");
                ResetInput();
            }
        });


        function LoadRackByStore() {
            debugger;
            var storeId = $('#' + ddlStore).val();
            $.ajax({
                type: "POST",
                url: "../Inventory/INVBin.aspx/LoadRackByStore",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                data: JSON.stringify({ storeId: storeId }),
                success: function (locdata) {
                    debugger;
                    if (locdata.d.length > 0) {                     
                        var option = "<option value=''>--select--</option>";
                        $.each(locdata.d, function (key, val) {
                            option = option + '<option value=' + val.RACK_ID + '>' + val.RACK_NAME + '</option>';
                          
                        });
                        $('#' + ddlRack).html(option);

                    } else {
                        $('#' + ddlRack).html('');
                                       
                    }

                },
                error: function (result) {
                    alert(result.responseText);
                }

            });
        }
        function LoadRackByStoreForSearch() {
            debugger;
            var storeId = $('#' + ddlStoreSrc).val();
            $.ajax({
                type: "POST",
                url: "../Inventory/INVBin.aspx/LoadRackByStore",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                data: JSON.stringify({ storeId: storeId }),
                success: function (locdata) {
                    debugger;
                    if (locdata.d.length > 0) {
                        var option = "<option value=''>--select--</option>";
                        $.each(locdata.d, function (key, val) {
                            option = option + '<option value=' + val.RACK_ID + '>' + val.RACK_NAME + '</option>';

                        });
                        $('#' + ddlRackSrc).html(option);

                    } else {
                        $('#' + ddlRackSrc).html('');

                    }

                },
                error: function (result) {
                    alert(result.responseText);
                }

            });
        }

        function Save() {
            debugger;

            $(".has-warning").removeClass("has-warning");
            ResetAndSetMessage("", "", "");
            var obj = new Object();
            var objList = new Array();
                  
            var tblItemList = $('#tblItemList').find('tbody').find('tr');
            for (var k = 0; k < tblItemList.length; k++) {
                debugger;
                var obj = new Object();              
                obj.BIN_NAME = $(tblItemList[k]).find('#lblBinName').text();
                obj.RACK_ID = $(tblItemList[k]).find('#hdnRackId').val();
                obj.SHORT_NAME = $(tblItemList[k]).find('#lblShortName').text();
                obj.DESCRIPTION = $(tblItemList[k]).find('#lblDescription').text();
                objList.push(obj);
            }

            if (objList.length > 0) {
                var y = confirm('Are you sure to save Bin?');
                if (y) {
                    $.ajax({
                        type: "POST",
                        url: "../Inventory/INVBin.aspx/Save",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        async: true,
                        data: JSON.stringify({ objList: objList }),
                        success: function (result) {                        
                         
                            ResetAndSetMessage("S", "Saved successfully.", "green");
                            ResetInput();                          
                            ResetForm();

                        },
                        error: function (result) {
                            alert(result.responseText);
                        }

                    });
                }

            } else {
                ResetAndSetMessage("S", "Please add bin first.", "red");
             
            }
        

        }

        function LoadBin() {
            $('#tblSearchedRackList').find('tbody').empty();
            var storeId = $('#' + ddlRackSrc).val();
            $.ajax({
                type: "POST",
                url: "../Inventory/INVBin.aspx/GetBinByrackId",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                data: JSON.stringify({ storeId: storeId }),
                success: function (locdata) {
                    if (locdata.d.length > 0) {
                        var sl = 1;
                        var tr = "";
                        $.each(locdata.d, function (key, val) {
                            tr = tr + '<tr>' +
                                '<td>' + sl + '</td>' +
                                 '<td><span id="lblRackId">' + val.BIN_ID + '</span></td>' +
                                  '<td><span id="lblRackname">' + val.BIN_NAME + '</span></td>' +
                                 '<td><span id="lblShortName">' + val.SHORT_NAME + '</span></td>' +
                                   //'<td><span id="lblStoreName">' + val.SHORT_NAME + '</span></td>' +
                                 '<td><span id="lblDescription">' + val.DESCRIPTION + '</span></td>' +

                              '<td></td>' +
                            '</tr>';
                            sl = sl + 1;
                        });
                        $('#tblSearchedRackList').find('tbody').append(tr);

                    } else {
                        alert('No Data Found');
                        // $('#divLoadGDCList').html('<h1>No Data Found.</h1>');
                    }

                },
                error: function (result) {
                    alert(result.responseText);
                }

            });
        }

        $(document).ready(function () {



            var divReplace = $("#divSearchRackListDialog").dialog({
                autoOpen: false,
                resizable: false,
                modal: true,
                closeOnEscape: true,
                width: 1050,
                height: 550
            });
            divReplace.parent().appendTo(jQuery("form:first"));


            $('#' + btnFind).click(function () {
                $("#divSearchRackListDialog").dialog("open");
            });




            $('#' + btnSave).click(function () {
                Save();
            });
            $("#" + btnRefresh).click(function (e) {               
                $(".has-warning").removeClass('has-warning');
                ResetInput();
                ResetForm();
            });

            $('#' + ddlStore).change(function () {
                debugger;


                LoadRackByStore();
            });


            $('#' + ddlStoreSrc).change(function () {
                debugger;
                LoadRackByStoreForSearch();
            });


            

            $('#' + btnSearchRackByStore).click(function () {
                LoadBin();
            });



            
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="min-height: 30px; width: 100%; text-align: center !important;" id="dvMessage">
        <span runat="server" id="lblMessage" style="font-weight: bold; text-align: center; font-size: 20px;"></span>
    </div>
    <div id="dvHeader" class="dvHeader" align="center" style="width: 100%; height: auto;">
        <asp:Label ID="lblHeader" runat="server" Font-Bold="True" Font-Size="Large">Bin Management(BM)</asp:Label>
    </div>
    <div id="dvContentMain" class="dvContentMain" style="height: 400px;">

        <div id="pLoading" style="display: none; margin: 0px; padding: 0px; position: fixed; right: 0px; top: 0px; width: 100%; height: 100%; z-index: 30001; opacity: 0.8;">
            <p style="position: absolute; color: White; top: 20%; left: 20%;">
                <img src="../image/loading.gif" alt="loading...">
            </p>
        </div>
        <table border="0" cellspacing="4" cellpadding="2" align="center" style="width: 100%">

            <tr>
                <td align="right" class="auto-style2">
                    <asp:Label ID="lblGCFLNO" runat="server" Text="Bin ID :" Font-Bold="true"></asp:Label>
                </td>
                <td align="left" class="auto-style3">
                    <asp:TextBox ID="txtBinId" runat="server" CssClass="textBox" ReadOnly="true" Font-Bold="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="" align="right">
                    <asp:Label ID="Label3" runat="server" Text="Store :" Font-Bold="True" Font-Size="Medium"></asp:Label><span style="color: red">*</span>
                </td>
                <td align="left" class="auto-style1">
                    <asp:DropDownList ID="ddlStore" runat="server" CssClass="dropDownList required">                      
                    </asp:DropDownList>

                </td>

            </tr>
            <tr>
                <td style="" align="right">
                    <asp:Label ID="SLNO" runat="server" Text="Rack :" Font-Bold="True" Font-Size="Medium"></asp:Label><span style="color: red">*</span>
                </td>
                <td align="left" class="auto-style1">
                    <select id="ddlRack" runat="server" class="dropDownList required"></select>
                </td>

            </tr>

            <tr>
                <td style="" align="right">
                    <asp:Label ID="lblBSRDATE" runat="server" Text="Bin Name :" Font-Bold="true"></asp:Label><span style="color: red">*</span>
                </td>
                <td align="left" class="auto-style1">
                    <asp:TextBox ID="txtBinName" runat="server" Font-Bold="true" CssClass="textBox required"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td style="" align="right">
                    <asp:Label ID="Label4" runat="server" Text="Short Name :" Font-Bold="true"></asp:Label><span style="color: red">*</span>
                </td>
                <td align="left" class="auto-style1">
                    <asp:TextBox ID="txtShortName" runat="server" Font-Bold="true" CssClass="textBox required"></asp:TextBox>
                </td>
            </tr>
            <tr style="display:none">
                <td style="" align="right">
                    <asp:Label ID="lblSALESDATESTATUS" runat="server" Text="Is Auto :"></asp:Label>
                </td>
                <td align="left" class="auto-style1">
                    <input type="checkbox" runat="server" id="cbIsAuto" />
                </td>
            </tr>
            <tr style="display:none">
                <td style="" align="right">
                    <asp:Label ID="Label1" runat="server" Text="Number Of Bin :"></asp:Label>
                </td>
                <td align="left" class="auto-style1">
                    <input type="text" class="textBox" runat="server" id="txtNumberOfBin" />
                </td>
            </tr>
            <tr>
                <td style="" align="right">
                    <asp:Label ID="Label2" runat="server" Text="Description :"></asp:Label>
                </td>
                <td align="left" class="auto-style1">
                    <textarea id="taDescription" runat="server" rows="2" cols="30"></textarea>
                </td>
            </tr>
            <tr>
                <td style="" align="right"></td>
                <td align="left" class="auto-style1">
                    <input type="button" class="buttoncommon" value="Add" id="btnAddNewItem" />
                </td>
            </tr>
        </table>
        <table border="0" class="cmnTable" id="tblItemList" align="center" style="width: 100% ;display: none">
            <thead>
                <tr>
                    <th>SL.</th>
                    <th>Bin Name</th>
                    <th>Short Name</th>
                    <th>Rack Name</th>
                    <th>Description</th>
                    <th>Action</th>
                </tr>
            </thead>
            <tbody>
            </tbody>
        </table>
    </div>


    <div id="divSearchRackListDialog" style="display:none">
        <table style="width:100%">
            <thead>
                <tr>
                    <th></th>
                     <th>Store</th>
                     <th><asp:DropDownList ID="ddlStoreSrc" runat="server" CssClass="dropDownList"></asp:DropDownList></th>       
                     <th>Rack</th>
                     <th><asp:DropDownList ID="ddlRackSrc" runat="server" CssClass="dropDownList"></asp:DropDownList></th>             
                    <th><input id="btnSearchRackByStore" type="button" runat="server" class="buttonSearch" value="Search" /></th>                   
                </tr>
            </thead>
        </table>
        <table border="0" class="cmnTable" id="tblSearchedRackList" align="center" style="width: 100%;">
            <thead>
                <tr>
                    <th>SL.</th>
                     <th>Bin ID</th>
                    <th>Bin Name</th>
                    <th>Short Name</th>                    
                    <th>Description</th>                
                </tr>
            </thead>
            <tbody>
            </tbody>
        </table>
    </div>

    <div id="dvContentFooter" class="dvContentFooter" align="center">
        <table>
            <tr>

                <td>
                    <input id="btnSave" type="button" runat="server" class="buttonSave" value="Save" />

                    <input id="btnEdit" type="button" runat="server" class="buttonEdit" value="Edit" style="display: none" />

                </td>
                <td>
                    <input type="button" id="btnFind" runat="server" class="buttonSearch" value="Find" />
                </td>


                <td>

                    <input type="button" runat="server" id="btnRefresh" class="buttonRefresh" value="Reset" />

                </td>
                <td>
                    <input id="btnPrintBSR" type="button" class="buttonPrint" style="text-align: right; width: 100px !important;display:none" value="Print" onclick="CheckBsrNumber()" />

                </td>

                <td>
                    <input type="button" id="btnClose" class="buttonClose" value="Close" onclick="if (ContentForm) { ContentForm.CloseForm(); }" />


                </td>
            </tr>
        </table>
    </div>
</asp:Content>
