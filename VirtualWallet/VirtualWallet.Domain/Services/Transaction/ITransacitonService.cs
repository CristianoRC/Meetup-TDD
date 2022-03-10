using VirtualWallet.Domain.Entities;

namespace VirtualWallet.Domain.Services.Transaction;

public interface ITransactionService
{
    Task CreateTransaction(Wallet from, Wallet to, decimal amount);
}