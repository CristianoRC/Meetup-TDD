using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using VirtualWallet.Domain.Entities;
using VirtualWallet.Domain.Repository;

namespace VirtualWallet.Test;

public class TransactionRepositoryMock : ITransactionRepository
{
    public Dictionary<Guid, (Transaction from, Transaction to)> Transactions { get; }

    public TransactionRepositoryMock()
    {
        Transactions = new Dictionary<Guid, (Transaction from, Transaction to)>();
    }

    public async Task CreateTransactions(Transaction from, Transaction to)
    {
        Transactions.Add(Guid.NewGuid(), (from, to));
    }
}