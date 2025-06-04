using QhatuPUCPPresentacion.WebService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QhatuPUCPPresentacion.Publicacion
{
    public partial class DetallePublicacion : System.Web.UI.Page
    {
        private PublicacionWSClient publicacionService = new PublicacionWSClient();
        private ComentarioWSClient comentarioService = new ComentarioWSClient();
        private DenunciaWSClient denunciaService = new DenunciaWSClient();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int idPublicacion = ObtenerIdPublicacionDesdeQueryString();
                CargarDetallePublicacion(idPublicacion);
                CargarComentarios(idPublicacion);
            }
        }

        private int ObtenerIdPublicacionDesdeQueryString()
        {
            return int.TryParse(Request.QueryString["id"], out int id) ? id : 0;
        }

        private void CargarDetallePublicacion(int idPublicacion)
        {
            var pub = publicacionService.obtenerPublicacion(idPublicacion);
            var usuario = pub.usuario;

            lblTitulo.Text = pub.titulo;
            lblDescripcion.Text = pub.descripcion;
            lblAutor.Text = usuario.nombre;
            imgAvatar.ImageUrl = "/Public/images/user-avatar.png";
            imgPublicacion.ImageUrl = pub.rutaImagen;
        }

        private void CargarComentarios(int idPublicacion)
        {
            var usuarioSesion = Session["usuario"] as usuario;
            var lista = comentarioService.listarComentario();

            var comentariosFiltrados = lista
                .Where(c => c.publicacion != null && c.publicacion.idPublicacion == idPublicacion)
                .Select(c => new
                {
                    IdComentario = c.idComentario,
                    Autor = c.usuario?.nombre ?? "",
                    AvatarUrl = "/Public/images/user-avatar.png",
                    Fecha = c.fechaComentario.ToString("dd/MM/yyyy"),
                    Contenido = c.contenido,
                    Valoracion = c.valoracion,
                    EsPropio = usuarioSesion != null && c.usuario?.idUsuario == usuarioSesion.idUsuario
                }).ToList();

            rptComentarios.DataSource = comentariosFiltrados;
            rptComentarios.DataBind();
        }

        protected void btnFavorito_Click(object sender, EventArgs e)
        {
            var usuarioSesion = Session["usuario"] as usuario;
            int idPublicacion = ObtenerIdPublicacionDesdeQueryString();

            if (usuarioSesion == null) return;

            if (publicacionService.esFavorito(usuarioSesion.idUsuario, idPublicacion))
                publicacionService.eliminarFavoritos(usuarioSesion.idUsuario, idPublicacion);
            else
                publicacionService.agregarFavoritos(usuarioSesion.idUsuario, idPublicacion);

            CargarDetallePublicacion(idPublicacion);
        }

        protected void btnComentar_Click(object sender, EventArgs e)
        {
            var usuarioSesion = Session["usuario"] as usuario;
            int idPublicacion = ObtenerIdPublicacionDesdeQueryString();

            if (usuarioSesion == null || string.IsNullOrWhiteSpace(txtComentario.Text)) return;

            var comentario = new comentario()
            {
                contenido = txtComentario.Text.Trim(),
                valoracion = int.Parse(hfValoracion.Value),
                usuario = usuarioSesion,
                publicacion = new publicacion { idPublicacion = idPublicacion },
                fechaComentario = DateTime.Now
            };

            comentarioService.registrarComentario(comentario);
            txtComentario.Text = "";
            hfValoracion.Value = "0";

            CargarComentarios(idPublicacion);
        }

        protected void rptComentarios_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int idComentario = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Eliminar")
            {
                comentarioService.eliminarComentario(idComentario);
                CargarComentarios(ObtenerIdPublicacionDesdeQueryString());
            }
            else if (e.CommandName == "Denunciar")
            {
                hfComentarioIdDenuncia.Value = idComentario.ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "mostrarModal", "mostrarModalDenuncia();", true);
            }
        }

        protected void btnEnviarDenuncia_Click(object sender, EventArgs e)
        {
            var usuarioSesion = Session["usuario"] as usuario;
            if (usuarioSesion == null || string.IsNullOrWhiteSpace(txtMotivoDenuncia.Text)) return;

            int idComentario;
            if (int.TryParse(hfComentarioIdDenuncia.Value, out idComentario))
            {
                DenunciarComentario(idComentario, txtMotivoDenuncia.Text.Trim());
            }
            else
            {
                var denuncia = new denuncia
                {
                    autor = new publicacion { idPublicacion = ObtenerIdPublicacionDesdeQueryString() },
                    denunciante = usuarioSesion,
                    motivo = txtMotivoDenuncia.Text.Trim(),
                    activo = true
                };

                denunciaService.registrarDenuncia(denuncia);
                ScriptManager.RegisterStartupScript(this, GetType(), "alerta", "alert('Denuncia enviada correctamente');", true);
            }

            hfComentarioIdDenuncia.Value = "";
            txtMotivoDenuncia.Text = "";
        }

        protected void DenunciarComentario(int idComentario, string motivo)
        {
            var usuarioSesion = Session["usuario"] as usuario;
            if (usuarioSesion == null || string.IsNullOrWhiteSpace(motivo)) return;

            var comentario = comentarioService.obtenerComentario(idComentario);
            if (comentario == null || comentario.publicacion == null) return;

            var denuncia = new denuncia
            {
                autor = new publicacion { idPublicacion = comentario.publicacion.idPublicacion },
                denunciante = usuarioSesion,
                motivo = $"Comentario ID {idComentario}: {motivo}",
                activo = true
            };

            denunciaService.registrarDenuncia(denuncia);
            ScriptManager.RegisterStartupScript(this, GetType(), "denunciaOk", "alert('Comentario denunciado.');", true);
        }
    }
}
