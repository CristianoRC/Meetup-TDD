using System;
using System.Threading.Tasks;
using Bogus;
using Moq;
using VirtualWallet.Domain.Repository;
using VirtualWallet.Domain.Services;
using Xunit;
using Person = VirtualWallet.Domain.Entities.Person;

namespace VirtualWallet.Test;

public class PersonServiceTest
{
    [Fact(DisplayName = "Não deve cadastara pessoa no banco de dados se tiver algum campo inválido")]
    public async Task Invalid()
    {
        var faker = new Faker();
        var personRepository = new Mock<IPersonRepository>();
        var service = new PersonService(personRepository.Object);

        var invalidPerson = new Person(Guid.NewGuid(), string.Empty, faker.Person.Email, faker.Person.Phone);
        await service.Create(invalidPerson);
        
        personRepository.Verify(repository => repository.Create(It.IsAny<Person>()), Times.Never);
    }
    
    [Fact(DisplayName = "Deve cadastara pessoa no banco de dados se a entidade for válida")]
    public async Task Valid()
    {
        var faker = new Faker();
        var personRepository = new Mock<IPersonRepository>();
        var service = new PersonService(personRepository.Object);

        var invalidPerson = new Person(Guid.NewGuid(), faker.Person.FullName, faker.Person.Email, faker.Person.Phone);
        await service.Create(invalidPerson);
        
        personRepository.Verify(repository => repository.Create(It.IsAny<Person>()), Times.Once);
    }
}