using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CAVU.Domain
{
    public class ParkingSlot
    {
        public int SlotID { get; set; }
        public string VechicalID { get; set; }
        public DateTime FromDate { get; set;}
        public DateTime ToDate { get; set; }
        
    }
}