using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApp
{
    internal class City
    {
        public int cityId { get; set; }
        public string city { get; set; }
        public int countryId { get; set; }
        public DateTime createDate { get; set; } = DateTime.UtcNow;
        public string createdBy { get; set; }
        public DateTime lastUpdate { get; set; } = DateTime.UtcNow;
        public string lastUpdateBy { get; set; }
    }
}
