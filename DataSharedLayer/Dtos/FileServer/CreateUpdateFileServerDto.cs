using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainShared.Dtos.FileServer
{
    public class CreateUpdateFileServerDto
    {
        public string Title { get; set; }
        public string ConnectionString { get; set; }
    }
}
