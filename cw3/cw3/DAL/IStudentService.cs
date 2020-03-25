using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using cw3.Models;
namespace cw3.DAL
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetStudentsAsync();
        Task<IEnumerable<Enrollment>> GetStudentEnrollmentsAsync(string index);
    }
}
