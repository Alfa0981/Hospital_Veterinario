<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccesoDenegado.aspx.cs" Inherits="Hospital_Veterinario.AccesoDenegado" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Acceso Denegado</title>
    <style>
        body {
            background: #f4f8fb;
            margin: 0;
            padding: 0;
            font-family: 'Segoe UI', Arial, sans-serif;
        }

        .navbar {
            background-color: #2176ae;
            padding: 12px 20px;
            display: flex;
            justify-content: space-between;
            align-items: center;
            color: white;
        }

        .navbar-title {
            font-size: 1.4em;
            font-weight: bold;
        }

        .navbar-menu {
            display: flex;
            gap: 15px;
        }

        .navbar-button {
            background: none;
            border: none;
            color: white;
            font-size: 1em;
            cursor: pointer;
            transition: opacity 0.2s;
        }

        .navbar-button:hover {
            opacity: 0.8;
        }

        .center-container {
            display: flex;
            justify-content: center;
            align-items: center;
            height: 85vh;
        }

        .error-box {
            background: #fff;
            border-radius: 12px;
            box-shadow: 0 4px 16px rgba(44, 62, 80, 0.08);
            padding: 40px 30px;
            text-align: center;
            min-width: 320px;
        }

        .error-label {
            font-size: 1.4em;
            color: #e74c3c;
            margin-bottom: 20px;
            font-weight: bold;
        }

        .error-description {
            font-size: 1em;
            color: #333;
            margin-bottom: 30px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <!-- Menú superior -->
        <div class="navbar">
            <div class="navbar-title">Hospital Veterinario</div>
            <div class="navbar-menu">
                <asp:Button ID="btnVolver" runat="server" CssClass="navbar-button"
                    Text="Volver al inicio" OnClick="btnVolver_Click" />
            </div>
        </div>

        <!-- Contenido central -->
        <div class="center-container">
            <div class="error-box">
                <div class="error-label">Acceso Denegado</div>
                <div class="error-description">
                    No tenés permisos para acceder a esta sección del sistema.
                </div>
                <asp:Button ID="btnVolver2" runat="server" Text="Volver al inicio"
                    CssClass="navbar-button" OnClick="btnVolver_Click" />
            </div>
        </div>
    </form>
</body>
</html>
