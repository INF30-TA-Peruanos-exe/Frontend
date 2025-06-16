using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QhatuPUCPPresentacion.WebService;

namespace QhatuPUCPPresentacion.Filtros
{
    public partial class NuevoCurso : System.Web.UI.Page
    {
        protected CursoWSClient client;
        protected string id_curso_str;
        protected void Page_Init(object sender, EventArgs e)
        {
            client = new CursoWSClient();
            id_curso_str = Request.QueryString["id_curso"]?.ToString();
            if (id_curso_str != null)
            {
                LblTitulo.Text = "Modificar Curso";
                int id_curso = int.Parse(id_curso_str);
                curso curso = client.obtenerCurso(id_curso);
                TxtIdCurso.Text = curso.idCurso.ToString();
                TxtNombre.Text = curso.nombre;
                ChkActivo.Checked = curso.activo;
            }
            else
            {
                LblTitulo.Text = "Agregar Curso";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                curso curso = new curso();
                curso.nombre = TxtNombre.Text.Trim();
                curso.activo = ChkActivo.Checked;
                if (id_curso_str == null)
                    client.registrarCurso(curso);
                else
                {
                    curso.idCurso = int.Parse(TxtIdCurso.Text.Trim());
                    client.actualizarCurso(curso);
                }
                Response.Redirect("/Filtros/ListaCurso.aspx");
            }
            catch (Exception ex)
            {
                LblError.Text = ex.Message;
                return;
            }
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Filtros/ListaCurso.aspx");
        }
    }
}