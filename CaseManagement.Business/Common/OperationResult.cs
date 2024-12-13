using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagement.Business.Common
{
        public class OperationResult<T>
        {
            public T Data { get; set; }
            public string Status { get; set; }
            public string Message { get; set; }
        }

        public class OperationResult
        {
            public string Status { get; set; }
            public string Message { get; set; }
        }

}
