using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lmyc.Models.ReservationViewModels
{
    public class ReservationViewModel
    {
        public int ReservationId { get; set; }

        [Required(ErrorMessage = "Start Date Time is required.")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("From Date")]
        public DateTime StartDateTime { get; set; }

        [Required(ErrorMessage = "End Date Time is required.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("To Date")]
        public DateTime EndDateTime { get; set; }

        [DisplayName("Non-Member Crew")]
        [Required(ErrorMessage = "Please provide valid members.")]
        public string NonMemberCrew { get; set; }

        [MaxLength(1024, ErrorMessage = "Itinerary cannot be more than 1024 character")]
        public string Itinerary { get; set; }

        public double AllocatedHours { get; set; }

        [DisplayName("Member Crew")]
        [Required(ErrorMessage = "Please provide valid members.")]
        public List<ReservationUser> MemberCrew { get; set; }

        [DisplayName("Created By")]
        public string CreatedBy { get; set; }

        public int BoatCreditsPerHour { get; set; }
        
        public int BoatId { get; set; }
    }
}
