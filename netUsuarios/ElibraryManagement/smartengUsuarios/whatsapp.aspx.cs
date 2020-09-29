using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace smartengUsuarios
{
    public partial class whatsapp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnWhats_Click(object sender, EventArgs e)
        {
            Response.Redirect("https://wa.me/524621022303?text=Buen%20dia,%20gracias%20por%20comunicarse%20con%20smarteng");
        }
    }
}