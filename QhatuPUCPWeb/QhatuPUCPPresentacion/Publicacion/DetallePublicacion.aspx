<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="DetallePublicacion.aspx.cs" Inherits="QhatuPUCPPresentacion.Publicacion.DetallePublicacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link runat="server" href="~/Publicacion/detallePublicacion.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <asp:HiddenField ID="hfValoracion" runat="server" />
    <asp:HiddenField ID="hfIdPublicacion" runat="server" />

    <!-- Contenedor de publicación y comentarios -->
    <div class="d-flex flex-column flex-lg-row gap-4">

        <!-- Columna izquierda: publicación -->
        <div class="w-100 w-lg-25">
            <div class="publicacion-card position-relative bg-white rounded-4 p-4 shadow-sm text-dark">
                <!-- Botón denunciar -->
                <asp:Panel ID="pnlDenunciar" runat="server" CssClass="position-absolute top-0 end-0 m-3" Visible="false">
                    <button type="button"
                        class="btn btn-outline-danger btn-sm px-3 py-1"
                        data-bs-toggle="modal"
                        data-bs-target="#modalDenuncia">
                        Denunciar
                    </button>
                </asp:Panel>
                <div class="d-flex align-items-center mb-3">
                    <asp:Image ID="imgAvatar" runat="server" CssClass="autor-avatar me-3" />
                    <div>
                        <strong>
                            <asp:Label ID="lblAutor" runat="server" /></strong><br />
                        <small class="text-muted">
                            <asp:Label ID="lblTiempo" runat="server" /></small>
                    </div>
                </div>

                <h6 class="fw-bold">
                    <asp:Label ID="lblTitulo" runat="server" /></h6>
                <p>
                    <asp:Label ID="lblDescripcion" runat="server" /></p>
                <asp:Image ID="imgPublicacion" runat="server" CssClass="img-fluid rounded mt-2" />
            </div>
        </div>

        <!-- Columna derecha: Comentarios -->
        <div class="w-100 w-lg-50">
            <div class="comentario-panel bg-white text-dark rounded-4 p-4 shadow-sm w-100 mt-4 mt-lg-0">

                <h6 class="mb-4"><strong>Comentarios</strong></h6>

                <!-- Caja de comentario -->
                <div class="d-flex align-items-start mb-4">
                    <img src='<%= ResolveUrl("~/Public/images/user-avatar.png") %>' class="comentario-avatar me-3 mt-1" />
                    <div class="flex-grow-1">
                        <asp:TextBox ID="txtComentario" runat="server"
                            CssClass="form-control border-0 shadow-sm mb-3"
                            placeholder="Escribe un comentario..." TextMode="MultiLine" Rows="2" />

                        <label class="form-label mb-1"><strong>Valoración:</strong></label>
                        <div class="mb-3">
                            <span class="estrella" data-valor="1">&#9734;</span>
                            <span class="estrella" data-valor="2">&#9734;</span>
                            <span class="estrella" data-valor="3">&#9734;</span>
                            <span class="estrella" data-valor="4">&#9734;</span>
                            <span class="estrella" data-valor="5">&#9734;</span>
                        </div>

                        <asp:Button ID="btnComentar" runat="server" Text="Comentar"
                            CssClass="btn btn-outline-primary btn-sm px-3 py-1"
                            OnClick="btnComentar_Click" />
                    </div>
                </div>

                <!-- Lista de comentarios -->
                <asp:Repeater ID="rptComentarios" runat="server" OnItemCommand="rptComentarios_ItemCommand">
                    <ItemTemplate>
                        <div class="d-flex align-items-start mb-3 comentario-item position-relative">
                            <img src='<%# Eval("AvatarUrl") %>' class="comentario-avatar me-3 mt-1" />
                            <div class="comentario-content position-relative w-100">
                                <strong><%# Eval("Autor") %></strong>
                                <div class="comentario-meta"><%# FormatearFecha(Eval("Fecha")) %></div>
                                <div class="mt-1"><%# Eval("Contenido") %></div>
                                <asp:Panel runat="server" Visible='<%# Eval("TieneValoracion") %>' CssClass="mt-1 small">
                                    Valoración:<%# new string('★', Convert.ToInt32(Eval("Valoracion"))) + new string('☆', 5 - Convert.ToInt32(Eval("Valoracion"))) %>
                                </asp:Panel>

                                <%-- Botón de tres puntos para opciones que es visible solo si el comentario es del usuario --%>
                                <asp:Panel runat="server" CssClass="dropdown comment-options position-absolute top-0 end-0"
                                    Visible='<%# Convert.ToBoolean(Eval("EsPropio")) %>'>
                                    <button class="btn btn-sm bg-transparent border-0 px-2" type="button"
                                        data-bs-toggle="dropdown" aria-expanded="false" title="Opciones">
                                        <i class="fas fa-ellipsis-h"></i>
                                    </button>
                                    <ul class="dropdown-menu">
                                        <li>
                                            <asp:LinkButton ID="btnEliminarComentario" runat="server" CssClass="dropdown-item"
                                                CommandName="Eliminar" CommandArgument='<%# Eval("IdComentario") %>'>Eliminar
                                            </asp:LinkButton>
                                        </li>
                                    </ul>
                                </asp:Panel>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
    <!--Modal de denuncia-->
    <div class="modal fade" id="modalDenuncia" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered custom-modal-ancho">
            <div class="modal-content rounded-4">
                <div class="modal-body p-4">
                    <h6 class="fw-bold text-dark mb-2">Motivo</h6>

                    <asp:TextBox ID="txtMotivoDenuncia" runat="server"
                        CssClass="form-control rounded-3 border-0 shadow-sm mb-3"
                        placeholder="Escribe el motivo de la denuncia" TextMode="MultiLine" Rows="3" />

                    <div class="d-flex justify-content-end">
                        <asp:Button ID="btnConfirmarDenuncia" runat="server" Text="Denunciar"
                            CssClass="btn btn-outline-danger btn-sm rounded-pill px-3 py-1"
                            OnClick="btnConfirmarDenuncia_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Toast de éxito -->
    <div class="position-fixed bottom-0 end-0 p-3" style="z-index: 1100">
        <div id="toastDenuncia" class="toast toast-exito" role="alert" aria-live="assertive" aria-atomic="true">
            <div class="d-flex">
                <div class="toast-body">
                    <i class="fas fa-check-circle me-2"></i>
                    <span>Denuncia registrada correctamente.</span>
                </div>
                <button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast" aria-label="Cerrar"></button>
            </div>
        </div>
    </div>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
    <script>
        const estrellas = document.querySelectorAll(".estrella");
        const inputHidden = document.getElementById("<%= hfValoracion.ClientID %>");

        estrellas.forEach((estrella, idx) => {
            estrella.addEventListener("click", () => {
                estrellas.forEach((e, i) => {
                    e.classList.toggle("seleccionada", i <= idx);
                });
                inputHidden.value = idx + 1;
            });
        });
    </script>
</asp:Content>
