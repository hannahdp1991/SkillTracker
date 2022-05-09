using DataAnnotationsExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminService.Models
{
    public class Skill
    {
        public string SkillName { get; set; }

        [Max(30)]
        public int ExpertiseLevel { get; set; }
    }
}
