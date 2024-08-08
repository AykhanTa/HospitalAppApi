using AutoMapper;
using HospitalApp.DLL.Entities;
using HospitalAppApi.Dtos.DepartmentDtos;

namespace HospitalAppApi.Profiles
{
    public class MapProfile:Profile
    {
        private readonly HttpContextAccessor _contextAccessor;
        public MapProfile(HttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
            var uriBuilder=new UriBuilder
                (
                _contextAccessor.HttpContext.Request.Scheme,
                _contextAccessor.HttpContext.Request.Host.Host,
               (int) _contextAccessor.HttpContext.Request.Host.Port
                );
            var url=uriBuilder.Uri.AbsoluteUri;
            CreateMap<Doctor, DoctorInDepartmentReturnDto>();
            CreateMap<Department, DepartmentReturnDto>()
                .ForMember(d => d.Image, map => map.MapFrom(d =>url+"images/"+d.Image));

        }
    }
}
