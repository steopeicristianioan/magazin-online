using DataAccessLibrary.NETFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexStore.repository
{
    public abstract class Repository
    {
        protected string connection = staticMethods.StaticMethods.getConnectionString();
        protected MySqlDataAccess db = new MySqlDataAccess();
    }
}
