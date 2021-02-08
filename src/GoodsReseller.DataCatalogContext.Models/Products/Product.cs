using System;
using GoodsReseller.SeedWork;
using GoodsReseller.SeedWork.ValueObjects;

namespace GoodsReseller.DataCatalogContext.Models.Products
{
    public class Product : VersionedEntity, IAggregateRoot
    {
        public string Label { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Money UnitPrice { get; private set; }
        public Discount DiscountPerUnit { get; private set; }
        public Guid[] ProductIds { get; private set; }
        
        
        // TODO: extract to Metadata
        public DateValueObject CreationDate { get; private set; }
        public DateValueObject? LastUpdateDate { get; private set; }
        public bool IsRemoved { get; private set; }
        
        public Product(
            Guid id,
            int version,
            string label,
            string name,
            string description,
            Money unitPrice,
            Discount discountPerUnit,
            Guid[] productIds = null)
            : this(id, version, label, name, description, productIds)
        {
            UnitPrice = unitPrice;
            DiscountPerUnit = discountPerUnit;
        }

        private Product(
            Guid id,
            int version,
            string label,
            string name,
            string description,
            Guid[] productIds = null)
            : base(id, version)
        {
            if (label == null)
            {
                throw new ArgumentNullException(nameof(label));
            }
            
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            
            if (description == null)
            {
                throw new ArgumentNullException(nameof(description));
            }

            Label = label;
            Name = name;
            Description = description;
            CreationDate = new DateValueObject(DateTime.Now);
            IsRemoved = false;
            ProductIds = productIds ?? Array.Empty<Guid>();
        } 

        public static Product Restore(
            Guid id,
            int version,
            string label,
            string name,
            string description,
            Money unitPrice,
            Discount discountPerUnit,
            DateValueObject creationDate,
            DateValueObject? lastUpdateDate,
            bool isRemoved,
            Guid[]? productIds = null)
        {
            return new Product(
                id,
                version,
                label,
                name,
                description,
                unitPrice,
                discountPerUnit,
                productIds)
            {
                CreationDate = creationDate,
                LastUpdateDate = lastUpdateDate,
                IsRemoved = isRemoved
            };
        }

        public void Update(
            string label,
            string name,
            string description,
            Money unitPrice,
            Discount discountPerUnit,
            DateValueObject lastUpdateDate,
            Guid[] productIds = null)
        {
            if (IsRemoved)
            {
                throw new InvalidOperationException($"Product with id = {Id} has already been removed");
            }
            
            if (label == null)
            {
                throw new ArgumentNullException(nameof(label));
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

            Label = label;
            Name = name;
            Description = description;
            UnitPrice = unitPrice;
            DiscountPerUnit = discountPerUnit;
            ProductIds = productIds ?? Array.Empty<Guid>();
            
            IncrementVersion();
            LastUpdateDate = lastUpdateDate;
        }

        // TODO: extract to VersionedEntity
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