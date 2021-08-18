using System.Collections.Generic;

namespace Org.VSATemplate.Domain.Dtos.Student
{
    public abstract class StudentForManipulationDto
    {
        public string Name { get; set; }

        public string Note { get; set; }

        public List<ClassForManipulationDto> Classes { get; set; }
    }
}