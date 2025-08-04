using EscuelaPrimaria.Entity;
using EscuelaPrimaria.Response;

namespace EscuelaPrimaria.Service
{
    public interface IStudentRespository: IRespository<Student>
    {
        Student GetWithSubjectStudent(long Id);
        IEnumerable<Student> GetAllStudents();
       Task<IEnumerable<StudentCalificationSummaryResponse>> getListSummaryAsync();
    }
}
