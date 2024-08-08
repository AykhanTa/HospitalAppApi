using AutoMapper;
using HospitalApp.DLL.Data;
using HospitalApp.DLL.Entities;
using HospitalAppApi.Dtos.DepartmentDtos;
using HospitalAppApi.Extensions;
using HospitalAppApi.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly HospitalAppContext _context;
        private readonly IMapper _mapper;
        public DepartmentController(HospitalAppContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            var departments = await _context.Departments
                .AsNoTracking()
                .Include(d => d.Doctors)
                .ToListAsync();
            List<DepartmentReturnDto> list = new();
            foreach (var department in departments)
            {
                list.Add(_mapper.Map<DepartmentReturnDto>(department));
            }
            return Ok(list);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var existDepartment =await _context.Departments
                .AsNoTracking()
                .Include("Doctors")
                .FirstOrDefaultAsync(d => d.Id == id);
            if (existDepartment == null) return NotFound();
            return Ok(_mapper.Map<DepartmentReturnDto>(existDepartment));
        }
        [HttpPost("")]
        public async Task<IActionResult> Create(DepartmentCreateDto departmentCreateDto)
        {
            if (await _context.Departments.AnyAsync(d => d.Name.ToLower() == departmentCreateDto.Name.ToLower()))
                return BadRequest("Department name already exists...");
            var file = departmentCreateDto.File;

            Department department = new();
            department.Name = departmentCreateDto.Name;
            department.Limit= departmentCreateDto.Limit;
            department.Image = file.Save(Directory.GetCurrentDirectory(), "images");
            department.CreateDate= DateTime.Now;
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,DepartmentUpdateDto departmentUpdateDto)
        {
            var existDepartment= await _context.Departments.FirstOrDefaultAsync(d => d.Id == id);
            if (existDepartment is null) return NotFound();
            if (existDepartment.Name.ToLower() != departmentUpdateDto.Name.ToLower() && await _context.Departments.AnyAsync(d => d.Id!=existDepartment.Id && d.Name.ToLower() == departmentUpdateDto.Name.ToLower()))
                return BadRequest("Department name already exists..");
            var file = departmentUpdateDto.File;
            if (file!=null)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", existDepartment.Image);
                FileHelper.Delete(path);
                existDepartment.Image = file.Save(Directory.GetCurrentDirectory(), "images");
            }
            existDepartment.Name=departmentUpdateDto.Name;
            existDepartment.Limit=departmentUpdateDto.Limit;
            await _context.SaveChangesAsync();
            return StatusCode(204);


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existDepartment=await _context.Departments
                .FirstOrDefaultAsync(d => d.Id == id);
            if(existDepartment == null) return NotFound();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", existDepartment.Image);
            FileHelper.Delete(path);
            _context.Departments.Remove(existDepartment);
            await _context.SaveChangesAsync();
            return StatusCode(StatusCodes.Status204NoContent);

        }
    }
}
