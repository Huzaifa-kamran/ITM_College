using System;
using System.Collections.Generic;

namespace ITM_College.Models
{
    public partial class Contact
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? UserEmail { get; set; }
        public string? Message { get; set; }
    }
}
