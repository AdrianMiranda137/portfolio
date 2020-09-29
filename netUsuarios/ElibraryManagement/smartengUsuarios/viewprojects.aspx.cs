using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace smartengUsuarios
{
    public partial class viewprojects : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            string tabla = "Proyecto"+Request.QueryString["project"];
            string sessionrole = Session["role"] as string;
            if (!(sessionrole == "admin" || sessionrole == "company") || Request.QueryString["project"] == "")
            {
                Response.Redirect("homepage.aspx");
            }
            else
            {
                try
                {
                    SqlConnection con = new SqlConnection(strcon);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("SELECT * from Proyecto where IdProyecto='" + Request.QueryString["project"] + "';", con);
                    SqlCommand cmd2 = new SqlCommand("SELECT Fotografia from Proyecto where IdProyecto='" + Request.QueryString["project"] + "'", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    lblId.Text = dt.Rows[0]["IdProyecto"].ToString();
                    lblNombre.Text = dt.Rows[0]["Nombre"].ToString();
                
                    lblFechaI.Text = dt.Rows[0]["FechaInicio"].ToString();
                    lblInspectores.Text = dt.Rows[0]["Inspectores"].ToString();

                    SqlCommand cmd3 = new SqlCommand("SELECT Nombre from Empleado where NEmpleado="+ dt.Rows[0]["Lider"].ToString() + ";", con);
                    SqlCommand cmd4 = new SqlCommand("SELECT Nombre from Empresa where IdEmpresa=" + dt.Rows[0]["Empresa"].ToString() + ";", con);
                    SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
                    SqlDataAdapter da4 = new SqlDataAdapter(cmd4);
                    DataTable dt3 = new DataTable();
                    DataTable dt4 = new DataTable();
                    da3.Fill(dt3);
                    da4.Fill(dt4);

                    lblLider.Text = dt3.Rows[0]["Nombre"].ToString();
                    lblEmpresa.Text = dt4.Rows[0]["Nombre"].ToString();

                    byte[] img = (byte[])cmd2.ExecuteScalar();

                    if (img != null && img.Length > 0)
                    {
                        imagePreview.ImageUrl = "data:image;base64," + Convert.ToBase64String(img);
                    }

                    lblEstado.Text = dt.Rows[0]["Estado"].ToString().Trim();

                    if (dt.Rows[0]["Estado"].ToString().Trim() == "active")
                    {
                    lblEstado.Attributes.Add("class", "badge badge-pill badge-success");
                    }
                    else if (dt.Rows[0]["Estado"].ToString().Trim() == "pending")
                    {
                    lblEstado.Attributes.Add("class", "badge badge-pill badge-warning");
                    }
                    else if (dt.Rows[0]["Estado"].ToString().Trim() == "deactive")
                    {
                    lblEstado.Attributes.Add("class", "badge badge-pill badge-danger");
                    }
                    else
                    {
                    lblEstado.Attributes.Add("class", "badge badge-pill badge-info");
                    }

                    loadStadistics(selectProjectData2());

                    SqlDataSource1.SelectCommand = "SELECT * FROM " + tabla;
                    SqlDataSource1.Select(DataSourceSelectArguments.Empty);
                    SqlDataSource1.DataBind();
                    GridView1.DataBind();

                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('" + ex.Message + "');</script>");
                }
            }
        }

        public string loadChart(System.Data.DataTable Datos)
        {
            string strDatos;
            strDatos = "[";

            foreach (DataRow dr in Datos.Rows)
            {
                strDatos += "[";
                strDatos += "" + dr[0].ToString() + "" + "," +
                    dr[1].ToString().Replace(",", ".") + "," +
                    dr[2].ToString().Replace(",", ".") + "," +
                    dr[3].ToString().Replace(",", ".");

                strDatos += "],";
            }

            strDatos += "]";

            return strDatos;
        }

        public string loadChart2(System.Data.DataTable Datos)
        {
            DateTime fecha;
            string strFecha;

            for (int x = 0; x < Datos.Rows.Count; x++)
            {
                fecha = Convert.ToDateTime(Datos.Rows[x]["Fecha"].ToString()).AddMonths(-1); ;
                strFecha = fecha.ToString("yyyy, MM, dd").Substring(0,12);
                Datos.Rows[x]["Fecha"] = "new Date("+strFecha+")";
            }

            string strDatos;
            strDatos = "[";

            foreach (DataRow dr in Datos.Rows)
            {
                strDatos += "[";
                strDatos += "" + dr[0].ToString() + "" + "," +
                    dr[1].ToString().Replace(",", ".") + "," +
                    dr[2].ToString().Replace(",", ".") + "," +
                    dr[3].ToString().Replace(",", ".");

                strDatos += "],";
            }

            strDatos = strDatos.Substring(0,strDatos.Length-1);
            strDatos += "]";

            return strDatos;
        }

        public DataTable selectProjectData()
        {
            DataTable Datos = new DataTable();
            SqlConnection con = new SqlConnection(strcon);

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand cmd = new SqlCommand("SELECT NRegistro, OK, NG, RW, Total from Proyecto" + Request.QueryString["project"] + " WHERE Turno != 'I';", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(Datos);

            return Datos;
        }

        public DataTable selectProjectData2()
        {
            DataTable Datos = new DataTable();
            SqlConnection con = new SqlConnection(strcon);

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand cmd = new SqlCommand("SELECT Fecha, OK, NG, RW, Total from Proyecto" + Request.QueryString["project"] + " WHERE Turno = 'I';", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(Datos);
            
            return Datos;
        }

        public void loadStadistics(DataTable Datos)
        {
            int menorNG = 0;
            int menorRW = 0;
            int mayorNG = 0;
            int mayorRW = 0;

            for (int x = 1; x < Datos.Rows.Count; x++)
            {
                if (Convert.ToInt32(Datos.Rows[x]["NG"].ToString()) < Convert.ToInt32(Datos.Rows[menorNG]["NG"].ToString()))
                    menorNG = x;
                if (Convert.ToInt32(Datos.Rows[x]["RW"].ToString()) < Convert.ToInt32(Datos.Rows[menorRW]["RW"].ToString()))
                    menorRW = x;
                if (Convert.ToInt32(Datos.Rows[x]["NG"].ToString()) > Convert.ToInt32(Datos.Rows[mayorNG]["NG"].ToString()))
                    mayorNG = x;
                if (Convert.ToInt32(Datos.Rows[x]["RW"].ToString()) > Convert.ToInt32(Datos.Rows[mayorRW]["RW"].ToString()))
                    mayorRW = x;
            }

            lblDiaMenosNG.Text = Datos.Rows[menorNG]["Fecha"].ToString();
            lblDiaMenosRW.Text = Datos.Rows[menorRW]["Fecha"].ToString();
            lblDiaMasNG.Text = Datos.Rows[mayorNG]["Fecha"].ToString();
            lblDiaMasRW.Text = Datos.Rows[mayorRW]["Fecha"].ToString();
        }

        public string loadChartDonut(DataTable Datos)
        {
            int total=0, ok = 0, ng = 0, rw = 0;
            string strDatos;

            for (int x=0; x<Datos.Rows.Count; x++)
            {
                ok += Convert.ToInt32(Datos.Rows[x]["OK"].ToString());
                ng += Convert.ToInt32(Datos.Rows[x]["NG"].ToString());
                rw += Convert.ToInt32(Datos.Rows[x]["RW"].ToString());
                total += Convert.ToInt32(Datos.Rows[x]["Total"].ToString());
            }

            strDatos = "[" +
                "['Categoria', 'Piezas']," +
                "['OK', "+ ok.ToString() +"]," +
                "['RW', " + rw.ToString() + "]," +
                "['NG', " + ng.ToString() + "]," +
                "]";

            txtPiezasTurno.Text = (total / Datos.Rows.Count).ToString();
            lblTotalPiezas.Text = total.ToString();

            return strDatos;
        }
    }
}