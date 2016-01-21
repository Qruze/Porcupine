using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Porcupine.Models
{
    public class Project
    {
        
        public int Id { get; set; }

        public string Name { get; set; }
        public List<Part> Parts { get; set; }
    }

    public class Part
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public int NumOfDays { get; set; }
        public bool OnlyWorkDays { get; set; }

    }
}