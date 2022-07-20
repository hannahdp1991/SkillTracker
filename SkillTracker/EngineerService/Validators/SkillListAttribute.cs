using EngineerService.Constants;
using EngineerService.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace EngineerService.Validators
{
    public class SkillListAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            IList<Skill> skillInfo = (IList<Skill>)value;
            if (skillInfo.Any(skill => !SkillList.TechnicalSkills.Contains(skill.SkillName)) &&
            skillInfo.Any(skill => !SkillList.NonTechnicalSkills.Contains(skill.SkillName)))
            {
                return new ValidationResult("Invalid skill list");

            }
            return ValidationResult.Success;
        }
    }
}
