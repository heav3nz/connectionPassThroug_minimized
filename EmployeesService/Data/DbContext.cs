using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesService.Data
{
    public class DbContext: Connection
    {
        public DbContext(int commandTimeOut=0): base("Data source=tcp:demetechserver.database.windows.net,1433; Initial Catalog=life; User Id=cliffmedisut@demetechserver; password=Pa$$w0rd2010sqlupdate", commandTimeOut)
        {

        }
    }
}
