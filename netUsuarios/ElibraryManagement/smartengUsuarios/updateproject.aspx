<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="updateproject.aspx.cs" Inherits="smartengUsuarios.updateproject" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
      <div class="row">
         <div class="col-md-3">
            <div class="card">
               <div class="card-body">
                  <div class="row" id="headerProject2">
                      <div class="col-md-12">
                        <div class="form-group">
                            <center>
                                <asp:Image ID="imagePreview" runat="server"/>
                            </center>
                        </div>
                     </div>
                      <div class="col-md-12">
                        <center>
                            <h4><asp:Label ID="lblNombre" runat="server"></asp:Label></h4>
                            <asp:Label class="badge badge-pill badge-info" ID="lblEstado" runat="server" Text="Your status"></asp:Label>
                         </center>
                     </div>
                     <div class="col-md-12">
                        <label>Empresa: </label> <asp:Label ID="lblEmpresa" runat="server"></asp:Label>
                        </br>
                        <label>ID: </label> <asp:Label ID="lblId" runat="server"></asp:Label>
                         </br>
                         <label>Fecha Inicio: </label> <asp:Label ID="lblFechaI" runat="server"></asp:Label>
                         </br>
                         <label>Inspectores: </label> <asp:Label ID="lblInspectores" runat="server"></asp:Label>
                         </br>
                         <label>Lider: </label> <asp:Label ID="lblLider" runat="server"></asp:Label>
                     </div>
                  </div>
                   </div>
                </div>
             </div>
          <div class="col-md-9">
            <div class="card">
               <div class="card-body">
                   <div class="row">
                     <div class="col-md-4">
                        <label>OK</label>
                        <div class="form-group">
                           <asp:TextBox CssClass="form-control" ID="txtOK" runat="server" textmode="number"></asp:TextBox>
                        </div>
                     </div>
                       <div class="col-md-4">
                        <label>NG</label>
                        <div class="form-group">
                           <asp:TextBox CssClass="form-control" ID="txtNG" runat="server" textmode="number"></asp:TextBox>
                        </div>
                           <label>Defecto:</label>
                        <div class="form-group">
                           <asp:DropDownList class="form-control" ID="ddlNG" runat="server" AutoPostBack="True">
                           </asp:DropDownList>
                        </div>
                     </div>
                       <div class="col-md-4">
                        <label>RW</label>
                        <div class="form-group">
                           <asp:TextBox CssClass="form-control" ID="txtRW" runat="server" textmode="number"></asp:TextBox>
                        </div>
                           <label>Defecto:</label>
                        <div class="form-group">
                           <asp:DropDownList class="form-control" ID="ddlRW" runat="server" AutoPostBack="True">
                           </asp:DropDownList>
                        </div>
                     </div>
                  </div>
                   <div class="row">
                       <div class="col-md-12">
                        <label>Observaciones</label>
                        <div class="form-group">
                           <asp:TextBox CssClass="form-control" ID="txtObservaciones" runat="server" textmode="MultiLine"></asp:TextBox>
                        </div>
                     </div>
                   </div>
                   <div class="row">
                       <div class="col-md-4">
                            <div class="form-group">
                                <asp:Button class="btn btn-info btn-block btn-lg" ID="btnAgregar" runat="server" Text="Agregar Entrada" OnClick="btnAgregar_Click" />
                            </div>
                        </div>
                       <div class="col-md-4">
                            <div class="form-group">
                                <asp:Button class="btn btn-success btn-block btn-lg" ID="btnIniciar" runat="server" Text="Iniciar turno" OnClick="btnIniciar_Click"/>
                            </div>
                        </div>
                       <div class="col-md-4">
                            <div class="form-group">
                                <asp:Button class="btn btn-danger btn-block btn-lg" ID="btnFinalizar" runat="server" Text="Finalizar turno" OnClick="btnFinalizar_Click"/>
                            </div>
                        </div>
                    </div>
            </div>
         </div>
      </div>
   </div>
        </div>
</asp:Content>
