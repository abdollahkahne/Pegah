using System.ComponentModel.DataAnnotations;

namespace MVCSample.Data
{
    public class Education {
        public long Id {get;set;}
        public EducationLevel Level { get; set; }
        public string? Major { get; set; }
        public string? Minor {get;set;}
        public float? GPA {get;set;}
        public virtual University? University {get;set;}
        public long? UniversityId {get;set;}
        public DateTime? Start {get;set;}
        public DateTime? End {get;set;}
        public virtual Person Person {get;set;}
        public long PersonId {get;set;}

    }
     public enum EducationLevel {
        Collegue,
        Bachelor,
        Master,
        PHD,
        [Display(Name="هیچکدام")]
        None,
    }
}