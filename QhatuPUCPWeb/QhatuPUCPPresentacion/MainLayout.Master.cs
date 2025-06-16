using System;

namespace QhatuPUCPPresentacion
{
    public partial class Site1 : System.Web.UI.MasterPage
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
            }
        }

        protected void BtnCerrarSession_Click(object sender, EventArgs e)
        {
            Session["usuario"] = null;
            Response.Redirect("~/Inicio/Login.aspx");
        }
    }
}