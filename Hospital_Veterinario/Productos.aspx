<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="Hospital_Veterinario.Productos" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Productos - Hospital Veterinario</title>
    <style>
        body{background:#f4f8fb;font-family:'Segoe UI',Arial,sans-serif;}
        .navbar{background:#2176ae;padding:12px 20px;display:flex;justify-content:space-between;align-items:center;color:#fff}
        .navbar-title{font-size:1.4em;font-weight:600}
        .navbar-menu{display:flex;gap:15px}
        .navbar-button{background:none;border:none;color:#fff;font-size:1em;cursor:pointer;transition:opacity .2s}
        .navbar-button:hover{opacity:.85}

        .productos-container{max-width:1200px;margin:30px auto;background:#fff;border-radius:12px;
            box-shadow:0 4px 16px rgba(44,62,80,.08);padding:26px 30px}
        .page-title{color:#2176ae;font-size:1.8em;font-weight:600;margin:6px 0 20px;text-align:center}
        .section-title{color:#2176ae;font-size:1.2em;font-weight:600;margin:18px 0 8px}
        .table{width:100%;border-collapse:collapse}
        .table th,.table td{border:1px solid #d0e3f1;padding:8px;text-align:center}
        .table th{background:#2176ae;color:#fff}
        .table tr:nth-child(even){background:#f0f6fa}
        .btn{background:#2176ae;color:#fff;border:none;border-radius:6px;padding:6px 12px;cursor:pointer;transition:background .2s}
        .btn:hover{background:#185a8c}
        .btn-outline{background:#fff;color:#2176ae;border:1px solid #2176ae}
        .btn-outline:hover{background:#e8f2f9}
        .qty{width:70px}
        .totals{text-align:right;margin-top:10px;font-weight:700}
        .msg{color:#c0392b;margin:8px 0;display:block}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <!-- Navegador -->
        <div class="navbar">
            <div class="navbar-title">Hospital Veterinario</div>
            <div class="navbar-menu">
                <asp:LinkButton ID="btnNavInicio" runat="server" CssClass="navbar-button"
                    OnClientClick="window.location='Inicio.aspx'; return false;">Inicio</asp:LinkButton>
                <asp:LinkButton ID="btnNavBitacora" runat="server" CssClass="navbar-button"
                    OnClientClick="window.location='Bitacora.aspx'; return false;">Bitácora</asp:LinkButton>
            </div>
        </div>

        <div class="productos-container">
            <asp:Button ID="btnVolver" runat="server" Text="← Volver" CssClass="btn btn-outline"
                        OnClientClick="history.back(); return false;" />

            <div class="page-title">Tienda de Productos</div>
            <asp:Label ID="lblError" runat="server" CssClass="msg" Visible="false" />

            <!-- Catálogo -->
            <div class="section-title">Catálogo</div>
            <asp:GridView ID="gvProductos" runat="server" CssClass="table" AutoGenerateColumns="False"
                OnRowCommand="gvProductos_RowCommand" DataKeyNames="Id">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="ID" />
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                    <asp:BoundField DataField="Precio" HeaderText="Precio" DataFormatString="{0:C}" />
                    <asp:BoundField DataField="StockActual" HeaderText="Stock" />
                    <asp:TemplateField HeaderText="Cantidad">
                        <ItemTemplate>
                            <asp:TextBox ID="txtCantidad" runat="server" CssClass="qty" Text="1" TextMode="Number" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnAgregar" runat="server" CommandName="Agregar"
                                CommandArgument='<%# Container.DataItemIndex %>' Text="Agregar" CssClass="btn" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            <!-- Carrito -->
            <div class="section-title">Carrito</div>
            <asp:GridView ID="gvCarrito" runat="server" CssClass="table" AutoGenerateColumns="False"
                OnRowCommand="gvCarrito_RowCommand" DataKeyNames="ProductoId"
                EmptyDataText="Todavía no agregaste productos.">
                <Columns>
                    <asp:BoundField DataField="ProductoId" HeaderText="ID" />
                    <asp:BoundField DataField="Nombre" HeaderText="Producto" />
                    <asp:BoundField DataField="PrecioUnitario" HeaderText="Precio" DataFormatString="{0:C}" />
                    <asp:TemplateField HeaderText="Cantidad">
                        <ItemTemplate>
                            <asp:TextBox ID="txtCantidadCarrito" runat="server" CssClass="qty"
                                Text='<%# Eval("Cantidad") %>' TextMode="Number" />
                            <asp:Button ID="btnActualizar" runat="server" CommandName="Actualizar"
                                CommandArgument='<%# Container.DataItemIndex %>' Text="Actualizar" CssClass="btn btn-outline" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Subtotal" HeaderText="Subtotal" DataFormatString="{0:C}" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnQuitar" runat="server" CommandName="Quitar"
                                CommandArgument='<%# Container.DataItemIndex %>' Text="Quitar" CssClass="btn btn-outline" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            <div class="totals">
                <asp:Label ID="lblTotal" runat="server" Text="Total: $0,00"></asp:Label>
            </div>

            <div style="text-align:right; margin-top:10px">
                <asp:Button ID="btnLimpiarCarrito" runat="server" Text="Vaciar carrito"
                    CssClass="btn btn-outline" OnClick="btnLimpiarCarrito_Click" />
                <asp:Button ID="btnPagar" runat="server" Text="Pagar" CssClass="btn" Enabled="false" OnClick="btnPagar_Click" />
            </div>
        </div>
    </form>
</body>
</html>
