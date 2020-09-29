<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="services.aspx.cs" Inherits="smartengUsuarios.services" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="animation-area">
        <ul class="box-area">
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
            <li></li>
        </ul>
    </div>
      <div class="row contNosotros">
         <div class="col-md-8" id="contenidoServicios">
             
        </div>
        <div class="col-md-4" id="contImagen">
            <asp:Image ID="image1" runat="server" visible="false" ImageUrl="imgs/threed_mockupAzul.png"/>
            <asp:Image ID="image2" runat="server" visible="false" ImageUrl="imgs/EngranajeAzul.png"/>
            <asp:Image ID="image3" runat="server" visible="false" ImageUrl="imgs/ProgramasCursosAzul.png"/>
            <asp:Image ID="image4" runat="server" visible="false" ImageUrl="imgs/Interfaz2.png"/>
        </div>
        </div>
</asp:Content>
