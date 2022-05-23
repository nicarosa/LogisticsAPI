using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogisticsAPI.Models
{
    public class Shipment
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string ClientName { get; set; }
        public string ClientAddress { get; set; }
        public string ClientPhone { get; set; }
        public string ShipmentType { get; set; }
        public string ProductType { get; set; }
        public int Quantity { get; set; }
        public DateTime RegisterTime { get; set; }
        public DateTime DeliveryTime { get; set; }
        public Decimal Price { get; set; }
        public Decimal Discount { get; set; }
        public string TransportNumber { get; set; }
        public string GuideNumber { get; set; }
    }
}
