using EscuelaPrimaria.Entity;

namespace EscuelaPrimaria.Model
{
    public class SubjectStudentDto :IDto
    {
        public long TeacherId { get; set; }
        public long SubjectId { get; set; }
        public long? StudentId { get; set; }
        public string Trimestre { get; set; }
        public string Year { get; set; }
        public decimal? Score { get; set; }
        public DateTime Date { get; set; }

        public SubjectStudentDto()
        {

        }
        public SubjectStudentDto(SubjectStudent entity)
        {
            Id = entity.Id;
            StudentId = entity.StudentId;
            SubjectId = entity.SubjectId;
            TeacherId = entity.TeacherId;
            Year = entity?.Year;
            Trimestre = entity?.Trimestre;
            Score = entity?.Score;
            Date = entity.Date;
            CreatedAt = entity.CreatedAt;
            UpdatedAt = entity.UpdatedAt;
            UpdatedBy = entity.UpdatedBy;
            CreatedBy = entity.CreatedBy;


        }
    }

}





