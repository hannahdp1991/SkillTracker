using EngineerService.Models;
using System.Collections.Generic;

namespace SkillTrackerTest.Builders
{
    class SkillProfileRequestBuilder
    {
        public SkillProfile skillProfileRequest = new SkillProfile();

        public SkillProfileRequestBuilder() { }


        public void WithDefaults()
        {
            var associateInfo = new Associate();
            skillProfileRequest.SkillInfo = new List<Skill>();

            associateInfo.AssociateId = "346757";
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

            skillProfileRequest.SkillInfo.Add(skill1);
            skillProfileRequest.SkillInfo.Add(skill2);
            skillProfileRequest.SkillInfo.Add(skill3);
            skillProfileRequest.SkillInfo.Add(skill4);

        }

        public void WithAssociate(Associate associate)
        {
            skillProfileRequest.AssociateInfo = associate;
        }

        public void withSkill(Skill skill)
        {
            skillProfileRequest.SkillInfo.Add(skill);
        }

        public SkillProfile Build()
        {
            return skillProfileRequest;
        }
    }
}
