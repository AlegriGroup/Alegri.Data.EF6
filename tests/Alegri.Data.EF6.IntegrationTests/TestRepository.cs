namespace Alegri.Data.EF6.IntegrationTests
{
    public class TestRepository : TrackedRepository<TestEntity>
    {
        public TestRepository(IDatabaseContext dbContext) : base(dbContext)
        {
        }

        public TestEntity GetByName(string name)
        {
            return Get(user => user.Name.Equals(name, ComparisonCulture));
        }
    }
}