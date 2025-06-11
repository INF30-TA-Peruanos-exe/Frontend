using QhatuPUCPPresentacion.WebService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QhatuPUCPPresentacion.Publicacion
{
    public partial class DetallePublicacion : Page
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
                    Response.Redirect("~/Inicio/PaginaInicio.aspx");
                }
            }
        }

        private void CargarPublicacion(int id)
        {
            var pub = publicacionService.obtenerPublicacion(id);

            lblTitulo.Text = pub.titulo;
            lblDescripcion.Text = pub.descripcion;
            lblAutor.Text = pub.usuario.nombre;
            lblTiempo.Text = publicacionService.obtenerFechaPublicacionFormateada(id);
            imgPublicacion.ImageUrl = pub.rutaImagen;
            imgAvatar.ImageUrl = "/Public/images/user-avatar.png";
        }

        private void CargarComentarios(int idPublicacion)
        {
            var todos = comentarioService.listarComentario();

            // Validación: evitar errores si el servicio devuelve null
            if (todos == null)
            {
                rptComentarios.DataSource = new List<object>();
                rptComentarios.DataBind();
                return;
            }

            var comentarios = todos
                .Where(c => c.publicacion != null && c.publicacion.idPublicacion == idPublicacion && c.activo)
                .Select(c => new
                {
                    Autor = c.comentador?.nombre ?? "Usuario",
                    AvatarUrl = "/Public/images/user-avatar.png",
                    Fecha = c.fecha.ToString("dd/MM/yyyy") ?? "",
                    Contenido = c.contenido,
                    Valoracion = c.valoracion
                })
                .ToList();

            rptComentarios.DataSource = comentarios;
            rptComentarios.DataBind();
        }



        protected void btnComentar_Click(object sender, EventArgs e)
        {
            var usuario = Session["usuario"] as usuario;
            if (usuario == null)
            {
                Response.Redirect("~/Inicio/Login.aspx");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtComentario.Text) || string.IsNullOrWhiteSpace(hfValoracion.Value))
            {
                // Validación mínima (puedes mostrar mensaje si deseas)
                return;
            }

            var comentario = new comentario
            {
                contenido = txtComentario.Text.Trim(),
                valoracion = int.Parse(hfValoracion.Value),
                comentador = usuario,
                activo = true,
                publicacion = new publicacion
                {
                    idPublicacion = int.Parse(hfIdPublicacion.Value)
                }
            };

            comentarioService.registrarComentario(comentario);

            // Limpiar campos
            txtComentario.Text = "";
            hfValoracion.Value = "";

            // Recargar comentarios
            CargarComentarios(comentario.publicacion.idPublicacion);
        }
    }
}