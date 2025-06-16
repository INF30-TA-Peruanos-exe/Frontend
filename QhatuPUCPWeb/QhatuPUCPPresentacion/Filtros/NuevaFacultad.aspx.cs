using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QhatuPUCPPresentacion.WebService;

namespace QhatuPUCPPresentacion.Filtros
{
    public partial class NuevaFacultad : System.Web.UI.Page
    {
        protected FacultadWSClient client;
        protected string id_facultad_str;
        protected void Page_Init(object sender, EventArgs e)
        {
            client = new FacultadWSClient();
            id_facultad_str = Request.QueryString["id_facultad"]?.ToString();
            if (id_facultad_str != null)
            {
                LblTitulo.Text = "Modificar Facultad";
                int id_facultad = int.Parse(id_facultad_str);
                facultad facultad = client.obtenerFacultad(id_facultad);
                TxtIdFacultad.Text = facultad.idFacultad.ToString();
                TxtNombre.Text = facultad.nombre;
                ChkActivo.Checked = facultad.activo;
            }
            else
            {
                LblTitulo.Text = "Agregar Facultad";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                facultad facultad = new facultad();
                facultad.nombre = TxtNombre.Text.Trim();
                facultad.activo = ChkActivo.Checked;
                if (id_facultad_str == null)
                    client.registrarFacultad(facultad);
                else
                {
                    facultad.idFacultad = int.Parse(TxtIdFacultad.Text.Trim());
                    client.actualizarFacultad(facultad);
                }
                Response.Redirect("/Filtros/ListaFacultad.aspx");
            }
            catch (Exception ex)
            {
                LblError.Text = ex.Message;
                return;
            }
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Filtros/ListaFacultad.aspx");
        }
    }
}