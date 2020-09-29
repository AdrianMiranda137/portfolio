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
    public partial class companyprofile : System.Web.UI.Page
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
                    getCompanyProjectData();

                    if (!Page.IsPostBack)
                    {
                        getCompanyPersonalDetails();
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
                updateCompanyPersonalDetails();

            }
        }

        void updateCompanyPersonalDetails()
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


                SqlCommand cmd = new SqlCommand("update Empresa set Contraseña=@Contraseña WHERE IdEmpresa='" + Session["username"].ToString().Trim() + "'", con);

                cmd.Parameters.AddWithValue("@Contraseña", password);

                int result = cmd.ExecuteNonQuery();
                con.Close();
                if (result > 0)
                {

                    Response.Write("<script>alert('Contraseña cambiada');</script>");
                    getCompanyPersonalDetails();
                    getCompanyProjectData();
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

        void getCompanyPersonalDetails()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * from Empresa where IdEmpresa='" + Session["username"].ToString() + "';", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                txtNombre.Text = dt.Rows[0]["Nombre"].ToString();
                lblNombre.Text = dt.Rows[0]["Nombre"].ToString();
                txtNombreContacto.Text = dt.Rows[0]["NombreContacto"].ToString();
                txtTelefono.Text = dt.Rows[0]["Telefono"].ToString();
                txtEmail.Text = dt.Rows[0]["Email"].ToString().Trim();
                txtDireccion.Text = dt.Rows[0]["Direccion"].ToString();
                TextBox8.Text = dt.Rows[0]["Usuario"].ToString();
                TextBox9.Text = dt.Rows[0]["Contraseña"].ToString();

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

        void getCompanyProjectData()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT IdProyecto, Nombre, Estado from Proyecto where Empresa='" + Session["username"].ToString() + "' AND Estado != 'deactive';", con);
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
            Response.Redirect("viewprojects.aspx?project=" + GridView1.SelectedRow.Cells[1].Text);
        }
    }
}