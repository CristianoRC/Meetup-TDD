using VirtualWallet.Domain.Entities;

namespace VirtualWallet.Domain.Repository;

public interface ITransactionRepository
{
    Task CreateTransactions(Transaction from, Transaction to);
}