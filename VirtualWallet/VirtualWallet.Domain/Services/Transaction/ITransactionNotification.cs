using VirtualWallet.Domain.Entities;

namespace VirtualWallet.Domain.Services.Transaction;

public interface ITransactionNotification
{
    Task NewTransaction(Person from, Person to);
}