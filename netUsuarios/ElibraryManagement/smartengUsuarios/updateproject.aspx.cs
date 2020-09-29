using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Schema;

namespace smartengUsuarios
{
    public partial class updateproject : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            string tabla = "Proyecto" + Request.QueryString["project"];
            string sessionrole = Session["role"] as string;

            if ((sessionrole != "admin" && sessionrole != "lider") || Request.QueryString["project"] == "")
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

                    SqlCommand cmd = new SqlCommand("select * from Proyecto WHERE IdProyecto=" + Request.QueryString["project"], con);
                    SqlCommand cmd2 = new SqlCommand("SELECT Fotografia from Proyecto where IdProyecto='" + Request.QueryString["project"] + "'", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    lblId.Text = dt.Rows[0]["IdProyecto"].ToString();
                    lblNombre.Text = dt.Rows[0]["Nombre"].ToString();

                    lblFechaI.Text = dt.Rows[0]["FechaInicio"].ToString();
                    lblInspectores.Text = dt.Rows[0]["Inspectores"].ToString();

                    string defectos = dt.Rows[0]["Defectos"].ToString();
                    int nDefectos = 0;

                    foreach (char c in defectos)
                    {
                        if(c==',')
                        {
                            nDefectos++;
                        }
                    }

                    DataTable tableDefectos = new DataTable();

                    SqlCommand cmd3 = new SqlCommand("SELECT Nombre from Empleado where NEmpleado=" + dt.Rows[0]["Lider"].ToString() + ";", con);
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

                    MostrarDefectos();
                    EstadoTurno();
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('" + ex.Message + "');</script>");
                }
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            addNewProjectEntry("A");
        }

        void addNewProjectEntry(string Turno)
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                string tabla = "Proyecto" + Request.QueryString["project"];
                int total;

                total = Convert.ToInt32(txtOK.Text) + Convert.ToInt32(txtNG.Text) + Convert.ToInt32(txtRW.Text);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("INSERT INTO "+tabla+ "(Fecha,Hora,OK,NG,DefectoNG,RW,DefectoRW,Total,Ins,Observaciones,Turno) values(@Fecha,@Hora,@OK,@NG,@DefectoNG,@RW,@DefectoRW,@Total,@Ins,@Observaciones,@Turno)", con);
                cmd.Parameters.AddWithValue("@Fecha", (DateTime.Today.Day.ToString()+ '/' + DateTime.Today.Month.ToString() + '/' + DateTime.Today.Year.ToString()));
                cmd.Parameters.AddWithValue("@Hora", (DateTime.Now.Hour.ToString()+':'+ DateTime.Now.Minute.ToString()));
                cmd.Parameters.AddWithValue("@OK", txtOK.Text.Trim());
                cmd.Parameters.AddWithValue("@NG", txtNG.Text.Trim());
                cmd.Parameters.AddWithValue("@DefectoNG", ddlNG.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@RW", txtRW.Text.Trim());
                cmd.Parameters.AddWithValue("@DefectoRW", ddlRW.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@Total", total.ToString());
                cmd.Parameters.AddWithValue("@Ins", lblInspectores.Text.Trim());
                cmd.Parameters.AddWithValue("@Observaciones", txtObservaciones.Text.Trim());
                cmd.Parameters.AddWithValue("@Turno", Turno.Trim());
                cmd.ExecuteNonQuery();

                SqlCommand cmd2 = new SqlCommand("SELECT TOP 1 * from Proyecto ORDER BY IdProyecto DESC", con);
                SqlDataReader dr = cmd2.ExecuteReader();

                con.Close();
                Response.Write("<script>alert('Entrada añadida exitosamente');</script>");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }

            clearForm();
        }

        void clearForm()
        {
            txtOK.Text = "";
            txtNG.Text = "";
            txtRW.Text = "";
            txtObservaciones.Text = "";
        }

        void MostrarDefectos()
        {
            try
            {
                string defectos;
                int nDefectos = 0;
                DataTable dtDefectos = new DataTable();

                SqlConnection con = new SqlConnection(strcon);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();

                }

                SqlCommand cmd = new SqlCommand("select * from Proyecto WHERE IdProyecto=" + Request.QueryString["project"], con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                defectos = dt.Rows[0]["Defectos"].ToString()+",..";

                foreach (char c in defectos)
                {
                    if (c == ',')
                    {
                        nDefectos++;
                    }
                }

                dtDefectos.Columns.Add("Defectos",typeof(string));
                dtDefectos.Columns.Add("idDefecto", typeof(int));

                for(int x=1; x<=nDefectos; x++)
                {
                    dtDefectos.Rows.Add(defectos.Substring(0,defectos.IndexOf(',',0,defectos.Length)),x);
                    defectos = defectos.Substring(defectos.IndexOf(',', 0, defectos.Length)+2);
                }
                if (!Page.IsPostBack)
                {
                    ddlNG.DataSource = dtDefectos;
                    ddlNG.DataTextField = "Defectos";
                    ddlNG.DataValueField = "idDefecto";
                    ddlNG.DataBind();

                    ddlRW.DataSource = dtDefectos;
                    ddlRW.DataTextField = "Defectos";
                    ddlRW.DataValueField = "idDefecto";
                    ddlRW.DataBind();
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        void EstadoTurno()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();

                }

                SqlCommand cmd = new SqlCommand("SELECT TOP 1 * FROM  Proyecto" + Request.QueryString["project"] + " ORDER BY NRegistro DESC", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if(dt.Rows[0]["Turno"].ToString()!="F")
                {
                    btnAgregar.Visible = true;
                    btnFinalizar.Visible = true;
                    btnIniciar.Visible = false;
                }
                else
                {
                    btnAgregar.Visible = false;
                    btnFinalizar.Visible = false;
                    btnIniciar.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void btnIniciar_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                string tabla = "Proyecto" + Request.QueryString["project"];

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("INSERT INTO " + tabla + "(Fecha,Hora,Turno) values(@Fecha,@Hora,'I');", con);
                cmd.Parameters.AddWithValue("@Fecha", (DateTime.Today.Day.ToString() + "/" + DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString()));
                cmd.Parameters.AddWithValue("@Hora", (DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString()));
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script>alert('Turno iniciado');</script>");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
            clearForm();
        }

        protected void btnFinalizar_Click(object sender, EventArgs e)
        {
            addNewProjectEntry("F");

            try
            {
                SqlConnection con = new SqlConnection(strcon);
                string tabla = "Proyecto" + Request.QueryString["project"];
                int ok=0, ng=0, rw=0, total=0;

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd1 = new SqlCommand("SELECT TOP 1 * FROM Proyecto33 WHERE Turno='I' ORDER BY NRegistro DESC", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd1);
                DataTable dt = new DataTable();
                da.Fill(dt);

                SqlCommand cmd2 = new SqlCommand("SELECT * FROM " + tabla + " WHERE NRegistro>"+ dt.Rows[0]["NRegistro"].ToString(), con);
                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);

                for (int x=0;x<dt2.Rows.Count;x++)
                {
                    ok += Convert.ToInt32(dt2.Rows[x]["OK"].ToString());
                    ng += Convert.ToInt32(dt2.Rows[x]["NG"].ToString());
                    rw += Convert.ToInt32(dt2.Rows[x]["RW"].ToString());
                    total += Convert.ToInt32(dt2.Rows[x]["Total"].ToString());
                }

                SqlCommand cmd = new SqlCommand("UPDATE " + tabla + " SET OK=@OK, NG=@NG, RW=@RW, Total=@Total WHERE NRegistro=" + dt.Rows[0]["NRegistro"].ToString(), con);
                cmd.Parameters.AddWithValue("@OK", ok.ToString().Trim());
                cmd.Parameters.AddWithValue("@NG", ng.ToString().Trim());
                cmd.Parameters.AddWithValue("@RW", rw.ToString().Trim());
                cmd.Parameters.AddWithValue("@Total", total.ToString().Trim());
                cmd.Parameters.AddWithValue("@NRegistro", Convert.ToInt32(dt.Rows[0]["NRegistro"]));
                cmd.ExecuteNonQuery();

                con.Close();
                Response.Redirect("updateproject.aspx?project=" + Request.QueryString["project"]);
                Response.Write("<script>alert('Turno finalizado');</script>");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }

            clearForm();
        }
    }
}