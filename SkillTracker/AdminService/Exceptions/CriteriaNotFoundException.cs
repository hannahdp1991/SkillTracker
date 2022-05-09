using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminService.Exceptions
{
    public class CriteriaNotFoundException: ApplicationException
    {
        public CriteriaNotFoundException() { }

        public CriteriaNotFoundException(string message): base(message) { }
    }
}
