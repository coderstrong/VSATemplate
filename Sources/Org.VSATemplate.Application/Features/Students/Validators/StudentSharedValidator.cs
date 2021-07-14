using FluentValidation;
using Org.VSATemplate.Domain.Students.Dtos;

namespace Org.VSATemplate.Domain.Students.Validators
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