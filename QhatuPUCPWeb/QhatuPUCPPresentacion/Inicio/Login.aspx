<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="QhatuPUCPPresentacion.Inicio.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Login</title>
    <link href="Login.css" rel="stylesheet" type="text/css" />

    <!-- Bootstrap 5 y jQuery -->
    <!-- Bootstrap 5 estable -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <!--Para manejar que no se cambien despues de dar click a registrar-->
        <asp:HiddenField ID="hdnActiveTab" runat="server" />
        <div class="d-flex justify-content-center align-items-center mt-5">
            <div class="card">
                <ul class="nav nav-pills mb-3" id="pills-tab" role="tablist">
                    <li class="nav-item text-center">
                        <a class="nav-link active btl" id="pills-home-tab" data-bs-toggle="pill" href="#pills-home" role="tab" aria-controls="pills-home" aria-selected="true">Login</a>
                    </li>
                    <li class="nav-item text-center">
                        <a class="nav-link btr" id="pills-profile-tab" data-bs-toggle="pill" href="#pills-profile" role="tab" aria-controls="pills-profile" aria-selected="false">Signup</a>
                    </li>
                </ul>

                <div class="tab-content" id="pills-tabContent">
                    <!-- Login -->
                    <div class="tab-pane fade show active" id="pills-home" role="tabpanel" aria-labelledby="pills-home-tab">
                        <div class="form px-4 pt-5">
                            <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control" placeholder="Correo electrónico"></asp:TextBox>
                            <asp:TextBox ID="txtContrasena" runat="server" CssClass="form-control" TextMode="Password" placeholder="Contraseña"></asp:TextBox>
                            <asp:Button ID="BtnLogin" runat="server" CssClass="btn btn-dark btn-block" Text="Login" OnClick="BtnLogin_Click" />
                            <asp:Label ID="lblError" runat="server" CssClass="text-danger mt-2"></asp:Label>
                        </div>
                    </div>

                    <!-- Signup (inactivo por ahora) -->
                    <div class="tab-pane fade" id="pills-profile" role="tabpanel" aria-labelledby="pills-profile-tab">
                        <div class="form px-4 pt-5">
                            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control mb-2" placeholder="Nombre completo"></asp:TextBox>
                            <asp:TextBox ID="txtCodigoPucp" runat="server" CssClass="form-control mb-2" placeholder="Código Pucp"></asp:TextBox>
                            <asp:TextBox ID="txtCorreoNuevo" runat="server" CssClass="form-control mb-2" placeholder="Correo electrónico"></asp:TextBox>
                            <asp:TextBox ID="txtNombreUsuario" runat="server" CssClass="form-control mb-2" placeholder="Nombre de Usuario"></asp:TextBox>
                            <asp:TextBox ID="txtContrasenaNueva" runat="server" CssClass="form-control mb-2" TextMode="Password" placeholder="Contraseña"></asp:TextBox>
        
                            <asp:Button ID="BtnSignup" runat="server" CssClass="btn btn-dark btn-block" Text="Registrarse" OnClick="BtnSignup_Click" />
                            <asp:Label ID="lblSignupError" runat="server" CssClass="text-danger mt-2"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
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
            var activeTab = $(e.target).attr('href'); // '#pills-profile' o '#pills-home'
            $('#<%= hdnActiveTab.ClientID %>').val(activeTab);
        });

        // Restaurar la pestaña activa tras postback
        var lastTab = $('#<%= hdnActiveTab.ClientID %>').val();
        if (lastTab) {
            var trigger = $('a[href="' + lastTab + '"]');
            if (trigger.length) {
                new bootstrap.Tab(trigger[0]).show();
            }
        }
    });
</script>