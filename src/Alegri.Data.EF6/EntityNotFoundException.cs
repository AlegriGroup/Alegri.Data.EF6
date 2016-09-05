using System;

namespace Alegri.Data.EF6
{
    public class EntityNotFoundException<T> : Exception
    {
        public T Id { get; set; }

        public EntityNotFoundException(T id)
        {
            Id = id;
        }
    }
}