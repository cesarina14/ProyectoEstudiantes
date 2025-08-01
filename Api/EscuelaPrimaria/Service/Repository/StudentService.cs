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
        private readonly SchoolContext _Context;
        private readonly ILoggingService _LoggingService;
        public StudentService(SchoolContext _context, ILoggingService _loggingService)
        {
            _Context = _context;
            _LoggingService = _loggingService;
        }
        public async Task<StudentResponse> Add(StudentRequest request)
        {
            try
            {

                var _entity = request.ToEntity();
                _Context.Students.Add(_entity);
                await _Context.SaveChangesAsync();
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
                var _entity = _Context.Students.Find(id);
                if (_entity == null)
                {
                    return new StudentResponse
                    {
                        Code = 500,
                        Message = "No existe el registro",
                        Success = false

                    };
                }
                _Context.Students.Remove(_entity);
                await _Context.SaveChangesAsync();
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

                var query = _Context.Students.Include(s => s.SubjectStudents).AsQueryable();
                var _entity = query.FirstOrDefault(x => x.Id == id);
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
                var query = _Context.Students.Include(s => s.SubjectStudents).AsQueryable();
                var _entity = query.FirstOrDefault(x => x.Id == request.Id);
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
                await _Context.SaveChangesAsync();
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

                var query = _Context.Students.Include(s => s.SubjectStudents).AsQueryable();

                var dtoList = query.Select(e => new StudentDto(e)).ToList();
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

                var _list = await _Context.Students.Where(e => !_Context.Attendence
                                  .Any(a => a.StudentId == e.Id && a.Date.Date == _now.Date))
                                  .ToListAsync();

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
                var data = await _Context.SubjectStudent.Where(s => s.Score.HasValue).ToListAsync();

                var summaryList = data
                        .GroupBy(ss => ss.Score.Value switch
                        {
                            >= 90 => "A",
                            >= 80 => "B",
                            >= 70 => "C",
                            >= 60 => "D",
                            _ => "F"
                        })
                        .Select(g => new StudentCalificationSummaryResponse
                        {
                            Literal = g.Key,
                            Count = g.LongCount(),
                            Range = g.Key switch
                            {
                                "A" => "90 - 100",
                                "B" => "80 - 89",
                                "C" => "70 - 79",
                                "D" => "60 - 69",
                                "F" => "< 60",
                                _ => "N/A"
                            }
                        }).ToList();
                return new Response<List<StudentCalificationSummaryResponse>>
                {
                    Code = 0,
                    Success = true,
                    Value = summaryList
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
