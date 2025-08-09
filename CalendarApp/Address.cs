using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApp
{
    internal class Address
    {
        public int addressId { get; set; }
        public string address { get; set; }
        public string address2 { get; set; }
        public int cityId { get; set; }
        public int postalCode { get; set; }
        public string phone { get; set; }
        public DateTime createDate { get; set; } = DateTime.UtcNow;
        public string createdBy { get; set; }
        public DateTime lastUpdate { get; set; } = DateTime.UtcNow;
        public string lastUpdateBy { get; set; }
    }
}
