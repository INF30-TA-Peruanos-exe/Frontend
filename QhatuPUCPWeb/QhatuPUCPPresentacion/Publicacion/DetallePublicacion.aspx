<%@ Page Title="Detalle de Publicación" Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="DetallePublicacion.aspx.cs" Inherits="QhatuPUCPPresentacion.Publicacion.DetallePublicacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .comentario-avatar {
            width: 40px;
            height: 40px;
            border-radius: 50%;
            object-fit: cover;
        }

        .comentario-item {
            border-bottom: 1px solid #eee;
            padding-bottom: 1rem;
        }

        .comentario-content {
            flex-grow: 1;
        }

        .dropdown-menu {
            min-width: 8rem;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <asp:HiddenField ID="hfIdPublicacion" runat="server" />
    <asp:HiddenField ID="hfComentarioIdDenuncia" runat="server" />
    <asp:HiddenField ID="hfTipoDenuncia" runat="server" />

    <div class="d-flex flex-column flex-lg-row gap-4 mt-offset">
        <!-- Publicación -->
        <div class="w-100 w-lg-25">
            <div class="publicacion-card">
                <asp:Button ID="btnMostrarModalDenuncia" runat="server"
                    CssClass="btn-denunciar"
                    Text="Denunciar"
                    OnClientClick="prepararDenunciaPublicacion(); return false;" />
                <div class="d-flex align-items-center mb-3">
                    <asp:Image ID="imgAvatar" runat="server" CssClass="autor-avatar me-3" />
                    <div>
                        <strong><asp:Label ID="lblAutor" runat="server" /></strong><br />
                        <small class="text-muted"><asp:Label ID="lblTiempo" runat="server" /></small>
                    </div>
                </div>
                <h6 class="fw-bold"><asp:Label ID="lblTitulo" runat="server" /></h6>
                <p><asp:Label ID="lblDescripcion" runat="server" /></p>
                <asp:Image ID="imgPublicacion" runat="server" CssClass="img-fluid rounded mt-2" />
            </div>
        </div>

        <!-- Comentarios -->
        <div class="w-100 w-lg-50">
            <div class="comentario-panel">
                <h6 class="mb-4"><strong>Comentarios</strong></h6>

                <!-- Agregar Comentario -->
                <div class="d-flex align-items-start mb-4">
                    <img src="/Public/images/user-avatar.png" class="comentario-avatar me-3 mt-1" />
                    <div class="flex-grow-1">
                        <asp:TextBox ID="txtComentario" runat="server" CssClass="form-control border-0 shadow-sm mb-3" TextMode="MultiLine" Rows="2" placeholder="Escribe un comentario..." />
                        <label class="form-label mb-1"><strong>Valoración:</strong></label>
                        <div class="mb-3">
                            <span class="estrella" data-valor="1">☆</span>
                            <span class="estrella" data-valor="2">☆</span>
                            <span class="estrella" data-valor="3">☆</span>
                            <span class="estrella" data-valor="4">☆</span>
                            <span class="estrella" data-valor="5">☆</span>
                            <asp:HiddenField ID="hfValoracion" runat="server" />
                        </div>
                        <asp:Button ID="btnComentar" runat="server" CssClass="btn btn-primary btn-sm" Text="Comentar" OnClick="btnComentar_Click" />
                    </div>
                </div>

                <!-- Lista de Comentarios -->
                <asp:Repeater ID="rptComentarios" runat="server">
                    <ItemTemplate>
                        <div class="comentario-item mb-3 d-flex">
                            <img src='<%# Eval("AvatarUrl") %>' class="comentario-avatar me-3 mt-1" />
                            <div class="comentario-content w-100">
                                <div class="d-flex justify-content-between">
                                    <div>
                                        <strong><%# Eval("Autor") %></strong>
                                        <div class="comentario-meta"><%# Eval("Fecha") %></div>
                                    </div>

                                    <asp:PlaceHolder ID="phMenu" runat="server">
                                        <div class="dropdown">
                                            <button class="btn btn-sm" type="button" data-bs-toggle="dropdown">
                                                <i class="fa fa-ellipsis-h"></i>
                                            </button>
                                            <div class="dropdown-menu dropdown-menu-end">
                                                <asp:LinkButton
                                                    ID="lnkAccion"
                                                    runat="server"
                                                    CssClass="dropdown-item" />
                                            </div>
                                        </div>
                                    </asp:PlaceHolder>
                                </div>

                                <div class="mt-1"><%# Eval("Contenido") %></div>
                                <div class="mt-1 small">
                                    Valoración:
                    <%# new String('★', Convert.ToInt32(Eval("Valoracion"))) + 
                         new String('☆', 5 - Convert.ToInt32(Eval("Valoracion"))) %>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>

    <div class="modal fade" id="modalDenuncia" tabindex="-1" role="dialog" aria-labelledby="modalDenunciaLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Denunciar publicación</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Cerrar"></button>
                </div>
                <div class="modal-body">
                    <asp:TextBox ID="txtMotivoDenuncia" runat="server" TextMode="MultiLine" CssClass="form-control" Rows="4" placeholder="Describe el motivo de la denuncia" />
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnEnviarDenuncia" runat="server" CssClass="btn btn-danger" Text="Enviar denuncia" OnClick="btnEnviarDenuncia_Click" />
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
    <script>
        function mostrarModalDenuncia() {
            var modal = new bootstrap.Modal(document.getElementById('modalDenuncia'));
            modal.show();
        }

        document.addEventListener("DOMContentLoaded", function () {
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
        });

        function prepararDenunciaPublicacion() {
            document.getElementById('<%= hfTipoDenuncia.ClientID %>').value = 'publicacion';
            document.getElementById('<%= hfComentarioIdDenuncia.ClientID %>').value = '';
            mostrarModalDenuncia();
        }

    </script>
</asp:Content>



