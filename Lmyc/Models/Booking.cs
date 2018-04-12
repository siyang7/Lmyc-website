using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Lmyc.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        [Required(ErrorMessage = "Start Date Time is required.")]
        [DisplayFormat(DataFormatString = "{0:MMM dd, yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("From Date")]
        public DateTime StartDateTime { get; set; }

        [Required(ErrorMessage = "End Date Time is required.")]
        [DisplayFormat(DataFormatString = "{0:MMM dd, yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("To Date")]
        public DateTime EndDateTime { get; set; }

        [DisplayName("Non-Member Crews")]
        [Required(ErrorMessage = "Please provide valid members.")]
        public string NonMemberCrews { get; set; }

        [MaxLength(1024, ErrorMessage = "Itinerary cannot be more than 1024 character")]
        public string Itinerary { get; set; }
        
        [Display(Name = "Allocated Hours")]
        public int AllocatedHours { get; set; }

        [Display(Name = "Created By")]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        public int BoatId { get; set; }

        [ForeignKey("BoatId")]
        public Boat Boat { get; set; }

        public ICollection<UserBooking> UserBookings { get; set; }

        public void CalculateHours()
        {
            if (StartDateTime > EndDateTime)
            {
                AllocatedHours = 0;
                return;
            }
            int totalCredit = 0;
            DateTime tomorrow = StartDateTime;
            var day = StartDateTime;
            int max = 10;
            int totalDays = StartDateTime.Date.Subtract(EndDateTime.Date).Duration().Days + 1;
            for (int i = 0; i < totalDays; i++)
            {
                max = (day.DayOfWeek.Equals(DayOfWeek.Saturday) || day.DayOfWeek.Equals(DayOfWeek.Sunday)) ? 15 : 10;
                if (i == totalDays - 1)
                {
                    var hourDiff = EndDateTime.Subtract(tomorrow).Hours;
                    totalCredit += (hourDiff >= max || hourDiff == 0) ? max : hourDiff;
                }
                else
                {
                    tomorrow = day.AddDays(1).AddHours(-day.Hour);
                    var hourDiff = (tomorrow.Subtract(day)).Hours;
                    day = tomorrow;
                    totalCredit += (hourDiff >= max || hourDiff == 0) ? max : hourDiff;
                }
            }
            AllocatedHours = totalCredit;
        }
    }
}
