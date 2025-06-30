<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="Postear.aspx.cs" Inherits="QhatuPUCPPresentacion.HacerPubli.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="estilos_postear.css" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/select2@4.1.0/dist/css/select2.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/jquery@3.6.0/dist/jquery.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/select2@4.1.0/dist/js/select2.min.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="container">
        <div class="row justify-content-center">
            <asp:Panel ID="pnlCrearPublicacion" runat="server" CssClass="card p-4 shadow-sm rounded-4 w-100">
                <h3 class="mb-0 fw-semibold">Crear nueva publicación</h3>

                <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger mb-3 d-block" />

                <div class="mb-3">
                    <label for="txtTitulo" class="form-label">Título</label>
                    <asp:TextBox ID="txtTitulo" runat="server" CssClass="form-control" placeholder="Ingresa el título" />
                </div>

                <div class="mb-3">
                    <label for="txtDescripcion" class="form-label">Descripción</label>
                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" placeholder="Escribe la descripción de tu publicación" />
                </div>

                <div class="row mb-0">


                    <div class="col-md-4 mb-3">
                        <label class="form-label">Facultades asociadas</label>
                        <div class="dropdown-checklist">
                            <button type="button" class="form-control dropdown-toggle" onclick="toggleDropdown(this)">
                                Seleccionar facultades
                            </button>
                            <div class="dropdown-list d-none">
                                <asp:CheckBoxList ID="chkFacultades" runat="server" CssClass="form-check" RepeatDirection="Vertical" />
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4 mb-3">
                        <label class="form-label">Especialidades asociadas</label>
                        <div class="dropdown-checklist">
                            <button type="button" class="form-control dropdown-toggle" onclick="toggleDropdown(this)">
                                Seleccionar especialidades
                            </button>
                            <div class="dropdown-list d-none">
                                <asp:CheckBoxList ID="chkEspecialidades" runat="server" CssClass="form-check" RepeatDirection="Vertical" />
                            </div>
                        </div>
                    </div>

                    <div class="col-md-4 mb-3">
                        <label class="form-label">Cursos asociados</label>
                        <div class="dropdown-checklist">
                            <button type="button" class="form-control dropdown-toggle" onclick="toggleDropdown(this)">
                                Seleccionar cursos
                            </button>
                            <div class="dropdown-list d-none">
                                <asp:CheckBoxList ID="chkCursos" runat="server" CssClass="form-check" RepeatDirection="Vertical" />
                            </div>
                        </div>
                    </div>
                </div>

                <div class="mb-3">
                    <label for="fuImagen" class="form-label">Subir imagen</label>
                    <asp:FileUpload ID="fuImagen" runat="server" CssClass="form-control" />
                    <small class="form-text text-muted">Si no subes una imagen, se usará la predeterminada.</small>
                </div>

                <div class="text-end">
                    <asp:Button ID="btnPublicar" runat="server" Text="Publicar" CssClass="btn btn-primary px-4" OnClick="btnPublicar_Click" />
                </div>
            </asp:Panel>

        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
    <script>
        function toggleDropdown(button) {
            const dropdown = button.nextElementSibling;
            dropdown.classList.toggle("d-none");
        }

        // Cerrar si haces clic fuera
        document.addEventListener('click', function (e) {
            document.querySelectorAll('.dropdown-checklist .dropdown-list').forEach(function (list) {
                if (!list.contains(e.target) && !list.previousElementSibling.contains(e.target)) {
                    list.classList.add('d-none');
                }
            });
        });
    </script>

</asp:Content>

