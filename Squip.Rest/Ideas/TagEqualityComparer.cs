using System.Collections.Generic;
using Squip.Rest.Ideas.Domain;

namespace Squip.Rest.Ideas;

public class TagEqualityComparer : IEqualityComparer<Tag>
{
    public bool Equals(Tag x, Tag y)
    {
        if (ReferenceEquals(x, y))
            return true;
        if (ReferenceEquals(x, null))
            return false;
        if (ReferenceEquals(y, null))
            return false;
        if (x.GetType() != y.GetType())
            return false;
        return x.Name.ToLower() == y.Name.ToLower();
    }

    public int GetHashCode(Tag obj)
    {
        return obj.Name.GetHashCode();
    }
}
