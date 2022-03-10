using System;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using Moq;
using VirtualWallet.Domain.Entities;
using VirtualWallet.Domain.Repository;
using VirtualWallet.Domain.Services.Transaction;
using Xunit;
using Person = VirtualWallet.Domain.Entities.Person;

namespace VirtualWallet.Test;

public class TransactionServiceTest
{
    [Fact(DisplayName = "A transação sempre precisa acontecer entre contas diferentes")]
    public async Task SameAccounts()
    {
        var faker = new Faker();
        var person = new Person(Guid.NewGuid(), faker.Person.FullName, faker.Person.Email, faker.Person.Phone);
        var wallet = new Wallet(Guid.NewGuid(), person);

        var repositoryMock = Mock.Of<ITransactionRepository>();
        var service = new TransactionService(repositoryMock, null);
        var act = () => service.CreateTransaction(wallet, wallet, faker.Finance.Amount(-1));
        await act.Should().ThrowAsync<ArgumentException>();
    }

    [Fact(DisplayName = "A transação sempre precisa ter um amount maior que zero")]
    public async Task ZeroAmount()
    {
        var faker = new Faker();
        var firstPerson = new Person(Guid.NewGuid(), faker.Person.FullName, faker.Person.Email, faker.Person.Phone);
        var secondPerson = new Person(Guid.NewGuid(), faker.Person.FullName, faker.Person.Email, faker.Person.Phone);
        var firstWallet = new Wallet(Guid.NewGuid(), firstPerson);
        var secondWallet = new Wallet(Guid.NewGuid(), secondPerson);

        var repositoryMock = Mock.Of<ITransactionRepository>();
        var service = new TransactionService(repositoryMock, null);
        var act = () => service.CreateTransaction(firstWallet, secondWallet, Decimal.Zero);
        await act.Should().ThrowAsync<ArgumentException>();
    }

    [Fact(DisplayName = "Para toda transação criada precisamos ter outra no sentido contrário")]
    public async Task BookEntry()
    {
        var faker = new Faker();
        var firstPerson = new Person(Guid.NewGuid(), faker.Person.FullName, faker.Person.Email, faker.Person.Phone);
        var secondPerson = new Person(Guid.NewGuid(), faker.Person.FullName, faker.Person.Email, faker.Person.Phone);
        var firstWallet = new Wallet(Guid.NewGuid(), firstPerson);
        var secondWallet = new Wallet(Guid.NewGuid(), secondPerson);

        var repositoryMock = new TransactionRepositoryMock();
        var notificationMock = Mock.Of<ITransactionNotification>();
        var service = new TransactionService(repositoryMock, notificationMock);
        var amount = faker.Finance.Amount(max: -1);
        await service.CreateTransaction(firstWallet, secondWallet, amount);

        repositoryMock.Transactions.Count.Should().Be(1);
        repositoryMock.Transactions[repositoryMock.Transactions.Keys.First()].from.Amount.Should().Be(amount);
        repositoryMock.Transactions[repositoryMock.Transactions.Keys.First()].to.Amount.Should().Be(amount * -1);
    }
}