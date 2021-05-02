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
        public Money AddedCost { get; private set; }
        public Guid[] ProductIds { get; private set; }
        
        public Product(
            Guid id,
            int version,
            string label,
            string name,
            string description,
            Money unitPrice,
            Discount discountPerUnit,
            Money addedCost = null,
            Guid[] productIds = null)
            : this(id, version, label, name, description, productIds)
        {
            UnitPrice = unitPrice;
            DiscountPerUnit = discountPerUnit;
            AddedCost = addedCost;
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
            ProductIds = productIds ?? Array.Empty<Guid>();
        }

        public void Update(
            string label,
            string name,
            string description,
            Money unitPrice,
            Discount discountPerUnit,
            Money addedCost = null,
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

            Label = label;
            Name = name;
            Description = description;
            UnitPrice = unitPrice;
            DiscountPerUnit = discountPerUnit;
            AddedCost = addedCost;
            ProductIds = productIds ?? Array.Empty<Guid>();
            
            IncrementVersion();
            LastUpdateDate = new DateValueObject();
        }
    }
}