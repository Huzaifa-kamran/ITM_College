using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ITM_College.Models
{
    public partial class Facility
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Facility Name is required.")]
        public string? FacilityName { get; set; }

        [Required(ErrorMessage = "Facility Description is required.")]
        public string? FacilityDesc { get; set; }

        [Display(Name = "Image")] // Custom display name
        public string? FacilityImg { get; set; }
    }
}
