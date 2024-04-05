using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.JsonFieldModels
{
    public class MessageReadedByViewModel
    {
        public List<Guid> ReadedByUsers { get; set; } = new List<Guid>();
    }
}
