<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayoutMasterAdmin.Master" AutoEventWireup="true" CodeBehind="DenunciasAdmin.aspx.cs" Inherits="QhatuPUCPPresentacion.PaginasAdministrador.DenunciasAdmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
<div class="container mt-4">
        <!-- Título -->
        <div class="text-center mb-4">
            <h2 class="fw-bold text-white">
                <i class="fa-solid fa-triangle-exclamation"></i>Gestión de Denuncias
            </h2>
        </div>

        <!-- Barra de búsqueda y botón de descarga -->
        <div class="row justify-content-center align-items-center mb-4 g-2">
            <div class="col-md-8 col-sm-12">
            <div class="card shadow-sm p-4 bg-white rounded-4 border-0">

                <!-- Título (opcional) -->
                <h5 class="fw-bold mb-3 text-primary">
                    <i class="fa-solid fa-filter me-2"></i>Filtrar Denuncias
                </h5>

                <!-- Buscar por motivo -->
                <div class="input-group mb-3">
                    <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control" placeholder="Buscar por motivo..." AutoPostBack="true" OnTextChanged="txtBuscar_TextChanged" />
                    <span class="input-group-text bg-primary text-white">
                        <i class="fa-solid fa-magnifying-glass"></i>
                    </span>
                </div>

                <!-- Rango de fechas -->
                <div class="row g-2 mb-3">
                    <div class="col-md-6">
                        <label class="form-label fw-semibold">Desde:</label>
                        <asp:TextBox ID="txtFechaInicio" runat="server" CssClass="form-control" TextMode="Date" />
                    </div>
                    <div class="col-md-6">
                        <label class="form-label fw-semibold">Hasta:</label>
                        <asp:TextBox ID="txtFechaFin" runat="server" CssClass="form-control" TextMode="Date" />
                    </div>
                </div>

                <!-- Botones en la misma fila -->
                <div class="row align-items-center">
                    <div class="col-md-6">
                        <asp:Button ID="btnBuscar" runat="server" Text="🔍 Buscar" CssClass="btn btn-primary fw-semibold w-100" OnClick="btnBuscar_Click" />
                    </div>
                    <div class="col-md-6">
                        <asp:Button ID="btnDescargarReporte" runat="server" Text="📥 Reporte de Denuncias" CssClass="btn btn-success fw-semibold w-100" OnClick="btnDescargarReporte_Click" />
                    </div>
                </div>

                <!-- Mensaje de error -->
                <div class="mt-3">
                    <asp:Label ID="lblError" runat="server" CssClass="text-danger fw-bold small" EnableViewState="false" />
                </div>
            </div>
        </div>

            
    </div>
</div>
<!-- Tabla con datos -->
    <div class="container p-4 bg-light rounded shadow-sm">
        <table class="table table-hover table-bordered align-middle">
        <thead class="table-dark text-center align-middle">
            <tr>
                <th scope="col"><i class="fa-solid fa-hashtag me-1"></i> ID</th>
                <th scope="col"><i class="fa-solid fa-file-lines me-1"></i> Publicación</th>
                <th scope="col"><i class="fa-solid fa-user me-1"></i> Denunciante</th>
                <th scope="col"><i class="fa-solid fa-circle-exclamation me-1"></i> Motivo</th>
                <th scope="col"><i class="fa-solid fa-calendar-days me-1"></i> Fecha</th>
                <th scope="col"><i class="fa-solid fa-info-circle me-1"></i> Estado</th>
                <th scope="col"><i class="fa-solid fa-gear me-1"></i> Acciones</th>
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
                        <td class="text-center">
                            <!-- Botón Revisar -->
                            <asp:Button ID="btnRevisar" runat="server" 
                                CommandName="Revisar" 
                                CommandArgument='<%# Eval("IdDenuncia") %>'
                                Text='<%# ((int)Eval("admin.idUsuario") != 0) ? "✓ Revisada" : "🔍 Revisar" %>'
                                CssClass='<%# ((int)Eval("admin.idUsuario") != 0) ? "btn btn-success btn-sm me-1" : "btn btn-outline-primary btn-sm me-1" %>'
                                Enabled='<%# ((int)Eval("admin.idUsuario") == 0) %>' />

                            <!-- Botón Ocultar -->
                            <asp:LinkButton ID="btnOcultar" runat="server"
                               CommandName="Ocultar"
                               CommandArgument='<%# Eval("idDenuncia") %>'
                               CssClass="btn btn-outline-danger btn-sm"
                               OnClientClick="return confirm('¿Estás seguro de que deseas ocultar esta denuncia?');">
                               <i class="fa-solid fa-eye-slash me-1"></i> Ocultar
                            </asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>
</div>
    <div class="d-flex justify-content-center align-items-center gap-3 mt-3">
        <asp:Button ID="btnAnterior" runat="server" Text="<" OnClick="btnAnterior_Click"
            CssClass="btn btn-outline-light btn-sm" />

        <asp:Label ID="lblPagina" runat="server" CssClass="fw-bold px-3 fs-6 text-white"></asp:Label>

        <asp:Button ID="btnSiguiente" runat="server" Text=">" OnClick="btnSiguiente_Click"
            CssClass="btn btn-outline-light btn-sm" />
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
