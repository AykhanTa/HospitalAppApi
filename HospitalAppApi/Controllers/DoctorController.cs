using AutoMapper;
using HospitalApp.DLL.Data;
using HospitalApp.DLL.Entities;
using HospitalAppApi.Dtos.DoctorDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly HospitalAppContext _appContext;
        private readonly IMapper _mapper;

        public DoctorController(HospitalAppContext appContext, IMapper mapper)
        {
            _appContext = appContext;
            _mapper = mapper;
        }

        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            var doctors=await _appContext.Doctors.AsNoTracking().Include(d=>d.Department).ToListAsync();
            List<DoctorReturnDto> list = new();
            foreach (var doctor in doctors)
            {
                list.Add(_mapper.Map<DoctorReturnDto>(doctor));
            }
            return Ok(list);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var existDoctor = await _appContext.Doctors
                .AsNoTracking()
                .Include("Department")
                .FirstOrDefaultAsync(d => d.Id == id);
            if (existDoctor == null) return NotFound();
            return Ok(_mapper.Map<DoctorReturnDto>(existDoctor));
        }
        [HttpPost("")]
        public async Task<IActionResult> Create(DoctorCreateDto doctorCreateDto)
        {
            if (!await _appContext.Departments.AnyAsync(d => d.Id == doctorCreateDto.DepartmentId))
                return BadRequest("Department is not found...");
            Doctor doctor = new();
            doctor.Name=doctorCreateDto.Name;
            doctor.Experience=doctorCreateDto.Experience;
            doctor.DepartmentId=doctorCreateDto.DepartmentId;
            doctor.CreateDate = DateTime.Now;
            await _appContext.Doctors.AddAsync(doctor);
            await _appContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existDoctor = await _appContext.Doctors.FirstOrDefaultAsync(d => d.Id == id);
            if (existDoctor is null) return NotFound();
            _appContext.Doctors.Remove(existDoctor);
            await _appContext.SaveChangesAsync();
            return NoContent();

        }
        //[HttpPut("{id}")]
        //public IActionResult Update(int id)
        //{

        //}

    }
}
