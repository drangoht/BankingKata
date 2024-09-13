using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingKata
{
    public record Operation(int OperationNumber, DateTime OperationDate, Amount OperationAmount,Amount Balance);
}
