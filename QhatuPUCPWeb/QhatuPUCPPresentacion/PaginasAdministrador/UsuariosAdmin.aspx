<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayoutMasterAdmin.Master" AutoEventWireup="true" CodeBehind="UsuariosAdmin.aspx.cs" Inherits="QhatuPUCPPresentacion.PaginasAdministrador.UsuariosAdmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="container mb-4">
        <h2 class="fw-bold">Usuarios</h2>
    </div>
    <!-- Tabla con datos -->
    <div class="container p-4">
        <table class="table table-hover table-bordered">
            <thead class="table-dark">
                <tr>
                    <th>Id</th>
                    <th>Nombre</th>
                    <th>Nombre de Usuario</th>
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
                            <td><%# Eval("estado") %></td>
                            <td style="position: relative;">
                                <div style="display: inline-block; position: relative;">
                                    <!-- Botón Eliminar -->
                                    <button type="button" class="btn btn-sm btn-danger ms-2" onclick="mostrarConfirmacion(this)">
                                        Eliminar
                                    </button>
                                    <!-- Globo de confirmación -->
                                    <div class="confirmacion-eliminar alert alert-warning d-none p-2 position-absolute" 
                                         style="top: 100%; left: 0; z-index: 1000; white-space: nowrap;">
                                        ¿Eliminar?
                                        <div class="mt-1 text-end">
                                            <asp:Button ID="btnEliminar" runat="server"
                                                CommandName="Eliminar"
                                                CommandArgument='<%# Eval("idUsuario") %>'
                                                Text="Sí"
                                                CssClass="btn btn-sm btn-danger me-1" />
                                            <button type="button" class="btn btn-sm btn-secondary" onclick="cancelarConfirmacion(this)">
                                                No
                                            </button>
                                        </div>
                                    </div>
                                </div>
                                <div style="display: inline-block; position: relative;">
                                    <!-- Botón Editar -->
                                    <button type="button" class="btn btn-sm btn-warning ms-2" onclick="mostrarConfirmacion(this, 'editar')">
                                        Cambiar Estado
                                    </button>

                                    <!-- Globo confirmación de edición -->
                                    <div class="confirmacion-editar alert alert-info d-none p-2 position-absolute"
                                         style="top: 100%; left: 0; z-index: 1000; white-space: nowrap;">
                                        ¿Cambiar estado?
                                        <div class="mt-1 text-end">
                                            <asp:Button ID="btnEditar" runat="server"
                                                CommandName="CambiarEstado"
                                                CommandArgument='<%# Eval("idUsuario") %>'
                                                Text="Sí"
                                                CssClass="btn btn-sm btn-primary me-1" />
                                            <button type="button" class="btn btn-sm btn-secondary" onclick="cancelarConfirmacion(this)">
                                                No
                                            </button>
                                        </div>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Scripts" runat="server">
    <script>
        function mostrarConfirmacion(button, tipo) {
            // Oculta TODAS las confirmaciones (eliminar y editar)
            document.querySelectorAll('.confirmacion-eliminar, .confirmacion-editar')
                .forEach(el => el.classList.add('d-none'));

            // Muestra la correspondiente
            let confirmBox;
            if (tipo === 'eliminar') {
                confirmBox = button.nextElementSibling;
            } else if (tipo === 'editar') {
                confirmBox = button.nextElementSibling;
            }
            confirmBox.classList.remove('d-none');
        }

        function cancelarConfirmacion(button) {
            const confirmBox = button.closest('.alert');
            confirmBox.classList.add('d-none');
        }

        // Cierra el globo al hacer clic fuera
        document.addEventListener('click', function (event) {
            const isInside = event.target.closest('.confirmacion-eliminar') ||
                event.target.closest('.confirmacion-editar') ||
                event.target.closest('button[onclick^="mostrarConfirmacion"]');
            if (!isInside) {
                document.querySelectorAll('.confirmacion-eliminar, .confirmacion-editar')
                    .forEach(el => el.classList.add('d-none'));
            }
        });
    </script>
</asp:Content>

