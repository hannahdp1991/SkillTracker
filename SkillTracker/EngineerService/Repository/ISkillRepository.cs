using EngineerService.Models;

namespace EngineerService.Repository
{
    public interface ISkillRepository
    {
        public bool Add(SkillProfile profile);

        public bool Update(int userId, SkillProfile profile);
    }
}
