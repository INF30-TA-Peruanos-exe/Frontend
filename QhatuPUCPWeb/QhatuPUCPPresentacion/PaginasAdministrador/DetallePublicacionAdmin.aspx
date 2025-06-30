<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayoutMasterAdmin.Master" AutoEventWireup="true" CodeBehind="DetallePublicacionAdmin.aspx.cs" Inherits="QhatuPUCPPresentacion.PaginasAdministrador.DetallePublicacionAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/PaginasAdministrador/DetallePubliAdmin.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <asp:HiddenField ID="hfValoracion" runat="server" />
    <asp:HiddenField ID="hfIdPublicacion" runat="server" />

    <div class="container mt-4">

        <!-- Título de página -->
        <div class="text-center mb-4">
            <h2 class="fw-bold text-white">
                <i class="fa-solid fa-newspaper text-info me-2"></i>Detalle de la Publicación
            </h2>
        </div>

        <!-- Tarjeta de publicación -->
        <div class="card shadow-sm mb-4 border-0">
            <div class="card-header bg-dark text-white fw-semibold">
                Detalles de la Publicación
            </div>
            <div class="card-body">
                <div class="row align-items-center">
                    <div class="col-md-8">
                        <h5 class="fw-bold"><asp:Label ID="lblTitulo" runat="server" /></h5>
                        <p><asp:Label ID="lblDescripcion" runat="server" /></p>
                        <p class="text-muted mb-1">
                            <i class="fa-solid fa-user me-1 text-info"></i>Autor: <strong><asp:Label ID="lblAutor" runat="server" /></strong>
                        </p>
                        <p class="text-muted">
                            <i class="fa-solid fa-clock me-1 text-info"></i>Publicado: <asp:Label ID="lblTiempo" runat="server" />
                        </p>
                    </div>
                    <div class="col-md-4 text-center">
                        <asp:Image ID="imgPublicacion" runat="server" CssClass="img-fluid img-thumbnail" />
                    </div>
                </div>
            </div>
        </div>

        <!-- Tabla de comentarios -->
        <div class="card shadow-sm border-0">
            <div class="card-header bg-secondary text-white fw-semibold">
                Comentarios
            </div>
            <div class="card-body p-0">
                <asp:Repeater ID="rptComentarios" runat="server" OnItemCommand="rptComentarios_ItemCommand">
                    <HeaderTemplate>
                        <div class="table-responsive">
                            <table class="table table-hover table-bordered align-middle">
                                <thead class="table-dark text-center align-middle">
                                    <tr>
                                        <th>#</th>
                                        <th scope="col"><i class="fa-solid fa-user me-1"></i>Autor</th>
                                        <th><i class="fa-solid fa-calendar-days me-1 text-info"></i>Fecha de Comentario</th>
                                        <th><i class="fa-solid fa-comment-dots me-1 text-info"></i>Contenido</th>
                                        <th><i class="fa-solid fa-star me-1 text-warning"></i>Valoración (★)</th>
                                        <th><i class="fa-solid fa-wrench me-1 text-info"></i>Acciones</th>
                                    </tr>
                                </thead>
                                <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td class="text-center"><%# Container.ItemIndex + 1 %></td>
                            <td><%# Eval("Autor") %></td>
                            <td><%# Eval("Fecha") %></td>
                            <td><%# Eval("Contenido") %></td>
                            <td class="text-center">
                                <%# new string('★', Convert.ToInt32(Eval("Valoracion"))) + new string('☆', 5 - Convert.ToInt32(Eval("Valoracion"))) %>
                            </td>
                            <td class="text-center">
                                <asp:LinkButton ID="BtnEliminar" runat="server"
                                    CommandName="Eliminar" CommandArgument='<%# Eval("idComentario") %>'
                                    OnClick="BtnEliminar_Click"
                                    OnClientClick="return confirm('¿Está seguro de eliminar al usuario?');"
                                    CssClass="btn btn-outline-danger btn-sm">
                                    <i class="fa-solid fa-trash"></i> Eliminar
                                </asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                                </tbody>
                            </table>
                        </div>
                    </FooterTemplate>
                </asp:Repeater>
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
