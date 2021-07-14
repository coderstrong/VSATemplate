using AutoMapper;
using Org.VSATemplate.Domain.Students.Dtos;
using Org.VSATemplate.Domain.Entities;

namespace Org.VSATemplate.Domain.Students.Mappings
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            //createmap<to this, from this>
            CreateMap<Student, StudentDto>()
                .ReverseMap();
            CreateMap<StudentForCreationDto, Student>();
            CreateMap<StudentForUpdateDto, Student>()
                .ReverseMap();
        }
    }
}