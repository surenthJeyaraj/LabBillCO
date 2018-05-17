using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Domain.Model;

namespace Web.Models.HL7
{
    public class HL7EntryFormViewModel
    {
        public int Status { get; set; }

        public string HL7Stream { get; set; }

        public string OrderId { get; set; }

       public UserRole? UserRole { get; set; }

        public EntryOption? EntryOption { get; set; }
    }
}