using FluentAssertions;
using WebAppEssentials.Enums;
using WebAppEssentials.Extensions;
using WebAppEssentials.WebApi.Tests.TestSetup;

namespace WebAppEssentials.WebApi.Tests.Extensions;

public class QueryableExtensionsTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Order_ByPropertyNameAscending_ReturnsOrderedQueryable()
    {
        var data = new List<TestEntity>
        {
            new TestEntity { Id = 2, Name = "B" },
            new TestEntity { Id = 1, Name = "A" }
        }.AsQueryable();

        var result = data.Order("Name", SortDirection.Ascending);

        result.First().Name.Should().Be("A");
        result.Last().Name.Should().Be("B");
    }

    
}