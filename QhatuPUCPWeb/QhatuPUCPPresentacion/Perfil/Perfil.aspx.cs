using QhatuPUCPPresentacion.WebService;
using System;
using System.Collections.Generic;
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
                Dictionary<int, string> fechasFormateadas = new Dictionary<int, string>();

                foreach (publicacion p in todas)
                {
                    if (p.usuario != null && p.usuario.idUsuario == usuario.idUsuario)
                    {
                        propias.Add(p);

                        try
                        {
                            //llama al webservice para obtener la fecha en string
                            string fecha = publicacionService.obtenerFechaPublicacionFormateada(p.idPublicacion);
                            fechasFormateadas[p.idPublicacion] = fecha;
                        }
                        catch
                        {
                            fechasFormateadas[p.idPublicacion] = "Sin fecha";
                        }
                    }
                }

                ViewState["fechasFormateadas"] = fechasFormateadas;
                rptPublicaciones.DataSource = propias;
                rptPublicaciones.DataBind();
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

        protected void rptPublicacion_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                int idPublicacion = Convert.ToInt32(e.CommandArgument);
                try
                {
                    PublicacionWSClient ws = new PublicacionWSClient();
                    ws.eliminarPublicacion(idPublicacion); // Solo se llama, sin esperar retorno

                    // Recarga la página para actualizar la lista
                    Response.Redirect(Request.RawUrl);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Error al eliminar publicación: " + ex.Message);
                    // Aquí puedes mostrar una alerta en pantalla si deseas
                }
            }
        }

    }
}
