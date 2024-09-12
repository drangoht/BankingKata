using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingKata
{
    public record Operation(DateTime OperationDate, Amount OperationAmount,Amount Balance);
}
