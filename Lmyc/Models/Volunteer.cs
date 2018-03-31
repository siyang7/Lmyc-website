using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Lmyc.Models
{
    public class Volunteer
    {   [Key]
        public int VoluntterId { get; set; }
        [ForeignKey("Id")]
        public virtual ApplicationUser User { get; set; }

        [Required(ErrorMessage = "Date Time is required.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Duration in hours is requried")]
        [Range(1, 100, ErrorMessage = "Duration of voluntter work have to be between 1 to 100 hours")]
        [DisplayName("Number of Hours")]
        public int Duration { get; set; }

        [Required(ErrorMessage = "Description of the Voluntter work is required")]
        [Range(20, 2048, ErrorMessage = "Description of voluntter work have to be between 20 to 2048 letters")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Please select at least one classification code")]
        [DisplayName("Classification Codes")]
        public ClassificationCode ClassificationCodes { get; set; }
    }
    public enum ClassificationCode
    {
        [DisplayName("Boat Maint (Hard)")]
        BoatMaintHard,
        [DisplayName("Boat Maint (Monthly)")]
        BoatMaintMonthly,
        Training,
        [DisplayName("Cruise Skipper (Training)")]
        CruiseSkipperTraining,
        [DisplayName("Day Skipper")]
        DaySkipper,
        Executive,
        [DisplayName("Winter Watch")]
        WinterWatch
    }
}
