using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cw3.DAL;
using cw3.Models;
using Microsoft.AspNetCore.Mvc;

namespace cw3.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private readonly IDbService _dbService;
        public StudentsController(IDbService dbService)
        {
            _dbService = dbService;
        }
        [HttpGet]
        public IActionResult GetStudents(string orderBy)
        {
            return Ok(_dbService.GetStudents());
        }
        [HttpGet("{id}")]
       public IActionResult GetStudents(int id)
        {
            if (id == 1)
            {
                return Ok("Kowalski");
            }
            else if (id == 2)
            {
                return Ok("Malewksi");
            }
            return NotFound("Nie znaleziono studenta");
        }
        [HttpPost]
        public IActionResult CreateStudents(Student student)
        {
            student.IndexNumber = $"s{new Random().Next(1, 20000)}";
            return Ok(student);
        }

        [HttpPut("{id}")]
        public IActionResult PutStudents(int id)
        {
            return Ok("Aktualizacja dokonczona");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudents(int id)
        {
            return Ok("Aktualizacja dokonczona");
        }
    }
}
