<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" 
    CodeBehind="PaginaFavoritosNew.aspx.cs" Inherits="QhatuPUCPPresentacion.Favoritos.PaginaFavoritosNew" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .publicacion-oculta {
            position: relative;
        }
        .publicacion-oculta .overlay-oculto {
            position: absolute;
            top: 0;
            left: 0;
            width: 100%;
            height: 180px; /* Misma altura que la imagen */
            background-color: rgba(128, 128, 128, 0.5);
            z-index: 1;
        }
        .publicacion-oculta .card-img-top {
            filter: grayscale(70%);
        }
        .publicacion-oculta .badge-oculto {
            position: absolute;
            top: 10px;
            left: 10px;
            z-index: 2;
            background-color: #6c757d;
            color: white;
            padding: 3px 8px;
            border-radius: 4px;
            font-size: 0.8rem;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="container mt-5">
        <div class="cards-box p-4">
            <div class="row g-4">
                <asp:Repeater ID="rptPublicacionesFavoritas" runat="server" >
                    <ItemTemplate>
                        <div class='col-12 col-sm-6 col-md-4 position-relative <%# Eval("estado").ToString() == "OCULTO" ? "publicacion-oculta" : "" %>'>
                            <div class="card h-100 shadow-sm border-0">
                                <a href='<%# ResolveUrl("~/Publicacion/DetallePublicacion.aspx?id=" + Eval("idPublicacion")) %>'
                                    style="text-decoration: none; color: inherit;">
                                    <%# Eval("estado").ToString() == "OCULTO" ? "<div class='overlay-oculto'></div><span class='badge-oculto'>OCULTO</span>" : "" %>
                                    <img src='<%# Eval("rutaImagen") %>' class="card-img-top"
                                        style="height: 180px; object-fit: cover;" />

                                    <div class="card-body pb-4">
                                        <p class="card-text mb-0 fw-medium" style="font-size: 0.95rem;">
                                            <%# Eval("titulo") %>
                                        </p>
                                    </div>
                                </a>

                                <asp:LinkButton ID="BtnEliminar" runat="server"
                                    CommandName="Eliminar" CommandArgument='<%# Eval("idPublicacion") %>'
                                    OnClick="BtnEliminar_Click"
                                    OnClientClick="return confirm('¿Está seguro de eliminar esta publicación de guardados?');"
                                    CssClass="position-absolute bottom-0 end-0 m-3 p-0 bg-transparent border-0"
                                    Style="z-index: 10;">
                                    <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24"
                                         stroke-width="1.5" stroke="#e63946" width="24" height="24">
                                        <path stroke-linecap="round" stroke-linejoin="round"
                                              d="m14.74 9-.346 9m-4.788 0L9.26 9m9.968-3.21c.342.052.682.107 1.022.166m-1.022-.165L18.16 19.673a2.25 2.25 0 0 1-2.244 2.077H8.084a2.25 2.25 0 0 1-2.244-2.077L4.772 5.79m14.456 0a48.108 48.108 0 0 0-3.478-.397m-12 .562c.34-.059.68-.114 1.022-.165m0 0a48.11 48.11 0 0 1 3.478-.397m7.5 0v-.916c0-1.18-.91-2.164-2.09-2.201a51.964 51.964 0 0 0-3.32 0c-1.18.037-2.09 1.022-2.09 2.201v.916m7.5 0a48.667 48.667 0 0 0-7.5 0" />
                                    </svg>
                                </asp:LinkButton>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
</asp:Content>