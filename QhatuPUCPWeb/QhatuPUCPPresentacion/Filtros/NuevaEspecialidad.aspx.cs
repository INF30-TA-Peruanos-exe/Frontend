using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QhatuPUCPPresentacion.WebService;

namespace QhatuPUCPPresentacion.Filtros
{
    public partial class NuevaEspecialidad : System.Web.UI.Page
    {
        protected EspecialidadWSClient client;
        protected string id_especialidad_str;
        protected void Page_Init(object sender, EventArgs e)
        {
            client = new EspecialidadWSClient();
            id_especialidad_str = Request.QueryString["id_especialidad"]?.ToString();
            if (id_especialidad_str != null)
            {
                LblTitulo.Text = "Modificar Especialidad";
                int id_especialidad = int.Parse(id_especialidad_str);
                especialidad especialidad = client.obtenerEspecialidad(id_especialidad);
                TxtIdEspecialidad.Text = especialidad.idEspecialidad.ToString();
                TxtNombre.Text = especialidad.nombre;
                ChkActivo.Checked = especialidad.activo;
            }
            else
            {
                LblTitulo.Text = "Agregar Especialidad";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                especialidad especialidad = new especialidad();
                especialidad.nombre = TxtNombre.Text.Trim();
                especialidad.activo = ChkActivo.Checked;
                if (id_especialidad_str == null)
                    client.registrarEspecialidad(especialidad);
                else
                {
                    especialidad.idEspecialidad = int.Parse(TxtIdEspecialidad.Text.Trim());
                    client.actualizarEspecialidad(especialidad);
                }
                Response.Redirect("/Filtros/ListaEspecialidad.aspx");
            }
            catch (Exception ex)
            {
                LblError.Text = ex.Message;
                return;
            }
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Filtros/ListaEspecialidad.aspx");
        }
    }
}