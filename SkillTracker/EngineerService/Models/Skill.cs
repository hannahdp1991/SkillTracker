using DataAnnotationsExtensions;
using System.ComponentModel.DataAnnotations;

namespace EngineerService.Models
{
    public class Skill
    {
        private const string SkillRangeErrorMessage = "Expertise level should be between 0 and 20";
        private const string SkillLevelErrorMessage = "Expertise level must be provided";
        private const string SkillNameErrorMessage = "Skill name must be provided";

        [Required(AllowEmptyStrings = false, ErrorMessage = SkillNameErrorMessage)]
        public string SkillName { get; set; }

        [Max(20, ErrorMessage = SkillRangeErrorMessage)]
        [Min(0, ErrorMessage = SkillRangeErrorMessage)]
        [Required(AllowEmptyStrings = false, ErrorMessage = SkillLevelErrorMessage)]
        public int ExpertiseLevel { get; set; }
    }
}
