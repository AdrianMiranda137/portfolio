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
    public partial class companysignup1 : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            string sessionrole = Session["role"] as string;
            if (!(sessionrole == "admin" || sessionrole == "lider"))
            {
                Response.Redirect("homepage.aspx");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (checkMemberExists())
            {

                Response.Write("<script>alert('Este usuario ya existe, intente con uno nuevo');</script>");
                Response.Redirect("admincompanymanagement.aspx");
            }
            else
            {
                signUpNewMember();
            }
        }

        bool checkMemberExists()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT * from Empresa where Usuario='" + TextBox1.Text.Trim() + "';", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                return false;
            }
        }

        void signUpNewMember()
        {
            //Response.Write("<script>alert('Testing');</script>");
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("INSERT INTO Empresa(Nombre,NombreContacto,Telefono,Email,Direccion,Usuario,Contraseña,Estado) values(@Nombre,@NombreContacto,@Telefono,@Email,@Direccion,@Usuario,@Contraseña,@Estado)", con);
                cmd.Parameters.AddWithValue("@Nombre", TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@NombreContacto", TextBox2.Text.Trim());
                cmd.Parameters.AddWithValue("@Telefono", TextBox3.Text.Trim());
                cmd.Parameters.AddWithValue("@Email", TextBox4.Text.Trim());
                cmd.Parameters.AddWithValue("@Direccion", TextBox5.Text.Trim());
                cmd.Parameters.AddWithValue("@Usuario", TextBox8.Text.Trim());
                cmd.Parameters.AddWithValue("@Contraseña", TextBox10.Text.Trim());
                cmd.Parameters.AddWithValue("@estado", "pending");
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script>alert('Empresa registrada exitosamente');</script>");
                Response.Redirect("admincompanymanagement.aspx");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
    }
}