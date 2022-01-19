using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfectWorld.Data.Dtos
{
    public class VMLogin
    {
        [Required]
        [RegularExpression(@"^[a-z0-9''-'\s]{4,10}$")]
        public string Usuario { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9''-'\s]{4,12}$")]
        public string Contrasenia { get; set; }

        public bool Valid()
        {
            if (this.Usuario == "") return false;
            if (this.Contrasenia == "") return false;

            return true;
        }
    }
}
