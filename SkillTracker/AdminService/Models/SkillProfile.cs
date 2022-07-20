using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AdminService.Models
{
    public class SkillProfile
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [Required]
        public Associate AssociateInfo { get; set; }

        [Required]
        public IList<Skill> SkillInfo { get; set; }

        public DateTime LastUpdated { get; set; }

        public DateTime AddedDate { get; set; }
    }
}
