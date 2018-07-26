using System;
using System.ComponentModel.DataAnnotations;

namespace SquipApi.Pocos
{
    public abstract class BaseEntity
    {
        [Required]
        public DateTime CreatedDateTime { get; set; }
        [Required]
        public long CreatedByUserId { get; set; }
        [Required]
        public DateTime ModifiedDateTime { get; set; }
        [Required]
        public long ModifiedByUserId { get; set; }

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
