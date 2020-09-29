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
    public partial class login : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();

                }
                SqlCommand cmd2 = new SqlCommand("select * from Empleado where Usuario='" + TextBox1.Text.Trim() + "' AND Contrasena='" + TextBox2.Text.Trim() + "' AND Estado != 'deactive'", con);
                SqlDataReader dr2 = cmd2.ExecuteReader();
                SqlCommand cmd3 = new SqlCommand("select * from Empresa where Usuario='" + TextBox1.Text.Trim() + "' AND Contraseña='" + TextBox2.Text.Trim() + "' AND Estado != 'deactive'", con);
                SqlDataReader dr3 = cmd3.ExecuteReader();

                if (dr2.HasRows)
                {
                    while (dr2.Read())
                    {
                        Session["username"] = dr2.GetValue(0).ToString();
                        Session["fullname"] = dr2.GetValue(1).ToString();
                        Session["role"] = dr2.GetValue(10).ToString();
                        //Session["status"] = dr.GetValue(10).ToString();

                        if (dr2.GetValue(10).ToString() == "employe")
                        {
                            Response.Redirect("userprofile.aspx");
                        }
                        else if (dr2.GetValue(10).ToString() == "rh")
                        {
                            Response.Redirect("adminmembermanagement.aspx");
                        }
                        else
                        {
                            Response.Redirect("adminprojectmanagement.aspx");
                        }
                    }
                }
                if(dr3.HasRows)
                { 
                    while (dr3.Read())
                    {
                        Session["username"] = dr3.GetValue(0).ToString();
                        Session["fullname"] = dr3.GetValue(2).ToString();
                        Session["role"] = "company";
                        //Session["status"] = dr.GetValue(10).ToString();
                        Response.Redirect("companyprofile.aspx");
                    }
                }

                if (!(dr2.HasRows && dr3.HasRows))
                {
                    Response.Write("<script>alert('No se encontró usuario');</script>");
                    Response.Redirect("homepage.apx");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
    }
}