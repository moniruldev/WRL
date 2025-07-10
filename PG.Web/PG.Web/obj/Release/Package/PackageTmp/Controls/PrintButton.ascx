<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PrintButton.ascx.cs" Inherits="PG.Web.Controls.PrintButton" %>


<style type="text/css">

  .dvPrintOptions
  {
  	    height:auto;
        width:auto;
        display:none;
        border: 1px solid #DCDCDC; 
        background-color: #fff; 
        z-index: 999;
        position: absolute;
  }
  
  .dvPrintOptionsHeader
  {
  	    height:15px;
        width:100%;
        background-color:Blue; 
        z-index: 999;
        text-align:center;
        color:White;
  }
  
  .dvPrintUpdatePanel
  {
  	  	height:0px;
        width:0px;
        display:none;
        visibility:hidden;
  }
  
 
  .dvPrintIFrame
  {
  	  	height:0px;
        width:0px;
  }

</style>

<script lang="javascript" type="text/javascript">
    // <!CDATA[

    var updatePanelPrintButton = '<%=UpdatePanelPrintButton.ClientID%>';
    var hdnPrintReportKey = '<%=hdnPrintReportKey.ClientID%>';
    var hdnPrintError = '<%=hdnPrintError.ClientID%>';
    var hdnPrintReportPageLink = '<%=hdnPrintReportPageLink.ClientID%>';

    var hdnPrintReportViewPdfPageLink = '<%=hdnPrintReportViewPdfPageLink.ClientID%>';


    var hdnPrintAuto = '<%=hdnPrintAuto.ClientID%>';

    var hdnPdfPrint = '<%=hdnPdfPrint.ClientID%>';

    var hdnPrintAction = '<%=hdnPrintAction.ClientID%>';
    var btnPrintAction = '<%=btnPrintAction.ClientID%>';

    var ddlReportViewMode = '<%=ddlReportViewMode.ClientID%>';

    var ifPrintButton = '<%=ifPrintButton.ClientID%>';


    $(document).ready(function () {
        var pageInstancePrintButton = Sys.WebForms.PageRequestManager.getInstance();
        pageInstancePrintButton.add_pageLoaded(function (sender, args) {
            var panels = args.get_panelsUpdated();
            for (i = 0; i < panels.length; i++) {
                if (panels[i].id == updatePanelPrintButton) {
                    ///alert('print');
                    //PrintTask();
                    PrintButtonPrintTask();
                }
            }
        });

        var autoPrint = parseInt($('#' + hdnPrintAuto).val());
        if (autoPrint == 1) {
            PrintButtonPrintTask();
        }
    });


    function PrintButtonPrintTask() {
        var rptKey = $('#' + hdnPrintReportKey).val();
        var rptError = $('#' + hdnPrintError).val();
       

        if (rptError != '') {
            alert(rptError);
            return;
        }

        if (rptKey == '') {
            alert('No Report Specified!');
            return;
        }

        var reportAction = parseInt($('#' + hdnPrintAction).val());
        


        switch (reportAction) {
            case 0:  //preview
                PrintButtonReportOpen(rptKey, false);
                break;
            case 1:  //print
                PrintButtonReportPrint(rptKey);
                break;
            case 2:  //Export
                PrintButtonReportExport(rptKey);
                break;
        }
    }


    function PrintButtonReportOpen(key, showWait) {
        // alert(showWait)

        if (!key) {
            key = '';
        }

        var openType = parseInt($('#' + ddlReportViewMode).val());

        var reportOpen = $('#' + ddlReportViewMode).val();
        var rptPageLink = $('#' + hdnPrintReportPageLink).val();


        //alert(openType);

        //var url = "./Report/ReportView.aspx?rk=" + key
        var url = rptPageLink + "?rk=" + key;

        //if (pageInTab == 1)


        switch (openType) {
            case 0:
                //window.open(url);
                if (IForm.PageMode == Enums.PageMode.InTab) {
                    window.open(url +"&_t=1", "_self");
                }
                else {
                    window.open(url, "_self");
                }
                break;
            case 1:
                //alert(ZForm.PageMode);
                if (ZForm.PageMode == Enums.PageMode.InTab) {
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
                        window.parent.TabMenu.OpenMenuByData(tdata);
                    }
                    catch (err) {
                        alert("error in page");
                    }
                }
                else {
                    //on new window/tab
                    //window.open(url,'_blank');   

                    //window.location = url;
                    window.open(url, "_self");
                }
                break;
            case 2:
                window.open(url,"_blank");
                break;
            case 3:
                alert('Not Implemented.');
                break;
        }
    }

    function PrintButtonReportPrint(key) {
        var rptPageLink = $('#' + hdnPrintReportPageLink).val();

        var pdfPrint = parseInt($('#' + hdnPdfPrint).val());
        if (pdfPrint == 1) {
            rptPageLink = $('#' + hdnPrintReportViewPdfPageLink).val();
        }



        //var url = "./Report/ReportView.aspx?rk=" + key
        var url = rptPageLink + "?rk=" + key;

        iframe = document.getElementById(ifPrintButton);
        if (iframe === null) {
            iframe = document.createElement('iframe');
            iframe.id = hiddenIFrameID;
            //iframe.style.display = 'none';
            document.body.appendChild(iframe);
        }
        iframe.src = url;   
    }

    function PrintButtonReportExport(key) {
        var rptPageLink = $('#' + hdnPrintReportPageLink).val();

        //var url = "./Report/ReportView.aspx?rk=" + key
        var url = rptPageLink + "?rk=" + key;
        
        iframe = document.getElementById(ifPrintButton);
        if (iframe === null) {
            iframe = document.createElement('iframe');
            iframe.id = hiddenIFrameID;
            iframe.style.display = 'none';
            document.body.appendChild(iframe);
        }
        iframe.src = url;   
    }


    function PrintButtonClick(printAction) {
        $('#' + hdnPrintAction).val(printAction);
        switch (printAction) {
            case 0:
                $('#' + btnPrintAction).val('Preview');
                $('#' + btnPrintAction).removeClass('buttonPrint').removeClass('buttonExport').addClass('buttonPrintPreview');
                break;
            case 1:
                $('#' + btnPrintAction).val('Print');
                $('#' + btnPrintAction).removeClass('buttonPrintPreview').removeClass('buttonExport').addClass('buttonPrint');
                break;
            case 2:
                $('#' + btnPrintAction).val('Export');
                $('#' + btnPrintAction).removeClass('buttonPrint').removeClass('buttonPrintPreview').addClass('buttonExport');
                //$('#' + btnPrintAction).addClass('buttonExport');
                break;
        }
        return true;
    }



    jQuery(function ($) {
        //$(document).ready(function() {

        //$('#' + btnQuickLink).one('click', showMenu);






        $('#' + 'btnPrintOption').click(function (ev) {
            showMenu();
        });

        function showMenu() {
            $('#dvPrintOptions').css('left', 0).css('top', 0);
            $('#dvPrintOptions').toggle();
            //var btnLeft = $('#' + btnQuickLink).offset().left;
            //var btnTop = $('#' + btnQuickLink).offset().top; // +$('#divSaveButton').css('padding');

            var btnLeft = $('#' + 'btnPrintOption').offset().left;
            var btnTop = $('#' + 'btnPrintOption').offset().top; // +$('#divSaveButton').css('padding');

            var btnWidth = $('#btnPrintOption').outerWidth();
            var btnHeight = $('#btnPrintOption').outerHeight();


            //var btnTop = $('#' + btnQuickLink).offset().top + $('#' + btnQuickLink).outerHeight(); // +$('#divSaveButton').css('padding');

            var popupWidth = $('#dvPrintOptions').outerWidth();
            var popupHeight = $('#dvPrintOptions').outerHeight();

            //var popupLeft = btnLeft + btnWidth - popupWidth;
            
            //for top,left
//            var popupLeft = btnLeft;
//            var popupTop = btnTop - popupHeight;

            
            //for top right   
            var popupLeft = btnLeft + btnWidth - popupWidth;
            var popupTop = btnTop - popupHeight;

            $('#dvPrintOptions').css('left', popupLeft).css('top', popupTop);
            
            //$('#dvQuickLinks').toggle();
            //$('#dvQuickLinks').slideUp();
            //$('#dvQuickLinks').effect('slideUp');

            //        $('#dvQuickLinks').position({
            //           of: $('#' + btnQuickLink ),
            //           my : "left top",
            //           at: "left bottom",
            //           collision: "filp flip",
            //           offset: "0 2" //left top
            //        });
        }

        function closeMenu() {
            $('#dvPrintOptions').hide();
            //$('#' + btnQuickLink).one('click', showMenu);
        }


        $(document).mousedown(function (ev) {
            if (JSUtility.IsEventInElement(ev, 'dvPrintOptions')) return true;
            if (JSUtility.IsEventInElement(ev, 'btnPrintOption')) return true;
            //$('#dvQuickLinks').hide();
            closeMenu();
        });

        $(window).resize(function (ev) {
            //$('#dvQuickLinks').hide();
            closeMenu();
        });

        $(window).scroll(function (ev) {
            //$('#dvQuickLinks').hide();
            closeMenu();
        });

    });               //button menu

    // ]]>
