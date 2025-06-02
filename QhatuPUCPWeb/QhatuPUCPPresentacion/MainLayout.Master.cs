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
                    lblUsuario.Text = Session["usuario"].ToString();
                }
                else
                {
                    Response.Redirect("~/Inicio/Login.aspx");
                }
            }
        }
    }
}