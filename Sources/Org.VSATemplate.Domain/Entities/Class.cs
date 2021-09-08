using MakeSimple.SharedKernel.Contract;
using System;
using System.Collections.Generic;

namespace Org.VSATemplate.Domain.Entities
{
    public class Class : Entity
    {
        public Guid Id { get; set; }
        public string ClassCode { get; set; }
        public string Name { get; set; }

        public string Note { get; set; }

        public long StudentId { get; set; }
        public virtual Student Student { get; set; }
    }
}