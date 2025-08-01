using EscuelaPrimaria.Entity;
using EscuelaPrimaria.Model;

namespace EscuelaPrimaria.Request
{
    public class AttendenceRequest:AttendenceDto

    {
    }
    public static class AttendenceMapper
    {
        public static Attendence ToEntity(this AttendenceRequest request)
        {
            return new Attendence
            {
               
                StudentId = request.StudentId,
                Date = DateTime.Now.Date,
                Present = request.Present,
                CreatedAt = DateTime.Now,
                CreatedBy = "Test"

                
            };
        }
    }
}
