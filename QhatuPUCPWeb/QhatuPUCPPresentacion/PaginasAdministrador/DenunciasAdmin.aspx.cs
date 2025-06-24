using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QhatuPUCPPresentacion.WebService;

namespace QhatuPUCPPresentacion.PaginasAdministrador
{
    public partial class DenunciasAdmin : System.Web.UI.Page
    {
        protected DenunciaWSClient client;
        protected void Page_Init(object sender, EventArgs e)
        {
            client = new DenunciaWSClient();
            CargarDenuncias();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDenuncias();
            }
        }
        private void CargarDenuncias()
        {
            try
            {
                List<denuncia> denuncias = new List<denuncia>();

                denuncias = client.listarDenuncia().ToList();

                Console.WriteLine(denuncias.Count);

                ViewState["Denuncias"] = denuncias; // guardar para filtrado posterior
                rptDenuncias.DataSource = denuncias;
                rptDenuncias.DataBind();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar denuncias: " + ex.Message);
            }
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string criterio = txtBuscar.Text.Trim().ToLower();

            if (ViewState["Denuncias"] != null)
            {
                var denuncias = (List<denuncia>)ViewState["Denuncias"];
                var filtradas = denuncias
                    .Where(d => d.motivo.ToLower().Contains(criterio))
                    .ToList();

                rptDenuncias.DataSource = filtradas;
                rptDenuncias.DataBind();
            }
        }
        protected void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            btnBuscar_Click(sender, e); // o directamente filtrar aquí
        }
    }
}