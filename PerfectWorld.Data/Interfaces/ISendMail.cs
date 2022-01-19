using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfectWorld.Data.Interfaces
{
    public interface ISendMail
    {
        public Task<int> Send(string To);
        public Task<int> Send(string To, string Addbody);
        public Task<int> Send(string To, string Addbody, string AddSubject);

    }
}
