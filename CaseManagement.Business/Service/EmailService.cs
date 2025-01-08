using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagement.Business.Service
{
    public class EmailService
    {
        //TODO
        public void SendEmail(string to, string subject, string body)
        {
            Console.WriteLine("In email service");
            Console.WriteLine("to"+ to);
            Console.WriteLine("body"+ body);
            Console.WriteLine("subject"+ subject);
        }
    }
}
