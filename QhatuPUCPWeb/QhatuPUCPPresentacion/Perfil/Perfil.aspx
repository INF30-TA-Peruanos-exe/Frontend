<%@ Page Title="Perfil" Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="Perfil.aspx.cs" Inherits="QhatuPUCPPresentacion.Perfil.Perfil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Public/css/estilos_perfil.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="container mt-5 mb-5">
        <div class="row justify-content-center">
            <div class="col-md-8">
                <div class="card shadow" style="background-color: #f8f9fa;">
                    <div class="card-body p-5">
                        <h2 class="text-center mb-4">Mi Perfil</h2>
                        <hr />
                        <asp:Label ID="lblNombre" runat="server" CssClass="perfil-info d-block mb-2" Text="Nombre: -"></asp:Label>
                        <asp:Label ID="lblCodigoPUCP" runat="server" CssClass="perfil-info d-block mb-2" Text="Código PUCP: -"></asp:Label>
                        <asp:Label ID="lblCorreo" runat="server" CssClass="perfil-info d-block mb-2" Text="Correo: -"></asp:Label>
                        <asp:Label ID="lblNombreUsuario" runat="server" CssClass="perfil-info d-block mb-2" Text="Nombre de Usuario: -"></asp:Label>
                        <asp:Label ID="lblEstado" runat="server" CssClass="perfil-estado badge mt-2" Text="Estado: -"></asp:Label>
                    </div>
                </div>
            </div>
        </div>

        <!-- Publicaciones del usuario -->
        <div class="row justify-content-center mt-5">
            <div class="col-md-10">
                <h3 class="mb-4">Mis Publicaciones</h3>
                <asp:Repeater ID="rptPublicaciones" runat="server">
                    <ItemTemplate>
                        <div class="card mb-3">
                            <div class="card-body">
                                <h5 class="card-title"><%# Eval("titulo") %></h5>
                                <p class="card-text"><%# Eval("descripcion") %></p>
                                <small class="text-muted">Publicado el <%# FormatearFechaString(Eval("fechaPublicacion")) %></small>

                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
</div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
