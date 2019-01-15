using System;

namespace Squip.Pocos
{
    public abstract class BasePoco
    {
        public DateTime CreatedAt { get; private set; }

        public DateTime UpdatedAt { get; private set; }

        public void OnBeforeInsert()
        {
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void OnBeforeUpdate()
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
