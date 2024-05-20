<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="profile.aspx.cs" Inherits="AG_webD2.profile" %>

<!DOCTYPE html>
<html lang="en">
<head>
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
</head>
<body>
    <div class="container">
        <h1>Profile</h1>
        <form action="profile.aspx" method="post" runat="server" id="formId">
            <input type="text" readonly name="studentNum" placeholder="Student number" required id="studentNum" runat="server">
            <asp:Button runat="server" ID="btnEdit" Text="Edit" OnClick="btnEdit_clicked"></asp:Button>
            <asp:Button runat="server" ID="btnDelete" Text="Delete" OnClick="btnDelete_clicked"></asp:Button>
            <input type="text" name="name" placeholder="Name" required id="name" runat="server">
            <input type="text" name="surname" placeholder="Surname"  required id="surname" runat="server">
            <input type="text" name="username" required id="username" placeholder="Username" runat="server">
            <input type="password" name="password" placeholder="Password" required id="password" runat="server">
            <asp:Button runat="server" ID="btnSave" Text="Save" OnClick="btnSave_clicked"></asp:Button>
           
        </form>
    </div>
</body>
</html>