using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using QhatuPUCPPresentacion.WebService;

namespace QhatuPUCPPresentacion.Notificaciones
{
    public partial class Notificaciones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarNotificaciones();
            }
        }

        private void CargarNotificaciones()
        {


            if (Session["usuario"] == null)
            {
                Response.Redirect("~/Inicio/Login.aspx");
            }
            NotificacionWSClient notificacion = new NotificacionWSClient();
            usuario usuario = Session["usuario"] as usuario;
            var notificaciones = notificacion.listarNotificacionUsuario(usuario.idUsuario);

            if (notificaciones != null)
            {
                rptNotificacionesUsuario.DataSource = notificaciones;
                rptNotificacionesUsuario.DataBind();
            }
            else
            {
                rptNotificacionesUsuario.DataSource = null;
                rptNotificacionesUsuario.DataBind();
            }
        }
    }
}