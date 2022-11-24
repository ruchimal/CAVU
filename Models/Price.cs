using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;

namespace CAVU.Models
{
    public class Price
    {
        public static int ParkingCost(DateTime StartDate, DateTime EndDate)
        {
            
            CultureInfo culture = new CultureInfo("en-UK");
            if (StartDate.Year == EndDate.Year && StartDate.Month == EndDate.Month)
            {
                return CalculatePrice(StartDate.Month, (EndDate.Day - StartDate.Day));
            }
            else
            {
                int parkingCost = 0;
                parkingCost = CalculatePrice(StartDate.Month, DateTime.DaysInMonth(StartDate.Year, EndDate.Month) - StartDate.Day);
                DateTime current = StartDate.AddMonths(1);
                while (current <= EndDate.AddMonths(-1))
                {
                    parkingCost += CalculatePrice(current.Month, DateTime.DaysInMonth(current.Year, current.Month));
                    current = current.AddMonths(1);
                }
                return parkingCost; 

            }
        }
        private static int CalculatePrice (int Month,int nDays)
        {
            switch (Month)
            {
                //20 GBP for winter months
                case 1:
                case 2:
                case 11:
                case 12:
                    return (20 * nDays);
               
                default:
                    //15 GBP for non winter months
                    return (15 * nDays);
            }
        }
    }
}