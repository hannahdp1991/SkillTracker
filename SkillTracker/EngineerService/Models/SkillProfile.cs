using EngineerService.Validators;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EngineerService.Models
{
    public class SkillProfile
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [Required]
        public Associate AssociateInfo { get; set; }

        [Required]
        [SkillList]
        public IList<Skill> SkillInfo { get; set; }

        public DateTime AddedDate { get; set; }

        public DateTime LastUpdated { get; set; }

        public SkillProfile()
        {
            this.AddedDate = DateTime.Now;
        }
    }
}
