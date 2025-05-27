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
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <!-- Menú superior -->
        <div class="navbar">
    <div class="navbar-title">Hospital Veterinario</div>
    <div class="navbar-menu">
        <asp:LinkButton ID="btnMascotas" runat="server" CssClass="navbar-button"
            OnClientClick="alert('Sección Mascotas en proceso'); return false;">Mascotas</asp:LinkButton>

        <asp:LinkButton ID="btnProfesionales" runat="server" CssClass="navbar-button"
            OnClientClick="alert('Sección Profesionales en proceso'); return false;">Profesionales</asp:LinkButton>

        <asp:LinkButton ID="btnProductos" runat="server" CssClass="navbar-button"
            OnClientClick="alert('Sección Productos en proceso'); return false;">Productos</asp:LinkButton>

        <asp:LinkButton ID="btnComercio" runat="server" CssClass="navbar-button"
            OnClientClick="alert('Sección Comercio en proceso'); return false;">Comercio</asp:LinkButton>

        <asp:LinkButton ID="btnUsuarios" runat="server" CssClass="navbar-button"
            OnClientClick="alert('Sección Usuarios en proceso'); return false;">Usuarios</asp:LinkButton>

        <asp:Button ID="btnBitacora" runat="server" CssClass="navbar-button"
            Text="Bitácora" OnClick="btnBitacora_Click" />

        <asp:Button ID="btnCerrarSesion" runat="server" CssClass="navbar-button"
            Text="Cerrar sesión" OnClick="btnCerrarSesion_Click" />
    </div>
</div>

        <!-- Contenido central -->
        <div class="center-container">
            <div class="welcome-box">
                <asp:Label ID="lblBienvenida" runat="server" CssClass="welcome-label" Text="¡Bienvenido!"></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>
