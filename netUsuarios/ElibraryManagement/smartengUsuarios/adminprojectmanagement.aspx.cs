using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

namespace smartengUsuarios
{
    public partial class adminprojectmanagement : System.Web.UI.Page
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

                if (!Page.IsPostBack)
                {
                    ddlEmpresa.DataSource = MostrarEmpresas();
                    ddlEmpresa.DataTextField = "Nombre";
                    ddlEmpresa.DataValueField = "IdEmpresa";
                    ddlEmpresa.DataBind();

                    ddlLider.DataSource = MostrarEmpleados();
                    ddlLider.DataTextField = "Nombre";
                    ddlLider.DataValueField = "NEmpleado";
                    ddlLider.DataBind();
                }
            }
        }

        protected void btnIr_Click(object sender, EventArgs e)
        {
            getProjectByID();
        }

        protected void btnActivar_Click(object sender, EventArgs e)
        {
            updateProjectStatusByID("active");
        }

        protected void btnPausar_Click(object sender, EventArgs e)
        {
            updateProjectStatusByID("pending");
        }

        protected void btnDesactivar_Click(object sender, EventArgs e)
        {
            updateProjectStatusByID("deactive");
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            if (TextBox7.Text == "deactive")
                deleteProjectByID();
            else
                Response.Write("<script>alert('Sólo se pueden eliminar proyectos desactivados');</script>");
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            updateProjectByID();
        }

        bool checkIfProjectExists()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * from Proyecto where IdProyecto='" + txtId.Text.Trim() + "';", con);
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

        void deleteProjectByID()
        {
            if (checkIfProjectExists())
            {
                try
                {
                    SqlConnection con = new SqlConnection(strcon);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("DELETE from Proyecto WHERE IdProyecto='" + txtId.Text.Trim() + "'", con);
                    SqlCommand cmd2 = new SqlCommand("DROP TABLE IF EXISTS Proyecto" + txtId.Text.Trim(), con);
                    cmd.ExecuteNonQuery();
                    cmd2.ExecuteNonQuery();
                    con.Close();
                    Response.Write("<script>alert('Proyecto eliminado exitosamente');</script>");
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
                Response.Write("<script>alert('Proyecto no encontrado');</script>");
            }
        }

        void getProjectByID()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();

                }
                SqlCommand cmd = new SqlCommand("select * from Proyecto where IdProyecto='" + txtId.Text.Trim() + "'", con);
                SqlCommand cmd2 = new SqlCommand("select Fotografia from Proyecto where IdProyecto='" + txtId.Text.Trim() + "'", con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtNombre.Text = dr.GetValue(1).ToString();
                        TextBox7.Text = dr.GetValue(7).ToString();
                        ddlEmpresa.SelectedValue = dr.GetValue(2).ToString();
                        ddlLider.SelectedValue = dr.GetValue(3).ToString();
                        txtFechaInicio.Text = dr.GetValue(4).ToString();
                        txtInspectores.Text = dr.GetValue(5).ToString();
                        txtDefectos.Text =dr.GetValue(8).ToString();

                        if(dr.GetValue(7).ToString()!="active")
                        {
                            btnActualizar.Enabled = false;
                            btnEntrada.Enabled = false;
                        }

                        byte[] img = (byte[])cmd2.ExecuteScalar();

                        if (img != null && img.Length > 0)
                        {
                            imagePreview.ImageUrl = "data:image;base64," + Convert.ToBase64String(img);
                        }
                    }

                }
                else
                {
                    Response.Write("<script>alert('Proyecto no encontrado');</script>");
                }

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        void updateProjectStatusByID(string status)
        {
            if (checkIfProjectExists())
            {
                try
                {
                    SqlConnection con = new SqlConnection(strcon);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();

                    }
                    SqlCommand cmd = new SqlCommand("UPDATE Proyecto SET Estado='" + status + "' WHERE IdProyecto='" + txtId.Text.Trim() + "'", con);
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
                Response.Write("<script>alert('Proyecto no encontrado');</script>");
            }

        }

        void clearForm()
        {
            txtId.Text = "";
            txtNombre.Text = "";
            txtFechaInicio.Text = "";
            txtInspectores.Text = "";
            TextBox7.Text = "";
            ddlLider.Text = "";
            ddlEmpresa.Text = "";
            imagePreview.ImageUrl = "img/logoblanco.png";
        }

        public void updateProjectByID()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("UPDATE Proyecto set Nombre=@Nombre, Empresa=@Empresa, Lider=@Lider, FechaInicio=@FechaInicio, Inspectores=@Inspectores, Defectos=@Defectos WHERE IdProyecto='" + txtId.Text.Trim() + "'", con);
                cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text.Trim());
                cmd.Parameters.AddWithValue("@Empresa", ddlEmpresa.SelectedValue);
                cmd.Parameters.AddWithValue("@Lider", ddlLider.SelectedValue);
                cmd.Parameters.AddWithValue("@FechaInicio", txtFechaInicio.Text.Trim());
                cmd.Parameters.AddWithValue("@Inspectores", txtInspectores.Text.Trim());
                cmd.Parameters.AddWithValue("@Defectos", txtDefectos.Text.Trim());

                int result = cmd.ExecuteNonQuery();

                if (FileUpload2.HasFile)
                {
                    int size = FileUpload2.PostedFile.ContentLength;
                    byte[] Fotografia = new byte[size];

                    FileUpload2.PostedFile.InputStream.Read(Fotografia, 0, size);
                    Bitmap fotografiaBin = new Bitmap(FileUpload2.PostedFile.InputStream);

                    string fotografiaDataURL64 = "data:image/jpg;base64," + Convert.ToBase64String(Fotografia);

                    imagePreview.ImageUrl = fotografiaDataURL64;

                    SqlCommand cmd2 = new SqlCommand("UPDATE Proyecto set Fotografia=@Fotografia WHERE IdProyecto='" + txtId.Text.Trim() + "'", con);
                    cmd2.Parameters.AddWithValue("@Fotografia", Fotografia);
                    int result2 = cmd2.ExecuteNonQuery();

                    if (result2 > 0)
                    {

                        Response.Write("<script>alert('Fotografia actualizada');</script>");
                    }
                }

                con.Close();
                if (result > 0)
                {

                    Response.Write("<script>alert('Información actualizada');</script>");
                    GridView1.DataBind();
                }
                else
                {
                    Response.Write("<script>alert('Proyecto no encontrado');</script>");
                }

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        DataSet MostrarEmpresas()
        {
            DataSet DS = new DataSet();
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT * from Empresa WHERE Estado='active'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(DS);
                con.Close();
                return DS;
            }
            catch
            {
                return DS;
            }
        }

        DataSet MostrarEmpleados()
        {
            DataSet DS = new DataSet();
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT * from Empleado WHERE Estado='active' AND Rol='lider'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(DS);
                con.Close();
                return DS;
            }
            catch
            {
                return DS;
            }
        }

        protected void btnVer_Click(object sender, EventArgs e)
        {
            if (checkIfProjectExists())
            {
                Response.Redirect("viewprojects.aspx?project=" + txtId.Text);
            }
            else
            {
                Response.Write("<script>alert('Proyecto no encontrado');</script>");
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            signUpNewProject();
            GridView1.DataBind();
        }

        void signUpNewProject()
        {
            try
            {

                SqlConnection con = new SqlConnection(strcon);
                string nombreTabla = "Proyecto";

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
                SqlCommand cmd = new SqlCommand("INSERT INTO Proyecto(Nombre,Empresa,Lider,FechaInicio,Inspectores,Fotografia,Estado,Defectos) values(@Nombre,@Empresa,@Lider,@FechaInicio,@Inspectores,@Fotografia,@Estado,@Defectos)", con);
                cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text.Trim());
                cmd.Parameters.AddWithValue("@Empresa", ddlEmpresa.SelectedValue);
                cmd.Parameters.AddWithValue("@Lider", ddlLider.SelectedValue);
                cmd.Parameters.AddWithValue("@FechaInicio", txtFechaInicio.Text.Trim());
                cmd.Parameters.AddWithValue("@Inspectores", txtInspectores.Text.Trim());
                cmd.Parameters.AddWithValue("@Fotografia", Fotografia);
                cmd.Parameters.AddWithValue("@estado", "pending");
                cmd.Parameters.AddWithValue("@Defectos", txtDefectos.Text.Trim());
                cmd.ExecuteNonQuery();

                SqlCommand cmd2 = new SqlCommand("SELECT TOP 1 * from Proyecto ORDER BY IdProyecto DESC", con);
                SqlDataReader dr = cmd2.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        nombreTabla += dr.GetValue(0).ToString();
                        Response.Write("<script>alert('" + nombreTabla + "');</script>");
                        SqlCommand cmd3 = new SqlCommand("CREATE TABLE " + nombreTabla + " (NRegistro int IDENTITY(1,1), Fecha varchar(50), Hora varchar(50), OK varchar(50), NG varchar(50), DefectoNG varchar(50), RW varchar(50),  DefectoRW varchar(50), Total varchar(50), Ins varchar(50), Observaciones varchar(50), Turno varchar(50))", con);
                        cmd3.ExecuteNonQuery();
                    }

                }
                con.Close();
                Response.Write("<script>alert('Proyecto registrado exitosamente');</script>");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void btnEntrada_Click(object sender, EventArgs e)
        {
            if (checkIfProjectExists())
            {
                Response.Redirect("updateproject.aspx?project=" + txtId.Text);
            }
            else
            {
                Response.Write("<script>alert('Proyecto no encontrado');</script>");
            }
        }
    }
}