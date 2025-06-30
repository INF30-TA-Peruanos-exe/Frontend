<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayoutMasterAdmin.Master" AutoEventWireup="true" CodeBehind="NuevoAdmin.aspx.cs" Inherits="QhatuPUCPPresentacion.PaginasAdministrador.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Contenido" runat="server">
    <div class="container mt-4">
        <h2 class="fw-bold text-center text-white mb-4">
            <i class="fa-solid fa-user-shield me-2 text-warning"></i>Asignar Nuevo Administrador
        </h2>

        <!-- Buscador de usuarios -->
        <div class="input-group mb-4">
            <span class="input-group-text"><i class="fa-solid fa-envelope"></i></span>
            <asp:TextBox ID="txtBuscarCorreo" runat="server" CssClass="form-control" Placeholder="Buscar usuario por correo..." />
            <asp:Button ID="btnBuscarCorreo" runat="server" Text="Buscar" CssClass="btn btn-primary" OnClick="btnBuscarCorreo_Click" />
        </div>

        <!-- Lista de usuarios buscados -->
        <asp:Repeater ID="rptUsuarios" runat="server">
            <ItemTemplate>
                <div class="card mb-3">
                    <div class="card-body">
                        <h5 class="card-title"><%# Eval("nombre") %> (<%# Eval("nombreUsuario") %>)</h5>
                        <p class="card-text"><i class="fa-solid fa-envelope me-1"></i><%# Eval("correo") %></p>

                        <!-- Campo para clave maestra y botón asignar -->
                        <div class="input-group">
                            <asp:TextBox ID="txtClaveMaestra" runat="server" CssClass="form-control" 
                                Placeholder="Clave maestra" />

                            <asp:Button ID="btnAsignar" runat="server" Text="Asignar"
                                CommandName="Asignar" CommandArgument='<%# Eval("idUsuario") %>'
                                CssClass="btn btn-primary btn-sm mt-2"
                                OnCommand="btnAsignar_Command" />
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>

        <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger fw-bold"></asp:Label>
    </div>
</asp:Content>
