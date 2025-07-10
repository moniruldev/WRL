<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="RoleMenuAssign.aspx.cs" Inherits="PG.Web.Admin.RoleMenuAssign" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   <link href="../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />
  
    <style type="text/css">
        /*.headerStyle            { background-color: #0c5990; color: #fff; font-weight: bold;height:30px }
        .rowStyle               { background-color: #e9ecef; }
        .alternatingRowStyle    { background-color: #fff; }
        .footerStyle            { background-color: #4f5e74; }
        .pagerStyle             { background-color: #409cde; color: #fff; }
        .pagerStyle td table    { margin: auto; }*/


        .gvhspadding
        {
           padding:8px;
        }

        .hiddenClass {
            display: none;
        }


    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <div  id="dvPageContent" style="width:100%; height:100%;" >
 
  <div id="dvContentHeader" class="dvContentHeader">  
    <div id="dvHeader" class="dvHeader">
        <asp:Label ID="lblHeader" runat="server" Text="Role Menu Assign" CssClass="lblHeader"></asp:Label>
    </div>
    <div id="dvMessage" class="dvMessage" >
        <asp:Label ID="lblMessage" runat="server" CssClass="lblMessage" Text="" Width="100%"></asp:Label>
    </div>
  
     <div id="dvHeaderControl" class="dvHeaderControl">
          
     </div>
    
  </div>  
  
  <div id="dvContentMain" class="dvContentMain"> 
       <div id="dvControlsHead" style="height:auto;width:90%;padding-left:40px;padding-top:20px">
   
        <table cellpadding="0" cellspacing="0" >
            <tr>
                <td>
                    Role Name
                </td>
                <td style="padding-left:8px">
                    <asp:DropDownList ID="ddlRole" CssClass="DropDownListWide2" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Menu 
                </td>
                 <td style="padding-left:8px;padding-top:5px">
                    <table border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtMenu" runat="server" Width="206px" CssClass="textBox"></asp:TextBox>
                            </td>
                            <td>
                                <input id="btnMenu" type="button" value="" runat="server" class="buttonDropdown"
                                   />
                                <asp:HiddenField ID="hdnMenu" runat="server" Value="0" />
                                 <asp:HiddenField ID="hdnAppId" runat="server" Value="0" />
                            </td>
                           
                        </tr>
                    </table>
                </td>
            </tr>
             <tr>
                <td>
                    Checked Status
                </td>
                <td style="padding-left:8px;padding-top:5px">
                    <asp:DropDownList ID="ddlCheckedStatus" CssClass="DropDownListWide2" runat="server">
                        <asp:ListItem Text="All" Value="-1" Selected="True"></asp:ListItem>
                         <asp:ListItem Text="Checked" Value="C" ></asp:ListItem>
                         <asp:ListItem Text="UnChecked" Value="U" ></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td></td>
                <td style="padding-left:8px;padding-top:5px"><asp:Button ID="btnLoad" runat="server" CssClass="buttoncommon" Text="Load" OnClick="btnLoad_Click" /></td>
            </tr>
        </table>
           <div style="padding-top:20px"></div>
           <asp:GridView ID="gvRoleMenu" runat="server" BackColor="White" AutoGenerateColumns="False" 
            BorderStyle="None" BorderWidth="5px" CellPadding="10" ForeColor="Black" GridLines="Vertical" 
                CssClass="gridView" AlternatingRowStyle-CssClass="gv_altRow" FooterStyle-CssClass="gv_footer"
                        PagerStyle-CssClass="gv_pager"
               OnRowDataBound="gvRoleMenu_RowDataBound">
             <Columns>
                  <asp:TemplateField ItemStyle-Width="40px" ItemStyle-CssClass="gvhspadding" HeaderStyle-CssClass="gvhspadding">
                   <HeaderTemplate>
                     <input id="checkAll" type="checkbox" onclick = "checkAll(this);" runat="server" />
                    </HeaderTemplate>
                    <ItemTemplate>
                     <asp:CheckBox ID="chkMenuItem" runat="server" onclick = "Check_Click(this)" />
                    </ItemTemplate>
                   </asp:TemplateField>
                 <asp:BoundField HeaderText="Menu ID" DataField="APPMENUID" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                 <asp:BoundField HeaderText="Parent Menu"  DataField="PARENTMENUID"  ItemStyle-Width="170px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"/>
                 <asp:BoundField HeaderText="Menu Name"  DataField="APPMENUTEXT"  ItemStyle-Width="170px" />
                  <asp:BoundField HeaderText="Menu URL"  DataField="APPMENUURL"  ItemStyle-Width="250px" />
                 
                
                 <asp:TemplateField ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderText="Show Menu">
                     <ItemTemplate>
                         <asp:DropDownList ID="ddlShowMenu" runat="server">
                             <asp:ListItem Text="True" Value="True"></asp:ListItem>
                             <asp:ListItem Text="False" Value="False"></asp:ListItem>
                         </asp:DropDownList>
                     </ItemTemplate>
                 </asp:TemplateField>
                 
             </Columns>
                <HeaderStyle CssClass="headerStyle" ForeColor="White" />
            <RowStyle CssClass="rowStyle" />
            <AlternatingRowStyle CssClass="alternatingRowStyle" />
            <FooterStyle CssClass="footerStyle" />
            <PagerStyle CssClass="pagerStyle" ForeColor="White" />
         </asp:GridView>

    
           </div>
      </div>

     <div>

      


     </div>
    
    
   <div id="dvContentFooter" class="dvContentFooter" align="center">
        <table>
            <tr>
                <td></td>

                <td>
                    <asp:Button ID="btnSave" runat="server" Text="Save" Height="26px" Width="105px" CssClass="buttoncommon" OnClick="btnSave_Click"  />
                   
                </td>
               
                <td>
                    <asp:Button ID="btnRefresh" runat="server" Text="Refresh" CssClass="buttonRefresh checkIsDirty"  />
                </td>


                <td></td>


                <td>
                    <input id="btnClose" type="button" runat="server" class="buttonClose" value="Close" onclick="if (ContentForm) { ContentForm.CloseForm(); }" />
                </td>


            </tr>
        </table>

    </div>
 </div>   

    <script type="text/javascript">

        var menuItemListServiceLink = '<%=this.MenuItemListServiceLink%>';

        var txtMenu = '<%=txtMenu.ClientID%>';

        var hdnMenu = '<%=hdnMenu.ClientID%>';

        var hdnAppId = '<%=hdnAppId.ClientID%>';

        var btnMenu = '<%=btnMenu.ClientID%>';

        var btnSave = '<%=btnSave.ClientID%>';




        $(document).ready(function () {

            if ($('#' + txtMenu).is(':visible')) {
                bindMenuList();
            }

            $("#" + btnSave).click(function (e) {

                var rValue = confirm("Are you sure to save?");
                return rValue;
            });

        });



        function checkAll(objRef) {

            var GridView = objRef.parentNode.parentNode.parentNode;

            var inputList = GridView.getElementsByTagName("input");

            for (var i = 0; i < inputList.length; i++) {

                //Get the Cell To find out ColumnIndex

                var row = inputList[i].parentNode.parentNode;

                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {

                    if (objRef.checked) {

                        //If the header checkbox is checked

                        //check all checkboxes

                        //and highlight all rows

                        row.style.backgroundColor = "#409cde";

                        inputList[i].checked = true;

                    }

                    else {

                        //If the header checkbox is checked

                        //uncheck all checkboxes

                        //and change rowcolor back to original

                        if (row.rowIndex % 2 == 0) {

                            //Alternating Row Color

                            row.style.backgroundColor = "#80ade5";

                        }

                        else {

                            row.style.backgroundColor = "white";

                        }

                        inputList[i].checked = false;

                    }

                }

            }

        }

        function Check_Click(objRef) {

            //Get the Row based on checkbox

            var row = objRef.parentNode.parentNode;

            if (objRef.checked) {

                //If checked change color to Aqua

                row.style.backgroundColor = "#409cde";

            }

            else {

                //If not checked change back to original color

                if (row.rowIndex % 2 == 0) {

                    //Alternating Row Color

                    row.style.backgroundColor = "#80ade5";

                }

                else {

                    row.style.backgroundColor = "white";

                }

            }



            //Get the reference of GridView

            var GridView = row.parentNode;



            //Get all input elements in Gridview

            var inputList = GridView.getElementsByTagName("input");



            for (var i = 0; i < inputList.length; i++) {

                //The First element is the Header Checkbox

                var headerCheckBox = inputList[0];



                //Based on all or none checkboxes

                //are checked check/uncheck Header Checkbox

                var checked = true;

                if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {

                    if (!inputList[i].checked) {

                        checked = false;

                        break;

                    }

                }

            }

            headerCheckBox.checked = checked;



        }


       


        function bindMenuList() {
            var cgColumns = [{ 'columnName': 'menuName', 'width': '300', 'align': 'left', 'highlight': 2, 'label': 'Name' }
                             , { 'columnName': 'menuId', 'width': '50', 'align': 'left', 'highlight': 0, 'label': 'ID' }

            ];

         
            var appId = $('#' + hdnAppId).val();
            //var serviceURL = GLAccountServiceLink + "?isterm=1&includeempty=1&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            var serviceURL = menuItemListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            serviceURL += "&namecomptype=" + Enums.DataCompareType.Contains;
            //serviceURL += "&companyid=" + companyid;
            serviceURL += "&ispaging=1";
            serviceURL += "&appId="+appId;
          

            var menuElem = $('#' + txtMenu);

            $('#' + btnMenu).click(function (e) {
                //elmID = $(elem).attr('id');
                //$(elem).combogrid("show");
                $(menuElem).combogrid("dropdownClick");
            });


            $(menuElem).combogrid({
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
                width: 450,
                url: serviceURL,
                search: function (event, ui) {
                    //var companyCode = $('#' + ddlCompany).val();
                    //var newServiceURL = serviceURL + "&companycode=" + companyCode
                    //$(this).combogrid("option", "url", newServiceURL);
                },
                select: function (event, ui) {
                    if (!ui.item) {
                        event.preventDefault();

                        $('#' + hdnMenu).val('0');
                        $('#' + txtMenu).val('');
                        return false;
                        //ClearGLAccountData(elemID);
                    }


                    if (ui.item.menuId == '') {
                        event.preventDefault();
                        return false;
                        //ClearGLAccountData(elemID);
                    }
                    else {

                        $('#' + hdnMenu).val(ui.item.menuId);
                        $('#' + txtMenu).val(ui.item.menuName);
                    }
                    return false;
                },

                lc: ''
            });


            $(menuElem).blur(function () {
                var self = this;

                var menu = $(menuElem).val();
                if (menu == '') {
                    $('#' + hdnMenu).val('0');
                }
            });
        }

    </script>
      <script src="../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
</asp:Content>
