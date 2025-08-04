using EscuelaPrimaria.Entity;
using EscuelaPrimaria.Service.Repository;
using EscuelaPrimaria.Service;
using Microsoft.EntityFrameworkCore;

namespace EscuelaPrimaria.Collection
{
    public class AttendenceCollection : Respository<Attendence>, IAttendenceRespository
    {
        public AttendenceCollection(SchoolContext context) : base(context)
        {
        }
    }
}
