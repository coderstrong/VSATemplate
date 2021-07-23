using FluentValidation;
using Microsoft.Extensions.Logging;
using Org.VSATemplate.Application.Features.Students;
using Org.VSATemplate.Domain.Dtos.Student;

namespace Org.VSATemplate.Domain.Students.Validators
{
    public class StudentForCreationDtoValidation : StudentSharedValidator<StudentForCreationDto>
    {
        public StudentForCreationDtoValidation(ILogger<CreateStudentValidation> logger)
        {
            logger.LogTrace("INSTANCE CREATED - {ClassName}", GetType().Name);
            RuleFor(command => command.Name).NotEmpty().WithMessage("not found");
        }
    }
}