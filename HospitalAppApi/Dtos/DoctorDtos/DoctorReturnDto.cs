﻿using HospitalApp.DLL.Entities;

namespace HospitalAppApi.Dtos.DoctorDtos
{
    public class DoctorReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Experience { get; set; }
        public int DepartmentId { get; set; }
        public DepartmentInDoctorReturnDto Department { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
    public class DepartmentInDoctorReturnDto
    {
        public string Name { get; set; }
        public int DoctorsCount { get; set; }
    }
}
