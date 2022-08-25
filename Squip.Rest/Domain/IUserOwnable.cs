using System;

namespace Squip.Rest.Domain;

public interface IUserOwnable
{
    public Guid UserId { get; set; }
    public User User { get; set; }
}
