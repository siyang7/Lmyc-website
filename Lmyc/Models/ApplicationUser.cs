using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Lmyc.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        /**
         * Users First and Last Names.
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
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Street field must be between 1 & 100 characters")]
        [Required(ErrorMessage = "Street field is required.")]
        [DataType(DataType.Text)]
        public string Street { get; set; }

        [StringLength(100, MinimumLength = 1, ErrorMessage = "City field must be between 1 & 100 characters")]
        [Required(ErrorMessage = "Street field is required.")]
        [DataType(DataType.Text)]
        public string City { get; set; }

        [Required(ErrorMessage = "Postal code field is required.")]
        [DataType(DataType.Text)]
        [DisplayName("Postal Code")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Province field is required.")]
        [DataType(DataType.Text)]
        [DisplayName("Province")]
        public string Province { get; set; }

        [StringLength(100, MinimumLength = 1, ErrorMessage = "Country must be between 1 & 100 characters")]
        [Required(ErrorMessage = "Country field is required.")]
        [DataType(DataType.Text)]
        public string Country { get; set; }

        /**
         * Users Phone numbers.
         */
        [Required(ErrorMessage = "At least a home phone number is required.")]
        [DisplayName("Home Phone")]
        [Phone]
        public string HomePhone { get; set; }
        
        [DisplayName("Mobile Phone")]
        [Phone]
        public string MobilePhone { get; set; }
        
        [DisplayName("Work Phone")]
        [Phone]
        public string WorkPhone { get; set; }

        /**
         * Emergency Contact Phone Numbers
         */
        [Required(ErrorMessage = "At least one emergency contact number is required.")]
        [DisplayName("First Emergency Contact")]
        [Phone]
        public string EmergencyContactOne { get; set; }

        [DisplayName("Second Emergency Contact")]
        [Phone]
        public string EmergencyContactTwo { get; set; }

        /**
         * User skills and qualifications.
         */
        [Required(ErrorMessage = "Your sailing qualifications is needed.")]
        [DisplayName("Sailing Qualifications")]
        [DataType(DataType.Text)]
        public string SailingQualifications { get; set; }

        [Required(ErrorMessage = "Your sailing qualifications is needed.")]
        [DataType(DataType.Text)]
        public string Skills { get; set; }

        [Required(ErrorMessage = "Your sailing experience is needed.")]
        [DisplayName("Sailing Experience")]
        public int SailingExperience { get; set; }

        /**
         * Credit Tracking Information.
         */
        [DisplayName("Starting Credits")]
        [Range(0, 1000)]
        public int StartingCredit { get; set; }

        [DisplayName("Used Credits")]
        [Range(0, 1000)]
        public int UsedCredits { get; set; }

        [DisplayName("Credit Balance")]
        [Range(0, 1000)]
        public int CreditBalance { get; set; }

        ICollection<UserBooking> UserBookings { get; set; }
    }
}
