<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="Apoyo.aspx.cs" Inherits="QhatuPUCPPresentacion.Inicio.Apoyo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="container d-flex flex-column align-items-center justify-content-center py-5">
    <!-- Título centrado -->
        <h1 class="text-center mb-4">Apoya Nuestra Plataforma</h1>
        
        <!-- Imagen centrada con tamaño controlado -->
        <div class="text-center mb-4">
            <img src="<%= ResolveUrl("~/Public/images/Apoyo.jpg") %>" alt="Apoyo a la plataforma" class="img-fluid rounded" style="max-width: 300px; height: auto;" />
        </div>
        
        <!-- Texto descriptivo opcional -->
        <p class="text-center col-md-8">
            Tu apoyo nos ayuda a mantener y mejorar esta plataforma para todos los usuarios.
            ¡Agradecemos cualquier contribución!
        </p>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
