using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingKata
{
    public interface IAccount
    {
        public void Deposit(int amount);
        public void WithDraw(int amount);
        public string PrintStatement();
        public List<Operation> GetOperations();
        public Amount GetBalance();
    }
}
