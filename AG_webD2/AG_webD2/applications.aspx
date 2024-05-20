<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="applications.aspx.cs" Inherits="AG_webD2.applications" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Apply</title>
     <style>
    body {
        font-family: Arial, sans-serif;
        background-color: #f2f2f2;
    }

    .container {
        width: 100%; max-width: 80%;
        margin: 0 auto; padding: 20px;
        background-color: #ffffff;
        border-radius: 5px;
        box-shadow: 0 0 10px rgba(0, 0, 0, 0.2);
        text-align: center;
        position: relative; top: 50px;
    }

    .container img {
        width: 100px; height: 100px;
        margin-bottom: 20px;
    }

    .application,
    .container input[type="text"],
    .container input[type="password"],
    .container input[type="email"] {
        width: 100%; padding: 10px;
        margin: 8px 0;
        border: 1px solid #ccc;
        border-radius: 3px;
    }

    .application,
    .container input[type="submit"] {
        background-color: #007bff;
        color: #fff;  border-radius: 3px;
        border: none; padding: 10px 20px;
        cursor: pointer;
    }

    .application,
    .container input[type="submit"]:hover {
        background-color: #0056b3;
    }

    .application
    {
        text-decoration: none;
        display: inline-block;
        width: max-content;
    }

    .container h1 {
        font-size: 24px; margin-bottom: 20px;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="container">
    <h1>Applications</h1>
    <div runat="server" id="applicationsDiv"></div>
</div>
</asp:Content>

<%--<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <title>Apply</title>
     <style>
    body {
        font-family: Arial, sans-serif;
        background-color: #f2f2f2;
    }

    .container {
        width: 100%; max-width: 80%;
        margin: 0 auto; padding: 20px;
        background-color: #ffffff;
        border-radius: 5px;
        box-shadow: 0 0 10px rgba(0, 0, 0, 0.2);
        text-align: center;
        position: relative; top: 50px;
    }

    .container img {
        width: 100px; height: 100px;
        margin-bottom: 20px;
    }

    .application,
    .container input[type="text"],
    .container input[type="password"],
    .container input[type="email"] {
        width: 100%; padding: 10px;
        margin: 8px 0;
        border: 1px solid #ccc;
        border-radius: 3px;
    }

    .application,
    .container input[type="submit"] {
        background-color: #007bff;
        color: #fff;  border-radius: 3px;
        border: none; padding: 10px 20px;
        cursor: pointer;
    }

    .application,
    .container input[type="submit"]:hover {
        background-color: #0056b3;
    }

    .application
    {
        text-decoration: none;
        display: inline-block;
        width: max-content;
    }

    .container h1 {
        font-size: 24px; margin-bottom: 20px;
    }
</style>
</head>
<body>
     <div class="container">
    <form id="form1" runat="server">
        <h1>Applications</h1>
        <div runat="server" id="applicationsDiv"></div>
    </form>
    </div>
   
</body>
</html>--%>