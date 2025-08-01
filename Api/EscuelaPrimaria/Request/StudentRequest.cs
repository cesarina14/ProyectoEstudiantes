using EscuelaPrimaria.Entity;
using EscuelaPrimaria.Model;

namespace EscuelaPrimaria.Request
{
    public class StudentRequest : StudentDto
    {
        public StudentRequest(){}
        public StudentRequest(Student entity) 
            : base(entity)
        {
        }
    }
    public static class StudentMapper
    {
        public static Student ToEntity(this StudentRequest request)
        {
            return new Student
            {
                Name = request.Name,
                LastName = request.LastName,
                Phone = request.Phone,
                Age = request.Age,
                Gender = request.Gender,
                Active = request.Active,
                Tutor = request.Tutor,
                TutorPhone = request.TutorPhone,
                TutorRelationShip = request.TutorRelationShip,
                CreatedAt = DateTime.Now,
                CreatedBy = "Test",
                SubjectStudents = request.SubjectStudentList?.Select(c => new SubjectStudent
                {
                    StudentId = c.StudentId.Value,
                    SubjectId = c.SubjectId,
                    Score = c.Score,
                    Trimestre = c.Trimestre,
                    Year = c.Year,
                    TeacherId = c.TeacherId,
                    CreatedAt = DateTime.Now,
                    CreatedBy = "Test"

                }).ToList() ?? new List<SubjectStudent>()


            };

        }
    }
}
