<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="viewDocument.aspx.cs" Inherits="AG_webD2.viewDocument" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="documentDiv" class="container" runat="server"></div>
    <input type="button" runat="server" name="Download" />
</asp:Content>
