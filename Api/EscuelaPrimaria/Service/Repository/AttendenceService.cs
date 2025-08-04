using Azure.Core;
using EscuelaPrimaria.Collection;
using EscuelaPrimaria.Entity;
using EscuelaPrimaria.Model;
using EscuelaPrimaria.Request;
using EscuelaPrimaria.Response;
using Microsoft.EntityFrameworkCore;

namespace EscuelaPrimaria.Service.NewFolder
{
    public class AttendenceService : IAttendenceService
    {
        private readonly IAttendenceRespository _Respository;
        private readonly ILoggingService _LoggingService;
        public AttendenceService(IAttendenceRespository _repository, ILoggingService _loggingService)
        {
            _Respository = _repository;
            _LoggingService = _loggingService;
        }
        public async Task<AttendenceResponse> Add(List<AttendenceRequest> request)
        {
            foreach (var item in request)
            {

                var _entity = item.ToEntity();
                try
                {

                    await _Respository.AddAsync(_entity);
                   
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
                var _entity =  await _Respository.Get(id);
                if (_entity == null)
                {
                    return new AttendenceResponse
                    {
                        Code = 500,
                        Message = "No existe el registro",
                        Success = false

                    };
                }
                 _Respository.Delete(_entity);
     
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
                var List = await _Respository.GetAllAsync();
                var _entity = List.FirstOrDefault(r => r.Id ==id);
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
                var query = await _Respository.GetAllAsync();
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


                var List = await _Respository.GetAllAsync();
                var _entity = List.FirstOrDefault(r=> r.Id == request.Id);
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
               _Respository.Update(_entity);

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
