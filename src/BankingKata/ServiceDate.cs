using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingKata
{
    public class ServiceDate : IServiceDate
    {
        public DateTime GetDate()
        {
            return DateTime.UtcNow;
        }
    }
}
