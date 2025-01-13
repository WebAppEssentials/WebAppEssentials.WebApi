using WebAppEssentials.Entities;

namespace WebAppEssentials.WebApi.Tests.TestSetup;

public class TestEntity : BaseEntity<int>
{
    public string Name { get; set; }
}