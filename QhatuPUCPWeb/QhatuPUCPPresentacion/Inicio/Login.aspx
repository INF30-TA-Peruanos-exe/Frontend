<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="QhatuPUCPPresentacion.Inicio.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Login</title>
    <link href="Login.css" rel="stylesheet" type="text/css" />

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="hdnActiveTab" runat="server" />
        <div class="login-container d-flex justify-content-center align-items-center py-5" style="min-height: 100vh;">
            <div class="d-flex flex-column flex-md-row w-100 shadow-lg rounded-4 overflow-hidden bg-white login-card">
                
                <div class="login-imagen col-12 col-md-6 p-4 d-flex justify-content-center align-items-center bg-pucp text-white text-center">
                    <div class="d-flex flex-column flex-portrait-row align-items-center text-center text-portrait-start gap-4">
                        <img src='<%= ResolveUrl("~/Public/images/bienvenida-login.png") %>' class="login-logo" />
                        <div>
                            <h3 class="fw-bold">PUCP Qhatu</h3>
                            <h2>¡Bienvenido a Qhatu!</h2>
                            <p class="mb-1">Tu espacio para publicar, buscar y conectar.</p>
                        </div>
                    </div>
                </div>

                <div class="login-formulario col-12 col-md-6 p-5">
                    <div class="card shadow-lg rounded-4 mx-auto p-4" style="max-width: 480px; background-color: #ffffffee;">
                        <ul class="nav nav-pills mb-4 justify-content-center" id="pills-tab" role="tablist">
                            <li class="nav-item">
                                <a class="nav-link active" id="pills-home-tab" data-bs-toggle="pill" href="#pills-home" role="tab" aria-selected="true">Login</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" id="pills-profile-tab" data-bs-toggle="pill" href="#pills-profile" role="tab" aria-selected="false">Signup</a>
                            </li>
                        </ul>

                        <div class="tab-content" id="pills-tabContent">
                            <div class="tab-pane fade show active" id="pills-home" role="tabpanel">
                                <div class="form">
                                    <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control mb-3" placeholder="Correo electrónico"></asp:TextBox>
                                    <asp:TextBox ID="txtContrasena" runat="server" CssClass="form-control mb-3" TextMode="Password" placeholder="Contraseña"></asp:TextBox>
                                    <asp:Button ID="BtnLogin" runat="server" CssClass="btn btn-dark w-100" Text="Iniciar sesión" OnClick="BtnLogin_Click" />
                                    <asp:Label ID="lblError" runat="server" CssClass="text-danger mt-2 d-block"></asp:Label>
                                </div>
                            </div>

                            <div class="tab-pane fade" id="pills-profile" role="tabpanel">
                                <div class="form">
                                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control mb-3" placeholder="Nombre completo"></asp:TextBox>
                                    <asp:TextBox ID="txtCodigoPucp" runat="server" CssClass="form-control mb-3" placeholder="Código PUCP"></asp:TextBox>
                                    <asp:TextBox ID="txtCorreoNuevo" runat="server" CssClass="form-control mb-3" placeholder="Correo electrónico"></asp:TextBox>
                                    <asp:TextBox ID="txtNombreUsuario" runat="server" CssClass="form-control mb-3" placeholder="Nombre de usuario"></asp:TextBox>
                                    <asp:TextBox ID="txtContrasenaNueva" runat="server" CssClass="form-control mb-3" TextMode="Password" placeholder="Contraseña"></asp:TextBox>
                                    <asp:Button ID="BtnSignup" runat="server" CssClass="btn btn-dark w-100" Text="Registrarse" OnClick="BtnSignup_Click" />
                                    <asp:Label ID="lblSignupError" runat="server" CssClass="text-danger mt-2 d-block"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </form>

    <script>
        $(document).ready(function () {
            $('#pills-tab a').on('click', function (e) {
                e.preventDefault();
                $(this).tab('show');
            });
        });
    </script>

    <script>
        $(document).ready(function () {
            $('a[data-bs-toggle="pill"]').on('shown.bs.tab', function (e) {
                var activeTab = $(e.target).attr('href');
                $('#<%= hdnActiveTab.ClientID %>').val(activeTab);
            });

            var lastTab = $('#<%= hdnActiveTab.ClientID %>').val();
            if (lastTab) {
                var trigger = $('a[href="' + lastTab + '"]');
                if (trigger.length) {
                    new bootstrap.Tab(trigger[0]).show();
                }
            }
        });
    </script>
</body>
</html>

