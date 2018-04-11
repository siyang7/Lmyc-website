using Lmyc.Models.UserViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lmyc.Models.BookingViewModels
{
    public class BookingViewModel
    {
        public int BookingId { get; set; }

        public int BoatId { get; set; }

        public string BoatName { get; set; }

        [Required(ErrorMessage = "Start Date Time is required.")]
        [DisplayFormat(DataFormatString = "{0:MMM dd, yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "From Date")]
        public DateTime StartDateTime { get; set; }

        [Required(ErrorMessage = "End Date Time is required.")]
        [DisplayFormat(DataFormatString = "{0:MMM dd, yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "To Date")]
        public DateTime EndDateTime { get; set; }

        [Display(Name = "Non-Member Crews")]
        public string NonMemberCrews { get; set; }

        [MaxLength(1024, ErrorMessage = "Itinerary cannot be more than 1024 character")]
        public string Itinerary { get; set; }

        [Display(Name = "Allocated Hours")]
        public int AllocatedHours { get; set; }

        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public IEnumerable<UserRoleData> MemberCrews { get; set; }
    }
}
