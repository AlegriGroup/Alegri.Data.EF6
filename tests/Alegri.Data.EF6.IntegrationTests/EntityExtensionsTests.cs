using System;
using FluentAssertions;
using Xunit;

namespace Alegri.Data.EF6.IntegrationTests
{
    public class EntityExtensionsTests
    {
        [Fact]
        public void SetDeletedTest()
        {
            var entity = TestEntity.Create("Emma Watson");


            var dt = DateTime.UtcNow;
            var parents = "Jacqueline Luesby and Chris Watson";
            var created = EntityExtensions.SetCreated(entity, parents, dt);

            created.Name.Should().Be("Emma Watson");

            created.CreatedBy.Should().Be(parents);
            created.LastUpdatedBy.Should().Be(parents);

            created.CreatedOn.Should().Be(dt);
            created.LastUpdatedOn.Should().Be(dt);
        }

        [Fact]
        public void SetUpdated()
        {
            var entity = TestEntity.Create("Harry Potter");

            var dt = DateTime.UtcNow;
            var parents = "Joanne K. Rowling";

            var updated = EntityExtensions.SetUpdated(entity, parents, dt);

            updated.Name.Should().Be("Harry Potter");

            updated.LastUpdatedBy.Should().Be(parents);
            updated.LastUpdatedOn.Should().Be(dt);
        }

        [Fact]
        public void SetDeleted()
        {
            var entity = TestEntity.Create("Entity Name");

            var dt = DateTime.UtcNow;
            var deletedBy = "Deleted By";
            var reason = "Reason By";

            var deleted = EntityExtensions.SetDeleted(entity, deletedBy, reason, dt);

            deleted.Name.Should().Be("Entity Name");

            deleted.DeletedBy.Should().Be(deletedBy);
            deleted.DeletedOn.Should().Be(dt);
            deleted.DeletedReason.Should().Be(reason);
        }
    }
}
