using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Spartan_Test.Models
{
    public class Equipment
    {
        public string Id { get; set; }
        public int ExternalId { get; set; }
        public string EquipmentTypeId { get; set; }
        public int MeterReading { get; set; }
        public string Description { get; set; }
    }
}