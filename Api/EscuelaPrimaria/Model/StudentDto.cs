using System.ComponentModel.DataAnnotations;
using EscuelaPrimaria.Entity;

namespace EscuelaPrimaria.Model
{
    public class StudentDto : IDto
    {
        public string Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Gender { get; set; } = string.Empty;
        public string LastName { get; set; }
        public int? Age { get; set; }
        public string? Tutor { get; set; }
        public string TutorRelationShip { get; set; } = string.Empty;
        public string TutorPhone { get; set; } = string.Empty;
        public List<SubjectStudentDto> SubjectStudentList { get; set; } = new List<SubjectStudentDto>();

        public StudentDto()
        {

        }

        public StudentDto(Student entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Age = entity.Age;
            LastName = entity.LastName;
            Gender = entity.Gender;
            Phone = entity.Phone;
            Tutor = entity.Tutor;
            TutorPhone = entity.TutorPhone;
            TutorRelationShip = entity.TutorRelationShip;
            Active = entity.Active ?? false;
            SubjectStudentList = entity.SubjectStudents.Select(g => new SubjectStudentDto(g)).ToList();
            CreatedAt = entity.CreatedAt.HasValue ? entity.CreatedAt : DateTime.Now;
            CreatedBy = "Test";

        }
        public void UpdateEntity(Student student)
        {
            student.Name = Name;
            student.LastName = LastName;
            student.Age = Age;
            student.Phone = Phone;
            student.Gender = Gender;
            student.Active = Active;
            student.TutorRelationShip = TutorRelationShip;
            student.TutorPhone = TutorPhone;
            student.Tutor = Tutor;
            student.UpdatedAt = DateTime.Now;
            student.UpdatedBy = "Test";
            if (SubjectStudentList.Any())
            {
                foreach (var _entity in SubjectStudentList)
                {
                    if (_entity.Id <= 0)
                    {
                        var new_subject = new SubjectStudent
                        {
                            StudentId = student.Id,
                            SubjectId = _entity.SubjectId,
                            Score = _entity.Score,
                            TeacherId = _entity.TeacherId,
                            Date = _entity.Date,
                            Trimestre = _entity.Trimestre,
                            Active = _entity.Active,
                            Year = _entity.Year,
                            CreatedAt = DateTime.Now,
                            CreatedBy = "Test"
                        };
                        student.SubjectStudents.Add(new_subject);
                    }
                    else
                    {
                        var existing = student.SubjectStudents.FirstOrDefault(g => g.Id == _entity.Id);

                        if (existing != null)
                        {
                            // Actualizar calificación existente
                            existing.Score = _entity.Score;
                            existing.Trimestre = _entity.Trimestre;
                            existing.Active = _entity.Active;
                            existing.TeacherId = _entity.TeacherId;
                            existing.SubjectId = _entity.SubjectId;
                            existing.Year = _entity.Year;
                            existing.UpdatedAt = DateTime.Now;
                            existing.UpdatedBy = "Test";
                        }
                    }

                }

                var toRemove = student.SubjectStudents
                    .Where(g => !SubjectStudentList.Any(dto => dto.Id == g.Id))
                    .ToList();

                foreach (var g in toRemove)
                {
                    student.SubjectStudents.Remove(g);
                }

            }

        }
    }
}
