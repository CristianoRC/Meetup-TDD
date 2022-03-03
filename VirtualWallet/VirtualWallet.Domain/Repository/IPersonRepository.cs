using VirtualWallet.Domain.Entities;

namespace VirtualWallet.Domain.Repository;

public interface IPersonRepository
{
    Task Create(Person person);
}