using FluentAssertions;
using Xunit;

namespace VirtualWallet.Test;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        1.Should().BePositive();
    }
}