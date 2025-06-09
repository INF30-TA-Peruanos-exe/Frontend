<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="PaginaInicio.aspx.cs" Inherits="QhatuPUCPPresentacion.Inicio.PaginaInicio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <!-- Filtros -->
    <div class="container mb-4">
        <div class="row g-2">
            <div class="col-auto">
                <asp:DropDownList ID="ddlFacultad" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlFacultad_SelectedIndexChanged" />
            </div>
            <div class="col-auto">
                <asp:DropDownList ID="ddlEspecialidad" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlEspecialidad_SelectedIndexChanged" />
            </div>
            <div class="col-auto">
                <asp:DropDownList ID="ddlCurso" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlCurso_SelectedIndexChanged" />
            </div>
        </div>
    </div>

    <!-- Cards con contenedor -->
    <div class="container">
        <div class="cards-box p-4">
            <div class="row g-4">
                <asp:Repeater ID="rptPublicaciones" runat="server">
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

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
    
</asp:Content>
