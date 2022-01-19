using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfectWorld.Data.Dtos
{
    public class VMUsers
    {

        public int Id { get; set; }
        [MaxLength(10)]
        [RegularExpression(@"^[a-z0-9''-'\s]{4,10}$")]
        public string Usuario { get; set; }
        [MaxLength(10)]
        [RegularExpression(@"^[a-zA-Z0-9''-'\s]{4,12}$")]
        public string Contrasena { get; set; }
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Correo { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9''-'\s]{4,30}$")]
        public string Secret { get; set; }

        public bool Valid()
        {
            if (this.Usuario == "") return false;
            if (this.Contrasena == "") return false;
            if (this.Correo == "") return false;
            if (this.Secret == "") return false;

            return true;
        }
    }
}
