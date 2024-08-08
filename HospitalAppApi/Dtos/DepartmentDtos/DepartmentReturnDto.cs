using HospitalApp.DLL.Entities;

namespace HospitalAppApi.Dtos.DepartmentDtos
{
    public class DepartmentReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Limit { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string Image { get; set; }
        public List<DoctorInDepartmentReturnDto> Doctors { get; set; }
    }
    public class DoctorInDepartmentReturnDto
    {
        public string Name { get; set; }
        public int Experience { get; set; }
    }
}
