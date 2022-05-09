using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace EngineerService.Models
{
    public class SkillProfile
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public Associate AssociateInfo { get; set; }

        public IList<Skill> SkillInfo { get; set; }

        public DateTime AddedDate { get; set; }

        public SkillProfile()
        {
            this.AddedDate = DateTime.Now;
        }
    }
}
