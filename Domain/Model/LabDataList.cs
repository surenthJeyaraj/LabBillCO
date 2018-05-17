using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Model
{
    public class LabDataList
    {
        public string AssignedTo { get; set; }
        public int Status { get; set; }
        public string ScanId { get; set; }
        public string FileName { get; set; }
        public string LabToken { get; set; }

        public string FileUrl { get; set; }
        public Guid LabscanGuid { get; set; }
    }
}
