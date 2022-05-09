using AdminService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminService.Repository
{
    public interface ISkillProfileRepository
    {
        IList<SkillProfile> Search(Criteria criteria);

        IList<SkillProfile> Get();
    }
}
