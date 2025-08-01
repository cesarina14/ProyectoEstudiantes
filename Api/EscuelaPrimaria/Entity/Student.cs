using System.ComponentModel.DataAnnotations;

namespace EscuelaPrimaria.Entity
{
    public partial class Student : Entity
    {
     

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;
        [Phone]
        [MaxLength(20)]
        public string? Phone { get; set; }
    
        public int? Age { get; set; }
        public string? Gender { get; set; }
        public string? Tutor { get; set; }
        public string TutorRelationShip { get; set; } = string.Empty;
        public string TutorPhone { get; set; } = string.Empty;
        [Phone]
        public bool? Active { get; set; } = true;

        public ICollection<SubjectStudent> SubjectStudents { get; set; } = new List<SubjectStudent>();
        public ICollection<Attendence> Attendences { get; set; } = new List<Attendence>();
        
    }
}


