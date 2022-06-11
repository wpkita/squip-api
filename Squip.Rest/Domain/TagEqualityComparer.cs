using System;
using System.Collections.Generic;

namespace Squip.Rest.Domain;

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
        return x.Name == y.Name;
    }

    public int GetHashCode(Tag obj)
    {
        return obj.Name.GetHashCode();
    }
}
