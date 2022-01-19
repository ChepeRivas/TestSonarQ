using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfectWorld.Data.Helpers
{
    public class ResultClass
    {
        public ResultClass()
        {
            this.data = null;
            this.message = "Proeceso excitoso";
            this.state = true;
        }
        public bool state { get; set; }
        public string message { get; set; }
        public Object data { get; set; }

    }
}
