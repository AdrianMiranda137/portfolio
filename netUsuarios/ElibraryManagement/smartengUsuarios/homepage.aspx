<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="homepage.aspx.cs" Inherits="smartengUsuarios.homepage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('#exampleModal').modal('show');
            modalPage('whatsapp.aspx');
        });
    </script>
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
    <div id="seccion1">
        <img id="brain" src="svg/BrainWT.svg"/>
        <h2 id="soluciones">SOLUCIONES</h2>
        <h2 id="inteligentes">INTELIGENTES</h2>
        <div id="carrusel">
                <div id="carouselExampleCaptions" class="carousel slide" data-ride="carousel">
                  <div class="carousel-inner">
                    <div class="carousel-item active" data-interval="5000">
                      <img src="svg/ComputadoraNeon.svg" class="d-block w-100" alt="...">
                      <div class="carousel-caption">
                          <h3>Desarrollo de software</h3>
                      </div>
                    </div>
                    <div class="carousel-item" data-interval="5000">
                      <img src="svg/EngraneNeon.svg" class="d-block w-100" alt="...">
                      <div class="carousel-caption">
                          <h3>Mecanizado y diseño de piezas</h3>
                      </div>
                    </div>
                    <div class="carousel-item" data-interval="5000">
                      <img src="svg/PlanoNeon.svg" class="d-block w-100" alt="...">
                      <div class="carousel-caption">
                          <h3>Elaboración de planos técnicos</h3>
                      </div>
                    </div>
                    <div class="carousel-item" data-interval="5000">
                      <img src="svg/ChecklistNeon.svg" class="d-block w-100" alt="...">
                      <div class="carousel-caption">
                          <h3>Sorteo de calidad</h3>
                      </div>
                    </div>
                  </div>
                </div>
            <h2 id="services">CONOCE TODOS NUESTROS SERVICIOS</h2>
        </div>
    </div>
</asp:Content>
