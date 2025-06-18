using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QhatuPUCPPresentacion.WebService;

namespace QhatuPUCPPresentacion.Inicio
{
    public partial class PaginaInicioAdmin : System.Web.UI.Page
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

        protected void rptPublicaciones_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                PublicacionWSClient client = new PublicacionWSClient();
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

        protected void BtnEliminar_Click(object sender, EventArgs e)
        {
            PublicacionWSClient client = new PublicacionWSClient();
            string argument = ((LinkButton)sender).CommandArgument.ToString();
            int id_publicacion = int.Parse(argument);
            client.eliminarPublicacion(id_publicacion);
            CargarPublicaciones();
        }
    }
    /*
    public partial class PaginaInicio : System.Web.UI.Page
    {
        protected UsuarioWSClient usuarioService;
        protected PublicacionWSClient publicacionService;
        protected FacultadWSClient facultadService;
        protected CursoWSClient cursoService;
        protected EspecialidadWSClient especialidadService;

        protected void Page_Init(object sender, EventArgs e)
        {
            usuarioService = new UsuarioWSClient();
            publicacionService = new PublicacionWSClient();
            facultadService = new FacultadWSClient();
            cursoService = new CursoWSClient();
            especialidadService = new EspecialidadWSClient();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarFacultades();
                CargarEspecialidades();
                CargarCursos();
                CargarPublicaciones();
            }
        }

        private void CargarFacultades()
        {
            ddlFacultad.DataSource = facultadService.listarFacultad();
            ddlFacultad.DataTextField = "nombre";
            ddlFacultad.DataValueField = "idFacultad";
            ddlFacultad.DataBind();
            ddlFacultad.Items.Insert(0, new ListItem("[Todas]", "0"));
        }

        private void CargarEspecialidades()
        {
            ddlEspecialidad.DataSource = especialidadService.listarEspecialidad();
            ddlEspecialidad.DataTextField = "nombre";
            ddlEspecialidad.DataValueField = "idEspecialidad";
            ddlEspecialidad.DataBind();
            ddlEspecialidad.Items.Insert(0, new ListItem("[Todas]", "0"));
        }

        private void CargarCursos()
        {
            ddlCurso.DataSource = cursoService.listarCurso();
            ddlCurso.DataTextField = "nombre";
            ddlCurso.DataValueField = "idCurso";
            ddlCurso.DataBind();
            ddlCurso.Items.Insert(0, new ListItem("[Todos]", "0"));
        }

        private void CargarPublicaciones()
        {
            int idUsuario = ObtenerIdUsuarioSesion();
            var publicaciones = publicacionService.listarPublicacionConFavoritos(idUsuario);
            rptPublicaciones.DataSource = publicaciones;
            rptPublicaciones.DataBind();
            ViewState["Publicaciones"] = publicaciones;
        }

        private void BuscarPublicacionesPorFiltros()
        {
            int idUsuario = ObtenerIdUsuarioSesion();
            var publicaciones = publicacionService.listarPublicacionConFavoritos(idUsuario);

            int idFacultad = int.Parse(ddlFacultad.SelectedValue);
            int idEspecialidad = int.Parse(ddlEspecialidad.SelectedValue);
            int idCurso = int.Parse(ddlCurso.SelectedValue);

            var resultado = publicaciones;

            if (idFacultad > 0)
                resultado = resultado.Where(p => p.publicacionesFacultades != null &&
                                                 p.publicacionesFacultades.Any(f => f.idFacultad == idFacultad)).ToArray();

            if (idEspecialidad > 0)
                resultado = resultado.Where(p => p.publicacionesEspecialidades != null &&
                                                 p.publicacionesEspecialidades.Any(e => e.idEspecialidad == idEspecialidad)).ToArray();

            if (idCurso > 0)
                resultado = resultado.Where(p => p.publicacionesCursos != null &&
                                                 p.publicacionesCursos.Any(c => c.idCurso == idCurso)).ToArray();

            rptPublicaciones.DataSource = resultado;
            rptPublicaciones.DataBind();
            ViewState["Publicaciones"] = resultado;
        }

        protected void ddlFacultad_SelectedIndexChanged(object sender, EventArgs e) => BuscarPublicacionesPorFiltros();
        protected void ddlEspecialidad_SelectedIndexChanged(object sender, EventArgs e) => BuscarPublicacionesPorFiltros();
        protected void ddlCurso_SelectedIndexChanged(object sender, EventArgs e) => BuscarPublicacionesPorFiltros();

        protected void rptPublicaciones_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int idPublicacion = Convert.ToInt32(e.CommandArgument);
            usuario usuarioSesion = Session["usuario"] as usuario;

            if (usuarioSesion == null)
            {
                Response.Redirect("~/Inicio/Login.aspx");
                return;
            }

            if (e.CommandName == "VerDetalle")
            {
                Response.Redirect($"DetallePublicacion.aspx?id={idPublicacion}");
            }
            else if (e.CommandName == "Favorito")
            {
                var publicaciones = (publicacion[])ViewState["Publicaciones"];
                var pub = Array.Find(publicaciones, p => p.idPublicacion == idPublicacion);

                if (pub != null)
                {
                    if (pub.esFavorito)
                        publicacionService.eliminarFavoritos(usuarioSesion.idUsuario, idPublicacion);
                    else
                        publicacionService.agregarFavoritos(usuarioSesion.idUsuario, idPublicacion);
                }

                BuscarPublicacionesPorFiltros();
            }
        }

        private int ObtenerIdUsuarioSesion()
        {
            var usuario = Session["usuario"] as usuario;
            return usuario != null ? usuario.idUsuario : 0;
        }
    }*/
}