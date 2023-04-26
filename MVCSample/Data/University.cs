namespace MVCSample.Data
{
    public class University {
        public long Id {get;set;}
        public string? Name { get; set; }
        public UniversityType Type { get; set; }
        public string? Address {get;set;}
        public virtual ICollection<Education> Educations {get;}=new List<Education>();
    }
    public enum UniversityType {
        Free,
        State,
    }
}