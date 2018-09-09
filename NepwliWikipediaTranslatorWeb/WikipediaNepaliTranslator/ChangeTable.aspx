<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangeTable.aspx.cs" Inherits="WikipediaNepaliTranslator.ChangeTable" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #dbField
        {
            height: 125px;
            width: 402px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <textarea id="dbField" runat="server">
    ALTER TABLE dbo.Logs ADD date date NULL, webservice bit NULL
    
    </textarea>
    </div>
    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Button" />
    <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
    </form>
</body>
</html>
