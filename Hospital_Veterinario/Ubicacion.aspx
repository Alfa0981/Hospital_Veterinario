<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ubicacion.aspx.cs" Inherits="Hospital_Veterinario.Ubicacion" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Ubicación - Hospital Veterinario</title>
    <style>
        body {
            background: #f4f8fb;
            font-family: 'Segoe UI', Arial, sans-serif;
        }
        .ubicacion-container {
            max-width: 1100px;
            margin: 40px auto;
            background: #fff;
            border-radius: 12px;
            box-shadow: 0 4px 16px rgba(44, 62, 80, 0.08);
            padding: 30px 40px;
        }
        .ubicacion-title {
            color: #2176ae;
            font-size: 2em;
            font-weight: 600;
            margin-bottom: 30px;
            text-align: center;
        }
        .ubicacion-subtitle {
            text-align: center;
            color: #666;
            margin-bottom: 20px;
        }
        .ubicacion-btn-volver {
            background: #2176ae;
            color: #fff;
            border: none;
            border-radius: 6px;
            padding: 8px 24px;
            font-size: 1em;
            cursor: pointer;
            transition: background 0.2s;
        }
        .ubicacion-btn-volver:hover {
            background: #185a8c;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">        
        <div class="ubicacion-container">
            <div style="margin-bottom: 20px;">
                <asp:Button ID="btnVolver" runat="server" Text="← Volver" CssClass="ubicacion-btn-volver" OnClientClick="history.back(); return false;" />
            </div>

            <div class="ubicacion-title">📍 Nuestra ubicación en Palermo</div>
            <div class="ubicacion-subtitle">Av. Santa Fe 3253, Palermo, CABA</div>

            <div style="border-radius:12px; overflow:hidden; box-shadow:0 2px 12px rgba(0,0,0,.08);">
                <iframe
                    title="Mapa - Palermo"
                    style="width:100%; height:600px; border:0;"
                    src="https://www.google.com/maps?hl=es&q=Av.%20Santa%20Fe%203253,%20Palermo,%20Buenos%20Aires&z=16&output=embed"
                    allowfullscreen
                    loading="lazy">
                </iframe>
            </div>
        </div>
    </form>
</body>
</html>
