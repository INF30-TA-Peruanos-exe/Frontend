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

        protected void Page_Init(object senver, EventArgs e)
        {
            usuarioService = new UsuarioWSClient();
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
                usuario[] usuarios=usuarioService.listarUsuarios();
                usuario usuarioValido = null;
                
                //Busca en todos los usuarios
                for(int i = 0; i < usuarios.Length; i++)
                {
                    if (usuarios[i].correo == correo && usuarios[i].contrasena == contrasena)
                    {
                        usuarioValido = usuarios[i];
                        break;
                    }
                }

                if (usuarioValido != null)
                {
                    Session["usuario"] = usuarioValido;
                    Response.Redirect("~/Inicio/PaginaInicio.aspx");
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
            string correo = txtCorreoNuevo.Text.Trim();
            string telefono = txtTelefono.Text.Trim();
            string contrasena = txtContrasenaNueva.Text.Trim();

            // Validación simple
            // Pseudocódigo detallado:
            // 1. Validar que los campos no estén vacíos.
            // 2. Si hay error, mostrar mensaje y ejecutar JS para cambiar a la pestaña "pills-profile".
            // 3. Si no hay error, continuar con el registro.

            // Reemplaza el bloque de validación en BtnSignup_Click:
            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(correo) ||
                string.IsNullOrEmpty(telefono) || string.IsNullOrEmpty(contrasena))
            {
                lblSignupError.Text = "Por favor, complete todos los campos.";
                // Mantener el tab "pills-profile" activo usando JavaScript
                ScriptManager.RegisterStartupScript(this, GetType(), "activarProfileTab",
                    "$('#pills-profile-tab').tab('show');", true);
                return;
            }

            try
            {
                // Aquí deberías insertar los datos en tu base de datos
                // o llamar a una capa de lógica de negocio para registrar al usuario
                // Ejemplo (ficticio):
                // UsuarioBL.RegistrarUsuario(nombre, correo, telefono, contrasena);

                lblSignupError.CssClass = "text-success mt-2";
                lblSignupError.Text = "Usuario registrado correctamente.";
            }
            catch (Exception ex)
            {
                lblSignupError.Text = "Error al registrar: " + ex.Message;
            }
        }

    }
}