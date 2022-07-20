using EngineerService.Exceptions;
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

        public bool Update(string userId, UpdateSkillProfile profile)
        {
            try
            {
                var foundSkillProfile = this.GetSkillProfileById(userId);
                if (foundSkillProfile != null)
                {
                    if ((profile.LastUpdated - foundSkillProfile?.AddedDate)?.TotalDays > 10)
                    {
                        var filter = Builders<SkillProfile>.Filter.Eq(profile => profile.Id, foundSkillProfile.Id);
                        SkillProfile updatedProfile = CreateUpdatedProfile(profile, foundSkillProfile);
                        var result = _context.SkillProfiles.ReplaceOne(filter, updatedProfile);
                        return result.IsAcknowledged && result.ModifiedCount > 0;
                    }
                    else
                    {
                        throw new Exception("You can update a profile only after 10 days from the date of addition");
                    }
                }

                return false;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private SkillProfile CreateUpdatedProfile(UpdateSkillProfile profile, SkillProfile foundSkillProfile)
        {
            var updatedProfile = new SkillProfile();
            updatedProfile.Id = foundSkillProfile.Id;
            updatedProfile.AssociateInfo = foundSkillProfile.AssociateInfo;
            updatedProfile.SkillInfo = profile.SkillInfo;
            updatedProfile.LastUpdated = profile.LastUpdated;
            updatedProfile.AddedDate = foundSkillProfile.AddedDate;
            return updatedProfile;
        }

        private SkillProfile GetSkillProfileById(string userId)
        {
            var filter = Builders<SkillProfile>.Filter.Eq(profile => profile.AssociateInfo.AssociateId, userId.Trim());
            var result =
                _context.SkillProfiles.Find(filter).ToList().OrderByDescending(profile => profile.SkillInfo.Max(skill => skill.ExpertiseLevel)).FirstOrDefault();
            return result;
        }
    }
}
