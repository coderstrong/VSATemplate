using AutoMapper;
using Org.VSATemplate.Domain.Dtos.Student;
using Org.VSATemplate.Domain.Entities;

namespace Org.VSATemplate.Application.Features.Students.Mappings
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