using QhatuPUCPPresentacion.WebService; // Asegúrate que este sea el nombre correcto de tu servicio
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QhatuPUCPPresentacion.Inicio
{
    public partial class PaginaInicio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarFiltros();
                CargarPublicaciones();
            }
        }

        private void CargarFiltros()
        {
            // Facultades
            ddlFacultad.Items.Clear();
            ddlFacultad.Items.Add("Facultad");
            ddlFacultad.Items.Add("Ingeniería");
            ddlFacultad.Items.Add("Ciencias Sociales");

            // Especialidades
            ddlEspecialidad.Items.Clear();
            ddlEspecialidad.Items.Add("Especialidad");
            ddlEspecialidad.Items.Add("Informática");
            ddlEspecialidad.Items.Add("Economía");

            // Cursos
            ddlCurso.Items.Clear();
            ddlCurso.Items.Add("Curso");
            ddlCurso.Items.Add("Programación 3");
            ddlCurso.Items.Add("Ingeniería Económica");
        }

        private void CargarPublicaciones()
        {
            try
            {
                PublicacionWSClient client = new PublicacionWSClient();
                publicacion[] publicacionesWS = client.listarPublicacion();

                List<publicacion> publicaciones = publicacionesWS.ToList();

                //// Aplicar filtros
                //if (ddlFacultad.SelectedIndex > 0)
                //    publicaciones = publicaciones.Where(p => p.publicacionesFacultades== ddlFacultad.SelectedItem.Text).ToList();

                //if (ddlEspecialidad.SelectedIndex > 0)
                //    publicaciones = publicaciones.Where(p => p.publicacionesEspecialidades == ddlEspecialidad.SelectedItem.Text).ToList();

                //if (ddlCurso.SelectedIndex > 0)
                //    publicaciones = publicaciones.Where(p => p.publicacionesCursos == ddlCurso.SelectedItem.Text).ToList();

                rptPublicaciones.DataSource = publicaciones;
                rptPublicaciones.DataBind();
            }
            catch (Exception ex)
            {
                // Puedes mostrar un mensaje de error o loguearlo
                Console.WriteLine("Error al cargar publicaciones: " + ex.Message);
            }
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
    }
}
