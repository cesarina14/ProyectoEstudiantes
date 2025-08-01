using EscuelaPrimaria.Model;
using EscuelaPrimaria.Request;
using EscuelaPrimaria.Response;
using Microsoft.EntityFrameworkCore;

namespace EscuelaPrimaria.Service.NewFolder
{
    public class AttendenceService : IAttendenceeService
    {
        private readonly SchoolContext _Context;
        private readonly ILoggingService _LoggingService;
        public AttendenceService(SchoolContext context, ILoggingService _loggingService)
        {
            _Context = context;
            _LoggingService = _loggingService;
        }
        public async Task<AttendenceResponse> Add(List<AttendenceRequest> request)
        {
            foreach (var item in request)
            {

                var _entity = item.ToEntity();
                try
                {

                    _Context.Attendence.Add(_entity);
                    await _Context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    _LoggingService.LogError(ex.Message, ex);
                    return new AttendenceResponse
                    {
                        Code = 500,
                        Message = ex.Message,

                    };
                }
            }


            return new AttendenceResponse { Success = true, Value = null };
        }

        public async Task<AttendenceResponse> Delete(long id)
        {
            try
            {
                var _entity = _Context.Attendence.Find(id);
                if (_entity == null)
                {
                    return new AttendenceResponse
                    {
                        Code = 500,
                        Message = "No existe el registro",
                        Success = false

                    };
                }
                _Context.Attendence.Remove(_entity);
                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _LoggingService.LogError(ex.Message, ex);
                return new AttendenceResponse
                {
                    Code = 500,
                    Message = ex.Message,

                };
            }

            return new AttendenceResponse { Success = true };
        }

        public async Task<AttendenceResponse> Find(long id)
        {
            try
            {
                var _entity = _Context.Attendence.FirstOrDefault(x => x.Id == id);
                if (_entity == null)
                {
                    return new AttendenceResponse
                    {
                        Code = 500,
                        Message = "No existe el registro",
                        Success = false

                    };
                }
                return new AttendenceResponse { Success = true, Value = new AttendenceDto(_entity) };
            }
            catch (Exception ex)
            {
                _LoggingService.LogError(ex.Message, ex);
                return new AttendenceResponse
                {
                    Code = 500,
                    Message = ex.Message,

                };
            }


        }

        public async Task<AttendenceResponse> ListAll()
        {
            try
            {
                var query = _Context.Attendence.AsQueryable();
                var dtoList = query.Select(e => new AttendenceDto(e)).ToList();
                return new AttendenceResponse { Success = true, Value = dtoList };
            }
            catch (Exception ex)
            {
                _LoggingService.LogError(ex.Message, ex);
                return new AttendenceResponse
                {
                    Code = 500,
                    Message = ex.Message,

                };
            }
        }

        public async Task<AttendenceResponse> Update(AttendenceRequest request)
        {
            try
            {


                var _entity = _Context.Attendence.FirstOrDefault(x => x.Id == request.Id);
                if (_entity == null)
                {
                    return new AttendenceResponse
                    {
                        Code = 500,
                        Message = "No existe el registro",
                        Success = false

                    };
                }

                request.UpdateEntity(_entity);
                await _Context.SaveChangesAsync();

                return new AttendenceResponse { Success = true, Value = new AttendenceDto(_entity) };
            }
            catch (Exception ex)
            {
                _LoggingService.LogError(ex.Message, ex);
                return new AttendenceResponse
                {
                    Code = 500,
                    Message = ex.Message,

                };
            }
        }


    }
}
