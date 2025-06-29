using QhatuPUCPPresentacion.WebService;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QhatuPUCPPresentacion.PaginasAdministrador
{
    public partial class DenunciasAdmin : System.Web.UI.Page
    {
        protected DenunciaWSClient client;
        protected void Page_Init(object sender, EventArgs e)
        {
            client = new DenunciaWSClient();
            CargarDenuncias();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDenuncias();
            }
        }
        private void CargarDenuncias()
        {
            try
            {
                List<denuncia> denuncias = new List<denuncia>();

                denuncias = client.listarDenuncia().ToList();

                Console.WriteLine(denuncias.Count);

                ViewState["Denuncias"] = denuncias; // guardar para filtrado posterior

                rptDenuncias.DataSource = denuncias;
                rptDenuncias.DataBind();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar denuncias: " + ex.Message);
            }
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            lblError.Text = ""; // Limpiar mensajes anteriores

            // Validar fechas
            DateTime fechaInicio, fechaFin;
            bool tieneInicio = DateTime.TryParse(txtFechaInicio.Text, out fechaInicio);
            bool tieneFin = DateTime.TryParse(txtFechaFin.Text, out fechaFin);

            // Validar que fecha inicio no sea mayor que fecha fin
            if (tieneInicio && tieneFin && fechaInicio > fechaFin)
            {
                lblError.Text = "La fecha de inicio no puede ser mayor que la fecha de fin.";
                return;
            }

            // Validar que las fechas sean válidas si se ingresaron
            if ((tieneInicio && fechaInicio == DateTime.MinValue) ||
                (tieneFin && fechaFin == DateTime.MinValue))
            {
                lblError.Text = "Por favor ingrese fechas válidas.";
                return;
            }

            // Obtener criterio de búsqueda
            string criterio = txtBuscar.Text.Trim().ToLower();

            if (ViewState["Denuncias"] != null)
            {
                var denuncias = (List<denuncia>)ViewState["Denuncias"];

                // Aplicar filtros
                var query = denuncias.AsEnumerable();

                if (!string.IsNullOrEmpty(criterio))
                {
                    query = query.Where(d => d.motivo.ToLower().Contains(criterio));
                }

                if (tieneInicio)
                {
                    query = query.Where(d => d.fechaDenuncia.Date >= fechaInicio.Date);
                }

                if (tieneFin)
                {
                    query = query.Where(d => d.fechaDenuncia.Date <= fechaFin.Date);
                }

                var filtradas = query.ToList();

                // Mostrar resultados
                rptDenuncias.DataSource = filtradas;
                rptDenuncias.DataBind();

                if (filtradas.Count == 0)
                {
                    lblError.Text = "No se encontraron resultados con los criterios de búsqueda especificados.";
                }
            }
            else
            {
                lblError.Text = "No hay datos disponibles para filtrar.";
            }
        }

        // Manejar el evento de cambio de texto si es necesario
        protected void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            // Puedes llamar al mismo método de búsqueda
            btnBuscar_Click(sender, e);
        }
        protected void rptDenuncias_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Revisar")
            {
                administrador adminLogueado = Session["administrador"] as administrador;
                int idDenuncia = Convert.ToInt32(e.CommandArgument);
                System.Diagnostics.Debug.WriteLine($"Admin ID: {adminLogueado.idUsuario}, Denuncia ID: {idDenuncia}");
                try
                {
                    denuncia denuncia = client.obtenerDenuncia(idDenuncia);

                    if (denuncia == null)
                    {
                        MostrarError("Denuncia no encontrada");
                        return;
                    }
                    denuncia.admin = adminLogueado;
                    bool exito = client.actualizarDenuncia(denuncia);
                    if (exito)
                    {
                        CargarDenuncias();
                        MostrarExito("Denuncia marcada como revisada");
                    }
                    else
                    {
                        MostrarError("Error al actualizar la denuncia");
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
                    MostrarError($"Error del sistema: {ex.Message}");
                }
            }
            else if (e.CommandName == "Ocultar")
            {
                int idDenuncia = Convert.ToInt32(e.CommandArgument);
                client.eliminarDenuncia(idDenuncia); 
                CargarDenuncias();
            }

        }

        private void MostrarError(string mensaje)
        {
            lblError.Text = mensaje;
            lblError.CssClass = "text-danger fw-bold d-block mb-2";
            lblError.Visible = true;
        }

        private void MostrarExito(string mensaje)
        {
            lblError.Text = mensaje;
            lblError.CssClass = "text-success fw-bold d-block mb-2";
            lblError.Visible = true;
        }
        protected void btnDescargarReporte_Click(object sender, EventArgs e)
        {
            try
            {
                // Llamada al método reporteIncidencias
                byte[] reportePDF = client.reporteIncidencias();

                // Validación del contenido
                if (reportePDF != null && reportePDF.Length > 0)
                {
                    // Preparar respuesta para descargar el PDF
                    Response.Clear();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("Content-Disposition", "attachment; filename=ReporteIncidencias.pdf");
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