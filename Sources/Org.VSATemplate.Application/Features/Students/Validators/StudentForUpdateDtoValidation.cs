using FluentValidation;
using Org.VSATemplate.Domain.Dtos.Student;

namespace Org.VSATemplate.Domain.Students.Validators
{
    public class StudentForUpdateDtoValidation : StudentSharedValidator<StudentForUpdateDto>
    {
        public StudentForUpdateDtoValidation()
        {
            RuleFor(command => command.Name).NotEmpty().WithMessage("Name not found");
        }
    }
}