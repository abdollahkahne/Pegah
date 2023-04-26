using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCSample.Models
{
    public class IndexViewModel
    {
        public IEnumerable<PersonViewModel> People { get; set; }
        public string Start { get; set; }
        public string Finish { get; set; }
        public string Fullname { get; set; }
        public string NationalId { get; set; }
    }
}