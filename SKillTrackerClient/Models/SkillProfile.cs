using System;
using System.Collections.Generic;

namespace Models
{
    public class SkillProfile
    {
        public Associate AssociateInfo { get; set; }

        public IList<Skill> SkillInfo { get; set; }
    }
}
