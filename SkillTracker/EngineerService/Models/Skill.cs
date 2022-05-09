using DataAnnotationsExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EngineerService.Models
{
    public class Skill
    {
        public string SkillName { get; set; }

        [Max(20)]
        [Min(0)]
        public int ExpertiseLevel { get; set; }
    }
}
