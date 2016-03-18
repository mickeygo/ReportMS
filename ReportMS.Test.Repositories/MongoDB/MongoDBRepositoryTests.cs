using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ReportMS.Test.Repositories.MongoDB
{
    [TestClass]
    public class MongoDBRepositoryTests
    {
        internal const string mongoDB_Database = @"RmsMongoTest";
        internal const string mongoDB_ConnectionString = @"mongodb://localhost/?safe=true";

        [TestMethod]
        public void MongoDBRepositoryTests_InsertDocument()
        {
            var document = new BsonDocument
            {
                {"Name", "yanggang"},
                {"Birth", DateTime.Now.AddDays(-1)},
                {"Email", "gang.yang@advantech.com.cn"},
                {
                    "Tel", new BsonDocument
                    {
                        {"Phone", 88887777},
                        {"ext", 1100}
                    }
                }
            };
            
            var client = new MongoClient(mongoDB_ConnectionString);
            var database = client.GetDatabase(mongoDB_Database);
            var collection = database.GetCollection<BsonDocument>("database");
            collection.InsertOne(document);
        }

        [TestMethod]
        public void MongoDBRepositoryTests_InsertDocumentAsync()
        {

        }

        [TestMethod]
        public void MongoDBRepositoryTests_ModifyDocument()
        {
            
        }

        [TestMethod]
        public void MongoDBRepositoryTests_ModifyDocumentAsync()
        {
            
        }

        [TestMethod]
        public void MongoDBRepositoryTests_DeleteDocument()
        {
            
        }

        [TestMethod]
        public void MongoDBRepositoryTests_DeleteDocumentAsync()
        {
            
        }

        [TestMethod]
        public void MongoDBRepositoryTests_FindAll()
        {
            var client = new MongoClient(mongoDB_ConnectionString);
            var database = client.GetDatabase(mongoDB_Database);
            var collection = database.GetCollection<BsonDocument>("database");
            var document = collection.FindSync(new BsonDocument()).First();

            Assert.IsNull(document, document.ToString());
        }

        [TestMethod]
        public void MongoDBRepositoryTests_FindAllBySpecification()
        {
            
        }
    }
}
