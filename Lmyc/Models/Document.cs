using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Lmyc.Models
{
    public class Document
    {
        [Key]
        public int DocumentId { get; set; }

        [Display(Name ="DocumentName")]
        public string DocumentName { get; set; }

        [NotMapped]
        public IFormFile FileToUpload { get; set; }

        [Display(Name = "Link")]
        public string Path { get; set; }

        [Display(Name = "Created By")]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
    }
}
