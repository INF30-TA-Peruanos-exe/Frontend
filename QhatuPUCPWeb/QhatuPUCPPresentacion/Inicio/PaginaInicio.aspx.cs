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

                List<publicacion> publicaciones = client.listarPublicacion()?.ToList() ?? new List<publicacion>();

                int idFacultad = int.Parse(ddlFacultad.SelectedValue);
                int idEspecialidad = int.Parse(ddlEspecialidad.SelectedValue);
                int idCurso = int.Parse(ddlCurso.SelectedValue);

                //filtro de publicacion-facultad
                if (idFacultad > 0)
                {
                    var publicacionesFacultad = client.listarPorFacultad(idFacultad) ?? new publicacion[0]; //si devuelve vacio se reemplaza por un arreglo vacio
                    var idsFacultad = new HashSet<int>(publicacionesFacultad.Select(p => p.idPublicacion));

                    if (idsFacultad.Count == 0)
                        publicaciones.Clear();
                    else
                        publicaciones = publicaciones.Where(p => idsFacultad.Contains(p.idPublicacion)).ToList();
                }

                //filtro de publicacion-especialidad
                if (idEspecialidad > 0 && publicaciones.Count > 0)
                {
                    var publicacionesEspecialidad = client.listarPorEspecialidad(idEspecialidad) ?? new publicacion[0];
                    var idsEspecialidad = new HashSet<int>(publicacionesEspecialidad.Select(p => p.idPublicacion));

                    if (idsEspecialidad.Count == 0)
                        publicaciones.Clear();
                    else
                        publicaciones = publicaciones.Where(p => idsEspecialidad.Contains(p.idPublicacion)).ToList();
                }

                //filtro de publicacion-curso
                if (idCurso > 0 && publicaciones.Count > 0)
                {
                    var publicacionesCurso = client.listarPorCurso(idCurso) ?? new publicacion[0];
                    var idsCurso = new HashSet<int>(publicacionesCurso.Select(p => p.idPublicacion));

                    if (idsCurso.Count == 0)
                        publicaciones.Clear();
                    else
                        publicaciones = publicaciones.Where(p => idsCurso.Contains(p.idPublicacion)).ToList();
                }

                // se muestra solo las publicaciones que tengan estado visible
                publicaciones = publicaciones.Where(p => p.estado == estadoPublicacion.VISIBLE).ToList();

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
        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            int id_objeto = Int32.Parse(((LinkButton)sender).CommandArgument);

            PublicacionWSClient client = new PublicacionWSClient();

            publicacion publicacion = client.obtenerPublicacion(id_objeto);
            usuario usuario = Session["usuario"] as usuario;
            int id_user = publicacion.usuario.idUsuario;
            if (!client.esFavorito(usuario.idUsuario, id_objeto))
            {
                client.agregarFavoritos(usuario.idUsuario, id_objeto);
            }
            CargarPublicaciones();
            //Response.Redirect("/Usuario/EditarUsuario.aspx?id_usuario=" + id_usuario_str);
        }

    }
}
