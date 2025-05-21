<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="Hospital_Veterinario.Inicio" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Inicio - Hospital Veterinario</title>
    <style>
        body {
            background: #f4f8fb;
            margin: 0;
            padding: 0;
            font-family: 'Segoe UI', Arial, sans-serif;
        }
        .center-container {
            display: flex;
            justify-content: center;
            align-items: center;
            height: 90vh;
        }
        .welcome-box {
            background: #fff;
            border-radius: 12px;
            box-shadow: 0 4px 16px rgba(44, 62, 80, 0.08);
            padding: 40px 30px;
            text-align: center;
            min-width: 320px;
        }
        .welcome-label {
            font-size: 1.5em;
            color: #2176ae;
            margin-bottom: 30px;
            font-weight: 600;
        }
        .bitacora-btn {
            background: #2176ae;
            color: #fff;
            border: none;
            border-radius: 6px;
            padding: 12px 32px;
            font-size: 1.1em;
            cursor: pointer;
            transition: background 0.2s;
        }
        .bitacora-btn:hover {
            background: #185a8c;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="center-container">
            <div class="welcome-box">
                <asp:Label ID="lblBienvenida" runat="server" CssClass="welcome-label" Text="¡Bienvenido!"></asp:Label>
                <br />
                <asp:Button ID="btnBitacora" runat="server" CssClass="bitacora-btn" Text="Bitacora" OnClick="btnBitacora_Click" />
            </div>
        </div>
    </form>
</body>
</html>
