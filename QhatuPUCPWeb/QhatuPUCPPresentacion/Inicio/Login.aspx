<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="QhatuPUCPPresentacion.Inicio.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Login</title>
    <link href="Login.css" rel="stylesheet" type="text/css" />

    <!-- Bootstrap 5 y jQuery -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-alpha1/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-alpha1/dist/js/bootstrap.bundle.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
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
                        <div class="form px-4">
                            <input type="text" class="form-control" placeholder="Name" />
                            <input type="text" class="form-control" placeholder="Email" />
                            <input type="text" class="form-control" placeholder="Phone" />
                            <input type="text" class="form-control" placeholder="Password" />
                            <button class="btn btn-dark btn-block">Signup</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
