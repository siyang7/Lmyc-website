using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lmyc.Models.UserViewModels
{
    public class UserRoleData
    {
        public string UserId { get; set; }

        public string Name { get; set; }
        
        public string RoleName { get; set; }

        public int UsedCredit { get; set; }
    }
}
