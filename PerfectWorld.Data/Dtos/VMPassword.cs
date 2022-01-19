using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfectWorld.Data.Dtos
{
    public class VMPassword
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9''-'\s]{8,10}$")]
        public string key { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9''-'\s]{4,12}$")]
        public string NewPassword { get; set; }

    }
}
