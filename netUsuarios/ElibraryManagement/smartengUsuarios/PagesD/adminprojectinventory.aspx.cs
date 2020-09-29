using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace smartengUsuarios
{
    public partial class adminbookinventory : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        static string global_filepath;
        static int global_actual_stock, global_current_stock, global_issued_books;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DropDownList1.DataSource = MostrarEmpresas();
                DropDownList1.DataTextField = "Nombre";
                DropDownList1.DataValueField = "IdEmpresa";
                DropDownList1.DataBind();

                DropDownList3.DataSource = MostrarEmpleados();
                DropDownList3.DataTextField = "Nombre";
                DropDownList3.DataValueField = "NEmpleado";
                DropDownList3.DataBind();
            }

            GridView1.DataBind();
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
                SqlCommand cmd = new SqlCommand("SELECT * from Empresa", con);
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
                SqlCommand cmd = new SqlCommand("SELECT * from Empleado", con);
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

        bool checkMemberExists()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT * from Proyecto where Nombre='" + TextBox2.Text.Trim() + "';", con);
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

        protected void Button3_Click(object sender, EventArgs e)
        {

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
                SqlCommand cmd = new SqlCommand("INSERT INTO Proyecto(Nombre,Empresa,Lider,FechaInicio,Inspectores,Estado) values(@Nombre,@Empresa,@Lider,@FechaInicio,@Inspectores,@Estado)", con);
                cmd.Parameters.AddWithValue("@Nombre", TextBox2.Text.Trim());
                cmd.Parameters.AddWithValue("@Empresa", DropDownList1.SelectedValue);
                cmd.Parameters.AddWithValue("@Lider", DropDownList3.SelectedValue);
                cmd.Parameters.AddWithValue("@FechaInicio", TextBox8.Text.Trim());
                cmd.Parameters.AddWithValue("@Inspectores", TextBox9.Text.Trim());
                cmd.Parameters.AddWithValue("@estado", "pending");
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script>alert('Proyecto registrado exitosamente');</script>");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (checkMemberExists())
            {

                Response.Write("<script>alert('Esta pieza ya ha sido registrada');</script>");
            }
            else
            {
                signUpNewMember();
            }
        }
    }
}