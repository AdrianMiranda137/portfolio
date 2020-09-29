<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="adminprojectmanagement.aspx.cs" Inherits="smartengUsuarios.adminprojectmanagement" %>
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
                           <h4>ADMINISTRAR PROYECTOS</h4>
                        </center>
                     </div>
                  </div>
                  <div class="row">
                     <div class="col">
                        <center>
                           <asp:Image ID="imagePreview" runat="server" ImageUrl="svg/ChecklistNeon.svg" Height="100px" />
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
                              <asp:TextBox CssClass="form-control" ID="txtId" runat="server"></asp:TextBox>
                              <asp:LinkButton class="btn btn-info" ID="btnIr" runat="server" OnClick="btnIr_Click"><i class="fas fa-check-circle"></i></asp:LinkButton>
                           </div>
                        </div>
                     </div>
                     <div class="col-md-8">
                        <label>Nombre pieza</label>
                        <div class="form-group">
                           <asp:TextBox CssClass="form-control" ID="txtNombre" runat="server"></asp:TextBox>
                        </div>
                     </div>
                  </div>
                   <div class="row">
                     <div class="col-md-6">
                        <label>Empresa</label>
                        <div class="form-group">
                           <asp:DropDownList class="form-control" ID="ddlEmpresa" runat="server" AutoPostBack="True">
                           </asp:DropDownList>
                        </div>
                     </div>
                     <div class="col-md-6">
                        <label>Lider</label>
                        <div class="form-group">
                           <asp:DropDownList class="form-control" ID="ddlLider" runat="server" AutoPostBack="True">
                           </asp:DropDownList>
                        </div>
                     </div>
                  </div>
                   <div class="row">
                      <div class="col-md-6">
                        <label>Fecha de inicio</label>
                        <div class="form-group">
                           <asp:TextBox CssClass="form-control" ID="txtFechaInicio" runat="server" TextMode="Date"></asp:TextBox>
                        </div>
                     </div>
                     <div class="col-md-6">
                        <label>Inspectores</label>
                        <div class="form-group">
                           <asp:TextBox CssClass="form-control" ID="txtInspectores" runat="server" TextMode="Number"></asp:TextBox>
                        </div>
                     </div>
                  </div>
                   <div class="row">
                      <div class="col-md-12">
                        <label>Defectos</label>
                        <div class="form-group">
                           <asp:TextBox CssClass="form-control" ID="txtDefectos" runat="server" TextMode="Multiline"></asp:TextBox>
                        </div>
                     </div>
                  </div>
                  <div class="row">
                      <div class="col-md-6">
                        <label>Fotografía</label>
                        <div class="col">
                            <asp:FileUpload class="form-control" accept=".jpg" ID="FileUpload2" runat="server" />
                        </div>
                     </div>
                    <div class="col-md-6">
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
                        <asp:Button ID="btnAgregar" class="btn btn-lg btn-block btn-success" runat="server" Text="Agregar" OnClick="btnAgregar_Click" />
                     </div>
                      <div class="col-4 mx-auto">
                        <asp:Button ID="btnActualizar" class="btn btn-lg btn-block btn-warning" runat="server" Text="Actualizar" OnClick="btnActualizar_Click" />
                     </div>
                     <div class="col-4 mx-auto">
                        <asp:Button ID="btnEliminar" class="btn btn-lg btn-block btn-danger" runat="server" Text="Eliminar" OnClick="btnEliminar_Click" />
                     </div>
                  </div>
                   <div class="row">
                       <br />
                    </div>
                   <div class="row">
                      <div class="col-6 mx-auto">
                        <asp:Button ID="btnVer" class="btn btn-lg btn-block btn-info" runat="server" Text="Ver" OnClick="btnVer_Click" />
                     </div>
                     <div class="col-6 mx-auto">
                        <asp:Button ID="btnEntrada" class="btn btn-lg btn-block btn-secondary" runat="server" Text="Nueva entrada" OnClick="btnEntrada_Click" />
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
                           <h4>LISTA DE PROYECTOS</h4>
                        </center>
                     </div>
                  </div>
                  <div class="row">
                        <div class="col">
                            <div class="card-body">
                                <div class="row gridviewContenedor">
                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:con %>" SelectCommand="SELECT * FROM [Proyecto]"></asp:SqlDataSource>
                                     <div class="col">
                                        <asp:GridView class="table table-responsive-md table-hover table-bordered" ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="IdProyecto" DataSourceID="SqlDataSource1" BackColor="WhiteSmoke">
                                            <Columns>
                                                <asp:boundfield datafield="IdProyecto"/>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <center>
                                                            <asp:Image ID="Image2" runat="server" height="100px" ImageUrl='<%#"data:image;base64,"+Convert.ToBase64String((byte[])Eval("Fotografia")) %>'/>
                                                            </br>
                                                            <asp:Label ID="Label13" runat="server" Font-Bold="True" Text='<%# Eval("Estado") %>'></asp:Label>
                                                        </center>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:boundfield datafield="Nombre" HeaderText="Nombre"/>
                                                <asp:boundfield datafield="Empresa" HeaderText="Empresa"/>
                                                <asp:boundfield datafield="Lider" HeaderText="Lider"/>
                                                <asp:boundfield datafield="FechaInicio" HeaderText="Inicio"/>
                                                <asp:boundfield datafield="Inspectores" HeaderText="Inspectores"/>
                                </Columns>
                                        </asp:GridView>
                                     </div>
                                </div>
                            </div>
                        </div>
                    </div>
               </div>
            </div>
         </div>
      </div>
   </div>
</asp:Content>