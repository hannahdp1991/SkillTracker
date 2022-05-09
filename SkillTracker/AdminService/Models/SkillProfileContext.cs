using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminService.Models
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
