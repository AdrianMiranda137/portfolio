<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="viewprojects.aspx.cs" Inherits="smartengUsuarios.viewprojects" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(".table").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable();
        });
    </script>
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <!-- Grafica Linea -->
    <script type="text/javascript">
        google.charts.load('current', { 'packages': ['corechart'] });
        google.charts.setOnLoadCallback(drawChart);

        function drawChart() {
            var data = new google.visualization.DataTable();
            var data2 = new google.visualization.DataTable();

            data.addColumn('number', 'NRegistro');
            data.addColumn('number', 'OK');
            data.addColumn('number', 'NG');
            data.addColumn('number', 'RW');

            data2.addColumn('date', 'Fecha');
            data2.addColumn('number', 'OK');
            data2.addColumn('number', 'NG');
            data2.addColumn('number', 'RW');

            data.addRows(<%=this.loadChart(this.selectProjectData())%>);
            data2.addRows(<%=this.loadChart2(this.selectProjectData2())%>);
            
            var options = {
                title: 'Linea del tiempo',
                fontSize: 12,
                series: {
                    0: { color: '#027495' }, 1: { color: '#01A9C1' }, 2: { color: 'BAD6DB' }
                },
                legend: {
                    position: 'bottom',
                    textstyle: {
                        fontSize: 14
                    }
                },
                hAxis: {
                    title: "Num. Registro",
                },
                vAxis: {
                    title:"Piezas inspeccionadas",
                    minValue:0,
                },
                pointShape: "circle",
                pointSize: 10
            };

            var chart = new google.visualization.LineChart(document.getElementById('curve_chart'));
            chart.draw(data, options);

            chart = new google.visualization.LineChart(document.getElementById('curve_chart2'));
            chart.draw(data2, options);
        }
    </script>

    <!-- Grafica Dona -->
    <script type="text/javascript">
        google.charts.load("current", { packages: ["corechart"] });
        google.charts.setOnLoadCallback(drawChart);
        function drawChart() {
            var data = google.visualization.arrayToDataTable(<%=this.loadChartDonut(this.selectProjectData2())%>);

            var options = {
                title: 'Clasificación piezas',
                pieHole: 0.4,
                slices: {
                    0: { color: '#027495' }, 1: { color: '#01A9C1' }, 2: { color: 'BAD6DB' }
                }
            };

            var chart = new google.visualization.PieChart(document.getElementById('donutchart'));
            chart.draw(data, options);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="container-fluid">
      <div class="row">
         <div class="col-md-12 mx-auto">
            <div class="card">
               <div class="card-body">
                  <div class="row" id="headerProject">
                      <div class="col-md-2">
                        </div>
                      <div class="col-md-2">
                        <div class="form-group">
                            <center>
                                <asp:Image ID="imagePreview" runat="server"/>
                            </center>
                        </div>
                     </div>
                      <div class="col-md-3">
                        <center>
                            <h4><asp:Label ID="lblNombre" runat="server"></asp:Label></h4>
                            <asp:Label class="badge badge-pill badge-info" ID="lblEstado" runat="server" Text="Your status"></asp:Label>
                            </br>
                            <label>Empresa: </label> <asp:Label ID="lblEmpresa" runat="server"></asp:Label>
                         </center>
                     </div>
                     <div class="col-md-3">
                         <label>ID: </label> <asp:Label ID="lblId" runat="server"></asp:Label>
                         </br>
                         <label>Fecha Inicio: </label> <asp:Label ID="lblFechaI" runat="server"></asp:Label>
                         </br>
                         <label>Inspectores: </label> <asp:Label ID="lblInspectores" runat="server"></asp:Label>
                         </br>
                         <label>Lider: </label> <asp:Label ID="lblLider" runat="server"></asp:Label>
                     </div>
                  </div>
                   <div class="row">
                        <div class="col-md-8 graphics">
                            <div id="curve_chart"></div>
                        </div>
                       <div class="col-md-4">
                           <div id="estadisticas">
                               <label>Piezas por turno: </label> <asp:Label ID="txtPiezasTurno" runat="server"></asp:Label>
                               </br>
                               <label>Piezas revisadas: </label> <asp:Label ID="lblTotalPiezas" runat="server"></asp:Label>
                               <center>
                                    <label>Fechas con mayor registro</label>
                                   </center>
                               <label>NG: </label> <asp:Label ID="lblDiaMasNG" runat="server"></asp:Label>
                               </br>
                               <label>RW: </label> <asp:Label ID="lblDiaMasRW" runat="server"></asp:Label>
                               </br>
                               <center>
                                    <label>Fechas con menor registro</label>
                                   </center>
                               <label>NG: </label> <asp:Label ID="lblDiaMenosNG" runat="server"></asp:Label>
                               </br>
                               <label>RW: </label> <asp:Label ID="lblDiaMenosRW" runat="server"></asp:Label>
                           </div>
                        </div>
                   </div>
                   <div class="row">
                        <div class="col-md-8">
                            <div id="curve_chart2"></div>
                        </div>
                       
                       <div class="col-md-4">
                           <div id="donutchart"></div>
                       </div>
                   </div>
                  <div class="row">
                      <div class="col-md-12">
                          </br>
                         <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:con %>"></asp:SqlDataSource>
                         <div class="col gridviewContenedor">
                            <asp:GridView class="table table-responsive-md table-hover table-bordered" ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="Fecha" DataSourceID="SqlDataSource1" BackColor="WhiteSmoke">
                                <Columns>
                                    <asp:boundfield datafield="NRegistro"/>
                                    <asp:boundfield datafield="Fecha" HeaderText="Fecha"/>
                                    <asp:boundfield datafield="Hora" HeaderText="Hora"/>
                                    <asp:boundfield datafield="OK" HeaderText="OK"/>
                                    <asp:boundfield datafield="NG" HeaderText="NG"/>
                                    <asp:boundfield datafield="DefectoNG"/>
                                    <asp:boundfield datafield="RW" HeaderText="RW"/>
                                    <asp:boundfield datafield="DefectoRW"/>
                                    <asp:boundfield datafield="Total" HeaderText="Total"/>
                                    <asp:boundfield datafield="Observaciones" HeaderText="Observaciones"/>
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
</asp:Content>