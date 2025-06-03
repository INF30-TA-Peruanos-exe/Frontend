using QhatuPUCPPresentacion.WebService;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QhatuPUCPPresentacion.HacerPubli
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        CursoWSClient clientCurso = new CursoWSClient();
        FacultadWSClient clientFacultad = new FacultadWSClient();
        EspecialidadWSClient clientEspecialidad = new EspecialidadWSClient();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["usuario"] == null)
                {
                    Response.Redirect("~/Inicio/Login.aspx");
                }

                cargarDatosCheckbox();
            }
        }

        private void cargarDatosCheckbox()
        {
            try
            {
                chkCursos.DataSource = clientCurso.listarCurso();
                chkCursos.DataTextField = "nombre";
                chkCursos.DataValueField = "idCurso";
                chkCursos.DataBind();

                System.Diagnostics.Debug.WriteLine("Cursos cargados: " + chkCursos.Items.Count);
                foreach (ListItem item in chkCursos.Items)
                {
                    System.Diagnostics.Debug.WriteLine($"Curso: {item.Text} (ID: {item.Value})");
                }

                chkFacultades.DataSource = clientFacultad.listarFacultad();
                chkFacultades.DataTextField = "nombre";
                chkFacultades.DataValueField = "idFacultad";
                chkFacultades.DataBind();

                chkEspecialidades.DataSource = clientEspecialidad.listarEspecialidad();
                chkEspecialidades.DataTextField = "nombre";
                chkEspecialidades.DataValueField = "idEspecialidad";
                chkEspecialidades.DataBind();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al cargar listas: " + ex.Message;
            }
        }

        protected void btnPublicar_Click(object sender, EventArgs e)
        {
            try
            {
                PublicacionWSClient client = new PublicacionWSClient();
                publicacion nueva = new publicacion();

                nueva.titulo = txtTitulo.Text.Trim();
                nueva.descripcion = txtDescripcion.Text.Trim();
                nueva.activo = true;
                nueva.rutaImagen = "/Public/images/imagen-ataque.jpg";
                nueva.estado = estadoPublicacion.VISIBLE;
                nueva.estadoSpecified = true;

                usuario usuarioSesion = Session["usuario"] as usuario;
                if (usuarioSesion == null)
                {
                    throw new Exception("Sesión de usuario no válida.");
                }
                nueva.usuario = usuarioSesion;

                // Cursos
                List<curso> cursos = new List<curso>();
                foreach (ListItem item in chkCursos.Items)
                {
                    if (item.Selected)
                    {
                        var cursoTemp = clientCurso.obtenerCurso(int.Parse(item.Value));
                        if (cursoTemp != null && !string.IsNullOrEmpty(cursoTemp.nombre))
                        {
                            cursos.Add(cursoTemp);
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine($"Curso inválido: {item.Value}");
                        }
                    }
                }

                System.Diagnostics.Debug.WriteLine("Cursos seleccionados:");
                foreach (ListItem item in chkCursos.Items)
                {
                    if (item.Selected)
                    {
                        System.Diagnostics.Debug.WriteLine($"✔ {item.Text} (ID: {item.Value})");
                    }
                }

                if (cursos.Count == 0)
                {
                    throw new Exception("Debe seleccionar al menos un curso.");
                }
                nueva.publicacionesCursos = cursos.ToArray();

                // Facultades
                List<facultad> facultades = new List<facultad>();
                foreach (ListItem item in chkFacultades.Items)
                {
                    if (item.Selected)
                    {
                        facultades.Add(clientFacultad.obtenerFacultad(int.Parse(item.Value)));
                    }
                }
                nueva.publicacionesFacultades = facultades.ToArray();

                // Especialidades
                List<especialidad> especialidades = new List<especialidad>();
                foreach (ListItem item in chkEspecialidades.Items)
                {
                    if (item.Selected)
                    {
                        especialidades.Add(clientEspecialidad.obtenerEspecialidad(int.Parse(item.Value)));
                    }
                }
                nueva.publicacionesEspecialidades = especialidades.ToArray();

                // crear
                client.crearPublicacion(nueva);
                Response.Redirect("~/Inicio/PaginaInicio.aspx");
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al publicar: " + ex.Message;
            }
        }

        private QhatuPUCPPresentacion.WebService.date CrearFechaSoap(DateTime fecha)
        {
            string fechaIso = System.Xml.XmlConvert.ToString(fecha, System.Xml.XmlDateTimeSerializationMode.Utc);
            string xml = $"<date xmlns=\"http://www.w3.org/2001/XMLSchema\">{fechaIso}</date>";

            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.LoadXml(xml);

            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(QhatuPUCPPresentacion.WebService.date));
            var soapDate = (QhatuPUCPPresentacion.WebService.date)serializer.Deserialize(new System.Xml.XmlNodeReader(doc.DocumentElement));

            return soapDate;
        }
    }
}
