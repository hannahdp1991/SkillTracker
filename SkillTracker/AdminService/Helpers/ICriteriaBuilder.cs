using AdminService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminService.Helpers
{
    public interface ICriteriaBuilder
    {
        void Build(string criteria, string value);
        
        Criteria getCriteria();
    }
}
