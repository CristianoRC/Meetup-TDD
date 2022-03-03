using Flunt.Notifications;

namespace VirtualWallet.Domain.Entities;

public class Person : Notifiable<Notification>, IEntity
{
    public Person(Guid id, string name, string email, string phone)
    {
        Id = id;
        Name = name;
        Email = email;
        Phone = phone;
        Validate();
    }

    public Guid Id { get; }
    public string Name { get; }
    public string Email { get; }
    public string Phone { get; }

    public void Validate()
    {
        if (string.IsNullOrEmpty(Name))
            AddNotification("Invalid Name", "Invalid Name");

        if (string.IsNullOrEmpty(Email))
            AddNotification("Invalid Email", "Invalid Email");

        if (string.IsNullOrEmpty(Phone))
            AddNotification("Invalid Phone", "Invalid Phone");

        if (Id == Guid.Empty)
            AddNotification("Invalid Id", "Invalid Id");
    }

    public bool IsValid()
    {
        return Notifications.Count == 0;
    }
}