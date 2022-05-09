using EngineerService.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EngineerService.Repository
{
    public class SkillRepository : ISkillRepository
    {
        private readonly SkillProfileContext _context;

        public SkillRepository(SkillProfileContext skillContext)
        {
            _context = skillContext;
        }

        public bool Add(SkillProfile profile)
        {
            try
            {
                _context.SkillProfiles.InsertOne(profile);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(int userId, SkillProfile profile)
        {
            try
            {
                var foundSkillProfile = this.GetSkillProfileById(userId);
                if (foundSkillProfile != null)
                {
                    var filter = Builders<SkillProfile>.Filter.Eq(profile => profile.Id, foundSkillProfile.Id);
                    var result = _context.SkillProfiles.ReplaceOne(filter, profile);
                    return result.IsAcknowledged && result.ModifiedCount > 0;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        private SkillProfile GetSkillProfileById(int userId)
        {
            var filter = Builders<SkillProfile>.Filter.Eq(profile => profile.AssociateInfo.AssociateId, Convert.ToInt32(userId));
            var result =
                _context.SkillProfiles.Find(filter).ToList().OrderByDescending(profile => profile.SkillInfo.Max(skill => skill.ExpertiseLevel)).FirstOrDefault();
            return result;
        }
    }
}
