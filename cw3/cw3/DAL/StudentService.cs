using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using cw3.Models;



namespace cw3.DAL
{
    public class StudentService : IStudentService
    {
        private readonly string _connectionString = "Data Source=db-mssql;Initial Catalog=s18579;Integrated Security=True";

        public async Task<IEnumerable<Student>> GetStudentsAsync()
        {
            var students = new List<Student>();

            using (var conn = new SqlConnection(_connectionString))
            using (var command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = "SELECT IndexNumber, FirstName, LastName, BirthDate FROM Student";

                conn.Open();
                var dataReader = await command.ExecuteReaderAsync();
                while (await dataReader.ReadAsync())
                {
                    var student = new Student
                    {
                        IndexNumber = dataReader["IndexNumber"].ToString(),
                        FirstName = dataReader["FirstName"].ToString(),
                        LastName = dataReader["LastName"].ToString(),
                        BirthDate = DateTime.Parse(dataReader["BirthDate"].ToString())
                    };
                    students.Add(student);
                }
            }

            return students;
        }

        public async Task<IEnumerable<Enrollment>> GetStudentEnrollmentsAsync(string index)
        {
            var enrollments = new List<Enrollment>();

            using (var conn = new SqlConnection(_connectionString))
            using (var command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText =
                    @"  SELECT 
                        e.IdEnrollment, e.Semester, e.StartDate, e.StartDate, st.IdStudy, st.Name FROM Student s
                        LEFT JOIN Enrollment e on e.IdEnrollment = s.IdEnrollment
                        LEFT JOIN Studies st on st.IdStudy = e.IdEnrollment
                        WHERE 
                        s.IndexNumber = @index";
                command.Parameters.AddWithValue("index", index);

                conn.Open();
                var dataReader = await command.ExecuteReaderAsync();
                while (await dataReader.ReadAsync())
                {
                    var enrollment = new Enrollment
                    {
                        IdEnrollment = int.Parse(dataReader["IdEnrollment"].ToString()),
                        Semester = int.Parse(dataReader["Semester"].ToString()),
                        StartDate = DateTime.Parse(dataReader["StartDate"].ToString()),
                        Study = new Study
                        {
                            IdStudy = int.Parse(dataReader["IdStudy"].ToString()),
                            Name = dataReader["Name"].ToString(),
                        }
                    };
                    enrollments.Add(enrollment);
                }
            }

            return enrollments;
        }
    }
}