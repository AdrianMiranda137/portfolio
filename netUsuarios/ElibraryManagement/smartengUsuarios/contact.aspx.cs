 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Drawing;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace smartengUsuarios
{
    public partial class contact : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            try
            {
                var from = "adrian.miranda137@gmail.com";
                var to = "adrian.miranda137@gmail.com";
                const string Password = "797031AristotelesYDante";
                string mailSubject = "Mensaje nuevo de la página";
                string mailMessage = "Empresa: " + txtEmpresa.Text.ToString() + "\n";
                mailMessage += "Nombre de contacto: " + txtNombre.Text.ToString() + "\n";
                mailMessage += "Telefono: " + txtTelefono.Text.ToString() + "\n";
                mailMessage += "Email: " + txtEmail.Text.ToString() + "\n";
                mailMessage += "Mensaje: \n" + txtMensaje.Text.ToString() + "\n";

                var smtp = new SmtpClient();
                {
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Credentials = new NetworkCredential(from, Password);
                    smtp.Timeout = 20000;
                }

                smtp.Send(from,to,mailSubject,mailMessage);

                lblConfirmacion.Text = "Gracias por comunicarte con nosotros";

                txtEmail.Text = "";
                txtEmpresa.Text = "";
                txtMensaje.Text = "";
                txtNombre.Text = "";
                txtTelefono.Text = "";
            }

            catch(Exception)
            {
                lblConfirmacion.Text = "Algo fue mal, prueba otra vez";
                lblConfirmacion.ForeColor = Color.Red;
            }
        }
    }
}