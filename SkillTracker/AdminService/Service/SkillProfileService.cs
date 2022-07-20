using AdminService.Models;
using AdminService.Repository;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminService.Service
{
    public class SkillProfileService : ISkillProfileService
    {
        private readonly ISkillProfileRepository skillRepository;
        private readonly IDistributedCache cache;

        public SkillProfileService(ISkillProfileRepository _repo, IDistributedCache _cache)
        {
            this.skillRepository = _repo;
            this.cache = _cache;
        }
        public async Task<IList<SkillProfile>> SearchProfile(Criteria criteria)
        {
            IList<SkillProfile> foundSkillProfiles;
            string serializedSkillProfiles;

            var encodedSkillProfiles = await cache.GetAsync(criteria.CriteriaType + criteria.CriteraValue);
            if (encodedSkillProfiles != null)
            {
                serializedSkillProfiles = Encoding.UTF8.GetString(encodedSkillProfiles);
                foundSkillProfiles = JsonConvert.DeserializeObject<IList<SkillProfile>>(serializedSkillProfiles);
            }
            else
            {
                foundSkillProfiles = this.skillRepository.Search(criteria);
                serializedSkillProfiles = JsonConvert.SerializeObject(foundSkillProfiles);
                encodedSkillProfiles = Encoding.UTF8.GetBytes(serializedSkillProfiles);
                var options = new DistributedCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                    .SetAbsoluteExpiration(DateTime.Now.AddHours(6));
                await cache.SetAsync(criteria.CriteriaType + criteria.CriteraValue, encodedSkillProfiles, options);
            }
            return foundSkillProfiles;
        }

        public IList<SkillProfile> Get()
        {
            return this.skillRepository.Get();
        }
    }
}
