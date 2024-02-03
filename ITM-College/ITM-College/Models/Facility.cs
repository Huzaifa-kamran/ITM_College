using System;
using System.Collections.Generic;

namespace ITM_College.Models
{
    public partial class Facility
    {
        public int Id { get; set; }
        public string? FacilityName { get; set; }
        public string? FacilityDesc { get; set; }
        public string? FacilityImg { get; set; }
    }
}
