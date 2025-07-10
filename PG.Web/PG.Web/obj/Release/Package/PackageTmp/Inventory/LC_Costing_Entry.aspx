<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="LC_Costing_Entry.aspx.cs" Inherits="PG.Web.Inventory.LC_Costing_Entry" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
    <script src="../javascript/jquery.attributeobserver.js" type="text/javascript"></script>
    <link href="../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">

        var ItemListServiceLink = '<%=this.ItemListServiceLink%>';
        var LCListServiceLink = '<%= this.LCListServiceLink %>';
        var GetImpPOList = '<%= this.GetImpPOList %>';
        
        var ddlDeptID = '<%=ddlDeptID.ClientID%>';
        var txtItemName = '<%=txtItemName.ClientID%>';
        var btnItemID = '<%= btnItemID.ClientID%>';
        var hdnItemID = '<%=hdnItemID.ClientID %>';
        var hdnDeptID = '<%=hdnDeptID.ClientID %>';
        var txtLcNo = '<%=txtLcNo.ClientID%>';
        var btnLcNo = '<%=btnLcNo.ClientID%>';
        var hdnLcId = '<%=hdnLcId.ClientID%>';
        var btnFixedCharge = '<%=btnFixedCharge.ClientID%>';
        var lblItemCount = '<%=lblItemCount.ClientID%>';
        var lbltotType = '<%=lbltotType.ClientID%>';
        
        var btnSave = '<%=btnSave.ClientID%>';
        var txtItemQty = '<%=txtItemQty.ClientID%>';
        var txtUom = '<%=txtUom.ClientID%>';
        var txtConvertedItemQty = '<%=txtConvertedItemQty.ClientID%>';
        var txtConvertedUom = '<%=txtConvertedUom.ClientID%>';
        var txtItemQtyAct = '<%=txtItemQtyAct.ClientID%>';
        var txtUomAct = '<%=txtUomAct.ClientID%>';
        var txtConvertedItemQtyAct = '<%=txtConvertedItemQtyAct.ClientID%>';
        var txtConvertedUomAct = '<%=txtConvertedUomAct.ClientID%>';
        
        var SupplierListServiceLink = '<%=this.SupplierListServiceLink%>';
        var txtSupplierName = '<%=txtSupplierName.ClientID%>';
        var btnSupplierID = '<%=btnSupplierID.ClientID%>';
        var hdnSupplierID = '<%=hdnSupplierID.ClientID%>';
        var hdnTotLCValueUSD = '<%=hdnTotLCValueUSD.ClientID%>';
        
        var txtPoNo = '<%=txtPoNo.ClientID%>';
        var btnPoNo = '<%=btnPoNo.ClientID%>';
        var hdnIMP_PURCHASE_ID = '<%=hdnIMP_PURCHASE_ID.ClientID%>';
        
       <%-- var txtGridWeightPanel = '<%=txtGridWeightPanel.ClientID%>';--%>

        function isNumberKey(evt, obj) {

            var charCode = (evt.which) ? evt.which : event.keyCode
            var value = obj.value;
            var dotcontains = value.indexOf(".") != -1;
            if (dotcontains)
                if (charCode == 46) return false;
            if (charCode == 46 || charCode == 44) return true;
            if (charCode > 31 && (charCode < 48 || charCode > 57 ))
                return false;
            return true;
        }

        $(document).ready(function () {


            if ($('#' + txtItemName).is(':visible')) {

                bindItemList();

            }

            if ($('#' + txtLcNo).is(':visible')) {

                bindLCList();

            }

            if ($('#' + txtSupplierName).is(':visible')) {

                bindSupplierList();

            }

            $("#" + btnSave).click(function (e) {

                var rValue = confirm("Are you sure to save?");
                return rValue;
            });

            if ($('#' + txtPoNo).is(':visible')) {

                bindIMPOList();

            }


        });

        function bindSupplierList() {

            var cgColumns = [
                              { 'columnName': 'supname', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                             , { 'columnName': 'supcode', 'width': '80', 'align': 'left', 'highlight': 2, 'label': 'Code' }
                             , { 'columnName': 'supaddress', 'width': '200', 'align': 'left', 'highlight': 4, 'label': 'Address' }
                             , { 'columnName': 'supphone', 'width': '80', 'align': 'left', 'highlight': 0, 'label': 'phone' }
            ];

            var serviceURL = SupplierListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            //serviceURL += "&companyid=" + companyid;
            serviceURL += "&ispaging=1&suptype=F";

            var supplierIDElem = $('#' + txtSupplierName);

            $('#' + btnSupplierID).click(function (e) {
                $(supplierIDElem).combogrid("dropdownClick");
            });

            $(supplierIDElem).combogrid({
                debug: true,
                searchButton: false,
                resetButton: false,
                alternate: true,
                munit: 'px',
                scrollBar: true,
                showPager: true,
                colModel: cgColumns,
                autoFocus: true,
                showError: true,
                width: 600,
                url: serviceURL,
                search: function (event, ui) {
                    var newServiceURL = serviceURL;
                    $(this).combogrid("option", "url", newServiceURL);
                },
                select: function (event, ui) {
                    if (!ui.item) {
                        event.preventDefault();

                        // $('#' + hdnDealerID).val('0');
                        $('#' + txtDealerID).val('');
                        return false;
                    }


                    if (ui.item.dealerid == '') {
                        event.preventDefault();
                        return false;
                    }
                    else {
                        $('#' + hdnSupplierID).val(ui.item.supid);
                        $('#' + txtSupplierName).val(ui.item.supname);


                    }
                    return false;
                },

                lc: ''
            });


            $(supplierIDElem).blur(function () {
                var self = this;

                var customerID = $(supplierIDElem).val();
                if (customerID == '') {
                     $('#' + hdnSupplierID).val('0');
                    $('#' + txtSupplierName).val('');

                }
            });
        }

        function bindLCList() {

            var cgColumns = [
             
                            { 'columnName': 'lcno', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'LC No' }
                            , { 'columnName': 'lcdate', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'LC Date' }
                            , { 'columnName': 'supname', 'width': '200', 'align': 'left', 'highlight': 4, 'label': 'Supplier' }

            ];

            var itemServiceURL = LCListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;

            itemServiceURL += "&ispaging=1";
            var itemIDElem = $('#' + txtLcNo);

            $('#' + btnLcNo).click(function (e) {
                $(itemIDElem).combogrid("dropdownClick");
            });

            $(itemIDElem).combogrid({
                debug: true,
                searchButton: false,
                resetButton: false,
                alternate: true,
                munit: 'px',
                scrollBar: true,
                showPager: true,
                colModel: cgColumns,
                autoFocus: true,
                showError: true,
                width: 500,
                url: itemServiceURL,
                search: function (event, ui) {

                    //var vdeptid = $('#' + hdnDeptID).val();
                    var supid = $('#' + hdnSupplierID).val();
                    var newServiceURL = itemServiceURL + "&supid=" + supid;//+ "&deptid=" + vdeptid;
                    $(this).combogrid("option", "url", newServiceURL);


                },
                select: function (event, ui) {
                    if (!ui.item) {
                        event.preventDefault();


                        $('#' + hdnLcId).val('0');
                        return false;
                        //ClearGLAccountData(elemID);
                    }


                    if (ui.item.itemid == '') {
                        event.preventDefault();
                        return false;
                        //ClearGLAccountData(elemID);
                    }
                    else {

                        $('#' + hdnLcId).val(ui.item.lcid);
                        $('#' + txtLcNo).val(ui.item.lcno);
                        //$('#' + lbltotType).val(ui.item.itemcount);
                        //$('#' + hdnTotLCValueUSD).val(ui.item.lctotvalueusd);
                        
                        
                    }
                    return false;
                },

                lc: ''
            });

            $(itemIDElem).blur(function () {
                var self = this;
                var groupID = $(itemIDElem).val();
                if (groupID == '') {
                    // $('#' + hdnDealerID).val('0');
                    $('#' + txtLcNo).val('');
                    //$('#' + txtGroupCode).val('');
                    $('#' + hdnLcId).val('0');
                    //$('#' + lbltotType).val('0');
                    //$('#' + hdnItemID).val('0');
                    //$('#' + txtItemName).val('');
                    //$('#' + hdnTotLCValueUSD).val('0');
                    //SetDefaultValue();

                }
            });
        }

        function bindIMPOList() {

            var cgColumns = [

                            { 'columnName': 'purno', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'PO No' }
                            , { 'columnName': 'purdate', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Pur Date' }
                            

            ];

            var itemServiceURL = GetImpPOList + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;

            itemServiceURL += "&ispaging=1";
            var itemIDElem = $('#' + txtPoNo);

            $('#' + btnPoNo).click(function (e) {
                $(itemIDElem).combogrid("dropdownClick");
            });

            $(itemIDElem).combogrid({
                debug: true,
                searchButton: false,
                resetButton: false,
                alternate: true,
                munit: 'px',
                scrollBar: true,
                showPager: true,
                colModel: cgColumns,
                autoFocus: true,
                showError: true,
                width: 500,
                url: itemServiceURL,
                search: function (event, ui) {

                    //var vdeptid = $('#' + hdnDeptID).val();
                    var lcid = $('#' + hdnLcId).val();
                    var newServiceURL = itemServiceURL + "&lcid=" + lcid;//+ "&deptid=" + vdeptid;
                    $(this).combogrid("option", "url", newServiceURL);


                },
                select: function (event, ui) {
                    if (!ui.item) {
                        event.preventDefault();


                        $('#' + hdnIMP_PURCHASE_ID).val('0');
                        return false;
                        //ClearGLAccountData(elemID);
                    }


                    if (ui.item.purid == '') {
                        event.preventDefault();
                        return false;
                        //ClearGLAccountData(elemID);
                    }
                    else {

                        $('#' + hdnIMP_PURCHASE_ID).val(ui.item.purid);
                        $('#' + txtPoNo).val(ui.item.purno);
                        $('#' + lbltotType).val(ui.item.itemcount);
                        $('#' + hdnTotLCValueUSD).val(ui.item.lctotvalueusd);


                    }
                    return false;
                },

                lc: ''
            });

            $(itemIDElem).blur(function () {
                var self = this;
                var groupID = $(itemIDElem).val();
                if (groupID == '') {
                    // $('#' + hdnDealerID).val('0');
                    $('#' + txtPoNo).val('');
                    //$('#' + txtGroupCode).val('');
                    $('#' + hdnIMP_PURCHASE_ID).val('0');
                    $('#' + lbltotType).val('0');
                    $('#' + hdnItemID).val('0');
                    $('#' + txtItemName).val('');
                    $('#' + hdnTotLCValueUSD).val('0');
                    SetDefaultValue();

                }
            });
        }

        function bindItemList() {

            var cgColumns = [
                              { 'columnName': 'itemname', 'width': '300', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                             , { 'columnName': 'itemcode', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Code' }
                             //, { 'columnName': 'itemgroupdesc', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Group Name' }
                              , { 'columnName': 'qty', 'width': '120', 'align': 'left', 'highlight': 4, 'label': 'Quantity' }
                             , { 'columnName': 'uom', 'width': '50', 'align': 'left', 'highlight': 4, 'label': 'Uom' }
                             
            ];

            var itemServiceURL = ItemListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;

            itemServiceURL += "&ispaging=1";
            var itemIDElem = $('#' + txtItemName);

            $('#' + btnItemID).click(function (e) {
                $(itemIDElem).combogrid("dropdownClick");
            });

            $(itemIDElem).combogrid({
                debug: true,
                searchButton: false,
                resetButton: false,
                alternate: true,
                munit: 'px',
                scrollBar: true,
                showPager: true,
                colModel: cgColumns,
                autoFocus: true,
                showError: true,
                width: 600,
                url: itemServiceURL,
                search: function (event, ui) {
                    //var lcid = $('#' + hdnLcId).val();
                    var purid = $('#' + hdnIMP_PURCHASE_ID).val();
                    var newServiceURL = itemServiceURL + "&purid=" + purid;
                    $(this).combogrid("option", "url", newServiceURL);


                },
                select: function (event, ui) {
                    if (!ui.item) {
                        event.preventDefault();


                        $('#' + hdnItemID).val('0');
                        return false;
                        //ClearGLAccountData(elemID);
                    }


                    if (ui.item.itemid == '') {
                        event.preventDefault();
                        return false;
                        //ClearGLAccountData(elemID);
                    }
                    else {

                        $('#' + hdnItemID).val(ui.item.itemid);
                        $('#' + txtItemName).val(ui.item.itemname);
                        $('#' + txtItemQty).val(ui.item.qty);
                        $('#' + txtUom).val(ui.item.uom);
                        $('#' + txtConvertedItemQty).val(ui.item.convertedqty);
                        $('#' + txtConvertedUom).val(ui.item.converteduom);
                        //$('#' + txtItemQtyAct).val(ui.item.qty);
                        //$('#' + txtUomAct).val(ui.item.uom);
                        //$('#' + txtConvertedItemQtyAct).val(ui.item.convertedqty);
                        //$('#' + txtConvertedUomAct).val(ui.item.converteduom);
                        $("[id*=btnFixedCharge]").click();


                    }
                    return false;
                },

                lc: ''
            });

            $(itemIDElem).blur(function () {
                var self = this;
                var groupID = $(itemIDElem).val();
                if (groupID == '') {
                    // $('#' + hdnDealerID).val('0');
                    $('#' + txtItemName).val('');
                    //$('#' + txtGroupCode).val('');
                    $('#' + hdnItemID).val('0');
                    $('#' + txtItemQty).val('');
                    $('#' + txtUom).val('');
                    $('#' + txtConvertedItemQty).val('');
                    $('#' + txtConvertedUom).val('');
                    //$('#' + txtItemQtyAct).val('');
                    //$('#' + txtUomAct).val('');
                    //$('#' + txtConvertedItemQtyAct).val('');
                    //$('#' + txtConvertedUomAct).val('');
                    SetDefaultValue();

                }
            });
        }


        function GetItemNo(eCode, serviceURL) {
            var prcNo = null;
            var isError = false;
            var isComplete = false;
            //ajax call

            var newServiceURL = serviceURL + " &selectedId=" + eCode;
            var dummyVar = $.ajax({
                type: "GET",
                cache: false,
                async: false,
                dataType: "json",
                url: newServiceURL,

                success: function (prddata) {
                    //            if (accdata.menu[0].count > 0) {
                    //                menu = menudata.menu[0];
                    //            }
                    if (prddata.rows.length > 0) {
                        prcNo = prddata.rows[0];
                    }
                },
                complete: function () {
                    if (!isError) {
                        //return;
                        //alert (menu.name);
                    }
                    isComplete = true;
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    isError = true;
                    alert(textStatus);
                }
            });
            return prcNo;
        }

        function SetDefaultValue()
        {
            document.getElementById("<%=hdnCostingId.ClientID%>").value = "0";
            document.getElementById("<%=txtCnfPrice.ClientID%>").value = "0";
            document.getElementById("<%=txtConversionRate.ClientID%>").value = "0";
            document.getElementById("<%=txtDocumentValue.ClientID%>").value = "0";
            document.getElementById("<%=txtMarineInsurance.ClientID%>").value = "0";
            document.getElementById("<%=txtInsuranceAndOth.ClientID%>").value = "0";
            document.getElementById("<%=txtAssessableValue.ClientID%>").value = "0";
            document.getElementById("<%=txtGlobalTaxes.ClientID%>").value = "0";
            document.getElementById("<%=txtCd.ClientID%>").value = "0";
            document.getElementById("<%=txtRd.ClientID%>").value = "0";
            document.getElementById("<%=txtSd.ClientID%>").value = "0";
            document.getElementById("<%=txtVat.ClientID%>").value = "0";
            document.getElementById("<%=txtAit.ClientID%>").value = "0";
            document.getElementById("<%=txtAt.ClientID%>").value = "0";
            document.getElementById("<%=txtTotalItemTax.ClientID%>").value = "0";
            document.getElementById("<%=txtTotalTaxes.ClientID%>").value = "0";
            document.getElementById("<%=txtPortCharge.ClientID%>").value = "0";
            document.getElementById("<%=txtAddVat.ClientID%>").value = "0";
            document.getElementById("<%=txtOthers.ClientID%>").value = "0";
            document.getElementById("<%=txtTotPortCharge.ClientID%>").value = "0";
            document.getElementById("<%=txtShippingCharge.ClientID%>").value = "0";
            document.getElementById("<%=txtNocCharge.ClientID%>").value = "0";
            document.getElementById("<%=txtBOperatorCharge.ClientID%>").value = "0";
            document.getElementById("<%=txtSpPermisionCharge.ClientID%>").value = "0";
            document.getElementById("<%=txtSaftaCharge.ClientID%>").value = "0";
            document.getElementById("<%=txtCnfCommision.ClientID%>").value = "0";
            document.getElementById("<%=txtOthersCharge.ClientID%>").value = "0";
            document.getElementById("<%=txtTotClearingCharge.ClientID%>").value = "0";
            document.getElementById("<%=txtSeaFreight.ClientID%>").value = "0";
            document.getElementById("<%=txtTransport.ClientID%>").value = "0";
            document.getElementById("<%=txtMiscellaneous.ClientID%>").value = "0";
            document.getElementById("<%=txtTotWithVat.ClientID%>").value = "0";
            document.getElementById("<%=txtTotWOVat.ClientID%>").value = "0";
            document.getElementById("<%=txtFactor.ClientID%>").value = "0";
            document.getElementById("<%=txtItemQty.ClientID%>").value = "0";
            document.getElementById("<%=txtConvertedItemQty.ClientID%>").value = "0";
            document.getElementById("<%=txtRate.ClientID%>").value = "0";
            document.getElementById("<%=txtPortAit.ClientID%>").value = "0";
            document.getElementById("<%=lblItemCount.ClientID%>").value = "0";
            
            
            SetDefaultValueAct();
           
          
        }

        function SetDefaultValueAct()
        {
            document.getElementById("<%=txtCommercialValueAct.ClientID%>").value = "0";
            document.getElementById("<%=txtConversionRateAct.ClientID%>").value = "0";
            document.getElementById("<%=txtDocumentValueAct.ClientID%>").value = "0";
            document.getElementById("<%=txtMarineInsuranceAct.ClientID%>").value = "0";
            document.getElementById("<%=txtInsuranceOthAct.ClientID%>").value = "0";
            document.getElementById("<%=txtAssessableValueAct.ClientID%>").value = "0";
            document.getElementById("<%=txtGlobalTaxAct.ClientID%>").value = "0";
            document.getElementById("<%=txtCdAct.ClientID%>").value = "0";
            document.getElementById("<%=txtRdAct.ClientID%>").value = "0";
            document.getElementById("<%=txtSdAct.ClientID%>").value = "0";
            document.getElementById("<%=txtVatAct.ClientID%>").value = "0";
            document.getElementById("<%=txtAitAct.ClientID%>").value = "0";
            document.getElementById("<%=txtAtAct.ClientID%>").value = "0";
            document.getElementById("<%=txtTotItemTaxAct.ClientID%>").value = "0";
            document.getElementById("<%=txtTotTaxesAmtAct.ClientID%>").value = "0";
            document.getElementById("<%=txtPortChargeAct.ClientID%>").value = "0";
            document.getElementById("<%=txtAddVatAct.ClientID%>").value = "0";
            document.getElementById("<%=txtOthersAct.ClientID%>").value = "0";
            document.getElementById("<%=txtTotPortChargeAct.ClientID%>").value = "0";
            document.getElementById("<%=txtShippingChargeAct.ClientID%>").value = "0";
            document.getElementById("<%=txtNocChargeAct.ClientID%>").value = "0";
            document.getElementById("<%=txtBOperatorChargeAct.ClientID%>").value = "0";
            document.getElementById("<%=txtSpPerChargeAct.ClientID%>").value = "0";
            document.getElementById("<%=txtShaftaChargeAct.ClientID%>").value = "0";
            document.getElementById("<%=txtcnfCommAct.ClientID%>").value = "0";
            document.getElementById("<%=txtOthChargeAct.ClientID%>").value = "0";
            document.getElementById("<%=txtTotClearingChargeAct.ClientID%>").value = "0";
            document.getElementById("<%=txtSeaFreightAct.ClientID%>").value = "0";
            document.getElementById("<%=txtTransportAct.ClientID%>").value = "0";
            document.getElementById("<%=txtMiscellaneousAct.ClientID%>").value = "0";
            document.getElementById("<%=txtTotWithVatAct.ClientID%>").value = "0";
            document.getElementById("<%=txtTotWithoutVatAct.ClientID%>").value = "0";
            document.getElementById("<%=txtFactorAct.ClientID%>").value = "0";
            document.getElementById("<%=txtItemQtyAct.ClientID%>").value = "0";
            document.getElementById("<%=txtConvertedItemQty.ClientID%>").value = "0";
            document.getElementById("<%=txtRateAct.ClientID%>").value = "0";
            document.getElementById("<%=txtPortAitAct.ClientID%>").value = "0";

        }

        function calculatePanel()
        {
            var lbltotType = document.getElementById("<%=lbltotType.ClientID%>").value;
            
            if (lbltotType == 1) {
                //alert(lbltotType);
                var ItemCount = document.getElementById("<%=lblItemCount.ClientID%>").value;


                var txtItemName = document.getElementById("<%=txtItemName.ClientID%>").value;
                if (txtItemName == "") {
                    alert("Please Select Item !!");
                    document.getElementById("<%=txtItemName.ClientID%>").focus();
                    SetDefaultValue();
                    return false;
                }


                var txtCnfPrice = document.getElementById("<%=txtCnfPrice.ClientID%>").value;
                txtCnfPrice = parseFloat(txtCnfPrice.replaceAll(',', ''));
                if (isNaN(txtCnfPrice)) {
                    txtCnfPrice = 0;
                }
                var txtConversionRate = document.getElementById("<%=txtConversionRate.ClientID%>").value;
                txtConversionRate = parseFloat(txtConversionRate.replaceAll(',', ''));
                if (isNaN(txtConversionRate)) {
                    txtConversionRate = 0;
                }
                documentValue = txtCnfPrice * txtConversionRate;
                var hdnTotLCValueUSD = document.getElementById("<%=hdnTotLCValueUSD.ClientID%>").value;
                lcinvtotvaluebdt = hdnTotLCValueUSD * txtConversionRate;
                document.getElementById("<%=lblItemCount.ClientID%>").value = lcinvtotvaluebdt.toFixed(4);
                if (!isNaN(documentValue)) {
                    document.getElementById("<%=txtDocumentValue.ClientID%>").value = documentValue.toFixed(2);
                }

                var txtDocumentValue = parseFloat(document.getElementById("<%=txtDocumentValue.ClientID%>").value);

                var txtMarineInsurance = document.getElementById("<%=txtMarineInsurance.ClientID%>").value;
                txtMarineInsurance = parseFloat(txtMarineInsurance.replaceAll(',', ''));
                if (isNaN(txtMarineInsurance)) {
                    txtMarineInsurance = 0;
                }
                var txtInsuranceAndOth = document.getElementById("<%=txtInsuranceAndOth.ClientID%>").value;
                txtInsuranceAndOth = parseFloat(txtInsuranceAndOth.replaceAll(',', ''));
                if (isNaN(txtInsuranceAndOth)) {
                    txtInsuranceAndOth = 0;
                }
                var AssesValue = txtDocumentValue + txtMarineInsurance + txtInsuranceAndOth;
                if (!isNaN(AssesValue)) {
                    document.getElementById("<%=txtAssessableValue.ClientID%>").value = AssesValue.toFixed(2);
                }

                var txtGlobalTaxes = document.getElementById("<%=txtGlobalTaxes.ClientID%>").value; <%--parseFloat(document.getElementById("<%=txtGlobalTaxes.ClientID%>").value);--%>
                txtGlobalTaxes = parseFloat(txtGlobalTaxes.replaceAll(',', ''));
                //txtGlobalTaxesDiv = parseFloat(txtGlobalTaxes / ItemCount).toFixed(3);
                if (isNaN(txtGlobalTaxes)) {
                    txtGlobalTaxes = 0;
                }
                var txtCd = document.getElementById("<%=txtCd.ClientID%>").value;
                txtCd = parseFloat(txtCd.replaceAll(',', ''));
                if (isNaN(txtCd)) {
                    txtCd = 0;
                }
                var txtRd = document.getElementById("<%=txtRd.ClientID%>").value;
                txtRd = parseFloat(txtRd.replaceAll(',', ''));
                if (isNaN(txtRd)) {
                    txtRd = 0;
                }
                var txtSd = document.getElementById("<%=txtSd.ClientID%>").value;
                txtSd = parseFloat(txtSd.replaceAll(',', ''));
                if (isNaN(txtSd)) {
                    txtSd = 0;
                }
                var txtVat = document.getElementById("<%=txtVat.ClientID%>").value;
                txtVat = parseFloat(txtVat.replaceAll(',', ''));
                if (isNaN(txtVat)) {
                    txtVat = 0;
                }
                var txtAit = document.getElementById("<%=txtAit.ClientID%>").value;
                txtAit = parseFloat(txtAit.replaceAll(',', ''));
                if (isNaN(txtAit)) {
                    txtAit = 0;
                }
                var txtAt = document.getElementById("<%=txtAt.ClientID%>").value;
                txtAt = parseFloat(txtAt.replaceAll(',', ''));
                if (isNaN(txtAt)) {
                    txtAt = 0;
                }

                var TotalItemTax = txtCd + txtRd + txtSd + txtVat + txtAit + txtAt;
                if (!isNaN(TotalItemTax)) {
                    document.getElementById("<%=txtTotalItemTax.ClientID%>").value = TotalItemTax.toFixed(2);
                }
                //alert(txtGlobalTaxesDiv);
                var TotalTax = txtGlobalTaxes + TotalItemTax;
                //var TotalTax = parseFloat(txtGlobalTaxesDiv) + TotalItemTax;
                
                if (!isNaN(TotalTax)) {
                    document.getElementById("<%=txtTotalTaxes.ClientID%>").value = TotalTax.toFixed(2);
                }


                var txtPortCharge = document.getElementById("<%=txtPortCharge.ClientID%>").value;
                txtPortCharge = parseFloat(txtPortCharge.replaceAll(',', ''));
                //txtPortChargeDiv = parseFloat(txtPortCharge / ItemCount).toFixed(3);
                if (isNaN(txtPortCharge)) {
                    txtPortCharge = 0;
                }
                var txtAddVat = document.getElementById("<%=txtAddVat.ClientID%>").value;
                txtAddVat = parseFloat(txtAddVat.replaceAll(',', ''));
                //txtAddVatDiv = parseFloat(txtAddVat / ItemCount).toFixed(3);
                if (isNaN(txtAddVat)) {
                    txtAddVat = 0;
                }
                var txtPortAit = document.getElementById("<%=txtPortAit.ClientID%>").value;
                txtPortAit = parseFloat(txtPortAit.replaceAll(',', ''));
                //txtPortAitDiv = (txtPortAit / ItemCount).toFixed(3);
                if (isNaN(txtPortAit)) {
                    txtPortAit = 0;
                }
                var txtOthers = document.getElementById("<%=txtOthers.ClientID%>").value;
                txtOthers = parseFloat(txtOthers.replaceAll(',', ''));
                //txtOthersDiv = parseFloat(txtOthers / ItemCount).toFixed(3);
                if (isNaN(txtOthers)) {
                    txtOthers = 0;
                }

                //TotalPortChargeDiv = parseFloat(txtPortChargeDiv) + parseFloat(txtAddVatDiv) + parseFloat(txtPortAitDiv) + parseFloat(txtOthersDiv);

                var TotalPortCharge = txtPortCharge + txtAddVat + txtPortAit + txtOthers;
                if (!isNaN(TotalPortCharge)) {
                    document.getElementById("<%=txtTotPortCharge.ClientID%>").value = TotalPortCharge.toFixed(2);
                }
               
                txtShippingCharge = document.getElementById("<%=txtShippingCharge.ClientID%>").value;
                txtShippingCharge = parseFloat(txtShippingCharge.replaceAll(',', ''));
                //var txtShippingChargeDiv = (txtShippingCharge / ItemCount).toFixed(3);
                if (isNaN(txtShippingCharge)) {
                    txtShippingCharge = 0;
                }
               
                txtNocCharge = document.getElementById("<%=txtNocCharge.ClientID%>").value;
                txtNocCharge = parseFloat(txtNocCharge.replaceAll(',', ''));
                //var txtNocChargeDiv = (txtNocCharge / ItemCount).toFixed(3);
                if (isNaN(txtNocCharge)) {
                    txtNocCharge = 0;
                }
                txtBOperatorCharge = document.getElementById("<%=txtBOperatorCharge.ClientID%>").value;
                txtBOperatorCharge = parseFloat(txtBOperatorCharge.replaceAll(',', ''));
                //var txtBOperatorChargeDiv = (txtBOperatorCharge / ItemCount).toFixed(3);
                if (isNaN(txtBOperatorCharge)) {
                    txtBOperatorCharge = 0;
                }
                txtSpPermisionCharge = document.getElementById("<%=txtSpPermisionCharge.ClientID%>").value;
                txtSpPermisionCharge = parseFloat(txtSpPermisionCharge.replaceAll(',', ''));
                //var txtSpPermisionChargeDiv = (txtSpPermisionCharge / ItemCount).toFixed(3);
                if (isNaN(txtSpPermisionCharge)) {
                    txtSpPermisionCharge = 0;
                }
                txtSaftaCharge = document.getElementById("<%=txtSaftaCharge.ClientID%>").value;
                txtSaftaCharge = parseFloat(txtSaftaCharge.replaceAll(',', ''));
                //var txtSaftaChargeDiv = (txtSaftaCharge / ItemCount).toFixed(3);
                if (isNaN(txtSaftaCharge)) {
                    txtSaftaCharge = 0;
                }
                txtCnfCommision = document.getElementById("<%=txtCnfCommision.ClientID%>").value;
                txtCnfCommision = parseFloat(txtCnfCommision.replaceAll(',', ''));
                //var txtCnfCommisionDiv = (txtCnfCommision / ItemCount).toFixed(3);
                if (isNaN(txtCnfCommision)) {
                    txtCnfCommision = 0;
                }
                txtOthersCharge = document.getElementById("<%=txtOthersCharge.ClientID%>").value;
                txtOthersCharge = parseFloat(txtOthersCharge.replaceAll(',', ''));
                //var txtOthersChargeDiv = (txtOthersCharge / ItemCount).toFixed(3);
                if (isNaN(txtOthersCharge)) {
                    txtOthersCharge = 0;
                }
                
                txtTransport = document.getElementById("<%=txtTransport.ClientID%>").value;
                txtTransport = parseFloat(txtTransport.replaceAll(',', ''));
                //var txtTransportDiv = (txtTransport / ItemCount).toFixed(3);
                if (isNaN(txtTransport)) {
                    txtTransport = 0;
                }

                
                txtMiscellaneous = document.getElementById("<%=txtMiscellaneous.ClientID%>").value;
                txtMiscellaneous = parseFloat(txtMiscellaneous.replaceAll(',', ''));
                //var txtMiscellaneousDiv = parseFloat(txtMiscellaneous / ItemCount).toFixed(3);
                if (isNaN(txtMiscellaneous)) {
                    txtMiscellaneous = 0;
                }
               
                //var TotalOthChargeDiv = parseFloat(txtShippingChargeDiv) + parseFloat(txtNocChargeDiv) + parseFloat(txtBOperatorChargeDiv) + parseFloat(txtSpPermisionChargeDiv) + parseFloat(txtSaftaChargeDiv) + parseFloat(txtCnfCommisionDiv) + parseFloat(txtOthersChargeDiv) + parseFloat(txtTransportDiv) + parseFloat(txtMiscellaneousDiv);
                TotalOthCharge = txtShippingCharge + txtNocCharge + txtBOperatorCharge + txtSpPermisionCharge + txtSaftaCharge + txtCnfCommision + txtOthersCharge + txtTransport + txtMiscellaneous;
                TotalCharge = TotalPortCharge + TotalOthCharge;
                //var TotalChargeDiv = parseFloat(TotalPortChargeDiv) + parseFloat(TotalOthChargeDiv);
                if (!isNaN(TotalCharge)) {
                    document.getElementById("<%=txtTotClearingCharge.ClientID%>").value = TotalCharge.toFixed(2);
                }

                txtSeaFreight = document.getElementById("<%=txtSeaFreight.ClientID%>").value;
                txtSeaFreight = parseFloat(txtSeaFreight.replaceAll(',', ''));
                //var txtSeaFreightDiv = (txtSeaFreight / ItemCount).toFixed(3);
                if (isNaN(txtSeaFreight)) {
                    txtSeaFreight = 0;
                }
                //alert(lbltotType);
                //TotWithVat = AssesValue + TotalTax + parseFloat(TotalChargeDiv) + parseFloat(txtSeaFreightDiv);// + txtTransport + txtMiscellaneous;
                //TotWithVat = AssesValue + TotalTax + TotalCharge + parseFloat(txtSeaFreight); date 270723
                TotWithVat = parseFloat(txtDocumentValue) + parseFloat(txtMarineInsurance) + TotalTax + TotalCharge + parseFloat(txtSeaFreight);
                
                //alert(TotWithVat);
                
                if (!isNaN(TotWithVat)) {
                    document.getElementById("<%=txtTotWithVat.ClientID%>").value = TotWithVat.toFixed(2);
                }

                TotVat = txtVat + txtAit + txtAt;
                TotWOVat = TotWithVat - TotVat;
                if (!isNaN(TotWOVat)) {
                    document.getElementById("<%=txtTotWOVat.ClientID%>").value = TotWOVat.toFixed(2);
                }

                factor = TotWOVat / documentValue;
                if (!isNaN(factor)) {
                    document.getElementById("<%=txtFactor.ClientID%>").value = factor.toFixed(2);
                }

                txtItemQty = parseFloat(document.getElementById("<%=txtItemQty.ClientID%>").value);
                if (isNaN(txtItemQty)) {
                    txtItemQty = 0;
                }

                txtConvertedItemQty = parseFloat(document.getElementById("<%=txtConvertedItemQty.ClientID%>").value);
                if (isNaN(txtConvertedItemQty)) {
                    txtConvertedItemQty = 0;
                }
                Rate = TotWOVat / txtConvertedItemQty;

                if (!isNaN(Rate)) {
                    document.getElementById("<%=txtRate.ClientID%>").value = Rate.toFixed(4);
                }
            }
            else
            {
                //alert(1);
                var ItemCount = document.getElementById("<%=lblItemCount.ClientID%>").value;


                var txtItemName = document.getElementById("<%=txtItemName.ClientID%>").value;
                if (txtItemName == "") {
                    alert("Please Select Item !!");
                    document.getElementById("<%=txtItemName.ClientID%>").focus();
                    SetDefaultValue();
                    return false;
                }


                var txtCnfPrice = document.getElementById("<%=txtCnfPrice.ClientID%>").value;
                txtCnfPrice = parseFloat(txtCnfPrice.replaceAll(',', ''));
                if (isNaN(txtCnfPrice)) {
                    txtCnfPrice = 0;
                }
                var txtConversionRate = document.getElementById("<%=txtConversionRate.ClientID%>").value;
                txtConversionRate = parseFloat(txtConversionRate.replaceAll(',', ''));
                if (isNaN(txtConversionRate)) {
                    txtConversionRate = 0;
                }
                documentValue = txtCnfPrice * txtConversionRate;
                var hdnTotLCValueUSD = document.getElementById("<%=hdnTotLCValueUSD.ClientID%>").value;
                lcinvtotvaluebdt = hdnTotLCValueUSD * txtConversionRate;
                document.getElementById("<%=lblItemCount.ClientID%>").value = lcinvtotvaluebdt.toFixed(4);
                if (!isNaN(documentValue)) {
                    document.getElementById("<%=txtDocumentValue.ClientID%>").value = documentValue.toFixed(2);
                }

                var txtDocumentValue = parseFloat(document.getElementById("<%=txtDocumentValue.ClientID%>").value);

                var txtMarineInsurance = document.getElementById("<%=txtMarineInsurance.ClientID%>").value;
                txtMarineInsurance = parseFloat(txtMarineInsurance.replaceAll(',', ''));
                if (isNaN(txtMarineInsurance)) {
                    txtMarineInsurance = 0;
                }
                var txtInsuranceAndOth = document.getElementById("<%=txtInsuranceAndOth.ClientID%>").value;
                txtInsuranceAndOth = parseFloat(txtInsuranceAndOth.replaceAll(',', ''));
                if (isNaN(txtInsuranceAndOth)) {
                    txtInsuranceAndOth = 0;
                }
                var AssesValue = txtDocumentValue + txtMarineInsurance + txtInsuranceAndOth;
                if (!isNaN(AssesValue)) {
                    document.getElementById("<%=txtAssessableValue.ClientID%>").value = AssesValue.toFixed(2);
                }

                var txtGlobalTaxes = document.getElementById("<%=txtGlobalTaxes.ClientID%>").value; <%--parseFloat(document.getElementById("<%=txtGlobalTaxes.ClientID%>").value);--%>
                txtGlobalTaxes = parseFloat(txtGlobalTaxes.replaceAll(',', ''));
                txtGlobalTaxesDiv = parseFloat(txtGlobalTaxes / ItemCount).toFixed(3);
                if (isNaN(txtGlobalTaxes)) {
                    txtGlobalTaxes = 0;
                }
                var txtCd = document.getElementById("<%=txtCd.ClientID%>").value;
                txtCd = parseFloat(txtCd.replaceAll(',', ''));
                if (isNaN(txtCd)) {
                    txtCd = 0;
                }
                var txtRd = document.getElementById("<%=txtRd.ClientID%>").value;
                txtRd = parseFloat(txtRd.replaceAll(',', ''));
                if (isNaN(txtRd)) {
                    txtRd = 0;
                }
                var txtSd = document.getElementById("<%=txtSd.ClientID%>").value;
                txtSd = parseFloat(txtSd.replaceAll(',', ''));
                if (isNaN(txtSd)) {
                    txtSd = 0;
                }
                var txtVat = document.getElementById("<%=txtVat.ClientID%>").value;
                txtVat = parseFloat(txtVat.replaceAll(',', ''));
                if (isNaN(txtVat)) {
                    txtVat = 0;
                }
                var txtAit = document.getElementById("<%=txtAit.ClientID%>").value;
                txtAit = parseFloat(txtAit.replaceAll(',', ''));
                if (isNaN(txtAit)) {
                    txtAit = 0;
                }
                var txtAt = document.getElementById("<%=txtAt.ClientID%>").value;
                txtAt = parseFloat(txtAt.replaceAll(',', ''));
                if (isNaN(txtAt)) {
                    txtAt = 0;
                }

                var TotalItemTax = txtCd + txtRd + txtSd + txtVat + txtAit + txtAt;
                if (!isNaN(TotalItemTax)) {
                    document.getElementById("<%=txtTotalItemTax.ClientID%>").value = TotalItemTax.toFixed(2);
                }
                //alert(txtGlobalTaxesDiv);
                //var TotalTax = txtGlobalTaxes + TotalItemTax;
                var TotalTax = parseFloat(txtGlobalTaxesDiv) + TotalItemTax;

                if (!isNaN(TotalTax)) {
                    document.getElementById("<%=txtTotalTaxes.ClientID%>").value = TotalTax.toFixed(2);
                }


                var txtPortCharge = document.getElementById("<%=txtPortCharge.ClientID%>").value;
                txtPortCharge = parseFloat(txtPortCharge.replaceAll(',', ''));
                txtPortChargeDiv = parseFloat(txtPortCharge / ItemCount).toFixed(3);
                if (isNaN(txtPortCharge)) {
                    txtPortCharge = 0;
                }
                var txtAddVat = document.getElementById("<%=txtAddVat.ClientID%>").value;
                txtAddVat = parseFloat(txtAddVat.replaceAll(',', ''));
                txtAddVatDiv = parseFloat(txtAddVat / ItemCount).toFixed(3);
                if (isNaN(txtAddVat)) {
                    txtAddVat = 0;
                }
                var txtPortAit = document.getElementById("<%=txtPortAit.ClientID%>").value;
                txtPortAit = parseFloat(txtPortAit.replaceAll(',', ''));
                txtPortAitDiv = (txtPortAit / ItemCount).toFixed(3);
                if (isNaN(txtPortAit)) {
                    txtPortAit = 0;
                }
                var txtOthers = document.getElementById("<%=txtOthers.ClientID%>").value;
                txtOthers = parseFloat(txtOthers.replaceAll(',', ''));
                txtOthersDiv = parseFloat(txtOthers / ItemCount).toFixed(3);
                if (isNaN(txtOthers)) {
                    txtOthers = 0;
                }

                TotalPortChargeDiv = parseFloat(txtPortChargeDiv) + parseFloat(txtAddVatDiv) + parseFloat(txtPortAitDiv) + parseFloat(txtOthersDiv);

                var TotalPortCharge = txtPortCharge + txtAddVat + txtPortAit + txtOthers;
                if (!isNaN(TotalPortCharge)) {
                    document.getElementById("<%=txtTotPortCharge.ClientID%>").value = TotalPortCharge.toFixed(2);
                }

                txtShippingCharge = document.getElementById("<%=txtShippingCharge.ClientID%>").value;
                txtShippingCharge = parseFloat(txtShippingCharge.replaceAll(',', ''));
                var txtShippingChargeDiv = (txtShippingCharge / ItemCount).toFixed(3);
                if (isNaN(txtShippingCharge)) {
                    txtShippingCharge = 0;
                }
                txtNocCharge = document.getElementById("<%=txtNocCharge.ClientID%>").value;
                txtNocCharge = parseFloat(txtNocCharge.replaceAll(',', ''));
                var txtNocChargeDiv = (txtNocCharge / ItemCount).toFixed(3);
                if (isNaN(txtNocCharge)) {
                    txtNocCharge = 0;
                }
                txtBOperatorCharge = document.getElementById("<%=txtBOperatorCharge.ClientID%>").value;
                txtBOperatorCharge = parseFloat(txtBOperatorCharge.replaceAll(',', ''));
                var txtBOperatorChargeDiv = (txtBOperatorCharge / ItemCount).toFixed(3);
                if (isNaN(txtBOperatorCharge)) {
                    txtBOperatorCharge = 0;
                }
                txtSpPermisionCharge = document.getElementById("<%=txtSpPermisionCharge.ClientID%>").value;
                txtSpPermisionCharge = parseFloat(txtSpPermisionCharge.replaceAll(',', ''));
                var txtSpPermisionChargeDiv = (txtSpPermisionCharge / ItemCount).toFixed(3);
                if (isNaN(txtSpPermisionCharge)) {
                    txtSpPermisionCharge = 0;
                }
                txtSaftaCharge = document.getElementById("<%=txtSaftaCharge.ClientID%>").value;
                txtSaftaCharge = parseFloat(txtSaftaCharge.replaceAll(',', ''));
                var txtSaftaChargeDiv = (txtSaftaCharge / ItemCount).toFixed(3);
                if (isNaN(txtSaftaCharge)) {
                    txtSaftaCharge = 0;
                }
                txtCnfCommision = document.getElementById("<%=txtCnfCommision.ClientID%>").value;
                txtCnfCommision = parseFloat(txtCnfCommision.replaceAll(',', ''));
                var txtCnfCommisionDiv = (txtCnfCommision / ItemCount).toFixed(3);
                if (isNaN(txtCnfCommision)) {
                    txtCnfCommision = 0;
                }
                txtOthersCharge = document.getElementById("<%=txtOthersCharge.ClientID%>").value;
                txtOthersCharge = parseFloat(txtOthersCharge.replaceAll(',', ''));
                var txtOthersChargeDiv = (txtOthersCharge / ItemCount).toFixed(3);
                if (isNaN(txtOthersCharge)) {
                    txtOthersCharge = 0;
                }
                txtTransport = document.getElementById("<%=txtTransport.ClientID%>").value;
                txtTransport = parseFloat(txtTransport.replaceAll(',', ''));
                var txtTransportDiv = (txtTransport / ItemCount).toFixed(3);
                if (isNaN(txtTransport)) {
                    txtTransport = 0;
                }
                txtMiscellaneous = document.getElementById("<%=txtMiscellaneous.ClientID%>").value;
                txtMiscellaneous = parseFloat(txtMiscellaneous.replaceAll(',', ''));
                var txtMiscellaneousDiv = parseFloat(txtMiscellaneous / ItemCount).toFixed(3);
                if (isNaN(txtMiscellaneous)) {
                    txtMiscellaneous = 0;
                }
                var TotalOthChargeDiv = parseFloat(txtShippingChargeDiv) + parseFloat(txtNocChargeDiv) + parseFloat(txtBOperatorChargeDiv) + parseFloat(txtSpPermisionChargeDiv) + parseFloat(txtSaftaChargeDiv) + parseFloat(txtCnfCommisionDiv) + parseFloat(txtOthersChargeDiv) + parseFloat(txtTransportDiv) + parseFloat(txtMiscellaneousDiv);
                TotalOthCharge = txtShippingCharge + txtNocCharge + txtBOperatorCharge + txtSpPermisionCharge + txtSaftaCharge + txtCnfCommision + txtOthersCharge + txtTransport + txtMiscellaneous;
                TotalCharge = TotalPortCharge + TotalOthCharge;
                var TotalChargeDiv = parseFloat(TotalPortChargeDiv) + parseFloat(TotalOthChargeDiv);
                if (!isNaN(TotalCharge)) {
                    document.getElementById("<%=txtTotClearingCharge.ClientID%>").value = TotalCharge.toFixed(2);
                }

                txtSeaFreight = document.getElementById("<%=txtSeaFreight.ClientID%>").value;
                txtSeaFreight = parseFloat(txtSeaFreight.replaceAll(',', ''));
                var txtSeaFreightDiv = (txtSeaFreight / ItemCount).toFixed(3);
                if (isNaN(txtSeaFreight)) {
                    txtSeaFreight = 0;
                }

                //TotWithVat = AssesValue + TotalTax + parseFloat(TotalChargeDiv) + parseFloat(txtSeaFreightDiv);// + txtTransport + txtMiscellaneous;
                TotWithVat = parseFloat(txtDocumentValue) + parseFloat(txtMarineInsurance) + TotalTax + parseFloat(TotalChargeDiv) + parseFloat(txtSeaFreightDiv);
                
                //TotWithVat = AssesValue + TotalTax + TotalCharge + parseFloat(txtSeaFreight);
                //alert(TotWithVat);
                if (!isNaN(TotWithVat)) {
                    document.getElementById("<%=txtTotWithVat.ClientID%>").value = TotWithVat.toFixed(2);
                }

                TotVat = txtVat + txtAit + txtAt;
                TotWOVat = TotWithVat - TotVat;
                if (!isNaN(TotWOVat)) {
                    document.getElementById("<%=txtTotWOVat.ClientID%>").value = TotWOVat.toFixed(2);
                }

                factor = TotWOVat / documentValue;
                if (!isNaN(factor)) {
                    document.getElementById("<%=txtFactor.ClientID%>").value = factor.toFixed(2);
                }

                txtItemQty = parseFloat(document.getElementById("<%=txtItemQty.ClientID%>").value);
                if (isNaN(txtItemQty)) {
                    txtItemQty = 0;
                }

                txtConvertedItemQty = parseFloat(document.getElementById("<%=txtConvertedItemQty.ClientID%>").value);
                if (isNaN(txtConvertedItemQty)) {
                    txtConvertedItemQty = 0;
                }
                Rate = TotWOVat / txtConvertedItemQty;

                if (!isNaN(Rate)) {
                    document.getElementById("<%=txtRate.ClientID%>").value = Rate.toFixed(4);
                }
            }
          
        }

        function calculatePanelAct() {

            var lbltotType = document.getElementById("<%=lbltotType.ClientID%>").value;
            
            if (lbltotType == 1) {
                var ItemCount = document.getElementById("<%=lblItemCount.ClientID%>").value;
                var txtItemName = document.getElementById("<%=txtItemName.ClientID%>").value;
                if (txtItemName == "") {
                    alert("Please Select Item !!");
                    document.getElementById("<%=txtItemName.ClientID%>").focus();
                    SetDefaultValue();
                    return false;
                }


                var txtCommercialValueAct = document.getElementById("<%=txtCommercialValueAct.ClientID%>").value;
                txtCommercialValueAct = parseFloat(txtCommercialValueAct.replaceAll(',', ''));
                if (isNaN(txtCommercialValueAct)) {
                    txtCommercialValueAct = 0;
                }
                var txtConversionRateAct = document.getElementById("<%=txtConversionRateAct.ClientID%>").value;
                txtConversionRateAct = parseFloat(txtConversionRateAct.replaceAll(',', ''));
                if (isNaN(txtConversionRateAct)) {
                    txtConversionRateAct = 0;
                }
                documentValueAct = txtCommercialValueAct * txtConversionRateAct;
                if (!isNaN(documentValueAct)) {
                    document.getElementById("<%=txtDocumentValueAct.ClientID%>").value = documentValueAct.toFixed(2);

                }

                var txtDocumentValueAct = parseFloat(document.getElementById("<%=txtDocumentValueAct.ClientID%>").value);

                var txtMarineInsuranceAct = document.getElementById("<%=txtMarineInsuranceAct.ClientID%>").value;
                txtMarineInsuranceAct = parseFloat(txtMarineInsuranceAct.replaceAll(',', ''));
                if (isNaN(txtMarineInsuranceAct)) {
                    txtMarineInsuranceAct = 0;
                }
                var txtInsuranceOthAct = document.getElementById("<%=txtInsuranceOthAct.ClientID%>").value;
                txtInsuranceOthAct = parseFloat(txtInsuranceOthAct.replaceAll(',', ''));
                if (isNaN(txtInsuranceOthAct)) {
                    txtInsuranceOthAct = 0;
                }
                var AssesValueAct = txtDocumentValueAct + txtMarineInsuranceAct + txtInsuranceOthAct;
                if (!isNaN(AssesValueAct)) {
                    document.getElementById("<%=txtAssessableValueAct.ClientID%>").value = AssesValueAct.toFixed(2);
                }

                var txtGlobalTaxAct = document.getElementById("<%=txtGlobalTaxAct.ClientID%>").value;
                txtGlobalTaxAct = parseFloat(txtGlobalTaxAct.replaceAll(',', ''));
                //txtGlobalTaxesActDiv = parseFloat(txtGlobalTaxAct / ItemCount).toFixed(3);
                if (isNaN(txtGlobalTaxAct)) {
                    txtGlobalTaxAct = 0;
                }
                var txtCdAct = document.getElementById("<%=txtCdAct.ClientID%>").value;
                txtCdAct = parseFloat(txtCdAct.replaceAll(',', ''));
                if (isNaN(txtCdAct)) {
                    txtCdAct = 0;
                }
                var txtRdAct = document.getElementById("<%=txtRdAct.ClientID%>").value;
                txtRdAct = parseFloat(txtRdAct.replaceAll(',', ''));
                if (isNaN(txtRdAct)) {
                    txtRdAct = 0;
                }
                var txtSdAct = document.getElementById("<%=txtSdAct.ClientID%>").value;
                txtSdAct = parseFloat(txtSdAct.replaceAll(',', ''));
                if (isNaN(txtSdAct)) {
                    txtSdAct = 0;
                }
                var txtVatAct = document.getElementById("<%=txtVatAct.ClientID%>").value;
                txtVatAct = parseFloat(txtVatAct.replaceAll(',', ''));
                if (isNaN(txtVatAct)) {
                    txtVatAct = 0;
                }
                var txtAitAct = document.getElementById("<%=txtAitAct.ClientID%>").value;
                txtAitAct = parseFloat(txtAitAct.replaceAll(',', ''));
                if (isNaN(txtAitAct)) {
                    txtAitAct = 0;
                }
                var txtAtAct = document.getElementById("<%=txtAtAct.ClientID%>").value;
                txtAtAct = parseFloat(txtAtAct.replaceAll(',', ''));
                if (isNaN(txtAtAct)) {
                    txtAtAct = 0;
                }

                var TotalItemTaxAct = txtCdAct + txtRdAct + txtSdAct + txtVatAct + txtAitAct + txtAtAct;
                if (!isNaN(TotalItemTaxAct)) {
                    document.getElementById("<%=txtTotItemTaxAct.ClientID%>").value = TotalItemTaxAct.toFixed(2);
                }

                var TotalTaxAct = txtGlobalTaxAct + TotalItemTaxAct;
                //var TotalTaxAct = parseFloat(txtGlobalTaxesActDiv) + TotalItemTaxAct;

                if (!isNaN(TotalTaxAct)) {
                    document.getElementById("<%=txtTotTaxesAmtAct.ClientID%>").value = TotalTaxAct.toFixed(2);
                }

                var txtPortChargeAct = document.getElementById("<%=txtPortChargeAct.ClientID%>").value;
                txtPortChargeAct = parseFloat(txtPortChargeAct.replaceAll(',', ''));
                //var txtPortChargeActDiv = parseFloat(txtPortChargeAct / ItemCount).toFixed(3);
                if (isNaN(txtPortChargeAct)) {
                    txtPortChargeAct = 0;
                }
                var txtAddVatAct = document.getElementById("<%=txtAddVatAct.ClientID%>").value;
                txtAddVatAct = parseFloat(txtAddVatAct.replaceAll(',', ''));
                //var txtAddVatActDiv = parseFloat(txtAddVatAct / ItemCount).toFixed(3);
                if (isNaN(txtAddVatAct)) {
                    txtAddVatAct = 0;
                }
                var txtPortAitAct = document.getElementById("<%=txtPortAitAct.ClientID%>").value;
                txtPortAitAct = parseFloat(txtPortAitAct.replaceAll(',', ''));
                //var txtPortAitActDiv = parseFloat(txtPortAitAct / ItemCount).toFixed(3);
                if (isNaN(txtPortAitAct)) {
                    txtPortAitAct = 0;
                }
                var txtOthersAct = document.getElementById("<%=txtOthersAct.ClientID%>").value;
                txtOthersAct = parseFloat(txtOthersAct.replaceAll(',', ''));
                //var txtOthersActDiv = parseFloat(txtOthersAct / ItemCount).toFixed(3);
                if (isNaN(txtOthersAct)) {
                    txtOthersAct = 0;
                }

                //var TotalPortChargeActDiv = parseFloat(txtPortChargeActDiv) + parseFloat(txtAddVatActDiv) + parseFloat(txtPortAitActDiv) + parseFloat(txtOthersActDiv);

                var TotalPortChargeAct = txtPortChargeAct + txtAddVatAct + txtPortAitAct + txtOthersAct;
                if (!isNaN(TotalPortChargeAct)) {
                    document.getElementById("<%=txtTotPortChargeAct.ClientID%>").value = TotalPortChargeAct.toFixed(2);
                }

                txtShippingChargeAct = document.getElementById("<%=txtShippingChargeAct.ClientID%>").value;
                txtShippingChargeAct = parseFloat(txtShippingChargeAct.replaceAll(',', ''));
                //var txtShippingChargeActDiv = parseFloat(txtShippingChargeAct / ItemCount).toFixed(3);
                if (isNaN(txtShippingChargeAct)) {
                    txtShippingChargeAct = 0;
                }
                txtNocChargeAct = document.getElementById("<%=txtNocChargeAct.ClientID%>").value;
                txtNocChargeAct = parseFloat(txtNocChargeAct.replaceAll(',', ''));
                //var txtNocChargeActDiv = parseFloat(txtNocChargeAct / ItemCount).toFixed(3);
                if (isNaN(txtNocChargeAct)) {
                    txtNocChargeAct = 0;
                }
                txtBOperatorChargeAct = document.getElementById("<%=txtBOperatorChargeAct.ClientID%>").value;
                txtBOperatorChargeAct = parseFloat(txtBOperatorChargeAct.replaceAll(',', ''));
                //var txtBOperatorChargeActDiv = parseFloat(txtBOperatorChargeAct / ItemCount).toFixed(3);
                if (isNaN(txtBOperatorChargeAct)) {
                    txtBOperatorChargeAct = 0;
                }
                txtSpPerChargeAct = document.getElementById("<%=txtSpPerChargeAct.ClientID%>").value;
                txtSpPerChargeAct = parseFloat(txtSpPerChargeAct.replaceAll(',', ''));
                //var txtSpPerChargeActDiv = parseFloat(txtSpPerChargeAct / ItemCount).toFixed(3);
                if (isNaN(txtSpPerChargeAct)) {
                    txtSpPerChargeAct = 0;
                }
                txtShaftaChargeAct = document.getElementById("<%=txtShaftaChargeAct.ClientID%>").value;
                txtShaftaChargeAct = parseFloat(txtShaftaChargeAct.replaceAll(',', ''));
                //var txtShaftaChargeActDiv = parseFloat(txtShaftaChargeAct / ItemCount).toFixed(3);
                if (isNaN(txtShaftaChargeAct)) {
                    txtShaftaChargeAct = 0;
                }
                txtcnfCommAct = document.getElementById("<%=txtcnfCommAct.ClientID%>").value;
                txtcnfCommAct = parseFloat(txtcnfCommAct.replaceAll(',', ''));
                //var txtcnfCommActDiv = parseFloat(txtcnfCommAct / ItemCount).toFixed(3);
                if (isNaN(txtcnfCommAct)) {
                    txtcnfCommAct = 0;
                }
                txtOthChargeAct = document.getElementById("<%=txtOthChargeAct.ClientID%>").value;
                txtOthChargeAct = parseFloat(txtOthChargeAct.replaceAll(',', ''));
                //var txtOthChargeActDiv = parseFloat(txtOthChargeAct / ItemCount).toFixed(3);
                if (isNaN(txtOthChargeAct)) {
                    txtOthChargeAct = 0;
                }
                txtTransportAct = document.getElementById("<%=txtTransportAct.ClientID%>").value;
                txtTransportAct = parseFloat(txtTransportAct.replaceAll(',', ''));
                //var txtTransportActDiv = parseFloat(txtTransportAct / ItemCount).toFixed(3);
                if (isNaN(txtTransportAct)) {
                    txtTransportAct = 0;
                }
                txtMiscellaneousAct = document.getElementById("<%=txtMiscellaneousAct.ClientID%>").value;
                txtMiscellaneousAct = parseFloat(txtMiscellaneousAct.replaceAll(',', ''));
                //var txtMiscellaneousActDiv = parseFloat(txtMiscellaneousAct / ItemCount).toFixed(3);
                if (isNaN(txtMiscellaneousAct)) {
                    txtMiscellaneousAct = 0;
                }

                //TotalOthChargeActDiv = parseFloat(txtShippingChargeActDiv) + parseFloat(txtNocChargeActDiv) + parseFloat(txtBOperatorChargeActDiv) + parseFloat(txtSpPerChargeActDiv) + parseFloat(txtShaftaChargeActDiv) + parseFloat(txtcnfCommActDiv) + parseFloat(txtOthChargeActDiv) + parseFloat(txtTransportActDiv) + parseFloat(txtMiscellaneousActDiv);

                TotalOthChargeAct = txtShippingChargeAct + txtNocChargeAct + txtBOperatorChargeAct + txtSpPerChargeAct + txtShaftaChargeAct + txtcnfCommAct + txtOthChargeAct + txtTransportAct + txtMiscellaneousAct;

                //TotalChargeActDiv = parseFloat(TotalPortChargeActDiv) + parseFloat(TotalOthChargeActDiv);

                TotalChargeAct = TotalPortChargeAct + TotalOthChargeAct;
                if (!isNaN(TotalChargeAct)) {
                    document.getElementById("<%=txtTotClearingChargeAct.ClientID%>").value = TotalChargeAct.toFixed(2);
                }

                txtSeaFreightAct = document.getElementById("<%=txtSeaFreightAct.ClientID%>").value;
                txtSeaFreightAct = parseFloat(txtSeaFreightAct.replaceAll(',', ''));
                //var txtSeaFreightActDiv = parseFloat(txtSeaFreightAct / ItemCount).toFixed(3);
                if (isNaN(txtSeaFreightAct)) {
                    txtSeaFreightAct = 0;
                }


                //TotWithVatAct = AssesValueAct + TotalTaxAct + parseFloat(TotalChargeActDiv) + parseFloat(txtSeaFreightActDiv);// + txtTransport + txtMiscellaneous;

                //TotWithVatAct = AssesValueAct + TotalTaxAct + TotalChargeAct + parseFloat(txtSeaFreightAct);
                TotWithVatAct = parseFloat(txtDocumentValueAct) + parseFloat(txtMarineInsuranceAct) + TotalTaxAct + TotalChargeAct + parseFloat(txtSeaFreightAct);
                
                if (!isNaN(TotWithVatAct)) {
                    document.getElementById("<%=txtTotWithVatAct.ClientID%>").value = TotWithVatAct.toFixed(2);
                }

                TotVatAct = txtVatAct + txtAitAct + txtAtAct;
                TotWOVatAct = TotWithVatAct - TotVatAct;
                if (!isNaN(TotWOVatAct)) {
                    document.getElementById("<%=txtTotWithoutVatAct.ClientID%>").value = TotWOVatAct.toFixed(2);
                }

                factorAct = TotWOVatAct / documentValueAct;
                if (!isNaN(factorAct)) {
                    document.getElementById("<%=txtFactorAct.ClientID%>").value = factorAct.toFixed(2);
                }

                txtItemQtyAct = parseFloat(document.getElementById("<%=txtItemQtyAct.ClientID%>").value);
                if (isNaN(txtItemQtyAct)) {
                    txtItemQtyAct = 0;
                }

                txtConvertedItemQtyAct = parseFloat(document.getElementById("<%=txtConvertedItemQtyAct.ClientID%>").value);
                if (isNaN(txtConvertedItemQtyAct)) {
                    txtConvertedItemQtyAct = 0;
                }
                RateAct = TotWOVatAct / txtConvertedItemQtyAct;

                if (!isNaN(RateAct)) {
                    document.getElementById("<%=txtRateAct.ClientID%>").value = RateAct.toFixed(4);
                }

            }
            else
            {
                var ItemCount = document.getElementById("<%=lblItemCount.ClientID%>").value;
                var txtItemName = document.getElementById("<%=txtItemName.ClientID%>").value;
                if (txtItemName == "") {
                    alert("Please Select Item !!");
                    document.getElementById("<%=txtItemName.ClientID%>").focus();
                    SetDefaultValue();
                    return false;
                }


                var txtCommercialValueAct = document.getElementById("<%=txtCommercialValueAct.ClientID%>").value;
                txtCommercialValueAct = parseFloat(txtCommercialValueAct.replaceAll(',', ''));
                if (isNaN(txtCommercialValueAct)) {
                    txtCommercialValueAct = 0;
                }
                var txtConversionRateAct = document.getElementById("<%=txtConversionRateAct.ClientID%>").value;
                txtConversionRateAct = parseFloat(txtConversionRateAct.replaceAll(',', ''));
                if (isNaN(txtConversionRateAct)) {
                    txtConversionRateAct = 0;
                }
                documentValueAct = txtCommercialValueAct * txtConversionRateAct;
                if (!isNaN(documentValueAct)) {
                    document.getElementById("<%=txtDocumentValueAct.ClientID%>").value = documentValueAct.toFixed(2);

                }

                var txtDocumentValueAct = parseFloat(document.getElementById("<%=txtDocumentValueAct.ClientID%>").value);

                var txtMarineInsuranceAct = document.getElementById("<%=txtMarineInsuranceAct.ClientID%>").value;
                txtMarineInsuranceAct = parseFloat(txtMarineInsuranceAct.replaceAll(',', ''));
                if (isNaN(txtMarineInsuranceAct)) {
                    txtMarineInsuranceAct = 0;
                }
                var txtInsuranceOthAct = document.getElementById("<%=txtInsuranceOthAct.ClientID%>").value;
                txtInsuranceOthAct = parseFloat(txtInsuranceOthAct.replaceAll(',', ''));
                if (isNaN(txtInsuranceOthAct)) {
                    txtInsuranceOthAct = 0;
                }
                var AssesValueAct = txtDocumentValueAct + txtMarineInsuranceAct + txtInsuranceOthAct;
                if (!isNaN(AssesValueAct)) {
                    document.getElementById("<%=txtAssessableValueAct.ClientID%>").value = AssesValueAct.toFixed(2);
                }

                var txtGlobalTaxAct = document.getElementById("<%=txtGlobalTaxAct.ClientID%>").value;
                txtGlobalTaxAct = parseFloat(txtGlobalTaxAct.replaceAll(',', ''));
                txtGlobalTaxesActDiv = parseFloat(txtGlobalTaxAct / ItemCount).toFixed(3);
                if (isNaN(txtGlobalTaxAct)) {
                    txtGlobalTaxAct = 0;
                }
                var txtCdAct = document.getElementById("<%=txtCdAct.ClientID%>").value;
                txtCdAct = parseFloat(txtCdAct.replaceAll(',', ''));
                if (isNaN(txtCdAct)) {
                    txtCdAct = 0;
                }
                var txtRdAct = document.getElementById("<%=txtRdAct.ClientID%>").value;
                txtRdAct = parseFloat(txtRdAct.replaceAll(',', ''));
                if (isNaN(txtRdAct)) {
                    txtRdAct = 0;
                }
                var txtSdAct = document.getElementById("<%=txtSdAct.ClientID%>").value;
                txtSdAct = parseFloat(txtSdAct.replaceAll(',', ''));
                if (isNaN(txtSdAct)) {
                    txtSdAct = 0;
                }
                var txtVatAct = document.getElementById("<%=txtVatAct.ClientID%>").value;
                txtVatAct = parseFloat(txtVatAct.replaceAll(',', ''));
                if (isNaN(txtVatAct)) {
                    txtVatAct = 0;
                }
                var txtAitAct = document.getElementById("<%=txtAitAct.ClientID%>").value;
                txtAitAct = parseFloat(txtAitAct.replaceAll(',', ''));
                if (isNaN(txtAitAct)) {
                    txtAitAct = 0;
                }
                var txtAtAct = document.getElementById("<%=txtAtAct.ClientID%>").value;
                txtAtAct = parseFloat(txtAtAct.replaceAll(',', ''));
                if (isNaN(txtAtAct)) {
                    txtAtAct = 0;
                }

                var TotalItemTaxAct = txtCdAct + txtRdAct + txtSdAct + txtVatAct + txtAitAct + txtAtAct;
                if (!isNaN(TotalItemTaxAct)) {
                    document.getElementById("<%=txtTotItemTaxAct.ClientID%>").value = TotalItemTaxAct.toFixed(2);
                }

                //var TotalTaxAct = txtGlobalTaxAct + TotalItemTaxAct;
                var TotalTaxAct = parseFloat(txtGlobalTaxesActDiv) + TotalItemTaxAct;

                if (!isNaN(TotalTaxAct)) {
                    document.getElementById("<%=txtTotTaxesAmtAct.ClientID%>").value = TotalTaxAct.toFixed(2);
                }

                var txtPortChargeAct = document.getElementById("<%=txtPortChargeAct.ClientID%>").value;
                txtPortChargeAct = parseFloat(txtPortChargeAct.replaceAll(',', ''));
                var txtPortChargeActDiv = parseFloat(txtPortChargeAct / ItemCount).toFixed(3);
                if (isNaN(txtPortChargeAct)) {
                    txtPortChargeAct = 0;
                }
                var txtAddVatAct = document.getElementById("<%=txtAddVatAct.ClientID%>").value;
                txtAddVatAct = parseFloat(txtAddVatAct.replaceAll(',', ''));
                var txtAddVatActDiv = parseFloat(txtAddVatAct / ItemCount).toFixed(3);
                if (isNaN(txtAddVatAct)) {
                    txtAddVatAct = 0;
                }
                var txtPortAitAct = document.getElementById("<%=txtPortAitAct.ClientID%>").value;
                txtPortAitAct = parseFloat(txtPortAitAct.replaceAll(',', ''));
                var txtPortAitActDiv = parseFloat(txtPortAitAct / ItemCount).toFixed(3);
                if (isNaN(txtPortAitAct)) {
                    txtPortAitAct = 0;
                }
                var txtOthersAct = document.getElementById("<%=txtOthersAct.ClientID%>").value;
                txtOthersAct = parseFloat(txtOthersAct.replaceAll(',', ''));
                var txtOthersActDiv = parseFloat(txtOthersAct / ItemCount).toFixed(3);
                if (isNaN(txtOthersAct)) {
                    txtOthersAct = 0;
                }

                var TotalPortChargeActDiv = parseFloat(txtPortChargeActDiv) + parseFloat(txtAddVatActDiv) + parseFloat(txtPortAitActDiv) + parseFloat(txtOthersActDiv);

                var TotalPortChargeAct = txtPortChargeAct + txtAddVatAct + txtPortAitAct + txtOthersAct;
                if (!isNaN(TotalPortChargeAct)) {
                    document.getElementById("<%=txtTotPortChargeAct.ClientID%>").value = TotalPortChargeAct.toFixed(2);
                }

                txtShippingChargeAct = document.getElementById("<%=txtShippingChargeAct.ClientID%>").value;
                txtShippingChargeAct = parseFloat(txtShippingChargeAct.replaceAll(',', ''));
                var txtShippingChargeActDiv = parseFloat(txtShippingChargeAct / ItemCount).toFixed(3);
                if (isNaN(txtShippingChargeAct)) {
                    txtShippingChargeAct = 0;
                }
                txtNocChargeAct = document.getElementById("<%=txtNocChargeAct.ClientID%>").value;
                txtNocChargeAct = parseFloat(txtNocChargeAct.replaceAll(',', ''));
                var txtNocChargeActDiv = parseFloat(txtNocChargeAct / ItemCount).toFixed(3);
                if (isNaN(txtNocChargeAct)) {
                    txtNocChargeAct = 0;
                }
                txtBOperatorChargeAct = document.getElementById("<%=txtBOperatorChargeAct.ClientID%>").value;
                txtBOperatorChargeAct = parseFloat(txtBOperatorChargeAct.replaceAll(',', ''));
                var txtBOperatorChargeActDiv = parseFloat(txtBOperatorChargeAct / ItemCount).toFixed(3);
                if (isNaN(txtBOperatorChargeAct)) {
                    txtBOperatorChargeAct = 0;
                }
                txtSpPerChargeAct = document.getElementById("<%=txtSpPerChargeAct.ClientID%>").value;
                txtSpPerChargeAct = parseFloat(txtSpPerChargeAct.replaceAll(',', ''));
                var txtSpPerChargeActDiv = parseFloat(txtSpPerChargeAct / ItemCount).toFixed(3);
                if (isNaN(txtSpPerChargeAct)) {
                    txtSpPerChargeAct = 0;
                }
                txtShaftaChargeAct = document.getElementById("<%=txtShaftaChargeAct.ClientID%>").value;
                txtShaftaChargeAct = parseFloat(txtShaftaChargeAct.replaceAll(',', ''));
                var txtShaftaChargeActDiv = parseFloat(txtShaftaChargeAct / ItemCount).toFixed(3);
                if (isNaN(txtShaftaChargeAct)) {
                    txtShaftaChargeAct = 0;
                }
                txtcnfCommAct = document.getElementById("<%=txtcnfCommAct.ClientID%>").value;
                txtcnfCommAct = parseFloat(txtcnfCommAct.replaceAll(',', ''));
                var txtcnfCommActDiv = parseFloat(txtcnfCommAct / ItemCount).toFixed(3);
                if (isNaN(txtcnfCommAct)) {
                    txtcnfCommAct = 0;
                }
                txtOthChargeAct = document.getElementById("<%=txtOthChargeAct.ClientID%>").value;
                txtOthChargeAct = parseFloat(txtOthChargeAct.replaceAll(',', ''));
                var txtOthChargeActDiv = parseFloat(txtOthChargeAct / ItemCount).toFixed(3);
                if (isNaN(txtOthChargeAct)) {
                    txtOthChargeAct = 0;
                }
                txtTransportAct = document.getElementById("<%=txtTransportAct.ClientID%>").value;
                txtTransportAct = parseFloat(txtTransportAct.replaceAll(',', ''));
                //var txtTransportActDiv = parseFloat(txtTransportAct / ItemCount).toFixed(3);
                if (isNaN(txtTransportAct)) {
                    txtTransportAct = 0;
                }
                txtMiscellaneousAct = document.getElementById("<%=txtMiscellaneousAct.ClientID%>").value;
                txtMiscellaneousAct = parseFloat(txtMiscellaneousAct.replaceAll(',', ''));
                var txtMiscellaneousActDiv = parseFloat(txtMiscellaneousAct / ItemCount).toFixed(3);
                if (isNaN(txtMiscellaneousAct)) {
                    txtMiscellaneousAct = 0;
                }

                TotalOthChargeActDiv = parseFloat(txtShippingChargeActDiv) + parseFloat(txtNocChargeActDiv) + parseFloat(txtBOperatorChargeActDiv) + parseFloat(txtSpPerChargeActDiv) + parseFloat(txtShaftaChargeActDiv) + parseFloat(txtcnfCommActDiv) + parseFloat(txtOthChargeActDiv) + parseFloat(txtTransportActDiv) + parseFloat(txtMiscellaneousActDiv);

                TotalOthChargeAct = txtShippingChargeAct + txtNocChargeAct + txtBOperatorChargeAct + txtSpPerChargeAct + txtShaftaChargeAct + txtcnfCommAct + txtOthChargeAct + txtTransportAct + txtMiscellaneousAct;

                TotalChargeActDiv = parseFloat(TotalPortChargeActDiv) + parseFloat(TotalOthChargeActDiv);

                TotalChargeAct = TotalPortChargeAct + TotalOthChargeAct;
                if (!isNaN(TotalChargeAct)) {
                    document.getElementById("<%=txtTotClearingChargeAct.ClientID%>").value = TotalChargeAct.toFixed(2);
                }

                txtSeaFreightAct = document.getElementById("<%=txtSeaFreightAct.ClientID%>").value;
                txtSeaFreightAct = parseFloat(txtSeaFreightAct.replaceAll(',', ''));
                var txtSeaFreightActDiv = parseFloat(txtSeaFreightAct / ItemCount).toFixed(3);
                if (isNaN(txtSeaFreightAct)) {
                    txtSeaFreightAct = 0;
                }


                //TotWithVatAct = AssesValueAct + TotalTaxAct + parseFloat(TotalChargeActDiv) + parseFloat(txtSeaFreightActDiv);270723// + txtTransport + txtMiscellaneous;
                TotWithVatAct = parseFloat(txtDocumentValueAct) + parseFloat(txtMarineInsuranceAct) + TotalTaxAct + parseFloat(TotalChargeActDiv) + parseFloat(txtSeaFreightActDiv);
                
                //TotWithVatAct = AssesValueAct + TotalTaxAct + TotalChargeAct + parseFloat(txtSeaFreightAct);
                if (!isNaN(TotWithVatAct)) {
                    document.getElementById("<%=txtTotWithVatAct.ClientID%>").value = TotWithVatAct.toFixed(2);
                }

                TotVatAct = txtVatAct + txtAitAct + txtAtAct;
                TotWOVatAct = TotWithVatAct - TotVatAct;
                if (!isNaN(TotWOVatAct)) {
                    document.getElementById("<%=txtTotWithoutVatAct.ClientID%>").value = TotWOVatAct.toFixed(2);
                }

                factorAct = TotWOVatAct / documentValueAct;
                if (!isNaN(factorAct)) {
                    document.getElementById("<%=txtFactorAct.ClientID%>").value = factorAct.toFixed(2);
                }

                txtItemQtyAct = parseFloat(document.getElementById("<%=txtItemQtyAct.ClientID%>").value);
                if (isNaN(txtItemQtyAct)) {
                    txtItemQtyAct = 0;
                }

                txtConvertedItemQtyAct = parseFloat(document.getElementById("<%=txtConvertedItemQtyAct.ClientID%>").value);
                if (isNaN(txtConvertedItemQtyAct)) {
                    txtConvertedItemQtyAct = 0;
                }
                RateAct = TotWOVatAct / txtConvertedItemQtyAct;
                //alert(RateAct);
                if (!isNaN(RateAct)) {
                    document.getElementById("<%=txtRateAct.ClientID%>").value = RateAct.toFixed(4);
                }
            }
        }

        function calculateDifference() {

            var txtGlobalTaxes = document.getElementById("<%=txtGlobalTaxes.ClientID%>").value;
            txtGlobalTaxes = parseFloat(txtGlobalTaxes.replaceAll(',', ''));
            if (isNaN(txtGlobalTaxes)) {
                txtGlobalTaxes = 0;
            }
            var txtGlobalTaxAct = document.getElementById("<%=txtGlobalTaxAct.ClientID%>").value;
            txtGlobalTaxAct = parseFloat(txtGlobalTaxAct.replaceAll(',', ''));
            if (isNaN(txtGlobalTaxAct)) {
                txtGlobalTaxAct = 0;
            }
            diffGlobalTax = txtGlobalTaxAct - txtGlobalTaxes;
            if (!isNaN(diffGlobalTax)) {
                document.getElementById("<%=lbldifGlobalTax.ClientID%>").innerText = diffGlobalTax.toFixed(2);
            }

            var txtCnfPrice = document.getElementById("<%=txtCnfPrice.ClientID%>").value;
            txtCnfPrice = parseFloat(txtCnfPrice.replaceAll(',', ''));
            if (isNaN(txtCnfPrice)) {
                txtCnfPrice = 0;
            }
            var txtCommercialValueAct = document.getElementById("<%=txtCommercialValueAct.ClientID%>").value;
            txtCommercialValueAct = parseFloat(txtCommercialValueAct.replaceAll(',', ''));
            if (isNaN(txtCommercialValueAct)) {
                txtCommercialValueAct = 0;
            }
            diffCommercialValue = txtCommercialValueAct - txtCnfPrice;
            if (!isNaN(diffCommercialValue)) {
                document.getElementById("<%=lblDiffCommercialValue.ClientID%>").innerText = diffCommercialValue.toFixed(2);
            }

            var txtConversionRate = document.getElementById("<%=txtConversionRate.ClientID%>").value;
            txtConversionRate = parseFloat(txtConversionRate.replaceAll(',', ''));
            if (isNaN(txtConversionRate)) {
                txtConversionRate = 0;
            }
            var txtConversionRateAct = document.getElementById("<%=txtConversionRateAct.ClientID%>").value;
            txtConversionRateAct = parseFloat(txtConversionRateAct.replaceAll(',', ''));
            if (isNaN(txtConversionRateAct)) {
                txtConversionRateAct = 0;
            }
            diffConversionRate = txtConversionRateAct - txtConversionRate;
            if (!isNaN(diffConversionRate)) {
                document.getElementById("<%=lblDiffConversionRate.ClientID%>").innerText = diffConversionRate.toFixed(2);
            }

            var txtDocumentValue = document.getElementById("<%=txtDocumentValue.ClientID%>").value;
            txtDocumentValue = parseFloat(txtDocumentValue.replaceAll(',', ''));
            if (isNaN(txtDocumentValue)) {
                txtDocumentValue = 0;
            }
            var txtDocumentValueAct = document.getElementById("<%=txtDocumentValueAct.ClientID%>").value;
            txtDocumentValueAct = parseFloat(txtDocumentValueAct.replaceAll(',', ''));
            if (isNaN(txtDocumentValueAct)) {
                txtDocumentValueAct = 0;
            }
            diffDocumentValue = txtDocumentValueAct - txtDocumentValue;
            if (!isNaN(diffDocumentValue)) {
                document.getElementById("<%=lblDiffDocumentValue.ClientID%>").innerText = diffDocumentValue.toFixed(2);
            }


            var txtMarineInsurance = document.getElementById("<%=txtMarineInsurance.ClientID%>").value;
            txtMarineInsurance = parseFloat(txtMarineInsurance.replaceAll(',', ''));
            if (isNaN(txtMarineInsurance)) {
                txtMarineInsurance = 0;
            }
            var txtMarineInsuranceAct = document.getElementById("<%=txtMarineInsuranceAct.ClientID%>").value;
            txtMarineInsuranceAct = parseFloat(txtMarineInsuranceAct.replaceAll(',', ''));
            if (isNaN(txtMarineInsuranceAct)) {
                txtMarineInsuranceAct = 0;
            }
            diffMarineInsurance = txtMarineInsuranceAct - txtMarineInsurance;
            if (!isNaN(diffMarineInsurance)) {
                document.getElementById("<%=lblDiffMarineInsurance.ClientID%>").innerText = diffMarineInsurance.toFixed(2);
            }


            var txtInsuranceAndOth = document.getElementById("<%=txtInsuranceAndOth.ClientID%>").value;
            txtInsuranceAndOth = parseFloat(txtInsuranceAndOth.replaceAll(',', ''));
            if (isNaN(txtInsuranceAndOth)) {
                txtInsuranceAndOth = 0;
            }
            var txtInsuranceOthAct = document.getElementById("<%=txtInsuranceOthAct.ClientID%>").value;
            txtInsuranceOthAct = parseFloat(txtInsuranceOthAct.replaceAll(',', ''));
            if (isNaN(txtInsuranceOthAct)) {
                txtInsuranceOthAct = 0;
            }
            diffInsuranceAndOth = txtInsuranceOthAct - txtInsuranceAndOth;
            if (!isNaN(diffInsuranceAndOth)) {
                document.getElementById("<%=lblDiffInsuranceOth.ClientID%>").innerText = diffInsuranceAndOth.toFixed(2);
            }


            var txtAssessableValue = document.getElementById("<%=txtAssessableValue.ClientID%>").value;
            txtAssessableValue = parseFloat(txtAssessableValue.replaceAll(',', ''));
            if (isNaN(txtAssessableValue)) {
                txtAssessableValue = 0;
            }
            var txtAssessableValueAct = document.getElementById("<%=txtAssessableValueAct.ClientID%>").value;
            txtAssessableValueAct = parseFloat(txtAssessableValueAct.replaceAll(',', ''));
            if (isNaN(txtAssessableValueAct)) {
                txtAssessableValueAct = 0;
            }
            diffAssessableValue = txtAssessableValueAct - txtAssessableValue;
            if (!isNaN(diffAssessableValue)) {
                document.getElementById("<%=lblDiffAssessableValue.ClientID%>").innerText = diffAssessableValue.toFixed(2);
            }


            var txtCd = document.getElementById("<%=txtCd.ClientID%>").value;
            txtCd = parseFloat(txtCd.replaceAll(',', ''));
            if (isNaN(txtCd)) {
                txtCd = 0;
            }
            var txtCdAct = document.getElementById("<%=txtCdAct.ClientID%>").value;
            txtCdAct = parseFloat(txtCdAct.replaceAll(',', ''));
            if (isNaN(txtCdAct)) {
                txtCdAct = 0;
            }
            diffCd = txtCdAct - txtCd;
            if (!isNaN(diffCd)) {
                document.getElementById("<%=lblDiffCD.ClientID%>").innerText = diffCd.toFixed(2);
            }


            var txtRd = document.getElementById("<%=txtRd.ClientID%>").value;
            txtRd = parseFloat(txtRd.replaceAll(',', ''));
            if (isNaN(txtRd)) {
                txtRd = 0;
            }
            var txtRdAct = document.getElementById("<%=txtRdAct.ClientID%>").value;
            txtRdAct = parseFloat(txtRdAct.replaceAll(',', ''));
            if (isNaN(txtRdAct)) {
                txtRdAct = 0;
            }
            diffRd = txtRdAct - txtRd;
            if (!isNaN(diffRd)) {
                document.getElementById("<%=lblDiffRD.ClientID%>").innerText = diffRd.toFixed(2);
            }



            var txtSd = document.getElementById("<%=txtSd.ClientID%>").value;
            txtSd = parseFloat(txtSd.replaceAll(',', ''));
            if (isNaN(txtSd)) {
                txtSd = 0;
            }
            var txtSdAct = document.getElementById("<%=txtSdAct.ClientID%>").value;
            txtSdAct = parseFloat(txtSdAct.replaceAll(',', ''));
            if (isNaN(txtSdAct)) {
                txtSdAct = 0;
            }
            diffSd = txtSdAct - txtSd;
            if (!isNaN(diffSd)) {
                document.getElementById("<%=lblDiffSD.ClientID%>").innerText = diffSd.toFixed(2);
            }

            var txtVat = document.getElementById("<%=txtVat.ClientID%>").value;
            txtVat = parseFloat(txtVat.replaceAll(',', ''));
            if (isNaN(txtVat)) {
                txtVat = 0;
            }
            var txtVatAct = document.getElementById("<%=txtVatAct.ClientID%>").value;
            txtVatAct = parseFloat(txtVatAct.replaceAll(',', ''));
            if (isNaN(txtVatAct)) {
                txtVatAct = 0;
            }
            diffVat = txtVatAct - txtVat;
            if (!isNaN(diffVat)) {
                document.getElementById("<%=lblDiffVat.ClientID%>").innerText = diffVat.toFixed(2);
            }

            var txtAit = document.getElementById("<%=txtAit.ClientID%>").value;
            txtAit = parseFloat(txtAit.replaceAll(',', ''));
            if (isNaN(txtAit)) {
                txtAit = 0;
            }
            var txtAitAct = document.getElementById("<%=txtAitAct.ClientID%>").value;
            txtAitAct = parseFloat(txtAitAct.replaceAll(',', ''));
            if (isNaN(txtAitAct)) {
                txtAitAct = 0;
            }
            diffAit = txtAitAct - txtAit;
            if (!isNaN(diffAit)) {
                document.getElementById("<%=lblDiffAit.ClientID%>").innerText = diffAit.toFixed(2);
            }
            


            var txtAt = document.getElementById("<%=txtAt.ClientID%>").value;
            txtAt = parseFloat(txtAt.replaceAll(',', ''));
            if (isNaN(txtAt)) {
                txtAt = 0;
            }
            var txtAtAct = document.getElementById("<%=txtAtAct.ClientID%>").value;
            txtAtAct = parseFloat(txtAtAct.replaceAll(',', ''));
            if (isNaN(txtAtAct)) {
                txtAtAct = 0;
            }
            diffAt = txtAtAct - txtAt;
            if (!isNaN(diffAt)) {
                document.getElementById("<%=lblDiffAt.ClientID%>").innerText = diffAt.toFixed(2);
            }



            var txtTotalItemTax = document.getElementById("<%=txtTotalItemTax.ClientID%>").value;
            txtTotalItemTax = parseFloat(txtTotalItemTax.replaceAll(',', ''));
            if (isNaN(txtTotalItemTax)) {
                txtTotalItemTax = 0;
            }
            var txtTotItemTaxAct = document.getElementById("<%=txtTotItemTaxAct.ClientID%>").value;
            txtTotItemTaxAct = parseFloat(txtTotItemTaxAct.replaceAll(',', ''));
            if (isNaN(txtTotItemTaxAct)) {
                txtTotItemTaxAct = 0;
            }
            diffTotalItemTax = txtTotItemTaxAct - txtTotalItemTax;
            if (!isNaN(diffTotalItemTax)) {
                document.getElementById("<%=lblDiffTotItemTax.ClientID%>").innerText = diffTotalItemTax.toFixed(2);
            }

            var txtTotalTaxes = document.getElementById("<%=txtTotalTaxes.ClientID%>").value;
            txtTotalTaxes = parseFloat(txtTotalTaxes.replaceAll(',', ''));
            if (isNaN(txtTotalTaxes)) {
                txtTotalTaxes = 0;
            }
            var txtTotTaxesAmtAct = document.getElementById("<%=txtTotTaxesAmtAct.ClientID%>").value;
            txtTotTaxesAmtAct = parseFloat(txtTotTaxesAmtAct.replaceAll(',', ''));
            if (isNaN(txtTotTaxesAmtAct)) {
                txtTotTaxesAmtAct = 0;
            }
            diffTotalTaxes = txtTotTaxesAmtAct - txtTotalTaxes;
            if (!isNaN(diffTotalTaxes)) {
                document.getElementById("<%=lblDiffTotTaxAmt.ClientID%>").innerText = diffTotalTaxes.toFixed(2);
            }


            var txtPortCharge = document.getElementById("<%=txtPortCharge.ClientID%>").value;
            txtPortCharge = parseFloat(txtPortCharge.replaceAll(',', ''));
            if (isNaN(txtPortCharge)) {
                txtPortCharge = 0;
            }
            var txtPortChargeAct = document.getElementById("<%=txtPortChargeAct.ClientID%>").value;
            txtPortChargeAct = parseFloat(txtPortChargeAct.replaceAll(',', ''));
            if (isNaN(txtPortChargeAct)) {
                txtPortChargeAct = 0;
            }
            diffPortCharge = txtPortChargeAct - txtPortCharge;
            if (!isNaN(diffPortCharge)) {
                document.getElementById("<%=dfPch.ClientID%>").innerText = diffPortCharge.toFixed(2);
            }


            var txtAddVat = document.getElementById("<%=txtAddVat.ClientID%>").value;
            txtAddVat = parseFloat(txtAddVat.replaceAll(',', ''));
            if (isNaN(txtAddVat)) {
                txtAddVat = 0;
            }
            var txtAddVatAct = document.getElementById("<%=txtAddVatAct.ClientID%>").value;
            txtAddVatAct = parseFloat(txtAddVatAct.replaceAll(',', ''));
            if (isNaN(txtAddVatAct)) {
                txtAddVatAct = 0;
            }
            diffAddVat = txtAddVatAct - txtAddVat;
            if (!isNaN(diffAddVat)) {
                document.getElementById("<%=dfAvt.ClientID%>").innerText = diffAddVat.toFixed(2);
            }


            var txtPortAit = document.getElementById("<%=txtPortAit.ClientID%>").value;
            txtPortAit = parseFloat(txtPortAit.replaceAll(',', ''));
            if (isNaN(txtPortAit)) {
                txtPortAit = 0;
            }
            var txtPortAitAct = document.getElementById("<%=txtPortAitAct.ClientID%>").value;
            txtPortAitAct = parseFloat(txtPortAitAct.replaceAll(',', ''));
            if (isNaN(txtPortAitAct)) {
                txtPortAitAct = 0;
            }
            diffPortAit = txtPortAitAct - txtPortAit;
            if (!isNaN(diffPortAit)) {
                document.getElementById("<%=dfPAit.ClientID%>").innerText = diffPortAit.toFixed(2);
            }


            var txtOthers = document.getElementById("<%=txtOthers.ClientID%>").value;
            txtOthers = parseFloat(txtOthers.replaceAll(',', ''));
            if (isNaN(txtOthers)) {
                txtOthers = 0;
            }
            var txtOthersAct = document.getElementById("<%=txtOthersAct.ClientID%>").value;
            txtOthersAct = parseFloat(txtOthersAct.replaceAll(',', ''));
            if (isNaN(txtOthersAct)) {
                txtOthersAct = 0;
            }
            diffOthers = txtOthersAct - txtOthers;
            if (!isNaN(diffOthers)) {
                document.getElementById("<%=dfOth.ClientID%>").innerText = diffOthers.toFixed(2);
            }

            var txtTotPortCharge = document.getElementById("<%=txtTotPortCharge.ClientID%>").value;
            txtTotPortCharge = parseFloat(txtTotPortCharge.replaceAll(',', ''));
            if (isNaN(txtTotPortCharge)) {
                txtTotPortCharge = 0;
            }
            var txtTotPortChargeAct = document.getElementById("<%=txtTotPortChargeAct.ClientID%>").value;
            txtTotPortChargeAct = parseFloat(txtTotPortChargeAct.replaceAll(',', ''));
            if (isNaN(txtTotPortChargeAct)) {
                txtTotPortChargeAct = 0;
            }
            diffTotPortCharge = txtTotPortChargeAct - txtTotPortCharge;
            if (!isNaN(diffTotPortCharge)) {
                document.getElementById("<%=dfTPch.ClientID%>").innerText = diffTotPortCharge.toFixed(2);
            }


            var txtShippingCharge = document.getElementById("<%=txtShippingCharge.ClientID%>").value;
            txtShippingCharge = parseFloat(txtShippingCharge.replaceAll(',', ''));
            if (isNaN(txtShippingCharge)) {
                txtShippingCharge = 0;
            }
            var txtShippingChargeAct = document.getElementById("<%=txtShippingChargeAct.ClientID%>").value;
            txtShippingChargeAct = parseFloat(txtShippingChargeAct.replaceAll(',', ''));
            if (isNaN(txtShippingChargeAct)) {
                txtShippingChargeAct = 0;
            }
            diffShippingCharge = txtShippingChargeAct - txtShippingCharge;
            if (!isNaN(diffShippingCharge)) {
                document.getElementById("<%=dfSch.ClientID%>").innerText = diffShippingCharge.toFixed(2);
            }


            var txtNocCharge = document.getElementById("<%=txtNocCharge.ClientID%>").value;
            txtNocCharge = parseFloat(txtNocCharge.replaceAll(',', ''));
            if (isNaN(txtNocCharge)) {
                txtNocCharge = 0;
            }
            var txtNocChargeAct = document.getElementById("<%=txtNocChargeAct.ClientID%>").value;
            txtNocChargeAct = parseFloat(txtNocChargeAct.replaceAll(',', ''));
            if (isNaN(txtNocChargeAct)) {
                txtNocChargeAct = 0;
            }
            diffNocCharge = txtNocChargeAct - txtNocCharge;
            if (!isNaN(diffNocCharge)) {
                document.getElementById("<%=dfNoc.ClientID%>").innerText = diffNocCharge.toFixed(2);
            }


            var txtSaftaCharge = document.getElementById("<%=txtSaftaCharge.ClientID%>").value;
            txtSaftaCharge = parseFloat(txtSaftaCharge.replaceAll(',', ''));
            if (isNaN(txtSaftaCharge)) {
                txtSaftaCharge = 0;
            }
            var txtShaftaChargeAct = document.getElementById("<%=txtShaftaChargeAct.ClientID%>").value;
            txtShaftaChargeAct = parseFloat(txtShaftaChargeAct.replaceAll(',', ''));
            if (isNaN(txtShaftaChargeAct)) {
                txtShaftaChargeAct = 0;
            }
            diffSaftaCharge = txtShaftaChargeAct - txtSaftaCharge;
            if (!isNaN(diffSaftaCharge)) {
                document.getElementById("<%=dfsftch.ClientID%>").innerText = diffSaftaCharge.toFixed(2);
            }


            var txtTransport = document.getElementById("<%=txtTransport.ClientID%>").value;
            txtTransport = parseFloat(txtTransport.replaceAll(',', ''));
            if (isNaN(txtTransport)) {
                txtTransport = 0;
            }
            var txtTransportAct = document.getElementById("<%=txtTransportAct.ClientID%>").value;
            txtTransportAct = parseFloat(txtTransportAct.replaceAll(',', ''));
            if (isNaN(txtTransportAct)) {
                txtTransportAct = 0;
            }
            diffTransport = txtTransportAct - txtTransport;
            if (!isNaN(diffTransport)) {
                document.getElementById("<%=dfTr.ClientID%>").innerText = diffTransport.toFixed(2);
            }


            var txtCnfCommision = document.getElementById("<%=txtCnfCommision.ClientID%>").value;
            txtCnfCommision = parseFloat(txtCnfCommision.replaceAll(',', ''));
            if (isNaN(txtCnfCommision)) {
                txtCnfCommision = 0;
            }
            var txtcnfCommAct = document.getElementById("<%=txtcnfCommAct.ClientID%>").value;
            txtcnfCommAct = parseFloat(txtcnfCommAct.replaceAll(',', ''));
            if (isNaN(txtcnfCommAct)) {
                txtcnfCommAct = 0;
            }
            diffCnfCommision = txtcnfCommAct - txtCnfCommision;
            if (!isNaN(diffCnfCommision)) {
                document.getElementById("<%=dfCnfcom.ClientID%>").innerText = diffCnfCommision.toFixed(2);
            }


            var txtOthersCharge = document.getElementById("<%=txtOthersCharge.ClientID%>").value;
            txtOthersCharge = parseFloat(txtOthersCharge.replaceAll(',', ''));
            if (isNaN(txtOthersCharge)) {
                txtOthersCharge = 0;
            }
            var txtOthChargeAct = document.getElementById("<%=txtOthChargeAct.ClientID%>").value;
            txtOthChargeAct = parseFloat(txtOthChargeAct.replaceAll(',', ''));
            if (isNaN(txtOthChargeAct)) {
                txtOthChargeAct = 0;
            }
            diffOthersCharge = txtOthChargeAct - txtOthersCharge;
            if (!isNaN(diffOthersCharge)) {
                document.getElementById("<%=dfothch.ClientID%>").innerText = diffOthersCharge.toFixed(2);
            }


            var txtBOperatorCharge = document.getElementById("<%=txtBOperatorCharge.ClientID%>").value;
            txtBOperatorCharge = parseFloat(txtBOperatorCharge.replaceAll(',', ''));
            if (isNaN(txtBOperatorCharge)) {
                txtBOperatorCharge = 0;
            }
            var txtBOperatorChargeAct = document.getElementById("<%=txtBOperatorChargeAct.ClientID%>").value;
            txtBOperatorChargeAct = parseFloat(txtBOperatorChargeAct.replaceAll(',', ''));
            if (isNaN(txtBOperatorChargeAct)) {
                txtBOperatorChargeAct = 0;
            }
            diffBOperatorCharge = txtBOperatorChargeAct - txtBOperatorCharge;
            if (!isNaN(diffBOperatorCharge)) {
                document.getElementById("<%=dfBopch.ClientID%>").innerText = diffBOperatorCharge.toFixed(2);
            }


            var txtSpPermisionCharge = document.getElementById("<%=txtSpPermisionCharge.ClientID%>").value;
            txtSpPermisionCharge = parseFloat(txtSpPermisionCharge.replaceAll(',', ''));
            if (isNaN(txtSpPermisionCharge)) {
                txtSpPermisionCharge = 0;
            }
            var txtSpPerChargeAct = document.getElementById("<%=txtSpPerChargeAct.ClientID%>").value;
            txtSpPerChargeAct = parseFloat(txtSpPerChargeAct.replaceAll(',', ''));
            if (isNaN(txtSpPerChargeAct)) {
                txtSpPerChargeAct = 0;
            }
            diffSpPermisionCharge = txtSpPerChargeAct - txtSpPermisionCharge;
            if (!isNaN(diffSpPermisionCharge)) {
                document.getElementById("<%=dfspper.ClientID%>").innerText = diffSpPermisionCharge.toFixed(2);
            }


            var txtMiscellaneous = document.getElementById("<%=txtMiscellaneous.ClientID%>").value;
            txtMiscellaneous = parseFloat(txtMiscellaneous.replaceAll(',', ''));
            if (isNaN(txtMiscellaneous)) {
                txtMiscellaneous = 0;
            }
            var txtMiscellaneousAct = document.getElementById("<%=txtMiscellaneousAct.ClientID%>").value;
            txtMiscellaneousAct = parseFloat(txtMiscellaneousAct.replaceAll(',', ''));
            if (isNaN(txtMiscellaneousAct)) {
                txtMiscellaneousAct = 0;
            }
            diffMiscellaneous = txtMiscellaneousAct - txtMiscellaneous;
            if (!isNaN(diffMiscellaneous)) {
                document.getElementById("<%=dfmis.ClientID%>").innerText = diffMiscellaneous.toFixed(2);
            }


            var txtTotClearingCharge = document.getElementById("<%=txtTotClearingCharge.ClientID%>").value;
            txtTotClearingCharge = parseFloat(txtTotClearingCharge.replaceAll(',', ''));
            if (isNaN(txtTotClearingCharge)) {
                txtTotClearingCharge = 0;
            }
            var txtTotClearingChargeAct = document.getElementById("<%=txtTotClearingChargeAct.ClientID%>").value;
            txtTotClearingChargeAct = parseFloat(txtTotClearingChargeAct.replaceAll(',', ''));
            if (isNaN(txtTotClearingChargeAct)) {
                txtTotClearingChargeAct = 0;
            }
            diffTotClearingCharge = txtTotClearingChargeAct - txtTotClearingCharge;
            if (!isNaN(diffTotClearingCharge)) {
                document.getElementById("<%=dftclch.ClientID%>").innerText = diffTotClearingCharge.toFixed(2);
            }


            var txtSeaFreight = document.getElementById("<%=txtSeaFreight.ClientID%>").value;
            txtSeaFreight = parseFloat(txtSeaFreight.replaceAll(',', ''));
            if (isNaN(txtSeaFreight)) {
                txtSeaFreight = 0;
            }
            var txtSeaFreightAct = document.getElementById("<%=txtSeaFreightAct.ClientID%>").value;
            txtSeaFreightAct = parseFloat(txtSeaFreightAct.replaceAll(',', ''));
            if (isNaN(txtSeaFreightAct)) {
                txtSeaFreightAct = 0;
            }
            diffSeaFreight = txtSeaFreightAct - txtSeaFreight;
            if (!isNaN(diffSeaFreight)) {
                document.getElementById("<%=lblDiffSeaFreight.ClientID%>").innerText = diffSeaFreight.toFixed(2);
            }


            var txtTotWithVat = document.getElementById("<%=txtTotWithVat.ClientID%>").value;
            txtTotWithVat = parseFloat(txtTotWithVat.replaceAll(',', ''));
            if (isNaN(txtTotWithVat)) {
                txtTotWithVat = 0;
            }
            var txtTotWithVatAct = document.getElementById("<%=txtTotWithVatAct.ClientID%>").value;
            txtTotWithVatAct = parseFloat(txtTotWithVatAct.replaceAll(',', ''));
            if (isNaN(txtTotWithVatAct)) {
                txtTotWithVatAct = 0;
            }
            diffTotWithVat = txtTotWithVatAct - txtTotWithVat;
            if (!isNaN(diffTotWithVat)) {
                document.getElementById("<%=dfTWv.ClientID%>").innerText = diffTotWithVat.toFixed(2);
            }


            var txtTotWOVat = document.getElementById("<%=txtTotWOVat.ClientID%>").value;
            txtTotWOVat = parseFloat(txtTotWOVat.replaceAll(',', ''));
            if (isNaN(txtTotWOVat)) {
                txtTotWOVat = 0;
            }
            var txtTotWithoutVatAct = document.getElementById("<%=txtTotWithoutVatAct.ClientID%>").value;
            txtTotWithoutVatAct = parseFloat(txtTotWithoutVatAct.replaceAll(',', ''));
            if (isNaN(txtTotWithoutVatAct)) {
                txtTotWithoutVatAct = 0;
            }
            diffTotWOVat = txtTotWithoutVatAct - txtTotWOVat;
            if (!isNaN(diffTotWOVat)) {
                document.getElementById("<%=dfTWov.ClientID%>").innerText = diffTotWOVat.toFixed(2);
            }


            var txtFactor = document.getElementById("<%=txtFactor.ClientID%>").value;
            txtFactor = parseFloat(txtFactor.replaceAll(',', ''));
            if (isNaN(txtFactor)) {
                txtFactor = 0;
            }
            var txtFactorAct = document.getElementById("<%=txtFactorAct.ClientID%>").value;
            txtFactorAct = parseFloat(txtFactorAct.replaceAll(',', ''));
            if (isNaN(txtFactorAct)) {
                txtFactorAct = 0;
            }
            diffFactor = txtFactorAct - txtFactor;
            if (!isNaN(diffFactor)) {
                document.getElementById("<%=dfFac.ClientID%>").innerText = diffFactor.toFixed(2);
            }


            var txtRate = document.getElementById("<%=txtRate.ClientID%>").value;
            txtRate = parseFloat(txtRate.replaceAll(',', ''));
            if (isNaN(txtRate)) {
                txtRate = 0;
            }
            var txtRateAct = document.getElementById("<%=txtRateAct.ClientID%>").value;
            txtRateAct = parseFloat(txtRateAct.replaceAll(',', ''));
            if (isNaN(txtRateAct)) {
                txtRateAct = 0;
            }
            diffRate = txtRateAct - txtRate;
            if (!isNaN(diffRate)) {
                document.getElementById("<%=dfRt.ClientID%>").innerText = diffRate.toFixed(2);
            }

           


        }

        function CopyProvisionalData(chk) {
           
            <%--  var txtItemName = document.getElementById("<%=txtItemName.ClientID%>").value;
            if (txtItemName == "") {
                alert("Please Select Item !!");
                document.getElementById("<%=txtItemName.ClientID%>").focus();
                SetDefaultValue();
                return false;
            }--%>
         
            if (document.getElementById("<%=chkCopy.ClientID%>").checked)
            {

            var txtCnfPrice = document.getElementById("<%=txtCnfPrice.ClientID%>").value;
            txtCnfPrice = parseFloat(txtCnfPrice.replaceAll(',', ''));
            if (isNaN(txtCnfPrice)) {
                txtCnfPrice = 0;
            }
            document.getElementById("<%=txtCommercialValueAct.ClientID%>").value = txtCnfPrice;
            var txtConversionRate = document.getElementById("<%=txtConversionRate.ClientID%>").value;
            txtConversionRate = parseFloat(txtConversionRate.replaceAll(',', ''));
            if (isNaN(txtConversionRate)) {
                txtConversionRate = 0;
            }
            document.getElementById("<%=txtConversionRateAct.ClientID%>").value = txtConversionRate;
          <%--  documentValue = txtCnfPrice * txtConversionRate;
            if (!isNaN(documentValue)) {
                document.getElementById("<%=txtDocumentValue.ClientID%>").value = documentValue.toFixed(2);
            }--%>
                //alert(1);
                var txtDocumentValue = parseFloat(document.getElementById("<%=txtDocumentValue.ClientID%>").value);
                document.getElementById("<%=txtDocumentValueAct.ClientID%>").value = txtDocumentValue;//documentValue.toFixed(2);
                //alert(2);
            var txtMarineInsurance = document.getElementById("<%=txtMarineInsurance.ClientID%>").value;
            txtMarineInsurance = parseFloat(txtMarineInsurance.replaceAll(',', ''));
            if (isNaN(txtMarineInsurance)) {
                txtMarineInsurance = 0;
            }
            document.getElementById("<%=txtMarineInsuranceAct.ClientID%>").value = txtMarineInsurance;

            var txtInsuranceAndOth = document.getElementById("<%=txtInsuranceAndOth.ClientID%>").value;
            txtInsuranceAndOth = parseFloat(txtInsuranceAndOth.replaceAll(',', ''));
            if (isNaN(txtInsuranceAndOth)) {
                txtInsuranceAndOth = 0;
            }
            document.getElementById("<%=txtInsuranceOthAct.ClientID%>").value = txtInsuranceAndOth;

         <%--   var AssesValue = txtDocumentValue + txtMarineInsurance + txtInsuranceAndOth;
            if (!isNaN(AssesValue)) {
                document.getElementById("<%=txtAssessableValue.ClientID%>").value = AssesValue.toFixed(2);
            }--%>
                document.getElementById("<%=txtAssessableValueAct.ClientID%>").value = parseFloat(document.getElementById("<%=txtAssessableValue.ClientID%>").value);

            var txtGlobalTaxes = document.getElementById("<%=txtGlobalTaxes.ClientID%>").value; //parseFloat(document.getElementById("<%=txtGlobalTaxes.ClientID%>").value);
            txtGlobalTaxes = parseFloat(txtGlobalTaxes.replaceAll(',', ''));
            if (isNaN(txtGlobalTaxes)) {
                txtGlobalTaxes = 0;
            }
            document.getElementById("<%=txtGlobalTaxAct.ClientID%>").value = txtGlobalTaxes;

            var txtCd = document.getElementById("<%=txtCd.ClientID%>").value;
            txtCd = parseFloat(txtCd.replaceAll(',', ''));
            if (isNaN(txtCd)) {
                txtCd = 0;
            }
            document.getElementById("<%=txtCdAct.ClientID%>").value = txtCd;

            var txtRd = document.getElementById("<%=txtRd.ClientID%>").value;
            txtRd = parseFloat(txtRd.replaceAll(',', ''));
            if (isNaN(txtRd)) {
                txtRd = 0;
            }
            document.getElementById("<%=txtRdAct.ClientID%>").value = txtRd;

            var txtSd = document.getElementById("<%=txtSd.ClientID%>").value;
            txtSd = parseFloat(txtSd.replaceAll(',', ''));
            if (isNaN(txtSd)) {
                txtSd = 0;
            }
            document.getElementById("<%=txtSdAct.ClientID%>").value = txtSd;

            var txtVat = document.getElementById("<%=txtVat.ClientID%>").value;
            txtVat = parseFloat(txtVat.replaceAll(',', ''));
            if (isNaN(txtVat)) {
                txtVat = 0;
            }
            document.getElementById("<%=txtVatAct.ClientID%>").value = txtVat;

            var txtAit = document.getElementById("<%=txtAit.ClientID%>").value;
            txtAit = parseFloat(txtAit.replaceAll(',', ''));
            if (isNaN(txtAit)) {
                txtAit = 0;
            }
            document.getElementById("<%=txtAitAct.ClientID%>").value = txtAit;

            var txtAt = document.getElementById("<%=txtAt.ClientID%>").value;
            txtAt = parseFloat(txtAt.replaceAll(',', ''));
            if (isNaN(txtAt)) {
                txtAt = 0;
            }
            document.getElementById("<%=txtAtAct.ClientID%>").value = txtAt;

          <%--  var TotalItemTax = txtCd + txtRd + txtSd + txtVat + txtAit + txtAt;
            if (!isNaN(TotalItemTax)) {
                document.getElementById("<%=txtTotalItemTax.ClientID%>").value = TotalItemTax.toFixed(2);
            }--%>
                document.getElementById("<%=txtTotItemTaxAct.ClientID%>").value = parseFloat(document.getElementById("<%=txtTotalItemTax.ClientID%>").value);

          <%--  var TotalTax = txtGlobalTaxes + TotalItemTax;
            if (!isNaN(TotalTax)) {
                document.getElementById("<%=txtTotalTaxes.ClientID%>").value = TotalTax.toFixed(2);
            }--%>
                document.getElementById("<%=txtTotTaxesAmtAct.ClientID%>").value = parseFloat(document.getElementById("<%=txtTotalTaxes.ClientID%>").value);


            var txtPortCharge = document.getElementById("<%=txtPortCharge.ClientID%>").value;
            txtPortCharge = parseFloat(txtPortCharge.replaceAll(',', ''));
            if (isNaN(txtPortCharge)) {
                txtPortCharge = 0;
            }
            document.getElementById("<%=txtPortChargeAct.ClientID%>").value = txtPortCharge;

            var txtAddVat = document.getElementById("<%=txtAddVat.ClientID%>").value;
            txtAddVat = parseFloat(txtAddVat.replaceAll(',', ''));
            if (isNaN(txtAddVat)) {
                txtAddVat = 0;
            }
            document.getElementById("<%=txtAddVatAct.ClientID%>").value = txtAddVat;

            var txtPortAit = document.getElementById("<%=txtPortAit.ClientID%>").value;
            txtPortAit = parseFloat(txtPortAit.replaceAll(',', ''));
            if (isNaN(txtPortAit)) {
                txtPortAit = 0;
            }
            document.getElementById("<%=txtPortAitAct.ClientID%>").value = txtPortAit;

            var txtOthers = document.getElementById("<%=txtOthers.ClientID%>").value;
            txtOthers = parseFloat(txtOthers.replaceAll(',', ''));
            if (isNaN(txtOthers)) {
                txtOthers = 0;
            }
            document.getElementById("<%=txtOthersAct.ClientID%>").value = txtOthers;

           <%-- var TotalPortCharge = txtPortCharge + txtAddVat + txtPortAit + txtOthers;
            if (!isNaN(TotalPortCharge)) {
                document.getElementById("<%=txtTotPortCharge.ClientID%>").value = TotalPortCharge.toFixed(2);
            }--%>
                document.getElementById("<%=txtTotPortChargeAct.ClientID%>").value = parseFloat(document.getElementById("<%=txtTotPortCharge.ClientID%>").value);

            txtShippingCharge = document.getElementById("<%=txtShippingCharge.ClientID%>").value;
            txtShippingCharge = parseFloat(txtShippingCharge.replaceAll(',', ''));
            if (isNaN(txtShippingCharge)) {
                txtShippingCharge = 0;
            }
            document.getElementById("<%=txtShippingChargeAct.ClientID%>").value = txtShippingCharge;

            txtNocCharge = document.getElementById("<%=txtNocCharge.ClientID%>").value;
            txtNocCharge = parseFloat(txtNocCharge.replaceAll(',', ''));
            if (isNaN(txtNocCharge)) {
                txtNocCharge = 0;
            }
            document.getElementById("<%=txtNocChargeAct.ClientID%>").value = txtNocCharge;

            txtBOperatorCharge = document.getElementById("<%=txtBOperatorCharge.ClientID%>").value;
            txtBOperatorCharge = parseFloat(txtBOperatorCharge.replaceAll(',', ''));
            if (isNaN(txtBOperatorCharge)) {
                txtBOperatorCharge = 0;
            }
            document.getElementById("<%=txtBOperatorChargeAct.ClientID%>").value = txtBOperatorCharge;

            txtSpPermisionCharge = document.getElementById("<%=txtSpPermisionCharge.ClientID%>").value;
            txtSpPermisionCharge = parseFloat(txtSpPermisionCharge.replaceAll(',', ''));

            if (isNaN(txtSpPermisionCharge)) {
                txtSpPermisionCharge = 0;
            }
            document.getElementById("<%=txtSpPerChargeAct.ClientID%>").value = txtSpPermisionCharge;

            txtSaftaCharge = document.getElementById("<%=txtSaftaCharge.ClientID%>").value;
            txtSaftaCharge = parseFloat(txtSaftaCharge.replaceAll(',', ''));
            if (isNaN(txtSaftaCharge)) {
                txtSaftaCharge = 0;
            }
            document.getElementById("<%=txtShaftaChargeAct.ClientID%>").value = txtSaftaCharge;

            txtCnfCommision = document.getElementById("<%=txtCnfCommision.ClientID%>").value;
            txtCnfCommision = parseFloat(txtCnfCommision.replaceAll(',', ''));
            if (isNaN(txtCnfCommision)) {
                txtCnfCommision = 0;
            }
            document.getElementById("<%=txtcnfCommAct.ClientID%>").value = txtCnfCommision;

            txtOthersCharge = document.getElementById("<%=txtOthersCharge.ClientID%>").value;
            txtOthersCharge = parseFloat(txtOthersCharge.replaceAll(',', ''));
            if (isNaN(txtOthersCharge)) {
                txtOthersCharge = 0;
            }
            document.getElementById("<%=txtOthChargeAct.ClientID%>").value = txtOthersCharge;

            txtTransport = document.getElementById("<%=txtTransport.ClientID%>").value;
            txtTransport = parseFloat(txtTransport.replaceAll(',', ''));
            if (isNaN(txtTransport)) {
                txtTransport = 0;
            }
            document.getElementById("<%=txtTransportAct.ClientID%>").value = txtTransport;

            txtMiscellaneous = document.getElementById("<%=txtMiscellaneous.ClientID%>").value;
            txtMiscellaneous = parseFloat(txtMiscellaneous.replaceAll(',', ''));
            if (isNaN(txtMiscellaneous)) {
                txtMiscellaneous = 0;
            }
            document.getElementById("<%=txtMiscellaneousAct.ClientID%>").value = txtMiscellaneous;

         <%--   TotalOthCharge = txtShippingCharge + txtNocCharge + txtBOperatorCharge + txtSpPermisionCharge + txtSaftaCharge + txtCnfCommision + txtOthersCharge + txtTransport + txtMiscellaneous;
            TotalCharge = TotalPortCharge + TotalOthCharge;
            if (!isNaN(TotalCharge)) {
                document.getElementById("<%=txtTotClearingChargeAct.ClientID%>").value = TotalCharge.toFixed(2);
            }--%>
              document.getElementById("<%=txtTotClearingChargeAct.ClientID%>").value = parseFloat(document.getElementById("<%=txtTotClearingCharge.ClientID%>").value);

            txtSeaFreight = document.getElementById("<%=txtSeaFreight.ClientID%>").value;
            txtSeaFreight = parseFloat(txtSeaFreight.replaceAll(',', ''));
            if (isNaN(txtSeaFreight)) {
                txtSeaFreight = 0;
            }
            document.getElementById("<%=txtSeaFreightAct.ClientID%>").value = txtSeaFreight;


          <%--  TotWithVat = AssesValue + TotalTax + TotalCharge + txtSeaFreight;// + txtTransport + txtMiscellaneous;
            if (!isNaN(TotWithVat)) {
                document.getElementById("<%=txtTotWithVatAct.ClientID%>").value = TotWithVat.toFixed(2);
            }--%>
                document.getElementById("<%=txtTotWithVatAct.ClientID%>").value = (document.getElementById("<%=txtTotWithVat.ClientID%>").value);

           <%-- TotVat = txtVat + txtAit + txtAt;
            TotWOVat = TotWithVat - TotVat;
            if (!isNaN(TotWOVat)) {
                document.getElementById("<%=txtTotWithoutVatAct.ClientID%>").value = TotWOVat.toFixed(2);
            }--%>
               document.getElementById("<%=txtTotWithoutVatAct.ClientID%>").value = parseFloat(document.getElementById("<%=txtTotWOVat.ClientID%>").value);

          <%--  factor = TotWOVat / documentValue;
            if (!isNaN(factor)) {
                document.getElementById("<%=txtFactorAct.ClientID%>").value = factor.toFixed(2);
            }--%>
               document.getElementById("<%=txtFactorAct.ClientID%>").value = parseFloat(document.getElementById("<%=txtFactor.ClientID%>").value);

           txtItemQty = parseFloat(document.getElementById("<%=txtItemQty.ClientID%>").value);
            if (isNaN(txtItemQty)) {
                txtItemQty = 0;
            }
            document.getElementById("<%=txtItemQtyAct.ClientID%>").value = txtItemQty;

            txtConvertedItemQty = parseFloat(document.getElementById("<%=txtConvertedItemQty.ClientID%>").value);
            if (isNaN(txtConvertedItemQty)) {
                txtConvertedItemQty = 0;
            }
            document.getElementById("<%=txtConvertedItemQtyAct.ClientID%>").value = txtConvertedItemQty;

          <%--  Rate = TotWOVat / txtConvertedItemQty;

            if (!isNaN(Rate)) {
                document.getElementById("<%=txtRateAct.ClientID%>").value = Rate.toFixed(4);
            }--%>
              document.getElementById("<%=txtRateAct.ClientID%>").value = (document.getElementById("<%=txtRate.ClientID%>").value);

                document.getElementById("<%=lblChkCopy.ClientID%>").style.color = 'green';

                calculateDifference();
                calculatePanelAct();
            }

            else
            {
                SetDefaultValueAct();
                document.getElementById("<%=lblChkCopy.ClientID%>").style.color = 'black';
            }


        }

        /* Tab javascript */
        function openCity(evt, cityName) {
            var i, tabcontent, tablinks;
            tabcontent = document.getElementsByClassName("tabcontent");
            for (i = 0; i < tabcontent.length; i++) {
                tabcontent[i].style.display = "none";
            }
            tablinks = document.getElementsByClassName("tablinks");
            for (i = 0; i < tablinks.length; i++) {
                //tablinks[i].className = tablinks[i].className.replace(" active", "#");
            }
            document.getElementById(cityName).style.display = "block";
            evt.currentTarget.className += " active";
        }

        $(function () {
            $("#tabs").tabs();
        });

    </script>

    <style type="text/css">
        /* Style the tab */
.tab {
    overflow: hidden;
    /*border: 1px solid #ccc;
    background-color: #f1f1f1;*/
}

/* Style the buttons inside the tab */
.tab button {
    background-color: inherit;
    float: left;
    border: none;
    outline: none;
    cursor: pointer;
    padding: 14px 16px;
    transition: 0.3s;
    font-size: 17px;
    
}

/* Change background color of buttons on hover */
.tab button:hover {
    background-color: #ddd;
}

/* Create an active/current tablink class */
.tab button.active {
    background-color: #ccc;
}

/* Style the tab content */
.tabcontent {
    display: none;
    padding: 6px 12px;
    /*border: 1px solid #ccc;*/
    border-top: none;
}

        .auto-style1 {
            height: 27px;
        }

    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvPageContent" style="width:100%;height:100%">
        <asp:HiddenField ID="hdnDeptID" runat="server" Value="" />
         <asp:HiddenField ID="hdnLcId" runat="server" />
         <asp:HiddenField ID="hdnItemID" runat="server" />
         <asp:HiddenField ID="hdnCostingId" runat="server" Value="" />
         <asp:HiddenField ID="hdnSupplierID" runat="server" Value="0" />
        <asp:HiddenField ID="hdnTotLCValueUSD" runat="server" Value="0" />
        <asp:HiddenField ID="hdnIMP_PURCHASE_ID" runat="server" />
        
        <div id="dvContentHeader" class="dvContentHeader">
            <div id="dvHeader" class="dvHeader_Prod" align="left">
                <asp:Label ID="lblHeader" runat="server" Text="LC Costing Entry " CssClass="lblHeader" Font-Bold="true" Font-Size="15px"></asp:Label>

            </div>
            <div id="dvMessage" class="dvMessage">
                <asp:Label ID="lblMessage" runat="server" CssClass="" Text="" Width="100%"></asp:Label>
            </div>
            <div id="dvHeaderControl" class="dvHeaderControl">
            </div>
           
        </div>
        
        <div id="dvContentMain" align="left" class="dvContentMain" style="height:auto">
           <div id="tabs" style=" background : url(../image/bg_greendot.gif) !important;">
              <ul>
                <li><a href="#tabs-1">Provisional</a></li>
                <li><a href="#tabs-2">Actual</a></li>
    
              </ul>
           
            <div id="dvControlsHead" style="height: auto; width: 100%;" align="left">
                <table>
                    <tr>
                        
                     
                        <td align="right">
                            <asp:Label runat="server" ID="lblSupplier" Text="Supplier : "></asp:Label>
                        </td>
                        <td>
                           <asp:TextBox ID="txtSupplierName" runat="server" CssClass="textBox" Width="180px"></asp:TextBox>
                           <input id="btnSupplierID" type="button" value="" runat="server" class="buttonDropdown" tabindex="-1" />

                        </td>
                        <td style="text-align:right">
                            <asp:Label ID="lblLcNo" runat="server" Text="LC No :"></asp:Label>
                        </td>
                         <td>
                            <asp:TextBox ID="txtLcNo" runat="server" CssClass="textBox textAutoSelect" Width="160px" Height="20px"  ></asp:TextBox>
                            <input id="btnLcNo" type="button" value="" runat="server" class="buttonDropdown" style="width: 15px " tabindex="-1" />
                        </td>
                      <td style="text-align:right">
                            <asp:Label ID="lblPONo" runat="server" Text="PO No :"></asp:Label>
                        </td>
                         <td>
                            <asp:TextBox ID="txtPoNo" runat="server" CssClass="textBox textAutoSelect" Width="160px" Height="20px"  ></asp:TextBox>
                            <input id="btnPoNo" type="button" value="" runat="server" class="buttonDropdown" style="width: 15px " tabindex="-1" />
                        </td>
                        <td  style="text-align:right;">
                            <asp:Label ID="lblItemID" runat="server" Text="Item: "></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtItemName" runat="server" CssClass="textBox textAutoSelect" Width="202px" Height="20px"></asp:TextBox>
                            <input id="btnItemID" type="button" value="" runat="server" class="buttonDropdown" style="width: 15px " tabindex="-1" />
                            <asp:TextBox ID="lblItemCount" runat="server" Width="100px" Style="color:red;font-weight:bold;" ></asp:TextBox>
                            
                        </td>
                         
                    </tr>
                    <tr>
                        
                     
                      
                        <td align="right" class="auto-style1">
                            <asp:Label ID="lblCostingNo" runat="server" Text="Costing No:"></asp:Label>
                        </td>
                        <td class="auto-style1">
                           <asp:TextBox ID="txtCostingNo" CssClass="textBox textAutoSelect"  runat="server"  ></asp:TextBox>
                        </td>
                     
                          <td align="right" class="auto-style1">
                            <asp:Label ID="lblBENo" runat="server" Text="B/E No:"></asp:Label>
                        </td>
                        <td class="auto-style1">
                           <asp:TextBox ID="txtBENo" CssClass="textBox textAutoSelect"  runat="server"  ></asp:TextBox>
                        </td>
                       
                           <td align="right" class="auto-style1">
                            <asp:Label ID="lblBillNo" runat="server" Text="Bill No:"></asp:Label>
                        </td>
                        <td class="auto-style1">
                           <asp:TextBox ID="txtBillNo" CssClass="textBox textAutoSelect"  runat="server"  ></asp:TextBox>
                            &nbsp;
                            
                        </td>
                         <td style="text-align:right;">
                            <asp:Label ID="lblDeptID" runat="server" Text="Department : "></asp:Label>
                        </td>
                        <td>
                        <asp:DropDownList ID="ddlDeptID" runat="server" CssClass="dropDownList required" Width="205px"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="lblcostingItemCategory" runat="server" Text="Type:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlCostingItemCategory" CssClass="dropDownList"></asp:DropDownList>
                             <asp:TextBox ID="lbltotType" runat="server" Width="5px" Style="color:red;font-weight:bold;" ></asp:TextBox>
                        </td>
                        <td>
                             <asp:CheckBox ID="chkCopy" runat="server" onclick="CopyProvisionalData(this)" />
                            <asp:Label runat="server" ID="lblChkCopy" Text="Copy" style="color:black;font-weight:bold;"></asp:Label>
                        </td>
                    </tr>
                    </table>

            </div>
            <div id="tabs-1">
            <div id="dvControls" style="width:980px;height:auto;border:1px solid blue" >
                <div id="dvCostDtl" style="overflow-y:auto; height:auto;"> <%--400px;--%>
                <div id="dvPanel" style="width:455px; float:left; font-weight:bold; padding-left:20px;">
                    <table>
                        <tr>
                             <td style="text-align:right;">
                                    <asp:Label ID="lblItemQty" runat="server" Text="Item Quantity:"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtItemQty" runat="server" CssClass="textBox textAutoSelect" placeHolder="0" Width="120px" onkeypress=" return isNumberKey(event,this);"  ></asp:TextBox>
                                 </td>
                            <td>
                                <asp:TextBox ID="txtUom" runat="server" Width="30px" ></asp:TextBox>
                            </td>
                             <td>
                               <asp:TextBox ID="txtConvertedItemQty" runat="server" CssClass="textBox textAutoSelect" placeHolder="0" Width="120px" onkeypress=" return isNumberKey(event,this);"  ></asp:TextBox>
                              </td>
                            <td>
                                <asp:TextBox ID="txtConvertedUom" runat="server" Width="30px" ></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <fieldset style=" height:200px; border:1px solid">
                        <legend style=" font-size:12px;color:red;">Value</legend>
                        <table>
                            <tr>
                                <td style="text-align:right;">
                                    <asp:Label ID="lblCNFPrice" runat="server" Text="Commercial Value :"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtCnfPrice" CssClass="textBox textAutoSelect"  runat="server"  onChange="calculatePanel()" onkeypress=" return isNumberKey(event,this);"></asp:TextBox>
                                 </td>
                              

                            </tr>
                            <tr>
                                <td style="text-align:right;">
                                    <asp:Label ID="lblConversionRate" runat="server" Text="Conversion Rate :"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtConversionRate" runat="server"  CssClass="textBox textAutoSelect" onChange="calculatePanel()" onkeypress=" return isNumberKey(event,this);"></asp:TextBox>
                                 </td>
                            

                            </tr>
                            <tr>
                                <td style="text-align:right;">
                                    <asp:Label ID="lblDocumentValue" runat="server" Text="Document Value :"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtDocumentValue" runat="server" CssClass="textBox textAutoSelect"  onkeypress=" return isNumberKey(event,this);"  ></asp:TextBox>
                                 </td>

                            </tr>

                             <tr>
                                <td style="text-align:right;">
                                    <asp:Label ID="lblMarineInsurance" runat="server" Text="Marine Insurance :"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtMarineInsurance" CssClass="textBox textAutoSelect" runat="server"  onChange="calculatePanel()" onkeypress=" return isNumberKey(event,this);" ></asp:TextBox>
                                 </td>

                            </tr>
                            <tr>
                                <td style="text-align:right;">
                                    <asp:Label ID="lblInsuranceAndOth" runat="server" Text="Insurance & Others :"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtInsuranceAndOth" runat="server" CssClass="textBox textAutoSelect"  onChange="calculatePanel()" onkeypress=" return isNumberKey(event,this);" ></asp:TextBox>
                                 </td>

                            </tr>
                            <tr>
                                <td style="text-align:right;">
                                    <asp:Label ID="lblAssessableValue" runat="server" Text="Assessable Value :"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtAssessableValue" runat="server" CssClass="textBox textAutoSelect"  onkeypress=" return isNumberKey(event,this);"  ></asp:TextBox>
                                 </td>
                            </tr>
                       
                          
                        </table>

                    </fieldset>

                </div>
                   
                <div id="dvPcs" style="width:455px; float:left; padding-left:10px; font-weight:bold;">
                    <table>
                          <tr>
                              <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                <td style="text-align:right;">
                                    <asp:Label ID="lblGlobalTaxes" runat="server" Text="GLobal Taxes:"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtGlobalTaxes" runat="server" CssClass="textBox textAutoSelect"  onChange="calculatePanel()" onkeypress=" return isNumberKey(event,this);" ></asp:TextBox>
                                 </td>
                            </tr>
                      </table>
                    <fieldset style=" height:200px; border:1px solid">
                        <legend style=" font-size:12px;color:red;">Item Taxes</legend>
                         <table>
                            <tr>
                                <td style="text-align:right;">
                                    <asp:Label ID="lblCd" runat="server" Text="CD :"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtCd" runat="server" CssClass="textBox textAutoSelect"  onChange="calculatePanel()" onkeypress=" return isNumberKey(event,this);" ></asp:TextBox>
                                 </td>

                            </tr>
                            <tr>
                                <td style="text-align:right;">
                                    <asp:Label ID="lblRd" runat="server" Text="RD :"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtRd" runat="server" CssClass="textBox textAutoSelect"  onChange="calculatePanel()" onkeypress=" return isNumberKey(event,this);"  ></asp:TextBox>
                                 </td>

                            </tr>
                            <tr>
                                <td style="text-align:right;">
                                    <asp:Label ID="lblSd" runat="server" Text="SD :"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtSd" runat="server" CssClass="textBox textAutoSelect"  onChange="calculatePanel()" onkeypress=" return isNumberKey(event,this);" ></asp:TextBox>
                                 </td>

                            </tr>
                   
                             <tr>
                                <td style="text-align:right;">
                                    <asp:Label ID="lblVat" runat="server" Text="VAT :"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtVat" runat="server" CssClass="textBox textAutoSelect"  onChange="calculatePanel()" onkeypress=" return isNumberKey(event,this);"  ></asp:TextBox>
                                 </td>

                            </tr>
                            <tr>
                                <td style="text-align:right;">
                                    <asp:Label ID="lblAit" runat="server" Text="AIT :"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtAit" runat="server" CssClass="textBox textAutoSelect" onChange="calculatePanel()" onkeypress=" return isNumberKey(event,this);" ></asp:TextBox>
                                 </td>

                            </tr>
                            <tr>
                                <td style="text-align:right;">
                                    <asp:Label ID="lblAt" runat="server" Text="AT :"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtAt" runat="server" CssClass="textBox textAutoSelect"  onChange="calculatePanel()" onkeypress=" return isNumberKey(event,this);"  ></asp:TextBox>
                                 </td>

                            </tr>
                               <tr>
                                <td style="text-align:right;">
                                    <asp:Label ID="lblTotalItemTax" runat="server" Text="Total Item Taxes :"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtTotalItemTax" runat="server" CssClass="textBox textAutoSelect"  onkeypress=" return isNumberKey(event,this);"  ></asp:TextBox>
                                 </td>

                            </tr>
                              <tr>
                                <td style="text-align:right;">
                                    <asp:Label ID="lblTotalTaxes" runat="server" Text="Total Taxes Amount :"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtTotalTaxes" runat="server" CssClass="textBox textAutoSelect"  onkeypress=" return isNumberKey(event,this);"  ></asp:TextBox>
                                 </td>
                            </tr>

                        </table>

                    </fieldset>

                </div>

                <div id="ClearingCharge" style="width:920px; float:left; padding-left:20px; font-weight:bold;">
                      <fieldset style=" height:auto; border:1px solid; padding:5px;">
                         <legend style=" font-size:12px;color:red;">Clearing Charge</legend>
                          <div style="width:300px; float:left;">
                              <table>
                                  <tr>
                                 <td style="text-align:right;">
                                    <asp:Label ID="lblPortCharge" runat="server" Text="Port Charge :"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtPortCharge" runat="server" CssClass="textBox textAutoSelect"   onChange="calculatePanel()" onkeypress=" return isNumberKey(event,this);" ></asp:TextBox>
                                 </td>
                                  </tr>
                                  <tr>
                                  <td style="text-align:right;">
                                    <asp:Label ID="lblAddVat" runat="server" Text="Add Vat :"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtAddVat" runat="server" CssClass="textBox textAutoSelect"  onChange="calculatePanel()" onkeypress=" return isNumberKey(event,this);" ></asp:TextBox>
                                 </td>
                                  </tr>
                                   <tr>
                                  <td style="text-align:right;">
                                    <asp:Label ID="lblPortAit" runat="server" Text="AIT :"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtPortAit" runat="server" CssClass="textBox textAutoSelect"  onChange="calculatePanel()" onkeypress=" return isNumberKey(event,this);" ></asp:TextBox>
                                 </td>
                                  </tr>
                                  <tr>
                                 <td style="text-align:right;">
                                    <asp:Label ID="lblOthers" runat="server" Text="Others :"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtOthers" runat="server" CssClass="textBox textAutoSelect"  onChange="calculatePanel()" onkeypress=" return isNumberKey(event,this);"></asp:TextBox>
                                 </td>
                                  </tr>
                                  <tr>
                                  <td style="text-align:right;">
                                    <asp:Label ID="lblTotPortCharge" runat="server" Text="Total Port Charge :"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtTotPortCharge" runat="server" CssClass="textBox textAutoSelect"  onkeypress=" return isNumberKey(event,this);" ></asp:TextBox>
                                 </td>
                                  </tr>
                                  </table>
                          </div>
                          <div style="width:290px; float:left;">
                             <table>
                                 <tr>
                                 <td style="text-align:right;">
                                    <asp:Label ID="lblShippingCharge" runat="server" Text="Shipping Charge :"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtShippingCharge" runat="server" CssClass="textBox textAutoSelect" onChange="calculatePanel()" onkeypress=" return isNumberKey(event,this);" ></asp:TextBox>
                                 </td>

                                 </tr>
                                 <tr>
                                 <td style="text-align:right;">
                                    <asp:Label ID="lblNocCharge" runat="server" Text="NOC Charge :"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtNocCharge" runat="server" CssClass="textBox textAutoSelect"  onChange="calculatePanel()" onkeypress=" return isNumberKey(event,this);" ></asp:TextBox>
                                 </td>

                                 </tr>
                                 <tr>
                                 <td style="text-align:right;">
                                    <asp:Label ID="lblSaftaCharge" runat="server" Text="SAFTA Charge :"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtSaftaCharge" runat="server" CssClass="textBox textAutoSelect"  onChange="calculatePanel()" onkeypress=" return isNumberKey(event,this);" ></asp:TextBox>
                                 </td>

                                 </tr>
                           
                                 <tr>
                                 <td style="text-align:right;">
                                    <asp:Label ID="lblTransport" runat="server" Text="Transport :"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtTransport" runat="server" CssClass="textBox textAutoSelect"  onChange="calculatePanel()" onkeypress=" return isNumberKey(event,this);"  ></asp:TextBox>
                                 </td>
                                 </tr>
                                 <tr>
                               
                                <td style="text-align:right;">
                                     <asp:Label ID="lblCnfCommision" runat="server" Text="C&F Comm. :"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtCnfCommision" runat="server" CssClass="textBox textAutoSelect"  onChange="calculatePanel()" onkeypress=" return isNumberKey(event,this);" ></asp:TextBox>
                                </td>
                            </tr>
                             
                             </table>

                          </div>
                          <div style="width:310px; float:right;" >
                            <table>
                        
                    
                          
                         
                            <tr>
                                  <td style="text-align:right;">
                                    <asp:Label ID="lblOthersCharge" runat="server" Text="Others Charge :"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtOthersCharge" runat="server" CssClass="textBox textAutoSelect"  onChange="calculatePanel()" onkeypress=" return isNumberKey(event,this);" ></asp:TextBox>
                                 </td>
                            </tr>
                             <tr>
                                 <td style="text-align:right;">
                                    <asp:Label ID="lblBOperatorCharge" runat="server" Text="B. Operator Charge :"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtBOperatorCharge" runat="server" CssClass="textBox textAutoSelect"  onChange="calculatePanel()" onkeypress=" return isNumberKey(event,this);" ></asp:TextBox>
                                 </td>
                             </tr>
                             <tr>
                                 <td style="text-align:right;">
                                    <asp:Label ID="lblSpPermisionCharge" runat="server" Text="Sp. Per. Charge :"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtSpPermisionCharge" runat="server" CssClass="textBox textAutoSelect"  onChange="calculatePanel()" onkeypress=" return isNumberKey(event,this);" ></asp:TextBox>
                                 </td>
                             </tr>
                            <tr>
                                 <td style="text-align:right;">
                                    <asp:Label ID="lblMiscellaneous" runat="server" Text="Miscellaneous :"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtMiscellaneous" runat="server" CssClass="textBox textAutoSelect"  onChange="calculatePanel()" onkeypress=" return isNumberKey(event,this);" ></asp:TextBox>
                                 </td>
                             </tr>
                        
                            <tr>
                                   <td style="text-align:right;">
                                    <asp:Label ID="lblTotClearingCharge" runat="server" Text="Total Charge :"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtTotClearingCharge" runat="server" CssClass="textBox textAutoSelect"  onkeypress=" return isNumberKey(event,this);" ></asp:TextBox>
                                 </td>

                            </tr>
                          
                        </table>

                          </div>

                       


                    </fieldset>

                    
                          <div style="padding-left:0px;">
                              <fieldset style=" height:auto; border:1px solid">
                                  <legend style=" font-size:12px;color:red;">Others</legend>
                              <table>
                                  <tr>
                                      <td></td>
                             
                                      <td></td>

                                 <td style="text-align:right;">
                                    <asp:Label ID="lblSeaFreight" runat="server" Text="Sea Freight :"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtSeaFreight" runat="server" CssClass="textBox textAutoSelect"  onChange="calculatePanel()" onkeypress=" return isNumberKey(event,this);" ></asp:TextBox>
                                 </td>
                                      <td></td>
                            

                                  </tr>
                              </table>
                               </fieldset>
                          </div>

                </div>

               </div>

                <div id="TotalCost" style="font-weight:bold; padding-left:10px;">
                    <table>
                        <tr>
                               <td style="text-align:right;">
                                    <asp:Label ID="lblTotWithVat" runat="server" Text="Total With VAT :"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtTotWithVat" runat="server" CssClass="textBox textAutoSelect"  onkeypress=" return isNumberKey(event,this);" ></asp:TextBox>
                                 </td>
                               
                                <td></td>
                                   <td style="text-align:right;">
                                    <asp:Label ID="lblTotWOVat" runat="server" Text="Total W/O VAT :"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtTotWOVat" runat="server" CssClass="textBox textAutoSelect"  onkeypress=" return isNumberKey(event,this);" ></asp:TextBox>
                                 </td>
                                   <td></td>
                                   <td style="text-align:right;">
                                    <asp:Label ID="lblFactor" runat="server" Text="Factor :"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtFactor" runat="server" CssClass="textBox textAutoSelect" Width="100px"  onkeypress=" return isNumberKey(event,this);" ></asp:TextBox>
                                 </td>
                            <td></td>

                                 <td style="text-align:right;">
                                    <asp:Label ID="lblRate" runat="server" Text="Rate :"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtRate" runat="server" CssClass="textBox textAutoSelect" Width="120px"  onkeypress=" return isNumberKey(event,this);" ></asp:TextBox>
                                 </td>
                        </tr>
                    </table>

                </div>
            </div>
            </div>


            <%-- DV ACTUAL --%>
               <div id="tabs-2">
                 <div id="dvControlsAct" style="width:1100px;height:auto;border:1px solid blue" > <%--980px--%>
                <div id="dvCostDtlAct" style="overflow-y:auto; height:auto;"> <%--400px;--%>
                <div id="dvPanelAct" style="width:535px; float:left; font-weight:bold; padding-left:10px;">
                    <table>
                        <tr>
                             <td style="text-align:right;">
                                    <asp:Label ID="lblItemQtyAct" runat="server" Text="Item Quantity:"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtItemQtyAct" runat="server" CssClass="textBox textAutoSelect" placeHolder="0" Width="120px" onkeypress=" return isNumberKey(event,this);"  ></asp:TextBox>
                                 </td>
                            <td>
                                <asp:TextBox ID="txtUomAct" runat="server" Width="30px" ></asp:TextBox>
                            </td>
                             <td>
                               <asp:TextBox ID="txtConvertedItemQtyAct" runat="server" CssClass="textBox textAutoSelect" onChange="calculatePanelAct()" onBlur="calculateDifference()" placeHolder="0" Width="120px" onkeypress=" return isNumberKey(event,this);"  ></asp:TextBox>
                              </td>
                            <td>
                                <asp:TextBox ID="txtConvertedUomAct" runat="server" Width="30px" ></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <fieldset style=" height:200px; border:1px solid">
                        <legend style=" font-size:12px;color:red;">Value Actual</legend>
                        <table>
                            <tr>
                                <td style="text-align:right;">
                                    <asp:Label ID="lblCommercialValueAct" runat="server" Text="Commercial Value :"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtCommercialValueAct" CssClass="textBox textAutoSelect"  runat="server" onChange="calculatePanelAct()"  onBlur="calculateDifference()" onkeypress=" return isNumberKey(event,this);" ></asp:TextBox>
                                 </td>
                                 <td>
                                  <asp:Label runat="server" ID="lblDiffCommercialValue"></asp:Label>
                              </td>
                              

                            </tr>
                            <tr>
                                <td style="text-align:right;">
                                    <asp:Label ID="lblConversionRateAct" runat="server" Text="Conversion Rate :"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtConversionRateAct" runat="server"  CssClass="textBox textAutoSelect" onChange="calculatePanelAct()"  onBlur="calculateDifference()" onkeypress=" return isNumberKey(event,this);"></asp:TextBox>
                                 </td>
                                 <td>
                                  <asp:Label runat="server" ID="lblDiffConversionRate"></asp:Label>
                              </td>
                            

                            </tr>
                            <tr>
                                <td style="text-align:right;">
                                    <asp:Label ID="lblDocumentValueAct" runat="server" Text="Document Value :"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtDocumentValueAct" runat="server" CssClass="textBox textAutoSelect"  onkeypress=" return isNumberKey(event,this);"  ></asp:TextBox>
                                 </td>
                                 <td>
                                  <asp:Label runat="server" ID="lblDiffDocumentValue"></asp:Label>
                              </td>

                            </tr>

                             <tr>
                                <td style="text-align:right;">
                                    <asp:Label ID="lblMarineInsuranceAct" runat="server" Text="Marine Insurance :"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtMarineInsuranceAct" CssClass="textBox textAutoSelect" runat="server"  onChange="calculatePanelAct()"  onBlur="calculateDifference()" onkeypress=" return isNumberKey(event,this);" ></asp:TextBox>
                                 </td>
                                  <td>
                                  <asp:Label runat="server" ID="lblDiffMarineInsurance"></asp:Label>
                              </td>

                            </tr>
                            <tr>
                                <td style="text-align:right;">
                                    <asp:Label ID="lblInsuranceOthAct" runat="server" Text="Insurance & Others :"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtInsuranceOthAct" runat="server" CssClass="textBox textAutoSelect"  onChange="calculatePanelAct()"  onBlur="calculateDifference()" onkeypress=" return isNumberKey(event,this);" ></asp:TextBox>
                                 </td>
                                 <td>
                                  <asp:Label runat="server" ID="lblDiffInsuranceOth"></asp:Label>
                              </td>

                            </tr>
                            <tr>
                                <td style="text-align:right;">
                                    <asp:Label ID="lblAssessableValueAct" runat="server" Text="Assessable Value :"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtAssessableValueAct" runat="server" CssClass="textBox textAutoSelect"  onkeypress=" return isNumberKey(event,this);"  ></asp:TextBox>
                                 </td>
                                 <td>
                                  <asp:Label runat="server" ID="lblDiffAssessableValue"></asp:Label>
                              </td>
                            </tr>
                       
                          
                        </table>

                    </fieldset>

                </div>
                   
                <div id="dvPcsAct" style="width:535px; float:left; padding-left:10px; font-weight:bold;">
                    <table>
                          <tr>
                              <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                <td style="text-align:right;">
                                    <asp:Label ID="lblGlobalTaxAct" runat="server" Text="GLobal Taxes Actual:"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtGlobalTaxAct" runat="server" CssClass="textBox textAutoSelect"  onChange="calculatePanelAct()" onBlur="calculateDifference()" onkeypress=" return isNumberKey(event,this);" ></asp:TextBox>
                                 </td>
                              <td>
                                  <asp:Label runat="server" ID="lbldifGlobalTax"></asp:Label>
                              </td>
                            </tr>
                      </table>
                    <fieldset style=" height:200px; border:1px solid">
                        <legend style=" font-size:12px;color:red;">Item Taxes Actual</legend>
                         <table>
                            <tr>
                                <td style="text-align:right;">
                                    <asp:Label ID="lblCdAct" runat="server" Text="CD :"></asp:Label>
                                </td>
                                
                                 <td>
                                     <asp:TextBox ID="txtCdAct" runat="server" CssClass="textBox textAutoSelect"  onChange="calculatePanelAct()" onBlur="calculateDifference()" onkeypress=" return isNumberKey(event,this);" ></asp:TextBox>
                                 </td>
                                 <td>
                                  <asp:Label runat="server" ID="lblDiffCD"></asp:Label>
                                 </td>

                            </tr>
                            <tr>
                                <td style="text-align:right;">
                                    <asp:Label ID="lblRdAct" runat="server" Text="RD :"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtRdAct" runat="server" CssClass="textBox textAutoSelect"  onChange="calculatePanelAct()" onBlur="calculateDifference()" onkeypress=" return isNumberKey(event,this);"  ></asp:TextBox>
                                 </td>
                                 <td>
                                  <asp:Label runat="server" ID="lblDiffRD"></asp:Label>
                                 </td>

                            </tr>
                            <tr>
                                <td style="text-align:right;">
                                    <asp:Label ID="lblSdAct" runat="server" Text="SD :"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtSdAct" runat="server" CssClass="textBox textAutoSelect"  onChange="calculatePanelAct()" onBlur="calculateDifference()" onkeypress=" return isNumberKey(event,this);" ></asp:TextBox>
                                 </td>
                                 <td>
                                  <asp:Label runat="server" ID="lblDiffSD"></asp:Label>
                                 </td>

                            </tr>
                   
                             <tr>
                                <td style="text-align:right;">
                                    <asp:Label ID="lblVatAct" runat="server" Text="VAT :"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtVatAct" runat="server" CssClass="textBox textAutoSelect"  onChange="calculatePanelAct()" onBlur="calculateDifference()" onkeypress=" return isNumberKey(event,this);"  ></asp:TextBox>
                                 </td>
                                  <td>
                                  <asp:Label runat="server" ID="lblDiffVat"></asp:Label>
                                 </td>

                            </tr>
                            <tr>
                                <td style="text-align:right;">
                                    <asp:Label ID="lblAitAct" runat="server" Text="AIT :"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtAitAct" runat="server" CssClass="textBox textAutoSelect" onChange="calculatePanelAct()" onBlur="calculateDifference()" onkeypress=" return isNumberKey(event,this);" ></asp:TextBox>
                                 </td>
                                 <td>
                                  <asp:Label runat="server" ID="lblDiffAit"></asp:Label>
                                 </td>

                            </tr>
                            <tr>
                                <td style="text-align:right;">
                                    <asp:Label ID="lblAtAct" runat="server" Text="AT :"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtAtAct" runat="server" CssClass="textBox textAutoSelect"  onChange="calculatePanelAct()" onBlur="calculateDifference()" onkeypress=" return isNumberKey(event,this);"  ></asp:TextBox>
                                 </td>
                                 <td>
                                  <asp:Label runat="server" ID="lblDiffAt"></asp:Label>
                                 </td>

                            </tr>
                               <tr>
                                <td style="text-align:right;">
                                    <asp:Label ID="lblTotItemTaxAct" runat="server" Text="Total Item Taxes :"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtTotItemTaxAct" runat="server" CssClass="textBox textAutoSelect"  onkeypress=" return isNumberKey(event,this);"  ></asp:TextBox>
                                 </td>
                                    <td>
                                  <asp:Label runat="server" ID="lblDiffTotItemTax"></asp:Label>
                                 </td>

                            </tr>
                              <tr>
                                <td style="text-align:right;">
                                    <asp:Label ID="lblTotTaxesAmtAct" runat="server" Text="Total Taxes Amount :"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtTotTaxesAmtAct" runat="server" CssClass="textBox textAutoSelect"  onkeypress=" return isNumberKey(event,this);"  ></asp:TextBox>
                                 </td>
                                   <td>
                                  <asp:Label runat="server" ID="lblDiffTotTaxAmt"></asp:Label>
                                 </td>
                            </tr>

                        </table>

                    </fieldset>

                </div>

                <div id="ClearingChargeAct" style="width:1080px; float:left; padding-left:10px; font-weight:bold;">
                      <fieldset style=" height:auto; border:1px solid; padding:5px;">
                         <legend style=" font-size:12px;color:red;">Clearing Charge Actual</legend>
                          <div style="width:350px; float:left;">
                              <table>
                                  <tr>
                                 <td style="text-align:right;">
                                    <asp:Label ID="lblPortChargeAct" runat="server" Text="Port Charge :"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtPortChargeAct" runat="server" CssClass="textBox textAutoSelect"   onChange="calculatePanelAct()" onBlur="calculateDifference()" onkeypress=" return isNumberKey(event,this);" ></asp:TextBox>
                                 </td>
                                   <td>
                                  <asp:Label runat="server" ID="dfPch"></asp:Label>
                                 </td>
                                  </tr>
                                  <tr>
                                  <td style="text-align:right;">
                                    <asp:Label ID="lblAddVatAct" runat="server" Text="Add Vat :"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtAddVatAct" runat="server" CssClass="textBox textAutoSelect"  onChange="calculatePanelAct()" onBlur="calculateDifference()" onkeypress=" return isNumberKey(event,this);" ></asp:TextBox>
                                 </td>
                                  <td>
                                  <asp:Label runat="server" ID="dfAvt"></asp:Label>
                                 </td>
                                  </tr>
                                   <tr>
                                  <td style="text-align:right;">
                                    <asp:Label ID="lblClearingAitAct" runat="server" Text="AIT :"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtPortAitAct" runat="server" CssClass="textBox textAutoSelect"  onChange="calculatePanelAct()" onBlur="calculateDifference()" onkeypress=" return isNumberKey(event,this);" ></asp:TextBox>
                                 </td>
                                  <td>
                                  <asp:Label runat="server" ID="dfPAit"></asp:Label>
                                 </td>
                                  </tr>
                                  <tr>
                                 <td style="text-align:right;">
                                    <asp:Label ID="lblOthersAct" runat="server" Text="Others :"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtOthersAct" runat="server" CssClass="textBox textAutoSelect"  onChange="calculatePanelAct()" onBlur="calculateDifference()" onkeypress=" return isNumberKey(event,this);"></asp:TextBox>
                                 </td>
                                   <td>
                                  <asp:Label runat="server" ID="dfOth"></asp:Label>
                                 </td>
                                  </tr>
                                  <tr>
                                  <td style="text-align:right;">
                                    <asp:Label ID="lblTotPortChargeAct" runat="server" Text="Total Port Charge :"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtTotPortChargeAct" runat="server" CssClass="textBox textAutoSelect"  onkeypress=" return isNumberKey(event,this);" ></asp:TextBox>
                                 </td>
                                  <td>
                                  <asp:Label runat="server" ID="dfTPch"></asp:Label>
                                 </td>
                                  </tr>
                                  </table>
                          </div>
                          <div style="width:340px; float:left;">
                             <table>
                                 <tr>
                                 <td style="text-align:right;">
                                    <asp:Label ID="lblShippingChargeAct" runat="server" Text="Shipping Charge :"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtShippingChargeAct" runat="server" CssClass="textBox textAutoSelect" onChange="calculatePanelAct()" onBlur="calculateDifference()" onkeypress=" return isNumberKey(event,this);" ></asp:TextBox>
                                 </td>
                                  <td>
                                  <asp:Label runat="server" ID="dfSch"></asp:Label>
                                 </td>   

                                 </tr>
                                 <tr>
                                 <td style="text-align:right;">
                                    <asp:Label ID="lblNocChargeAct" runat="server" Text="NOC Charge :"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtNocChargeAct" runat="server" CssClass="textBox textAutoSelect"  onChange="calculatePanelAct()" onBlur="calculateDifference()" onkeypress=" return isNumberKey(event,this);" ></asp:TextBox>
                                 </td>
                                   <td>
                                  <asp:Label runat="server" ID="dfNoc"></asp:Label>
                                 </td>

                                 </tr>
                                 <tr>
                                 <td style="text-align:right;">
                                    <asp:Label ID="lblShaftaChargeAct" runat="server" Text="SAFTA Charge :"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtShaftaChargeAct" runat="server" CssClass="textBox textAutoSelect"  onChange="calculatePanelAct()" onBlur="calculateDifference()" onkeypress=" return isNumberKey(event,this);" ></asp:TextBox>
                                 </td>
                                  <td>
                                  <asp:Label runat="server" ID="dfsftch"></asp:Label>
                                 </td>

                                 </tr>
                           
                                 <tr>
                                 <td style="text-align:right;">
                                    <asp:Label ID="lblTransportAct" runat="server" Text="Transport :"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtTransportAct" runat="server" CssClass="textBox textAutoSelect"  onChange="calculatePanelAct()" onBlur="calculateDifference()" onkeypress=" return isNumberKey(event,this);"  ></asp:TextBox>
                                 </td>
                                  <td>
                                  <asp:Label runat="server" ID="dfTr"></asp:Label>
                                 </td>
                                 </tr>
                                 <tr>
                               
                                <td style="text-align:right;">
                                     <asp:Label ID="lblcnfCommAct" runat="server" Text="C&F Comm. :"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtcnfCommAct" runat="server" CssClass="textBox textAutoSelect"  onChange="calculatePanelAct()" onBlur="calculateDifference()" onkeypress=" return isNumberKey(event,this);" ></asp:TextBox>
                                </td>
                                <td>
                                  <asp:Label runat="server" ID="dfCnfcom"></asp:Label>
                                 </td>
                            </tr>
                             
                             </table>

                          </div>
                          <div style="width:360px; float:right;" >
                            <table>
                        
                    
                          
                         
                            <tr>
                                  <td style="text-align:right;">
                                    <asp:Label ID="lblOthChargeAct" runat="server" Text="Others Charge :"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtOthChargeAct" runat="server" CssClass="textBox textAutoSelect"  onChange="calculatePanelAct()" onBlur="calculateDifference()" onkeypress=" return isNumberKey(event,this);" ></asp:TextBox>
                                 </td>
                                <td>
                                  <asp:Label runat="server" ID="dfothch"></asp:Label>
                                 </td>
                            </tr>
                             <tr>
                                 <td style="text-align:right;">
                                    <asp:Label ID="lblBOperatorChargeAct" runat="server" Text="B. Operator Charge :"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtBOperatorChargeAct" runat="server" CssClass="textBox textAutoSelect"  onChange="calculatePanelAct()" onBlur="calculateDifference()" onkeypress=" return isNumberKey(event,this);" ></asp:TextBox>
                                 </td>
                                 <td>
                                  <asp:Label runat="server" ID="dfBopch"></asp:Label>
                                 </td>
                             </tr>
                             <tr>
                                 <td style="text-align:right;">
                                    <asp:Label ID="lblSpPerChargeAct" runat="server" Text="Sp. Per. Charge :"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtSpPerChargeAct" runat="server" CssClass="textBox textAutoSelect"  onChange="calculatePanelAct()" onBlur="calculateDifference()" onkeypress=" return isNumberKey(event,this);" ></asp:TextBox>
                                 </td>
                                 <td>
                                  <asp:Label runat="server" ID="dfspper"></asp:Label>
                                 </td>
                             </tr>
                            <tr>
                                 <td style="text-align:right;">
                                    <asp:Label ID="lblMiscellaneousAct" runat="server" Text="Miscellaneous :"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtMiscellaneousAct" runat="server" CssClass="textBox textAutoSelect"  onChange="calculatePanelAct()" onBlur="calculateDifference()" onkeypress=" return isNumberKey(event,this);" ></asp:TextBox>
                                 </td>
                                <td>
                                  <asp:Label runat="server" ID="dfmis"></asp:Label>
                                 </td>
                             </tr>
                        
                            <tr>
                                   <td style="text-align:right;">
                                    <asp:Label ID="lblTotClearingChargeAct" runat="server" Text="Total Charge :"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtTotClearingChargeAct" runat="server" CssClass="textBox textAutoSelect"  onkeypress=" return isNumberKey(event,this);" ></asp:TextBox>
                                 </td>
                                <td>
                                  <asp:Label runat="server" ID="dftclch"></asp:Label>
                                 </td>

                            </tr>
                          
                        </table>

                          </div>

                       


                    </fieldset>

                    
                          <div style="padding-left:0px;">
                              <fieldset style=" height:auto; border:1px solid">
                                  <legend style=" font-size:12px;color:red;">Others Actual</legend>
                              <table>
                                  <tr>
                                      <td></td>
                             
                                      <td></td>

                                 <td style="text-align:right;">
                                    <asp:Label ID="lblSeaFreightAct" runat="server" Text="Sea Freight :"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtSeaFreightAct" runat="server" CssClass="textBox textAutoSelect"  onChange="calculatePanelAct()" onBlur="calculateDifference()" onkeypress=" return isNumberKey(event,this);" ></asp:TextBox>
                                 </td>
                                  <td>
                                  <asp:Label runat="server" ID="lblDiffSeaFreight"></asp:Label>
                                 </td>
                            

                                  </tr>
                              </table>
                               </fieldset>
                          </div>

                </div>

               </div>

                <div id="TotalCostAct" style="font-weight:bold; padding-left:10px;">
                    <table>
                        <tr>
                               <td style="text-align:right;">
                                    <asp:Label ID="lblTotWithVatAct" runat="server" Text="Total With VAT :"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtTotWithVatAct" runat="server" CssClass="textBox textAutoSelect"  onkeypress=" return isNumberKey(event,this);" ></asp:TextBox>
                                 </td>
                                 <td>
                                  <asp:Label runat="server" ID="dfTWv"></asp:Label>
                                 </td>
                                <td></td>
                                   <td style="text-align:right;">
                                    <asp:Label ID="lblTotWithoutVatAct" runat="server" Text="Total W/O VAT :"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtTotWithoutVatAct" runat="server" CssClass="textBox textAutoSelect"  onkeypress=" return isNumberKey(event,this);" ></asp:TextBox>
                                 </td>
                                  <td>
                                  <asp:Label runat="server" ID="dfTWov"></asp:Label>
                                 </td>
                                   <td></td>
                                   <td style="text-align:right;">
                                    <asp:Label ID="lblFactorAct" runat="server" Text="Factor :"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtFactorAct" runat="server" CssClass="textBox textAutoSelect" Width="100px"  onkeypress=" return isNumberKey(event,this);" ></asp:TextBox>
                                 </td>
                                 <td>
                                  <asp:Label runat="server" ID="dfFac"></asp:Label>
                                 </td>
                            <td></td>

                                 <td style="text-align:right;">
                                    <asp:Label ID="lblRateAct" runat="server" Text="Rate :"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtRateAct" runat="server" CssClass="textBox textAutoSelect" Width="120px"  onkeypress=" return isNumberKey(event,this);" ></asp:TextBox>
                                 </td>
                                 <td>
                                  <asp:Label runat="server" ID="dfRt" Style="color:red;"></asp:Label>
                                 </td>
                        </tr>
                    </table>

                </div>
            </div>
                </div>

            <%-- END DV ACTUAL --%>
        </div>

        </div>

              <div id="dvContentFooter" class="" align="left">
            <table>
                <tr>
                    <td style=""></td>
                    <td>
                        <asp:Button ID="btnAddNew" runat="server" Text="New" CssClass="buttonNew" OnClick="btnAddNew_Click"  />
                        
                    </td>
                    <td>
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonSave checkRequired" AccessKey="s" OnClientClick="if ( ! UserSaveConfirmation()) return false;" OnClick="btnSave_Click" />
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="buttonEdit" OnClick="btnEdit_Click" />
                    </td>
                     <td>
                        <asp:Button ID="btnAuthorized" runat="server" Text="Authorize" CssClass="buttonAthorize" OnClick="btnAuthorized_Click" OnClientClick="return confirm('Are you sure to Authorize?');" />
                    </td>
                   
                    <td>
                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" CssClass="buttonRefresh checkIsDirty" OnClick="btnRefresh_Click" />
                    </td>
                    
                    <td>
                        <input id="btnClose" type="button" runat="server" class="buttonClose" value="Close" onclick="if (ContentForm) { ContentForm.CloseForm(); }" />
                        <asp:Button ID="btnFixedCharge" runat="server" OnClick="btnFixedCharge_Click" />
                    </td>
                   
                </tr>
            </table>
        </div>  

    </div>
</asp:Content>
