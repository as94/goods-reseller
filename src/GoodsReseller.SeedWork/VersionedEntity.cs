using System;

namespace GoodsReseller.SeedWork
{
    public abstract class VersionedEntity : Entity
    {
        public int Version { get; private set; }

        protected VersionedEntity(Guid id, int version) : base(id)
        {
            if (version <= 0)
            {
                throw new ArgumentException("Version should be more than 0");
            }
            
            Version = version;
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

        protected void IncrementVersion()
        {
            Version++;
        }
        
        public override void Remove()
        {
            base.Remove();
            IncrementVersion();
        }
    }
}