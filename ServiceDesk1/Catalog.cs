using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceDesk1
{
    public class Catalog
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }

        public Catalog(string name, string shortDescription)
        {
            this.Name = name;
            this.ShortDescription = shortDescription;
        }
    }
}
