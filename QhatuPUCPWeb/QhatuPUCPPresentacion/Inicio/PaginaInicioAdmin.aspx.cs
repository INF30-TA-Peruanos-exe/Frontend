using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using QhatuPUCPPresentacion.WebService;

namespace QhatuPUCPPresentacion.Inicio
{
    public partial class PaginaInicioAdmin : System.Web.UI.Page
    {
        CursoWSClient cursoClient;
        FacultadWSClient facultadClient;
        EspecialidadWSClient especialidadClient;
        PublicacionWSClient client;

        private static int paginaActual = 1;
        private static int tamanoPagina = 10; // Número de publicaciones por página

        protected void Page_Init(object sender, EventArgs e)
        {
            cursoClient = new CursoWSClient();
            facultadClient = new FacultadWSClient();
            client = new PublicacionWSClient();
            especialidadClient = new EspecialidadWSClient();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                paginaActual = 1; // Reiniciar la página actual al cargar la página por primera vez
                CargarPublicaciones();
            }
        }

        private void CargarPublicaciones()
        {
            try
            {
                List<publicacion> todas_publicaciones = new List<publicacion>();

                todas_publicaciones = client.listarPublicacion().ToList();

                int totalPublicaciones = todas_publicaciones.Count;

                var publicacionesPagina = todas_publicaciones
                    .Skip((paginaActual - 1) * tamanoPagina) // Saltar las publicaciones de las páginas anteriores
                    .Take(tamanoPagina) // Tomar el número de publicaciones para la página actual
                    .ToList();

                ViewState["Publicaciones"] = todas_publicaciones; // guardar para filtrado posterior
                rptPublicaciones.DataSource = publicacionesPagina;
                rptPublicaciones.DataBind();

                lblPagina.Text = $"Página {paginaActual} de {Math.Ceiling((double)todas_publicaciones.Count / tamanoPagina)}";

                btnAnterior.Enabled = paginaActual > 1; // Deshabilitar si es la primera página
                btnSiguiente.Enabled = (paginaActual * tamanoPagina) < todas_publicaciones.Count; // Deshabilitar si es la última página

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar publicaciones: " + ex.Message);
            }
        }
        protected void btnAnterior_Click(object sender, EventArgs e)
        {
            if (paginaActual > 1)
            {
                paginaActual--;
                CargarPublicacionesFiltradas();
            }
        }

        protected void btnSiguiente_Click(object sender, EventArgs e)
        {
            paginaActual++;
            CargarPublicacionesFiltradas();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string criterio = txtBuscar.Text.Trim().ToLower();
            ViewState["FiltroTitulo"] = criterio;

            if (ViewState["Publicaciones"] != null)
            {
                var publicaciones = (List<publicacion>)ViewState["Publicaciones"];
                var filtradas = publicaciones
                    .Where(p => p.titulo.ToLower().Contains(criterio))
                    .ToList();

                ViewState["PublicacionesFiltradas"] = filtradas;
                paginaActual = 1;
                CargarPublicacionesFiltradas();
            }
        }
        protected void CargarPublicacionesFiltradas()
        {
            var publicaciones = ViewState["PublicacionesFiltradas"] as List<publicacion> ??
                ViewState["Publicaciones"] as List<publicacion>;

            int total = publicaciones.Count;

            var pagina = publicaciones
                .Skip((paginaActual - 1) * tamanoPagina)
                .Take(tamanoPagina)
                .ToList();

            rptPublicaciones.DataSource = pagina;
            rptPublicaciones.DataBind();

            lblPagina.Text = $"Página {paginaActual} de {Math.Ceiling((double)total / tamanoPagina)}";
            btnAnterior.Enabled = paginaActual > 1;
            btnSiguiente.Enabled = (paginaActual * tamanoPagina) < total;
        }
        protected void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            btnBuscar_Click(sender, e); // o directamente filtrar aquí
        }
        protected void BtnEliminar_Click(object sender, EventArgs e)
        {
            string argument = ((LinkButton)sender).CommandArgument.ToString();
            int id_publi = int.Parse(argument);
            client.eliminarPublicacion(id_publi);
            CargarPublicaciones();
        }
        protected void ddlFacultad_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarPublicaciones();
        }

        protected void ddlEspecialidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarPublicaciones();
        }

        protected void ddlCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarPublicaciones();
        }

        protected void btnDescargarReporte_Click(object sender, EventArgs e)
        {
            try
            {
                // Llamada al método reporteIncidencias
                byte[] reportePDF = client.reportePublicaciones();

                // Validación del contenido
                if (reportePDF != null && reportePDF.Length > 0)
                {
                    // Preparar respuesta para descargar el PDF
                    Response.Clear();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("Content-Disposition", "attachment; filename=Top_Publicaciones.pdf");
                    Response.OutputStream.Write(reportePDF, 0, reportePDF.Length);
                    Response.Flush();
                    Response.End();
                }
                else
                {
                    // Mensaje de error si viene vacío
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('El reporte está vacío o no se pudo generar.');", true);
                }
            }
            catch (Exception ex)
            {
                // Mensaje de error
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Error al descargar el reporte: " + ex.Message.Replace("'", "") + "');", true);
            }
        }
    }
    }