using System;
using System.Collections.Generic;
using FluentValidation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace CaseManagement.Business.Features
{
    public abstract class AbstractQuery
    {
        public abstract ValidationResult Validate();
    }

    public abstract class AbstractCommand
    {
        public abstract ValidationResult Validate();
    }
}
