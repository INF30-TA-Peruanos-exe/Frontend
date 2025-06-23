using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QhatuPUCPPresentacion.WebService;

namespace QhatuPUCPPresentacion.Inicio
{
    public partial class Login : System.Web.UI.Page
    {
        protected UsuarioWSClient usuarioService;
        protected AdministradorWSClient administradorService;

        protected void Page_Init(object senver, EventArgs e)
        {
            usuarioService = new UsuarioWSClient();
            administradorService = new AdministradorWSClient();
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            string correo = txtCorreo.Text.Trim();
            string contrasena = txtContrasena.Text.Trim();

            // Aquí deberías consumir tu WebService de Usuario
            // y validar si el login es correcto

            //if (correo == "admin@pucp.edu.pe" && contrasena == "123") // Ejemplo fijo
            //{
            //    Session["usuario"] = correo;
            //    Response.Redirect("PaginaInicio.aspx");
            //}
            //else
            //{
            // Mostrar un mensaje de error si quieres
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alerta", "alert('Credenciales incorrectas');", true);
            //}
            try
            {
                //usuario[] usuarios=usuarioService.listarUsuarios();
                usuario usuarioValido = usuarioService.obtenerUsuarioPorCorreoYContra(correo,contrasena);
                //administrador[] administradores = administradorService.listarAdministrador();
                administrador administradorValido = administradorService.obtenerAdministradorPorCorreoYContra(contrasena);

                //Busca en todos los usuarios
                /*for (int i = 0; i < usuarios.Length; i++)
                {
                    if (usuarios[i].correo == correo && usuarios[i].contrasena == contrasena)
                    {
                        usuarioValido = usuarios[i];
                        break;
                    }
                    for(int j = 0; j < administradores.Length; j++)
                    {
                        if (usuarios[i].idUsuario == administradores[j].idUsuario && administradores[j].claveMaestra == contrasena)
                        {
                            usuarioValido = usuarios[i];
                            administradorValido = administradores[j];
                            break;
                        }
                    }

                }*/

                if (usuarioValido != null && administradorValido == null)
                {
                    Session["usuario"] = usuarioValido;
                    Response.Redirect("~/Inicio/PaginaInicio.aspx");
                }
                else if (administradorValido != null && administradorValido.correo==correo)
                {
                    Session["administrador"] = administradorValido;
                    Session["usuario"] = usuarioValido;
                    Response.Redirect("~/Inicio/PaginaInicioAdmin.aspx");
                }
                else
                {
                    lblError.Text = "Correo o contrasena incorrectos";
                }



            }catch (Exception ex)
            {
                lblError.Text = "Error en el WS";
            }

        }

        protected void BtnSignup_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text.Trim();
            string codigoPucp = txtCodigoPucp.Text.Trim();
            string correo = txtCorreoNuevo.Text.Trim();
            string nombreUsuario = txtNombreUsuario.Text.Trim();
            string contrasena = txtContrasenaNueva.Text.Trim();

            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(correo) ||
                string.IsNullOrEmpty(codigoPucp) || string.IsNullOrEmpty(contrasena) || string.IsNullOrEmpty(nombreUsuario))
            {
                lblSignupError.Text = "Por favor, complete todos los campos.";
                ScriptManager.RegisterStartupScript(this, GetType(), "activarProfileTab",
                    "$('#pills-profile-tab').tab('show');", true);
                return;
            }

            try
            {
                usuario usuario = new usuario
                {
                    activo = true,
                    codigoPUCP = int.Parse(codigoPucp),
                    correo = correo,
                    contrasena = contrasena,
                    nombre = nombre,
                    nombreUsuario = nombreUsuario,
                    estado = estadoUsuario.HABILITADO,
                    estadoSpecified = true
                };

                UsuarioWSClient usuarioService = new UsuarioWSClient();
                usuarioService.registrarUsuario(usuario);

                usuario usuarioRegistrado = usuarioService.obtenerUsuarioPorCorreoYContra(correo, contrasena);

                Session["usuario"] = usuarioRegistrado;
                Response.Redirect("~/Inicio/PaginaInicio.aspx");
            }
            catch (Exception ex)
            {
                lblSignupError.CssClass = "text-danger mt-2";
                ScriptManager.RegisterStartupScript(this, GetType(), "activarProfileTab",
                    "$('#pills-profile-tab').tab('show');", true);
                lblSignupError.Text = "Error al registrar: " + ex.Message;
            }
        }

    }
}