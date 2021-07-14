using MakeSimple.SharedKernel.Contract;

namespace Org.VSATemplate.Domain.Students.Dtos
{
    public class StudentDto : AuditEntity<long>
    {
        public string Name { get; set; }

        public string Note { get; set; }
    }
}