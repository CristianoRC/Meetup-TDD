using System;
using Bogus;
using FluentAssertions;
using VirtualWallet.Domain.Entities;
using Xunit;

namespace VirtualWallet.Test;

public class TransactionTest
{
    [Fact(DisplayName = "Toda transação precisa ter um identificador único")]
    public void Identificador()
    {
        var faker = new Faker();
        var transaciton = new Transaction(faker.Finance.Amount(1), ETransactionType.Credit);

        transaciton.Id.Should().NotBe(Guid.Empty);
    }

    [Theory(DisplayName = "Não pode fazer transação de Zero dinheiros")]
    [InlineData(ETransactionType.Credit)]
    [InlineData(ETransactionType.Debit)]
    public void ZeroDinheiros(ETransactionType type)
    {
        var transaciton = new Transaction(0, type);
        transaciton.Notifications.Count.Should().Be(1);
    }

    [Fact(DisplayName = "O valor de credito precisa ser sempre positivo")]
    public void Credito()
    {
        var faker = new Faker();
        var transaciton = new Transaction(faker.Finance.Amount(max: -1), ETransactionType.Credit);
        transaciton.Notifications.Count.Should().Be(1);
    }

    [Fact(DisplayName = "O valor de debito precisa ser sempre negativo")]
    public void Debito()
    {
        var faker = new Faker();
        var transaciton = new Transaction(faker.Finance.Amount(min: 1), ETransactionType.Debit);
        transaciton.Notifications.Count.Should().Be(1);
    }

    [Fact(DisplayName = "Deve colocar a data atual quando criar uma nova transação")]
    public void DataDeCriacao()
    {
        var faker = new Faker();
        var transaciton = new Transaction(faker.Finance.Amount(max: -1), ETransactionType.Debit);
        transaciton.Notifications.Count.Should().Be(0);
        transaciton.Date.Should().BeBefore(DateTimeOffset.Now);
        transaciton.Date.Should().BeAfter(DateTimeOffset.Now.AddSeconds(-60));
    }
}