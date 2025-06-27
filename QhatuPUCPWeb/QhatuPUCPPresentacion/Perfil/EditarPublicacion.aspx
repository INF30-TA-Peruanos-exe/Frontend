<%@ Page Title="Editar Publicación" Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="EditarPublicacion.aspx.cs" Inherits="QhatuPUCPPresentacion.Perfil.EditarPublicacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Public/css/estilos_perfil.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="container">
        <div class="card shadow">
            <div class="card-body">
                <h2 class="mb-4">Editar Publicación</h2>
                <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger" />
                <div class="mb-3">
                    <label for="txtTitulo" class="form-label">Título</label>
                    <asp:TextBox ID="txtTitulo" runat="server" CssClass="form-control" />
                </div>
                <div class="mb-3">
                    <label for="txtDescripcion" class="form-label">Descripción</label>
                    <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control" />
                </div>
                <div class="mb-3">
                <label for="ddlEstado" class="form-label">Estado</label>
                    <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-select">
                        <asp:ListItem Text="VISIBLE" Value="VISIBLE" />
                        <asp:ListItem Text="OCULTO" Value="OCULTO" />
                    </asp:DropDownList>
                </div>
                <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-success" Text="Guardar Cambios" OnClick="btnGuardar_Click" />
                <asp:Button ID="btnCancelar" runat="server" CssClass="btn btn-secondary ms-2" Text="Cancelar" PostBackUrl="~/Perfil/Perfil.aspx" />
            </div>
        </div>
    </div>
</asp:Content>
