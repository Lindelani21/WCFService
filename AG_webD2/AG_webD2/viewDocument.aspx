<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="viewDocument.aspx.cs" Inherits="AG_webD2.viewDocument" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style></style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div  class="container">
        <div id="documentDiv" runat="server"></div>
        <asp:Button runat="server" ID="btnDownload"  OnClick="btnDownload_Clicked" text="Download" ></asp:Button>
    </div>
</asp:Content>
