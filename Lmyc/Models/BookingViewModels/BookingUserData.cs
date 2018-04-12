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
        public string Role { get; set; }
        
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public bool Assigned { get; set; }

        public int UsedCredit { get; set; }
    }
}
