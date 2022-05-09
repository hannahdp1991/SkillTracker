using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace EngineerService.Models
{
    public class SkillProfileContext
    {
        MongoClient mongoClient;
        IMongoDatabase mongoDatabase;

        public SkillProfileContext(IConfiguration configuration)
        {
            mongoClient = new MongoClient(configuration.GetSection("MongoDb:ConnectionString").Value);
            mongoDatabase = mongoClient.GetDatabase(configuration.GetSection("MongoDb:SkillProfileDatabase").Value);
        }

        public IMongoCollection<SkillProfile> SkillProfiles => mongoDatabase.GetCollection<SkillProfile>("SkillProfile");
    }
}
