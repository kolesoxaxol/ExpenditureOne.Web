using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationHelper
{
    public static class SmtpChecker
    {
        public static void CheckSmtp()
        {
            try
            {
                var client = new SmtpClient("smtp.office365.com", 587);

                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential("doors@tuboul.co.il", "Digita1");
                client.EnableSsl = true;

                client.Send("doors@tuboul.co.il", "yevhenii.ko@ideo-digital.com", "test", "testbody");

                Console.WriteLine("Sent");
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException.Message);
            }
            Console.ReadLine();
        }
    }
}
