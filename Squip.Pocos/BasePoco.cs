using System;

namespace Squip.Pocos
{
    public abstract class BasePoco
    {
        public DateTime CreatedAt { get; private set; }

        public DateTime UpdatedAt { get; private set; }

        public DateTime DeletedAt { get; private set; }

        public bool IsSoftDeleted { get; set; }

        public void OnBeforeInsert()
        {
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void OnBeforeUpdate()
        {
            UpdatedAt = DateTime.UtcNow;
        }

        public void OnBeforeDelete()
        {
            DeletedAt = DateTime.UtcNow;
            IsSoftDeleted = true;
        }
    }
}
