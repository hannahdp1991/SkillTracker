using AdminService.Exceptions;
using AdminService.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdminService.Repository
{
    public class SkillProfileRepository : ISkillProfileRepository
    {

        private readonly SkillProfileContext skillProfileContext;

        public SkillProfileRepository(SkillProfileContext _context)
        {
            this.skillProfileContext = _context;
        }

        public IList<SkillProfile> Get()
        {
            var list = this.skillProfileContext.SkillProfiles.Find(_ => true).ToList();
            System.Diagnostics.Debug.WriteLine(list[0]?.SkillInfo?.Count);
            return list;
        }

        public IList<SkillProfile> Search(Criteria criteria)
        {
            FilterDefinition<SkillProfile> filter;
            switch (criteria.CriteriaType)
            {
                case CriteriaType.AssociateId:
                    filter = Builders<SkillProfile>.Filter.Eq(profile => profile.AssociateInfo.AssociateId, Convert.ToInt32(criteria.CriteraValue));
                    break;
                case CriteriaType.AssociateName:
                    filter = Builders<SkillProfile>.Filter.Regex(profile => profile.AssociateInfo.Name,
                        new BsonRegularExpression(new Regex(criteria.CriteraValue, RegexOptions.IgnoreCase)));
                    break;
                case CriteriaType.SkillName:
                    filter = Builders<SkillProfile>.Filter.ElemMatch(profile => profile.SkillInfo, Builders<Skill>.Filter.And(
                        Builders<Skill>.Filter.Eq(skill => skill.SkillName, criteria.CriteraValue),
                        Builders<Skill>.Filter.Gte(skill => skill.ExpertiseLevel, 10)));
                    break;
                default:
                    throw new CriteriaNotFoundException();
            }

            var result =
                this.skillProfileContext.SkillProfiles.Find(filter).ToList().OrderByDescending(profile => profile.SkillInfo.Max(skill => skill.ExpertiseLevel)).ToList();
            return result;
        }
    }
}
