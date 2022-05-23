using LogisticsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogisticsAPI.Repositories
{
    interface IShipmentCollection
    {
        Task InsertShipment(Shipment shipment);
        Task UpdateShipment(Shipment shipment);
        Task DeleteShipment(string id);
        Task<List<Shipment>> GetAllShipments();
        Task<Shipment> GetShipmentById(string id);
    }
}
