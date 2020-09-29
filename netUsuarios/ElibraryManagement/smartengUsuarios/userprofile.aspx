<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="userprofile.aspx.cs" Inherits="smartengUsuarios.userprofile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
       $(document).ready(function () {
           $(".table").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable();
       });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
      <div class="row">
         <div class="col-md-5">
            <div class="card">
               <div class="card-body">
                  <div class="row">
                     <div class="col">
                        <center>
                           <asp:Image ID="imagePreview" runat="server" Height="100px"/>
                        </center>
                     </div>
                  </div>
                  <div class="row">
                     <div class="col">
                        <center>
                           <h4><asp:Label ID="lblNombre" runat="server"></asp:Label></h4>
                           <asp:Label class="badge badge-pill badge-info" ID="Label1" runat="server" Text="Your status"></asp:Label>
                        </center>
                     </div>
                  </div>
                  <div class="row">
                     <div class="col">
                        <hr>
                     </div>
                  </div>
                  <div class="row">
                     <div class="col-md-3">
                        <label>Nº Empleado</label>
                        <div class="form-group">
                           <asp:TextBox CssClass="form-control" ID="txtNEmpleado" runat="server" readonly="true"></asp:TextBox>
                        </div>
                     </div>
                      <div class="col-md-9">
                        <label>Nombre completo</label>
                        <div class="form-group">
                           <asp:TextBox CssClass="form-control" ID="txtNombre" runat="server" readonly="true"></asp:TextBox>
                        </div>
                     </div>
                  </div>
                  <div class="row">
                     <div class="col-md-6">
                        <label>Teléfono</label>
                        <div class="form-group">
                           <asp:TextBox CssClass="form-control" ID="txtTelefono" runat="server" TextMode="Number" readonly="true"></asp:TextBox>
                        </div>
                     </div>
                     <div class="col-md-6">
                        <label>E-mail</label>
                        <div class="form-group">
                           <asp:TextBox CssClass="form-control" ID="txtEmail" runat="server" TextMode="Email" readonly="true"></asp:TextBox>
                        </div>
                     </div>
                  </div>
                   <div class="row">
                     <div class="col-md-6">
                        <label>Fecha de nacimiento</label>
                        <div class="form-group">
                           <asp:TextBox CssClass="form-control" ID="txtFechaNac" runat="server" TextMode="Date" readonly="true"></asp:TextBox>
                        </div>
                     </div>
                     <div class="col-md-6">
                        <label>NSS</label>
                        <div class="form-group">
                           <asp:TextBox CssClass="form-control" ID="txtNSS" runat="server" readonly="true"></asp:TextBox>
                        </div>
                     </div>
                  </div>
                  <div class="row">
                     <div class="col">
                        <label>Dirección</label>
                        <div class="form-group">
                           <asp:TextBox CssClass="form-control" ID="txtDireccion" runat="server" TextMode="MultiLine" Rows="2" readonly="true"></asp:TextBox>
                        </div>
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
                     <div class="col-md-4">
                        <label>Usuario</label>
                        <div class="form-group">
                           <asp:TextBox class="form-control" ID="TextBox8" runat="server" ReadOnly="True"></asp:TextBox>
                        </div>
                     </div>
                     <div class="col-md-4">
                        <label>Contraseña antigua</label>
                        <div class="form-group">
                           <asp:TextBox class="form-control" ID="TextBox9" runat="server" ReadOnly="True"></asp:TextBox>
                        </div>
                     </div>
                     <div class="col-md-4">
                        <label>Contraseña nueva</label>
                        <div class="form-group">
                           <asp:TextBox class="form-control" ID="TextBox10" runat="server" TextMode="Password"></asp:TextBox>
                        </div>
                     </div>
                  </div>
                  <div class="row">
                     <div class="col-8 mx-auto">
                        <center>
                           <div class="form-group">
                              <asp:Button class="btn btn-info btn-block btn-lg" ID="Button1" runat="server" Text="Actualizar" OnClick="Button1_Click" />
                           </div>
                        </center>
                     </div>
                  </div>
               </div>
            </div>
         </div>
         <div class="col-md-7">
            <div class="card">
               <div class="card-body">
                  <div class="row">
                     <div class="col">
                        <center>
                           <img width="100px" src="svg/ChecklistNeon.svg"/>
                        </center>
                     </div>
                  </div>
                  <div class="row">
                     <div class="col">
                        <center>
                           <h4>Proyectos</h4>
                        </center>
                     </div>
                  </div>
                  <div class="row">
                     <div class="col">
                        <hr>
                     </div>
                  </div>
                  <div class="row">
                     <div class="col gridviewContenedor">
                        <asp:GridView class="table table-striped table-bordered" ID="GridView1" runat="server" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                            <Columns>
                                <asp:CommandField  ShowSelectButton="true" ButtonType="Button" />
                            </Columns>
                        </asp:GridView>
                     </div>
                  </div>
               </div>
            </div>
         </div>
      </div>
   </div>
</asp:Content>