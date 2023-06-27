using System;
using NodaTime;
using Squip.Rest.Users.Domain;

namespace Squip.Rest.Common.Domain;

public abstract class DomainModelBase : IChangeable, IUserOwnable
{
    public Guid Id { get; set; }
    public Instant InstantCreatedAt { get; set; }
    public Instant InstantUpdatedAt { get; set; }

    public void PreCreate()
    {
        InstantCreatedAt = SystemClock.Instance.GetCurrentInstant();
    }

    public void PreUpdate()
    {
        InstantUpdatedAt = SystemClock.Instance.GetCurrentInstant();
    }

    public Guid UserId { get; set; }
    public User User { get; set; }
}
