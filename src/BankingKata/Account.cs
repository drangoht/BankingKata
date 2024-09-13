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
 
        private Operations? _operations ;
        public Account(IServiceDate date)
        {
            _operations = new Operations(date);
        }

        public void Deposit(int amount) =>
            _operations.Add(new Amount(amount), OperationType.Deposit);

        public Amount GetBalance()
        {
            return _operations.GetBalance();
        }

        public List<Operation> GetOperations() =>  _operations.List();


        public string PrintStatement()
        {
            string statement = "Date\tAmount\tBalance\n";
            foreach (var op in _operations.List())
            {
                statement += $"{op.OperationDate.ToString("dd.M.yyyy")}\t{op.OperationAmount.Value}\t{op.Balance.Value}\n";  
            }
            return statement;
        }

        public void WithDraw(int amount) =>
            _operations.Add(new Amount(amount), OperationType.WithDraw);
        
    }
}
