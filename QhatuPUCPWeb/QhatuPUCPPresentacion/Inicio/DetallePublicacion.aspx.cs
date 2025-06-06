using QhatuPUCPPresentacion.WebService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QhatuPUCPPresentacion.Inicio
{
    public partial class DetallePublicacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] == null)
                {
                    Response.Redirect("~/Inicio/PaginaInicio.aspx");
                    return;
                }

                int idPublicacion = int.Parse(Request.QueryString["id"]);
                PublicacionWSClient ws = new PublicacionWSClient();
                publicacion p = ws.obtenerPublicacion(idPublicacion);

                if (p != null)
                {
                    lblTitulo.Text = p.titulo;
                    lblDescripcion.Text = p.descripcion;
                    lblFecha.Text = "Publicado el: " + ws.obtenerFechaPublicacionFormateada(p.idPublicacion);
                    imgPublicacion.Src = p.rutaImagen;
                    imgPublicacion.Alt = p.titulo;
                }
                else
                {
                    lblTitulo.Text = "Publicación no encontrada";
                }
            }
        }
    }
}