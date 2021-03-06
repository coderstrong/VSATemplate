using System.Collections.Generic;

namespace Org.VSATemplate.Domain.Dtos.Student
{
    public abstract class StudentForManipulationDto
    {
        public string Name { get; set; }

        public string Note { get; set; }

        public virtual ICollection<ClassForCreationDto> Classes { get; set; }
    }
}