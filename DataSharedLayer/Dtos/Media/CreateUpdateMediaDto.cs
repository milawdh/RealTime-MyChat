using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DomainShared.Dtos.Media
{
    public class CreateUpdateMediaDto
    {
        public IFormFile File { get; set; }
        public TblMessage Message { get; set; }
    }
}
