<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayoutMasterAdmin.Master" AutoEventWireup="true" CodeBehind="UsuariosAdmin.aspx.cs" Inherits="QhatuPUCPPresentacion.PaginasAdministrador.UsuariosAdmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="container mt-4">
        <!-- Título -->
        <div class="text-center mb-4">
            <h2 class="fw-bold text-white">
                <i class="fa-solid fa-users-gear me-2 text-info"></i>Gestión de Usuarios
            </h2>
        </div>

        <!-- Barra de búsqueda y botón de descarga -->
        <div class="row g-3 mb-4">
                <!-- Campo de búsqueda (ocupa más espacio en pantallas grandes, todo en móviles) -->
                <div class="col-lg-8 col-md-7 col-12">
                    <div class="input-group shadow-sm">
                        <span class="input-group-text bg-secondary border-0 text-white">
                            <i class="fa-solid fa-magnifying-glass"></i>
                        </span>
                        <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control bg-dark text-white border-0" placeholder="Buscar por nombre..." />
                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-outline-light" OnClick="btnBuscar_Click" />
                    </div>
                </div>

                <!-- Botón descargar (queda al lado en escritorio, abajo en móvil) -->
                <div class="col-lg-4 col-md-5 col-12">
                    <asp:Button ID="btnDescargarReporte" runat="server" Text="⬇ Top Usuarios"
                        CssClass="btn btn-success w-100 shadow-sm h-100" OnClick="btnDescargarReporte_Click" />
                </div>
            </div>
    </div>
        <!-- Tabla con datos -->
    <div class="container p-4 bg-light rounded shadow-sm">
        <div class="table-responsive">
            <table class="table table-hover table-bordered align-middle">
            <thead class="table-dark text-center align-middle">
                <tr>
                    <th scope="col"><i class="fa-solid fa-hashtag me-1"></i>ID</th>
                    <th scope="col"><i class="fa-solid fa-user me-1"></i>Nombre</th>
                    <th scope="col"><i class="fa-solid fa-user-tag me-1"></i>Usuario</th>
                    <th scope="col"><i class="fa-solid fa-envelope me-1"></i>Correo</th>
                    <th scope="col"><i class="fa-solid fa-circle-info me-1"></i>Estado</th>
                    <th scope="col"><i class="fa-solid fa-gear me-1"></i>Acciones</th>
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
                            <td class="text-center">
                                <%# (Eval("estado").ToString() == "Activo") 
                                      ? "<span class='badge bg-success'>Activo</span>" 
                                      : "<span class='badge bg-secondary'>Inactivo</span>" %>
                            </td>
                            <td class="text-center">
                                <asp:LinkButton ID="BtnEditar" runat="server"
                                    CommandName="Editar" CommandArgument='<%# Eval("idUsuario") %>'
                                    OnClick="BtnEditar_Click"
                                    CssClass="btn btn-outline-primary btn-sm me-1">
                                    <i class="fa-solid fa-user-check me-1"></i>Estado
                                </asp:LinkButton>
                                <asp:LinkButton ID="BtnEliminar" runat="server"
                                    CommandName="Eliminar" CommandArgument='<%# Eval("idUsuario") %>'
                                    OnClick="BtnEliminar_Click"
                                    OnClientClick="return confirm('¿Está seguro de eliminar al usuario?');"
                                    CssClass="btn btn-outline-danger btn-sm">
                                    <i class="fa-solid fa-trash"></i> Eliminar
                                </asp:LinkButton>
                                <asp:LinkButton ID="BtnAsignarAdmin" runat="server"
                                    CommandName="AsignarAdmin" CommandArgument='<%# Eval("idUsuario") %>'
                                    CssClass="btn btn-outline-warning btn-sm"
                                    OnClientClick='<%# "mostrarModalAdmin(" + Eval("idUsuario") + "); return false;" %>'>
                                    <i class="fa-solid fa-user-shield me-1"></i>Asignar Admin
                                </asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>
        <asp:Label ID="LblError" runat="server" CssClass="text-danger fw-semibold"></asp:Label>
    </div>
    <div class="d-flex flex-wrap justify-content-center align-items-center gap-2 mt-3">
        <asp:Button ID="btnAnterior" runat="server" Text="<" OnClick="btnAnterior_Click"
            CssClass="btn btn-outline-light btn-sm" />

        <asp:Label ID="lblPagina" runat="server" CssClass="fw-bold px-3 fs-6 text-white"></asp:Label>

        <asp:Button ID="btnSiguiente" runat="server" Text=">" OnClick="btnSiguiente_Click"
            CssClass="btn btn-outline-light btn-sm" />
    </div>

    <!-- Modal para asignar administrador -->
    <div class="modal fade" id="modalAsignarAdmin" tabindex="-1" aria-labelledby="modalAdminLabel" aria-hidden="true">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header bg-primary text-white">
            <h5 class="modal-title" id="modalAdminLabel">Asignar Administrador</h5>
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Cerrar"></button>
          </div>
          <div class="modal-body">
            <asp:HiddenField ID="hfIdUsuarioAdmin" runat="server" />
        
            <div class="mb-3">
              <label for="txtClaveMaestra" class="form-label fw-bold">
                Clave Maestra del Administrador:
              </label>
              <div class="input-group">
                <asp:TextBox ID="txtClaveMaestra" runat="server" CssClass="form-control" TextMode="Password" />
                <button type="button" class="btn btn-outline-secondary" onclick="toggleClaveMaestra()">
                  <i class="fa-solid fa-eye" id="iconoClave"></i>
                </button>
              </div>
              <small class="text-muted">Esta clave maestra será necesaria para realizar funciones administrativas.</small>
            </div>
          </div>

          <div class="modal-footer">
            <asp:Button ID="btnAsignarAdminModal" runat="server" Text="Asignar"
              CssClass="btn btn-success" OnClick="btnAsignarAdminModal_Click" />
            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
          </div>
        </div>
      </div>
    </div>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Scripts" runat="server">
    <script>
        function mostrarModalAdmin(idUsuario) {
            document.getElementById('<%= hfIdUsuarioAdmin.ClientID %>').value = idUsuario;
          document.getElementById('<%= txtClaveMaestra.ClientID %>').value = '';

          var modal = new bootstrap.Modal(document.getElementById('modalAsignarAdmin'));
          modal.show();
      }

      function toggleClaveMaestra() {
          const txt = document.getElementById('<%= txtClaveMaestra.ClientID %>');
            const icono = document.getElementById('iconoClave');

            if (txt.type === "password") {
                txt.type = "text";
                icono.classList.remove("fa-eye");
                icono.classList.add("fa-eye-slash");
            } else {
                txt.type = "password";
                icono.classList.remove("fa-eye-slash");
                icono.classList.add("fa-eye");
            }
        }
    </script>
</asp:Content>

