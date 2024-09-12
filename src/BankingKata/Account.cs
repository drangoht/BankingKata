using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingKata
{
    public class Account : IAccount
    {
        private Operations _operations = new Operations();

        public Account()
        {
            Operations _operations = new Operations();
        }

        public void Deposit(int amount) =>
            _operations.Add(new Amount(amount), OperationType.Deposit);

        public Amount GetBalance()
        {
            return _operations.GetBalance();
        }


        public void PrintStatement()
        {
            throw new NotImplementedException();
        }

        public void WithDraw(int amount) =>
            _operations.Add(new Amount(amount), OperationType.WithDraw);
        
    }
}
