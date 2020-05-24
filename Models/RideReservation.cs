using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ridewithme.Models
{
    public class RideReservation
    {
        [Key]
        public int ReservationID { get; set; }

        public int RideID { get; set; }

        public string DriverName { get; set; }

        public string PassengerName { get; set; }

        public int SeatsRequired { get; set; }

        public string ReservationStatus { get; set; } 

    }
}