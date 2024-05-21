<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="registration.aspx.cs" Inherits="AG_webD2.registration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8">
<meta name="viewport" content="width=device-width, initial-scale=1.0">
<title>Registration Page</title>
<style>
    body {
        font-family: Arial, sans-serif;
        background-color: #f2f2f2;
    }

    .container {
        width: 100%; max-width: 400px;
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

    .container input[type="text"],
    .container input[type="password"],
    .container input[type="email"] {
        width: 100%; padding: 10px;
        margin: 8px 0;
        border: 1px solid #ccc;
        border-radius: 3px;
    }

    .container input[type="submit"] {
        background-color: #007bff;
        color: #fff;  border-radius: 3px;
        border: none; padding: 10px 20px;
        cursor: pointer;
    }

    .container input[type="submit"]:hover {
        background-color: #0056b3;
    }

    .container h1 {
        font-size: 24px; margin-bottom: 20px;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="container">
     <h1>Register</h1>
         <img src="./Files/AG_Logo.jpg" />
         <br />
         <span>Student Number:</span>
         <input type="text" name="studentNum" placeholder="Student number" required id="studentNum" runat="server"/>
         </br>
         <span>Name:</span>
         <input type="text" name="name" placeholder="Name" required id="name" runat="server"/>
         </br>
         <span>Surname:</span>
         <input type="text" name="surname" placeholder="Surname" required id="surname" runat="server"/>
         </br>
         <span>Username:</span>
         <input type="text" name="username" placeholder="Username" required id="username" runat="server"/>
         </br>
         <span>Password:</span>
         <input type="password" name="password" placeholder="Password" required id="password" runat="server"/>
         </br>
         <asp:Button runat="server" ID="btnRegister" Text="Register" OnClick="btnRegister_clicked"></asp:Button>
 </div>
</asp:Content>

