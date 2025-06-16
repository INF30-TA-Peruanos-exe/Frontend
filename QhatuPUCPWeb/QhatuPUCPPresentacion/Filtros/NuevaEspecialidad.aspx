<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayoutMasterAdmin.Master" AutoEventWireup="true" CodeBehind="NuevaEspecialidad.aspx.cs" Inherits="QhatuPUCPPresentacion.Filtros.NuevaEspecialidad" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
     <div class="container">
     <div class="card">
         <div class="card-header">
             <h2>
                 <asp:Label ID="LblTitulo" runat="server" Text=""></asp:Label>
             </h2>
         </div>  
         <div class="card-body">
             <div class="mb-3">
                 <label for="TxtIdEspecialidad" class="form-label">Id Especialidad:</label>
                 <asp:TextBox ID="TxtIdEspecialidad" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
             </div>
             <div class="mb-3">
                 <label for="TxtNombre" class="form-label">Nombre de Especialidad:</label>
                 <asp:TextBox ID="TxtNombre" runat="server" CssClass="form-control"></asp:TextBox>
             </div>
             <div class="mb-3">
                 <label for="ChkActivo" class="form-label">¿Esta Activo?</label>
                 <asp:CheckBox ID="ChkActivo" runat="server" CssClass="form-check-input" />
             </div>
             <div class="mb-3">
                 <asp:Label ID="LblError" runat="server" Text="" CssClass="text-danger"></asp:Label>
             </div>
         </div>
         <div class="card-footer">
             <asp:Button ID="BtnAgregar" runat="server" OnClick="BtnAgregar_Click"
                 Text="Guardar" CssClass="btn btn-success" />
             <asp:Button ID="BtnCancelar" runat="server" OnClick="BtnCancelar_Click"
                 Text="Cancelar" CssClass="btn btn-secondary" />
         </div>
     </div>
 </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
