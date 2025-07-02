using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QhatuPUCPPresentacion.WebService;

namespace QhatuPUCPPresentacion.Filtros
{
    public partial class ListaCurso : System.Web.UI.Page
    {
        protected CursoWSClient client;
        private static int paginaActual = 1;
        private static int tamanoPagina = 10; // Número de cursos por página
        protected void Page_Init(object sender, EventArgs e)
        {
            client = new CursoWSClient();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                paginaActual = 1; // Reiniciar la página actual al cargar la página por primera vez
                CargarCursos();
            }
        }

        private void CargarCursos()
        {
            try
            {
                List<curso> cursos_todas = new List<curso>();

                cursos_todas = client.listarCurso().ToList();

                int totalCursos = cursos_todas.Count;

                var cursos = cursos_todas
                    .Skip((paginaActual - 1) * tamanoPagina) // Saltar los cursos de las páginas anteriores
                    .Take(tamanoPagina) // Tomar el número de cursos para la página actual
                    .ToList();  

                ViewState["Cursos"] = cursos_todas; // guardar para filtrado posterior
                rptCursos.DataSource = cursos;
                rptCursos.DataBind();

                lblPagina.Text = $"Página {paginaActual} de {Math.Ceiling((double)totalCursos / tamanoPagina)}";

                btnAnterior.Enabled = paginaActual > 1; // Deshabilitar si es la primera página
                btnSiguiente.Enabled = (paginaActual * tamanoPagina) < totalCursos; // Deshabilitar si es la última página
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar cursos: " + ex.Message);
            }
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string criterio = txtBuscar.Text.Trim().ToLower();
            ViewState["CriterioBusqueda"] = criterio; // Guardar el criterio de búsqueda

            if (ViewState["Cursos"] != null)
            {
                var cursos = (List<curso>)ViewState["Cursos"];
                var filtradas = cursos
                    .Where(u => u.nombre.ToLower().Contains(criterio))
                    .ToList();

                ViewState["CursosFiltradas"] = filtradas; // Guardar las facultades filtradas para paginación
                paginaActual = 1; // Reiniciar la página actual al buscar

                CargarFiltradas();
            }
        }
        protected void CargarFiltradas()
        {
            var cursos = ViewState["CursosFiltradas"] as List<curso> ??
                ViewState["Cursos"] as List<curso>;

            int total = cursos.Count;

            var pagina = cursos
                .Skip((paginaActual - 1) * tamanoPagina)
                .Take(tamanoPagina)
                .ToList();

            rptCursos.DataSource = pagina;
            rptCursos.DataBind();

            lblPagina.Text = $"Página {paginaActual} de {Math.Ceiling((double)total / tamanoPagina)}";
            btnAnterior.Enabled = paginaActual > 1;
            btnSiguiente.Enabled = (paginaActual * tamanoPagina) < total;
        }
        protected void btnAnterior_Click(object sender, EventArgs e)
        {
            if (paginaActual > 1)
            {
                paginaActual--;
                CargarFiltradas();
            }
        }

        protected void btnSiguiente_Click(object sender, EventArgs e)
        {
            paginaActual++;
            CargarFiltradas();
        }
        protected void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            btnBuscar_Click(sender, e); // o directamente filtrar aquí
        }
        protected void BtnAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect(ResolveUrl("~/Filtros/NuevoCurso.aspx"));
        }
        protected void BtnEditar_Click(object sender, EventArgs e)
        {
            string id_curso_str = ((LinkButton)sender).CommandArgument.ToString();

            Response.Redirect(ResolveUrl("~/Filtros/NuevoCurso.aspx?id_curso=" + id_curso_str));
        }

        protected void BtnEliminar_Click(object sender, EventArgs e)
        {
            string argument = ((LinkButton)sender).CommandArgument.ToString();
            int id_curso = int.Parse(argument);
            client.eliminarCurso(id_curso);
            CargarCursos();
        }
    }
}