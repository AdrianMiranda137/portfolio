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
    public partial class userlogin : System.Web.UI.Page
    {
        string strcon = System.Configuration.ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();

                }
                SqlCommand cmd = new SqlCommand("select * from Empleado where Usuario='" + TextBox1.Text.Trim() + "' AND Contraseña='" + TextBox2.Text.Trim() + "'", con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Response.Write("<script>alert('Inicio de sesión exitoso');</script>");
                        Session["username"] = dr.GetValue(0).ToString();
                        Session["fullname"] = dr.GetValue(1).ToString();
                        Session["role"] = "employe";
                    }
                    Response.Redirect("userprofile.aspx");
                }
                else
                {
                    Response.Write("<script>alert('Valores incorrectos');</script>");
                }

            }
            catch (Exception ex)
            {

            }
        }
    }
}