namespace VirtualWallet.Domain.Entities;

public interface IEntity
{
    void Validate();

    bool IsValid();
}