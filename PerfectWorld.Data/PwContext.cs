using LibConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfectWorld.Data
{
    //Develop
    //public class PwContext: DbSource
    //{
    //    static string server = "144.126.137.160";
    //    static string user = "testgpw";
    //    static string pwd = "Wf54y&5m";
    //    static string database = "testgpw";

    //    static string CadenaConexion = string.Format("server={0};user={1};pwd={2};database={3};SslMode=none", server, user, pwd, database);
    //    public PwContext() : base(CadenaConexion) { }
    //}
    //Produccion
    public class PwContext : DbSource
    {
        static string server = "127.0.0.1";
        static string user = "olympdba";
        static string pwd = "ek8cD7?9";
        static string database = "godsdbo";

        static string CadenaConexion = string.Format("server={0};user={1};pwd={2};database={3};SslMode=none", server, user, pwd, database);
        public PwContext() : base(CadenaConexion) { }
    }
}
