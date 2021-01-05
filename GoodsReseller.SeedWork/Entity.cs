using System;

namespace GoodsReseller.SeedWork
{
    public abstract class Entity
    {
        public Guid Id { get; }
        public int Version { get; private set; }

        protected Entity(Guid id, int version)
        {
            if (version <= 0)
            {
                throw new ArgumentException("Version should be more than 0");
            }
            
            Id = id;
            Version = version;
        }

        public bool IsTransient()
        {
            return Id == default;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Entity))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (GetType() != obj.GetType())
            {
                return false;
            }

            var item = (Entity) obj;
            if (item.IsTransient() || IsTransient())
            {
                return false;
            }

            return item.Id == Id;
        }

        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                return Id.GetHashCode();
            }

            return base.GetHashCode();
        }

        public static bool operator ==(Entity left, Entity right)
        {
            if (Equals(left, null))
            {
                return Equals(right, null);
            }

            return left.Equals(right);
        }

        public static bool operator !=(Entity left, Entity right)
        {
            return !(left == right);
        }

        protected void IncrementVersion()
        {
            Version++;
        }
    }
}