using MakeSimple.SharedKernel.Contract;
using System;

namespace Org.VSATemplate.Domain.Entities
{
    public class Student : AuditEntity<long>
    {
        public string Name { get; set; }

        public string Note { get; set; }

        public virtual Class Class { get; set; }
        public Guid ClassId { get; set; }
    }
}