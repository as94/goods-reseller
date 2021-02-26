using System;
using GoodsReseller.SeedWork.ValueObjects;

namespace GoodsReseller.SeedWork
{
    public abstract class Entity
    {
        public Guid Id { get; }
        
        
        public DateValueObject CreationDate { get; }
        public DateValueObject? LastUpdateDate { get; protected set; }
        public bool IsRemoved { get; protected set; }

        protected Entity(Guid id)
        {
            Id = id;
            
            CreationDate = new DateValueObject();
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
        
        
        public virtual void Remove()
        {
            if (IsRemoved)
            {
                return;
            }
            
            IsRemoved = true;
            LastUpdateDate = new DateValueObject();
        }
    }
}