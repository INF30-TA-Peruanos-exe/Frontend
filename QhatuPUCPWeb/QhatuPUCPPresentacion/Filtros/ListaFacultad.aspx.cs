using QhatuPUCPPresentacion.WebService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QhatuPUCPPresentacion.Filtros
{
    public partial class ListaFacultad : System.Web.UI.Page
    {
        protected FacultadWSClient client;
        private static int paginaActual = 1;
        private static int tamanoPagina = 10; // Número de facultades por página
        protected void Page_Init(object sender, EventArgs e)
        {
            client = new FacultadWSClient();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                paginaActual = 1; // Reiniciar la página actual al cargar la página por primera vez
                CargarFacultades();
            }
        }

        private void CargarFacultades()
        {
            try
            {
                List<facultad> facultades_todas = new List<facultad>();

                facultades_todas = client.listarFacultad().ToList();

                int totalFacultades = facultades_todas.Count;

                var facultades = facultades_todas
                    .Skip((paginaActual - 1) * tamanoPagina) // Saltar las facultades de las páginas anteriores
                    .Take(tamanoPagina) // Tomar el número de facultades para la página actual
                    .ToList();

                ViewState["Facultades"] = facultades_todas; // guardar para filtrado posterior
                rptFacultades.DataSource = facultades;
                rptFacultades.DataBind();

                lblPagina.Text = $"Página {paginaActual} de {Math.Ceiling((double)totalFacultades / tamanoPagina)}";

                btnAnterior.Enabled = paginaActual > 1; // Deshabilitar si es la primera página
                btnSiguiente.Enabled = (paginaActual * tamanoPagina) < totalFacultades; // Deshabilitar si es la última página
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar facultades: " + ex.Message);
            }
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string criterio = txtBuscar.Text.Trim().ToLower();
            ViewState["CriterioBusqueda"] = criterio; // Guardar el criterio de búsqueda

            if (ViewState["Facultades"] != null)
            {
                var facultades = (List<facultad>)ViewState["Facultades"];
                var filtradas = facultades
                    .Where(u => u.nombre.ToLower().Contains(criterio))
                    .ToList();

                ViewState["FacultadesFiltradas"] = filtradas; // Guardar las facultades filtradas para paginación
                paginaActual = 1; // Reiniciar la página actual al buscar

                CargarFiltradas();
            }
        }
        protected void CargarFiltradas()
        {
            var facultades = ViewState["FacultadesFiltradas"] as List<facultad> ??
                ViewState["Facultades"] as List<facultad>;

            int total = facultades.Count;

            var pagina = facultades
                .Skip((paginaActual - 1) * tamanoPagina)
                .Take(tamanoPagina)
                .ToList();

            rptFacultades.DataSource = pagina;
            rptFacultades.DataBind();

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
            Response.Redirect(ResolveUrl("~/Filtros/NuevaFacultad.aspx"));
        }
        protected void BtnEditar_Click(object sender, EventArgs e)
        {
            string id_facultad_str = ((LinkButton)sender).CommandArgument.ToString();

            Response.Redirect(ResolveUrl("~/Filtros/NuevaFacultad.aspx?id_facultad=" + id_facultad_str));
        }

        protected void BtnEliminar_Click(object sender, EventArgs e)
        {
            string argument = ((LinkButton)sender).CommandArgument.ToString();
            int id_facultad = int.Parse(argument);
            client.eliminarFacultad(id_facultad);
            CargarFacultades();
        }
    }
}