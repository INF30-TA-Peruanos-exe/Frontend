using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using QhatuPUCPPresentacion.WebService;

namespace QhatuPUCPPresentacion.Filtros
{
    public partial class ListaEspecialidad : System.Web.UI.Page
    {
        protected EspecialidadWSClient client;
        protected void Page_Init(object sender, EventArgs e)
        {
            client = new EspecialidadWSClient();
            CargarEspecialidades();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarEspecialidades();
            }
        }

        private void CargarEspecialidades()
        {
            try
            {
                List<especialidad> especialidades = new List<especialidad>();

                // Si no hay filtro, cargar todas
                especialidades = client.listarEspecialidad().ToList();

                ViewState["Especialidades"] = especialidades; // guardar para filtrado posterior
                rptEspecialidades.DataSource = especialidades;
                rptEspecialidades.DataBind();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar especialidades: " + ex.Message);
            }
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string criterio = txtBuscar.Text.Trim().ToLower();

            if (ViewState["Especialidades"] != null)
            {
                var especialidades = (List<curso>)ViewState["Especialidades"];
                var filtradas = especialidades
                    .Where(u => u.nombre.ToLower().Contains(criterio))
                    .ToList();

                rptEspecialidades.DataSource = filtradas;
                rptEspecialidades.DataBind();
            }
        }
        protected void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            btnBuscar_Click(sender, e); // o directamente filtrar aquí
        }
        protected void BtnAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Filtros/NuevaEspecialidad.aspx");
        }
        protected void BtnEditar_Click(object sender, EventArgs e)
        {
            string id_especialidad_str = ((LinkButton)sender).CommandArgument.ToString();

            Response.Redirect("/Filtros/NuevaEspecialidad.aspx?id_especialidad=" + id_especialidad_str);
        }

        protected void BtnEliminar_Click(object sender, EventArgs e)
        {
            string argument = ((LinkButton)sender).CommandArgument.ToString();
            int id_especialidad = int.Parse(argument);
            client.eliminarEspecialidad(id_especialidad);
            CargarEspecialidades();
        }
    }
}