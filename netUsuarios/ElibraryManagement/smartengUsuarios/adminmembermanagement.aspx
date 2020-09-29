<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="adminmembermanagement.aspx.cs" Inherits="smartengUsuarios.adminmembermanagement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(".table").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid" id="formAdminMemeber">
      <div class="row">
         <div class="col-md-12">
            <div class="card">
               <div class="card-body">
                  <div class="row">
                     <div class="col">
                        <center>
                           <h4>ADMINISTRAR EMPLEADOS</h4>
                        </center>
                     </div>
                  </div>
                  <div class="row">
                     <div class="col">
                        <center>
                           <asp:Image ID="imagePreview" runat="server" Height="100px" ImageUrl="svg/CascoNeon.svg"/>
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
                        <label>Num. Emp.</label>
                        <div class="form-group">
                           <div class="input-group">
                              <asp:TextBox CssClass="form-control" ID="TextBox1" runat="server"></asp:TextBox>
                              <asp:LinkButton class="btn btn-info" ID="LinkButton4" runat="server" OnClick="LinkButton4_Click"><i class="fas fa-check-circle"></i></asp:LinkButton>
                           </div>
                        </div>
                     </div>
                     <div class="col-md-9">
                        <label>Nombre completo</label>
                        <div class="form-group">
                           <asp:TextBox CssClass="form-control" ID="TextBox2" runat="server"></asp:TextBox>
                        </div>
                     </div>
                  </div>
                   <div class="row">
                     <div class="col-md-3">
                        <label>Fecha de nacimiento.</label>
                        <div class="form-group">
                           <div class="input-group">
                              <asp:TextBox CssClass="form-control" ID="txtFechaNac" runat="server" TextMode="Date"></asp:TextBox>
                           </div>
                        </div>
                     </div>
                     <div class="col-md-3">
                        <label>NSS</label>
                        <div class="form-group">
                           <asp:TextBox CssClass="form-control" ID="txtNss" runat="server"></asp:TextBox>
                        </div>
                     </div>
                     <div class="col-md-3">
                        <label>Telefono</label>
                        <div class="form-group">
                           <div class="input-group">
                              <asp:TextBox CssClass="form-control" ID="txtTelefono" runat="server"></asp:TextBox>
                           </div>
                        </div>
                     </div>
                     <div class="col-md-3">
                        <label>Email</label>
                        <div class="form-group">
                           <asp:TextBox CssClass="form-control" ID="txtEmail" runat="server"></asp:TextBox>
                        </div>
                     </div>
                  </div>
                   <div class="row">
                     <div class="col-md-3">
                        <label>Dirección</label>
                        <div class="form-group">
                           <div class="input-group">
                              <asp:TextBox CssClass="form-control" ID="txtDireccion" runat="server" TextMode="MultiLine"></asp:TextBox>
                           </div>
                        </div>
                     </div>
                       <div class="col-md-3">
                     </div>
                      <div class="col-md-3">
                        <label>Fotografía</label>
                        <div class="col">
                            <asp:FileUpload class="form-control" accept=".jpg" ID="FileUpload2" runat="server" />
                        </div>
                     </div>
                    <div class="col-md-3">
                    <label>Estado</label>
                    <div class="form-group">
                        <div class="input-group">
                            <asp:TextBox CssClass="form-control mr-1" ID="TextBox7" runat="server" ReadOnly="True"></asp:TextBox>
                            <asp:LinkButton class="btn btn-success mr-1" ID="LinkButton2" runat="server" OnClick="LinkButton2_Click"><i class="fas fa-check-circle"></i></asp:LinkButton>
                            <asp:LinkButton class="btn btn-warning mr-1" ID="LinkButton3" runat="server" OnClick="LinkButton3_Click"><i class="far fa-pause-circle"></i></asp:LinkButton>
                            <asp:LinkButton class="btn btn-danger mr-1" ID="LinkButton5" runat="server" OnClick="LinkButton5_Click"><i class="fas fa-times-circle"></i></asp:LinkButton>
                        </div>
                    </div>
                    </div>
                  </div>
                  <div class="row">
                      <div class="col-3">
                          </div>
                     <div class="col-3">
                        <asp:Button ID="Button2" class="btn btn-lg btn-block btn-danger" runat="server" Text="Eliminar" OnClick="Button2_Click" />
                     </div>
                      <div class="col-3">
                        <asp:Button ID="btnActualizar" class="btn btn-lg btn-block btn-warning" runat="server" Text="Actualizar" OnClick="btnActualizar_Click" />
                     </div>
                  </div>
               </div>
            </div>
         </div>
          </div>
          </div>
    <div class="container-fluid">
      <div class="row">
         <div class="col-md-12">
            <div class="card">
               <div class="card-body">
                  <div class="row">
                     <div class="col">
                        <center>
                           <h4>LISTA DE EMPLEADOS</h4>
                        </center>
                     </div>
                  </div>
                  <div class="row gridviewContenedor">
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:con %>" SelectCommand="SELECT * FROM [Empleado] WHERE NEmpleado>2"></asp:SqlDataSource>
                            <div class="col" id="tablaEmpleados">
                            <asp:GridView class="table table-responsive-md table-hover table-bordered" ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="NEmpleado" DataSourceID="SqlDataSource1" BackColor="WhiteSmoke">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <center>
                                                <asp:Image ID="Image2" runat="server" height="100px" ImageUrl='<%#"data:image;base64,"+Convert.ToBase64String((byte[])Eval("Fotografia")) %>'/>
                                                <br>
                                                </br>
                                                <asp:Label ID="Label13" runat="server" Font-Bold="True" Text='<%# Eval("Estado") %>'></asp:Label>
                                            </center>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:boundfield datafield="NEmpleado" HeaderText="Nº"/>
                                    <asp:boundfield datafield="Nombre" HeaderText="Nombre"/>
                                    <asp:boundfield datafield="Rol" HeaderText="Puesto"/>
                                    <asp:boundfield datafield="FechaRegistro" HeaderText="Registro"/>
                                    <asp:boundfield datafield="Telefono" HeaderText="Telefono"/>
                                    <asp:boundfield datafield="Email" HeaderText="Email"/>
                                    <asp:boundfield datafield="FechaNac" HeaderText="Nacimiento"/>
                                    <asp:boundfield datafield="NSS" HeaderText="NSS"/>
                                    <asp:boundfield datafield="Direccion" HeaderText="Dirección"/>
                                </Columns>
                                <FooterStyle ForeColor="WhiteSmoke" />
                            </asp:GridView>
                        </div>
                    </div>
               </div>
            </div>
         </div>
      </div>
   </div>
</asp:Content>