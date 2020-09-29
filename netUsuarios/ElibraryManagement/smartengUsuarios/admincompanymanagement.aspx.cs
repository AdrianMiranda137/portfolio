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
    public partial class admincompanymanagement : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            string sessionrole = Session["role"] as string;
            if (!(sessionrole == "admin" || sessionrole == "lider"))
            {
                Response.Redirect("homepage.aspx");
            }
            else
            {
                GridView1.DataBind();
            }
        }

        protected void btnIr_Click(object sender, EventArgs e)
        {
            getCompanyByID();
        }

        protected void btnActivar_Click(object sender, EventArgs e)
        {
            updateCompanyStatusByID("active");
        }

        protected void btnPausar_Click(object sender, EventArgs e)
        {
            updateCompanyStatusByID("pending");
        }

        protected void btnDesactivar_Click(object sender, EventArgs e)
        {
            updateCompanyStatusByID("deactive");
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            if(TextBox7.Text=="deactive")
                deleteCompanyByID();
            else
                Response.Write("<script>alert('Sólo se pueden eliminar empresas desactivadas');</script>");

        }

        protected void btnActualiza_Click(object sender, EventArgs e)
        {
            updateCompanyByID();
        }

        bool checkIfCompanyExists()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * from Empresa where IdEmpresa='" + TextBox1.Text.Trim() + "';", con);
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

        void deleteCompanyByID()
        {
            if (checkIfCompanyExists())
            {
                try
                {
                    SqlConnection con = new SqlConnection(strcon);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("DELETE from Empresa WHERE IdEmpresa='" + TextBox1.Text.Trim() + "'", con);

                    cmd.ExecuteNonQuery();
                    con.Close();
                    Response.Write("<script>alert('Empresa eliminada exitosamente');</script>");
                    clearForm();
                    GridView1.DataBind();

                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('" + ex.Message + "');</script>");
                }

            }
            else
            {
                Response.Write("<script>alert('Empresa no encontrada');</script>");
            }
        }

        void getCompanyByID()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();

                }
                SqlCommand cmd = new SqlCommand("select * from Empresa where IdEmpresa='" + TextBox1.Text.Trim() + "'", con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        TextBox2.Text = dr.GetValue(1).ToString();
                        TextBox7.Text = dr.GetValue(8).ToString();
                        txtNombreContacto.Text = dr.GetValue(2).ToString();
                        txtTelefono.Text = dr.GetValue(3).ToString();
                        txtEmail.Text = dr.GetValue(4).ToString();
                        txtDireccion.Text = dr.GetValue(5).ToString();
                    }

                }
                else
                {
                    Response.Write("<script>alert('Empresa no encontrada');</script>");
                }

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        void updateCompanyStatusByID(string status)
        {
            if (checkIfCompanyExists())
            {
                try
                {
                    SqlConnection con = new SqlConnection(strcon);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();

                    }
                    SqlCommand cmd = new SqlCommand("UPDATE Empresa SET Estado='" + status + "' WHERE IdEmpresa='" + TextBox1.Text.Trim() + "'", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    GridView1.DataBind();
                    Response.Write("<script>alert('Estado de cuenta actualizado');</script>");


                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('" + ex.Message + "');</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Empresa no encontrada');</script>");
            }

        }

        void clearForm()
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox7.Text = "";
            txtNombreContacto.Text = "";
            txtTelefono.Text = "";
            txtEmail.Text = "";
            txtDireccion.Text = "";
        }

        public void updateCompanyByID()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("UPDATE Empresa set Nombre=@Nombre, NombreContacto=@NombreContacto, Telefono=@Telefono, Email=@Email, Direccion=@Direccion WHERE IdEmpresa='" + TextBox1.Text.Trim() + "'", con);
                cmd.Parameters.AddWithValue("@Nombre", TextBox2.Text.Trim());
                cmd.Parameters.AddWithValue("@NombreContacto", txtNombreContacto.Text.Trim());
                cmd.Parameters.AddWithValue("@Telefono", txtTelefono.Text.Trim());
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@Direccion", txtDireccion.Text.Trim());
                int result = cmd.ExecuteNonQuery();
                con.Close();
                GridView1.DataBind();
                if (result > 0)
                {

                    Response.Write("<script>alert('Información actualizada');</script>");
                    GridView1.DataBind();
                }
                else
                {
                    Response.Write("<script>alert('Empresa no encontrada');</script>");
                }

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
    }
}