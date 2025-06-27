<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="Notificaciones.aspx.cs" Inherits="QhatuPUCPPresentacion.Notificaciones.Notificaciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Notificaciones/notificaciones.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="container">
        <h2>Mis Notificaciones</h2>
        
        <asp:Panel ID="pnlNotificaciones" runat="server">
            <asp:Repeater ID="rptNotificacionesUsuario" runat="server">
                <ItemTemplate>
                    <div class="notificacion-item">
                        <div class="notificacion-fecha"><%# Eval("Fecha", "{0:dd/MM/yyyy HH:mm}") %></div>
                        <div class="notificacion-mensaje"><%# Eval("mensaje") %></div>
                    </div>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="lblEmpty" runat="server" Visible='<%# ((Repeater)Container.NamingContainer).Items.Count == 0 %>'
                        Text="No tienes notificaciones nuevas." CssClass="sin-notificaciones"></asp:Label>
                </FooterTemplate>
            </asp:Repeater>
        </asp:Panel>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
    <script type="text/javascript">
    </script>
</asp:Content>
