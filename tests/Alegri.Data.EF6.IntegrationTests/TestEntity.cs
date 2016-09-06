using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.SqlServer.Server;

namespace Alegri.Data.EF6.IntegrationTests
{
    public class TestEntity : ValidatableEntity
    {
        [Required]
        public String Name { get; set; }

        public static TestEntity Create(string name)
        {
            return new TestEntity
            {
                Id = Guid.NewGuid(),
                Name = name
            };
        }
    }
}