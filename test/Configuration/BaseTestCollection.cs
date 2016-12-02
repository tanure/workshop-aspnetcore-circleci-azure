using Xunit;

namespace test.Configuration
{
    [CollectionDefinition("Base Collection")]
    public class BaseTestCollection : ICollectionFixture<BaseTestFixture>
    {
        
    }
}