using EngineerService.Models;

namespace EngineerService.Service
{
    public interface IAddSkillProfileMessageSender
    {
        public void SendSkillProfile(SkillProfile profile);
    }
}
