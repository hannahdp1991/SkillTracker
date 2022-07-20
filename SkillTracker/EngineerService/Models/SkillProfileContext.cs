using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;

namespace EngineerService.Models
{
    public class SkillProfileContext
    {
        MongoClient mongoClient;
        IMongoDatabase mongoDatabase;

        public SkillProfileContext(IConfiguration configuration)
        {
            var client = Environment.GetEnvironmentVariable("mongo_db");
            if (client == null || string.IsNullOrEmpty(client))
            {
                client = configuration.GetSection("MongoDb:ConnectionString").Value;
            }

            mongoClient = new MongoClient(client);
            mongoDatabase = mongoClient.GetDatabase(configuration.GetSection("MongoDb:SkillProfileDatabase").Value);
        }

        public IMongoCollection<SkillProfile> SkillProfiles => mongoDatabase.GetCollection<SkillProfile>("SkillProfile");
    }
}
