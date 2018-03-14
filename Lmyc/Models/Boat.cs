using LmycEntityLib.Enum;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lmyc.Models
{
    public class Boat
    {
        [Key]
        public string BoatId { get; set; }

        [MaxLength(150)]
        [DisplayName("Boat Name")]
        [Required(ErrorMessage = "Please provide a Boat Name.")]
        public string BoatName { get; set; }

        public BoatStatus BoatStatus { get; set; }

        [MaxLength(1024)]
        [DisplayName("Boat Photo")]
        public byte[] BoatPicture { get; set; }

        [MaxLength(500)]
        [DisplayName("Boat Description")]
        [Required(ErrorMessage = "Please provide a Description.")]
        public string BoatDescription { get; set; }

        [DisplayName("Boat Length")]
        [Required(ErrorMessage = "Please provide a Length.")]
        [Range(1.00, 100000.00, ErrorMessage = "Length must be between 1 and 100,000")]
        public double BoatLength { get; set; }

        [MaxLength(150)]
        [DisplayName("Boats Make")]
        [Required(ErrorMessage = "Please provide a Make.")]
        public string BoatMake { get; set; }

        [DisplayName("Year Boat Was Made")]
        [Required(ErrorMessage = "Please provide a Boat Year.")]
        [Range(1800, 2018, ErrorMessage = "Please provide a valid year.")]
        public int BoatYear { get; set; }

        [Range(0, 1000)]
        [DisplayName("Credits Per Hour Of Usage")]
        public int CreditsPerHourOfUsage { get; set; }

        [ForeignKey("User")]
        [DisplayName("Created By")]
        public string CreatedBy { get; set; }

        [ScaffoldColumn(false)]
        public ApplicationUser User { get; set; }
    }
}
