<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="admincompanymanagement.aspx.cs" Inherits="smartengUsuarios.admincompanymanagement" %>
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
                           <h4>ADMINISTRAR EMPRESAS</h4>
                        </center>
                     </div>
                  </div>
                  <div class="row">
                     <div class="col">
                        <center>
                           <img height="100px" src="svg/EmpresaNeon.svg" />
                        </center>
                     </div>
                  </div>
                  <div class="row">
                     <div class="col">
                        <hr>
                     </div>
                  </div>
                  <div class="row">
                     <div class="col-md-4">
                        <label>ID</label>
                        <div class="form-group">
                           <div class="input-group">
                              <asp:TextBox CssClass="form-control" ID="TextBox1" runat="server"></asp:TextBox>
                              <asp:LinkButton class="btn btn-info" ID="btnIr" runat="server" OnClick="btnIr_Click"><i class="fas fa-check-circle"></i></asp:LinkButton>
                           </div>
                        </div>
                     </div>
                     <div class="col-md-8">
                        <label>Nombre</label>
                        <div class="form-group">
                           <asp:TextBox CssClass="form-control" ID="TextBox2" runat="server"></asp:TextBox>
                        </div>
                     </div>
                  </div>
                   <div class="row">
                     <div class="col-md-12">
                        <label>Nombre de contacto</label>
                        <div class="form-group">
                           <div class="input-group">
                              <asp:TextBox CssClass="form-control" ID="txtNombreContacto" runat="server"></asp:TextBox>
                           </div>
                        </div>
                     </div>
                  </div>
                   <div class="row">
                     <div class="col-md-6">
                        <label>Telefono</label>
                        <div class="form-group">
                           <div class="input-group">
                              <asp:TextBox CssClass="form-control" ID="txtTelefono" runat="server"></asp:TextBox>
                           </div>
                        </div>
                     </div>
                     <div class="col-md-6">
                        <label>Email</label>
                        <div class="form-group">
                           <asp:TextBox CssClass="form-control" ID="txtEmail" runat="server"></asp:TextBox>
                        </div>
                     </div>
                  </div>
                   <div class="row">
                     <div class="col-md-12">
                        <label>Dirección</label>
                        <div class="form-group">
                           <div class="input-group">
                              <asp:TextBox CssClass="form-control" ID="txtDireccion" runat="server"></asp:TextBox>
                           </div>
                        </div>
                     </div>
                  </div>
                  <div class="row">
                    <div class="col-md-12">
                    <label>Estado</label>
                    <div class="form-group">
                        <div class="input-group">
                            <asp:TextBox CssClass="form-control mr-1" ID="TextBox7" runat="server" ReadOnly="True"></asp:TextBox>
                            <asp:LinkButton class="btn btn-success mr-1" ID="btnActivar" runat="server" OnClick="btnActivar_Click"><i class="fas fa-check-circle"></i></asp:LinkButton>
                            <asp:LinkButton class="btn btn-warning mr-1" ID="btnPausar" runat="server" OnClick="btnPausar_Click"><i class="far fa-pause-circle"></i></asp:LinkButton>
                            <asp:LinkButton class="btn btn-danger mr-1" ID="btnDesactivar" runat="server" OnClick="btnDesactivar_Click"><i class="fas fa-times-circle"></i></asp:LinkButton>
                        </div>
                    </div>
                    </div>
                  </div>
                  <div class="row">
                     <div class="col-4 mx-auto">
                        <asp:Button ID="btnEliminar" class="btn btn-lg btn-block btn-danger" runat="server" Text="Eliminar" OnClick="btnEliminar_Click" />
                     </div>
                      <div class="col-4 mx-auto">
                        <asp:Button ID="btnActualiza" class="btn btn-lg btn-block btn-warning" runat="server" Text="Actualizar" OnClick="btnActualiza_Click" />
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
                           <h4>LISTA DE EMPRESAS</h4>
                        </center>
                     </div>
                  </div>
                  <div class="row gridviewContenedor">
                     <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:con %>" SelectCommand="SELECT * FROM [Empresa]"></asp:SqlDataSource>
                     <div class="col">
                        <asp:GridView class="table table-responsive-sm table-hover table-bordered" ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="IdEmpresa" DataSourceID="SqlDataSource1" BackColor="WhiteSmoke">
                            <Columns>
                                <asp:boundfield datafield="IdEmpresa"/>
                                <asp:boundfield datafield="Nombre" HeaderText="Nombre"/>
                                <asp:boundfield datafield="NombreContacto" HeaderText="Contacto"/>
                                <asp:boundfield datafield="Telefono" HeaderText="Telefono"/>
                                <asp:boundfield datafield="Estado" HeaderText="Estado"/>
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