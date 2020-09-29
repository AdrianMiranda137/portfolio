<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="smartengUsuarios.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css">
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
          <div class="row">
             <div class="col-md-12 mx-auto">
                <div class="card">
                   <div class="card-body">
                      <div class="row">
                         <div class="col">
                            <center>
                               <img width="150px" src="svg/LoginNeon.svg"/>
                            </center>
                         </div>
                      </div>
                      <div class="row">
                         <div class="col">
                            <center>
                               <h3>INICIAR SESION</h3>
                            </center>
                         </div>
                      </div>
                      <div class="row">
                         <div class="col">
                            <label>Usuario</label>
                            <div class="form-group">
                               <asp:TextBox CssClass="form-control" ID="TextBox1" runat="server"></asp:TextBox>
                            </div>
                            <label>Contraseña</label>
                            <div class="form-group">
                               <asp:TextBox CssClass="form-control" ID="TextBox2" runat="server" TextMode="Password"></asp:TextBox>
                            </div>
                            <div class="form-group">
                               <asp:Button class="btn btn-info btn-lg btn-block" ID="btnLogin" runat="server" Text="Entrar" OnClick="btnLogin_Click" />
                            </div>
                         </div>
                      </div>
                   </div>
                </div>
             </div>
          </div>
       </div>
    </form>
</body>
</html>
