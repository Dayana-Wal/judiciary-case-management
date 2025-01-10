using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagement.Business.Queries
{
    public interface ILookUpConstantsQuery
    {
        public Task<int> GetConstantId(string code, string type);
    }
}
