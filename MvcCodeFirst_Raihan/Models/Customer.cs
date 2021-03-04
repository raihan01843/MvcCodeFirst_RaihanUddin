using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcCodeFirst_Raihan.Models
{
    public class Customer
    {
        public Guid CustomerID  {get;set;}

        public string Name { get; set; }

        public string Address { get; set; }
        public DateTime OrderDate { get; set; }
        
        public virtual ICollection<Order> Orders { get; set; }
    }
}