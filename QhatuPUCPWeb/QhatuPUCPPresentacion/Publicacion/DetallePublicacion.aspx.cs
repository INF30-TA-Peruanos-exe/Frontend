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
            var usuario = Session["usuario"] as usuario;

            lblTitulo.Text = pub.titulo;
            lblDescripcion.Text = pub.descripcion;
            lblAutor.Text = pub.usuario.nombreUsuario;
            lblTiempo.Text = FormatearFecha(pub.fechaPublicacion);
            imgPublicacion.ImageUrl = pub.rutaImagen;
            imgAvatar.ImageUrl = "/Public/images/user-avatar.png";

            if (usuario != null && pub.usuario != null)
            {
                pnlDenunciar.Visible = (usuario.idUsuario != pub.usuario.idUsuario);
            }
        }

        protected void btnConfirmarDenuncia_Click(object sender, EventArgs e)
        {
            var usuario = Session["usuario"] as usuario;
            if (usuario == null)
            {
                Response.Redirect("~/Inicio/Login.aspx");
                return;
            }

            string motivo = txtMotivoDenuncia.Text.Trim();
            if (string.IsNullOrWhiteSpace(motivo))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "error", "alert('El motivo no puede estar vacío.');", true);
                return;
            }

            int idPublicacion = int.Parse(hfIdPublicacion.Value);

            var denuncia = new denuncia
            {
                denunciante = usuario,
                autor = new publicacion { idPublicacion = idPublicacion },
                motivo = motivo,
                admin = null,
                activo = true
            };

            var denunciaService = new DenunciaWSClient();
            denunciaService.registrarDenuncia(denuncia);

            ScriptManager.RegisterStartupScript(this, GetType(), "denunciaOk", @"
                var modalElement = document.getElementById('modalDenuncia');
                var modal = bootstrap.Modal.getInstance(modalElement);
                if(modal) modal.hide();

                var toastEl = document.getElementById('toastDenuncia');
                var toast = new bootstrap.Toast(toastEl, {delay: 3000});
                toast.show();", true);
            txtMotivoDenuncia.Text = "";
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
                    Autor = c.comentador?.nombreUsuario ?? "Usuario",
                    AvatarUrl = "/Public/images/user-avatar.png",
                    Fecha = c.fecha.ToString("dd/MM/yyyy HH:mm"),
                    Contenido = c.contenido,
                    Valoracion = c.valoracion,
                    TieneValoracion = c.valoracion > 0,
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

        protected void btnComentar_Click(object sender, EventArgs e)
        {
            var usuario = Session["usuario"] as usuario;
            if (usuario == null)
            {
                Response.Redirect("~/Inicio/Login.aspx");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtComentario.Text))
            {
                return;
            }

            var publicacionCompleta = publicacionService.obtenerPublicacion(int.Parse(hfIdPublicacion.Value));

            var comentario = new comentario
            {
                contenido = txtComentario.Text.Trim(),
                valoracion = string.IsNullOrWhiteSpace(hfValoracion.Value) ? 0 : int.Parse(hfValoracion.Value),
                comentador = usuario,
                activo = true,
                publicacion = publicacionCompleta
            };

            comentarioService.registrarComentario(comentario);
            txtComentario.Text = "";
            hfValoracion.Value = "";
            CargarComentarios(comentario.publicacion.idPublicacion);
        }

        public string FormatearFecha(object fechaObj)
        {
            DateTime fecha;
            if (!DateTime.TryParse(fechaObj.ToString(), out fecha))
                return "";

            DateTime ahora = DateTime.Now.Date;
            DateTime fechaSoloDia = fecha.Date;
            string hora = fecha.ToString("HH:mm");

            int diferenciaDias = (ahora - fechaSoloDia).Days;

            switch (diferenciaDias)
            {
                case 0:
                    return "Hoy " + hora;
                case 1:
                    return "Ayer " + hora;
                case 2:
                    return "Hace 2 días " + hora;
                default:
                    return fecha.ToString("dd/MM/yyyy HH:mm");
            }
        }


    }
}