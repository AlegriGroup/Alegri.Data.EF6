using System;
using FluentAssertions;
using Xunit;

namespace Alegri.Data.EF6.IntegrationTests
{
    public class RepositoryTests
    {
        private TestDbContext _dbContext;
        private TestRepository _testRepository;
        public RepositoryTests()
        {
            _dbContext = new TestDbContext();
            _testRepository = new TestRepository(_dbContext);
        }

        [Fact]
        public void AddAndGetTest()
        {
            var randomName = Guid.NewGuid().ToString();
            var entity = TestEntity.Create(randomName);

            var addedEntity = _testRepository.Add(entity, "UNIT TEST");
            _testRepository.Save();

            addedEntity.Name.Should().Be(randomName);
            addedEntity.CreatedBy.Should().Be("UNIT TEST");
            addedEntity.CreatedOn.Should().NotBe(default(DateTime));

            var getByNameEntity = _testRepository.GetByName(randomName);
            getByNameEntity.Should().Be(addedEntity, "Get by Name failed");

            var getById = _testRepository.Get(entity.Id);
            getById.Should().Be(addedEntity, "Get by Id failed");
        }

        [Fact]
        public void AddAndDelete()
        {
            var randomName = Guid.NewGuid().ToString();
            var entity = TestEntity.Create(randomName);

            var addedEntity = _testRepository.Add(entity, "UNIT TEST");
            _testRepository.Save();

            var getById = _testRepository.Get(entity.Id);
            getById.Should().Be(addedEntity, "Get by Id failed");

            var deletedEntity = _testRepository.Delete(entity, "UNIT TEST", "TEST DELETE");
            _testRepository.Save();

            deletedEntity.Name.Should().Be(randomName);
            deletedEntity.DeletedBy.Should().Be("UNIT TEST");
            deletedEntity.DeletedReason.Should().Be("TEST DELETE");
            deletedEntity.DeletedOn.Should().NotBe(default(DateTime));

            deletedEntity.LastUpdatedBy.Should().Be("UNIT TEST");
            deletedEntity.LastUpdatedOn.Should().NotBe(default(DateTime));

            var getByIdRemoved = _testRepository.Get(entity.Id);
            getByIdRemoved.Should().Be(null);
        }
    }
}
