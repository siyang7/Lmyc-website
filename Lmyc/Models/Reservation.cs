using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Lmyc.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }

        [ForeignKey("User")]
        [DisplayName("Created By")]
        public string CreatedBy { get; set; }

        [ScaffoldColumn(false)]
        public ApplicationUser User { get; set; }

        [ForeignKey("Boat")]
        [DisplayName("Boat Name")]
        public string ReservedBoat { get; set; }

        [ScaffoldColumn(false)]
        public Boat Boat { get; set; }

        [Required(ErrorMessage = "Start Date Time is required.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("From Date")]
        public DateTime StartDateTime { get; set; }

        [Required(ErrorMessage = "End Date Time is required.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("To Date")]
        public DateTime EndDateTime { get; set; }

        [DisplayName("Member Crew")]
        [Required(ErrorMessage = "Please provide valid members.")]
        public List<ApplicationUser> MemberCrew { get; set; }

        [DisplayName("Non-Member Crew")]
        [Required(ErrorMessage = "Please provide valid members.")]
        public string NonMemberCrew { get; set; }

        [MaxLength(1024, ErrorMessage = "Itinerary cannot be more than 1024 character")]
        public string Itinerary { get; set; }

        public double AllocatedCredit { get; set; }
        public double AllocatedHours { get; set; }
    }
}
