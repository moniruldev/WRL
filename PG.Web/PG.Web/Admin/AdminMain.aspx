<%@ Page Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AdminMain.aspx.cs" Inherits="PG.Web.Admin.AdminMain" Title="Admin Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Button ID="btnRestart" runat="server" Text="Restart Application" 
        onclick="btnRestart_Click" />
    <asp:Button ID="btnClearCache" runat="server" Text="Clear Cache" 
        onclick="btnClearCache_Click" />
    <asp:Label ID="lblText" runat="server" Text=""></asp:Label>
    </asp:Content>
