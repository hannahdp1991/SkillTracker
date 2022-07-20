using System;
using System.Collections.Generic;
using System.Text;
using AdminService.Models;

namespace SkillTrackerTest.Builders
{
    class AdminSkillProfileBuilder
    {
        public IList<SkillProfile> skillProfileList = new List<SkillProfile>();

        public AdminSkillProfileBuilder()
        {
        }

        public void WithDefaults()
        {

            var skillProfile = new SkillProfile();

            var associateInfo = new Associate();
            skillProfile.SkillInfo = new List<Skill>();

            associateInfo.AssociateId = "CTS346757";
            associateInfo.Mobile = "9999999999";
            associateInfo.Name = "tester";
            associateInfo.Email = "tester@gmail.com";

            var skill1 = new Skill();
            skill1.SkillName = "html";
            skill1.ExpertiseLevel = 10;

            var skill2 = new Skill();
            skill1.SkillName = "spoken";
            skill1.ExpertiseLevel = 20;

            var skill3 = new Skill();
            skill1.SkillName = "docker";
            skill1.ExpertiseLevel = 10;

            var skill4 = new Skill();
            skill1.SkillName = "aptitude";
            skill1.ExpertiseLevel = 15;

            skillProfile.SkillInfo.Add(skill1);
            skillProfile.SkillInfo.Add(skill2);
            skillProfile.SkillInfo.Add(skill3);
            skillProfile.SkillInfo.Add(skill4);

            skillProfileList.Add(skillProfile);
        }

        public void withSkillProfile(SkillProfile profile)
        {
            skillProfileList.Add(profile);
        }

        public void clearList()
        {
            skillProfileList.Clear();
        }

        public IList<SkillProfile> Build()
        {
            return skillProfileList;
        }
    }
}
