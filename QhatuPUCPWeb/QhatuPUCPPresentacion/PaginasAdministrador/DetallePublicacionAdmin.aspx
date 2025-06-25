<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayoutMasterAdmin.Master" AutoEventWireup="true" CodeBehind="DetallePublicacionAdmin.aspx.cs" Inherits="QhatuPUCPPresentacion.PaginasAdministrador.DetallePublicacionAdmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/PaginasAdministrador/DetallePubliAdmin.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <asp:HiddenField ID="hfValoracion" runat="server" />
    <asp:HiddenField ID="hfIdPublicacion" runat="server" />

    <div class="container mt-4">
        <!-- Tarjeta de publicación -->
        <div class="card shadow-sm mb-4">
            <div class="card-header bg-dark text-white d-flex justify-content-between align-items-center">
                <span><strong>Detalles de la Publicación</strong></span>
            </div>
            <div class="card-body">
                <div class="row">
                    <div class="col-md-8">
                        <h5><asp:Label ID="lblTitulo" runat="server" /></h5>
                        <p class="mb-2"><asp:Label ID="lblDescripcion" runat="server" /></p>
                        <p class="mb-1 text-muted">Autor: <strong><asp:Label ID="lblAutor" runat="server" /></strong></p>
                        <p class="text-muted">Publicado: <asp:Label ID="lblTiempo" runat="server" /></p>
                    </div>
                    <div class="col-md-4 text-center">
                        <asp:Image ID="imgPublicacion" runat="server" CssClass="img-thumbnail img-fluid" />
                    </div>
                </div>
            </div>
        </div>

        <!-- Tabla de comentarios -->
        <div class="card shadow-sm">
            <div class="card-header bg-secondary text-white d-flex justify-content-between align-items-center">
                <span><strong>Comentarios</strong></span>
            </div>
            <div class="card-body p-0">
                <asp:Repeater ID="rptComentarios" runat="server" OnItemCommand="rptComentarios_ItemCommand">
                    <HeaderTemplate>
                        <table class="table table-hover table-bordered mb-0">
                            <thead class="table-light">
                                <tr>
                                    <th>#</th>
                                    <th>Autor</th>
                                    <th>Fecha</th>
                                    <th>Contenido</th>
                                    <th>Valoración</th>
                                    <th>Acciones</th>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%# Container.ItemIndex + 1 %></td>
                            <td><%# Eval("Autor") %></td>
                            <td><%# Eval("Fecha") %></td>
                            <td><%# Eval("Contenido") %></td>
                            <td><%# new string('★', Convert.ToInt32(Eval("Valoracion"))) + new string('☆', 5 - Convert.ToInt32(Eval("Valoracion"))) %></td>
                            <td>
                                <asp:LinkButton ID="BtnEliminar" runat="server"
                                    CommandName="Eliminar" CommandArgument='<%# Eval("idComentario") %>'
                                    OnClick="BtnEliminar_Click"
                                    OnClientClick="return confirm('¿Está seguro de eliminar el comentario?');"
                                    CssClass="btn btn-sm btn-danger">
                                    <i class="fa-solid fa-trash"></i>
                                </asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                            </tbody>
                        </table>
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
