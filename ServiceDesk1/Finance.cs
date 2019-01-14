using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceDesk1
{
    public class Finance
    {
        public int OrderID { get; set; }
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipCity { get; set; }
        public string ShipCountry { get; set; }
        public string CustomerName { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int UnitPrice { get; set; }
        public Int16 Quantity { get; set; }
        public int Discount { get; set; }
        public int ExtendedPrice { get; set; }
        public int Freight { get; set; }
        public Finance()
        {

        }
        public Finance(int OrderID, string ShipName, string ShipAddress, string ShipCity, string ShipCountry, string CustomerName, DateTime? OrderDate, DateTime? ShippedDate, DateTime? RequiredDate, int ProductID, string ProductName, int UnitPrice, Int16 Quantity, int Discount, int ExtendedPrice, int Freight)
        {
            this.OrderID = OrderID;
            this.ShipName = ShipName;
            this.ShipAddress = ShipAddress;
            this.ShipCountry = ShipCountry;
            this.CustomerName = CustomerName;
            this.OrderDate = OrderDate;
            this.ShippedDate = ShippedDate;
            this.RequiredDate = RequiredDate;
            this.ProductID = ProductID;
            this.ProductName = ProductName;
            this.UnitPrice = UnitPrice;
            this.Quantity = Quantity;
            this.Discount = Discount;
            this.ExtendedPrice = ExtendedPrice;
            this.Freight = Freight;
        }
    }
}
