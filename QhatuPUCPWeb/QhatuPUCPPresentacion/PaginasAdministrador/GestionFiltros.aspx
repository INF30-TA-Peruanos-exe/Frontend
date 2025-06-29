<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayoutMasterAdmin.Master" AutoEventWireup="true" CodeBehind="GestionFiltros.aspx.cs" Inherits="QhatuPUCPPresentacion.PaginasAdministrador.GestionFiltros" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .card-filtro {
            transition: all 0.3s ease;
            border-radius: 1rem;
            border: none;
            box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
        }

        .card-filtro:hover {
            transform: translateY(-5px);
            box-shadow: 0 6px 16px rgba(0, 0, 0, 0.15);
        }

        .icono-filtro {
            font-size: 2.5rem;
            color: #0d6efd;
        }

        .titulo-filtro {
            font-weight: 600;
            font-size: 1.2rem;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="container my-5">
        <h2 class="fw-bold text-white text-center mb-4">Gestión de Filtros</h2>
        <div class="row row-cols-1 row-cols-md-3 g-4">

            <!-- Card 1: Facultad -->
            <div class="col">
                <a href="<%= ResolveUrl("~/Filtros/ListaFacultad.aspx") %>" class="text-decoration-none">
                    <div class="card card-filtro h-100 text-center p-4">
                        <i class="fas fa-university icono-filtro mb-3"></i>
                        <div class="titulo-filtro">Facultades</div>
                        <p class="text-muted">Gestiona las facultades registradas en el sistema.</p>
                    </div>
                </a>
            </div>

            <!-- Card 2: Especialidad -->
            <div class="col">
                <a href="<%= ResolveUrl("~/Filtros/ListaEspecialidad.aspx") %>" class="text-decoration-none">
                    <div class="card card-filtro h-100 text-center p-4">
                        <i class="fas fa-graduation-cap icono-filtro mb-3"></i>
                        <div class="titulo-filtro">Especialidades</div>
                        <p class="text-muted">Edita y visualiza las especialidades disponibles.</p>
                    </div>
                </a>
            </div>

            <!-- Card 3: Curso -->
            <div class="col">
                <a href="<%= ResolveUrl("~/Filtros/ListaCurso.aspx") %>" class="text-decoration-none">
                    <div class="card card-filtro h-100 text-center p-4">
                        <i class="fas fa-book icono-filtro mb-3"></i>
                        <div class="titulo-filtro">Cursos</div>
                        <p class="text-muted">Administra los cursos que pueden vincularse.</p>
                    </div>
                </a>
            </div>

        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
