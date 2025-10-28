<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConfirmarCompra.aspx.cs" Inherits="Hospital_Veterinario.ConfirmarCompra" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
     <title>Confirmar Compra - Hospital Veterinario</title>
    <style>
        body{background:#f4f8fb;font-family:'Segoe UI',Arial,sans-serif;}
        .container{max-width:900px;margin:40px auto;background:#fff;padding:20px;
                   border-radius:12px;box-shadow:0 4px 16px rgba(44,62,80,.08);}
        h2{color:#2176ae;text-align:center;}
        .table{width:100%;border-collapse:collapse;margin-top:20px;}
        .table th,.table td{border:1px solid #d0e3f1;padding:8px;text-align:center;}
        .table th{background:#2176ae;color:#fff;}
        .btn{background:#2176ae;color:#fff;border:none;border-radius:6px;padding:8px 14px;
             cursor:pointer;transition:background .2s;margin-top:20px;}
        .btn:hover{background:#185a8c;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>Confirmar Compra</h2>
            <asp:GridView ID="gvResumen" runat="server" CssClass="table" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="ProductoId" HeaderText="ID" />
                    <asp:BoundField DataField="Nombre" HeaderText="Producto" />
                    <asp:BoundField DataField="PrecioUnitario" HeaderText="Precio" DataFormatString="{0:C}" />
                    <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                    <asp:BoundField DataField="Subtotal" HeaderText="Subtotal" DataFormatString="{0:C}" />
                </Columns>
            </asp:GridView>

            <asp:Label ID="lblTotal" runat="server" Text="Total: $0,00" Font-Bold="true"></asp:Label><br />
            <asp:Button ID="btnConfirmarPago" runat="server" Text="Confirmar Pago" CssClass="btn" OnClick="btnConfirmarPago_Click" />
        </div>
    </form>
</body>
</html>
