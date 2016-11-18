using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KitKat.ViewModels
{
    public class RoleViewModel
    {
        [Required]
        [StringLength(50,MinimumLength =3)]
        public string RoleName { get; set; }
    }
}
