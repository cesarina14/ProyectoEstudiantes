using EscuelaPrimaria.Model;
using EscuelaPrimaria.Request;
using EscuelaPrimaria.Response;

namespace EscuelaPrimaria.Service
{
    public interface IAttendenceService
    {
        Task<AttendenceResponse> Add(List<AttendenceRequest> request);
        Task<AttendenceResponse> Update(AttendenceRequest request);
        Task<AttendenceResponse> Delete(long id);
        Task<AttendenceResponse> Find(long id);
        Task<AttendenceResponse> ListAll();
    }
}
