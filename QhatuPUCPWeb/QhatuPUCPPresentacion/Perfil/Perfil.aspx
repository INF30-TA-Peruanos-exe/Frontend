<%@ Page Title="Perfil" Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="Perfil.aspx.cs" Inherits="QhatuPUCPPresentacion.Perfil.Perfil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Public/css/estilos_perfil.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="container mt-5 mb-5">
        <!-- card para los datos del perfil -->
        <div class="row justify-content-center">
            <div class="col-md-8">
                <div class="card shadow" style="background-color: #f8f9fa;">
                    <div class="card-body p-5">
                        <h2 class="text-center mb-4">Mi Perfil</h2>
                        <hr />
                        <asp:Label ID="lblNombre" runat="server" CssClass="perfil-info d-block mb-2" Text="Nombre: -"></asp:Label>
                        <asp:Label ID="lblCodigoPUCP" runat="server" CssClass="perfil-info d-block mb-2" Text="Código PUCP: -"></asp:Label>
                        <asp:Label ID="lblCorreo" runat="server" CssClass="perfil-info d-block mb-2" Text="Correo: -"></asp:Label>
                        <asp:Label ID="lblNombreUsuario" runat="server" CssClass="perfil-info d-block mb-2" Text="Nombre de Usuario: -"></asp:Label>
                        <asp:Label ID="lblEstado" runat="server" CssClass="perfil-estado badge mt-2" Text="Estado: -"></asp:Label>
                        <asp:Button ID="btnEditarPerfil" runat="server" CssClass="btn btn-outline-primary position-absolute top-0 end-0 m-3" Text='Modificar perfil'
                        UseSubmitBehavior="false" OnClientClick="abrirModalEditar(); return false;" />

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
                        <div class="card mb-3">
                            <div class="card-body">
                                <a href=' <%# "../Inicio/DetallePublicacion.aspx?id=" + Eval("idPublicacion")%>' style="text-decoration:none; color: inherit">
                                    <h5 class="card-title"><%# Eval("titulo") %></h5>
                                    <p class="card-text"><%# Eval("descripcion") %></p>
                                    <small class="text-muted">Publicado el <%# Container.Page.GetType().GetMethod("FormatearFechaString").Invoke(Container.Page, new object[] { Eval("idPublicacion") }) %></small>
                                    <span class='<%# "badge me-2 " + cambiarSegunEstado(Eval("estado").ToString()) %>'>
                                    <%# Eval("estado") %>
                                </span>
                                </a>
                                <asp:LinkButton ID="btnEditar" runat="server" CssClass="btn btn-outline-warning btn-sm me-2 float-end"
                                    CommandName="Editar"
                                    CommandArgument='<%# Eval("idPublicacion") %>'><i class="fas fa-edit"></i> Editar
                                </asp:LinkButton>
                                <asp:LinkButton ID="btnEliminar" runat="server" CssClass="btn btn-outline-danger btn-sm float-end" CommandName="Eliminar" 
                                     CommandArgument='<%# Eval("idPublicacion") %>' OnClientClick="return confirm('¿Seguro que quieres eliminar esta publicación?');"><i class="fas fa-trash-alt"></i>
                                </asp:LinkButton>
                           </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>

    <!-- modal para editar el perfil del usuario -->
    <div class="modal fade" id="modalEditarPerfil" tabindex="-1" role="dialog" aria-labelledby="modalEditarPerfilLabel" aria-hidden="true">
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header">
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


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
    <script>
    function abrirModalEditar() {
        var modal = new bootstrap.Modal(document.getElementById('modalEditarPerfil'));
        modal.show();
    }
    </script>
</asp:Content>
