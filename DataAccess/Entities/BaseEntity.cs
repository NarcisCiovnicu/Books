﻿namespace DataAccess.Entities
{
    public class BaseEntity<T>
    {
        public T Id { get; set; } = default!;
    }
}
