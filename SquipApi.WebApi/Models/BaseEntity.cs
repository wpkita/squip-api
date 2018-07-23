using System;
using System.ComponentModel.DataAnnotations;

namespace SquipApi.WebApi.Models
{
    public abstract class BaseEntity
    {
        [Required]
        public DateTime CreatedDateTime { get; set; }
        [Required]
        public DateTime ModifiedDateTime { get; set; }
        [Required]
        public string TenantId { get; set; }

        public void OnBeforeInsert()
        {
            CreatedDateTime = DateTime.UtcNow;
            ModifiedDateTime = DateTime.UtcNow;
        }

        public void OnBeforeUpdate()
        {
            ModifiedDateTime = DateTime.UtcNow;
        }
    }
}
