using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class ClsLogin
    {
        [Required]
        [StringLength(45)]
        public string user_name { get; set; }
        [Required]
        [StringLength(45)]
        public string password { get; set; }
    }

    public class ClsRegister
    {
        [Required]
        [StringLength(45)]
        public string firstname { get; set; }
        [Required]
        [StringLength(45)]
        public string lastname { get; set; }
        [Required]
        [StringLength(45)]
        public string email_id { get; set; }
        [Required]
        [StringLength(45)]
        public string password { get; set; }
    }

    public class ClsChangePassword
    {
        [Required]
        [StringLength(45)]
        public string old_password { get; set; }
        [Required]
        [StringLength(45)]
        public string new_password { get; set; }
        [Required]
        [StringLength(45)]
        public string email_id { get; set; }
        [Required]
        [StringLength(45)]
        public string user_code { get; set; }
    }
}
