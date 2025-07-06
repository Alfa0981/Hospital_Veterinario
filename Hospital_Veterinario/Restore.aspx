<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Restore.aspx.cs" Inherits="Hospital_Veterinario.Restore" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Restaurar Base de Datos</h2>
            <p>Seleccione el archivo de respaldo para restaurar la base de datos:</p>
            <asp:FileUpload ID="fuBackup" runat="server" Visible="False" />
            <br />
            <br />
            <asp:Button ID="btnRestore" runat="server" Text="Restaurar" OnClick="btnRestore_Click" CssClass="btn btn-primary" />
            <br />
            <br />
            <asp:Label ID="lblMensaje" runat="server" ForeColor="Green" Visible="false"></asp:Label>
            <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="false"></asp:Label>
            <br />
            <br />
            <asp:Button ID="btnBack" runat="server" Text="Volver" OnClick="btnBack_Click" CssClass="btn btn-secondary" />
        </div>
    </form>
</body>
</html>
