using Flunt.Notifications;

namespace VirtualWallet.Domain.Entities;

public class Wallet : Notifiable<Notification>, IEntity, IComparable
{
    public Wallet(Guid id, Person person)
    {
        Id = id;
        Person = person;
    }

    public Guid Id { get; }
    public Person Person { get; }

    public bool IsValid()
    {
        return Person.IsValid();
    }

    public int CompareTo(object? obj)
    {
        if (obj is null)
            return 0;

        if (obj.GetType() != typeof(Wallet))
            return 0;

        if (Id == ((Wallet) obj).Id)
            return 1;

        return 0;
    }
}