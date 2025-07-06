<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DVForm.aspx.cs" Inherits="Hospital_Veterinario.DVForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Verificación de Integridad</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f8f9fa;
            padding: 40px;
        }

        .dv-container {
            max-width: 500px;
            margin: 0 auto;
            background-color: white;
            border-radius: 12px;
            box-shadow: 0 2px 10px rgba(0,0,0,0.1);
            padding: 30px;
            text-align: center;
        }

        .dv-error {
            color: #dc3545;
            font-size: 18px;
            margin-bottom: 20px;
        }

        .dv-button {
            background-color: #007bff;
            border: none;
            color: white;
            padding: 10px 20px;
            margin: 10px;
            border-radius: 6px;
            cursor: pointer;
            font-size: 16px;
        }

            .dv-button:hover {
                background-color: #0056b3;
            }

        .dv-backup {
            background-color: #6c757d;
        }

            .dv-backup:hover {
                background-color: #5a6268;
            }

        .dv-exit {
            background-color: #dc3545;
        }

            .dv-exit:hover {
                background-color: #c82333;
            }

        .centered-grid {
            margin: 0 auto;
            text-align: center;
        }


    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="dv-container">
            <asp:Label ID="lblError" runat="server" CssClass="dv-error" Text="Se detectaron inconsistencias en los datos."></asp:Label>
            <br />
            <br />
            <asp:GridView ID="dgvErrores" runat="server" CssClass="centered-grid"></asp:GridView>
            <br />
            <asp:Button ID="btnRecalcularDV" runat="server" CssClass="dv-button" Text="Recalcular Dígitos Verificadores" OnClick="btnRecalcularDV_Click" />
            <asp:Button ID="btnRestaurarBackup" runat="server" CssClass="dv-button dv-backup" Text="Restaurar desde Backup" OnClick="btnRestaurarBackup_Click" />

            <asp:Button ID="btnSalir" runat="server" CssClass="dv-button dv-exit" Text="Salir" OnClick="btnSalir_Click" />
            <br />
            <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
            <br />
            <br />
            

        </div>
    </form>
</body>
</html>

