using System;
using NodaTime;
using Squip.Rest.Common.Domain;

namespace Squip.Rest.Users.Domain;

public class User : IChangeable
{
    public string OidcSub { get; set; }
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
}
