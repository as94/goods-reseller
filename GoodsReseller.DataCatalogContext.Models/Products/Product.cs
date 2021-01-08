using System;
using GoodsReseller.SeedWork;
using GoodsReseller.SeedWork.ValueObjects;

namespace GoodsReseller.DataCatalogContext.Models.Products
{
    public sealed class Product : VersionedEntity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Money UnitPrice { get; private set; }
        public Factor DiscountPerUnit { get; private set; }
        
        public bool IsRemoved { get; private set; }
        
        public Product(
            Guid id,
            int version,
            string name,
            string description,
            Money unitPrice,
            Factor discountPerUnit)
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
            IsRemoved = false;
        }

        public static Product Restore(
            Guid id,
            int version,
            string name,
            string description,
            Money unitPrice,
            Factor discountPerUnit,
            bool isRemoved)
        {
            return new Product(
                id,
                version,
                name,
                description,
                unitPrice,
                discountPerUnit)
            {
                IsRemoved = isRemoved
            };
        }

        public void Update(string name, string description, Money unitPrice, Factor discountPerUnit)
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
            
            Name = name;
            Description = description;
            UnitPrice = unitPrice;
            DiscountPerUnit = discountPerUnit;
            
            IncrementVersion();
        }

        public void Remove()
        {
            if (IsRemoved)
            {
                return;
            }
            
            IsRemoved = true;
            IncrementVersion();
        }
    }
}