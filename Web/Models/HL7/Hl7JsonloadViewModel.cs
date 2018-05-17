using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Model;

namespace Web.Models.HL7
{
    public class Hl7JsonloadViewModel
    {
        public string OrderId { get; set; } 
        public string Email { get; set; }

        public UserRole Role { get; set; }
    }
}