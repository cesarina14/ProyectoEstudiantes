namespace EscuelaPrimaria.Entity
{
    public partial class Subject : Entity
    {
        public string Name { get; set; } = string.Empty;
        public ICollection<SubjectStudent> SubjectStudents { get; set; } = new List<SubjectStudent>();
  
    }
}
