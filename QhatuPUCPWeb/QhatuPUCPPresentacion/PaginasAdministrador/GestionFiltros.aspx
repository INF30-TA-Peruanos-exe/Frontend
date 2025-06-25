<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayoutMasterAdmin.Master" AutoEventWireup="true" CodeBehind="GestionFiltros.aspx.cs" Inherits="QhatuPUCPPresentacion.PaginasAdministrador.GestionFiltros" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="container mb-4">
    <div class="container-fluid px-4 py-3 mb-4 bg-light border-bottom">
        <div class="d-flex justify-content-between align-items-center">
            <h3 class="fw-bold text-primary mb-0">
                Administración de Filtros
            </h3>
        </div>
    </div>
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
