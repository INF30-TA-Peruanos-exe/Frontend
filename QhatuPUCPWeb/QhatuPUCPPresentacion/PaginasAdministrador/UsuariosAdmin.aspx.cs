using QhatuPUCPPresentacion.WebService;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QhatuPUCPPresentacion.PaginasAdministrador
{
    public partial class UsuariosAdmin : System.Web.UI.Page
    {
        protected UsuarioWSClient client;
        protected void Page_Init(object sender, EventArgs e)
        {
            client = new UsuarioWSClient();
            CargarUsuarios();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarUsuarios();
            }
        }

        private void CargarUsuarios()
        {
            try
            {
                List<usuario> usuarios = new List<usuario>();

                // Si no hay filtro, cargar todas
                usuarios = client.listarUsuarios().ToList();

                rptUsuarios.DataSource = usuarios;
                rptUsuarios.DataBind();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar usuarios: " + ex.Message);
            }
        }

        protected void BtnEditar_Click(object sender, EventArgs e)
        {
            int id_usuario= Int32.Parse(((LinkButton)sender).CommandArgument);

            usuario update_usuario = client.obtenerUsuario(id_usuario);

            if (update_usuario.estado == estadoUsuario.HABILITADO)
                update_usuario.estado = estadoUsuario.RESTRINGIDO;
            else
                update_usuario.estado = estadoUsuario.HABILITADO;

            client.actualizarUsuario(update_usuario);

            CargarUsuarios();

            //Response.Redirect("/Usuario/EditarUsuario.aspx?id_usuario=" + id_usuario_str);
        }

        protected void BtnEliminar_Click(object sender, EventArgs e)
        {
            string argument = ((LinkButton)sender).CommandArgument.ToString();
            int id_usuario = int.Parse(argument);
            if (!client.eliminarUsuario(id_usuario))
            {
                LblError.Text = "Error al eliminar el área";
            }
            else
            {
                CargarUsuarios();
            }
        }
    }
}