<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayoutMasterAdmin.Master" AutoEventWireup="true" CodeBehind="UsuariosAdmin.aspx.cs" Inherits="QhatuPUCPPresentacion.PaginasAdministrador.UsuariosAdmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="container mt-4">
        <!-- Título -->
        <div class="text-center mb-4">
            <h2 class="fw-bold">Usuarios</h2>
        </div>

        <!-- Barra de búsqueda y botón de descarga -->
        <div class="row justify-content-center mb-4">
            <!-- Búsqueda -->
            <div class="col-md-6 col-sm-12 mb-2">
                <div class="input-group">
                    <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control" placeholder="Buscar por nombre..." AutoPostBack="true" OnTextChanged="txtBuscar_TextChanged" />
                    <button type="submit" runat="server" id="btnBuscar" class="btn btn-celeste">Buscar</button>
                </div>
            </div>

            <!-- Botón descargar -->
            <div class="col-md-3 col-sm-12">
                <asp:Button ID="btnDescargarReporte" runat="server" Text="Top Usuarios" CssClass="btn btn-success w-100 h-100" OnClick="btnDescargarReporte_Click" />
            </div>
        </div>
    </div>
    <!-- Tabla con datos -->
    <div class="container p-4">
        <table class="table table-hover table-bordered">
            <thead class="table-dark">
                <tr>
                    <th>Id</th>
                    <th>Nombre</th>
                    <th>Nombre de Usuario</th>
                    <th>Correo</th>
                    <th>Estado</th>
                    <th>Acciones</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="rptUsuarios" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("idUsuario") %></td>
                            <td><%# Eval("nombre") %></td>
                            <td><%# Eval("nombreUsuario") %></td>
                            <td><%# Eval("correo") %></td>
                            <td><%# Eval("estado") %></td>
                            <td>
                                <ItemTemplate>
                                    <asp:LinkButton ID="BtnEditar" runat="server"
                                        CommandName="Editar" CommandArgument='<%# Eval("idUsuario") %>'
                                        OnClick="BtnEditar_Click"
                                        CssClass="btn btn-sm btn-success">
                                        <i class="fa-solid fa-user-check me-1"></i> Cambiar Estado
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="BtnEliminar" runat="server"
                                        CommandName="Eliminar" CommandArgument='<%# Eval("idUsuario") %>'
                                        OnClick="BtnEliminar_Click"
                                        OnClientClick="return confirm('¿Está seguro de eliminar la publicacion?');"
                                        CssClass="btn btn-sm btn-danger">
                                        <i class="fa-solid fa-trash"></i> Eliminar
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Label ID="LblError" runat="server" Text="" CssClass="text-danger">
                </asp:Label>
            </tbody>
        </table>
    </div>

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>

