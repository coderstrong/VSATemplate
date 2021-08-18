using AutoMapper;
using Org.VSATemplate.Domain.Dtos.Student;
using Org.VSATemplate.Domain.Entities;

namespace Org.VSATemplate.Domain.Students.Mappings
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            //createmap<to this, from this>
            CreateMap<Student, ClassDto>()
                .ReverseMap();
            CreateMap<ClassForCreationDto, Student>();
            CreateMap<ClassForUpdateDto, Student>()
                .ReverseMap();
        }
    }
}