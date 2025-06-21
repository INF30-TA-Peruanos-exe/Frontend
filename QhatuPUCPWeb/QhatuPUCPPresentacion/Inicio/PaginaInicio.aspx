<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="PaginaInicio.aspx.cs" Inherits="QhatuPUCPPresentacion.Inicio.PaginaInicio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Inicio/paginaInicio.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />

    <div class="container mb-4">
        <div class="row g-2">
            <div class="col-auto">
                <asp:DropDownList ID="ddlFacultad" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlFacultad_SelectedIndexChanged" />
            </div>
            <div class="col-auto">
                <asp:DropDownList ID="ddlEspecialidad" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlEspecialidad_SelectedIndexChanged" />
            </div>
            <div class="col-auto">
                <asp:DropDownList ID="ddlCurso" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlCurso_SelectedIndexChanged" />
            </div>
        </div>
    </div>

    <div class="container">
        <div class="cards-box p-4">
            <div class="row g-4">
                <asp:Repeater ID="rptPublicaciones" runat="server">
                    <ItemTemplate>
                        <div class="col-12 col-sm-6 col-md-4 position-relative">
                            <div class="card h-100 shadow-sm border-0">
                                <a href='<%# ResolveUrl("~/Publicacion/DetallePublicacion.aspx?id=" + Eval("idPublicacion")) %>'
                                    style="text-decoration: none; color: inherit;">
                                    <img src='<%# Eval("rutaImagen") %>' class="card-img-top"
                                        style="height: 180px; object-fit: cover;" />

                                    <div class="card-body pb-4">
                                        <p class="card-text mb-0 fw-medium" style="font-size: 0.95rem;">
                                            <%# Eval("titulo") %>
                                        </p>
                                    </div>
                                </a>
                                <a href="javascript:void(0);"
                                   class="btn-guardar position-absolute bottom-0 end-0 m-3 p-0 bg-transparent border-0"
                                   onclick="toggleFavorito(<%# Eval("idPublicacion") %>, this)"
                                   data-favorito='<%# Eval("esFavorito").ToString().ToLower() %>'>
                                    <svg id='<%# "icon_" + Eval("idPublicacion") %>' xmlns="http://www.w3.org/2000/svg"
                                         viewBox="0 0 24 24" stroke-width="1.5" stroke="#2f5e93" width="24" height="24" fill='<%# (bool)Eval("esFavorito") ? "#2f5e93" : "none" %>'>
                                        <path stroke-linecap="round" stroke-linejoin="round" d="M17.593 3.322c1.1.128 1.907 1.077 1.907 2.185V21L12 17.25 4.5 21V5.507 c0-1.108.806-2.057 1.907-2.185a48.507 48.507 0 0 1 11.186 0Z" />
                                    </svg>
                                </a>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <div class="d-flex justify-content-end mt-4">
                    <div class="btn-group pagination-wrapper" role="group">
                        <asp:Repeater ID="rptPaginas" runat="server" OnItemCommand="rptPaginas_ItemCommand">
                            <ItemTemplate>
                                <asp:LinkButton runat="server"
                                    CommandName="Page"
                                    CommandArgument='<%# Eval("Numero") %>'
                                    CssClass='<%# (bool)Eval("EsActual") ? "btn btn-sm btn-custom-active" : "btn btn-sm btn-custom-inactive" %>'
                                    Text='<%# Eval("Texto") %>' />
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
        </div>
    </div>
    
    <!--Contenedor para toast-->
    <div id="toast-container" class="position-fixed bottom-0 end-0 p-3" style="z-index: 1100;"></div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
    <script>
        function toggleFavorito(idPublicacion, el) {
            PageMethods.CambiarFavorito(idPublicacion,
                function (res) {
                    if (res.exito) {
                        const svg = el.querySelector("svg");
                        const nuevo = res.nuevoEstado;
                        svg.setAttribute("fill", nuevo ? "#2f5e93" : "none");
                        el.setAttribute("data-favorito", nuevo ? "true" : "false");
                        const mensaje = nuevo ? "Publicación agregada a Guardados." : "Publicación eliminada de Guardados.";
                        mostrarToast(mensaje);
                    } else {
                        alert("Error: " + res.mensaje);
                    }
                },
                function (err) {
                    console.error("Error en PageMethod:", err);
                }
            );
        }

        function mostrarToast(mensaje) {
            const container = document.getElementById("toast-container");
            const id = "toast_" + Date.now();
            const html = `
                <div id="${id}" class="toast toast-exito border-0 px-2 py-1 mb-2" role="alert" aria-live="assertive" aria-atomic="true" data-bs-delay="60000" data-bs-autohide="true">
                    <div class="d-flex align-items-center justify-content-between">
                        <div class="toast-body d-flex align-items-center flex-grow-1">
                            <i class="fas fa-check-circle me-2 flex-shrink-0"></i>
                            <span>${mensaje}</span>
                        </div>
                        <button type="button" class="btn-close ms-2 flex-shrink-0" style="transform: scale(0.85);" data-bs-dismiss="toast" aria-label="Cerrar"></button>
                    </div>
                </div>`;
            container.insertAdjacentHTML("afterbegin", html);
            const toastEl = document.getElementById(id);
            const toast = new bootstrap.Toast(toastEl);
            toast.show();
            toastEl.addEventListener("hidden.bs.toast", () => {
                toastEl.remove();
            });
        }

    </script>
</asp:Content>
