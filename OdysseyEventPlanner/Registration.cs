using System;
using System.ComponentModel.DataAnnotations;

namespace OdysseyEventPlanner.Models
{
    public class Registrations
    {
        [Key]
    public int RegistrationID { get; set; } // Unique identifier for each registration

        public string? Username { get; set; } // User's name (if applicable)

        public string? EventType { get; set; } // Type of event (selectable options)

        public string? EventName { get; set; } // Name of the event

        public DateTime EventDate { get; set; } // Date of the event

        public int? NumberOfGuests { get; set; } // Number of guests for the event (nullable)

        public string? Location { get; set; } // Location of the event (selectable options)

        public string? Decor { get; set; } // Description of decor (selectable options)

        public string? Transportation { get; set; } // Transportation details (selectable options)

        public string? SpecialRequest { get; set; } // Any special requests

        public string? Accommodation { get; set; } // Accommodation details (selectable options)

        public string? Package { get; set; } // Package name (if applicable) 
    }
}