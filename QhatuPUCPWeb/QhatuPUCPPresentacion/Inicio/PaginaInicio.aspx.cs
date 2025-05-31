using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            List<Publicacion> publicaciones = ObtenerPublicacionesDeEjemplo();

            // Filtros aplicados
            if (ddlFacultad.SelectedIndex > 0)
                publicaciones = publicaciones.Where(p => p.Facultad == ddlFacultad.SelectedItem.Text).ToList();

            if (ddlEspecialidad.SelectedIndex > 0)
                publicaciones = publicaciones.Where(p => p.Especialidad == ddlEspecialidad.SelectedItem.Text).ToList();

            if (ddlCurso.SelectedIndex > 0)
                publicaciones = publicaciones.Where(p => p.Curso == ddlCurso.SelectedItem.Text).ToList();

            rptPublicaciones.DataSource = publicaciones;
            rptPublicaciones.DataBind();
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

        private List<Publicacion> ObtenerPublicacionesDeEjemplo()
        {
            return new List<Publicacion>
            {
                new Publicacion {
                    Titulo = "Se alquila calculadora para ingeniería económica",
                    ImagenUrl = "/Public/images/calculadora.jpg",
                    EsFavorito = true,
                    Facultad = "Ingeniería",
                    Especialidad = "Informática",
                    Curso = "Ingeniería Económica"
                },
                new Publicacion {
                    Titulo = "Libro de álgebra disponible",
                    ImagenUrl = "/Public/images/placeholder.jpg",
                    EsFavorito = false,
                    Facultad = "Ingeniería",
                    Especialidad = "Informática",
                    Curso = "Programación 3"
                },
                new Publicacion {
                    Titulo = "Material de economía social",
                    ImagenUrl = "/Public/images/placeholder.jpg",
                    EsFavorito = false,
                    Facultad = "Ciencias Sociales",
                    Especialidad = "Economía",
                    Curso = "Ingeniería Económica"
                },

                new Publicacion {
                    Titulo = "Se alquila calculadora para ingeniería económica",
                    ImagenUrl = "/Public/images/calculadora.jpg",
                    EsFavorito = true,
                    Facultad = "Ingeniería",
                    Especialidad = "Informática",
                    Curso = "Ingeniería Económica"
                },
                new Publicacion {
                    Titulo = "Libro de álgebra disponible",
                    ImagenUrl = "/Public/images/placeholder.jpg",
                    EsFavorito = false,
                    Facultad = "Ingeniería",
                    Especialidad = "Informática",
                    Curso = "Programación 3"
                },
                new Publicacion {
                    Titulo = "Material de economía social",
                    ImagenUrl = "/Public/images/placeholder.jpg",
                    EsFavorito = false,
                    Facultad = "Ciencias Sociales",
                    Especialidad = "Economía",
                    Curso = "Ingeniería Económica"
                },
                new Publicacion {
                    Titulo = "Se alquila calculadora para ingeniería económica",
                    ImagenUrl = "/Public/images/calculadora.jpg",
                    EsFavorito = true,
                    Facultad = "Ingeniería",
                    Especialidad = "Informática",
                    Curso = "Ingeniería Económica"
                },
                new Publicacion {
                    Titulo = "Libro de álgebra disponible",
                    ImagenUrl = "/Public/images/placeholder.jpg",
                    EsFavorito = false,
                    Facultad = "Ingeniería",
                    Especialidad = "Informática",
                    Curso = "Programación 3"
                },
                new Publicacion {
                    Titulo = "Material de economía social",
                    ImagenUrl = "/Public/images/placeholder.jpg",
                    EsFavorito = false,
                    Facultad = "Ciencias Sociales",
                    Especialidad = "Economía",
                    Curso = "Ingeniería Económica"
                }

            };
        }

        public class Publicacion
        {
            public string Titulo { get; set; }
            public string ImagenUrl { get; set; }
            public bool EsFavorito { get; set; }
            public string Facultad { get; set; }
            public string Especialidad { get; set; }
            public string Curso { get; set; }
        }
    }
}