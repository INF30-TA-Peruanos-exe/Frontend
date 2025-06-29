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
                CargarPublicaciones();
            }
        }

        private void CargarPublicaciones()
        {
            try
            {
                List<publicacion> publicaciones = new List<publicacion>();

                publicaciones = client.listarPublicacion().ToList();


                ViewState["Publicaciones"] = publicaciones; // guardar para filtrado posterior
                rptPublicaciones.DataSource = publicaciones;
                rptPublicaciones.DataBind();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar publicaciones: " + ex.Message);
            }
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string criterio = txtBuscar.Text.Trim().ToLower();

            if (ViewState["Publicaciones"] != null)
            {
                var publicaciones = (List<publicacion>)ViewState["Publicaciones"];
                var filtradas = publicaciones
                    .Where(p => p.titulo.ToLower().Contains(criterio))
                    .ToList();

                rptPublicaciones.DataSource = filtradas;
                rptPublicaciones.DataBind();
            }
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