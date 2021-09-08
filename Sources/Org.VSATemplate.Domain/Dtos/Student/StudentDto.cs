using MakeSimple.SharedKernel.Contract;

namespace Org.VSATemplate.Domain.Dtos.Student
{
    public class StudentDto : AuditEntity
    {
        public string Name { get; set; }

        public string Note { get; set; }
    }
}