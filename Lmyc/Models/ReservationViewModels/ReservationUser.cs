using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lmyc.Models.ReservationViewModels
{
    public class ReservationUser
    {
        public string UserName { get; set; }
        public int Credits { get; set; }
        public int AllocatedHours { get; set; }
        public string Roles { get; set; }
    }
}
