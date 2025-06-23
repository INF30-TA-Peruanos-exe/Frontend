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
                CargarFiltros();
                CargarPublicaciones();
            }
        }

        private void CargarFiltros()
        {
            // cargar cursos
            curso[] cursos = cursoClient.listarCurso();
            ddlCurso.Items.Clear();
            ddlCurso.Items.Add(new ListItem("Curso", "0"));
            foreach (curso c in cursos)
            {
                ddlCurso.Items.Add(new ListItem(c.nombre, c.idCurso.ToString()));
            }

            // cargar facultades
            facultad[] facultades = facultadClient.listarFacultad();
            ddlFacultad.Items.Clear();
            ddlFacultad.Items.Add(new ListItem("Facultad", "0"));
            foreach (facultad f in facultades)
            {
                ddlFacultad.Items.Add(new ListItem(f.nombre, f.idFacultad.ToString()));
            }

            // cargar especialidades
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

        protected void rptPublicaciones_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                // Lógica para eliminar publicación
                client.eliminarPublicacion(id);
                // Volver a cargar las publicaciones actualizadas
                CargarPublicaciones();
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