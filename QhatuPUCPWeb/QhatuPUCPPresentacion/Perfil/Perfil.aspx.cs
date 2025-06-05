using QhatuPUCPPresentacion.WebService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QhatuPUCPPresentacion.Perfil
{
    public partial class Perfil : System.Web.UI.Page
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
                lblNombre.Text = "Nombre: " + usuario.nombre;
                lblCodigoPUCP.Text = "Código PUCP: " + usuario.codigoPUCP;
                lblCorreo.Text = "Correo: " + usuario.correo;
                lblNombreUsuario.Text = "Nombre de Usuario: " + usuario.nombreUsuario;
                lblEstado.Text = "Estado: " + usuario.estado.ToString();
                lblEstado.CssClass = "perfil-estado badge " + (usuario.estado == estadoUsuario.HABILITADO ? "bg-success" : "bg-danger");

                PublicacionWSClient publicacionService = new PublicacionWSClient();
                publicacion[] todas = publicacionService.listarPublicacion();
                List<publicacion> propias = new List<publicacion>();

                foreach (publicacion p in todas)
                {
                    if (p.usuario != null && p.usuario.idUsuario == usuario.idUsuario)
                    {
                        propias.Add(p);
                        System.Diagnostics.Debug.WriteLine(p.fechaPublicacion.ToString()); 
                    }
                }

                rptPublicaciones.DataSource = propias;
                rptPublicaciones.DataBind();
            }
        }

        public string FormatearFechaString(object fechaObj)
        {

            try
            {
                string fechaComoTexto = fechaObj?.ToString(); // El WS normalmente devuelve un string como "2024-06-03T00:00:00"
                if (DateTime.TryParse(fechaComoTexto, out DateTime fecha))
                {
                    return fecha.ToString("dd/MM/yyyy");
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