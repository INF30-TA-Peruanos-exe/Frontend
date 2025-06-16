using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QhatuPUCPPresentacion
{
    public partial class MainLayoutMasterAdmin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["administrador"] != null)
                {
                    var administrador = (WebService.administrador)Session["administrador"];
                    lblUsuario.Text = administrador.nombreUsuario;
                }
                else
                {
                    Response.Redirect("~/Inicio/Login.aspx");
                }
            }
        }

        protected void BtnCerrarSession_Click(object sender, EventArgs e)
        {
            Session["administrador"] = null;
            Response.Redirect("~/Inicio/Login.aspx");
        }
    }
}