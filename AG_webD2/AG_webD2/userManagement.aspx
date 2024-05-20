<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="userManagement.aspx.cs" Inherits="AG_webD2.userManagement" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ManageUsers</title>
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
    <form id="form1" runat="server">
        <div>

        </div>
    </form>
</body>
</html>
