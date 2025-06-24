<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayoutMasterAdmin.Master" AutoEventWireup="true" CodeBehind="UsuariosAdmin.aspx.cs" Inherits="QhatuPUCPPresentacion.PaginasAdministrador.UsuariosAdmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="container mb-4">
        <h2 class="fw-bold">Usuarios</h2>
    </div>
    <!-- Barra de búsqueda -->
    <div class="container mb-3">
        <div class="input-group">
            <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control" placeholder="Buscar por nombre..."  AutoPostBack="true" OnTextChanged="txtBuscar_TextChanged" />
            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-outline-secondary" OnClick="btnBuscar_Click" />
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
                                        Text="Cambiar Estado" />
                                    <asp:LinkButton ID="BtnEliminar" runat="server"
                                        CommandName="Eliminar" CommandArgument='<%# Eval("idUsuario") %>'
                                        OnClick="BtnEliminar_Click"
                                        OnClientClick="return confirm('¿Está seguro de eliminar el usuario?');"
                                        Text="<i class='fa-solid fa-trash'></i>" />
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

