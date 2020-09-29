<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="contact.aspx.cs" Inherits="smartengUsuarios.contact" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
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
                               <img width="150px" src="svg/SobreNeon.svg"/>
                            </center>
                         </div>
                      </div>
                      <div class="row">
                         <div class="col">
                            <center>
                               <h3>CONTACTANOS</h3>
                                <asp:Label ID="lblConfirmacion" runat="server"></asp:Label>
                            </center>
                         </div>
                      </div>
                      <div class="row">
                         <div class="col">
                            <label>Empresa*</label>
                            <div class="form-group">
                               <asp:TextBox CssClass="form-control" ID="txtEmpresa" runat="server"></asp:TextBox>
                            </div>
                            <label>Nombre de contacto*</label>
                            <div class="form-group">
                               <asp:TextBox CssClass="form-control" ID="txtNombre" runat="server"></asp:TextBox>
                            </div>
                             <label>E-mail*</label>
                            <div class="form-group">
                               <asp:TextBox CssClass="form-control" ID="txtEmail" runat="server" TextMode="Email"></asp:TextBox>
                            </div>
                             <label>Telefono*</label>
                            <div class="form-group">
                               <asp:TextBox CssClass="form-control" ID="txtTelefono" runat="server" TextMode="Number"></asp:TextBox>
                            </div>
                             <label>Mensaje*</label>
                            <div class="form-group">
                               <asp:TextBox CssClass="form-control" ID="txtMensaje" runat="server" TextMode="MultiLine" Rows="5"></asp:TextBox>
                            </div>
                            <div class="form-group">
                               <asp:Button class="btn btn-info btn-lg btn-block" ID="btnEnviar" runat="server" Text="Enviar" OnClick="btnEnviar_Click"/>
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
