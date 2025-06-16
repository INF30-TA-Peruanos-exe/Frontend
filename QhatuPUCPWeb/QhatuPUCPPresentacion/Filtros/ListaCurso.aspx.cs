using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QhatuPUCPPresentacion.WebService;

namespace QhatuPUCPPresentacion.Filtros
{
    public partial class ListaCurso : System.Web.UI.Page
    {
        protected CursoWSClient client;
        protected void Page_Init(object sender, EventArgs e)
        {
            client = new CursoWSClient();
            CargarCursos();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCursos();
            }
        }

        private void CargarCursos()
        {
            try
            {
                List<curso> cursos = new List<curso>();

                // Si no hay filtro, cargar todas
                cursos = client.listarCurso().ToList();

                rptCursos.DataSource = cursos;
                rptCursos.DataBind();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar cursos: " + ex.Message);
            }
        }
        protected void BtnAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Filtros/NuevoCurso.aspx");
        }
        protected void BtnEditar_Click(object sender, EventArgs e)
        {
            string id_curso_str = ((LinkButton)sender).CommandArgument.ToString();

            Response.Redirect("/Filtros/NuevoCurso.aspx?id_curso=" + id_curso_str);
        }

        protected void BtnEliminar_Click(object sender, EventArgs e)
        {
            string argument = ((LinkButton)sender).CommandArgument.ToString();
            int id_curso = int.Parse(argument);
            client.eliminarCurso(id_curso);
            CargarCursos();
        }
    }
}