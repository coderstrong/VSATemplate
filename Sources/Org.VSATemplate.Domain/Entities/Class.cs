﻿using MakeSimple.SharedKernel.Contract;
using System;
using System.Collections.Generic;

namespace Org.VSATemplate.Domain.Entities
{
    public class Class : Entity<Guid>
    {
        public string ClassCode { get; set; }
        public string Name { get; set; }

        public string Note { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}