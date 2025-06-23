<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayoutMasterAdmin.Master" AutoEventWireup="true" CodeBehind="PaginaInicioAdmin.aspx.cs" Inherits="QhatuPUCPPresentacion.Inicio.PaginaInicioAdmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <!-- Título de la página -->
    <div class="container mb-4">
        <h2 class="fw-bold">Publicaciones</h2>
    </div>
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
    <!-- Tabla con datos -->
    <div class="container p-4">
        <table class="table table-hover table-bordered">
            <thead class="table-dark">
                <tr>
                    <th>ID</th>
                    <th>Título</th>
                    <th>Estado Publicación</th>
                    <th>Fecha</th>
                    <th>Acciones</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="rptPublicaciones" runat="server"  OnItemDataBound="rptPublicaciones_ItemDataBound">
                    <ItemTemplate>
                        <tr runat="server" id="filaPublicacion">
                            <td><%# Eval("idPublicacion")%></td>
                            <td><%# Eval("titulo") %></td>
                            <td><%# Eval("estado") %></td>
                            <td><%# Eval("fechaPublicacion", "{0:dd/MM/yyyy}") %></td>
                            <td style="position: relative;">
                                <a class="btn btn-sm btn-primary"
                                   href='<%# ResolveUrl("~/PaginasAdministrador/DetallePublicacionAdmin.aspx?id=" + Eval("idPublicacion")) %>'>
                                    Ver Detalle
                                </a>

                                <asp:LinkButton ID="BtnEliminar" runat="server"
                                    CommandName="Eliminar" CommandArgument='<%# Eval("idPublicacion") %>'
                                    OnClick="BtnEliminar_Click"
                                    OnClientClick="return confirm('¿Está seguro de eliminar la publicacion?');"
                                    CssClass="btn btn-sm btn-danger">
                                    <i class="fa-solid fa-trash"></i>
                                </asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
