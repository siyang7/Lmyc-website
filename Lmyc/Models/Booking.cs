﻿using System;
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
    }
}
