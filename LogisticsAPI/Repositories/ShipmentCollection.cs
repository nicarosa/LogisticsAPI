using LogisticsAPI.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogisticsAPI.Repositories
{
    public class ShipmentCollection : IShipmentCollection
    {
        internal MongoDBRepository _repository = new MongoDBRepository();
        private IMongoCollection<Shipment> Collection;

        public ShipmentCollection()
        {
            Collection = _repository.db.GetCollection<Shipment>("Shipments");
        }

        public async Task DeleteShipment(string id)
        {
            var filter = Builders<Shipment>.Filter.Eq(s => s.Id, new ObjectId(id));
            await Collection.DeleteOneAsync(filter);
        }

        public async Task<List<Shipment>> GetAllShipments()
        {
            return await Collection.FindAsync(new BsonDocument()).Result.ToListAsync();
        }

        public async Task<Shipment> GetShipmentById(string id)
        {
            return await Collection.FindAsync(new BsonDocument { { "_id", new ObjectId(id) } }).Result.FirstAsync();
        }

        public async Task InsertShipment(Shipment shipment)
        {
            await Collection.InsertOneAsync(shipment);
        }

        public async Task UpdateShipment(Shipment shipment)
        {
            var filter = Builders<Shipment>.Filter.Eq(s => s.Id, shipment.Id);
            await Collection.ReplaceOneAsync(filter, shipment);
        }
    }
}
