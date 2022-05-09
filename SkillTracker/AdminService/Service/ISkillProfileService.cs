using AdminService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminService.Service
{
    public interface ISkillProfileService
    {
        public IList<SkillProfile> SearchProfile(Criteria criteria);

        public IList<SkillProfile> Get();
    }
}
