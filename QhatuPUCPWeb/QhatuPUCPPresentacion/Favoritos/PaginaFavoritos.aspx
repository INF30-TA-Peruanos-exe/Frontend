<%@ Page Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="PaginaFavoritos.aspx.cs" Inherits="QhatuPUCPPresentacion.Favoritos.PaginaFavoritos" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="row justify-content-center mt-5">
        <div class="container">
            <div class="cards-box p-4">
                <div class="row g-4">
                    <asp:Repeater ID="rptPublicacionesFavoritas" runat="server">
                        <ItemTemplate>
                            <div class="col-12 col-sm-6 col-md-4">
                                <a href='<%# ResolveUrl("~/Publicacion/DetallePublicacion.aspx?id=" + Eval("idPublicacion")) %>' style="text-decoration: none; color: inherit;">
                                    <div class="card h-100 shadow-sm border-0">
                                        <img src='<%# Eval("rutaImagen") %>' class="card-img-top" style="height: 180px; object-fit: cover;" />
                                        <div class="card-body d-flex justify-content-between align-items-center">
                                            <p class="card-text mb-0"><%# Eval("titulo") %></p>
                                        </div>
                                    </div>
                                </a>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
