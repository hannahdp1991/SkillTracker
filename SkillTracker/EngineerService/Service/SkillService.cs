using EngineerService.Models;
using EngineerService.Repository;
using System;

namespace EngineerService.Service
{
    public class SkillService : ISkillService
    {
        private readonly ISkillRepository _repository;

        public SkillService(ISkillRepository repository)
        {
            _repository = repository;
        }

        public bool Add(SkillProfile profile)
        {
            var result = this._repository.Add(profile);
            if (!result)
            {
                throw new Exception();
            }

            return result;
        }

        public bool Update(int userId, SkillProfile profile)
        {
            var result = this._repository.Update(userId, profile);
            if (!result)
            {
                throw new Exception();
            }

            return result;
        }
    }
}
