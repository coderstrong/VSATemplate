using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Org.VSATemplate.Application.Features.Students.Validators
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
