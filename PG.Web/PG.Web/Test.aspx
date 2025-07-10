<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="PG.Web.Test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script language="javascript" type="text/javascript">
// <![CDATA[

 // fire fox silent print  about:config. Add print.always_print_silent

        function Button2_onclick() {
            //var x = document.getElementById('objPDF');
            var x = document.getElementById('pdfDocument');
            //alert('sdf');
            //x.setActive();
            x.focus();

           // window.print();
           // x.contentWindow.print();
           x.print();
            //x.printAll();
        }

// ]]>
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
       this is test page
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Button" />
        <input id="Button2" type="button" value="button" onclick="return Button2_onclick()" /></div>
    <div style="width:60px;height:40px;">
       
    
    </div>


    <div  style="width:600px;height:400px;">
    <object 
       id="pdfDocument" 
       type="application/pdf" 
       data="test.pdf#toolbar=1&navpanes=0&scrollbar=1&page=1&zoom=100"  
       width="500px" 
       height="300px">
       <p>It appears you don't have a PDF plugin for your browser. <a target="_blank" href="test.pdf">Click here to download the PDF file.</a>
       </p>
       </object>

    </div>

    </form>
</body>
</html>
