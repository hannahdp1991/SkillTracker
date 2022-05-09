using AdminService.Models;
using AdminService.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminService.Service
{
    public class SkillProfileService: ISkillProfileService
    {
        private readonly ISkillProfileRepository skillRepository;

        public SkillProfileService(ISkillProfileRepository _repo)
        {
            this.skillRepository = _repo;
        }
        public IList<SkillProfile> SearchProfile(Criteria criteria)
        {
            return this.skillRepository.Search(criteria);
        }

        public IList<SkillProfile> Get()
        {
            return this.skillRepository.Get();
        }
    }
}
