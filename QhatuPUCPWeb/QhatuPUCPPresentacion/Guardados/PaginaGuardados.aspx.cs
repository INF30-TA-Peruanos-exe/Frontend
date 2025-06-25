using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QhatuPUCPPresentacion.WebService;

namespace QhatuPUCPPresentacion.Favoritos
{
    public partial class PaginaFavoritosNew : System.Web.UI.Page
    {
        /*Para manejar la paginación*/
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
                PaginaActual = 0;
            }
            CargarGuardados();
        }

        private void CargarGuardados()
        {
            try
            {
                PublicacionWSClient publicacionService = new PublicacionWSClient();
                usuario usuario = Session["usuario"] as usuario;
                if (usuario == null) return;

                List<publicacion> guardados = publicacionService.listarPublicacionConFavoritos(usuario.idUsuario)?.Where(p => p.esFavorito).ToList() ?? new List<publicacion>();


                // Solo publicaciones visibles
                guardados = guardados.Where(p => p.estado == estadoPublicacion.VISIBLE).ToList();

                // Paginación
                PagedDataSource pagedData = new PagedDataSource
                {
                    DataSource = guardados,
                    AllowPaging = true,
                    PageSize = TamanoPagina,
                    CurrentPageIndex = PaginaActual
                };

                rptPublicacionesGuardadas.DataSource = pagedData;
                rptPublicacionesGuardadas.DataBind();

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
                CargarGuardados();
            }
        }

        protected void btnAnterior_Click(object sender, EventArgs e)
        {
            PaginaActual--;
            CargarGuardados();
        }

        protected void btnSiguiente_Click(object sender, EventArgs e)
        {
            PaginaActual++;
            CargarGuardados();
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

        protected void btnActualizarFavoritos_Click(object sender, EventArgs e)
        {
            CargarGuardados();
            UpdatePanelFavoritos.Update();
        }
    }
}