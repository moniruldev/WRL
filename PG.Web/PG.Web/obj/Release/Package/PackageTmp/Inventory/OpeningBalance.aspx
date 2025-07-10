<%@ Page Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="OpeningBalance.aspx.cs" Inherits="PG.Web.Inventory.OpeningBalance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">
        // <!CDATA[

        var isPageResize = true;
        ContentForm.CalendarImageURL = "../../image/calendar.png";

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


            $("#groupBox").height(contInnerHeight - 10);
            var groupHeight = $("#groupBox").height();
            var groupHeaderHeight = $("#groupHeader").height();
            var groupFooterHeight = $("#groupFooter").height();
            $("#groupContent").height(groupHeight - groupHeaderHeight - groupFooterHeight - 2);

        }



        function fromParent(val1) {
            alert('this is called from parent: ' + val1);
        }

        function Button1_onclick() {
            //document.getElementById("btnSave").click();
            ///__doPostBack("<%= this.btnSave.UniqueID %>", "");
            __doPostBack("btnSave", "");
        }


         // ]]>
    </script>
   <style type="text/css">
    
  /*.FixedHeader { POSITION: relative; TOP: expression(this.offsetParent.scrollTop); BACKGROUND-COLOR: white } */
    
    .FixedHeader { POSITION: relative; BACKGROUND-COLOR: white }
    
    #dvMessage
    {
        height: 20px;
    }
    
    .style1
    {
        width: 113px;
    }
    

    
       .auto-style34 {
           width: 43px;
       }
       .auto-style38 {
           width: 87px;
       }
       .auto-style39 {
           width: 101px;
       }
       .auto-style40 {
           width: 88px;
       }
       .auto-style41 {
           width: 99px;
       }
    

    
   </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvPageContent" style="width:100%; height:auto;">
    <div id="dvContentHeader" class="dvContentHeader">
    <div id="dvHeader" class="dvHeader" >
        <asp:Label ID="lblHeader" CssClass="lblHeader" runat="server" Text="Opening Balance"></asp:Label>
    </div>
    <!--Message Div -->
    <div id="dvMsg" runat="server" class="dvMessage">
        <asp:Label ID="lblMessage" runat="server" Font-Size="Small" Width="100%"> </asp:Label>
    </div>
     <div id="dvHeaderControl" class="dvHeaderControl">
     </div>
    </div>

    <div id="dvContentMain" class="dvContentMain">
    <div id = "dvControls" style="height:auto; width:100%">
        <div id="dvControlsInner" class="groupBoxContainer boxShadow">    
             <div id="groupBox">
                  <div id="groupHeader" class="groupHeader">
                      <div style="width:100%;height:20px;">
                         <table>
                            <tr>
                             <td>
                                <div id="dvIconEditMode" class="iconView" runat="server" ></div>
                             </td>
                             <td>
                                <span>Opening Balance</span> 
                             </td>
                            </tr>
                         </table>
                         
                      </div>
                      
                  </div>
                  <div id="groupContent" class="groupContent" style="width:70%; height:auto; overflow:auto;">
                  <div id="groupContenInner" style="width:100%;height:auto; text-align:left;">
              <table style="text-align:left;" border="0" cellspacing="4" cellpadding="2">
                 <tr>
                 <td>
                     <asp:TextBox ID="txtItemName" runat="server" text="Item Code" visible="false" />
                     <asp:TextBox ID="txtUnit" runat="server" text="Unit Code" visible="false" />
                     <asp:TextBox ID="txtSlNo" runat="server" text="sl" visible="false" />
                 </td>
                 <td>
                  <asp:Label ID="lblItem" runat="server" Text="Item Name" ></asp:Label>
                 </td>
                 <td>
                 <asp:TextBox id="txtItemNameShow" runat="server" CssClass="textBox"></asp:TextBox>
                 </td>
               </tr>

                <tr>
                 <td>
                 </td>
                 <td>
                   <asp:Label ID="Label1" runat="server" Text="Unit" ></asp:Label>
                 </td>
                 <td>
                    <asp:TextBox id="txtUnitShow" runat="server" CssClass="textBox"></asp:TextBox>
                 </td>
               </tr>

                <tr>
                 <td>
                 </td>
                 <td>
                  <asp:Label ID="lblOpeningQnty" runat="server" Text="Opening Quantity" ></asp:Label>
                 </td>
                 <td>
                 <asp:TextBox id="txtOpeningQnty" runat="server" CssClass="textBox"></asp:TextBox>
                 </td>
               </tr>
                <tr>
                 <td>
                 </td>
                 <td>
                 <asp:Label id="lblRate" runat="server" Text="Rate" ></asp:Label>
                 </td>
                 <td>
                    <asp:TextBox id="txtRate" runat="server"  CssClass="textBox"></asp:TextBox> 
                    
                 </td>
                 <td>
                 </td>
               </tr>
                <tr>
                 <td>
                 </td>
                <td>
                   <asp:Label id="lblStore" runat="server" Text="Store" ></asp:Label>
                 </td>
                 <td style="" align="left">
                   <asp:DropDownList ID="ddlStore" runat="server" Width="165"  CssClass="dropDownList enableIsDirty"> </asp:DropDownList>
                 </td>
                 <td>
                 
                 </td>
               </tr>
               <tr>
                 <td>
                 </td>
                  <td>
                      <asp:Label ID="lblAuditDate" runat="server" Text="Audit Date"></asp:Label>
                      </td>                       
                          <td>                                                              
                          <asp:TextBox ID="txtAuditDate" runat="server" Width="160px" CssClass="textBox textDate dateParse"></asp:TextBox>
                         </td>
                   <td>

                   </td>
               </tr>


               <tr>
                 <td>
                 </td>
                 <td>
                 
                 </td>
                  <td>
                 <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonSave" onclick="btnSave_Click" />
                </td>
                 <td>
                 </td>
               </tr>
              </table>
              </div>

              </div>
                  
            </div>
          </div>  


        </div>  
    
         </div>
         


   
    </div> 
</asp:Content>
