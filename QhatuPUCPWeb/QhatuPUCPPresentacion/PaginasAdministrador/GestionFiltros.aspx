<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayoutMasterAdmin.Master" AutoEventWireup="true" CodeBehind="GestionFiltros.aspx.cs" Inherits="QhatuPUCPPresentacion.PaginasAdministrador.GestionFiltros" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="container mb-4">
        <h2 class="fw-bold text-white">Gestión de Filtros</h2>
        <div class="row g-3"> <!-- g-3 agrega espacio entre columnas -->
            <div class="col-12 col-md">
                <a href="<%= ResolveUrl("~/Filtros/ListaFacultad.aspx") %>" class="btn btn-light btn-lg w-100 text-start">
                    Facultades
                </a>
            </div>
            <div class="col-12 col-md">
                <a href="<%= ResolveUrl("~/Filtros/ListaEspecialidad.aspx") %>" class="btn btn-light btn-lg w-100 text-start">
                    Especialidades
                </a>
            </div>
            <div class="col-12 col-md">
                <a href="<%= ResolveUrl("~/Filtros/ListaCurso.aspx") %>" class="btn btn-light btn-lg w-100 text-start">
                    Cursos
                </a>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
