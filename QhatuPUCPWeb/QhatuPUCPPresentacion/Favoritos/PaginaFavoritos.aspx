<%@ Page Title="Favoritos" Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="PaginaFavoritos.aspx.cs" Inherits="QhatuPUCPPresentacion.Favoritos.PaginaFavoritos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .pagina-inicio-small {
            font-size: 0.9rem;
        }

        .pagina-inicio-small .card-text {
            font-size: 0.95rem;
        }

        .card {
            position: relative;
            cursor: pointer;
            transition: transform 0.2s;
        }

        .card:hover {
            transform: scale(1.01);
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="pagina-inicio-small">
        <div class="container">
            <h4 class="mb-4">Mis Publicaciones Favoritas</h4>
            <div class="row g-4">
                <asp:Repeater ID="rptFavoritos" runat="server" OnItemCommand="rptFavoritos_ItemCommand">
                    <ItemTemplate>
                        <div class="col-12 col-sm-6 col-md-4">
                            <div class="card h-100 shadow-sm border-0">
                                <asp:LinkButton ID="btnVerDetalle" runat="server"
                                    CommandName="VerDetalle"
                                    CommandArgument='<%# Eval("IdPublicacion") %>'
                                    CssClass="stretched-link"></asp:LinkButton>

                                <img src='<%# Eval("ImagenUrl") %>' class="card-img-top"
                                     style="height: 180px; object-fit: cover;" />

                                <div class="card-body d-flex justify-content-between align-items-center">
                                    <p class="card-text mb-0"><%# Eval("Titulo") %></p>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>