</script>




<div id="dvPrintButton" runat="server" style="width:auto;height:auto;">
  <table cellpadding="0" cellspacing="0">
     <tr>
       <td>
           <asp:Button ID="btnPrintAction" runat="server" Text="Preview" CssClass="buttonPrintPreview"
                onclick="btnPrintAction_Click" />
       </td>
       <td>
           <input id="btnPrintOption" type="button" value=">" class="buttoncommon" style="width:20px;"/>
       </td>
     </tr>
  </table>

  <div id="dvPrintOptions" class="dvPrintOptions">
        <div id="dvPrintOptionsHeader" class="dvPrintOptionsHeader">
            Print Options
        </div>
        <table>
           <tr>
             <td align="left">
               Export:
             </td>
             <td> 
                 <asp:DropDownList ID="ddlExport" runat="server" CssClass="dropDownList" 
                     Width="110" onselectedindexchanged="ddlExport_SelectedIndexChanged">
                    <asp:ListItem Selected="True" Value="0">PDF</asp:ListItem>
                    <asp:ListItem Value="1">Excel</asp:ListItem>
                    <asp:ListItem Value="2">Word</asp:ListItem>
                 </asp:DropDownList>
             </td>
             <td>
                <asp:Button ID="btnPrintExport" runat="server" Text="Export" CssClass = "buttoncommon"
                      Width="70" onclick="btnPrintExport_Click" OnClientClick="return PrintButtonClick(2);" />
             </td>
           </tr>

           <tr>
            <td>
              
            </td>
            <td>
             
             </td>
           </tr>

           <tr>
            <td align="right">
               Preview:
            </td>
             <td> 
                <asp:DropDownList ID="ddlReportViewMode" runat="server" CssClass="dropDownList" 
                     Width="110" onselectedindexchanged="ddlReportViewMode_SelectedIndexChanged">
                       <asp:ListItem Value="0">In This Tab</asp:ListItem>
                       <asp:ListItem Selected="True" Value="1">In New Tab</asp:ListItem>
                       <asp:ListItem Value="2">In New Window</asp:ListItem>
                       <asp:ListItem Value="3">In Dialog</asp:ListItem>
                   </asp:DropDownList>
             </td>

             <td>
                <asp:Button ID="btnPrintPreview" runat="server" Text="Preview"  CssClass = "buttoncommon"
                     Width="70" onclick="btnPrintPreview_Click" OnClientClick="return PrintButtonClick(0);" />
             </td>
           </tr>

           <tr>
             <td>
              
             </td>
             <td>
    
             </td>

             <td>
                <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass = "buttoncommon" 
                     Width="70" onclick="btnPrint_Click" OnClientClick="return PrintButtonClick(1);" />
             </td>
           </tr>
        </table>
  </div>


  <div id="dvPrintUpdatePanel" class="dvPrintUpdatePanel">
      <asp:UpdatePanel ID="UpdatePanelPrintButton" runat="server" 
          UpdateMode="Conditional">
        <ContentTemplate>
              <table>
                <tr>
                   <td>
                       <asp:HiddenField ID="hdnPrintReportKey" runat="server" Value="" />
                       <asp:HiddenField ID="hdnPrintError" runat="server" Value="" />
                       <asp:HiddenField ID="hdnPrintReportPageLink" runat="server" Value="" />
                       <asp:HiddenField ID="hdnPrintReportViewPdfPageLink" runat="server" Value="" />

                       <asp:HiddenField ID="hdnPrintAction" runat="server" Value="0" />
                       <asp:HiddenField ID="hdnPrintAuto" runat="server" Value="0" />

                       <asp:HiddenField ID="hdnPdfPrint" runat="server" Value="0" />
                   </td>
                </tr>
                
              </table>
              </ContentTemplate>
             <Triggers>
                 <asp:AsyncPostBackTrigger ControlID="btnPrintAction" EventName="Click">
                 </asp:AsyncPostBackTrigger>
                 <asp:AsyncPostBackTrigger ControlID="btnPrintPreview" EventName="Click" />
                 <asp:AsyncPostBackTrigger ControlID="btnPrint" EventName="Click" />
                 <asp:AsyncPostBackTrigger ControlID="btnPrintExport" EventName="Click" />
             </Triggers>
      </asp:UpdatePanel>
  </div>


  <div id="dvPrintIFrame" class="dvPrintIFrame" >
      <iframe id="ifPrintButton" runat="server" width="0" height="0"></iframe>
      

  </div>

</div>

