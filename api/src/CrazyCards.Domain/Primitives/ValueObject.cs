namespace CrazyCards.Domain.Primitives;

public abstract class ValueObject : IEquatable<ValueObject>
{
    public static bool operator ==(ValueObject a, ValueObject b)
    {
        if (a is null && b is null)
        {
            return true;
        }

        if (a is null || b is null)
        {
            return false;
        }

        return a.Equals(b);
    }

    public static bool operator !=(ValueObject a, ValueObject b) => !(a == b);
    
    public bool Equals(ValueObject other)
    {
        if (other is null)
        {
            return false;
        }

        return GetAtomicValues().SequenceEqual(other.GetAtomicValues());
    }

    public override bool Equals(object obj)
    {
        if (obj == null)
        {
            return false;
        }

        if (GetType() != obj.GetType())
        {
            return false;
        }

        if (!(obj is ValueObject valueObject))
        {
            return false;
        }

        return GetAtomicValues().SequenceEqual(valueObject.GetAtomicValues());
    }

    public override int GetHashCode() =>
        GetAtomicValues()
            .Aggregate(default(HashCode), (hashCode, obj) =>
            {
                hashCode.Add(obj.GetHashCode());

                return hashCode;
            }).ToHashCode();
    
    protected abstract IEnumerable<object> GetAtomicValues();
}