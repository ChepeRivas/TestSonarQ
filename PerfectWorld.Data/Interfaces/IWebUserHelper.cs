using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfectWorld.Data.Interfaces
{
    public interface IWebUserHelper
    {
        int GetUserId();
        string GetUserCorreo();
        string GetUserLogin();
    }
}
