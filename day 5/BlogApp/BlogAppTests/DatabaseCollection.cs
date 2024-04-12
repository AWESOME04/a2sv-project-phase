using BlogApp.Tests; // This should point to the namespace where DatabaseFixture is defined
using Xunit;

[CollectionDefinition("DatabaseCollection")]
public class DatabaseCollection : ICollectionFixture<DatabaseFixture>
{
    // This class has no code, and is never created. Its purpose is
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.
}
