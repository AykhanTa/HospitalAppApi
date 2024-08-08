using FluentValidation;

namespace HospitalAppApi.Dtos.DoctorDtos
{
    public class DoctorUpdateDto
    {
        public string Name { get; set; }
        public int Experience { get; set; }
        public int DepartmentId { get; set; }

    }
    public class DoctorUpdateDtoValidator : AbstractValidator<DoctorUpdateDto>
    {
        public DoctorUpdateDtoValidator()
        {
            RuleFor(d => d.Name)
               .MaximumLength(20)
               .MinimumLength(3)
               .NotEmpty();
            RuleFor(d => d.Experience)
                .InclusiveBetween(1, 50)
                .NotEmpty();
        }
    }
}
