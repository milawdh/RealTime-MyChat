using Domain.Audited.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.File
{
    public class TblFile : AuditedValidation<TblFile>
    {
        public Guid Id { get; set; }
        public byte[] Data { get; set; }

    }
}
