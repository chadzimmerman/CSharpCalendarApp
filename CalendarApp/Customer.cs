using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApp
{
    internal class Customer
    {
        public int customerId { get; set; }
        public string customerName { get; set; }
        public int addressId { get; set; }
        public int active {  get; set; }
        public DateTime createDate { get; set; } = DateTime.UtcNow;
        public string createdBy { get; set; }
        public DateTime lastUpdate { get; set; } = DateTime.UtcNow;
        public string lastUpdateBy { get; set; }

    }
}
