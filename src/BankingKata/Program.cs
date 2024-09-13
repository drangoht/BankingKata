// See https://aka.ms/new-console-template for more information
using BankingKata;

Console.WriteLine("Hello, World!");
Account account = new Account(new ServiceDate());
account.Deposit(25);
account.Deposit(25);
account.WithDraw(10);
account.Deposit(100);
account.WithDraw(80);
Console.WriteLine(account.PrintStatement());