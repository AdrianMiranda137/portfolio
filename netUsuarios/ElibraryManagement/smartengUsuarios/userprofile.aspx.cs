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
    public partial class userprofile : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string sessionrole = Session["role"] as string;
                if (string.IsNullOrEmpty(sessionrole))
                {
                    Response.Write("<script>alert('Sesión expirada. Vuelve a iniciar sesión');</script>");
                    Response.Redirect("homepage.aspx");
                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        getUserPersonalDetails();
                    
                    
                    }

                    if(Label1.Text=="active")
                    {
                        getUserProjectData();
                    }
                }
            }
            catch (Exception ex)
            {

                Response.Write("<script>alert('Sesión expirada. Vuelve a iniciar sesión');</script>");
                Response.Redirect("homepage.aspx");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Session["username"].ToString() == "" || Session["username"] == null)
            {
                Response.Write("<script>alert('Sesión expirada. Vuelve a iniciar sesión');</script>");
                Response.Redirect("homepage.aspx");
            }
            else
            {
                updateUserPersonalDetails();

            }
        }

        void updateUserPersonalDetails()
        {
            string password = "";
            if (TextBox10.Text.Trim() == "")
            {
                password = TextBox9.Text.Trim();
            }
            else
            {
                password = TextBox10.Text.Trim();
            }
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }


                SqlCommand cmd = new SqlCommand("update Empleado set  Contrasena=@Contrasena WHERE NEmpleado='" + Session["username"].ToString().Trim() + "'", con);

                cmd.Parameters.AddWithValue("@Contrasena", password);

                int result = cmd.ExecuteNonQuery();
                con.Close();
                if (result > 0)
                {

                    Response.Write("<script>alert('Contraseña actualizada exitosamente');</script>");
                    getUserPersonalDetails();
                    getUserProjectData();
                }
                else
                {
                    Response.Write("<script>alert('Entrada invalida');</script>");
                }

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        void getUserPersonalDetails()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * from Empleado where NEmpleado='" + Session["username"].ToString() + "';", con);
                SqlCommand cmd2 = new SqlCommand("select Fotografia from Empleado where NEmpleado='" + Session["username"].ToString() + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                lblNombre.Text = dt.Rows[0]["Nombre"].ToString().Substring(0);
                txtNEmpleado.Text = dt.Rows[0]["NEmpleado"].ToString();
                txtNombre.Text = dt.Rows[0]["Nombre"].ToString();
                txtFechaNac.Text = dt.Rows[0]["FechaNac"].ToString();
                txtTelefono.Text = dt.Rows[0]["Telefono"].ToString();
                txtEmail.Text = dt.Rows[0]["Email"].ToString().Trim();
                txtNSS.Text = dt.Rows[0]["NSS"].ToString();
                txtDireccion.Text = dt.Rows[0]["Direccion"].ToString();
                TextBox8.Text = dt.Rows[0]["Usuario"].ToString();
                TextBox9.Text = dt.Rows[0]["Contrasena"].ToString();

                byte[] img = (byte[])cmd2.ExecuteScalar();

                if (img != null && img.Length > 0)
                {
                    imagePreview.ImageUrl = "data:image;base64," + Convert.ToBase64String(img);
                }

                Label1.Text = dt.Rows[0]["Estado"].ToString().Trim();

                if (dt.Rows[0]["Estado"].ToString().Trim() == "active")
                {
                    Label1.Attributes.Add("class", "badge badge-pill badge-success");
                }
                else if (dt.Rows[0]["Estado"].ToString().Trim() == "pending")
                {
                    Label1.Attributes.Add("class", "badge badge-pill badge-warning");
                }
                else if (dt.Rows[0]["Estado"].ToString().Trim() == "deactive")
                {
                    Label1.Attributes.Add("class", "badge badge-pill badge-danger");
                }
                else
                {
                    Label1.Attributes.Add("class", "badge badge-pill badge-info");
                }


            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");

            }
        }

        void getUserProjectData()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT IdProyecto, Nombre, Estado from Proyecto where Lider='" + Session["username"].ToString() + "' AND Estado = 'active';", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                GridView1.DataSource = dt;
                GridView1.DataBind();


            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");

            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect("updateproject.aspx?project="+GridView1.SelectedRow.Cells[1].Text);
        }
    }
}