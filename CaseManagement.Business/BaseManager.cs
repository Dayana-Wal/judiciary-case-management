using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CaseManagement.Business
{
    public class BaseManager
    {
         public static string NewUlid() { return Ulid.NewUlid().ToString(); }
    }
}
