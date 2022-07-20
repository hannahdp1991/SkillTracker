using EngineerService.Constants;
using EngineerService.Exceptions;
using EngineerService.Models;
using EngineerService.Repository;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineerService.Service
{
    public class SkillService : ISkillService
    {
        private readonly ISkillRepository _repository;
        private readonly ServiceBusSender _addSkillProfileMessageSender;
        private readonly IDistributedCache cache;

        public SkillService(ISkillRepository repository, ServiceBusSender serviceBus, IDistributedCache _cache)
        {
            _repository = repository;
            _addSkillProfileMessageSender = serviceBus;
            this.cache = _cache;
        }

        public async Task<bool> Add(SkillProfile profile)
        {
            try
            {
                var result = this._repository.Add(profile);

                if (!result)
                {
                    throw new Exception();
                }

                await _addSkillProfileMessageSender.SendMessage(profile);
                var serializedSkillProfile = JsonConvert.SerializeObject(profile);
                var encodedSkillProfile = Encoding.UTF8.GetBytes(serializedSkillProfile);
                var options = new DistributedCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                    .SetAbsoluteExpiration(DateTime.Now.AddHours(6));
                await cache.SetAsync(profile.AssociateInfo.AssociateId, encodedSkillProfile, options);
                return result;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public bool Update(string userId, UpdateSkillProfile profile)
        {
            try
            {
                var result = this._repository.Update(userId, profile);
                if (!result)
                {
                    throw new UserNotFoundException();
                }

                return result;
            }
            catch (UserNotFoundException exception)
            {
                throw exception;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
