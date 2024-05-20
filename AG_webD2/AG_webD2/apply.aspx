<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="apply.aspx.cs" Inherits="AG_webD2.apply" %>



<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <title>Apply</title>
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
    <form id="form1" runat="server">
        
             <h1 runat="server" id="heading">Apply</h1> 
 <input type="text" name="studentNum" readonly placeholder="Student number" required id="studentNum" runat="server">
<%-- <input type="text" name="role" placeholder="Assistant/Tutor" required id="role" runat="server">
 <input type="text" name="module" placeholder="CSC/IFM" required id="module" runat="server">--%>
        <br />
 Assistant Type:
 <select name="role" id="role" runat="server">
     <option value="TUT">Tutor</option>
     <option value="MKR">Marker</option>
 </select> 
        <br />
Preffered Module:
<select name="role" id="module" runat="server">
    <option value="CSC">Computer Science</option>
    <option value="IFM">Informatics</option>
</select> 

<br />
        Status:
        <select name="Status" id="status" runat="server">
    <option value="Pending">Pending</option>
    <option value="Processed">Processed</option>
    <option value="Hired">Hired</option>
</select> 

        <br />
    <asp:Button runat="server" ID="updateStatus" hidden="true" OnClick="onStatusChanged" text="Update Status" ></asp:Button>

        <br />
        <div id="documents" runat="server"></div>

        <br />
 <span id="fileUpload" runat="server">Upload</span>
 <input type="file" name="trascript" required id="transcript" placeholder="Username" runat="server">
  
        <br />
            <asp:Button runat="server" ID="btnApply"  OnClick="btnApply_Clicked" text="Apply" ></asp:Button>
             
        
         
     <asp:Button runat="server" ID="download" OnClick="btnDownload_Clicked" text="Download Contract" ></asp:Button>
     <asp:Button runat="server" ID="upload"  OnClick="btnUpload_Clicked" text="Upload Contract" ></asp:Button>
     <asp:Button runat="server" ID="send"  OnClick="btnSend_Clicked" text="Send Contract" ></asp:Button>
 
      
    </form>
         </div>
   
</body>
</html>