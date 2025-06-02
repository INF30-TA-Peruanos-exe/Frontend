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

        protected void btnSignup_Click(object sender, EventArgs e)
        {
            // Lógica para registro (opcional si no se va a implementar)
        }

    }
}