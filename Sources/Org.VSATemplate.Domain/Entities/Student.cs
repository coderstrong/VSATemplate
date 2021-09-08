using MakeSimple.SharedKernel.Contract;
using Sieve.Attributes;
using System.Collections.Generic;

namespace Org.VSATemplate.Domain.Entities
{
    public class Student : AuditEntity
    {
        public long Id { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string Name { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string Note { get; set; }

        public virtual ICollection<Class> Classes { get; set; }
    }
}