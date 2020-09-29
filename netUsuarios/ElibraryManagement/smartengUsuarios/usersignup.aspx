<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="usersignup.aspx.cs" Inherits="smartengUsuarios.usersignup1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid">
          <div class="row">
             <div class="col-md-12 mx-auto">
                <div class="card">
                   <div class="card-body">
                      <div class="row">
                         <div class="col">
                            <center>
                               <asp:Image ID="imagePreview" runat="server" ImageUrl="svg/CascoNeon.svg" Height="100px"/>
                            </center>
                         </div>
                      </div>
                      <div class="row">
                         <div class="col">
                            <center>
                               <h4>REGISTRAR EMPLEADO</h4>
                             </center>
                         </div>
                      </div>
                      <div class="row">
                         <div class="col">
                            <hr>
                         </div>
                      </div>
                      <div class="row">
                         <div class="col-md-6">
                            <label>Nombre completo</label>
                            <div class="form-group">
                               <asp:TextBox CssClass="form-control" ID="TextBox1" runat="server" ></asp:TextBox>
                            </div>
                         </div>
                         <div class="col-md-6">
                            <label>Fecha de nacimiento</label>
                            <div class="form-group">
                               <asp:TextBox CssClass="form-control" ID="TextBox2" runat="server" TextMode="Date"></asp:TextBox>
                            </div>
                         </div>
                      </div>
                      <div class="row">
                         <div class="col-md-6">
                            <label>Teléfono</label>
                            <div class="form-group">
                               <asp:TextBox CssClass="form-control" ID="TextBox3" runat="server" TextMode="Number"></asp:TextBox>
                            </div>
                         </div>
                         <div class="col-md-6">
                            <label>E-mail</label>
                            <div class="form-group">
                               <asp:TextBox CssClass="form-control" ID="TextBox4" runat="server" TextMode="Email"></asp:TextBox>
                            </div>
                         </div>
                      </div>
                       <div class="row">
                         <div class="col-md-6">
                            <label>NSS</label>
                            <div class="form-group">
                               <asp:TextBox CssClass="form-control" ID="TextBox6" runat="server" TextMode="Number"></asp:TextBox>
                            </div>
                         </div>
                         <div class="col-md-6">
                            <label>Fotografía</label>
                            <div class="col">
                                <asp:FileUpload class="form-control" accept=".jpg" ID="FileUpload2" runat="server" OnDataBinding="FileUpload2_DataBinding" />
                            </div>
                         </div>
                      </div>
                      <div class="row">
                         <div class="col-md-6">
                            <label>Dirección</label>
                            <div class="form-group">
                               <asp:TextBox CssClass="form-control" ID="TextBox5" runat="server" TextMode="MultiLine" Rows="2"></asp:TextBox>
                            </div>
                         </div>
                        <div class="col-md-6">
                        </div>
                      </div>
                      <div class="row">
                         <div class="col">
                            <center>
                               <span class="badge badge-pill badge-info">Login Credentials</span>
                            </center>
                         </div>
                      </div>
                      <div class="row">
                         <div class="col-md-6">
                            <label>Usuario</label>
                            <div class="form-group">
                               <asp:TextBox class="form-control" ID="TextBox8" runat="server"></asp:TextBox>
                            </div>
                         </div>

                         <div class="col-md-6">
                            <label>Contraseña</label>
                            <div class="form-group">
                               <asp:TextBox class="form-control" ID="TextBox10" runat="server" TextMode="Password"></asp:TextBox>
                            </div>
                         </div>
                      </div>
                      <div class="row">
                         <div class="col-8 mx-auto">
                            <center>
                               <div class="form-group">
                                  <asp:Button class="btn btn-info btn-block btn-lg" ID="Button1" runat="server" Text="Registrar" OnClick="Button1_Click1" />
                               </div>
                            </center>
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
