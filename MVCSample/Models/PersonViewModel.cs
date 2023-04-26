using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCSample.Models
{
    public class PersonViewModel
    {
        public long Id { get; set; }
        [Required()]
        public string FirstName { get; set; }
        [Required()]
        public string LastName { get; set; }
        [Required()]
        public string NationalId { get; set; }
        public DateTime Created { get; set; }
        public IEnumerable<UniversityLine> UniversityLines { get; set; } = new List<UniversityLine>();
    }
}