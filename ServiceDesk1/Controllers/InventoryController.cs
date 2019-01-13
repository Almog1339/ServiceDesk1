using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ServiceDesk1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        [HttpGet("GetSuppliers")]
        public object GetSuppliers()
        {
            return DBHelper.GetSuppliers();
        }
        [HttpGet("GetOrders")]
        public object GetOrders()
        {
            return DBHelper.GetOrder();
        }
        [HttpPost("NewOrder")]
        public object NewOrder(int businessEntityID,DateTime OrderDate,DateTime Required_Date,DateTime Shipped_Date,int Freight,string ShipName,string ShipAddress,string ShipCity,string ShipCountry)
        {
            if (string.IsNullOrEmpty(ShipAddress)||string.IsNullOrEmpty(ShipCity) || string.IsNullOrEmpty(ShipCountry) || string.IsNullOrEmpty(ShipName)) {
                return -1;
            }
            else {
                return DBHelper.NewOrder(businessEntityID,OrderDate,Required_Date, Shipped_Date, Freight, ShipName, ShipAddress, ShipCity, ShipCountry);
            }
        }
    }
}