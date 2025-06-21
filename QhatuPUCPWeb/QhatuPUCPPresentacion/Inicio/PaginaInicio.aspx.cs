using QhatuPUCPPresentacion.WebService;
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
        private int PaginaActual
        {
            get { return ViewState["PaginaActual"] != null ? (int)ViewState["PaginaActual"] : 0; }
            set { ViewState["PaginaActual"] = value; }
        }

        private const int TamanoPagina = 9;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarFiltros();
                PaginaActual = 0;
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
                usuario usuario = Session["usuario"] as usuario;
                if (usuario == null) return;

                List<publicacion> publicaciones = client.listarPublicacionConFavoritos(usuario.idUsuario)?.ToList() ?? new List<publicacion>();

                int idFacultad = int.Parse(ddlFacultad.SelectedValue);
                int idEspecialidad = int.Parse(ddlEspecialidad.SelectedValue);
                int idCurso = int.Parse(ddlCurso.SelectedValue);

                // Filtro por facultad
                if (idFacultad > 0)
                {
                    var publicacionesFacultad = client.listarPorFacultad(idFacultad) ?? new publicacion[0];
                    var idsFacultad = new HashSet<int>(publicacionesFacultad.Select(p => p.idPublicacion));

                    publicaciones = publicaciones.Where(p => idsFacultad.Contains(p.idPublicacion)).ToList();
                }

                // Filtro por especialidad
                if (idEspecialidad > 0 && publicaciones.Count > 0)
                {
                    var publicacionesEspecialidad = client.listarPorEspecialidad(idEspecialidad) ?? new publicacion[0];
                    var idsEspecialidad = new HashSet<int>(publicacionesEspecialidad.Select(p => p.idPublicacion));

                    publicaciones = publicaciones.Where(p => idsEspecialidad.Contains(p.idPublicacion)).ToList();
                }

                // Filtro por curso
                if (idCurso > 0 && publicaciones.Count > 0)
                {
                    var publicacionesCurso = client.listarPorCurso(idCurso) ?? new publicacion[0];
                    var idsCurso = new HashSet<int>(publicacionesCurso.Select(p => p.idPublicacion));

                    publicaciones = publicaciones.Where(p => idsCurso.Contains(p.idPublicacion)).ToList();
                }

                // Solo publicaciones visibles
                publicaciones = publicaciones.Where(p => p.estado == estadoPublicacion.VISIBLE).ToList();

                // Paginación
                PagedDataSource pagedData = new PagedDataSource
                {
                    DataSource = publicaciones,
                    AllowPaging = true,
                    PageSize = TamanoPagina,
                    CurrentPageIndex = PaginaActual
                };

                rptPublicaciones.DataSource = pagedData;
                rptPublicaciones.DataBind();

                GenerarPaginas(pagedData.PageCount);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar publicaciones: " + ex.Message);
            }
        }

        private class PaginaItem
        {
            public int Numero { get; set; }
            public string Texto { get; set; }
            public bool EsActual { get; set; }
        }

        private void GenerarPaginas(int totalPaginas)
        {
            List<PaginaItem> paginas = new List<PaginaItem>();

            if (PaginaActual > 0)
                paginas.Add(new PaginaItem { Numero = PaginaActual - 1, Texto = "‹ Anterior", EsActual = false });

            int inicio = Math.Max(0, PaginaActual - 2);
            int fin = Math.Min(totalPaginas - 1, PaginaActual + 2);

            if (inicio > 0)
            {
                paginas.Add(new PaginaItem { Numero = 0, Texto = "1", EsActual = false });
                if (inicio > 1)
                    paginas.Add(new PaginaItem { Numero = -1, Texto = "...", EsActual = false });
            }

            for (int i = inicio; i <= fin; i++)
            {
                paginas.Add(new PaginaItem
                {
                    Numero = i,
                    Texto = (i + 1).ToString(),
                    EsActual = (i == PaginaActual)
                });
            }

            if (fin < totalPaginas - 2)
                paginas.Add(new PaginaItem { Numero = -1, Texto = "...", EsActual = false });

            if (fin < totalPaginas - 1)
                paginas.Add(new PaginaItem { Numero = totalPaginas - 1, Texto = totalPaginas.ToString(), EsActual = false });

            if (PaginaActual < totalPaginas - 1)
                paginas.Add(new PaginaItem { Numero = PaginaActual + 1, Texto = "Siguiente ›", EsActual = false });

            rptPaginas.DataSource = paginas.Where(p => p.Numero >= 0).ToList();
            rptPaginas.DataBind();
        }

        protected void rptPaginas_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Page")
            {
                PaginaActual = int.Parse(e.CommandArgument.ToString());
                CargarPublicaciones();
            }
        }

        protected void ddlFacultad_SelectedIndexChanged(object sender, EventArgs e)
        {
            PaginaActual = 0;
            CargarPublicaciones();
        }

        protected void ddlEspecialidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            PaginaActual = 0;
            CargarPublicaciones();
        }

        protected void ddlCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            PaginaActual = 0;
            CargarPublicaciones();
        }

        protected void btnAnterior_Click(object sender, EventArgs e)
        {
            PaginaActual--;
            CargarPublicaciones();
        }

        protected void btnSiguiente_Click(object sender, EventArgs e)
        {
            PaginaActual++;
            CargarPublicaciones();
        }

        [System.Web.Services.WebMethod]
        public static object CambiarFavorito(int idPublicacion)
        {
            var usuario = HttpContext.Current.Session["usuario"] as usuario;
            if (usuario == null)
                return new { exito = false, mensaje = "Sesión expirada" };
            var publicacion = new PublicacionWSClient();
            bool esFavorito = publicacion.esFavorito(usuario.idUsuario, idPublicacion);
            try
            {
                if (esFavorito)
                    publicacion.eliminarFavoritos(usuario.idUsuario, idPublicacion);
                else
                    publicacion.agregarFavoritos(usuario.idUsuario, idPublicacion);
                return new { exito = true, nuevoEstado = !esFavorito };
            }
            catch (Exception ex)
            {
                return new { exito = false, mensaje = ex.Message };
            }
        }

    }
}