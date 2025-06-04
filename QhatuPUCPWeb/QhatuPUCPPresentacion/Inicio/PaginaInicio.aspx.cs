using QhatuPUCPPresentacion.WebService; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QhatuPUCPPresentacion.Inicio
{
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
    }
}
