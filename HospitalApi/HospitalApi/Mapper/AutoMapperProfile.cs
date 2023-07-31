using API.Entities;
using AutoMapper;
using HospitalApi.DTOs;
using HospitalApi.Models;

namespace HospitalApi.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dto => dto.Username, cfg => cfg.MapFrom(u => u.Name))
                .ForMember(dto => dto.Token, cfg => cfg.Ignore());

            CreateMap<User, DoctorDto>()
                .ForMember(dto => dto.HospitalName, cfg =>
                {
                    cfg.PreCondition(u => u.Hospital is not null);
                    cfg.MapFrom(u => u.Hospital.HospitalName);
                })
                .ForMember(dto => dto.HospitalAddress, cfg =>
                {
                    cfg.PreCondition(u => u.Hospital is not null);
                    cfg.MapFrom(u => u.Hospital.Address);
                });

            CreateMap<Role, RoleDto>();

            CreateMap<Photo, PhotoDto>();
        }
    }
}
