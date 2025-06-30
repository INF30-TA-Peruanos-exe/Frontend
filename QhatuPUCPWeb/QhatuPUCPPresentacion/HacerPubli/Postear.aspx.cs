using QhatuPUCPPresentacion.WebService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

                //Subida de imagen
                if (fuImagen.HasFile)
                {
                    try
                    {
                        // Validar extensión permitida
                        string extension = Path.GetExtension(fuImagen.FileName).ToLower();
                        string[] extensionesPermitidas = {".jpg", ".jpeg", ".png"};

                        if (!extensionesPermitidas.Contains(extension))
                        {
                            lblMensaje.Text = "Solo se permiten imágenes (.jpg, .jpeg, .png)";
                            return; 
                        }

                        string nombreArchivo = Path.GetFileName(fuImagen.FileName);
                        string rutaRelativa = "/Imagenes/" + nombreArchivo;
                        string rutaFisica = Server.MapPath(rutaRelativa);

                        fuImagen.SaveAs(rutaFisica);
                        nueva.rutaImagen = rutaRelativa;
                    }
                    catch (Exception exImg)
                    {
                        System.Diagnostics.Debug.WriteLine("Error al guardar imagen: " + exImg.Message);
                        // Se mantiene imagen por defecto si hay error
                    }
                }

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

                System.Diagnostics.Debug.WriteLine($"→ Cursos enviados: {nueva.publicacionesCursos?.Length ?? 0}");
                foreach (var c in nueva.publicacionesCursos)
                    System.Diagnostics.Debug.WriteLine($"  - {c.idCurso}: {c.nombre}");

                System.Diagnostics.Debug.WriteLine($"→ Facultades enviadas: {nueva.publicacionesFacultades?.Length ?? 0}");
                foreach (var f in nueva.publicacionesFacultades)
                    System.Diagnostics.Debug.WriteLine($"  - {f.idFacultad}: {f.nombre}");

                System.Diagnostics.Debug.WriteLine($"→ Especialidades enviadas: {nueva.publicacionesEspecialidades?.Length ?? 0}");
                foreach (var es in nueva.publicacionesEspecialidades)
                    System.Diagnostics.Debug.WriteLine($"  - {es.idEspecialidad}: {es.nombre}");

                client.crearPublicacion(nueva);
                Response.Redirect("~/Inicio/PaginaInicio.aspx");
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al publicar: " + ex.Message;
            }
        }

    }
}
