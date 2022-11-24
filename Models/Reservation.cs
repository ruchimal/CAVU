using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CAVU.Domain;

namespace CAVU.Models
{
    public static class Reservation
    {
        public static int CheckAvailability(IList<ParkingSlot> ParkingSlots, DateTime fromD, DateTime toD)
        {
            var _pSlots = (from a in ParkingSlots where a.FromDate >= fromD && a.FromDate <= toD select a).ToList();
            return _pSlots.Count;           
        }
        public static Tuple<IList<ParkingSlot>,int> MakeReservation(IList<ParkingSlot> parkingSlots, DateTime fromD, DateTime toD, string VechicalID)
        {
            int _sNumber;
            if ((parkingSlots != null) && (!parkingSlots.Any()))
            {
                ParkingSlot a = new ParkingSlot();
                a.VechicalID = VechicalID;
                a.SlotID = 1;
                a.FromDate = fromD;
                a.ToDate = toD;
                parkingSlots.Add(a);
                _sNumber = 1;
                
            }
            else
            {
               
                for (int i=1;i<=10;i++)
                {
                    var _slotNumber = parkingSlots.FirstOrDefault(c => c.SlotID == i);

                    if (_slotNumber == null)
                    {
                        ParkingSlot a = new ParkingSlot();
                        a.VechicalID = VechicalID;
                        a.SlotID = i;
                        a.FromDate = fromD;
                        a.ToDate = toD;
                        parkingSlots.Add(a);
                        _sNumber = i;
                        break;
                    }
                }
                
            }
            return Tuple.Create(parkingSlots, 1);
        }                

        public static IList<ParkingSlot> DeleteReservation(IList<ParkingSlot> parkingSlots,string VechicalID)
        {
            var itemToRemove = parkingSlots.Single(r => r.VechicalID == VechicalID);
            parkingSlots.Remove(itemToRemove);
            return parkingSlots;
        }

        public static IList<ParkingSlot> AmendReservation(IList<ParkingSlot> parkingSlots, ParkingSlot NewReservation)
        {
            ParkingSlot finding = parkingSlots.Where(i => i.VechicalID == NewReservation.VechicalID).FirstOrDefault();
            int index = parkingSlots.IndexOf(finding);
            parkingSlots[index].FromDate=NewReservation.FromDate;
            parkingSlots[index].ToDate = NewReservation.ToDate;
            return parkingSlots;
        }
    }
}