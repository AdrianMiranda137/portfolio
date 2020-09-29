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
    public partial class usersignup : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            string sessionrole = Session["role"] as string;
            if (sessionrole != "admin")
            {
                Response.Redirect("homepage.aspx");
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
                SqlCommand cmd = new SqlCommand("SELECT * from Empleado where Usuario='" + TextBox8.Text.Trim() + "';", con);
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
                int size = FileUpload2.PostedFile.ContentLength;
                byte[] Fotografia = new byte[size];

                FileUpload2.PostedFile.InputStream.Read(Fotografia, 0, size);
                Bitmap fotografiaBin = new Bitmap(FileUpload2.PostedFile.InputStream);

                string fotografiaDataURL64 = "data:image/jpg;base64," + Convert.ToBase64String(Fotografia);

                imagePreview.ImageUrl = fotografiaDataURL64;

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("INSERT INTO Empleado(Nombre,FechaNac,Telefono,Email,NSS,Fotografia,Direccion,Usuario,Contraseña,Estado) values(@Nombre,@FechaNac,@Telefono,@Email,@NSS,@Fotografia,@Direccion,@Usuario,@Contraseña,@Estado)", con);
                cmd.Parameters.AddWithValue("@Nombre", TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@FechaNac", TextBox2.Text.Trim());
                cmd.Parameters.AddWithValue("@Telefono", TextBox3.Text.Trim());
                cmd.Parameters.AddWithValue("@Email", TextBox4.Text.Trim());
                cmd.Parameters.AddWithValue("@NSS", TextBox6.Text.Trim());
                cmd.Parameters.AddWithValue("@Fotografia", Fotografia);
                cmd.Parameters.AddWithValue("@Direccion", TextBox5.Text.Trim());
                cmd.Parameters.AddWithValue("@Usuario", TextBox8.Text.Trim());
                cmd.Parameters.AddWithValue("@Contraseña", TextBox10.Text.Trim());
                cmd.Parameters.AddWithValue("@estado", "pending");
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script>alert('Empleado registrado exitosamente');</script>");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            if (checkMemberExists())
            {

                Response.Write("<script>alert('Este usuario ya existe, intente con uno nuevo');</script>");
            }
            else
            {
            signUpNewMember();
            }
        }

        protected void FileUpload2_DataBinding(object sender, EventArgs e)
        {
            int size = FileUpload2.PostedFile.ContentLength;
            byte[] Fotografia = new byte[size];

            FileUpload2.PostedFile.InputStream.Read(Fotografia, 0, size);
            Bitmap fotografiaBin = new Bitmap(FileUpload2.PostedFile.InputStream);

            string fotografiaDataURL64 = "data:image/jpg;base64," + Convert.ToBase64String(Fotografia);

            imagePreview.ImageUrl = fotografiaDataURL64;
        }
    }
}