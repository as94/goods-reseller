using System;

namespace GoodsReseller.SeedWork
{
    public abstract class VersionedEntity
    {
        public Guid Id { get; }
        public int Version { get; private set; }

        protected VersionedEntity(Guid id, int version)
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
            if (!(obj is VersionedEntity))
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

            var item = (VersionedEntity) obj;
            if (item.IsTransient() || IsTransient())
            {
                return false;
            }

            return item.Id == Id && item.Version == Version;
        }

        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                return Id.GetHashCode() ^ Version.GetHashCode();
            }

            return base.GetHashCode();
        }

        public static bool operator ==(VersionedEntity left, VersionedEntity right)
        {
            if (Equals(left, null))
            {
                return Equals(right, null);
            }

            return left.Equals(right);
        }

        public static bool operator !=(VersionedEntity left, VersionedEntity right)
        {
            return !(left == right);
        }

        protected void IncrementVersion()
        {
            Version++;
        }
    }
}