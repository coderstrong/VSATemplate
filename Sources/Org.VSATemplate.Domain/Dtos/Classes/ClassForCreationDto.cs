using System;

namespace Org.VSATemplate.Domain.Dtos.Student
{
    public class ClassForCreationDto : ClassForManipulationDto
    {
        public Guid Id { get; set; }
    }
}