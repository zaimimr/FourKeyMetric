using MongoDB.Bson;
using MongoDB.Driver;

namespace FourKeyMetrics.Entities;

public class Incident {
    public Incident(long startTime, long finishTimeTime, string? jiraTicket, string title, string repository, string project, string organization, string platform)
    {
        _id = ObjectId.GenerateNewId();
        StartTime = startTime;
        FinishTime = finishTimeTime;
        JiraTicket = jiraTicket;
        Title = title;
        Repository = repository;
        Project = project;
        Organization = organization;
        Platform = platform;
    }

    public ObjectId _id { get; set; }
    
    public long StartTime { get; set; } // Pull request start time
    
    public long FinishTime { get; set; } // Build finish time
    
    public String? JiraTicket { get; set; }

    public String Title { get; set;}
    
    public String Repository { get; set;}

    public String Project { get; set;}
    
    public String Organization { get; set;}

    public String Platform { get; set;}
    
}

public static class IncidentDb
{
    public static IMongoCollection<Incident> Open()
    {
        var client = new MongoClient(Environment.GetEnvironmentVariable("MONGO_URL"));
        var db = client.GetDatabase(Environment.GetEnvironmentVariable("MONGO_DB"));
        var collectionName = "incidents";
        var uniqueIndexes =
            "{ Platform: 1, Organization: 1, Project: 1, Repository: 1, JiraTicket: 1, StartTime: 1 }";
        var collectionExists = db.ListCollectionNames().ToList().Contains(collectionName);
        if (collectionExists == false) {
            db.CreateCollection(collectionName);
            var collection = db.GetCollection<Incident>(collectionName);
            var options = new CreateIndexOptions { Unique = true };
            collection.Indexes.CreateOne(uniqueIndexes, options);
        }

        return db.GetCollection<Incident>(collectionName);
    }
}