using MakeSimple.SharedKernel.Contract;

namespace Org.VSATemplate.Domain.Dtos.Student
{
    public class ClassDto : AuditEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public string Note { get; set; }
    }
}