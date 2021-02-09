﻿// <auto-generated />
using System;
using GoodsReseller.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace GoodsReseller.Infrastructure.Migrations
{
    [DbContext(typeof(GoodsResellerDbContext))]
    [Migration("20210208191112_AddProductsTable")]
    partial class AddProductsTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Navigation("CreationDate");

                    b.Navigation("DiscountPerUnit");

                    b.Navigation("LastUpdateDate");

                    b.Navigation("UnitPrice");
                });
#pragma warning restore 612, 618
        }
    }
}