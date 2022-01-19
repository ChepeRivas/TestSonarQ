using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfectWorld.Data.Helpers.Atributtes
{
    public class LoginAttribute : TypeFilterAttribute
    {
        public LoginAttribute() : base(typeof(PerfectWorldFilter))
        {
        }
    }
}
