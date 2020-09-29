using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace smartengUsuarios
{
    public partial class services : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            

            switch (Convert.ToInt16(Request.QueryString["service"]))
            {
                case 1:
                    image1.Visible = true;
                    break;
                case 2:
                    image2.Visible = true;
                    break;
                case 3:
                    image3.Visible = true;
                    break;
                case 4:
                    image4.Visible = true;
                    break;
            }

            string servicio = "servicio" + Request.QueryString["service"]+".aspx";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "MyKey", "servicio1('" + servicio + "');", true);
        }
    }
}