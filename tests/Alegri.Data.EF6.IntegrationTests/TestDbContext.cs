using System;
using System.Data.Entity;

namespace Alegri.Data.EF6.IntegrationTests
{
    public class TestDbContext : DbContext, IDisposable, IDatabaseContext
    {
        /// <summary>
        /// Connection String must be like
        /// </summary>
        public TestDbContext() : base(nameof(TestDbContext))
        {
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var sqliteConnectionInitializer = new SqliteTestDbInitializer(modelBuilder);
            System.Data.Entity.Database.SetInitializer(sqliteConnectionInitializer);
        }


        public DbSet<TestEntity> TestEntities { get; set; }


        // Dispose() calls Dispose(true)
        public new void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);

            base.Dispose();

        }
        // NOTE: Leave out the finalizer altogether if this class doesn't 
        // own unmanaged resources itself, but leave the other methods
        // exactly as they are. 
        ~TestDbContext()
        {
            // Finalizer calls Dispose(false)
            Dispose(false);
        }
        // The bulk of the clean-up code is implemented in Dispose(bool)
        protected new virtual void Dispose(bool disposing)
        {
            if(disposing)
            {

            }

            base.Dispose(disposing);
        }
    }
}