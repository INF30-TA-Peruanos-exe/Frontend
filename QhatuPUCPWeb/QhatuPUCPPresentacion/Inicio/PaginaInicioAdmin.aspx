<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayoutMasterAdmin.Master" AutoEventWireup="true" CodeBehind="PaginaInicioAdmin.aspx.cs" Inherits="QhatuPUCPPresentacion.Inicio.PaginaInicioAdmin" %>
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
        <!-- Título -->
        <div class="text-center mb-4">
            <h2 class="fw-bold text-white">
                <i class="fa-solid fa-pen-to-square"></i>Gestión de Publicaciones
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

            <!-- Botón descargar -->
            <div class="col-md-3 col-sm-12">
                <asp:Button ID="btnDescargarReporte" runat="server" Text="⬇ Top Publicaciones" CssClass="btn btn-success w-100 shadow-sm" OnClick="btnDescargarReporte_Click" />
            </div>
        </div>
    </div>
    <!-- Tabla con datos -->
   <div class="container p-4 bg-light rounded shadow-sm">
       <table class="table table-hover table-bordered align-middle">
            <thead class="table-dark text-center align-middle">
                <tr>
                    <th scope="col"><i class="fa-solid fa-hashtag me-1"></i>ID</th>
                    <th scope="col"><i class="fa-solid fa-heading me-1"></i>Título</th>
                    <th scope="col"><i class="fa-solid fa-bullhorn me-1"></i>Estado</th>
                    <th scope="col"><i class="fa-solid fa-calendar-day me-1"></i>Fecha</th>
                    <th scope="col"><i class="fa-solid fa-gear me-1"></i>Acciones</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="rptPublicaciones" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("idPublicacion")%></td>
                            <td><%# Eval("titulo") %></td>
                            <td><%# Eval("estado") %></td>
                            <td><%# Eval("fechaPublicacion", "{0:dd/MM/yyyy}") %></td>
                            <td class="text-center">
                                <!-- Botón Ver Detalle -->
                                <a class="btn btn-outline-primary btn-sm me-1 shadow-sm"
                                   href='<%# ResolveUrl("~/PaginasAdministrador/DetallePublicacionAdmin.aspx?id=" + Eval("idPublicacion")) %>'
                                   title="Ver Detalle de la Publicación">
                                    <i class="fa-solid fa-eye me-1"></i>Ver
                                </a>

                                <!-- Botón Eliminar -->
                                <asp:LinkButton ID="BtnEliminar" runat="server"
                                    CommandName="Eliminar" CommandArgument='<%# Eval("idPublicacion") %>'
                                    OnClick="BtnEliminar_Click"
                                    OnClientClick="return confirm('¿Está seguro de eliminar la publicación?');"
                                    CssClass="btn btn-outline-danger btn-sm shadow-sm"
                                    ToolTip="Eliminar publicación">
                                    <i class="fa-solid fa-trash-can me-1"></i>Eliminar
                                </asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>
    <div class="d-flex justify-content-center align-items-center gap-3 mt-3">
        <asp:Button ID="btnAnterior" runat="server" Text="<" OnClick="btnAnterior_Click"
            CssClass="btn btn-outline-light btn-sm" />

        <asp:Label ID="lblPagina" runat="server" CssClass="fw-bold px-3 fs-6 text-white"></asp:Label>

        <asp:Button ID="btnSiguiente" runat="server" Text=">" OnClick="btnSiguiente_Click"
            CssClass="btn btn-outline-light btn-sm" />
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
