using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CAVU.Models;
using CAVU.Domain;

namespace CAVU.Controllers
{
    public class ParkingController : ApiController
    {
        public static IList<ParkingSlot> _ParkingSlots = new List<ParkingSlot>();
        //find a parking slot
        public HttpResponseMessage GetParkingSlot(int SlotID)
        {

            var slotDetails = _ParkingSlots.FirstOrDefault((p) => p.SlotID == SlotID);
            if (slotDetails == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return Request.CreateResponse(HttpStatusCode.OK,slotDetails);
        }
        //Check availability
        public HttpResponseMessage GetParkingAvailability( DateTime fromD, DateTime toD)
        {            
            if (Reservation.CheckAvailability(_ParkingSlots,fromD,toD)==0)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    ReasonPhrase = "Parking Full"
                };

                throw new HttpResponseException(response);
            }
            int _parkingCost=Price.ParkingCost(fromD, toD);
            return Request.CreateResponse(HttpStatusCode.OK, _parkingCost);
        }
        //make a reservation
        public HttpResponseMessage PostParkingReservation(DateTime fromD, DateTime toD, string VechicalID)
        {
            var _r =Reservation.MakeReservation(_ParkingSlots, fromD, toD, VechicalID);
            _ParkingSlots= _r.Item1;
            return Request.CreateResponse(HttpStatusCode.OK, _ParkingSlots[_r.Item2]);
        }
        //delete a parking reservation
        public HttpResponseMessage DeleteParkingReservation( string VechicalID)
        {
            _ParkingSlots = Reservation.DeleteReservation(_ParkingSlots, VechicalID);
            return Request.CreateResponse(HttpStatusCode.OK, _ParkingSlots);
        }
        //Amend a parking reservation
        public HttpResponseMessage PutParkingReservation(ParkingSlot ParkingDetails)
        {
            _ParkingSlots = Reservation.AmendReservation(_ParkingSlots, ParkingDetails);
            return Request.CreateResponse(HttpStatusCode.OK, _ParkingSlots);
        }
    }
}
