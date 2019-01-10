using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceDesk1
{
    public class Global
    {
        public int StoreID { get; set; }
        public int CountryID { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string Phone { get; set; }
        public Global(string country,string address,string zipCode,string phone)
        {
            this.Country = country;
            this.Address = address;
            this.ZipCode = zipCode;
            this.Phone = phone;
        }
        public Global()
        {

        }
    }
}
