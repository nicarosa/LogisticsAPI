using LogisticsAPI.Models;
using LogisticsAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogisticsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipmentController : Controller
    {
        private IShipmentCollection db = new ShipmentCollection();

        [HttpGet]
        public async Task<IActionResult> GetAllShipments()
        {
            return Ok(await db.GetAllShipments());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetShipmentDetails(string id)
        {
            return Ok(await db.GetShipmentById(id));
        }
       
        [HttpPost]
        public async Task<IActionResult> CreateShipment([FromBody] Shipment shipment)
        {
            if (shipment == null)
            {
                return BadRequest("Null");
            }
            if (shipment.ClientName == null)
            {
                return BadRequest("The shipment must have a client");
            }
            if (shipment.ProductType == null)
            {
                return BadRequest("The shipment must have a product type");
            }
            if (shipment.ShipmentType != "Land" && shipment.ShipmentType != "Sea")
            {
                return BadRequest("The shipment type should be either Land or Sea");
            }
            if (shipment.Quantity <= 0)
            {
                return BadRequest("The product quantity should be more than 0");
            }
            if (shipment.RegisterTime == null)
            {
                return BadRequest("The registry date shouldn't be empty");
            }
            if (shipment.DeliveryTime == null)
            {
                return BadRequest("The delivery date shouldn't be empty");
            }
            if (shipment.Quantity > 5 && shipment.ShipmentType == "Land")
            {
                shipment.Discount = shipment.Price * (decimal)0.02;
            }
            if (shipment.Quantity > 5 && shipment.ShipmentType == "Sea")
            {
                shipment.Discount = shipment.Price * (decimal)0.03;
            }
            if (shipment.ShipmentType == "Land" && (shipment.TransportNumber == null || shipment.TransportNumber.Length != 6 || !shipment.TransportNumber.Take(3).All(char.IsLetter) || !shipment.TransportNumber.Reverse().Take(3).All(char.IsDigit)))
            {
                return BadRequest("The licence plate should have 3 letters and then 3 numbers");
            }
            if (shipment.ShipmentType == "Sea" && (shipment.TransportNumber == null || shipment.TransportNumber.Length != 8 || !shipment.TransportNumber.Take(3).All(char.IsLetter) || !shipment.TransportNumber.Reverse().Take(1).All(char.IsLetter) || shipment.TransportNumber.Count(Char.IsDigit) != 4))
            {
                return BadRequest("The fleet number should start with 3 letters, then 4 numbers, then ending with a letter");
            }
            if (shipment.GuideNumber.Length != 10)
            {
                return BadRequest("The guidenumber should have 10 alphanumeric characters");
            }
            await db.InsertShipment(shipment);

            return Created("Created", true);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShipment([FromBody] Shipment shipment, string id)
        {
            if (shipment == null)
            {
                return BadRequest();
            }
            if (shipment.ClientName == null)
            {
                ModelState.AddModelError("ClientName", "The shipment must have a client");
            }
            if (shipment.ProductType == null)
            {
                ModelState.AddModelError("ProductType", "The shipment must have a product type");
            }
            if (shipment.ShipmentType != "Land" && shipment.ShipmentType != "Sea")
            {
                ModelState.AddModelError("ShipmentType", "The shipment type should be either Land or Sea");
            }
            if (shipment.Quantity <= 0)
            {
                ModelState.AddModelError("Quantity", "The product quantity should be more than 0");
            }
            if (shipment.RegisterTime == null)
            {
                ModelState.AddModelError("RegisterTime", "The registry date shouldn't be empty");
            }
            if (shipment.DeliveryTime == null)
            {
                ModelState.AddModelError("DeliveryTime", "The delivery date shouldn't be empty");
            }
            if (shipment.Quantity > 5 && shipment.ShipmentType == "Land")
            {
                shipment.Discount = shipment.Price * (decimal)0.02;
            }
            if (shipment.Quantity > 5 && shipment.ShipmentType == "Sea")
            {
                shipment.Discount = shipment.Price * (decimal)0.03;
            }
            if (shipment.ShipmentType == "Land" && shipment.TransportNumber == null || shipment.TransportNumber.Length != 6 || !shipment.TransportNumber.Take(3).All(char.IsLetter) || !shipment.TransportNumber.Reverse().Take(3).All(char.IsDigit))
            {
                ModelState.AddModelError("TransportNumber", "The licence plate should have 3 letters and then 3 numbers");
            }
            if (shipment.ShipmentType == "Sea" && shipment.TransportNumber == null || shipment.TransportNumber.Length != 8 || !shipment.TransportNumber.Take(3).All(char.IsLetter) || !shipment.TransportNumber.Reverse().Take(1).All(char.IsLetter) || shipment.TransportNumber.Count(Char.IsDigit) != 4)
            {
                ModelState.AddModelError("TransportNumber", "The fleet number should start with 3 letters, then 4 numbers, then ending with a letter");
            }
            if (shipment.GuideNumber.Length != 10)
            {
                ModelState.AddModelError("GuideNumber", "The guidenumber should have 10 alphanumeric characters");
            }
            shipment.Id = new ObjectId(id);
            await db.UpdateShipment(shipment);

            return Created("Created", true);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(string id)
        {
            await db.DeleteShipment(id);

            return NoContent();
        }
    }
}
