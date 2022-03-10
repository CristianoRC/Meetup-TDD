using VirtualWallet.Domain.Entities;

namespace VirtualWallet.Domain.Services;

public interface IPersonService
{
    Task Create(Person person);
}