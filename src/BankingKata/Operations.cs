using System.Collections.Immutable;

namespace BankingKata
{
    public class Operations
    {
        private List<Operation> _operations = new();

        public Operation Add(Amount amount, OperationType operationType)
        {
            if (amount.Value <= 0)
            {
                throw new Exception("Not a valid amount to create this operation");
            }
            Operation newOperation = GenerateOperation(amount, operationType);
            _operations.Add(newOperation);
            return newOperation;
        }

        private Operation GenerateOperation(Amount amount, OperationType operationType)
        {
            var signedAmount = OperationTypeToAmount(amount, operationType);
            Operation? lastOperation = GetLastOperation();
            if (lastOperation == null)
            {
                return new Operation(DateTime.UtcNow, signedAmount, new Amount(signedAmount.Value));
            }
            return new Operation(DateTime.UtcNow, signedAmount, new Amount(lastOperation.Balance.Value + signedAmount.Value));
        }

        private Amount OperationTypeToAmount(Amount amount, OperationType operationType)
        {
            switch (operationType)
            {
                case OperationType.Deposit:
                    return new Amount(amount.Value);
                case OperationType.WithDraw:
                    return new Amount(-1 * amount.Value);
            }
            return new Amount(amount.Value);
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
            return _operations.OrderByDescending(o => o.OperationDate).FirstOrDefault();

        }
    }
}