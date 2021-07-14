using FluentValidation;
using Microsoft.Extensions.Logging;
using Org.VSATemplate.Application.Features.Students;

namespace Org.VSATemplate.Domain.Students.Validators
{
    public class CreateStudentValidation : StudentSharedValidator<AddStudentCommand>
    {
        public CreateStudentValidation(ILogger<CreateStudentValidation> logger)
        {
            logger.LogTrace("INSTANCE CREATED - {ClassName}", GetType().Name);
            RuleFor(command => command.Name).NotEmpty().WithMessage("not found");
        }
    }
}