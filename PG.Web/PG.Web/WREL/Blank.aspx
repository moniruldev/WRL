<%@ Page Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="Blank.aspx.cs" Inherits="PG.Web.WREL.Blank" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">



    <script src="../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
    <script src="../javascript/jquery.attributeobserver.js" type="text/javascript"></script>
    <link href="../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />


    <script language="javascript" type="text/javascript">

        function PageResizeCompleted(pg, cntMain) {
            resizeContentInner(cntMain);

        }

        function resizeContentInner(cntMain) {
            var contHeight = $("#dvContentMain").height();
            var contHead = $("#dvControlsHead").height();
            var contFooter = $("#dvControlsFooter").height();

            var contInnerHeight = contHeight - contHead - contFooter - 5;
            $("#dvControls").height(contInnerHeight);

            $("#dvControlsInner").height(contInnerHeight - 10);
            $("#dvGridContainer").height(contInnerHeight - 10);
            var gridHeight = $("#dvGridContainer").height();
            var gridHeaderHeight = $("#dvGridHeader").height();
            var gridFooterHeight = $("#dvGridFooter").height();
            $("#dvGrid").height(gridHeight - gridHeaderHeight - gridFooterHeight - 2);
        }


        $(document).ready(function () {


        });
 


        //function tbopen(key, userid) {
        //    if (!key) {
        //        key = '';
        //    }

        //    var url = IForm.RootPath + "WREL/ParcelCreation.aspx?id=" + key;

        //    if (IForm.PageMode == Enums.PageMode.InTab) {

        //        var tdata = new xtabdata();
        //        tdata.linktype = Enums.LinkType.Direct;
        //        tdata.id = 0;
        //        tdata.name = "Parcel Creation";
        //        tdata.label = "Parcel Creation";
        //        tdata.type = 0;
        //        tdata.url = url;
        //        tdata.tabaction = Enums.TabAction.InNewTab;
        //        tdata.selecttab = 1;
        //        tdata.reload = 0;
        //        tdata.param = "";


        //        try {
        //            window.parent.TabMenu.OpenMenuByData(tdata);
        //        }
        //        catch (err) {
        //            alert("error in page");
        //        }
        //    }
        //    else {
        //        window.location = url;
        //    }
        //}

        function tbopen(pageName, label, key, userId) {
            if (!key) {
                key = '';
            }

            var url = IForm.RootPath + "WREL/" + pageName + ".aspx?id=" + key;

            if (IForm.PageMode == Enums.PageMode.InTab) {
                var tdata = new xtabdata();
                tdata.linktype = Enums.LinkType.Direct;
                tdata.id = 0;
                tdata.name = label;
                tdata.label = label;
                tdata.type = 0;
                tdata.url = url;
                tdata.tabaction = Enums.TabAction.InNewTab;
                tdata.selecttab = 1;
                tdata.reload = 0;
                tdata.param = "";

                try {
                    window.parent.TabMenu.OpenMenuByData(tdata);
                } catch (err) {
                    alert("error in page");
                }
            } else {
                window.location = url;
            }
        }


     
    </script>


</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     
    <div class="container py-5">
      
    </div>
</asp:Content>
