using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SailClubLibrary.Services
{
    public abstract class Connection
    {
        protected string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog = SailClubDB; Integrated Security = True; Connect Timeout = 30; Encrypt=False;Trust Server Certificate=False;Application Intent = ReadWrite; Multi Subnet Failover=False";
    }
}
