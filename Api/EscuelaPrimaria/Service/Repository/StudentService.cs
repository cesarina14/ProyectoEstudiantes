using System;
using EscuelaPrimaria.Entity;
using EscuelaPrimaria.Model;
using EscuelaPrimaria.Request;
using EscuelaPrimaria.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EscuelaPrimaria.Service.NewFolder
{
    public class StudentService : IStudentService
    {
       
        private readonly ILoggingService _LoggingService;
        private readonly IStudentRespository _Respository;
        public StudentService(SchoolContext _context, ILoggingService _loggingService, IStudentRespository _respository)
        {
            
            _LoggingService = _loggingService;
            _Respository = _respository;
        }
        public async Task<StudentResponse> Add(StudentRequest request)
        {
            try
            {

                var _entity = request.ToEntity();
                await _Respository.AddAsync(_entity);
                
                return new StudentResponse { Success = true, Value = new StudentDto(_entity) };
            }
            catch (Exception ex)
            {
                _LoggingService.LogError(ex.Message, ex);
                return new StudentResponse
                {
                    Code = 500,
                    Message = ex.Message,
                    Success= false,
                    
                };
            }
           
          
        }

        public async Task<StudentResponse> Delete(long id)
        {
            try
            {
                var _entity = await _Respository.Get(id);
                if (_entity == null)
                {
                    return new StudentResponse
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
                return new StudentResponse
                {
                    Code = 500,
                    Message = ex.Message,
                    Success = false

                };
            }
        
       
            return new StudentResponse { Success = true };
        }

        public async Task<StudentResponse> Find(long id)
        {
            try
            {

                var _entity = _Respository.GetWithSubjectStudent(id);
             
                if (_entity == null)
                {
                    return new StudentResponse
                    {
                        Code = 500,
                        Message = "No existe el registro",
                        Success = false

                    };
                }
                return new StudentResponse { Success = true, Value = new StudentDto(_entity) };
            }
            catch (Exception ex)
            {
                _LoggingService.LogError(ex.Message, ex);
                return new StudentResponse
                {
                    Code = 500,
                    Message = ex.Message,
                    Success = false

                };
            }
         
        }

        public async Task<StudentResponse> Update(StudentRequest request)
        {
            try
            {
                
                var _entity = _Respository.GetWithSubjectStudent(request.Id);
                if (_entity == null)
                {
                    return new StudentResponse
                    {
                        Code = 500,
                        Message = "No existe el registro",
                        Success = false

                    };
                }

                request.UpdateEntity(_entity);
                  _Respository.Update(_entity);
                return new StudentResponse { Success = true, Value = new StudentDto(_entity) };
            }
            catch(Exception ex)
            {
                _LoggingService.LogError(ex.Message, ex);
                return new StudentResponse
                {
                    Code = 500,
                    Message = ex.Message,
                    Success = false

                };
            }
            
        }

        public async Task<StudentResponse> ListAll()
        {
            try
            {

                var dtoList = _Respository.GetAllStudents().Select(e => new StudentDto(e)).ToList();
                return new StudentResponse { Success = true, Value = dtoList };
            }
            catch (Exception ex)
            {
                _LoggingService.LogError(ex.Message, ex);
                return new StudentResponse
                {
                    Code = 500,
                    Message = ex.Message,
                    Success = false

                };
            }
        }
        public async Task<StudentResponse> listToAttendenceList()
        {
            try
            {
                var _now = DateTime.Now;

                var _list =  _Respository.GetAllStudents().Where(e => !e.Attendences
                                  .Any(a => a.StudentId == e.Id && a.Date.Date == _now.Date))
                                  .ToList();

                var dtoList = _list.Select(e => new StudentDto(e)).ToList();
                return new StudentResponse { Success = true, Value = dtoList };
            }
            catch(Exception ex)
            {
                _LoggingService.LogError(ex.Message, ex);
                return new StudentResponse
                {
                    Code = 500,
                    Message = ex.Message,
                    Success = false

                };
            }
        }

        public  async Task<Response<List<StudentCalificationSummaryResponse>>> getCalificationSummary()
        {
            try
            {
                var list =  _Respository.getListSummaryAsync().Result.ToList();
                return new Response<List<StudentCalificationSummaryResponse>>
                {
                    Code = 0,
                    Success = true,
                    Value = list
                };
            }
            catch(Exception ex)
            {
                _LoggingService.LogError(ex.Message, ex);
                return new Response<List<StudentCalificationSummaryResponse>>
                {
                    Code = 0,
                    Success = false,
                    Message = ex.Message
                };
            }
            ;
    

          


        }
    }
}
