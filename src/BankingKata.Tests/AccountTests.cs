using Moq;
namespace BankingKata.Tests
{
    public class AccountTests
    {

        private readonly Mock<IServiceDate> objServiceDate;

        public AccountTests()
        {
            objServiceDate = new Mock<IServiceDate>();
        }
        [Fact]
        public void Should_Return_Correct_Balance_When_Deposit_Amount()
        {
            int operationAmount = 100;
            int attendedBalance = 100;
            objServiceDate.Setup(setup => setup.GetDate()).Returns(DateTime.UtcNow);
            Account account = new Account(objServiceDate.Object);
            account.Deposit(operationAmount);

            Amount balance = account.GetBalance();
            Assert.Equal(attendedBalance, balance.Value);
        }
        [Fact]
        public void Should_Return_Correct_Balance_When_WithDraw_Amount()
        {
            int operationAmount = 100;
            int attendedBalance = -100;
            objServiceDate.Setup(setup => setup.GetDate()).Returns(DateTime.UtcNow);
            Account account = new Account(objServiceDate.Object);
            account.WithDraw(operationAmount);

            Amount balance = account.GetBalance();
            Assert.Equal(attendedBalance, balance.Value);
        }
        [Fact]
        public void Should_Return_Correct_Balance_When_Deposit_Then_WithDraw_Amount()
        {
            int operationAmount = 100;
            int attendedBalance = 100;
            objServiceDate.Setup(setup => setup.GetDate()).Returns(DateTime.UtcNow);
            Account account = new Account(objServiceDate.Object);
            account.Deposit(operationAmount);
            Amount balance = account.GetBalance();
            Assert.Equal(attendedBalance, balance.Value);

            operationAmount = 20;
            attendedBalance = 80;
            account.WithDraw(operationAmount);

            balance = account.GetBalance();
            Assert.Equal(attendedBalance, balance.Value);
        }
        [Fact]
        public void Should_List_Operations_Ordered_When_Deposit_Then_WithDraw_Amount()
        {
            int operationDepositAmount = 100;
            int attendedDepositBalance = 100;
            int operationWithdrawAmount = 20;
            int attendedWithdrawBalance = 80;

            List<Operation> attendedOperations = new List<Operation>();
            attendedOperations.Add(new(1, DateTime.UtcNow, new Amount(operationDepositAmount), new Amount(attendedDepositBalance)));
            attendedOperations.Add(new(2, DateTime.UtcNow, new Amount(-operationWithdrawAmount), new Amount(attendedWithdrawBalance)));

            objServiceDate.Setup(setup => setup.GetDate()).Returns(DateTime.UtcNow);
            Account account = new Account(objServiceDate.Object);

            account.Deposit(operationDepositAmount);
            Amount balance = account.GetBalance();
            Assert.Equal(attendedDepositBalance, balance.Value);


            account.WithDraw(operationWithdrawAmount);
            balance = account.GetBalance();
            Assert.Equal(attendedWithdrawBalance, balance.Value);

            List<Operation> operations = account.GetOperations();
            Assert.Equal(attendedOperations.Count, operations.Count);

            bool operationAndAttendedOperationsHasSameValues = true;
            for (int op = 0; op < operations.Count; op++)
            {
                if (operations[op].OperationAmount != attendedOperations[op].OperationAmount)
                {
                    operationAndAttendedOperationsHasSameValues = false;
                    break;
                }
                if (operations[op].Balance != attendedOperations[op].Balance)
                {
                    operationAndAttendedOperationsHasSameValues = false;
                    break;
                }
            }
            Assert.True(operationAndAttendedOperationsHasSameValues);


        }

        [Fact]
        public void Should_PrintStatement_With_Operations_Ordered_When_Deposit_Then_WithDraw_Amount()
        {
            int operationDepositAmount = 100;
            int attendedDepositBalance = 100;
            DateTime depositDate = new DateTime(2024, 9, 12, 9, 0, 0);
            int operationWithdrawAmount = 20;
            int attendedWithdrawBalance = 80;
            DateTime withDrawDate = new DateTime(2024, 9, 12, 10, 0, 0);

            string attendedStatement = "Date\tAmount\tBalance\n";
            attendedStatement += $"{depositDate.ToString("dd.M.yyyy")}\t100\t100\n";
            attendedStatement += $"{withDrawDate.ToString("dd.M.yyyy")}\t-20\t80\n";

            objServiceDate.Setup(setup => setup.GetDate()).Returns(DateTime.UtcNow);
            Account account = new Account(objServiceDate.Object);

            objServiceDate.Setup(setup => setup.GetDate()).Returns(depositDate);
            account.Deposit(operationDepositAmount);
            Amount balance = account.GetBalance();
            Assert.Equal(attendedDepositBalance, balance.Value);

            objServiceDate.Setup(setup => setup.GetDate()).Returns(withDrawDate);
            account.WithDraw(operationWithdrawAmount);

            balance = account.GetBalance();
            Assert.Equal(attendedWithdrawBalance, balance.Value);

            var statement = account.PrintStatement();
            Assert.Equal(attendedStatement, statement);

        }

        [Fact]
        public void Should_Return_Zero_When_GetBalance_With_No_Operations()
        {
            int attendedBalance = 0;
            objServiceDate.Setup(setup => setup.GetDate()).Returns(DateTime.UtcNow);
            Account account = new Account(objServiceDate.Object);
            Amount balance = account.GetBalance();
            Assert.Equal(attendedBalance, balance.Value);
        }

        [Fact]
        public void Should_Throw_Exception_When_Deposit_With_Negative_Number()
        {
            int attendedBalance = 0;
            objServiceDate.Setup(setup => setup.GetDate()).Returns(DateTime.UtcNow);
            Account account = new Account(objServiceDate.Object);
            Assert.Throws<Exception>(() => account.Deposit(-1));
        }

        [Fact]
        public void Should_Throw_Exception_When_Withdraw_With_Negative_Number()
        {
            int attendedBalance = 0;
            objServiceDate.Setup(setup => setup.GetDate()).Returns(DateTime.UtcNow);
            Account account = new Account(objServiceDate.Object);
            Assert.Throws<Exception>(() => account.WithDraw(-1));
        }
    }
}