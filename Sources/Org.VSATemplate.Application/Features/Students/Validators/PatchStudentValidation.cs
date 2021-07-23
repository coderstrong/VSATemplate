using Org.VSATemplate.Domain.Dtos.Student;
using Org.VSATemplate.Domain.Students.Validators;

namespace Org.VSATemplate.Application.Features.Students.Validators
{
    public class PatchStudentValidation : StudentSharedValidator<StudentForUpdateDto>
    {
        public PatchStudentValidation()
        {
        }
    }
}
