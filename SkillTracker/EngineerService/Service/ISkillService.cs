using EngineerService.Models;

namespace EngineerService.Service
{
    public interface ISkillService
    {
        public bool Add(SkillProfile profile);

        public bool Update(int userId, SkillProfile profile);
    }
}
