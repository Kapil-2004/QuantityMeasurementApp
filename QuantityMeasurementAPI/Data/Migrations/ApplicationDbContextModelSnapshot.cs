using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using QuantityMeasurementAPI.Data;

#nullable disable

namespace QuantityMeasurementAPI.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("QuantityMeasurementModelLayer.Entities.QuantityMeasurementEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("ErrorMessage")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<bool>("HasError")
                        .HasColumnType("bit");

                    b.Property<string>("Operand1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Operand2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Operation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Result")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CreatedAt")
                        .HasDatabaseName("IX_QuantityMeasurements_CreatedAt");

                    b.HasIndex("Operation")
                        .HasDatabaseName("IX_QuantityMeasurements_Operation");

                    b.ToTable("QuantityMeasurements");
                });
#pragma warning restore 612, 618
        }
    }
}
