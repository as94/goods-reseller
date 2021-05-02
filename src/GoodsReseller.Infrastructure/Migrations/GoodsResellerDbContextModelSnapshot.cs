﻿// <auto-generated />
using System;
using GoodsReseller.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace GoodsReseller.Infrastructure.Migrations
{
    [DbContext(typeof(GoodsResellerDbContext))]
    partial class GoodsResellerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("GoodsReseller.AuthContext.Domain.Users.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("boolean");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("users");
                });

            modelBuilder.Entity("GoodsReseller.DataCatalogContext.Models.Products.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(1024)");

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("boolean");

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProductIds")
                        .HasColumnType("json");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("products");
                });

            modelBuilder.Entity("GoodsReseller.OrderContext.Domain.Orders.Entities.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .HasColumnType("json");

                    b.Property<string>("CustomerInfo")
                        .HasColumnType("json");

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("boolean");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("orders");
                });

            modelBuilder.Entity("GoodsReseller.OrderContext.Domain.Orders.Entities.OrderItem", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("order_items");
                });

            modelBuilder.Entity("GoodsReseller.SupplyContext.Domain.Supplies.Entities.Supply", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("boolean");

                    b.Property<string>("SupplierInfo")
                        .HasColumnType("json");

                    b.HasKey("Id");

                    b.ToTable("supplies");
                });

            modelBuilder.Entity("GoodsReseller.SupplyContext.Domain.Supplies.Entities.SupplyItem", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("boolean");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("SupplyId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("SupplyId");

                    b.ToTable("supply_items");
                });

            modelBuilder.Entity("GoodsReseller.AuthContext.Domain.Users.Entities.User", b =>
                {
                    b.OwnsOne("GoodsReseller.AuthContext.Domain.Users.ValueObjects.PasswordHash", "PasswordHash", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("varchar(1024)");

                            b1.HasKey("UserId");

                            b1.ToTable("users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsOne("GoodsReseller.AuthContext.Domain.Users.ValueObjects.Role", "Role", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uuid");

                            b1.Property<int>("Id")
                                .HasColumnType("integer");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("varchar(255)");

                            b1.HasKey("UserId");

                            b1.ToTable("users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsOne("GoodsReseller.SeedWork.ValueObjects.DateValueObject", "CreationDate", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uuid");

                            b1.Property<DateTime>("Date")
                                .HasColumnType("timestamp without time zone")
                                .HasColumnName("CreationDate");

                            b1.Property<DateTime>("DateUtc")
                                .HasColumnType("timestamp without time zone")
                                .HasColumnName("CreationDateUtc");

                            b1.HasKey("UserId");

                            b1.ToTable("users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsOne("GoodsReseller.SeedWork.ValueObjects.DateValueObject", "LastUpdateDate", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uuid");

                            b1.Property<DateTime>("Date")
                                .HasColumnType("timestamp without time zone")
                                .HasColumnName("LastUpdateDate");

                            b1.Property<DateTime>("DateUtc")
                                .HasColumnType("timestamp without time zone")
                                .HasColumnName("LastUpdateDateUtc");

                            b1.HasKey("UserId");

                            b1.ToTable("users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("CreationDate");

                    b.Navigation("LastUpdateDate");

                    b.Navigation("PasswordHash");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("GoodsReseller.DataCatalogContext.Models.Products.Product", b =>
                {
                    b.OwnsOne("GoodsReseller.SeedWork.ValueObjects.DateValueObject", "CreationDate", b1 =>
                        {
                            b1.Property<Guid>("ProductId")
                                .HasColumnType("uuid");

                            b1.Property<DateTime>("Date")
                                .HasColumnType("timestamp without time zone")
                                .HasColumnName("CreationDate");

                            b1.Property<DateTime>("DateUtc")
                                .HasColumnType("timestamp without time zone")
                                .HasColumnName("CreationDateUtc");

                            b1.HasKey("ProductId");

                            b1.ToTable("products");

                            b1.WithOwner()
                                .HasForeignKey("ProductId");
                        });

                    b.OwnsOne("GoodsReseller.SeedWork.ValueObjects.DateValueObject", "LastUpdateDate", b1 =>
                        {
                            b1.Property<Guid>("ProductId")
                                .HasColumnType("uuid");

                            b1.Property<DateTime>("Date")
                                .HasColumnType("timestamp without time zone")
                                .HasColumnName("LastUpdateDate");

                            b1.Property<DateTime>("DateUtc")
                                .HasColumnType("timestamp without time zone")
                                .HasColumnName("LastUpdateDateUtc");

                            b1.HasKey("ProductId");

                            b1.ToTable("products");

                            b1.WithOwner()
                                .HasForeignKey("ProductId");
                        });

                    b.OwnsOne("GoodsReseller.SeedWork.ValueObjects.Discount", "DiscountPerUnit", b1 =>
                        {
                            b1.Property<Guid>("ProductId")
                                .HasColumnType("uuid");

                            b1.Property<decimal>("Value")
                                .HasColumnType("numeric")
                                .HasColumnName("DiscountPerUnitValue");

                            b1.HasKey("ProductId");

                            b1.ToTable("products");

                            b1.WithOwner()
                                .HasForeignKey("ProductId");
                        });

                    b.OwnsOne("GoodsReseller.SeedWork.ValueObjects.Money", "AddedCost", b1 =>
                        {
                            b1.Property<Guid>("ProductId")
                                .HasColumnType("uuid");

                            b1.Property<decimal>("Value")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("numeric")
                                .HasDefaultValue(0m)
                                .HasColumnName("AddedCostValue");

                            b1.HasKey("ProductId");

                            b1.ToTable("products");

                            b1.WithOwner()
                                .HasForeignKey("ProductId");
                        });

                    b.OwnsOne("GoodsReseller.SeedWork.ValueObjects.Money", "UnitPrice", b1 =>
                        {
                            b1.Property<Guid>("ProductId")
                                .HasColumnType("uuid");

                            b1.Property<decimal>("Value")
                                .HasColumnType("numeric")
                                .HasColumnName("UnitPriceValue");

                            b1.HasKey("ProductId");

                            b1.ToTable("products");

                            b1.WithOwner()
                                .HasForeignKey("ProductId");
                        });

                    b.Navigation("AddedCost");

                    b.Navigation("CreationDate");

                    b.Navigation("DiscountPerUnit");

                    b.Navigation("LastUpdateDate");

                    b.Navigation("UnitPrice");
                });

            modelBuilder.Entity("GoodsReseller.OrderContext.Domain.Orders.Entities.Order", b =>
                {
                    b.OwnsOne("GoodsReseller.OrderContext.Domain.Orders.ValueObjects.OrderStatus", "Status", b1 =>
                        {
                            b1.Property<Guid>("OrderId")
                                .HasColumnType("uuid");

                            b1.Property<int>("Id")
                                .HasColumnType("integer");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("varchar(255)");

                            b1.HasKey("OrderId");

                            b1.ToTable("orders");

                            b1.WithOwner()
                                .HasForeignKey("OrderId");
                        });

                    b.OwnsOne("GoodsReseller.SeedWork.ValueObjects.DateValueObject", "CreationDate", b1 =>
                        {
                            b1.Property<Guid>("OrderId")
                                .HasColumnType("uuid");

                            b1.Property<DateTime>("Date")
                                .HasColumnType("timestamp without time zone")
                                .HasColumnName("CreationDate");

                            b1.Property<DateTime>("DateUtc")
                                .HasColumnType("timestamp without time zone")
                                .HasColumnName("CreationDateUtc");

                            b1.HasKey("OrderId");

                            b1.ToTable("orders");

                            b1.WithOwner()
                                .HasForeignKey("OrderId");
                        });

                    b.OwnsOne("GoodsReseller.SeedWork.ValueObjects.DateValueObject", "LastUpdateDate", b1 =>
                        {
                            b1.Property<Guid>("OrderId")
                                .HasColumnType("uuid");

                            b1.Property<DateTime>("Date")
                                .HasColumnType("timestamp without time zone")
                                .HasColumnName("LastUpdateDate");

                            b1.Property<DateTime>("DateUtc")
                                .HasColumnType("timestamp without time zone")
                                .HasColumnName("LastUpdateDateUtc");

                            b1.HasKey("OrderId");

                            b1.ToTable("orders");

                            b1.WithOwner()
                                .HasForeignKey("OrderId");
                        });

                    b.OwnsOne("GoodsReseller.SeedWork.ValueObjects.Money", "AddedCost", b1 =>
                        {
                            b1.Property<Guid>("OrderId")
                                .HasColumnType("uuid");

                            b1.Property<decimal>("Value")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("numeric")
                                .HasDefaultValue(0m)
                                .HasColumnName("AddedCostValue");

                            b1.HasKey("OrderId");

                            b1.ToTable("orders");

                            b1.WithOwner()
                                .HasForeignKey("OrderId");
                        });

                    b.OwnsOne("GoodsReseller.SeedWork.ValueObjects.Money", "DeliveryCost", b1 =>
                        {
                            b1.Property<Guid>("OrderId")
                                .HasColumnType("uuid");

                            b1.Property<decimal>("Value")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("numeric")
                                .HasDefaultValue(0m)
                                .HasColumnName("DeliveryCostValue");

                            b1.HasKey("OrderId");

                            b1.ToTable("orders");

                            b1.WithOwner()
                                .HasForeignKey("OrderId");
                        });

                    b.OwnsOne("GoodsReseller.SeedWork.ValueObjects.Money", "TotalCost", b1 =>
                        {
                            b1.Property<Guid>("OrderId")
                                .HasColumnType("uuid");

                            b1.Property<decimal>("Value")
                                .HasColumnType("numeric")
                                .HasColumnName("TotalCostValue");

                            b1.HasKey("OrderId");

                            b1.ToTable("orders");

                            b1.WithOwner()
                                .HasForeignKey("OrderId");
                        });

                    b.Navigation("AddedCost");

                    b.Navigation("CreationDate");

                    b.Navigation("DeliveryCost");

                    b.Navigation("LastUpdateDate");

                    b.Navigation("Status");

                    b.Navigation("TotalCost");
                });

            modelBuilder.Entity("GoodsReseller.OrderContext.Domain.Orders.Entities.OrderItem", b =>
                {
                    b.HasOne("GoodsReseller.OrderContext.Domain.Orders.Entities.Order", null)
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId");

                    b.OwnsOne("GoodsReseller.SeedWork.ValueObjects.DateValueObject", "CreationDate", b1 =>
                        {
                            b1.Property<Guid>("OrderItemId")
                                .HasColumnType("uuid");

                            b1.Property<DateTime>("Date")
                                .HasColumnType("timestamp without time zone")
                                .HasColumnName("CreationDate");

                            b1.Property<DateTime>("DateUtc")
                                .HasColumnType("timestamp without time zone")
                                .HasColumnName("CreationDateUtc");

                            b1.HasKey("OrderItemId");

                            b1.ToTable("order_items");

                            b1.WithOwner()
                                .HasForeignKey("OrderItemId");
                        });

                    b.OwnsOne("GoodsReseller.SeedWork.ValueObjects.DateValueObject", "LastUpdateDate", b1 =>
                        {
                            b1.Property<Guid>("OrderItemId")
                                .HasColumnType("uuid");

                            b1.Property<DateTime>("Date")
                                .HasColumnType("timestamp without time zone")
                                .HasColumnName("LastUpdateDate");

                            b1.Property<DateTime>("DateUtc")
                                .HasColumnType("timestamp without time zone")
                                .HasColumnName("LastUpdateDateUtc");

                            b1.HasKey("OrderItemId");

                            b1.ToTable("order_items");

                            b1.WithOwner()
                                .HasForeignKey("OrderItemId");
                        });

                    b.OwnsOne("GoodsReseller.SeedWork.ValueObjects.Discount", "DiscountPerUnit", b1 =>
                        {
                            b1.Property<Guid>("OrderItemId")
                                .HasColumnType("uuid");

                            b1.Property<decimal>("Value")
                                .HasColumnType("numeric")
                                .HasColumnName("DiscountPerUnitValue");

                            b1.HasKey("OrderItemId");

                            b1.ToTable("order_items");

                            b1.WithOwner()
                                .HasForeignKey("OrderItemId");
                        });

                    b.OwnsOne("GoodsReseller.SeedWork.ValueObjects.Money", "UnitPrice", b1 =>
                        {
                            b1.Property<Guid>("OrderItemId")
                                .HasColumnType("uuid");

                            b1.Property<decimal>("Value")
                                .HasColumnType("numeric")
                                .HasColumnName("UnitPriceValue");

                            b1.HasKey("OrderItemId");

                            b1.ToTable("order_items");

                            b1.WithOwner()
                                .HasForeignKey("OrderItemId");
                        });

                    b.OwnsOne("GoodsReseller.SeedWork.ValueObjects.Quantity", "Quantity", b1 =>
                        {
                            b1.Property<Guid>("OrderItemId")
                                .HasColumnType("uuid");

                            b1.Property<int>("Value")
                                .HasColumnType("integer")
                                .HasColumnName("QuantityValue");

                            b1.HasKey("OrderItemId");

                            b1.ToTable("order_items");

                            b1.WithOwner()
                                .HasForeignKey("OrderItemId");
                        });

                    b.Navigation("CreationDate");

                    b.Navigation("DiscountPerUnit");

                    b.Navigation("LastUpdateDate");

                    b.Navigation("Quantity");

                    b.Navigation("UnitPrice");
                });

            modelBuilder.Entity("GoodsReseller.SupplyContext.Domain.Supplies.Entities.Supply", b =>
                {
                    b.OwnsOne("GoodsReseller.SeedWork.ValueObjects.DateValueObject", "CreationDate", b1 =>
                        {
                            b1.Property<Guid>("SupplyId")
                                .HasColumnType("uuid");

                            b1.Property<DateTime>("Date")
                                .HasColumnType("timestamp without time zone")
                                .HasColumnName("CreationDate");

                            b1.Property<DateTime>("DateUtc")
                                .HasColumnType("timestamp without time zone")
                                .HasColumnName("CreationDateUtc");

                            b1.HasKey("SupplyId");

                            b1.ToTable("supplies");

                            b1.WithOwner()
                                .HasForeignKey("SupplyId");
                        });

                    b.OwnsOne("GoodsReseller.SeedWork.ValueObjects.DateValueObject", "LastUpdateDate", b1 =>
                        {
                            b1.Property<Guid>("SupplyId")
                                .HasColumnType("uuid");

                            b1.Property<DateTime>("Date")
                                .HasColumnType("timestamp without time zone")
                                .HasColumnName("LastUpdateDate");

                            b1.Property<DateTime>("DateUtc")
                                .HasColumnType("timestamp without time zone")
                                .HasColumnName("LastUpdateDateUtc");

                            b1.HasKey("SupplyId");

                            b1.ToTable("supplies");

                            b1.WithOwner()
                                .HasForeignKey("SupplyId");
                        });

                    b.OwnsOne("GoodsReseller.SeedWork.ValueObjects.Money", "TotalCost", b1 =>
                        {
                            b1.Property<Guid>("SupplyId")
                                .HasColumnType("uuid");

                            b1.Property<decimal>("Value")
                                .HasColumnType("numeric")
                                .HasColumnName("TotalCostValue");

                            b1.HasKey("SupplyId");

                            b1.ToTable("supplies");

                            b1.WithOwner()
                                .HasForeignKey("SupplyId");
                        });

                    b.Navigation("CreationDate");

                    b.Navigation("LastUpdateDate");

                    b.Navigation("TotalCost");
                });

            modelBuilder.Entity("GoodsReseller.SupplyContext.Domain.Supplies.Entities.SupplyItem", b =>
                {
                    b.HasOne("GoodsReseller.SupplyContext.Domain.Supplies.Entities.Supply", null)
                        .WithMany("SupplyItems")
                        .HasForeignKey("SupplyId");

                    b.OwnsOne("GoodsReseller.SeedWork.ValueObjects.DateValueObject", "CreationDate", b1 =>
                        {
                            b1.Property<Guid>("SupplyItemId")
                                .HasColumnType("uuid");

                            b1.Property<DateTime>("Date")
                                .HasColumnType("timestamp without time zone")
                                .HasColumnName("CreationDate");

                            b1.Property<DateTime>("DateUtc")
                                .HasColumnType("timestamp without time zone")
                                .HasColumnName("CreationDateUtc");

                            b1.HasKey("SupplyItemId");

                            b1.ToTable("supply_items");

                            b1.WithOwner()
                                .HasForeignKey("SupplyItemId");
                        });

                    b.OwnsOne("GoodsReseller.SeedWork.ValueObjects.DateValueObject", "LastUpdateDate", b1 =>
                        {
                            b1.Property<Guid>("SupplyItemId")
                                .HasColumnType("uuid");

                            b1.Property<DateTime>("Date")
                                .HasColumnType("timestamp without time zone")
                                .HasColumnName("LastUpdateDate");

                            b1.Property<DateTime>("DateUtc")
                                .HasColumnType("timestamp without time zone")
                                .HasColumnName("LastUpdateDateUtc");

                            b1.HasKey("SupplyItemId");

                            b1.ToTable("supply_items");

                            b1.WithOwner()
                                .HasForeignKey("SupplyItemId");
                        });

                    b.OwnsOne("GoodsReseller.SeedWork.ValueObjects.Discount", "DiscountPerUnit", b1 =>
                        {
                            b1.Property<Guid>("SupplyItemId")
                                .HasColumnType("uuid");

                            b1.Property<decimal>("Value")
                                .HasColumnType("numeric")
                                .HasColumnName("DiscountPerUnitValue");

                            b1.HasKey("SupplyItemId");

                            b1.ToTable("supply_items");

                            b1.WithOwner()
                                .HasForeignKey("SupplyItemId");
                        });

                    b.OwnsOne("GoodsReseller.SeedWork.ValueObjects.Money", "UnitPrice", b1 =>
                        {
                            b1.Property<Guid>("SupplyItemId")
                                .HasColumnType("uuid");

                            b1.Property<decimal>("Value")
                                .HasColumnType("numeric")
                                .HasColumnName("UnitPriceValue");

                            b1.HasKey("SupplyItemId");

                            b1.ToTable("supply_items");

                            b1.WithOwner()
                                .HasForeignKey("SupplyItemId");
                        });

                    b.OwnsOne("GoodsReseller.SeedWork.ValueObjects.Quantity", "Quantity", b1 =>
                        {
                            b1.Property<Guid>("SupplyItemId")
                                .HasColumnType("uuid");

                            b1.Property<int>("Value")
                                .HasColumnType("integer")
                                .HasColumnName("QuantityValue");

                            b1.HasKey("SupplyItemId");

                            b1.ToTable("supply_items");

                            b1.WithOwner()
                                .HasForeignKey("SupplyItemId");
                        });

                    b.Navigation("CreationDate");

                    b.Navigation("DiscountPerUnit");

                    b.Navigation("LastUpdateDate");

                    b.Navigation("Quantity");

                    b.Navigation("UnitPrice");
                });

            modelBuilder.Entity("GoodsReseller.OrderContext.Domain.Orders.Entities.Order", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("GoodsReseller.SupplyContext.Domain.Supplies.Entities.Supply", b =>
                {
                    b.Navigation("SupplyItems");
                });
#pragma warning restore 612, 618
        }
    }
}
