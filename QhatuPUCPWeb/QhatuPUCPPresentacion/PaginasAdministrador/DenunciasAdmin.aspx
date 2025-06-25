<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayoutMasterAdmin.Master" AutoEventWireup="true" CodeBehind="DenunciasAdmin.aspx.cs" Inherits="QhatuPUCPPresentacion.PaginasAdministrador.DenunciasAdmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="container-fluid px-4 py-3 mb-4 bg-light border-bottom">
        <div class="d-flex justify-content-between align-items-center">
            <h3 class="fw-bold text-primary mb-0">
                Administración de Denuncias
            </h3>
            <asp:Button ID="btnDescargarReporte" runat="server" Text="Descargar Reporte" 
                CssClass="btn btn-success" OnClick="btnDescargarReporte_Click" />
        </div>
    </div>
    <!-- Barra de búsqueda -->
<div class="container mb-3">
    <div class="input-group">
        <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control" placeholder="Buscar por motivo..."  AutoPostBack="true" OnTextChanged="txtBuscar_TextChanged" />
        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-outline-secondary" OnClick="btnBuscar_Click" />
    </div>
</div>
<!-- Tabla con datos -->
<div class="container p-4">
    <table class="table table-hover table-bordered">
        <thead class="table-dark">
            <tr>
                <th>Id</th> 
                <th>Autor</th>
                <th>Denunciante</th>
                <th>Motivo</th>
                <th>Fecha de Denuncia</th>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="rptDenuncias" runat="server">
                <ItemTemplate>
                    <tr>
                        <td><%# Eval("idDenuncia") %></td>
                        <td><%# Eval("autor.titulo") %></td>
                        <td><%# Eval("denunciante.nombre") %></td>
                        <td><%# Eval("motivo") %></td>
                        <td><%# Eval("fechaDenuncia", "{0:dd/MM/yyyy}") %></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
