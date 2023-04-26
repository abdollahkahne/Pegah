using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCSample.Data
{
    public class Person
    {
        public long Id { get; set; }
        public string FirstName {get;set;}
        public string LastName { get; set; }
        public string? NationalId {get;set;}
        public virtual ICollection<Education> Educations {get;}=new List<Education>();
        public DateTime Created { get; set; }

    }
   
    
}