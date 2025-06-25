using QhatuPUCPPresentacion.WebService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QhatuPUCPPresentacion.Filtros
{
    public partial class ListaFacultad : System.Web.UI.Page
    {
        protected FacultadWSClient client;
        protected void Page_Init(object sender, EventArgs e)
        {
            client = new FacultadWSClient();
            CargarFacultades();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarFacultades();
            }
        }

        private void CargarFacultades()
        {
            try
            {
                List<facultad> facultades = new List<facultad>();

                // Si no hay filtro, cargar todas
                facultades = client.listarFacultad().ToList();

                ViewState["Facultades"] = facultades; // guardar para filtrado posterior
                rptFacultades.DataSource = facultades;
                rptFacultades.DataBind();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar facultades: " + ex.Message);
            }
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string criterio = txtBuscar.Text.Trim().ToLower();

            if (ViewState["Facultades"] != null)
            {
                var facultades = (List<facultad>)ViewState["Facultades"];
                var filtradas = facultades
                    .Where(u => u.nombre.ToLower().Contains(criterio))
                    .ToList();

                rptFacultades.DataSource = filtradas;
                rptFacultades.DataBind();
            }
        }
        protected void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            btnBuscar_Click(sender, e); // o directamente filtrar aquí
        }
        protected void BtnAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Filtros/NuevaFacultad.aspx");
        }
        protected void BtnEditar_Click(object sender, EventArgs e)
        {
            string id_facultad_str = ((LinkButton)sender).CommandArgument.ToString();
            
            Response.Redirect("/Filtros/NuevaFacultad.aspx?id_facultad=" + id_facultad_str);
        }

        protected void BtnEliminar_Click(object sender, EventArgs e)
        {
            string argument = ((LinkButton)sender).CommandArgument.ToString();
            int id_facultad = int.Parse(argument);
            client.eliminarFacultad(id_facultad);
            CargarFacultades();
        }
    }
}