<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Bitacora.aspx.cs" Inherits="Hospital_Veterinario.Bitacora" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bitácora - Hospital Veterinario</title>
    <style>
        body {
            background: #f4f8fb;
            font-family: 'Segoe UI', Arial, sans-serif;
        }
        .bitacora-container {
            max-width: 1100px;
            margin: 40px auto;
            background: #fff;
            border-radius: 12px;
            box-shadow: 0 4px 16px rgba(44, 62, 80, 0.08);
            padding: 30px 40px;
        }
        .bitacora-title {
            color: #2176ae;
            font-size: 2em;
            font-weight: 600;
            margin-bottom: 30px;
            text-align: center;
        }
        .bitacora-filtros {
            display: flex;
            flex-wrap: wrap;
            gap: 20px;
            margin-bottom: 25px;
            justify-content: center;
        }
        .bitacora-filtros > div {
            display: flex;
            flex-direction: column;
        }
        .bitacora-filtros label {
            font-weight: 500;
            color: #2176ae;
            margin-bottom: 4px;
        }
        .bitacora-btn-filtrar {
            background: #2176ae;
            color: #fff;
            border: none;
            border-radius: 6px;
            padding: 8px 24px;
            font-size: 1em;
            cursor: pointer;
            align-self: flex-end;
            margin-top: 22px;
            transition: background 0.2s;
        }
        .bitacora-btn-filtrar:hover {
            background: #185a8c;
        }
        .bitacora-table {
            width: 100%;
            border-collapse: collapse;
            margin-top: 10px;
        }
        .bitacora-table th, .bitacora-table td {
            border: 1px solid #d0e3f1;
            padding: 10px 8px;
            text-align: center;
        }
        .bitacora-table th {
            background: #2176ae;
            color: #fff;
        }
        .bitacora-table tr:nth-child(even) {
            background: #f0f6fa;
        }
    </style>
    <script type="text/javascript">
    function validarFechas() {
        var desde = document.getElementById('<%= txtFechaDesde.ClientID %>');
        var hasta = document.getElementById('<%= txtFechaHasta.ClientID %>');

        if (desde.value && hasta.value) {
            var fechaDesde = new Date(desde.value);
            var fechaHasta = new Date(hasta.value);

            if (fechaDesde > fechaHasta) {
                // Si la fecha desde es mayor, iguala la fecha hasta
                hasta.value = desde.value;
            }
            if (fechaHasta < fechaDesde) {
                // Si la fecha hasta es menor, iguala la fecha desde
                desde.value = hasta.value;
            }
        }
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="bitacora-container">
            <div class="bitacora-title">Bitácora de Eventos</div>
            <asp:GridView ID="gvBitacora" runat="server" CssClass="bitacora-table" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="NombreApellido" HeaderText="Nombre y Apellido" />
                    <asp:BoundField DataField="Modulo" HeaderText="Módulo" />
                    <asp:BoundField DataField="Fecha" HeaderText="Fecha" />
                    <asp:BoundField DataField="Hora" HeaderText="Hora" />
                    <asp:BoundField DataField="Criticidad" HeaderText="Criticidad" />
                    <asp:BoundField DataField="Email" HeaderText="Email" />
                </Columns>
            </asp:GridView>

            <div class="bitacora-filtros">
                <div>
                    <label for="txtNombreApellido">Nombre y/o Apellido</label>
                    <asp:TextBox ID="txtNombreApellido" runat="server" />
                </div>

                <div>
                    <label for="txtModulo">Módulo</label>
                    <asp:TextBox ID="txtModulo" runat="server" />
                </div>

                <div>
                    <label for="txtEmail">Email</label>
                    <asp:TextBox ID="txtEmail" runat="server" />
                </div>

                <div style="display: flex; flex-direction: column; min-width: 220px;">
                    <span style="font-weight: 500; color: #2176ae; margin-bottom: 4px;">Filtro por fecha</span>
                    <div style="display: flex; gap: 8px;">
                        <div style="display: flex; flex-direction: column;">
                            <label for="txtFechaDesde" style="font-size: 0.95em;">Desde</label>
                            <asp:TextBox ID="txtFechaDesde" runat="server" TextMode="Date" onchange="validarFechas()"/>
                        </div>
                        <div style="display: flex; flex-direction: column;">
                            <label for="txtFechaHasta" style="font-size: 0.95em;">Hasta</label>
                            <asp:TextBox ID="txtFechaHasta" runat="server" TextMode="Date" onchange="validarFechas()"/>
                        </div>
                    </div>
                </div>

                <div>
                    <label for="txtCriticidad">Criticidad</label>
                    <asp:TextBox ID="txtCriticidad" runat="server" TextMode="Number" />
                </div>

                <div>
                    <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" CssClass="bitacora-btn-filtrar" OnClick="btnFiltrar_Click" />
                </div>
                <div>
                    <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar filtros" CssClass="bitacora-btn-filtrar" OnClick="btnLimpiar_Click" />
                </div>

            </div>
        </div>
    </form>
</body>
</html>
