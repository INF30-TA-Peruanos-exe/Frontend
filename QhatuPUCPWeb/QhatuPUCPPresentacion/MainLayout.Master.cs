using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QhatuPUCPPresentacion
{
    public partial class MainLayout : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["usuario"] != null)
                {
                    var usuario = (WebService.usuario)Session["usuario"];
                    lblUsuario.Text = usuario.nombreUsuario;
                }
                else
                {
                    Response.Redirect("~/Inicio/Login.aspx");
                }

                string path = Request.Url.AbsolutePath.ToLower();
                if (path.Contains("/inicio/paginainicio.aspx"))
                {
                    lblTituloNavbar.Text = "Inicio";
                    linkInicio.CssClass += " active";
                }
                else if (path.Contains("/perfil/perfil.aspx"))
                {
                    lblTituloNavbar.Text = "Perfil";
                    linkPerfil.CssClass += " active";
                }
                else if (path.Contains("/guardados/paginaguardados.aspx"))
                {
                    lblTituloNavbar.Text = "Guardados";
                    linkGuardados.CssClass += " active";
                }
                else if (path.Contains("/notificaciones/notificaciones.aspx"))
                {
                    lblTituloNavbar.Text = "Notificaciones";
                    linkNotificaciones.CssClass += " active";
                }
                else if (path.Contains("/hacerpubli/postear.aspx"))
                {
                    lblTituloNavbar.Text = "Postear";
                    linkPostear.CssClass += " active";
                }
                else
                {
                    lblTituloNavbar.Text = "QhatuPUCP";
                }


            }
        }

        protected void BtnCerrarSession_Click(object sender, EventArgs e)
        {
            Session["usuario"] = null;
            Response.Redirect("~/Inicio/Login.aspx");
        }
    }
}