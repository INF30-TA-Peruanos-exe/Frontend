<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="Postear.aspx.cs" Inherits="QhatuPUCPPresentacion.HacerPubli.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="estilos_postear.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <asp:Panel ID="pnlCrearPublicacion" runat="server" CssClass="card p-4 shadow-sm">
        <h3 class="mb-4">Crear nueva publicación</h3>

        <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger mb-3 d-block" />

        <!-- Título -->
        <div class="mb-3">
            <label for="txtTitulo" class="form-label">Título</label>
            <asp:TextBox ID="txtTitulo" runat="server" CssClass="form-control" placeholder="Ingresa el título" />
        </div>

        <!-- Descripción -->
        <div class="mb-3">
            <label for="txtDescripcion" class="form-label">Descripción</label>
            <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" placeholder="Escribe la descripción de tu publicación" />
        </div>

        <!-- Cursos -->
        <div class="mb-3">
            <label class="form-label">Cursos asociados</label>
            <asp:CheckBoxList ID="chkCursos" runat="server" CssClass="form-check mb-2 d-block" RepeatDirection="Vertical" />
        </div>

        <!-- Facultades -->
        <div class="mb-3">
            <label class="form-label">Facultades asociadas</label>
            <asp:CheckBoxList ID="chkFacultades" runat="server" CssClass="form-check mb-2 d-block" RepeatDirection="Vertical" />
        </div>

        <!-- Especialidades -->
        <div class="mb-3">
            <label class="form-label">Especialidades asociadas</label>
            <asp:CheckBoxList ID="chkEspecialidades" runat="server" CssClass="form-check mb-2 d-block" RepeatDirection="Vertical" />
        </div>

        <!-- Imagen -->
        <div class="mb-3">
            <label class="form-label">Imagen</label>
            <div class="form-text">Se usará una imagen por defecto: <code>/Public/images/imagen-ataque.jpg</code></div>
        </div>

        <!-- Botón -->
        <asp:Button ID="btnPublicar" runat="server" Text="Publicar" CssClass="btn btn-primary" OnClick="btnPublicar_Click" />
    </asp:Panel>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
