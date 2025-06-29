<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayoutMasterAdmin.Master" AutoEventWireup="true" CodeBehind="ListaEspecialidad.aspx.cs" Inherits="QhatuPUCPPresentacion.Filtros.ListaEspecialidad" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <style>
        /* Solo afecta placeholders en esta página */
        input::placeholder,
        textarea::placeholder {
            color: #ffffff !important;
            opacity: 1;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="container mt-4">
        <!-- Título -->
        <div class="text-center mb-4">
            <h2 class="fw-bold text-white">
                <i class="fas fa-graduation-cap icono-filtro mb-3"></i>Gestión de Especialidades
            </h2>
        </div>

        <!-- Barra de búsqueda y botón de Agregar -->
        <div class="row justify-content-center align-items-center mb-4 g-2">

            <!-- Campo de búsqueda -->
            <div class="col-md-6 col-sm-12">
                <div class="input-group shadow-sm">
                    <span class="input-group-text bg-secondary border-0 text-white">
                        <i class="fa-solid fa-magnifying-glass"></i>
                    </span>
                    <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control bg-dark text-white border-0" placeholder="Buscar por nombre..." />
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-outline-light" OnClick="btnBuscar_Click" />
                </div>
            </div>

            <!-- Botón Agregar -->
            <div class="col-md-3 col-sm-12">
                <asp:LinkButton  ID="BtnAgregar" runat="server"
                    Text='<i class="fa-solid fa-plus me-1"></i>Agregar Especialidad'
                    OnClick="BtnAgregar_Click"
                    CssClass="btn btn-success fw-semibold shadow-sm w-100"
                    UseSubmitBehavior="false" />
            </div>
        </div>
    </div>
<!-- Tabla con datos -->    
    <div class="container p-4 bg-light rounded shadow-sm">
        <table class="table table-hover table-bordered align-middle">
            <thead class="table-dark text-center align-middle">
            <tr>
                <th scope="col"><i class="fa-solid fa-hashtag me-1"></i>ID</th>
                <th scope="col"><i class="fa-solid fa-user me-1"></i>Nombre</th>
                <th scope="col"><i class="fa-solid fa-gear me-1"></i>Acciones</th>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="rptEspecialidades" runat="server">
                <ItemTemplate>
                    <tr>
                        <td><%# Eval("idEspecialidad") %></td>
                        <td><%# Eval("nombre") %></td>
                        <td class="text-center">
                            <asp:LinkButton ID="BtnEditar" runat="server" 
                                CommandName="Editar" CommandArgument='<%# Eval("idEspecialidad") %>'
                                OnClick="BtnEditar_Click"
                                CssClass="btn btn-outline-primary btn-sm me-1">
                                <i class="fa-solid fa-pen-to-square me-1"></i>Editar
                            </asp:LinkButton>

                            <asp:LinkButton ID="BtnEliminar" runat="server"
                                CommandName="Eliminar" CommandArgument='<%# Eval("idEspecialidad") %>'
                                OnClick="BtnEliminar_Click"
                                OnClientClick="return confirm('¿Está seguro de eliminar la especialidad?');"
                                CssClass="btn btn-outline-danger btn-sm">
                                <i class="fa-solid fa-trash me-1"></i>Eliminar
                            </asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <asp:Label ID="LblError" runat="server" CssClass="text-danger fw-semibold"></asp:Label>
        </tbody>
    </table>
    <div class="d-flex justify-content-center align-items-center gap-3 mt-3">
        <asp:Button ID="btnAnterior" runat="server" Text="<" OnClick="btnAnterior_Click"
            CssClass="btn btn-outline-light btn-sm" />

        <asp:Label ID="lblPagina" runat="server" CssClass="fw-bold px-3 fs-6 text-white"></asp:Label>

        <asp:Button ID="btnSiguiente" runat="server" Text=">" OnClick="btnSiguiente_Click"
            CssClass="btn btn-outline-light btn-sm" />
    </div>
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
