using System;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using Xunit;
using Person = VirtualWallet.Domain.Entities.Person;

namespace VirtualWallet.Test;

public class PersonTest
{
    [Fact(DisplayName =
        "Quando criar uma pessoa o com o nome vazio, a lista de erro deve conter um 1 relacionado a isso")]
    public async Task Name()
    {
        var faker = new Faker();
        var person = new Person(Guid.NewGuid(), string.Empty, faker.Person.Email, faker.Person.Phone);

        person.Notifications.Count.Should().Be(1);
        person.Notifications.First().Key.Should().Be("Invalid Name");
    }

    [Fact(DisplayName = "Quando criar uma pessoa o email tem que ser válido")]
    public async Task Email()
    {
        var faker = new Faker();
        var person = new Person(Guid.NewGuid(), faker.Person.FullName, string.Empty, faker.Person.Phone);

        person.Notifications.Count.Should().Be(1);
        person.Notifications.First().Key.Should().Be("Invalid Email");
    }


    [Fact(DisplayName = "Quando criar uma pessoa o telefone não pode estar vazio")]
    public async Task Number()
    {
        var faker = new Faker();
        var person = new Person(Guid.NewGuid(), faker.Person.FullName, faker.Person.Email, string.Empty);

        person.Notifications.Count.Should().Be(1);
        person.Notifications.First().Key.Should().Be("Invalid Phone");
    }

    [Fact(DisplayName = "Quando criar uma pessoa O Id tem que ser diferente do padrão")]
    public async Task Id()
    {
        var faker = new Faker();
        var person = new Person(Guid.Empty, faker.Person.FullName, faker.Person.Email, faker.Person.Phone);

        person.Notifications.Count.Should().Be(1);
        person.Notifications.First().Key.Should().Be("Invalid Id");
    }
}