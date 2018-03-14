using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using LmycDataLib.Enum;
using LmycEntityLib.Enum;
using Microsoft.AspNetCore.Identity;

namespace Lmyc.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        /**
         * Users First & Last Names.
         */
        [StringLength(100, MinimumLength = 1, ErrorMessage ="First Name have to be between 1 to 100 charater")]
        [Required(ErrorMessage = "First Name field is required.")]
        [DataType(DataType.Text)]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [StringLength(100, MinimumLength = 1, ErrorMessage = "Last Name have to be between 1 to 100 charater")]
        [Required(ErrorMessage = "Last Name field is required.")]
        [DataType(DataType.Text)]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        /**
         * User address details.
         */
        public string Street { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }

        /**
         * Users Phone numbers.
         */
        [Required]
        public string HomePhone { get; set; }
        public string MobilePhone { get; set; }
        public string WorkPhone { get; set; }

        /**
         * Emergency Contact Phone Numbers
         */
        public string EmergencyContactOnePhone { get; set; }
        public string EmergencyContactTwoPhone { get; set; }

        /**
         * User skills and qualifications.
         */
        public string SailingQualifications { get; set; }
        public string Skills { get; set; }
        public string SailingExperience { get; set; }

        /**
         * Credit Tracking Information.
         */
        public int StartingCredit { get; set; }
        public int UsedCredits { get; set; }
        public int CreditBalance { get; set; }

        /**
         * Member & Skipper Status.
         */
        [ScaffoldColumn(false)]
        public MemberStatus MemberStatus { get; set; }

        [ScaffoldColumn(false)]
        public SkipperStatus SkipperStatus { get; set; }
    }
}
