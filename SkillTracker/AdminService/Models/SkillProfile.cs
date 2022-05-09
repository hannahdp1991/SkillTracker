using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace AdminService.Models
{
    public class SkillProfile
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public Associate AssociateInfo { get; set; }

        public IList<Skill> SkillInfo { get; set; }
    }
}
