<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayoutMasterAdmin.Master" AutoEventWireup="true" CodeBehind="PaginaInicioAdmin.aspx.cs" Inherits="QhatuPUCPPresentacion.Inicio.PaginaInicioAdmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <!-- Título de la página -->
    <div class="container mt-4">
        <!-- Título -->
        <div class="text-center mb-4">
            <h2 class="fw-bold">Publicaciones</h2>
        </div>

        <!-- Barra de búsqueda y botón de descarga -->
        <div class="row justify-content-center mb-4">
            <!-- Búsqueda -->
            <div class="col-md-6 col-sm-12 mb-2">
            <div class="input-group">
                <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control" placeholder="Buscar por título..." AutoPostBack="true" OnTextChanged="txtBuscar_TextChanged" />
                <button type="submit" runat="server" id="btnBuscar" class="btn btn-celeste">Buscar</button>
            </div>
        </div>

            <!-- Botón descargar -->
            <div class="col-md-3 col-sm-12">
                <asp:Button ID="btnDescargarReporte" runat="server" Text="Top Publicaciones" CssClass="btn btn-success w-100 h-100" OnClick="btnDescargarReporte_Click" />
            </div>
        </div>
    </div>
    <!-- Tabla con datos -->
    <div class="container p-4">
        <table class="table table-hover table-bordered">
            <thead class="table-dark">
                <tr>
                    <th>ID</th>
                    <th>Título</th>
                    <th>Estado Publicación</th>
                    <th>Fecha</th>
                    <th>Acciones</th>
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
                            <td style="position: relative;">
                                <a class="btn btn-sm btn-primary"
                                   href='<%# ResolveUrl("~/PaginasAdministrador/DetallePublicacionAdmin.aspx?id=" + Eval("idPublicacion")) %>'>
                                    Ver Detalle
                                </a>

                                <asp:LinkButton ID="BtnEliminar" runat="server"
                                    CommandName="Eliminar" CommandArgument='<%# Eval("idPublicacion") %>'
                                    OnClick="BtnEliminar_Click"
                                    OnClientClick="return confirm('¿Está seguro de eliminar la publicacion?');"
                                    CssClass="btn btn-sm btn-danger">
                                    <i class="fa-solid fa-trash"></i> Eliminar
                                </asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
