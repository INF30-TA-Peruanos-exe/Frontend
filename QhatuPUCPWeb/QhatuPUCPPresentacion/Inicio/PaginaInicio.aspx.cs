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
            // cargar cursos
            CursoWSClient cursoClient = new CursoWSClient();
            curso[] cursos = cursoClient.listarCurso();
            ddlCurso.Items.Clear();
            ddlCurso.Items.Add(new ListItem("Curso", "0"));
            foreach (curso c in cursos)
            {
                ddlCurso.Items.Add(new ListItem(c.nombre, c.idCurso.ToString()));
            }

            // cargar facultades
            FacultadWSClient facultadClient = new FacultadWSClient();
            facultad[] facultades = facultadClient.listarFacultad();
            ddlFacultad.Items.Clear();
            ddlFacultad.Items.Add(new ListItem("Facultad", "0"));
            foreach (facultad f in facultades)
            {
                ddlFacultad.Items.Add(new ListItem(f.nombre, f.idFacultad.ToString()));
            }

            // cargar especialidades
            EspecialidadWSClient especialidadClient = new EspecialidadWSClient();
            especialidad[] especialidades = especialidadClient.listarEspecialidad();
            ddlEspecialidad.Items.Clear();
            ddlEspecialidad.Items.Add(new ListItem("Especialidad", "0"));
            foreach (especialidad e in especialidades)
            {
                ddlEspecialidad.Items.Add(new ListItem(e.nombre, e.idEspecialidad.ToString()));
            }
        }

        private void CargarPublicaciones()
        {
            try
            {
                PublicacionWSClient client = new PublicacionWSClient();
                List<publicacion> publicaciones = new List<publicacion>();

                // Lógica de filtros: prioridad Facultad > Especialidad > Curso
                if (ddlFacultad.SelectedIndex > 0)
                {
                    int idFacultad = int.Parse(ddlFacultad.SelectedValue);
                    publicaciones = client.listarPorFacultad(idFacultad).ToList();
                }
                else if (ddlEspecialidad.SelectedIndex > 0)
                {
                    int idEspecialidad = int.Parse(ddlEspecialidad.SelectedValue);
                    publicaciones = client.listarPorEspecialidad(idEspecialidad).ToList();
                }
                else if (ddlCurso.SelectedIndex > 0)
                {
                    int idCurso = int.Parse(ddlCurso.SelectedValue);
                    publicaciones = client.listarPorCurso(idCurso).ToList();
                }
                else
                {
                    // Si no hay filtro, cargar todas
                    publicaciones = client.listarPublicacion().ToList();
                }

                rptPublicaciones.DataSource = publicaciones;
                rptPublicaciones.DataBind();
            }
            catch (Exception ex)
            {
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
