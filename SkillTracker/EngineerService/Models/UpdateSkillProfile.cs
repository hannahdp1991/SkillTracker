using EngineerService.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EngineerService.Models
{
    public class UpdateSkillProfile
    {
        [Required]
        [SkillList]
        public IList<Skill> SkillInfo { get; set; }

        public DateTime LastUpdated { get; set; }

        public UpdateSkillProfile()
        {
            this.LastUpdated = new DateTime();

        }
    }
}
