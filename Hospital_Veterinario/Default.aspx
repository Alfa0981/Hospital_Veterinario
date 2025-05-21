<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Hospital_Veterinario._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main style="display: flex; justify-content: center; align-items: center; height: 80vh;">
        <div style="border: 1px solid #ccc; padding: 30px; border-radius: 10px; width: 300px; box-shadow: 0 0 10px rgba(0,0,0,0.1);">
            <h2 style="text-align: center; color: #2c3e50;">Login - Hospital Veterinario</h2>
            
            <div style="margin-bottom: 15px;">
                <label for="txtUsername">Usuario</label><br />
                <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" Width="100%" />
            </div>
            
            <div style="margin-bottom: 15px;">
                <label for="txtPassword">Contraseña</label><br />
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" Width="100%" />
            </div>
            
            <div style="text-align: center;">
                <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-primary" />
            </div>
        </div>
    </main>
</asp:Content>

