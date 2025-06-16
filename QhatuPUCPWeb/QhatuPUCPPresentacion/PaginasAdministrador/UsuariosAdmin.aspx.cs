using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QhatuPUCPPresentacion.WebService;

namespace QhatuPUCPPresentacion.PaginasAdministrador
{
    public partial class UsuariosAdmin : System.Web.UI.Page
    {
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
                UsuarioWSClient client = new UsuarioWSClient();
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

        protected void rptUsuarios_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            UsuarioWSClient client = new UsuarioWSClient();
            int id = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Eliminar")
            {
                // Lógica para eliminar Usuario
                client.eliminarUsuario(id);
                // Volver a cargar las usuarios actualizadas
                CargarUsuarios();
            }
            else if (e.CommandName == "CambiarEstado")
            {
                usuario update_user = client.obtenerUsuario(id);

                if (update_user.estado == estadoUsuario.HABILITADO)
                {
                    update_user.estado = estadoUsuario.RESTRINGIDO;
                }
                else
                {
                    update_user.estado = estadoUsuario.HABILITADO;
                }

                client.actualizarUsuario(update_user);
                // Volver a cargar las usuarios actualizadas
                CargarUsuarios();
            }
        }
    }
}