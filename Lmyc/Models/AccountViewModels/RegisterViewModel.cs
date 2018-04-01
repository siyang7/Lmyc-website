using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lmyc.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        /**
         * Users First and Last Names.
         */
        [StringLength(100, MinimumLength = 1)]
        [Required(ErrorMessage = "First Name field is required.")]
        [DataType(DataType.Text)]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [StringLength(100, MinimumLength = 1)]
        [Required(ErrorMessage = "Last Name field is required.")]
        [DataType(DataType.Text)]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [StringLength(100, MinimumLength = 1, ErrorMessage = "Street field must be between 1 & 100 characters")]
        [Required(ErrorMessage = "Street field is required.")]
        [DataType(DataType.Text)]
        public string Street { get; set; }

        [StringLength(100, MinimumLength = 1, ErrorMessage = "City field must be between 1 & 100 characters")]
        [Required(ErrorMessage = "Street field is required.")]
        [DataType(DataType.Text)]
        public string City { get; set; }

        [Required(ErrorMessage = "Province field is required.")]
        [DataType(DataType.Text)]
        [DisplayName("Province")]
        public string Province { get; set; }

        [Required(ErrorMessage = "Postal code field is required.")]
        [DataType(DataType.Text)]
        [DisplayName("Postal Code")]
        public string PostalCode { get; set; }

        [StringLength(100, MinimumLength = 1, ErrorMessage = "Country must be between 1 & 100 characters")]
        [Required(ErrorMessage = "Country field is required.")]
        [DataType(DataType.Text)]
        public string Country { get; set; }

        [Required]
        [DisplayName("Home Phone")]
        [Phone]
        public string HomePhone { get; set; }

        [Required(ErrorMessage = "At least one emergency contact number is required.")]
        [DisplayName("First Emergency Phone")]
        [Phone]
        public string EmergencyContactOne { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

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
    }
}
