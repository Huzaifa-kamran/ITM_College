using System;
using System.Collections.Generic;

namespace ITM_College.Models
{
    public partial class Admin
    {
        public int Id { get; set; }
        public string? AdminName { get; set; }
        public string? AdminEmail { get; set; }
        public string? Password { get; set; }
        public int? Role { get; set; }
        public string? AdminImg { get; set; }
    }
}
