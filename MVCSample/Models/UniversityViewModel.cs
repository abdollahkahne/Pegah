using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVCSample.Data;

namespace MVCSample.Models
{
    public class UniversityViewModel
    {
        public UniversityType Type { get; set; }
        public string Name {get;set;}
        public string Address {get;set;}
    }
}