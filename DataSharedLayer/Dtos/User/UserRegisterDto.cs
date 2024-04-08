using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainShared.Dtos.User
{
    public class UserRegisterDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} Can't be Empty")]
        [StringLength(32, ErrorMessage = "{0} Length Can't be More Than 32 Characters")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} Can't be Empty")]
        [StringLength(32, ErrorMessage = "{0} Length Can't be More Than 32 Characters")]

        public string LastName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} Can't be Empty")]
        [StringLength(16, ErrorMessage = "{0} Length Can't be More Than 16 Characters")]

        public string Tell { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} Can't be Empty")]
        [StringLength(32, ErrorMessage = "{0} Length Can't be More Than 32 Characters")]

        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} Can't be Empty")]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} Can't be Empty")]
        public string PasswordRepeat { get; set; }
    }

}
