using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVCSample.Data;

namespace MVCSample.Models
{
    public class UniversityLine
    {
        public long UniversityId { get; set; }
        public virtual UniversityViewModel? University {get;set;}
        public EducationLevel Level {get;set;}
        public string? Major {get;set;}
        public string? Minor {get;set;}
        public float GPA {get;set;}
        public string? StartDate {get;set;}
        public string? EndDate {get;set;}
    }
}