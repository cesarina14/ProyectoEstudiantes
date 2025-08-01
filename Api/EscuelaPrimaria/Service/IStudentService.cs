using EscuelaPrimaria.Model;
using EscuelaPrimaria.Request;
using EscuelaPrimaria.Response;

namespace EscuelaPrimaria.Service
{
    public interface IStudentService
    {
         Task<StudentResponse> Add(StudentRequest request);
        Task<StudentResponse> Update(StudentRequest request);
        Task<StudentResponse> Delete(long id);
        Task<StudentResponse> Find( long id );
        Task<StudentResponse> ListAll();

        Task<StudentResponse> listToAttendenceList();
        Task<Response<List<StudentCalificationSummaryResponse>>> getCalificationSummary();
    }
}
