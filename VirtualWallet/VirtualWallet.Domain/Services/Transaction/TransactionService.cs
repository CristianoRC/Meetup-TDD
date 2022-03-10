using VirtualWallet.Domain.Entities;
using VirtualWallet.Domain.Repository;

namespace VirtualWallet.Domain.Services.Transaction;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _repository;
    private readonly ITransactionNotification _notificationService;

    public TransactionService(ITransactionRepository repository, ITransactionNotification notificationService)
    {
        _repository = repository;
        _notificationService = notificationService;
    }

    public async Task CreateTransaction(Wallet from, Wallet to, decimal amount)
    {
        var isTheSameWallet = from.CompareTo(to) == 1;
        if (isTheSameWallet)
            throw new ArgumentException();

        if (amount == Decimal.Zero)
            throw new ArgumentException();

        var debitTransaction = new Entities.Transaction(amount, ETransactionType.Debit);
        var creditTransaction = new Entities.Transaction(amount * -1, ETransactionType.Credit);

        if (debitTransaction.IsValid() && creditTransaction.IsValid())
        {
            await _repository.CreateTransactions(debitTransaction, creditTransaction);
            await _notificationService.NewTransaction(from.Person, to.Person);
        }
        else
            throw new ArgumentException("Invalid Amount");
    }
}