<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="DetallePublicacion.aspx.cs" Inherits="QhatuPUCPPresentacion.Inicio.DetallePublicacion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">

        <div class="container mt-5 mb-5">
        <div class="card p-4 shadow">
            <h2 class="mb-3"><asp:Label ID="lblTitulo" runat="server" /></h2>
            <p><asp:Label ID="lblDescripcion" runat="server" /></p>
            <p class="text-muted"><asp:Label ID="lblFecha" runat="server" /></p>
            <img id="imgPublicacion" runat="server" class="img-fluid mt-3" />
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
