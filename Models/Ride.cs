using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ridewithme.Models
{
    public class Ride
    {
        [Key]
        public int RideID { get; set; }

        [Required]
        public RideType Type { get; set; }

        [Required]
        [Display(Name = "Start Location")]
        public string Origin { get; set; }

        [Required]
        [Display(Name = "Destination Location")]
        public string Destination { get; set; }

        [Display(Name = "Schedule")]
        public Schedule Schedule { get; set; }

        [Display(Name = "Occurance")]
        public Occurance Occurance { get; set; }

        // Car details

        [Display(Name = "Vehicle Type")]
        public VehicleType VehicleType { get; set; }

        public string Model { get; set; }

        public string Color { get; set; }

        [Display(Name = "Air Conditioned")]
        public AirConditioned AirConditioned { get; set; }

        [Display(Name = "Available Seats")]
        public int AvailableSeats { get; set; }

        [Display(Name = " Passenger Contribution")]
        public string Contribution { get; set; }

        [DataType(DataType.MultilineText)]
        public string Message { get; set; }

        [Required]
        [Display(Name = "Contact Number")]
        [Phone]
        [StringLength(10, MinimumLength = 10)]
        public string ContactNumber { get; set; }


        // Other Preferences

        [Display(Name = "Non Smoking")]
        public bool IsNonSmoking { get; set; }

        [Display(Name = "Male Only")]
        public bool IsMaleOnly { get; set; }

        [Display(Name = "Female Only")]
        public bool IsFemaleOnly { get; set; }

        public string DateAdded { get; set; }

        public string UserName { get; set; }

    }
    public enum RideType { Offer, Request }
    public enum Schedule { Recurring, [Display(Name = "One Time")] OneTime }
    public enum Occurance { Daily, Weekly, Monthly }
    public enum AirConditioned { Yes, No }
    public enum VehicleType { Car, Van, SUV, Bus }
}