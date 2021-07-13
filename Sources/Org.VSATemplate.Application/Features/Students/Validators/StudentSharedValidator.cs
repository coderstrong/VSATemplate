using FluentValidation;
using Org.VSATemplate.Domain.Dtos.Student;

namespace Org.VSATemplate.Application.Features.Students.Validators
{
    public class StudentSharedValidator<T> : AbstractValidator<T> where T : StudentForManipulationDto
    {
        public StudentSharedValidator()
        {
            // add fluent validation rules that should be shared between creation and update operations here
            //https://fluentvalidation.net/
        }
    }
}