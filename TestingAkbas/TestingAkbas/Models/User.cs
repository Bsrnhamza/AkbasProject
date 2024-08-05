using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestingAkbas.Models
{
    public class User
    {
        public int Id { get; set; } // Kullanıcı ID'si

        [Required]
        [StringLength(50)]
        public string Username { get; set; } // Kullanıcı adı

        [Required]
        [StringLength(50)]
        public string Password { get; set; } // Şifre

        [Required]
        [StringLength(50)]
        public string Role { get; set; } // Rol
    }
}
