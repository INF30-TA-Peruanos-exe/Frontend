using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QhatuPUCPPresentacion.WebService;

namespace QhatuPUCPPresentacion.Favoritos
{
    public partial class PaginaFavoritos : System.Web.UI.Page
    {
        protected PublicacionWSClient publicacionService;

        protected void Page_Init(object sender, EventArgs e)
        {
            publicacionService = new PublicacionWSClient();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarFavoritos();
        }

        private void CargarFavoritos()
        {
            var usuario = Session["usuario"] as usuario;
            if (usuario == null)
            {
                Response.Redirect("~/Inicio/Login.aspx");
                return;
            }

            var todas = publicacionService.listarPublicacionConFavoritos(usuario.idUsuario);
            var favoritas = todas.Where(p => p.esFavorito).ToArray();

            rptFavoritos.DataSource = favoritas;
            rptFavoritos.DataBind();
        }

        protected void rptFavoritos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "VerDetalle")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                Response.Redirect($"DetallePublicacion.aspx?id={id}");
            }
        }
    }
}