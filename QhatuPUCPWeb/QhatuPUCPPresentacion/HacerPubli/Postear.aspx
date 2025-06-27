<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="Postear.aspx.cs" Inherits="QhatuPUCPPresentacion.HacerPubli.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="estilos_postear.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="container">
        <div class="row justify-content-center">
                <asp:Panel ID="pnlCrearPublicacion" runat="server" CssClass="card p-4 shadow-sm rounded-4 w-100">
                    <h3 class="mb-0 fw-semibold">Crear nueva publicación</h3>

                    <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger mb-3 d-block" />

                    <!-- Título -->
                    <div class="mb-3">
                        <label for="txtTitulo" class="form-label">Título</label>
                        <asp:TextBox ID="txtTitulo" runat="server" CssClass="form-control" placeholder="Ingresa el título" />
                    </div>

                    <!-- Descripción -->
                    <div class="mb-3">
                        <label for="txtDescripcion" class="form-label">Descripción</label>
                        <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" placeholder="Escribe la descripción de tu publicación" />
                    </div>

                    <!-- Sección de checkboxes -->
                    <div class="row mb-0">
                        <div class="col-md-4 mb-3">
                            <label class="form-label">Cursos asociados</label>
                            <asp:CheckBoxList ID="chkCursos" runat="server" CssClass="form-check mb-2 d-block" RepeatDirection="Vertical" />
                        </div>
                        <div class="col-md-4 mb-3">
                            <label class="form-label">Facultades asociadas</label>
                            <asp:CheckBoxList ID="chkFacultades" runat="server" CssClass="form-check mb-2 d-block" RepeatDirection="Vertical" />
                        </div>
                        <div class="col-md-4 mb-3">
                            <label class="form-label">Especialidades asociadas</label>
                            <asp:CheckBoxList ID="chkEspecialidades" runat="server" CssClass="form-check mb-2 d-block" RepeatDirection="Vertical" />
                        </div>
                    </div>

                    <!-- Imagen -->
                    <div class="mb-3">
                        <label for="fuImagen" class="form-label">Subir imagen</label>
                        <asp:FileUpload ID="fuImagen" runat="server" CssClass="form-control" />
                        <small class="form-text text-muted">Si no subes una imagen, se usará la predeterminada.</small>
                    </div>


                    <!-- Botón -->
                    <div class="text-end">
                        <asp:Button ID="btnPublicar" runat="server" Text="Publicar" CssClass="btn btn-primary px-4" OnClick="btnPublicar_Click" />
                    </div>
                </asp:Panel>
            
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>

