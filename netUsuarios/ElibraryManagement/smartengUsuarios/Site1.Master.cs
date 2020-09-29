using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace smartengUsuarios
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string sessionrole = Session["role"] as string;
                if (string.IsNullOrEmpty(sessionrole))
                {
                    LinkSignup.Visible = true;
                    LinkLO.Visible = false;
                    LinkPerfil.Visible = false;
                    LinkLiderSU.Visible = false;
                    LinkEmpresaSU.Visible = false;
                    LinkEmpleadosAdmin.Visible = false;
                    LinkEmpresasAdmin.Visible = false;
                    LinkProyectosAdmin.Visible = false;
                }
                else if (Session["role"].Equals("admin"))
                {
                    LinkLO.Visible = true;
                    LinkEmpresasAdmin.Visible = true;
                    LinkProyectosAdmin.Visible = true;
                    LinkEmpresaSU.Visible = true;
                    LinkEmpleadosAdmin.Visible = true;
                    LinkLiderSU.Visible = true;

                    LinkPerfil.Visible = false;
                    LinkSignup.Visible = false;
                }
                else if (Session["role"].Equals("company"))
                {
                    LinkLO.Visible = true;
                    LinkPerfil.Visible = true;

                    LinkLiderSU.Visible = false;
                    LinkEmpresaSU.Visible = false;
                    LinkEmpleadosAdmin.Visible = false;
                    LinkEmpresasAdmin.Visible = false;
                    LinkProyectosAdmin.Visible = false;
                    LinkSignup.Visible = false;
                }
                else if (Session["role"].Equals("lider"))
                {
                    LinkLO.Visible = true;
                    LinkPerfil.Visible = true;
                    LinkEmpresaSU.Visible = true;
                    LinkEmpresasAdmin.Visible = true;
                    LinkProyectosAdmin.Visible = true;

                    LinkLiderSU.Visible = false;
                    LinkEmpleadosAdmin.Visible = false;
                    LinkSignup.Visible = false;
                }
                else if (Session["role"].Equals("rh"))
                {
                    LinkEmpresasAdmin.Visible = false;
                    LinkProyectosAdmin.Visible = false;
                    LinkSignup.Visible = false;
                    LinkEmpresaSU.Visible = false;
                    LinkPerfil.Visible = false;

                    LinkEmpleadosAdmin.Visible = true;
                    LinkLiderSU.Visible = true;
                    LinkLO.Visible = true;
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void LinkProyectosAdmin_Click(object sender, EventArgs e)
        {
            Response.Redirect("adminprojectmanagement.aspx");
        }

        protected void LinkProyectoIn_Click(object sender, EventArgs e)
        {
            Response.Redirect("adminprojectinventory.aspx");
        }

        protected void LinkEmpleadosAdmin_Click(object sender, EventArgs e)
        {
            Response.Redirect("adminmembermanagement.aspx");
        }

        protected void LinkEmpresasAdmin_Click(object sender, EventArgs e)
        {
            Response.Redirect("admincompanymanagement.aspx");
        }

        //logout button
        protected void LinkLO_Click(object sender, EventArgs e)
        {
            Session["username"] = "";
            Session["fullname"] = "";
            Session["role"] = "";
            Session["status"] = "";

            LinkLO.Visible = false;
            LinkPerfil.Visible = false;
            LinkLiderSU.Visible = false;
            LinkEmpresaSU.Visible = false;
            LinkEmpleadosAdmin.Visible = false;
            LinkEmpresasAdmin.Visible = false;
            LinkProyectosAdmin.Visible = false;
            Response.Redirect("homepage.aspx");
        }

        // view profile
        protected void LinkPerfil_Click(object sender, EventArgs e)
        {
            if (Session["role"].Equals("lider"))
            {
                Response.Redirect("userprofile.aspx");
            }
            else if (Session["role"].Equals("company"))
            {
                Response.Redirect("companyprofile.aspx");
            }
        }
    }
}