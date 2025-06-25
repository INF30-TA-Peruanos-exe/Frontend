<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="Notificaciones.aspx.cs" Inherits="QhatuPUCPPresentacion.Notificaciones.Notificaciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .notificacion-item {
            border: 1px solid #ddd;
            padding: 15px;
            margin-bottom: 10px;
            border-radius: 5px;
            background-color: #f9f9f9;
        }
        .notificacion-titulo {
            font-weight: bold;
            color: #333;
        }
        .notificacion-fecha {
            color: #666;
            font-size: 0.9em;
        }
        .notificacion-mensaje {
            margin-top: 5px;
            color: #000000; /* Texto en color negro */
        }
        .sin-notificaciones {
            text-align: center;
            padding: 20px;
            color: #666;
            font-style: italic;
        }
    </style>
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
        // Puedes agregar aquí cualquier script JavaScript que necesites
        // Por ejemplo, para marcar notificaciones como leídas
    </script>
</asp:Content>
