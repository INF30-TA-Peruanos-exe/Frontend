<%@ Page Title="Perfil" Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="Perfil.aspx.cs" Inherits="QhatuPUCPPresentacion.Perfil.Perfil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href="<%= ResolveUrl("~/Perfil/estilos_perfil.css") %>" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="container mt-2 mb-2">
        <!-- card para los datos del perfil -->
        <div class="row justify-content-center">
            <div class="col-md-8">
                <div class="card shadow" style="background-color: #f8f9fa;">
                    <div class="card-body paddpers">

                        <div class="d-flex justify-content-between align-items-center mb-3 flex-wrap">
                            <h3 class="mb-0">Mi Perfil</h3>
                            <div class="mt-2 mt-md-0">
                                <asp:Button ID="btnEditarPerfil" runat="server" CssClass="btn btn-outline-primary me-2 mb-2 mb-md-0" Text='Modificar perfil'
                                    UseSubmitBehavior="false" OnClientClick="abrirModalEditar(); return false;" />
                                <asp:Button ID="btnCambiarContrasena" runat="server" CssClass="btn btn-outline-primary mb-2 mb-md-0" Text="Cambiar contraseña"
                                    UseSubmitBehavior="false" OnClientClick="abrirModalCambiarContrasena(); return false;" />
                            </div>
                        </div>

                        <hr />

                        <!-- Datos del perfil -->
                        <asp:Label ID="lblNombre" runat="server" CssClass="d-block mb-2" Text="Nombre: -"></asp:Label>
                        <asp:Label ID="lblCodigoPUCP" runat="server" CssClass="d-block mb-2" Text="Código PUCP: -"></asp:Label>
                        <asp:Label ID="lblCorreo" runat="server" CssClass="d-block mb-2" Text="Correo: -"></asp:Label>
                        <asp:Label ID="lblNombreUsuario" runat="server" CssClass="d-block mb-2" Text="Nombre de Usuario: -"></asp:Label>
                        <asp:Label ID="lblEstado" runat="server" CssClass="perfil-estado badge mt-2" Text="Estado: -"></asp:Label>

                    </div>

                </div>
            </div>
        </div>

        <!-- repetidor para mostrar las publicaciones del usuario -->
        <div class="row justify-content-center mt-5">
            <div class="col-md-10">
                <h3 class="mb-4">Mis Publicaciones</h3>
                <asp:Repeater ID="rptPublicaciones" runat="server" OnItemCommand="rptPublicacion_ItemCommand">
                    <ItemTemplate>
                        <div class="card mb-3 border-0">
                            <div class="row g-0 h-100">
                                <div class="col-md-4 p-0-m-0">
                                <img src='<%# ResolveUrl("~" + Eval("rutaImagen").ToString().Replace("~", "")) %>' class="card-img-top img-fluid w-100 rounded-start" style="height: 180px; object-fit: cover;" />
                                </div>

                                <div class="col-md-8">
                                    <div class="card-body">
                                        <a href=' <%# "../Publicacion/DetallePublicacion.aspx?id=" + Eval("idPublicacion")%>' style="text-decoration: none; color: inherit">
                                            <h5 class="card-title mb-1 card-title-linea"><%# Eval("titulo") %></h5>
                                            <p class="card-text mb-1 card-text-linea"><%# Eval("descripcion") %></p>
                                            <small class="text-muted d-block">Publicado el <%# Container.Page.GetType().GetMethod("FormatearFechaString").Invoke(Container.Page, new object[] { Eval("idPublicacion") }) %></small>
                                            <span class='<%# "badge me-2 " + cambiarSegunEstado(Eval("estado").ToString()) %>'>
                                                <%# Eval("estado") %>
                                            </span>
                                        </a>

                                        <!-- Botones de editar y eliminar -->
                                        <div class="mt-2 text-end">
                                            <asp:LinkButton ID="btnEditar" runat="server" CssClass="btn btn-outline-primary btn-sm me-2"
                                                CommandName="Editar"
                                                CommandArgument='<%# Eval("idPublicacion") %>'>
                                                <i class="fas fa-edit"></i> Editar
                                            </asp:LinkButton>

                                            <asp:LinkButton ID="btnEliminar" runat="server" CssClass="btn btn-outline-danger btn-sm"
                                                CommandName="Eliminar"
                                                CommandArgument='<%# Eval("idPublicacion") %>'
                                                OnClientClick="return confirm('¿Seguro que quieres eliminar esta publicación?');">
                                                <i class="fas fa-trash-alt"></i> Eliminar
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>

    <!-- modal para editar perfil -->
    <div class="modal fade" id="modalEditarPerfil" tabindex="-1" role="dialog" aria-labelledby="modalEditarPerfilLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header text-dark">
                    <h5 class="modal-title">Editar Perfil</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Cerrar"></button>
                </div>
                <div class="modal-body">
                    <div class="mb-3">
                        <label for="txtNombre" class="form-label">Nombre</label>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
                    </div>
                    <div class="mb-3">
                        <label for="txtCorreo" class="form-label">Correo</label>
                        <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control" />
                    </div>
                    <div class="mb-3">
                        <label for="txtNombreUsuario" class="form-label">Nombre de Usuario</label>
                        <asp:TextBox ID="txtNombreUsuario" runat="server" CssClass="form-control" />
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnGuardarCambios" runat="server" CssClass="btn btn-success" Text="Guardar cambios" OnClick="btnGuardarCambios_Click" />
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                </div>
            </div>
        </div>
    </div>
    <!-- modal para cambiar contraseña -->
    <div class="modal fade" id="modalCambiarContrasena" tabindex="-1" role="dialog" aria-labelledby="modalCambiarContrasenaLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header" style="border-bottom: 1px solid #dee2e6;">
                    <h5 class="modal-title" style="color: #000;">Cambiar Contraseña</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Cerrar"></button>
                </div>
                <div class="modal-body">
                    <div class="mb-3">
                        <label for="txtNuevaContrasena" class="form-label" style="color: #000;">Nueva Contraseña</label>
                        <asp:TextBox ID="txtNuevaContrasena" runat="server" CssClass="form-control" TextMode="Password" Style="background-color: #fff; color: #000; border: 1px solid #ced4da;" />
                    </div>
                    <div class="mb-3">
                        <label for="txtConfirmarContrasena" class="form-label" style="color: #000;">Confirmar Nueva Contraseña</label>
                        <asp:TextBox ID="txtConfirmarContrasena" runat="server" CssClass="form-control" TextMode="Password" Style="background-color: #fff; color: #000; border: 1px solid #ced4da;" />
                    </div>
                    <asp:Label ID="lblMensajeCambioContrasena" runat="server" Style="color: #dc3545;"></asp:Label>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnGuardarContrasena" runat="server" CssClass="btn btn-success" Text="Guardar cambios" OnClick="btnGuardarContrasena_Click" Style="color: #fff;" />
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal" style="color: #fff;">Cancelar</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
    <script>
        function abrirModalEditar() {
            var modal = new bootstrap.Modal(document.getElementById('modalEditarPerfil'));
            modal.show();
        }
    </script>
    <script>
        function abrirModalCambiarContrasena() {
            var modal = new bootstrap.Modal(document.getElementById('modalCambiarContrasena'));
            modal.show();
        }
    </script>
</asp:Content>
