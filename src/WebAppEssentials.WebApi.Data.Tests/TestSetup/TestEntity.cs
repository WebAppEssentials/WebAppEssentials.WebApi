using WebAppEssentials.WebApi.Data.Entities;

namespace WebAppEssentials.WebApi.Data.Tests.TestSetup;

public class TestEntity : BaseEntity<int>
{
    public string Name { get; set; }
}