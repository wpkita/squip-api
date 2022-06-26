using System;
using NodaTime;

namespace Squip.Rest.Domain
{
    public abstract class DomainModelBase : IChangeable, IUserOwnable
    {
        public Guid Id { get; set; }
        public Instant InstantCreatedAt { get; set; }
        public Instant InstantUpdatedAt { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }

        public void PreCreate()
        {
            InstantCreatedAt = SystemClock.Instance.GetCurrentInstant();
        }

        public void PreUpdate()
        {
            InstantUpdatedAt = SystemClock.Instance.GetCurrentInstant();
        }
    }
}
