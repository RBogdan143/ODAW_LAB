using Lab6.Data;
using Lab6.Models.DTOs;
using Lab6.Models.One_to_Many;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatabaseController : ControllerBase
    {
        private readonly Lab6Context _lab5Context;

        public DatabaseController(Lab6Context lab4Context)
        {
            _lab5Context = lab4Context;
        }

        [HttpGet("model1")]
        public async Task<IActionResult> GetModel1()
        {
            return Ok(await _lab5Context.Models1.ToListAsync());
        }

        [HttpPost("model1")]
        public async Task<IActionResult> Create (Model1DTO model1Dto)
        {
            var newModel1 = new Model1
            {
                Id = Guid.NewGuid(),
                Name = model1Dto.Name
            };

            await _lab5Context.AddAsync(newModel1);
            await _lab5Context.SaveChangesAsync();

            return Ok(newModel1);
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(Model1DTO model1Dto)
        {
            Model1 model1ById = await _lab5Context.Models1.FirstOrDefaultAsync(x => x.Id == model1Dto.Id);
            if (model1ById == null)
            {
                return BadRequest("Object does not exist");
            }

            model1ById.Name = model1Dto.Name;
            _lab5Context.Update(model1ById);
            await _lab5Context.SaveChangesAsync();
            
            return Ok(model1ById);
        }

        //Temă 5
        [HttpGet("Teacher")]
        public async Task<IActionResult> GetTeacher()
        {
            return Ok(await _lab5Context.Teachers.ToListAsync());
        }

        [HttpPost("Teacher")]
        public async Task<IActionResult> Create(TeacherDTO teacherDto)
        {
            var newTeacher = new Teacher
            {
                Id = Guid.NewGuid(),
                Name = teacherDto.Name,
                Salary = teacherDto.Salary
            };

            await _lab5Context.AddAsync(newTeacher);
            await _lab5Context.SaveChangesAsync();

            return Ok(newTeacher);
        }

        [HttpPut("Teacher_update")]
        public async Task<IActionResult> Update(TeacherDTO teacherDto)
        {
            Teacher TeacherById = await _lab5Context.Teachers.FirstOrDefaultAsync(x => x.Id == teacherDto.Id);
            if (TeacherById == null)
            {
                return BadRequest("Object does not exist");
            }

            TeacherById.Name = teacherDto.Name;
            TeacherById.Salary = teacherDto.Salary;
            _lab5Context.Update(TeacherById);
            await _lab5Context.SaveChangesAsync();

            return Ok(TeacherById);
        }

        [HttpGet("Manual")]
        public async Task<IActionResult> GetManual()
        {
            return Ok(await _lab5Context.Manuals.ToListAsync());
        }

        [HttpPost("Manual")]
        public async Task<IActionResult> Create(ManualDTO manualDto)
        {
            var newManual = new Manual
            {
                Id = Guid.NewGuid(),
                Name = manualDto.Name,
                Description = manualDto.Description,
                TeacherId = manualDto.TeacherId
            };

            var teacher = await _lab5Context.Teachers.FirstOrDefaultAsync(t => t.Id == manualDto.TeacherId);
            if (teacher == null)
            {
                return BadRequest("Teacher does not exist");
            }

            if (teacher.Manuals == null)
            {
                teacher.Manuals = new List<Manual>();
            }
            teacher.Manuals.Add(newManual);

            await _lab5Context.AddAsync(newManual);
            await _lab5Context.SaveChangesAsync();

            return Ok(newManual);
        }

        [HttpPut("Manual_update")]
        public async Task<IActionResult> Update(ManualDTO manualDto)
        {
            Manual manualById = await _lab5Context.Manuals.FirstOrDefaultAsync(x => x.Id == manualDto.Id);
            if (manualById == null)
            {
                return BadRequest("Object does not exist");
            }

            manualById.Name = manualDto.Name;
            manualById.Description = manualDto.Description;
            _lab5Context.Update(manualById);
            await _lab5Context.SaveChangesAsync();

            return Ok(manualById);
        }

        //Temă 6
        [HttpGet("TeacherWithManual")]
        public async Task<IActionResult> GetTeacherWithManual()
        {
            var teachersWithManuals = await _lab5Context.Teachers.Include(x => x.Manuals).ToListAsync();
            return Ok(teachersWithManuals);
        }

        [HttpGet("TeacherManualJoin")]
        public async Task<IActionResult> GetTeacherManualJoin()
        {
            var teacherManualJoin = await _lab5Context.Teachers.Join(
                _lab5Context.Manuals,
                teacher => teacher.Id,
                manual => manual.TeacherId,
                (teacher, manual) => new
                {
                    TeacherName = teacher.Name,
                    ManualName = manual.Name
                }).ToListAsync();

            return Ok(teacherManualJoin);
        }
    }
}
