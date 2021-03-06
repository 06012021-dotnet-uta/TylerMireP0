// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistence;

namespace Persistence.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20210610045456_Debug4")]
    partial class Debug4
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.Customer", b =>
                {
                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("customerID");

                    b.Property<int?>("DefaultLocationId")
                        .HasColumnType("int")
                        .HasColumnName("defaultLocationID");

                    b.Property<string>("FirstName")
                        .HasMaxLength(25)
                        .IsUnicode(false)
                        .HasColumnType("varchar(25)")
                        .HasColumnName("firstName");

                    b.Property<string>("LastName")
                        .HasMaxLength(25)
                        .IsUnicode(false)
                        .HasColumnType("varchar(25)")
                        .HasColumnName("lastName");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("passwordHash");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .IsUnicode(false)
                        .HasColumnType("varchar(25)")
                        .HasColumnName("userName");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Domain.Location", b =>
                {
                    b.Property<Guid>("LocationId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("locationID");

                    b.Property<string>("LocationName")
                        .HasMaxLength(25)
                        .IsUnicode(false)
                        .HasColumnType("varchar(25)")
                        .HasColumnName("locationName");

                    b.HasKey("LocationId");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("Domain.LocationProductInventory", b =>
                {
                    b.Property<Guid>("LocationId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("locationID");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("productID");

                    b.Property<int?>("TotalProduct")
                        .HasColumnType("int")
                        .HasColumnName("totalProduct");

                    b.HasIndex("LocationId");

                    b.HasIndex("ProductId");

                    b.ToTable("LocationProductInventories");
                });

            modelBuilder.Entity("Domain.Order", b =>
                {
                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("orderID");

                    b.Property<Guid?>("LocationId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("locationID");

                    b.Property<int?>("OrderTotal")
                        .HasColumnType("int")
                        .HasColumnName("orderTotal");

                    b.HasKey("OrderId");

                    b.HasIndex("LocationId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Domain.Product", b =>
                {
                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("productID");

                    b.Property<string>("ProductDescription")
                        .HasMaxLength(250)
                        .IsUnicode(false)
                        .HasColumnType("varchar(250)")
                        .HasColumnName("productDescription");

                    b.Property<string>("ProductName")
                        .HasMaxLength(25)
                        .IsUnicode(false)
                        .HasColumnType("varchar(25)")
                        .HasColumnName("productName");

                    b.Property<double?>("ProductPrice")
                        .HasColumnType("float")
                        .HasColumnName("productPrice");

                    b.HasKey("ProductId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Domain.LocationProductInventory", b =>
                {
                    b.HasOne("Domain.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .HasConstraintName("FK__LocationP__locat__5070F446")
                        .IsRequired();

                    b.HasOne("Domain.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK__LocationP__produ__4F7CD00D")
                        .IsRequired();

                    b.Navigation("Location");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Domain.Order", b =>
                {
                    b.HasOne("Domain.Location", "Location")
                        .WithMany("Orders")
                        .HasForeignKey("LocationId")
                        .HasConstraintName("FK__Orders__location__4BAC3F29");

                    b.Navigation("Location");
                });

            modelBuilder.Entity("Domain.Location", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
