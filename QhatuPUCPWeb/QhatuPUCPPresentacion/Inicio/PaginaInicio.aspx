<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="PaginaInicio.aspx.cs" Inherits="QhatuPUCPPresentacion.Inicio.PaginaInicio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .pagina-inicio-small {
            font-size: 0.9rem;
        }

        .pagina-inicio-small .card-text {
            font-size: 0.95rem;
        }

        .pagina-inicio-small .form-select {
            font-size: 0.9rem;
        }

        .card-link {
            color: inherit;
            text-decoration: none;
        }

        .card-link:hover {
            text-decoration: none;
        }

        .btn-favorito {
            position: absolute;
            top: 10px;
            right: 15px;
            background: transparent;
            border: none;
            z-index: 2;
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
        <!-- Filtros -->
        <div class="container mb-4">
            <div class="row g-2">
                <div class="col-auto">
                    <asp:DropDownList ID="ddlFacultad" runat="server" CssClass="form-select"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlFacultad_SelectedIndexChanged" />
                </div>
                <div class="col-auto">
                    <asp:DropDownList ID="ddlEspecialidad" runat="server" CssClass="form-select"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlEspecialidad_SelectedIndexChanged" />
                </div>
                <div class="col-auto">
                    <asp:DropDownList ID="ddlCurso" runat="server" CssClass="form-select"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlCurso_SelectedIndexChanged" />
                </div>
            </div>
        </div>

        <!-- Publicaciones -->
        <div class="container">
            <div class="cards-box p-4">
                <div class="row g-4">
                    <asp:Repeater ID="rptPublicaciones" runat="server" OnItemCommand="rptPublicaciones_ItemCommand">
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

                                        <asp:LinkButton ID="btnFavorito" runat="server"
                                            CommandName="Favorito"
                                            CommandArgument='<%# Eval("IdPublicacion") %>'
                                            OnClientClick="event.stopPropagation();"
                                            CssClass="btn-favorito">
                                            <svg xmlns="http://www.w3.org/2000/svg"
                                                 fill='<%# (bool)Eval("EsFavorito") ? "gold" : "none" %>'
                                                 stroke='<%# (bool)Eval("EsFavorito") ? "gold" : "gray" %>'
                                                 viewBox="0 0 24 24" stroke-width="1.5" width="24" height="24">
                                                <path stroke-linecap="round" stroke-linejoin="round"
                                                      d="M11.48 3.499a.562.562 0 0 1 1.04 0l2.125 5.111a.563.563 0 0 0 .475.345l5.518.442c.499.04.701.663.321.988l-4.204 3.602a.563.563 0 0 0-.182.557l1.285 5.385a.562.562 0 0 1-.84.61l-4.725-2.885a.562.562 0 0 0-.586 0L6.982 20.54a.562.562 0 0 1-.84-.61l1.285-5.386a.562.562 0 0 0-.182-.557l-4.204-3.602a.562.562 0 0 1 .321-.988l5.518-.442a.563.563 0 0 0 .475-.345L11.48 3.5Z" />
                                            </svg>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
    <!-- Puedes incluir scripts si lo necesitas -->
</asp:Content>
