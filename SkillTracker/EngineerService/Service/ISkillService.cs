using EngineerService.Models;
using System.Threading.Tasks;

namespace EngineerService.Service
{
    public interface ISkillService
    {
        public Task<bool> Add(SkillProfile profile);

        public bool Update(string userId, UpdateSkillProfile profile);
    }
}
