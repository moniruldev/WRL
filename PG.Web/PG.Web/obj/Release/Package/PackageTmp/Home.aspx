<%@ Page Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="PG.Web.Home" Title="Interwave Accounting - Home" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


   
   <style type="text/css">
       
     
        
         
   </style>



    <script language="javascript" type="text/javascript">
// <!CDATA[

        var isPageResize = true;
     


function tbopen(key)
{
     if(!key)
     {
       key = '';
     }
 
    
    var url = "/Admin/SetPassword.aspx?uid=" + key
    //if (pageInTab == 1)
    if (ZForm.PageMode == Enums.PageMode.InTab)
    {

       var tdata = new xtabdata();
       tdata.linktype = Enums.LinkType.Direct;
       tdata.id = 6320;
       tdata.name = "SetPassword";
       //tdata.label = "User: " + userid;
       tdata.label = "Set Password";
       tdata.type = 0;
       tdata.url = url;
       tdata.tabaction = Enums.TabAction.InTabReuse;
       tdata.selecttab = 1;
       tdata.reload = 0;
       tdata.param = "";
       
                             
       try
       {                                          
        window.parent.OpenMenuByData(tdata);
       }
       catch(err)
       {
           alert("error in page");
       }
   }
   else
   {
      //on new window/tab
       //window.open(url,'_blank');   
   
       window.location = url;
   }
}

function tbopenSalInfo(key) {
    if (!key) {
        key = '';
    }


    var url = "/Master/EmpSalaryInfo.aspx?eid=" + key
    //if (pageInTab == 1)
    if (ZForm.PageMode == Enums.PageMode.InTab) {

        var tdata = new xtabdata();
        tdata.linktype = Enums.LinkType.Direct;
        tdata.id = 6320;
        tdata.name = "EmpSalaryInfo";
        //tdata.label = "User: " + userid;
        tdata.label = "Emp. Salary Sturture";
        tdata.type = 0;
        tdata.url = url;
        tdata.tabaction = Enums.TabAction.InTabReuse;
        tdata.selecttab = 1;
        tdata.reload = 0;
        tdata.param = "";
        
        try {
            window.parent.OpenMenuByData(tdata);
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

function fromParent(val1)
{
    alert('this is called from parent: ' + val1);
}


function showMessage() {
    var msg = 'this is message';
    var newDialog;
    newDialog = $('<div class="popup" title="Save item">' + msg +  '</div>');


    var buttonsConfig = [
    {
        text: "Ok",
        "class": "ok",
        click: function () {
        }
    },
    {
        text: "Annulla",
        "class": "cancel",
        click: function () {
            newDialog.dialog("close");
        }
    }
    ];

    newDialog.dialog({
        resizable: false,
        modal: true,
        show: 'clip',
        buttons: buttonsConfig
    });


//    newDialog.dialog({
//        resizable: false,
//        modal: true,
//        show: 'clip',
//        buttons: {
//            "Ok": function () {
//                newDialog.dialog("close");
//            }
//        }
//    });

//    newDialog.dialog({
//        resizable: false,
//        modal: true,
//        buttons: [{ text: "Yes", click: function () { newDialog.dialog("close"); } }],

//        show: 'clip'

//    });


//    newDialog.dialog({ title: "Success Message",
//        modal: true,
//        buttons: [{ text: "Yes", click: function () { } }, { text: "No", click: function () { $(this).dialog("close"); } }]
//    });
}

// ]]>

function Button1_onclick() {
    showMessage();
}

    </script>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvPageContent" style="width:100%; height:100%;">
         <div id="dvContentHeader" class="dvContentHeader">  
       
        <div>
            <asp:Label ID="lblWelcome" runat="server" Text="Welcome"></asp:Label>
        </div>
         </div>
          <div id="dvContentMain" class="dvContentMain"> 

            
              <input id="Button1" type="button" value="button" style="display:none;" onclick="return Button1_onclick()" /></div>
           <div id="dvContentFooter" class="dvContentFooter">
            </div>

    </div> 
    
</asp:Content>

