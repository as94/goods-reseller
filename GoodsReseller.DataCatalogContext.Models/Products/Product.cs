using System;
using GoodsReseller.SeedWork;
using GoodsReseller.SeedWork.ValueObjects;

namespace GoodsReseller.DataCatalogContext.Models.Products
{
    public sealed class Product : VersionedEntity, IAggregateRoot
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Money UnitPrice { get; private set; }
        public Discount DiscountPerUnit { get; private set; }
        
        public DateValueObject CreationDate { get; }
        public DateValueObject? LastUpdateDate { get; private set; }
        public bool IsRemoved { get; private set; }
        
        public Product(
            Guid id,
            int version,
            string name,
            string description,
            Money unitPrice,
            Discount discountPerUnit,
            DateValueObject creationDate)
            : base(id, version)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            
            if (description == null)
            {
                throw new ArgumentNullException(nameof(description));
            }
            
            Name = name;
            Description = description;
            UnitPrice = unitPrice;
            DiscountPerUnit = discountPerUnit;
            CreationDate = creationDate;
            IsRemoved = false;
        }

        public static Product Restore(
            Guid id,
            int version,
            string name,
            string description,
            Money unitPrice,
            Discount discountPerUnit,
            DateValueObject creationDate,
            DateValueObject? lastUpdateDate,
            bool isRemoved)
        {
            return new Product(
                id,
                version,
                name,
                description,
                unitPrice,
                discountPerUnit,
                creationDate)
            {
                LastUpdateDate = lastUpdateDate,
                IsRemoved = isRemoved
            };
        }

        public void Update(string name, string description, Money unitPrice, Discount discountPerUnit, DateValueObject lastUpdateDate)
        {
            if (IsRemoved)
            {
                throw new InvalidOperationException($"Product with id = {Id} has already been removed");
            }
            
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            
            if (description == null)
            {
                throw new ArgumentNullException(nameof(description));
            }

            if (lastUpdateDate == null)
            {
                throw new ArgumentNullException(nameof(lastUpdateDate));
            }

            Name = name;
            Description = description;
            UnitPrice = unitPrice;
            DiscountPerUnit = discountPerUnit;
            
            IncrementVersion();
            LastUpdateDate = lastUpdateDate;
        }

        public void Remove(DateValueObject lastUpdateDate)
        {
            if (IsRemoved)
            {
                return;
            }
            
            if (lastUpdateDate == null)
            {
                throw new ArgumentNullException(nameof(lastUpdateDate));
            }
            
            IsRemoved = true;
            
            IncrementVersion();
            LastUpdateDate = lastUpdateDate;
        }
    }
}