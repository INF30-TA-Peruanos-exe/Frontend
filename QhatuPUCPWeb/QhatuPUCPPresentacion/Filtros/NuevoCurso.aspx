<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayoutMasterAdmin.Master" AutoEventWireup="true" CodeBehind="NuevoCurso.aspx.cs" Inherits="QhatuPUCPPresentacion.Filtros.NuevoCurso" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="container mt-4">

        <div class="card bg-white text-dark shadow-lg border-light">
            <div class="card-header bg-secondary bg-gradient text-white">
                <h3 class="fw-bold mb-0">
                    <i class="fas fa-book icono-filtro mb-3"></i>
                    <asp:Label ID="LblTitulo" runat="server" Text="Nuevo Curso"></asp:Label>
                </h3>
            </div> 

         <div class="card-body">
             <div class="mb-3">
                 <label for="TxtIdCurso" class="form-label">Id Curso:</label>
                 <asp:TextBox ID="TxtIdCurso" runat="server" CssClass="form-control bg-white text-black border-secondary" ReadOnly="true" placeholder="(Se genera automáticamente)"></asp:TextBox>

                <!-- Nombre Facultad -->
                <div class="mb-3">
                    <label for="TxtNombre" class="form-label">Nombre de Facultad</label>
                    <asp:TextBox ID="TxtNombre" runat="server" CssClass="form-control  bg-white text-black border-secondary" placeholder="Ingrese nombre de la facultad"></asp:TextBox>
                </div>

                <!-- Activo -->
                <div class="form-check form-switch mb-3">
                    <input type="checkbox" class="form-check-input" id="ChkActivo"
                           runat="server" name="chkActivoSwitch" />
                    <label class="form-check-label" for="chkActivoSwitch">¿Está activa?</label>
                </div>

                <!-- Mensaje de error -->
                <div class="mb-3">
                    <asp:Label ID="LblError" runat="server" Text="" CssClass="text-danger fw-semibold"></asp:Label>
                </div>
            </div>

            <div class="card-footer d-flex justify-content-end gap-2">
                <asp:Button ID="BtnCancelar" runat="server" OnClick="BtnCancelar_Click"
                    Text="Cancelar" CssClass="btn btn-outline-secondary" />

                <asp:LinkButton ID="BtnAgregar" runat="server" OnClick="BtnAgregar_Click"
                    CssClass="btn btn-success">
                    <i class="fa-solid fa-save me-2"></i>Guardar
                </asp:LinkButton>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
