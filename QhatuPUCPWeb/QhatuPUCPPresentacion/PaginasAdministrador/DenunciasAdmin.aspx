<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayoutMasterAdmin.Master" AutoEventWireup="true" CodeBehind="DenunciasAdmin.aspx.cs" Inherits="QhatuPUCPPresentacion.PaginasAdministrador.DenunciasAdmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
<div class="container mt-4">
    <!-- Título -->
    <div class="text-center mb-4">
        <h2 class="fw-bold">Denuncias</h2>
    </div>

    <!-- Filtros de búsqueda -->
    <div class="row justify-content-center mb-4">
        <div class="col-md-8 col-sm-12">
            <div class="card p-3 shadow-sm">
                <!-- Buscar por motivo -->
                <div class="input-group mb-2">
                    <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control" placeholder="Buscar por motivo..." AutoPostBack="true" OnTextChanged="txtBuscar_TextChanged" />
                </div>

                <!-- Rango de fechas -->
                <div class="row mb-2">
                    <div class="col-md-6">
                        <label class="form-label">Desde:</label>
                        <asp:TextBox ID="txtFechaInicio" runat="server" CssClass="form-control" TextMode="Date" />
                    </div>
                    <div class="col-md-6">
                        <label class="form-label">Hasta:</label>
                        <asp:TextBox ID="txtFechaFin" runat="server" CssClass="form-control" TextMode="Date" />
                    </div>
                </div>

                <!-- Botón buscar y mensaje de error -->
                <div class="d-flex justify-content-between align-items-center">
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary" OnClick="btnBuscar_Click" />
                    <asp:Label ID="lblError" runat="server" CssClass="text-danger fw-bold d-block mb-2" EnableViewState="false" />
                </div>
            </div>
        </div>

        <!-- Botón descargar reporte -->
        <div class="col-md-3 col-sm-12 d-flex align-items-center">
            <asp:Button ID="btnDescargarReporte" runat="server" Text="Descargar Reporte" CssClass="btn btn-success w-100" OnClick="btnDescargarReporte_Click" />
        </div>
    </div>
</div>
<!-- Tabla con datos -->
<div class="container p-4">
    <table class="table table-hover table-bordered">
        <thead class="table-dark">
            <tr>
                <th>Id</th> 
                <th>Publicación</th>
                <th>Denunciante</th>
                <th>Motivo</th>
                <th>Fecha de Denuncia</th>
                <th>Estado</th>
                <th>Acciones</th>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="rptDenuncias" runat="server" OnItemCommand="rptDenuncias_ItemCommand">
                <ItemTemplate>
                    <tr>
                        <td><%# Eval("idDenuncia") %></td>
                        <td><%# Eval("autor.titulo") %></td>
                        <td><%# Eval("denunciante.nombre") %></td>
                        <td><%# Eval("motivo") %></td>
                        <td><%# Eval("fechaDenuncia", "{0:dd/MM/yyyy}") %></td>
                        <td><%# ((int)Eval("admin.idUsuario") != 0) ? "Revisada" : "Pendiente" %></td>
                        <td>
                            <asp:Button ID="btnRevisar" runat="server" 
                                CommandName="Revisar" 
                                CommandArgument='<%# Eval("IdDenuncia") %>'
                                Text='<%# ((int)Eval("admin.idUsuario") != 0) ? "✓ Revisada" : "Revisar" %>'
                                CssClass='<%# ((int)Eval("admin.idUsuario") != 0) ? "btn btn-success btn-sm" : "btn btn-outline-primary btn-sm" %>'
                                Enabled='<%# ((int)Eval("admin.idUsuario") == 0) %>' />
                            <asp:Button ID="btnOcultar" runat="server"
                               CommandName="Ocultar"
                               CommandArgument='<%# Eval("idDenuncia") %>'
                               Text="Ocultar"
                               CssClass="btn btn-outline-danger btn-sm ms-1"
                               OnClientClick="return confirm('¿Estás seguro de que deseas ocultar esta denuncia?');" />
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
