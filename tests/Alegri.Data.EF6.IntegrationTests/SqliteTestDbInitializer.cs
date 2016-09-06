using System.Data.Entity;
using SQLite.CodeFirst;

namespace Alegri.Data.EF6.IntegrationTests
{
    public class SqliteTestDbInitializer : SqliteDropCreateDatabaseAlways<TestDbContext>
    {
        public SqliteTestDbInitializer(DbModelBuilder modelBuilder)
            : base(modelBuilder) { }


    }
}