using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QhatuPUCPPresentacion.WebService;

namespace QhatuPUCPPresentacion.Favoritos
{
    public partial class PaginaFavoritos: System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["usuario"] == null)
                {
                    Response.Redirect("~/Inicio/Login.aspx");
                }
                usuario usuario = Session["usuario"] as usuario;

                PublicacionWSClient publicacionService = new PublicacionWSClient();
                publicacion[] favoritas = publicacionService.listarFavoritos(usuario.idUsuario);
                rptPublicacionesFavoritas.DataSource = favoritas;
                rptPublicacionesFavoritas.DataBind();
                



            }
        }
        public string FormatearFechaString(object idPublicacionObj)
        {
            try
            {
                int idPublicacion = Convert.ToInt32(idPublicacionObj);
                var fechas = ViewState["fechasFormateadas"] as Dictionary<int, string>;

                if (fechas != null && fechas.ContainsKey(idPublicacion))
                {
                    return fechas[idPublicacion];
                }
            }
            catch
            {
                return "Fecha inválida";
            }

            return "Sin fecha";
        }
    }
}