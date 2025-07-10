<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="INVRack.aspx.cs" Inherits="PG.Web.Inventory.INVRack" %>
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
        var ReportViewPageLink = '<%=this.ReportViewPageLink%>';
        var ReportViewPDFPageLink = '<%=this.ReportViewPDFPageLink%>';


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
            $('#' + txtDisplayName).val("");
            $('#' + taRemarks).val("");
            $('#' + txtShortName).val("");
        }

        function ResetForm() {
            $('#tblRackList').find('tbody').empty();

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

        function ResetRack() {
            $(".has-warning").removeClass("has-warning");
            $("input[type='text']").val("");
            $('#' + taRemarks).val("");
            $('#tblRackList').find('tbody').empty();
            $('#tblRackList').hide();
            $('#' + lblMessage).text('');
            $('#' + btnSave).val("Save");

        }

        //End of common Script

        var lblMessage = '<%=lblMessage.ClientID%>';

        var ddlStore = '<%=ddlStore.ClientID%>';
        var txtRackId = '<%=txtRackId.ClientID%>';
        var txtNumberOfRack = '<%=txtNumberOfRack.ClientID%>';
        var txtDisplayName = '<%=txtDisplayName.ClientID%>';
        var cbIsAuto = '<%=cbIsAuto.ClientID%>';
        var taRemarks = '<%=taRemarks.ClientID%>';
        var txtShortName = '<%=txtShortName.ClientID%>';
        var btnSave = '<%=btnSave.ClientID%>';
        var btnRefresh = '<%=btnRefresh.ClientID%>';
        var btnFind = '<%=btnFind.ClientID%>';   
        var ddlStoreSrc = '<%=ddlStoreSrc.ClientID%>';       
        var ddlStore = '<%=ddlStore.ClientID%>';
        var btnSearchRackByStore = '<%=btnSearchRackByStore.ClientID%>';      
        
        $(document).on('click', '#btnAddRack', function () {

            if (ValidateInput()) {
                var rackName = $('#' + txtDisplayName).val();
                var storeName = $('#' + ddlStore + ' option:selected').text();
                var storeId = $('#' + ddlStore).val();
                var description = $('#' + taRemarks).val();
                var shortName = $('#' + txtShortName).val();
                var newtr = newtr + '<tr>' +
                                  '<td><span class="slno"></span></td>' +
                                   
                                      '<td><span id="lblRackName">' + rackName + '</span></td>' +
                                         '<td><span id="lblShortName">' + shortName + '</span></td>' +
                                       '<td><input type="hidden" id="hdnStoreId" value=' + storeId + ' /><span id="lblShortName">' + storeName + '</span></td>' +
                                       '<td><span id="lblDescription">' + description + '</span></td>' +
                                   '<td><button class="deleteTempRow"><img src="../image/btnDeleteIconHover.png" alt="Delete" /></button></td>' +
                                '<td></td>' +
                              '</tr>';

                $('#tblRackList').find('tbody').append(newtr);
                $('#tblRackList').show();
                GenerateNewSlNo("tblRackList");
                ResetInput();
            }



        });

        function Save() {
            $(".has-warning").removeClass("has-warning");
            ResetAndSetMessage("", "", "");
            var obj = new Object();
            var objList = new Array();
            //if (ValidateInput()) {             

            //cObjSave.AUTHO_STATUS = $('#' + chkBsrAuthorized).is(':checked') ? "A" : "U";
            //var y = false;
            //if (cObjSave.AUTHO_STATUS == "A") {
            //    y = confirm('Are you sure to save and Authorize BSR?');
            //} else {
            //    y = confirm('Are you sure to save BSR?');
            //}

            //debugger;
            //obj.RACK_NAME = $('#' + txtBSRDATE).val();
            //obj.SHORT_NAME = $('#' + txtGCNO).val();
            //obj.RACK_ID = $('#' + ddlSALESDATESTATUS).val();
            //obj.DESCRIPTION = $('#' + ddlREGULARSERVICE).val(); //base.LoginUserID.ToString();

            var tblItemList = $('#tblRackList').find('tbody').find('tr');
            for (var k = 0; k < tblItemList.length; k++) {
                debugger;
                var obj = new Object();
                // obj = new Object();
                obj.RACK_NAME = $(tblItemList[k]).find('#lblRackName').text();
                obj.SHORT_NAME = $(tblItemList[k]).find('#lblShortName').text();
                obj.DESCRIPTION = $(tblItemList[k]).find('#lblDescription').text();
                obj.STORE_ID = $(tblItemList[k]).find('#hdnStoreId').val();
                objList.push(obj);
            }

            if (objList.length > 0) {
                var y = confirm('Are you sure to save Rack?');
                if (y) {
                    $.ajax({
                        type: "POST",
                        url: "../Inventory/INVRack.aspx/Save",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        async: true,
                        data: JSON.stringify({ objList: objList }),
                        success: function (result) {
                            // alert('Saved failed.Something wrong input.');
                            //var newSysBsr = result.d;
                            //debugger;
                            //if (newSysBsr.length > 0) {
                            ResetRack();
                            ResetAndSetMessage("S", "Saved successfully.", "green");

                            //} else {
                            //    alert('Saved failed.Something wrong input.');
                            //}
                            ResetForm();

                        },
                        error: function (result) {
                            alert(result.responseText);
                        }

                    });
                }

            } else {
                ResetAndSetMessage("S", "Please add rack first.", "red");
            }



            //} else {
            //    ResetAndSetMessage("S", "Please fillup all mandatory field", "red");
            //}

        }

        function LoadStore()
        {
            var storeId = $('#' + ddlStoreSrc).val();
            $.ajax({
                type: "POST",
                url: "../Inventory/INVRack.aspx/GetRackByStoreId",
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
                                 '<td><span id="lblRackId">' + val.RACK_NAME + '</span></td>' +
                                  '<td><span id="lblRackname">' + val.RACK_NAME + '</span></td>' +
                                 '<td><span id="lblShortName">' + val.SHORT_NAME + '</span></td>' +
                                   '<td><span id="lblStoreName">' + val.SHORT_NAME + '</span></td>' +
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
            $('#' + btnSave).click(function () {
                Save();
            });
            $("#" + btnRefresh).click(function (e) {
                $("#" + btnSave).val('Save');
                $("#" + btnSave).prop('disabled', false);
                $(".has-warning").removeClass('has-warning');
                ResetRack();
            });

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

            $('#' + btnSearchRackByStore).click(function () {
                LoadStore();
            });
            

            
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="min-height: 30px; width: 100%; text-align: center !important;" id="dvMessage">
        <span runat="server" id="lblMessage" style="font-weight: bold; text-align: center; font-size: 20px;"></span>
    </div>
    <div id="dvHeader" class="dvHeader" align="center" style="width: 100%; height: auto;">
        <asp:Label ID="lblHeader" runat="server" Font-Bold="True" Font-Size="Large">Rack Management(RM)</asp:Label>
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
                    <asp:Label ID="lblGCFLNO" runat="server" Text="Rack ID :" Font-Bold="true"></asp:Label>
                </td>
                <td align="left" class="auto-style3">
                    <asp:TextBox ID="txtRackId" runat="server" CssClass="textBox" ReadOnly="true" Font-Bold="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="" align="right">
                    <asp:Label ID="SLNO" runat="server" Text="Store :" Font-Bold="True" Font-Size="Medium"></asp:Label><span style="color: red">*</span>
                </td>
                <td align="left" class="auto-style1">
                    <asp:DropDownList ID="ddlStore" runat="server" CssClass="dropDownList required">
                        <asp:ListItem Selected="True" Text="Store1" Value="M"></asp:ListItem>
                        <asp:ListItem Text="Store2" Value="B"></asp:ListItem>
                        <asp:ListItem Text="Store3" Value="T"></asp:ListItem>
                    </asp:DropDownList>

                </td>

            </tr>


            <tr>
                <td style="" align="right">
                    <asp:Label ID="lblBSRDATE" runat="server" Text="Rack Name :" Font-Bold="true"></asp:Label><span style="color: red">*</span>
                </td>
                <td align="left" class="auto-style1">
                    <asp:TextBox ID="txtDisplayName" runat="server" Font-Bold="true" CssClass="textBox required"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td style="" align="right">
                    <asp:Label ID="Label3" runat="server" Text="Short Name :" Font-Bold="true"></asp:Label><span style="color: red">*</span>
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
                    <asp:Label ID="Label1" runat="server" Text="Number Of Rack :"></asp:Label>
                </td>
                <td align="left" class="auto-style1">
                    <input type="text" runat="server" class="textBox" id="txtNumberOfRack" />
                </td>
            </tr>
            <tr>
                <td style="" align="right">
                    <asp:Label ID="Label2" runat="server" Text="Description :"></asp:Label>
                </td>
                <td align="left" class="auto-style1">
                    <textarea id="taRemarks" runat="server" rows="2" cols="30"></textarea>
                </td>
            </tr>
            <tr>
                <td style="" align="right"></td>
                <td align="left" class="auto-style1">
                    <input type="button" class="buttoncommon" value="Add" id="btnAddRack" />
                </td>
            </tr>
        </table>
        <table border="0" class="cmnTable" id="tblRackList" align="center" style="width: 100%; display: none">
            <thead>
                <tr>
                    <th>SL.</th>
                    <th>Rack Name</th>
                    <th>Short Name</th>
                    <th>Store Name</th>
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
                    <th><input id="btnSearchRackByStore" type="button" runat="server" class="buttonSearch" value="Search" /></th>                   
                </tr>
            </thead>
        </table>
        <table border="0" class="cmnTable" id="tblSearchedRackList" align="center" style="width: 100%;">
            <thead>
                <tr>
                    <th>SL.</th>
                     <th>Rack ID</th>
                    <th>Rack Name</th>
                    <th>Short Name</th>
                    <th>Store Name</th>
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
                    <input type="button" id="btnFind" class="buttonSearch" runat="server" value="Find" />
                </td>


                <td>

                    <input type="button" runat="server" id="btnRefresh" class="buttonRefresh" value="Reset" />

                </td>
                <td>
                    <input id="btnPrintBSR" type="button"  class="buttonPrint" style="text-align: right; width: 100px !important;display:none" value="Print" onclick="CheckBsrNumber()" />

                </td>

                <td>
                    <input type="button" id="btnClose"  class="buttonClose" value="Close" onclick="if (ContentForm) { ContentForm.CloseForm(); }" />


                </td>
            </tr>
        </table>
    </div>
</asp:Content>
