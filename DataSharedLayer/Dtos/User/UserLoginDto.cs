using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainShared.Dtos.User
{
    public class UserLoginDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} Can't be Empty")]
        public string UserName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} Can't be Empty")]
        public string Password { get; set; }
    }
}
