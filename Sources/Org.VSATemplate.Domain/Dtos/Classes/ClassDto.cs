using MakeSimple.SharedKernel.Contract;

namespace Org.VSATemplate.Domain.Dtos.Student
{
    public class ClassDto : AuditEntity<long>
    {
        public string Name { get; set; }

        public string Note { get; set; }
    }
}