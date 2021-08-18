using FluentValidation;
using Org.VSATemplate.Domain.Dtos.Student;

namespace Org.VSATemplate.Domain.Students.Validators
{
    public class StudentForCreationDtoValidation : StudentSharedValidator<ClassForCreationDto>
    {
        public StudentForCreationDtoValidation()
        {
            RuleFor(command => command.Name).NotEmpty().WithMessage("Name not found");
        }
    }
}