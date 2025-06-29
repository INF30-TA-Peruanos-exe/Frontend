using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using QhatuPUCPPresentacion.WebService;

namespace QhatuPUCPPresentacion.Filtros
{
    public partial class ListaEspecialidad : System.Web.UI.Page
    {
        protected EspecialidadWSClient client;

        private static int paginaActual = 1;
        private static int tamanoPagina = 10; 
        protected void Page_Init(object sender, EventArgs e)
        {
            client = new EspecialidadWSClient();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                paginaActual = 1;
                CargarEspecialidades();
            }
        }

        private void CargarEspecialidades()
        {
            try
            {
                List<especialidad> especialidades_todas = new List<especialidad>();

                especialidades_todas = client.listarEspecialidad().ToList();

                int totalEspecialidades = especialidades_todas.Count;

                var especialidades = especialidades_todas
                    .Skip((paginaActual - 1) * tamanoPagina) // Saltar las especialidades de las páginas anteriores
                    .Take(tamanoPagina) // Tomar el número de especialidades para la página actual
                    .ToList();

                ViewState["Especialidades"] = especialidades_todas; // guardar para filtrado posterior
                rptEspecialidades.DataSource = especialidades;
                rptEspecialidades.DataBind();

                lblPagina.Text = $"Página {paginaActual} de {Math.Ceiling((double)totalEspecialidades / tamanoPagina)}";

                btnAnterior.Enabled = paginaActual > 1; // Deshabilitar si es la primera página
                btnSiguiente.Enabled = (paginaActual * tamanoPagina) < totalEspecialidades; // Deshabilitar si es la última página
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar especialidades: " + ex.Message);
            }
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string criterio = txtBuscar.Text.Trim().ToLower();
            ViewState["CriterioBusqueda"] = criterio; // Guardar el criterio de búsqueda

            if (ViewState["Especialidades"] != null)
            {
                var especialidades = (List<especialidad>)ViewState["Especialidades"];
                var filtradas = especialidades
                    .Where(u => u.nombre.ToLower().Contains(criterio))
                    .ToList();

                ViewState["EspecialidadesFiltradas"] = filtradas; // Guardar las facultades filtradas para paginación
                paginaActual = 1; // Reiniciar la página actual al buscar

                CargarFiltradas();
            }
        }
        protected void CargarFiltradas()
        {
            var especialidades = ViewState["EspecialidadesFiltradas"] as List<especialidad> ??
                ViewState["Especialidades"] as List<especialidad>;

            int total = especialidades.Count;

            var pagina = especialidades
                .Skip((paginaActual - 1) * tamanoPagina)
                .Take(tamanoPagina)
                .ToList();

            rptEspecialidades.DataSource = pagina;
            rptEspecialidades.DataBind();

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
            Response.Redirect("/Filtros/NuevaEspecialidad.aspx");
        }
        protected void BtnEditar_Click(object sender, EventArgs e)
        {
            string id_especialidad_str = ((LinkButton)sender).CommandArgument.ToString();

            Response.Redirect("/Filtros/NuevaEspecialidad.aspx?id_especialidad=" + id_especialidad_str);
        }

        protected void BtnEliminar_Click(object sender, EventArgs e)
        {
            string argument = ((LinkButton)sender).CommandArgument.ToString();
            int id_especialidad = int.Parse(argument);
            client.eliminarEspecialidad(id_especialidad);
            CargarEspecialidades();
        }
    }
}