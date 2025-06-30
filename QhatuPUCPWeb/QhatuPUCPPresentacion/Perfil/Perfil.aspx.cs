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

                //Para el modal
                txtNombre.Text = usuario.nombre;
                txtCorreo.Text = usuario.correo;
                txtNombreUsuario.Text = usuario.nombreUsuario;
                

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
                    ws.eliminarPublicacion(idPublicacion);

                    Response.Redirect(Request.RawUrl);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Error al eliminar publicación: " + ex.Message);
                }
            }
            else if (e.CommandName == "Editar")
            {
                int idPublicacion = Convert.ToInt32(e.CommandArgument);
                Response.Redirect($"~/Perfil/EditarPublicacion.aspx?id={idPublicacion}");
            }
        }

        protected void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            usuario usuarioActual = Session["usuario"] as usuario;

            if (usuarioActual != null)
            {
                usuarioActual.nombre = txtNombre.Text.Trim();
                usuarioActual.correo = txtCorreo.Text.Trim();
                usuarioActual.nombreUsuario = txtNombreUsuario.Text.Trim();

                try
                {
                    UsuarioWSClient usuarioService = new UsuarioWSClient();
                    usuarioService.actualizarUsuario(usuarioActual);

                    Session["usuario"] = usuarioActual;

                    lblNombre.Text = "Nombre: " + usuarioActual.nombre;
                    lblCorreo.Text = "Correo: " + usuarioActual.correo;
                    lblNombreUsuario.Text = "Nombre de Usuario: " + usuarioActual.nombreUsuario;

                    ScriptManager.RegisterStartupScript(this, GetType(), "modalClose", "$('#modalEditarPerfil').modal('hide');", true);
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertaError", $"alert('Error al modificar: {ex.Message}');", true);
                }
            }
        }

        protected void btnGuardarContrasena_Click(object sender, EventArgs e)
        {

            usuario usuarioActual = Session["usuario"] as usuario;

            if (usuarioActual != null)
            {
                string nuevaContrasena = txtNuevaContrasena.Text.Trim();
                string confirmaContrasena = txtConfirmarContrasena.Text.Trim();

                if (nuevaContrasena != confirmaContrasena)
                {
                    lblMensajeCambioContrasena.Text = "Las contraseñas no coinciden.";

                    ScriptManager.RegisterStartupScript(this, GetType(), "abrirModal", "$('#modalCambiarContrasena').modal('show');", true);
                    return;
                }

                usuarioActual.contrasena = nuevaContrasena;

                try
                {
                    UsuarioWSClient usuarioService = new UsuarioWSClient();
                    usuarioService.actualizarUsuario(usuarioActual);

                    Session["usuario"] = usuarioActual;

                    lblMensajeCambioContrasena.CssClass = "text-success";
                    lblMensajeCambioContrasena.Text = "Contraseña actualizada correctamente.";


                    ScriptManager.RegisterStartupScript(this, GetType(), "modalClose", "$('#modalCambiarContrasena').modal('hide');", true);
                }
                catch (Exception ex)
                {
                    lblMensajeCambioContrasena.Text = "Error al actualizar: " + ex.Message;

                    ScriptManager.RegisterStartupScript(this, GetType(), "abrirModal", "$('#modalCambiarContrasena').modal('show');", true);
                }
            }

        }
        public String cambiarSegunEstado(String estado)
        {
            switch (estado)
            {
                case "VISIBLE":
                    return "bg-success text-white";
                case "RESTRINGIDO":
                    return "bg-warning text-dark";
                case "OCULTO":
                    return "bg-dark text-white";
                default:
                    return "bg-light text-dark";
            }
        }


    }
}
