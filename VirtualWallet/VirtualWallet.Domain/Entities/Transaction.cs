using Flunt.Notifications;

namespace VirtualWallet.Domain.Entities;

public class Transaction : Notifiable<Notification>, IEntity
{
    public Transaction(decimal amount, ETransactionType transactionType)
    {
        Amount = amount;
        TransactionType = transactionType;
        Id = Guid.NewGuid();
        Date = DateTimeOffset.Now;
        IsValid();
    }

    public Transaction(Guid id, decimal amount, ETransactionType transactionType, DateTimeOffset date)
    {
        Id = id;
        Amount = amount;
        TransactionType = transactionType;
        Date = date;
        IsValid();
    }

    public Guid Id { get; }
    public decimal Amount { get; }
    public ETransactionType TransactionType { get; }
    public DateTimeOffset Date { get; }

    public bool IsValid()
    {
        if (Amount == 0)
            AddNotification(nameof(TransactionType), "Transaction Amount Invalid");

        if (TransactionType is ETransactionType.Credit && Amount < 0)
            AddNotification(nameof(TransactionType), "Transaction Invalid");

        if (TransactionType is ETransactionType.Debit && Amount > 0)
            AddNotification(nameof(TransactionType), "Transaction Invalid");

        return Notifications.Count == 0;
    }
}