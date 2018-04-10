using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lmyc.Models.BookingViewModels
{
    public class BookingUserData
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        
        [Display(Name = "Name")]
        public string Name { get; set; }

        public bool Assigned { get; set; }
    }
}
