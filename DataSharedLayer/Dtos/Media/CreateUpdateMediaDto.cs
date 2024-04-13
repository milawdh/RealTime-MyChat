using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainShared.Dtos.Media
{
    public class CreateUpdateMediaDto
    {
        public MediaType MediaType { get; set; }

        public string FileName { get; set; }
        public string FileMimType { get; set; }

        public Guid FileServerId { get; set; }

        public Guid MessageId { get; set; }
    }
}
