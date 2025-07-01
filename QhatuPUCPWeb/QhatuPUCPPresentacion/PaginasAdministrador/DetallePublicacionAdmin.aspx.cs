using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QhatuPUCPPresentacion.WebService;

namespace QhatuPUCPPresentacion.PaginasAdministrador
{
    public partial class DetallePublicacionAdmin : System.Web.UI.Page
    {
        protected PublicacionWSClient publicacionService;
        protected ComentarioWSClient comentarioService;

        protected void Page_Init(object sender, EventArgs e)
        {
            publicacionService = new PublicacionWSClient();
            comentarioService = new ComentarioWSClient();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int idPublicacion;
                if (Request.QueryString["id"] != null && int.TryParse(Request.QueryString["id"], out idPublicacion))
                {
                    hfIdPublicacion.Value = idPublicacion.ToString();
                    CargarPublicacion(idPublicacion);
                    CargarComentarios(idPublicacion);
                }
                else
                {
                    Response.Redirect("~/Inicio/PaginaInicioAdmin.aspx");
                }
            }
        }

        private void CargarPublicacion(int id)
        {
            var pub = publicacionService.obtenerPublicacion(id);
            var usuario = Session["usuario"] as usuario;
            string nombreArchivo = pub.rutaImagen;

            lblTitulo.Text = pub.titulo;
            lblDescripcion.Text = pub.descripcion;
            lblAutor.Text = pub.usuario.nombre;
            lblTiempo.Text = publicacionService.obtenerFechaPublicacionFormateada(id);
            imgPublicacion.ImageUrl = ResolveUrl("~/Imagenes/" + nombreArchivo);

        }

        private void CargarComentarios(int idPublicacion)
        {
            var usuario = Session["usuario"] as usuario;
            var comentarios = comentarioService.listarComentarioPorPublicacion(idPublicacion);

            if (comentarios == null)
            {
                rptComentarios.DataSource = new List<object>();
                rptComentarios.DataBind();
                return;
            }

            var comentariosProcesados = comentarios
                .Select(c => new
                {
                    IdComentario = c.idComentario,
                    Autor = c.comentador?.nombre ?? "Usuario",
                    Fecha = c.fecha.ToString("dd/MM/yyyy"),
                    Contenido = c.contenido,
                    Valoracion = c.valoracion,
                    EsPropio = usuario != null && c.comentador != null && c.comentador.idUsuario == usuario.idUsuario
                })
                .ToList();

            rptComentarios.DataSource = comentariosProcesados;
            rptComentarios.DataBind();
        }

        protected void rptComentarios_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                int idComentario = int.Parse(e.CommandArgument.ToString());
                comentarioService.eliminarComentario(idComentario);
                int idPublicacion = int.Parse(hfIdPublicacion.Value);
                CargarComentarios(idPublicacion);
            }
        }

        protected void BtnEliminar_Click(object sender, EventArgs e)
        {
            int idPublicacion = int.Parse(hfIdPublicacion.Value);
            string argument = ((LinkButton)sender).CommandArgument.ToString();
            int id_comentario = int.Parse(argument);
            comentarioService.eliminarComentario(id_comentario);
            CargarComentarios(idPublicacion);
        }
    }
}