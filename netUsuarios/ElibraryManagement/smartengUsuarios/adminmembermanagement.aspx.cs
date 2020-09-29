using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace smartengUsuarios
{
    public partial class adminmembermanagement : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            string sessionrole = Session["role"] as string;
            if (!(sessionrole == "admin" || sessionrole == "rh"))
            {
                Response.Redirect("homepage.aspx");
            }
            else
            {
                GridView1.DataBind();
            }
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            updateMemberStatusByID("active");
        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            updateMemberStatusByID("pending");
        }

        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            updateMemberStatusByID("deactive");
        }

        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            getMemberByID();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if(TextBox7.Text=="deactive")
                deleteMemberByID();
            else
                Response.Write("<script>alert('Sólo se pueden eliminar lideres desactivados');</script>");
        }

        bool checkIfMemberExists()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * from Empleado where NEmpleado='" + TextBox1.Text.Trim() + "';", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                System.Data.DataTable dt = new System.Data.DataTable();
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

        void deleteMemberByID()
        {
            if (checkIfMemberExists())
            {
                try
                {
                    SqlConnection con = new SqlConnection(strcon);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("DELETE from Empleado WHERE NEmpleado='" + TextBox1.Text.Trim() + "'", con);

                    cmd.ExecuteNonQuery();
                    con.Close();
                    Response.Write("<script>alert('Lider eliminado exitosamente');</script>");
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
                Response.Write("<script>alert('Empleado no encontrado');</script>");
            }
        }

        void getMemberByID()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();

                }
                SqlCommand cmd = new SqlCommand("select * from Empleado where NEmpleado='" + TextBox1.Text.Trim() + "'", con);
                SqlCommand cmd2 = new SqlCommand("select Fotografia from Empleado where NEmpleado='" + TextBox1.Text.Trim() + "'", con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        TextBox2.Text = dr.GetValue(1).ToString();
                        TextBox7.Text = dr.GetValue(11).ToString();
                        txtFechaNac.Text = dr.GetValue(2).ToString();
                        txtNss.Text = dr.GetValue(5).ToString();
                        txtTelefono.Text = dr.GetValue(3).ToString();
                        txtEmail.Text = dr.GetValue(4).ToString();
                        txtDireccion.Text = dr.GetValue(7).ToString();

                        byte[] img = (byte[])cmd2.ExecuteScalar();

                        if (img != null && img.Length > 0)
                        {
                            imagePreview.ImageUrl = "data:image;base64," + Convert.ToBase64String(img);
                        }
                    }

                }
                else
                {
                    Response.Write("<script>alert('Empleado no encontrado');</script>");
                }

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        void updateMemberStatusByID(string status)
        {
            if (checkIfMemberExists())
            {
                try
                {
                    SqlConnection con = new SqlConnection(strcon);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();

                    }
                    SqlCommand cmd = new SqlCommand("UPDATE Empleado SET Estado='" + status + "' WHERE NEmpleado='" + TextBox1.Text.Trim() + "'", con);
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
                Response.Write("<script>alert('Empleado no encontrado');</script>");
            }

        }

        void clearForm()
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox7.Text = "";
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            updateMemberByID();
        }

        public void updateMemberByID()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                if (FileUpload2.HasFile)
                {
                    int size = FileUpload2.PostedFile.ContentLength;
                    byte[] Fotografia = new byte[size];

                    FileUpload2.PostedFile.InputStream.Read(Fotografia, 0, size);
                    Bitmap fotografiaBin = new Bitmap(FileUpload2.PostedFile.InputStream);

                    string fotografiaDataURL64 = "data:image/jpg;base64," + Convert.ToBase64String(Fotografia);

                    imagePreview.ImageUrl = fotografiaDataURL64;

                    SqlCommand cmd2 = new SqlCommand("UPDATE Empleado set Fotografia=@Fotografia WHERE NEmpleado='" + TextBox1.Text.Trim() + "'", con);
                    cmd2.Parameters.AddWithValue("@Fotografia", Fotografia);
                    int result2 = cmd2.ExecuteNonQuery();

                    if (result2 > 0)
                    {

                        Response.Write("<script>alert('Fotografia actualizada');</script>");
                    }
                }

                SqlCommand cmd = new SqlCommand("UPDATE Empleado set Nombre=@Nombre, FechaNac=@FechaNac, Telefono=@Telefono, Email=@Email, NSS=@NSS, Direccion=@Direccion WHERE NEmpleado='" + TextBox1.Text.Trim() + "'", con);
                cmd.Parameters.AddWithValue("@Nombre", TextBox2.Text.Trim());
                cmd.Parameters.AddWithValue("@FechaNac", txtFechaNac.Text.Trim());
                cmd.Parameters.AddWithValue("@Telefono", txtTelefono.Text.Trim());
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@NSS", txtNss.Text.Trim());
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
                    Response.Write("<script>alert('Empleado no encontrado');</script>");
                }

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        
    }
}