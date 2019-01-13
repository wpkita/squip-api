using System;
using System.ComponentModel.DataAnnotations;

namespace Squip.Api.Models
{
    public abstract class BaseEntity
    {
        [Required]
        public DateTime CreatedAt { get; private set; }

        [Required]
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
