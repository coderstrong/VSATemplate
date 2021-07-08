using MakeSimple.SharedKernel.Contract;
using System.Collections.Generic;

namespace Org.VSATemplate.Domain.Entities
{
    public class Student : AuditEntity<long>
    {
        public string Name { get; set; }

        public string Note { get; set; }

        public virtual ICollection<Student> Classes { get; set; }
    }
}
