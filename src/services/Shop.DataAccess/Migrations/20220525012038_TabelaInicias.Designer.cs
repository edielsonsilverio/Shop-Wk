﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shop.DataAccess.Context;

#nullable disable

namespace Shop.DataAccess.Migrations
{
    [DbContext(typeof(ShopDbContext))]
    [Migration("20220525012038_TabelaInicias")]
    partial class TabelaInicias
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Shop.Business.Models.Categoria", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36)
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Categoria", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("fd92420e-eb29-409c-80a7-07b92ee16861"),
                            DataCadastro = new DateTime(2022, 5, 24, 21, 20, 38, 152, DateTimeKind.Local).AddTicks(2641),
                            Nome = "Roupa"
                        },
                        new
                        {
                            Id = new Guid("8a2addd9-d584-424f-bfff-760e7e6df8df"),
                            DataCadastro = new DateTime(2022, 5, 24, 21, 20, 38, 152, DateTimeKind.Local).AddTicks(2661),
                            Nome = "Brinquedo"
                        },
                        new
                        {
                            Id = new Guid("d11fd92b-64a7-4e96-8668-0d2e35fe089b"),
                            DataCadastro = new DateTime(2022, 5, 24, 21, 20, 38, 152, DateTimeKind.Local).AddTicks(2662),
                            Nome = "Sapato"
                        });
                });

            modelBuilder.Entity("Shop.Business.Models.Produto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36)
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CategoriaId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("DataCadastro")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<decimal>("EstoqueMaximo")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("EstoqueMinimo")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<decimal>("QuantidadeEstoque")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("ValorCompra")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ValorVenda")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.ToTable("Produto", (string)null);
                });

            modelBuilder.Entity("Shop.Business.Models.Produto", b =>
                {
                    b.HasOne("Shop.Business.Models.Categoria", "Categoria")
                        .WithMany("Produtos")
                        .HasForeignKey("CategoriaId")
                        .IsRequired();

                    b.Navigation("Categoria");
                });

            modelBuilder.Entity("Shop.Business.Models.Categoria", b =>
                {
                    b.Navigation("Produtos");
                });
#pragma warning restore 612, 618
        }
    }
}
