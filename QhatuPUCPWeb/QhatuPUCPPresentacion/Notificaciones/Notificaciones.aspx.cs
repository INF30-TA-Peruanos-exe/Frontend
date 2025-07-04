﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using QhatuPUCPPresentacion.WebService;

namespace QhatuPUCPPresentacion.Notificaciones
{
    public partial class WebForm1 : System.Web.UI.Page
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
            // Aquí obtienes las notificaciones del usuario actual
            // Puedes obtenerlas de una base de datos, servicio web, etc.
            // Este es un ejemplo con datos de prueba
            usuario usuario = Session["usuario"] as usuario;
            var notificaciones = notificacion.listarNotificacionUsuario(usuario.idUsuario);

            if (notificaciones != null)
            {
                rptNotificacionesUsuario.DataSource = notificaciones;
                rptNotificacionesUsuario.DataBind();
            }
            else
            {
                // El Repeater ya maneja el caso de no haber notificaciones con el FooterTemplate
                rptNotificacionesUsuario.DataSource = null;
                rptNotificacionesUsuario.DataBind();
            }
        }
    }
}