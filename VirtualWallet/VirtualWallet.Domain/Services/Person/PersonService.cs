using VirtualWallet.Domain.Entities;
using VirtualWallet.Domain.Repository;

namespace VirtualWallet.Domain.Services;

public class PersonService : IPersonService
{
    private readonly IPersonRepository _repository;

    public PersonService(IPersonRepository repository)
    {
        _repository = repository;
    }

    public async Task Create(Person person)
    {
        if (person.IsValid())
            await _repository.Create(person);
    }
}