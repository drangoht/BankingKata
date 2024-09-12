namespace BankingKata.Tests
{
    public class AccountTests
    {
        [Fact]
        public void Should_Return_Correct_Balance_When_Deposit_Amount()
        {
            int operationAmount = 100;
            int attendedBalance = 100;
            Account account = new Account();
            account.Deposit(operationAmount);

            Amount balance = account.GetBalance();
            Assert.Equal(balance.Value, attendedBalance);
        }
        [Fact]
        public void Should_Return_Correct_Balance_When_WithDraw_Amount()
        {
            int operationAmount = 100;
            int attendedBalance = -100;
            Account account = new Account();
            account.WithDraw(operationAmount);

            Amount balance = account.GetBalance();
            Assert.Equal(balance.Value, attendedBalance);
        }
    }
}