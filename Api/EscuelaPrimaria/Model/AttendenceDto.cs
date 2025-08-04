using System.Numerics;
using System.Reflection;
using EscuelaPrimaria.Entity;

namespace EscuelaPrimaria.Model
{
    public class AttendenceDto : IDto
    {
        public long StudentId { get; set; }
        public DateTime? Date { get; set; }
        public bool Present { get; set; }
        public AttendenceDto()
        {

        }
        public AttendenceDto(Attendence entity)
        {
            Id = entity.Id;
            Date = entity.Date != null ? entity.Date : DateTime.Now;
            StudentId = entity.StudentId;
            Present = entity.Present;
            CreatedAt = entity.CreatedAt.HasValue ? entity.CreatedAt : DateTime.Now;
            CreatedBy = "Test";
        }
        public void UpdateEntity(Attendence entity)
        {
            entity.Present = Present;
            entity.UpdatedAt = DateTime.Now;
            entity.UpdatedBy = "Test";
        }
    }
}
