using System.Collections.Immutable;

namespace BankingKata
{
    public class Operations
    {
        private List<Operation> _operations = new();

        private IServiceDate _date;
        public Operations(IServiceDate date)
        {
            _date = date;
        }
        public Operation Add(Amount amount, OperationType operationType)
        {
            if (amount.Value <= 0)
            {
                throw new Exception("Not a valid amount to create this operation");
            }
            //var operationDate = DateTime.UtcNow;
            Operation newOperation = GenerateOperation(amount, operationType, _date.GetDate());
            _operations.Add(newOperation);
            return newOperation;
        }
        public List<Operation> List() => _operations;

        private Operation GenerateOperation(Amount amount, OperationType operationType, DateTime operationDate)
        {
            var signedAmount = OperationTypeToAmount(amount, operationType);
            Operation? lastOperation = GetLastOperation();
            if (lastOperation == null)
            {
                return new Operation(1,operationDate, signedAmount, new Amount(signedAmount.Value));
            }
            return new Operation(lastOperation.OperationNumber+1,operationDate, signedAmount, new Amount(lastOperation.Balance.Value + signedAmount.Value));
        }

        private Amount OperationTypeToAmount(Amount amount, OperationType operationType)
        {
            return operationType switch
            {
                OperationType.Deposit => new Amount(amount.Value),
                OperationType.WithDraw => new Amount(-1 * amount.Value),
                _ => new Amount(amount.Value),
            };
        }

        public Amount GetBalance()
        {
            Operation? lastOperation = GetLastOperation();
            if (lastOperation == null)
            {
                return new Amount(0);
            }
            return lastOperation.Balance;
        }
        
        private Operation? GetLastOperation()
        {
            return _operations.OrderByDescending(o => o.OperationNumber).FirstOrDefault();

        }
    }
}