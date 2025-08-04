using System.Reflection.Metadata.Ecma335;
using EscuelaPrimaria.Entity;
using EscuelaPrimaria.Response;
using EscuelaPrimaria.Service;
using EscuelaPrimaria.Service.Repository;
using Microsoft.EntityFrameworkCore;

namespace EscuelaPrimaria.Collection
{
    public class StudentCollection : Respository<Student>, IStudentRespository
    {
        private readonly SchoolContext _Context;
        public StudentCollection(SchoolContext _context):base(_context) 
        {
            _Context = _context;
        }

        public IEnumerable<Student> GetAllStudents()
        {
            return _Context.Students.Include(s=> s.SubjectStudents).Include(e=> e.Attendences)
                .ToList();
        }

        public async Task<IEnumerable<StudentCalificationSummaryResponse>> getListSummaryAsync()
        {
            var data = await _Context.SubjectStudent.Where(s => s.Score.HasValue).ToListAsync();

             
                    return data.GroupBy(ss => ss.Score.Value switch
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
                    });
          
        }

        public Student GetWithSubjectStudent(long Id)
        {
            return _Context.Students.Include(s => s.SubjectStudents).FirstOrDefault(e => e.Id == Id);
        }
    }
}
