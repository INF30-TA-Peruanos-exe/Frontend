<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayoutMasterAdmin.Master" AutoEventWireup="true" CodeBehind="ListaEspecialidad.aspx.cs" Inherits="QhatuPUCPPresentacion.Filtros.ListaEspecialidad" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="container mb-4">
    <h2 class="fw-bold">Especialidad</h2>
    </div>
    <div class="text-end pb-3">
    <asp:Button ID="BtnAgregar" OnClick="BtnAgregar_Click" runat="server" Text="Agregar Especialidad" CssClass="btn btn-success" />
    </div>
<!-- Tabla con datos -->    
<div class="container p-4">
    <table class="table table-hover table-bordered">
        <thead class="table-dark">
            <tr>
                <th>Id</th>
                <th>Nombre</th>
                <th>Acciones</th>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="rptEspecialidades" runat="server">
                <ItemTemplate>
                    <tr>
                        <td><%# Eval("idEspecialidad") %></td>
                        <td><%# Eval("nombre") %></td>
                        <td>
                            <ItemTemplate>
                                <asp:LinkButton ID="BtnEditar" runat="server" 
                                    CommandName="Editar" CommandArgument='<%# Eval("idEspecialidad") %>'
                                    OnClick="BtnEditar_Click"
                                    CssClass="btn btn-sm btn-primary me-2">
                                    <i class="fa-solid fa-pen-to-square"></i> Editar
                                </asp:LinkButton>

                                <asp:LinkButton ID="BtnEliminar" runat="server"
                                    CommandName="Eliminar" CommandArgument='<%# Eval("idEspecialidad") %>'
                                    OnClick="BtnEliminar_Click"
                                    OnClientClick="return confirm('¿Está seguro de eliminar la Especialidad?');"
                                    CssClass="btn btn-sm btn-danger">
                                    <i class="fa-solid fa-trash"></i>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <asp:Label ID="LblError" runat="server" Text="" CssClass="text-danger">
            </asp:Label>
        </tbody>
    </table>
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
