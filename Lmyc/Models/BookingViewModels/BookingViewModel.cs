using Lmyc.Models.UserViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lmyc.Models.BookingViewModels
{
    public class BookingViewModel
    {
        public Booking Booking { get; set; }
        public IEnumerable<UserRoleData> UserRoles { get; set; }
    }
}
