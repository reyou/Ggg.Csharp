using Xunit;

namespace CompanyManagement.Test
{
    [CollectionDefinition("My Collection Name")]
    public class TimeCollection : ICollectionFixture<TimeFixture>
    {
    }
}
